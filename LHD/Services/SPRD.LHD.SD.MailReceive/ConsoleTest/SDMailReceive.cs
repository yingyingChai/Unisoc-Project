using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Text.RegularExpressions;
using System.Data;
using System.Collections;
using System.Net.Mail;
using System.Net;
using Net.Mail;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Data.SqlClient;
using HY.Common.Utility.Utils;
using ConsoleTest;

namespace SPRD.LHD.SD.MailReceive
{
    public class SDMailReceive
    {
        //SqlDataAdapter da;
        //SqlCommand sc;
        //AutomaticApprovalComponent aac = new AutomaticApprovalComponent();
        //EmployeeComponent ec = new EmployeeComponent();
        //ResourceComponent rc = new ResourceComponent();
        //ProcActivityRuleComonent prc = new ProcActivityRuleComonent();
        private string PopServer = "PopServer";  //初值，后面将读hy.common.config配置
        private int PopPort = 110;
        private string User = "User@163.com";
        private string Pass = "password";
        private bool UseSSL = false;
        private string sn_prefix = "@任务号[";  //邮件标题中，sn的前缀

        private string Log_Sn_Code = "未获取流程单号";//流程节点号只做邮件审批记日志用，不参与流程审批
        private string Log_Email_Address = "未获取回复人邮件地址";//回复人邮件地址，不参与流程审批
        private string Log_FromName = "未流程获取邮件回复人";//邮件回复人只做邮件审批记日志用，不参与流程审批
        private string Log_Content = "未获取流程审批人回复邮件内容";//邮件回复内容，不参与流程审批
        private string Log_Action = "未获取流程审批操作";//审批操作信息，不参与流程审批
        private string Log_Approval = "未获取流程审批人";//审批人信息，不参与流程审批


        public SDMailReceive()
        {

            //Logger.Log.Debug("ExecuteAutoApproval 构造函数，读hy.common.config配置..."); //added by zeng, 2010.11.04
            //读取邮件服务器的配置文件，普通邮件和邮件审批分不同邮箱 by xuyiru 2010-11-19
            // CustomInfoItem hostItem = ConfigurationManager.GetCustomInfo(null, "MailApproval", "POP3Server");

            PopServer = ConfigurationManager.AppSettings["POP3Server"];
            //  // CustomInfoItem portItem = ConfigurationManager.GetCustomInfo(null, "MailApproval", "POP3Port");
            PopPort = Convert.ToInt32(ConfigurationManager.AppSettings["POP3Port"]);
            ////   CustomInfoItem enableSslItem = ConfigurationManager.GetCustomInfo(null, "MailApproval", "isSSL");
            UseSSL = Boolean.Parse(ConfigurationManager.AppSettings["isSSL"]);
            //               ///   CustomInfoItem mailUsernameItem = ConfigurationManager.GetCustomInfo(null, "MailApproval", "user");
            User = ConfigurationManager.AppSettings["user"];
            // //  CustomInfoItem mailPwdItem = ConfigurationManager.GetCustomInfo(null, "MailApproval", "pwd");
            Pass = ConfigurationManager.AppSettings["pwd"];
        }

        /// <summary>
        /// 获取邮件
        /// </summary>
        public void MailPop3Client()
        {
            //Logger.Log.Info("邮件审批开始...");
            //Logger.Log.Info("************************---PopServer=" + PopServer);
            //Logger.Log.Debug("************************---PopPort=" + PopPort);
            //Logger.Log.Debug("************************---User=" + User);
            //Logger.Log.Debug("************************---Pass=" + Pass);
            //Logger.Log.Debug("************************---UseSSL=" + UseSSL);
            using (Pop3Client client = new Pop3Client(PopServer, PopPort, UseSSL, User, Pass))
            {
                //显示程序与POP3服务器的通信过程
                //client.Trace += new Action<string>(Console.WriteLine);

                //连接POP3服务器并验证用户身份
                Logger.Log.Info("Connect POP3 Server and verify...");
                client.Authenticate();
                Logger.Log.Info("Verify Success...");
                client.Stat();

                //枚举所有未读邮件
                Logger.Log.Info("Mail Exist..." + client.List());
                SpreadtrumLHDEntities Db = new SpreadtrumLHDEntities();
                foreach (Pop3ListItem item in client.List())
                {
                    MailMessageEx message = null;
                    try
                    {
                        
                        LOTSImportSDLogs lotlogs = new LOTSImportSDLogs();
                        Logger.Log.Info("RetrMailMessageEx item=" + item.MessageId);
                        message = client.RetrMailMessageEx(item);
                        if (message.Subject.ToLower().StartsWith("[lot judgement]")
                            //&& message.Sender.Address.ToLower() == "noreply@silicondash.com"
                            )
                        {
                            
                            Logger.Log.Info("RetrMailMessageEx message subject=" + message.Subject);
                            string[] data = getLotData(message.Subject);
                            Logger.Log.Info("RetrMailMessageEx message osat=" + data[2]);
                            Logger.Log.Info("RetrMailMessageEx message Device=" + data[3]);
                            Logger.Log.Info("RetrMailMessageEx message LotNO=" + data[4]);
                            Logger.Log.Info("RetrMailMessageEx message Stage=" + data[5]);
                            Logger.Log.Info("RetrMailMessageEx message DeliveryDate=" + message.DeliveryDate.ToString());
                            lotlogs.OSAT = data[2];
                            lotlogs.Device = data[3];
                            lotlogs.LotNO = data[4];
                            lotlogs.Stage = data[5];
                            lotlogs.DeliveryDate = message.DeliveryDate;
                            lotlogs.CreateTime = DateTime.Now;
                            lotlogs.LHDFormatStatus = "Pending";
                            lotlogs.LHDImportStatus = "Pending";
                            Db.LOTSImportSDLogs.Add(lotlogs);

                            //Db.Set<LOTSImportSDLogs>().Attach(lotlogs);
                            //Db.Entry(lotlogs).State = EntityState.Added;
                        }
                        client.Dele(item);
                    }
                    catch (Exception ex)
                    {
                        Logger.Log.Error("RetrMailMessageEx error=" + ex);
                        //client.Dele(item);
                        continue;
                    }

                }
                Db.SaveChanges();
                Logger.Log.Info("邮件处理完毕。");
                client.Quit();
            }
        }

        private string[] getLotData(string Subject)
        {
            //[Lot Judgement] AMKOR 150104_00 FTH241436851.01 QA5 failed check
            return Subject.Trim().Split(new char[1] { ' ' });
        }


    }
}
