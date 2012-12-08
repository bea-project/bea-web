using System;
using System.Collections.Generic;
using System.Text;
using bea.domain.location;
using FluentNHibernate.Mapping;

namespace bea.dal.map.location
{
    public class ProvinceMap : ClassMap<Province>
    {
        public ProvinceMap()
        {
            Table("province");
            Id(x => x.provinceId).GeneratedBy.Identity();
            Map(x => x.label).Not.Nullable();
            HasMany<City>(x => x.cities).AsBag();
        }
    }
}
