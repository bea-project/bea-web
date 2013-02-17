using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain.Ads;
using Bea.Models.Create;
using Bea.Models;

namespace Bea.Core.Services
{
    public interface IAdActivationServices
    {
        /// <summary>
        /// Activates an Ad based on its Id and a secret activation token
        /// </summary>
        /// <param name="adId">The ad Id</param>
        /// <param name="activationToken">The activation token</param>
        /// <returns>The result model of the activation process</returns>
        AdActivationResultModel ActivateAd(long adId, String activationToken);

        /// <summary>
        /// Generates a random activation token for later Ad activation process
        /// </summary>
        /// <returns></returns>
        String GenerateActivationToken();

        /// <summary>
        /// Sends an email to the Ad creator with the activation link
        /// </summary>
        /// <param name="ad">The ad to send the activation mail to</param>
        void SendActivationEmail(BaseAd ad);

        void SendEmailToUser(ContactUserFormModel model);
    }
}
