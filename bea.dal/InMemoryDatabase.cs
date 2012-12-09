using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Cfg;
using NHibernate;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using Bea.Dal.map;

namespace Bea.Dal
{
    public abstract class InMemoryDatabase : IDisposable
    {
        private static NHibernate.Cfg.Configuration _configuration;
        private static ISessionFactory _sessionFactory;

        protected ISession Session { get; set; }

        protected InMemoryDatabase()
        {
            _sessionFactory = CreateSessionFactory();
            Session = _sessionFactory.OpenSession();
            BuildSchema(Session);
        }
        private static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
              .Database(SQLiteConfiguration.Standard.InMemory().ShowSql())
              .Mappings(m => m.FluentMappings.AddFromAssemblyOf<InMemoryDatabase>())
              .ExposeConfiguration(Cfg => _configuration = Cfg)
              .BuildSessionFactory();
        }
        private static void BuildSchema(ISession Session)
        {
            SchemaExport export = new SchemaExport(_configuration);
            export.Execute(true, true, false, Session.Connection, null);
        }

        public void Dispose()
        {
            Session.Dispose();
        }
    }
}
