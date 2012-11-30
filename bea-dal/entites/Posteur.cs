using System;
using System.Text;
using System.Collections.Generic;


namespace bea_dal.map {
    
    public class Posteur {
        public Posteur() {
			annonces = new List<Annonce>();
        }
        public string posteurEmail { get; set; }
        public string password { get; set; }
        public IList<Annonce> annonces { get; set; }
    }
}
