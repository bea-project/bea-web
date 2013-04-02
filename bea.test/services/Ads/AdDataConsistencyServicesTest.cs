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
using Bea.Services.Ads;
using Bea.Models.Create;

namespace Bea.Test.Services
{
    [TestClass]
    public class AdDataConsistencyServicesTest
    {
        #region GetCommonAdDataConsistencyErrors
        [TestMethod]
        public void GetAdDataConsistencyErrors_Ad_NoError_ReturnsEmptyDictionary()
        {
            // Given
            AdDataConsistencyServices service = new AdDataConsistencyServices();
            AdvancedAdCreateModel model = new AdvancedAdCreateModel() {
                Body = "My Body",
                Title = "My Title",
                SelectedCityId = 1,
                SelectedCategoryId = 1,
                Name = "My Name",
                Email = "name@isp.com"
            };

            //When
            IDictionary<string, string> errors = service.GetAdDataConsistencyErrors(model);

            //Then
            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]
        public void GetAdDataConsistencyErrors_Ad_NoBody_ReturnsDictionaryWithBodyError()
        {
            // Given
            AdDataConsistencyServices service = new AdDataConsistencyServices();
            AdvancedAdCreateModel model = new AdvancedAdCreateModel()
            {
                Title = "My Title",
                SelectedCityId = 1,
                SelectedCategoryId = 1,
                Name = "My Name",
                Email = "name@isp.com"
            };

            // When
            IDictionary<string, string> actual = service.GetAdDataConsistencyErrors(model);

            // Then
            Assert.AreEqual(1, actual.Count);
            Assert.IsTrue(actual.Keys.Contains("Body"));
        }

        [TestMethod]
        public void GetAdDataConsistencyErrors_Ad_NoTitle_ReturnsDictionaryWithTitleError()
        {
            // Given
            AdDataConsistencyServices service = new AdDataConsistencyServices();
            AdvancedAdCreateModel model = new AdvancedAdCreateModel()
            {
                Body = "My Body",
                SelectedCityId = 1,
                SelectedCategoryId = 1,
                Name = "My Name",
                Email = "name@isp.com"
            };

            // When
            IDictionary<string, string> actual = service.GetAdDataConsistencyErrors(model);

            // Then
            Assert.AreEqual(1, actual.Count);
            Assert.IsTrue(actual.Keys.Contains("Title"));
        }

        [TestMethod]
        public void GetAdDataConsistencyErrors_Ad_NoCategory_ReturnsDictionaryWithCategoryError()
        {
            // Given
            AdDataConsistencyServices service = new AdDataConsistencyServices();
            AdvancedAdCreateModel model = new AdvancedAdCreateModel()
            {
                Body = "My Body",
                Title = "My Title",
                SelectedCityId = 1,
                Name = "My Name",
                Email = "name@isp.com"
            };

            // When
            IDictionary<string, string> actual = service.GetAdDataConsistencyErrors(model);

            // Then
            Assert.AreEqual(1, actual.Count);
            Assert.IsTrue(actual.Keys.Contains("SelectedCategoryId"));
        }

        [TestMethod]
        public void GetAdDataConsistencyErrors_Ad_NoCity_ReturnsDictionaryWithCityError()
        {
            // Given
            AdDataConsistencyServices service = new AdDataConsistencyServices();
            AdvancedAdCreateModel model = new AdvancedAdCreateModel()
            {
                Body = "My Body",
                Title = "My Title",
                SelectedCategoryId = 1,
                Name = "My Name",
                Email = "name@isp.com"
            };

            // When
            IDictionary<string, string> actual = service.GetAdDataConsistencyErrors(model);

            // Then
            Assert.AreEqual(1, actual.Count);
            Assert.IsTrue(actual.Keys.Contains("SelectedCityId"));
        }
        #endregion

