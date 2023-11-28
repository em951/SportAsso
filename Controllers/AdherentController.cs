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
                return RedirectToAction("ListeAdherents");
            }
            return View(adherent);
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
