﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Core.Dal;
using Bea.Domain;
using Bea.Domain.Ads;
using Bea.Domain.Categories;
using Bea.Domain.Location;
using Bea.Domain.Search;
using Bea.Models;
using Bea.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bea.Models.Search;
using Moq;
using Bea.Tools;
using Bea.Core.Services;
using Bea.Domain.Reference;
using Bea.Domain.Ads.WaterSport;

namespace Bea.Test.services
{
    [TestClass]
    public class SearchServicesTest
    {
        [TestMethod]
        public void SearchAds_2Ads_CallAdRepoAndBuildModels()
        {
            // Given
            IList<SearchAdCache> searchResult = new List<SearchAdCache>();
            searchResult.Add(new SearchAdCache
            {
                Title = "the ad title",
                City = new City() { Label = "the city", LabelUrlPart = "city" },
                Category = new Bea.Domain.Categories.Category()
            });
            searchResult.Add(new SearchAdCache
            {
                Title = "the ad title 2",
                City = new City() { Label = "the city", LabelUrlPart = "city" },
                Category = new Bea.Domain.Categories.Category()
            });

            var adRepoMock = new Moq.Mock<ISearchRepository>();
            adRepoMock.Setup(r => r.SearchAds(new string[] { "title" }, 98, null)).Returns(searchResult);

            AdvancedAdSearchModel model = new AdvancedAdSearchModel()
            {
                SearchString = "title",
                CitySelectedId = 98
            };

            SearchServices service = new SearchServices(null, null, adRepoMock.Object, null, null, null);

            // When
            AdSearchResultModel result = service.LightSearchAds(model);

            // Then
            Assert.AreEqual("title", result.SearchString);
            Assert.AreEqual(98, result.CitySelectedId);
            Assert.AreEqual(2, result.SearchResult.Count);
            Assert.AreEqual(2, result.SearchResultTotalCount);
            Assert.AreEqual("the ad title", result.SearchResult[0].Title);
            Assert.AreEqual("the ad title 2", result.SearchResult[1].Title);

            adRepoMock.VerifyAll();
        }

        [TestMethod]
        public void SearchAds_2SearchString_CallAdRepoWithSeveralWords()
        {
            // Given
            IList<SearchAdCache> searchResult = new List<SearchAdCache>();
            searchResult.Add(new SearchAdCache
            {
                Title = "ship",
                City = new City() { Label = "the city" },
                Category = new Bea.Domain.Categories.Category()
            });
            searchResult.Add(new SearchAdCache
            {
                Title = "computer",
                City = new City() { Label = "the city" },
                Category = new Bea.Domain.Categories.Category()
            });

            var adRepoMock = new Moq.Mock<ISearchRepository>();
            adRepoMock.Setup(r => r.SearchAds(new string[] { "ship", "computer" }, 98, null)).Returns(searchResult);

            AdvancedAdSearchModel model = new AdvancedAdSearchModel()
            {
                SearchString = "ship computer",
                CitySelectedId = 98
            };

            SearchServices service = new SearchServices(null, null, adRepoMock.Object, null, null, null);

            // When
            AdSearchResultModel result = service.LightSearchAds(model);

            // Then
            Assert.AreEqual("ship computer", result.SearchString);
            Assert.AreEqual(98, result.CitySelectedId);
            Assert.AreEqual(2, result.SearchResult.Count);
            Assert.AreEqual(2, result.SearchResultTotalCount);
            Assert.AreEqual("ship", result.SearchResult[0].Title);
            Assert.AreEqual("computer", result.SearchResult[1].Title);

            adRepoMock.VerifyAll();
        }

        [TestMethod]
        public void SearchAds_CategoryIsSelected_CallAdRepoWithOneCategoryId()
        {
            // Given
            Category cat = new Category { Id = 12 };

            IList<SearchAdCache> searchResult = new List<SearchAdCache>();
            searchResult.Add(new SearchAdCache
            {
                Title = "ship",
                City = new City() { Label = "the city" },
                Category = cat
            });

            var adRepoMock = new Moq.Mock<ISearchRepository>();
            adRepoMock.Setup(r => r.SearchAds(new string[] { "ship" }, null, new int[] { 12 })).Returns(searchResult);

            var repoMock = new Moq.Mock<IRepository>();
            repoMock.Setup(r => r.Get<Category>(12)).Returns(cat);

            AdvancedAdSearchModel model = new AdvancedAdSearchModel()
            {
                SearchString = "ship",
                CategorySelectedId = 12
            };

            SearchServices service = new SearchServices(repoMock.Object, null, adRepoMock.Object, null, null, null);

            // When
            AdSearchResultModel result = service.LightSearchAds(model);

            // Then
            Assert.AreEqual("ship", result.SearchString);
            Assert.IsNull(result.CitySelectedId);
            Assert.AreEqual(12, result.CategorySelectedId);
            Assert.AreEqual(1, result.SearchResult.Count);
            Assert.AreEqual(1, result.SearchResultTotalCount);
            Assert.AreEqual("ship", result.SearchResult[0].Title);

            adRepoMock.VerifyAll();
        }