        [TestMethod]
        public void IsEmailValid_Null_Email_Returns_False()
        {
            // Given
            AdDataConsistencyServices service = new AdDataConsistencyServices();

            // When
            Boolean actual = service.IsEmailValid(null);

            // Then
            Assert.AreEqual(false, actual);
        }
       
        [TestMethod]
        public void IsEmailValid_UnValid_Email_Returns_False()
        {
            // Given
            AdDataConsistencyServices service = new AdDataConsistencyServices();
            String Email = "toto";

            // When
            Boolean actual = service.IsEmailValid(Email);

            // Then
            Assert.AreEqual(false, actual);

        }

        [TestMethod]
        public void IsEmailValid_Valid_Email_Returns_True()
        {
            // Given
            AdDataConsistencyServices service = new AdDataConsistencyServices();
            String Email = "toto@gmail.com";

            // When
            Boolean actual = service.IsEmailValid(Email);

            // Then
            Assert.AreEqual(true, actual);

        }


        #region GetCarAdDataConsistencyErrors

        [TestMethod]
        public void GetAdDataConsistencyErrors_CarAd_NoError_ReturnsEmptyDictionary()
        {
            // Given
            AdDataConsistencyServices service = new AdDataConsistencyServices();
            AdvancedAdCreateModel model = new AdvancedAdCreateModel()
            {
                Body = "My Body",
                Title = "My Title",
                SelectedCityId = 1,
                SelectedCategoryId = 1,
                Name = "My Name",
                Email = "name@isp.com",
                SelectedFuelId = 1,
                SelectedCarBrandId = 1,
                SelectedYearId = 2008,
                Km=55000,
                Type=AdTypeEnum.CarAd
            };

            // When
            IDictionary<string, string> actual = service.GetAdDataConsistencyErrors(model);

            // Then
            Assert.AreEqual(0, actual.Count);
        }

        [TestMethod]
        public void GetAdDataConsistencyErrors_CarAd_MissingKilometer_ReturnsDictionaryWithKilometerError()
        {
            // Given
            AdDataConsistencyServices service = new AdDataConsistencyServices();
            AdvancedAdCreateModel model = new AdvancedAdCreateModel()
            {
                Body = "My Body",
                Title = "My Title",
                SelectedCityId = 1,
                SelectedCategoryId = 1,
                Name = "My Name",
                Email = "name@isp.com",
                SelectedFuelId = 1,
                SelectedCarBrandId = 1,
                SelectedYearId = 2008,
                Type = AdTypeEnum.CarAd
            };

            // When
            IDictionary<string, string> actual = service.GetAdDataConsistencyErrors(model);

            // Then
            Assert.AreEqual(1, actual.Count);
            Assert.IsTrue(actual.Keys.Contains("Km"));
        }

        [TestMethod]
        public void GetAdDataConsistencyErrors_CarAd_MissingFuel_ReturnsDictionaryWithFuelError()
        {
            // Given
            AdDataConsistencyServices service = new AdDataConsistencyServices();
            AdvancedAdCreateModel model = new AdvancedAdCreateModel()
            {
                Body = "My Body",
                Title = "My Title",
                SelectedCityId = 1,
                SelectedCategoryId = 1,
                Name = "My Name",
                Email = "name@isp.com",
                SelectedCarBrandId = 1,
                SelectedYearId = 2008,
                Km = 55000,
                Type = AdTypeEnum.CarAd
            };

            // When
            IDictionary<string, string> actual = service.GetAdDataConsistencyErrors(model);

            // Then
            Assert.AreEqual(1, actual.Count);
            Assert.IsTrue(actual.Keys.Contains("SelectedFuelId"));
        }

