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
            Map(x => x.Description);
            Map(x => x.RequestorEmailAddress);
            Map(x => x.RequestDate);
            Map(x => x.ReviewDate);
            References(x => x.SpamType);
            References(x => x.Ad);
        }
    }
}
