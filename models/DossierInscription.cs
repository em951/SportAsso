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
        public int DossierId { get; set; }

        [Required]
        [MaxLength(64)]
        public string StatutInscription { get; set; }

        [Required]
        public int AdherentId { get; set; }

        // Clé étrangère
        [ForeignKey("AdherentId")]
        public Adherent Adherent { get; set; }


        public ICollection<Paiement> Paiements { get; set; }

        public bool Certificat_medical { get; set; }
        public bool Assurance { get; set; }
        public bool Dossier_complet { get; set; }
    }
}