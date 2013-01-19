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

namespace Bea.Services
{
    public class AdServices : IAdServices
    {
        private readonly IAdRepository _adRepository;
        private readonly IRepository _repository;
        private readonly IHelperService _helperService;
        private readonly IUserServices _userServices;


        public AdServices(IAdRepository adRepository, IHelperService helperService, IRepository repository, IUserServices userServices)
        {
            _adRepository = adRepository;
            _helperService = helperService;
            _repository = repository;
            _userServices = userServices;

        }

        public IDictionary<City, int> CountAdsByCities()
        {
            return _adRepository.CountAdsByCity();
        }

        public IDictionary<User, int> CountAdsByUsers()
        {
            return _adRepository.CountAdsByUser();
        }

        public IList<Ad> GetAllAds()
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
            _repository.Save<User>(ad.CreatedBy);
            _repository.Save<BaseAd>(ad);
            SearchAdCache cacheAd = new SearchAdCache(ad);
            _repository.Save<SearchAdCache>(cacheAd);
            _repository.Flush();
        }

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

                default:
                    return null;
            }

            // Compute whether or not this Ad is new (less than 3 days)
            model.IsNew = ad.CreationDate > _helperService.GetCurrentDateTime().AddHours(-72);

            return model;
        }

        public BaseAd GetAdFromModel(AdCreateModel model, Dictionary<string, string> form)
        {
            BaseAd ad = null;

            if (form != null && form.ContainsKey("Type"))
            {
                switch (Int32.Parse(form["Type"]))
                {
                    case (int)AdTypeEnum.CarAd:
                        ad = GeatCarAdFromModel(form);
                        break;
                    case (int)AdTypeEnum.MotoAd:
                        ad = GeatMotoAdFromModel(form);
                        break;
                    case (int)AdTypeEnum.OtherVehiculeAd:
                        ad = GeatOtherVehicleAdFromModel(form);
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

             User createdBy = new User();
             createdBy.Firstname = model.Name;
             createdBy.Email = model.Email;
             createdBy.Password = "Password";

             ad.CreatedBy = createdBy;
             return ad;
        }


        private BaseAd GeatCarAdFromModel(Dictionary<string, string> form)
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

        private BaseAd GeatOtherVehicleAdFromModel(Dictionary<string, string> form)
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

        private MotoAd GeatMotoAdFromModel(Dictionary<string, string> form)
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
                motoAd.Brand = _repository.Get<VehicleBrand>(selectedBrandId);
            int engineSize;
            result = Int32.TryParse(form["EngineSize"], out engineSize);
            if (result)
                motoAd.EngineSize = engineSize;
            return (motoAd);
        }
    }
}
