using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Core.Dal;
using Bea.Domain.Location;
using NHibernate;
using NHibernate.Linq;


namespace Bea.Dal.Repository
{
    public class LocationRepository : ILocationRepository
    {
        protected ISessionFactory _sessionFactory;

        public LocationRepository(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public City GetCityFromLabel(string label)
        {
            return _sessionFactory.GetCurrentSession().Query<City>().Where(x => x.Label.Equals(label)).FirstOrDefault();
        }

        public IList<Province> GetAllProvinces()
        {
            return _sessionFactory.GetCurrentSession().Query<Province>().ToList();
        }
    }
}
