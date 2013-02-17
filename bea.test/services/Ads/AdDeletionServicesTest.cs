using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Core.Dal;
using Bea.Core.Services;
using Bea.Domain;
using Bea.Domain.Ads;
using Bea.Domain.Reference;
using Bea.Domain.Search;
using Bea.Models.Delete;
using Bea.Services.Ads;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bea.Test.Services.Ads
{
    [TestClass]
    public class AdDeletionServicesTest
    {
        [TestMethod]
        public void DeleteAd_AdDoesNotExist_ReturnMessage()
        {
            // Given
            long adId = 56;

            var adRepoMock = new Moq.Mock<IAdRepository>();
            adRepoMock.Setup(r => r.CanDeleteAd(adId)).Returns(false);

            AdDeletionServices service = new AdDeletionServices(adRepoMock.Object, null, null);

            // When
            DeleteAdModel model = service.DeleteAd(adId);

            // Then
            Assert.AreEqual(adId, model.AdId);
            Assert.IsNull(model.Password);
            Assert.IsNull(model.SelectedDeletionReasonId);
            Assert.AreEqual(0, model.NbTry);
            Assert.AreEqual("Cette annonce n'existe pas ou plus.", model.InfoMessage);
            Assert.IsFalse(model.CanDeleteAd);
            Assert.IsFalse(model.IsDeleted);
        }

        [TestMethod]
        public void DeleteAd_AdIsAlreadyDeleted_ReturnMessage()
        {
            // Given
            long adId = 56;

            var adRepoMock = new Moq.Mock<IAdRepository>();
            adRepoMock.Setup(r => r.CanDeleteAd(adId)).Returns(false);

            AdDeletionServices service = new AdDeletionServices(adRepoMock.Object, null, null);

            // When
            DeleteAdModel model = service.DeleteAd(adId);

            // Then
            Assert.AreEqual(adId, model.AdId);
            Assert.IsNull(model.Password);
            Assert.IsNull(model.SelectedDeletionReasonId);
            Assert.AreEqual(0, model.NbTry);
            Assert.AreEqual("Cette annonce n'existe pas ou plus.", model.InfoMessage);
            Assert.IsFalse(model.CanDeleteAd);
            Assert.IsFalse(model.IsDeleted);
        }

        [TestMethod]
        public void DeleteAd_AdExists_ReturnFilledInModelForDeletion()
        {
            // Given
            long adId = 56;

            var adRepoMock = new Moq.Mock<IAdRepository>();
            adRepoMock.Setup(r => r.CanDeleteAd(adId)).Returns(true);

            AdDeletionServices service = new AdDeletionServices(adRepoMock.Object, null, null);

            // When
            DeleteAdModel model = service.DeleteAd(adId);

            // Then
            Assert.AreEqual(adId, model.AdId);
            Assert.IsNull(model.Password);
            Assert.IsNull(model.SelectedDeletionReasonId);
            Assert.AreEqual(0, model.NbTry);
            Assert.IsNull(model.InfoMessage);
            Assert.IsTrue(model.CanDeleteAd);
            Assert.IsFalse(model.IsDeleted);
        }

        [TestMethod]
        public void PerformDeleteAd_AdDoesNotExists_ReturnMessage()
        {
            // Given
            DeleteAdModel model = new DeleteAdModel
            {
                AdId = 56
            };

            var adRepoMock = new Moq.Mock<IAdRepository>();
            adRepoMock.Setup(r => r.GetAdById<BaseAd>(model.AdId)).Returns((BaseAd)null);

            AdDeletionServices service = new AdDeletionServices(adRepoMock.Object, null, null);

            // When
            DeleteAdModel result = service.DeleteAd(model);

            // Then
            Assert.AreEqual(model.AdId, result.AdId);
            Assert.IsNull(result.Password);
            Assert.IsNull(result.SelectedDeletionReasonId);
            Assert.AreEqual(0, result.NbTry);
            Assert.AreEqual("Cette annonce n'existe pas ou plus.", result.InfoMessage);
            Assert.IsFalse(result.CanDeleteAd);
            Assert.IsFalse(result.IsDeleted);
        }

        [TestMethod]
        public void PerformDeleteAd_AdsAlreadyDeleted_ReturnMessage()
        {
            // Given
            DeleteAdModel model = new DeleteAdModel
            {
                AdId = 56
            };
            BaseAd ad = new Ad() { IsDeleted = true };

            var adRepoMock = new Moq.Mock<IAdRepository>();
            adRepoMock.Setup(r => r.GetAdById<BaseAd>(model.AdId)).Returns(ad);

            AdDeletionServices service = new AdDeletionServices(adRepoMock.Object, null, null);

            // When
            DeleteAdModel result = service.DeleteAd(model);

            // Then
            Assert.AreEqual(model.AdId, result.AdId);
            Assert.IsNull(result.Password);
            Assert.IsNull(result.SelectedDeletionReasonId);
            Assert.AreEqual(0, result.NbTry);
            Assert.AreEqual("Cette annonce n'existe pas ou plus.", result.InfoMessage);
            Assert.IsFalse(result.CanDeleteAd);
            Assert.IsFalse(result.IsDeleted);
        }

        [TestMethod]
        public void PerformDeleteAd_AdExists_PasswordDoesNotMatch()
        {
            // Given
            DeleteAdModel model = new DeleteAdModel
            {
                AdId = 56,
                Password = "plaf",
                NbTry = 1,
                SelectedDeletionReasonId = 7
            };
            User creator = new User()
            {
                Password = "p^louf"
            };
            BaseAd ad = new Ad()
            {
                CreatedBy = creator
            };

            var adRepoMock = new Moq.Mock<IAdRepository>();
            adRepoMock.Setup(r => r.GetAdById<BaseAd>(model.AdId)).Returns(ad);

            AdDeletionServices service = new AdDeletionServices(adRepoMock.Object, null, null);

            // When
            DeleteAdModel result = service.DeleteAd(model);

            // Then
            Assert.AreEqual(model.AdId, result.AdId);
            Assert.IsNull(result.Password);
            Assert.AreEqual(7, result.SelectedDeletionReasonId);
            Assert.AreEqual(2, result.NbTry);
            Assert.IsNull(result.InfoMessage);
            Assert.IsTrue(result.CanDeleteAd);
            Assert.IsFalse(result.IsDeleted);
        }

        [TestMethod]
        public void PerformDeleteAd_AdExists_PasswordMatches_MarkAdAsDeletedAndRemoveFromSearchAdCache()
        {
            // Given
            DeleteAdModel model = new DeleteAdModel
            {
                AdId = 56,
                Password = "p^louf",
                NbTry = 1,
                SelectedDeletionReasonId = 7
            };
            DeletionReason dr = new DeletionReason() { Id = 7 };
            SearchAdCache adc = new SearchAdCache() { AdId = 56 };
            User creator = new User()
            {
                Password = "p^louf"
            };
            BaseAd ad = new Ad()
            {
                CreatedBy = creator
            };

            var adRepoMock = new Moq.Mock<IAdRepository>();
            adRepoMock.Setup(r => r.GetAdById<BaseAd>(model.AdId)).Returns(ad);

            var repoMock = new Moq.Mock<IRepository>();
            repoMock.Setup(x => x.Get<DeletionReason>(7)).Returns(dr);
            repoMock.Setup(x => x.Get<SearchAdCache>(model.AdId)).Returns(adc);
            repoMock.Setup(x => x.Save<BaseAd>(ad));
            repoMock.Setup(x => x.Delete<SearchAdCache>(adc));
            
            var helperMock = new Moq.Mock<IHelperService>();
            helperMock.Setup(x => x.GetCurrentDateTime()).Returns(new DateTime(2013, 01, 26));

            AdDeletionServices service = new AdDeletionServices(adRepoMock.Object, repoMock.Object, helperMock.Object);

            // When
            DeleteAdModel result = service.DeleteAd(model);

            // Then
            Assert.AreEqual(model.AdId, result.AdId);
            Assert.IsNull(result.Password);
            Assert.IsNull(result.SelectedDeletionReasonId);
            Assert.AreEqual(0, result.NbTry);
            Assert.IsNull(result.InfoMessage);
            Assert.IsFalse(result.CanDeleteAd);
            Assert.IsTrue(result.IsDeleted);

            Assert.AreEqual(new DateTime(2013, 01, 26), ad.DeletionDate);
            Assert.IsTrue(ad.IsDeleted);
            Assert.AreEqual(dr, ad.DeletedReason);
        }

    }
}
