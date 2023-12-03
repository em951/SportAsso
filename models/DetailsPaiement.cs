using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SportAssovv.Models
{
    public class DetailsPaiement
    {
        public string NumeroCarte { get; set; }
        public decimal Valeur { get; set; }
        public string NomTitulaire { get; set; }
        public string DateValidite { get; set; }

        [Key, ForeignKey("Paiement")]
        public int PaiementId { get; set; }
        public virtual Paiement Paiement { get; set; }
    }
}