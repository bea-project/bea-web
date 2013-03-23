using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain.Reference;

namespace Bea.Core.Services
{
    public interface IReferenceServices
    {
        IList<CarFuel> GetAllCarFuels();
        IList<VehicleBrand> GetAllCarBrands();
        IList<MotoBrand> GetAllMotoBrands();
        IList<MotorBoatType> GetAllMotorBoatTypes();
        IList<MotorBoatEngineType> GetAllMotorBoatEngineTypes();
        IList<SailingBoatHullType> GetAllSailingBoatHullTypes();
        IList<SailingBoatType> GetAllSailingBoatTypes();
        IList<WaterSportType> GetAllWaterSportTypes();
        IList<RealEstateType> GetAllRealEstateTypes();
        
        // Static lists
        Dictionary<int, string> GetAllYears(int nbYears);
        Dictionary<int, string> GetAllKms();
        Dictionary<int, string> GetAllEngineSizes();
        Dictionary<int, string> GetAllHps();
        Dictionary<int, string> GetAllMotorBoatLength();
        Dictionary<int, string> GetAllSailingBoatLength();
        Dictionary<int, string> GetAllRealEstateNbRoom();
        IList<DeletionReason> GetAllDeletionReasons();

        IDictionary<int, BracketItemReference> GetAllKmBrackets();
        IDictionary<int, BracketItemReference> GetAllAgeBrackets();
        IDictionary<int, BracketItemReference> GetAllEngineSizeBrackets();
    }

}
