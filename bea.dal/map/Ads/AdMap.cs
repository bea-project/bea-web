using System;
using System.Collections.Generic;
using System.Text;
using Bea.Domain;
using Bea.Domain.Location;
using FluentNHibernate.Mapping;
using Bea.Domain.Categories;
using Bea.Domain.Ads;

namespace Bea.Dal.Map.Ads
{
    public class AdMap : SubclassMap<Ad>
    {
        public AdMap()
        {
            Map(x => x.Title).Not.Nullable();
            Map(x => x.Body).Not.Nullable();
            Map(x => x.CreationDate).Not.Nullable();
            Map(x => x.Price).Nullable();
            Map(x => x.IsOffer).Not.Nullable();
            Map(x => x.PhoneNumber);

            References<User>(x => x.CreatedBy).Not.Nullable();
            References<City>(x => x.City).Not.Nullable();
            References<Category>(x => x.Category).Not.Nullable();
        }
    }
}