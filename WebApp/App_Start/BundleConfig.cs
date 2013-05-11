namespace OpenCat
{
    using System.Web.Optimization;

    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/ember").Include(
                "~/Scripts/json2.js",
                "~/Scripts/jquery-2.0.0.js",
                "~/Scripts/bootstrap.js",
                "~/Scripts/handlebars.js",
                "~/Scripts/moment.js",
                "~/Scripts/ember-latest.js", 
                "~/Scripts/ember-data-latest.js", 
                "~/Scripts/fileuploader.js", 
                "~/Scripts/select2.js"));
            bundles.Add(new ScriptBundle("~/bundles/application").IncludeDirectory("~/Client/", "*.js", true));
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Scripts/modernizr-*"));
            bundles.Add(new StyleBundle("~/Content/css").IncludeDirectory("~/Content/", "*.css", false));
        }
    }
}