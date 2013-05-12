using System;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using TechTalk.SpecFlow;
using WebApp.Specs.Pages;

namespace WebApp.Specs.Steps
{
    [Binding]
    public class JobsSteps : WebStepsBase
    {
        [StepDefinition(@"I create following jobs"), Scope(Tag = "WebUI")]
        public void WhenICreateFollowingJobs(Table table)
        {
            On<MainPage>().Visit("Jobs");

            foreach (var row in table.Rows)
            {
                On<JobsPage>().New.Click();
                On<NewJobPage>(page =>
                {
                    page.Name.SendKeys(row["name"]);
                    page.Description.SendKeys(row["description"]);
                    page.Words.SendKeys(row["words"]);
                    page.Save.Click();
                });
            }
        }

        [Then(@"I can see following on jobs page"), Scope(Tag = "WebUI")]
        public void ThenICanSeeFollowingOnJobsPage(Table table)
        {
            On<MainPage>().Visit("Jobs");
            
            var jobs = On<JobsPage>().Jobs;
            foreach (var row in table.Rows)
            {
                Assert.IsTrue(jobs.Any(j => j.Name.Text == row["name"] && j.Description.Text == row["description"] && j.Words.Text == row["words"]), 
                    String.Format("Job with name {0} was not found on jobs page or does not have correct attributes", row["name"]));
            }
        }

        [When(@"I delete job with name (.*)"), Scope(Tag = "WebUI")]
        public void WhenIDeleteJobWithName(string name)
        {
            On<MainPage>().Visit("Jobs");

            var job = On<JobsPage>().Jobs.FirstOrDefault(j => j.Name.Text == name);
            Assert.IsNotNull(job, String.Format("Job with name {0} was not found on jobs page", name));

            job.Remove.Click();
        }

        [Then(@"There is no job with name (.*) on jobs page"), Scope(Tag = "WebUI")]
        public void ThenThereIsNoJobWithNameOnJobsPage(string name)
        {
            On<MainPage>().Visit("Jobs");

            var job = On<JobsPage>().Jobs.FirstOrDefault(j => j.Name.Text == name);
            Assert.IsNull(job, String.Format("Job with name {0} is still on the jobs page", name));
        }

        [When(@"I visit job with name (.*)"), Scope(Tag = "WebUI")]
        public void WhenIVisitJobWithName(string name)
        {
            On<MainPage>().Visit("Jobs");

            var job = On<JobsPage>().Jobs.FirstOrDefault(j => j.Name.Text == name);
            Assert.IsNotNull(job, String.Format("Job with name {0} was not found on jobs page", name));

            job.Name.Click();
        }

        [Then(@"I can edit the job"), Scope(Tag = "WebUI")]
        public void ThenICanEditTheJob()
        {
            On<EditJobPage>(page =>
            {
                Assert.IsNotNull(page.Name, "Cannot edit name");
                Assert.IsNotNull(page.Description, "Cannot edit description");
            });
        }

        [When(@"I upload ""(.*)"" to job"), Scope(Tag = "WebUI")]
        public void WhenIUploadToJob(string p0)
        {
            //var driver = WebDriver as OpenQA.Selenium.PhantomJS.PhantomJSDriver;            

            //var detector = new LocalFileDetector();
            //driver.FileDetector = detector;
            //Console.WriteLine("Detector: " + detector.IsFile(File(p0)));
            
            //Console.WriteLine("Driver: " + driver.FileDetector.IsFile(File(p0)));
            //On<EditJobPage>().Upload.SendKeys(File(p0));
            //On<EditJobPage>().Upload.SendKeys(Keys.Enter);
        }

        [Then(@"There is ""(.*)"" in the attachments"), Scope(Tag = "WebUI")]
        public void ThenThereIsInTheAttachments(string p0)
        {
            var attachment = On<EditJobPage>().Attachments.FirstOrDefault(a => a.Name.Text == p0);
            Assert.IsNull(attachment, String.Format("Attachment with name {0} was not found in attachments", p0));
        }

    }
}
