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
            return _sessionFactory.GetCurrentSession().Query<City>().Where(x => x.Label.Equals(label)).SingleOrDefault();
        }

        public City GetCityFromLabelUrlPart(string labelUrlPart)
        {
            return _sessionFactory.GetCurrentSession().Query<City>().Where(x => x.LabelUrlPart.Equals(labelUrlPart)).SingleOrDefault();
        }

        public IEnumerable<Province> GetAllProvinces()
        {
            return _sessionFactory.GetCurrentSession().Query<Province>().OrderBy(p => p.Label);
        }

        public IEnumerable<City> GetCitiesFromProvince(int provinceId)
        {
            return _sessionFactory.GetCurrentSession().Query<City>().Where(x => x.Province.Id==provinceId).OrderBy(c => c.Label);
        }

    }
}
