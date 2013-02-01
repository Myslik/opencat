﻿using OpenCat.Utils;
using System.Reflection;
using System.Text;

namespace OpenCat
{
    public class Compiler
    {
        private ScriptEngine _engine = null;
        private ParsedScript _vm = null;

        public Compiler()
        {
            _engine = new ScriptEngine("jscript");
            _vm = _engine.Parse(
                LoadResource("sandbox.js")
                + LoadResource("compiler.js"));
        }


        private ParsedScript VM
        {
            get { return this._vm; }
        }

        private static string LoadResource(string name)
        {
            var asm = Assembly.GetCallingAssembly();
            var stream = asm.GetManifestResourceStream("OpenCat.Compiler." + name);
            var reader = new System.IO.StreamReader(stream);
            return reader.ReadToEnd();
        }

        public string Precompile(string template)
        {
            return (string)VM.CallMethod("precompile", template);
        }
    }

    public class TemplateBuilder
    {
        private StringBuilder builder;
        private Compiler compiler;

        public TemplateBuilder()
        {
            this.builder = new StringBuilder();
            this.compiler = new Compiler();
        }

        public void Register(string templateName, string templateBody)
        {
            builder.AppendFormat("Ember.TEMPLATES[\"{0}\"] = Ember.Handlebars.template({1});\n", templateName, this.compiler.Precompile(templateBody));
        }

        public override string ToString()
        {
            return this.builder.ToString();
        }
    }
}
