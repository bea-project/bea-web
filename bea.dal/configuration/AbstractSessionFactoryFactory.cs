using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace bea.dal.configuration
{
    public abstract class AbstractSessionFactoryFactory
    {
        protected ISessionFactory _sessionFactory;
        public ISessionFactory SessionFactory
        {
            get { return _sessionFactory ?? (_sessionFactory = CreateSessionFactory()); }
        }

        public NHibernate.Cfg.Configuration Configuration { get; set; }

        protected AbstractSessionFactoryFactory()
        {
            _sessionFactory = CreateSessionFactory();
        }

        protected ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
              .Database(SetPersistenceConfigurer())
              .Mappings(m => m.FluentMappings.AddFromAssemblyOf<AbstractSessionFactoryFactory>())
              .ExposeConfiguration(cfg => ExposeConfiguration(cfg))
              .BuildSessionFactory();
        }

        // TODO: For some reason, this code doesn't trigger db creation... to look into...
        protected virtual void ExposeConfiguration(NHibernate.Cfg.Configuration cfg)
        {
            Configuration = cfg;
            SchemaExport sch = new SchemaExport(cfg);
            sch.Create(true, true);
        }

        protected virtual IPersistenceConfigurer SetPersistenceConfigurer()
        {
            throw new NotImplementedException("You need to set a persistence configuration in the concrete class.");
        }
    }
}
