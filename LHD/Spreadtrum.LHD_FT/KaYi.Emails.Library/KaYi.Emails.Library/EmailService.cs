using KaYi.Services.Emails.Library.Gateway;
using KaYi.Services.Emails.Library.Model;
using System;
namespace KaYi.Emails.Library
{
	public static class EmailService
	{
		private static EmailGateway emailGateway = new EmailGateway();
		public static Email GetEmailByID(string emailID)
		{
			return EmailService.emailGateway.GetEmailByID(emailID);
		}
		public static void AddEmail(Email email)
		{
			EmailService.emailGateway.AddNew(email);
		}
	}
}
