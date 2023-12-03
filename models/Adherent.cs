using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;


namespace SportAssovv.Models
{
    public class Adherent
    {
        public Adherent() {
              
        }

        [Key]
        public int AdherentId { get; set; }

        [Required]
        [MaxLength(64)]
        public string Nom { get; set; }

        [Required]
        [MaxLength(64)]
        public string Prenom { get; set; }

        [Required]
        public string Adresse { get; set; }

        [Required]
        [MaxLength(64)]
        public string Email { get; set; }

        [Required]
        [MaxLength(20)]
        public string Telephone { get; set; }


        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true, NullDisplayText = "NULL")]
        public Nullable<System.DateTime> DateNaissance { get; set; }

        [Required]
        [MaxLength(64)]
        public string MotDePasse { get; set; }

        [MaxLength(64)]
        public string Role { get; set; }

        //1..1 adherent dossier
        public virtual DossierInscription DossierInscription { get; set; }

        // Propriétés de navigation
        public ICollection<DisciplineAdherent> Disciplines { get; set; }
        public ICollection<DisciplineAdherent> DisciplineAdherents { get; set; }
    }
}