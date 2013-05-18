using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OpenCat.Models;

namespace OpenCat.Parsers
{
    public abstract class Parser : IParser
    {
        protected virtual Unit CreateUnit(Attachment attachment)
        {
            return new Unit
            {
                attachment_id = attachment.id,
                job_id = attachment.job_id
            };
        }

        public abstract bool CanParse(Attachment attachment);
        public abstract IEnumerable<Unit> Parse(Attachment attachment);
    }
}