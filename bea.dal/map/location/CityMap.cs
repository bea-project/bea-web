using System;
using System.Collections.Generic;
using System.Text;
using FluentNHibernate.Mapping;
using bea.dal.entites.location;
using bea.dal.entities;

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
