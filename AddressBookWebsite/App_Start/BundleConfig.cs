using System.Web;
using System.Web.Optimization;

namespace AddressBookWebsite
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.

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

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/kendo/css").Include(
            "~/Content/kendo/kendo.common-bootstrap.min.css",
            "~/Content/kendo/kendo.bootstrap.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
            "~/Scripts/kendo/kendo.all.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
            "~/Scripts/kendo/kendo.core.min.js"));

            // "~/Scripts/kendo/kendo.timezones.min.js", // uncomment if using the Scheduler

            bundles.Add(new ScriptBundle("~/bundles/jqueryform").Include(
                          "~/Scripts/jquery.form.js"));

                bundles.Add(new ScriptBundle("~/bundles/avatar").Include(
                          "~/Scripts/site.avatar.js"));

                bundles.Add(new ScriptBundle("~/bundles/jcrop").Include(
                          "~/Scripts/jquery.Jcrop.js"));

                bundles.Add(new StyleBundle("~/Content/jcrop").Include(
                          "~/Content/jquery.Jcrop.css"));

                bundles.Add(new StyleBundle("~/Content/avatar").Include(
                          "~/Content/site.avatar.css"));

                bundles.IgnoreList.Clear();
            }
        }
    }
