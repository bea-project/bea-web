using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bea.Domain;
using Bea.Dal;

namespace Bea.Test
{
    [TestClass]
    public class UserMapTest : InMemoryData
    {
        [TestMethod]
        public void AddUserWithoutPassword()
        {
            //Password is set to be non nullable, the save should throw an exception, and the user should not be added
            User userToBeAdded = new User();
            userToBeAdded.Email = "userToBeAdded@bea.com";
            try
            {
                Session.Save(userToBeAdded);
                Session.Flush();
            }
            catch
            {}
            
            IQueryable<User> userToBeAddedFromDb = new Repository<User>(Session).FilterBy(x => x.Email.Equals("userToBeAdded@bea.com"));
            Assert.IsTrue(userToBeAddedFromDb.Count() == 0);
        }

        [TestMethod]
        public void AddUserWithoutEmail()
        {
            //Password is set to be non nullable, the save should throw an exception, and the user should not be added
            User userToBeAdded = new User();
            userToBeAdded.Password = "userToBeAddedPwd";
            try
            {
                Session.Save(userToBeAdded);
                Session.Flush();
            }
            catch
            { }

            IQueryable<User> userToBeAddedFromDb = new Repository<User>(Session).FilterBy(x => x.Email.Equals("userToBeAdded@bea.com"));
            Assert.IsTrue(userToBeAddedFromDb.Count() == 0);
        }

        [TestMethod]
        public void AddUser()
        {
            //This user has both email and assword, the save should work and the user added to the database
            User userToBeAdded = new User();
            userToBeAdded.Email = "userToBeAdded@bea.com";
            userToBeAdded.Password = "userToBeAddedPwd";
            Session.Save(userToBeAdded);
            Session.Flush();
            IQueryable<User> userToBeAddedFromDb = new Repository<User>(Session).FilterBy(x => x.Email.Equals("userToBeAdded@bea.com"));
            Assert.IsTrue(userToBeAddedFromDb.Count() == 1);
        }
        
        [TestMethod]
        public void RemoveUserWithoutAds()
        {
            //Time to kill Nico !!
            User nico = new Repository<User>(Session).FilterBy(x => x.Email.Equals("nicolas.raynaud@gmail.com")).First();
            Assert.IsNotNull(nico);
            Session.Delete(nico);
            Session.Flush();
            List<User> isNicoStillAlive = new Repository<User>(Session).FilterBy(x => x.Email.Equals("nicolas.raynaud@gmail.com")).ToList<User>();
            Assert.IsTrue(isNicoStillAlive.Count() == 0);
        }

        [TestMethod]
        public void RemoveUserWithAds()
        {
            //Time to kill Bruno
            //Bruno has the only Ad of the database, deleting him should set the total number of ads of the db to 0

            IQueryable<Ad> ads = new Repository<Ad>(Session).All();
            Assert.IsTrue(ads.Count() == 1);

            User bruno = new Repository<User>(Session).FilterBy(x => x.Email.Equals("bruno.deprez@gmail.com")).First();
            Assert.IsNotNull(bruno);
            Session.Delete(bruno);
            Session.Flush();
            ads = new Repository<Ad>(Session).All();
            Assert.IsTrue(ads.Count() == 0);
        }
    }
}
