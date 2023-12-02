using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportAssovv.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Qui Sommes Nous? ";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contactez-nous";

            return View();
        }

        public ActionResult LoginAdmin()
        {
            ViewBag.Message = "Espace Administrateur";
            return View();
        }

        public ActionResult Moncompte()
        {
            ViewBag.Message = "Mon compte/S'inscrire";
            return View();
        }

    }
}