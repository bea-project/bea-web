﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Core.Dal;
using Bea.Domain.Categories;
using Bea.Models.References;
using Bea.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bea.Test.Services
{
    [TestClass]
    public class CategoryServicesTest
    {
        [TestMethod]
        public void GetAllCategoriesAndGroups_ReturnCategoriesClassedByGroup()
        {
            // Given
            Category g1 = new Category { Id = 1, Label = "first" };
            Category g2 = new Category { Id = 3, Label = "second" };
            Category g3 = new Category { Id = 7, Label = "third" };

            Category c1 = new Category { Id = 2, Label = "first c1" };
            g1.AddCategory(c1);
            Category c2 = new Category { Id = 4, Label = "second c2" };
            g2.AddCategory(c2);
            Category c3 = new Category { Id = 6, Label = "second c3" };
            g2.AddCategory(c3);
            Category c4 = new Category { Id = 5, Label = "third c4" };
            g3.AddCategory(c4);

            var repoMock = new Moq.Mock<IRepository>();
            repoMock.Setup(x => x.GetAll<Category>()).Returns(new List<Category>() { g1, g2, g3 });

            CategoryServices service = new CategoryServices(repoMock.Object);

            // When
            IList<CategoryItemModel> result = service.GetAllCategoriesAndGroups();

            // Then
            Assert.AreEqual(1, result[0].Id);
            Assert.AreEqual("FIRST", result[0].Label);
            Assert.IsTrue(result[0].IsGroup);

            Assert.AreEqual(2, result[1].Id);
            Assert.AreEqual("first c1", result[1].Label);
            Assert.IsFalse(result[1].IsGroup);

            Assert.AreEqual(3, result[2].Id);
            Assert.AreEqual("SECOND", result[2].Label);
            Assert.IsTrue(result[2].IsGroup);

            Assert.AreEqual(4, result[3].Id);
            Assert.AreEqual("second c2", result[3].Label);
            Assert.IsFalse(result[3].IsGroup);

            Assert.AreEqual(6, result[4].Id);
            Assert.AreEqual("second c3", result[4].Label);
            Assert.IsFalse(result[4].IsGroup);

            Assert.AreEqual(7, result[5].Id);
            Assert.AreEqual("THIRD", result[5].Label);
            Assert.IsTrue(result[5].IsGroup);

            Assert.AreEqual(5, result[6].Id);
            Assert.AreEqual("third c4", result[6].Label);
            Assert.IsFalse(result[6].IsGroup);
        }

        [TestMethod]
        public void GetCategoryChildrenLabelFromParentLabel_ReturnChildrenLabels()
        {
            // Given
            Category g1 = new Category { Id = 1, Label = "first" };

            Category c1 = new Category { Id = 2, Label = "first c1" };
            g1.AddCategory(c1);
            Category c2 = new Category { Id = 4, Label = "first c2" };
            g1.AddCategory(c2);
            Category c3 = new Category { Id = 6, Label = "first c3" };
            g1.AddCategory(c3);
            Category c4 = new Category { Id = 5, Label = "first c4" };
            g1.AddCategory(c4);

            var repoMock = new Moq.Mock<IRepository>();
            repoMock.Setup(x => x.Get<Category>(1)).Returns(g1);

            CategoryServices service = new CategoryServices(repoMock.Object);

            // When
            IList<String> result = service.GetCategoryChildrenLabelFromParentId(1);

            // Then
            Assert.AreEqual("FIRST C1", result[0]);
            Assert.AreEqual("FIRST C2", result[1]);
            Assert.AreEqual("FIRST C3", result[2]);
            Assert.AreEqual("FIRST C4", result[3]);
        }

        [TestMethod]
        public void GetCategoryChildrenLabelFromParentLabel_NoChildren_Returns_Null()
        {
            // Given
            Category g1 = new Category { Id = 1, Label = "first" };

            var repoMock = new Moq.Mock<IRepository>();
            repoMock.Setup(x => x.Get<Category>(1)).Returns(g1);

            CategoryServices service = new CategoryServices(repoMock.Object);

            // When
            IList<String> result = service.GetCategoryChildrenLabelFromParentId(1);

            // Then
            Assert.AreEqual(null, result);
        }
    }
}
