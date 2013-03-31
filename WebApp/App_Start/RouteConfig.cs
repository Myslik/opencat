using OpenCat.Routers;
using System.Web.Mvc;
using System.Web.Routing;

namespace OpenCat
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.Add("TemplateRoute", new Route("bundles/templates", new TemplateRouter()));

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}