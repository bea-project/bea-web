using System;
using System.Text;
using System.Collections.Generic;


namespace bea_dal.map {
    
    public class Posteur {
        public Posteur()
        {
            annonces = new List<Annonce>();
        }
        public virtual string posteurEmail { get; set; }
        public virtual string password { get; set; }
        public virtual IList<Annonce> annonces { get; set; }
    }
}
