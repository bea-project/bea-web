using System;
using System.Collections.Generic;
using System.Transactions;
using Bea.Core.Dal;
using Bea.Core.Services;
using Bea.Core.Services.Ads;
using Bea.Domain;
using Bea.Domain.Ads;
using Bea.Domain.Ads.WaterSport;
using Bea.Domain.Categories;
using Bea.Domain.Location;
using Bea.Domain.Reference;
using Bea.Models.Create;
using Bea.Models.Details;
using Bea.Models.Details.Vehicles;

namespace Bea.Services.Ads
{
    public class AdServices : IAdServices
    {
        private readonly IAdRepository _adRepository;
        private readonly IRepository _repository;
        private readonly IAdActivationServices _adActivationServices;

        public AdServices(IAdRepository adRepository, IRepository repository, IAdActivationServices adActivationServices)
        {
            _adRepository = adRepository;
            _repository = repository;
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


        public IList<BaseAd> GetAdsByEmail(String email)
        {
            return _adRepository.GetAdsByEmail(email);
        }

        public Ad GetAdById(long adId)
        {
            return _adRepository.GetAdById<Ad>(adId);
        }

        public void AddAd(BaseAd ad)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                _repository.Save(ad.CreatedBy);
                _repository.Save(ad);
                _repository.Flush();
                _adActivationServices.SendActivationEmail(ad);
                scope.Complete();
            }
        }

        #region creation

        public BaseAd GetAdFromAdCreateModel(AdvancedAdCreateModel model)
        {
            BaseAd baseAd = GetBaseAdFromCreateModel(model);
            if (!model.SelectedCategoryId.HasValue)
                return baseAd;
            Category selectedCategory = _repository.Get<Category>(model.SelectedCategoryId);
            if(selectedCategory.Type == AdTypeEnum.Ad)
                return baseAd;
            switch (selectedCategory.Type)
            {
                case AdTypeEnum.CarAd:
                    baseAd = GetAdFromCreateModel<CarAd>(baseAd, model);
                    break;
                case AdTypeEnum.RealEstateAd:
                    baseAd = GetAdFromCreateModel<RealEstateAd>(baseAd, model);
                    break;
                case AdTypeEnum.MotoAd:
                    baseAd = GetAdFromCreateModel<MotoAd>(baseAd, model);
                    break;
                case AdTypeEnum.OtherVehiculeAd:
                    baseAd = GetAdFromCreateModel<OtherVehicleAd>(baseAd, model);
                    break;
                case AdTypeEnum.VehiculeAd:
                    baseAd = GetAdFromCreateModel<VehicleAd>(baseAd, model);
                    break;
                case AdTypeEnum.MotorBoatAd:
                    baseAd = GetAdFromCreateModel<MotorBoatAd>(baseAd, model);
                    break;
                case AdTypeEnum.MotorBoatEngineAd:
                    baseAd = GetAdFromCreateModel<MotorBoatEngineAd>(baseAd, model);
                    break;
                case AdTypeEnum.SailingBoatAd:
                    baseAd = GetAdFromCreateModel<SailingBoatAd>(baseAd, model);
                    break;
            }
            return baseAd;
        }

        private T GetAdFromCreateModel<T>(BaseAd baseAd, AdvancedAdCreateModel model) where T : BaseAd, new()
        {
            dynamic ad = new T();
            //Common data
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
            
            //Other Data
            if (model.Km.HasValue)
                ad.Kilometers = model.Km.Value;
            if (model.SelectedYearId.HasValue)
                ad.Year = model.SelectedYearId.Value;
            if (model.IsAutomatic.HasValue)
                ad.IsAutomatic = model.IsAutomatic.Value;
            if (model.SelectedCarBrandId.HasValue)
                ad.Brand = _repository.Get<VehicleBrand>(model.SelectedCarBrandId.Value);
            if (model.SelectedFuelId.HasValue)
                ad.Fuel = _repository.Get<CarFuel>(model.SelectedFuelId);
            if (model.SelectedMotoBrandId.HasValue)
                ad.Brand = _repository.Get<MotoBrand>(model.SelectedMotoBrandId.Value);
            if (model.EngineSize.HasValue)
                ad.EngineSize = model.EngineSize.Value;
            
            if (model.RoomNb.HasValue)
                ad.RoomsNumber = model.RoomNb.Value;
            if (model.SelectedRealEstateTypeId.HasValue)
                ad.Type = _repository.Get<RealEstateType>(model.SelectedRealEstateTypeId);
            if (model.SurfaceArea.HasValue)
                ad.SurfaceArea = model.SurfaceArea.Value;
            if (model.IsFurnished.HasValue)
                ad.IsFurnished = model.IsFurnished.Value;
            if (model.SelectedDistrictId.HasValue)
                ad.District = _repository.Get<District>(model.SelectedDistrictId.Value);

            if (model.Length.HasValue)
                ad.Length = model.Length.Value;
            if(model.SelectedSailingBoatTypeId.HasValue)
                ad.SailingBoatType = _repository.Get<SailingBoatType>(model.SelectedSailingBoatTypeId);
            if (model.SelectedHullTypeId.HasValue)
                ad.HullType = _repository.Get<SailingBoatHullType>(model.SelectedHullTypeId);
            if (model.SelectedMotorBoatTypeId.HasValue)
                ad.MotorBoatType = _repository.Get<MotorBoatType>(model.SelectedMotorBoatTypeId);
            if (model.Hp.HasValue)
                ad.Hp = model.Hp.Value;
            if (model.SelectedMotorBoatEngineTypeId.HasValue)
                ad.MotorType = _repository.Get<MotorBoatEngineType>(model.SelectedMotorBoatEngineTypeId);

            return ad;
        }

        private BaseAd GetBaseAdFromCreateModel(AdvancedAdCreateModel model)
        {
            BaseAd baseAd = new BaseAd();
            if (model.SelectedCityId.HasValue)
                baseAd.City = _repository.Get<City>(model.SelectedCityId.Value);
            if (model.SelectedCategoryId.HasValue)
                baseAd.Category = _repository.Get<Category>(model.SelectedCategoryId.Value);
            baseAd.Body = model.Body;
            baseAd.CreationDate = DateTime.Now;
            baseAd.Price = model.Price.GetValueOrDefault();
            baseAd.Title = model.Title;
            baseAd.IsOffer = model.IsOffer;
            baseAd.PhoneNumber = model.Telephone;
            baseAd.ActivationToken = _adActivationServices.GenerateActivationToken();
            User createdBy = new User();
            createdBy.Firstname = model.Name;
            createdBy.Email = model.Email;
            createdBy.Password = "Password";
            baseAd.CreatedBy = createdBy;
            GetAdPicturesFromModel(baseAd, model.ImageIds);
            return baseAd;
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

        #endregion
    }
}
