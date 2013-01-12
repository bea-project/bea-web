using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain.Reference;

namespace Bea.Core.Dal
{
    public interface IReferenceRepository
    {
        /// <summary>
        /// Get all the car brands
        /// </summary>
        /// <returns>A list of car brands</returns>
        IList<VehicleBrand> GetAllCarBrands();

        /// <summary>
        /// Get all the car fuels
        /// </summary>
        /// <returns>A list of car fuels</returns>
        IList<CarFuel> GetAllCarFuels();
    }
}
