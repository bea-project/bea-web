using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Bea.Domain.Reference;

namespace Bea.Dal.Map.Reference
{
    public class MotoBrandMap : ClassMap<MotoBrand>
    {
        public MotoBrandMap()
        {
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Label).Not.Nullable();
            Map(x => x.IsMainBrand).Not.Nullable();
        }
    }
}
