namespace OpenCat.ApiControllers
{
    using OpenCat.Data;
    using OpenCat.Models;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    public class DocumentsController : ApiController
    {
        public Repository<Document> Repository { get; set; }

        public DocumentsController()
        {
            Repository = new Repository<Document>();
        }

        public DTO Get()
        {
            return new DTO { documents = Repository.Get() };
        }

        public DTO Get(string id)
        {
            var document = Repository.Get(id);
            if (document == null) throw new HttpResponseException(HttpStatusCode.NotFound);
            return new DTO { document = document };
        }

        public DTO Post(DTO dto)
        {
            Repository.Create(dto.document);
            return dto;
        }

        public DTO Put(string id, DTO dto)
        {
            var ok = Repository.Edit(id, dto.document);
            if (!ok) throw new HttpResponseException(HttpStatusCode.BadRequest);
            return dto;
        }

        public void Delete(string id)
        {
            Repository.Delete(id);
        }
    }
}
