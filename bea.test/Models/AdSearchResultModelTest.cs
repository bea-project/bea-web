﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain;
using Bea.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bea.Test.Models
{
    [TestClass]
    public class AdSearchResultModelTest
    {
        [TestMethod]
        public void AdSearchResultModel_ConstructorTest()
        {
            // Given
            Ad ad = new Ad()
            {
                Id = 19,
                Title = "the title",
                CreationDate = new DateTime(2012, 12, 21),
                City = new Domain.Location.City { Label = "Nouméa" },
                Price = 127
            };

            // When
            AdSearchResultModel model = new AdSearchResultModel(ad);

            // Then
            Assert.AreEqual(ad.Id, model.AdId);
            Assert.AreEqual(ad.Title, model.Title);
            Assert.AreEqual(ad.CreationDate, model.CreationDate);
            Assert.AreEqual(ad.City.Label, model.Location);
            Assert.AreEqual("127 Francs", model.Price);
        }
    }
}
