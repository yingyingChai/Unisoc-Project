using System;
using System.Collections.Generic;
using System.Collections.Specialized;
namespace KaYi.Utilities
{
	public static class Url
	{
		public static string ConvertLocalPathToUrl(string path)
		{
			string text = path;
			while (text.IndexOf("\\") >= 0)
			{
				text = text.Replace("\\", "/");
			}
			return text;
		}
		public static string ConvertUrlToLocalPath(string url)
		{
			string text = url;
			while (text.IndexOf("/") >= 0)
			{
				text = text.Replace("/", "\\");
			}
			return text;
		}
		public static string removeKey(NameValueCollection collections, string key)
		{
			string text = string.Empty;
			foreach (string text2 in collections)
			{
				if (text2 != key && text2 != null)
				{
					text = string.Concat(new string[]
					{
						text,
						text2,
						"=",
						collections[text2],
						"&"
					});
				}
			}
			return text;
		}
		public static string removeKeys(NameValueCollection collections, IList<string> keys)
		{
			string text = string.Empty;
			foreach (string text2 in collections)
			{
				if (!keys.Contains(text2) && text2 != null)
				{
					text = string.Concat(new string[]
					{
						text,
						text2,
						"=",
						collections[text2],
						"&"
					});
				}
			}
			return text;
		}
	}
}
