namespace Twitter.WebApplication
{
    using System.Web.Optimization;

    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
                        "~/Scripts/kendo/kendo.all.min.js",
                        "~/Scripts/kendo/kendo.aspnetmvc.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

//            bundles.Add(new StyleBundle("~/bundles/css").Include(
//                      "~/Content/bootstrap.css",
//                      "~/Content/site.css"));
//
//            bundles.Add(new StyleBundle("~/Content/kedno")
//                .Include("~/Content/kendo/kendo.common.core.min.css")
//                .Include("~/Content/kendo/kendo.common.min.css")
//                .Include("~/Content/kendo/kendo.material.min.css"));
//
//            bundles.IgnoreList.Clear();
//            BundleTable.EnableOptimizations = true;
        }
    }
}
