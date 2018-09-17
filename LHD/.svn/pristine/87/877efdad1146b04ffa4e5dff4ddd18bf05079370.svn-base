namespace Spreadtrum.LHD.DAL.Systems
{
    using KaYi.Database;
    using KaYi.Utilities;
    using Spreadtrum.LHD.Entity.Systems;
    using Spreadtrum.LHD.Entity.Users;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public class NotificationGateway
    {
        private DALGateway<Notification> dbGateway = new DALGateway<Notification>();

        public NotificationGateway()
        {
            this.dbGateway.LoadSchema("NOTIFICATIONS");
        }

        public void AddNew(Notification newNotification)
        {
            this.dbGateway.AddNew(newNotification);
        }

        public void DeleteByNotificationID(string NotificationID)
        {
            this.dbGateway.DeleteByFieldValue("MessageID", NotificationID);
        }

        public Notification GetNotificationByID(string NotificationID)
        {
            Conditions conditions = new Conditions {
                ConditionExpressions = { new Condition("MessageID", Operator.EqualTo, NotificationID) }
            };
            return this.dbGateway.getRecord(conditions);
        }

        public IList<Notification> GetNotificationsBy(string recipientID, string lotID, bool unreadOnly, NotificationTypes messageType, string orderBy, bool desc, int pageIndex, int pageSize, out int recordCount)
        {
            Conditions conditions = new Conditions();
            if (!StringHelper.isNullOrEmpty(recipientID))
            {
                conditions.ConditionExpressions.Add(new Condition("RecipientID", Operator.EqualTo, recipientID));
            }
            if (!StringHelper.isNullOrEmpty(lotID))
            {
                conditions.ConditionExpressions.Add(new Condition("LotID", Operator.EqualTo, lotID));
            }
            if (unreadOnly)
            {
                conditions.ConditionExpressions.Add(new Condition("Opened", Operator.EqualTo, false));
            }
            if (messageType != NotificationTypes.NotSpecified)
            {
                conditions.ConditionExpressions.Add(new Condition("NotificationType", Operator.EqualTo, messageType));
            }
            conditions.Connector = Connector.AND;
            OrderExpression orderExpression = null;
            if (!StringHelper.isNullOrEmpty(orderBy))
            {
                orderExpression = new OrderExpression(orderBy, desc);
            }
            return this.dbGateway.getRecords(pageIndex, pageSize, conditions, orderExpression, null, Connector.AND, out recordCount);
        }

        public void UpdateByPK(Notification objNotification)
        {
            this.dbGateway.UpdateByFieldValue("MessageID", objNotification.MessageID, objNotification);
        }

        public void SetNotificationStatus(string role, string lotID, string Status,string NotificationType)
        {
            this.dbGateway.ExecuteSQLStatement("EXEC sp_SetNotificationStatus '" + role.Replace("Admin","")+ "','" + lotID + "','" + Status + "','" + NotificationType + "'");
        }

        public void NewCommentNotification(string role, string lotID, string commentID)
        {
            this.dbGateway.ExecuteSQLStatement("EXEC sp_NewCommentNotification '" + role.Replace("Admin", "") + "','" + lotID + "','" + commentID + "'");
        }
    }
}

