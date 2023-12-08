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

        
        [ForeignKey("Adherent")]
        public int AdherentId { get; set; }
        public Adherent Adherent { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PaiementId { get; set; }
        public int MontantPaye { get; set; }
        public DateTime DatePaiement { get; set; }
        public string NumeroCarte { get; set; }
        public decimal Valeur { get; set; }
        public string NomTitulaire { get; set; }
        public string DateValidite { get; set; }


    }
}

