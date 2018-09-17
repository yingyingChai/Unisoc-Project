namespace Spreadtrum.LHD.Business
{
    using KaYi.FileSystem.Model;
    using KaYi.FileSystem.Services;
    using KaYi.Utilities;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Spreadtrum.LHD.DAL.Lots;
    using Spreadtrum.LHD.Entity.Lots;
    using Spreadtrum.LHD.Entity.Systems;
    using Spreadtrum.LHD.Entity.Users;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.InteropServices;

    public static class LotService
    {
        private static CommentGateway commentGateway = new CommentGateway();
        private static LotGateway lotGateway = new LotGateway();
        private static LotViewGateway lotViewGateway = new LotViewGateway();
        private static NotificationswithLotGateway notificationWithLotGateway = new NotificationswithLotGateway();
        private static LotStatusListGateway statusListGateway = new LotStatusListGateway();
        private static SWBinGateway swBinGateway = new SWBinGateway();

        public static void AddDisposeComment(Comment comment)
        {
            Lot lotByLotID = lotGateway.GetLotByLotID(comment.LotID);
            commentGateway.AddNew(comment);
            if (!comment.Internal)
            {
                //NotificationService.CreateNewCommentNotification(lotByLotID, UserRoles.OSAT, comment);
                NotificationService.CreateNewCommentNotificationForOSATUsers(lotByLotID, comment);
            }
            NotificationService.CreateNewCommentNotification(lotByLotID, UserRoles.PC, comment);
            NotificationService.CreateNewCommentNotification(lotByLotID, UserRoles.PE, comment);
            NotificationService.CreateNewCommentNotification(lotByLotID, UserRoles.QA, comment);
            
            
        }

        public static void AddNormalComment(Comment comment)
        {
            commentGateway.AddNew(comment);
            Lot lotByLotID = lotGateway.GetLotByLotID(comment.LotID);
            if (!comment.Internal)
            {
                NotificationService.CreateNewCommentNotificationForOSATUsers(lotByLotID, comment);
            }
            NotificationService.CreateNewCommentNotification(lotByLotID, UserRoles.PC, comment);
            NotificationService.CreateNewCommentNotification(lotByLotID, UserRoles.PE, comment);
            NotificationService.CreateNewCommentNotification(lotByLotID, UserRoles.QA, comment);
        }

        public static void ClearSystem()
        {
            lotGateway.ClearSystem();
        }

        public static Lot doOtherBinDispose(string lotID, int newPEDispose, string otherBinDisposeCommentID, string otherBinDisposeComment, User currentUser)
        {
            Lot lotByLotID = lotGateway.GetLotByLotID(lotID);
            Comment comment = GenerateComment(lotID, 5, otherBinDisposeCommentID, otherBinDisposeComment, false, currentUser);
            comment.OtherBinDispose = true;
            AddDisposeComment(comment);
            if (lotByLotID.PEDispose == 0xff)
            {
                AddDisposeComment(GenerateComment(lotID, newPEDispose, Guid.NewGuid().ToString(), GetDisposeTextByDispose(newPEDispose), false, currentUser));
                lotByLotID.PEDispose = 1;
            }
            lotByLotID.OtherBinDispose = true;
            UpdateLot(lotByLotID);
            return lotByLotID;
        }

        public static Comment GenerateComment(string lotID, int dispose, string commentID, string commentText, bool internalOnly, User currentUser)
        {
            Comment comment = new Comment {
                CommentID = commentID,
                CommentText = commentText,
                CommentType = CommentTypes.CommentOnly,
                CreateTime = DateTime.Now,
                Dispose = dispose,
                Internal = internalOnly,
                LastOperator = currentUser.UserID,
                LotID = lotID,
                Operator = currentUser.UserID,
                OperatorName = currentUser.FullName,
                OperatorRole = currentUser.Role.ToString(),
                OperatorBUName = currentUser.BUName,
                OperatorEmail = currentUser.Email,
                RecordState = 0,
                UpdateTime = DateTime.Now
            };
            if ((currentUser.Role != UserRoles.OSAT) && (currentUser.Role != UserRoles.OSATAdmin))
            {
                comment.DisposeText = GetDisposeTextByDispose(dispose);
                return comment;
            }
            comment.DisposeText = "Confirm";
            return comment;
        }

        public static IList<Comment> GetCommentsByLotID(string lotID, string commentType, bool osatUser, int pageIndex, int pageSize, out int recordCount)
        {
            return ReadExtendPropertiesOfComments(commentGateway.GetCommentsBy(lotID, commentType, osatUser, "CreateTime", true, pageIndex, pageSize, out recordCount));
        }

        public static string GetDisposeTextByDispose(int dispose)
        {
            string str = string.Empty;
            switch (dispose)
            {
                case 0:
                    return "Release";

                case 1:
                    return "Bin1 Release";

                case 2:
                    return "Rescreen";

                case 3:
                    return "Scrap";

                case 4:
                    return "Pending";

                case 5:
                    return "Other Bin Dispose";

                case 0xfd:
                    return "Manual Hold";
            }
            return str;
        }

        private static string GetFailedBinLimitByIndex(JToken lotJudegementNode, int index)
        {
            string str = "0";
            for (int i = 0; i <= (lotJudegementNode["metadata"].Count<JToken>() - 1); i++)
            {
                JToken token = lotJudegementNode["metadata"][i];
                string str2 = (token["key"] != null) ? token["key"].ToString() : null;
                string str3 = (token["values"] != null) ? token["values"].ToString() : null;
                str3 = str3.Replace("\r", "").Replace("\n", "").Replace("[", "").Replace("]", "").Replace("\"", "").Trim();
                if (str2 == "failBinLimit")
                {
                    char[] separator = new char[] { ',' };
                    str = str3.Split(separator)[index];
                }
            }
            return str;
        }

        private static string GetFailedRate(JToken lotJudegementNode, int indexOfElement)
        {
            string str = "0";
            for (int i = 0; i <= (lotJudegementNode["metadata"].Count<JToken>() - 1); i++)
            {
                JToken token = lotJudegementNode["metadata"][i];
                string str2 = (token["key"] != null) ? token["key"].ToString() : null;
                string str3 = (token["values"] != null) ? token["values"].ToString() : null;
                str3 = str3.Replace("\r", "").Replace("\n", "").Replace("[", "").Replace("]", "").Replace("\"", "").Trim();
                if (str2 == "failBinPercent")
                {
                    char[] separator = new char[] { ',' };
                    str = str3.Split(separator)[indexOfElement];
                }
            }
            return str;
        }

        public static Lot GetLotByLotID(string lotID)
        {
            return lotGateway.GetLotByLotID(lotID);
        }

        public static JObject GetLotJudgementObject(Lot lot)
        {
            JObject obj2 = (JObject) JsonConvert.DeserializeObject(lot.LotJudgementMessage);
            JToken token = obj2["LOT_JUDGEMENT"][0];
            token["decision"] = lot.SPRDDecisionText.ToUpper();
            token["decisionReason"] = "";
            if (token["metadata"] != null)
            {
                for (int i = 0; i <= (token["metadata"].Count<JToken>() - 1); i++)
                {
                    DateTime time;
                    JToken token2 = token["metadata"][i];
                    string str = ((token2["key"] != null) ? token2["key"].ToString() : null).ToUpper();
                    if (!(str == "DECISIONMAKER"))
                    {
                        if (str == "DECIDERROLE")
                        {
                            goto Label_011F;
                        }
                        if (str == "DECISIONTIME")
                        {
                            goto Label_0140;
                        }
                        if ((str == "DICISIONCOMMENT") || (str == "DECISIONATTACHMENT"))
                        {
                        }
                    }
                    else
                    {
                        token2["values"] = string.Format("[{0}]", "\"PE\"");
                    }
                    continue;
                Label_011F:
                    token2["values"] = string.Format("[{0}]", "\"PE\"");
                    continue;
                Label_0140:
                    time = (lot.QADisposeTime > lot.PEDisposeTime) ? lot.QADisposeTime : lot.PEDisposeTime;
                    string str2 = "\"" + time.ToString("yyyyMMdd-hhmmss.fff") + "\"";
                    token2["values"] = string.Format("[{0}]", str2);
                }
            }
            return obj2;
        }

        public static IList<Lot> GetLotsBySDStates(string osatID, int sdStates)
        {
            return lotGateway.GetLotsBySDStates(osatID, sdStates);
        }

        public static IList<LotStatusList> GetLotStatusListsBy(int QADispose, int PEDispose, bool otherBinDispose, bool vendorConfirmed, bool otherBinDisposeConfirmed)
        {
            return statusListGateway.GetLotStatusListsBy(QADispose, PEDispose, otherBinDispose, vendorConfirmed, otherBinDisposeConfirmed);
        }

        public static LotView GetLotViewByLotID(string lotID)
        {
            return lotViewGateway.GetLotViewByLotID(lotID);
        }

        public static IList<LotView> GetLotViews(LotQuery query, string orderBy, bool desc, int pageIndex, int pageSize, out int recordCount)
        {
            NotificationTypes notSpecified = NotificationTypes.NotSpecified;
            if (query.NewCommentOnly)
            {
                notSpecified = NotificationTypes.Comment;
            }
            if (query.WaitForConfirmOnly)
            {
                notSpecified = NotificationTypes.Confirm;
            }
            if (query.WaitForDisposeOnly)
            {
                notSpecified = NotificationTypes.LotDispose;
            }
            if (query.WaitForOtherBinDispose)
            {
                notSpecified = NotificationTypes.OtherBinDispose;
            }
            if (query.EqaLotsOnly)
            {
                notSpecified = NotificationTypes.EQANotification;
            }
            IList<LotView> lotviews = null;
            switch (notSpecified)
            {
                case NotificationTypes.Comment:
                case NotificationTypes.LotDispose:
                case NotificationTypes.Confirm:
                case NotificationTypes.OtherBinDispose:
                case NotificationTypes.EQANotification:
                    lotviews = GetLotViewsFromNotifications(query, notSpecified, true, orderBy, desc, pageIndex, pageSize, out recordCount);
                    return ReadUnReadMessageCountByCurrentUser(query.CurrentUser, lotviews);

                case NotificationTypes.NotSpecified:
                    lotviews = lotViewGateway.GetLotViewBy(query, orderBy, desc, pageIndex, pageSize, out recordCount);
                    return ReadUnReadMessageCountByCurrentUser(query.CurrentUser, lotviews);
            }
            lotviews = null;
            recordCount = 0;
            return lotviews;
        }

        private static IList<LotView> GetLotViewsFromNotifications(LotQuery query, NotificationTypes notificationType, bool unReadOnly, string orderBy, bool desc, int pageIndex, int pageSize, out int recordCount)
        {
            IList<LotView> list = new List<LotView>();
            foreach (NotificationswithLot lot in notificationWithLotGateway.GetNotificationswithLotsBy(query, notificationType, unReadOnly, orderBy, desc, pageIndex, pageSize, out recordCount))
            {
                list.Add(GetLotViewByLotID(lot.LotID));
            }
            return list;
        }

        public static IList<SWBin> GetSWBinsBy(string lotID, string code, string defect, string qty, string failRate, string isPassed, string limited, string orderBy, bool desc, int pageIndex, int pageSize, out int recordCount)
        {
            return swBinGateway.GetSWBinsBy(lotID, code, defect, qty, failRate, isPassed, limited, orderBy, desc, pageIndex, pageSize, out recordCount);
        }

        public static void ManualHold(string lotID, string userID, string holdReason, string commentID)
        {
            NotificationService.ClearNotificationsByRoleAndLotID(UserRoles.OSAT, lotID, NotificationTypes.Confirm);
            NotificationService.ClearNotificationsByRoleAndLotID(UserRoles.QA, lotID, NotificationTypes.LotDispose);
            NotificationService.ClearNotificationsByRoleAndLotID(UserRoles.PE, lotID, NotificationTypes.LotDispose);
            Lot lotByLotID = lotGateway.GetLotByLotID(lotID);
            lotByLotID.ManualHold = true;
            lotByLotID.LastOperator = userID;
            lotByLotID.OperateType = "ManualHold";
            lotByLotID.HoldReason = holdReason;
            lotByLotID.Status = "WAIT QA & PE";
            lotByLotID.LastDecision = 0xfd;
            lotByLotID.UpdateTime = DateTime.Now;
            UpdateLot(lotByLotID);
            NotificationService.CreateSPRDDisposeNotificationByUserRoleWhileNewLotArrived(lotByLotID, UserRoles.QA, "Manual Hold a Lot, pending your disposition.");
            NotificationService.CreateSPRDDisposeNotificationByUserRoleWhileNewLotArrived(lotByLotID, UserRoles.PE, "Manual Hold a Lot, pending your disposition.");
            User userByID = UserService.GetUserByID(userID);
            AddDisposeComment(GenerateComment(lotID, 0xfd, commentID, holdReason, false, userByID));
        }

        public static Lot processNewLot(Lot newLot)
        {
            newLot.ID = Guid.NewGuid().ToString();
            string str = newLot.AutoJudgeResult.ToUpper();
            if (!(str == "NORMAL") && !(str == "RELEASE"))
            {
                if (str == "HOLD")
                {
                    newLot.Status = "WAIT QA & PE";
                }
                else
                {
                    newLot.Status = "UNKNOWN";
                }
            }
            else
            {
                newLot.Status = "END";
            }
            if (newLot.SWbins != null)
            {
                for (int i = 0; i <= (newLot.SWbins.Count - 1); i++)
                {
                    newLot.SWbins[i].LotID = newLot.LotID;
                    newLot.SWbins[i].ID = Guid.NewGuid().ToString();
                    newLot.SWbins[i].CreateTime = DateTime.Now;
                    newLot.SWbins[i].LastOperator = "Silicondash Client";
                    newLot.SWbins[i].RecordState = 0;
                }
            }
            return newLot;
        }

        private static Lot ReadBasicInfo(string osatID, string jsonStr, JToken lotJudegementNode)
        {
            Lot lot1 = new Lot {
                LotID = Guid.NewGuid().ToString(),
                SDStates = 0,
                LotJudgementMessage = jsonStr,
                VenderID = osatID,
                Stage = (lotJudegementNode["stage"] != null) ? lotJudegementNode["stage"].ToString() : null
            };
            DateTime time = DateTime.ParseExact(lotJudegementNode["lotFinishTime"].ToString(), "yyyyMMdd-HHmmss.fff", CultureInfo.CurrentCulture);
            lot1.CompletionDate = time;
            lot1.AutoJudgeResult = (lotJudegementNode["decision"] != null) ? lotJudegementNode["decision"].ToString() : null;
            lot1.LotNO = (lotJudegementNode["lotId"] != null) ? lotJudegementNode["lotId"].ToString() : null;
            lot1.TesterID = (lotJudegementNode["tester"] != null) ? lotJudegementNode["tester"].ToString() : null;
            lot1.TestProgram = (lotJudegementNode["programName"] != null) ? lotJudegementNode["programName"].ToString() : null;
            lot1.Platforms = (lotJudegementNode["testerType"] != null) ? lotJudegementNode["testerType"].ToString() : null;
            lot1.LBNO = (lotJudegementNode["loadboard"] != null) ? lotJudegementNode["loadboard"].ToString() : null;
            lot1.HoldReason = (lotJudegementNode["decisionReason"] != null) ? lotJudegementNode["decisionReason"].ToString() : null;
            lot1.Url = (lotJudegementNode["url"] != null) ? lotJudegementNode["url"].ToString() : null;
            lot1.CreateTime = DateTime.Now;
            lot1.ID = "";
            lot1.LastOperator = "Silicondash Agent";
            lot1.ManualHold = false;
            lot1.OperateTime = DateTime.Now;
            lot1.OperateType = "Silicondash Agent";
            lot1.OperatorID = "Silicondash Agent";
            lot1.OperatorName = "Silicondash Agent";
            lot1.OtherBinDispose = false;
            lot1.PEDispose = 0xff;
            lot1.PEDisposeText = "";
            lot1.PreviousRecordID = "";
            lot1.QADispose = 0xff;
            lot1.QADisposeText = "";
            lot1.RecordState = 0;
            lot1.RecordType = "Record";
            lot1.SPRDDecision = 0xff;
            lot1.SPRDDecisionText = "";
            lot1.Status = "";
            lot1.UpdateTime = DateTime.Now;
            lot1.VenderJudgeResult = "";
            lot1.Version = 0;
            lot1.VersionID = 0;
            lot1.VenderConfirmed = false;
            lot1.OtherBinDisposeConfirmed = false;
            lot1.OtherBinDisposeConfirmTime = Convert.ToDateTime("1999-12-31");
            lot1.QADisposeTime = Convert.ToDateTime("1999-12-31");
            lot1.PEDisposeTime = Convert.ToDateTime("1999-12-31");
            lot1.VenderConfirmTime = Convert.ToDateTime("1999-12-31");
            return lot1;
        }

        public static Comment ReadExtendPropertiesOfComment(Comment comment)
        {
            int recordCount = 0;
            comment.Attachments = FileSystemService.GetFilesBy("", "", comment.CommentID, "", "", FileStates.Normal, FileTypes.File, "", false, 0, 0x270f, out recordCount);
            return comment;
        }

        public static IList<Comment> ReadExtendPropertiesOfComments(IList<Comment> comments)
        {
            for (int i = 0; i <= (comments.Count - 1); i++)
            {
                comments[i] = ReadExtendPropertiesOfComment(comments[i]);
            }
            return comments;
        }

        private static Lot ReadHWBins(Lot lot, JToken lotJudegementNode, int totalCount)
        {
            if (lotJudegementNode["hbinPareto"] != null)
            {
                JToken token = lotJudegementNode["hbinPareto"];
                double num = (token["2"] != null) ? Convert.ToDouble(token["2"].ToString()) : 0.0;
                lot.OSRate = (num / ((double) totalCount)) * 100.0;
                if (lotJudegementNode["hbinDefinitions"] == null)
                {
                    return lot;
                }
                for (int i = 0; i <= (lotJudegementNode["hbinDefinitions"].Count<JToken>() - 1); i++)
                {
                    switch (Convert.ToInt32(lotJudegementNode["hbinDefinitions"][i]["bin"].ToString()))
                    {
                        case 1:
                            lot.Bin1 = (token["1"] != null) ? Convert.ToInt32(token["1"].ToString()) : 0;
                            break;

                        case 2:
                            lot.Bin2 = (token["2"] != null) ? Convert.ToInt32(token["2"].ToString()) : 0;
                            break;

                        case 3:
                            lot.Bin3 = (token["3"] != null) ? Convert.ToInt32(token["3"].ToString()) : 0;
                            break;

                        case 4:
                            lot.Bin4 = (token["4"] != null) ? Convert.ToInt32(token["4"].ToString()) : 0;
                            break;

                        case 5:
                            lot.Bin5 = (token["5"] != null) ? Convert.ToInt32(token["5"].ToString()) : 0;
                            break;

                        case 6:
                            lot.Bin6 = (token["6"] != null) ? Convert.ToInt32(token["6"].ToString()) : 0;
                            break;

                        case 7:
                            lot.Bin7 = (token["7"] != null) ? Convert.ToInt32(token["7"].ToString()) : 0;
                            break;

                        case 8:
                            lot.Bin8 = (token["8"] != null) ? Convert.ToInt32(token["8"].ToString()) : 0;
                            break;

                        case 9:
                            lot.Bin9 = (token["9"] != null) ? Convert.ToInt32(token["9"].ToString()) : 0;
                            break;

                        case 10:
                            lot.Bin10 = (token["10"] != null) ? Convert.ToInt32(token["10"].ToString()) : 0;
                            break;
                    }
                }
            }
            return lot;
        }

        public static Lot ReadLotFromJson(string osatID, string jsonStr, IList<string> messages)
        {
            JObject obj2 = (JObject) JsonConvert.DeserializeObject(jsonStr);
            if (obj2["LOT_JUDGEMENT"] != null)
            {
                try
                {
                    int totalCount = 0;
                    JToken lotJudegementNode = obj2["LOT_JUDGEMENT"][0];
                    messages.Add("Starting analysis the LOT_JUDGEMENT message.\r\n");
                    messages.Add("Basic information anylysis finished.\r\n");
                    messages.Add("Status anylysis finished.\r\n");
                    messages.Add("HWBins anylysis finished.\r\n");
                    messages.Add("SWBins anylysis finished.\r\n");
                    messages.Add("MetaData anylysis finished.\r\n");
                    return ReadMetaData(ReadSWBins(ReadHWBins(ReadStats(ReadBasicInfo(osatID, jsonStr, lotJudegementNode), lotJudegementNode, out totalCount), lotJudegementNode, totalCount), lotJudegementNode, totalCount), lotJudegementNode);
                }
                catch (Exception exception)
                {
                    messages.Add(exception.Message);
                    return null;
                }
            }
            messages.Add("No LOT_JUDEGEMENT node found.");
            return null;
        }

        public static Lot ReadMetaData(Lot result, JToken lotJudegementNode)
        {
            if (lotJudegementNode["metadata"] != null)
            {
                for (int j = 0; j <= (lotJudegementNode["metadata"].Count<JToken>() - 1); j++)
                {
                    char[] chArray2;
                    char[] chArray1;
                    JToken token = lotJudegementNode["metadata"][j];
                    string s = (token["key"] != null) ? token["key"].ToString() : null;
                    string str2 = (token["values"] != null) ? token["values"].ToString() : null;
                    str2 = str2.Replace("\r", "").Replace("\n", "").Replace("[", "").Replace("]", "").Replace("\"", "").Trim();
                    switch(s)
                    {
                        case "lotMoveInQuantity":
                            result.QtyIn = StringHelper.isNullOrEmpty(str2) ? 0 : Convert.ToInt32(str2);
                            continue;
                        case "lotMoveOutQuantity":
                            result.QtyOut = StringHelper.isNullOrEmpty(str2) ? 0 : Convert.ToInt32(str2);
                            continue;
                        case "damage":
                            result.Damage = StringHelper.isNullOrEmpty(str2) ? 0 : Convert.ToInt32(str2);
                            continue;
                        case "loss":
                            result.Loss = StringHelper.isNullOrEmpty(str2) ? 0 : Convert.ToInt32(str2);
                            continue;
                        case "decisionTime":
                            result.decisionTime = DateTime.ParseExact(str2, "yyyyMMdd-HHmmss.fff", CultureInfo.CurrentCulture);
                            continue;

                        case "decisionRecipeTrigger":
                            if (!StringHelper.isNullOrEmpty(str2))
                            {
                                result.TriggerMode = Convert.ToBoolean(str2) == true ? "Auto" : "Manual";
                            }
                            continue;
                    }
                    switch (ComputeStringHash(s))
                    {
                        case 0x743dff19:
                        {
                            if (s == "osatLotType")
                            {
                                goto Label_0307;
                            }
                            continue;
                        }
                        case 0x8995e1f6:
                        {
                            if (s == "customerDeviceCode")
                            {
                                goto Label_02ED;
                            }
                            continue;
                        }
                        case 0xa515f130:
                        {
                            if (s == "lotMoveOutLoss")
                            {
                                goto Label_034C;
                            }
                            continue;
                        }
                        case 0x1edbf2d1:
                        {
                            if (s == "customerLotId")
                            {
                                goto Label_0321;
                            }
                            continue;
                        }
                        case 0x4f07fc82:
                        {
                            if (!(s == "osat"))
                            {
                            }
                            continue;
                        }
                        case 0x639131de:
                        {
                            if (s == "lotMoveOutDamage")
                            {
                                goto Label_036A;
                            }
                            continue;
                        }
                        case 0xa5303adc:
                        {
                            if (s == "timesTested")
                            {
                                break;
                            }
                            continue;
                        }
                        case 0xb160bcac:
                        {
                            if (s == "SIPLotsTestedAtCP")
                            {
                                goto Label_0388;
                            }
                            continue;
                        }
                        case 0xc0db213f:
                        {
                            if (s == "SIPLots")
                            {
                                goto Label_03FE;
                            }
                            continue;
                        }
                        case 0xf0a0ec54:
                        {
                            if (s == "customerDeviceName")
                            {
                                goto Label_02E0;
                            }
                            continue;
                        }
                        case 0xf3b08d84:
                        {
                            if (s == "osatLotId")
                            {
                                goto Label_02FA;
                            }
                            continue;
                        }
                        //case 0xc1000c0e:
                        //{
                        //    if (s == "lotMoveOutQuantity")
                        //    {
                        //        goto Label_032E;
                        //    }
                        //    continue;
                        //}
                        case 0xcf517828:
                        {
                            if (s == "osatDecision")
                            {
                                goto Label_0314;
                            }
                            continue;
                        }
                        default:
                        {
                            continue;
                        }
                    }
                    result.RetestTimes = StringHelper.isNullOrEmpty(str2) ? 0 : Convert.ToInt32(str2);
                    continue;
                Label_02E0:
                    result.DeviceName = str2;
                    continue;
                Label_02ED:
                    result.DeviceCode = str2;
                    continue;
                Label_02FA:
                    result.SubconLot = str2;
                    continue;
                Label_0307:
                    result.LotType = str2;
                    continue;
                Label_0314:
                    result.VenderJudgeResult = str2;
                    continue;
                Label_0321:
                    result.CustomerLotID = str2;
                    continue;
                Label_032E:
                    result.QtyIn = StringHelper.isNullOrEmpty(str2) ? 0 : Convert.ToInt32(str2);
                    continue;
                Label_034C:
                    result.Loss = StringHelper.isNullOrEmpty(str2) ? 0 : Convert.ToInt32(str2);
                    continue;
                Label_036A:
                    result.Damage = StringHelper.isNullOrEmpty(str2) ? 0 : Convert.ToInt32(str2);
                    continue;
                Label_0388:
                    chArray1 = new char[] { ',' };
                    string[] strArray = str2.Split(chArray1);
                    for (int k = 0; k <= (strArray.Length - 1); k++)
                    {
                        switch (k)
                        {
                            case 0:
                                result.Die1CP = strArray[k];
                                break;

                            case 1:
                                result.Die2CP = strArray[k];
                                break;

                            case 2:
                                result.Die3CP = strArray[k];
                                break;

                            case 3:
                                result.Die4CP = strArray[k];
                                break;
                        }
                    }
                    continue;
                Label_03FE:
                    chArray2 = new char[] { ',' };
                    string[] strArray2 = str2.Split(chArray2);
                    for (int m = 0; m <= (strArray2.Length - 1); m++)
                    {
                        switch (m)
                        {
                            case 0:
                                result.Die1LotNO = strArray2[m];
                                break;

                            case 1:
                                result.Die2LotNO = strArray2[m];
                                break;

                            case 2:
                                result.Die3LotNO = strArray2[m];
                                break;

                            case 3:
                                result.Die4LotNO = strArray2[m];
                                break;
                        }
                    }
                }
            }
            int index = 0;
            for (int i = 0; i <= (lotJudegementNode["metadata"].Count<JToken>() - 1); i++)
            {
                JToken token2 = lotJudegementNode["metadata"][i];
                string str3 = (token2["key"] != null) ? token2["key"].ToString() : null;
                string str4 = (token2["values"] != null) ? token2["values"].ToString() : null;
                str4 = str4.Replace("\r", "").Replace("\n", "").Replace("[", "").Replace("]", "").Replace("\"", "").Trim();
                if (str3 == "failBin")
                {
                    char[] separator = new char[] { ',' };
                    string[] strArray3 = str4.Split(separator);
                    for (int n = 0; n <= (strArray3.Length - 1); n++)
                    {
                        string str5 = strArray3[n];
                        for (int num8 = 0; num8 <= (result.SWbins.Count - 1); num8++)
                        {
                            //if (result.SWbins[num8].Code == Convert.ToInt32(str5))
                            if (result.SWbins[num8].Code.ToString() == str5.Trim())
                            {
                                result.SWbins[num8].IsPassed = "P";
                                result.SWbins[num8].Limited = Convert.ToSingle(GetFailedBinLimitByIndex(lotJudegementNode, index));
                                result.SWbins[num8].FailedBinPercent = Convert.ToDouble(GetFailedRate(lotJudegementNode, index));
                                result.SWbins[num8].IsTriggerd = (result.HoldReason.IndexOf(str5.ToString()) >= 0) ? "1" : "2";
                                index++;
                            }
                        }
                    }
                    for (int num9 = 0; num9 <= (result.SWbins.Count - 1); num9++)
                    {
                        if (result.SWbins[num9].IsPassed != "P")
                        {
                            result.SWbins[num9].IsPassed = "F";
                        }
                    }
                }
            }
            return result;
        }

        private static Lot ReadStats(Lot lot, JToken lotJudegementNode, out int totalCount)
        {
            if (lotJudegementNode["stats"] != null)
            {
                JToken token = lotJudegementNode["stats"];
                lot.Yield = (token["passPercent"] != null) ? Convert.ToDouble(token["passPercent"].ToString()) : 0.0;
                totalCount = (token["totalCount"] != null) ? Convert.ToInt32(token["totalCount"].ToString()) : 0;
                lot.totalCount = (token["totalCount"] != null) ? Convert.ToInt32(token["totalCount"].ToString()) : 0;
                lot.passCount = (token["passCount"] != null) ? Convert.ToInt32(token["passCount"].ToString()) : 0;
                return lot;
            }
            totalCount = 0;
            return lot;
        }

        private static Lot ReadSWBins(Lot lot, JToken lotJudegementNode, int totalCount)
        {
            JToken jToken = lotJudegementNode["sbinPareto"];
            if (lotJudegementNode["sbinDefinitions"] != null)
            {
                IList<SWBin> list = new List<SWBin>();
                for (int i = 0; i <= lotJudegementNode["sbinDefinitions"].Count<JToken>() - 1; i++)
                {
                    JToken jToken2 = lotJudegementNode["sbinDefinitions"][i];
                    SWBin sWBin = new SWBin();
                    sWBin.Code = Convert.ToInt32(jToken2["bin"].ToString());
                    sWBin.Defect = Convert.ToString(jToken2["binName"].ToString());
                    sWBin.Qty = ((jToken[sWBin.Code.ToString()] != null) ? Convert.ToInt32(jToken[sWBin.Code.ToString()].ToString()) : 0);
                    sWBin.LotID = lot.LotID;
                    if (jToken[sWBin.Code.ToString()] != null && jToken[sWBin.Code.ToString()].ToString() != "")
                    {
                        sWBin.FailRate = Convert.ToSingle(jToken[sWBin.Code.ToString()].ToString()) / (float)totalCount * 100f;
                    }
                    sWBin.CreateTime = DateTime.Now;
                    list.Add(sWBin);
                }
                lot.SWbins = list;
            }
            return lot;
        }

        public static IList<LotView> ReadUnReadMessageCountByCurrentUser(string currentUserID, IList<LotView> lotviews)
        {
            if ((lotviews != null) && (lotviews.Count >= 1))
            {
                int num;
                IList<string> lotIds = new List<string>();
                foreach (LotView view in lotviews)
                {
                    lotIds.Add(view.LotID);
                }
                List<UnreadMessageCounter> list2 = (List<UnreadMessageCounter>) NotificationService.GetUnreadMessageCounterBy(currentUserID, lotIds, NotificationTypes.Comment);
                for (int i = 0; i <= (lotviews.Count - 1); i = num + 1)
                {
                    UnreadMessageCounter counter = list2.Find(unreadMessageCounter => (unreadMessageCounter.LotID == lotviews[i].LotID) && unreadMessageCounter.RecipientID.Equals(currentUserID));
                    if (counter != null)
                    {
                        lotviews[i].UnreadNotificationForCurrentUser = counter.UnreadMessage;
                    }
                    else
                    {
                        lotviews[i].UnreadNotificationForCurrentUser = 0;
                    }
                    num = i;
                }
            }
            return lotviews;
        }

        public static void RecallLot(string lotID, string userID, string commentID, string comment)
        {
            NotificationService.ClearNotificationsByRoleAndLotID(UserRoles.OSAT, lotID, NotificationTypes.Confirm);
            NotificationService.ClearNotificationsByRoleAndLotID(UserRoles.QA, lotID, NotificationTypes.LotDispose);
            NotificationService.ClearNotificationsByRoleAndLotID(UserRoles.PE, lotID, NotificationTypes.LotDispose);
            NotificationService.ClearNotificationsByRoleAndLotID(UserRoles.PE, lotID, NotificationTypes.OtherBinDispose);
            NotificationService.ClearNotificationsByRoleAndLotID(UserRoles.PC, lotID, NotificationTypes.LotDispose);
            Lot firstRecordByLotID = lotGateway.GetFirstRecordByLotID(lotID);
            lotGateway.DeleteLotRecords(lotID);
            firstRecordByLotID.UpdateTime = DateTime.Now;
            firstRecordByLotID.RecordType = "Record";
            firstRecordByLotID.OperateType = "Recall";
            firstRecordByLotID.UpdateTime = DateTime.Now;
            lotGateway.AddLotRecord(firstRecordByLotID);
            if (firstRecordByLotID.AutoJudgeResult.ToUpper() != "NORMAL")
            {
                NotificationService.CreateSPRDDisposeNotificationByUserRoleWhileNewLotArrived(firstRecordByLotID, UserRoles.QA, "Lot Recalled, waiting for your dispose.");
                NotificationService.CreateSPRDDisposeNotificationByUserRoleWhileNewLotArrived(firstRecordByLotID, UserRoles.PE, "Lot Recalled, waiting for your dispose.");
            }
            User userByID = UserService.GetUserByID(userID);
            Comment comment1 = GenerateComment(firstRecordByLotID.LotID, firstRecordByLotID.LastDecision, commentID, comment, false, userByID);
            comment1.DisposeText = "Recall";
            AddDisposeComment(comment1);
        }

        public static void SaveLotAndInformQA_AND_PE(Lot lot)
        {
            Lot lot2 = processNewLot(lot);
            if (lot2.SWbins != null)
            {
                for (int i = 0; i <= (lot2.SWbins.Count - 1); i++)
                {
                    lot2.SWbins[i].LotID = lot2.LotID;
                    swBinGateway.AddNew(lot2.SWbins[i]);
                }
            }
            if (lot2.AutoJudgeResult.ToUpper() != "NORMAL")
            {
                if (lot2.Stage.IndexOf("QA") >= 0 || lot2.Stage.IndexOf("QC") >= 0)
                {
                    lot2.LastDecision = 0xfe;
                }
                else
                {
                    lot2.LastDecision = 0xfe;
                }
            }
            else
            {
                lot2.LastDecision = 0xff;
            }
            lotGateway.SocketSaveLot(lot2);
            //lotGateway.AddLotRecord(lot2);
            Lot LotMail = lotGateway.GetLotByLotID(lot2.LotID);
            if (LotMail.AutoJudgeResult.ToUpper() != "NORMAL")
            {
                if (LotMail.Stage.IndexOf("QA") >= 0 || LotMail.Stage.IndexOf("QC") >= 0)
                {
                    LotMail.LastDecision = 0xfe;
                    NotificationService.CreateOSATEQANotificationsWhileLotArrived(LotMail.VenderID, LotMail, "EQA Lot is pending for your disposition.");
                    NotificationService.CreateSPRDDisposeNotificationByUserRoleWhileNewLotArrived(LotMail, UserRoles.QA, "New lot arrived, is pending your disposition.");
                    NotificationService.CreateSPRDDisposeNotificationByUserRoleWhileNewLotArrived(LotMail, UserRoles.PE, "New lot arrived, is pending your disposition.");
                }
                else
                {
                    LotMail.LastDecision = 0xfe;
                    NotificationService.CreateSPRDDisposeNotificationByUserRoleWhileNewLotArrived(LotMail, UserRoles.QA, "New lot arrived, is pending your disposition.");
                    NotificationService.CreateSPRDDisposeNotificationByUserRoleWhileNewLotArrived(LotMail, UserRoles.PE, "New lot arrived, is pending your disposition.");
                }
            }

        }

        public static void UpdateLot(Lot lot)
        {
            int lot1SPRDDecision = lot.SPRDDecision;
            Lot lot2 = updateLotStatus(lot);
            lotGateway.UpdateLot(lot2);
            if (((lot1SPRDDecision != lot2.SPRDDecision) && (lot2.SPRDDecision != 4)) && (lot2.SPRDDecision != 0xff))
            {
                NotificationService.CreateOSATConfirmNotificationsWhileLotChanged(lot.VenderID, lot, "SPRDDECISION");
            }
        }

        private static Lot updateLotStatus(Lot lot)
        {
            IList<LotStatusList> list = statusListGateway.GetLotStatusListsBy(lot.QADispose, lot.PEDispose, lot.OtherBinDispose, lot.VenderConfirmed, lot.OtherBinDisposeConfirmed);
            if (list.Count != 1)
            {
                return null;
            }
            LotStatusList list2 = list[0];
            lot.Status = list2.Status;
            if (list2.SPRDDecision != -1)
            {
                lot.SPRDDecision = list2.SPRDDecision;
            }
            return lot;
        }


        private static uint ComputeStringHash(string text)
        {
            uint hashCode = 0;
            if (text != null)
            {
                hashCode = unchecked((uint)2166136261);

                int i = 0;
                goto start;

            again:
                hashCode = unchecked((text[i] ^ hashCode) * 16777619);
                i = i + 1;

            start:
                if (i < text.Length)
                    goto again;
            }
            return hashCode;
        }
    }
}

