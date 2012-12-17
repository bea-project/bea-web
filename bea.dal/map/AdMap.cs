using System;
using System.Collections.Generic;
using System.Text;
using Bea.Domain;
using Bea.Domain.Location;
using FluentNHibernate.Mapping;

namespace Bea.Dal.Map
{
    public class AdMap : ClassMap<Ad>
    {
        public AdMap()
        {
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Title).Not.Nullable();
            Map(x => x.Body).Not.Nullable();
            Map(x => x.CreationDate).Not.Nullable();
            Map(x => x.Price).Not.Nullable();

            References<User>(x => x.CreatedBy).Not.Nullable();
            References<City>(x => x.City).Not.Nullable();
            
            HasMany<AdImage>(x => x.Images).Inverse().LazyLoad().Cascade.Delete();
        }
    }
}