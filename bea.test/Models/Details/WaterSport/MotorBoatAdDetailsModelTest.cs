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
    public class MotorBoatAdDetailsModelTest
    {
        [TestMethod]
        public void Test_CtorWithAd()
        {
            // Given
            MotorBoatAd ad = new MotorBoatAd
            {
                Title = "title",
                Type  = new Bea.Domain.Reference.MotorBoatType { Label = "type" },
                MotorType = new Bea.Domain.Reference.MotorBoatEngineType { Label = "motor type" },
                Year = 2012,
                Length = 15.80000M,
                Hp = 89,
                City = new City(),
                CreatedBy = new User()
            };

            var helperMock = new Moq.Mock<IHelperService>();
            helperMock.Setup(x => x.GetCulture()).Returns(new System.Globalization.CultureInfo("Fr"));

            // When
            MotorBoatAdDetailsModel actual = new MotorBoatAdDetailsModel(ad, helperMock.Object);

            // Then
            Assert.AreEqual(ad.Title, actual.Title);
            Assert.AreEqual(ad.Type.Label, actual.BoatType);
            Assert.AreEqual(ad.MotorType.Label, actual.MotorType);
            Assert.AreEqual(ad.Year, actual.Year);
            Assert.AreEqual("15,80 Mtr", actual.Length);
            Assert.AreEqual("89 Cv", actual.Hp);
        }

        [TestMethod]
        public void Test_CtorWithAd_NoTypes()
        {
            // Given
            MotorBoatAd ad = new MotorBoatAd
            {
                Title = "title",
                City = new City(),
                CreatedBy = new User()
            };

            // When
            MotorBoatAdDetailsModel actual = new MotorBoatAdDetailsModel(ad, null);

            // Then
            Assert.AreEqual(ad.Title, actual.Title);
            Assert.AreEqual(String.Empty, actual.BoatType);
            Assert.AreEqual(String.Empty, actual.MotorType);
            Assert.AreEqual(null, actual.Year);
            Assert.AreEqual(String.Empty, actual.Length);
            Assert.AreEqual(String.Empty, actual.Hp);
        }

    }
}
