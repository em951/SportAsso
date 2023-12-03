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
        [Key]
        public int PaiementId { get; set; }

        [Required]
        public int MontantPaye { get; set; }

        [Required]
        public DateTime DatePaiement { get; set; }

        [Required]
        [MaxLength(64)]
        public string StatutPaiement { get; set; }

        public int AdherentId { get; set; }

        [ForeignKey("AdherentId")]
        public Adherent Adherent { get; set; }

        public int DossierId { get; set; }

        [ForeignKey("DossierId")]
        public DossierInscription DossierInscription { get; set; }
    }

}