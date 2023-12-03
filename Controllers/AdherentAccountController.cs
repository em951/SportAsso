using SportAssovv.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportAssovv.Controllers
{
    public class AdherentAccountController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public AdherentAccountController()
        {
         
        }

        //Login du membre, redirection a la view AdherentAccount 
        [HttpPost] //Cet attribut indique que cette action répond uniquement aux requêtes POST
        public ActionResult Login(string email, string password)
        {
            // Vérifiez si l'utilisateur avec l'e-mail fourni existe dans la base de données
            var user = db.Adherents.SingleOrDefault(a => a.Email == email);

            if (user != null && user.MotDePasse == password)
            {
                // Si les informations d'identification sont correctes, renvoyez la vue avec les détails de l'utilisateur et du DossierInscription.
                return View("~/Views/AdherentAccount/AdherentAccount.cshtml");
            }
            else
            {
                // Si les informations d'identification sont incorrectes, redirigez vers la page de connexion
                return View("~/Views/Home/Moncompte.cshtml");
            }
        }


        //LoginAdmin, redirection pour la view AdminAccount

        [HttpPost]
        public ActionResult LoginAdmin(string email, string password) {
            var user = db.Adherents.SingleOrDefault(a => a.Email == email);
            var role = user.Role.ToLower();
            if (user != null && user.MotDePasse == password)
            {
                if (role == "admin") 
                {
                    return View("~/Views/AdherentAccount/AdminAccount.cshtml"); 
                }

            }
           
            return View("LoginAdmin"); 
            
        }

        public ActionResult AdminAccount()
        {
            return View("~/Views/AdherentAccount/AdminAccount.cshtml"); 
        }

    }
}