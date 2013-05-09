namespace OpenCat.Models
{
    using System;

    public class User : Entity
    {
        public string identifier { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string gravatar { get; set; }

        public string password { get; set; }
    }
}
