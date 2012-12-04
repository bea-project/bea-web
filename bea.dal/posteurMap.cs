using System;
using System.Collections.Generic;
using System.Text;
using FluentNHibernate.Mapping;
using bea.dal.entities;

namespace bea.dal.map
{
    public class PosteurMap : ClassMap<Posteur>
    {
        public PosteurMap()
        {
            Table("posteur");
            Id(x => x.posteurEmail).Column("posteur_email");
            Map(x => x.password).Column("password");
            HasMany<Annonce>(x => x.annonces).KeyColumn("posteur_fk");
        }
    }
}
