using Browser.Core.Framework;
using LMS.Data;
using OpenQA.Selenium;
using NUnit.Framework;
using LMS.AppFramework.Constants_;
using LMS.AppFramework.DBUtils_;
using LMS.AppFramework.LMSHelperMethods;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Config;
using AventStack.ExtentReports;
using System;

namespace LMS.UITest
{
    /// <summary>
    /// Extending BrowserTest. Handles setup and configuration for all of selenium tests to run tests against multiple
    /// web browsers (Chrome, Firefox, IE).
    /// </summary>
    public abstract class TestBase : BrowserTest
    {
        public IWebDriver browser;

        public LMSHelperMethods Help = new LMSHelperMethods();
        public static TimeZoneInfo est = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
        public DateTime currentDatetimeinEst = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, est);
        //ublic CMEHelperGeneral CMEHelp = new CMEHelperGeneral();

        #region Constructors

        // Local Selenium Test
        public TestBase(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }

        // Remote Selenium Grid Test
        public TestBase(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri) { }
        #endregion Constructors

        /// <summary>
        /// Use this to override TestSetup in BrowserTest to perform setup logic that should occur before EVERY TEST inside this project. 
        /// Can use TestTearDown also
        /// </summary>
        public override void BeforeTest()
        {
            base.BeforeTest();

            browser = base.Browser;
            DBUtils.SQLconnString = Constants_LMS.SQLconnString;
        }


    }
}
