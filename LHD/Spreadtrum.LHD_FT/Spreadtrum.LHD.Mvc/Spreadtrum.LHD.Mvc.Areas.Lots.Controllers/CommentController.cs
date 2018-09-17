namespace Spreadtrum.LHD.Mvc.Areas.Lots.Controllers
{
    using KaYi.Utilities;
    using Spreadtrum.LHD.Business;
    using Spreadtrum.LHD.Entity.Lots;
    using Spreadtrum.LHD.Entity.Users;
    using Spreadtrum.LHD.Mvc.Areas.Shared;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Runtime.InteropServices;
    using System.Web.Mvc;

    public class CommentController : BaseController
    {
        public void AddComment()
        {
            string commentID = base.Request.Form["hidNewCommentID"];
            string s = base.Request.Form["txtComment"];
            s = base.Server.HtmlEncode(s).Replace("\n", "<br/>");
            bool internalOnly = base.Request.Form["chkInternal"] != null;
            LotService.AddNormalComment(LotService.GenerateComment(base.Request.Form["lotID"], -1, commentID, s, internalOnly, BaseController.CurrentUserInfo));
        }

        [HttpPost]
        public ActionResult getCommentList(string lotID, int pageIndex = 0, int pageSize = 5)
        {
            int recordCount = 0;
            IList<Comment> list = null;
            if ((BaseController.CurrentUserInfo.Role == UserRoles.OSAT) || (BaseController.CurrentUserInfo.Role == UserRoles.OSATAdmin))
            {
                list = LotService.GetCommentsByLotID(lotID, "", true, pageIndex, pageSize, out recordCount);
            }
            else
            {
                list = LotService.GetCommentsByLotID(lotID, "", false, pageIndex, pageSize, out recordCount);
            }
            bool flag = ConfigurationManager.AppSettings["LocalDownload"] == "1";
            string str = ConfigurationManager.AppSettings["RemoteDownloadUrlPrefix"];
            for (int i = 0; i <= (list.Count - 1); i++)
            {
                for (int j = 0; j <= (list[i].Attachments.Count - 1); j++)
                {
                    if (!flag)
                    {
                        list[i].Attachments[j].StoreRelativePath = str + list[i].Attachments[j].StoreRelativePath;
                    }
                }
            }
            var data = new {
                currentPage = pageIndex,
                totalPages = PagerUtility.GetPageCount(recordCount, pageSize),
                rows = list
            };
            NotificationService.ClearCommentNotificationByUserIDAndLotID(BaseController.CurrentUserInfo.UserID, lotID);
            return base.Json(data);
        }
    }
}

