using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Core.Dal;
using Bea.Domain;
using Bea.Domain.Ads;
using Bea.Domain.Location;
using Bea.Domain.Search;
using Bea.Models;
using Bea.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
                City = new City() { Label = "the city" },
                Category = new Bea.Domain.Categories.Category()
            });
            searchResult.Add(new SearchAdCache
            {
                Title = "the ad title 2",
                City = new City() { Label = "the city" },
                Category = new Bea.Domain.Categories.Category()
            });

            var adRepoMock = new Moq.Mock<IAdRepository>();
            adRepoMock.Setup(r => r.SearchAds(new string[] { "title" }, null, null, 98, null)).Returns(searchResult);

            AdSearchModel model = new AdSearchModel()
            {
                SearchString = "title",
                ProvinceSelectedId = null,
                CitySelectedId = 98
            };

            SearchServices service = new SearchServices(adRepoMock.Object);
            
            // When
            AdSearchResultModel result = service.SearchAds(model);

            // Then
            Assert.AreEqual("title", result.SearchString);
            Assert.IsNull(result.ProvinceSelectedId);
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
            adRepoMock.Setup(r => r.SearchAds(new string[] { "ship", "computer" }, null, null, 98, null)).Returns(searchResult);

            AdSearchModel model = new AdSearchModel()
            {
                SearchString = "ship computer",
                ProvinceSelectedId = null,
                CitySelectedId = 98
            };

            SearchServices service = new SearchServices(adRepoMock.Object);

            // When
            AdSearchResultModel result = service.SearchAds(model);

            // Then
            Assert.AreEqual("ship computer", result.SearchString);
            Assert.IsNull(result.ProvinceSelectedId);
            Assert.AreEqual(98, result.CitySelectedId);
            Assert.AreEqual(2, result.SearchResult.Count);
            Assert.AreEqual(2, result.SearchResultTotalCount);
            Assert.AreEqual("ship", result.SearchResult[0].Title);
            Assert.AreEqual("computer", result.SearchResult[1].Title);

            adRepoMock.VerifyAll();
        }
    }
}
