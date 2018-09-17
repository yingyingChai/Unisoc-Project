namespace Spreadtrum.LHD.DAL.Lots
{
    using KaYi.Database;
    using Spreadtrum.LHD.Entity.Lots;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public class LotGateway
    {
        private DALGateway<Lot> dbGateway = new DALGateway<Lot>();

        public LotGateway()
        {
            this.dbGateway.LoadSchema("LOTS");
        }

        public void AddLotRecord(Lot lot)
        {
            this.dbGateway.AddNew(lot);
        }

        public void ClearSystem()
        {
            this.dbGateway.ExecuteSQLStatement("delete from LOTS");
            this.dbGateway.ExecuteSQLStatement("delete from SW_BIN");
            this.dbGateway.ExecuteSQLStatement("delete from NOTIFICATIONS");
            this.dbGateway.ExecuteSQLStatement("delete from COMMENTS");
            this.dbGateway.ExecuteSQLStatement("delete from FILES");
        }

        public void DeleteLotRecords(string lotID)
        {
            this.dbGateway.DeleteByFieldValue("LotID", lotID);
        }

        public Lot GetFirstRecordByLotID(string lotID)
        {
            Conditions conditions = new Conditions {
                ConditionExpressions = { new Condition("LotID", Operator.EqualTo, lotID) }
            };
            OrderExpression orderExpression = new OrderExpression("VersionID", false);
            int recordCount = 0;
            IList<Lot> list = this.dbGateway.getRecords(0, 0x270f, conditions, orderExpression, null, Connector.AND, out recordCount);
            if ((list != null) && (list.Count > 0))
            {
                return list[0];
            }
            return null;
        }

        public Lot GetLotByLotID(string lotID)
        {
            Conditions conditions = new Conditions {
                ConditionExpressions = { new Condition("LotID", Operator.EqualTo, lotID), new Condition("RecordType", Operator.EqualTo, "Record") },
                Connector = Connector.AND
            };
            OrderExpression orderExpression = new OrderExpression("VersionID", true);
            int recordCount = 0;
            IList<Lot> list = this.dbGateway.getRecords(0, 0x270f, conditions, orderExpression, null, Connector.AND, out recordCount);
            if ((list != null) && (list.Count > 0))
            {
                return list[0];
            }
            return null;
        }

        public Lot GetLotByLotNO(string lotNO)
        {
            Conditions conditions = new Conditions {
                ConditionExpressions = { new Condition("LotNO", Operator.EqualTo, lotNO), new Condition("RecordType", Operator.EqualTo, "Record") },
                Connector = Connector.AND
            };
            return this.dbGateway.getRecord(conditions);
        }

        public Lot GetLotByRecordID(string id)
        {
            Conditions conditions = new Conditions {
                ConditionExpressions = { new Condition("ID", Operator.EqualTo, id) }
            };
            return this.dbGateway.getRecord(conditions);
        }

        public IList<Lot> GetLotsBy(int pageIndex, int pageSize, out int recordCount)
        {
            Conditions conditions = new Conditions {
                ConditionExpressions = { new Condition("RecordType", Operator.EqualTo, "Record") }
            };
            return this.dbGateway.getRecords(pageIndex, pageSize, conditions, null, null, Connector.AND, out recordCount);
        }

        public IList<Lot> GetLotsBySDStates(string osatID, int sdStates)
        {
            Conditions conditions = new Conditions {
                ConditionExpressions = { new Condition("VenderID", Operator.EqualTo, osatID), new Condition("SDStates", Operator.EqualTo, sdStates), new Condition("RecordType", Operator.EqualTo, "Record") },
                Connector = Connector.AND
            };
            int recordCount = 0;
            return this.dbGateway.getRecords(0, 0x270f, conditions, null, null, Connector.AND, out recordCount);
        }

        public void UpdateLot(Lot lot)
        {
            Conditions conditions = new Conditions {
                ConditionExpressions = { new Condition("ID", Operator.EqualTo, lot.ID) }
            };
            Lot newEntity = this.dbGateway.getRecord(conditions);
            newEntity.ID = Guid.NewGuid().ToString();
            newEntity.RecordType = "History";
            this.dbGateway.AddNew(newEntity);
            lot.VersionID = newEntity.VersionID + 1;
            this.dbGateway.UpdateByFieldValue("ID", lot.ID, lot);
        }

        public void SocketSaveLot(Lot lot)
        {
            Conditions conditions = new Conditions
            {
                ConditionExpressions = { new Condition("LotNO", Operator.EqualTo, lot.LotNO) }
            };
            conditions.ConditionExpressions.Add(new Condition("RecordType", Operator.EqualTo, "Record"));
            conditions.ConditionExpressions.Add(new Condition("Stage", Operator.EqualTo, lot.Stage));
            conditions.Connector = Connector.AND;
            Lot currentEntity = this.dbGateway.getRecord(conditions);
            if (currentEntity != null)
            {
                var _id = currentEntity.ID;
                currentEntity.ID = Guid.NewGuid().ToString();
                currentEntity.RecordType = "History";
                this.dbGateway.AddNew(currentEntity);
                lot.VersionID = currentEntity.VersionID + 1;
                lot.ID = _id;
                this.dbGateway.UpdateByFieldValue("ID", _id, lot);
            }
            else
            {
                this.AddLotRecord(lot);
            }
            this.dbGateway.ExecuteSQLStatement("EXEC sp_UpdateLotsReasons '" + lot.ID + "'");
        }
    }
}

