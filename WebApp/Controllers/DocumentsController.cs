using System.Collections.Generic;
using System.Web.Http;
using OpenCat.Models;

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

        public DTO Get(int id)
        {
            return new DTO { document = Repository.Get(id) };
        }

        public DTO Post(DTO dto)
        {

            Repository.Create(dto.document);
            return dto;
        }

        public DTO Put(int id, DTO dto)
        {
            dto.document.id = id;
            Repository.Edit(dto.document);
            return dto;
        }

        public void Delete(int id)
        {
            Repository.Delete(id);
        }
    }
}
