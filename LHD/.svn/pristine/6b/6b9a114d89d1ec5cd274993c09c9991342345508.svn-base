using System;
using System.Net;
using System.Text;
namespace KaYi.Utilities
{
	public static class HttpClient
	{
		public static string GetResponseFromURL(string url)
		{
			string result = string.Empty;
			WebClient webClient = new WebClient();
			webClient.Encoding = Encoding.UTF8;
			try
			{
				result = webClient.DownloadString(url);
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}
		public static string GetResponseFromURLUnicode(string url)
		{
			string result = string.Empty;
			WebClient webClient = new WebClient();
			webClient.Encoding = Encoding.Default;
			try
			{
				result = webClient.DownloadString(url);
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}
		public static string UrlEncode(string str)
		{
			StringBuilder stringBuilder = new StringBuilder();
			byte[] bytes = Encoding.UTF8.GetBytes(str);
			for (int i = 0; i < bytes.Length; i++)
			{
				stringBuilder.Append("%" + Convert.ToString(bytes[i], 16));
			}
			return stringBuilder.ToString();
		}
	}
}
