using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
                ProvinceSelectedId = 78,
                CitySelectedId = 12,
                CategorySelectedId = 7
            };

            // When
            AdSearchResultModel instance = new AdSearchResultModel(searchModel);

            // Then
            Assert.AreEqual(searchModel.SearchString, instance.SearchString);
            Assert.AreEqual(searchModel.ProvinceSelectedId, instance.ProvinceSelectedId);
            Assert.AreEqual(searchModel.CitySelectedId, instance.CitySelectedId);
            Assert.AreEqual(searchModel.CategorySelectedId, instance.CategorySelectedId);
        }
    }
}
