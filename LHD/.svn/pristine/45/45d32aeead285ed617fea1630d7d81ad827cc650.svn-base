using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KaYi.Database;
using KaYi.Utilities;
using Spreadtrum.LHD.Entity.Lots;
using System.Data;

namespace Spreadtrum.LHD.DAL.Lots
{
   public class Lot_TransformedGateway
    {
        private DALGateway<Lot_Transformed> dbGateway = new DALGateway<Lot_Transformed>();
        public Lot_TransformedGateway()
        {
            this.dbGateway.LoadSchema("LOTS_TRANSFORMED");
        }
        public IList<Lot_Transformed> GetAllLots(Lot_TransformedQuery lot, int pageSize, int pageIndex, out int recordCount)
        {
            Conditions conditions = GetCondition(lot);
            OrderExpression orderExpression = null;
            if (!StringHelper.isNullOrEmpty(lot.OrderBy))
            {
                orderExpression = new OrderExpression(lot.OrderBy, lot.OrderDesc);
            }

            IList<Lot_Transformed> list = this.dbGateway.getRecords(pageIndex, pageSize, conditions, orderExpression, null, Connector.AND, out recordCount);
            return list;
        }
               
        private Conditions GetCondition(Lot_TransformedQuery lot)
        {
            Conditions conditions = new Conditions();
            //if (!StringHelper.isNullOrEmpty(lot.SearchType)) {
            //    conditions.ConditionExpressions.Add(new Condition("AutoJudeResult", Operator.EqualTo, lot.SearchType));
            //}
            if (!StringHelper.isNullOrEmpty(lot.ProductName))
            {
                conditions.ConditionExpressions.Add(new Condition("ProductName", Operator.Like, lot.ProductName));
            }
            if (!StringHelper.isNullOrEmpty(lot.WaferCode))
            {
                conditions.ConditionExpressions.Add(new Condition("WaferCode", Operator.Like, lot.WaferCode));
            }
            if (!StringHelper.isNullOrEmpty(lot.Osat))
            {
                conditions.ConditionExpressions.Add(new Condition("Vendor", Operator.EqualTo, lot.Osat));
            }
           
            if (!StringHelper.isNullOrEmpty(lot.LotId))
            {
                conditions.ConditionExpressions.Add(new Condition("LotId", Operator.Like, lot.LotId));
            }
            //if (lot.Status>0 && lot.OutClose<=0)
            //{
            //    conditions.ConditionExpressions.Add(new Condition("Status", Operator.EqualTo, lot.Status));
            //}
            if (lot.OutClose > 0)
            {
                if (lot.Status > 0)
                {
                    if (lot.PeVendorState > 0)
                    {
                        conditions.ConditionExpressions.Add(new Condition("Status", Operator.Between, lot.Status,lot.PeVendorState));
                    }
                    else {
                        conditions.ConditionExpressions.Add(new Condition("Status", Operator.EqualTo, lot.Status));
                    }
                }
                else
                {
                    conditions.ConditionExpressions.Add(new Condition("Status", Operator.LessThan, (int)WaferStatus.Close));
                }
            }
            else {
                if (lot.Status > 0)
                {
                    if (lot.PeVendorState > 0)
                    {
                        conditions.ConditionExpressions.Add(new Condition("Status", Operator.Between, lot.Status, lot.PeVendorState));
                    }
                    else
                    {
                        conditions.ConditionExpressions.Add(new Condition("Status", Operator.EqualTo, lot.Status));
                    }
                }
            }
            if (!StringHelper.isNullOrEmpty(lot.HoldReason))
            {
                conditions.ConditionExpressions.Add(new Condition("HoldReason", Operator.Like, lot.HoldReason));
            }
            if (lot.WfCount>0)
            {
                conditions.ConditionExpressions.Add(new Condition("WfCount", Operator.EqualTo, lot.WfCount));
            }
            if (!StringHelper.isNullOrEmpty(lot.Yield))
            {
                conditions.ConditionExpressions.Add(new Condition("Yield", Operator.EqualTo, lot.Yield));
            }
            if (!StringHelper.isNullOrEmpty(lot.CompletionDate))
            {
                string[] dateArray = lot.CompletionDate.Split('-');
                conditions.ConditionExpressions.Add(new Condition("CompletionDate", Operator.Between, dateArray[0], dateArray[1]+ " 23:59:59"));
            }
            if (lot.LastDays > 0) {
                var dt = DateTime.Now.ToLocalTime();
                if (lot.LastDays == 1)
                {
                    conditions.ConditionExpressions.Add(new Condition("CompletionDate", Operator.Between, dt.AddHours(-24), dt));
                }
                else {
                    string str_startTime = dt.AddDays(-2).ToShortDateString();
                    conditions.ConditionExpressions.Add(new Condition("CompletionDate", Operator.Between,str_startTime,dt.ToShortDateString() ));
                }
            }
            conditions.ConditionExpressions.Add(new Condition("RecordType",Operator.EqualTo, "Record"));
            conditions.Connector = Connector.AND;
            return conditions;
        }

        public Lot_Transformed GetTransformById(string id)
        {
            Conditions con = new Conditions();
            if (!string.IsNullOrEmpty(id))
            {
                con.ConditionExpressions.Add(new Condition("ID",Operator.EqualTo,id));
            }
            return this.dbGateway.getRecord(con);
        }

        public void UpdateLot(Lot_Transformed lot)
        {
            this.dbGateway.UpdateByFieldValue("ID", lot.ID, lot);
        }
        public int SaveLot(Lot_Transformed lot)
        {
           return this.dbGateway.AddNew(lot);
        }
        public void CPSocketSaveLot(Lot_Transformed lot)
        {
            Conditions conditions = new Conditions
            {
                ConditionExpressions = { new Condition("LotId", Operator.EqualTo, lot.LotId) }
            };
            conditions.ConditionExpressions.Add(new Condition("RecordType", Operator.EqualTo, "Record"));
            conditions.ConditionExpressions.Add(new Condition("Stage", Operator.EqualTo, lot.Stage));
            conditions.Connector = Connector.AND;
            Lot_Transformed currentEntity = this.dbGateway.getRecord(conditions);
            if (currentEntity != null)
            {
                this.dbGateway.AddNew(lot);
                currentEntity.VersionID = currentEntity.VersionID + 1;
                currentEntity.RecordType = "History";
                this.dbGateway.UpdateByFieldValue("ID", currentEntity.ID, currentEntity);
            }
            else
            {
                this.SaveLot(lot);
            }
        }
    }
}
