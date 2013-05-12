using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Models.Search;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bea.Test.Models.Search
{
    [TestClass]
    public class AdHomeSearchResultModelTest
    {
        [TestMethod]
        public void AdHomeSearchResultModel_ctor_callparentctor()
        {
            // Given
            AdSearchModel sM = new AdSearchModel();
            sM.SearchString = "prout";

            // When
            AdHomeSearchResultModel result = new AdHomeSearchResultModel(sM);

            // Then
            Assert.AreEqual(0, result.Results.Count);
            Assert.AreEqual("prout", result.SearchString);
        }
    }
}
