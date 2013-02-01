using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain;

namespace Bea.Core.Services
{
    public interface IAdImageServices
    {
        AdImage StoreImage(Guid id, Boolean isPrimary);
        Boolean ValidateImageForUpload(String contentType, int imageLength);
    }
}
