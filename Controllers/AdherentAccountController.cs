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
        private readonly ApplicationDbContext _context;

        public AdherentAccountController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost] //Cet attribut indique que cette action répond uniquement aux requêtes POST
        public ActionResult Login(string email, string password)
        {
            // Vérifiez si l'utilisateur avec l'e-mail fourni existe dans la base de données
            var user = _context.Adherents.SingleOrDefault(a => a.Email == email);

            if (user != null && user.MotDePasse == password)
            {
               // Si les informations d'identification sont correctes, renvoyez la vue avec les détails de l'utilisateur et du DossierInscription.
                return View("AdherentAccount");
            }
            else
            {
                // Si les informations d'identification sont incorrectes, redirigez vers la page de connexion
                return View("Moncompte");
            }
        }


        [HttpPost]
        public ActionResult LoginAdmin(string email, string password) {
            var user = _context.Adherents.SingleOrDefault(a => a.Email == email);
            if (user != null && user.MotDePasse == password)
            {
                if (user.Role == "Admin")
                {
                    return View("AdminAccount"); 
                }

            }
           
            return View("LoginAdmin"); 
            
        }

    }
}