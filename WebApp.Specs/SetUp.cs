using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using NUnit.Framework;

namespace WebApp.Specs
{
    [SetUpFixture]
    public class SetUp
    {
        private Process iisexpress;

        private string IISExpress
        {
            get
            {
                var programs = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
                return Path.Combine(programs, "IIS Express\\iisexpress.exe");
            }
        }

        private string SitePath
        {
            get
            {
                var path = new DirectoryInfo(Environment.CurrentDirectory);
                return Path.Combine(path.Parent.Parent.Parent.FullName, "WebApp");
            }
        }

        private int Port
        {
            get
            {
                return Int32.Parse(ConfigurationManager.AppSettings["port"]);
            }
        }

        [SetUp]
        public void RunBeforeAnyTests()
        {
            iisexpress = new Process();
            iisexpress.StartInfo = new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = IISExpress,
                Arguments = String.Format("/path:{0} /port:{1}", SitePath, Port)
            };
            iisexpress.Start();
        }

        [TearDown]
        public void RunAfterAnyTests()
        {
            iisexpress.Kill();
        }
    }
}
