namespace Spreadtrum.LHD.Mvc.Areas.Systems
{
    using System;
    using System.Web.Mvc;

    public class SystemsAreaRegistration : AreaRegistration
    {
        public override void RegisterArea(AreaRegistrationContext context)
        {
            string[] namespaces = new string[] { "Spreadtrum.LHD.Mvc.Areas.Systems.Controllers" };
            context.MapRoute("Systems_default", "Systems/{controller}/{action}/{id}", new { action = "Index", id = UrlParameter.Optional }, namespaces);
        }

        public override string AreaName
        {
            get
            {
                return "Systems";
            }
        }
    }
}

