using System;
using System.Text;
using System.Collections.Generic;


namespace bea.dal.map {
    
    public class Annonce {
        public Annonce() { }
        public virtual int annonceId { get; set; }
        public virtual Posteur posteur { get; set; }
        public virtual string titre { get; set; }
        public virtual string corps { get; set; }
    }
}
