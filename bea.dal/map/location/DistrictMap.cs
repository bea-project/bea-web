using System;
using System.Collections.Generic;
using System.Text;
using FluentNHibernate.Mapping;
using bea.dal.entites.location;

namespace bea.dal.map.location
{
    public class DistrictMap : ClassMap<District>
    {
        public DistrictMap()
        {
            Table("district");
            Id(x => x.districtId).GeneratedBy.Identity();
            Map(x => x.label).Not.Nullable();
        }
    }
}
