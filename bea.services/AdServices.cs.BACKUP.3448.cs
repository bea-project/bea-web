using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Core.Dal;
using Bea.Core.Services;
using Bea.Domain;
using Bea.Domain.Ads;
using Bea.Domain.Location;
using Bea.Models;
using Bea.Domain.Categories;
using Bea.Models.Details;
using Bea.Domain.Reference;
using Bea.Domain.Search;
using System.Transactions;
using Bea.Models.Create;
using Bea.Domain.Ads.WaterSport;

namespace Bea.Services
{
    public class AdServices : IAdServices
    {
        private readonly IAdRepository _adRepository;
        private readonly IRepository _repository;
        private readonly IHelperService _helperService;
        private readonly IUserServices _userServices;
        private readonly IAdActivationServices _adActivationServices;

        public AdServices(IAdRepository adRepository, IHelperService helperService, IRepository repository, IUserServices userServices, IAdActivationServices adActivationServices)
        {
            _adRepository = adRepository;
            _helperService = helperService;
            _repository = repository;
            _userServices = userServices;
            _adActivationServices = adActivationServices;
        }

        public IDictionary<City, int> CountAdsByCities()
        {
            return _adRepository.CountAdsByCity();
        }

        public IDictionary<User, int> CountAdsByUsers()
        {
            return _adRepository.CountAdsByUser();
        }

        public IList<BaseAd> GetAllAds()
        {
            return _adRepository.GetAllAds();
        }

        public Ad GetAdById(long adId)
        {
            return _adRepository.GetAdById<Ad>(adId);
        }

        public void DeleteAdById(long adId)
        {
            _repository.Delete(_repository.Get<Ad>(adId));
            _repository.Flush();
        }

        public void AddAd(BaseAd ad)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                _repository.Save(ad.CreatedBy);
                _repository.Save(ad);
                _repository.Flush();
                scope.Complete();
            }
        }

        #region consultation

        public AdDetailsModel GetAdDetails(long adId)
        {
            AdTypeEnum adType = _adRepository.GetAdType(adId);

            if (adType == AdTypeEnum.Undefined)
                return null;

            AdDetailsModel model = CreateAdDetailsModelFromAd(adType, adId);

            return model;
        }

        protected AdDetailsModel CreateAdDetailsModelFromAd(AdTypeEnum adType, long adId)
        {
            AdDetailsModel model = null;
            BaseAd ad = null;

            // Get the right Ad based on its type
            switch (adType)
            {
                case AdTypeEnum.Ad:
                    ad = _adRepository.GetAdById<Ad>(adId);
                    model = new AdDetailsModel(ad);
                    break;

                case AdTypeEnum.CarAd:
                    ad = _adRepository.GetAdById<CarAd>(adId);
                    model = new CarAdDetailsModel(ad as CarAd);
                    break;

                case AdTypeEnum.MotoAd:
                    ad = _adRepository.GetAdById<MotoAd>(adId);
                    model = new MotoAdDetailsModel(ad as MotoAd);
                    break;

                case AdTypeEnum.OtherVehiculeAd:
                    ad = _adRepository.GetAdById<OtherVehicleAd>(adId);
                    model = new OtherVehicleAdDetailsModel(ad as OtherVehicleAd);
                    break;

                default:
                    return null;
            }

            // Compute whether or not this Ad is new (less than 3 days)
            model.IsNew = ad.CreationDate > _helperService.GetCurrentDateTime().AddHours(-72);

            return model;
        }

        #endregion

        #region creation

        public BaseAd GetAdFromModel(AdCreateModel model, Dictionary<string, string> form)
        {
            BaseAd ad = null;

            if (form != null && form.ContainsKey("Type"))
            {
                switch (Int32.Parse(form["Type"]))
                {
                    case (int)AdTypeEnum.CarAd:
                        ad = GetCarAdFromModel(form);
                        break;
                    case (int)AdTypeEnum.MotoAd:
                        ad = GetMotoAdFromModel(form);
                        break;
                    case (int)AdTypeEnum.OtherVehiculeAd:
                        ad = GetOtherVehicleAdFromModel(form);
                        break;
                    case (int)AdTypeEnum.VehiculeAd:
                        ad = GetVehicleAdFromModel(form);
                        break;
                    case (int)AdTypeEnum.MotorBoatAd:
                        ad = GetMotorBoatAdFromModel(form);
                        break;
                    case (int)AdTypeEnum.SailingBoatAd:
                        ad = GetSailingBoatAdFromModel(form);
                        break;
                    case (int)AdTypeEnum.MotorBoatEngineAd:
                        ad = MotorBoatEngineAdFromModel(form);
                        break;
                    default:
                        ad = new Ad();
                        break;
                }
            }
            else
                ad = new Ad();

            return GetCommonAdFromModel(ad, model);

        }

        private BaseAd GetCommonAdFromModel(BaseAd ad, AdCreateModel model)
        {
            if (model.SelectedCityId.HasValue)
                ad.City = _repository.Get<City>(model.SelectedCityId.Value);

            if (model.SelectedCategoryId.HasValue)
                ad.Category = _repository.Get<Category>(model.SelectedCategoryId.Value);

            ad.Body = model.Body;
            ad.CreationDate = DateTime.Now;
            ad.Price = model.Price.GetValueOrDefault();
            ad.Title = model.Title;
            ad.IsOffer = model.IsOffer;
            ad.PhoneNumber = model.Telephone;
            ad.ActivationToken = _adActivationServices.GenerateActivationToken();

            User createdBy = new User();
            createdBy.Firstname = model.Name;
            createdBy.Email = model.Email;
            createdBy.Password = "Password";
            ad.CreatedBy = createdBy;

            GetAdPicturesFromModel(ad, model.ImageIds);

            return ad;
        }

