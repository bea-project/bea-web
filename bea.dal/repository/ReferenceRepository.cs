using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Core.Dal;
using Bea.Domain;
using NHibernate;
using NHibernate.Linq;
using Bea.Domain.Reference;

namespace Bea.Dal.Repository
{
    public class ReferenceRepository : IReferenceRepository
    {
        protected ISessionFactory _sessionFactory;

        public ReferenceRepository(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public IList<VehicleBrand> GetAllCarBrands()
        {
            return _sessionFactory.GetCurrentSession().Query<VehicleBrand>().ToList();
        }

        public IList<CarFuel> GetAllCarFuels()
        {
            return _sessionFactory.GetCurrentSession().Query<CarFuel>().ToList();
        }
    }
}
