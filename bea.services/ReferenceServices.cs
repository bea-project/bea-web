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
        private readonly IRepository _repository;

        public ReferenceServices(IReferenceRepository referenceRepository, IRepository repository)
        {
            _referenceRepository = referenceRepository;
            _repository = repository;
        }
        
        public IList<MotoBrand> GetAllMotoBrands()
        {
            return _repository.GetAll<MotoBrand>().ToList();
        }

        public IList<VehicleBrand> GetAllCarBrands()
        {
            //return _referenceRepository.GetAllCarBrands();
            return _repository.GetAll<VehicleBrand>().ToList();
        }

        public IList<CarFuel> GetAllCarFuels()
        {
            //return _referenceRepository.GetAllCarFuels();
            return _repository.GetAll<CarFuel>().ToList();
        }

        public Dictionary<int, string> GetAllYears(int nbYears)
        {
            
            int currentYear = DateTime.Now.Year;
            int minYear = currentYear - nbYears;
            Dictionary<int, string> years = new Dictionary<int,string>();
            for (int year = currentYear; year >= minYear; year--)
                years.Add(year,year.ToString());
            years[minYear] = minYear + " ou avant";
            return years;
        }
    }
}
