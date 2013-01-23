using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Transactions;
using Bea.Core.Dal;
using Bea.Core.Services;
using Bea.Domain.Ads;
using Bea.Domain.Search;
using Bea.Models.Create;

namespace Bea.Services
{
    public class AdActivationServices : IAdActivationServices
    {
        private readonly IRepository _repository;

        public AdActivationServices(IRepository repository)
        {
            _repository = repository;
        }

        public AdActivationResultModel ActivateAd(long adId, String activationToken)
        {
            AdActivationResultModel model = new AdActivationResultModel();
            model.AdId = adId;

            BaseAd adToValidate = _repository.Get<BaseAd>(adId);

            if (adToValidate == null)
            {
                model.InfoMessage = "Cette annonce n'existe pas ou a expiré.";
                model.IsActivated = false;
                return model;
            }

            if (adToValidate.IsActivated)
            {
                model.InfoMessage = "Cette annonce a déjà été activée.";
                model.IsActivated = true;
                return model;
            }

            if (!adToValidate.ActivationToken.Equals(activationToken))
            {
                model.InfoMessage = "Vous ne pouvez pas activer cette annonce.";
                model.IsActivated = false;
                return model;
            }

            // Actually activate the ad
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                adToValidate.IsActivated = true;
                _repository.Save(adToValidate);
                SearchAdCache searchAdCache = new SearchAdCache(adToValidate);
                _repository.Save(searchAdCache);
                _repository.Flush();
                scope.Complete();
            }

            model.InfoMessage = "Merci d'avoir activé votre annonce.";
            model.IsActivated = true;

            return model;
        }

        public string GenerateActivationToken()
        {
            return Guid.NewGuid().ToString();
        }

        //TODO: modify from and reply-to addresses
        public void SendActivationEmail(BaseAd ad)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("beaprojectnc@gmail.com");
            mail.To.Add(ad.CreatedBy.Email);
            mail.Subject = String.Format("BEA Activez votre annonce\"{0}\"", ad.Title);
            mail.ReplyToList.Add("no-reply@bea.nc");
            mail.Body = String.Format("Bonjour,"
                 + "Vous venez de créer votre annonce. Afin que celle-ci soit visible sur bea.nc, nous avons besoin de valider votre adresse email."
                         + "Pour cela, merci de bien vouloir cliquer sur le lien ci-dessous pour confirmer votre annonce."
                         + "http://bea.nc/Ad/Activate/{0}/{1}", ad.Id, ad.ActivationToken);

            SendEmailAsync(mail, ad);
        }

        public void SendEmailAsync(MailMessage message, BaseAd ad)
        {
            SmtpClient SmtpServer = new SmtpClient();
            SmtpServer.SendAsync(message, ad.Id);
        }
    }
}
