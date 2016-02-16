using System.Web.Mvc;
using System.Web.Routing;

namespace SoftwareContable
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "SearchReports",
                url: "{controller}/{action}/{initialFolio}/{finalFolio}",
                defaults: new { controller = "Search", action = "Report" },
                constraints: new { initialFolio = @"\d+", finalFolio = @"\d+" }
            );

            routes.MapRoute(
                name: "UserAuthorization",
                url: "{controller}/{action}/{userId}",
                defaults: new { controller = "User", action = "Authorize" },
                constraints: new { userId = @"\d+" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional }
            );
        }
    }
}
