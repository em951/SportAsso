using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static System.Collections.Specialized.BitVector32;

namespace SportAssovv.Models
{
    public class Creneaux
    {
        [Key]
        public int CreneauHoraireId { get; set; }

        [Required]
        public DateTime JourHeureDebut { get; set; }

        [Required]
        public TimeSpan HeureFin { get; set; }

        [Required]
        public int SectionId { get; set; }

        // Clé étrangère
        [ForeignKey("SectionId")]
        public Section Section { get; set; }

        public ICollection<Section> Sections { get; set; }

    }

}