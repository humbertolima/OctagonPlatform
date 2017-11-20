using System.Web.Optimization;

namespace OctagonPlatform
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            //Allow me to include ajax in my views
            bundles.Add(new ScriptBundle("~/bundles/ajax").Include(
                "~/Scripts/jquery.unobtrusive-ajax.min"));

            /*bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
                "~/Scripts/DataTables/jquery.dataTables.js",
                "~/Scripts/DataTables/jquery.bootstrap.js")); */

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap-superhero.css",
                  //    "~/Content/datatables/css/datatables.bootstrap.css",
                      "~/Content/site.css"));
            bundles.Add(new StyleBundle("~/Content/cssjqueryui").Include(                   
                     "~/Content/themes/base/jquery-ui.css"));

            bundles.Add(new StyleBundle("~/Content/base").Include(
                      "~/Content/themes/base/*.css"));

            bundles.Add(new ScriptBundle("~/bundles/xpopulate").Include(
                        "~/Scripts/xpopulate.js"));

            bundles.Add(new ScriptBundle("~/bundles/search").Include(
                "~/Scripts/Search.js"));

            bundles.Add(new ScriptBundle("~/bundles/user").Include(
                "~/Scripts/User.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                "~/Scripts/jquery-ui-1.12.1.js"
                
                ));
        }
    }
}
