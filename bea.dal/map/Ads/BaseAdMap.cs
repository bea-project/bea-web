using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain;
using Bea.Domain.Ads;
using Bea.Domain.Categories;
using Bea.Domain.Location;
using FluentNHibernate.Mapping;

namespace Bea.Dal.Map.Ads
{
    public class BaseAdMap : ClassMap<BaseAd>
    {
        public BaseAdMap()
        {
            Id(x => x.Id).GeneratedBy.Identity();

            Map(x => x.IsActivated).Default("0");
            Map(x => x.ActivationToken);

            Map(x => x.IsDeleted).Default("0");
            Map(x => x.DeletionDate);
            References(x => x.DeletedReason);

            References<User>(x => x.CreatedBy).Not.Nullable();
            References<Category>(x => x.Category);
            References<City>(x => x.City);
            HasMany<AdImage>(x => x.Images).Inverse().LazyLoad().Cascade.AllDeleteOrphan();
        }
    }
}
