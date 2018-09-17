namespace Spreadtrum.LHD.DAL.Lots
{
    using KaYi.Database;
    using Spreadtrum.LHD.Entity.Lots;
    using System;
    using System.Collections.Generic;

    public class LotStatusListGateway
    {
        private DALGateway<LotStatusList> dbGateway = new DALGateway<LotStatusList>();

        public LotStatusListGateway()
        {
            this.dbGateway.LoadSchema("LOT_STATUS_LIST");
        }

        public IList<LotStatusList> GetLotStatusListsBy(int QADispose, int PEDispose, bool otherBinDispose, bool vendorConfirmed, bool otherBinDisposeConfirmed)
        {
            Conditions conditions = new Conditions {
                ConditionExpressions = { new Condition("QADispose", Operator.EqualTo, QADispose), new Condition("PEDispose", Operator.EqualTo, PEDispose), new Condition("OtherBinDispose", Operator.EqualTo, otherBinDispose), new Condition("VenderConfirmed", Operator.EqualTo, vendorConfirmed), new Condition("OtherBinDisposeConfirmed", Operator.EqualTo, otherBinDisposeConfirmed) },
                Connector = Connector.AND
            };
            int recordCount = 0;
            return this.dbGateway.getRecords(0, 0x270f, conditions, null, null, Connector.AND, out recordCount);
        }
    }
}

