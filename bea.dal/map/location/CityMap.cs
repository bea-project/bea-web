using System;
using System.Collections.Generic;
using System.Text;
using Bea.Domain;
using Bea.Domain.Ads;
using Bea.Domain.Location;
using FluentNHibernate.Mapping;

namespace Bea.Dal.map.Location
{
    public class CityMap : ClassMap<City>
    {
        public CityMap()
        {
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Label).Not.Nullable();
            References<Province>(x => x.Province);
            HasMany<BaseAd>(x => x.Ads).AsBag();
        }
    }
}
