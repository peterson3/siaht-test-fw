using System.Web;
using System.Web.Optimization;

namespace tester_webapp
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/app.js",
                      "~/Scripts/plugins/flot/excanvas.min.js",
                      "~/Scripts/plugins/flot/flot-data.js",
                      "~/Scripts/plugins/flot/jquery.flot.js",
                      "~/Scripts/plugins/flot/jquery.flot.pie.js",
                      "~/Scripts/plugins/flot/jquery.flot.resize.js",
                      "~/Scripts/plugins/flot/jquery.flot.tooltip.min.js",
                      "~/Scripts/plugins/morris/morris-data.js",
                      "~/Scripts/plugins/morris/morris.js",
                      "~/Scripts/plugins/morris/raphael.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/sb-admin.css",
                      "~/Content/plugins/morris.css"));
        }
    }
}
