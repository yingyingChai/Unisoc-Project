using System;
namespace KaYi.Web.Infrastructure.Model.UI
{
	public class PageDisplayStyle
	{
		private bool showTitle;
		private bool showCreateDate;
		public bool ShowTitle
		{
			get
			{
				return this.showTitle;
			}
			set
			{
				this.showTitle = value;
			}
		}
		public bool ShowCreateDate
		{
			get
			{
				return this.showCreateDate;
			}
			set
			{
				this.showCreateDate = value;
			}
		}
	}
}
