namespace Spreadtrum.LHD.Mvc.Areas.Lots.Controllers
{
    using KaYi.Utilities;
    using KaYi.Web.Infrastructure.Model.System.HandlerResponses;
    using Spreadtrum.LHD.Business;
    using Spreadtrum.LHD.Entity.Lots;
    using Spreadtrum.LHD.Entity.Systems;
    using Spreadtrum.LHD.Entity.Users;
    using Spreadtrum.LHD.Mvc.Areas.Shared;
    using System;
    using System.Web.Mvc;

    public class DisposeController : BaseController
    {
        [HttpPost]
        public ActionResult doOtherBinDispose()
        {
            string lotID = base.Request.Form["lotIDForOtherBinDispose"];
            string otherBinDisposeCommentID = base.Request.Form["hidOtherBinDisposeCommentID"];
            string str3 = base.Request.Form["txtOtherBinDisposeComment"];
            int newPEDispose = Convert.ToInt32(base.Request.Form["hidPEDispose"]);
            if (StringHelper.isNullOrEmpty(str3))
            {
                str3 = "Other Bin dispose.";
            }
            Lot lot = LotService.doOtherBinDispose(lotID, newPEDispose, otherBinDisposeCommentID, str3, BaseController.CurrentUserInfo);
            NotificationService.ClearNotificationsByRoleAndLotID(UserRoles.PE, lotID, NotificationTypes.LotDispose);
            NotificationService.ClearNotificationsByRoleAndLotID(UserRoles.PE, lotID, NotificationTypes.OtherBinDispose);
            if (lot.VenderConfirmed)
            {
                NotificationService.CreateOSATConfirmNotificationsWhileLotChanged(lot.VenderID, lot, "OTHERBINDISPOSE");
            }
            ResponseTypes tip = ResponseTypes.Tip;
            TipTypes information = TipTypes.Information;
            base.Response.Write(new HandlerResponse("0", "doOtherBinDisposeSuccessed", tip.ToString(), information.ToString(), "", "", lot.Status).GenerateJsonResponse());
            return null;
        }

        public ActionResult Index()
        {
            return base.View();
        }

        public void ManualHold(string lotID, string HoldReason, string commentID)
        {
            LotService.ManualHold(lotID, BaseController.CurrentUserInfo.UserID, HoldReason, commentID);
        }

        public void Recall(string lotID, string commentID, string comment)
        {
            LotService.RecallLot(lotID, BaseController.CurrentUserInfo.UserID, commentID, comment);
        }

