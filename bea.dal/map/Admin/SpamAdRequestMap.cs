using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain.Admin;
using FluentNHibernate.Mapping;

namespace Bea.Dal.Map.Admin
{
    public class SpamAdRequestMap : ClassMap<SpamAdRequest>
    {
        public SpamAdRequestMap()
        {
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.IsSpam);
            References(x => x.Ad);
        }
    }
}
