using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Bea.Core.Dal;
using Bea.Core.Services;
using Bea.Core.Services.Ads;
using Bea.Domain;
using Bea.Domain.Ads;
using Bea.Domain.Reference;
using Bea.Domain.Search;
using Bea.Models.Delete;

namespace Bea.Services.Ads
{
    public class AdDeletionServices : IAdDeletionServices
    {
        private readonly IAdRepository _adRepository;
        private readonly IRepository _repository;
        private readonly IHelperService _helper;

        public AdDeletionServices(IAdRepository adRepository, IRepository repository, IHelperService helper)
        {
            _adRepository = adRepository;
            _repository = repository;
            _helper = helper;
        }

        public DeleteAdModel DeleteAd(long adId)
        {
            DeleteAdModel result = new DeleteAdModel();
            result.AdId = adId;
            result.CanDeleteAd = _adRepository.CanDeleteAd(adId);

            if (!result.CanDeleteAd)
                result.InfoMessage = "Cette annonce n'existe pas ou plus.";

            return result;
        }

        public DeleteAdModel DeleteAd(DeleteAdModel model)
        {
            DeleteAdModel result = new DeleteAdModel();
            result.AdId = model.AdId;

            BaseAd ad = _adRepository.GetAdById<BaseAd>(model.AdId);

            if (ad == null || ad.IsDeleted)
            {
                result.InfoMessage = "Cette annonce n'existe pas ou plus.";
                result.CanDeleteAd = false;
                return result;
            }

            if (model.Password != ad.CreatedBy.Password)
            {
                result.NbTry = ++model.NbTry;
                result.SelectedDeletionReasonId = model.SelectedDeletionReasonId;
                result.CanDeleteAd = true;
                return result;
            }

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                ad.IsDeleted = true;
                ad.DeletionDate = _helper.GetCurrentDateTime();
                if (model.SelectedDeletionReasonId.HasValue)
                    ad.DeletedReason = _repository.Get<DeletionReason>(model.SelectedDeletionReasonId);
                _repository.Save<BaseAd>(ad);

                SearchAdCache adCache = _repository.Get<SearchAdCache>(ad.Id);
                if (adCache != null)
                    _repository.Delete<SearchAdCache>(adCache);

                result.IsDeleted = true;
                result.InfoMessage = "Votre annonce a correctement été supprimée. Elle n'est plus disponible à la recherche.";

                scope.Complete();
            }

            return result;
        }
    }
}
