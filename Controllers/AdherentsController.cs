using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SportAssovv.Models;

namespace SportAssovv.Controllers
{
    public class AdherentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Adherents
        public ActionResult Index()
        {
            var adherents = db.Adherents.Include(a => a.DossierInscription);
            return View(adherents.ToList());
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

        // GET: Adherents/Create
        public ActionResult Create()
        {
            ViewBag.AdherentId = new SelectList(db.DossierInscription, "AdherentId", "StatutInscription");
            return View();
        }

        // POST: Adherents/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
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

            ViewBag.AdherentId = new SelectList(db.DossierInscription, "AdherentId", "StatutInscription", adherent.AdherentId);
            return View(adherent);
        }

        // GET: Adherents/Edit/5
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
            ViewBag.AdherentId = new SelectList(db.DossierInscription, "AdherentId", "StatutInscription", adherent.AdherentId);
            return View(adherent);
        }

        // POST: Adherents/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
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
            ViewBag.AdherentId = new SelectList(db.DossierInscription, "AdherentId", "StatutInscription", adherent.AdherentId);
            return View(adherent);
        }

        // GET: Adherents/Delete/5
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

        // POST: Adherents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Adherent adherent = db.Adherents.Find(id);
            db.Adherents.Remove(adherent);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        //Action pour refaire le mot de passe
        // GET: Adherents/ResetMotPasse
        public ActionResult ResetMotPasse()
        {
            return View();
        }

        //Action pour refaire le mot de passe
        // Post: Adherents/ResetMotPasse
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetMotPasse(string email, string nouvelleMotPasse)
        {
            var adherent = db.Adherents.FirstOrDefault(a => a.Email == email);

            if (adherent == null)
            {
                ViewBag.Message = "Email inexistante";
                return View();
            }

            adherent.MotDePasse = nouvelleMotPasse;
            db.SaveChanges();

            ViewBag.Message = "Le mot de passe a été changé avec succès";
            return View();
        }

    

        // GET: Adherent/Register
        public ActionResult Register()
        {
            ViewBag.AdherentId = new SelectList(db.DossierInscription, "AdherentId", "StatutInscription");
            return View();
        }

        // POST: Adherent/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "AdherentId,Nom,Prenom,Adresse,Email,Telephone,DateNaissance,MotDePasse,Role")] Adherent adherent)
        {
            if (ModelState.IsValid)
            {
                // adherent.Role = "membre"; 
                db.Adherents.Add(adherent);
                db.SaveChanges();
                return View("~/Views/Home/Moncompte.cshtml");
            }

            ViewBag.AdherentId = new SelectList(db.DossierInscription, "AdherentId", "StatutInscription", adherent.AdherentId);
            return View(adherent);
        }

        // GET: Adherents/EditMembre/5
        public ActionResult EditMembre(int? id)
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
            ViewBag.AdherentId = new SelectList(db.DossierInscription, "AdherentId", "StatutInscription", adherent.AdherentId);
            return View(adherent);
        }

        // POST: Adherents/EditMembre/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditMembre([Bind(Include = "AdherentId,Nom,Prenom,Adresse,Email,Telephone,DateNaissance,MotDePasse,Role")] Adherent adherent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(adherent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AdherentId = new SelectList(db.DossierInscription, "AdherentId", "StatutInscription", adherent.AdherentId);
            return View(adherent);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
