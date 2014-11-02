using System.Web.Optimization;

namespace ProductManager.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.UseCdn = true;
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
                      "~/Scripts/respond.js"));


            var angularBundle = new ScriptBundle("~/bundles/angular");
            angularBundle.CdnPath = "https://ajax.googleapis.com/ajax/libs/angularjs/1.3.0-rc.2/angular.min.js";
            bundles.Add(angularBundle);


            var angularRoute = new ScriptBundle("~/bundles/angularRoute");
            angularRoute.CdnPath = "//cdnjs.cloudflare.com/ajax/libs/angular.js/1.2.20/angular-route.min.js";
            bundles.Add(angularRoute);

            var angularResource = new ScriptBundle("~/bundles/angularResource");
            angularResource.CdnPath = "//cdnjs.cloudflare.com/ajax/libs/angular.js/1.2.20/angular-resource.min.js";
            bundles.Add(angularResource);

            var bootstrapUi = new ScriptBundle("~/bundles/bootstrapUi");
            bootstrapUi.CdnPath = "//angular-ui.github.io/bootstrap/ui-bootstrap-tpls-0.10.0.js";
            bundles.Add(bootstrapUi);

            var angularLocalLib = new ScriptBundle("~/bundles/angularLib");
            angularLocalLib.Include("~/Scripts/Angular/Lib/Services/CallService.js");
            bundles.Add(angularLocalLib);

            var angularTable = new ScriptBundle("~/bundles/angularTable");
            angularTable.CdnPath = "http://bazalt-cms.com/assets/ng-table/0.3.1/ng-table.js";
            bundles.Add(angularTable);


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/Web/SubCategoryDetails.css",
                      "~/Content/site.css"));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}
