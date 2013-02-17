using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bea.Models.Delete
{
    public class DeleteAdModel
    {
        public long AdId { get; set; }
        public String Password { get; set; }
        public int? SelectedDeletionReasonId { get; set; }
        public int NbTry { get; set; }

        public Boolean CanDeleteAd { get; set; }
        public Boolean IsDeleted { get; set; }

        public String InfoMessage { get; set; }
    }
}
