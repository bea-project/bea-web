using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            IDictionary<int, String> result = service.GetAllAgeBrackets();
            Assert.AreEqual("jusqu'à 2 ans", result[1]);
            Assert.AreEqual("2 - 5 ans", result[2]);
            Assert.AreEqual("5 - 10 ans", result[3]);
            Assert.AreEqual("10 ans et plus", result[4]);
        }

        [TestMethod]
        public void ConstructorTestKmsBracket()
        {
            // When
            ReferenceServices service = new ReferenceServices(null);

            // Then
            IDictionary<int, String> result = service.GetAllKmBrackets();
            Assert.AreEqual("jusqu'à 5000 Km", result[1]);
            Assert.AreEqual("5000 - 10000 Km", result[2]);
            Assert.AreEqual("10000 - 20000 Km", result[3]);
            Assert.AreEqual("20000 - 50000 Km", result[4]);
            Assert.AreEqual("50000 Km et plus", result[5]);
        }
    }
}
