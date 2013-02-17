using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Models.Delete;

namespace Bea.Core.Services.Ads
{
    public interface IAdDeletionServices
    {
        /// <summary>
        /// Returns whether or not an ad is ready for deletion
        /// </summary>
        /// <param name="adId">The Ad id</param>
        /// <returns>The model for Ad deletion</returns>
        DeleteAdModel DeleteAd(long adId);

        /// <summary>
        /// Performs an Ad deletion operation
        /// (by first checking the password againts the one in the ad)
        /// </summary>
        /// <param name="model">The ad informations</param>
        /// <returns>The result of the attempt to delete the Ad</returns>
        DeleteAdModel DeleteAd(DeleteAdModel model);
    }
}
