using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenCat.Models
{
    public class Unit : Entity
    {
        public string source { get; set; }
        public string target { get; set; }

        public string job_id { get; set; }
        public string attachment_id { get; set; }
    }
}