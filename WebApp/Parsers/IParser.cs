using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCat.Models;

namespace OpenCat.Parsers
{
    public interface IParser
    {
        bool CanParse(Attachment attachment);
        IEnumerable<Unit> Parse(Attachment attachment);
    }
}
