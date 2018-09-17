using KaYi.Database;
using KaYi.Web.Infrastructure.Model.System.VisitStatistics;
using System;
using System.Collections.Generic;
namespace KaYi.Web.Infrastructure.DAL.System.VisitStatistics
{
	public class VisitCounterByDateGateway
	{
		private DALGateway<VisitorCounterByDate> dbGateway = new DALGateway<VisitorCounterByDate>();
		public VisitCounterByDateGateway()
		{
			this.dbGateway.LoadSchema("V_VISITOR_COUNT_BY_DATE");
		}
		public IList<VisitorCounterByDate> GetVisitorCounterByDate(string hostName, DateTime startDate, DateTime endDate, int pageIndex, int pageSize, out int recordCount)
		{
			Conditions conditions = new Conditions();
			conditions.ConditionExpressions.Add(new Condition("StartDate", Operator.GreaterOrEqualTo, startDate));
			conditions.ConditionExpressions.Add(new Condition("StartDate", Operator.SmallerOrEqualTo, endDate));
			if (hostName != "" && hostName != null)
			{
				conditions.ConditionExpressions.Add(new Condition("HostName", Operator.EqualTo, hostName));
			}
			conditions.Connector = Connector.AND;
			OrderExpression orderExpression = new OrderExpression("StartDate", false);
			return this.dbGateway.getRecords(pageIndex, pageSize, conditions, orderExpression, null, Connector.AND, out recordCount);
		}
	}
}
