using System;
using System.Text;
using System.Collections.Generic;


namespace bea_dal.map {
    
    public class Annonce {
        public Annonce() { }
        public int annonceId { get; set; }
        public Posteur posteur { get; set; }
        public string titre { get; set; }
        public string corps { get; set; }
        public string posteurFk { get; set; }
    }
}
