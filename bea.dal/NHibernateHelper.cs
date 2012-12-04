using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Cfg;

namespace bea.dal
{
    public class NHibernateHelper
    {
        private ISessionFactory _sessionFactory;

        public ISessionFactory SessionFactory
        {
            get { return _sessionFactory ?? (_sessionFactory = CreateSessionFactory()); }
        }

        private ISessionFactory CreateSessionFactory()
        {
            IPersistenceConfigurer cfg = MySQLConfiguration.Standard.ConnectionString(c => c.FromConnectionStringWithKey("ProdDb"));


            return Fluently.Configure()
                .Database(cfg)
                .Mappings(m =>
                {
                    m.FluentMappings
                        .AddFromAssemblyOf<NHibernateHelper>();
                    ;
                })
                .BuildSessionFactory();
        }
    }
}
