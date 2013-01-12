using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Bea.Domain.Reference;

namespace Bea.Dal.Map.Reference
{
    class CarBrandMap : ClassMap<VehicleBrand>
    {
        public CarBrandMap()
        {
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Label).Not.Nullable();
        }
    }
}