        public ActionResult SaveDecision()
        {
            string code = string.Empty;
            string message = string.Empty;
            string str3 = string.Empty;
            int dispose = -1;
            Lot lotByLotID = LotService.GetLotByLotID(base.Request.Form["lotID"]);
            int lastDecision = lotByLotID.LastDecision;
            switch (BaseController.CurrentUserInfo.Role)
            {
                case UserRoles.OSAT:
                case UserRoles.OSATAdmin:
                {
                    string str6 = base.Request.Form["chkConfirmedValue"];
                    char[] separator = new char[] { ',' };
                    string[] strArray = str6.Split(separator);
                    if ((str6.Length <= 0) || (lotByLotID.SPRDDecision == 0xff))
                    {
                        code = "1";
                        message = "Confirm failed because the lot status changed.";
                    }
                    else
                    {
                        NotificationService.ClearNotificationsByOSATIDAndLotID(lotByLotID.VenderID, lotByLotID.LotID);
                        foreach (string str7 in strArray)
                        {
                            if (!(str7 == "0"))
                            {
                                if (str7 == "1")
                                {
                                    lotByLotID.VenderConfirmed = true;
                                    lotByLotID.VenderConfirmTime = DateTime.Now;
                                }
                                else if (str7 == "2")
                                {
                                    lotByLotID.VenderConfirmed = true;
                                    lotByLotID.VenderConfirmTime = DateTime.Now;
                                    str3 = "Confirm Rescreen";
                                }
                                else if (str7 == "3")
                                {
                                    lotByLotID.VenderConfirmed = true;
                                    lotByLotID.VenderConfirmTime = DateTime.Now;
                                    str3 = "Confirm Scrap";
                                }
                                else if (str7 == "5")
                                {
                                    lotByLotID.OtherBinDisposeConfirmed = true;
                                    lotByLotID.OtherBinDisposeConfirmTime = DateTime.Now;
                                }
                            }
                            else
                            {
                                lotByLotID.VenderConfirmed = true;
                                lotByLotID.VenderConfirmTime = DateTime.Now;
                                str3 = "Confirm Release";
                            }
                        }
                        if (str6.Contains("1") && str6.Contains("5"))
                        {
                            str3 = "Confirm Bin1 Release and Other Bin Dispose";
                        }
                        if (str6.Contains("1") && !str6.Contains("5"))
                        {
                            str3 = "Confirm Bin1 Release";
                        }
                        if (!str6.Contains("1") && str6.Contains("5"))
                        {
                            str3 = "Confirm Other Bin Dispose";
                        }
                        LotService.UpdateLot(lotByLotID);
                        lotByLotID = LotService.GetLotByLotID(lotByLotID.LotID);
                        if (lotByLotID.Status == "END")
                        {
                            lotByLotID.SDStates = 0xff;
                            LotService.UpdateLot(lotByLotID);
                        }
                    }
                    //base.Session.Contents["User"] = UserService.GetOSATUserByHashedCID(BaseController.CurrentUserInfo.UserID);
                    goto Label_0431;
                }
                case UserRoles.PE:
                case UserRoles.PEAdmin:
                    lotByLotID.PEDisposeTime = DateTime.Now;
                    dispose = Convert.ToInt32(base.Request.Form["PEDispose"]);
                    lotByLotID.SetPEDispose(dispose);
                    NotificationService.ClearNotificationsByRoleAndLotID(BaseController.CurrentUserInfo.Role, lotByLotID.LotID, NotificationTypes.LotDispose);
                    switch (dispose)
                    {
                        case 1:
                            if (!lotByLotID.OtherBinDispose)
                            {
                                NotificationService.CreateOtherBinDisposeNotificationToSomebody(lotByLotID.LotID, BaseController.CurrentUserInfo.UserID);
                            }
                            goto Label_0114;

                        case 2:
                        case 3:
                            NotificationService.ClearNotificationsByRoleAndLotID(UserRoles.PE, lotByLotID.LotID, NotificationTypes.OtherBinDispose);
                            goto Label_0114;

                        case 4:
                            //不需要给自己发邮件
                            //NotificationService.CreateDisposeNotification(lotByLotID, "", BaseController.CurrentUserInfo);
                            goto Label_0114;
                    }
                    break;

                case UserRoles.QA:
                case UserRoles.QAAdmin:
                    dispose = Convert.ToInt32(base.Request.Form["QADispose"]);
                    lotByLotID.QADisposeTime = DateTime.Now;
                    lotByLotID.SetQADispose(dispose);
                    NotificationService.ClearNotificationsByRoleAndLotID(BaseController.CurrentUserInfo.Role, lotByLotID.LotID, NotificationTypes.LotDispose);
                    switch (dispose)
                    {
                        case 1:
                            if (!lotByLotID.OtherBinDispose)
                            {
                                NotificationService.CreateOtherBinDisposeNotificationsToPEs(lotByLotID.LotID);
                            }
                            break;

                        case 2:
                        case 3:
                            NotificationService.ClearNotificationsByRoleAndLotID(UserRoles.PE, lotByLotID.LotID, NotificationTypes.OtherBinDispose);
                            break;

                        case 4:
                            //不需要给自己发邮件
                            //NotificationService.CreateDisposeNotification(lotByLotID, "", BaseController.CurrentUserInfo);
                            break;
                    }
                    //base.Session.Contents["User"] = UserService.GetUserByID(BaseController.CurrentUserInfo.UserID);
                    code = "0";
                    message = "Save decision successed.";
                    goto Label_0431;

                default:
                    goto Label_0431;
            }
        Label_0114:
            //base.Session.Contents["User"] = UserService.GetUserByID(BaseController.CurrentUserInfo.UserID);
            code = "0";
            message = "Save decision successed.";
        Label_0431:
            LotService.UpdateLot(lotByLotID);
            string commentText = base.Request.Form["txtComment"];
            string commentID = base.Request.Form["hidNewCommentID"];
            bool internalOnly = base.Request.Form["chkInternal"] != null;
            if (internalOnly && (dispose != -1))
            {
                Comment comment = LotService.GenerateComment(lotByLotID.LotID, dispose, commentID, commentText, internalOnly, BaseController.CurrentUserInfo);
                Comment comment2 = LotService.GenerateComment(lotByLotID.LotID, dispose, Guid.NewGuid().ToString(), "", false, BaseController.CurrentUserInfo);
                if (!StringHelper.isNullOrEmpty(str3))
                {
                    comment.CommentText = str3 + "<br/>" + comment.CommentText;
                    comment2.CommentText = str3;
                }
                LotService.AddDisposeComment(comment);
                LotService.AddDisposeComment(comment2);
            }
            else
            {
                Comment comment3 = LotService.GenerateComment(lotByLotID.LotID, dispose, commentID, commentText, internalOnly, BaseController.CurrentUserInfo);
                if (!StringHelper.isNullOrEmpty(str3))
                {
                    comment3.CommentText = str3 + "<br/>" + comment3.CommentText;
                }
                LotService.AddDisposeComment(comment3);
            }
            ResponseTypes tip = ResponseTypes.Tip;
            TipTypes information = TipTypes.Information;
            base.Response.Write(new HandlerResponse(code, message, tip.ToString(), information.ToString(), "", "", lotByLotID.Status).GenerateJsonResponse());
            return null;
        }
    }
}

