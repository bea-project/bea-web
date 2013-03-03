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
            Map(x => x.LabelUrlPart).Not.Nullable();
            Map(x => x.Type).Not.Nullable();
            References(x => x.ParentCategory);
            HasMany(x => x.SubCategories).KeyColumn("ParentCategory_Id").Cascade.All();
            HasMany(x => x.Ads).AsBag();
        }
    }
}
