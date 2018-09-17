using KaYi.Services.EventServices.Model;
using System;
using System.Configuration;
using System.IO;
using System.Text;
namespace KaYi.Services.EventServices
{
	public static class EventService
	{
		private static string logFilePath = ConfigurationManager.AppSettings["LogFile"];
		public static void AppendLog(EventLog eventLog)
		{
			FileStream expr_0B = new FileStream(EventService.logFilePath, FileMode.Append);
			StreamWriter expr_11 = new StreamWriter(expr_0B);
			expr_11.Write("----------------------------------------------------------\r\n");
			expr_11.Write(string.Format("EventID:{0}\r\n", eventLog.EventID));
			expr_11.Write(string.Format("TIME:{0}\r\n", eventLog.Time.ToString()));
			expr_11.Write(string.Format("Source:{0}\r\n", eventLog.EventSource));
			expr_11.Write(string.Format("Code:{0}\r\n", eventLog.EventCode));
			expr_11.Write(string.Format("Error Message:{0}\r\n", eventLog.EventDescription));
			expr_11.Write("----------------------------------------------------------\r\n");
			expr_11.Write("\r\n");
			expr_11.Flush();
			expr_11.Close();
			expr_0B.Close();
		}
		public static void AppendToLogFileToAbsFile(string fileName, string logMessage)
		{
			FileStream expr_07 = new FileStream(fileName, FileMode.Append);
			StreamWriter expr_17 = new StreamWriter(expr_07, Encoding.GetEncoding("UTF-8"));
			expr_17.Write(logMessage);
			expr_17.Flush();
			expr_17.Close();
			expr_17.Dispose();
			expr_07.Close();
			expr_07.Dispose();
		}
		public static void AppendToLogFile(string fileName, string logMessage)
		{
			FileStream expr_62 = new FileStream(string.Format("{0}{1}_{2}_{3}_log.txt", new object[]
			{
				fileName,
				DateTime.Now.Year.ToString(),
				DateTime.Now.Month.ToString(),
				DateTime.Now.Day.ToString()
			}), FileMode.Append);
			StreamWriter expr_68 = new StreamWriter(expr_62);
			expr_68.Write(string.Format("{0}\r\n", logMessage));
			expr_68.Flush();
			expr_68.Close();
			expr_62.Close();
		}
	}
}
