using System;
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
using Bea.Domain.Categories;
using Bea.Models.Create;
using Bea.Domain.Search;
using Bea.Core.Services.Ads;
using Bea.Services.Ads;

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

            AdServices service = new AdServices(adRepoMock.Object, null, null, null, null);

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

            AdServices service = new AdServices(adRepoMock.Object, helperMock.Object, null, null, null);

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

            AdServices service = new AdServices(adRepoMock.Object, helperMock.Object, null, null, null);

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

            AdServices service = new AdServices(adRepoMock.Object, helperMock.Object, null, null, null);

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

            AdServices service = new AdServices(adRepoMock.Object, helperMock.Object, null, null, null);

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

            AdServices service = new AdServices(adRepoMock.Object, helperMock.Object, null, null, null);

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

            AdServices service = new AdServices(adRepoMock.Object, helperMock.Object, null, null, null);

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

            AdServices service = new AdServices(adRepoMock.Object, null, null, null, null);

            // When
            AdDetailsModel actual = service.GetAdDetails(17);

            // Then
            Assert.IsNull(actual);
        }

        [TestMethod]
        public void GetAdFromModel_AdModel_ReturnsAd()
        {
            // Given
            var repoMock = new Moq.Mock<IRepository>();
            Category category = new Category() { Id = 17, Label = "Kite" };
            repoMock.Setup(r => r.Get<Category>(17)).Returns(category);

            var userServicesMock = new Moq.Mock<IUserServices>();
            string email = "bruno.deprez@gmail.com";
            User user = new User() { Email = email };
            userServicesMock.Setup(r => r.GetUserFromEmail(email)).Returns(user);

            var activationServiceMock = new Moq.Mock<IAdActivationServices>();
            activationServiceMock.Setup(x => x.GenerateActivationToken()).Returns("activationToken");

            AdServices service = new AdServices(null, null, repoMock.Object, userServicesMock.Object, activationServiceMock.Object);

            // When
            AdCreateModel model = new AdCreateModel() { SelectedCategoryId = 17 };
            BaseAd ad = service.GetAdFromModel(model, null);

            // Then
            Assert.IsTrue(ad is Ad);
        }

        [TestMethod]
        public void GetAdFromModel_NoCategory_ReturnsAd()
        {
            // Given
            var repoMock = new Moq.Mock<IRepository>();

            var userServicesMock = new Moq.Mock<IUserServices>();
            string email = "bruno.deprez@gmail.com";
            User user = new User() { Email = email };
            userServicesMock.Setup(r => r.GetUserFromEmail(email)).Returns(user);

            var activationServiceMock = new Moq.Mock<IAdActivationServices>();
            activationServiceMock.Setup(x => x.GenerateActivationToken()).Returns("activationToken");

            AdServices service = new AdServices(null, null, repoMock.Object, userServicesMock.Object, activationServiceMock.Object);

            // When
            AdCreateModel model = new AdCreateModel() { SelectedCategoryId = null };
            BaseAd ad = service.GetAdFromModel(model, null);

            // Then
            Assert.IsTrue(ad is Ad);
        }

        [TestMethod]
        public void GetAdFromModel_CarAdModel_ReturnsCarAd()
        {
            // Given
            var repoMock = new Moq.Mock<IRepository>();
            Category category = new Category() { Id = 17, Label = "Voitures" };
            repoMock.Setup(r => r.Get<Category>(17)).Returns(category);

            var userServicesMock = new Moq.Mock<IUserServices>();
            string email = "bruno.deprez@gmail.com";
            User user = new User() { Email = email };
            userServicesMock.Setup(r => r.GetUserFromEmail(email)).Returns(user);

            var activationServiceMock = new Moq.Mock<IAdActivationServices>();
            activationServiceMock.Setup(x => x.GenerateActivationToken()).Returns("activationToken");

            AdServices service = new AdServices(null, null, repoMock.Object, userServicesMock.Object, activationServiceMock.Object);

            // When
            Dictionary<string, string> form = new Dictionary<string, string>();
            form.Add("Type", "10");
            form.Add("SelectedYearId", "");
            form.Add("Km", "");
            form.Add("SelectedFuelId", "");
            form.Add("SelectedBrandId", "");

            AdCreateModel model = new AdCreateModel() { SelectedCategoryId = 17 };
            BaseAd ad = service.GetAdFromModel(model, form);

            // Then
            Assert.IsTrue(ad is CarAd);

        }

        [TestMethod]
        public void GetAdPicturesFromModel_NoPictures_ReturnAdWithoutPictures()
        {
            // Given
            BaseAd ad = new Ad();
            String images = null;

            AdServices service = new AdServices(null, null, null, null, null);

            // When
            
            BaseAd result = service.GetAdPicturesFromModel(ad, images);

            // Then
            Assert.AreEqual(ad, result);
            Assert.AreEqual(0, ad.Images.Count);
        }

        [TestMethod]
        public void GetAdPicturesFromModel_2Pictures_FetchThemFromRepoAndSetFirstAsMainImage()
        {
            // Given
            BaseAd ad = new Ad();
            String images = "b9da8b1e-aa77-401b-84e0-a1290130b7b7;b9da8b1e-aa77-401b-84e0-a1290130b999;";

            AdImage img1 = new AdImage() { Id = Guid.Parse("b9da8b1e-aa77-401b-84e0-a1290130b7b7") };
            AdImage img2 = new AdImage() { Id = Guid.Parse("b9da8b1e-aa77-401b-84e0-a1290130b999") };

            var repoMock = new Moq.Mock<IRepository>();
            repoMock.Setup(x => x.Get<AdImage>(Guid.Parse("b9da8b1e-aa77-401b-84e0-a1290130b7b7"))).Returns(img1);
            repoMock.Setup(x => x.Get<AdImage>(Guid.Parse("b9da8b1e-aa77-401b-84e0-a1290130b999"))).Returns(img2);

            AdServices service = new AdServices(null, null, repoMock.Object, null, null);

            // When
            BaseAd result = service.GetAdPicturesFromModel(ad, images);

            // Then
            Assert.AreEqual(ad, result);
            Assert.AreEqual(2, ad.Images.Count);
            Assert.AreEqual("b9da8b1e-aa77-401b-84e0-a1290130b7b7", ad.Images[0].Id.ToString());
            Assert.AreEqual(ad, ad.Images[0].BaseAd);
            Assert.IsTrue(ad.Images[0].IsPrimary);
            Assert.AreEqual("b9da8b1e-aa77-401b-84e0-a1290130b999", ad.Images[1].Id.ToString());
            Assert.AreEqual(ad, ad.Images[1].BaseAd);
            Assert.IsFalse(ad.Images[1].IsPrimary);
        }
    }
}
