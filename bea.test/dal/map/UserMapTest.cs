using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Dal;
using Bea.Domain;
using Bea.Test.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;

namespace Bea.Test.dal.map
{
    [TestClass]
    public class UserMapTest : DataAccessTestBase
    {
        [TestMethod]
        public void AddUserWithPassword()
        {
            //Password is set to be non nullable, the save should throw an exception, and the user should not be added
            User userToBeAdded = new User() {
                Email = "userToBeAdded@bea.com",
                Password = "secret"
            };
            ISessionFactory sessionFactory = NhibernateHelper.SessionFactory;
            Repository<User> repo = new Repository<User>(sessionFactory.GetCurrentSession());

            using (ITransaction transaction = sessionFactory.GetCurrentSession().BeginTransaction())
            {
                sessionFactory.GetCurrentSession().Save(userToBeAdded);

                sessionFactory.GetCurrentSession().Get<User>(userToBeAdded.Id);
                IQueryable<User> userToBeAddedFromDb = repo.FilterBy(x => x.Email.Equals("userToBeAdded@bea.com"));
                Assert.IsTrue(userToBeAddedFromDb.Count() == 1);
            }
        }

        [TestMethod]
        public void AddUserWithPassword2()
        {
            //Password is set to be non nullable, the save should throw an exception, and the user should not be added
            User userToBeAdded = new User()
            {
                Email = "userToBeAdded@bea.com",
                Password = "secret"
            };
            ISessionFactory sessionFactory = NhibernateHelper.SessionFactory;
            Repository<User> repo = new Repository<User>(sessionFactory.GetCurrentSession());

            using (ITransaction transaction = sessionFactory.GetCurrentSession().BeginTransaction())
            {
                sessionFactory.GetCurrentSession().Save(userToBeAdded);

                sessionFactory.GetCurrentSession().Get<User>(userToBeAdded.Id);
                IQueryable<User> userToBeAddedFromDb = repo.FilterBy(x => x.Email.Equals("userToBeAdded@bea.com"));
                Assert.IsTrue(userToBeAddedFromDb.Count() == 1);
            }
        }
    }
}
