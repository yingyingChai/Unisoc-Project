using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spreadtrum.LHD.DAL.Lots;
using Spreadtrum.LHD.Entity.Lots;
using System.Data;
using KaYi.FileSystem.Services;
using KaYi.FileSystem.Model;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Globalization;

namespace Spreadtrum.LHD.Business
{
  public  class Lot_TransformService
    {
        private Lot_TransformedGateway way = new Lot_TransformedGateway();
        private TransformedCommentGateway commentway = new TransformedCommentGateway();
        private Wafer_SbinGateway sbinWay = new Wafer_SbinGateway();
        private WaferGeteway2 waferWay2 = new WaferGeteway2();
        public IList<Lot_Transformed> GetAllLots(Lot_TransformedQuery lot, int pageSize, int PageIndex, out int recordCount)
        {
            return this.way.GetAllLots(lot, pageSize, PageIndex,out recordCount);
        }
        public Lot_Transformed GetTransformById(string id)
        {
            return this.way.GetTransformById(id);
        }
        public  IList<TranformedComment> GetCommentsByLotID(string lotID, string commentType, bool osatUser, int pageIndex, int pageSize, out int recordCount)
        {
            return ReadExtendPropertiesOfComments(commentway.GetCommentsBy(lotID, commentType, osatUser, "CreateTime", true, pageIndex, pageSize, out recordCount));
        }
        public  IList<TranformedComment> ReadExtendPropertiesOfComments(IList<TranformedComment> comments)
        {
            for (int i = 0; i <= (comments.Count - 1); i++)
            {
                comments[i] = ReadExtendPropertiesOfComment(comments[i]);
            }
            return comments;
        }
        //comment files
        public  TranformedComment ReadExtendPropertiesOfComment(TranformedComment comment)
        {
            int recordCount = 0;
            comment.Attachments = FileSystemService.GetFilesBy("", "", comment.CommentID, "", "", FileStates.Normal, FileTypes.File, "", false, 0, 0x270f, out recordCount);
            return comment;
        }

        
        public int SaveLot(Lot_Transformed lot)
        {
            return way.SaveLot(lot);
        }
        public Lot_Transformed ReadJson(string jsonStr, IList<string> messages)
        {
            JObject obj = (JObject)JsonConvert.DeserializeObject(jsonStr);
            if (obj["LOT_JUDGEMENT"] != null)
            {
                try
                {
                    JToken lotJudegementNode = obj["LOT_JUDGEMENT"][0];
                    messages.Add("Starting analysis the  message.\r\n");
                    messages.Add("Basic information anylysis finished.\r\n");
                    messages.Add("Status anylysis finished.\r\n");
                    messages.Add("Lot anylysis finished.\r\n");
                    messages.Add("Wafer anylysis finished.\r\n");
                    messages.Add("MetaData anylysis finished.\r\n");
                    return BindLotInfo((JObject)JsonConvert.DeserializeObject(lotJudegementNode.ToString()));
                }
                catch (Exception ex)
                {
                    messages.Add(ex.ToString() + "\r\n");
                    return null;
                }
            }
            return null;
        }
        private Lot_Transformed ReadMeta(JObject metaObj)
        {

            try
            {
                if (metaObj["metadata"] != null)
                {
                    
                    List<LotMeteModel> metaList = JsonConvert.DeserializeObject<List<LotMeteModel>>(metaObj["metadata"].ToString());
                    if (metaList != null && metaList.Count > 0)
                    {
                        Lot_Transformed mata = new Lot_Transformed();
                        List<string> waferList = new List<string>();
                        int waferidcount = 0;
                        List<string> allBinList = new List<string>();
                        List<string> allBinLimtList = new List<string>();
                        List<string> allBinPercentList = new List<string>();
                        int index = 0;
                        string waferid = "";
                        foreach (LotMeteModel item in metaList)
                        {
                            if (item.key == EnumLotMeta.osat.ToString())
                            {
                                mata.Vendor = item.values[0];
                            }
                            #region allitem
                            if (item.key == EnumLotMeta.allItem.ToString())
                            {
                                index = 0;
                                waferid = "";
                                foreach (string s in item.values)
                                {
                                    index++;
                                    if (index > 1)
                                    {
                                        if (!waferid.Equals(s))
                                        {
                                            waferList.Add(s);
                                            waferidcount = 0;
                                        }
                                        waferidcount++;
                                    }
                                    else
                                    {
                                        mata.WaferCode = s;
                                    }
                                    waferid = s;
                                }
                            }
                            #endregion
                            if (item.key == EnumLotMeta.allBin.ToString())
                            {
                                allBinList = item.values;
                                allBinList.RemoveAt(0);
                            }
                            if (item.key == EnumLotMeta.allBinLimit.ToString())
                            {
                                allBinLimtList = item.values;
                                allBinLimtList.RemoveAt(0);
                            }
                            if (item.key == EnumLotMeta.allBinPercent.ToString())
                            {
                                allBinPercentList = item.values;
                                allBinPercentList.RemoveAt(0);
                            }
                        }
                        mata.WaferList = waferList;
                        mata.WaferidCount = waferidcount;
                        mata.AllBinList = allBinList;
                        mata.AllBinLimtList = allBinLimtList;
                        mata.AllBinPercentList = allBinPercentList;
                        return mata;
                    }
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        private Lot_Transformed BindLotInfo(JObject obj)
        {

            Lot_Transformed lot = ReadMeta(obj);
            string lotid = Guid.NewGuid().ToString().Replace("-", "");
            lot.ID = lotid;
           // lot.WaferCode = lot.WaferCode;
            //lot.Vendor = lot.Vendor;
            lot.ProductName = obj["product"] != null ? obj["product"].ToString() : "";
            lot.LotId = obj["lotId"] != null ? obj["lotId"].ToString() : "";
            string dicision = obj["decision"] != null ? obj["decision"].ToString() : "";
            lot.AutoJudeResult = dicision;
            int status = lot.AutoJudeResult.ToLower() == LotAutoJudgement.hold.ToString() ? (int)WaferStatus.WaitPE : (int)WaferStatus.Close;
            lot.Status = status;
            lot.WfCount = lot.WaferList.Count;
            if (obj["stats"] != null)
            {
                Dictionary<string, string> dicstats = JsonConvert.DeserializeObject<Dictionary<string, string>>(obj["stats"].ToString());
                decimal yield= dicstats["passPercent"] != null ? decimal.Parse(Math.Round(Convert.ToDouble(dicstats["passPercent"].ToString()), 2, MidpointRounding.AwayFromZero).ToString("f2")) : 0;
                lot.Yield = yield;// (dicstats["passPercent"] != null) ? Math.Round(Convert.ToDouble(dicstats["passPercent"].ToString()), 2, MidpointRounding.AwayFromZero) : 0.00;
            }
            if (obj["lotFinishTime"] != null)
            {
                lot.CompletionDate = DateTime.ParseExact(obj["lotFinishTime"].ToString(), "yyyyMMdd-HHmmss.fff", CultureInfo.CurrentCulture); 
            }
            if (obj["creationDate"] != null)
            {
                lot.UploadDate = DateTime.ParseExact(obj["creationDate"].ToString(), "yyyyMMdd-HHmmss.fff", CultureInfo.CurrentCulture);
            }
            else {
                lot.UploadDate = DateTime.Now.ToLocalTime();
            }
            if (obj["decisionReason"] != null)
            {
                lot.HoldReason = obj["decisionReason"].ToString();
            }
            lot.Stage = obj["stage"] != null ? obj["stage"].ToString() : "";
            lot.TestProgram = obj["programName"] != null ? obj["programName"].ToString() : "";//obj["temperature"] != null ? obj["temperature"].ToString() : "";
            lot.TesterID = obj["tester"] != null ? obj["tester"].ToString() : "";
            lot.Platform = obj["testerType"] != null ? obj["testerType"].ToString() : "";
            lot.LBNO = obj["probecard"] != null ? obj["probecard"].ToString() : "";
            lot.Url = obj["url"] != null ? obj["url"].ToString() : "";
            lot.CreateDate = DateTime.Now.ToLocalTime();
            if (dicision.ToUpper().Equals("NORMAL"))
            {
                lot.OperatorStatus = (int)OperationStatus.Release;
            }
            else {
                lot.OperatorStatus = (int)OperationStatus.Hold;
            }
            lot.RecordType = "Record";
            lot.VersionID = 0;
            lot.Program = obj["programName"] != null ? obj["programName"].ToString() : "";
            if (obj["startTime"] != null)
            {
                lot.StartTime = DateTime.ParseExact(obj["startTime"].ToString(), "yyyyMMdd-HHmmss.fff", CultureInfo.CurrentCulture); ;
            }
            return lot;
        }
        private void SaveWafer(Lot_Transformed lot)
        {
           
            List<string> waferList = lot.WaferList;
            if (waferList != null && waferList.Count>0) {
                SqlWafer wafer = null;
                int index = 0;
                string waferid = "";
                int waferIndex = 0;
                List<string> allBinPercentList = lot.AllBinPercentList;
                int waferidcount = lot.WaferidCount;
                foreach (string s in waferList)
                {
                    waferid = Guid.NewGuid().ToString().Replace("-", "");
                    wafer = new SqlWafer();
                    wafer.ID = waferid;
                    wafer.TransformID =lot.ID;
                    wafer.WaferID = s.Substring(lot.LotId.Length,s.Length-lot.LotId.Length);
                    wafer.Status = lot.Status;
                    wafer.HoldReason = lot.HoldReason;
                   // stryield=allBinPercentList[index * waferidcount] != null ? Math.Round(Convert.ToDouble(allBinPercentList[index * waferidcount].ToString()), 2, MidpointRounding.AwayFromZero).ToString("f2") : "0";
                    wafer.Yield =allBinPercentList[index * waferidcount] != null ? decimal.Parse(Math.Round(Convert.ToDouble(allBinPercentList[index * waferidcount].ToString()), 2, MidpointRounding.AwayFromZero).ToString("f2")) : 0;
                    wafer.CompletionDate = lot.CompletionDate;
                    wafer.PEDispose = 0;
                    wafer.QADispose = 0;
                    wafer.SPRDDecision = 0;
                    wafer.CreateDate = DateTime.Now.ToLocalTime();
                    wafer.VendorConfirm = 0;
                    wafer.Program = lot.Program;
                    wafer.StartTime = lot.StartTime;
                    waferWay2.SaveWafer(wafer);
                    SaveWaferSbin(lot, waferid, ref waferIndex);
                    index++;
                }
            }
        }
        private void SaveWaferSbin(Lot_Transformed lot,string waferid,ref int waferIndex)
        {
            int waferidcount = lot.WaferidCount;
            List<string> allBinPercentList = lot.AllBinPercentList;
            List<string> allBinList = lot.AllBinList;
            List<string> allBinLimtList = lot.AllBinLimtList;
            Wafer_Sbin sbin = null;
            for (int i = 0; i < waferidcount; i++)
            {
                waferIndex++;
                if (waferIndex == 2 && i == 1)
                {
                    waferIndex = 1;
                }
                if (i > 0)
                {
                    sbin = new Wafer_Sbin();
                    sbin.ID = Guid.NewGuid().ToString().Replace("-", "");
                    sbin.WaferID = waferid;
                    sbin.LotID = lot.ID;
                    sbin.SbinValue = !string.IsNullOrEmpty(allBinPercentList[waferIndex]) ? Math.Round(Convert.ToDouble(allBinPercentList[waferIndex]), 2, MidpointRounding.AwayFromZero).ToString("f2") : "0";
                    sbin.IsTriggered = false;
                    sbin.SbinText = allBinList[i];
                    sbin.SbinLimit = !string.IsNullOrEmpty(allBinLimtList[i]) ? Math.Round(Convert.ToDouble(allBinLimtList[i]), 2, MidpointRounding.AwayFromZero).ToString("f2") : "0";
                    sbin.Sort = i;
                    sbin.CreatedTime = DateTime.Now.ToLocalTime();
                    sbinWay.SaveSBin(sbin);
                }
            }
        }
        public void SaveCPJsonlot(Lot_Transformed lot)
        {
            this.way.CPSocketSaveLot(lot);
            this.SaveWafer(lot);
            //jingying liu apply to colse email notification
            //if (lot.AutoJudeResult.ToUpper() != "NORMAL")
            //{
            //    NotificationService.CreateCpSPRDDisposeNotificationByUserRoleWhileNewLotArrived(lot, Entity.Users.UserRoles.PE, "New lot arrived, is pending your disposition.");
            //}
        }
    }
}
