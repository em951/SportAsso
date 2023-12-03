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
            ViewBag.AdherentId = new SelectList(db.DossiersInscription, "AdherentId", "StatutInscription");
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

            ViewBag.AdherentId = new SelectList(db.DossiersInscription, "AdherentId", "StatutInscription", adherent.AdherentId);
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
            ViewBag.AdherentId = new SelectList(db.DossiersInscription, "AdherentId", "StatutInscription", adherent.AdherentId);
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
            ViewBag.AdherentId = new SelectList(db.DossiersInscription, "AdherentId", "StatutInscription", adherent.AdherentId);
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

        public ActionResult ResetMotPasse()
        {
            return View();
        }

        //Action pour refaire le mot de passe

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetMotPasse(string email, string nouvelleMotPasse)
        {
            
            try
            {
                // Verifier si l'email existe dans la base de données 
                var adherent = db.Adherents.SingleOrDefault(a => a.Email == email);

                if (adherent == null)
                {
                    TempData["ErrorMessage"] = "Email non trouvé.";
                    return View();
                }

                // Nova Senha
                adherent.MotDePasse = nouvelleMotPasse;
                db.SaveChanges();
                TempData["SuccessMessage"] = "Le mot de passe a été changé avec succès!";

                // Redirecionar para a página inicial
                return RedirectToAction("~/Views/Home/Index.cshtml");
            }
            catch (DbEntityValidationException ex)
            {
                // Capturar exceção de validação e exibir detalhes no console ou logs
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Console.WriteLine($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                    }
                }

                // Você pode adicionar tratamento adicional ou redirecionar para uma página de erro
                TempData["ErrorMessage"] = "Erro ao salvar no banco de dados. Verifique os logs para obter detalhes.";
                return View();
            }
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
                // adherent.Role = "membre"; 
                db.Adherents.Add(adherent);
                db.SaveChanges();
                return View("~/Views/Home/Moncompte.cshtml");
            }

            ViewBag.AdherentId = new SelectList(db.DossiersInscription, "AdherentId", "StatutInscription", adherent.AdherentId);
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
