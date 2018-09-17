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
using HY.Common.Utility.Utils;

namespace SPRD.LHD.SD.MailReceive
{
    public partial class Service1 : ServiceBase
    {
        string secondsStr = ConfigurationManager.AppSettings["Intervals"];
        public Service1()
        {
            System.Timers.Timer time = new System.Timers.Timer();

            int seconds = int.Parse(secondsStr);//时间周期存放在webconfig中
            time.Interval = seconds;//每隔多少毫秒后给服务器发信息
            time.AutoReset = true;
            time.Enabled = true;
            time.Elapsed += new System.Timers.ElapsedEventHandler(time_Elapsed);
            //定时触发一个事件如果不是定时触发，而是时刻都在运行可以使用protected override void //OnStart(string[] args)，和protected override void OnStop()两个方法表示服务启动和结束事件
            time.Start();

        }

        protected override void OnStart(string[] args)
        {
            DoUpload();
        }
        private void time_Elapsed(object sender, ElapsedEventArgs e)
        {
            DoUpload();
            // TODO: 在此处添加代码以启动服务。
        }
        protected override void OnStop()
        {
        }
        protected override void OnPause()
        {
            //服务暂停执行代码
            base.OnPause();
        }
        protected override void OnContinue()
        {
            //服务恢复执行代码
            base.OnContinue();
        }
        protected override void OnShutdown()
        {
            //系统即将关闭执行代码
            base.OnShutdown();
        }
        static string ConvertToString(object value)
        {
            if (value == null || value == DBNull.Value)
            {
                return "";
            }
            return value.ToString();
        }

         private void DoUpload()
        {
            Logger.Log.Info(""); Logger.Log.Info(""); Logger.Log.Info(""); Logger.Log.Info("");
            try
            {

                SDMailReceive eaa = new SDMailReceive();
                Logger.Log.Debug("进入邮件审批!!!!!");
                eaa.MailPop3Client();
            }
            catch (Exception ex)
            {
                Logger.Log.Error("邮件审批未知出错!原因:" + ex);
                if (ex.ToString().IndexOf("Pop3 client is not connected") == -1)  //忽略pop3 server连不上的错误
                {
                    SDMailReceive eaa = new SDMailReceive();
                    //  CustomInfoItem hostItem = ConfigurationManager.GetCustomInfo(null, "MailApproval", "adminEmail");
                    // string adminEmail = hostItem.Value;
                    string mailSubject = "邮件审批未知出错!";
                    string mailBody = "原因:" + ex;
                    //  eaa.SendMail(adminEmail, mailSubject, mailBody);
                }
            }
        }
    }
}
