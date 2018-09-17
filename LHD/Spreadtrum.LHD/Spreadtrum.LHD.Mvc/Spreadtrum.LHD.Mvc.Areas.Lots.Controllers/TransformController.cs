using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KaYi.Utilities;
using Newtonsoft.Json;
using Spreadtrum.LHD.Entity.Lots;
using Spreadtrum.LHD.Business;
using Spreadtrum.LHD.Mvc.Areas.Shared;
using Spreadtrum.LHD.Mvc.Export;
using KaYi.Web.Infrastructure.Model.System.HandlerResponses;
using System.Data;
using System.Configuration;
using System.Text;
using Spreadtrum.LHD.Entity.Users;
using Newtonsoft.Json.Linq;
using KaYi.FileSystem.Services;
using KaYi.FileSystem.Model;

namespace Spreadtrum.LHD.Mvc.Areas.Lots.Controllers
{
    public class TransformController : BaseController
    {
        private Lot_TransformService service = new Lot_TransformService();
        private TansformedCommentService commentService = new TansformedCommentService();
        private Wafer_sbinService sbinService = new Wafer_sbinService();
        private WaferService waferService = new WaferService();
       
        public ActionResult test()
        {
            string jsonStr = service.GetTransformById("test1").TestStr;
            IList<string> message = new List<string>();
            Lot_Transformed lot = service.ReadJson(jsonStr, message);
            service.SaveCPJsonlot(lot);
            #region
            //JObject obj = (JObject)JsonConvert.DeserializeObject(jsonStr);
            //Lot_Transformed lot = new Lot_Transformed();
            //lot.ProductName = obj["product"].ToString();
            //lot.LotId = obj["lotId"].ToString();
            //string str_completiondate = obj["lotFinishTime"].ToString();
            //DateTime dt = Convert.ToDateTime(str_completiondate.Substring(0, 4) + "-" + str_completiondate.Substring(4, 2) + "-" + str_completiondate.Substring(6, 2) + " " + str_completiondate.Substring(9, 2) + ":" + str_completiondate.Substring(11, 2) + ":" + str_completiondate.Substring(13, 2) + "." + str_completiondate.Substring(16, 3));
            //lot.CompletionDate = dt;
            //lot.AutoJudeResult = obj["decision"].ToString();
            //Dictionary<string, string> dicstats = JsonConvert.DeserializeObject<Dictionary<string, string>>(obj["stats"].ToString());
            //lot.Yield = (dicstats["passPercent"] != null) ? Math.Round(Convert.ToDouble(dicstats["passPercent"].ToString()), 2, MidpointRounding.AwayFromZero) : 0.0;
            //lot.Stage = obj["stage"].ToString();
            //lot.TestProgram = obj["temperature"].ToString();
            //lot.TesterID = obj["tester"].ToString();
            //lot.Platform = obj["testerType"].ToString();
            //lot.LBNO = obj["probecard"].ToString();
            //List<LotMeteModel> metaList = JsonConvert.DeserializeObject<List<LotMeteModel>>(obj["metadata"].ToString());
            //List<string> waferList = new List<string>();
            //int waferidcount = 0;
            //List<string> allBinList = new List<string>();
            //List<string> allBinLimtList = new List<string>();
            //List<string> allBinPercentList = new List<string>();
            //foreach (LotMeteModel item in metaList)
            //{
            //    if (item.key == EnumLotMeta.osat.ToString())
            //    {
            //        lot.Vendor = item.values[0];
            //    }
            //    if (item.key == EnumLotMeta.allItem.ToString())
            //    {
            //        int index = 0;
            //        string waferid = "";
            //        foreach (string s in item.values)
            //        {
            //            index++;
            //            if (index > 1)
            //            {
            //                if (!waferid.Equals(s))
            //                {
            //                    waferList.Add(s);
            //                    waferidcount = 0;
            //                }
            //                waferidcount++;
            //            }
            //            else
            //            {
            //                lot.WaferCode = s;
            //            }
            //            waferid = s;
            //        }
            //    }
            //    if (item.key == EnumLotMeta.allBin.ToString())
            //    {
            //        allBinList = item.values;
            //        allBinList.RemoveAt(0);
            //    }
            //    if (item.key == EnumLotMeta.allBinLimit.ToString())
            //    {
            //        allBinLimtList = item.values;
            //        allBinLimtList.RemoveAt(0);
            //    }
            //    if (item.key == EnumLotMeta.allBinPercent.ToString())
            //    {
            //        allBinPercentList = item.values;
            //        allBinPercentList.RemoveAt(0);
            //    }
            //}
            //lot.WfCount = waferList.Count;
            ////  lot.UploadDate = DateTime.Now.ToLocalTime();
            //lot.Url = obj["url"].ToString();
            //int status = lot.AutoJudeResult.ToLower() == LotAutoJudgement.hold.ToString() ? (int)WaferStatus.WaitPE : (int)WaferStatus.Close;
            //lot.Status = status;
            //string lotid = Guid.NewGuid().ToString().Replace("-", "");
            //lot.ID = lotid;
            //lot.CreateDate = DateTime.Now.ToLocalTime();
            //string holdReason = "";
            //if (obj["decisionReason"] != null)
            //{
            //    holdReason = obj["decisionReason"].ToString();
            //}
            //lot.HoldReason = holdReason;
            //int suc = service.SaveLot(lot);
            //if (suc >= 0)
            //{//添加wafer
            //    Response.Write("添加lot成功");
            //    SqlWafer wafer = null;
            //    int index = 0;
            //    int waferIndex = 0;
            //    foreach (string s in waferList)
            //    {
            //        string waferid = Guid.NewGuid().ToString().Replace("-", "");
            //        wafer = new SqlWafer();
            //        wafer.ID = waferid;
            //        wafer.TransformID = lotid;
            //        wafer.WaferID = s;
            //        wafer.Status = status;
            //        wafer.HoldReason = holdReason;
            //        wafer.Yield = allBinPercentList[index * waferidcount] != null ? float.Parse(Math.Round(Convert.ToDouble(allBinPercentList[index * waferidcount].ToString()), 2, MidpointRounding.AwayFromZero).ToString()) : 0;
            //        wafer.CompletionDate = dt;
            //        wafer.PEDispose = 0;
            //        wafer.QADispose = 0;
            //        wafer.SPRDDecision = 0;
            //        wafer.CreateDate = DateTime.Now.ToLocalTime();
            //        wafer.VendorConfirm = 0;
            //        wafer.Program = obj["Program"] != null ? obj["Program"].ToString() : "";
            //        if (obj["startTime"] != null)
            //        {
            //            string str_starttime = obj["startTime"].ToString();
            //            DateTime startdt = DateTime.Parse(str_starttime.Substring(0, 4) + "-" + str_starttime.Substring(4, 2) + "-" + str_starttime.Substring(6, 2) + " " + str_starttime.Substring(9, 2) + ":" + str_starttime.Substring(11, 2) + ":" + str_starttime.Substring(13, 2) + "." + str_starttime.Substring(16, 3));
            //            wafer.StartTime = startdt;
            //        }
            //        else
            //        {
            //            wafer.StartTime = DateTime.Now.ToLocalTime();
            //        }
            //        if (waferService.SaveWafer(wafer) >= 0)//保存sbin
            //        {
            //            Response.Write("添加wafer成功");
            //            for (int i = 0; i < waferidcount; i++)
            //            {
            //                waferIndex++;
            //                if (waferIndex == 2 && i == 1)
            //                {
            //                    waferIndex = 1;
            //                }
            //                if (i > 0)
            //                {

            //                    try
            //                    {
            //                        Wafer_Sbin sbin = new Wafer_Sbin();
            //                        sbin.ID = Guid.NewGuid().ToString().Replace("-", "");
            //                        sbin.WaferID = waferid;
            //                        sbin.LotID = lotid;
            //                        sbin.SbinValue = !string.IsNullOrEmpty(allBinPercentList[waferIndex]) ? Math.Round(Convert.ToDouble(allBinPercentList[waferIndex]), 2, MidpointRounding.AwayFromZero).ToString() : "0";
            //                        sbin.IsTriggered = false;
            //                        sbin.SbinText = allBinList[i];
            //                        sbin.SbinLimit = !string.IsNullOrEmpty(allBinLimtList[i]) ? Math.Round(Convert.ToDouble(allBinLimtList[i]), 2, MidpointRounding.AwayFromZero).ToString() : "0";
            //                        sbin.Sort = i;
            //                        sbin.CreatedTime = DateTime.Now.ToLocalTime();
            //                        sbinService.SaveSBin(sbin);
            //                        Response.Write("添加sbin成功");
            //                    }
            //                    catch (Exception e)
            //                    {
            //                        throw;
            //                    }
            //                }
            //            }
            //        }

            //        index++;
            //    }
            //}
            #endregion
            return View();
        }
        public ActionResult Index(string st="",int oc=0)
        {
            ViewData["searchType"] = st;
            ViewData["outClose"] = oc;
            //ViewData["OperatorStatus"] = os;
            VendorsService vs = new VendorsService();
            IList<Vendors> vendorList = vs.VendorList(VendorTypes.CP.ToString());
            return base.View(vendorList);
        }
        public ActionResult mIndex(string st = "", int oc = 0)
        {
            ViewData["searchType"] = st;
            ViewData["outClose"] = oc;
            //ViewData["OperatorStatus"] = os;
            VendorsService vs = new VendorsService();
            IList<Vendors> vendorList = vs.VendorList(VendorTypes.CP.ToString());
            return base.View(vendorList);
        }
        public ActionResult Search(int pageSize, int pageIndex)
        {
            Lot_TransformedQuery query = TransformQueryUtility.GetLotTransformQuery(base.Request);
            if (StringHelper.isNullOrEmpty(query.OrderBy))
            {
                query.OrderBy = "CreateDate";
                query.OrderDesc = true;
            }
            if (CurrentUserInfo.Role == UserRoles.Fab || CurrentUserInfo.Role == UserRoles.FabAdmin)
            {
              query.Osat= CurrentUserInfo.BUName;
              query.Status=(int)WaferStatus.WaitVendor;
              query.PeVendorState = (int)WaferStatus.WaitPEVendor;
            }
            int recordCount = 0;
            IList<Lot_Transformed> list = service.GetAllLots(query, pageSize, pageIndex, out recordCount);
            if (list != null && list.Count > 0) {
                DateTime dt = DateTime.Parse(DateTime.Now.ToLocalTime().ToShortDateString());
                foreach (Lot_Transformed lot in list)
                {
                    lot.StatusText = WaferHelper.waferStatusDes(lot.Status);
                    int recordcount = 0;
                    IList<File> listFile= FileSystemService.GetFilesBy(lot.ID, "", "", "", "", FileStates.NotSpecified, FileTypes.NotSpecifiled, "", false, 0xf423f, 0, out recordcount);
                    lot.FileCount =recordcount;
                    lot.DiesposRemark = waferService.LoadCountByDispose(lot.ID, lot.Status);
                    lot.dayCount = (dt - DateTime.Parse(lot.CompletionDate.ToShortDateString())).Days;
                }
            }
            var json = JsonConvert.SerializeObject(new
            {
                currentPage = pageIndex,
                totalPages = PagerUtility.GetPageCount(recordCount, pageSize),
                rows = list
            });
            base.Response.Write(json);
            return null;
            
        }

