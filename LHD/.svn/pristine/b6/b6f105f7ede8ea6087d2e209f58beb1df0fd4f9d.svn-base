using System;
using System.Collections.Generic;
using System.ServiceProcess;
using System.Text;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Text.RegularExpressions;
using System.Collections;
using System.Net.Mail;
using System.Net;
using Net.Mail;
using System.Configuration;
using System.Timers;

namespace SPRD.LHD.SD.MailReceive
{
     class Program
    {

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main(string[] args)
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new Service1()
            };
            ServiceBase.Run(ServicesToRun);
            //Logger.Log.Info(""); Logger.Log.Info(""); Logger.Log.Info(""); Logger.Log.Info("");
            //try
            //{

            //    ExecuteAutoApproval eaa = new ExecuteAutoApproval();
            //    Logger.Log.Debug("进入邮件审批!!!!!");
            //    eaa.MailPop3Client();
            //}
            //catch (Exception ex)
            //{
            //    Logger.Log.Error("邮件审批未知出错!原因:" + ex);
            //    if (ex.ToString().IndexOf("Pop3 client is not connected") == -1)  //忽略pop3 server连不上的错误
            //    {
            //        ExecuteAutoApproval eaa = new ExecuteAutoApproval();
            //        //  CustomInfoItem hostItem = ConfigurationManager.GetCustomInfo(null, "MailApproval", "adminEmail");
            //        // string adminEmail = hostItem.Value;
            //        string mailSubject = "邮件审批未知出错!";
            //        string mailBody = "原因:" + ex;
            //        //  eaa.SendMail(adminEmail, mailSubject, mailBody);
            //    }
            //}
        }
        //static string ConvertToString(object value)
        //{
        //    if (value == null || value == DBNull.Value)
        //    {
        //        return "";
        //    }
        //    return value.ToString();
        //}

    }



}
