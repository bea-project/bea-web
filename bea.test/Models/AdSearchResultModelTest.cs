using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bea.Models.Search;

namespace Bea.Test.Models
{
    [TestClass]
    public class AdSearchResultModelTest
    {
        [TestMethod]
        public void CopyCtor_withAdSearchModel()
        {
            // Given
            AdSearchModel searchModel = new AdSearchModel
            {
                SearchString = "toto",
                CitySelectedId = 12,
                CategorySelectedId = 7,
                CategorySelectedLabel = "Véhicules"
            };

            // When
            AdSearchResultModel instance = new AdSearchResultModel(searchModel);

            // Then
            Assert.AreEqual(searchModel.SearchString, instance.SearchString);
            Assert.AreEqual(searchModel.CitySelectedId, instance.CitySelectedId);
            Assert.AreEqual(searchModel.CategorySelectedLabel, instance.CategorySelectedLabel);
            Assert.AreEqual(searchModel.CategorySelectedId, instance.CategorySelectedId);
        }
    }
}
