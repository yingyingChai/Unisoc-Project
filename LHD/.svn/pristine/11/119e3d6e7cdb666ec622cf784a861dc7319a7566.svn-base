namespace Spreadtrum.LHD.Mvc.Areas.Systems.Controllers
{
    using Spreadtrum.LHD.Business;
    using Spreadtrum.LHD.Entity.Users;
    using Spreadtrum.LHD.Mvc.Areas.Shared;
    using System.Web.Mvc;

    public class TipController : Controller
    {
        public ActionResult getUserInfoById()
        {
            User userByID = UserService.GetUserByID(BaseController.CurrentUserInfo.UserID);
            BaseController.CurrentUserInfo = userByID;
            var data = new {
                Notification = (userByID.NewComments + userByID.WaitForConfirm) + userByID.WaitForDispose,
                WaitForConfirm = userByID.WaitForConfirm,
                WaitForDispose = userByID.WaitForDispose,
                NewComments = userByID.NewComments
            };
            return base.Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}