<<<<<<< HEAD
        private BaseAd MotorBoatEngineAdFromModel(Dictionary<string, string> form)
        {
            MotorBoatEngineAd motorBoatEngineAd = new MotorBoatEngineAd();

            int selectedTypeId;
            bool result = Int32.TryParse(form["SelectedTypeId"], out selectedTypeId);
            if (result)
                motorBoatEngineAd.Type = _repository.Get<MotorBoatEngineType>(selectedTypeId);

            int selectedYearId;
            result = Int32.TryParse(form["SelectedYearId"], out selectedYearId);
            if (result)
                motorBoatEngineAd.Year = selectedYearId;

            int hP;
            result = Int32.TryParse(form["Hp"], out hP);
            if (result)
                motorBoatEngineAd.Hp = hP;

            int nbCylinders;
            result = Int32.TryParse(form["NbCylinders"], out nbCylinders);
            if (result)
                motorBoatEngineAd.NbCylinder = nbCylinders;

            int nbHours;
            result = Int32.TryParse(form["NbHours"], out nbHours);
            if (result)
                motorBoatEngineAd.NbHours = nbHours;

            return (motorBoatEngineAd);
        }

=======
        public BaseAd GetAdPicturesFromModel(BaseAd ad, String imageIds)
        {
            if (String.IsNullOrEmpty(imageIds))
                return ad;

            bool first = true;

            foreach (String imageId in imageIds.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
            {
                AdImage image = _repository.Get<AdImage>(Guid.Parse(imageId));
                if (first)
                {
                    image.IsPrimary = true;
                    first = false;
                }

                ad.AddImage(image);
            }

            return ad;
        }
>>>>>>> 2f15550545fba679e7e21dca8a57e5f6f0609d3a

        private BaseAd GetCarAdFromModel(Dictionary<string, string> form)
        {
            CarAd carAd = new CarAd();
            int kilometer;
            bool result = Int32.TryParse(form["Km"], out kilometer);
            if (result)
                carAd.Kilometers = kilometer;
            int selectedYearId;
            result = Int32.TryParse(form["SelectedYearId"], out selectedYearId);
            if (result)
                carAd.Year = selectedYearId;
            int selectedFuelId;
            result = Int32.TryParse(form["SelectedFuelId"], out selectedFuelId);
            if (result)
                carAd.Fuel = _repository.Get<CarFuel>(selectedFuelId);
            int selectedBrandId;
            result = Int32.TryParse(form["SelectedBrandId"], out selectedBrandId);
            if (result)
                carAd.Brand = _repository.Get<VehicleBrand>(selectedBrandId);
            return (carAd);
        }
        
        private BaseAd GetSailingBoatAdFromModel(Dictionary<string, string> form)
        {
            SailingBoatAd sailingBoatAd = new SailingBoatAd();

            int selectedTypeId;
            bool result = Int32.TryParse(form["SelectedTypeId"], out selectedTypeId);
            if (result)
                sailingBoatAd.Type = _repository.Get<SailingBoatType>(selectedTypeId);

            int selectedHullTypeId;
            result = Int32.TryParse(form["SelectedHullTypeId"], out selectedHullTypeId);
            if (result)
                sailingBoatAd.HullType = _repository.Get<SailingBoatHullType>(selectedHullTypeId);

            int selectedYearId;
            result = Int32.TryParse(form["SelectedYearId"], out selectedYearId);
            if (result)
                sailingBoatAd.Year = selectedYearId;

            Decimal length;
            result = Decimal.TryParse(form["Length"], out length);
            if (result)
                sailingBoatAd.Length = length;

            return (sailingBoatAd);
        }

        private BaseAd GetMotorBoatAdFromModel(Dictionary<string, string> form)
        {
            MotorBoatAd motorBoatAd = new MotorBoatAd();
            
            int selectedTypeId;
            bool result = Int32.TryParse(form["SelectedTypeId"], out selectedTypeId);
            if (result)
                motorBoatAd.Type = _repository.Get<MotorBoatType>(selectedTypeId);

            int selectedMotorTypeId;
            result = Int32.TryParse(form["SelectedMotorTypeId"], out selectedMotorTypeId);
            if (result)
                motorBoatAd.MotorType = _repository.Get<MotorBoatEngineType>(selectedMotorTypeId);

            int selectedYearId;
            result = Int32.TryParse(form["SelectedYearId"], out selectedYearId);
            if (result)
                motorBoatAd.Year = selectedYearId;

            int hP;
            result = Int32.TryParse(form["Hp"], out hP);
            if (result)
                motorBoatAd.Hp = hP;

            Decimal length;
            result = Decimal.TryParse(form["Length"], out length);
            if (result)
                motorBoatAd.Length = length;

            int nbHours;
            result = Int32.TryParse(form["NbHours"], out nbHours);
            if (result)
                motorBoatAd.NbHours = nbHours;

            return (motorBoatAd);
        }

        private BaseAd GetVehicleAdFromModel(Dictionary<string, string> form)
        {
            VehicleAd vehicleAd = new VehicleAd();
            int kilometer;
            bool result = Int32.TryParse(form["Km"], out kilometer);
            if (result)
                vehicleAd.Kilometers = kilometer;
            int selectedYearId;
            result = Int32.TryParse(form["SelectedYearId"], out selectedYearId);
            if (result)
                vehicleAd.Year = selectedYearId;
            return (vehicleAd);
        }

        private BaseAd GetOtherVehicleAdFromModel(Dictionary<string, string> form)
        {
            OtherVehicleAd otherVehicleAd = new OtherVehicleAd();
            int kilometer;
            bool result = Int32.TryParse(form["Km"], out kilometer);
            if (result)
                otherVehicleAd.Kilometers = kilometer;
            int selectedYearId;
            result = Int32.TryParse(form["SelectedYearId"], out selectedYearId);
            if (result)
                otherVehicleAd.Year = selectedYearId;
            int selectedFuelId;
            result = Int32.TryParse(form["SelectedFuelId"], out selectedFuelId);
            if (result)
                otherVehicleAd.Fuel = _repository.Get<CarFuel>(selectedFuelId);
            return (otherVehicleAd);
        }

        private MotoAd GetMotoAdFromModel(Dictionary<string, string> form)
        {
            MotoAd motoAd = new MotoAd();
            int kilometer;
            bool result = Int32.TryParse(form["Km"], out kilometer);
            if (result)
                motoAd.Kilometers = kilometer;
            int selectedYearId;
            result = Int32.TryParse(form["SelectedYearId"], out selectedYearId);
            if (result)
                motoAd.Year = selectedYearId;
            int selectedBrandId;
            result = Int32.TryParse(form["SelectedBrandId"], out selectedBrandId);
            if (result)
                motoAd.Brand = _repository.Get<MotoBrand>(selectedBrandId);
            int engineSize;
            result = Int32.TryParse(form["EngineSize"], out engineSize);
            if (result)
                motoAd.EngineSize = engineSize;
            return (motoAd);
        }

        #endregion
    }
}
