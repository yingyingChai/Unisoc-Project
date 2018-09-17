namespace Spreadtrum.LHD.DAL.Systems
{
    using KaYi.Database;
    using Spreadtrum.LHD.Entity.Systems;
    using System;
    using System.Collections.Generic;

    public class SlicondashOSATConfigurationGateway
    {
        private DALGateway<SilicondashOSATConfiguration> dbGateway = new DALGateway<SilicondashOSATConfiguration>();

        public SlicondashOSATConfigurationGateway()
        {
            this.dbGateway.LoadSchema("SILICONDASH_OSAT_CONFIGURATIONS");
        }

        public IList<SilicondashOSATConfiguration> GetAllOSATConfigurations()
        {
            Conditions conditions = new Conditions {
                ConditionExpressions = { new Condition("States", Operator.EqualTo, 0) }
            };
            return this.dbGateway.getRecords(0, 0x270f, conditions, null);
        }
    }
}

