using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain.Reference;
using FluentNHibernate.Mapping;

namespace Bea.Dal.Map.Reference
{
    public class SpamAdTypeMap : ClassMap<SpamAdType>
    {
        public SpamAdTypeMap()
        {
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Label);
        }
    }
}
