using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenCat.Models
{
    public class DTO
    {
        public DTO()
        {
            this.Errors = new List<object>();
        }

        public Document document { get; set; }
        public IEnumerable<Document> documents { get; set; }
        public List<object> Errors { get; set; }
    }
}