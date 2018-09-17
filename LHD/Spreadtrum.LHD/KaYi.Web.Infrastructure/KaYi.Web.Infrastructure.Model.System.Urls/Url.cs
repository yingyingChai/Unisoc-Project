using System;
namespace KaYi.Web.Infrastructure.Model.System.Urls
{
	public class Url
	{
		private string _UrlID = string.Empty;
		private string _Url = string.Empty;
		private string _NavigatorID = string.Empty;
		private string _AllowRoles = string.Empty;
		private string _PageUrl = string.Empty;
		private string _Title = string.Empty;
		public string UrlID
		{
			get
			{
				return this._UrlID;
			}
			set
			{
				this._UrlID = value;
			}
		}
		public string UrlPath
		{
			get
			{
				return this._Url;
			}
			set
			{
				this._Url = value;
			}
		}
		public string NavigatorID
		{
			get
			{
				return this._NavigatorID;
			}
			set
			{
				this._NavigatorID = value;
			}
		}
		public string AllowRoles
		{
			get
			{
				return this._AllowRoles;
			}
			set
			{
				this._AllowRoles = value;
			}
		}
		public string PageUrl
		{
			get
			{
				return this._PageUrl;
			}
			set
			{
				this._PageUrl = value;
			}
		}
		public string Title
		{
			get
			{
				return this._Title;
			}
			set
			{
				this._Title = value;
			}
		}
	}
}
