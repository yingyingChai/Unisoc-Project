using KaYi.Database;
using KaYi.Web.Infrastructure.Model.System.Session;
using System;
using System.Collections.Generic;
namespace KaYi.Web.Infrastructure.DAL.System.Session
{
	public class SessionGateway
	{
		private DALGateway<WebSession> dbGateway = new DALGateway<WebSession>();
		public SessionGateway()
		{
			this.dbGateway.LoadSchema("T_SESSIONS");
		}
		public void AddNew(WebSession newSession)
		{
			newSession.CreateTime = (newSession.UpdateTime = DateTime.Now);
			newSession.UpdateStamp = Guid.NewGuid().ToString();
			this.dbGateway.AddNew(newSession);
		}
		public void DeleteBySessionID(string sessionID)
		{
			this.dbGateway.DeleteByFieldValue("SessionID", sessionID);
		}
		public void UpdateByPK(WebSession objSession)
		{
			objSession.UpdateTime = DateTime.Now;
			objSession.UpdateStamp = Guid.NewGuid().ToString();
			this.dbGateway.UpdateByFieldValue("SessionID", objSession.SessionID, objSession);
		}
		public WebSession GetSessionsBySessionID(string sessionID)
		{
			Conditions conditions = new Conditions();
			conditions.ConditionExpressions.Add(new Condition("SessionID", Operator.EqualTo, sessionID));
			return this.dbGateway.getRecord(conditions);
		}
		public IList<WebSession> GetSessionByAccountID(string accountID, int pageIndex, int pageSize, out int recordCount)
		{
			Conditions conditions = new Conditions();
			conditions.ConditionExpressions.Add(new Condition("AccountID", Operator.EqualTo, accountID));
			OrderExpression orderExpression = new OrderExpression("StartTime", true);
			return this.dbGateway.getRecords(pageIndex, pageSize, conditions, orderExpression, null, Connector.AND, out recordCount);
		}
	}
}