        [TestMethod]
        public void GetAdDataConsistencyErrors_CarAd_MissingBrand_ReturnsDictionaryWithBranError()
        {
            // Given
            AdDataConsistencyServices service = new AdDataConsistencyServices();
            AdvancedAdCreateModel model = new AdvancedAdCreateModel()
            {
                Body = "My Body",
                Title = "My Title",
                SelectedCityId = 1,
                SelectedCategoryId = 1,
                Name = "My Name",
                Email = "name@isp.com",
                SelectedFuelId = 1,
                SelectedYearId = 2008,
                Km = 55000,
                Type = AdTypeEnum.CarAd
            };

            // When
            IDictionary<string, string> actual = service.GetAdDataConsistencyErrors(model);

            // Then
            Assert.AreEqual(1, actual.Count);
            Assert.IsTrue(actual.Keys.Contains("SelectedCarBrandId"));
        }

        [TestMethod]
        public void GetAdDataConsistencyErrors_CarAd_MissingYear_ReturnsDictionaryWithBranError()
        {
            // Given
            AdDataConsistencyServices service = new AdDataConsistencyServices();
            AdvancedAdCreateModel model = new AdvancedAdCreateModel()
            {
                Body = "My Body",
                Title = "My Title",
                SelectedCityId = 1,
                SelectedCategoryId = 1,
                Name = "My Name",
                Email = "name@isp.com",
                SelectedFuelId = 1,
                SelectedCarBrandId = 1,
                Km = 55000,
                Type = AdTypeEnum.CarAd
            };
            // When
            IDictionary<string, string> actual = service.GetAdDataConsistencyErrors(model);

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
            AdvancedAdCreateModel model = new AdvancedAdCreateModel()
            {
                Body = "My Body",
                Title = "My Title",
                SelectedCityId = 1,
                SelectedCategoryId = 1,
                Name = "My Name",
                Email = "name@isp.com",
                SelectedMotoBrandId = 1,
                SelectedYearId = 2008,
                Km = 55000,
                EngineSize=600,
                Type = AdTypeEnum.MotoAd
            };
            // When
            IDictionary<string, string> actual = service.GetAdDataConsistencyErrors(model);

            // Then
            Assert.AreEqual(0, actual.Count);
        }

        [TestMethod]
        public void GetAdDataConsistencyErrors_MotoAd_MissingKilometer_ReturnsDictionaryWithKilometerError()
        {
            // Given
            AdDataConsistencyServices service = new AdDataConsistencyServices();
            AdvancedAdCreateModel model = new AdvancedAdCreateModel()
            {
                Body = "My Body",
                Title = "My Title",
                SelectedCityId = 1,
                SelectedCategoryId = 1,
                Name = "My Name",
                Email = "name@isp.com",
                SelectedMotoBrandId = 1,
                SelectedYearId = 2008,
                EngineSize = 600,
                Type = AdTypeEnum.MotoAd
            };

            // When
            IDictionary<string, string> actual = service.GetAdDataConsistencyErrors(model);

            // Then
            Assert.AreEqual(1, actual.Count);
            Assert.IsTrue(actual.Keys.Contains("Km"));
        }

        [TestMethod]
        public void GetAdDataConsistencyErrors_MotoAd_MissingEngineSize_ReturnsDictionaryWithEngineSizeError()
        {
            // Given
            AdDataConsistencyServices service = new AdDataConsistencyServices();
            AdvancedAdCreateModel model = new AdvancedAdCreateModel()
            {
                Body = "My Body",
                Title = "My Title",
                SelectedCityId = 1,
                SelectedCategoryId = 1,
                Name = "My Name",
                Email = "name@isp.com",
                SelectedMotoBrandId = 1,
                SelectedYearId = 2008,
                Km = 55000,
                Type = AdTypeEnum.MotoAd
            };

            // When
            IDictionary<string, string> actual = service.GetAdDataConsistencyErrors(model);

            // Then
            Assert.AreEqual(1, actual.Count);
            Assert.IsTrue(actual.Keys.Contains("EngineSize"));
        }

