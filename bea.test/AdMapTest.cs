using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using bea.dal;
using bea.domain;
using bea.domain.location;

namespace bea.test
{
    [TestClass]
    public class AdMapTest:InMemoryData
    {
        [TestMethod]
        public void AddAdToUser()
        {
            //User Bruno has one Ad, adding another one should bring his Ad list size to 2
            //This ad is located in Noumea, it should bring Noumea's number of ad to 2
            Ad toBeAdded = new Ad();
            User bruno = new Repository<User>(Session).FilterBy(x => x.email.Equals("bruno.deprez@gmail.com")).First();
            City noumea = new Repository<City>(Session).FilterBy(x => x.label.Equals("Noumea")).First();
            toBeAdded.title = "Machine a cafe";
            toBeAdded.body = "Magnifique machine Nespresso dedicacee par Georges Clooney";
            bruno.AddAd(toBeAdded);
            noumea.AddAd(toBeAdded);
            Session.Save(toBeAdded);
            Session.SaveOrUpdate(bruno);
            Session.SaveOrUpdate(noumea);
            Session.Flush();
            Assert.IsTrue(bruno.ads.Count == 2);
            Assert.IsTrue(noumea.ads.Count == 2);
        }
    }
}
