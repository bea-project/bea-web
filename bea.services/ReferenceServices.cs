using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Core.Services;
using Bea.Domain.Reference;
using Bea.Core.Dal;

namespace Bea.Services
{
    public class ReferenceServices : IReferenceServices
    {
        private readonly IReferenceRepository _referenceRepository;

        public ReferenceServices(IReferenceRepository referenceRepository)
        {
            _referenceRepository = referenceRepository;
        }
        
        public IList<VehicleBrand> GetAllCarBrands()
        {
            return _referenceRepository.GetAllCarBrands();
        }

        public IList<CarFuel> GetAllCarFuels()
        {
            return _referenceRepository.GetAllCarFuels();
        }

        public Dictionary<int, string> GetAllYears()
        {
            int minYear = 1960;
            Dictionary<int, string> years = new Dictionary<int,string>();
            for(int year = DateTime.Now.Year;year >= minYear;year--)
                years.Add(year,year.ToString());
            years[minYear] = minYear + " ou avant";
            return years;
        }
    }
}
