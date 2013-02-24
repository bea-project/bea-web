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
using Bea.Domain.Search;

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

                SearchAdCache a = new SearchAdCache
                {
                    AdId = 1,
                    Title = "titre 1",
                    Body = "content",
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 18),
                    Category = cat,
                    City = c
                };
                repo.Save<City>(c);
                repo.Save<Category>(cat);
                repo.Save(a);

                SearchAdCache a2 = new SearchAdCache
                {
                    AdId = 2,
                    Title = "title 2",
                    Body = "content",
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 17),
                    Category = cat,
                    City = c
                };
                repo.Save(a2);

                repo.Flush();

                #endregion

                // When
                IList<SearchAdCache> result = adRepo.SearchAds(orSearchStrings: new String[] { "tre" });

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

                SearchAdCache a = new SearchAdCache
                {
                    AdId = 1,
                    Title = "titre 1",
                    Body = "content",
                    City = c,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 18),
                    Category = cat
                };
                repo.Save<City>(c);
                repo.Save<Category>(cat);
                repo.Save(a);

                City c2 = new City
                {
                    Label = "CherzmOi2"
                };

                Category cat2 = new Category
                {
                    Label = "Auto"
                };

                SearchAdCache a2 = new SearchAdCache
                {
                    AdId = 2,
                    Title = "title 2",
                    Body = "content",
                    City = c2,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 17),
                    Category=cat2
                };
                repo.Save<City>(c2);
                repo.Save<Category>(cat2);
                repo.Save(a2);


                repo.Flush();

                #endregion

                // When
                IList<SearchAdCache> result = adRepo.SearchAds(orSearchStrings: new String[] { "ti" }, cityId: c2.Id);

                // Then
                Assert.AreEqual(1, result.Count);
                Assert.AreEqual(a2, result[0]);
            }
        }

        [TestMethod]
        public void SearchAds_SearchByTitleAndCategories()
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

                SearchAdCache a = new SearchAdCache
                {
                    AdId = 1,
                    Title = "titre 1",
                    Body = "content",
                    City = c,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 18),
                    Category = cat
                };
                repo.Save<City>(c);
                repo.Save<Category>(cat);
                repo.Save(a);

                City c2 = new City
                {
                    Label = "CherzmOi2"
                };

                Category cat2 = new Category
                {
                    Label = "Auto"
                };

                SearchAdCache a2 = new SearchAdCache
                {
                    AdId = 2,
                    Title = "title 2",
                    Body = "content",
                    City = c2,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 17),
                    Category = cat2
                };
                repo.Save<City>(c2);
                repo.Save<Category>(cat2);
                repo.Save(a2);


                repo.Flush();

                #endregion

                // When
                IList<SearchAdCache> result = adRepo.SearchAds(orSearchStrings: new String[] { "ti" }, categoryIds: new int[] { cat.Id, cat2.Id });

                // Then
                Assert.AreEqual(2, result.Count);
                Assert.AreEqual(a, result[0]);
                Assert.AreEqual(a2, result[1]);
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

                SearchAdCache a = new SearchAdCache
                {
                    AdId = 1,
                    Title = "titre 1",
                    Body = "content",
                    City = c,
                    Province = p1,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 18),
                    Category = cat
                };
                repo.Save<Province>(p1);
                repo.Save<City>(c);
                repo.Save<Category>(cat);
                repo.Save<SearchAdCache>(a);

                Province p2 = new Province
                {
                    Label = "p2"
                };
                City c2 = new City
                {
                    Label = "CherzmOi2"
                };
                p2.AddCity(c2);
                SearchAdCache a2 = new SearchAdCache
                {
                    AdId = 2,
                    Title = "title 2",
                    Body = "content",
                    City = c2,
                    Province = p2,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 17),
                    Category = cat
                };
                repo.Save<Province>(p2);
                repo.Save<City>(c2);
                repo.Save<SearchAdCache>(a2);

                repo.Flush();

                #endregion

                // When
                IList<SearchAdCache> result = adRepo.SearchAds(orSearchStrings: new String[] { "ti" }, provinceId: c2.Province.Id);

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

                SearchAdCache a = new SearchAdCache
                {
                    AdId = 1,
                    Title = "titre 1",
                    Body = "content",
                    City = c,
                    Province = p1,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 18),
                    Category = cat
                };
                repo.Save<Province>(p1);
                repo.Save<City>(c);
                repo.Save<Category>(cat);
                repo.Save<SearchAdCache>(a);

                Province p2 = new Province
                {
                    Label = "p2"
                };
                City c2 = new City
                {
                    Label = "CherzmOi2"
                };
                p2.AddCity(c2);
                SearchAdCache a2 = new SearchAdCache
                {
                    AdId = 2,
                    Title = "title 2",
                    Body = "content",
                    City = c2,
                    Province = p2,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 17),
                    Category = cat
                };
                repo.Save<Province>(p2);
                repo.Save<City>(c2);
                repo.Save<SearchAdCache>(a2);


                repo.Flush();

                #endregion

                // When
                IList<SearchAdCache> result = adRepo.SearchAds(null, new String[] { "ti" }, provinceId: c2.Province.Id, cityId: c.Id);

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

                SearchAdCache a = new SearchAdCache
                {
                    AdId = 1,
                    Title = "ship",
                    Body = "content",
                    City = c,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 18),
                    Category = cat
                };
                repo.Save<Province>(p1);
                repo.Save<City>(c);
                repo.Save<Category>(cat);
                repo.Save(a);

                Province p2 = new Province
                {
                    Label = "p2"
                };
                City c2 = new City
                {
                    Label = "CherzmOi2"
                };
                p2.AddCity(c2);

                SearchAdCache a2 = new SearchAdCache
                {
                    AdId = 2,
                    Title = "car",
                    Body = "content",
                    City = c,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 17),
                    Category = cat
                };
                repo.Save<Province>(p2);
                repo.Save<City>(c2);
                repo.Save<SearchAdCache>(a2);


                repo.Flush();

                #endregion

                // When
                IList<SearchAdCache> result = adRepo.SearchAds(null, new String[] { "car", "ship" }, null, null);

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

                SearchAdCache a = new SearchAdCache
                {
                    AdId = 1,
                    Title = "ship",
                    Body = "computer",
                    City = c,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 18),
                    Category = cat
                };
                repo.Save<Province>(p1);
                repo.Save<City>(c);
                repo.Save<Category>(cat);
                repo.Save(a);

                Province p2 = new Province
                {
                    Label = "p2"
                };
                City c2 = new City
                {
                    Label = "CherzmOi2"
                };
                p2.AddCity(c2);
                SearchAdCache a2 = new SearchAdCache
                {
                    AdId = 2,
                    Title = "ship",
                    Body = "content",
                    City = c,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 17),
                    Category = cat
                };
                repo.Save<Province>(p2);
                repo.Save<City>(c2);
                repo.Save(a2);


                repo.Flush();

                #endregion

                // When
                IList<SearchAdCache> result = adRepo.SearchAds(new String[] { "computer", "ship" }, null, null, null);

                // Then
                Assert.AreEqual(1, result.Count);
                Assert.AreEqual(a, result[0]);
            }
        }

        [TestMethod]
        public void GetAdById_GetAd_ReturnAdObject()
        {
            ISessionFactory sessionFactory = NhibernateHelper.SessionFactory;
            Repository repo = new Repository(sessionFactory);
            AdRepository adRepo = new AdRepository(sessionFactory);

            using (ITransaction transaction = sessionFactory.GetCurrentSession().BeginTransaction())
            {
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
                long id = repo.Save<Ad, long>(a);

                repo.Flush();

                #endregion

                Assert.AreEqual(a, adRepo.GetAdById<Ad>(id));
            }
        }

        [TestMethod]
        public void GetAdById_GetCarAd_ReturnCarAdObject()
        {
            ISessionFactory sessionFactory = NhibernateHelper.SessionFactory;
            Repository repo = new Repository(sessionFactory);
            AdRepository adRepo = new AdRepository(sessionFactory);

            using (ITransaction transaction = sessionFactory.GetCurrentSession().BeginTransaction())
            {
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

                CarAd a = new CarAd
                {
                    Title = "honda civic type R",
                    Body = "the best!!",
                    CreatedBy = u,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 18),
                    Category = cat,
                    Kilometers = 25000,
                    Year = 1998
                };
                c.AddAd(a);
                cat.AddAd(a);
                repo.Save<Province>(p1);
                repo.Save<City>(c);
                repo.Save<Category>(cat);
                long id = repo.Save<CarAd, long>(a);

                repo.Flush();

                #endregion

                Assert.IsNull(adRepo.GetAdById<Ad>(id));
                Assert.AreEqual(a, adRepo.GetAdById<CarAd>(id));
            }
        }

        [TestMethod]
        public void GetAdType_CarAdExists_ReturnType()
        {
            ISessionFactory sessionFactory = NhibernateHelper.SessionFactory;
            Repository repo = new Repository(sessionFactory);
            AdRepository adRepo = new AdRepository(sessionFactory);

            using (ITransaction transaction = sessionFactory.GetCurrentSession().BeginTransaction())
            {
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
                    Label = "Auto",
                    Type = AdTypeEnum.CarAd
                };

                CarAd a = new CarAd
                {
                    Title = "honda civic type R",
                    Body = "the best!!",
                    CreatedBy = u,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 18),
                    Category = cat,
                    Kilometers = 25000,
                    Year = 1998
                };
                c.AddAd(a);
                cat.AddAd(a);
                repo.Save<Province>(p1);
                repo.Save<City>(c);
                repo.Save<Category>(cat);
                long id = repo.Save<CarAd, long>(a);

                repo.Flush();

                #endregion

                Assert.AreEqual(AdTypeEnum.CarAd, adRepo.GetAdType(id));
            }
        }

        [TestMethod]
        public void GetAdType_ClassicAdExists_ReturnType()
        {
            ISessionFactory sessionFactory = NhibernateHelper.SessionFactory;
            Repository repo = new Repository(sessionFactory);
            AdRepository adRepo = new AdRepository(sessionFactory);

            using (ITransaction transaction = sessionFactory.GetCurrentSession().BeginTransaction())
            {
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
                    Label = "Informatique",
                    Type = AdTypeEnum.Ad
                };

                Ad a = new Ad
                {
                    Title = "video game",
                    Body = "the best!!",
                    CreatedBy = u,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 18),
                    Category = cat,
                };
                c.AddAd(a);
                cat.AddAd(a);
                repo.Save<Province>(p1);
                repo.Save<City>(c);
                repo.Save<Category>(cat);
                long id = repo.Save<Ad, long>(a);

                repo.Flush();

                #endregion

                Assert.AreEqual(AdTypeEnum.Ad, adRepo.GetAdType(id));
            }
        }

        [TestMethod]
        public void GetAdType_AdDoesNotExist_ReturnUndefined()
        {
            ISessionFactory sessionFactory = NhibernateHelper.SessionFactory;
            Repository repo = new Repository(sessionFactory);
            AdRepository adRepo = new AdRepository(sessionFactory);

            using (ITransaction transaction = sessionFactory.GetCurrentSession().BeginTransaction())
            {
                Assert.AreEqual(AdTypeEnum.Undefined, adRepo.GetAdType(18));
            }
        }

        [TestMethod]
        public void CanDeleteAd_AdDoesNotExists_ReturnFalse()
        {
            ISessionFactory sessionFactory = NhibernateHelper.SessionFactory;
            AdRepository adRepo = new AdRepository(sessionFactory);

            using (ITransaction transaction = sessionFactory.GetCurrentSession().BeginTransaction())
            {
                Assert.IsFalse(adRepo.CanDeleteAd(56));
            }
        }

        [TestMethod]
        public void CanDeleteAd_AdExistsAndAlreadyDeleted_ReturnFalse()
        {
            ISessionFactory sessionFactory = NhibernateHelper.SessionFactory;
            Repository repo = new Repository(sessionFactory);
            AdRepository adRepo = new AdRepository(sessionFactory);

            using (ITransaction transaction = sessionFactory.GetCurrentSession().BeginTransaction())
            {
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
                    Label = "Informatique",
                    Type = AdTypeEnum.Ad
                };

                Ad a = new Ad
                {
                    Title = "video game",
                    Body = "the best!!",
                    CreatedBy = u,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 18),
                    Category = cat,
                    IsDeleted = true
                };
                c.AddAd(a);
                cat.AddAd(a);
                repo.Save<Province>(p1);
                repo.Save<City>(c);
                repo.Save<Category>(cat);
                long id = repo.Save<Ad, long>(a);

                repo.Flush();

                #endregion

                Assert.IsFalse(adRepo.CanDeleteAd(id));
            }
        }

        [TestMethod]
        public void CanDeleteAd_AdExistsAndNotDeleted_ReturnTrue()
        {
            ISessionFactory sessionFactory = NhibernateHelper.SessionFactory;
            Repository repo = new Repository(sessionFactory);
            AdRepository adRepo = new AdRepository(sessionFactory);

            using (ITransaction transaction = sessionFactory.GetCurrentSession().BeginTransaction())
            {
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
                    Label = "Informatique",
                    Type = AdTypeEnum.Ad
                };

                Ad a = new Ad
                {
                    Title = "video game",
                    Body = "the best!!",
                    CreatedBy = u,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 18),
                    Category = cat,
                    IsDeleted = false
                };
                c.AddAd(a);
                cat.AddAd(a);
                repo.Save<Province>(p1);
                repo.Save<City>(c);
                repo.Save<Category>(cat);
                long id = repo.Save<Ad, long>(a);

                repo.Flush();

                #endregion

                Assert.IsTrue(adRepo.CanDeleteAd(id));
            }
        }
        [TestMethod]
        public void GetAdsByEmail_ReturnListOfAds_WithUserAndCategoryFetched()
        {
            ISessionFactory sessionFactory = NhibernateHelper.SessionFactory;
            AdRepository adRepo = new AdRepository(sessionFactory);
            Repository repo = new Repository(sessionFactory);
            using (ITransaction transaction = sessionFactory.GetCurrentSession().BeginTransaction())
            {
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

                User u2 = new User
                {
                    Email = "test2@test2.com",
                    Password = "hihi"
                };
                repo.Save<User>(u2);

                City c = new City
                {
                    Label = "city"
                };
                p1.AddCity(c);

                Category cat = new Category
                {
                    Label = "Informatique",
                    Type = AdTypeEnum.Ad
                };

                Category cat2 = new Category
                {
                    Label = "Voiture",
                    Type = AdTypeEnum.Ad
                };



                Ad a = new Ad
                {
                    Title = "video game",
                    Body = "the best!!",
                    CreatedBy = u,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 18),
                    Category = cat,
                    IsDeleted = false,
                    IsActivated = true
                };

                Ad a2 = new Ad
                {
                    Title = "Ferrari F430",
                    Body = "Valab'",
                    CreatedBy = u,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 18),
                    Category = cat2,
                    IsDeleted = false, 
                    IsActivated = true
                };

                Ad a3 = new Ad
                {
                    Title = "Ferrari F430",
                    Body = "Valab'",
                    CreatedBy = u2,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 18),
                    Category = cat2,
                    IsDeleted = false,
                    IsActivated = true
                };
                c.AddAd(a);
                c.AddAd(a2);
                c.AddAd(a3);
                cat.AddAd(a);
                cat2.AddAd(a2);
                cat2.AddAd(a3);
                repo.Save<Province>(p1);
                repo.Save<City>(c);
                repo.Save<Category>(cat);
                repo.Save<Category>(cat2);
                repo.Save<Ad, long>(a);
                repo.Save<Ad, long>(a2);
                repo.Save<Ad, long>(a3);
                repo.Flush();
                #endregion

                List<BaseAd> adsUser1 = adRepo.GetAdsByEmail(u.Email).ToList();
                List<BaseAd> adsUser2 = adRepo.GetAdsByEmail(u2.Email).ToList();
                Assert.AreEqual(2, adsUser1.Count);
                Assert.AreEqual(u.Email, adsUser1[0].CreatedBy.Email);
                Assert.AreEqual(1, adsUser2.Count);
                Assert.AreEqual(u2.Email, adsUser2[0].CreatedBy.Email);
            }
        }

        [TestMethod]
        public void GetAdsByEmail_UnknownEmail_ReturnEmptyListOfAds()
        {
            ISessionFactory sessionFactory = NhibernateHelper.SessionFactory;
            AdRepository adRepo = new AdRepository(sessionFactory);
            List<BaseAd> ads = adRepo.GetAdsByEmail("unknown Email").ToList();
            Assert.AreEqual(0, ads.Count);
        }
    }
}