﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Models.Request;
using Bea.Core.Dal;
using Bea.Domain.Ads;
using Bea.Core.Services.Ads;
using Bea.Core.Services;

namespace Bea.Services.Ads
{
    public class AdRequestServices : IAdRequestServices
    {
        private readonly IAdRepository _adRepository;
        private readonly IEmailServices _emailServices;
        private readonly ITemplatingService _templatingServices;
        private readonly IApplicationSettingsProvider _settingsProvider;

        public AdRequestServices(IAdRepository adRepository, IEmailServices emailServices, ITemplatingService templatingServices)
        {
            _adRepository = adRepository;
            _emailServices = emailServices;
            _templatingServices = templatingServices;
        }

        public AdRequestModel RequestAds(AdRequestModel model)
        {
            AdRequestModel result = new AdRequestModel();
            result.Email = model.Email;
            result.CanRequestAd = false;

            List<BaseAd> ads = _adRepository.GetAdsByEmail(model.Email).ToList();
            if (ads.Count == 0)
            {
                result.InfoMessage = "Cette adresse email ne correspond aucune annonce.";
                return result;
            }

            IDictionary<String, String> data = new Dictionary<String, String>();
            data.Add("name", ads[0].CreatedBy.Firstname);
            data.Add("adCount", ads.Count.ToString());
            data.Add("websiteAddress", _settingsProvider.WebsiteAddress);
            data.Add("websiteName", _settingsProvider.WebsiteName);

            IDictionary<String, object[]> list = new Dictionary<String, object[]>();
            list.Add("ads", ads.ToArray());

            String subject = "BEA Vos Annonces";
            String body = _templatingServices.GetTemplatedDocument("AdsRequestEmail.vm", data, list);
            _emailServices.SendEmail(subject, body, model.Email, "no-reply@bea.nc");
            
            result.InfoMessage = "Un email vient de vous etre envoyé avec la liste de vos annonces";
            
            return result;
        }
    }
}
