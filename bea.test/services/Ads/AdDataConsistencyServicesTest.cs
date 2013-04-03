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

        #region CheckEmailConsistency
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
        #endregion

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
        public void GetAdDataConsistencyErrors_CarAd_NegativeKilometer_ReturnsDictionaryWithKilometerError()
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
                Km = -55000,
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
        public void GetAdDataConsistencyErrors_MotoAd_NegativeKilometer_ReturnsDictionaryWithKilometerError()
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
                Km = -55000,
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
        public void GetAdDataConsistencyErrors_MotoAd_NegativeEngineSize_ReturnsDictionaryWithEngineSizeError()
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
                EngineSize = -600,
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
        public void GetAdDataConsistencyErrors_VehicleAd_NegativeKiliometer_ReturnsDictionaryWithKmError()
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
                Km = -55000,
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
        public void GetAdDataConsistencyErrors_OtherVehicleAd_NegativeKiliometer_ReturnsDictionaryWithKmError()
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
                Km = -55000,
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

        #region GetRealEstateAdDataConsistencyErrors

        [TestMethod]
        public void GetAdDataConsistencyErrors_RealEstateAd_NoError_ReturnsEmptyDictionary()
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
                RoomNb = 5,
                SurfaceArea = 50,
                SelectedRealEstateTypeId = 1,
                IsFurnished = true,
                Type = AdTypeEnum.RealEstateAd
            };

            // When
            IDictionary<string, string> actual = service.GetAdDataConsistencyErrors(model);

            // Then
            Assert.AreEqual(0, actual.Count);
        }

        [TestMethod]
        public void GetAdDataConsistencyErrors_RealEstateAd_NoRoomNb_ReturnsDictionaryWithRoomNbError()
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
                SurfaceArea = 50,
                SelectedRealEstateTypeId = 1,
                IsFurnished = true,
                Type = AdTypeEnum.RealEstateAd
            };

            // When
            IDictionary<string, string> actual = service.GetAdDataConsistencyErrors(model);

            // Then
            Assert.AreEqual(1, actual.Count);
            Assert.IsTrue(actual.Keys.Contains("RoomNb"));
        }

        [TestMethod]
        public void GetAdDataConsistencyErrors_RealEstateAd_NegativeRoomNb_ReturnsDictionaryWithRoomNbError()
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
                SurfaceArea = 50,
                SelectedRealEstateTypeId = 1,
                IsFurnished = true,
                RoomNb = -5,
                Type = AdTypeEnum.RealEstateAd
            };

            // When
            IDictionary<string, string> actual = service.GetAdDataConsistencyErrors(model);

            // Then
            Assert.AreEqual(1, actual.Count);
            Assert.IsTrue(actual.Keys.Contains("RoomNb"));
        }

        [TestMethod]
        public void GetAdDataConsistencyErrors_RealEstateAd_NoSurfaceArea_ReturnsDictionaryWithSurfaceAreaError()
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
                RoomNb = 5,
                SelectedRealEstateTypeId = 1,
                IsFurnished = true,
                Type = AdTypeEnum.RealEstateAd
            };

            // When
            IDictionary<string, string> actual = service.GetAdDataConsistencyErrors(model);

            // Then
            Assert.AreEqual(1, actual.Count);
            Assert.IsTrue(actual.Keys.Contains("SurfaceArea"));
        }

        [TestMethod]
        public void GetAdDataConsistencyErrors_RealEstateAd_NegativeSurfaceArea_ReturnsDictionaryWithSurfaceAreaError()
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
                RoomNb = 5,
                SelectedRealEstateTypeId = 1,
                IsFurnished = true,
                SurfaceArea = -50,
                Type = AdTypeEnum.RealEstateAd
            };

            // When
            IDictionary<string, string> actual = service.GetAdDataConsistencyErrors(model);

            // Then
            Assert.AreEqual(1, actual.Count);
            Assert.IsTrue(actual.Keys.Contains("SurfaceArea"));
        }

        [TestMethod]
        public void GetAdDataConsistencyErrors_RealEstateAd_NoRealEstateType_ReturnsDictionaryWithSelectedRealEstateTypeIdError()
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
                RoomNb = 5,
                SurfaceArea = 50,
                IsFurnished = true,
                Type = AdTypeEnum.RealEstateAd
            };

            // When
            IDictionary<string, string> actual = service.GetAdDataConsistencyErrors(model);

            // Then
            Assert.AreEqual(1, actual.Count);
            Assert.IsTrue(actual.Keys.Contains("SelectedRealEstateTypeId"));
        }

        [TestMethod]
        public void GetAdDataConsistencyErrors_RealEstateAd_NoIsFurnished_ReturnsDictionaryWithIsFurnishedError()
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
                RoomNb = 5,
                SurfaceArea = 50,
                SelectedRealEstateTypeId = 1,
                Type = AdTypeEnum.RealEstateAd
            };

            // When
            IDictionary<string, string> actual = service.GetAdDataConsistencyErrors(model);

            // Then
            Assert.AreEqual(1, actual.Count);
            Assert.IsTrue(actual.Keys.Contains("IsFurnished"));
        }

        
        #endregion

        #region GetMotorBoatAdDataConsistencyErrors

        [TestMethod]
        public void GetAdDataConsistencyErrors_MotorBoatAdAd_NoError_ReturnsEmptyDictionary()
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
                SelectedMotorBoatTypeId = 1,
                Length = 5,
                SelectedYearId = 2011,
                SelectedMotorTypeId = 1,
                Hp=200,
                Type = AdTypeEnum.MotorBoatAd
            };

            // When
            IDictionary<string, string> actual = service.GetAdDataConsistencyErrors(model);

            // Then
            Assert.AreEqual(0, actual.Count);
        }

        [TestMethod]
        public void GetAdDataConsistencyErrors_MotorBoatAdAd_NegativeHp_ReturnsDictionaryWithHpError()
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
                SelectedMotorBoatTypeId = 1,
                Length = 5,
                SelectedYearId = 2011,
                SelectedMotorTypeId = 1,
                Hp = -200,
                Type = AdTypeEnum.MotorBoatAd
            };

            // When
            IDictionary<string, string> actual = service.GetAdDataConsistencyErrors(model);

            // Then
            Assert.AreEqual(1, actual.Count);
            Assert.IsTrue(actual.Keys.Contains("Hp"));
        }

        [TestMethod]
        public void GetAdDataConsistencyErrors_MotorBoatAdAd_NoHp_ReturnsDictionaryWithHpError()
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
                SelectedMotorBoatTypeId = 1,
                Length = 5,
                SelectedYearId = 2011,
                SelectedMotorTypeId = 1,
                Type = AdTypeEnum.MotorBoatAd
            };

            // When
            IDictionary<string, string> actual = service.GetAdDataConsistencyErrors(model);

            // Then
            Assert.AreEqual(1, actual.Count);
            Assert.IsTrue(actual.Keys.Contains("Hp"));
        }

        [TestMethod]
        public void GetAdDataConsistencyErrors_MotorBoatAdAd_NoSelectedMotorBoatTypeId_ReturnsDictionaryWithSelectedMotorBoatTypeIdError()
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
                Length = 5,
                SelectedYearId = 2011,
                SelectedMotorTypeId = 1,
                Hp = 220,
                Type = AdTypeEnum.MotorBoatAd
            };

            // When
            IDictionary<string, string> actual = service.GetAdDataConsistencyErrors(model);

            // Then
            Assert.AreEqual(1, actual.Count);
            Assert.IsTrue(actual.Keys.Contains("SelectedMotorBoatTypeId"));
        }

        [TestMethod]
        public void GetAdDataConsistencyErrors_MotorBoatAdAd_NegativeLength_ReturnsDictionaryWithLengthError()
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
                SelectedMotorBoatTypeId = 1,
                Length = -5,
                SelectedYearId = 2011,
                SelectedMotorTypeId = 1,
                Hp = 220,
                Type = AdTypeEnum.MotorBoatAd
            };

            // When
            IDictionary<string, string> actual = service.GetAdDataConsistencyErrors(model);

            // Then
            Assert.AreEqual(1, actual.Count);
            Assert.IsTrue(actual.Keys.Contains("Length"));
        }

        [TestMethod]
        public void GetAdDataConsistencyErrors_MotorBoatAdAd_NoLength_ReturnsDictionaryWithLengthError()
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
                SelectedMotorBoatTypeId = 1,
                SelectedYearId = 2011,
                SelectedMotorTypeId = 1,
                Hp = 220,
                Type = AdTypeEnum.MotorBoatAd
            };

            // When
            IDictionary<string, string> actual = service.GetAdDataConsistencyErrors(model);

            // Then
            Assert.AreEqual(1, actual.Count);
            Assert.IsTrue(actual.Keys.Contains("Length"));
        }

        [TestMethod]
        public void GetAdDataConsistencyErrors_MotorBoatAdAd_NoYear_ReturnsDictionaryWithYearError()
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
                SelectedMotorBoatTypeId = 1,
                Length = 5,
                SelectedMotorTypeId = 1,
                Hp = 200,
                Type = AdTypeEnum.MotorBoatAd
            };

            // When
            IDictionary<string, string> actual = service.GetAdDataConsistencyErrors(model);

            // Then
            Assert.AreEqual(1, actual.Count);
            Assert.IsTrue(actual.Keys.Contains("SelectedYearId"));
        }

        [TestMethod]
        public void GetAdDataConsistencyErrors_MotorBoatAdAd_NoSelectedMotorTypeId_ReturnsDictionaryWithSelectedMotorTypeIdError()
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
                SelectedMotorBoatTypeId = 1,
                Length = 5,
                SelectedYearId = 2011,
                Hp = 200,
                Type = AdTypeEnum.MotorBoatAd
            };

            // When
            IDictionary<string, string> actual = service.GetAdDataConsistencyErrors(model);

            // Then
            Assert.AreEqual(1, actual.Count);
            Assert.IsTrue(actual.Keys.Contains("SelectedMotorTypeId"));
        }

        #endregion

        #region GetSailingBoatAdDataConsistencyErrors

        [TestMethod]
        public void GetAdDataConsistencyErrors_SailingBoatAdAd_NoError_ReturnsEmptyDictionary()
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
                SelectedSailingBoatTypeId = 1,
                Length = 5,
                SelectedYearId = 2011,
                SelectedHullTypeId = 1,
                Type = AdTypeEnum.SailingBoatAd
            };

            // When
            IDictionary<string, string> actual = service.GetAdDataConsistencyErrors(model);

            // Then
            Assert.AreEqual(0, actual.Count);
        }

        [TestMethod]
        public void GetAdDataConsistencyErrors_SailingBoatAdAd_NoSelectedSailingBoatTypeId_ReturnsDictionaryWithSelectedSailingBoatTypeIdErrors()
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
                Length = 5,
                SelectedYearId = 2011,
                SelectedHullTypeId = 1,
                Type = AdTypeEnum.SailingBoatAd
            };

            // When
            IDictionary<string, string> actual = service.GetAdDataConsistencyErrors(model);

            // Then
            Assert.AreEqual(1, actual.Count);
            Assert.IsTrue(actual.Keys.Contains("SelectedSailingBoatTypeId"));
        }

        [TestMethod]
        public void GetAdDataConsistencyErrors_SailingBoatAdAd_NoLength_ReturnsDictionaryWithoLengthErrors()
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
                SelectedSailingBoatTypeId = 1,
                SelectedYearId = 2011,
                SelectedHullTypeId = 1,
                Type = AdTypeEnum.SailingBoatAd
            };

            // When
            IDictionary<string, string> actual = service.GetAdDataConsistencyErrors(model);

            // Then
            Assert.AreEqual(1, actual.Count);
            Assert.IsTrue(actual.Keys.Contains("Length"));
        }

        [TestMethod]
        public void GetAdDataConsistencyErrors_SailingBoatAdAd_NegativeLength_ReturnsDictionaryWithoLengthErrors()
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
                SelectedSailingBoatTypeId = 1,
                Length = -5,
                SelectedYearId = 2011,
                SelectedHullTypeId = 1,
                Type = AdTypeEnum.SailingBoatAd
            };

            // When
            IDictionary<string, string> actual = service.GetAdDataConsistencyErrors(model);

            // Then
            Assert.AreEqual(1, actual.Count);
            Assert.IsTrue(actual.Keys.Contains("Length"));
        }

        [TestMethod]
        public void GetAdDataConsistencyErrors_SailingBoatAdAd_NoSelectedYearId_ReturnsDictionaryWithSelectedYearIdErrors()
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
                SelectedSailingBoatTypeId = 1,
                Length = 5,
                SelectedHullTypeId = 1,
                Type = AdTypeEnum.SailingBoatAd
            };

            // When
            IDictionary<string, string> actual = service.GetAdDataConsistencyErrors(model);

            // Then
            Assert.AreEqual(1, actual.Count);
            Assert.IsTrue(actual.Keys.Contains("SelectedYearId"));
        }

        [TestMethod]
        public void GetAdDataConsistencyErrors_SailingBoatAdAd_NoSelectedHullTypeId_ReturnsDictionaryWithSelectedHullTypeIdErrors()
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
                SelectedSailingBoatTypeId = 1,
                Length = 5,
                SelectedYearId = 2011,
                Type = AdTypeEnum.SailingBoatAd
            };

            // When
            IDictionary<string, string> actual = service.GetAdDataConsistencyErrors(model);

            // Then
            Assert.AreEqual(1, actual.Count);
            Assert.IsTrue(actual.Keys.Contains("SelectedHullTypeId"));
        }

        #endregion

        #region GetMotorBoatEngineAdDataConsistencyErrors

        [TestMethod]
        public void GetAdDataConsistencyErrors_MotorBoatEngineAdAd_NoError_ReturnsEmptyDictionary()
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
                SelectedMotorBoatEngineTypeId =1,
                Hp=500,
                SelectedYearId=2011,
                Type = AdTypeEnum.MotorBoatEngineAd
            };

            // When
            IDictionary<string, string> actual = service.GetAdDataConsistencyErrors(model);

            // Then
            Assert.AreEqual(0, actual.Count);
        }

        [TestMethod]
        public void GetAdDataConsistencyErrors_MotorBoatEngineAdAd_NoSelectedMotorBoatEngineTypeId_ReturnsDictionaryWithSelectedMotorBoatEngineTypeIdError()
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
                Hp = 500,
                SelectedYearId = 2011,
                Type = AdTypeEnum.MotorBoatEngineAd
            };

            // When
            IDictionary<string, string> actual = service.GetAdDataConsistencyErrors(model);

            // Then
            Assert.AreEqual(1, actual.Count);
            Assert.IsTrue(actual.Keys.Contains("SelectedMotorBoatEngineTypeId"));
        }

        [TestMethod]
        public void GetAdDataConsistencyErrors_MotorBoatEngineAdAd_NoHp_ReturnsDictionaryWithSelectedHpError()
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
                SelectedMotorBoatEngineTypeId = 1,
                SelectedYearId = 2011,
                Type = AdTypeEnum.MotorBoatEngineAd
            };

            // When
            IDictionary<string, string> actual = service.GetAdDataConsistencyErrors(model);

            // Then
            Assert.AreEqual(1, actual.Count);
            Assert.IsTrue(actual.Keys.Contains("Hp"));
        }

        [TestMethod]
        public void GetAdDataConsistencyErrors_MotorBoatEngineAdAd_NegativeHp_ReturnsDictionaryWithSelectedHpError()
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
                SelectedMotorBoatEngineTypeId = 1,
                Hp = -500,
                SelectedYearId = 2011,
                Type = AdTypeEnum.MotorBoatEngineAd
            };

            // When
            IDictionary<string, string> actual = service.GetAdDataConsistencyErrors(model);

            // Then
            Assert.AreEqual(1, actual.Count);
            Assert.IsTrue(actual.Keys.Contains("Hp"));
        }

        [TestMethod]
        public void GetAdDataConsistencyErrors_MotorBoatEngineAdAd_NoSelectedYearId_ReturnsDictionaryWithSelectedYearId()
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
                SelectedMotorBoatEngineTypeId = 1,
                Hp = 500,
                Type = AdTypeEnum.MotorBoatEngineAd
            };

            // When
            IDictionary<string, string> actual = service.GetAdDataConsistencyErrors(model);

            // Then
            Assert.AreEqual(1, actual.Count);
            Assert.IsTrue(actual.Keys.Contains("SelectedYearId"));
        }

        #endregion

        #region GetWaterSportAdDataConsistencyErrors

        [TestMethod]
        public void GetAdDataConsistencyErrors_WaterSportAdAd_NoError_ReturnsEmptyDictionary()
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
                SelectedWaterSportTypeId = 1,
                Type = AdTypeEnum.WaterSportAd
            };

            // When
            IDictionary<string, string> actual = service.GetAdDataConsistencyErrors(model);

            // Then
            Assert.AreEqual(0, actual.Count);
        }

        [TestMethod]
        public void GetAdDataConsistencyErrors_WaterSportAdAd_NoSelectedWaterSportTypeId_ReturnsDictionaryWithWaterSportError()
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
                Type = AdTypeEnum.WaterSportAd
            };

            // When
            IDictionary<string, string> actual = service.GetAdDataConsistencyErrors(model);

            // Then
            Assert.AreEqual(1, actual.Count);
            Assert.IsTrue(actual.Keys.Contains("SelectedWaterSportTypeId"));
        }

        #endregion

    }
}
