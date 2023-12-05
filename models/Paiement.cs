using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SportAssovv.Models
{
    public class Paiement
    {
        public Paiement()
        {
            DatePaiement = DateTime.Now; // Définit DatePaiement sur la date et l'heure locales actuelles
        }

        [Key]
        public int PaiementId { get; set; }

        public int MontantPaye { get; set; }

        public DateTime DatePaiement { get; set; }

        [MaxLength(64)]
        public string StatutPaiement { get; set; }

        public int AdherentId { get; set; }

        [ForeignKey("AdherentId")]
        public Adherent Adherent { get; set; }

        [ForeignKey("DossierInscription")]
        public int DossierId { get; set; }

        public virtual DossierInscription DossierInscription { get; set; }

        public virtual DetailsPaiement DetailsPaiement { get; set; }


    }
}

