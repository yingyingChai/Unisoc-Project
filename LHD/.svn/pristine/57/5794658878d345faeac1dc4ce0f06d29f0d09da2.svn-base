using System;
namespace KaYi.Web.Infrastructure.Model.UI.Tab
{
	public class TabPage
	{
		private string _caption = string.Empty;
		private PageTypes _pageType;
		private string _pageUrl = string.Empty;
		private string _icon = string.Empty;
		private bool _active;
		private string _initializeFunction = string.Empty;
		public bool Active
		{
			get
			{
				return this._active;
			}
			set
			{
				this._active = value;
			}
		}
		public string Caption
		{
			get
			{
				return this._caption;
			}
			set
			{
				this._caption = value;
			}
		}
		public PageTypes PageType
		{
			get
			{
				return this._pageType;
			}
			set
			{
				this._pageType = value;
			}
		}
		public string PageUrl
		{
			get
			{
				return this._pageUrl;
			}
			set
			{
				this._pageUrl = value;
			}
		}
		public string InitializeFunction
		{
			get
			{
				return this._initializeFunction;
			}
			set
			{
				this._initializeFunction = value;
			}
		}
		public TabPage(string caption, PageTypes pageType, string pageUrl, string icon, bool active, string initializeFunction)
		{
			this._caption = caption;
			this._pageUrl = pageUrl;
			this._pageType = pageType;
			this._icon = icon;
			this._active = active;
			this._initializeFunction = initializeFunction;
		}
	}
}
