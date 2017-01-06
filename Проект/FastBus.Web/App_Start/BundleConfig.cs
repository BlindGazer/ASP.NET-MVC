using System.Web.Optimization;

namespace FastBus.Web
{ 
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/main-css").Include(
                "~/Content/metro.css",
                "~/Content/metro-icons.css",
                "~/Content/metro-schemes.css",
                "~/Content/select2.css",
                "~/Content/site.css"
                ));

            bundles.Add(new ScriptBundle("~/bundles/general-scripts").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery.validate.min.js",
                "~/Scripts/jquery.validate.unobtrusive.min.js",
                "~/Scripts/metro.js",
                "~/Scripts/select2.js",
                "~/Scripts/general.js",
                "~/Scripts/helper.js"
                ));
        }
    }
}