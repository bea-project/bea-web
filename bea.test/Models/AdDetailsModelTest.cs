using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain;
using Bea.Domain.Location;
using Bea.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bea.Test.Models
{
    [TestClass]
    public class AdDetailsModelTest
    {
        [TestMethod]
        public void AdDetailsModel_ctor()
        {
            // Given
            City c = new City()
            {
                Label = "Nouméa"
            };

            User u = new User()
            {
                Firstname = "Nicolas"
            };

            Ad ad = new Ad()
            {
                Id = 17,
                Title = "title",
                Price = 1270,
                City = c,
                CreatedBy = u,
                CreationDate = new DateTime(2012, 05, 12),
                Body = "body"
            };

            // When
            AdDetailsModel model = new AdDetailsModel(ad);

            // Then
            Assert.AreEqual("title", model.Title);
            Assert.AreEqual("Nouméa", model.Location);
            Assert.AreEqual("Nicolas", model.UserFirstName);
            Assert.AreEqual(new DateTime(2012, 05, 12), model.CreationDate);
            Assert.AreEqual("body", model.Body);
            Assert.AreEqual("1 270 Francs", model.Price);
            Assert.AreEqual(17, model.AdId);
        }
    }
}
