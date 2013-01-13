using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain.Reference;

namespace Bea.Core.Services
{
    public interface IReferenceServices
    {
        IList<CarBrand> GetAllCarBrands();
        IList<CarFuel> GetAllCarFuels();
    }
}
