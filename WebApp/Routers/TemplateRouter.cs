namespace OpenCat.Routers
{
    using Microsoft.Ajax.Utilities;
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Web;
    using System.Web.Routing;

    public class TemplateRouter : IRouteHandler
    {
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new TemplateRouteHandler();
        }
    }

    public class TemplateRouteHandler : IHttpHandler
    {
        private const string templateFormat = "Ember.TEMPLATES[\"{0}\"] = Ember.Handlebars.compile(\"{1}\");\n";

        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            var builder = new StringBuilder();

            var fullPath = context.Server.MapPath("~/Client/routes/");
            var rootPath = new Uri(fullPath);
            var files = Directory.EnumerateFiles(fullPath, "*.html", System.IO.SearchOption.AllDirectories);
            foreach (var file in files)
            {
                using (var reader = File.OpenText(file))
                {
                    var path = rootPath.MakeRelativeUri(new Uri(file)).ToString();
                    var name = Path.ChangeExtension(path, null).Replace(Path.DirectorySeparatorChar, '/');
                    var template = reader.ReadToEnd().Replace("\r\n", "").Replace("\n", "").Replace("\"", "\\\"");
                    builder.AppendLine(String.Format(templateFormat, name, template));
                }
            }

            var componentPath = context.Server.MapPath("~/Client/components/");
            var componentFiles = Directory.EnumerateFiles(componentPath, "*.html", System.IO.SearchOption.AllDirectories);
            foreach (var file in files)
            {
                using (var reader = File.OpenText(file))
                {
                    var path = Path.GetFileNameWithoutExtension(file);
                    var name = String.Format("components/{0}", path);
                    var template = reader.ReadToEnd().Replace("\r\n", "").Replace("\n", "").Replace("\"", "\\\"");
                    builder.AppendLine(String.Format(templateFormat, name, template));
                }
            }

            var minifier = new Minifier();
            context.Response.ContentType = "text/javascript";
            var content = minifier.MinifyJavaScript(builder.ToString());
            if (minifier.ErrorList.Any())
            {
                context.Response.Write(builder.ToString());
            }
            else
            {
                context.Response.Write(content);
            }
            context.Response.End();
        }
    }
}