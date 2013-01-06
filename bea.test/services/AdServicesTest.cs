﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Core.Dal;
using Bea.Core.Services;
using Bea.Domain;
using Bea.Domain.Ads;
using Bea.Domain.Location;
using Bea.Models;
using Bea.Models.Details;
using Bea.Services;
using Bea.Test.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bea.Test.services
{
    [TestClass]
    public class AdServicesTest
    {
        [TestMethod]
        public void CountAdsByCities_CallAdRepository_ReturnResultOfRepo()
        {
            // Given
            IDictionary<City, int> result = new Dictionary<City, int>();
            var adRepoMock = new Moq.Mock<IAdRepository>();
            adRepoMock.Setup(r => r.CountAdsByCity()).Returns(result);

            AdServices service = new AdServices(adRepoMock.Object, null,null,null);

            // When
            IDictionary<City, int> actual = service.CountAdsByCities();

            // Then
            Assert.AreEqual(result, actual);
            adRepoMock.Verify();
        }

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

            AdServices service = new AdServices(adRepoMock.Object, helperMock.Object, null,null);

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

            AdServices service = new AdServices(adRepoMock.Object, helperMock.Object, null,null);

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

            AdServices service = new AdServices(adRepoMock.Object, helperMock.Object, null,null);

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

            AdServices service = new AdServices(adRepoMock.Object, helperMock.Object, null,null);

            // When
            AdDetailsModel actual = service.GetAdDetails(17);

            // Then
            Assert.AreEqual(17, actual.AdId);
            Assert.IsTrue(actual is CarAdDetailsModel);
        }

        [TestMethod]
        public void GetAdDetails_AdDoesNotExist_ReturnNull()
        {
            // Given
            var adRepoMock = new Moq.Mock<IAdRepository>();
            adRepoMock.Setup(r => r.GetAdType(17)).Returns(AdTypeEnum.Undefined);

            AdServices service = new AdServices(adRepoMock.Object, null, null,null);

            // When
            AdDetailsModel actual = service.GetAdDetails(17);

            // Then
            Assert.IsNull(actual);
        }

        //[TestMethod]
        //public void GetAdFromModel_NoCitySelected_PutNullInAdCity()
        //{
        //    BaseAd ad = new Ad();
        //    City city = new City() { Id = 17 };
        //    var repoMock = new Moq.Mock<IRepository>();
        //    repoMock.Setup(r => r.Get<City>(17)).Returns(city);



        //    AdServices service = new AdServices(null, null, repoMock.Object, null);

        //}

    }
}
