using Browser.Core.Framework;
using LMS.Data;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using LMS.AppFramework;
using System.Data;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using Browser.Core.Framework.Utils;
using System.Diagnostics;
using OpenQA.Selenium.Firefox;
using System.Configuration;
using OpenQA.Selenium.Remote;
using System.Reflection;
using System.IO;
using System.Globalization;
using OpenQA.Selenium.Chrome;
using log4net;
using LMS.AppFramework.Constants_;

namespace UAMS.UITest
{
    [BrowserMode(BrowserMode.New)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]

    [TestFixture]
    [Parallelizable(ParallelScope.Children)]
    public class UAMS_Load_Tests : TestBase_UAMS
    {
        #region Constructors

        public UAMS_Load_Tests(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public UAMS_Load_Tests(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version , platform, hubUri, extrasUri)
        { }

        #endregion Constructors

        #region Setup/Teardown

        public override void BeforeTest()
        {
            DBUtils.SQLconnString = Constants_LMS.SQLconnString;
        }

        public override void AfterTest()
        {

        }

        //public DesiredCapabilities TestSetup1()
        //{
        //    DesiredCapabilities caps = null;
        //    if (BrowserName == BrowserNames.Chrome)
        //    {
        //        caps = ChromeOptions.ToCapabilities() as DesiredCapabilities;
        //        var buildNumber = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        //        caps.SetCapability("build", buildNumber);
        //        caps.SetCapability("name", TestContext.CurrentContext.Test.Name);
        //    }
        //    else if (BrowserName == BrowserNames.InternetExplorer)
        //    {
        //        caps = InternetExplorerOptions.ToCapabilities() as DesiredCapabilities;
        //    }
        //    else if (BrowserName == BrowserNames.Firefox)
        //    {
        //        caps = FirefoxOptions.ToCapabilities() as DesiredCapabilities;
        //    }
        //    else
        //    {
        //        throw new InvalidOperationException(string.Format("Uknonwn browser name {0}", BrowserName));
        //    }

        //    var service = FirefoxDriverService.CreateDefaultService(@"C:\seleniumdrivers", "geckodriver.exe");
        //    service.FirefoxBinaryPath = @"C:\Program Files (x86)\Mozilla Firefox\firefox.exe";

        //    return caps;
        //}

        #endregion Setup/Teardown


        #region Tests

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity01()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));

        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity02()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity03()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity04()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity05()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity06()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity07()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity08()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity09()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity10()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity11()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity12()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity13()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity14()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity15()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity16()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity17()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity18()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity19()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity20()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity21()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity22()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity23()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity24()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity25()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity26()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity27()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity28()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity29()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity30()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity31()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity32()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity33()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity34()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity35()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity36()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity37()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity38()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity39()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity40()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity41()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity42()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity43()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity44()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity45()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}
        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity46()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity47()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity48()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity49()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity50()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]        
        //public void CompleteActivity51()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity52()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity53()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity54()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity55()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity56()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity57()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity58()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity59()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity60()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity61()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity62()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity63()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity64()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity65()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity66()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity67()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity68()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity69()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity70()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity71()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity72()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity73()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity74()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity75()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity76()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity77()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity78()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity79()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity80()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity81()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity82()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity83()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity84()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity85()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity86()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity87()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity88()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity89()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity90()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity91()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity92()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity93()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity94()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity95()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}
        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity96()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity97()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity98()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity99()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity100()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity101()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity102()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity103()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity104()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity105()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity106()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity107()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity108()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity109()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity110()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity111()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity112()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity113()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity114()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity115()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity116()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity117()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity118()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity119()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity120()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity121()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity122()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity123()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity124()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity125()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity126()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity127()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity128()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity129()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity130()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity131()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity132()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity133()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity134()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity135()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity136()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity137()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity138()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity139()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity140()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity141()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity142()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity143()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity144()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity145()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}
        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity146()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity147()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity148()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity149()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity150()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity151()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity152()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity153()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity154()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity155()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity156()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity157()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity158()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity159()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity160()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity161()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity162()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity163()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity164()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity165()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity166()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity167()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity168()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity169()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity170()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity171()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity172()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity173()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity174()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity175()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity176()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity177()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity178()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity179()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity180()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity181()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity182()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity183()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity184()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity185()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity186()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity187()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity188()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity189()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity190()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity191()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity192()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity193()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity194()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity195()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}
        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity196()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity197()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity198()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity199()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity200()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity201()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity202()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity203()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity204()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity205()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity206()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity207()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity208()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity209()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity210()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity211()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity212()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity213()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity214()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity215()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity216()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity217()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity218()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity219()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity220()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity221()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}


        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity222()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity223()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity224()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity225()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity226()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity227()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity228()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity229()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        //[TestCase, Category("UAMS"), Category("loadtestcompleteactivity"), Category("Prod")]
        //public void CompleteActivity230()
        //{
        //    DesiredCapabilities caps = TestSetup1();
        //    IWebDriver driver = new RemoteWebDriver(new Uri(SeleniumCoreSettings.HubUri), caps, TimeSpan.FromSeconds(800));
        //    LoadTestScenario(driver, UserUtils.CreateUser(Constants.SiteCodes.UAMS));
        //}

        /// <summary>
        /// Record the time it takes to get response
        /// </summary>
        /// <param name="driver"></param>
        public void LoadTestScenario(IWebDriver driver, UserModel user)
        {
            driver.Manage().Window.Maximize();

            try
            {
                Help.CompleteActivity(driver, Constants.SiteCodes.UAMS, "mjnew1", false, user.Username, user.Password);
            }
            catch (Exception e)
            {
                //if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.ResultState.Failure.Status)
                //{
                //    screenshotLocation = TakeScreenshot(context.Test);
                //}

                driver.Close();
                driver.Quit();
                throw new Exception(e.Message);
            }

            driver.Close();
            driver.Quit();
        }


        #endregion tests
    }
}






