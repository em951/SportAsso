using SportAssovv.Models;
using SportAssovv.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportAssovv.Controllers
{
    public class AdherentAccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdherentAccountController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost] // Este atributo indica que esta ação responde apenas a solicitações POST
        public ActionResult Login(string email, string password)
        {
            // Verificar se o usuário com o email fornecido existe no banco de dados
            var user = _context.Adherents.SingleOrDefault(a => a.Email == email);

            if (user != null && user.MotDePasse == password)
            {
                var dossierInscription = _context.DossiersInscription
                                 .SingleOrDefault(d => d.AdherentId == user.AdherentId);

                var viewModel = new AdherentDossierViewModel
                {
                    Adherent = user,
                    DossierInscription = dossierInscription
                };

                // Se as credenciais estiverem corretas, retornar a visualização com os detalhes do usuário e do DossierInscription
                return View("AdherentAccount", viewModel);
            }
            else
            {
                // Se as credenciais estiverem incorretas, redirecionar de volta para a página de login
                return RedirectToAction("Index");
            }
        }

    }
}