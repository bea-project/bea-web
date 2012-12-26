using System;
using System.Collections.Generic;
using System.Text;
using Bea.Domain.Location;
using FluentNHibernate.Mapping;

namespace Bea.Dal.map.Location
{
    public class ProvinceMap : ClassMap<Province>
    {
        public ProvinceMap()
        {
            Table("province");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Label).Not.Nullable();
            HasMany<City>(x => x.Cities).AsBag().Cascade.SaveUpdate().Inverse();
        }
    }
}
