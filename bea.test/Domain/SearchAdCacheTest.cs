using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain;
using Bea.Domain.Ads;
using Bea.Domain.Categories;
using Bea.Domain.Location;
using Bea.Domain.Search;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bea.Test.Domain
{
    [TestClass]
    public class SearchAdCacheTest
    {
        [TestMethod]
        public void SearchAdCache_CopyCtorWithBaseAd_NoImages_CopyBaseAdProperties()
        {
            // Given
            BaseAd ad = new Ad()
            {
                Id = 1245,
                Title = "title",
                Body = "body",
                City = new City { Id = 45, Province = new Province { Id = 11 } },
                Category = new Category { Id = 89, CategoryGroup = new CategoryGroup { Id = 1447 } },
                Price = 200457,
                CreationDate = new DateTime(2013, 01, 05)
            };

            // When
            SearchAdCache actual = new SearchAdCache(ad);

            // Then
            Assert.AreEqual(ad.Id, actual.AdId);
            Assert.AreEqual(ad.Title, actual.Title);
            Assert.AreEqual(ad.Body, actual.Body);
            Assert.AreEqual(ad.City, actual.City);
            Assert.AreEqual(ad.City.Province, actual.Province);
            Assert.AreEqual(ad.Category, actual.Category);
            Assert.AreEqual(ad.Category.CategoryGroup, actual.CategoryGroup);
            Assert.AreEqual(ad.Price, actual.Price);
            Assert.AreEqual(ad.CreationDate, actual.CreationDate);
            Assert.AreEqual(ad.Category.Type, actual.AdType);
            Assert.IsNull(actual.AdImageId);
        }

        [TestMethod]
        public void SearchAdCache_CopyCtorWithBaseAd_WithImages_CopyBaseAdProperties()
        {
            // Given
            BaseAd ad = new Ad()
            {
                Id = 1245,
                Title = "title",
                Body = "body",
                City = new City { Id = 45, Province = new Province { Id = 11 } },
                Category = new Category { Id = 89, CategoryGroup = new CategoryGroup { Id = 1447 } },
                Images = new List<AdImage>() 
                {
                    new AdImage { Id = Guid.Parse("b9da8b1e-aa77-401b-84e0-a1290130b7b7"), IsPrimary = false },
                    new AdImage { Id = Guid.Parse("e9da8b1e-aa77-401b-84e0-a1290130b7b7"), IsPrimary = true },
                }
            };

            // When
            SearchAdCache actual = new SearchAdCache(ad);

            // Then
            Assert.AreEqual("e9da8b1e-aa77-401b-84e0-a1290130b7b7", actual.AdImageId);
        }
    }
}
