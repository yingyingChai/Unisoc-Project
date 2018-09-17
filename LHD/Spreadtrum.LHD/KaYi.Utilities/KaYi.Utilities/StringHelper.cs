using System;
using System.Collections.Generic;
using System.ComponentModel;
namespace KaYi.Utilities
{
	public static class StringHelper
	{
		public static string FormatDecimal(float number, int digits)
		{
			return string.Format("{0:F" + digits.ToString() + "}", number);
		}
		public static string RemoveControlChars(string str)
		{
			string text = "";
			if (str != null && str.Length > 0)
			{
				char[] array = str.ToCharArray();
				for (int i = 0; i < array.Length; i++)
				{
					if ((array[i] >= ' ' && array[i] != '\u007f') || array[i] == '\r' || array[i] == '\n')
					{
						text += array[i].ToString();
					}
				}
			}
			return text;
		}
		public static Dictionary<string, string> GetAttributes(string attributeString)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			string[] array = attributeString.Split(new char[]
			{
				';'
			});
			for (int i = 0; i <= array.Length - 1; i++)
			{
				string[] array2 = array[i].Split(new char[]
				{
					':'
				});
				if (array2.Length >= 2)
				{
					dictionary.Add(array2[0], array2[1]);
				}
			}
			return dictionary;
		}
		public static object StringToEnum(string value, Type enumType)
		{
			return new EnumConverter(enumType).ConvertFrom(value);
		}
		public static string DeleteEmptyFirstLine(string code)
		{
			while (code.StartsWith("\r\n"))
			{
				code = code.Substring(2, code.Length - 2);
			}
			return code;
		}
		public static string replaceWithRealSpace(string html)
		{
			return html.Replace("&nbsp;", " ");
		}
		public static string ReplaceICFromGUID(string id)
		{
			while (id.IndexOf("-") >= 0)
			{
				id = id.Replace("-", "");
			}
			return "ID" + id;
		}
		public static string ReplaceICFromGUIDWithoutID(string id)
		{
			while (id.IndexOf("-") >= 0)
			{
				id = id.Replace("-", "");
			}
			return id;
		}
		public static string GetString(object obj)
		{
			if (obj == null)
			{
				return string.Empty;
			}
			return obj.ToString();
		}
		public static string GetStringFromList(IList<string> values)
		{
			string text = string.Empty;
			if (values != null && values.Count > 0)
			{
				foreach (string current in values)
				{
					text += string.Format("'{0}',", current);
				}
				if (text.EndsWith(","))
				{
					text = text.Substring(0, text.Length - 1);
				}
			}
			return text;
		}
		public static IList<string> BuildStringList(string value)
		{
			if (value == null)
			{
				return null;
			}
			return new List<string>
			{
				value
			};
		}
		public static string CutString(string value, int maxLength)
		{
			if (value == null)
			{
				return string.Empty;
			}
			if (value.Length > maxLength)
			{
				value = value.Substring(0, maxLength) + "...";
			}
			return value;
		}
		public static string GetSubstringInStr(string str, string startAt, string endAt)
		{
			int num = str.IndexOf(startAt);
			int num2 = str.IndexOf(endAt, num + startAt.Length);
			return str.Substring(num + startAt.Length, num2 - num - startAt.Length);
		}
		public static bool isNullOrEmpty(string value)
		{
			return value == null || value == "";
		}
		public static bool isEmptyString(string value)
		{
			return value == null || value == "" || (value != null && value.Trim().Length <= 0);
		}
		public static string MergeString(IList<string> strs, char spliter)
		{
			string text = string.Empty;
			string text2 = spliter.ToString();
			foreach (string current in strs)
			{
				text = text + current + spliter.ToString();
			}
			if (text.EndsWith(text2))
			{
				text = text.Substring(0, text.Length - text2.Length);
			}
			return text;
		}
		public static IList<string> SplitString(string data, char spliter)
		{
			return data.Split(new char[]
			{
				spliter,
				spliter
			});
		}
		public static string RemoveLastChar(string str)
		{
			return str.Substring(0, str.Length - 1);
		}
		public static string RemoveLastComma(string str)
		{
			if (!str.EndsWith(","))
			{
				return str;
			}
			return str.Substring(0, str.Length - 1);
		}
		public static string GetTimeDescription(DateTime date)
		{
			string result = string.Empty;
			switch ((int)DateTime.Now.Subtract(date).TotalDays)
			{
			case 0:
				result = date.ToString("HH:mm");
				break;
			case 1:
				result = "昨天";
				break;
			case 2:
				result = "前天";
				break;
			default:
				if (DateTime.Now.Year - date.Year >= 1)
				{
					result = date.Year.ToString() + "年";
				}
				else
				{
					result = date.ToString("MM月dd日");
				}
				break;
			}
			return result;
		}
	}
}
