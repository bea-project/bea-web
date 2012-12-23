using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace Bea.Dal.Configuration
{
    public abstract class AbstractSessionFactoryFactory
    {
        private ISessionFactory _sessionFactory;
        protected Boolean _rebuildSchema;

        public ISessionFactory GetSessionFactory()
        {
            return _sessionFactory;
        }

        protected AbstractSessionFactoryFactory()
        {
            _sessionFactory = CreateSessionFactory();
        }

        protected AbstractSessionFactoryFactory(Boolean rebuildSchema)
        {
            _rebuildSchema = rebuildSchema;
            _sessionFactory = CreateSessionFactory();
        }

        protected ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure(new NHibernate.Cfg.Configuration())
              .Database(SetPersistenceConfigurer())
              .Mappings(m => m.FluentMappings.AddFromAssemblyOf<AbstractSessionFactoryFactory>())
              .ExposeConfiguration(cfg => ExposeConfiguration(cfg))
              .BuildSessionFactory();
        }

        protected virtual void ExposeConfiguration(NHibernate.Cfg.Configuration cfg)
        {
            SchemaExport sch = new SchemaExport(cfg);
            sch.Create(true, _rebuildSchema);
        }

        protected virtual IPersistenceConfigurer SetPersistenceConfigurer()
        {
            throw new NotImplementedException("You need to set a persistence configuration in the concrete class.");
        }
    }
}
