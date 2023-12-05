using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SportAssovv.Models
{
    public class DossierInscription
    {
     
        [Key]
        [ForeignKey("Adherent")]
        public int AdherentId { get; set; }

        public Adherent Adherent { get; set; }

        public bool Certificat_medical { get; set; }
        public bool Assurance { get; set; }
        public bool Dossier_complet { get; set; }

        public byte[] Certificat_medical_data { get; set; }
        public string Certificat_medical_contentType { get; set; }
        public byte[] Assurance_data { get; set; }
        public string Assurance_contentType { get; set; }
    }
}