using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bea.Dal.Configuration;
using FluentNHibernate.Cfg.Db;
using NHibernate.Cfg;

namespace Bea.Web.NhibernateHelper
{
    public class MySQLWebSessionFactoryFactory : AbstractSessionFactoryFactory
    {
        public MySQLWebSessionFactoryFactory(Boolean rebuildSchema)
            : base(rebuildSchema)
        {

        }

        protected override IPersistenceConfigurer SetPersistenceConfigurer()
        {
            return MySQLConfiguration.Standard
                .ConnectionString(c => c.FromConnectionStringWithKey("mySqlServerConnectionString")).ShowSql();
        }

        protected override void ExposeConfiguration(Configuration cfg)
        {
            base.ExposeConfiguration(cfg);
            cfg.CurrentSessionContext<NHibernate.Context.WebSessionContext>();
        }
    }
}