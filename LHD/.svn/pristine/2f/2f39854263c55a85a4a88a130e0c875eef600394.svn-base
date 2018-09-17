using KaYi.Web.Infrastructure.DAL.System.Log;
using KaYi.Web.Infrastructure.Model.System.Log;
using System;
namespace KaYi.Web.Infrastructure.Services
{
	public class LogService
	{
		private static SysLogGateway logGateway = new SysLogGateway();
		public static void AppendSysLog(string ip, string operation, string _operator, string clientID, string sessionID, string accountID, string parameters, string absolutePath)
		{
			SysLog sysLog = new SysLog();
			sysLog.CreateTime = DateTime.Now;
			sysLog.IPAdd = ip;
			sysLog.OperateID = Guid.NewGuid().ToString();
			sysLog.OperateTime = DateTime.Now;
			sysLog.Operation = operation;
			sysLog.Operator = _operator;
			sysLog.Parameters = parameters;
			sysLog.AccountID = accountID;
			sysLog.SessionID = sessionID;
			sysLog.ClientID = clientID;
			sysLog.UpdateStamp = Guid.NewGuid().ToString();
			sysLog.UpdateTime = DateTime.Now;
			sysLog.AbsolutePath = absolutePath;
			LogService.logGateway.AddNew(sysLog);
		}
	}
}
