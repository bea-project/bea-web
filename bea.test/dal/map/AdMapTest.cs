using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Dal.Repository;
using Bea.Domain;
using Bea.Domain.Location;
using Bea.Test.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;

namespace Bea.Test.Dal.map
{
    [TestClass]
    public class AdMapTest : DataAccessTestBase
    {
        [TestMethod]
        public void CreateAd_AddAdToCityAndUser()
        {
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
                int cId = c.Id;

                Ad ad = new Ad
                {
                    Title = "title",
                    Body = "body",
                    CreatedBy = u
                };
                c.AddAd(ad);
                u.AddAd(ad);

                repo.Save(ad);
                repo.Flush();
                repo.Clear();

                Assert.AreEqual(1, repo.Get<User>(uId).Ads.Count);
                Assert.AreEqual(1, repo.Get<City>(cId).Ads.Count);
            }
        }

        [TestMethod]
        public void CreateAd_AddImages()
        {
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
                int cId = c.Id;

                Ad ad = new Ad
                {
                    Title = "title",
                    Body = "body",
                    CreatedBy = u
                };
                c.AddAd(ad);
                u.AddAd(ad);

                AdImage img = new AdImage();
                repo.Save(img);

                ad.AddImage(img);
                repo.Save(ad);
                long aId = ad.Id;

                repo.Flush();
                repo.Clear();

                Assert.AreEqual(1, repo.Get<User>(uId).Ads.Count);
                Assert.AreEqual(1, repo.Get<City>(cId).Ads.Count);
                Assert.AreEqual(1, repo.Get<Ad>(aId).Images.Count);
            }
        }
    }
}
