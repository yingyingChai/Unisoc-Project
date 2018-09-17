using System;
using System.Configuration;
namespace KaYi.Web.Infrastructure.KaYiSystem
{
	public static class ApplicationConfiguration
	{
		private static string systemLogFile = string.Empty;
		private static string dbLogFile = string.Empty;
		public static string SystemLogFile
		{
			get
			{
				return ConfigurationManager.AppSettings["SysLog"];
			}
		}
		public static string DBLogFile
		{
			get
			{
				return ConfigurationManager.AppSettings["LogFile"];
			}
		}
	}
}
