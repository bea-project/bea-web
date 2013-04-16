using Bea.Models.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bea.Core.Services.Ads
{
    public interface IAdContactServices
    {
        /// <summary>
        /// Returns whether or not an ad is ready to be contacted
        /// </summary>
        /// <param name="adId">The Ad id</param>
        /// <returns>The model for Ad contact</returns>
        ContactAdModel ContactAd(ContactAdModel model);
    }
}
