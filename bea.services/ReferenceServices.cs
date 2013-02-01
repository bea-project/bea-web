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
       
        private readonly IRepository _repository;

        public ReferenceServices(IRepository repository)
        {
            _repository = repository;
        }
        
        public IList<MotoBrand> GetAllMotoBrands()
        {
            return _repository.GetAll<MotoBrand>().ToList();
        }

        public IList<VehicleBrand> GetAllCarBrands()
        {
            return _repository.GetAll<VehicleBrand>().Where(x => x.IsMainBrand).ToList();
        }

        public IList<CarFuel> GetAllCarFuels()
        {
            return _repository.GetAll<CarFuel>().ToList();
        }

        public IList<MotorBoatType> GetAllMotorBoatTypes()
        {
            return _repository.GetAll<MotorBoatType>().ToList();
        }

        public IList<MotorBoatEngineType> GetAllMotorBoatEngineTypes()
        {
            return _repository.GetAll<MotorBoatEngineType>().ToList();
        }

        public IList<SailingBoatHullType> GetAllSailingBoatHullTypes()
        {
            return _repository.GetAll<SailingBoatHullType>().ToList();
        }

        public IList<SailingBoatType> GetAllSailingBoatTypes()
        {
            return _repository.GetAll<SailingBoatType>().ToList();
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

        public IList<WaterSportType> GetAllWaterSportTypes()
        {
            return _repository.GetAll<WaterSportType>().ToList();
        }

        public IList<RealEstateType> GetAllRealEstateTypes()
        {
            return _repository.GetAll<RealEstateType>().ToList();
        }
    }
}
