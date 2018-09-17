namespace Spreadtrum.LHD.DAL.Systems
{
    using KaYi.Database;
    using KaYi.Utilities;
    using Spreadtrum.LHD.Entity.Systems;
    using System;
    using System.Collections.Generic;

    public class UnreadMessageCounterGateway
    {
        private DALGateway<UnreadMessageCounter> dbGateway = new DALGateway<UnreadMessageCounter>();

        public UnreadMessageCounterGateway()
        {
            this.dbGateway.LoadSchema("VW_UNREADMESSAGESBYUSERIDANDLOTID");
        }

        public IList<UnreadMessageCounter> GetUnreadMessageCountersBy(string userID, IList<string> lotIds, NotificationTypes notificationType)
        {
            string str = string.Empty;
            foreach (string str3 in lotIds)
            {
                str = str + "'" + str3 + "',";
            }
            if (str.Length > 0)
            {
                str = str.Substring(0, str.Length - 1);
            }
            Conditions conditions = new Conditions();
            if (!StringHelper.isNullOrEmpty(userID))
            {
                conditions.ConditionExpressions.Add(new Condition("RecipientID", Operator.EqualTo, userID));
            }
            conditions.ConditionExpressions.Add(new Condition("NotificationType", Operator.EqualTo, notificationType));
            conditions.Connector = Connector.AND;
            string str2 = string.Format(" LotId in ({0}) ", str);
            int recordCount = 0;
            return this.dbGateway.getRecords(0, lotIds.Count, conditions, null, StringHelper.BuildStringList(str2), Connector.AND, out recordCount);
        }
    }
}

