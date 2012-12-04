using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using bea.dal;
using bea.dal.entities;

namespace bea.test
{
    [TestClass]
    public class AdMapTest:InMemoryData
    {
        [TestMethod]
        public void AddAdToUser()
        {
            //User Bruno has one Ad, adding another one should bring his Ad list size to 2
            Ad toBeAdded = new Ad();
            User bruno = new Repository<User>(Session).FilterBy(x => x.email.Equals("bruno.deprez@gmail.com")).First();
            toBeAdded.title = "Machine a cafe";
            toBeAdded.body = "Magnifique machine Nespresso dedicacee par Georges Clooney";
            bruno.AddAd(toBeAdded);
            Session.Save(toBeAdded);
            Session.SaveOrUpdate(bruno);
            Session.Flush();
            Assert.IsTrue(bruno.ads.Count == 2);
        }
    }
}