        [TestMethod]
        public void SearchAds_CategoryGroupIsSelected_CallAdRepoWithSubCategoriesIds()
        {
            // Given
            IList<SearchAdCache> searchResult = new List<SearchAdCache>();
            searchResult.Add(new SearchAdCache
            {
                Title = "ship",
                City = new City() { Label = "the city" },
                Category = new Bea.Domain.Categories.Category()
            });

            Category group = new Category();
            group.AddCategory(new Category { Id = 12 });
            group.AddCategory(new Category { Id = 17 });

            var adRepoMock = new Moq.Mock<ISearchRepository>();
            adRepoMock.Setup(r => r.SearchAds(new string[] { "ship" }, null, new int[] { 12, 17 })).Returns(searchResult);

            var repoMock = new Moq.Mock<IRepository>();
            repoMock.Setup(r => r.Get<Category>(12)).Returns(group);


            AdvancedAdSearchModel model = new AdvancedAdSearchModel()
            {
                SearchString = "ship",
                CategorySelectedId = 12
            };

            SearchServices service = new SearchServices(repoMock.Object, null, adRepoMock.Object, null, null, null);

            // When
            AdSearchResultModel result = service.LightSearchAds(model);

            // Then
            Assert.AreEqual("ship", result.SearchString);
            Assert.IsNull(result.CitySelectedId);
            Assert.AreEqual(12, result.CategorySelectedId);
            Assert.AreEqual(1, result.SearchResult.Count);
            Assert.AreEqual(1, result.SearchResultTotalCount);
            Assert.AreEqual("ship", result.SearchResult[0].Title);

            adRepoMock.VerifyAll();
        }

        [TestMethod]
        public void SearchAdsFromUrl_CategoryIsSelected_RunSearchWithCategory()
        {
            // Given
            Category cat = new Category { Id = 12, LabelUrlPart = "cat-url-label", Label = "Label" };

            IList<SearchAdCache> searchResult = new List<SearchAdCache>();
            searchResult.Add(new SearchAdCache
            {
                Title = "ship",
                City = new City() { Label = "the city" },
                Category = cat
            });

            var adRepoMock = new Moq.Mock<ISearchRepository>();
            adRepoMock.Setup(r => r.SearchAds(null, null, It.Is<int[]>(x => x[0] == cat.Id))).Returns(searchResult);

            var repoMock = new Moq.Mock<IRepository>();
            repoMock.Setup(r => r.Get<Category>(cat.Id)).Returns(cat);

            var catRepoMock = new Moq.Mock<ICategoryRepository>();
            catRepoMock.Setup(r => r.GetCategoryFromUrlPart("cat-url-label")).Returns(cat);

            SearchServices service = new SearchServices(repoMock.Object, catRepoMock.Object, adRepoMock.Object, null, null, null);

            // When
            AdSearchResultModel result = service.SearchAdsFromUrl(null, "cat-url-label");

            // Then
            Assert.IsNull(result.SearchString);
            Assert.IsNull(result.CitySelectedId);
            Assert.AreEqual(12, result.CategorySelectedId);
            Assert.AreEqual("Label", result.CategorySelectedLabel);
            Assert.AreEqual(1, result.SearchResult.Count);
            Assert.AreEqual(1, result.SearchResultTotalCount);
            Assert.AreEqual("ship", result.SearchResult[0].Title);

            adRepoMock.VerifyAll();
        }

