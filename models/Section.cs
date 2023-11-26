using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SportAssovv.Models
{
    public class Section
    {
        [Key]
        public int SectionId { get; set; }

        [Required]
        [MaxLength(64)]
        public string NomSection { get; set; }
        
        // Propriétés de navigation
        public ICollection<Creneaux> Creneaux { get; set; }
        public ICollection<DisciplineSection> Disciplines { get; set; }
    }
}