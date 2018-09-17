namespace Spreadtrum.LHD.DAL.Systems
{
    using KaYi.Database;
    using Spreadtrum.LHD.Entity.Systems;
    using System;
    using System.Collections.Generic;
    public class NotificationCounterGateway
    {
        private DALGateway<NotificationCounter> dbGateway = new DALGateway<NotificationCounter>();
        public NotificationCounterGateway()
        {
            this.dbGateway.LoadSchema("VW_NOTIFICATIONCOUNTER");
        }
        public IList<NotificationCounter> GetAllNotificationCounters()
        {
            int recordCount = 0;
            return this.dbGateway.getRecords(0, 0x3b9ac9ff, null, null, null, Connector.AND, out recordCount);
        }
    }
}

