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
                Body = "body",
            };
            ad.Images.Add(new AdImage() { Id = Guid.Parse("e9da8b1e-aa77-401b-84e0-a1290130b7b7") });
            ad.Images.Add(new AdImage() { Id = Guid.Parse("e9da8b1e-aa77-401b-84e0-a1290130b7b9") });

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
            Assert.AreEqual(2, model.ImagesIds.Count);
            Assert.AreEqual("e9da8b1e-aa77-401b-84e0-a1290130b7b7", model.ImagesIds[0]);
            Assert.AreEqual("e9da8b1e-aa77-401b-84e0-a1290130b7b9", model.ImagesIds[1]);
        }
    }
}