        [TestMethod]
        public void SearchAdsFromUrl_CategoryIsSelected_ReturnParentCategoryLabelUrlPartAsImagePath()
        {
            // Given
            Category cat = new Category { Id = 12, LabelUrlPart = "cat-url-label", Label = "Label", ImageName = "image" };

            var adRepoMock = new Moq.Mock<ISearchRepository>();
            adRepoMock.Setup(r => r.SearchAds(null, null, It.Is<int[]>(x => x[0] == cat.Id))).Returns(new List<SearchAdCache>());

            var repoMock = new Moq.Mock<IRepository>();
            repoMock.Setup(r => r.Get<Category>(cat.Id)).Returns(cat);

            var catRepoMock = new Moq.Mock<ICategoryRepository>();
            catRepoMock.Setup(r => r.GetCategoryFromUrlPart("cat-url-label")).Returns(cat);

            SearchServices service = new SearchServices(repoMock.Object, catRepoMock.Object, adRepoMock.Object, null, null, null);

            // When
            AdSearchResultModel result = service.SearchAdsFromUrl(null, "cat-url-label");

            // Then
            Assert.IsNull(result.SearchString);
            Assert.IsNull(result.CitySelectedId);
            Assert.AreEqual(12, result.CategorySelectedId);
            Assert.AreEqual("image", result.CategoryImagePath);
            Assert.AreEqual("Label", result.CategorySelectedLabel);

            adRepoMock.VerifyAll();
        }

        [TestMethod]
        public void SearchAdsFromUrl_CityIsSelected_RunSearchWithCity()
        {
            // Given
            City city = new City { Id = 12, LabelUrlPart = "city-url-label", Label = "Label" };

            IList<SearchAdCache> searchResult = new List<SearchAdCache>();
            searchResult.Add(new SearchAdCache
            {
                Title = "ship",
                City = city,
                Category = new Category { Label = "pouet" }
            });

            var adRepoMock = new Moq.Mock<ISearchRepository>();
            adRepoMock.Setup(r => r.SearchAds(null, city.Id, null)).Returns(searchResult);

            var catLocationServiceMock = new Moq.Mock<ILocationServices>();
            catLocationServiceMock.Setup(r => r.GetCityFromLabelUrlPart("city-url-label")).Returns(city);

            SearchServices service = new SearchServices(null, null, adRepoMock.Object, null, null, catLocationServiceMock.Object);

            // When
            AdSearchResultModel result = service.SearchAdsFromUrl("city-url-label", null);

            // Then
            Assert.IsNull(result.SearchString);
            Assert.AreEqual(12, result.CitySelectedId);
            Assert.AreEqual(1, result.SearchResult.Count);
            Assert.AreEqual(1, result.SearchResultTotalCount);
            Assert.AreEqual("ship", result.SearchResult[0].Title);

            adRepoMock.VerifyAll();
        }

        [TestMethod]
        public void AdvancedSearchAds_SearchThroughAds_CallSearchRepoOnAds()
        {
            // Given
            Category cat = new Category { Id = 1, LabelUrlPart = "cat-url-label", Label = "Label", Type = AdTypeEnum.Ad };

            AdSearchModel model = new AdSearchModel();
            model.CategorySelectedId = 1;
            model.SearchString = "toto";
            model.CitySelectedId = 12;

            IList<SearchAdCache> searchResult = new List<SearchAdCache>();
            searchResult.Add(new SearchAdCache
            {
                Title = "ship",
                City = new City() { Label = "the city" },
                Category = cat
            });

            var repoMock = new Moq.Mock<IRepository>();
            repoMock.Setup(r => r.Get<Category>(cat.Id)).Returns(cat);

            var searchRepoMock = new Moq.Mock<ISearchRepository>();
            searchRepoMock.Setup(r => r.SearchAds(It.Is<String[]>(x => x[0] == "toto"), 12, It.Is<int[]>(x => x[0] == 1))).Returns(searchResult);

            SearchServices service = new SearchServices(repoMock.Object, null, searchRepoMock.Object, null, null, null);

            // When
            AdSearchResultModel result = service.LightSearchAds(model);

            // Then
            Assert.AreEqual(1, result.SearchResultTotalCount);

        }

