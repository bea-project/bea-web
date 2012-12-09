using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bea.Domain.location
{
    public class Province
    {
        public Province()
        {
            this.Cities = new List<City>();
        }

        public virtual int Id { get; set; }
        public virtual string Label { get; set; }
        public virtual IList<City> Cities { get; set; }
        
        public virtual void AddCity(City city)
        {
            this.Cities.Add(city);
            city.Province = this;
        }
    }
}
