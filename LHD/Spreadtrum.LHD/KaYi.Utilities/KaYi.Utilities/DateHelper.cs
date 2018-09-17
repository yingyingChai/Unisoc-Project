using System;
namespace KaYi.Utilities
{
	public static class DateHelper
	{
		public static string GetChineseWeekDay(DateTime date)
		{
			string result = string.Empty;
			switch (date.DayOfWeek)
			{
			case DayOfWeek.Sunday:
				result = "星期日";
				break;
			case DayOfWeek.Monday:
				result = "星期一";
				break;
			case DayOfWeek.Tuesday:
				result = "星期二";
				break;
			case DayOfWeek.Wednesday:
				result = "星期三";
				break;
			case DayOfWeek.Thursday:
				result = "星期四";
				break;
			case DayOfWeek.Friday:
				result = "星期五";
				break;
			case DayOfWeek.Saturday:
				result = "星期六";
				break;
			}
			return result;
		}
		public static string FormatDateForDisplay(object dateTime, string formatString, string nullValue)
		{
			if (dateTime == null)
			{
				return nullValue;
			}
			object arg_11_0 = dateTime.GetType();
			string result = string.Empty;
			string a = arg_11_0.ToString();
			DateTime dateTime2;
			if (a == "DateTime")
			{
				dateTime2 = (DateTime)dateTime;
			}
			else
			{
				try
				{
					dateTime2 = Convert.ToDateTime(dateTime);
				}
				catch (Exception)
				{
					dateTime2 = Convert.ToDateTime("0001-01-01");
				}
			}
			if (dateTime2.Year == 1)
			{
				result = nullValue;
			}
			else
			{
				result = dateTime2.ToString(formatString);
			}
			return result;
		}
		public static DateTime GetTime(string timeStamp)
		{
			DateTime dateTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
			long ticks = long.Parse(timeStamp + "0000000");
			TimeSpan value = new TimeSpan(ticks);
			return dateTime.Add(value);
		}
		public static int ConvertDateTimeInt(DateTime time)
		{
			DateTime d = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
			return (int)(time - d).TotalSeconds;
		}
	}
}
