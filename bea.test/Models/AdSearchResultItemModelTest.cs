using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain;
using Bea.Domain.Ads;
using Bea.Domain.Categories;
using Bea.Domain.Location;
using Bea.Domain.Search;
using Bea.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bea.Test.Models
{
    [TestClass]
    public class AdSearchResultItemModelTest
    {
        [TestMethod]
        public void AdSearchResultModel_ConstructorTest()
        {
            // Given
            SearchAdCache ad = new SearchAdCache()
            {
                AdId = 19,
                Title = "the title",
                CreationDate = new DateTime(2012, 12, 21),
                City = new City { Label = "Nouméa" },
                Price = 127,
                AdImageId = "456",
                Category = new Category { Label = "category" }
            };

            // When
            AdSearchResultItemModel model = new AdSearchResultItemModel(ad);

            // Then
            Assert.AreEqual(ad.AdId, model.AdId);
            Assert.AreEqual(ad.Title, model.Title);
            Assert.AreEqual(ad.CreationDate, model.CreationDate);
            Assert.AreEqual(ad.City.Label, model.Location);
            Assert.AreEqual("127 Francs", model.Price);
            Assert.AreEqual(ad.AdImageId, model.MainImageId);
            Assert.AreEqual(ad.Category.Label, model.Category);
        }
    }
}
