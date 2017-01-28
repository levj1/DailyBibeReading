using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DBReading
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute (
                "StatesList",
                "Home/States/List/{CountryCode}",
                new { controller = "Test", action = "StateList", CountryCode = "" }
                );

            routes.MapRoute (
                "CountriesList",
                "Home/Countries/List",
                new { controller = "Test", action = "CountryList" }
                );

            routes.MapRoute(
                "BookList",
                "Home/Books/List/{name}",
                new { controller = "GeneratePlan", action = "BookList", Name = "", id = "" }
                );

            routes.MapRoute(
                "GroupList",
                "Home/Groups/List",
                new { controller = "GeneratePlan", action = "GroupBookList" }
                );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
