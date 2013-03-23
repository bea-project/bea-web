using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Dal.Repository;
using Bea.Domain;
using Bea.Domain.Ads;
using Bea.Domain.Categories;
using Bea.Domain.Location;
using Bea.Domain.Reference;
using Bea.Domain.Search;
using Bea.Test.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;

namespace Bea.Test.Dal.repository
{
    [TestClass]
    public class SearchRepositoryTest : DataAccessTestBase
    {
        [TestMethod]
        public void SearchAds_SearchByTitleAndBodyOnly()
        {
            ISessionFactory sessionFactory = NhibernateHelper.SessionFactory;
            Repository repo = new Repository(sessionFactory);
            SearchRepository adRepo = new SearchRepository(sessionFactory);

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
                    Label = "CherzmOi",
                    LabelUrlPart = "city"
                };

                Category cat = new Category
                {
                    Label = "Moto",
                    LabelUrlPart = "Moto"
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
                IList<SearchAdCache> result = adRepo.SearchAds(andSearchStrings: new String[] { "tre" });

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
            SearchRepository adRepo = new SearchRepository(sessionFactory);

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
                    Label = "CherzmOi",
                    LabelUrlPart = "city",
                };

                Category cat = new Category
                {
                    Label = "Moto",
                    LabelUrlPart = "Moto",
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
                    Label = "CherzmOi2",
                    LabelUrlPart = "city2",
                };

                Category cat2 = new Category
                {
                    Label = "Auto",
                    LabelUrlPart = "Auto",
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
                IList<SearchAdCache> result = adRepo.SearchAds(andSearchStrings: new String[] { "ti" }, cityId: c2.Id);

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
            SearchRepository adRepo = new SearchRepository(sessionFactory);

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
                    Label = "CherzmOi",
                    LabelUrlPart = "city"
                };

                Category cat = new Category
                {
                    Label = "Moto",
                    LabelUrlPart = "Moto"
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
                    Label = "CherzmOi2",
                    LabelUrlPart = "city2"
                };

                Category cat2 = new Category
                {
                    Label = "Auto",
                    LabelUrlPart = "Auto"
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
                IList<SearchAdCache> result = adRepo.SearchAds(andSearchStrings: new String[] { "ti" }, categoryIds: new int[] { cat.Id, cat2.Id });

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
            SearchRepository adRepo = new SearchRepository(sessionFactory);

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
                    Label = "city",
                    LabelUrlPart = "city"
                };
                p1.AddCity(c);

                Category cat = new Category
                {
                    Label = "Moto",
                    LabelUrlPart = "Moto"
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
                    Label = "CherzmOi2",
                    LabelUrlPart = "city2"
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
                IList<SearchAdCache> result = adRepo.SearchAds(new String[] { "computer", "ship" }, null, null);

                // Then
                Assert.AreEqual(1, result.Count);
                Assert.AreEqual(a, result[0]);
            }
        }

        [TestMethod]
        public void SearchCarAds_TitleOnly_ReturnAd()
        {
            ISessionFactory sessionFactory = NhibernateHelper.SessionFactory;
            Repository repo = new Repository(sessionFactory);
            SearchRepository adRepo = new SearchRepository(sessionFactory);

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
                    Label = "city",
                    LabelUrlPart = "city"
                };
                p1.AddCity(c);

                Category cat = new Category
                {
                    Label = "Auto",
                    LabelUrlPart = "Auto"
                };

