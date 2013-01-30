using System;
using System.IO;
using System.Web.Mvc;

namespace OpenCat.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(string qqfile)
        {
            if (Request.IsAjaxRequest())
            {
                try
                {
                    var fname = Server.UrlDecode(Request.Headers["x-file-name"]);
                    var path = "C:\\temp\\" + fname;
                    using (var writer = System.IO.File.OpenWrite(path))
                    {
                        CopyStream(Request.InputStream, writer);
                    }
                    return Json(new { success = true });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, error = ex.Message });
                }
            }
            else
            {
                try
                {
                    var files = Request.Files;
                    foreach (var key in files.AllKeys)
                    {
                        var file = files[key];
                        var fname = file.FileName;
                        var path = "C:\\temp\\" + fname;
                        file.SaveAs(path);
                    }
                    // IE fix
                    return Json(new { success = true }, "text/html");
                }
                catch (Exception ex)
                {
                    // IE fix
                    return Json(new { success = false, error = ex.Message }, "text/html");
                }
            }
        }

        public static void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[8 * 1024];
            int len;
            while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, len);
            }
        }
    }
}
