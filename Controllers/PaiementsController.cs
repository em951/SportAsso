using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SportAssovv.Models;

namespace SportAssovv.Controllers
{
    public class PaiementsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Paiements
        public ActionResult Index()
        {
            var paiements = db.Paiements.Include(p => p.Adherent).Include(p => p.DetailsPaiement).Include(p => p.DossierInscription);
            return View(paiements.ToList());
        }

        // GET: Paiements/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paiement paiement = db.Paiements.Find(id);
            if (paiement == null)
            {
                return HttpNotFound();
            }
            return View(paiement);
        }

        // GET: Paiements/Create
        public ActionResult Create()
        {
            ViewBag.AdherentId = new SelectList(db.Adherents, "AdherentId", "Nom");
            ViewBag.PaiementId = new SelectList(db.DetailsPaiements, "PaiementId", "NumeroCarte");
            ViewBag.DossierId = new SelectList(db.DossierInscription, "AdherentId", "StatutInscription");
            return View();
        }

        // POST: Paiements/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PaiementId,MontantPaye,DatePaiement,StatutPaiement,AdherentId,DossierId,PaiementDetailsId")] Paiement paiement)
        {
            if (ModelState.IsValid)
            {
                db.Paiements.Add(paiement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AdherentId = new SelectList(db.Adherents, "AdherentId", "Nom", paiement.AdherentId);
            ViewBag.PaiementId = new SelectList(db.DetailsPaiements, "PaiementId", "NumeroCarte", paiement.PaiementId);
            ViewBag.DossierId = new SelectList(db.DossierInscription, "AdherentId", "StatutInscription", paiement.DossierId);
            return View(paiement);
        }

        // GET: Paiements/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paiement paiement = db.Paiements.Find(id);
            if (paiement == null)
            {
                return HttpNotFound();
            }
            ViewBag.AdherentId = new SelectList(db.Adherents, "AdherentId", "Nom", paiement.AdherentId);
            ViewBag.PaiementId = new SelectList(db.DetailsPaiements, "PaiementId", "NumeroCarte", paiement.PaiementId);
            ViewBag.DossierId = new SelectList(db.DossierInscription, "AdherentId", "StatutInscription", paiement.DossierId);
            return View(paiement);
        }

        // POST: Paiements/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PaiementId,MontantPaye,DatePaiement,StatutPaiement,AdherentId,DossierId,PaiementDetailsId")] Paiement paiement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paiement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AdherentId = new SelectList(db.Adherents, "AdherentId", "Nom", paiement.AdherentId);
            ViewBag.PaiementId = new SelectList(db.DetailsPaiements, "PaiementId", "NumeroCarte", paiement.PaiementId);
            ViewBag.DossierId = new SelectList(db.DossierInscription, "AdherentId", "StatutInscription", paiement.DossierId);
            return View(paiement);
        }

        // GET: Paiements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paiement paiement = db.Paiements.Find(id);
            if (paiement == null)
            {
                return HttpNotFound();
            }
            return View(paiement);
        }

        // POST: Paiements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Paiement paiement = db.Paiements.Find(id);
            db.Paiements.Remove(paiement);
            db.SaveChanges();
            return RedirectToAction("Index");
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