                SearchAdCache a = new SearchAdCache
                {
                    AdId = 1,
                    Title = "aveo",
                    Body = "aveo sport 1.2 16s",
                    City = c,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 18),
                    Category = cat
                };

                CarAd car = new CarAd
                {
                    Id = 1,
                    Title = "aveo",
                    Body = "aveo sport 1.2 16s",
                    City = c,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 18),
                    Category = cat,
                    CreatedBy = u
                };

                repo.Save<Province>(p1);
                repo.Save<City>(c);
                repo.Save<Category>(cat);
                repo.Save<User>(u);
                repo.Save<CarAd>(car);
                repo.Save(a);

                Category cat2 = new Category
                {
                    Label = "Moto",
                    LabelUrlPart = "Moto"
                };

                SearchAdCache a2 = new SearchAdCache
                {
                    Title = "aveo",
                    Body = "aveo sport 1.2 16s",
                    City = c,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 18),
                    Category = cat2
                };

                MotoAd moto = new MotoAd
                {
                    Id = 1,
                    Title = "aveo",
                    Body = "aveo sport 1.2 16s",
                    City = c,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 18),
                    Category = cat2,
                    CreatedBy = u
                };
                repo.Save(cat2);
                repo.Save(moto);
                repo.Save(a2);

                repo.Flush();

                #endregion

                // When
                IList<SearchAdCache> result = adRepo.SearchVehicleAds<CarAd>(new String[] { "aveo" }, null, null, null, null, null, null, null, null, null, null, null);

                // Then
                Assert.AreEqual(1, result.Count);
                Assert.AreEqual(a, result[0]);
            }
        }

        [TestMethod]
        public void SearchCarAds_CarProperties_ReturnCarAd()
        {
            ISessionFactory sessionFactory = NhibernateHelper.SessionFactory;
            Repository repo = new Repository(sessionFactory);
            SearchRepository adRepo = new SearchRepository(sessionFactory);

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
                    Label = "city",
                    LabelUrlPart = "city"
                };
                p1.AddCity(c);

                Category cat = new Category
                {
                    Label = "Auto",
                    LabelUrlPart = "Auto"
                };

                CarFuel fuel = new CarFuel
                {
                    Label = "Diesel"
                };

                VehicleBrand brand = new VehicleBrand
                {
                    Label = "Aveo"
                };

                SearchAdCache a = new SearchAdCache
                {
                    AdId = 1,
                    Title = "aveo",
                    Body = "aveo sport 1.2 16s",
                    City = c,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 18),
                    Category = cat
                };

                CarAd car = new CarAd
                {
                    Id = 1,
                    Title = "aveo",
                    Body = "aveo sport 1.2 16s",
                    City = c,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 18),
                    Category = cat,
                    Year = 2011,
                    Kilometers = 10000,
                    IsAutomatic = true,
                    Fuel = fuel,
                    Brand = brand,
                    CreatedBy = u
                };

                repo.Save(brand);
                repo.Save(fuel);
                repo.Save(p1);
                repo.Save(c);
                repo.Save(cat);
                repo.Save(u);
                repo.Save(car);
                repo.Save(a);

                SearchAdCache a2 = new SearchAdCache
                {
                    Title = "aveo",
                    Body = "aveo sport 1.2 16s",
                    City = c,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 18),
                    Category = cat
                };

                CarAd car2 = new CarAd
                {
                    Id = 1,
                    Title = "aveo",
                    Body = "aveo sport 1.2 16s",
                    City = c,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 18),
                    Category = cat,
                    Year = 2001,
                    Kilometers = 95000,
                    Brand = brand,
                    CreatedBy = u
                };
                repo.Save(car2);
                repo.Save(a2);

                repo.Flush();

                #endregion

                // When
                IList<SearchAdCache> result = adRepo.SearchVehicleAds<CarAd>(new String[] { "aveo" }, null, null, 0, 11000, 2000, 2012, brand.Id, fuel.Id, true, null, null);

                // Then
                Assert.AreEqual(1, result.Count);
                Assert.AreEqual(a, result[0]);
            }
        }

        [TestMethod]
        public void SearchCarAds_MotoProperties_ReturnMotoAd()
        {
            ISessionFactory sessionFactory = NhibernateHelper.SessionFactory;
            Repository repo = new Repository(sessionFactory);
            SearchRepository adRepo = new SearchRepository(sessionFactory);

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
                    Label = "city",
                    LabelUrlPart = "city"
                };
                p1.AddCity(c);

                Category cat = new Category
                {
                    Label = "Auto",
                    LabelUrlPart = "Auto"
                };

                SearchAdCache a = new SearchAdCache
                {
                    AdId = 1,
                    Title = "aveo",
                    Body = "aveo sport 1.2 16s",
                    City = c,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 18),
                    Category = cat
                };

                CarAd car = new CarAd
                {
                    Id = 1,
                    Title = "aveo",
                    Body = "aveo sport 1.2 16s",
                    City = c,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 18),
                    Category = cat,
                    CreatedBy = u
                };

                repo.Save(p1);
                repo.Save(c);
                repo.Save(cat);
                repo.Save(u);
                repo.Save(car);
                repo.Save(a);

                Category cat2 = new Category
                {
                    Label = "Moto",
                    LabelUrlPart = "Moto"
                };

                MotoBrand brand = new MotoBrand
                {
                    Label = "Suzuki"
                };

                SearchAdCache a2 = new SearchAdCache
                {
                    AdId = 2,
                    Title = "aveo",
                    Body = "aveo sport 1.2 16s",
                    City = c,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 18),
                    Category = cat2
                };

                MotoAd moto = new MotoAd
                {
                    Id = 2,
                    Title = "aveo",
                    Body = "aveo sport 1.2 16s",
                    City = c,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 18),
                    Category = cat2,
                    Brand = brand,
                    EngineSize = 650,
                    CreatedBy = u
                };
                repo.Save(brand);
                repo.Save(cat2);
                repo.Save(moto);
                repo.Save(a2);

                repo.Flush();

                #endregion

                // When
                IList<SearchAdCache> result = adRepo.SearchVehicleAds<MotoAd>(new String[] { "aveo" }, null, null, null, null, null, null, brand.Id, null, null, 250, 800);

                // Then
                Assert.AreEqual(1, result.Count);
                Assert.AreEqual(a2, result[0]);
            }
        }
    }
}
