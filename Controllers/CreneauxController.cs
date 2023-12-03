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
    public class CreneauxController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Creneaux
        public ActionResult Index()
        {
            var creneaux = db.Creneaux.Include(c => c.Section);
            return View(creneaux.ToList());
        }

        // GET: Creneaux/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Creneaux creneaux = db.Creneaux.Find(id);
            if (creneaux == null)
            {
                return HttpNotFound();
            }
            return View(creneaux);
        }

        // GET: Creneaux/Create
        public ActionResult Create()
        {
            ViewBag.SectionId = new SelectList(db.Sections, "SectionId", "NomSection");
            return View();
        }

        // POST: Creneaux/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CreneauHoraireId,JourHeureDebut,HeureFin,SectionId")] Creneaux creneaux)
        {
            if (ModelState.IsValid)
            {
                db.Creneaux.Add(creneaux);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SectionId = new SelectList(db.Sections, "SectionId", "NomSection", creneaux.SectionId);
            return View(creneaux);
        }

        // GET: Creneaux/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Creneaux creneaux = db.Creneaux.Find(id);
            if (creneaux == null)
            {
                return HttpNotFound();
            }
            ViewBag.SectionId = new SelectList(db.Sections, "SectionId", "NomSection", creneaux.SectionId);
            return View(creneaux);
        }

        // POST: Creneaux/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CreneauHoraireId,JourHeureDebut,HeureFin,SectionId")] Creneaux creneaux)
        {
            if (ModelState.IsValid)
            {
                db.Entry(creneaux).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SectionId = new SelectList(db.Sections, "SectionId", "NomSection", creneaux.SectionId);
            return View(creneaux);
        }

        // GET: Creneaux/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Creneaux creneaux = db.Creneaux.Find(id);
            if (creneaux == null)
            {
                return HttpNotFound();
            }
            return View(creneaux);
        }

        // POST: Creneaux/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Creneaux creneaux = db.Creneaux.Find(id);
            db.Creneaux.Remove(creneaux);
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
