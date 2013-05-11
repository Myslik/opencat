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

        private string BuildName(string file)
        {
            string[] parts = Path.ChangeExtension(file, null).Split('/');
            if (parts.Length == 1) return parts[0];
            return String.Join("/", parts.NonConsecutive().Where(p => p != "default"));
        }

        private StringBuilder _builder = new StringBuilder();
        private HttpContext _context;
        private void Map(string path, Func<string, string> naming)
        {
            var absolute = new Uri(_context.Server.MapPath(path));
            var files = Directory.EnumerateFiles(absolute.AbsolutePath, "*.html", SearchOption.AllDirectories);
            var line = String.Join("\n", files.Select(f =>
            {
                var name = naming(absolute.MakeRelativeUri(new Uri(f)).ToString());
                var template = File.OpenText(f).ReadToEnd().Replace("\r\n", "").Replace("\n", "").Replace("\"", "\\\"");
                return String.Format(templateFormat, name, template);
            }));
            _builder.AppendLine(line);
        }

        public void ProcessRequest(HttpContext context)
        {
            _context = context;

            Map("~/Client/routes/", BuildName);
            Map("~/Client/components/", file => String.Format("components/{0}", Path.GetFileNameWithoutExtension(file)));

            var minifier = new Minifier();
            context.Response.ContentType = "text/javascript";
            var content = minifier.MinifyJavaScript(_builder.ToString());
            context.Response.Write(minifier.ErrorList.Any() ? _builder.ToString() : content);
            context.Response.End();
        }
    }
}