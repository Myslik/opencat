namespace OpenCat.ApiControllers
{
    using OpenCat.Models;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Web.Http;

    public class LanguagesController : ApiController
    {
        public IEnumerable<Language> Get()
        {
            return CultureInfo.GetCultures(CultureTypes.AllCultures)
                .OrderBy(culture => culture.DisplayName)
                .Select(culture => new Language { ietf = culture.IetfLanguageTag, name = culture.DisplayName });
        }
    }
}
