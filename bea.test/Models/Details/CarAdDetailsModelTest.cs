using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain;
using Bea.Domain.Ads;
using Bea.Domain.Location;
using Bea.Domain.Reference;
using Bea.Models.Details;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bea.Test.Models.Details
{
    [TestClass]
    public class CarAdDetailsModelTest
    {
        [TestMethod]
        public void AdDetailsModel_ctor_WithCarAd_Automatique()
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

            CarAd ad = new CarAd()
            {
                Id = 17,
                Title = "title",
                Price = 1270,
                City = c,
                CreatedBy = u,
                CreationDate = new DateTime(2012, 05, 12, 17, 26, 08),
                Body = "body",
                Kilometers = 2000,
                Year = 2013,
                IsAutomatic = true
            };

            // When
            CarAdDetailsModel model = new CarAdDetailsModel(ad);

            // Then
            Assert.AreEqual(17, model.AdId);
            Assert.AreEqual(2000, model.Kilometers);
            Assert.AreEqual(2013, model.Year);
            Assert.AreEqual("Automatique", model.GearType);
        }

        [TestMethod]
        public void AdDetailsModel_ctor_WithCarAd_Manuelle()
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

            CarAd ad = new CarAd()
            {
                Id = 17,
                Title = "title",
                Price = 1270,
                City = c,
                CreatedBy = u,
                CreationDate = new DateTime(2012, 05, 12, 17, 26, 08),
                Body = "body",
                Kilometers = 200000,
                Year = 1997,
                IsAutomatic = false
            };

            // When
            CarAdDetailsModel model = new CarAdDetailsModel(ad);

            // Then
            Assert.AreEqual(17, model.AdId);
            Assert.AreEqual(200000, model.Kilometers);
            Assert.AreEqual(1997, model.Year);
            Assert.AreEqual("Manuelle", model.GearType);
        }

        [TestMethod]
        public void AdDetailsModel_ctor_WithCarAd_BrandAndFuel()
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

            CarAd ad = new CarAd()
            {
                Id = 17,
                Title = "title",
                Price = 1270,
                City = c,
                CreatedBy = u,
                CreationDate = new DateTime(2012, 05, 12, 17, 26, 08),
                Body = "body",
                Kilometers = 2000,
                Year = 2013,
                IsAutomatic = true,
                Brand = new CarBrand { Label = "Honda" },
                Fuel = new CarFuel { Label = "Super" }
            };

            // When
            CarAdDetailsModel model = new CarAdDetailsModel(ad);

            // Then
            Assert.AreEqual("Honda", model.Brand);
            Assert.AreEqual("Super", model.Fuel);
        }

        [TestMethod]
        public void AdDetailsModel_ctor_WithCarAd_NoBrandNorFuel()
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

            CarAd ad = new CarAd()
            {
                Id = 17,
                Title = "title",
                Price = 1270,
                City = c,
                CreatedBy = u,
                CreationDate = new DateTime(2012, 05, 12, 17, 26, 08),
                Body = "body",
                Kilometers = 2000,
                Year = 2013,
                IsAutomatic = true,
                Brand = null,
                Fuel = null
            };

            // When
            CarAdDetailsModel model = new CarAdDetailsModel(ad);

            // Then
            Assert.AreEqual(String.Empty, model.Brand);
            Assert.AreEqual(String.Empty, model.Fuel);
        }
    }
}
