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
        public int DisciplineSectionId { get; set; }

        public int DisciplineId { get; set; }
        public Discipline Discipline { get; set; }

        public int SectionId { get; set; }
        public Section Section { get; set; }
    }

}