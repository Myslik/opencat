namespace OpenCat.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Mvc;

    public static class ControllerExtensions
    {
        public static JsonResult UploadFiles(this Controller controller, Action<UploadedFile> action)
        {
            if (controller.Request.IsAjaxRequest())
            {
                try
                {
                    action.Invoke(new UploadedFile(
                        controller.Server.UrlDecode(controller.Request.Headers["x-file-name"]),
                        controller.Request.InputStream
                    ));
                    return new JsonResult { Data = new { success = true } };
                }
                catch (IOException ex)
                {
                    return new JsonResult { Data = new { success = false, error = ex.Message } };
                }
            }
            else
            {
                try
                {
                    foreach (var key in controller.Request.Files.AllKeys)
                    {
                        var file = controller.Request.Files[key];
                        action.Invoke(new UploadedFile(file.FileName, file.InputStream));
                    }
                    
                    return new JsonResult { Data = new { success = true }, ContentType = "text/html" }; // IE fix
                }
                catch (IOException ex)
                {
                    return new JsonResult { Data = new { success = false, error = ex.Message }, ContentType = "text/html" }; // IE fix
                }
            }
        }
    }
}