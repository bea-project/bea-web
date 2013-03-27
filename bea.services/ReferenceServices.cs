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
            _kmBrackets = new Dictionary<int, BracketItemReference>();
            _kmBrackets.Add(1, new BracketItemReference { Id = 1, Label = "jusqu'à 5000 Km", LowValue = 0, HighValue = 5999 });
            _kmBrackets.Add(2, new BracketItemReference { Id = 2, Label = "5000 - 10000 Km", LowValue = 4500, HighValue = 10999 });
            _kmBrackets.Add(3, new BracketItemReference { Id = 3, Label = "10000 - 20000 Km", LowValue = 9500, HighValue = 20999 });
            _kmBrackets.Add(4, new BracketItemReference { Id = 4, Label = "20000 - 50000 Km", LowValue = 19500, HighValue = 50999 });
            _kmBrackets.Add(5, new BracketItemReference { Id = 5, Label = "50000 Km et plus", LowValue = 49500, HighValue = 1000000 });
            
            _ageBrackets = new Dictionary<int, BracketItemReference>();
            _ageBrackets.Add(1, new BracketItemReference { Id = 1, Label = "jusqu'à 2 ans", LowValue = 0, HighValue = 3 });
            _ageBrackets.Add(2, new BracketItemReference { Id = 2, Label = "2 - 5 ans", LowValue = 1, HighValue = 6 });
            _ageBrackets.Add(3, new BracketItemReference { Id = 3, Label = "5 - 10 ans", LowValue = 4, HighValue = 11 });
            _ageBrackets.Add(4, new BracketItemReference { Id = 4, Label = "10 ans et plus", LowValue = 9, HighValue = 200 });


            _engineSierBrackets = new Dictionary<int, BracketItemReference>();
            _engineSierBrackets.Add(1, new BracketItemReference { Id = 1, Label = "jusqu'à 50 cm3", LowValue = 0, HighValue = 50 });
            _engineSierBrackets.Add(2, new BracketItemReference { Id = 2, Label = "50 - 125 cm3", LowValue = 50, HighValue = 125 });
            _engineSierBrackets.Add(3, new BracketItemReference { Id = 3, Label = "125 - 250 cm3", LowValue = 125, HighValue = 301 });
            _engineSierBrackets.Add(4, new BracketItemReference { Id = 4, Label = "250 - 650 cm3", LowValue = 250, HighValue = 651 });
            _engineSierBrackets.Add(5, new BracketItemReference { Id = 5, Label = "650 - 1000 cm3", LowValue = 649, HighValue = 1001 });
            _engineSierBrackets.Add(6, new BracketItemReference { Id = 6, Label = "1000 cm3 et plus", LowValue = 999, HighValue = 5000 });

            _realEstateNbRooms = new Dictionary<int, string>();
            _realEstateNbRooms.Add(1, "1");
            _realEstateNbRooms.Add(2, "2");
            _realEstateNbRooms.Add(3, "3");
            _realEstateNbRooms.Add(4, "4");
            _realEstateNbRooms.Add(5, "5");
            _realEstateNbRooms.Add(6, "6");
            _realEstateNbRooms.Add(7, "7");
            _realEstateNbRooms.Add(8, "8 et plus");

            int currentYear = DateTime.Now.Year;
            int minYear = currentYear - 40;
            _years = new Dictionary<int, string>();
            for (int year = currentYear; year >= minYear; year--)
                _years.Add(year, year.ToString());
            _years[minYear] = minYear + " ou avant";
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

        private IDictionary<int, BracketItemReference> _kmBrackets;
        public IDictionary<int, BracketItemReference> GetAllKmBrackets()
        {
            return _kmBrackets;
        }

        private IDictionary<int, BracketItemReference> _ageBrackets;
        public IDictionary<int, BracketItemReference> GetAllAgeBrackets()
        {
            return _ageBrackets;
        }

        private IDictionary<int, BracketItemReference> _engineSierBrackets;
        public IDictionary<int, BracketItemReference> GetAllEngineSizeBrackets()
        {
            return _engineSierBrackets;
        }

        private IDictionary<int, String> _realEstateNbRooms;
        public IDictionary<int, String> GetAllRealEstateNbRoom()
        {
            return _realEstateNbRooms;
        }

        private IDictionary<int, string> _years;
        public IDictionary<int, string> GetAllYears()
        {
            return _years;
        }

        #endregion
    }
}
