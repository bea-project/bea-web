using System;
using System.Collections.Generic;
using System.Text;
using Bea.Domain;
using Bea.Domain.location;
using FluentNHibernate.Mapping;

namespace Bea.Dal.map.location
{
    public class CityMap : ClassMap<City>
    {
        public CityMap()
        {
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Label).Not.Nullable();
            HasMany<Ad>(x => x.Ads).AsBag();
        }
    }
}
