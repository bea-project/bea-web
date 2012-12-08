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
            Table("city");
            Id(x => x.cityId).GeneratedBy.Identity();
            Map(x => x.label).Not.Nullable();
            HasMany<Ad>(x => x.ads).AsBag();
            //HasMany<District>(x => x.districts).AsBag();
        }
    }
}
