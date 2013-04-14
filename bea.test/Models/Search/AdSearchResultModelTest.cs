using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bea.Models.Search;
using Bea.Domain.Categories;

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
                CategorySelectedLabel = "Véhicules",
                CategoryImagePath = "thepath"
            };

            Category cat = new Category
            {
                Id = 16,
                Label = "Voitures",
                ImageName = "car.png"
            };

            // When
            searchModel.SetCategory(cat);

            // Then
            Assert.AreEqual(16, searchModel.CategorySelectedId);
            Assert.AreEqual("Voitures", searchModel.CategorySelectedLabel);
            Assert.AreEqual("car.png", searchModel.CategoryImagePath);
        }
    }
}
