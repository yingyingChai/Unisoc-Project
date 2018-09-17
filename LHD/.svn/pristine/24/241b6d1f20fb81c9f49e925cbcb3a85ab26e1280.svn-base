namespace Spreadtrum.LHD.DAL.Lots
{
    using KaYi.Database;
    using KaYi.Utilities;
    using Spreadtrum.LHD.Entity.Lots;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public class LotViewGateway
    {
        private DALGateway<LotView> dbGateway = new DALGateway<LotView>();

        public LotViewGateway()
        {
            this.dbGateway.LoadSchema("VW_LOTS");
        }

        public LotView GetLotByRecordID(string ID)
        {
            Conditions conditions = new Conditions {
                ConditionExpressions = { new Condition("ID", Operator.EqualTo, ID) }
            };
            return this.dbGateway.getRecord(conditions);
        }

        public IList<LotView> GetLotHistoryByLotID(string lotID)
        {
            int recordCount = 0;
            Conditions conditions = new Conditions();
            if (!StringHelper.isNullOrEmpty(lotID))
            {
                conditions.ConditionExpressions.Add(new Condition("LotID", Operator.EqualTo, lotID));
            }
            OrderExpression orderExpression = new OrderExpression("CreateTime", true);
            return this.dbGateway.getRecords(0, 0x1869f, conditions, orderExpression, null, Connector.AND, out recordCount);
        }

        public IList<LotView> GetLotViewBy(LotQuery query, string orderBy, bool desc, int pageIndex, int pageSize, out int recordCount)
        {
            Conditions conditions = new Conditions();
            if (query != null)
            {
                if (query.AutoJudgeResult != AutoJudgeResult.Empty)
                {
                    conditions.ConditionExpressions.Add(new Condition("AutoJudgeResult", Operator.EqualTo, query.AutoJudgeResult));
                }
                if (!StringHelper.isNullOrEmpty(query.Bin1))
                {
                    conditions.ConditionExpressions.Add(new Condition("Bin1", Operator.EqualTo, query.Bin1));
                }
                if (!StringHelper.isNullOrEmpty(query.Bin2))
                {
                    conditions.ConditionExpressions.Add(new Condition("Bin2", Operator.EqualTo, query.Bin2));
                }
                if (!StringHelper.isNullOrEmpty(query.Bin3))
                {
                    conditions.ConditionExpressions.Add(new Condition("Bin3", Operator.EqualTo, query.Bin3));
                }
                if (!StringHelper.isNullOrEmpty(query.Bin4))
                {
                    conditions.ConditionExpressions.Add(new Condition("Bin4", Operator.EqualTo, query.Bin4));
                }
                if (!StringHelper.isNullOrEmpty(query.Bin5))
                {
                    conditions.ConditionExpressions.Add(new Condition("Bin5", Operator.EqualTo, query.Bin5));
                }
                if (!StringHelper.isNullOrEmpty(query.Bin6))
                {
                    conditions.ConditionExpressions.Add(new Condition("Bin6", Operator.EqualTo, query.Bin6));
                }
                if (!StringHelper.isNullOrEmpty(query.Bin7))
                {
                    conditions.ConditionExpressions.Add(new Condition("Bin7", Operator.EqualTo, query.Bin7));
                }
                if (!StringHelper.isNullOrEmpty(query.Bin8))
                {
                    conditions.ConditionExpressions.Add(new Condition("Bin8", Operator.EqualTo, query.Bin8));
                }
                if (!StringHelper.isNullOrEmpty(query.Bin9))
                {
                    conditions.ConditionExpressions.Add(new Condition("Bin9", Operator.EqualTo, query.Bin9));
                }
                if (!StringHelper.isNullOrEmpty(query.Bin10))
                {
                    conditions.ConditionExpressions.Add(new Condition("Bin10", Operator.EqualTo, query.Bin10));
                }
                if (!StringHelper.isNullOrEmpty(query.CompletionDate))
                {
                    conditions.ConditionExpressions.Add(new Condition("CompletionDate", Operator.Between, query.CompletionDate, query.CompletionDate + " 23:59:59"));
                }
                if (!StringHelper.isNullOrEmpty(query.Damage))
                {
                    conditions.ConditionExpressions.Add(new Condition("Damage", Operator.EqualTo, query.Damage));
                }
                if (!StringHelper.isNullOrEmpty(query.DeviceCode))
                {
                    conditions.ConditionExpressions.Add(new Condition("DeviceCode", Operator.Like, query.DeviceCode));
                }
                if (!StringHelper.isNullOrEmpty(query.DeviceName))
                {
                    conditions.ConditionExpressions.Add(new Condition("DeviceName", Operator.Like, query.DeviceName));
                }
                if (!StringHelper.isNullOrEmpty(query.Die1CP))
                {
                    conditions.ConditionExpressions.Add(new Condition("Die1CP", Operator.Like, query.Die1CP));
                }
                if (!StringHelper.isNullOrEmpty(query.Die1LotNo))
                {
                    conditions.ConditionExpressions.Add(new Condition("Die1LotNO", Operator.Like, query.Die1LotNo));
                }
                if (!StringHelper.isNullOrEmpty(query.Die2CP))
                {
                    conditions.ConditionExpressions.Add(new Condition("Die2CP", Operator.Like, query.Die2CP));
                }
                if (!StringHelper.isNullOrEmpty(query.Die2LotNo))
                {
                    conditions.ConditionExpressions.Add(new Condition("Die2LotNO", Operator.Like, query.Die2LotNo));
                }
                if (!StringHelper.isNullOrEmpty(query.Die3CP))
                {
                    conditions.ConditionExpressions.Add(new Condition("Die3CP", Operator.Like, query.Die3CP));
                }
                if (!StringHelper.isNullOrEmpty(query.Die3LotNo))
                {
                    conditions.ConditionExpressions.Add(new Condition("Die3LotNO", Operator.Like, query.Die3LotNo));
                }
                if (!StringHelper.isNullOrEmpty(query.Die4CP))
                {
                    conditions.ConditionExpressions.Add(new Condition("Die4CP", Operator.Like, query.Die4CP));
                }
                if (!StringHelper.isNullOrEmpty(query.Die4LotNo))
                {
                    conditions.ConditionExpressions.Add(new Condition("Die4LotNO", Operator.Like, query.Die4LotNo));
                }
                if (!StringHelper.isNullOrEmpty(query.HoldReason))
                {
                    conditions.ConditionExpressions.Add(new Condition("HoldReason", Operator.Like, query.HoldReason));
                }
                if (!StringHelper.isNullOrEmpty(query.LBNO))
                {
                    conditions.ConditionExpressions.Add(new Condition("LBNO", Operator.Like, query.LBNO));
                }
                if (!StringHelper.isNullOrEmpty(query.Loss))
                {
                    conditions.ConditionExpressions.Add(new Condition("Loss", Operator.EqualTo, query.Loss));
                }
                if (!StringHelper.isNullOrEmpty(query.LotNO))
                {
                    conditions.ConditionExpressions.Add(new Condition("LotNO", Operator.Like, query.LotNO));
                }
                if (!StringHelper.isNullOrEmpty(query.LotType))
                {
                    conditions.ConditionExpressions.Add(new Condition("LotType", Operator.EqualTo, query.LotType));
                }
                if (query.ManualHold != -1)
                {
                    conditions.ConditionExpressions.Add(new Condition("ManualHold", Operator.EqualTo, query.ManualHold));
                }
                if (query.OtherBinDispose != -1)
                {
                    conditions.ConditionExpressions.Add(new Condition("OtherBinDispose", Operator.EqualTo, query.OtherBinDispose));
                }
                if (!StringHelper.isNullOrEmpty(query.Osat))
                {
                    conditions.ConditionExpressions.Add(new Condition("VendorName", Operator.Like, query.Osat));
                }
                if (!StringHelper.isNullOrEmpty(query.OsRate))
                {
                    conditions.ConditionExpressions.Add(new Condition("RoundOSRate", Operator.Like, query.OsRate));
                }
                if (query.PEDispose != -1)
                {
                    conditions.ConditionExpressions.Add(new Condition("PEDispose", Operator.EqualTo, query.PEDispose));
                }
                if (!StringHelper.isNullOrEmpty(query.Platform))
                {
                    conditions.ConditionExpressions.Add(new Condition("Platforms", Operator.Like, query.Platform));
                }
                if (query.QADispose != -1)
                {
                    conditions.ConditionExpressions.Add(new Condition("QADispose", Operator.EqualTo, query.QADispose));
                }
                if (query.QtyIn != -1)
                {
                    conditions.ConditionExpressions.Add(new Condition("QtyIn", Operator.EqualTo, query.QtyIn));
                }
                if (query.RetestTimes != -1)
                {
                    conditions.ConditionExpressions.Add(new Condition("RetestTimes", Operator.EqualTo, query.RetestTimes));
                }
                if (query.SPRDDecision != -1)
                {
                    conditions.ConditionExpressions.Add(new Condition("SPRDDecision", Operator.EqualTo, query.SPRDDecision));
                }
                if (!StringHelper.isNullOrEmpty(query.SubconLot))
                {
                    conditions.ConditionExpressions.Add(new Condition("SubconLot", Operator.Like, query.SubconLot));
                }
                if (!StringHelper.isNullOrEmpty(query.TesterID))
                {
                    conditions.ConditionExpressions.Add(new Condition("TesterID", Operator.Like, query.TesterID));
                }
                if (!StringHelper.isNullOrEmpty(query.TestProgram))
                {
                    conditions.ConditionExpressions.Add(new Condition("TestProgram", Operator.Like, query.TestProgram));
                }
                if (!StringHelper.isNullOrEmpty(query.Status))
                {
                    conditions.ConditionExpressions.Add(new Condition("Status", Operator.EqualTo, query.Status));
                }
                if (!StringHelper.isNullOrEmpty(query.Yield))
                {
                    conditions.ConditionExpressions.Add(new Condition("RoundYield", Operator.Like, query.Yield));
                }
                if (!StringHelper.isNullOrEmpty(query.Stage))
                {
                    conditions.ConditionExpressions.Add(new Condition("Stage", Operator.Like, query.Stage));
                }
            }
            conditions.ConditionExpressions.Add(new Condition("RecordType", Operator.EqualTo, "Record"));
            conditions.Connector = Connector.AND;
            OrderExpression orderExpression = null;
            if (!StringHelper.isNullOrEmpty(orderBy))
            {
                orderExpression = new OrderExpression(orderBy, desc);
            }
            return this.dbGateway.getRecords(pageIndex, pageSize, conditions, orderExpression, null, Connector.AND, out recordCount);
        }

        public LotView GetLotViewByLotID(string lotID)
        {
            Conditions conditions = new Conditions {
                ConditionExpressions = { new Condition("LotID", Operator.EqualTo, lotID), new Condition("RecordType", Operator.EqualTo, "Record") }
            };
            OrderExpression orderExpression = new OrderExpression("VersionID", true);
            conditions.Connector = Connector.AND;
            int recordCount = 0;
            IList<LotView> list = this.dbGateway.getRecords(0, 0x270f, conditions, orderExpression, null, Connector.AND, out recordCount);
            if ((list != null) && (list.Count > 0))
            {
                return list[0];
            }
            return null;
        }
    }
}

