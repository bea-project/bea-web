using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Core.Dal;
using Bea.Core.Services;
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
            DateTime d = new DateTime(2012, 01, 17);

            Guid guidId = new Guid("14a5e994-fd5d-4a32-859d-a152013ad860");
            var repoMock = new Moq.Mock<IRepository>();
            repoMock.Setup(x => x.Save<AdImage>(Moq.It.IsAny<AdImage>()));

            var helperMock = new Moq.Mock<IHelperService>();
            helperMock.Setup(x => x.GetCurrentDateTime()).Returns(d);

            AdImageServices service = new AdImageServices(repoMock.Object, helperMock.Object);

            // Then
            AdImage actual = service.StoreImage(guidId, true);

            Assert.AreEqual(guidId, actual.Id);
            Assert.IsTrue(actual.IsPrimary);
            Assert.AreEqual(d, actual.UploadedDate);
        }
    }
}
