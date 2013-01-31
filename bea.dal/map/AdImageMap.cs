using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain;
using FluentNHibernate.Mapping;

namespace Bea.Dal.Map
{
    public class AdImageMap : ClassMap<AdImage>
    {
        public AdImageMap()
        {
            Id(x => x.Id).GeneratedBy.Assigned();
            Map(x => x.IsPrimary).Not.Nullable().Default("false");
            Map(x => x.UploadedDate);
            References(x => x.BaseAd).Nullable().LazyLoad();
        }
    }
}
