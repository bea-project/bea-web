using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Bea.Domain
{
    public class AdImage
    {
        public virtual Guid Id { get; set; }
        public virtual bool IsPrimary { get; set; }
        public virtual String FileName { get; set; }
        public virtual byte[] ImageBytes { get; set; }
        public virtual byte[] ImageThumbnailBytes { get; set; }
        public virtual Ad Ad { get; set; }
    }
}
