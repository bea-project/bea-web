using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Bea.Domain.Category;

namespace Bea.Dal.Map.Category
{
    public class CategoryGroupMap : ClassMap<CategoryGroup>
    {
        public CategoryGroupMap()
        {
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Label).Not.Nullable();
            HasMany<CategoryElement>(x => x.Categories).AsBag().Cascade.SaveUpdate().Inverse();
        }
    }
}
