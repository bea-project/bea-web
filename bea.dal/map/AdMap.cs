using System;
using System.Collections.Generic;
using System.Text;
using FluentNHibernate.Mapping;
using bea.dal.entities;
using bea.dal.entites.location;

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