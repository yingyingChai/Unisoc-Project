namespace Spreadtrum.LHD.Mvc.Areas.Users.Controllers
{
    using KaYi.Web.Infrastructure.Model.System.HandlerResponses;
    using Newtonsoft.Json;
    using Spreadtrum.LHD.Business;
    using Spreadtrum.LHD.Entity.Users;
    using Spreadtrum.LHD.Mvc.Areas.Shared;
    using System;
    using System.Web.Mvc;

    public class SPRDController : Controller
    {
        [HttpPost]
        public string AddSprdUserToLHD(string hidNewUserSPRDID, string hidUserRole, string chkManager, string txtEnglishName, string txtChineseName, string txtAccount, string chkActive,string JobType)
        {
            UserRoles roles;
            ResponseTypes tip;
            TipTypes information;
            User userByThirdPartyAccountID = UserService.GetUserByThirdPartyAccountID("SPRDUser", @"Spreadtrum\" + txtAccount);
            if (userByThirdPartyAccountID == null)
            {
                string userID = BaseController.CurrentUserInfo.UserID;
                if (!string.IsNullOrWhiteSpace(chkManager))
                {
                    hidUserRole = hidUserRole + "Admin";
                }
                roles = (UserRoles) Enum.Parse(typeof(UserRoles), hidUserRole);
                bool active = !string.IsNullOrWhiteSpace(chkActive);
                SPRDUser sPRDUserByID = SPRDInterface.GetSPRDUserByID(hidNewUserSPRDID);
                UserService.CreateSPRDUser(roles, sPRDUserByID.Account, "SPRD", sPRDUserByID.EnglishName, sPRDUserByID.Email, active, userID, JobType);
                tip = ResponseTypes.Tip;
                information = TipTypes.Information;
                base.Response.Write(new HandlerResponse("0", "createLHDUserSuccessed", tip.ToString(), information.ToString(), "", "", "").GenerateJsonResponse());
                return null;
            }
            roles = (UserRoles) Enum.Parse(typeof(UserRoles), hidUserRole);
            if (!string.IsNullOrWhiteSpace(chkManager))
            {
                hidUserRole = hidUserRole + "Admin";
            }
            roles = (UserRoles) Enum.Parse(typeof(UserRoles), hidUserRole);
            UserService.ReActiveUser(userByThirdPartyAccountID.UserID, roles);
            tip = ResponseTypes.Tip;
            information = TipTypes.Information;
            base.Response.Write(new HandlerResponse("1", "reactiveLHDUserSuccessed", tip.ToString(), information.ToString(), "", "", "").GenerateJsonResponse());
            return null;
        }

        public Action SearchUserByKeyword()
        {
            string s = JsonConvert.SerializeObject(SPRDInterface.GetSPRDUserByKeyword(base.Request.QueryString["keyword"]));
            base.Response.Write(s);
            return null;
        }
    }
}

