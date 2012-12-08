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
            User bruno = new Repository<User>(Session).FilterBy(x => x.Email.Equals("bruno.deprez@gmail.com")).First();
            City noumea = new Repository<City>(Session).FilterBy(x => x.Label.Equals("Noumea")).First();
            toBeAdded.Title = "Machine a cafe";
            toBeAdded.Body = "Magnifique machine Nespresso dedicacee par Georges Clooney";
            bruno.AddAd(toBeAdded);
            noumea.AddAd(toBeAdded);
            Session.Save(toBeAdded);
            Session.SaveOrUpdate(bruno);
            Session.SaveOrUpdate(noumea);
            Session.Flush();
            Assert.IsTrue(bruno.Ads.Count == 2);
            Assert.IsTrue(noumea.Ads.Count == 2);
        }
    }
}
