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
        public ActionResult Create([Bind(Include = "AdherentId,DossierId,Certificat_medical,Assurance,Dossier_complet,Certificat_medical_data,Certificat_medical_contentType,Assurance_data,Assurance_contentType")] DossierInscription dossierInscription)
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


        // GET: DossierInscriptions/CreateDossierMembre
        public ActionResult CreateDossierMembre()
        {
            /*ViewBag.AdherentId = new SelectList(db.Adherents, "AdherentId", "Nom");
            return View();*/

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
    
            return View();
        }
        //action pour faire l'upload de files par le membre
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDossierMembre(DossierInscription dossierInscription, HttpPostedFileBase certificatFile, HttpPostedFileBase assuranceFile)
        {
            
            if (ModelState.IsValid)
            {
                // Verifique se os arquivos foram fornecidos e salve-os
                if (certificatFile != null && certificatFile.ContentLength > 0)
                {
                    dossierInscription.Certificat_medical_data = new byte[certificatFile.ContentLength];
                    certificatFile.InputStream.Read(dossierInscription.Certificat_medical_data, 0, certificatFile.ContentLength);
                    dossierInscription.Certificat_medical_contentType = certificatFile.ContentType;
                }

                if (assuranceFile != null && assuranceFile.ContentLength > 0)
                {
                    dossierInscription.Assurance_data = new byte[assuranceFile.ContentLength];
                    assuranceFile.InputStream.Read(dossierInscription.Assurance_data, 0, assuranceFile.ContentLength);
                    dossierInscription.Assurance_contentType = assuranceFile.ContentType;
                }

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
        public ActionResult Edit([Bind(Include = "AdherentId,DossierId,Certificat_medical,Assurance,Dossier_complet,Certificat_medical_data,Certificat_medical_contentType,Assurance_data,Assurance_contentType")] DossierInscription dossierInscription)
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
