using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bea.Models.Contact
{
    public class ContactAdModel
    {
        public long AdId { get; set; }
        public String InfoMessage { get; set; }
        public String EmailTo { get; set; } //To be filled later in the process
        public String FirstName { get; set; }
        public String AdTitle { get; set; }
        public String Name { get; set; }
        public String Email { get; set; }
        public String Telephone { get; set; }
        public String EmailBody { get; set; }
        public Boolean CopySender { get; set; }
        public Boolean CanContactAd { get; set; }
        public ContactAdModel() { }

        public ContactAdModel(long AdId)
        {
            this.AdId = AdId;
        }
    }
}
