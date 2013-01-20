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
            
            Map(x => x.IsActivated);
            Map(x => x.ActivationToken);

            References<Category>(x => x.Category);
            HasMany<AdImage>(x => x.Images).Inverse().LazyLoad().Cascade.Delete();
        }
    }
}
