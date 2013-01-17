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

            // When
            IDictionary<string, string> actual = service.GetAdDataConsistencyErrors(ad);

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

            // When
            IDictionary<string, string> actual = service.GetAdDataConsistencyErrors(ad);

            // Then
            Assert.AreEqual(1, actual.Count);
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

            // When
            IDictionary<string, string> actual = service.GetAdDataConsistencyErrors(ad);

            // Then
            Assert.AreEqual(1, actual.Count);
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

            // When
            IDictionary<string, string> actual = service.GetAdDataConsistencyErrors(ad);

            // Then
            Assert.AreEqual(1, actual.Count);
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

            // When
            IDictionary<string, string> actual = service.GetAdDataConsistencyErrors(ad);

            // Then
            Assert.AreEqual(1, actual.Count);
            Assert.IsTrue(actual.Keys.Contains("SelectedCityId"));
        }

        #region GetCarAdDataConsistencyErrors

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
                Brand = new VehicleBrand { Label = "My BCar Brand" },
                Year = 2013
            };
            IDictionary<string, string> errors = new Dictionary<string, string>();

            // When
            IDictionary<string, string> actual = service.GetCarAdDataConsistencyErrors(ad as CarAd, errors);

            // Then
            Assert.AreEqual(0, actual.Count);
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
                Brand = new VehicleBrand { Label = "My BCar Brand" },
                Year = 2013
            };

            IDictionary<string, string> errors = new Dictionary<string, string>();

            // When
            IDictionary<string, string> actual = service.GetCarAdDataConsistencyErrors(ad as CarAd, errors);

            // Then
            Assert.AreEqual(1, actual.Count);
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
                Year = 2013
            };

            IDictionary<string, string> errors = new Dictionary<string, string>();

            // When
            IDictionary<string, string> actual = service.GetCarAdDataConsistencyErrors(ad as CarAd, errors);

            // Then
            Assert.AreEqual(1, actual.Count);
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
                Year = 2013
            };

            IDictionary<string, string> errors = new Dictionary<string, string>();

            // When
            IDictionary<string, string> actual = service.GetCarAdDataConsistencyErrors(ad as CarAd, errors);

            // Then
            Assert.AreEqual(1, actual.Count);
            Assert.IsTrue(actual.Keys.Contains("SelectedBrandId"));
        }

        [TestMethod]
        public void GetAdDataConsistencyErrors_CarAd_MissingYear_ReturnsDictionaryWithBranError()
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
                Brand = new VehicleBrand { Label = "My BCar Brand" },
                Kilometers = 25
            };

            IDictionary<string, string> errors = new Dictionary<string, string>();

            // When
            IDictionary<string, string> actual = service.GetCarAdDataConsistencyErrors(ad as CarAd, errors);

            // Then
            Assert.AreEqual(1, actual.Count);
            Assert.IsTrue(actual.Keys.Contains("SelectedYearId"));
        }

        #endregion

        #region GetMotoAdDataConsistencyErrors

        [TestMethod]
        public void GetAdDataConsistencyErrors_MotoAd_NoError_ReturnsEmptyDictionary()
        {
            // Given
            AdDataConsistencyServices service = new AdDataConsistencyServices();
            BaseAd ad = new MotoAd()
            {
                Body = "Body Text",
                Category = new Category { Label = "My Category" },
                City = new City { Label = "My City" },
                Title = "Title Text",
                Kilometers = 25,
                EngineSize = 600,
                Brand = new VehicleBrand { Label = "My BCar Brand" },
                Year = 2013
            };
            IDictionary<string, string> errors = new Dictionary<string, string>();

            // When
            IDictionary<string, string> actual = service.GetMotoAdDataConsistencyErrors(ad as MotoAd, errors);

            // Then
            Assert.AreEqual(0, actual.Count);
        }

        [TestMethod]
        public void GetAdDataConsistencyErrors_MotoAd_MissingKilometer_ReturnsDictionaryWithKilometerError()
        {
            // Given
            AdDataConsistencyServices service = new AdDataConsistencyServices();
            BaseAd ad = new MotoAd()
            {
                Body = "Body Text",
                Category = new Category { Label = "My Category" },
                City = new City { Label = "My City" },
                Title = "Title Text",
                EngineSize = 600,
                Brand = new VehicleBrand { Label = "My BCar Brand" },
                Year = 2013
            };

            IDictionary<string, string> errors = new Dictionary<string, string>();

            // When
            IDictionary<string, string> actual = service.GetMotoAdDataConsistencyErrors(ad as MotoAd, errors);

            // Then
            Assert.AreEqual(1, actual.Count);
            Assert.IsTrue(actual.Keys.Contains("Km"));
        }

        [TestMethod]
        public void GetAdDataConsistencyErrors_MotoAd_MissingEngineSize_ReturnsDictionaryWithFuelError()
        {
            // Given
            AdDataConsistencyServices service = new AdDataConsistencyServices();
            BaseAd ad = new MotoAd()
            {
                Body = "Body Text",
                Category = new Category { Label = "My Category" },
                City = new City { Label = "My City" },
                Title = "Title Text",

                Brand = new VehicleBrand { Label = "My BCar Brand" },
                Kilometers = 25,
                Year = 2013
            };

            IDictionary<string, string> errors = new Dictionary<string, string>();

            // When
            IDictionary<string, string> actual = service.GetMotoAdDataConsistencyErrors(ad as MotoAd, errors);

            // Then
            Assert.AreEqual(1, actual.Count);
            Assert.IsTrue(actual.Keys.Contains("EngineSize"));
        }

        [TestMethod]
        public void GetAdDataConsistencyErrors_MotoAd_MissingYear_ReturnsDictionaryWithBranError()
        {
            // Given
            AdDataConsistencyServices service = new AdDataConsistencyServices();
            BaseAd ad = new MotoAd()
            {
                Body = "Body Text",
                Category = new Category { Label = "My Category" },
                City = new City { Label = "My City" },
                Title = "Title Text",
                EngineSize = 600,
                Kilometers = 25
            };

            IDictionary<string, string> errors = new Dictionary<string, string>();

            // When
            IDictionary<string, string> actual = service.GetMotoAdDataConsistencyErrors(ad as MotoAd, errors);

            // Then
            Assert.AreEqual(1, actual.Count);
            Assert.IsTrue(actual.Keys.Contains("SelectedYearId"));
        }

        #endregion
    }
}
