namespace Spreadtrum.LHD.Mvc.Areas.Accounts
{
    using System;
    using System.Web.Mvc;

    public class AccountsAreaRegistration : AreaRegistration
    {
        public override void RegisterArea(AreaRegistrationContext context)
        {
            string[] namespaces = new string[] { "Spreadtrum.LHD.Mvc.Areas.Accounts.Controllers" };
            context.MapRoute("Accounts_default", "Accounts/{controller}/{action}/{id}", new { controller = "Login", action = "Index", id = UrlParameter.Optional }, namespaces);
        }

        public override string AreaName
        {
            get
            {
                return "Accounts";
            }
        }
    }
}

