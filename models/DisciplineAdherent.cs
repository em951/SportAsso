using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SportAssovv.Models
{
    public class DisciplineAdherent
    {
        [Key]
        public int DisciplineId { get; set; }

        [Key]
        public int AdherentId { get; set; }

        // Clés étrangères
        [ForeignKey("DisciplineId")]
        public Discipline Discipline { get; set; }

        [ForeignKey("AdherentId")]
        public Adherent Adherent { get; set; }
   
       
    
    }
}