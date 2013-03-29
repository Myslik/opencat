namespace OpenCat.Transforms
{
    using Microsoft.Ajax.Utilities;
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web.Optimization;

    public class TemplateTransform : IBundleTransform
    {
        private const string templateFolder = "~/Client/templates/";
        private const string templateFormat = "Ember.TEMPLATES[\"{0}\"] = Ember.Handlebars.compile(\"{1}\");\n";
        private Regex extension = new Regex(@"\.hbs\.html$");

        public TemplateTransform()
        {
        }

        public void Process(BundleContext context, BundleResponse response)
        {
            var builder = new StringBuilder();
            var rootPath = new Uri(context.HttpContext.Server.MapPath(templateFolder));
            foreach (var info in response.Files)
            {
                using (var reader = info.OpenText())
                {
                    var path = rootPath.MakeRelativeUri(new Uri(info.FullName)).ToString();
                    var name = Path.ChangeExtension(path, null).Replace(Path.DirectorySeparatorChar, '/');
                    var template = reader.ReadToEnd().Replace("\r\n", "").Replace("\n", "").Replace("\"", "\\\"");
                    builder.AppendLine(String.Format(templateFormat, name, template));
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