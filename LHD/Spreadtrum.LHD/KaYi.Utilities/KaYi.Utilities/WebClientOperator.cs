using System;
using System.Web;
namespace KaYi.Utilities
{
	public static class WebClientOperator
	{
		public static string GetClientIP(HttpRequest request)
		{
			string text = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
			if (text == null || text == "")
			{
				text = request.ServerVariables["REMOTE_ADDR"];
			}
			return text;
		}
		public static void WriteCookies(HttpResponse response, string key, string value, DateTime expireTime)
		{
			HttpCookie httpCookie = new HttpCookie(key);
			httpCookie.Value = value;
			httpCookie.Expires = expireTime;
			response.Cookies.Add(httpCookie);
		}
		public static void WriteCookies(HttpResponseBase response, string key, string value, DateTime expireTime)
		{
			HttpCookie httpCookie = new HttpCookie(key);
			httpCookie.Value = value;
			httpCookie.Expires = expireTime;
			response.Cookies.Add(httpCookie);
		}
		public static bool GetCheckBoxValue(HttpRequest request, string controlName)
		{
			return request.Form[controlName] != null;
		}
	}
}
