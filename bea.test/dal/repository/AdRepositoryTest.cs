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
using Bea.Domain.Categories;
using Bea.Domain.Ads;

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

                Category cat = new Category
                {
                    Label = "Bateau"
                };

                Ad a = new Ad
                {
                    Title = "titre",
                    Body = "content",
                    CreatedBy = u,
                    City = c,
                    Category = cat
                };
                c.AddAd(a);
                cat.AddAd(a);
                repo.Save<City>(c);
                repo.Save<Category>(cat);
                repo.Save<Ad>(a);

                Ad a2 = new Ad
                {
                    Title = "titre",
                    Body = "content",
                    CreatedBy = u,
                    City = c,
                    Category = cat
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
                    City = c2,
                    Category = cat
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

                Category cat = new Category
                {
                    Label = "Moto"
                };

                Ad a = new Ad
                {
                    Title = "titre 1",
                    Body = "content",
                    CreatedBy = u,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 12)
                };
                c.AddAd(a);
                cat.AddAd(a);
                repo.Save<City>(c);
                repo.Save<Category>(cat);
                repo.Save<Ad>(a);

                Ad a2 = new Ad
                {
                    Title = "title 2",
                    Body = "content",
                    CreatedBy = u,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 18)
                };
                c.AddAd(a2);
                cat.AddAd(a2);
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

                Category cat = new Category 
                {
                    Label = "Voiture"
                };

                Ad a = new Ad
                {
                    Title = "titre 1",
                    Body = "content",
                    CreatedBy = u,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 12),
                    Category = cat
                };
                c.AddAd(a);
                cat.AddAd(a);
                repo.Save<City>(c);
                repo.Save<Category>(cat);
                repo.Save<Ad>(a);

                Ad a2 = new Ad
                {
                    Title = "title 2",
                    Body = "content",
                    CreatedBy = u,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 17),
                    Category = cat
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

                Category cat = new Category
                {
                    Label = "Voiture"
                };

                Ad a = new Ad
                {
                    Title = "titre 1",
                    Body = "content",
                    CreatedBy = u,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 18)
                };
                c.AddAd(a);
                cat.AddAd(a);
                repo.Save<City>(c);
                repo.Save<Category>(cat);
                repo.Save<Ad>(a);

                Ad a2 = new Ad
                {
                    Title = "title 2",
                    Body = "content",
                    CreatedBy = u,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 17)
                };
                c.AddAd(a2);
                cat.AddAd(a2);
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

                Category cat = new Category 
                {
                    Label = "Moto"
                };

                Ad a = new Ad
                {
                    Title = "titre 1",
                    Body = "content",
                    CreatedBy = u,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 18),
                    Category = cat
                };
                c.AddAd(a);
                cat.AddAd(a);
                repo.Save<City>(c);
                repo.Save<Category>(cat);
                repo.Save<Ad>(a);

                Ad a2 = new Ad
                {
                    Title = "title 2",
                    Body = "content",
                    CreatedBy = u,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 17),
                    Category = cat
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

        [TestMethod]
        public void SearchAds_SearchByTitleAndBodyOnly()
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

                Category cat = new Category
                {
                    Label = "Moto"
                };

                Ad a = new Ad
                {
                    Title = "titre 1",
                    Body = "content",
                    CreatedBy = u,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 18),
                    Category = cat
                };
                c.AddAd(a);
                cat.AddAd(a);
                repo.Save<City>(c);
                repo.Save<Category>(cat);
                repo.Save<Ad>(a);

                Ad a2 = new Ad
                {
                    Title = "title 2",
                    Body = "content",
                    CreatedBy = u,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 17),
                    Category = cat
                };
                c.AddAd(a2);
                cat.AddAd(a2);
                repo.Save<Ad>(a2);

                repo.Flush();

                #endregion

                // When
                IList<Ad> result = adRepo.SearchAds(orSearchStrings: new String[] { "tre" });

                // Then
                Assert.AreEqual(1, result.Count);
                Assert.AreEqual(a, result[0]);
            }
        }

        [TestMethod]
        public void SearchAds_SearchByTitleAndBodyAndCity()
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

                Category cat = new Category
                {
                    Label = "Moto"
                };

                Ad a = new Ad
                {
                    Title = "titre 1",
                    Body = "content",
                    CreatedBy = u,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 18),
                    Category = cat
                };
                c.AddAd(a);
                cat.AddAd(a);
                repo.Save<City>(c);
                repo.Save<Category>(cat);
                repo.Save<Ad>(a);

                City c2 = new City
                {
                    Label = "CherzmOi2"
                };

                Category cat2 = new Category
                {
                    Label = "Auto"
                };

                Ad a2 = new Ad
                {
                    Title = "title 2",
                    Body = "content",
                    CreatedBy = u,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 17),
                    Category=cat2
                };
                c2.AddAd(a2);
                cat2.AddAd(a2);
                repo.Save<City>(c2);
                repo.Save<Category>(cat2);
                repo.Save<Ad>(a2);


                repo.Flush();

                #endregion

                // When
                IList<Ad> result = adRepo.SearchAds(orSearchStrings: new String[] { "ti" }, cityId: c2.Id);

                // Then
                Assert.AreEqual(1, result.Count);
                Assert.AreEqual(a2, result[0]);
            }
        }

        [TestMethod]
        public void SearchAds_SearchByTitleAndBodyAndProvince()
        {
            ISessionFactory sessionFactory = NhibernateHelper.SessionFactory;
            Repository repo = new Repository(sessionFactory);
            AdRepository adRepo = new AdRepository(sessionFactory);

            using (ITransaction transaction = sessionFactory.GetCurrentSession().BeginTransaction())
            {
                // Given
                #region test data
                Province p1 = new Province
                {
                    Label = "p1"
                };

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
                p1.AddCity(c);
                
                Category cat = new Category
                {
                    Label = "Moto"
                };

                Ad a = new Ad
                {
                    Title = "titre 1",
                    Body = "content",
                    CreatedBy = u,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 18),
                    Category = cat
                };
                c.AddAd(a);
                cat.AddAd(a);
                repo.Save<Province>(p1);
                repo.Save<City>(c);
                repo.Save<Category>(cat);
                repo.Save<Ad>(a);

                Province p2 = new Province
                {
                    Label = "p2"
                };
                City c2 = new City
                {
                    Label = "CherzmOi2"
                };
                p2.AddCity(c2);
                Ad a2 = new Ad
                {
                    Title = "title 2",
                    Body = "content",
                    CreatedBy = u,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 17),
                    Category = cat
                };
                c2.AddAd(a2);
                repo.Save<Province>(p2);
                repo.Save<City>(c2);
                repo.Save<Ad>(a2);

                repo.Flush();

                #endregion

                // When
                IList<Ad> result = adRepo.SearchAds(orSearchStrings: new String[] { "ti" }, provinceId: c2.Province.Id);

                // Then
                Assert.AreEqual(1, result.Count);
                Assert.AreEqual(a2, result[0]);
            }
        }

        [TestMethod]
        public void SearchAds_SearchByTitleAndBodyAndCityAndProvince_DontSearchByProvince()
        {
            ISessionFactory sessionFactory = NhibernateHelper.SessionFactory;
            Repository repo = new Repository(sessionFactory);
            AdRepository adRepo = new AdRepository(sessionFactory);

            using (ITransaction transaction = sessionFactory.GetCurrentSession().BeginTransaction())
            {
                // Given
                #region test data
                Province p1 = new Province
                {
                    Label = "p1"
                };

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
                p1.AddCity(c);

                Category cat = new Category
                {
                    Label = "Moto"
                };

                Ad a = new Ad
                {
                    Title = "titre 1",
                    Body = "content",
                    CreatedBy = u,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 18),
                    Category = cat
                };
                c.AddAd(a);
                cat.AddAd(a);
                repo.Save<Province>(p1);
                repo.Save<City>(c);
                repo.Save<Category>(cat);
                repo.Save<Ad>(a);

                Province p2 = new Province
                {
                    Label = "p2"
                };
                City c2 = new City
                {
                    Label = "CherzmOi2"
                };
                p2.AddCity(c2);
                Ad a2 = new Ad
                {
                    Title = "title 2",
                    Body = "content",
                    CreatedBy = u,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 17),
                    Category = cat
                };
                c2.AddAd(a2);
                repo.Save<Province>(p2);
                repo.Save<City>(c2);
                repo.Save<Ad>(a2);


                repo.Flush();

                #endregion

                // When
                IList<Ad> result = adRepo.SearchAds(null, new String[]{ "ti" }, provinceId: c2.Province.Id, cityId: c.Id);

                // Then
                Assert.AreEqual(1, result.Count);
                Assert.AreEqual(a, result[0]);
            }
        }

        [TestMethod]
        public void SearchAds_SearchByTitleAndBodyOrString_CreateWhereOrQuery()
        {
            ISessionFactory sessionFactory = NhibernateHelper.SessionFactory;
            Repository repo = new Repository(sessionFactory);
            AdRepository adRepo = new AdRepository(sessionFactory);

            using (ITransaction transaction = sessionFactory.GetCurrentSession().BeginTransaction())
            {
                // Given
                #region test data
                Province p1 = new Province
                {
                    Label = "p1"
                };

                User u = new User
                {
                    Email = "test@test.com",
                    Password = "hihi"
                };
                repo.Save<User>(u);

                City c = new City
                {
                    Label = "ship"
                };
                p1.AddCity(c);
                
                Category cat = new Category
                {
                    Label = "Moto"
                };

                Ad a = new Ad
                {
                    Title = "ship",
                    Body = "content",
                    CreatedBy = u,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 18),
                    Category = cat
                };
                c.AddAd(a);
                cat.AddAd(a);
                repo.Save<Province>(p1);
                repo.Save<City>(c);
                repo.Save<Category>(cat);
                repo.Save<Ad>(a);

                Province p2 = new Province
                {
                    Label = "p2"
                };
                City c2 = new City
                {
                    Label = "CherzmOi2"
                };
                p2.AddCity(c2);

                Ad a2 = new Ad
                {
                    Title = "car",
                    Body = "content",
                    CreatedBy = u,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 17),
                    Category = cat
                };
                c2.AddAd(a2);
                repo.Save<Province>(p2);
                repo.Save<City>(c2);
                repo.Save<Ad>(a2);


                repo.Flush();

                #endregion

                // When
                IList<Ad> result = adRepo.SearchAds(null, new String[]{ "car", "ship" }, null, null);

                // Then
                Assert.AreEqual(2, result.Count);
                Assert.AreEqual(a, result[0]);
                Assert.AreEqual(a2, result[1]);
            }
        }

        [TestMethod]
        public void SearchAds_SearchByTitleAndBodyAndString_CreateWhereAndQuery()
        {
            ISessionFactory sessionFactory = NhibernateHelper.SessionFactory;
            Repository repo = new Repository(sessionFactory);
            AdRepository adRepo = new AdRepository(sessionFactory);

            using (ITransaction transaction = sessionFactory.GetCurrentSession().BeginTransaction())
            {
                // Given
                #region test data
                Province p1 = new Province
                {
                    Label = "p1"
                };

                User u = new User
                {
                    Email = "test@test.com",
                    Password = "hihi"
                };
                repo.Save<User>(u);

                City c = new City
                {
                    Label = "city"
                };
                p1.AddCity(c);

                Category cat = new Category
                {
                    Label = "Moto"
                };

                Ad a = new Ad
                {
                    Title = "ship",
                    Body = "computer",
                    CreatedBy = u,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 18),
                    Category = cat
                };
                c.AddAd(a);
                cat.AddAd(a);
                repo.Save<Province>(p1);
                repo.Save<City>(c);
                repo.Save<Category>(cat);
                repo.Save<Ad>(a);

                Province p2 = new Province
                {
                    Label = "p2"
                };
                City c2 = new City
                {
                    Label = "CherzmOi2"
                };
                p2.AddCity(c2);
                Ad a2 = new Ad
                {
                    Title = "ship",
                    Body = "content",
                    CreatedBy = u,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 17),
                    Category = cat
                };
                c2.AddAd(a2);
                repo.Save<Province>(p2);
                repo.Save<City>(c2);
                repo.Save<Ad>(a2);


                repo.Flush();

                #endregion

                // When
                IList<Ad> result = adRepo.SearchAds(new String[] { "computer", "ship" }, null, null, null);

                // Then
                Assert.AreEqual(1, result.Count);
                Assert.AreEqual(a, result[0]);
            }
        }
    }
}