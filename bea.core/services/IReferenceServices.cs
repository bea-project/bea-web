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
        Dictionary<int, string> GetAllYears(int nbYears);
    }

}
