using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using OpenCat.Models;
using OpenCat.Parsers;

namespace OpenCat.Plugins
{
    public class Plugin : IPlugin
    {
        public virtual string Name { get; protected set; }
        public virtual string Description { get; protected set; }
        public virtual string Author { get; protected set; }

        public static IEnumerable<Plugin> LoadAll()
        {
            return Assembly.GetExecutingAssembly().GetTypes()
                .Where(x => x.IsSubclassOf(typeof(Plugin)))
                .Select(t => Activator.CreateInstance(t) as Plugin);
        }

        public static IEnumerable<T> Load<T>()
        {
            return Assembly.GetExecutingAssembly().GetTypes()
                .Where(x => x.IsSubclassOf(typeof(Plugin)))
                .Where(x => x.IsSubclassOf(typeof(T)))
                .Select(t => Activator.CreateInstance(t)).Cast<T>();
        }

        public static IEnumerable<Unit> Parse(Attachment attachment)
        {
            var parsers = Load<Parser>();

            foreach (var parser in parsers)
            {
                if (parser.CanParse(attachment))
                {
                    return parser.Parse(attachment);
                }
            }
            return new Unit[0].AsEnumerable();
        }
    }
}