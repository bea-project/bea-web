using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain;
using Bea.Domain.Ads;
using Bea.Models;
using Bea.Models.Create;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bea.Test.Models.AdCreate
{
    [TestClass]
    public class AdCreateModelTest
    {
        [TestMethod]
        public void Constructor_SetIsOfferToTrue()
        {
            // When
            AdCreateModel actual = new AdCreateModel();

            // Then
            Assert.IsTrue(actual.IsOffer);
        }

        [TestMethod]
        public void Constructor_WithoutImages_LeaveImageIdsAsNull()
        {
            // Given
            BaseAd ad = new Ad();

            // When
            AdCreateModel actual = new AdCreateModel(ad);

            // Then
            Assert.AreEqual(String.Empty, actual.ImageIds);
        }

        [TestMethod]
        public void Constructor_With2Images_ConcatImageIds()
        {
            // Given
            BaseAd ad = new Ad();
            ad.AddImage(new AdImage() { Id = Guid.Parse("e9da8b1e-aa77-401b-84e0-a1290130b7b7") });
            ad.AddImage(new AdImage() { Id = Guid.Parse("e9da8b1e-aa77-401b-84e0-a1290130b7b9") });

            // When
            AdCreateModel actual = new AdCreateModel(ad);

            // Then
            Assert.AreEqual("e9da8b1e-aa77-401b-84e0-a1290130b7b7;e9da8b1e-aa77-401b-84e0-a1290130b7b9;", actual.ImageIds);
        }

        [TestMethod]
        public void Constructor_WithoutProperties_FillInModel()
        {
            // Given
            BaseAd ad = new Ad();
            ad.Body = "body";
            ad.Title = "title";
            ad.PhoneNumber = "78.85.75";
            ad.Price = 2000;
            ad.Category = new Bea.Domain.Categories.Category { Id = 12 };
            ad.City = new Bea.Domain.Location.City { Id = 11, Province = new Bea.Domain.Location.Province { Id = 13 } };
            ad.CreatedBy = new User { Email = "e@e.com", Firstname = "Nico" };
            // When
            AdCreateModel actual = new AdCreateModel(ad);

            // Then
            Assert.AreEqual("body", actual.Body);
            Assert.AreEqual("title", actual.Title);
            Assert.AreEqual("78.85.75", actual.Telephone);
            Assert.AreEqual(2000, actual.Price);
            Assert.AreEqual(12, actual.SelectedCategoryId);
            Assert.AreEqual(11, actual.SelectedCityId);
            Assert.AreEqual(13, actual.SelectedProvinceId);
            Assert.AreEqual("e@e.com", actual.Email);
            Assert.AreEqual("Nico", actual.Name);
        }
    }
}
