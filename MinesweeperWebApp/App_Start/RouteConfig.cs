using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MinesweeperWebApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

           

            routes.MapRoute(
                name: "login",
                url: "{controller}/{action}",
                defaults: new { controller = "Account", action = "ShowLoginForm", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "register",
                url: "{controller}/{action}",
                defaults: new { controller = "Account", action = "ShowRegistrationForm", id = UrlParameter.Optional }
            );

             routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",

                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            ); 
        }
    }
}
