using SportAssovv.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SportAssovv.Controllers
{
    public class AdherentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private ApplicationDbContext db = new ApplicationDbContext();

        public AdherentController()
        {
            _context = new ApplicationDbContext();
        }

        public AdherentController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            var adherents = db.Adherents.Include(a => a.DossierInscription);
            return View(adherents.ToList());
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

        // GET: Adherents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adherent adherent = db.Adherents.Find(id);
            if (adherent == null)
            {
                return HttpNotFound();
            }
            return View(adherent);
        }

        // GET: Adherent/Create
        public ActionResult Create()
        {
            ViewBag.AdherentId = new SelectList(db.DossiersInscription, "AdherentId", "StatutInscription");
            return View();
        }


        // POST: Adherent/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AdherentId,Nom,Prenom,Adresse,Email,Telephone,DateNaissance,MotDePasse,Role")] Adherent adherent)
        {
            if (ModelState.IsValid)
            {
                db.Adherents.Add(adherent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AdherentId = new SelectList(db.DossiersInscription, "AdherentId", "StatutInscription", adherent.AdherentId);
            return View(adherent);
        }

        // GET: Adherent/Register
        public ActionResult Register()
        {
            ViewBag.AdherentId = new SelectList(db.DossiersInscription, "AdherentId", "StatutInscription");
            return View();
        }

        // POST: Adherent/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "AdherentId,Nom,Prenom,Adresse,Email,Telephone,DateNaissance,MotDePasse,Role")] Adherent adherent)
        {
            if (ModelState.IsValid)
            {
                db.Adherents.Add(adherent);
                db.SaveChanges();
                return View("~/Views/Home/Moncompte.cshtml");
            }

            ViewBag.AdherentId = new SelectList(db.DossiersInscription, "AdherentId", "StatutInscription", adherent.AdherentId);
            return View(adherent);
        }



        // GET: Adherent/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adherent adherent = db.Adherents.Find(id);
            if (adherent == null)
            {
                return HttpNotFound();
            }
            ViewBag.AdherentId = new SelectList(db.DossiersInscription, "AdherentId", "StatutInscription", adherent.AdherentId);
            return View(adherent);
        }

        // POST: Adherent/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AdherentId,Nom,Prenom,Adresse,Email,Telephone,DateNaissance,MotDePasse,Role")] Adherent adherent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(adherent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AdherentId = new SelectList(db.DossiersInscription, "AdherentId", "StatutInscription", adherent.AdherentId);
            return View(adherent);
        }

        // GET: Adherent/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adherent adherent = db.Adherents.Find(id);
            if (adherent == null)
            {
                return HttpNotFound();
            }
            return View(adherent);
        }

        // POST: Adherent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Adherent adherent = db.Adherents.Find(id);
            db.Adherents.Remove(adherent);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



    }

}
