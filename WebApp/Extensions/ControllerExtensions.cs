namespace OpenCat.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Mvc;

    public static class ControllerExtensions
    {
        public static JsonResult UploadFiles(this Controller controller, Action<IEnumerable<UploadedFile>> action)
        {
            if (controller.Request.IsAjaxRequest())
            {
                try
                {
                    var files = new List<UploadedFile> { 
                        new UploadedFile ( 
                            controller.Server.UrlDecode(controller.Request.Headers["x-file-name"]),
                            controller.Request.InputStream
                        )
                    };
                    action.Invoke(files);
                    return new JsonResult { Data = new { success = true } };
                }
                catch (Exception ex)
                {
                    return new JsonResult { Data = new { success = false, error = ex.Message } };
                }
            }
            else
            {
                try
                {
                    var files = new List<UploadedFile>();
                    foreach (var key in controller.Request.Files.AllKeys)
                    {
                        var file = controller.Request.Files[key];
                        files.Add(new UploadedFile(file.FileName, file.InputStream));
                    }
                    action.Invoke(files);
                    return new JsonResult { Data = new { success = true }, ContentType = "text/html" }; // IE fix
                }
                catch (Exception ex)
                {
                    return new JsonResult { Data = new { success = false, error = ex.Message }, ContentType = "text/html" }; // IE fix
                }
            }
        }
    }
}