using System.Web.Mvc;
using System.Web.Routing;

namespace HalFiyatlari.Api
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            #region Properties
            string[] nSpace = new[] { "HalFiyatlari.Api.Controllers" };
            #endregion

            #region Other Process
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.IgnoreRoute("robots.txt");
            #endregion

            #region Home Routes
            routes.MapRoute(
                name: "AboutUs",
                url: "hakkinda",
                defaults: new { controller = "Home", action = "AboutUs" },
                namespaces: nSpace
            );

            routes.MapRoute(
                name: "PrivacyPolicy",
                url: "gizlilik-sozlesmesi",
                defaults: new { controller = "Home", action = "PrivacyPolicy" },
                namespaces: nSpace
                );

            routes.MapRoute(
                name: "TermsConditions",
                url: "sartlar-ve-kosullar",
                defaults: new { controller = "Home", action = "TermsConditions" },
                namespaces: nSpace
                );

            routes.MapRoute(
                name: "Search",
                url: "arama",
                defaults: new { controller = "Home", action = "Search" },
                namespaces: nSpace
                );

            routes.MapRoute(
                name: "Contact",
                url: "iletisim",
                defaults: new { controller = "Home", action = "Contact" },
                namespaces: nSpace
            );
            #endregion

            #region Default Route
            routes.MapRoute(
                name: "Home",
                url: "",
                defaults: new { controller = "Home", action = "Index" },
                namespaces: nSpace
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: nSpace
            );
            #endregion
        }
    }
}
