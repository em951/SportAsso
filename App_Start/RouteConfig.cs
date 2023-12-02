﻿using System;
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
           name: "Adherent",
           url: "Adherent/{action}/{id}",
           defaults: new { controller = "Adherent", action = "Index", id = UrlParameter.Optional }
       );
            routes.MapRoute(
           name: "AdherentRegister",
           url: "Adherent/Register",
           defaults: new { controller = "Adherent", action = "Register" }
       );
        }
    }
}
