using Bea.Domain.Reference;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bea.Dal.Map.Reference
{
    public class SailingBoatTypeMap : ClassMap<SailingBoatType>
    {
        public SailingBoatTypeMap()
        {
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Label).Not.Nullable();
        }
    }
}
