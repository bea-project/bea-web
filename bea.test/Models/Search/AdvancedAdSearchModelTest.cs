using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Models.Search;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bea.Test.Models.Search
{
    [TestClass]
    public class AdvancedAdSearchModelTest
    {
        [TestMethod]
        public void AdvancedAdSearchModelCopy_Ctorwithcopy()
        {
            // Given
            AdvancedAdSearchModel m = new AdvancedAdSearchModel
            {
                MinPrice = 1,
                MaxPrice = 2,

                AgeBracketSelectedId = 3,
                KmBracketSelectedId = 4,

                BrandSelectedId = 5,
                FuelSelectedId = 6,
                IsAutomatic = true,

                EngineSizeBracketSelectedId = 8,

                NbRoomsBracketSelectedId = 9,
                SelectedRealEstateTypeId = 10,
                SelectedDistrictId = 11,
                SurfaceAreaBracketSelectedId = 12,
                IsFurnished = true,

                SelectedHullTypeId = 13,
                MinYearSelectedId = 14,
                MaxYearSelectedId = 15,
                MinLengthSelectedId = 16,
                MaxLengthSelectedId = 17,
                MinHpSelectedId = 18,
                MaxHpSelectedId = 19,
                SelectedMotorTypeId = 20
            };

            // When
            AdvancedAdSearchModel actual = new AdvancedAdSearchModel(m);

            // Then
            Assert.AreEqual(actual.MinPrice, 1);
            Assert.AreEqual(actual.MaxPrice, 2);

            Assert.AreEqual(actual.AgeBracketSelectedId, 3);
            Assert.AreEqual(actual.KmBracketSelectedId, 4);

            Assert.AreEqual(actual.BrandSelectedId, 5);
            Assert.AreEqual(actual.FuelSelectedId, 6);
            Assert.IsTrue(actual.IsAutomatic.Value);

            Assert.AreEqual(actual.EngineSizeBracketSelectedId, 8);

            Assert.AreEqual(actual.NbRoomsBracketSelectedId, 9);
            Assert.AreEqual(actual.SelectedRealEstateTypeId, 10);
            Assert.AreEqual(actual.SelectedDistrictId, 11);
            Assert.AreEqual(actual.SurfaceAreaBracketSelectedId, 12);
            Assert.IsTrue(actual.IsFurnished.Value);

            Assert.AreEqual(actual.SelectedHullTypeId, 13);
            Assert.AreEqual(actual.MinYearSelectedId, 14);
            Assert.AreEqual(actual.MaxYearSelectedId, 15);
            Assert.AreEqual(actual.MinLengthSelectedId, 16);
            Assert.AreEqual(actual.MaxLengthSelectedId, 17);
            Assert.AreEqual(actual.MinHpSelectedId, 18);
            Assert.AreEqual(actual.MaxHpSelectedId, 19);
            Assert.AreEqual(actual.SelectedMotorTypeId, 20);
        }
    }
}