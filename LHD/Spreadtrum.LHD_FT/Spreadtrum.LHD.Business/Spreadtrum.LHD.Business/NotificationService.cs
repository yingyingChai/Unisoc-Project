namespace Spreadtrum.LHD.Business
{
    using KaYi.Services.Emails.Library.Gateway;
    using KaYi.Services.Emails.Library.Model;
    using Spreadtrum.LHD.DAL.Systems;
    using Spreadtrum.LHD.Entity.Lots;
    using Spreadtrum.LHD.Entity.Systems;
    using Spreadtrum.LHD.Entity.Users;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public static class NotificationService
    {
        public static EmailGateway emailGateway = new EmailGateway();
        public static NotificationCounterGateway notificationCounterGateway = new NotificationCounterGateway();
        public static NotificationGateway notificationGateway = new NotificationGateway();
        public static UnreadMessageCounterGateway unreadMessageGateway = new UnreadMessageCounterGateway();

        public static void ClearCommentNotificationByUserIDAndLotID(string userID, string lotID)
        {
            int recordCount = 0;
            foreach (Notification notification in GetNotificationsBy(userID, lotID, true, NotificationTypes.Comment, "", false, 0, 0x1869f, out recordCount))
            {
                notification.ReadTime = DateTime.Now;
                notification.Opened = true;
                notificationGateway.UpdateByPK(notification);
            }
        }

        public static void ClearNotificationsByOSATIDAndLotID(string vendorID, string lotID)
        {
            using (IEnumerator<OSATUser> enumerator = UserService.GetOSATUsersByOSATID(vendorID).GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    ClearOSATConfirmNotification(enumerator.Current.MD5CID, lotID);
                }
            }
        }

        public static void ClearNotificationsByRoleAndLotID(UserRoles role, string lotID, NotificationTypes notificationType)
        {
            int recordCount = 0;
            //foreach (User user in UserService.GetUsersByRole(role, 0, 0x270f, out recordCount))
            //{
            //    foreach (Notification notification in notificationGateway.GetNotificationsBy(user.UserID, lotID, true, notificationType, "", false, 0, 0x270f, out recordCount))
            //    {
            //        notification.ReadTime = DateTime.Now;
            //        notification.Opened = true;
            //        notificationGateway.UpdateByPK(notification);
            //    }
            //}
            notificationGateway.SetNotificationStatus(role.ToString(), lotID, "1", notificationType.ToString());
        }

        public static void ClearOSATConfirmNotification(string userID, string lotID)
        {
            int recordCount = 0;
            foreach (Notification notification in GetNotificationsBy(userID, lotID, true, NotificationTypes.Confirm, "", false, 0, 0x270f, out recordCount))
            {
                notification.ReadTime = DateTime.Now;
                notification.Opened = true;
                notificationGateway.UpdateByPK(notification);
            }
        }

        public static void ClearOtherBinDisposeNotificationByUserIDAndLotID(string userID, string lotID)
        {
            int recordCount = 0;
            foreach (Notification notification in GetNotificationsBy(userID, lotID, true, NotificationTypes.OtherBinDispose, "", false, 0, 0x270f, out recordCount))
            {
                notification.ReadTime = DateTime.Now;
                notification.Opened = true;
                notificationGateway.UpdateByPK(notification);
            }
        }

        public static void CreateDisposeNotification(Lot lot, string message, User user)
        {
            Notification newNotification = new Notification {
                CreateTime = DateTime.Now,
                EmailID = Guid.NewGuid().ToString(),
                LotID = lot.LotID,
                Message = message,
                MessageID = Guid.NewGuid().ToString(),
                NotificationType = NotificationTypes.LotDispose,
                Opened = false,
                ReadTime = Convert.ToDateTime("1999-12-31"),
                RecipientID = user.UserID,
                RecordState = 0,
                UpdateTime = DateTime.Now
            };
            notificationGateway.AddNew(newNotification);
            string str = emailGateway.GetEmailByID("SPRDDecision").Body.Replace("InsertFullNameHere", user.FullName).Replace("insertLotNoHere", lot.LotNO).Replace("InsertUrlHere", string.Format("https://lhd.spreadtrum.com/Lots/Details?LotID={0}", lot.LotID)).Replace("InsertTimeHere", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")).Replace("InsertStepFail", lot.HoldReason).Replace("InsertStepWarn", lot.ReasonStepWarn).Replace("InsertSBLFail", lot.ReasonSBLFail).Replace("InsertUntestedFail", lot.ReasonUntestedFail).Replace("InsertStepOrderFail", lot.ReasonStepOrderFail).Replace("InsertDamageHear", lot.Damage.ToString()).Replace("InsertLossHear", lot.Loss.ToString());
            object[] args = new object[] { lot.VenderID, lot.DeviceCode, lot.DeviceName, lot.LotNO, lot.LotType, lot.Stage };
            string subject = string.Format("[LHD] {0}/{1}/{2}/{3}/{4}/{5}", args);
            CreateEmail(newNotification.EmailID, newNotification.RecipientID, user.Email, subject, str, lot.LotNO);
        }

        private static void CreateEmail(string emailID, string userID, string recipient, string subject, string message, string lotNO)
        {
            Email email = new Email {
                AccountIDs = userID,
                Body = message,
                Cc = "",
                EmailID = emailID,
                HtmlFormat = true,
                NextTryTime = DateTime.Now.AddSeconds(30.0),
                Priority = 0,
                Recipients = recipient,
                Sender = "lhdadmin@spreadtrum.com",
                State = EmailState.Unsend,
                Subject = subject,
                TryTime = 10
            };
            emailGateway.AddNew(email);
        }

        public static void CreateNewCommentNotification(Lot lot, UserRoles recipientRole, Comment comment)
        {
            //int recordCount = 0;
            //var decision="";
            //if(comment.OperatorRole.ToLower().Contains("pe"))
            //{
            //    decision = "Decision: " + lot.PEDisposeText;
            //}
            //else if (comment.OperatorRole.ToLower().Contains("qa"))
            //{
            //    decision = "Decision: " + lot.QADisposeText;
            //}
            //foreach (User user in UserService.GetUsersByRole(recipientRole, 0, 0x270f, out recordCount))
            //{
            //    if (user.UserID != comment.Operator)
            //    {
            //        Notification newNotification = new Notification {
            //            CreateTime = DateTime.Now,
            //            EmailID = Guid.NewGuid().ToString(),
            //            LotID = lot.LotID,
            //            Message = string.Format("Lot has new Comment", new object[0]),
            //            MessageID = Guid.NewGuid().ToString(),
            //            NotificationType = NotificationTypes.Comment,
            //            Opened = false,
            //            ReadTime = Convert.ToDateTime("1999-12-31"),
            //            RecipientID = user.UserID,
            //            RecordState = 0,
            //            UpdateTime = DateTime.Now
            //        };
            //        notificationGateway.AddNew(newNotification);
                    
            //        string message = emailGateway.GetEmailByID("CommentNotification").Body.Replace("InsertFullNameHere", user.FullName).Replace("insertLotNoHere", lot.LotNO).Replace("InsertUrlHere", string.Format("https://lhd.spreadtrum.com/Lots/Details?LotID={0}", lot.LotID)).Replace("InsertTimeHere", DateTime.Now.ToString("yyyy-MM-dd hh:MM:ss")).Replace("InsertCommentOperator", comment.OperatorName).Replace("InsertComments", "Comments: "+ comment.CommentText).Replace("InsertDecisionHear", decision);
            //        object[] args = new object[] { lot.VenderID, lot.DeviceCode, lot.DeviceName, lot.LotNO, lot.LotType, lot.Stage };
            //        string subject = string.Format("[LHD] {0}/{1}/{2}/{3}/{4}/{5} has new comment.", args);
            //        CreateEmail(newNotification.EmailID, newNotification.RecipientID, user.Email, subject, message, lot.LotNO);
            //    }
            //}

            notificationGateway.NewCommentNotification(recipientRole.ToString(), lot.ID, comment.CommentID);
        }

        public static void CreateNewCommentNotificationForOSATUsers(Lot lot, Comment comment)
        {
            var decision = "";
            if (comment.OperatorRole.ToLower().Contains("pe"))
            {
                decision = "Decision: " + lot.PEDisposeText;
            }
            else if (comment.OperatorRole.ToLower().Contains("qa"))
            {
                decision = "Decision: " + lot.QADisposeText;
            }
            
            foreach (OSATUser user in UserService.GetOSATUsersByOSATID(lot.VenderID))
            {
                if (user.MD5CID != comment.Operator)
                {
                    Notification newNotification = new Notification {
                        CreateTime = DateTime.Now,
                        EmailID = Guid.NewGuid().ToString(),
                        LotID = lot.LotID,
                        Message = string.Format("Lot has new Comment", new object[0]),
                        MessageID = Guid.NewGuid().ToString(),
                        NotificationType = NotificationTypes.Comment,
                        Opened = false,
                        ReadTime = Convert.ToDateTime("1999-12-31"),
                        RecipientID = user.MD5CID,
                        RecordState = 0,
                        UpdateTime = DateTime.Now
                    };
                    notificationGateway.AddNew(newNotification);
                    string message = emailGateway.GetEmailByID("CommentNotification").Body.Replace("InsertFullNameHere", user.SupUserName).Replace("insertLotNoHere", lot.LotNO).Replace("InsertUrlHere", string.Format("https://lhd.spreadtrum.com/Lots/Details?LotID={0}", lot.LotID)).Replace("InsertTimeHere", DateTime.Now.ToString("yyyy-MM-dd hh:MM:ss")).Replace("InsertCommentOperator", comment.OperatorName).Replace("InsertComments", "Comments: " + comment.CommentText).Replace("InsertDecisionHear", decision);
                    object[] args = new object[] { lot.VenderID, lot.DeviceCode, lot.DeviceName, lot.LotNO, lot.LotType, lot.Stage };
                    string subject = string.Format("[LHD] {0}/{1}/{2}/{3}/{4}/{5} has new comment.", args);
                    CreateEmail(newNotification.EmailID, newNotification.RecipientID, user.SupMail, subject, message, lot.LotNO);
                }
            }
        }

        public static void CreateOSATConfirmNotificationsWhileLotChanged(string osatID, Lot lot, string message)
        {
            new Dictionary<string, string>();
            foreach (OSATUser user in UserService.GetOSATUsersByOSATID(osatID))
            {
                Notification newNotification = new Notification {
                    CreateTime = DateTime.Now,
                    EmailID = Guid.NewGuid().ToString(),
                    LotID = lot.LotID,
                    Message = message,
                    MessageID = Guid.NewGuid().ToString(),
                    NotificationType = NotificationTypes.Confirm,
                    Opened = false,
                    ReadTime = Convert.ToDateTime("1999-12-31"),
                    RecipientID = user.MD5CID,
                    RecordState = 0,
                    UpdateTime = DateTime.Now
                };
                notificationGateway.AddNew(newNotification);
                string str = emailGateway.GetEmailByID("OSATNotification").Body.Replace("InsertFullNameHere", user.SupUserName).Replace("insertLotNoHere", lot.LotNO).Replace("InsertUrlHere", string.Format("https://lhd.spreadtrum.com/Lots/Details?LotID={0}", lot.LotID)).Replace("InsertTimeHere", DateTime.Now.ToString("yyyy-MM-dd hh:MM:ss"));
                string subject = string.Format("Please confirm Lot {0},", lot.LotNO);
                string newValue = string.Empty;
                if (!(message == "SPRDDECISION"))
                {
                    if (message == "OTHERBINDISPOSE")
                    {
                        goto Label_0175;
                    }
                }
                else
                {
                    newValue = LotService.GetDisposeTextByDispose(lot.SPRDDecision);
                }
                goto Label_017C;
            Label_0175:
                newValue = "Other Bin Dispose.";
            Label_017C:
                subject = subject + newValue;
                str = str.Replace("insertDisposeTextHere", newValue);
                CreateEmail(newNotification.EmailID, newNotification.RecipientID, user.SupMail, subject, str, lot.LotNO);
            }
        }

        public static void CreateOSATEQANotificationsWhileLotArrived(string osatID, Lot lot, string message)
        {
            new Dictionary<string, string>();
            foreach (OSATUser user in UserService.GetOSATUsersByOSATID(osatID))
            {
                Notification newNotification = new Notification {
                    CreateTime = DateTime.Now,
                    EmailID = Guid.NewGuid().ToString(),
                    LotID = lot.LotID,
                    Message = message,
                    MessageID = Guid.NewGuid().ToString(),
                    NotificationType = NotificationTypes.EQANotification,
                    Opened = false,
                    ReadTime = Convert.ToDateTime("1999-12-31"),
                    RecipientID = user.MD5CID,
                    RecordState = 0,
                    UpdateTime = DateTime.Now
                };
                notificationGateway.AddNew(newNotification);
                string str = emailGateway.GetEmailByID("OSATEQANotification").Body.Replace("InsertFullNameHere", user.SupUserName).Replace("insertLotNoHere", lot.LotNO).Replace("InsertUrlHere", string.Format("https://lhd.spreadtrum.com/Lots/Details?LotID={0}", lot.LotID)).Replace("InsertTimeHere", DateTime.Now.ToString("yyyy-MM-dd hh:MM:ss"));
                string subject = string.Format("[LHD] EQA Lot {0} is pending your disposition. ", lot.LotNO);
                CreateEmail(newNotification.EmailID, newNotification.RecipientID, user.SupMail, subject, str, lot.LotNO);
            }
        }

        public static void CreateOtherBinDisposeNotificationsToPEs(string lotID)
        {
            int recordCount = 0;
            foreach (User user in UserService.GetUsersByRole(UserRoles.PE, 0, 0x270f, out recordCount))
            {
                CreateOtherBinDisposeNotificationToSomebody(lotID, user.UserID);
            }
        }

        public static void CreateOtherBinDisposeNotificationToSomebody(string lotID, string userID)
        {
            Notification newNotification = new Notification {
                CreateTime = DateTime.Now,
                EmailID = Guid.NewGuid().ToString(),
                LotID = lotID,
                Message = "Other Bin dispose",
                MessageID = Guid.NewGuid().ToString(),
                NotificationType = NotificationTypes.OtherBinDispose,
                Opened = false,
                ReadTime = Convert.ToDateTime("1999-12-31"),
                RecipientID = userID,
                RecordState = 0,
                UpdateTime = DateTime.Now
            };
            notificationGateway.AddNew(newNotification);
        }

        public static void CreateSPRDDisposeNotificationByUserRoleWhileNewLotArrived(Lot lot, UserRoles role, string message)
        {
            int recordCount = 0;
            foreach (User user in UserService.GetUsersByRole(role, 0, 0x270f, out recordCount))
            {
                CreateDisposeNotification(lot, message, user);
            }
        }

        public static IList<NotificationCounter> GetAllNotificationCounters()
        {
            return notificationCounterGateway.GetAllNotificationCounters();
        }

        public static IList<Notification> GetNotificationsBy(string recipientID, string lotID, bool unreadOnly, NotificationTypes messageType, string orderBy, bool desc, int pageIndex, int pageSize, out int recordCount)
        {
            return notificationGateway.GetNotificationsBy(recipientID, lotID, unreadOnly, messageType, orderBy, desc, pageIndex, pageSize, out recordCount);
        }

        public static IList<UnreadMessageCounter> GetUnreadMessageCounterBy(string userID, IList<string> lotIds, NotificationTypes notificationType)
        {
            return unreadMessageGateway.GetUnreadMessageCountersBy(userID, lotIds, notificationType);
        }

        public static void SetAllReaded(string userID)
        {
            int recordCount = 0;
            foreach (Notification notification in notificationGateway.GetNotificationsBy(userID, "", true, NotificationTypes.Comment, "", false, 0, 0x270f, out recordCount))
            {
                notification.Opened = true;
                notification.ReadTime = DateTime.Now;
                notificationGateway.UpdateByPK(notification);
            }
        }
    }
}

