using System; 
using System.Collections.Generic; 
using System.Text; 
using FluentNHibernate.Mapping;

namespace bea_dal.map {
    
    
    public class PosteurMap : ClassMap<Posteur> {
        
        public PosteurMap() {
			Table("posteur");
			Id(x => x.posteurEmail).GeneratedBy.Assigned().Column("posteur_email");
			Map(x => x.password).Column("password");
			HasMany<Annonce>(x => x.annonces).KeyColumn("posteur_fk");
        }
    }
}
