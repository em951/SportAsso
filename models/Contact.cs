using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations; 

namespace SportAssovv.Models
{
    public class Contact
    {
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Mensagem { get; set; }
    }
}