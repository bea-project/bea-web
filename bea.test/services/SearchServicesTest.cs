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
using Bea.Models.Search.Vehicles;
using Bea.Domain.Reference;

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

            AdSearchModel model = new AdSearchModel()
            {
                SearchString = "title",
                CitySelectedId = 98
            };

            SearchServices service = new SearchServices(null, null, adRepoMock.Object, null, null);
            
            // When
            AdSearchResultModel result = service.SearchAds(model);

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

            AdSearchModel model = new AdSearchModel()
            {
                SearchString = "ship computer",
                CitySelectedId = 98
            };

            SearchServices service = new SearchServices(null, null, adRepoMock.Object, null, null);

            // When
            AdSearchResultModel result = service.SearchAds(model);

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

            AdSearchModel model = new AdSearchModel()
            {
                SearchString = "ship",
                CategorySelectedId = 12
            };

            SearchServices service = new SearchServices(repoMock.Object, null, adRepoMock.Object, null, null);

            // When
            AdSearchResultModel result = service.SearchAds(model);

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


            AdSearchModel model = new AdSearchModel()
            {
                SearchString = "ship",
                CategorySelectedId = 12
            };

            SearchServices service = new SearchServices(repoMock.Object, null, adRepoMock.Object, null, null);

            // When
            AdSearchResultModel result = service.SearchAds(model);

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

            SearchServices service = new SearchServices(repoMock.Object, catRepoMock.Object, adRepoMock.Object, null, null);

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

            SearchServices service = new SearchServices(repoMock.Object, null, searchRepoMock.Object, null, null);

            // When
            AdSearchResultModel result = service.AdvancedSearchAds(model);

            // Then
            Assert.AreEqual(1, result.SearchResultTotalCount);

        }

        [TestMethod]
        public void AdvancedSearchAds_SearchThroughCarAds_CallSearchRepoOnCarAds()
        {
            // Given
            Category cat = new Category { Id = 1, LabelUrlPart = "cat-url-label", Label = "Label", Type = AdTypeEnum.CarAd };

            CarAdSearchModel model = new CarAdSearchModel();
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
            searchRepoMock.Setup(r => r.SearchVehicleAds<CarAd>(
                It.Is<String[]>(x => x[0] == "toto"), 
                12, 
                It.Is<int[]>(x => x[0] == 1),
                50, 100,
                2010, 2013,
                19,
                89,
                true,
                null, null)).Returns(searchResult);

            SearchServices service = new SearchServices(repoMock.Object, null, searchRepoMock.Object, helperMock.Object, refMock.Object);

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

            MotoAdSearchModel model = new MotoAdSearchModel();
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
            searchRepoMock.Setup(r => r.SearchVehicleAds<MotoAd>(
                It.Is<String[]>(x => x[0] == "toto"),
                12,
                It.Is<int[]>(x => x[0] == 1),
                50, 100,
                2010, 2013,
                19,
                null,
                null,
                250, 650)).Returns(searchResult);

            SearchServices service = new SearchServices(repoMock.Object, null, searchRepoMock.Object, helperMock.Object, refMock.Object);

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

            OtherVehicleAdSearchModel model = new OtherVehicleAdSearchModel();
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
            searchRepoMock.Setup(r => r.SearchVehicleAds<OtherVehicleAd>(
                It.Is<String[]>(x => x[0] == "toto"),
                12,
                It.Is<int[]>(x => x[0] == 1),
                50, 100,
                2010, 2013,
                null,
                89,
                null,
                null, null)).Returns(searchResult);

            SearchServices service = new SearchServices(repoMock.Object, null, searchRepoMock.Object, helperMock.Object, refMock.Object);

            // When
            AdSearchResultModel result = service.AdvancedSearchAds(model);

            // Then
            Assert.AreEqual(1, result.SearchResultTotalCount);
        }
    }
}
