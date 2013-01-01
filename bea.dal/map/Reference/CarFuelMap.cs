using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Bea.Domain.Reference;

namespace Bea.Dal.Map.Reference
{
    public class CarFuelMap : ClassMap<CarFuel>
    {
        public CarFuelMap()
        {
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Label).Not.Nullable();
        }
    }
}
