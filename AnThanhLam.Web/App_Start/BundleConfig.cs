using AnThanhLam.Common;
using System.Web;
using System.Web.Optimization;

namespace AnThanhLam.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //  // bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //           //    "~/Scripts/jquery-{version}.js"));

            ////   bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //               "~/Scripts/jquery.validate*"));

            //   // Use the development version of Modernizr to develop with and learn from. Then, when you're
            //   // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            //   bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //               "~/Scripts/modernizr-*"));

            //   bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //             "~/Scripts/bootstrap.js"));

            //   bundles.Add(new StyleBundle("~/Content/css").Include(
            //             "~/Content/bootstrap.css",
            //             "~/Content/site.css"));
            bundles.Add(new ScriptBundle("~/js/jquery").Include("~/Assets/client/js/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/js/plugins").Include(
                 "~/Assets/admin/libs/jquery-ui/jquery-ui.min.js",
                 "~/Assets/admin/libs/mustache/mustache.js",
                 "~/Assets/admin/libs/numeral/numeral.js",
                 "~/Assets/admin/libs/jquery-validation/dist/jquery.validate.js",
                 "~/Assets/client/js/common.js"
                ));

            bundles.Add(new StyleBundle("~/css/base")
                .Include("~/Assets/client/css/bootstrap.css", new CssRewriteUrlTransform())
                .Include("~/Assets/client/font-awesome-4.6.3/css/font-awesome.css", new CssRewriteUrlTransform())
                .Include("~/Assets/admin/libs/jquery-ui/themes/smoothness/jquery-ui.min.css", new CssRewriteUrlTransform())
                .Include("~/Assets/client/css/style.css", new CssRewriteUrlTransform())
                .Include("~/Assets/client/css/custom.css", new CssRewriteUrlTransform())
                );
            BundleTable.EnableOptimizations = bool.Parse(ConfigHelper.GetByKey("EnableBundles"));

        }
    }
}
