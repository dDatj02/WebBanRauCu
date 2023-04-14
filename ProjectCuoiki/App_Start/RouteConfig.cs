using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ProjectCuoiki
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("News", "{type}/{meta}",
             new { controller = "News", action = "Index", meta = UrlParameter.Optional },
             new RouteValueDictionary
             { 
                    {"type","tin-tuc"}
             },
              namespaces: new[] { "ProjectCuoiki.Controllers" }
             );
            routes.MapRoute("Product", "{type}/{meta}",
                new { controller = "Product", action = "Index",  meta = UrlParameter.Optional },
                   new RouteValueDictionary
             {
                    {"type","san-pham"}
             },
                 new[] { "ProjectCuoiKi.Controllers" }
               );
            routes.MapRoute("Cart", "{cart}",
              new { controller = "Cart", action = "Index" },
                 new RouteValueDictionary
           {
                    {"cart","gio-hang"}
           },
               new[] { "ProjectCuoiKi.Controllers" }
             );
            routes.MapRoute("Contact", "{Contact}",
              new { controller = "Contact", action = "Index" },
                 new RouteValueDictionary
           {
                    {"Contact","lien-he"}
           },
               new[] { "ProjectCuoiKi.Controllers" }
             );
            routes.MapRoute("SingleProduct", "{sp}/{type}/{meta}",
              new { controller = "SingleProduct", action = "Index", type=UrlParameter.Optional,meta = UrlParameter.Optional },
                 new RouteValueDictionary
           {
                    {"sp","san-pham"}
           },
               new[] { "ProjectCuoiKi.Controllers" }
             );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Default", action = "Index", id = UrlParameter.Optional }
             

            );
         
        }
    }
}
