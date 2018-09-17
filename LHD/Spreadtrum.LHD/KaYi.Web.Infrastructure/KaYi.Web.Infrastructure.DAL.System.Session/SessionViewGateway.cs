using KaYi.Database;
using KaYi.Web.Infrastructure.Model.System.Session;
using System;
using System.Collections.Generic;
namespace KaYi.Web.Infrastructure.DAL.System.Session
{
	public class SessionViewGateway
	{
		private DALGateway<SessionView> dbGateway = new DALGateway<SessionView>();
		public SessionViewGateway()
		{
			this.dbGateway.LoadSchema("V_SESSIONS");
		}
		public IList<SessionView> GetSessionViewsBy(string accountID, int pageIndex, int pageSize, out int recordCount)
		{
			Conditions conditions = new Conditions();
			conditions.ConditionExpressions.Add(new Condition("AccountID", Operator.EqualTo, accountID));
			OrderExpression orderExpression = new OrderExpression("StartTime", true);
			return this.dbGateway.getRecords(pageIndex, pageSize, conditions, orderExpression, null, Connector.AND, out recordCount);
		}
		public SessionView GetSessionViewByID(string SessionViewID)
		{
			Conditions conditions = new Conditions();
			conditions.ConditionExpressions.Add(new Condition("SessionViewID", Operator.EqualTo, SessionViewID));
			return this.dbGateway.getRecord(conditions);
		}
	}
}
