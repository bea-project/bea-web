using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Core.Dal;
using Bea.Domain;
using Bea.Models;
using Bea.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bea.Test.services
{
    [TestClass]
    public class SearchServicesTest
    {
        [TestMethod]
        public void SearchAdsByTitle_2Ads_CalAdRepoAndBuildModels()
        {
            // Given
            IList<Ad> searchResult = new List<Ad>();
            searchResult.Add(new Ad
            {
                Title = "the ad title",
                City = new Domain.Location.City() { Label = "the city" }
            });
            searchResult.Add(new Ad
            {
                Title = "the ad title 2",
                City = new Domain.Location.City() { Label = "the city" }
            });

            var adRepoMock = new Moq.Mock<IAdRepository>();
            adRepoMock.Setup(r => r.SearchAdsByTitle("title")).Returns(searchResult);

            SearchServices service = new SearchServices(adRepoMock.Object);

            // When
            AdSearchResultModel result = service.SearchAdsByTitle("title");

            // Then
            Assert.AreEqual("title", result.SearchString);
            Assert.AreEqual(2, result.SearchResult.Count);
            Assert.AreEqual(2, result.SearchResultTotalCount);
            Assert.AreEqual("the ad title", result.SearchResult[0].Title);
            Assert.AreEqual("the ad title 2", result.SearchResult[1].Title);

            adRepoMock.VerifyAll();
        }
    }
}
