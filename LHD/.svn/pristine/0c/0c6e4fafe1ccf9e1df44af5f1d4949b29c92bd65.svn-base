using KaYi.Web.Infrastructure.Model.System.Session;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;
using System.Web;
using System.Web.SessionState;
namespace KaYi.Web.Infrastructure
{
	public static class Context
	{
		public static void GetContext(HttpContext context, out HttpRequest request, out HttpResponse response, out HttpSessionState session, out WebSession webSession, out string action)
		{
			HttpRequest request2 = context.Request;
			string text = request2.Form["KaYiData"];
			action = ((request2.Form["KaYiAction"] != null) ? request2.Form["KaYiAction"] : request2.QueryString["KaYiAction"]);
			if (action != null)
			{
				action = action.ToUpper();
			}
			else
			{
				action = "";
			}
			if (text != null && text != "")
			{
				string[] array = text.Split(new char[]
				{
					'&'
				});
				if (array.Length != 0)
				{
					for (int i = 0; i <= array.Length - 1; i++)
					{
						string[] expr_A6 = array[i].Split(new char[]
						{
							'='
						});
						string key = HttpUtility.UrlDecode(expr_A6[0]);
						string value = HttpUtility.UrlDecode(expr_A6[1]);
						Context.AppendKeyValueToForm(request2, key, value);
					}
				}
			}
			request = request2;
			response = context.Response;
			session = context.Session;
			webSession = (WebSession)session.Contents["Session"];
		}
		private static void AppendKeyValueToForm(HttpRequest request, string key, string value)
		{
			NameValueCollection nameValueCollection = request.Form;
			nameValueCollection = (NameValueCollection)request.GetType().GetField("_form", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(request);
			PropertyInfo expr_37 = nameValueCollection.GetType().GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
			expr_37.SetValue(nameValueCollection, false, null);
			nameValueCollection[key] = value;
			expr_37.SetValue(nameValueCollection, true, null);
		}
		public static IList<string> GetSelectedIDs(HttpRequest request, string layerID)
		{
			string text = "chk_" + layerID + "_";
			IList<string> list = new List<string>();
			foreach (string text2 in request.Form)
			{
				if (text2.StartsWith(text))
				{
					list.Add(text2.Substring(text.Length, text2.Length - text.Length));
				}
			}
			return list;
		}
	}
}
