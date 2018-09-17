using KaYi.Web.Infrastructure.Model.System.Session;
using System;
using System.Web.UI;
namespace KaYi.Web.Infrastructure.Pages
{
	public class WebControls : UserControl
	{
		private WebSession _session;
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
		public bool Logined
		{
			get
			{
				return this._session != null && this._session.LoginAccount != null;
			}
		}
		protected override void OnInit(EventArgs e)
		{
			if (!base.DesignMode)
			{
				base.OnInit(e);
				if (base.Session.Contents["Session"] != null)
				{
					this._session = (WebSession)base.Session.Contents["Session"];
				}
			}
		}
	}
}
