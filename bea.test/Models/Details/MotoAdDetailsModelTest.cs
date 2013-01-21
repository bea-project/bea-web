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
    public class MotoAdDetailsModelTest
    {
        [TestMethod]
        public void MotoAdDetailsModel_ctor_WithMotoAd()
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

            MotoAd ad = new MotoAd()
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
                EngineSize = 600
            };

            // When
            MotoAdDetailsModel model = new MotoAdDetailsModel(ad);

            // Then
            Assert.AreEqual(17, model.AdId);
            Assert.AreEqual(2000, model.Kilometers);
            Assert.AreEqual(2013, model.Year);
            Assert.AreEqual(600, model.EngineSize);
        }

        [TestMethod]
        public void MotoAdDetailsModel_ctor_WithMotoAd_OtherBrand()
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

            MotoAd ad = new MotoAd()
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
                Brand = new MotoBrand { Label = "Autre" },
                OtherBrand = "Batmobile",
            };

            // When
            MotoAdDetailsModel model = new MotoAdDetailsModel(ad);

            // Then
            Assert.AreEqual("Batmobile", model.Brand);
        }

    }
}
