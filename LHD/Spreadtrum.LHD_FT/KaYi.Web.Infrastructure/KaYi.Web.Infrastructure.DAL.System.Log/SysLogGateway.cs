using KaYi.Database;
using KaYi.Web.Infrastructure.Model.System.Log;
using System;
namespace KaYi.Web.Infrastructure.DAL.System.Log
{
	public class SysLogGateway
	{
		private DALGateway<SysLog> dbGateway = new DALGateway<SysLog>();
		public SysLogGateway()
		{
			this.dbGateway.LoadSchema("T_SYS_LOG");
		}
		public void AddNew(SysLog newSysLog)
		{
			newSysLog.CreateTime = (newSysLog.UpdateTime = DateTime.Now);
			newSysLog.UpdateStamp = Guid.NewGuid().ToString();
			this.dbGateway.AddNew(newSysLog);
		}
		public void DeleteBySysLogID(string syslogID)
		{
			this.dbGateway.DeleteByFieldValue("OperateID", syslogID);
		}
		public void UpdateByPK(SysLog objSysLog)
		{
			objSysLog.UpdateTime = DateTime.Now;
			objSysLog.UpdateStamp = Guid.NewGuid().ToString();
			this.dbGateway.UpdateByFieldValue("OperateID", objSysLog.OperateID, objSysLog);
		}
		public SysLog GetSysLogsBySysLogID(string syslogID)
		{
			Conditions conditions = new Conditions();
			conditions.ConditionExpressions.Add(new Condition("OperateID", Operator.EqualTo, syslogID));
			return this.dbGateway.getRecord(conditions);
		}
		public int GetAccessCountIn(string ip, int mins)
		{
			Conditions conditions = new Conditions();
			conditions.ConditionExpressions.Add(new Condition("IPAdd", Operator.EqualTo, ip));
			conditions.Connector = Connector.AND;
			DateTime now = DateTime.Now;
			DateTime dateTime = now.AddMinutes(-1.0);
			return this.dbGateway.GetRecordCountByConditions(conditions, string.Format(" OperateTime between '{0}' and '{1}'", dateTime.ToString(), now.ToString()));
		}
	}
}
