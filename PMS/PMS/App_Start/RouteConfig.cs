using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PMS
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "SA",
                url: "{controller}/{action}/{id}/{operation}/{additionalId}",
                defaults: new { controller = "SA", action = "Index", id = UrlParameter.Optional, operation = UrlParameter.Optional, additionalId = UrlParameter.Optional }
            );
        }
    }
}
