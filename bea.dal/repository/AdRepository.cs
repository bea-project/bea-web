using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using bea.core.dal;
using bea.domain;
using bea.domain.location;
using NHibernate;
using NHibernate.Linq;

namespace bea_dal.repository
{
    public class AdRepository : IAdRepository
    {
        protected ISessionFactory _sessionFactory;

        public AdRepository(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public IDictionary<City, int> CountAdsByCity()
        {
            var query = (from ad in _sessionFactory.GetCurrentSession().Query<Ad>()
                        group ad by ad.City into adsByCity
                        select new { City = adsByCity.Key, Count = adsByCity.Count() });

            IDictionary<City, int> result = query.ToList().ToDictionary(x => x.City, x => x.Count);

            return result;
        }

    }
}
