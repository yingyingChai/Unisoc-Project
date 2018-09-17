using System;
namespace KaYi.Web.Infrastructure.Model.Login
{
	public class QQClient
	{
		private string clientID = string.Empty;
		private string clientSecret = string.Empty;
		private string redirectUrl = string.Empty;
		private string sessionID = string.Empty;
		public string ClientID
		{
			get
			{
				return this.clientID;
			}
			set
			{
				this.clientID = value;
			}
		}
		public string ClientSecret
		{
			get
			{
				return this.clientSecret;
			}
			set
			{
				this.clientSecret = value;
			}
		}
		public string RedirectUri
		{
			get
			{
				return this.redirectUrl;
			}
			set
			{
				this.redirectUrl = value;
			}
		}
		public string SessionID
		{
			get
			{
				return this.sessionID;
			}
			set
			{
				this.sessionID = value;
			}
		}
	}
}
