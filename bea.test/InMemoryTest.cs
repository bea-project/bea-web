using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bea.Dal;
using Bea.Domain;
using System.Data.SQLite;
using Bea.Domain.location;

namespace Bea.Test
{
    [TestClass]
    public class InMemoryTest : InMemoryData
    {
        [TestMethod]
        //Makes sure each entities are created, not checking the connection between them
        public void DatabaseCreation()
        {
            
            //User
            Repository<User> userRepo = new Repository<User>(Session);
            Assert.IsNotNull(userRepo);
            Assert.IsTrue(userRepo.All().Count() == 2);
            
            //Ad
            Repository<Ad> adRepo = new Repository<Ad>(Session);
            Assert.IsNotNull(adRepo);
            Assert.IsTrue(adRepo.All().Count() == 1);

            //City
            Repository<City> cityRepo = new Repository<City>(Session);
            Assert.IsNotNull(cityRepo);
            Assert.IsTrue(cityRepo.All().Count() == 2);

            //Province
            Repository<Province> provinceRepo = new Repository<Province>(Session);
            Assert.IsNotNull(provinceRepo);
            Assert.IsTrue(provinceRepo.All().Count() == 2);
        }
    }
}