        public ActionResult ExportData(Lot_TransformedQuery query)
        {
            int recordCount = 0;
            string xlsName = string.Format("Transform_{0}", DateTime.Now.ToString("yyyyMMdd_hhmmss"));
            if (StringHelper.isNullOrEmpty(query.OrderBy))
            {
                query.OrderBy = "CreateDate";
                query.OrderDesc = true;
            }
            if (CurrentUserInfo.Role == UserRoles.Fab || CurrentUserInfo.Role == UserRoles.FabAdmin)
            {
                query.Osat = CurrentUserInfo.BUName;
                query.Status = (int)WaferStatus.WaitVendor;
            }
            IList<Lot_Transformed> list = service.GetAllLots(query, 0xf423f, 0, out recordCount);
            if (list != null && list.Count > 0)
            {
                foreach (Lot_Transformed lot in list)
                {
                    lot.StatusText = WaferHelper.waferStatusDes(lot.Status);
                }
            }
            string nextUrl = Exporthelper.GetExport<Lot_Transformed>("LOTS", list, "/Export/Excels/Transform/", "/Content/Exports/ExportTransform.xml", xlsName, 0, 0);
            ResponseTypes redirect = ResponseTypes.Redirect;
            TipTypes information = TipTypes.Information;
            base.Response.Write(new HandlerResponse("0", "导出Excel文件", redirect.ToString(), information.ToString(), nextUrl, "", "").GenerateJsonResponse());
            return null;
        }
        public ActionResult Detail(string id)
        {
            Lot_Transformed lot = service.GetTransformById(id);
            if (lot != null)
            {
                lot.StatusText = WaferHelper.waferStatusDes(lot.Status);
            }
            IList<Wafer_Sbin> list = sbinService.GetSbinTextByLotId(id);
            LotDetailModel model = new LotDetailModel();
            model.LotTransformed = lot;
            model.ListSbin = list;
            return base.View(model);
        }
        public ActionResult mDetail(string id)
        {
            Lot_Transformed lot = service.GetTransformById(id);
            if (lot != null)
            {
                lot.StatusText = WaferHelper.waferStatusDes(lot.Status);
            }
            IList<Wafer_Sbin> list = sbinService.GetSbinTextByLotId(id);
            LotDetailModel model = new LotDetailModel();
            model.LotTransformed = lot;
            model.ListSbin = list;
            return base.View(model);
        }
        public void AddComment()
        {
            string commentID = base.Request.Form["hidNewCommentID"];
            string s = base.Request.Form["txtComment"];
            s = base.Server.HtmlEncode(s).Replace("\n", "<br/>");
            bool internalOnly = base.Request.Form["chkInternal"] != null;
            string lotid = base.Request.Form["LotID"];
            commentService.AddComment(commentService.GenerateTranformedComment(commentID, lotid, s, internalOnly, BaseController.CurrentUserInfo));
        }
        public ActionResult CommentList(string lotID, int pageIndex = 0, int pageSize = 5)
        {
            int recordCount = 0;
            IList<TranformedComment> list = null;
            if (CurrentUserInfo.Role == UserRoles.Fab || CurrentUserInfo.Role == UserRoles.FabAdmin)
            {
                list = service.GetCommentsByLotID(lotID, "", true, pageIndex, pageSize, out recordCount);
            }
            else {
                list = service.GetCommentsByLotID(lotID, "", false, pageIndex, pageSize, out recordCount);
            }
            bool flag = ConfigurationManager.AppSettings["LocalDownload"] == "1";
            string str = ConfigurationManager.AppSettings["RemoteDownloadUrlPrefix"];
            for (int i = 0; i <= (list.Count - 1); i++)
            {
                for (int j = 0; j <= (list[i].Attachments.Count - 1); j++)
                {
                    if (!flag)
                    {
                        list[i].Attachments[j].StoreRelativePath = str + list[i].Attachments[j].StoreRelativePath;
                    }
                }
            }
            var data = new
            {
                currentPage = pageIndex,
                totalPages = PagerUtility.GetPageCount(recordCount, pageSize),
                rows = list
            };
            return base.Json(data);
        }
        public ActionResult PeDisposeByLot(string lotids, int status)
        {
            int type = 0;
            var role = CurrentUserInfo.Role;
            if (role == UserRoles.PE || role == UserRoles.PEAdmin)
            {
                type = 1;
            }
            if (role == UserRoles.QA || role == UserRoles.QAAdmin)
            {
                type = 2;
            }
            //if (role == UserRoles.Fab || role == UserRoles.FabAdmin)
            //{
            //    type = 3;
            //}
            int suc = waferService.DisposeByLot(lotids,type,status,base.Request,CurrentUserInfo.FullName);
            var json = JsonConvert.SerializeObject(new
            {
                suc = suc,
            });
            base.Response.Write(json);
            return null;
        }
    }
}