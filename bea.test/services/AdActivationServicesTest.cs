using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Core.Dal;
using Bea.Domain.Ads;
using Bea.Domain.Categories;
using Bea.Domain.Location;
using Bea.Domain.Search;
using Bea.Models.Create;
using Bea.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bea.Test.Services
{
    [TestClass]
    public class AdActivationServicesTest
    {
        [TestMethod]
        public void ActivateAd_AdDoesNotExist_ReturnModelIsActivatedFalse()
        {
            // Given
            BaseAd ad = null;

            var repoMock = new Moq.Mock<IRepository>();
            repoMock.Setup(x => x.Get<BaseAd>(78)).Returns(ad);

            AdActivationServices service = new AdActivationServices(repoMock.Object);

            // When
            AdActivationResultModel model = service.ActivateAd(78, null);

            // Then
            Assert.IsFalse(model.IsActivated);
            Assert.AreEqual(78, model.AdId);
            Assert.AreEqual("Cette annonce n'existe pas ou a expiré.", model.InfoMessage);

            repoMock.Verify(x => x.Save(ad), Moq.Times.Never());
            repoMock.Verify(x => x.Save(Moq.It.IsAny<SearchAdCache>()), Moq.Times.Never());
        }

        [TestMethod]
        public void ActivateAd_AdIsAlreadyActivated_ReturnModelIsActivatedTrue()
        {
            // Given
            BaseAd ad = new Ad()
            {
                IsActivated = true
            };
            long adId = 78;

            var repoMock = new Moq.Mock<IRepository>();
            repoMock.Setup(x => x.Get<BaseAd>(adId)).Returns(ad);

            AdActivationServices service = new AdActivationServices(repoMock.Object);

            // When
            AdActivationResultModel model = service.ActivateAd(78, null);

            // Then
            Assert.IsTrue(model.IsActivated);
            Assert.AreEqual(78, model.AdId);
            Assert.AreEqual("Cette annonce a déjà été activée.", model.InfoMessage);

            repoMock.Verify(x => x.Save(ad), Moq.Times.Never());
            repoMock.Verify(x => x.Save(Moq.It.IsAny<SearchAdCache>()), Moq.Times.Never());
        }

        [TestMethod]
        public void ActivateAd_ActivationTokenDoesNotWork_ReturnModelIsActivatedFalse()
        {
            // Given
            BaseAd ad = new Ad()
            {
                IsActivated = false,
                ActivationToken = "AAA"
            };
            long adId = 78;

            var repoMock = new Moq.Mock<IRepository>();
            repoMock.Setup(x => x.Get<BaseAd>(adId)).Returns(ad);

            AdActivationServices service = new AdActivationServices(repoMock.Object);

            // When
            AdActivationResultModel model = service.ActivateAd(adId, "BBB");

            // Then
            Assert.IsFalse(model.IsActivated);
            Assert.AreEqual(78, model.AdId);
            Assert.AreEqual("Vous ne pouvez pas activer cette annonce.", model.InfoMessage);

            repoMock.Verify(x => x.Save(ad), Moq.Times.Never());
            repoMock.Verify(x => x.Save(Moq.It.IsAny<SearchAdCache>()), Moq.Times.Never());
        }

        [TestMethod]
        public void ActivateAd_ActivationTokenWorks_ReturnModelIsActivatedTrueAndActivatesAd()
        {
            // Given
            BaseAd ad = new Ad()
            {
                IsActivated = false,
                ActivationToken = "AAA",
                City = new City(),
                Category = new Category()
            };
            long adId = 78;

            var repoMock = new Moq.Mock<IRepository>();
            repoMock.Setup(x => x.Get<BaseAd>(adId)).Returns(ad);

            AdActivationServices service = new AdActivationServices(repoMock.Object);

            // When
            AdActivationResultModel model = service.ActivateAd(78, "AAA");

            // Then
            Assert.IsTrue(model.IsActivated);
            Assert.AreEqual(78, model.AdId);
            Assert.AreEqual("Merci d'avoir activé votre annonce.", model.InfoMessage);

            repoMock.Verify(x => x.Save(ad), Moq.Times.Once());
            repoMock.Verify(x => x.Save(Moq.It.IsAny<SearchAdCache>()), Moq.Times.Once());
        }
    }
}
