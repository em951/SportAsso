using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true, NullDisplayText = "NULL")]
        public Nullable<System.DateTime> Jour { get; set; }

        [Required]
        public TimeSpan HeureDebut { get; set; }

        [Required]
        public TimeSpan HeureFin { get; set; }

        [Required]
        public String Encadrant { get; set; }

        [Required]
        public String Lieu { get; set; }

        // Propriétés de navigation

        
        public int DisciplineId { get; set; }    
        public Discipline Discipline { get; set; }
    }
}