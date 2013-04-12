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
    public class SailingBoatAdDetailsModelTest
    {
        [TestMethod]
        public void Test_CtorWithAd()
        {
            // Given
            SailingBoatAd ad = new SailingBoatAd
            {
                Title = "title",
                SailingBoatType = new Bea.Domain.Reference.SailingBoatType { Label = "type" },
                HullType = new Bea.Domain.Reference.SailingBoatHullType { Label = "hull" },
                Year = 2012,
                Length = 15.75000,
                City = new City(),
                CreatedBy = new User()
            };

            var helperMock = new Moq.Mock<IHelperService>();
            helperMock.Setup(x => x.GetCulture()).Returns(new System.Globalization.CultureInfo("Fr"));

            // When
            SailingBoatAdDetailsModel actual = new SailingBoatAdDetailsModel(ad, helperMock.Object);

            // Then
            Assert.AreEqual(ad.Title, actual.Title);
            Assert.AreEqual(ad.SailingBoatType.Label, actual.BoatType);
            Assert.AreEqual(ad.HullType.Label, actual.HullType);
            Assert.AreEqual(ad.Year, actual.Year);
            Assert.AreEqual("15,75 Mtr", actual.Length);
        }

        [TestMethod]
        public void Test_CtorWithAd_NoTypes()
        {
            // Given
            SailingBoatAd ad = new SailingBoatAd
            {
                Title = "title",
                City = new City(),
                CreatedBy = new User()
            };

            // When
            SailingBoatAdDetailsModel actual = new SailingBoatAdDetailsModel(ad, null);

            // Then
            Assert.AreEqual(ad.Title, actual.Title);
            Assert.AreEqual(String.Empty, actual.BoatType);
            Assert.AreEqual(String.Empty, actual.HullType);
            Assert.AreEqual(null, actual.Year);
            Assert.AreEqual(String.Empty, actual.Length);
        }
    }
}
