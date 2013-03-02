using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain;
using Bea.Domain.Ads;
using Bea.Domain.Ads.WaterSport;
using Bea.Domain.Location;
using Bea.Models.Details.RealEstate;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bea.Test.Models.Details.RealEstate
{
    [TestClass]
    public class RealEstateAdDetailsModelTest
    {
        [TestMethod]
        public void Test_CtorWithAd()
        {
            // Given
            RealEstateAd ad = new RealEstateAd
            {
                Title = "title",
                Type = new Bea.Domain.Reference.RealEstateType { Label = "tyyype" },
                RoomsNumber = 4,
                District = new District { Label = "quartier" },
                SurfaceArea = 80,
                City = new City(),
                CreatedBy = new User()
            };

            // When
            RealEstateAdDetailsModel actual = new RealEstateAdDetailsModel(ad);

            // Then
            Assert.AreEqual(ad.Title, actual.Title);
            Assert.AreEqual(ad.Type.Label, actual.Type);
            Assert.AreEqual(ad.District.Label, actual.District);
            Assert.AreEqual(4, actual.RoomNb);
            Assert.AreEqual("80 m²", actual.SurfaceArea);
        }

        [TestMethod]
        public void Test_CtorWithAd_NoTypes()
        {
            // Given
            RealEstateAd ad = new RealEstateAd
            {
                Title = "title",
                City = new City(),
                CreatedBy = new User()
            };

            // When
            RealEstateAdDetailsModel actual = new RealEstateAdDetailsModel(ad);

            // Then
            Assert.AreEqual(ad.Title, actual.Title);
            Assert.AreEqual(String.Empty, actual.Type);
            Assert.AreEqual(String.Empty, actual.District);
            Assert.AreEqual(null, actual.RoomNb);
            Assert.AreEqual(String.Empty, actual.SurfaceArea);
        }
    }
}
