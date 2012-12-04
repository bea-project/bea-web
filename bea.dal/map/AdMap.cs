using System;
using System.Collections.Generic;
using System.Text;
using FluentNHibernate.Mapping;
using bea.dal.entities;

namespace bea.dal.map
{
    public class AdMap : ClassMap<Ad>
    {
        public AdMap()
        {
            Table("annonce");
            Id(x => x.adId).GeneratedBy.Identity().Column("ad_id");
            References(x => x.createdBy).Column("user_fk");
            Map(x => x.title).Column("title");
            Map(x => x.body).Column("body");
        }
    }
}