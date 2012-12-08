using System;
using System.Collections.Generic;
using System.Text;
using bea.domain;
using FluentNHibernate.Mapping;

namespace bea.dal.map
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Table("user");
            Id(x => x.userId).GeneratedBy.Identity();
            Map(x => x.email).Not.Nullable();
            Map(x => x.password).Not.Nullable();
            Map(x => x.creationDate);
            HasMany<Ad>(x => x.ads).AsBag().Cascade.All();
        }
    }
}
