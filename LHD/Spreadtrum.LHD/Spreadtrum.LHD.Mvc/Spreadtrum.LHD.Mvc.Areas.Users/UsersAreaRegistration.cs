namespace Spreadtrum.LHD.Mvc.Areas.Users
{
    using System;
    using System.Web.Mvc;

    public class UsersAreaRegistration : AreaRegistration
    {
        public override void RegisterArea(AreaRegistrationContext context)
        {
            string[] namespaces = new string[] { "Spreadtrum.LHD.Mvc.Areas.Users.Controllers" };
            context.MapRoute("Users_default", "Users/{controller}/{action}/{id}", new { controller = "UserManager", action = "Index", id = UrlParameter.Optional }, namespaces);
        }

        public override string AreaName
        {
            get
            {
                return "Users";
            }
        }
    }
}

