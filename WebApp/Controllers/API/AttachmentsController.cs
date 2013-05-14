namespace OpenCat.ApiControllers
{
    using OpenCat.Models;
    using System.Collections.Generic;
    using System.Web.Http;
    using OpenCat.Services;

    public class AttachmentsController : ApiController
    {
        private AttachmentService Attachments { get; set; }

        public AttachmentsController(AttachmentService attachments)
        {
            Attachments = attachments;
        }

        public IEnumerable<Attachment> Get([FromUri] string[] ids)
        {
            if (ids.Length > 0)
            {
                return Attachments.Read(ids);
            }
            return Attachments.Read();
        }

        public Attachment Get(string id)
        {
            return Attachments.Read(id);
        }

        public void Delete(string id)
        {
            Attachments.Delete(id);
        }
    }
}
