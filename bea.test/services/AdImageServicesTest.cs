using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Core.Dal;
using Bea.Domain;
using Bea.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bea.Test.Services
{
    [TestClass]
    public class AdImageServicesTest
    {
        [TestMethod]
        public void StoreImage_SavesImageInDbAndReturnCreatedIdentifier()
        {
            // Given
            var repoMock = new Moq.Mock<IRepository>();
            repoMock.Setup(x => x.Save<AdImage, int>(Moq.It.IsAny<AdImage>())).Returns(9);

            AdImageServices service = new AdImageServices(repoMock.Object);

            // Then
            Assert.AreEqual(9, service.StoreImage("coucou", new byte[10]));
        }

        [TestMethod]
        public void GetAdImage_GetLargeImage_ReturnLargeImage()
        {
            // Given
            String guidString = "e9da8b1e-aa77-401b-84e0-a1290130b7b7";
            Guid g = Guid.Parse(guidString);

            AdImage img = new AdImage();
            img.ImageBytes = new byte[10];

            var repoMock = new Moq.Mock<IRepository>();
            repoMock.Setup(x => x.Get<AdImage>(g)).Returns(img);

            AdImageServices service = new AdImageServices(repoMock.Object);

            // Then
            Assert.AreEqual(img.ImageBytes, service.GetAdImage(guidString, false));
        }

        [TestMethod]
        public void GetAdImage_GetThumbnailImage_ReturnThumbnailImage()
        {
            // Given
            String guidString = "e9da8b1e-aa77-401b-84e0-a1290130b7b7";
            Guid g = Guid.Parse(guidString);

            AdImage img = new AdImage();
            img.ImageThumbnailBytes = new byte[10];

            var repoMock = new Moq.Mock<IRepository>();
            repoMock.Setup(x => x.Get<AdImage>(g)).Returns(img);

            AdImageServices service = new AdImageServices(repoMock.Object);

            // Then
            Assert.AreEqual(img.ImageThumbnailBytes, service.GetAdImage(guidString, true));
        }

        [TestMethod]
        public void GetAdImage_NoImage_ReturnNull()
        {
            // Given
            String guidString = "e9da8b1e-aa77-401b-84e0-a1290130b7b7";
            Guid g = Guid.Parse(guidString);

            var repoMock = new Moq.Mock<IRepository>();
            repoMock.Setup(x => x.Get<AdImage>(g)).Returns((AdImage)null);

            AdImageServices service = new AdImageServices(repoMock.Object);

            // Then
            Assert.IsNull(service.GetAdImage(guidString, true));
        }
    }
}
