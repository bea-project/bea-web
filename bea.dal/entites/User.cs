using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using bea.dal.entities;

namespace bea.dal.entities
{
    public class User
    {
        public User()
        {
            this.ads = new List<Ad>();
            this.creationDate = DateTime.Now;
        }
        public virtual int userId { get; set; }
        public virtual string email { get; set; }
        public virtual string password { get; set; }
        public virtual IList<Ad> ads { get; set; }
        public virtual DateTime creationDate { get; set; }

        //Add the Ad to the user and set the Ad created by to this
        public virtual void AddAd(Ad adToBeAdded)
        {
            this.ads.Add(adToBeAdded);
            adToBeAdded.createdBy = this;
        }
    }
}
