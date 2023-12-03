using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SportAssovv
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

           routes.MapRoute(
           name: "Adherents",
           url: "Adherents/{action}/{id}",
           defaults: new { controller = "Adherents", action = "Index", id = UrlParameter.Optional }
       );
            routes.MapRoute(
           name: "AdherentRegister",
           url: "Adherents/Register",
           defaults: new { controller = "Adherents", action = "Register" }
       );


            routes.MapRoute(
                name:"Admin Account",
                url: "AdherentAccount/AdminAccount",
                defaults: new {controlle= "AdherentAccount", action = "AdminAccount" }
                );
        }
    }
}
