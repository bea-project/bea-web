using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Core.Dal;
using Bea.Dal.Repository;
using Bea.Domain;
using Bea.Domain.Ads;
using Bea.Domain.Ads.WaterSport;
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
        public void AdvancedSearchAds_CarAds_TitleOnly_ReturnAd()
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

                AdSearchParameters param = new AdSearchParameters
                {
                    AndSearchStrings = new String[] { "aveo" }
                };

                // When
                IList<SearchAdCache> result = adRepo.AdvancedSearchAds<CarAd>(param);

                // Then
                Assert.AreEqual(1, result.Count);
                Assert.AreEqual(a, result[0]);
            }
        }

        [TestMethod]
        public void AdvancedSearchAds_CarAds_CarProperties_ReturnCarAd()
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

                AdSearchParameters param = new AdSearchParameters
                {
                    AndSearchStrings = new String[] { "aveo" },
                    MinKm = 0,
                    MaxKm = 11000,
                    MinYear = 2000,
                    MaxYear = 2012,
                    BrandId = brand.Id,
                    FueldId = fuel.Id,
                    IsAuto = true
                };

                // When
                IList<SearchAdCache> result = adRepo.AdvancedSearchAds<CarAd>(param);

                // Then
                Assert.AreEqual(1, result.Count);
                Assert.AreEqual(a, result[0]);
            }
        }

        [TestMethod]
        public void AdvancedSearchAds_MotoAds_MotoProperties_ReturnMotoAd()
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

                AdSearchParameters param = new AdSearchParameters
                {
                    AndSearchStrings = new String[] { "aveo" },
                    BrandId = brand.Id,
                    MinEngineSize = 250,
                    MaxEngineSize = 800
                };

                // When
                IList<SearchAdCache> result = adRepo.AdvancedSearchAds<MotoAd>(param);

                // Then
                Assert.AreEqual(1, result.Count);
                Assert.AreEqual(a2, result[0]);
            }
        }

        [TestMethod]
        public void AdvancedSearchAds_RealEstateAds_RealEstateProperties_ReturnRealEstateAd()
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
                    Label = "Location",
                    LabelUrlPart = "Location"
                };

                SearchAdCache a = new SearchAdCache
                {
                    AdId = 1,
                    Title = "appart",
                    Body = "boite a chaussure",
                    City = c,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 18),
                    Category = cat
                };

                RealEstateType t1 = new RealEstateType
                {
                    Label = "Location"
                };

                District d = new District
                {
                    City = c,
                    Label = "Cheznous"
                };

                RealEstateAd loc = new RealEstateAd
                {
                    Id = 1,
                    Title = "appart",
                    Body = "boite a chaussure",
                    City = c,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 18),
                    Category = cat,
                    CreatedBy = u,
                    Type = t1,
                    District = d,
                    RoomsNumber = 5,
                    IsFurnished = true,
                    SurfaceArea = 45
                };

                repo.Save(t1);
                repo.Save(d);
                repo.Save(p1);
                repo.Save(c);
                repo.Save(cat);
                repo.Save(u);
                repo.Save(loc);
                repo.Save(a);

                MotoBrand brand = new MotoBrand
                {
                    Label = "Suzuki"
                };

                SearchAdCache a2 = new SearchAdCache
                {
                    AdId = 2,
                    Title = "appart2",
                    Body = "boite a chaussure",
                    City = c,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 18),
                    Category = cat
                };

                RealEstateAd loc2 = new RealEstateAd
                {
                    Id = 2,
                    Title = "appart2",
                    Body = "boite a chaussure",
                    City = c,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 18),
                    Category = cat,
                    CreatedBy = u,
                    Type = t1,
                    District = d,
                    RoomsNumber = 4,
                    IsFurnished = true,
                    SurfaceArea = 65
                };
                repo.Save(loc2);
                repo.Save(a2);

                repo.Flush();

                #endregion

                AdSearchParameters param = new AdSearchParameters
                {
                    AndSearchStrings = new String[] { "appart" },
                    MinNbRooms = 2,
                    MaxNbRooms = 4,
                    DistrictId = 1,
                    RealEstateTypeId = 1,
                    IsFurnished = true,
                    MinSurfaceArea = 60,
                    MaxSurfaceArea = 65
                };

                // When
                IList<SearchAdCache> result = adRepo.AdvancedSearchAds<RealEstateAd>(param);

                // Then
                Assert.AreEqual(1, result.Count);
                Assert.AreEqual(a2, result[0]);
            }
        }

        [TestMethod]
        public void AdvancedSearchAds_Ad_MinMaxPrice_ReturnMatchingAds()
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
                    Label = "Location",
                    LabelUrlPart = "Location"
                };

                SearchAdCache a = new SearchAdCache
                {
                    AdId = 1,
                    Title = "chaussure",
                    Body = "boite a chaussure",
                    City = c,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 18),
                    Category = cat
                };

                Ad loc = new Ad
                {
                    Id = 1,
                    Title = "chaussure",
                    Body = "boite a chaussure",
                    City = c,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 18),
                    Category = cat,
                    CreatedBy = u,
                    Price = 1000
                };

                repo.Save(p1);
                repo.Save(c);
                repo.Save(cat);
                repo.Save(u);
                repo.Save(loc);
                repo.Save(a);

                SearchAdCache a2 = new SearchAdCache
                {
                    AdId = 2,
                    Title = "chaussure",
                    Body = "boite a chaussure",
                    City = c,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 18),
                    Category = cat
                };

                Ad loc2 = new Ad
                {
                    Id = 2,
                    Title = "chaussure",
                    Body = "boite a chaussure",
                    City = c,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 18),
                    Category = cat,
                    CreatedBy = u,
                    Price = 2000
                };
                repo.Save(loc2);
                repo.Save(a2);

                repo.Flush();

                #endregion

                AdSearchParameters param = new AdSearchParameters
                {
                    MinPrice = 0,
                    MaxPrice = 1000
                };

                // When
                IList<SearchAdCache> result = adRepo.AdvancedSearchAds<Ad>(param);

                // Then
                Assert.AreEqual(1, result.Count);
                Assert.AreEqual(a, result[0]);
            }
        }

        [TestMethod]
        public void AdvancedSearchAds_MotorBoatAd_MotorBoatProperties_ReturnMotorBoatAd()
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
                    Label = "Bateau à moteur",
                    LabelUrlPart = "Bateau"
                };

                SearchAdCache a = new SearchAdCache
                {
                    AdId = 1,
                    Title = "bateau",
                    Body = "la desc du bateau",
                    City = c,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 18),
                    Category = cat
                };

                MotorBoatType mbt = new MotorBoatType()
                {
                    Id = 5,
                    Label = "Bois"
                };

                MotorBoatEngineType mt = new MotorBoatEngineType()
                {
                    Id = 7,
                    Label = "4 temps"
                };

                MotorBoatAd bat = new MotorBoatAd
                {
                    Id = 1,
                    Title = "bateau",
                    Body = "la desc du bateau",
                    City = c,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 18),
                    Category = cat,
                    CreatedBy = u,
                    Price = 1000,
                    Hp = 100,
                    Length = 6,
                    MotorBoatType = mbt,
                    MotorType = mt,
                    Year = 2005
                };

                repo.Save(mbt);
                repo.Save(mt);
                repo.Save(p1);
                repo.Save(c);
                repo.Save(cat);
                repo.Save(u);
                repo.Save(bat);
                repo.Save(a);

                repo.Flush();

                #endregion

                AdSearchParameters param = new AdSearchParameters
                {
                    MotorBoatTypeId = 1,
                    MotorEngineTypeId = 1,
                    MinHp = 99,
                    MaxHp = 100,
                    MinLength = 6,
                    MaxLength = 10,
                    MinYear = 2004,
                    MaxYear = 2006
                };

                // When
                IList<SearchAdCache> result = adRepo.AdvancedSearchAds<MotorBoatAd>(param);

                // Then
                Assert.AreEqual(1, result.Count);
                Assert.AreEqual(a, result[0]);
            }
        }

        [TestMethod]
        public void AdvancedSearchAds_MotorBoatEngineAd_MotorBoatEngineProperties_ReturnMotorBoatEngineAd()
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
                    Label = "Bateau à moteur",
                    LabelUrlPart = "Bateau"
                };

                SearchAdCache a = new SearchAdCache
                {
                    AdId = 1,
                    Title = "bateau",
                    Body = "la desc du bateau",
                    City = c,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 18),
                    Category = cat
                };

                MotorBoatEngineType mt = new MotorBoatEngineType()
                {
                    Id = 7,
                    Label = "4 temps"
                };

                MotorBoatEngineAd bat = new MotorBoatEngineAd
                {
                    Id = 1,
                    Title = "moteur",
                    Body = "la desc du bateau",
                    City = c,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 18),
                    Category = cat,
                    CreatedBy = u,
                    Price = 1000,
                    Hp = 100,
                    Year = 2005,
                    MotorType = mt
                };

                repo.Save(mt);
                repo.Save(p1);
                repo.Save(c);
                repo.Save(cat);
                repo.Save(u);
                repo.Save(bat);
                repo.Save(a);

                repo.Flush();

                #endregion

                AdSearchParameters param = new AdSearchParameters
                {
                    MotorEngineTypeId = 1,
                    MinHp = 99,
                    MaxHp = 100,
                    MinYear = 2004,
                    MaxYear = 2006
                };

                // When
                IList<SearchAdCache> result = adRepo.AdvancedSearchAds<MotorBoatEngineAd>(param);

                // Then
                Assert.AreEqual(1, result.Count);
                Assert.AreEqual(a, result[0]);
            }
        }

        [TestMethod]
        public void AdvancedSearchAds_SailingBoatAd_SailingBoatProperties_ReturnSailingBoatAd()
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
                    Label = "Bateau à moteur",
                    LabelUrlPart = "Bateau"
                };

                SearchAdCache a = new SearchAdCache
                {
                    AdId = 1,
                    Title = "bateau",
                    Body = "la desc du bateau",
                    City = c,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 18),
                    Category = cat
                };

                SailingBoatHullType ht = new SailingBoatHullType()
                {
                    Label = "Bois"
                };

                SailingBoatType st = new SailingBoatType()
                {
                    Label = "Cata"
                };

                SailingBoatAd bat = new SailingBoatAd
                {
                    Id = 1,
                    Title = "bateau",
                    Body = "la desc du bateau",
                    City = c,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 18),
                    Category = cat,
                    CreatedBy = u,
                    Length = 12,
                    Year = 2005,
                    HullType = ht,
                    SailingBoatType = st
                };

                repo.Save(ht);
                repo.Save(st);
                repo.Save(p1);
                repo.Save(c);
                repo.Save(cat);
                repo.Save(u);
                repo.Save(bat);
                repo.Save(a);

                repo.Flush();

                #endregion

                AdSearchParameters param = new AdSearchParameters
                {
                    SailingBoatTypeId = 1,
                    HullTypeId = 1,
                    MinLength = 10,
                    MaxLength = 12,
                    MinYear = 2004,
                    MaxYear = 2006
                };

                // When
                IList<SearchAdCache> result = adRepo.AdvancedSearchAds<SailingBoatAd>(param);

                // Then
                Assert.AreEqual(1, result.Count);
                Assert.AreEqual(a, result[0]);
            }
        }

        [TestMethod]
        public void AdvancedSearchAds_WaterSportAd_WaterSportProperties_ReturnWaterSportAd()
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
                    Label = "Bateau à moteur",
                    LabelUrlPart = "Bateau"
                };

                SearchAdCache a = new SearchAdCache
                {
                    AdId = 1,
                    Title = "bateau",
                    Body = "la desc du bateau",
                    City = c,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 18),
                    Category = cat
                };

                WaterSportType st = new WaterSportType()
                {
                    Label = "pmt"
                };

                WaterSportAd bat = new WaterSportAd
                {
                    Id = 1,
                    Title = "bateau",
                    Body = "la desc du bateau",
                    City = c,
                    CreationDate = new DateTime(2012, 01, 16, 23, 52, 18),
                    Category = cat,
                    CreatedBy = u,
                    Type = st
                };

                repo.Save(st);
                repo.Save(p1);
                repo.Save(c);
                repo.Save(cat);
                repo.Save(u);
                repo.Save(bat);
                repo.Save(a);

                repo.Flush();

                #endregion

                AdSearchParameters param = new AdSearchParameters
                {
                    WaterTypeId = 1
                };

                // When
                IList<SearchAdCache> result = adRepo.AdvancedSearchAds<WaterSportAd>(param);

                // Then
                Assert.AreEqual(1, result.Count);
                Assert.AreEqual(a, result[0]);
            }
        }
    }
}
