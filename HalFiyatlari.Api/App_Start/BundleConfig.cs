using System.Web.Optimization;

namespace HalFiyatlari.Api
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/css").Include(
                      "~/Content/css/bootstrap.css",
                      "~/Content/css/fontawesome-all.css",
                      "~/Content/css/swiper.css",
                      "~/Content/css/magnific-popup.css"
                      ));

            bundles.Add(new StyleBundle("~/bundles/style").Include("~/Content/css/pages/_layout.css", "~/Content/css/styles.css"));

            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                "~/Content/js/jquery.min.js",
                "~/Content/js/popper.min.js",
                "~/Content/js/bootstrap.min.js",
                "~/Content/js/jquery.easing.min.js",
                "~/Content/js/swiper.min.js",
                "~/Content/js/jquery.magnific-popup.js",
                "~/Content/js/validator.min.js"
                ));

            bundles.Add(new StyleBundle("~/bundles/script").Include("~/Content/js/pages/_layout.js", "~/Content/js/scripts.js"));

            //bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/del/bootstrap.css", "~/Content/del/site.css"));

            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Content/del/jquery-{version}.js"));

            //// Use the development version of Modernizr to develop with and learn from. Then, when you're
            //// ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Content/del/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Content/del/bootstrap.js"));
        }
    }
}
