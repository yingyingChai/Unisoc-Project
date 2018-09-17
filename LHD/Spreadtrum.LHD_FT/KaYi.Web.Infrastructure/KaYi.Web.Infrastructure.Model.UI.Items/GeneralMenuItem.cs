using System;
namespace KaYi.Web.Infrastructure.Model.UI.Items
{
	public class GeneralMenuItem
	{
		private string url = string.Empty;
		private string id = string.Empty;
		private string text = string.Empty;
		private string tag = string.Empty;
		private string icon = string.Empty;
		private string cssClass = string.Empty;
		public string CssClass
		{
			get
			{
				return this.cssClass;
			}
			set
			{
				this.cssClass = value;
			}
		}
		public string Icon
		{
			get
			{
				return this.icon;
			}
			set
			{
				this.icon = value;
			}
		}
		public string Url
		{
			get
			{
				return this.url;
			}
			set
			{
				this.url = value;
			}
		}
		public string Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}
		public string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				this.text = value;
			}
		}
		public string Tag
		{
			get
			{
				return this.tag;
			}
			set
			{
				this.tag = value;
			}
		}
	}
}
