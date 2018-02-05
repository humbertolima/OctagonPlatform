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
                      "~/Octagon/assets/css/bootstrap.min.css",
                      "~/Octagon/assets/css/sb-admin.css",
                      "~/Octagon/assets/css/sb-admin.min.css",
                      "~/Content/datatables/css/datatables.min.css",
                      "~/Octagon/assets/vendor/metisMenu/metisMenu.min.css",
                      "~/Octagon/assets/vendor/dist/css/sb-admin-2.css",
                      "~/Octagon/assets/vendor/morrisjs/morris.css",
                      "~/Octagon/assets/fonts/font-awesome.min.css",
                      "~/Octagon/assets/css/styles.css",
                      "~/Content/themes/base/jquery-ui.css"

                      //    "~/Content/datatables/css/datatables.bootstrap.css",
                      ));
            bundles.Add(new StyleBundle("~/Content/home/css").Include(
                     "~/Octagon/assets/css/bootstrap.min.css",
                     "~/Octagon/assets/css/styles.css",
                     "~/Octagon/assets/fonts/font-awesome.min.css",
                     "~/Octagon/assets/fonts/simple-line-icons.min.css",
                     "~/Octagon/assets/css/bootstrap.min.css"
                    
                     ));
           
  

            //bundles.Add(new StyleBundle("~/Content/cssjqueryui").Include(
            //         "~/Content/themes/base/jquery-ui.css"));

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

            bundles.Add(new ScriptBundle("~/bundles/selectmulti").Include(
              "~/Content/selectmulti/js/jquery.select-multiple.js"

              ));
            bundles.Add(new StyleBundle("~/bundles/selectmulticss").Include(
                     "~/Content/selectmulti/css/select-multiple.css"));


            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(             
             "~/Scripts/jquery-3.1.1.min.js",
             "~/Scripts/datatables/datatables.min.js",
             "~/Octagon/assets/bootstrap/dist/js/bootstrap.min.js",
             "~/Octagon/assets/vendor/metisMenu/metisMenu.min.js",
             "~/Octagon/assets/vendor/raphael/raphael.min.js",
             "~/Octagon/assets/vendor/morrisjs/morris.min.js",
             "~/Octagon/assets/data/AMS-Dashboard-data.js",
             "~/Octagon/assets/data/AMS-data.js",
             "~/Octagon/assets/data/demmoney-data.js",
             "~/Octagon/assets/data/CashEverywhere-data.js",
             "~/Octagon/assets/data/vendbot-data.js",
             "~/Octagon/assets/vendor/dist/js/sb-admin-2.js",
             "~/Scripts/jquery-ui-1.12.1.js",
             "~/Content/quicksearch/dist/jquery.quicksearch.min.js",
             "~/Scripts/jquery.unobtrusive-ajax.min.js"
             ));

            BundleTable.EnableOptimizations = true;

        }
    }
}
