using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bea.Domain
{
    public class User
    {
        public User()
        {
            this.Ads = new List<Ad>();
        }

        public virtual int UserId { get; set; }
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }
        public virtual IList<Ad> Ads { get; set; }
        public virtual DateTime CreationDate { get; set; }

        //Add the Ad to the user and set the Ad created by to this
        public virtual void AddAd(Ad adToBeAdded)
        {
            this.Ads.Add(adToBeAdded);
            adToBeAdded.CreatedBy = this;
        }
    }
}
