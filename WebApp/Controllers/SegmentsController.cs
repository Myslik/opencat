using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OpenCat.Models;

namespace OpenCat.Controllers
{
    public class SegmentsController : ApiController
    {
        // GET api/values
        public IEnumerable<Segment> Get()
        {
            return new Segment[] { new Segment { source = "source", target = "target" } };
        }

        // GET api/values/5
        public Segment Get(int id)
        {
            return new Segment { source = "source", target = "target" };
        }

        // POST api/values
        public void Post([FromBody]Segment value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]Segment value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}