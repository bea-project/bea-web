using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Core.dal;
using Bea.Domain;
using Bea.Domain.location;
using NHibernate;
using NHibernate.Linq;

namespace Bea.Dal.repository
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
