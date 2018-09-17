namespace Spreadtrum.LHD.Mvc.Areas.Lots
{
    using System;
    using System.Web.Mvc;

    public class LotsAreaRegistration : AreaRegistration
    {
        public override void RegisterArea(AreaRegistrationContext context)
        {
            string[] namespaces = new string[] { "Spreadtrum.LHD.Mvc.Areas.Lots.Controllers" };
            context.MapRoute("Lots_default", "Lots/{controller}/{action}/{id}", new { controller = "Query", action = "Index", id = UrlParameter.Optional }, namespaces);
        }

        public override string AreaName
        {
            get
            {
                return "Lots";
            }
        }
    }
}

