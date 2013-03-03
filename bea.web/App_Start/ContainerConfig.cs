using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Bea.Core.Dal;
using Bea.Core.Services;
using Bea.Services;
using NHibernate;
using Bea.Web.NhibernateHelper;
using Bea.Dal.Repository;
using System.Reflection;
using System.Web.Http;
using Bea.Services.Ads;
using Bea.Core.Services.Ads;
using System.Web.Hosting;
using System.IO;
using Bea.Tools;

namespace Bea.Web.App_Start
{
    public class ContainerConfig
    {
        public static void RegisterContainer()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            //builder.Register<ISessionFactory>(x => new SQLiteWebSessionFactoryFactory(true).GetSessionFactory()).SingleInstance();
            builder.Register<ISessionFactory>(x => new MySQLWebSessionFactoryFactory(false).GetSessionFactory()).SingleInstance();
            builder.RegisterType<Repository>().As<IRepository>().SingleInstance();
            builder.RegisterType<AdRepository>().As<IAdRepository>().SingleInstance();
            builder.RegisterType<UserRepository>().As<IUserRepository>().SingleInstance();
            builder.RegisterType<LocationRepository>().As<ILocationRepository>().SingleInstance();
            builder.RegisterType<ReferenceRepository>().As<IReferenceRepository>().SingleInstance();
            builder.RegisterType<AdServices>().As<IAdServices>().SingleInstance();
            builder.RegisterType<SearchServices>().As<ISearchServices>().SingleInstance();
            builder.RegisterType<AdImageServices>().As<IAdImageServices>().SingleInstance();
            builder.RegisterType<LocationServices>().As<ILocationServices>().SingleInstance();
            builder.RegisterType<UserServices>().As<IUserServices>().SingleInstance();
            builder.RegisterType<HelperService>().As<IHelperService>().SingleInstance();
            builder.RegisterType<ReferenceServices>().As<IReferenceServices>().SingleInstance();
            builder.RegisterType<CategoryServices>().As<ICategoryServices>().SingleInstance();
            builder.RegisterType<AdDataConsistencyServices>().As<IAdDataConsistencyServices>().SingleInstance();
            builder.RegisterType<AdRequestServices>().As<IAdRequestServices>().SingleInstance();
            builder.RegisterType<AdActivationServices>().As<IAdActivationServices>().SingleInstance();
            builder.RegisterType<AdDeletionServices>().As<IAdDeletionServices>().SingleInstance();
            builder.RegisterType<AdDetailsServices>().As<IAdDetailsServices>().SingleInstance();
            builder.RegisterType<EmailServices>().As<IEmailServices>().SingleInstance();
            builder.Register<ITemplatingService>(x => new TemplatingService(Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "App_Data\\Templates"))).SingleInstance();
            
            // Register the inMemoryData singleton to inject data
            builder.Register(x => new InMemoryDataInjector(x.Resolve<ISessionFactory>(), x.Resolve<IRepository>())).SingleInstance();

            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}