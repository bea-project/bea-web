using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Bea.Core.Dal;
using Bea.Core.Services.Admin;
using Bea.Domain.Admin;
using Bea.Domain.Ads;
using Bea.Domain.Reference;
using Bea.Models.Request;
using Bea.Tools;

namespace Bea.Services.Admin
{
    public class SpamAdServices : ISpamAdServices
    {
        private IAdRepository _adRepository;
        private IRepository _repository;
        private IHelperService _helper;

        public SpamAdServices(IAdRepository adRepository, IRepository repository, IHelperService helper)
        {
            _adRepository = adRepository;
            _repository = repository;
            _helper = helper;
        }

        public SpamAdRequestModel CanSpamRequestAd(long adId)
        {
            SpamAdRequestModel result = new SpamAdRequestModel();

            if (!_adRepository.CanDeleteAd(adId))
            {
                result.InfoMessage = "Cette annonce n'existe pas ou plus.";
                result.CanSignal = false;
                return result;
            }

            result.CanSignal = true;
            result.AdId = adId;

            return result;
        }

        public SpamAdRequestModel SpamRequestAd(SpamAdRequestModel model)
        {
            SpamAdRequestModel result = CanSpamRequestAd(model.AdId);

            if (!result.CanSignal)
                return result;

            BaseAd ad = _adRepository.GetAdById<BaseAd>(model.AdId);
            
            this.CanSpamRequestAd(model.AdId);

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                SpamAdRequest req = new SpamAdRequest();
                req.Ad = ad;
                req.RequestDate = _helper.GetCurrentDateTime();
                req.RequestorEmailAddress = model.RequestorEmail;
                req.SpamType = _repository.Get<SpamAdType>(model.SelectedSpamAdTypeId);
                req.Description = model.Description;

                _repository.Save(req);
                scope.Complete();
            }

            result.InfoMessage = "Votre signalement a correctement été transmis. Merci de votre précieuse aide dans la chasse aux mauvaises annonces !";
            result.CanSignal = false;

            return result;
        }
    }
}
