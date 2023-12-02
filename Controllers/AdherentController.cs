using SportAssovv.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportAssovv.Controllers
{
    public class AdherentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdherentController()
        {
            _context = new ApplicationDbContext();
        }

        public AdherentController(ApplicationDbContext context)
        {
            _context = context;
        }

        //Action pour inscription dans la liste des adhérents (formulaire d'inscription)
        public ActionResult Inscription()
        { //repeter l'action create? 
            return View();
        }

        //Action pour refaire le mot de passe

        public ActionResult ResetMotPasse()
        {
            return View(); 
        }

        //Action pour refaire le mot de passe

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetMotPasse(string email, string nouvelleMotPasse)
        {
            // Verifier si l'email existe dans la base de données 
            var adherent = _context.Adherents.SingleOrDefault(a => a.Email == email);

            if (adherent == null)
            {
                ModelState.AddModelError("", "Email non trouvé.");
                return View();
            }

            // Nouvelle Mot de Passe
            adherent.MotDePasse = nouvelleMotPasse;
            _context.SaveChanges();

            // Redirection pour la page de inscription
            return RedirectToAction("Inscription");
        }

        // Action pour afficher la liste des adhérents
        public ActionResult ListeAdherents()
        {
            var adherents = _context.Adherents.ToList();
            return View(adherents);
        }

        // GET: Adherent/Details/5
        public ActionResult Details(int id)
        {
            var adherent = _context.Adherents.Find(id);
            return View(adherent);
        }

        // GET: Adherent/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Adherent/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Adherent adherent)
        {
            if (ModelState.IsValid)
           {
                _context.Adherents.Add(adherent);
                _context.SaveChanges();
                // Après avoir enregistré avec succès l'Adhérent, créez automatiquement
                // une nouvelle entrée dans la table DossierInscription
                DossierInscription dossier = new DossierInscription
                {
                    AdherentId = adherent.AdherentId, // ID d'adhérent nouvellement créé
                    StatutInscription = "nouveau" // Status inscription
                };

                _context.DossiersInscription.Add(dossier);
                _context.SaveChanges();
                return RedirectToAction("ListeAdherents");
            }

           

            return View("Index");
        }

        // GET: Adherent/Edit/5
        public ActionResult Edit(int id)
        {
            var adherent = _context.Adherents.Find(id);
            return View(adherent);
        }

        // POST: Adherent/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Adherent adherent)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(adherent).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("ListeAdherents");
            }
            return View(adherent);
        }

        // GET: Adherent/Delete/5
        public ActionResult Delete(int id)
        {
            var adherent = _context.Adherents.Find(id);
            return View(adherent);
        }

        // POST: Adherent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var adherent = _context.Adherents.Find(id);
            _context.Adherents.Remove(adherent);
            _context.SaveChanges();
            return RedirectToAction("ListeAdherents");
        }
    }

}
