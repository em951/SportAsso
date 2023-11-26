using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SportAssovv.Models
{
    public class Adherent
    {
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

        [Required]
        public DateTime DateNaissance { get; set; }

        [Required]
        [MaxLength(64)]
        public string MotDePasse { get; set; }

        // Propriétés de navigation
        public ICollection<DisciplineAdherent> Disciplines { get; set; }
        public ICollection<DisciplineAdherent> DisciplineAdherents { get; set; }
    }
}