using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain;
using Bea.Domain.Ads.WaterSport;
using Bea.Domain.Location;
using Bea.Domain.Reference;
using Bea.Models.Details.WaterSport;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bea.Test.Models.Details.WaterSport
{
    [TestClass]
    public class WaterSportAdDetailsModelTest
    {
        [TestMethod]
        public void Test_CtorWithAd()
        {
            // Given
            WaterSportAd ad = new WaterSportAd
            {
                Title = "title",
                Type = new WaterSportType { Label = "Kite" },
                City = new City(),
                CreatedBy = new User()
            };
            
            // When
            WaterSportAdDetailsModel actual = new WaterSportAdDetailsModel(ad);

            // Then
            Assert.AreEqual(ad.Title, actual.Title);
            Assert.AreEqual(ad.Type.Label, actual.SportType);
        }

        [TestMethod]
        public void Test_CtorWithAd_NoType()
        {
            // Given
            WaterSportAd ad = new WaterSportAd
            {
                Title = "title",
                City = new City(),
                CreatedBy = new User()
            };

            // When
            WaterSportAdDetailsModel actual = new WaterSportAdDetailsModel(ad);

            // Then
            Assert.AreEqual(ad.Title, actual.Title);
            Assert.AreEqual(String.Empty, actual.SportType);
        }
    }
}
