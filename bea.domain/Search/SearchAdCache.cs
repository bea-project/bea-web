using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain.Ads;
using Bea.Domain.Categories;
using Bea.Domain.Location;

namespace Bea.Domain.Search
{
    public class SearchAdCache
    {
        public virtual long AdId { get; set; }
        public virtual String Title { get; set; }
        public virtual String Body { get; set; }
        public virtual Province Province { get; set; }
        public virtual City City { get; set; }
        public virtual Category Category { get; set; }
        public virtual CategoryGroup CategoryGroup { get; set; }
        public virtual Double Price { get; set; }
        public virtual DateTime CreationDate { get; set; }
        public virtual AdTypeEnum AdType { get; set; }
        public virtual String AdImageId { get; set; }

        public SearchAdCache()
        {
        }

        public SearchAdCache(BaseAd ad)
        {
            AdId = ad.Id;
            Title = ad.Title;
            Body = ad.Body;
            Province = ad.City.Province;
            City = ad.City;
            CategoryGroup = ad.Category.CategoryGrp;
            Category = ad.Category;
            Price = ad.Price;
            CreationDate = ad.CreationDate;
            AdType = ad.Category.Type;
            AdImageId = ad.Images.Where(x => x.IsPrimary).Select(x => x.Id.ToString()).SingleOrDefault();
        }
    }
}
