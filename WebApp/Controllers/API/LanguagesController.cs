namespace OpenCat.ApiControllers
{
    using OpenCat.Models;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Web.Http;
    using System.Net;

    public class LanguagesController : ApiController
    {
        private IQueryable<Language> Languages { get; set; }

        public LanguagesController()
        {
            Languages = CultureInfo.GetCultures(CultureTypes.AllCultures)
                    .OrderBy(culture => culture.DisplayName)
                    .Select(culture => new Language { id = culture.IetfLanguageTag, name = culture.DisplayName })
                    .AsQueryable();
        }

        public IEnumerable<Language> Get()
        {
            return Languages.AsEnumerable();
        }

        public Language Get(string id)
        {
            var language = Languages.Where(l => l.id == id).SingleOrDefault();
            if (language == null) throw new HttpResponseException(HttpStatusCode.NotFound);
            return language;
        }
    }
}
