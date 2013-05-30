using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Bea.Models.Request
{
    public class SpamAdRequestModel
    {
        public long AdId { get; set; }
        public Boolean CanSignal { get; set; }
        public String InfoMessage { get; set; }
        public int? SelectedSpamAdTypeId { get; set; }
        public String RequestorEmail { get; set; }
        public String Description { get; set; }
    }
}
