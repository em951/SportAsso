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

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true, NullDisplayText = "NULL")]
        public Nullable<System.DateTime> DatePaiement { get; set; }
        public string NumeroCarte { get; set; }
        public string NomTitulaire { get; set; }
        public string DateExpiration { get; set; }
        public string Cvv { get; set; }

    }
}

