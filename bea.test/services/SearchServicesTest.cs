using System;
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

            var adRepoMock = new Moq.Mock<IAdRepository>();
            adRepoMock.Setup(r => r.SearchAds(new string[] { "title" }, null, 98, null)).Returns(searchResult);

            AdSearchModel model = new AdSearchModel()
            {
                SearchString = "title",
                CitySelectedId = 98
            };

            SearchServices service = new SearchServices(adRepoMock.Object, null, null, null);
            
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

            var adRepoMock = new Moq.Mock<IAdRepository>();
            adRepoMock.Setup(r => r.SearchAds(new string[] { "ship", "computer" }, null, 98, null)).Returns(searchResult);

            AdSearchModel model = new AdSearchModel()
            {
                SearchString = "ship computer",
                CitySelectedId = 98
            };

            SearchServices service = new SearchServices(adRepoMock.Object, null, null, null);

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

            var adRepoMock = new Moq.Mock<IAdRepository>();
            adRepoMock.Setup(r => r.SearchAds(new string[] { "ship" }, null, null, new int[] { 12 })).Returns(searchResult);

            var repoMock = new Moq.Mock<IRepository>();
            repoMock.Setup(r => r.Get<Category>(12)).Returns(cat);

            AdSearchModel model = new AdSearchModel()
            {
                SearchString = "ship",
                CategorySelectedId = 12
            };

            SearchServices service = new SearchServices(adRepoMock.Object, repoMock.Object, null, null);

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

            var adRepoMock = new Moq.Mock<IAdRepository>();
            adRepoMock.Setup(r => r.SearchAds(new string[] { "ship" }, null, null, new int[] { 12, 17 })).Returns(searchResult);

            var repoMock = new Moq.Mock<IRepository>();
            repoMock.Setup(r => r.Get<Category>(12)).Returns(group);


            AdSearchModel model = new AdSearchModel()
            {
                SearchString = "ship",
                CategorySelectedId = 12
            };

            SearchServices service = new SearchServices(adRepoMock.Object, repoMock.Object, null, null);

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
            Category cat = new Category { Id = 12, LabelUrlPart = "cat-url-label" };

            IList<SearchAdCache> searchResult = new List<SearchAdCache>();
            searchResult.Add(new SearchAdCache
            {
                Title = "ship",
                City = new City() { Label = "the city" },
                Category = cat
            });

            var adRepoMock = new Moq.Mock<IAdRepository>();
            adRepoMock.Setup(r => r.SearchAds(null, null, null, It.Is<int[]>(x => x[0] == cat.Id))).Returns(searchResult);

            var repoMock = new Moq.Mock<IRepository>();
            repoMock.Setup(r => r.Get<Category>(cat.Id)).Returns(cat);

            var catRepoMock = new Moq.Mock<ICategoryRepository>();
            catRepoMock.Setup(r => r.GetCategoryFromUrlPart("cat-url-label")).Returns(cat);

            SearchServices service = new SearchServices(adRepoMock.Object, repoMock.Object, catRepoMock.Object, null);

            // When
            AdSearchResultModel result = service.SearchAdsFromUrl(null, "cat-url-label");

            // Then
            Assert.IsNull(result.SearchString);
            Assert.IsNull(result.CitySelectedId);
            Assert.AreEqual(12, result.CategorySelectedId);
            Assert.AreEqual(1, result.SearchResult.Count);
            Assert.AreEqual(1, result.SearchResultTotalCount);
            Assert.AreEqual("ship", result.SearchResult[0].Title);

            adRepoMock.VerifyAll();
        }
    }
}
