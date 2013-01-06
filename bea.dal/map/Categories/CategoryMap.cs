using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Bea.Domain;
using Bea.Domain.Categories;
using Bea.Domain.Ads;

namespace Bea.Dal.Map.Categories
{
    public class CategoryMap : ClassMap<Category>
    {
        public CategoryMap()
        {
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Label).Not.Nullable();
            References<CategoryGroup>(x => x.CategoryGrp);
            HasMany<BaseAd>(x => x.Ads).AsBag();
        }
    }
}
