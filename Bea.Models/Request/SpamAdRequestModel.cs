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

        [Required(ErrorMessage = "Quel est le motif de votre signalement ?")]
        public int? SelectedSpamAdTypeId { get; set; }

        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Nous avons besoin de votre adresse email !")]
        //[Required(ErrorMessage = "Nous avons besoin de votre adresse email !")]
        public String RequestorEmail { get; set; }
        public String Description { get; set; }
    }
}
