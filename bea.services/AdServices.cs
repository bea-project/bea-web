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

        public void AddAd(Ad ad)
        {
            _repository.Save(ad);
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
            BaseAd baseAd = new Ad();
            //Category modelCategory = _repository.Get<Category>(model.SelectedCategoryId);
            //if (modelCategory!=null)
            //{
            //    switch (modelCategory.Label)
            //    {
            //        case "Voitures":
            //            baseAd = new CarAd();

            //            break;
            //    }
            //}
            

            if (model.SelectedCityId.HasValue)
                baseAd.City = _repository.Get<City>(model.SelectedCityId.Value);
            if (model.SelectedCategoryId.HasValue)
                baseAd.Category = _repository.Get<Category>(model.SelectedCategoryId.Value);
            baseAd.CreatedBy = _userServices.GetUserFromEmail("bruno.deprez@gmail.com");
            baseAd.Body = model.Body;
            baseAd.CreationDate = DateTime.Now;
            baseAd.Price = model.Price.GetValueOrDefault();
            baseAd.Title = model.Title;
            baseAd.IsOffer = model.IsOffer;
            baseAd.PhoneNumber = model.Telephone;

            return baseAd;
        }
    }
}
