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
            return CultureInfo.GetCultures(CultureTypes.AllCultures).Take(40)
                    .OrderBy(culture => culture.DisplayName)
                    .Select(culture => new Language { id = culture.IetfLanguageTag, name = culture.DisplayName });
        }

        public Language Get(string id)
        {
            var culture = CultureInfo.GetCultureInfo(id);
            return new Language { id = culture.IetfLanguageTag, name = culture.DisplayName };
        }
    }
}
