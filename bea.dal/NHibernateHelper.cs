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
            //IPersistenceConfigurer cfg = MySQLConfiguration.Standard.ConnectionString("server=5405a0a1-6330-4145-b792-a118002f59ec.mysql.sequelizer.com;database=db5405a0a163304145b792a118002f59ec;uid=fdlddzaqgfwwlfvm;pwd=L5jrvnc6ZWWbbLspgtHRfwdRvBDggEunetjn52g8gDrtgaLL4LVgMhxp2z42Q5Yt");


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
