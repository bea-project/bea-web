using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain;
using Bea.Domain.Ads;
using Bea.Domain.Categories;
using Bea.Domain.Location;
using Bea.Domain.Reference;
using FluentNHibernate.Mapping;

namespace Bea.Dal.Map.Ads
{
    public class RealEstateAdMap : SubclassMap<RealEstateAd>
    {
        public RealEstateAdMap()
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

            Map(x => x.RoomsNumber);
            Map(x => x.SurfaceArea);
            Map(x => x.FloorNumber);
            Map(x => x.IsFurnished).Default("0");
            References<RealEstateType>(x => x.RealEstateType);
        }
    }
}
