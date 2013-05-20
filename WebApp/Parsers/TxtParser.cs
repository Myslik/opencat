using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using OpenCat.Models;

namespace OpenCat.Parsers
{
    public class TxtParser : Parser
    {
        public override string Name { get { return "Basic Text Parser"; } }
        public override string Description { get { return "Parses plain text files."; } }
        public override string Author { get { return "premyslkrajcovic@gmail.com"; } }

        public override IEnumerable<Unit> Parse(Attachment attachment)
        {
            using (var reader = new StreamReader(attachment.OpenRead()))
            {
                while (reader.Peek() >= 0)
                {
                    var values = reader.ReadLine().Split('|');
                    if (values.Length == 2)
                    {
                        var unit = CreateUnit(attachment);
                        unit.source = values[0];
                        unit.target = values[1];
                        yield return unit;
                    }
                }
            }
        }

        public override bool CanParse(Attachment attachment)
        {
            return attachment.name.EndsWith(".txt", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}