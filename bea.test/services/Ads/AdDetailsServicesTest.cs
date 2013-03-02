using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Core.Dal;
using Bea.Core.Services;
using Bea.Domain;
using Bea.Domain.Ads;
using Bea.Domain.Ads.WaterSport;
using Bea.Domain.Location;
using Bea.Models.Details;
using Bea.Models.Details.Vehicles;
using Bea.Models.Details.WaterSport;
using Bea.Services.Ads;
using Bea.Tools;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bea.Test.Services.Ads
{
    [TestClass]
    public class AdDetailsServicesTest
    {
        [TestMethod]
        public void GetAdDetails_AdIsNotNew_GetAdFromRepoAndPopulateIsNewFalse()
        {
            // Given
            Ad ad = new Ad();
            ad.CreationDate = new DateTime(2012, 02, 05);
            ad.CreatedBy = new User { Firstname = "Michel" };
            ad.City = new City { Label = "Ville" };

            var adRepoMock = new Moq.Mock<IAdRepository>();
            adRepoMock.Setup(r => r.GetAdType(17)).Returns(AdTypeEnum.Ad);
            adRepoMock.Setup(r => r.GetAdById<Ad>(17)).Returns(ad);

            var helperMock = new Moq.Mock<IHelperService>();
            helperMock.Setup(s => s.GetCurrentDateTime()).Returns(new DateTime(2012, 02, 20));

            AdDetailsServices service = new AdDetailsServices(adRepoMock.Object, helperMock.Object);

            // When
            AdDetailsModel actual = service.GetAdDetails(17);

            // Then
            Assert.IsFalse(actual.IsNew);
        }

        [TestMethod]
        public void GetAdDetails_AdIsNew_GetAdFromRepoAndPopulateIsNewTrue()
        {
            // Given
            Ad ad = new Ad();
            ad.CreationDate = new DateTime(2012, 02, 18);
            ad.CreatedBy = new User { Firstname = "Michel" };
            ad.City = new City { Label = "Ville" };

            var adRepoMock = new Moq.Mock<IAdRepository>();
            adRepoMock.Setup(r => r.GetAdType(17)).Returns(AdTypeEnum.Ad);
            adRepoMock.Setup(r => r.GetAdById<Ad>(17)).Returns(ad);

            var helperMock = new Moq.Mock<IHelperService>();
            helperMock.Setup(s => s.GetCurrentDateTime()).Returns(new DateTime(2012, 02, 20));

            AdDetailsServices service = new AdDetailsServices(adRepoMock.Object, helperMock.Object);

            // When
            AdDetailsModel actual = service.GetAdDetails(17);

            // Then
            Assert.IsTrue(actual.IsNew);
        }

        [TestMethod]
        public void GetAdDetails_AdExists_GetAdFromRepoAndReturnAdModel()
        {
            // Given
            Ad ad = new Ad() { Id = 17 };
            ad.CreationDate = new DateTime(2012, 02, 18);
            ad.CreatedBy = new User { Firstname = "Michel" };
            ad.City = new City { Label = "Ville" };

            var repoMock = new Moq.Mock<IRepository>();
            repoMock.Setup(x => x.Get<BaseAd>(17)).Returns(ad as BaseAd);

            var adRepoMock = new Moq.Mock<IAdRepository>();
            adRepoMock.Setup(r => r.GetAdType(17)).Returns(AdTypeEnum.Ad);
            adRepoMock.Setup(r => r.GetAdById<Ad>(17)).Returns(ad);

            var helperMock = new Moq.Mock<IHelperService>();
            helperMock.Setup(s => s.GetCurrentDateTime()).Returns(new DateTime(2012, 02, 20));

            AdDetailsServices service = new AdDetailsServices(adRepoMock.Object, helperMock.Object);

            // When
            AdDetailsModel actual = service.GetAdDetails(17);

            // Then
            Assert.AreEqual(17, actual.AdId);
        }

        [TestMethod]
        public void GetAdDetails_CarAdExists_GetAdFromRepoAndReturnCarAdModel()
        {
            // Given
            CarAd ad = new CarAd() { Id = 17 };
            ad.CreationDate = new DateTime(2012, 02, 18);
            ad.CreatedBy = new User { Firstname = "Michel" };
            ad.City = new City { Label = "Ville" };

            var repoMock = new Moq.Mock<IRepository>();
            repoMock.Setup(x => x.Get<BaseAd>(17)).Returns(ad as BaseAd);

            var adRepoMock = new Moq.Mock<IAdRepository>();
            adRepoMock.Setup(r => r.GetAdType(17)).Returns(AdTypeEnum.CarAd);
            adRepoMock.Setup(r => r.GetAdById<CarAd>(17)).Returns(ad);

            var helperMock = new Moq.Mock<IHelperService>();
            helperMock.Setup(s => s.GetCurrentDateTime()).Returns(new DateTime(2012, 02, 20));

            AdDetailsServices service = new AdDetailsServices(adRepoMock.Object, helperMock.Object);

            // When
            AdDetailsModel actual = service.GetAdDetails(17);

            // Then
            Assert.AreEqual(17, actual.AdId);
            Assert.IsTrue(actual is CarAdDetailsModel);
        }

        [TestMethod]
        public void GetAdDetails_MotoAdExists_GetAdFromRepoAndReturnMotoAdModel()
        {
            // Given
            MotoAd ad = new MotoAd() { Id = 17 };
            ad.CreationDate = new DateTime(2012, 02, 18);
            ad.CreatedBy = new User { Firstname = "Michel" };
            ad.City = new City { Label = "Ville" };

            var repoMock = new Moq.Mock<IRepository>();
            repoMock.Setup(x => x.Get<BaseAd>(17)).Returns(ad as BaseAd);

            var adRepoMock = new Moq.Mock<IAdRepository>();
            adRepoMock.Setup(r => r.GetAdType(17)).Returns(AdTypeEnum.MotoAd);
            adRepoMock.Setup(r => r.GetAdById<MotoAd>(17)).Returns(ad);

            var helperMock = new Moq.Mock<IHelperService>();
            helperMock.Setup(s => s.GetCurrentDateTime()).Returns(new DateTime(2012, 02, 20));

            AdDetailsServices service = new AdDetailsServices(adRepoMock.Object, helperMock.Object);

            // When
            AdDetailsModel actual = service.GetAdDetails(17);

            // Then
            Assert.AreEqual(17, actual.AdId);
            Assert.IsTrue(actual is MotoAdDetailsModel);
        }

        [TestMethod]
        public void GetAdDetails_OtherVehicleAdExists_GetAdFromRepoAndReturnMotoAdModel()
        {
            // Given
            OtherVehicleAd ad = new OtherVehicleAd() { Id = 17 };
            ad.CreationDate = new DateTime(2012, 02, 18);
            ad.CreatedBy = new User { Firstname = "Michel" };
            ad.City = new City { Label = "Ville" };

            var repoMock = new Moq.Mock<IRepository>();
            repoMock.Setup(x => x.Get<BaseAd>(17)).Returns(ad as BaseAd);

            var adRepoMock = new Moq.Mock<IAdRepository>();
            adRepoMock.Setup(r => r.GetAdType(17)).Returns(AdTypeEnum.OtherVehiculeAd);
            adRepoMock.Setup(r => r.GetAdById<OtherVehicleAd>(17)).Returns(ad);

            var helperMock = new Moq.Mock<IHelperService>();
            helperMock.Setup(s => s.GetCurrentDateTime()).Returns(new DateTime(2012, 02, 20));

            AdDetailsServices service = new AdDetailsServices(adRepoMock.Object, helperMock.Object);

            // When
            AdDetailsModel actual = service.GetAdDetails(17);

            // Then
            Assert.AreEqual(17, actual.AdId);
            Assert.IsTrue(actual is OtherVehicleAdDetailsModel);
        }

        [TestMethod]
        public void GetAdDetails_AdDoesNotExist_ReturnNull()
        {
            // Given
            var adRepoMock = new Moq.Mock<IAdRepository>();
            adRepoMock.Setup(r => r.GetAdType(17)).Returns(AdTypeEnum.Undefined);

            AdDetailsServices service = new AdDetailsServices(adRepoMock.Object, null);

            // When
            AdDetailsModel actual = service.GetAdDetails(17);

            // Then
            Assert.IsNull(actual);
        }

        [TestMethod]
        public void GetAdDetails_MotorBoatAdExists_GetAdFromRepoAndReturnMotoAdModel()
        {
            // Given
            MotorBoatAd ad = new MotorBoatAd() { Id = 17 };
            ad.CreationDate = new DateTime(2012, 02, 18);
            ad.CreatedBy = new User { Firstname = "Michel" };
            ad.City = new City { Label = "Ville" };

            var repoMock = new Moq.Mock<IRepository>();
            repoMock.Setup(x => x.Get<BaseAd>(17)).Returns(ad as BaseAd);

            var adRepoMock = new Moq.Mock<IAdRepository>();
            adRepoMock.Setup(r => r.GetAdType(17)).Returns(AdTypeEnum.MotorBoatAd);
            adRepoMock.Setup(r => r.GetAdById<MotorBoatAd>(17)).Returns(ad);

            var helperMock = new Moq.Mock<IHelperService>();
            helperMock.Setup(s => s.GetCurrentDateTime()).Returns(new DateTime(2012, 02, 20));

            AdDetailsServices service = new AdDetailsServices(adRepoMock.Object, helperMock.Object);

            // When
            AdDetailsModel actual = service.GetAdDetails(17);

            // Then
            Assert.AreEqual(17, actual.AdId);
            Assert.IsTrue(actual is MotorBoatAdDetailsModel);
        }

        [TestMethod]
        public void GetAdDetails_SailingBoatAdExists_GetAdFromRepoAndReturnMotoAdModel()
        {
            // Given
            SailingBoatAd ad = new SailingBoatAd() { Id = 17 };
            ad.CreationDate = new DateTime(2012, 02, 18);
            ad.CreatedBy = new User { Firstname = "Michel" };
            ad.City = new City { Label = "Ville" };

            var repoMock = new Moq.Mock<IRepository>();
            repoMock.Setup(x => x.Get<BaseAd>(17)).Returns(ad as BaseAd);

            var adRepoMock = new Moq.Mock<IAdRepository>();
            adRepoMock.Setup(r => r.GetAdType(17)).Returns(AdTypeEnum.SailingBoatAd);
            adRepoMock.Setup(r => r.GetAdById<SailingBoatAd>(17)).Returns(ad);

            var helperMock = new Moq.Mock<IHelperService>();
            helperMock.Setup(s => s.GetCurrentDateTime()).Returns(new DateTime(2012, 02, 20));

            AdDetailsServices service = new AdDetailsServices(adRepoMock.Object, helperMock.Object);

            // When
            AdDetailsModel actual = service.GetAdDetails(17);

            // Then
            Assert.AreEqual(17, actual.AdId);
            Assert.IsTrue(actual is SailingBoatAdDetailsModel);
        }

        [TestMethod]
        public void GetAdDetails_MotorBoatEngineAdExists_GetAdFromRepoAndReturnMotoAdModel()
        {
            // Given
            MotorBoatEngineAd ad = new MotorBoatEngineAd() { Id = 17 };
            ad.CreationDate = new DateTime(2012, 02, 18);
            ad.CreatedBy = new User { Firstname = "Michel" };
            ad.City = new City { Label = "Ville" };

            var repoMock = new Moq.Mock<IRepository>();
            repoMock.Setup(x => x.Get<BaseAd>(17)).Returns(ad as BaseAd);

            var adRepoMock = new Moq.Mock<IAdRepository>();
            adRepoMock.Setup(r => r.GetAdType(17)).Returns(AdTypeEnum.MotorBoatEngineAd);
            adRepoMock.Setup(r => r.GetAdById<MotorBoatEngineAd>(17)).Returns(ad);

            var helperMock = new Moq.Mock<IHelperService>();
            helperMock.Setup(s => s.GetCurrentDateTime()).Returns(new DateTime(2012, 02, 20));

            AdDetailsServices service = new AdDetailsServices(adRepoMock.Object, helperMock.Object);

            // When
            AdDetailsModel actual = service.GetAdDetails(17);

            // Then
            Assert.AreEqual(17, actual.AdId);
            Assert.IsTrue(actual is MotorBoatEngineAdDetailsModel);
        }

        [TestMethod]
        public void GetAdDetails_WaterSportAdExists_GetAdFromRepoAndReturnMotoAdModel()
        {
            // Given
            WaterSportAd ad = new WaterSportAd() { Id = 17 };
            ad.CreationDate = new DateTime(2012, 02, 18);
            ad.CreatedBy = new User { Firstname = "Michel" };
            ad.City = new City { Label = "Ville" };

            var repoMock = new Moq.Mock<IRepository>();
            repoMock.Setup(x => x.Get<BaseAd>(17)).Returns(ad as BaseAd);

            var adRepoMock = new Moq.Mock<IAdRepository>();
            adRepoMock.Setup(r => r.GetAdType(17)).Returns(AdTypeEnum.WaterSportAd);
            adRepoMock.Setup(r => r.GetAdById<WaterSportAd>(17)).Returns(ad);

            var helperMock = new Moq.Mock<IHelperService>();
            helperMock.Setup(s => s.GetCurrentDateTime()).Returns(new DateTime(2012, 02, 20));

            AdDetailsServices service = new AdDetailsServices(adRepoMock.Object, helperMock.Object);

            // When
            AdDetailsModel actual = service.GetAdDetails(17);

            // Then
            Assert.AreEqual(17, actual.AdId);
            Assert.IsTrue(actual is WaterSportAdDetailsModel);
        }
    }
}
