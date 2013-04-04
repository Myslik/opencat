namespace OpenCat
{
    using System.Web.Optimization;

    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/ember").Include(
                "~/Scripts/json2.js",
                "~/Scripts/jquery-1.9.1.js",
                "~/Scripts/underscore.js",
                "~/Scripts/bootstrap.js",
                "~/Scripts/handlebars.js",
                "~/Scripts/moment.js",
                "~/Scripts/ember.js", 
                "~/Scripts/ember-data.js", 
                "~/Scripts/fileuploader.js", 
                "~/Scripts/select2.js"));
            bundles.Add(new ScriptBundle("~/bundles/application").IncludeDirectory("~/Client/", "*.js", true));
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Scripts/modernizr-*"));
            bundles.Add(new StyleBundle("~/Content/css").IncludeDirectory("~/Content/", "*.css", false));
        }
    }
}