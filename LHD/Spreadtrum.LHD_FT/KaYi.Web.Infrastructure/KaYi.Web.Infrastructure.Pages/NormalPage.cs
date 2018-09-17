using KaYi.Web.Infrastructure.Model.System.Session;
using KaYi.Web.Infrastructure.Model.System.WebSiteProtect;
using KaYi.Web.Infrastructure.Services;
using System;
using System.Configuration;
using System.Web.UI;
namespace KaYi.Web.Infrastructure.Pages
{
	public class NormalPage : Page
	{
		private bool writeLog = true;
		private bool needLogin;
		private WebSession _session;
		public bool WriteLog
		{
			get
			{
				return this.writeLog;
			}
			set
			{
				this.writeLog = value;
			}
		}
		public bool NeedLogin
		{
			get
			{
				return this.needLogin;
			}
			set
			{
				this.needLogin = value;
			}
		}
		public bool Logined
		{
			get
			{
				return this._session.LoginAccount != null;
			}
		}
		public WebSession session
		{
			get
			{
				return this._session;
			}
			set
			{
				this._session = value;
			}
		}
		public bool Postback
		{
			get
			{
				return base.Request.Form.Keys.Count > 0;
			}
		}
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.processBlackListAndLog();
			if (this.needLogin && this._session.LoginAccount == null)
			{
				string arg_63_0 = ConfigurationManager.AppSettings["TimeOutPage"];
				string str = base.Server.UrlEncode(base.Request.Path + "?" + base.Request.QueryString);
				string url = arg_63_0 + "?timeout=1&nextUrl=" + str;
				base.Response.Redirect(url);
			}
		}
		protected override void OnInit(EventArgs e)
		{
			if (!base.DesignMode)
			{
				base.OnInit(e);
				if (this.Session.Contents["Session"] != null)
				{
					this._session = (WebSession)this.Session.Contents["Session"];
				}
			}
		}
		private void processBlackListAndLog()
		{
			string[] array = "and|exec|insert|select|delete|update|chr|truncate|char|declare".ToUpper().Split(new char[]
			{
				'|'
			});
			for (int i = 0; i < array.Length; i++)
			{
				string value = array[i];
				if (base.Request.QueryString.ToString().ToUpper().IndexOf(value) >= 0)
				{
					base.Response.End();
				}
			}
			if (this._session != null)
			{
				switch (this._session.BlockType)
				{
				case BlockType.Deny:
					base.Response.End();
					return;
				case BlockType.NoLog:
					break;
				case BlockType.NoBlock:
					if (this.WriteLog)
					{
						LogService.AppendSysLog(this._session.ClientIP, this.Page.Request.RequestType, "WEB_SERVICE", this._session.ClientID, this._session.SessionID, this._session.AccountID, this.Page.Request.Url.Query, this.Page.Request.Url.AbsolutePath);
					}
					break;
				default:
					return;
				}
			}
		}
	}
}
