using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bea.domain.location
{
    public class Province
    {
        public Province()
        {
            this.cities = new List<City>();
        }
        public virtual int provinceId { get; set; }
        public virtual string label { get; set; }
        public virtual IList<City> cities { get; set; }
        public virtual void AddCity(City city)
        {
            this.cities.Add(city);
            city.Province = this;
        }
    }
}
