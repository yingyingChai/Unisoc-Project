using System;
using System.Web;
namespace KaYi.Web.Infrastructure.Pages
{
	public class PagerInfo
	{
		private int pageIndex;
		private int pageSize;
		public int PageIndex
		{
			get
			{
				return this.pageIndex;
			}
			set
			{
				this.pageIndex = value;
			}
		}
		public int PageSize
		{
			get
			{
				return this.pageSize;
			}
			set
			{
				this.pageSize = value;
			}
		}
		public PagerInfo(HttpRequest Request)
		{
			string text = Request.QueryString["pageIndex"];
			string text2 = Request.QueryString["pageSize"];
			if (text != null)
			{
				this.pageIndex = Convert.ToInt32(text);
			}
			else
			{
				this.pageIndex = 0;
			}
			if (text2 != null)
			{
				this.pageSize = Convert.ToInt32(text2);
				return;
			}
			this.pageSize = 9999;
		}
	}
}
