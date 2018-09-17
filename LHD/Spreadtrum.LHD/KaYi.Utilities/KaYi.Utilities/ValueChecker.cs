using System;
using System.Text.RegularExpressions;
namespace KaYi.Utilities
{
	public static class ValueChecker
	{
		private static string _integer = "^-?[1-9]\\d*$";
		private static string _positiveInteger = "^[1-9]\\d*$";
		private static string _negativeInteger = "^-[1-9]\\d*$";
		private static string _number = "^([+-]?)\\d*\\.?\\d+$";
		private static string _positiveNumber = "^[1-9]\\d*|0$";
		private static string _negativeNumer = "^-[1-9]\\d*|0$";
		private static string _decimal = "^([+-]?)\\d*\\.\\d+$";
		private static string _positiveDecimal = "^[1-9]\\d*.\\d*|0.\\d*[1-9]\\d*$";
		private static string _negativeDecimal = "^-([1-9]\\d*.\\d*|0.\\d*[1-9]\\d*)$";
		private static string _rar = "(.*)\\.(rar|zip|7zip|tgz)$";
		private static string _email = "^\\w+((-\\w+)|(\\.\\w+))*\\@[A-Za-z0-9]+((\\.|-)[A-Za-z0-9]+)*\\.[A-Za-z0-9]+$";
		private static string _chinese = "^[\\u4e00-\\u9fa5]{2,16}$";
		private static string _ascii = "^[\\x00-\\xFF]+$";
		private static string _zipcode = "^\\d{6}$";
		private static string _mobile = "^(13|15|18|14)[0-9]{9}$";
		private static string _ip4 = "^(25[0-5]|2[0-4]\\d|[0-1]\\d{2}|[1-9]?\\d)\\.(25[0-5]|2[0-4]\\d|[0-1]\\d{2}|[1-9]?\\d)\\.(25[0-5]|2[0-4]\\d|[0-1]\\d{2}|[1-9]?\\d)\\.(25[0-5]|2[0-4]\\d|[0-1]\\d{2}|[1-9]?\\d)$";
		private static string _picture = "(.*)\\.(jpg|bmp|gif|ico|pcx|jpeg|tif|png|raw|tga)$";
		private static string _qq = "^[1-9]*[1-9][0-9]*$";
		private static string _tel = "^(([0\\+]\\d{2;3}-)?(0\\d{2;3})-)?(\\d{7;8})(-(\\d{3;}))?$";
		private static string _username = "^\\w+$";
		private static string _letter = "^[A-Za-z]+$";
		private static string _letter_u = "^[A-Z]+$";
		private static string _letter_l = "^[a-z]+$";
		private static string _idcard = "^[1-9]([0-9]{14}|[0-9]{17})$";
		private static string _url = "^http[s]?:\\/\\/([\\w-]+\\.)+[\\w-]+([\\w-./?%&=]*)?$";
		private static string _userName_chinese = "^(?!_)(?!.*?_$)[a-zA-Z0-9_\\u4e00-\\u9fa5]{1,15}$";
		private static bool checker(string regExp, string value)
		{
			return new Regex(regExp).IsMatch(value);
		}
		public static bool IsNumber(string value)
		{
			return ValueChecker.checker(ValueChecker._number, value);
		}
		public static bool IsNegativeNumber(string value)
		{
			return ValueChecker.checker(ValueChecker._negativeNumer, value);
		}
		public static bool IsPositiveNumber(string value)
		{
			return ValueChecker.checker(ValueChecker._positiveNumber, value);
		}
		public static bool IsDecimal(string value)
		{
			return ValueChecker.checker(ValueChecker._decimal, value);
		}
		public static bool IsPositiveDecimal(string value)
		{
			return ValueChecker.checker(ValueChecker._positiveDecimal, value);
		}
		public static bool IsNegativeDecimal(string value)
		{
			return ValueChecker.checker(ValueChecker._negativeDecimal, value);
		}
		public static bool IsInteger(string value)
		{
			return ValueChecker.checker(ValueChecker._integer, value);
		}
		public static bool IsNegativeInteger(string value)
		{
			return ValueChecker.checker(ValueChecker._negativeInteger, value);
		}
		public static bool IsPositiveInteger(string value)
		{
			return ValueChecker.checker(ValueChecker._positiveInteger, value);
		}
		public static bool IsLLetterOnly(string value)
		{
			return ValueChecker.checker(ValueChecker._letter_l, value);
		}
		public static bool IsULetterOnly(string value)
		{
			return ValueChecker.checker(ValueChecker._letter_u, value);
		}
		public static bool IsLetterOnly(string value)
		{
			return ValueChecker.checker(ValueChecker._letter, value);
		}
		public static bool IsUserName(string value)
		{
			return ValueChecker.checker(ValueChecker._username, value);
		}
		public static bool IsDate(string value)
		{
			bool result;
			try
			{
				Convert.ToDateTime(value);
				result = true;
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}
		public static bool IsDateTime(string value)
		{
			bool result;
			try
			{
				Convert.ToDateTime(value);
				result = true;
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}
		public static bool isMoney(string value)
		{
			return ValueChecker.checker("^([0-9]+|[0-9]{1,3}(,[0-9]{3})*)(.[0-9]{1,2})?$", value);
		}
		public static bool IsMobile(string value)
		{
			return ValueChecker.checker(ValueChecker._mobile, value);
		}
		public static bool IsEmail(string value)
		{
			return ValueChecker.checker(ValueChecker._email, value);
		}
		public static bool IsUrl(string value)
		{
			return ValueChecker.checker(ValueChecker._url, value);
		}
		public static bool IsZipCode(string value)
		{
			return ValueChecker.checker(ValueChecker._zipcode, value);
		}
		public static bool IsTelephone(string value)
		{
			return ValueChecker.checker(ValueChecker._tel, value);
		}
		public static bool IsIDChard(string value)
		{
			return ValueChecker.checker(ValueChecker._idcard, value);
		}
		public static bool IsChineseOnly(string value)
		{
			return ValueChecker.checker(ValueChecker._chinese, value);
		}
		public static bool IsAsciiOnly(string value)
		{
			return ValueChecker.checker(ValueChecker._ascii, value);
		}
		public static bool IsQQ(string value)
		{
			return ValueChecker.checker(ValueChecker._qq, value);
		}
		public static bool IsPicFileName(string value)
		{
			return ValueChecker.checker(ValueChecker._picture, value);
		}
		public static bool IsCompressedFileName(string value)
		{
			return ValueChecker.checker(ValueChecker._rar, value);
		}
		public static bool IsIPV4(string value)
		{
			return ValueChecker.checker(ValueChecker._ip4, value);
		}
		public static bool isUserName_chinese(string value)
		{
			return ValueChecker.checker(ValueChecker._userName_chinese, value);
		}
	}
}
