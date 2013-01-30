using Bea.Domain.Reference;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bea.Dal.Map.Reference
{
    class SailingBoatHullTypeMap : ClassMap<SailingBoatHullType>
    {
        public SailingBoatHullTypeMap()
        {
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Label).Not.Nullable();
        }
    }
}
