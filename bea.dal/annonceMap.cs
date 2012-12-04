using System;
using System.Collections.Generic;
using System.Text;
using FluentNHibernate.Mapping;
using bea.dal.entities;

namespace bea.dal.map
{
    public class AnnonceMap : ClassMap<Annonce>
    {
        public AnnonceMap()
        {
            Table("annonce");
            Id(x => x.annonceId).GeneratedBy.Identity().Column("annonce_id");
            References(x => x.posteur).Column("posteur_fk");
            Map(x => x.titre).Column("titre");
            Map(x => x.corps).Column("corps");
        }
    }
}
