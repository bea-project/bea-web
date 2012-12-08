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
            Id(x => x.Id).GeneratedBy.Identity();
            References<User>(x => x.CreatedBy).Not.Nullable();
            References<City>(x => x.City).Not.Nullable();
            Map(x => x.Title).Not.Nullable();
            Map(x => x.Body).Not.Nullable();
        }
    }
}