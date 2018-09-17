namespace Spreadtrum.LHD.Mvc
{
    using System;
    using System.Web.Mvc;
    using System.Web.Routing;

    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapMvcAttributeRoutes();
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            string[] namespaces = new string[] { "Spreadtrum.LHD.Mvc.Controllers" };
            routes.MapRoute("Default", "{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = UrlParameter.Optional }, namespaces);
        }
    }
}

