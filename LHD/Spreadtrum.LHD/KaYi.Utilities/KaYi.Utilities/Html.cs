using System;
using System.Text.RegularExpressions;
using System.Web;
namespace KaYi.Utilities
{
	public static class Html
	{
		public static string GetClearTextFromHTML(string str)
		{
			return Html.formatHtml(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(str, "<!--[\\s\\S]*?-->", "", RegexOptions.IgnoreCase), "<[//]*tr[^>]*>", "", RegexOptions.IgnoreCase), "<[//]*p[^>]*>", "\n", RegexOptions.IgnoreCase), "<[//]*br[^>]*>", "\n", RegexOptions.IgnoreCase), "<[//]*div[^>]*>", "\n", RegexOptions.IgnoreCase), "<STYLE[\\s\\S]*?</STYLE>", "", RegexOptions.IgnoreCase), "<script[\\s\\S]*?</script>", "", RegexOptions.IgnoreCase), "<[\\?!A-Za-z/][^><]*>", ""), "\r", ""), "\n", "\r\n"), "[\u3000|\\s]*\\r\\n[\u3000|\\s]*\\r\\n", "\r\n"), "(\\r\\n)[^ \u3000]", "$1"));
		}
		private static string formatHtml(string str)
		{
			return Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(str, " ", " "), "&nbsp;", " "), "&nbsp", " "), "&#8226;", " "), "&#8226", " "), "&#146;", "'"), "&#147;", "“"), "&#148;", "”"), "&#160;", ""), "&amp;", "&"), "&copy;", "?"), "&#150;", "–"), "&quot;", " "), "&lt;", " "), "&gt;", " "), "&#13;&#10;", "");
		}
		public static string DeleteEnterFromHtml(string str)
		{
			while (str.IndexOf("\r\n") >= 0)
			{
				str = str.Replace("\r\n", "");
			}
			return str;
		}
		public static string GetOutputStrFromDB(string str)
		{
			str = HttpUtility.HtmlDecode(str);
			while (str.IndexOf("\\n") > 0)
			{
				str = str.Replace("\\n", "\n");
			}
			return str;
		}
	}
}
