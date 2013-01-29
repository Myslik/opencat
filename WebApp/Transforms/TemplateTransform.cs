namespace OpenCat.Transforms
{
    using Microsoft.Ajax.Utilities;
    using System;
    using System.IO;
    using System.Linq;
    using System.Web.Optimization;

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