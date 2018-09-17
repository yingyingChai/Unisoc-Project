namespace Spreadtrum.LHD.DAL.Lots
{
    using KaYi.Database;
    using KaYi.Utilities;
    using Spreadtrum.LHD.Entity.Lots;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public class SWBinGateway
    {
        private DALGateway<SWBin> dbGateway = new DALGateway<SWBin>();

        public SWBinGateway()
        {
            this.dbGateway.LoadSchema("SW_BIN");
        }

        public void AddNew(SWBin newSWBin)
        {
            this.dbGateway.AddNew(newSWBin);
        }

        public void DeleteByLotID(string lotID)
        {
            this.dbGateway.DeleteByFieldValue("LotID", lotID);
        }

        public void DeleteBySWBinID(string SWBinID)
        {
            this.dbGateway.DeleteByFieldValue("SWBinID", SWBinID);
        }

        public IList<SWBin> GetSWBinsBy(string lotID, string code, string defect, string qty, string failRate, string isPassed, string limited, string orderBy, bool desc, int pageIndex, int pageSize, out int recordCount)
        {
            Conditions conditions = new Conditions {
                ConditionExpressions = { new Condition("LotID", Operator.EqualTo, lotID) }
            };
            if (!StringHelper.isNullOrEmpty(code))
            {
                conditions.ConditionExpressions.Add(new Condition("Code", Operator.Like, code));
            }
            if (!StringHelper.isNullOrEmpty(defect))
            {
                conditions.ConditionExpressions.Add(new Condition("Defect", Operator.Like, defect));
            }
            if (!StringHelper.isNullOrEmpty(qty))
            {
                conditions.ConditionExpressions.Add(new Condition("Qty", Operator.Like, qty));
            }
            if (!StringHelper.isNullOrEmpty(failRate))
            {
                conditions.ConditionExpressions.Add(new Condition("FailRate", Operator.Like, failRate));
            }
            if (!StringHelper.isNullOrEmpty(isPassed))
            {
                conditions.ConditionExpressions.Add(new Condition("IsPassed", Operator.EqualTo, isPassed));
            }
            if (!StringHelper.isNullOrEmpty(limited))
            {
                conditions.ConditionExpressions.Add(new Condition("Limited", Operator.EqualTo, limited));
            }
            conditions.Connector = Connector.AND;
            OrderExpression orderExpression = null;
            if (!StringHelper.isNullOrEmpty(orderBy))
            {
                orderExpression = new OrderExpression(orderBy, desc);
                return this.dbGateway.getRecords(pageIndex, pageSize, conditions, orderExpression, null, Connector.AND, out recordCount);
            }
            return this.dbGateway.getRecordsWithCustomerOrderStatement(pageIndex, pageSize, conditions, " order by IsTriggerd,IsPassed", null, Connector.AND, out recordCount);
        }
    }
}