        [TestMethod]
        public void GetAdDataConsistencyErrors_MotoAd_MissingYear_ReturnsDictionaryWithYearError()
        {
            // Given
            AdDataConsistencyServices service = new AdDataConsistencyServices();
            AdvancedAdCreateModel model = new AdvancedAdCreateModel()
            {
                Body = "My Body",
                Title = "My Title",
                SelectedCityId = 1,
                SelectedCategoryId = 1,
                Name = "My Name",
                Email = "name@isp.com",
                SelectedMotoBrandId = 1,
                
                Km = 55000,
                EngineSize = 600,
                Type = AdTypeEnum.MotoAd
            };
            // When
            IDictionary<string, string> actual = service.GetAdDataConsistencyErrors(model);

            // Then
            Assert.AreEqual(1, actual.Count);
            Assert.IsTrue(actual.Keys.Contains("SelectedYearId"));
        }

        [TestMethod]
        public void GetAdDataConsistencyErrors_MotoAd_MissingBrand_ReturnsDictionaryWithBrandError()
        {
            // Given
            AdDataConsistencyServices service = new AdDataConsistencyServices();
            AdvancedAdCreateModel model = new AdvancedAdCreateModel()
            {
                Body = "My Body",
                Title = "My Title",
                SelectedCityId = 1,
                SelectedCategoryId = 1,
                Name = "My Name",
                Email = "name@isp.com",
                SelectedYearId = 2008,
                Km = 55000,
                EngineSize = 600,
                Type = AdTypeEnum.MotoAd
            };
            // When
            IDictionary<string, string> actual = service.GetAdDataConsistencyErrors(model);

            // Then
            Assert.AreEqual(1, actual.Count);
            Assert.IsTrue(actual.Keys.Contains("SelectedMotoBrandId"));
        }

        #endregion

        #region GetVehicleAdDataConsistencyErrors

        [TestMethod]
        public void GetAdDataConsistencyErrors_VehicleAd_NoError_ReturnsEmptyDictionary()
        {
            // Given
            AdDataConsistencyServices service = new AdDataConsistencyServices();
            AdvancedAdCreateModel model = new AdvancedAdCreateModel()
            {
                Body = "My Body",
                Title = "My Title",
                SelectedCityId = 1,
                SelectedCategoryId = 1,
                Name = "My Name",
                Email = "name@isp.com",
                SelectedYearId = 2008,
                Km = 55000,
                Type = AdTypeEnum.VehiculeAd
            };
            // When
            IDictionary<string, string> actual = service.GetAdDataConsistencyErrors(model);

            // Then
            Assert.AreEqual(0, actual.Count);
        }

        [TestMethod]
        public void GetAdDataConsistencyErrors_VehicleAd__NoKiliometer_ReturnsDictionaryWithKmError()
        {
            // Given
            AdDataConsistencyServices service = new AdDataConsistencyServices();
            AdvancedAdCreateModel model = new AdvancedAdCreateModel()
            {
                Body = "My Body",
                Title = "My Title",
                SelectedCityId = 1,
                SelectedCategoryId = 1,
                Name = "My Name",
                Email = "name@isp.com",
                SelectedYearId = 2008,
                Type = AdTypeEnum.VehiculeAd
            };
            // When
            IDictionary<string, string> actual = service.GetAdDataConsistencyErrors(model);

            // Then
            Assert.AreEqual(1, actual.Count);
            Assert.IsTrue(actual.Keys.Contains("Km"));
        }

        [TestMethod]
        public void GetAdDataConsistencyErrors_VehicleAd_NoYear_ReturnsDictionaryWithYear()
        {
            // Given
            AdDataConsistencyServices service = new AdDataConsistencyServices();
            AdvancedAdCreateModel model = new AdvancedAdCreateModel()
            {
                Body = "My Body",
                Title = "My Title",
                SelectedCityId = 1,
                SelectedCategoryId = 1,
                Name = "My Name",
                Email = "name@isp.com",
                Km = 55000,
                Type = AdTypeEnum.VehiculeAd
            };
            // When
            IDictionary<string, string> actual = service.GetAdDataConsistencyErrors(model);

            // Then
            Assert.AreEqual(1, actual.Count);
            Assert.IsTrue(actual.Keys.Contains("SelectedYearId"));
        }

