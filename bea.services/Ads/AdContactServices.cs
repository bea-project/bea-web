using Bea.Core.Dal;
using Bea.Core.Services.Ads;
using Bea.Domain;
using Bea.Domain.Ads;
using Bea.Models.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace Bea.Services.Ads
{
    public class AdContactServices : IAdContactServices
    {
        private readonly IAdRepository _adRepository;
        private readonly IRepository _repository;

        public AdContactServices(IAdRepository adRepository, IRepository repository)
        {
            _adRepository = adRepository;
            _repository = repository;
        }

        public ContactAdModel ContactAd(ContactAdModel model)
        {
            //ContactAdModel result = new ContactAdModel(model.AdId);
            BaseAd ad = _adRepository.GetAdById<BaseAd>(model.AdId);
            if (ad == null || ad.IsDeleted)
            {
                model.InfoMessage = "Cette annonce n'existe pas ou plus.";
                model.CanContactAd = false;
                return model;
            }
            //Should never happened, and should be logged for investigation if it does
            if(ad.CreatedBy == null)
            {
                model.InfoMessage = "Impossible de contacter l'annonceur.";
                model.CanContactAd = false;
                return model;
            }
            User user = _repository.Get<User>(ad.CreatedBy.Id);
            if (user == null)
            {
                model.InfoMessage = "Impossible de contacter l'annonceur.";
                model.CanContactAd = false;
                return model;
            }
            model.EmailTo = user.Email;
            SendEmailToUser(model);
            if (model.CopySender)
            {
                model.EmailTo = model.Email;
                SendEmailToUser(model);
            }
            model.CanContactAd = true;
            model.InfoMessage = "Votre message a été envoyé !";
            return model;
        }

        public void SendEmailToUser(ContactAdModel model)
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
