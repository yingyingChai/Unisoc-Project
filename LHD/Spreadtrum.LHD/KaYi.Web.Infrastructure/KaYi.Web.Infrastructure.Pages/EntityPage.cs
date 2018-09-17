using KaYi.Utilities;
using System;
namespace KaYi.Web.Infrastructure.Pages
{
	public class EntityPage : NormalPage
	{
		private PageStates pageState;
		private string pageTitle = string.Empty;
		private Entity entity;
		private string entityID = string.Empty;
		public PageStates PageState
		{
			get
			{
				return this.pageState;
			}
			set
			{
				this.pageState = value;
			}
		}
		public string PageTitle
		{
			get
			{
				return this.pageTitle;
			}
			set
			{
				this.pageTitle = value;
			}
		}
		public Entity Entity
		{
			get
			{
				return this.entity;
			}
			set
			{
				this.entity = value;
			}
		}
		public string EntityID
		{
			get
			{
				return this.entityID;
			}
			set
			{
				this.entityID = value;
			}
		}
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
		}
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			string a = StringHelper.GetString(base.Request.QueryString["action"]).ToUpper();
			this.EntityID = StringHelper.GetString(base.Request.QueryString["id"]);
			if (a == "EDIT")
			{
				this.PageState = PageStates.EditingRecord;
				return;
			}
			if (!(a == "CREATE"))
			{
				base.Response.End();
				return;
			}
			this.EntityID = Guid.NewGuid().ToString();
			this.PageState = PageStates.CreatingRecord;
		}
	}
}
