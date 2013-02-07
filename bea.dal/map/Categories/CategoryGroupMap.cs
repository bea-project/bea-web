using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Bea.Domain.Categories;

namespace Bea.Dal.Map.Categories
{
    public class CategoryGroupMap : ClassMap<CategoryGroup>
    {
        public CategoryGroupMap()
        {
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Label).Not.Nullable();
            HasMany(x => x.Categories).Cascade.SaveUpdate();
        }
    }
}
