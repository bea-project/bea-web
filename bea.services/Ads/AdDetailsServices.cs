using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Core.Dal;
using Bea.Core.Services;
using Bea.Core.Services.Ads;
using Bea.Domain.Ads;
using Bea.Domain.Ads.WaterSport;
using Bea.Models.Details;
using Bea.Models.Details.Vehicles;
using Bea.Models.Details.WaterSport;
using Bea.Tools;

namespace Bea.Services.Ads
{
    public class AdDetailsServices : IAdDetailsServices
    {
        private readonly IAdRepository _adRepository;
        private readonly IHelperService _helperService;

        public AdDetailsServices(IAdRepository adRepository, IHelperService helperService)
        {
            _adRepository = adRepository;
            _helperService = helperService;
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

                case AdTypeEnum.MotoAd:
                    ad = _adRepository.GetAdById<MotoAd>(adId);
                    model = new MotoAdDetailsModel(ad as MotoAd);
                    break;

                case AdTypeEnum.OtherVehiculeAd:
                    ad = _adRepository.GetAdById<OtherVehicleAd>(adId);
                    model = new OtherVehicleAdDetailsModel(ad as OtherVehicleAd);
                    break;

                case AdTypeEnum.SailingBoatAd:
                    ad = _adRepository.GetAdById<SailingBoatAd>(adId);
                    model = new SailingBoatAdDetailsModel(ad as SailingBoatAd, _helperService);
                    break;

                case AdTypeEnum.MotorBoatAd:
                    ad = _adRepository.GetAdById<MotorBoatAd>(adId);
                    model = new MotorBoatAdDetailsModel(ad as MotorBoatAd, _helperService);
                    break;

                case AdTypeEnum.MotorBoatEngineAd:
                    ad = _adRepository.GetAdById<MotorBoatEngineAd>(adId);
                    model = new MotorBoatEngineAdDetailsModel(ad as MotorBoatEngineAd);
                    break;

                case AdTypeEnum.WaterSportAd:
                    ad = _adRepository.GetAdById<WaterSportAd>(adId);
                    model = new WaterSportAdDetailsModel(ad as WaterSportAd);
                    break;

                default:
                    return null;
            }

            // Compute whether or not this Ad is new (less than 3 days)
            model.IsNew = ad.CreationDate > _helperService.GetCurrentDateTime().AddHours(-72);

            return model;
        }

    }
}
