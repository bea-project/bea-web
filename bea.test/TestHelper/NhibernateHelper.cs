using bea.dal.configuration;
using NHibernate;

namespace bea.test.TestHelper
{
    public class NhibernateHelper
    {
        private static AbstractSessionFactoryFactory _sff = new SQLiteSessionFactoryFactory();
        private static ISessionFactory _sessionFactory;

        public static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    _sessionFactory = _sff.SessionFactory;
                }

                return _sessionFactory;
            }
        }


        public static NHibernate.Cfg.Configuration Configuration
        {
            get
            {
                return _sff.Configuration;
            }
        }
    }
}
