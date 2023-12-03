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
    public class DetailsPaiementsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DetailsPaiements
        public ActionResult Index()
        {
            var detailsPaiements = db.DetailsPaiements.Include(d => d.Paiement);
            return View(detailsPaiements.ToList());
        }

        // GET: DetailsPaiements/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetailsPaiement detailsPaiement = db.DetailsPaiements.Find(id);
            if (detailsPaiement == null)
            {
                return HttpNotFound();
            }
            return View(detailsPaiement);
        }

        // GET: DetailsPaiements/Create
        public ActionResult Create()
        {
            ViewBag.PaiementId = new SelectList(db.Paiements, "PaiementId", "StatutPaiement");
            return View();
        }

        // POST: DetailsPaiements/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PaiementId,NumeroCarte,Valeur,NomTitulaire,DateValidite")] DetailsPaiement detailsPaiement)
        {
            if (ModelState.IsValid)
            {
                db.DetailsPaiements.Add(detailsPaiement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PaiementId = new SelectList(db.Paiements, "PaiementId", "StatutPaiement", detailsPaiement.PaiementId);
            return View(detailsPaiement);
        }

        // GET: DetailsPaiements/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetailsPaiement detailsPaiement = db.DetailsPaiements.Find(id);
            if (detailsPaiement == null)
            {
                return HttpNotFound();
            }
            ViewBag.PaiementId = new SelectList(db.Paiements, "PaiementId", "StatutPaiement", detailsPaiement.PaiementId);
            return View(detailsPaiement);
        }

        // POST: DetailsPaiements/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PaiementId,NumeroCarte,Valeur,NomTitulaire,DateValidite")] DetailsPaiement detailsPaiement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detailsPaiement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PaiementId = new SelectList(db.Paiements, "PaiementId", "StatutPaiement", detailsPaiement.PaiementId);
            return View(detailsPaiement);
        }

        // GET: DetailsPaiements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetailsPaiement detailsPaiement = db.DetailsPaiements.Find(id);
            if (detailsPaiement == null)
            {
                return HttpNotFound();
            }
            return View(detailsPaiement);
        }

        // POST: DetailsPaiements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DetailsPaiement detailsPaiement = db.DetailsPaiements.Find(id);
            db.DetailsPaiements.Remove(detailsPaiement);
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
