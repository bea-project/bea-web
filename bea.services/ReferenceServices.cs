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

            // Initialize static lists
            _kmBrackets = new Dictionary<int, string>();
            _kmBrackets.Add(1, "jusqu'à 5000 Km");
            _kmBrackets.Add(2, "5000 - 10000 Km");
            _kmBrackets.Add(3, "10000 - 20000 Km");
            _kmBrackets.Add(4, "20000 - 50000 Km");
            _kmBrackets.Add(5, "50000 Km et plus");

            _ageBrackets = new Dictionary<int, string>();
            _ageBrackets.Add(1, "jusqu'à 2 ans");
            _ageBrackets.Add(2, "2 - 5 ans");
            _ageBrackets.Add(3, "5 - 10 ans");
            _ageBrackets.Add(4, "10 ans et plus");
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

        public Dictionary<int, string> GetAllKms()
        {
            Dictionary<int, string> kms = new Dictionary<int, string>();
            kms.Add(0, "0");
            kms.Add(10000, "10000");
            kms.Add(20000, "20000");
            kms.Add(30000, "30000");
            kms.Add(40000, "40000");
            kms.Add(50000, "50000");
            kms.Add(60000, "10000");
            kms.Add(70000, "10000");
            kms.Add(80000, "10000");
            kms.Add(90000, "10000");
            kms.Add(100000, "100000");
            kms.Add(125000, "125000");
            kms.Add(150000, "150000");
            kms.Add(175000, "175000");
            kms.Add(200000, "200000");
            kms.Add(225000, "225000");
            kms.Add(250000, "250000");
            return kms;
        }

        public Dictionary<int, string> GetAllEngineSizes()
        {
            Dictionary<int, string> engineSizes = new Dictionary<int, string>();
            engineSizes.Add(0, "0");
            engineSizes.Add(50, "50");
            engineSizes.Add(80, "80");
            engineSizes.Add(125, "125");
            engineSizes.Add(250, "250");
            engineSizes.Add(500, "500");
            engineSizes.Add(600, "600");
            engineSizes.Add(750, "750");
            engineSizes.Add(1000, "1000");
            return engineSizes;
        }

        public Dictionary<int, string> GetAllHps()
        {
            Dictionary<int, string> hps = new Dictionary<int, string>();
            hps.Add(0, "0");
            hps.Add(50, "50");
            hps.Add(100, "100");
            hps.Add(150, "150");
            hps.Add(200, "200");
            hps.Add(250, "250");
            hps.Add(300, "300");
            return hps;
        }

        public Dictionary<int, string> GetAllMotorBoatLength()
        {
            Dictionary<int, string> motorBoatLength = new Dictionary<int, string>();
            motorBoatLength.Add(0, "0");
            motorBoatLength.Add(5, "5");
            motorBoatLength.Add(8, "8");
            motorBoatLength.Add(10, "10");
            motorBoatLength.Add(15, "15");
            return motorBoatLength;
        }

        public Dictionary<int, string> GetAllSailingBoatLength()
        {
            Dictionary<int, string> motorBoatLength = new Dictionary<int, string>();
            motorBoatLength.Add(0, "0");
            motorBoatLength.Add(5, "5");
            motorBoatLength.Add(10, "10");
            motorBoatLength.Add(15, "15");
            motorBoatLength.Add(20, "20");
            motorBoatLength.Add(25, "25");
            return motorBoatLength;
        }

        public Dictionary<int, string> GetAllRealEstateNbRoom()
        {
            Dictionary<int, string> realEstateNbRooms = new Dictionary<int, string>();
            realEstateNbRooms.Add(0, "0");
            realEstateNbRooms.Add(1, "1");
            realEstateNbRooms.Add(2, "2");
            realEstateNbRooms.Add(3, "3");
            realEstateNbRooms.Add(4, "4");
            realEstateNbRooms.Add(5, "5");
            realEstateNbRooms.Add(6, "6");
            realEstateNbRooms.Add(7, "7");
            realEstateNbRooms.Add(8, "8");
            return realEstateNbRooms;
        }

        public IList<WaterSportType> GetAllWaterSportTypes()
        {
            return _repository.GetAll<WaterSportType>().ToList();
        }

        public IList<RealEstateType> GetAllRealEstateTypes()
        {
            return _repository.GetAll<RealEstateType>().ToList();
        }

        public IList<DeletionReason> GetAllDeletionReasons()
        {
            return _repository.GetAll<DeletionReason>();
        }

        #region static lists

        private IDictionary<int, string> _kmBrackets;
        public IDictionary<int, string> GetAllKmBrackets()
        {
            return _kmBrackets;
        }

        private IDictionary<int, string> _ageBrackets;
        public IDictionary<int, string> GetAllAgeBrackets()
        {
            return _ageBrackets;
        }

        #endregion
    }
}
