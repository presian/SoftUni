using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Twitter.WebApplication
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                
               name: "PublocUserPage",
               url: "{controller}/{action}/{username}",
               defaults: new { controller = "Users", action = "PublicPage"},
               namespaces: new[] { "Twitter.WebApplication.Controllers" }
           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Twitter.WebApplication.Controllers" }
            );

        }
    }
}
