using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SimSo
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "mang",
                url: "mang/{name}",
                defaults: new { controller = "Home", action = "Network", name = UrlParameter.Optional });
            routes.MapRoute(
                name: "simtype",
                url: "loai-sim/{title}",
                defaults: new { controller = "Home", action = "SimType", title = UrlParameter.Optional });
            routes.MapRoute(
               name: "menu",
               url: "tin-tuc/{title}/{id}/{titleNew}",
               defaults: new { controller = "Home", action = "Menu", title = UrlParameter.Optional, id = UrlParameter.Optional, titleNew = UrlParameter.Optional });
            routes.MapRoute(
               name: "order",
               url: "dat-mua-sim/{number}",
               defaults: new { controller = "Order", action = "Create", number = UrlParameter.Optional }
           );
            routes.MapRoute(
                name: "tags",
                url: "dau-so/{tag}",
                defaults: new { controller = "Home", action = "Tags", tag = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
