using System;
using System.Collections.Generic;
using System.Text;
using Bea.Domain;
using FluentNHibernate.Mapping;

namespace Bea.Dal.map
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Table("user");
            Id(x => x.UserId).GeneratedBy.Identity();
            Map(x => x.Email).Not.Nullable();
            Map(x => x.Password).Not.Nullable();
            Map(x => x.CreationDate);
            HasMany<Ad>(x => x.Ads).AsBag().Cascade.All();
        }
    }
}
