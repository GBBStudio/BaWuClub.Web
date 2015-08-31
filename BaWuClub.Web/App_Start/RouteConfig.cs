using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BaWuClub.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
                name: "Member-s",
                url: "member/u-{uid}/{action}/{id}",
                defaults: new {Controller="Member", id = UrlParameter.Optional, uid = UrlParameter.Optional },
                namespaces: new[] { "BaWuClub.Web.Controllers" }
            );

        routes.MapRoute(
                name: "Member",
                url: "member/{action}/{id}",
                defaults: new { Controller = "Member", id = UrlParameter.Optional},
                namespaces: new[] { "BaWuClub.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "BaWuClub.Web.Controllers" }
            );


        }
    }
}