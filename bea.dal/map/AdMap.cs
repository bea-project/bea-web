using System;
using System.Collections.Generic;
using System.Text;
using bea.domain;
using bea.domain.location;
using FluentNHibernate.Mapping;

namespace bea.dal.map
{
    public class AdMap : ClassMap<Ad>
    {
        public AdMap()
        {
            Table("annonce");
            Id(x => x.adId).GeneratedBy.Identity();
            References<User>(x => x.createdBy).Not.Nullable();
            References<City>(x => x.location).Not.Nullable();
            Map(x => x.title).Not.Nullable();
            Map(x => x.body).Not.Nullable();
        }
    }
}