using Browser.Core.Framework;
using LMS.Data;
using LS.AppFramework.HelperMethods;
using Mainpro.AppFramework;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Configuration;
using System.Linq;
using DBUtils_Mainpro = Mainpro.AppFramework.DBUtils_Mainpro;

namespace Mainpro.UITest
{
    /// <summary>
    /// Entending BrowserTest. Handles setup and configuration for all of selenium tests to run tests against multiple
    /// web browsers (Chrome, Firefox, IE).
    /// </summary>
    public abstract class TestBase : BrowserTest
    {
        public IWebDriver browser;

        public string password = UserUtils.Password;

       

        public  static TimeZoneInfo est = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
        public  DateTime currentDatetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, est);

        public MainproHelperMethods Help = new MainproHelperMethods();
        public LSHelperMethods LSHelp = new LSHelperMethods();
       


        #region Constructors
        // Local Selenium Test
        public TestBase(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }

        // Remote Selenium Grid Test
        public TestBase(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri) { }
        #endregion Constructors

        /// <summary>
        /// Use this to override TestSetup in BrowserTest to perform setup logic that should occur before EVERY TEST. Can use TestTearDown also
        /// </summary>
        public override void BeforeTest()
        {
            base.BeforeTest();
            browser = base.Browser;
            DBUtils_Mainpro.SQLconnString = Const_Mainpro.SQLconnString;
            DBUtils.SQLconnString = Const_Mainpro.SQLconnString;


            /*var currentTestCategories = TestContext.CurrentContext.Test.Properties["Category"];
            string environmentWarning = string.Format("This test is being skipped because it is not compatible with the {0} environment, or not supposed" +
                        " to be executed on this environment. If this is incorrect, and you know that the this test should be " +
                        "executed on the {0} environment, then add a Category to this test with a value of {0}", Constants.CurrentEnvironment);

            // If the current environment is QA, but the current test's NUNIT Categories do not include a "QA" Category, then skip the test
            if (Help.EnvironmentEquals(Constants.Environments.CMEQA))
            {
                if (currentTestCategories.IsNullOrEmpty() || !currentTestCategories.Contains((string)"QA"))
                {
                    Assert.Ignore(environmentWarning);
                }
            }
            if (Help.EnvironmentEquals(Constants.Environments.UAT))
            {
                if (currentTestCategories.IsNullOrEmpty() || !currentTestCategories.Contains((string)"UAT"))
                {
                    Assert.Ignore(environmentWarning);
                }
            }
            if (Help.EnvironmentEquals(Constants.Environments.Production))
            {
                if (currentTestCategories.IsNullOrEmpty() || currentTestCategories.Any(str => (string)str != "Production"))
                {
                    Assert.Ignore(environmentWarning);
                }
            }*/
        }

        /// <summary>
        /// Use this to override TestSetup in BrowserTest to perform setup logic that should occur before EVERY TEST. Can use TestTearDown also
        /// </summary>
        public override void AfterTest()
        {
          
            // If the test failed, then print the username to a file in the TestOutput directory
            if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.ResultState.Failure.Status)
            {
                // If the test has nagivated to LTS or is not on the Mainpro URL site, we can not get the username because
                // the username is obtained by getting the Mainpro cookie inside the Mainpro URL. We wont need the username
                // in this case anyway because the screenshot will show it on LTS.
                if (Browser.Url.Contains("https://cfpc."))
                {
                    try
                    {
                        string username = APIHelp.GetUserName(browser);
                        FileUtils.Notepad_StoreResults(Browser, "UsernameForFailedTest_" + TestContext.CurrentContext.Test.Name,
                            username);
                    }
                    catch
                    {

                    }

                }
            }

            base.AfterTest();
        }
    }
}
