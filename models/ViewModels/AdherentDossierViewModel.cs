using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations; 

namespace SportAssovv.Models.ViewModels
{
    public class AdherentDossierViewModel
    {
        public Adherent Adherent { get; set; }
        public DossierInscription DossierInscription { get; set; }
    }
}