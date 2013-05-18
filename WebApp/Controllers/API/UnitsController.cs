namespace OpenCat.ApiControllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using OpenCat.Models;
    using OpenCat.Services;

    public class UnitsController : ApiController
    {
        public UnitService Units { get; set; }

        public UnitsController(UnitService units)
        {
            Units = units;
        }

        public IEnumerable<Unit> Get([FromUri] string[] ids)
        {
            if (ids != null)
            {
                return Units.Read(ids);
            }
            else
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }

        public Unit Get(string id)
        {
            var unit = Units.Read(id);
            if (unit == null) throw new HttpResponseException(HttpStatusCode.NotFound);
            return unit;
        }

        public void Put(string id, Unit unit)
        {
            var ok = Units.Update(id, unit);
            if (!ok) throw new HttpResponseException(HttpStatusCode.BadRequest);
        }
    }
}