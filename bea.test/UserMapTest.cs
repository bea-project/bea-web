using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using bea.dal.entities;
using bea.dal;

namespace bea.test
{
    [TestClass]
    public class UserMapTest : InMemoryData
    {
        [TestMethod]
        public void AddUser()
        {
            User userToBeAdded = new User();
            userToBeAdded.email = "userToBeAdded@bea.com";
            userToBeAdded.password = "userToBeAddedPwd";
            Session.Save(userToBeAdded);
            Session.Flush();
            IQueryable<User> userToBeAddedFromDb = new Repository<User>(Session).FilterBy(x => x.email.Equals("userToBeAdded@bea.com"));
            Assert.IsTrue(userToBeAddedFromDb.Count() == 1);
        }
        
        [TestMethod]
        public void RemoveUser()
        {
            //Time to kill Nico !!
            User nico = new Repository<User>(Session).FilterBy(x => x.email.Equals("nicolas.raynaud@gmail.com")).First();
            Session.Delete(nico);
            Session.Flush();
            List<User> isNicoStillAlive = new Repository<User>(Session).FilterBy(x => x.email.Equals("nicolas.raynaud@gmail.com")).ToList<User>();
            Assert.IsTrue(isNicoStillAlive.Count() == 0);
        }
    }
}
