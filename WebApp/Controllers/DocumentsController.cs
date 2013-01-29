using System.Collections.Generic;
using System.Web.Http;
using OpenCat.Models;
using System.Net.Http;
using System.Web.Http.Filters;
using MongoDB.Bson;
using System.Linq;

namespace OpenCat.Controllers
{
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
            return new DTO { document = Repository.Get(id) };
        }

        public DTO Post(DTO dto)
        {
            Repository.Create(dto.document);
            return dto;
        }

        public DTO Put(HttpRequestMessage request, string id, DTO dto)
        {
            Repository.Edit(id, dto.document);
            return dto;
        }

        public void Delete(string id)
        {
            Repository.Delete(id);
        }
    }
}
