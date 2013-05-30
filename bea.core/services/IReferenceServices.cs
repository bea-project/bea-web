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
        IList<DeletionReason> GetAllDeletionReasons();
        IList<T> GetAllReferences<T>();
        
        // Static lists
        Dictionary<int, string> GetAllHps();
        Dictionary<int, string> GetAllMotorBoatLength();
        Dictionary<int, string> GetAllSailingBoatLength();

        IDictionary<int, BracketItemReference> GetAllKmBrackets();
        IDictionary<int, BracketItemReference> GetAllAgeBrackets();
        IDictionary<int, BracketItemReference> GetAllEngineSizeBrackets();
        IDictionary<int, String> GetAllYears();
        IDictionary<int, BracketItemReference> GetAllRealEstateNbRoomsBrackets();
        IDictionary<int, BracketItemReference> GetAllSurfaceAreaBrackets();
    }

}
