using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain.Search;
using FluentNHibernate.Mapping;

namespace Bea.Dal.Map.Search
{
    public class SearchAdCacheMap : ClassMap<SearchAdCache>
    {
        public SearchAdCacheMap()
        {
            Id(x => x.AdId).GeneratedBy.Assigned();
            Map(x => x.AdType);
            Map(x => x.Title);
            Map(x => x.Body);
            Map(x => x.Price);
            Map(x => x.CreationDate);
            Map(x => x.AdImageId);
            References(x => x.Province);
            References(x => x.City);
            References(x => x.Category);
        }
    }
}