        [TestMethod]
        public void AdvancedSearchAds_SearchThroughCarAds_CallSearchRepoOnCarAds()
        {
            // Given
            Category cat = new Category { Id = 1, LabelUrlPart = "cat-url-label", Label = "Label", Type = AdTypeEnum.CarAd };

            AdvancedAdSearchModel model = new AdvancedAdSearchModel();
            model.CategorySelectedId = 1;
            model.SearchString = "toto";
            model.CitySelectedId = 12;
            model.AgeBracketSelectedId = 1;
            model.KmBracketSelectedId = 1;
            model.BrandSelectedId = 19;
            model.FuelSelectedId = 89;
            model.IsAutomatic = true;

            IList<SearchAdCache> searchResult = new List<SearchAdCache>();
            searchResult.Add(new SearchAdCache
            {
                Title = "car",
                City = new City() { Label = "the city" },
                Category = cat
            });

            var repoMock = new Moq.Mock<IRepository>();
            repoMock.Setup(r => r.Get<Category>(cat.Id)).Returns(cat);

            var helperMock = new Moq.Mock<IHelperService>();
            helperMock.Setup(r => r.GetCurrentDateTime()).Returns(new DateTime(2013, 01, 01));

            IDictionary<int, BracketItemReference> ageRef = new Dictionary<int, BracketItemReference>();
            ageRef.Add(1, new BracketItemReference { LowValue = 0, HighValue = 3 });
            IDictionary<int, BracketItemReference> kmRef = new Dictionary<int, BracketItemReference>();
            kmRef.Add(1, new BracketItemReference { LowValue = 50, HighValue = 100 });

            var refMock = new Moq.Mock<IReferenceServices>();
            refMock.Setup(s => s.GetAllAgeBrackets()).Returns(ageRef);
            refMock.Setup(s => s.GetAllKmBrackets()).Returns(kmRef);

            var searchRepoMock = new Moq.Mock<ISearchRepository>();
            searchRepoMock.Setup(r => r.AdvancedSearchAds<CarAd>(
                It.Is<AdSearchParameters>(p =>
                    p.AndSearchStrings[0].Equals("toto")
                    && p.CityId == 12
                    && p.CategoryIds[0] == 1
                    && p.MinKm == 50
                    && p.MaxKm == 100
                    && p.MinYear == 2010
                    && p.MaxYear == 2013
                    && p.BrandId == 19
                    && p.FueldId == 89
                    && p.IsAuto.Value))).Returns(searchResult);

            SearchServices service = new SearchServices(repoMock.Object, null, searchRepoMock.Object, helperMock.Object, refMock.Object, null);

            // When
            AdSearchResultModel result = service.AdvancedSearchAds(model);

            // Then
            Assert.AreEqual(1, result.SearchResultTotalCount);
        }

        [TestMethod]
        public void AdvancedSearchAds_SearchThroughMotoAds_CallSearchRepoOnMotoAds()
        {
            // Given
            Category cat = new Category { Id = 1, LabelUrlPart = "cat-url-label", Label = "Label", Type = AdTypeEnum.MotoAd };

            AdvancedAdSearchModel model = new AdvancedAdSearchModel();
            model.CategorySelectedId = 1;
            model.SearchString = "toto";
            model.CitySelectedId = 12;
            model.AgeBracketSelectedId = 1;
            model.KmBracketSelectedId = 1;
            model.BrandSelectedId = 19;
            model.EngineSizeBracketSelectedId = 2;

            IList<SearchAdCache> searchResult = new List<SearchAdCache>();
            searchResult.Add(new SearchAdCache
            {
                Title = "car",
                City = new City() { Label = "the city" },
                Category = cat
            });

            var repoMock = new Moq.Mock<IRepository>();
            repoMock.Setup(r => r.Get<Category>(cat.Id)).Returns(cat);

            var helperMock = new Moq.Mock<IHelperService>();
            helperMock.Setup(r => r.GetCurrentDateTime()).Returns(new DateTime(2013, 01, 01));

            IDictionary<int, BracketItemReference> ageRef = new Dictionary<int, BracketItemReference>();
            ageRef.Add(1, new BracketItemReference { LowValue = 0, HighValue = 3 });
            IDictionary<int, BracketItemReference> kmRef = new Dictionary<int, BracketItemReference>();
            kmRef.Add(1, new BracketItemReference { LowValue = 50, HighValue = 100 });
            IDictionary<int, BracketItemReference> esRef = new Dictionary<int, BracketItemReference>();
            esRef.Add(2, new BracketItemReference { LowValue = 250, HighValue = 650 });

            var refMock = new Moq.Mock<IReferenceServices>();
            refMock.Setup(s => s.GetAllAgeBrackets()).Returns(ageRef);
            refMock.Setup(s => s.GetAllKmBrackets()).Returns(kmRef);
            refMock.Setup(s => s.GetAllEngineSizeBrackets()).Returns(esRef);

            var searchRepoMock = new Moq.Mock<ISearchRepository>();
            searchRepoMock.Setup(r => r.AdvancedSearchAds<MotoAd>(
                It.Is<AdSearchParameters>(p =>
                    p.AndSearchStrings[0].Equals("toto")
                    && p.CityId == 12
                    && p.CategoryIds[0] == 1
                    && p.MinKm == 50
                    && p.MaxKm == 100
                    && p.MinYear == 2010
                    && p.MaxYear == 2013
                    && p.BrandId == 19
                    && p.MinEngineSize == 250
                    && p.MaxEngineSize == 650))).Returns(searchResult);

            SearchServices service = new SearchServices(repoMock.Object, null, searchRepoMock.Object, helperMock.Object, refMock.Object, null);

            // When
            AdSearchResultModel result = service.AdvancedSearchAds(model);

            // Then
            Assert.AreEqual(1, result.SearchResultTotalCount);
        }

