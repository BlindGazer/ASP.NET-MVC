using System.Web.Optimization;

namespace FastBus.Web
{
    public class BundleConfig
    {
        public const string JqueryPath = "~/bundles/jquery";
        public const string ScriptsPath = "~/bundles/metro-ui";
        public const string StylesPath = "~/Content/styles";

        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle(StylesPath).Include(
                    "~/Content/metro.css",
                    "~/Content/metro-icons.css",
                    "~/Content/metro-schemes.css",
                    "~/Content/metro-custom.css",
                    "~/Content/select2.css",
                    "~/Content/site.css",
                    "~/Content/animation.css"
                    ));
            bundles.Add(new ScriptBundle(JqueryPath).Include("~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle(ScriptsPath).Include(
                "~/Scripts/jquery.validate.min.js",
                "~/Scripts/jquery.validate.unobtrusive.min.js",
                "~/Scripts/jquery.unobtrusive-ajax.min.js",
                "~/Scripts/metro.js",
                "~/Scripts/select2.js",
                "~/Scripts/i18n/ru.js",
                "~/Scripts/helper.js",
                "~/Scripts/general.js"
                ));
        }
    }
}