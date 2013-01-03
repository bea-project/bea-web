using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bea.Dal;
using Bea.Domain;
using Bea.Domain.Location;
using Bea.Test.TestHelper;
using NHibernate;
using Bea.Dal.Repository;
using Bea.Domain.Category;

namespace Bea.Test
{
    [TestClass]
    public class AdMapTest : DataAccessTestBase
    {
        [TestMethod]
        public void AddAdToUser()
        {
            //User Bruno has one Ad, adding another one should bring his Ad list size to 2
            //This ad is located in Noumea, it should bring Noumea's number of ad to 2
            ISessionFactory sessionFactory = NhibernateHelper.SessionFactory;
            Repository repo = new Repository(sessionFactory);

            using (ITransaction transaction = sessionFactory.GetCurrentSession().BeginTransaction())
            {

                User u = new User()
                {
                    Email = "",
                    Password = ""
                };
                repo.Save(u);
                int uId = u.UserId;
                City c = new City()
                {
                    Label = "Nouméa"
                };
                repo.Save(c);

                CategoryElement cat = new CategoryElement
                {
                    Label = "Catamaran"
                };
                repo.Save(cat);

                Ad ad = new Ad
                {
                    Title = "Machine a cafe",
                    Body = "Magnifique machine Nespresso dedicacee par Georges Clooney"
                };
                c.AddAd(ad);
                cat.AddAd(ad);
                u.AddAd(ad);
                repo.Save(ad);

                repo.Flush();
                repo.Clear();

                Assert.AreEqual(1, repo.Get<User>(uId).Ads.Count);
            }
        }
    }
}
