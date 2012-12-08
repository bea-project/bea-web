using System;
using System.Collections.Generic;
using System.Text;
using bea.domain;
using bea.domain.location;
using FluentNHibernate.Mapping;

namespace bea.dal.map.location
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
