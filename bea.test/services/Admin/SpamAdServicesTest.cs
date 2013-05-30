using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Core.Dal;
using Bea.Domain.Admin;
using Bea.Domain.Ads;
using Bea.Domain.Reference;
using Bea.Models.Request;
using Bea.Services.Admin;
using Bea.Tools;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bea.Test.Services.Admin
{
    [TestClass]
    public class SpamAdServicesTest
    {
        [TestMethod]
        public void CanSpamRequestAd_AdDoesNotExist_ReturnTrueWithMessage()
        {
            // Given
            var adRepoMock = new Moq.Mock<IAdRepository>();
            adRepoMock.Setup(x => x.CanDeleteAd(7)).Returns(false);

            SpamAdServices service = new SpamAdServices(adRepoMock.Object, null, null);

            // When
            SpamAdRequestModel result = service.CanSpamRequestAd(7);
            
            // Then
            Assert.IsFalse(result.CanSignal);
            Assert.AreEqual("Cette annonce n'existe pas ou plus.", result.InfoMessage);
        }

        [TestMethod]
        public void CanSpamRequestAd_AdCanBeRequested_ReturnFalseWithContent()
        {
            // Given            
            var adRepoMock = new Moq.Mock<IAdRepository>();
            adRepoMock.Setup(x => x.CanDeleteAd(7)).Returns(true);

            SpamAdServices service = new SpamAdServices(adRepoMock.Object, null, null);

            // When
            SpamAdRequestModel result = service.CanSpamRequestAd(7);

            // Then
            Assert.AreEqual(7, result.AdId);
            Assert.IsNull(result.SelectedSpamAdTypeId);
            Assert.IsNull(result.RequestorEmail);
            Assert.IsNull(result.Description);
            Assert.IsTrue(result.CanSignal);
            Assert.IsNull(result.InfoMessage);
        }

        [TestMethod]
        public void SpamRequestAd_AdDoesNotExist_ReturnInfoMessage()
        {
            // Given
            var adRepoMock = new Moq.Mock<IAdRepository>();
            adRepoMock.Setup(x => x.CanDeleteAd(7)).Returns(false);

            SpamAdRequestModel model = new SpamAdRequestModel();
            model.AdId = 7;

            SpamAdServices service = new SpamAdServices(adRepoMock.Object, null, null);

            // When
            SpamAdRequestModel result = service.SpamRequestAd(model);

            // Then
            Assert.IsFalse(result.CanSignal);
            Assert.AreEqual("Cette annonce n'existe pas ou plus.", result.InfoMessage);
        }

        [TestMethod]
        public void SpamRequestAd_AdExists_SaveSpamRequest()
        {
            // Given
            var adRepoMock = new Moq.Mock<IAdRepository>();
            adRepoMock.Setup(x => x.CanDeleteAd(7)).Returns(true);

            var repoMock = new Moq.Mock<IRepository>();
            repoMock.Setup(x => x.Get<SpamAdType>(3)).Returns(new SpamAdType() { Id = 3 });

            var hSMock = new Moq.Mock<IHelperService>();
            hSMock.Setup(x => x.GetCurrentDateTime()).Returns(new DateTime(2013, 05, 17, 6, 7, 22));

            SpamAdRequestModel model = new SpamAdRequestModel();
            model.AdId = 7;
            model.Description = "description";
            model.RequestorEmail = "addresse@mail.com";
            model.SelectedSpamAdTypeId = 3;

            SpamAdServices service = new SpamAdServices(adRepoMock.Object, repoMock.Object, hSMock.Object);

            // When
            SpamAdRequestModel result = service.SpamRequestAd(model);

            // Then
            Assert.IsFalse(result.CanSignal);
            Assert.AreEqual("Votre signalement a correctement été transmis. Merci de votre précieuse aide dans la chasse aux mauvaises annonces !", result.InfoMessage);

            repoMock.Verify(x => x.Save(Moq.It.Is<SpamAdRequest>(b => 
                b.Description == model.Description 
                && b.RequestDate == new DateTime(2013, 05, 17, 6, 7, 22)
                && b.RequestorEmailAddress == model.RequestorEmail
                && b.SpamType.Id == 3)));
        }
    }
}
