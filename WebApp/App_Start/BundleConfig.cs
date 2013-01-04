using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace OpenCat
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new Bundle("~/bundles/templates", new TemplateTransform("~/Client/Templates/"))
                .IncludeDirectory("~/Client/Templates/", "*.html", true));

            bundles.Add(new ScriptBundle("~/bundles/ember").Include("~/Scripts/ember.js", "~/Scripts/ember-data.js"));

            bundles.Add(new ScriptBundle("~/bundles/application").Include(
                        "~/Client/Utils.js",
                        "~/Client/App.js",
                        "~/Client/Router.js",
                        "~/Client/Store.js",
                        "~/Client/Models/Document.js",
                        "~/Client/Controllers/DocumentsController.js",
                        "~/Client/Views/Document/List.js",
                        "~/Client/Views/Document/Edit.js",
                        "~/Client/Views/Document/New.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/site.css"));
        }
    }

    public class TemplateTransform : IBundleTransform
    {
        private string virtualRootPath;

        public TemplateTransform(string virtualRootPath)
        {
            this.virtualRootPath = virtualRootPath;
        }

        public void Process(BundleContext context, BundleResponse response)
        {
            var builder = new Ember.Handlebars.TemplateBuilder();
            var rootPath = new Uri(context.HttpContext.Server.MapPath(virtualRootPath));
            foreach (var info in response.Files)
            {
                using (var reader = info.OpenText())
                {
                    var name = rootPath.MakeRelativeUri(new Uri(info.FullName)).ToString();
                    builder.Register(Path.ChangeExtension(name, null).Replace(Path.DirectorySeparatorChar, '/'), reader.ReadToEnd());
                }
            }
            var minifier = new Minifier();
            response.ContentType = "text/javascript";
            var content = minifier.MinifyJavaScript(builder.ToString());
            if (minifier.ErrorList.Any())
            {
                response.Content = builder.ToString();
            }
            else
            {
                response.Content = content;
            }
        }
    }
}