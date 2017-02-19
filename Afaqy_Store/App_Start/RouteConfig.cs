using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Afaqy_Store
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "DefaultGetByPage",
            //    url: "{controller}/GetByPage/{pageNumber}/{pageSize}",
            //    defaults: new { action = "GetByPage" },
            //    constraints: new
            //    {
            //        //httphttpMethod = new HttpMethodConstraint(System.Net.Http.HttpMethod.Get.Method),
            //        pageNumber = @"\d+",
            //        pageSize = @"\d+"
            //    }
            //);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional }
            );
        }
    }
}
