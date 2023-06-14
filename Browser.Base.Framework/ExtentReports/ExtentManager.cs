using System;
using System.Globalization;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
// Extent 4 used AventStack.ExtentReports.Reporter.Configuration;, Extent 5 uses AventStack.ExtentReports.Reporter.Config;
//using AventStack.ExtentReports.Reporter.Configuration;
using AventStack.ExtentReports.Reporter.Config;
using NUnit.Framework;

// See below link section titled "An example of a thread-safe manager for ExtentTests". Creating these separate classes was the
// only way I could produce these reports and not have the Environment section be duplicated when I executed tests in parallel
// https://www.extentreports.com/docs/versions/3/net/
// Random How To: https://www.lambdatest.com/blog/report-in-nunit/
namespace Browser.Core.Framework
{
    public class ExtentManager
    {
        private static readonly Lazy<ExtentReports> _lazy = new Lazy<ExtentReports>(() => new ExtentReports());

        public static ExtentReports Instance { get { return _lazy.Value; } }

        static ExtentManager()
        {
            var reportPath = "";
            var testOutputDirectory = SeleniumCoreSettings.TestOutputLocation;

            // Extent Report version 4 does not allow you to give a custom name to the report file. This is by design
            // https://github.com/extent-framework/extentreports-csharp/issues/40 
            // The only workaround is to use the deprecated extent3reporter, as such...
            // ExtentV3HtmlReporter htmlReporter = new ExtentV3HtmlReporter(reportPath);
            // var htmlReporter = new ExtentV3HtmlReporter(@"C:\testR\AutoReport.html");
            // But the above workaround is deprecated and will be removed in a future release
            // Update: Version 5 allows for custom name, Currently (as of 2022-10-19) Extent report 
            // for .NET is at version 5 alpha 6. This version was released on 2021-03-08 and there 
            // has not been a newer version. It also was not released on NuGet so you have to
            // install it through the NuGet console. Also of note, there is now a paid and community version. The 
            // community version doesnt let you send emails. See https://www.extentreports.com/
            //var currentTestName =  GetScreenshotFileName(currentTest);

            if (!AppSettings.Config["ExtentReport_AddDateStampToFileName"].IsNullOrEmpty())
            {
                if (AppSettings.Config["ExtentReport_AddDateStampToFileName"].ToString() == "true" ||
                    AppSettings.Config["ExtentReport_AddDateStampToFileName"].ToString() == "True")
                {
                    reportPath = string.Format("{0}{1}", testOutputDirectory, string.Format("Report_({0}).html",
                        DateTime.Now.ToString("yyyy-MM-dd_HH.mm.ss", CultureInfo.InvariantCulture)));
                }
                else
                {
                    reportPath = string.Format("{0}{1}", testOutputDirectory, "Report.html");
                }
            }
            else
            {
                reportPath = string.Format("{0}{1}", testOutputDirectory, "Report.html");
            }

            // Extent 4 used ExtentHTMLReporter, Extent 5 uses ExtentSparkReporter
            var htmlReporter = new ExtentSparkReporter(reportPath);

            htmlReporter.Config.Theme = Theme.Dark;
            htmlReporter.Config.DocumentTitle = "Automation Results";
            htmlReporter.Config.ReportName = "Automation Results";

            string environment = AppSettings.Config["environment"];
            Instance.AddSystemInfo("Environment", environment);            

            Instance.AttachReporter(htmlReporter);
        }

        private ExtentManager()
        {
        }
    }
}