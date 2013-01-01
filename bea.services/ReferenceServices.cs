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
        
        public IList<CarBrand> GetAllCarBrands()
        {
            return _referenceRepository.GetAllCarBrands();
        }

        public IList<CarFuel> GetAllCarFuels()
        {
            return _referenceRepository.GetAllCarFuels();
        }
    }
}
