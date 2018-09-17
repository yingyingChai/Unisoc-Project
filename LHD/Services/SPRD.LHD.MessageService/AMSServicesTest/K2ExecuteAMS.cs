using System;
using System.Data;
using Quartz;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.IO;
using KaYi.Utilities;
using Spreadtrum.LHD.Entity.Lots;
using Spreadtrum.LHD.Business;
using SPRD.LHD.MessageService;

namespace AMSServicesTest
{
    public class K2ExecuteAMS : IStatefulJob
    {
        public static string AdminAddress = Convert.ToString(ConfigurationManager.AppSettings["AdminAddress"]);//账号

        public void Execute(JobExecutionContext context)
        {
            Log.Instance.Info("LHD Import Service Execute Start--------------------------------------------" + DateTime.Now.ToString());
            try
            {
                SpreadtrumLHDEntities Db = new SpreadtrumLHDEntities();
                //List<LOTSImportLogs> lot = new List<LOTSImportLogs>();
                DateTime now = DateTime.Now.AddMinutes(-2);
                var tbList = Db.LOTSImportLogs.Where(x => x.FormatStatus == "Pending" && x.CreateTime < now).ToList();
                if (tbList.Count > 0)
                {
                    foreach (var m in tbList)
                    {
                        Lot lot = new Lot();
                        Lot_Transformed lt = new Lot_Transformed();
                        Lot_TransformService lts = new Lot_TransformService();
                        try
                        {
                            if (m.Type.ToUpper() == "FT")
                            {

                                IList<string> messages = new List<string>();
                                string str2 = FileService.ReadTextFile(m.FilePath);
                                int index = str2.IndexOf("{\"LOT_JUDGEMENT");
                                string jsonStr = str2.Substring(index, (str2.Length - index) - 1);
                                lot = LotService.ReadLotFromJson(m.osatID, jsonStr, messages);
                                m.LotNO = lot.SubconLot;
                                m.Device = lot.DeviceCode + "(" + lot.DeviceName + ")";
                                m.Stage = lot.Stage;
                            }
                            else if (m.Type.ToUpper() == "CP")
                            {

                                IList<string> messages = new List<string>();
                                string str2 = FileService.ReadTextFile(m.FilePath);
                                int index = str2.IndexOf("{\"LOT_JUDGEMENT");
                                string jsonStr = str2.Substring(index, (str2.Length - index) - 1);
                                lt = lts.ReadJson(jsonStr, messages);
                            }
                            m.FormatStatus = "Success";
                            m.FormatTime = DateTime.Now;
                        }
                        catch (Exception fex)
                        {
                            m.FormatStatus = "Error";
                            m.Logs += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "--" + fex.Message + "<br/>";
                            Db.Set<LOTSImportLogs>().Attach(m);
                            Db.Entry(m).State = EntityState.Modified;

                            continue;
                        }

                        try
                        {
                            if (m.Type.ToUpper() == "FT")
                            {
                                if (lot != null)
                                {
                                    LotService.SaveLotAndInformQA_AND_PE(lot);
                                }
                            }
                            else if (m.Type.ToUpper() == "CP")
                            {
                                if (lt != null)
                                {
                                    lts.SaveCPJsonlot(lt);
                                }
                            }
                            m.ImportLHDStatus = "Success";
                            m.ImportLHDTime = DateTime.Now;
                        }
                        catch (Exception iex)
                        {
                            m.Logs += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "--" + iex.Message + "<br/>";
                            m.ImportLHDStatus = "Error";
                        }
                        Db.Set<LOTSImportLogs>().Attach(m);
                        Db.Entry(m).State = EntityState.Modified;

                    }
                }
                Db.SaveChanges();


                #region Template
                //    CVMSEntities Db = new CVMSEntities();

                //    List<T_Sys_MailQueue> MailsList = new List<T_Sys_MailQueue>();
                //    //Log.Instance.Info("Load Mail Data Begin-------------------------------------------" + DateTime.Now.ToString());
                //    var tbList = Db.T_Sys_MailQueue.Where(x => x.Status == "Pending").ToList();
                //    //Log.Instance.Info("Load Mail Data End-------------------------------------------" + DateTime.Now.ToString());

                //    if (tbList.Count > 0)
                //    {
                //        foreach (var m in tbList)
                //        {
                //            try
                //            {
                //                List<string> tolist = new List<string>();
                //                List<string> cclist = new List<string>();
                //                List<string> bcclist = new List<string>();
                //                //Log.Instance.Info("Load To Data Begin-------------------------------------------" + DateTime.Now.ToString());
                //                //Log.Instance.Info("Load To Data Begin-------------------------------------------" + m.To);

                //                if (!string.IsNullOrEmpty(m.To))
                //                {
                //                    string[] tos = m.To.Split(new char[1] { ';' });
                //                    foreach (var to in tos)
                //                    {
                //                        tolist.Add(to);
                //                    }

                //                }
                //                //Log.Instance.Info("Load CC Data Begin-------------------------------------------" + DateTime.Now.ToString());

                //                if (!string.IsNullOrEmpty(m.CC))
                //                {
                //                    string[] ccs = m.CC.Split(new char[1] { ';' });
                //                    foreach (var cc in ccs)
                //                    {
                //                        cclist.Add(cc);
                //                    }

                //                }
                //                //Log.Instance.Info("Load BCC Data Begin-------------------------------------------" + DateTime.Now.ToString());

                //                if (!string.IsNullOrEmpty(m.BCC))
                //                {
                //                    string[] bccs = m.BCC.Split(new char[1] { ';' });
                //                    foreach (var bcc in bccs)
                //                    {
                //                        bcclist.Add(bcc);
                //                    }
                //                }
                //                //Log.Instance.Info("Load Priority Data Begin-------------------------------------------" + DateTime.Now.ToString());

                //                MailPriority Priority;
                //                if (string.IsNullOrEmpty(m.Priority))
                //                {
                //                    Priority = MailPriority.Normal;
                //                }
                //                else
                //                {
                //                    Priority = (MailPriority)Enum.Parse(typeof(MailPriority), m.Priority);
                //                }


                //                //Log.Instance.Info("Send Mail Begin--------------------------------------------" + DateTime.Now.ToString());
                //                MailHelp.SendMail(tolist, cclist, bcclist, m.Subject, m.Boday, Priority);
                //                m.Status = "Sent";
                //                m.SendTime = DateTime.Now;
                //                //Log.Instance.Info("Send Mail End--------------------------------------------" + DateTime.Now.ToString());
                //            }
                //            catch (Exception ex)
                //            {
                //                Log.Instance.Error(ex);
                //                List<string> tolist = new List<string>();
                //                tolist.Add(AdminAddress);
                //                MailHelp.SendMail(tolist, null, null, "CVMS Mail Send Error", ex.Message, MailPriority.High);
                //                m.Status = "Error";
                //            }
                //            Db.Set<T_Sys_MailQueue>().Attach(m);
                //            Db.Entry(m).State = EntityState.Modified;

                //        }
                //        Db.SaveChanges();
                //    }
                #endregion

            }
            catch (Exception e)
            {
                Log.Instance.Error(e);
            }
            Log.Instance.Info("LHD Import Service Execute End--------------------------------------------" + DateTime.Now.ToString());
        }

    }

}
