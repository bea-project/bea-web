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

namespace Bea.Test.dal.repository
{
    [TestClass]
    public class AdRepositoryTest : DataAccessTestBase
    {
        [TestMethod]
        public void CountAdsByCity_2Citiesand3Ad_Return2elements()
        {
            ISessionFactory sessionFactory = NhibernateHelper.SessionFactory;
            Repository repo = new Repository(sessionFactory);
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

        [TestMethod]
        public void SearchAdsByTitle_SearchStringIsNull_ReturnEverythingOrderedByCreationDate()
        {
            ISessionFactory sessionFactory = NhibernateHelper.SessionFactory;
            Repository repo = new Repository(sessionFactory);
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
                    Title = "titre 1",
                    Body = "content",
                    CreatedBy = u,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 12)
                };
                c.AddAd(a);
                repo.Save<City>(c);
                repo.Save<Ad>(a);

                Ad a2 = new Ad
                {
                    Title = "title 2",
                    Body = "content",
                    CreatedBy = u,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 18)
                };
                c.AddAd(a2);
                repo.Save<Ad>(a2);


                repo.Flush();

                #endregion

                // When
                IList<Ad> result = adRepo.SearchAdsByTitle(null);

                // Then
                Assert.AreEqual(2, result.Count);
                Assert.AreEqual(a2, result[0]);
                Assert.AreEqual(a, result[1]);
            }
        }

        [TestMethod]
        public void SearchAdsByTitle_SearchStringIsEmpty_ReturnEverythingOrderedByCreationDate()
        {
            ISessionFactory sessionFactory = NhibernateHelper.SessionFactory;
            Repository repo = new Repository(sessionFactory);
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
                    Title = "titre 1",
                    Body = "content",
                    CreatedBy = u,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 12)
                };
                c.AddAd(a);
                repo.Save<City>(c);
                repo.Save<Ad>(a);

                Ad a2 = new Ad
                {
                    Title = "title 2",
                    Body = "content",
                    CreatedBy = u,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 17)
                };
                c.AddAd(a2);
                repo.Save<Ad>(a2);


                repo.Flush();

                #endregion

                // When
                IList<Ad> result = adRepo.SearchAdsByTitle(String.Empty);

                // Then
                Assert.AreEqual(2, result.Count);
                Assert.AreEqual(a2, result[0]);
                Assert.AreEqual(a, result[1]);
            }
        }

        [TestMethod]
        public void SearchAdsByTitle_SearchStringIsNotNullOrEmpty_ReturnMatchedAd()
        {
            ISessionFactory sessionFactory = NhibernateHelper.SessionFactory;
            Repository repo = new Repository(sessionFactory);
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
                    Title = "titre 1",
                    Body = "content",
                    CreatedBy = u,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 18)
                };
                c.AddAd(a);
                repo.Save<City>(c);
                repo.Save<Ad>(a);

                Ad a2 = new Ad
                {
                    Title = "title 2",
                    Body = "content",
                    CreatedBy = u,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 17)
                };
                c.AddAd(a2);
                repo.Save<Ad>(a2);


                repo.Flush();

                #endregion

                // When
                IList<Ad> result = adRepo.SearchAdsByTitle("tre");

                // Then
                Assert.AreEqual(1, result.Count);
                Assert.AreEqual(a, result[0]);
            }
        }

        [TestMethod]
        public void DeleteAdFromDb()
        {
            ISessionFactory sessionFactory = NhibernateHelper.SessionFactory;
            Repository repo = new Repository(sessionFactory);
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
                    Title = "titre 1",
                    Body = "content",
                    CreatedBy = u,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 18)
                };
                c.AddAd(a);
                repo.Save<City>(c);
                repo.Save<Ad>(a);

                Ad a2 = new Ad
                {
                    Title = "title 2",
                    Body = "content",
                    CreatedBy = u,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 17)
                };
                c.AddAd(a2);
                repo.Save<Ad>(a2);


                repo.Flush();

                #endregion

                // When
                adRepo.DeleteAdById(1);
                List<Ad> result= adRepo.GetAllAds();

                // Then
                Assert.AreEqual(1, result.Count);
                Assert.AreEqual(a2, result[0]);
            }
        }
    }
}