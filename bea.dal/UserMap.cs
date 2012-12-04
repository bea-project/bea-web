using System;
using System.Collections.Generic;
using System.Text;
using FluentNHibernate.Mapping;
using bea.dal.entities;

namespace bea.dal.map
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Table("user");
            Id(x => x.userId).GeneratedBy.Identity().Column("user_id");
            Map(x => x.email).Column("email");
            Map(x => x.password).Column("password");
            HasMany<Annonce>(x => x.ads).KeyColumn("user_fk");
        }
    }
}
