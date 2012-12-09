using Bea.Dal.configuration;
using NHibernate;

namespace Bea.Test.TestHelper
{
    public class NhibernateHelper
    {
        private static AbstractSessionFactoryFactory _sff = new SQLiteTestSessionFactoryFactory(true);
        private static ISessionFactory _sessionFactory;

        public static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    _sessionFactory = _sff.GetSessionFactory();
                }

                return _sessionFactory;
            }
        }
    }
}
