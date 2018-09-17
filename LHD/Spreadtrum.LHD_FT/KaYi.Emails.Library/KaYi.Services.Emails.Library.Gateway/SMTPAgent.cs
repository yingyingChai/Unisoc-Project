using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
namespace KaYi.Services.Emails.Library.Gateway
{
	public class SMTPAgent
	{
		private MailMessage mailMessage;
		private SmtpClient smtpClient;
		private string password;
		private string userid;
		private string smtpServer;
		private string port = string.Empty;
		public string Password
		{
			get
			{
				return this.password;
			}
			set
			{
				this.password = value;
			}
		}
		public string Userid
		{
			get
			{
				return this.userid;
			}
			set
			{
				this.userid = value;
			}
		}
		public string SmtpServer
		{
			get
			{
				return this.smtpServer;
			}
			set
			{
				this.smtpServer = value;
			}
		}
		public string Port
		{
			get
			{
				return this.port;
			}
			set
			{
				this.port = value;
			}
		}
		public SMTPAgent(string To, string From, string Body, string Title, string userid, string Password, string smtpServer, string port)
		{
			this.mailMessage = new MailMessage();
			this.mailMessage.To.Add(To);
			this.mailMessage.From = new MailAddress(From);
			this.mailMessage.Subject = Title;
			this.mailMessage.Body = Body;
			this.mailMessage.IsBodyHtml = true;
			this.mailMessage.BodyEncoding = Encoding.UTF8;
			this.mailMessage.Priority = MailPriority.Normal;
			this.password = Password;
			this.smtpServer = smtpServer;
			this.userid = userid;
			this.port = port;
		}
		public void Attachments(string Path)
		{
			string[] array = Path.Split(new char[]
			{
				','
			});
			for (int i = 0; i < array.Length; i++)
			{
				Attachment attachment = new Attachment(array[i], "application/octet-stream");
				ContentDisposition expr_2A = attachment.ContentDisposition;
				expr_2A.CreationDate = File.GetCreationTime(array[i]);
				expr_2A.ModificationDate = File.GetLastWriteTime(array[i]);
				expr_2A.ReadDate = File.GetLastAccessTime(array[i]);
				this.mailMessage.Attachments.Add(attachment);
			}
		}
		public void SendAsync(SendCompletedEventHandler CompletedMethod)
		{
			if (this.mailMessage != null)
			{
				this.smtpClient = new SmtpClient();
				this.smtpClient.Credentials = new NetworkCredential(this.userid, this.password);
				this.smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
				this.smtpClient.Host = this.smtpServer;
				this.smtpClient.Port = Convert.ToInt32(this.Port);
				this.smtpClient.SendCompleted += new SendCompletedEventHandler(CompletedMethod.Invoke);
				this.smtpClient.SendAsync(this.mailMessage, this.mailMessage.Body);
			}
		}
		public void Send()
		{
			if (this.mailMessage != null)
			{
				this.smtpClient = new SmtpClient();
				this.smtpClient.Credentials = new NetworkCredential(this.userid, this.password);
				this.smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
				this.smtpClient.Host = this.smtpServer;
				this.smtpClient.Port = Convert.ToInt32(this.port);
				this.smtpClient.Send(this.mailMessage);
			}
		}
	}
}
