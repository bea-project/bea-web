using System;
using Bea.Dal.Configuration;
using FluentNHibernate.Cfg.Db;
using NHibernate.Cfg;

namespace Bea.Test.TestHelper
{
    public class SQLiteTestSessionFactoryFactory : AbstractSessionFactoryFactory
    {
        public SQLiteTestSessionFactoryFactory(Boolean rebuildSchema)
            : base(rebuildSchema)
        {

        }

        protected override IPersistenceConfigurer SetPersistenceConfigurer()
        {
            return SQLiteConfiguration.Standard.InMemory().ShowSql()
                    .Provider<CustomLongLastingSQLiteConnectionProvider>();
        }

        protected override void ExposeConfiguration(Configuration cfg)
        {
            base.ExposeConfiguration(cfg);
            cfg.CurrentSessionContext<NHibernate.Context.ThreadStaticSessionContext>();
        }
    }
}
