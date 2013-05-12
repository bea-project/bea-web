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

            routes.MapRoute(
                name: "FrenchRouteToActivation",
                url: "Annonce/Activation/{id}/{activationToken}",
                defaults: new { controller = "Post", action = "Activate" }
            );

            routes.MapRoute(
                name: "FrenchRouteToPost",
                url: "Annonce/{action}/{id}",
                defaults: new { controller = "Post" }
            );

            routes.MapRoute(
                name: "FrenchRouteToSearch",
                url: "Recherche/",
                defaults: new { controller = "Search", action = "Index" }
            );

            routes.MapRoute(
                name: "FrenchRouteWithCategory",
                url: "Annonces/{categoryLabel}",
                defaults: new { controller = "Search", action = "SearchFromUrl" }
            );

            routes.MapRoute(
                name: "FrenchRouteWithCityAndCategory",
                url: "Annonces/{cityLabel}/{categoryLabel}",
                defaults: new { controller = "Search", action = "SearchFromUrl" }
            );

            routes.MapRoute(
                name: "DefaultWithParameter",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}