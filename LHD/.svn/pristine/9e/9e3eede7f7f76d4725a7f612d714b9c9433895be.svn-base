using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KaYi.Database;
using KaYi.Utilities;
using Spreadtrum.LHD.Entity.Lots;
namespace Spreadtrum.LHD.DAL.Lots
{
  public  class Wafer_HistoryGateway
    {
        private DALGateway<Wafer_History> dbGateway = new DALGateway<Wafer_History>();
        public Wafer_HistoryGateway()
        {
            this.dbGateway.LoadSchema("WAFER_HISTORY");
        }
        public int AddHistory(Wafer_History history)
        {
           return this.dbGateway.AddNew(history);
        }
    }
    public class VWWaferHistoryGateway
    {
        private DALGateway<VwWafer_History> dbGateway = new DALGateway<VwWafer_History>();
        public VWWaferHistoryGateway()
        {
            this.dbGateway.LoadSchema("VW_WAFERHISTORYS");
        }
        public IList<VwWafer_History> HistoryList(HistoryQuery query,int pageSize, int pageIndex, out int recordCount)
        {
            Conditions condition = GetConditions(query);
            OrderExpression orderExpression = null;
            if (!StringHelper.isEmptyString(query.OrderBy))
            {
                orderExpression = new OrderExpression(query.OrderBy,query.OrderDesc);
            }
            return this.dbGateway.getRecords(pageIndex, pageSize, condition, orderExpression, null, Connector.AND, out recordCount);
        }
        private Conditions GetConditions(HistoryQuery query)
        {
            Conditions condition = new Conditions();
            if (!StringHelper.isNullOrEmpty(query.ProductName)) {
                condition.ConditionExpressions.Add(new Condition("ProductName", Operator.Like,query.ProductName));
            }
            if (!StringHelper.isNullOrEmpty(query.WaferCode))
            {
                condition.ConditionExpressions.Add(new Condition("WaferCode", Operator.Like, query.WaferCode));
            }
            if (!StringHelper.isNullOrEmpty(query.LotId))
            {
                condition.ConditionExpressions.Add(new Condition("LotId", Operator.Like, query.LotId));
            }
            if (!StringHelper.isNullOrEmpty(query.WaferID))
            {
                condition.ConditionExpressions.Add(new Condition("WaferID", Operator.Like, query.WaferID));
            }
            if (query.Dispose>0)
            {
                if (query.Dispose == 111)
                {
                    condition.ConditionExpressions.Add(new Condition("IsVendor", Operator.EqualTo, true));
                }
                else {
                    condition.ConditionExpressions.Add(new Condition("Dispose", Operator.EqualTo, query.Dispose));
                }
            }
            if (!StringHelper.isNullOrEmpty(query.UserID))
            {
                condition.ConditionExpressions.Add(new Condition("UserID", Operator.Like, query.UserID));
            }
            if (!StringHelper.isNullOrEmpty(query.Comment))
            {
                condition.ConditionExpressions.Add(new Condition("Comment", Operator.Like, query.Comment));
            }
            if (!StringHelper.isNullOrEmpty(query.CreateTime))
            {
                string[] dateArray = query.CreateTime.Split('-');
                condition.ConditionExpressions.Add(new Condition("CreateTime", Operator.Between, dateArray[0], dateArray[1]+ " 23:59:59"));
            }
            condition.Connector = Connector.AND;
            return condition;
        }
    }
}
