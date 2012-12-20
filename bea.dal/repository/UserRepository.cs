using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Linq;
using Bea.Core.Dal;
using Bea.Domain;

namespace Bea.Dal.Repository
{
    public class UserRepository : IUserRepository
    {
        protected ISessionFactory _sessionFactory;

        public UserRepository(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public User GetUserFromEmail(string email)
        {
            return _sessionFactory.GetCurrentSession().Query<User>().Where(x => x.Email.Equals(email)).FirstOrDefault();
        }

    }
}
