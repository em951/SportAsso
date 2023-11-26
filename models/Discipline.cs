using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SportAssovv.Models
{
    public class Discipline
    {
        [Key]
        public int DisciplineId { get; set; }

        [Required]
        [MaxLength(64)]
        public string NomDiscipline { get; set; }

        // Navigation properties
        public ICollection<DisciplineAdherent> Adherents { get; set; }
        public ICollection<DisciplineSection> Sections { get; set; }
    }

}