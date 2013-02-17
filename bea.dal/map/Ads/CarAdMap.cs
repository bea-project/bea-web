using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain;
using Bea.Domain.Categories;
using Bea.Domain.Location;
using Bea.Domain.Ads;
using FluentNHibernate.Mapping;
using Bea.Domain.Reference;

namespace Bea.Dal.Map.Ads
{
    public class CarAdMap : SubclassMap<CarAd>
    {
        public CarAdMap()
        {
            Map(x => x.Title).Not.Nullable();
            Map(x => x.Body).Not.Nullable();
            Map(x => x.CreationDate).Not.Nullable();
            Map(x => x.Price).Nullable();
            Map(x => x.IsOffer).Not.Nullable();
            Map(x => x.PhoneNumber);

            References<City>(x => x.City).Not.Nullable();
            References<Category>(x => x.Category).Not.Nullable();

            Map(x => x.Kilometers);
            Map(x => x.Year);
            Map(x => x.IsAutomatic);
            Map(x => x.OtherBrand);
            References<CarFuel>(x => x.Fuel);
            References<VehicleBrand>(x => x.Brand);
        }
    }
}
