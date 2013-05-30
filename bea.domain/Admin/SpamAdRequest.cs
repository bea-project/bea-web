using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain.Ads;
using Bea.Domain.Reference;

namespace Bea.Domain.Admin
{
    public class SpamAdRequest
    {
        public virtual long Id { get; set; }
        public virtual BaseAd Ad { get; set; }
        public virtual String Description { get; set; }
        public virtual String RequestorEmailAddress { get; set; }
        public virtual SpamAdType SpamType { get; set; }
        public virtual DateTime RequestDate { get; set; }
        public virtual DateTime ReviewDate { get; set; }
        public virtual Boolean IsSpam { get; set; }

        public SpamAdRequest()
        {

        }

        public SpamAdRequest(BaseAd ad)
        {
            this.Ad = ad;
        }
    }
}
