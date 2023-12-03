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
    public class DisciplineSectionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DisciplineSections
        public ActionResult Index()
        {
            var disciplineSections = db.DisciplineSections.Include(d => d.Discipline).Include(d => d.Section);
            return View(disciplineSections.ToList());
        }

        // GET: DisciplineSections/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DisciplineSection disciplineSection = db.DisciplineSections.Find(id);
            if (disciplineSection == null)
            {
                return HttpNotFound();
            }
            return View(disciplineSection);
        }

        // GET: DisciplineSections/Create
        public ActionResult Create()
        {
            ViewBag.DisciplineId = new SelectList(db.Disciplines, "DisciplineId", "NomDiscipline");
            ViewBag.SectionId = new SelectList(db.Sections, "SectionId", "NomSection");
            return View();
        }

        // POST: DisciplineSections/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DisciplineId,SectionId,DisciplineSectionId")] DisciplineSection disciplineSection)
        {
            if (ModelState.IsValid)
            {
                db.DisciplineSections.Add(disciplineSection);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DisciplineId = new SelectList(db.Disciplines, "DisciplineId", "NomDiscipline", disciplineSection.DisciplineId);
            ViewBag.SectionId = new SelectList(db.Sections, "SectionId", "NomSection", disciplineSection.SectionId);
            return View(disciplineSection);
        }

        // GET: DisciplineSections/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DisciplineSection disciplineSection = db.DisciplineSections.Find(id);
            if (disciplineSection == null)
            {
                return HttpNotFound();
            }
            ViewBag.DisciplineId = new SelectList(db.Disciplines, "DisciplineId", "NomDiscipline", disciplineSection.DisciplineId);
            ViewBag.SectionId = new SelectList(db.Sections, "SectionId", "NomSection", disciplineSection.SectionId);
            return View(disciplineSection);
        }

        // POST: DisciplineSections/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DisciplineId,SectionId,DisciplineSectionId")] DisciplineSection disciplineSection)
        {
            if (ModelState.IsValid)
            {
                db.Entry(disciplineSection).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DisciplineId = new SelectList(db.Disciplines, "DisciplineId", "NomDiscipline", disciplineSection.DisciplineId);
            ViewBag.SectionId = new SelectList(db.Sections, "SectionId", "NomSection", disciplineSection.SectionId);
            return View(disciplineSection);
        }

        // GET: DisciplineSections/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DisciplineSection disciplineSection = db.DisciplineSections.Find(id);
            if (disciplineSection == null)
            {
                return HttpNotFound();
            }
            return View(disciplineSection);
        }

        // POST: DisciplineSections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DisciplineSection disciplineSection = db.DisciplineSections.Find(id);
            db.DisciplineSections.Remove(disciplineSection);
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
