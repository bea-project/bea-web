using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain;
using Bea.Domain.Ads.WaterSport;
using Bea.Domain.Location;
using Bea.Models.Details.WaterSport;
using Bea.Tools;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bea.Test.Models.Details.WaterSport
{
    [TestClass]
    public class MotorBoatEngineAdDetailsModelTest
    {
        [TestMethod]
        public void Test_CtorWithAd()
        {
            // Given
            MotorBoatEngineAd ad = new MotorBoatEngineAd
            {
                Title = "title",
                MotorType = new Bea.Domain.Reference.MotorBoatEngineType { Label = "type" },
                Year = 2012,
                Hp = 89,
                City = new City(),
                CreatedBy = new User()
            };

            // When
            MotorBoatEngineAdDetailsModel actual = new MotorBoatEngineAdDetailsModel(ad);

            // Then
            Assert.AreEqual(ad.Title, actual.Title);
            Assert.AreEqual(ad.MotorType.Label, actual.MotorType);
            Assert.AreEqual(ad.Year, actual.Year);
            Assert.AreEqual("89 Cv", actual.Hp);
        }

        [TestMethod]
        public void Test_CtorWithAd_NoTypes()
        {
            // Given
            MotorBoatEngineAd ad = new MotorBoatEngineAd
            {
                Title = "title",
                City = new City(),
                CreatedBy = new User()
            };

            // When
            MotorBoatEngineAdDetailsModel actual = new MotorBoatEngineAdDetailsModel(ad);

            // Then
            Assert.AreEqual(ad.Title, actual.Title);
            Assert.AreEqual(String.Empty, actual.MotorType);
            Assert.AreEqual(null, actual.Year);
            Assert.AreEqual(String.Empty, actual.Hp);
        }
    }
}
