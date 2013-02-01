using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Bea.Domain.Ads;

namespace Bea.Domain
{
    public class AdImage
    {
        public virtual Guid Id { get; set; }
        public virtual bool IsPrimary { get; set; }
        public virtual DateTime UploadedDate { get; set; }
        public virtual BaseAd BaseAd { get; set; }
    }
}
