using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain;
using Bea.Domain.Location;
using Bea.Test.TestHelper;
using Bea.Dal.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using Bea.Dal.Repository;

namespace Bea.Test.dal.repository
{
    [TestClass]
    public class AdRepositoryTest : DataAccessTestBase
    {
        [TestMethod]
        public void CountAdsByCity_2Citiesand3Ad_Return2elements()
        {
            ISessionFactory sessionFactory = NhibernateHelper.SessionFactory;
            Repository repo = new Repository(sessionFactory.GetCurrentSession());
            AdRepository adRepo = new AdRepository(sessionFactory);

            using (ITransaction transaction = sessionFactory.GetCurrentSession().BeginTransaction())
            {
                // Given
                #region test data

                User u = new User
                {
                    Email = "test@test.com",
                    Password = "hihi"
                };
                repo.Save<User>(u);

                City c = new City
                {
                    Label = "CherzmOi"
                };
                Ad a = new Ad
                {
                    Title = "titre",
                    Body = "content",
                    CreatedBy = u,
                };
                c.AddAd(a);
                repo.Save<City>(c);
                repo.Save<Ad>(a);

                Ad a2 = new Ad
                {
                    Title = "titre",
                    Body = "content",
                    CreatedBy = u,
                };
                c.AddAd(a2);
                repo.Save<Ad>(a2);

                City c2 = new City
                {
                    Label = "CherzmOi 2"
                };
                Ad a3 = new Ad
                {
                    Title = "titre",
                    Body = "content",
                    CreatedBy = u,
                };
                c2.AddAd(a3);
                repo.Save<City>(c2);
                repo.Save<Ad>(a3);

                repo.Flush();

                #endregion

                // When
                IDictionary<City, int> result = adRepo.CountAdsByCity();
                
                // Then
                Assert.AreEqual(2, result.Count);
                Assert.AreEqual(2, result[c]);
                Assert.AreEqual(1, result[c2]);
            }
        }
    }
}
