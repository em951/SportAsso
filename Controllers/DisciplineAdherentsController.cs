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
    public class DisciplineAdherentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DisciplineAdherents
        public ActionResult Index()
        {
            var disciplineAdherents = db.DisciplineAdherents.Include(d => d.Adherent).Include(d => d.Discipline);
            return View(disciplineAdherents.ToList());
        }

        // GET: DisciplineAdherents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DisciplineAdherent disciplineAdherent = db.DisciplineAdherents.Find(id);
            if (disciplineAdherent == null)
            {
                return HttpNotFound();
            }
            return View(disciplineAdherent);
        }

        // GET: DisciplineAdherents/Create
        public ActionResult Create()
        {
            ViewBag.AdherentId = new SelectList(db.Adherents, "AdherentId", "Nom");
            ViewBag.DisciplineId = new SelectList(db.Disciplines, "DisciplineId", "NomDiscipline");
            return View();
        }

        // POST: DisciplineAdherents/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DisciplineId,AdherentId")] DisciplineAdherent disciplineAdherent)
        {
            if (ModelState.IsValid)
            {
                db.DisciplineAdherents.Add(disciplineAdherent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AdherentId = new SelectList(db.Adherents, "AdherentId", "Nom", disciplineAdherent.AdherentId);
            ViewBag.DisciplineId = new SelectList(db.Disciplines, "DisciplineId", "NomDiscipline", disciplineAdherent.DisciplineId);
            return View(disciplineAdherent);
        }

        //GET: DisciplineAdherents/CreateMembre
        public ActionResult CreateMembre()
        {
            var adherentDetails = Session["AdherentDetails"] as SportAssovv.Models.Adherent;

            // Liste avec l'adhrent actuel de la section
            var adherentList = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Value = adherentDetails.AdherentId.ToString(), // AdherentId c'est l'identifiant
                    Text = adherentDetails.Nom // champs du dropdown
                }
            };

            ViewBag.AdherentId = new SelectList(adherentList, "Value", "Text");

            if (adherentDetails == null)
            {
                //Gérer la situation où les détails des adhérents ne sont pas présents dans la session
                return RedirectToAction("Error");
            }
            ViewBag.DisciplineId = new SelectList(db.Disciplines, "DisciplineId", "NomDiscipline");

            return View();

        }

        //POST: DisciplineAdherents/CreateMembre
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateMembre([Bind(Include = "DisciplineId,AdherentId")] DisciplineAdherent disciplineAdherent)
        {
            
            if (ModelState.IsValid)
            {
                db.DisciplineAdherents.Add(disciplineAdherent);
                db.SaveChanges();
                return View("~/Views/AdherentAccount/AdherentAccount.cshtml");
            }
            ViewBag.AdherentId = new SelectList(db.Adherents, "AdherentId", "Nom", disciplineAdherent.AdherentId);
            ViewBag.DisciplineId = new SelectList(db.Disciplines, "DisciplineId", "NomDiscipline", disciplineAdherent.DisciplineId);
            return View(disciplineAdherent);

        }

        // GET: DisciplineAdherents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DisciplineAdherent disciplineAdherent = db.DisciplineAdherents.Find(id);
            if (disciplineAdherent == null)
            {
                return HttpNotFound();
            }
            ViewBag.AdherentId = new SelectList(db.Adherents, "AdherentId", "Nom", disciplineAdherent.AdherentId);
            ViewBag.DisciplineId = new SelectList(db.Disciplines, "DisciplineId", "NomDiscipline", disciplineAdherent.DisciplineId);
            return View(disciplineAdherent);
        }

        // POST: DisciplineAdherents/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DisciplineId,AdherentId")] DisciplineAdherent disciplineAdherent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(disciplineAdherent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AdherentId = new SelectList(db.Adherents, "AdherentId", "Nom", disciplineAdherent.AdherentId);
            ViewBag.DisciplineId = new SelectList(db.Disciplines, "DisciplineId", "NomDiscipline", disciplineAdherent.DisciplineId);
            return View(disciplineAdherent);
        }

        // GET: DisciplineAdherents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DisciplineAdherent disciplineAdherent = db.DisciplineAdherents.Find(id);
            if (disciplineAdherent == null)
            {
                return HttpNotFound();
            }
            return View(disciplineAdherent);
        }

        // POST: DisciplineAdherents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DisciplineAdherent disciplineAdherent = db.DisciplineAdherents.Find(id);
            db.DisciplineAdherents.Remove(disciplineAdherent);
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
