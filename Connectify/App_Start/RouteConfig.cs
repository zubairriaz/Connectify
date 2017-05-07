using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Connectify
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute("Profile", "Profile/{action}/{id}", new { controller = "Profile", id= UrlParameter.Optional });
            routes.MapRoute("LogIn", "LogIn", new { controller = "Account", action = "LogIn" });
            routes.MapRoute("LogOut", "LogOut", new { controller = "Account", action = "LogOut" });
            routes.MapRoute("Username", "{username}", new { controller = "Account", action = "UserName" });
            routes.MapRoute("CreateAccount", "Account/CreateAccount", new { controller = "Account", action = "CreateAccount" });
            routes.MapRoute("Default", "", new { controller = "Account", action = "Index" });
            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}
