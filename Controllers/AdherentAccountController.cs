using SportAssovv.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SportAssovv.Controllers
{
    public class AdherentAccountController : Controller
    {
        private readonly ApplicationDbContext db; 
        public AdherentAccountController()
        {
            db= new ApplicationDbContext();

        }

        private bool isMembreValide(string Email, string MotDePasse)
        {// Vérifiez si l'utilisateur avec l'e-mail fourni existe dans la base de données
            var user = db.Adherents.SingleOrDefault(a => a.Email == Email);
            if (user != null && user.MotDePasse == MotDePasse)
            {
                return true;
            }
            return false; 

        }

        //Login du membre, redirection a la view AdherentAccount 
        [HttpPost] //Cet attribut indique que cette action répond uniquement aux requêtes POST
        [ValidateAntiForgeryToken]
        public ActionResult Login(Adherent adhrent)
        {
            if (isMembreValide(adhrent.Email, adhrent.MotDePasse)){
                FormsAuthentication.SetAuthCookie(adhrent.Email, false);
                // Obtenez les détails d'Adherent et stockez-les en session
                var adherentDetails = db.Adherents.SingleOrDefault(a => a.Email == adhrent.Email);
                Session["AdherentDetails"] = adherentDetails;

                return View("~/Views/AdherentAccount/AdherentAccount.cshtml");

            }   
            else
            {
                ModelState.AddModelError("", "Les informations d'identification sont invalides");
                // Si les informations d'identification sont incorrectes, redirigez vers la page de connexion
                return View("~/Views/Home/Moncompte.cshtml");
            }
        }

        private bool isAdminValide(string Email, string MotDePasse) {
            // Vérifiez si l'utilisateur avec l'e-mail fourni existe dans la base de données
            var user = db.Adherents.SingleOrDefault(a => a.Email == Email);
            var role = user.Role.ToLower();
            if (user != null && user.MotDePasse == MotDePasse)
            {
                if (role == "admin")
                {
                    return true;
                }
            }
            return false;
        }

        //LoginAdmin, redirection pour la view AdminAccount

        [HttpPost]
        public ActionResult LoginAdmin(Adherent adhrent) {
           if(isAdminValide(adhrent.Email, adhrent.MotDePasse))
            {  //Obtenez les détails d'Adherent et stockez-les en session
                FormsAuthentication.SetAuthCookie(adhrent.Email, false);
                var adherentDetails = db.Adherents.SingleOrDefault(a => a.Email == adhrent.Email);
                Session["AdherentDetails"] = adherentDetails;

                return View("~/Views/AdherentAccount/AdminAccount.cshtml");
            }
            else {
                ModelState.AddModelError("", "Les informations d'identification sont invalides");
                return View("~/Views/Home/LoginAdmin.cshtml"); 
            }
             
            
        }

        public ActionResult AdminAccount()
        {
            return View("~/Views/AdherentAccount/AdminAccount.cshtml"); 
        }

    }
}