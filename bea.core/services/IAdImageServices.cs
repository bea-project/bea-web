using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain;

namespace Bea.Core.Services
{
    public interface IAdImageServices
    {
        Guid StoreImage(String fileName, Byte[] imageBytes);
        byte[] GetAdImage(String id, bool isThumbnail);
    }
}
