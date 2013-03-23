using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain.Reference;
using Bea.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bea.Test.Services
{
    [TestClass]
    public class ReferenceServicesTest
    {
        [TestMethod]
        public void ConstructorTestAgeBracket()
        {
            // When
            ReferenceServices service = new ReferenceServices(null);

            // Then
            IDictionary<int, BracketItemReference> result = service.GetAllAgeBrackets();
            Assert.AreEqual("jusqu'à 2 ans", result[1].Label);
            Assert.AreEqual(0, result[1].LowValue);
            Assert.AreEqual(3, result[1].HighValue);
            Assert.AreEqual("2 - 5 ans", result[2].Label);
            Assert.AreEqual(1, result[2].LowValue);
            Assert.AreEqual(6, result[2].HighValue);
            Assert.AreEqual("5 - 10 ans", result[3].Label);
            Assert.AreEqual(4, result[3].LowValue);
            Assert.AreEqual(11, result[3].HighValue);
            Assert.AreEqual("10 ans et plus", result[4].Label);
            Assert.AreEqual(9, result[4].LowValue);
            Assert.AreEqual(200, result[4].HighValue);
        }

        [TestMethod]
        public void ConstructorTestKmsBracket()
        {
            // When
            ReferenceServices service = new ReferenceServices(null);

            // Then
            IDictionary<int, BracketItemReference> result = service.GetAllKmBrackets();
            Assert.AreEqual("jusqu'à 5000 Km", result[1].Label);
            Assert.AreEqual(0, result[1].LowValue);
            Assert.AreEqual(5999, result[1].HighValue);
            Assert.AreEqual("5000 - 10000 Km", result[2].Label);
            Assert.AreEqual(4500, result[2].LowValue);
            Assert.AreEqual(10999, result[2].HighValue);
            Assert.AreEqual("10000 - 20000 Km", result[3].Label);
            Assert.AreEqual(9500, result[3].LowValue);
            Assert.AreEqual(20999, result[3].HighValue);
            Assert.AreEqual("20000 - 50000 Km", result[4].Label);
            Assert.AreEqual(19500, result[4].LowValue);
            Assert.AreEqual(50999, result[4].HighValue);
            Assert.AreEqual("50000 Km et plus", result[5].Label);
            Assert.AreEqual(49500, result[5].LowValue);
            Assert.AreEqual(1000000, result[5].HighValue);
        }
    }
}
