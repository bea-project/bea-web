using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using bea.dal;
using bea.dal.entities;
using System.Data.SQLite;

namespace bea.test
{
    [TestClass]
    public class InMemoryTest : InMemoryData
    {
        [TestMethod]
        public void DatabaseCreation()
        {
            Repository<User> userRepo = new Repository<User>(Session);
            Assert.IsNotNull(userRepo);
            List<User> allUsers = userRepo.All().ToList<User>();
            Assert.IsNotNull(userRepo);
            Assert.IsTrue(allUsers.Count == 2);
            User user = allUsers.Where(x => x.email.Equals("bruno.deprez@gmail.com")).First();
            Assert.IsNotNull(user);
            Assert.IsTrue(user.ads.Count == 1);
        }
    }
}
