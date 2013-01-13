using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bea.Services;
using Bea.Domain.Ads;
using Bea.Domain.Categories;
using Bea.Domain.Location;
using Bea.Domain.Reference;

namespace Bea.Test.Services
{
    [TestClass]
    public class AdDataConsistencyServicesTest
    {
        [TestMethod]
        public void GetAdDataConsistencyErrors_Ad_NoError_ReturnsEmptyDictionary()
        {
            // Given
            AdDataConsistencyServices service = new AdDataConsistencyServices();
            BaseAd ad = new Ad()
            {
                Body = "Body Text",
                Category = new Category { Label = "My Category" },
                City = new City { Label = "My City" },
                Title = "Title Text",
            };
            int provinceId = 4;

            // When
            Dictionary<string, string> actual = service.GetAdDataConsistencyErrors(ad, provinceId);

            // Then
            Assert.IsTrue(actual.Count==0);
        }

        [TestMethod]
        public void GetAdDataConsistencyErrors_Ad_NoBody_ReturnsDictionaryWithBodyError()
        {
            // Given
            AdDataConsistencyServices service = new AdDataConsistencyServices();
            BaseAd ad = new Ad()
            {
                Category = new Category { Label = "My Category" },
                City = new City { Label = "My City" },
                Title = "Title Text"
            };
            int provinceId = 4;

            // When
            Dictionary<string, string> actual = service.GetAdDataConsistencyErrors(ad, provinceId);

            // Then
            Assert.IsTrue(actual.Count == 1);
            Assert.IsTrue(actual.Keys.Contains("Body"));
        }

        [TestMethod]
        public void GetAdDataConsistencyErrors_Ad_NoTitle_ReturnsDictionaryWithTitleError()
        {
            // Given
            AdDataConsistencyServices service = new AdDataConsistencyServices();
            BaseAd ad = new Ad()
            {
                Body = "Body Text",
                Category = new Category { Label = "My Category" },
                City = new City { Label = "My City" },
                
            };
            int provinceId = 4;

            // When
            Dictionary<string, string> actual = service.GetAdDataConsistencyErrors(ad, provinceId);

            // Then
            Assert.IsTrue(actual.Count == 1);
            Assert.IsTrue(actual.Keys.Contains("Title"));
        }

        [TestMethod]
        public void GetAdDataConsistencyErrors_Ad_NoCategory_ReturnsDictionaryWithCategoryError()
        {
            // Given
            AdDataConsistencyServices service = new AdDataConsistencyServices();
            BaseAd ad = new Ad()
            {
                Title = "Title Text",
                Body = "Body Text",
                City = new City { Label = "My City" },

            };
            int provinceId = 4;

            // When
            Dictionary<string, string> actual = service.GetAdDataConsistencyErrors(ad, provinceId);

            // Then
            Assert.IsTrue(actual.Count == 1);
            Assert.IsTrue(actual.Keys.Contains("SelectedCategoryId"));
        }

        [TestMethod]
        public void GetAdDataConsistencyErrors_Ad_NoCity_ReturnsDictionaryWithCityError()
        {
            // Given
            AdDataConsistencyServices service = new AdDataConsistencyServices();
            BaseAd ad = new Ad()
            {
                Title = "Title Text",
                Body = "Body Text",
                Category = new Category { Label = "My Category" },
            };
            int provinceId = 4;

            // When
            Dictionary<string, string> actual = service.GetAdDataConsistencyErrors(ad, provinceId);

            // Then
            Assert.IsTrue(actual.Count == 1);
            Assert.IsTrue(actual.Keys.Contains("SelectedCityId"));
        }

        [TestMethod]
        public void GetAdDataConsistencyErrors_Ad_NoProvince_ReturnsDictionaryWithProvinceError()
        {
            // Given
            AdDataConsistencyServices service = new AdDataConsistencyServices();
            BaseAd ad = new Ad()
            {
                Body = "Body Text",
                Category = new Category { Label = "My Category" },
                City = new City { Label = "My City" },
                Title = "Title Text",
            };
            

            // When
            Dictionary<string, string> actual = service.GetAdDataConsistencyErrors(ad, null);

            // Then
            Assert.IsTrue(actual.Count == 1);
            Assert.IsTrue(actual.Keys.Contains("SelectedProvinceId"));
        }

        [TestMethod]
        public void GetAdDataConsistencyErrors_CarAd_NoError_ReturnsEmptyDictionary()
        {
            // Given
            AdDataConsistencyServices service = new AdDataConsistencyServices();
            BaseAd ad = new CarAd()
            {
                Body = "Body Text",
                Category = new Category { Label = "My Category" },
                City = new City { Label = "My City" },
                Title = "Title Text",
                Kilometers = 25,
                Fuel = new CarFuel { Label = "My Fuel Label" },
                Brand = new VehicleBrand { Label = "My BCar Brand"}
            };
            int provinceId = 4;

            // When
            Dictionary<string, string> actual = service.GetAdDataConsistencyErrors(ad, provinceId);

            // Then
            Assert.IsTrue(actual.Count == 0);
        }

        [TestMethod]
        public void GetAdDataConsistencyErrors_CarAd_MissingKilometer_ReturnsDictionaryWithKilometerError()
        {
            // Given
            AdDataConsistencyServices service = new AdDataConsistencyServices();
            BaseAd ad = new CarAd()
            {
                Body = "Body Text",
                Category = new Category { Label = "My Category" },
                City = new City { Label = "My City" },
                Title = "Title Text",
                Fuel = new CarFuel { Label = "My Fuel Label" },
                Brand = new VehicleBrand { Label = "My BCar Brand" }
            };
            int provinceId = 4;

            // When
            Dictionary<string, string> actual = service.GetAdDataConsistencyErrors(ad, provinceId);

            // Then
            Assert.IsTrue(actual.Count == 1);
            Assert.IsTrue(actual.Keys.Contains("Km"));
        }

        [TestMethod]
        public void GetAdDataConsistencyErrors_CarAd_MissingFuel_ReturnsDictionaryWithFuelError()
        {
            // Given
            AdDataConsistencyServices service = new AdDataConsistencyServices();
            BaseAd ad = new CarAd()
            {
                Body = "Body Text",
                Category = new Category { Label = "My Category" },
                City = new City { Label = "My City" },
                Title = "Title Text",
                
                Brand = new VehicleBrand { Label = "My BCar Brand" },
                Kilometers = 25,
            };
            int provinceId = 4;

            // When
            Dictionary<string, string> actual = service.GetAdDataConsistencyErrors(ad, provinceId);

            // Then
            Assert.IsTrue(actual.Count == 1);
            Assert.IsTrue(actual.Keys.Contains("SelectedFuelId"));
        }

        [TestMethod]
        public void GetAdDataConsistencyErrors_CarAd_MissingBrand_ReturnsDictionaryWithBranError()
        {
            // Given
            AdDataConsistencyServices service = new AdDataConsistencyServices();
            BaseAd ad = new CarAd()
            {
                Body = "Body Text",
                Category = new Category { Label = "My Category" },
                City = new City { Label = "My City" },
                Title = "Title Text",
                Fuel = new CarFuel { Label = "My Fuel Label" },
                Kilometers = 25,
            };
            int provinceId = 4;

            // When
            Dictionary<string, string> actual = service.GetAdDataConsistencyErrors(ad, provinceId);

            // Then
            Assert.IsTrue(actual.Count == 1);
            Assert.IsTrue(actual.Keys.Contains("SelectedBrandId"));
        }
    }
}
