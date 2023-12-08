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
        public ActionResult Create([Bind(Include = "AdherentId,Certificat_medical,Assurance,Dossier_complet,Certificat_medical_data,Certificat_medical_contentType,Assurance_data,Assurance_contentType")] DossierInscription dossierInscription)
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
        // POST: DossierInscriptions/CreateDossierMembre
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
                return View("~/Views/AdherentAccount/AdherentAccount.cshtml");
            }

            ViewBag.AdherentId = new SelectList(db.Adherents, "AdherentId", "Nom", dossierInscription.AdherentId);
            return View(dossierInscription);
        }

        //action pour faire le changement do dossier par le membre

        //GET: DossierInscriptions/EditDossierMembre
        public ActionResult EditDossierMembre()
        {
            var adherentDetails = Session["AdherentDetails"] as SportAssovv.Models.Adherent;

            if (adherentDetails == null)
            {
                // Gérer la situation où les détails adhérents ne sont pas présents dans la session
                return RedirectToAction("Error");
            }

            // Retrouver le DossierInscription lié à l'adhérent connecté
            DossierInscription dossierInscription = db.DossierInscription.FirstOrDefault(d => d.AdherentId == adherentDetails.AdherentId);

            if (dossierInscription == null)
            {
                // S'il n'y a pas de DossierInscription pour cet adhérent, rediriger vers l'action de création
                return RedirectToAction("CreateDossierMembre");
            }

            return View(dossierInscription);

        }

        //POST: DossierInscriptions/EditDossierMembre
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditDossierMembre(DossierInscription dossierInscription, HttpPostedFileBase certificatFile, HttpPostedFileBase assuranceFile)
        {
            if (ModelState.IsValid)
            {
                // Vérifiez si les fichiers ont été fournis et enregistrez-les
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

                db.Entry(dossierInscription).State = EntityState.Modified;
                db.SaveChanges();
                TempData["SuccessMessage"] = "Les informations ont été mises à jour avec succès !";
                return RedirectToAction("EditDossierMembre");

            }
            return RedirectToAction("AdherentAccount", "Adherents"); // Redirection vers la page du compte adhérent

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
        public ActionResult Edit([Bind(Include = "AdherentId,Certificat_medical,Assurance,Dossier_complet,Certificat_medical_data,Certificat_medical_contentType,Assurance_data,Assurance_contentType")] DossierInscription dossierInscription)
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

        //faire le download du fichier
        public ActionResult DownloadFile()
        {
            var adherentDetails = Session["AdherentDetails"] as SportAssovv.Models.Adherent;

            if (adherentDetails == null)
            {
                // Lidar com a situação em que os detalhes do Adherent não estão presentes na sessão
                return RedirectToAction("Error");
            }

            // Encontre o DossierInscription relacionado ao Adherent logado
            var dossier = db.DossierInscription.FirstOrDefault(d => d.AdherentId == adherentDetails.AdherentId);

            if (dossier == null || dossier.Assurance_data == null)
            {
                // Se o DossierInscription não for encontrado ou não houver dados de Assurance
                return HttpNotFound();
            }

            // Mapeie tipos de conteúdo específicos com suas extensões correspondentes
            var contentTypeToExtension = new Dictionary<string, string>
                {
                    { "application/pdf", "pdf" },
                    { "image/jpeg", "jpg" },
                    {"image/png", "png"}
                 };

            var contentType = dossier.Assurance_contentType;
            var extension = "txt"; // Defina uma extensão padrão caso o tipo de conteúdo não seja mapeado

            if (contentTypeToExtension.ContainsKey(contentType))
            {
                extension = contentTypeToExtension[contentType];
            }

            // Retorne o arquivo como um FileResult
            return File(dossier.Assurance_data, contentType, "Assurance." + extension);
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
