using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Bea.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "AdImage",
            //    url: "AdImage/Get/{imageId}",
            //    defaults: new { controller = "AdImage", action = "Index" }
            //);

            routes.MapRoute(
                name: "AdActivationRoute",
                url: "Ad/Activate/{id}/{activationToken}",
                defaults: new { controller = "Ad", action = "Activate" }
            );

            routes.MapRoute(
                name: "DefaultWithParameter",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}