        [TestMethod]
        public void AdvancedSearchAds_SearchThroughOtherVehicleAds_CallSearchRepoOnOtherVehicleAds()
        {
            // Given
            Category cat = new Category { Id = 1, LabelUrlPart = "cat-url-label", Label = "Label", Type = AdTypeEnum.OtherVehiculeAd };

            AdvancedAdSearchModel model = new AdvancedAdSearchModel();
            model.CategorySelectedId = 1;
            model.SearchString = "toto";
            model.CitySelectedId = 12;
            model.AgeBracketSelectedId = 1;
            model.KmBracketSelectedId = 1;
            model.FuelSelectedId = 89;

            IList<SearchAdCache> searchResult = new List<SearchAdCache>();
            searchResult.Add(new SearchAdCache
            {
                Title = "car",
                City = new City() { Label = "the city" },
                Category = cat
            });

            var repoMock = new Moq.Mock<IRepository>();
            repoMock.Setup(r => r.Get<Category>(cat.Id)).Returns(cat);

            var helperMock = new Moq.Mock<IHelperService>();
            helperMock.Setup(r => r.GetCurrentDateTime()).Returns(new DateTime(2013, 01, 01));

            IDictionary<int, BracketItemReference> ageRef = new Dictionary<int, BracketItemReference>();
            ageRef.Add(1, new BracketItemReference { LowValue = 0, HighValue = 3 });
            IDictionary<int, BracketItemReference> kmRef = new Dictionary<int, BracketItemReference>();
            kmRef.Add(1, new BracketItemReference { LowValue = 50, HighValue = 100 });

            var refMock = new Moq.Mock<IReferenceServices>();
            refMock.Setup(s => s.GetAllAgeBrackets()).Returns(ageRef);
            refMock.Setup(s => s.GetAllKmBrackets()).Returns(kmRef);

            var searchRepoMock = new Moq.Mock<ISearchRepository>();
            searchRepoMock.Setup(r => r.AdvancedSearchAds<OtherVehicleAd>(
                It.Is<AdSearchParameters>(p =>
                    p.AndSearchStrings[0].Equals("toto")
                    && p.CityId == 12
                    && p.CategoryIds[0] == 1
                    && p.MinKm == 50
                    && p.MaxKm == 100
                    && p.MinYear == 2010
                    && p.MaxYear == 2013
                    && p.FueldId == 89))).Returns(searchResult);

            SearchServices service = new SearchServices(repoMock.Object, null, searchRepoMock.Object, helperMock.Object, refMock.Object, null);

            // When
            AdSearchResultModel result = service.AdvancedSearchAds(model);

            // Then
            Assert.AreEqual(1, result.SearchResultTotalCount);
        }