        #endregion

        #region GetOtherVehicleAdDataConsistencyErrors

        [TestMethod]
        public void GetAdDataConsistencyErrors_OtherVehicleAd_NoError_ReturnsEmptyDictionary()
        {
            // Given
            AdDataConsistencyServices service = new AdDataConsistencyServices();
            AdvancedAdCreateModel model = new AdvancedAdCreateModel()
            {
                Body = "My Body",
                Title = "My Title",
                SelectedCityId = 1,
                SelectedCategoryId = 1,
                Name = "My Name",
                Email = "name@isp.com",
                SelectedYearId = 2008,
                Km = 55000,
                SelectedFuelId = 1,
                Type = AdTypeEnum.OtherVehiculeAd
            };
            // When
            IDictionary<string, string> actual = service.GetAdDataConsistencyErrors(model);

            // Then
            Assert.AreEqual(0, actual.Count);
        }

        [TestMethod]
        public void GetAdDataConsistencyErrors_OtherVehicleAd__NoKiliometer_ReturnsDictionaryWithKmError()
        {
            // Given
            AdDataConsistencyServices service = new AdDataConsistencyServices();
            AdvancedAdCreateModel model = new AdvancedAdCreateModel()
            {
                Body = "My Body",
                Title = "My Title",
                SelectedCityId = 1,
                SelectedCategoryId = 1,
                Name = "My Name",
                Email = "name@isp.com",
                SelectedYearId = 2008,
                SelectedFuelId = 1,
                Type = AdTypeEnum.OtherVehiculeAd
            };
            // When
            IDictionary<string, string> actual = service.GetAdDataConsistencyErrors(model);

            // Then
            Assert.AreEqual(1, actual.Count);
            Assert.IsTrue(actual.Keys.Contains("Km"));
        }

        [TestMethod]
        public void GetAdDataConsistencyErrors_OtherVehicleAd_NoYear_ReturnsDictionaryWithYear()
        {
            // Given
            AdDataConsistencyServices service = new AdDataConsistencyServices();
            AdvancedAdCreateModel model = new AdvancedAdCreateModel()
            {
                Body = "My Body",
                Title = "My Title",
                SelectedCityId = 1,
                SelectedCategoryId = 1,
                Name = "My Name",
                Email = "name@isp.com",
                Km = 55000,
                SelectedFuelId = 1,
                Type = AdTypeEnum.OtherVehiculeAd
            };
            // When
            IDictionary<string, string> actual = service.GetAdDataConsistencyErrors(model);

            // Then
            Assert.AreEqual(1, actual.Count);
            Assert.IsTrue(actual.Keys.Contains("SelectedYearId"));
        }

        [TestMethod]
        public void GetAdDataConsistencyErrors_OtherVehicleAd_NoFuel_ReturnsDictionaryWithFuel()
        {
            // Given
            AdDataConsistencyServices service = new AdDataConsistencyServices();
            AdvancedAdCreateModel model = new AdvancedAdCreateModel()
            {
                Body = "My Body",
                Title = "My Title",
                SelectedCityId = 1,
                SelectedCategoryId = 1,
                Name = "My Name",
                Email = "name@isp.com",
                SelectedYearId = 2008,
                Km = 55000,
                Type = AdTypeEnum.OtherVehiculeAd
            };
            // When
            IDictionary<string, string> actual = service.GetAdDataConsistencyErrors(model);

            // Then
            Assert.AreEqual(1, actual.Count);
            Assert.IsTrue(actual.Keys.Contains("SelectedFuelId"));
        }


        #endregion
    }
}
