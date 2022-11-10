using System.Web;
using System.Web.Optimization;

namespace PMS
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-3.6.0.slim.min.js",
                        "~/Scripts/jquery-3.6.0.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate.min.js",
                        "~/Scripts/jquery.validate.unobtrusive.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-2.8.3.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/select2.min.js",
                      "~/Scripts/sb-admin-2.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/Site.css",
                      "~/Content/bootstrap.min.css",
                      "~/Content/select2.min.css",
                      "~/Content/select2-bootstrap4.min.css",
                      "~/Content/font-family-nunito.css",
                      "~/Content/sb-admin-2.min.css"));

            bundles.Add(new ScriptBundle("~/DataTable/css").Include(
                      "~/Content/dataTables.bootstrap4.min.css",
                      "~/Content/buttons.bootstrap4.min.css",
                      //"~/Content/dataTables.checkboxes.css",
                      "~/Content/fixedColumns.dataTables.min.css",
                      "~/Content/select.dataTables.min.css"));

            bundles.Add(new ScriptBundle("~/DataTable/checkboxCSS").Include(
                      "~/Content/dataTables.checkboxes.css"));

            bundles.Add(new ScriptBundle("~/DataTable/js").Include(
                      "~/Scripts/jquery.dataTables.min.js",
                      "~/Scripts/dataTables.bootstrap4.min.js",
                      "~/Scripts/dataTables.fixedColumns.min.js",
                      //"~/Scripts/dataTables.checkboxes.min.js",
                      "~/Scripts/dataTables.buttons.min.js",
                      "~/Scripts/buttons.bootstrap4.min.js",
                      "~/Scripts/buttons.print.min.js",
                      "~/Scripts/buttons.html5.min.js",
                      "~/Scripts/dataTables.select.min.js"));

            bundles.Add(new ScriptBundle("~/DataTable/checkboxJS").Include(
                      "~/Scripts/dataTables.checkboxes.min.js",
                      "~/Scripts/dataTables.select.min.js"));

            bundles.Add(new ScriptBundle("~/Popper/js").Include(
                      "~/Scripts/popper-4.0.min.js"));

            bundles.Add(new ScriptBundle("~/Notify/js").Include(
                      "~/Scripts/notify.min.js"));
        }
    }
}
