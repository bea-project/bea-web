using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bea.Domain;
using Bea.Dal;
using Bea.Test.TestHelper;
using NHibernate;
using Bea.Dal.Repository;

namespace Bea.Test
{
    [TestClass]
    public class UserMapTest : DataAccessTestBase
    {
        [TestMethod]
        [ExpectedException(typeof(PropertyValueException))]
        public void AddUserWithoutPassword()
        {
            //Password is set to be non nullable, the save should throw an exception, and the user should not be added
            ISessionFactory sessionFactory = NhibernateHelper.SessionFactory;
            Repository repo = new Repository(sessionFactory);
            User userToBeAdded = new User();
            userToBeAdded.Email = "userToBeAdded@bea.com";
            using (ITransaction transaction = sessionFactory.GetCurrentSession().BeginTransaction())
            {
                repo.Save(userToBeAdded);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(PropertyValueException))]
        public void AddUserWithoutEmail()
        {
            //Password is set to be non nullable, the save should throw an exception, and the user should not be added
            ISessionFactory sessionFactory = NhibernateHelper.SessionFactory;
            Repository repo = new Repository(sessionFactory);
            User userToBeAdded = new User();
            userToBeAdded.Password = "userToBeAddedPwd";
            using (ITransaction transaction = sessionFactory.GetCurrentSession().BeginTransaction())
            {
                repo.Save(userToBeAdded);
            }
        }

        [TestMethod]
        public void AddUser()
        {
            //This user has both email and assword, the save should work and the user added to the database
            ISessionFactory sessionFactory = NhibernateHelper.SessionFactory;
            Repository repo = new Repository(sessionFactory);
            Repository<User> uRepo = new Repository<User>(sessionFactory.GetCurrentSession());
            User userToBeAdded = new User();
            userToBeAdded.Email = "userToBeAdded@bea.com";
            userToBeAdded.Password = "userToBeAddedPwd";
            using (ITransaction transaction = sessionFactory.GetCurrentSession().BeginTransaction())
            {
                repo.Save(userToBeAdded);
                IQueryable<User> userToBeAddedFromDb = uRepo.FilterBy(x => x.Email.Equals("userToBeAdded@bea.com"));
                Assert.IsTrue(userToBeAddedFromDb.Count() == 1);
            }
        }

        [TestMethod]
        public void RemoveUserWithoutAds()
        {
            ISessionFactory sessionFactory = NhibernateHelper.SessionFactory;
            Repository repo = new Repository(sessionFactory);
            Repository<User> uRepo = new Repository<User>(sessionFactory.GetCurrentSession());
            User userWoAds = new User();
            userWoAds.Email = "userToBeAdded@bea.com";
            userWoAds.Password = "userToBeAddedPwd";
            using (ITransaction transaction = sessionFactory.GetCurrentSession().BeginTransaction())
            {
                repo.Save(userWoAds);
                IQueryable<User> userToBeAddedFromDb = uRepo.FilterBy(x => x.Email.Equals("userToBeAdded@bea.com"));
                Assert.IsTrue(userToBeAddedFromDb.Count() == 1);
                
                repo.Delete(userWoAds);
                userToBeAddedFromDb = uRepo.FilterBy(x => x.Email.Equals("userToBeAdded@bea.com"));
                Assert.IsTrue(userToBeAddedFromDb.Count() == 0);
            }
        }

        //[TestMethod]
        //public void RemoveUserWithAds()
        //{
        //    ISessionFactory sessionFactory = NhibernateHelper.SessionFactory;
        //    Repository repo = new Repository(sessionFactory);
        //    Repository<User> uRepo = new Repository<User>(sessionFactory.GetCurrentSession());
        //    User userWoAds = new User();
        //    userWoAds.Email = "userToBeAdded@bea.com";
        //    userWoAds.Password = "userToBeAddedPwd";

        //    Ad ad = new Ad();
        //    ad.Body = "blabla";
            

        //    using (ITransaction transaction = sessionFactory.GetCurrentSession().BeginTransaction())
        //    {
        //        repo.Save(userWoAds);
        //        IQueryable<User> userToBeAddedFromDb = uRepo.FilterBy(x => x.Email.Equals("userToBeAdded@bea.com"));
        //        Assert.IsTrue(userToBeAddedFromDb.Count() == 1);

        //        repo.Delete(userWoAds);
        //        userToBeAddedFromDb = uRepo.FilterBy(x => x.Email.Equals("userToBeAdded@bea.com"));
        //        Assert.IsTrue(userToBeAddedFromDb.Count() == 0);
        //    }
        //}
    }
}
