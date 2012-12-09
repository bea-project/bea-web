using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Bea.Core.dal;
using Bea.Core.services;
using Bea.Services;
using Bea.Dal.repository;
using Bea.Web.App_Start;
using NHibernate;
using NHibernate.Context;
using Bea.Web.NhibernateHelper;

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