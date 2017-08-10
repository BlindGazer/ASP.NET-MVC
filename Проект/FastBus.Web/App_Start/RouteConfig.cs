using System.Web.Mvc;
using System.Web.Routing;

namespace FastBus.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            routes.MapRoute(
                name: "DriverRoute",
                url: "Driver/Profile/{username}",
                defaults: new { controller = "Driver", action = "Update", username = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "UserRoute",
                url: "User/Profile/{username}",
                defaults: new { controller = "User", action = "Update", username = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Schedule", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
