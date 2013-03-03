using System;
using Bea.Domain;
using Bea.Domain.Ads;
using Bea.Domain.Categories;
using Bea.Domain.Location;
using Bea.Models.Details;
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

            Category cat = new Category()
            {
                Label = "Auto",
                LabelUrlPart = "Auto",
                ParentCategory = new Category { Label = "Véhicules", LabelUrlPart = "Vehicules" }
            };

            Ad ad = new Ad()
            {
                Id = 17,
                Title = "title",
                Price = 1270,
                City = c,
                CreatedBy = u,
                CreationDate = new DateTime(2012, 05, 12, 17, 26, 08),
                Body = "body",
            };
            cat.AddAd(ad);
            ad.Images.Add(new AdImage() { Id = Guid.Parse("e9da8b1e-aa77-401b-84e0-a1290130b7b7") });
            ad.Images.Add(new AdImage() { Id = Guid.Parse("e9da8b1e-aa77-401b-84e0-a1290130b7b9") });

            // When
            AdDetailsModel model = new AdDetailsModel(ad);

            // Then
            Assert.AreEqual("title", model.Title);
            Assert.AreEqual("Nouméa", model.Location);
            Assert.AreEqual("Nicolas", model.UserFirstName);
            Assert.AreEqual("samedi 12 mai 2012 17:26", model.CreationDateString);
            Assert.AreEqual("body", model.Body);
            Assert.AreEqual("1 270 Francs", model.Price);
            Assert.AreEqual(17, model.AdId);
            Assert.AreEqual(2, model.ImagesIds.Count);
            Assert.AreEqual("e9da8b1e-aa77-401b-84e0-a1290130b7b7", model.ImagesIds[0]);
            Assert.AreEqual("e9da8b1e-aa77-401b-84e0-a1290130b7b9", model.ImagesIds[1]);
            Assert.AreEqual("Auto", model.Category);
            Assert.AreEqual("Auto", model.CategoryUrlPart);
            Assert.AreEqual("Véhicules", model.CategoryGroup);
            Assert.AreEqual("Vehicules", model.CategoryGroupUrlPart); 
            
        }
    }
}
