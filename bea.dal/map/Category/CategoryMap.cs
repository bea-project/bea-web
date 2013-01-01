using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Bea.Domain.Category;
using Bea.Domain;

namespace Bea.Dal.Map.Category
{
    public class CategoryMap : ClassMap<CategoryElement>
    {
        public CategoryMap()
        {
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Label).Not.Nullable();
            References<CategoryGroup>(x => x.CategoryGrp);
            HasMany<Ad>(x => x.Ads).AsBag();
        }
    }
}
