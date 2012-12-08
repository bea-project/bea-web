using bea.dal.configuration;
using FluentNHibernate.Cfg.Db;

namespace bea.test.NhibernateHelper
{
    public class SQLiteSessionFactoryFactory : AbstractSessionFactoryFactory
    {
        protected override IPersistenceConfigurer SetPersistenceConfigurer()
        {
            return SQLiteConfiguration.Standard.InMemory().ShowSql();
        }
    }
}
