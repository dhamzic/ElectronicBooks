using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ElectronicBooks
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "User", action = "LogInView", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "BookBorrow",
                url: "{Book}/{Borrow}/{bookId}",
                defaults: new { controller = "Book", action = "Borrow", bookId = "" }
            );

            routes.MapRoute(
                name: "BookReturn",
                url: "{Book}/{Return}/{bookId}",
                defaults: new { controller = "Book", action = "Return", bookId = "" }
            );
        }
    }
}