        [TestMethod]
        public void AdvancedSearchAds_SearchThroughRealEstateAds_CallSearchRepoOnRealEstateAds()
        {
            // Given
            Category cat = new Category { Id = 1, LabelUrlPart = "cat-url-label", Label = "Label", Type = AdTypeEnum.RealEstateAd };

            AdvancedAdSearchModel model = new AdvancedAdSearchModel()
            {
                CategorySelectedId = 1,
                SearchString = "appart",
                NbRoomsBracketSelectedId = 2,
                SelectedDistrictId = 71,
                SelectedRealEstateTypeId = 2,
                MinPrice = 0,
                MaxPrice = 100000,
                SurfaceAreaBracketSelectedId = 3,
                IsFurnished = true
            };

            IList<SearchAdCache> searchResult = new List<SearchAdCache>();
            searchResult.Add(new SearchAdCache
            {
                Title = "appart",
                City = new City() { Label = "the city" },
                Category = cat
            });

            IDictionary<int, BracketItemReference> nbRoomsBr = new Dictionary<int, BracketItemReference>();
            nbRoomsBr.Add(2, new BracketItemReference { LowValue = 2, HighValue = 3 });

            IDictionary<int, BracketItemReference> surfBr = new Dictionary<int, BracketItemReference>();
            surfBr.Add(3, new BracketItemReference { LowValue = 45, HighValue = 70 });

            var refMock = new Moq.Mock<IReferenceServices>();
            refMock.Setup(s => s.GetAllRealEstateNbRoomsBrackets()).Returns(nbRoomsBr);
            refMock.Setup(s => s.GetAllSurfaceAreaBrackets()).Returns(surfBr);

            var repoMock = new Moq.Mock<IRepository>();
            repoMock.Setup(r => r.Get<Category>(cat.Id)).Returns(cat);

            var searchRepoMock = new Moq.Mock<ISearchRepository>();
            searchRepoMock.Setup(r => r.AdvancedSearchAds<RealEstateAd>(
                It.Is<AdSearchParameters>(p =>
                    p.AndSearchStrings[0].Equals("appart")
                    && p.MinNbRooms == 2
                    && p.MaxNbRooms == 3
                    && p.MinPrice == 0d
                    && p.MaxPrice == 100000d
                    && p.RealEstateTypeId == 2
                    && p.MinSurfaceArea == 45
                    && p.MaxSurfaceArea == 70
                    && p.IsFurnished.Value
                    && p.DistrictId == 71))).Returns(searchResult);

            SearchServices service = new SearchServices(repoMock.Object, null, searchRepoMock.Object, null, refMock.Object, null);

            // When
            AdSearchResultModel result = service.AdvancedSearchAds(model);

            // Then
            Assert.AreEqual(1, result.SearchResultTotalCount);
        }

        [TestMethod]
        public void AdvancedSearchAds_SearchThroughMotorBoatAds_CallSearchRepoOnMotorBoatAds()
        {
            // Given
            Category cat = new Category { Id = 1, LabelUrlPart = "cat-url-label", Label = "Label", Type = AdTypeEnum.MotorBoatAd };

            AdvancedAdSearchModel model = new AdvancedAdSearchModel()
            {
                CategorySelectedId = 1,
                SearchString = "bateau",
                AgeBracketSelectedId = 2,
                MinHp = 12,
                MaxHp = 38,
                MinLength = 0,
                MaxLength = 13,
                SelectedMotorBoatTypeId = 6,
                SelectedMotorTypeId = 2
            };

            IList<SearchAdCache> searchResult = new List<SearchAdCache>();
            searchResult.Add(new SearchAdCache
            {
                Title = "bateau",
                City = new City() { Label = "the city" },
                Category = cat
            });

            IDictionary<int, BracketItemReference> ageBr = new Dictionary<int, BracketItemReference>();
            ageBr.Add(2, new BracketItemReference { LowValue = 0, HighValue = 10 });

            var refMock = new Moq.Mock<IReferenceServices>();
            refMock.Setup(s => s.GetAllAgeBrackets()).Returns(ageBr);

            var repoMock = new Moq.Mock<IRepository>();
            repoMock.Setup(r => r.Get<Category>(cat.Id)).Returns(cat);

            var helperMock = new Moq.Mock<IHelperService>();
            helperMock.Setup(r => r.GetCurrentDateTime()).Returns(new DateTime(2013, 01, 01));

            var searchRepoMock = new Moq.Mock<ISearchRepository>();
            searchRepoMock.Setup(r => r.AdvancedSearchAds<MotorBoatAd>(
                It.Is<AdSearchParameters>(p =>
                    p.AndSearchStrings[0].Equals("bateau")
                    && p.MinHp == 12
                    && p.MaxHp == 38
                    && p.MinLength == 0.0
                    && p.MaxLength == 13.0
                    && p.MotorBoatTypeId == 6
                    && p.MotorEngineTypeId == 2
                    && p.MinYear == 2003
                    && p.MaxYear == 2013))).Returns(searchResult);

            SearchServices service = new SearchServices(repoMock.Object, null, searchRepoMock.Object, helperMock.Object, refMock.Object, null);

            // When
            AdSearchResultModel result = service.AdvancedSearchAds(model);

            // Then
            Assert.AreEqual(1, result.SearchResultTotalCount);
        }

