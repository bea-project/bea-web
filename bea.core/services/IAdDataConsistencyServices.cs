using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain.Ads;
using Bea.Models;

namespace Bea.Core.Services
{
    public interface IAdDataConsistencyServices
    {
        IDictionary<string, string> GetAdDataConsistencyErrors(BaseAd ad);

        //if other fields like province appear in the future, could be better to move to a dictionary
        Dictionary<string, string> GetAdDataConsistencyErrors(BaseAd ad, int? provinceId);
        Dictionary<string, string> GetDataConsistencyErrors(ContactUserFormModel model);
    }
}
