using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain.Reference;
using FluentNHibernate.Mapping;

namespace Bea.Dal.Map.Reference
{
    public class DeletionReasonMap : ClassMap<DeletionReason>
    {
        public DeletionReasonMap()
        {
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Label);
        }
    }
}
