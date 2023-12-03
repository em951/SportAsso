using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SportAssovv.Models;

namespace SportAssovv.Controllers
{
    public class DossierInscriptionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DossierInscriptions
        public ActionResult Index()
        {
            var dossiersInscription = db.DossierInscription.Include(d => d.Adherent);
            return View(dossiersInscription.ToList());
        }

        // GET: DossierInscriptions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DossierInscription dossierInscription = db.DossierInscription.Find(id);
            if (dossierInscription == null)
            {
                return HttpNotFound();
            }
            return View(dossierInscription);
        }

        //action pour faire l'upload de files par le membre
        public ActionResult FileMembre(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
                try
                {
                    string path = Path.Combine(Server.MapPath("~/Content/files"),
                    Path.GetFileName(file.FileName));
                    file.SaveAs(path);
                    ViewBag.Message = "File uploaded successfully";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            else
            {
                ViewBag.Message = "You have not specified a file.";
            }
            return View();//especificar a view
        }

        //action pour faire l'upload de files par le admin
        public ActionResult FileAdmin(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
                try
                {
                    string path = Path.Combine(Server.MapPath("~/Content/files"),
                    Path.GetFileName(file.FileName));
                    file.SaveAs(path);
                    ViewBag.Message = "File uploaded successfully";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            else
            {
                ViewBag.Message = "You have not specified a file.";
            }
            return View();//especificar a view
        }


        // GET: DossierInscriptions/Create
        public ActionResult Create()
        {
            ViewBag.AdherentId = new SelectList(db.Adherents, "AdherentId", "Nom");
            return View();
        }

        // POST: DossierInscriptions/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AdherentId,DossierId,StatutInscription,Certificat_medical,Assurance,Dossier_complet,Certificat_medical_data,Certificat_medical_contentType,Assurance_data,Assurance_contentType")] DossierInscription dossierInscription)
        {
            if (ModelState.IsValid)
            {
                db.DossierInscription.Add(dossierInscription);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AdherentId = new SelectList(db.Adherents, "AdherentId", "Nom", dossierInscription.AdherentId);
            return View(dossierInscription);
        }

        // GET: DossierInscriptions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DossierInscription dossierInscription = db.DossierInscription.Find(id);
            if (dossierInscription == null)
            {
                return HttpNotFound();
            }
            ViewBag.AdherentId = new SelectList(db.Adherents, "AdherentId", "Nom", dossierInscription.AdherentId);
            return View(dossierInscription);
        }

        // POST: DossierInscriptions/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AdherentId,DossierId,StatutInscription,Certificat_medical,Assurance,Dossier_complet,Certificat_medical_data,Certificat_medical_contentType,Assurance_data,Assurance_contentType")] DossierInscription dossierInscription)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dossierInscription).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AdherentId = new SelectList(db.Adherents, "AdherentId", "Nom", dossierInscription.AdherentId);
            return View(dossierInscription);
        }

        // GET: DossierInscriptions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DossierInscription dossierInscription = db.DossierInscription.Find(id);
            if (dossierInscription == null)
            {
                return HttpNotFound();
            }
            return View(dossierInscription);
        }

        // POST: DossierInscriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DossierInscription dossierInscription = db.DossierInscription.Find(id);
            db.DossierInscription.Remove(dossierInscription);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: DossierInscriptions/CreateMembre
        public ActionResult CreateMembre()
        {
            ViewBag.AdherentId = new SelectList(db.Adherents, "AdherentId", "Nom");
            return View();
        }

        // POST: DossierInscriptions/CreateMembre
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateMembre([Bind(Include = "AdherentId,DossierId,StatutInscription,Certificat_medical,Assurance,Dossier_complet,Certificat_medical_data,Certificat_medical_contentType,Assurance_data,Assurance_contentType")] DossierInscription dossierInscription)
        {
            if (ModelState.IsValid)
            {
                db.DossierInscription.Add(dossierInscription);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AdherentId = new SelectList(db.Adherents, "AdherentId", "Nom", dossierInscription.AdherentId);
            return View(dossierInscription);
        }

        // GET: DossierInscriptions/EditMembre/5
        public ActionResult EditMembre(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DossierInscription dossierInscription = db.DossierInscription.Find(id);
            if (dossierInscription == null)
            {
                return HttpNotFound();
            }
            ViewBag.AdherentId = new SelectList(db.Adherents, "AdherentId", "Nom", dossierInscription.AdherentId);
            return View(dossierInscription);
        }

        // POST: DossierInscriptions/EditMembre/5
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditMembre([Bind(Include = "AdherentId,DossierId,StatutInscription,Certificat_medical,Assurance,Dossier_complet,Certificat_medical_data,Certificat_medical_contentType,Assurance_data,Assurance_contentType")] DossierInscription dossierInscription)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dossierInscription).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AdherentId = new SelectList(db.Adherents, "AdherentId", "Nom", dossierInscription.AdherentId);
            return View(dossierInscription);
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
