using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Configuration;

namespace SPRD.LHD.MessageService
{
    static class MailHelp
    {
        public static string MailAddress = Convert.ToString(ConfigurationManager.AppSettings["MailAddress"]);//账号
        public static string MailDisplayName = Convert.ToString(ConfigurationManager.AppSettings["MailDisplayName"]);//发件人地址
        public static string MailPassword = Convert.ToString(ConfigurationManager.AppSettings["MailPassword"]);//发件人地址
        public static string MailHost = Convert.ToString(ConfigurationManager.AppSettings["MailHost"]);
        public static int MailPort = Convert.ToInt32(ConfigurationManager.AppSettings["MailPort"]);//端口

        public static void SendMail(List<string> To, List<string> CC,List<string> BCC, string Subject, string Body,MailPriority Priority)
        {
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            if (To != null && To.Count > 0)
            {
                foreach (var to in To)
                {
                    msg.To.Add(to);
                }
            }
            if (CC != null && CC.Count > 0)
            {
                foreach (var cc in CC)
                {
                    msg.CC.Add(cc);
                }
            }
            if (BCC != null && BCC.Count > 0)
            {
                foreach (var bcc in BCC)
                {
                    msg.Bcc.Add(bcc);
                }
            }
            msg.From = new MailAddress(MailAddress, MailDisplayName, System.Text.Encoding.UTF8);
            /* 上面3个参数分别是发件人地址（可以随便写），发件人姓名，编码*/
            msg.Subject = Subject;//邮件标题 
            msg.SubjectEncoding = System.Text.Encoding.UTF8;//邮件标题编码 
            msg.Body = Body;//邮件内容 
            msg.BodyEncoding = System.Text.Encoding.UTF8;//邮件内容编码 
            msg.IsBodyHtml = true;//是否是HTML邮件 
            msg.Priority = Priority;//邮件优先级 

            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential(MailAddress, MailPassword);
            //在zj.com注册的邮箱和密码 
            client.Host = MailHost;
            client.Port = MailPort;
            object userState = msg;
            try
            {
                client.Send(msg);
                //简单一点儿可以client.Send(msg); 
            }
            catch (Exception ex)
            {
                Log.Instance.Error(ex);
            }
        }
    }
}
