using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Core.Dal;
using Bea.Domain.Location;
using Bea.Services;
using Bea.Test.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bea.Test.services
{
    [TestClass]
    public class AdServicesTest : DataAccessTestBase
    {
        [TestMethod]
        public void CountAdsByCities_CallAdRepository_ReturnResultOfRepo()
        {
            // Given
            IDictionary<City, int> result = new Dictionary<City, int>();
            var adRepoMock = new Moq.Mock<IAdRepository>();
            adRepoMock.Setup(r => r.CountAdsByCity()).Returns(result);

            AdServices service = new AdServices(adRepoMock.Object);

            // When
            IDictionary<City, int> actual = service.CountAdsByCities();

            // Then
            Assert.AreEqual(result, actual);
            adRepoMock.Verify();
        }
    }
}