        [TestMethod]
        public void AdvancedSearchAds_SearchThroughMotorBoatEngineAds_CallSearchRepoOnMotorBoatEngineAds()
        {
            // Given
            Category cat = new Category { Id = 1, LabelUrlPart = "cat-url-label", Label = "Label", Type = AdTypeEnum.MotorBoatEngineAd };

            AdvancedAdSearchModel model = new AdvancedAdSearchModel()
            {
                CategorySelectedId = 1,
                SearchString = "moteur",
                MinHp = 12,
                MaxHp = 38,
                SelectedMotorTypeId = 2
            };

            IList<SearchAdCache> searchResult = new List<SearchAdCache>();
            searchResult.Add(new SearchAdCache
            {
                Title = "moteur",
                City = new City() { Label = "the city" },
                Category = cat
            });

            var repoMock = new Moq.Mock<IRepository>();
            repoMock.Setup(r => r.Get<Category>(cat.Id)).Returns(cat);

            var searchRepoMock = new Moq.Mock<ISearchRepository>();
            searchRepoMock.Setup(r => r.AdvancedSearchAds<MotorBoatEngineAd>(
                It.Is<AdSearchParameters>(p =>
                    p.AndSearchStrings[0].Equals("moteur")
                    && p.MinHp == 12
                    && p.MaxHp == 38
                    && p.MotorEngineTypeId == 2))).Returns(searchResult);

            SearchServices service = new SearchServices(repoMock.Object, null, searchRepoMock.Object, null, null, null);

            // When
            AdSearchResultModel result = service.AdvancedSearchAds(model);

            // Then
            Assert.AreEqual(1, result.SearchResultTotalCount);
        }

        [TestMethod]
        public void AdvancedSearchAds_SearchThroughSailingBoatAds_CallSearchRepoOnSailingBoatAds()
        {
            // Given
            Category cat = new Category { Id = 1, LabelUrlPart = "cat-url-label", Label = "Label", Type = AdTypeEnum.SailingBoatAd };

            AdvancedAdSearchModel model = new AdvancedAdSearchModel()
            {
                CategorySelectedId = 1,
                SearchString = "voilier",
                AgeBracketSelectedId = 2,
                MinLength = 0,
                MaxLength = 13,
                SelectedSailingBoatTypeId = 6,
                SelectedHullTypeId = 2
            };

            IList<SearchAdCache> searchResult = new List<SearchAdCache>();
            searchResult.Add(new SearchAdCache
            {
                Title = "voilier",
                City = new City() { Label = "the city" },
                Category = cat
            });

            IDictionary<int, BracketItemReference> ageBr = new Dictionary<int, BracketItemReference>();
            ageBr.Add(2, new BracketItemReference { LowValue = 0, HighValue = 11 });

            var refMock = new Moq.Mock<IReferenceServices>();
            refMock.Setup(s => s.GetAllAgeBrackets()).Returns(ageBr);

            var repoMock = new Moq.Mock<IRepository>();
            repoMock.Setup(r => r.Get<Category>(cat.Id)).Returns(cat);

            var helperMock = new Moq.Mock<IHelperService>();
            helperMock.Setup(r => r.GetCurrentDateTime()).Returns(new DateTime(2013, 01, 01));

            var searchRepoMock = new Moq.Mock<ISearchRepository>();
            searchRepoMock.Setup(r => r.AdvancedSearchAds<SailingBoatAd>(
                It.Is<AdSearchParameters>(p =>
                    p.AndSearchStrings[0].Equals("voilier")
                    && p.MinLength == 0.0
                    && p.MaxLength == 13.0
                    && p.SailingBoatTypeId == 6
                    && p.HullTypeId == 2
                    && p.MinYear == 2002
                    && p.MaxYear == 2013))).Returns(searchResult);

            SearchServices service = new SearchServices(repoMock.Object, null, searchRepoMock.Object, helperMock.Object, refMock.Object, null);

            // When
            AdSearchResultModel result = service.AdvancedSearchAds(model);

            // Then
            Assert.AreEqual(1, result.SearchResultTotalCount);
        }

