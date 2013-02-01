namespace OpenCat.ApiControllers
{
    using OpenCat.Models;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Web.Http;

    public class LanguagesController : ApiController
    {
        public DTO Get()
        {
            return new DTO
            {
                languages = CultureInfo.GetCultures(CultureTypes.AllCultures)
                    .OrderBy(culture => culture.DisplayName)
                    .Select(culture => new Language { id = culture.IetfLanguageTag, name = culture.DisplayName })
            };
        }

        public DTO Get(string id)
        {
            var culture = CultureInfo.GetCultureInfo(id);
            return new DTO { language = new Language { id = culture.IetfLanguageTag, name = culture.DisplayName } };
        }
    }
}
