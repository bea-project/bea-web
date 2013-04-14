using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Models.Search;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bea.Test.Models.Search
{
    [TestClass]
    public class AdSearchModelTest
    {
        [TestMethod]
        public void AdSearchModelCopy_Ctorwithcopy()
        {
            // Given
            AdSearchModel searchModel = new AdSearchModel
            {
                SearchString = "toto",
                CitySelectedId = 12,
                CategorySelectedId = 7,
                CategorySelectedLabel = "Véhicules",
                CategoryImagePath = "thepath"
            };

            // When
            AdSearchModel instance = new AdSearchModel(searchModel);

            // Then
            Assert.AreEqual(searchModel.SearchString, instance.SearchString);
            Assert.AreEqual(searchModel.CitySelectedId, instance.CitySelectedId);
            Assert.AreEqual(searchModel.CategorySelectedLabel, instance.CategorySelectedLabel);
            Assert.AreEqual(searchModel.CategorySelectedId, instance.CategorySelectedId);
            Assert.AreEqual(searchModel.CategoryImagePath, instance.CategoryImagePath);
        }

        [TestMethod]
        public void AdSearchModel_SetCategory()
        {
            // Given
            AdSearchModel searchModel = new AdSearchModel
            {
                SearchString = "toto",
                CitySelectedId = 12,
                CategorySelectedId = 7,
                CategorySelectedLabel = "Véhicules",
                CategoryImagePath = "thepath"
            };

            // When
            AdSearchModel instance = new AdSearchModel(searchModel);

            // Then
            Assert.AreEqual(searchModel.SearchString, instance.SearchString);
            Assert.AreEqual(searchModel.CitySelectedId, instance.CitySelectedId);
            Assert.AreEqual(searchModel.CategorySelectedLabel, instance.CategorySelectedLabel);
            Assert.AreEqual(searchModel.CategorySelectedId, instance.CategorySelectedId);
            Assert.AreEqual(searchModel.CategoryImagePath, instance.CategoryImagePath);
        }
    }
}