        [TestMethod]
        public void AdvancedSearchAds_SearchThroughWaterSportAds_CallSearchRepoOnWaterSportAds()
        {
            // Given
            Category cat = new Category { Id = 1, LabelUrlPart = "cat-url-label", Label = "Label", Type = AdTypeEnum.WaterSportAd };

            AdvancedAdSearchModel model = new AdvancedAdSearchModel()
            {
                CategorySelectedId = 1,
                SearchString = "kite",
                SelectedWaterTypeId = 9
            };

            IList<SearchAdCache> searchResult = new List<SearchAdCache>();
            searchResult.Add(new SearchAdCache
            {
                Title = "kite surf",
                City = new City() { Label = "the city" },
                Category = cat
            });

            var repoMock = new Moq.Mock<IRepository>();
            repoMock.Setup(r => r.Get<Category>(cat.Id)).Returns(cat);

            var searchRepoMock = new Moq.Mock<ISearchRepository>();
            searchRepoMock.Setup(r => r.AdvancedSearchAds<WaterSportAd>(
                It.Is<AdSearchParameters>(p =>
                    p.AndSearchStrings[0].Equals("kite")
                    && p.WaterTypeId == 9))).Returns(searchResult);

            SearchServices service = new SearchServices(repoMock.Object, null, searchRepoMock.Object, null, null, null);

            // When
            AdSearchResultModel result = service.AdvancedSearchAds(model);

            // Then
            Assert.AreEqual(1, result.SearchResultTotalCount);
        }

        [TestMethod]
        public void QuickSearch_Test()
        {
            // Given
            AdSearchModel sM = new AdSearchModel();
            sM.SearchString = "verre";
            sM.CitySelectedId = 45;

            Category c1 = new Category
            {
                Label = "Vehicule",
            };
            Category c2 = new Category
            {
                Label = "Voiture",
            };
            c1.AddCategory(c2);
            Category c3 = new Category
            {
                Label = "Moto",
            };
            c1.AddCategory(c3);

            Category c4 = new Category
            {
                Label = "Maison",
            };
            Category c5 = new Category
            {
                Label = "Meuble",
            };
            c4.AddCategory(c5);
            Category c6 = new Category
            {
                Label = "Vaisselle",
            };
            c4.AddCategory(c6);

            IDictionary<Category, int> res = new Dictionary<Category, int>();
            res.Add(c2, 2);
            res.Add(c3, 1);
            res.Add(c6, 6);

            var searchRepoMock = new Moq.Mock<ISearchRepository>();
            searchRepoMock.Setup(x => 
                x.CountByCategory(It.Is<string[]>(b => b[0] == sM.SearchString), It.Is<int?>(i => i.Value == sM.CitySelectedId)))
                .Returns(res);

            SearchServices service = new SearchServices(null, null, searchRepoMock.Object, null, null, null);

            // When
            AdHomeSearchResultModel ress = service.QuickSearch(sM);

            // Then
            Assert.AreEqual(sM.CitySelectedId, ress.CitySelectedId);
            Assert.AreEqual(sM.SearchString, ress.SearchString);
            Assert.AreEqual(9, ress.SearchResultTotalCount);
            Assert.AreEqual(2, ress.Results.Count);
            Assert.AreEqual(3, ress.Results[0].ResultCount);
            Assert.AreEqual("Vehicule", ress.Results[0].CategoryLabel);
            Assert.AreEqual("Voiture", ress.Results[0].SubCategoriesResults[0].CategoryLabel);
            Assert.AreEqual(2, ress.Results[0].SubCategoriesResults[0].ResultCount);
            Assert.AreEqual(0, ress.Results[0].SubCategoriesResults[0].SubCategoriesResults.Count);
            Assert.AreEqual("Moto", ress.Results[0].SubCategoriesResults[1].CategoryLabel);
            Assert.AreEqual(1, ress.Results[0].SubCategoriesResults[1].ResultCount);
            Assert.AreEqual(0, ress.Results[0].SubCategoriesResults[1].SubCategoriesResults.Count);

            Assert.AreEqual(6, ress.Results[1].ResultCount);
            Assert.AreEqual("Maison", ress.Results[1].CategoryLabel);
            Assert.AreEqual("Vaisselle", ress.Results[1].SubCategoriesResults[0].CategoryLabel);
            Assert.AreEqual(6, ress.Results[1].SubCategoriesResults[0].ResultCount);
            Assert.AreEqual(0, ress.Results[1].SubCategoriesResults[0].SubCategoriesResults.Count);
        }
    }

}
