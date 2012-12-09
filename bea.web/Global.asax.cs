﻿using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Bea.Web.App_Start;

namespace Bea.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ContainerConfig.RegisterContainer();

            // Enable log4net configuration
            log4net.Config.XmlConfigurator.Configure();
        }
    }
}