using bea.dal.configuration;
using FluentNHibernate.Cfg.Db;
using NHibernate.Cfg;

namespace bea.test.TestHelper
{
    public class SQLiteSessionFactoryFactory : AbstractSessionFactoryFactory
    {
        protected override IPersistenceConfigurer SetPersistenceConfigurer()
        {
            return SQLiteConfiguration.Standard.InMemory().ShowSql();
        }

        protected override void ExposeConfiguration(Configuration cfg)
        {
            base.ExposeConfiguration(cfg);
            cfg.SetProperty("current_session_context_class", "thread_static");
            cfg.SetProperty("connection.release_mode", "on_close");
        }
    }
}
