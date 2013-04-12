using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Bea.Models
{
    public class ContactUserFormModel
    {
        //public int UserId { get; set; }
        public long AdId { get; set; }
        public String EmailTo { get; set; } //To be filled later in the process
        public String FirstName { get; set; }
        public String AdTitle { get; set; }
        [DisplayName("Votre nom:")]
        public String Name { get; set; }
        [DisplayName("Votre email:")]
        public String Email { get; set; }
        [DisplayName("Votre téléphone:")]
        public String Telephone { get; set; }
        [DisplayName("Texte:")]
        public String EmailBody { get; set; }
        [DisplayName("Recevoir une copie de ce message:")]
        public Boolean CopySender { get; set; }

        public ContactUserFormModel() { }

        public ContactUserFormModel(long AdId)
        {
            this.AdId = AdId;
        }
    }
}
