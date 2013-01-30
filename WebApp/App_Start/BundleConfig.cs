namespace OpenCat
{
    using OpenCat.Transforms;
    using System.Web.Optimization;

    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new Bundle("~/bundles/templates", new TemplateTransform("~/Client/Templates/"))
                .IncludeDirectory("~/Client/Templates/", "*.html", true));
            bundles.Add(new ScriptBundle("~/bundles/ember").Include("~/Scripts/ember.js", "~/Scripts/ember-data.js", "~/Scripts/fileuploader.js"));
            bundles.Add(new ScriptBundle("~/bundles/application").IncludeDirectory("~/Client/", "*.js", true));
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Scripts/modernizr-*"));
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));
        }
    }
}