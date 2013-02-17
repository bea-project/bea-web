using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Transactions;
using Bea.Core.Dal;
using Bea.Core.Services;
using Bea.Core.Services.Ads;
using Bea.Domain.Ads;
using Bea.Domain.Search;
using Bea.Models.Create;
using Bea.Models;

namespace Bea.Services.Ads
{
    public class AdActivationServices : IAdActivationServices
    {
        private readonly IRepository _repository;
        private readonly IEmailServices _emailService;
        private readonly ITemplatingService _templatingService;

        public AdActivationServices(IRepository repository, ITemplatingService templatingService, IEmailServices emailService)
        {
            _repository = repository;
            _templatingService = templatingService;
            _emailService = emailService;
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

        public void SendActivationEmail(BaseAd ad)
        {
            IDictionary<String, String> data = new Dictionary<String, String>();
            data.Add("name", ad.CreatedBy.Firstname);
            data.Add("title", ad.Title);
            data.Add("id", ad.Id.ToString());
            data.Add("activationToken", ad.ActivationToken);

            String subject = String.Format("BEA Activez votre annonce\"{0}\"", ad.Title);
            String body = _templatingService.GetTemplatedDocument("ActivationEmail.vm", data);

            _emailService.SendEmail(subject, body, ad.CreatedBy.Email);
        }

        public void SendEmailToUser(ContactUserFormModel model)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(model.Email);
            mail.To.Add(model.EmailTo);
            mail.Subject = String.Format("Bea.nc - Notification : \"{0}\"", model.AdTitle);
            mail.Body = String.Format("Bonjour,"
                 + "un utilisateur du site bea.nc, {0}, vous envoie le message suivant. Vous pouvez lui repondre directement à partir de cet email ou par telephone: {1}.\n\n"
                 + "---------------------------------------------------------------\n\n"
                 + model.EmailBody + "\n\n"
                 + "---------------------------------------------------------------\n\n"
                 + "http://bea.nc/Post/Details/{2}", model.Name, (String.IsNullOrEmpty(model.Telephone)) ? "non communiqué" : model.Telephone, model.AdId);
            SmtpClient SmtpServer = new SmtpClient();
            SmtpServer.SendAsync(mail, null);
        }
    }
}
