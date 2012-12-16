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
            Id(x => x.Id).GeneratedBy.GuidComb();
            Map(x => x.IsPrimary).Not.Nullable().Default("false");
            Map(x => x.FileName);
            Map(x => x.ImageBytes).LazyLoad();
            Map(x => x.ImageThumbnailBytes).LazyLoad();
            References(x => x.Ad).Nullable().LazyLoad();
        }
    }
}
