using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static System.Collections.Specialized.BitVector32;

namespace SportAssovv.Models
{
    public class DisciplineSection
    {
        [Key]
        public int DisciplineId { get; set; }

        [Key]
        public int SectionId { get; set; }

        // Clés étrangères
        [ForeignKey("DisciplineId")]
        public Discipline Discipline { get; set; }

        [ForeignKey("SectionId")]
        public Section Section { get; set; }

        public ICollection<Creneaux> Creneaux { get; set; }
    }

}