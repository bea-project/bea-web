using Bea.Domain;
using Bea.Domain.Ads.WaterSport;
using Bea.Domain.Categories;
using Bea.Domain.Location;
using Bea.Domain.Reference;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bea.Dal.Map.Ads.WaterSport
{
    public class SailingBoatAdMap : SubclassMap<SailingBoatAd>
    {
        public SailingBoatAdMap()
        {
            Map(x => x.Title).Not.Nullable();
            Map(x => x.Body).Not.Nullable();
            Map(x => x.CreationDate).Not.Nullable();
            Map(x => x.Price).Nullable();
            Map(x => x.IsOffer).Not.Nullable();
            Map(x => x.PhoneNumber);

            References<City>(x => x.City).Not.Nullable();
            References<Category>(x => x.Category).Not.Nullable();

            Map(x => x.Length);
            Map(x => x.Year);

            References<SailingBoatType>(x => x.Type);
            References<SailingBoatHullType>(x => x.HullType);
        }
    }
}
