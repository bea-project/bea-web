using System;
using System.Collections.Generic;
using System.Text;
using FluentNHibernate.Mapping;
using Bea.Domain.Location;


namespace Bea.Dal.map.Location
{
    public class DistrictMap : ClassMap<District>
    {
        public DistrictMap()
        {
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Label).Not.Nullable();
            References<City>(x => x.City);
        }
    }
}
