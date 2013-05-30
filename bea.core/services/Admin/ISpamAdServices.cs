using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Models.Request;

namespace Bea.Core.Services.Admin
{
    public interface ISpamAdServices
    {
        SpamAdRequestModel CanSpamRequestAd(long adId);
        SpamAdRequestModel SpamRequestAd(SpamAdRequestModel request);
    }
}
