using Browser.Core.Framework.Resources;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using LOG4NET = log4net.ILog;
using AventStack.ExtentReports.Reporter;
// Extent 4 used AventStack.ExtentReports.Reporter.Configuration;, Extent 5 uses AventStack.ExtentReports.Reporter.Config;
//using AventStack.ExtentReports.Reporter.Configuration;
using AventStack.ExtentReports.Reporter.Config;
using AventStack.ExtentReports;
using NUnit.Framework.Internal;
using System.Configuration;
using NUnit.Framework.Interfaces;
using Browser.Core.Framework;
using System.Threading;
using Titanium.Web.Proxy;
using System.Collections.Concurrent;
using OpenQA.Selenium.Safari;
using WDSE.Decorators;
using WDSE.ScreenshotMaker;
using WDSE;
using log4net.Appender;
using log4net.Core;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using System.Collections;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Model;
using MySqlX.XDevAPI.Common;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using Spire.Pdf.Tables;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
using System.Management;

namespace Browser.Core.Framework
{
    /// <summary>
    /// Base class for all Selenium tests.  Handles setup and configuration to run tests against multiple web browsers
    /// </summary>
    public class BrowserTest //: Test
    //public abstract class BrowserTest // See Wikipedia.UITest > GlobalSetupAndTeardown for explanation of why we dont use abstract anymore
    {
        #region Private Fields

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private string _browserName;

        // Emulation support
        private string _emulationDevice;

        // Support for Selenium Grid Testing
        private bool _isRemote = false;
        private string _version = null;
        private string _platform = null;
        private string _hubUri = null;
        private string _extrasUri = null;

        private string _driverPathChrome = SeleniumCoreSettings.DriverLocation;
        private string _driverPathEdge = SeleniumCoreSettings.DriverLocation;
        private string _driverPathFirefox = SeleniumCoreSettings.DriverLocation;

        private Stopwatch _testStopwatch;
        private string initialUrl = null;

        #endregion

        #region Protected Properties

        /// <summary>
        /// Gets the context associated with the test fixture. There is a SEPARATE context associated with each TEST.
        /// You can access the test context by calling TestContext.Current within any given test.
        /// </summary>
        protected TestContext FixtureContext { get; private set; }

        /// <summary>
        /// Gets the initial URL that the browser shows upon startup in selenium.
        /// Most browser drivers define this, but if not we'll default to 'about:blank'.
        /// about:blank is supported in Chrome, Firefox, and IE
        /// </summary>        
        protected string InitialUrl
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(initialUrl))
                    return initialUrl;

                return "about:blank";
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the browser for the current test.  You can specify whether to re-use an existing browser instance or force a new
        /// browser instance by applying the BrowserModeAttribute to the test method or test class.
        /// </summary>        
        public IWebDriver Browser { get; private set; }

        /// <summary>
        /// The name of the browser to use for this test.
        /// </summary>
        public string BrowserName
        {
            get { return _browserName; }
            private set { _browserName = value; }
        }

        /// <summary>
        /// Whether the Browser is executing on Selenium Grid or Locally
        /// </summary>
        public bool RemoteExecution
        {
            get { return _isRemote; }
            private set { _isRemote = value; }
        }

        /// <summary>
        /// Which emulation device is being used  for this test
        /// </summary>
        public string EmulationDevice
        {
            get { return _emulationDevice; }
            private set { _emulationDevice = value; }
        }

        /// <summary>
        ///
        /// </summary>
        public string DriverPathChrome
        {
            get { return _driverPathChrome; }
            private set { _driverPathChrome = value; }
        }

        /// <summary>
        ///
        /// </summary>
        public string DriverPathEdge
        {
            get { return _driverPathEdge; }
            private set { _driverPathEdge = value; }
        }

        /// <summary>
        ///
        /// </summary>
        public string DriverPathFirefox
        {
            get { return _driverPathFirefox; }
            private set { _driverPathFirefox = value; }
        }

        /// <summary>
        /// Allows the tester to write test steps which will then appear in the Extent HTML test report
        /// </summary>
        public ExtentTest TESTSTEP;

        // window.document.onclick = function() { throw new Error("Custom error thrown here")}
        public List<string> javascripExceptionMessages = new List<string>();

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BrowserTest"/> class.
        /// </summary>
        /// <param name="browserName">The name of the browser to use.</param>
        public BrowserTest(string browserName, string emulationDevice)
        {
            if (string.IsNullOrEmpty(browserName)) throw new ArgumentException(browserName.ToString());
            // I had to comment this out and change it to browerName.ToString() because we are using TFS 2013 for build. The "nameof" is C#6.0 syntax,
            // and TFS 2013 does not build with C#6.0 syntax
            //if (string.IsNullOrEmpty(browserName)) throw new ArgumentException(nameof(browserName));

            // Private property for use in this class
            _isRemote = false;
            // Public properties for use in test level code
            RemoteExecution = false;
            BrowserName = browserName;
            EmulationDevice = emulationDevice;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BrowserTest"/> class,
        /// for remote testing against a Selenium Grid.
        /// </summary>
        /// <param name="browserName">The browser name to test.</param>
        /// <param name="version">The version of the browser to test.</param>
        /// <param name="platform">The platform to run the browser on.</param>
        /// <param name="hubUri">The uri of the Selenium Hub.</param>
        /// <param name="extrasUri">The uri for Selenium Extras.</param>
        public BrowserTest(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
        {
            // Note: version, platform, hubUri, and extrasUri are not "optional" so that this constructor
            // can work with NUnit. 

            if (string.IsNullOrEmpty(browserName)) throw new ArgumentException(browserName.ToString());
            // I had to comment this out and change it to browerName.TosTring() because we are using TFS 2013 for build. The "nameof" is C#6.0 syntax,
            // and TFS 2013 does not build with C#6.0 syntax
            // if (string.IsNullOrEmpty(browserName)) throw new ArgumentException(nameof(browserName));

            // Private property for use in this class
            _isRemote = true;
            // Public properties for use in test level code
            RemoteExecution = true;
            BrowserName = browserName;
            EmulationDevice = emulationDevice;

            // Note: Selenium Grid does not like when any of these parameters are null.
            _version = version ?? string.Empty;
            _platform = platform ?? string.Empty;

            _hubUri = string.IsNullOrEmpty(hubUri) ? SeleniumCoreSettings.HubUri : hubUri;
            _extrasUri = string.IsNullOrEmpty(extrasUri) ? SeleniumCoreSettings.ExtrasUri : extrasUri;
        }

        #endregion

        #region Setup/Teardown
        
        /// <summary>
        /// Override this method to perform setup logic that should be performed once per fixture. If you want to see how 
        /// logic can be performed before all test (ACROSS fixtures), see the GlobalSetupAndTeardown class within Wikipedia.UITest
        /// </summary>
        [OneTimeSetUp]
        public virtual void BeforeFixture()
        {
            // Getting the test FIXTURE context by calling TestContext.CurrentContext before the BeforeTest() method is reached.
            // Within BeforeTest(), we call TestContext.CurrentContext again, but that gets the TEST context. i.e. The Fixture
            // context will return the class name, SampleTests("chrome",""), under TestContext.CurrentContext.Test.Name. The
            // Test context will hold the test name, SampleTest_BasicsOfFramework, under TestContext.CurrentContext.Test.Name.
            // Also, within a Fixture context, we can get properties such as the browser type and whether a fixture is remote
            // or local, by extracting it from TestContext.CurrentContext.Test.Properties
            // Further Reading:
            // TestContext in NUnit may refer to an individual test method or a test fixture. Within a test method,
            // SetUp method or TearDown method, the context is that of the individual test case. Within a OneTimeSetUp or
            // OneTimeTearDown method, the context refers to the fixture as a whole. 
            // https://docs.nunit.org/articles/nunit/writing-tests/TestContext.html
            FixtureContext = TestContext.CurrentContext;

            // The below occurs before every fixture as explained in the summary above, as opposed to once globally (before ALL
            // tests). But its fine cause log4net does not lock the file and so race condition will not occur, although the
            // logging will be jumbled up between tests. I tried to implement an assembly-wide global one-time setup, but then
            // realized that is not possible, see the Test class for that explanation. Also, this loggin codecould have been added
            // inside BeforeTest() but log4net does not work in parallel without some complex logic, which would not be worth
            // the time cause it works fine within this one time fixture method
            var fullPath = string.Format("{0}\\log.txt", SeleniumCoreSettings.TestOutputLocation);
            // Log file path. See Log4net.config file within the Browser.Base.Framework project folder within the solution folder
            log4net.GlobalContext.Properties["LogFileName"] = fullPath;
            // The below line was causing warning on the Tests tab of the Output window and within the ADO pipeline. Fixed this
            // by commenting out cause its not needed and caused those issues
            // https://stackoverflow.com/questions/17468841/log4net-configuration-failed-to-find-section
            //log4net.Config.XmlConfigurator.Configure();

            SetWebDrivers();
        }

        /// <summary>
        /// Override this method to perform teardown logic that should be performed once per fixture. If you want to see how 
        /// logic can be performed after all test (ACROSS fixtures), see the GlobalSetupAndTeardown class within Wikipedia.UITest
        /// </summary>
        [OneTimeTearDown]
        public virtual void AfterFixture()
        {
            CleanupBrowser();
        }

        /// <summary>
        /// Override this method to perform setup logic that should occur before EVERY TEST
        /// </summary>
        [SetUp]
        public virtual void BeforeTest()
        {
            var currentTest = TestContext.CurrentContext;

            // We need to set the security protocol for APIs that we use here, so APIs can be called through our automation code
            // Note this may change from time to time, so well need to update this code
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls12;

            // Create an Extent test instance. This needs to be done here because if the driver fails to start the browser
            // (See the DriverStartedBrowserSessionSuccessfully method), then the CreateBrowser() line of code below would 
            // fail and the code would proceed to go to the AfterTest method, the code would NOT proceed inside this method
            var descriptionOfTest = currentTest.Test.Properties.Get("Description"); // This returns null. Right now I cant
                                                                                    // figure out how to get description of test
            string testNameForExtentReport = string.Format("Test Name: {0}<br />" + "Class Name: {1}<br />" +
                "Browser: {2}<br />" + "Remote Execution: {3}<br />",
                currentTest.Test.MethodName, GetType().Name, BrowserName, RemoteExecution.ToString());
            TESTSTEP = ExtentTestManager.CreateTest(testNameForExtentReport);

            var mode = GetBrowserMode();
            //LogHelper(currentTest, Level.Info, "BeginTest >>" + GetDriverCountMessage());

            // Determine whether to create a new browser session or reuse the existing one
            // Only create a new session if it was explicitly requested, or if this is the first test.
            if (Browser == null || mode == BrowserMode.New)
            {
                var sw = Stopwatch.StartNew();

                // Note: Each test can determine if it uses "New". Therefore, I can't do
                // the cleanup until the next test starts. Also note "Reuse" does not work 
                // across test fixtures.
                CleanupBrowser();

                _testStopwatch = Stopwatch.StartNew();

                CreateBrowser();
                sw.Stop();

                // Log requested and actual browser, version and platform
                string requestedBrowser = BrowserName;
                string requestedVersion = _version ?? "NotRequested/Unknown";
                string requestedPlatform = _platform ?? "NotRequested/Unknown";
                string actualBrowser = Browser.GetCapabilities().GetCapability("browserName").ToString();
                string actualVersion = Browser.GetCapabilities().GetCapability("browserVersion").ToString();
                string actualPlatform = Browser.GetCapabilities().GetCapability("platformName").ToString();
                var cap = Browser.GetCapabilities();

                LogHelper(currentTest, Level.Info, string.Format("RequestedBrowser={0}, RequestedVersion={1}, RequestedPlatform={2}, " +
                    "ActualBrowser={3}, ActualVersion={4}, ActualPlatform={5}, Time={6}",
                requestedBrowser, requestedVersion, requestedPlatform, actualBrowser, actualVersion, actualPlatform, sw.Elapsed));
            }
            else
            {
                Browser.Navigate().GoToUrl(InitialUrl);
            }
        }

        /// <summary>
        /// Override this method to perform cleanup that should be performed after EVERY TEST
        /// </summary>
        [TearDown]
        public virtual void AfterTest()
        {
            string screenshotAbsolutePath = "";
            bool screenshotSuccessful = false;
            var currentTest = TestContext.CurrentContext;
            string testLevelTestResultMessage = currentTest.Result.Message; // This will be null if the test passes. 
            string stackTrace = currentTest.Result.StackTrace;
            string testName = currentTest.Test.FullName;
            var testOutcome = currentTest.Result.Outcome;
            var testOutcomeStatus = currentTest.Result.Outcome.Status;
            var browserConsoleLogMsg = "";
            var stacktrace = string.IsNullOrEmpty(stackTrace) ? "" : stackTrace;
            string testOutputMsg = string.Format("To see the test output files from a " +
                        "local test execution, go to: {0}{1}To see test output files from a remote test execution, navigate to the " +
                        "Azure DevOps build execution number URL, then click on the Published link. If you build is in Bamboo, ask your " +
                        "DevOps or QA engineer where the Bamboo Build definition configures the files to be dropped",
                        SeleniumCoreSettings.TestOutputLocation, Environment.NewLine);

            IfDriverInstanceFailedToStartThenEndTest(currentTest);
            IfBrowserUnresponsiveThenEndTest(currentTest);

            _testStopwatch.Stop();

            LogHelper(currentTest, Level.Info, string.Format("EndTest << {0} - {1} - {2} - {3}", testName, testOutcome, _testStopwatch.Elapsed,
                GetDriverCountMessage()));

            // If a set of tests get executed which do not produce any custom screenshots/excel files and they all
            // pass, then the 'TestOutput' folder will not be created. We will create that folder now because it is 
            // needed for the Bamboo build. If that placeholder folder does not exist after a build, the build throws
            // an error stating: Cannot find path 'C:\Users\bamboo\bamboo-agent-home\xml-data\build-dir\LMSREW-PREM-TES\testoutput'
            FileUtils.CreateDirectoryIfNotExists(SeleniumCoreSettings.TestOutputLocation);

            Status extentLogStatus;

            switch (testOutcomeStatus)
            {
                case TestStatus.Passed:
                    extentLogStatus = Status.Pass;
                    TESTSTEP.Log(extentLogStatus, "Test Passed");
                    break;

                case TestStatus.Skipped:
                    extentLogStatus = Status.Skip;
                    TESTSTEP.Log(extentLogStatus, "Test Skipped: " + testLevelTestResultMessage);
                    break;
                // If a test fails, throws a warning, or is inconclusive, we are going to do the same thing (screenshot, logging, etc)
                case TestStatus.Failed:
                case TestStatus.Warning:
                case TestStatus.Inconclusive:
                    if (testOutcomeStatus == TestStatus.Failed)
                    {
                        extentLogStatus = Status.Fail;
                    }
                    else if (testOutcomeStatus == TestStatus.Warning)
                    {
                        extentLogStatus = Status.Warning;
                    }
                    else
                    {
                        extentLogStatus = Status.Info;
                    }

                    // Printing the Browser's Console error messages (usually javascript errors) to the IDE console so that
                    // we can view them within the IDE by clicking the "Open additional output for this result", and also
                    // so we can see them in our TFS/Bambo build logs.
                    // https://stackoverflow.com/questions/18261338/get-chromes-console-log
                    // NOTE: You can manually paste the javascript line below in the Browsers Console to manually produce an
                    // error so you can then test out the below code inside the If statement is working properly:
                    // window.document.onclick = function() { throw new Error("Custom error thrown here")}

                    // We need this sleep here because the Browser takes a moment before it prints out the javascript
                    // errors to the Console of DevTools. In the future, maybe try to figure out a better solution than 
                    // a static sleep
                    Thread.Sleep(1500);

                    // Geckodriver currently does not support printing logs. Its not in W3C specification. There may be 
                    // workarounds, but no one executes in Firefox and/or does not require logs in Firefox
                    // https://stackoverflow.com/questions/59192232/selenium-trying-to-get-firefox-console-logs-results-in-webdrivererror-http-me
                    if (BrowserName != BrowserNames.Firefox)
                    {
                        // Just printing all Console error messages now, as we never needed to filter them yet.
                        // Filtering can be done below. To manually produce an error, type console.error('MyError') in DevTools
                        //var errorStrings = new List<string>{"WARNING","Failed","UncaughtError","SyntaxError","EvalError",
                        //    "ReferenceError","RangeError","TypeError","URIError"};
                        //var browserConsoleLogEntries = Browser.Manage().Logs.GetLog(LogType.Browser).
                        //    Where(x => errorStrings.Any(e => x.Message.Contains(e)));
                        //var browserConsoleLogEntries = Browser.Manage().Logs.GetLog(LogType.Browser);

                        //throw new Exception("");
                        var browserConsoleLogEntries = GetLogs(Browser);

                        browserConsoleLogMsg = browserConsoleLogEntries.Count == 0
                            ? "Console Error Message From Browser: 'No Console error was produced from the Browser. If you expected " +
                            "a Console Error, the Browser Base GetLogs method may have hit the Catch block calling " +
                            "commandExecutor.Execute. In this case, see: " +
                            "https://code.premierinc.com/docs/display/PQA/Issues+General#IssuesGeneral-GetLogsMethodWithinBrowserTestThrowsTeardownException'"
                            : string.Format("Console Error Message From Browser: {0}", string.Join(",", browserConsoleLogEntries));
                    }
                    else
                    {
                        browserConsoleLogMsg = "Console Error Message From Browser: Geckodriver does not support returning Console " +
                            "logs. Pease see the logs from the Chrome or Edge version of your test if you want to view logs";
                    }

                    var dotNetConsoleLogMsg = string.Format("{0}{1}{2}", browserConsoleLogMsg, Environment.NewLine, testOutputMsg);
                    Console.WriteLine(dotNetConsoleLogMsg);
                    LogHelper(currentTest, Level.Info, dotNetConsoleLogMsg);

                    screenshotAbsolutePath = TakeFailedTestScreenshot(currentTest.Test);

                    // If the screenshot failed to save, then we need to print that to the extent report, and then NOT call the
                    // AddTestAttachment method and NOT try to attach a screenshot to Extent report. Since there will be no screenshot,
                    // those methods will fail and then the lines of code after those methods will not get executed
                    if (screenshotAbsolutePath == "Could not get absolute path because it failed to save screenshot" ||
                        screenshotAbsolutePath == "Unable to take screenshot due to alertException = true")
                    {
                        screenshotSuccessful = false;
                    }
                    else
                    {
                        screenshotSuccessful = true;
                    }

                    var logMessage = string.Format("<pre>Test Level Error Message: {0}</pre> " +
                        "<pre>Additional Error Messages, if Applicable: {1}</pre> " +
                        "<pre>{2}</pre> " +
                        "<pre>{3}</pre> " +
                        "<pre>Stack Trace: {4}</pre> ",
                        currentTest.Result.Message,
                        screenshotSuccessful ? null : "Browser.Base.Framework's TakeFailedTestScreenshot method failed to take screenshot",
                        browserConsoleLogMsg,
                        testOutputMsg,
                        stacktrace);

                    if (screenshotSuccessful)
                    {
                        AddTestAttachmentToNunitTestContextInstance(screenshotAbsolutePath, currentTest);
                        string screenshotRelativePath = GetScreenShotRelativePath(screenshotAbsolutePath);

                        // Traditional screen capture
                        // extentTest.Fail(currentTest.Result.Message).AddScreenCaptureFromPath(screenshotLocation);                
                        // Non-traditional screen capture: CreateScreenCaptureFromBase64String                
                        TESTSTEP.Log(extentLogStatus, logMessage, MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotRelativePath).Build());
                    }
                    else
                    {
                        TESTSTEP.Log(extentLogStatus, logMessage);
                    }

                    break;
            }

            // March 19 2022: Premier started scanning our code for security vulnerabilities. Extent version 4 included transitive 
            // dependencies that had security vulnerabilities. I was able to update all but 1 of the transitive dependencies. See:
            // https://code.premierinc.com/docs/display/PQA/Security+Compliance for an explanantion on the security scan process,
            // then see...
            // https://code.premierinc.com/docs/display/PQA/Transitive+Dependencies#TransitiveDependencies-TransitiveDependenciesWeDidNotAttemptToUpdate
            // for an explanation on why we had to upgrade Extent to version 5, Alpha. Once I updated to version 5 Alpha, this next
            // line of code ExtentManager.Instance.Flush would fail occasionally when executing a very large number of tests 
            // in parallel. I would get the following error: The process cannot access the file Report.HTML, because it
            // being used by another process. Even though it would show a failure inside Visual Studio, Extent DID
            // successfully Flush the test to the HTML report and show it as passed. I guess there was a defect in version 5 
            // causing race conditions. I couldnt figure out how to fix it, so I put in a try catch and it hasnt failed since.
            // Extent version 4 never had this issue, so if the below workaround still doesnt prevent the above issue/error
            // even with a Try Catch again, we may have to revert back to version 4 and try to update the transitive dependency,
            // and if not that, hopefully Extent version 5 becomes open source again within .NET and we can then download the
            // non-Alpha version and try it. Hopefully with the Try Catch below, no more errors occur. Monitor
            // Update 2022/10/14: I have not seen this error since upgrading to version 5, Alpha
            try
            {
                ExtentManager.Instance.Flush();
            }
            catch
            {
            }

            //if (currentTest.Result.Outcome.Status == NUnit.Framework.Interfaces.ResultState.Failure.Status)
        }

        #endregion

        #region Virtual Methods

        /// <summary>
        /// Provides a way to customize the InternetExplorer configuration
        /// </summary>
        protected virtual BaseInternetExplorerOptions InternetExplorerOptions
        {
            get { return new BaseInternetExplorerOptions(); }
        }

        //protected virtual BaseChromeEmulationOptions ChromeEmulationOptions
        //{
        //    get { return new BaseChromeEmulationOptions(EmulationDevice); }
        //}

        /// <summary>
        /// Provides a way to customize the Chrome configuration
        /// </summary>
        protected virtual BaseChromeOptions ChromeOptions
        {
            get { return new BaseChromeOptions(EmulationDevice); }
        }

        /// <summary>
        /// Provides a way to customize the Firefox configuration.
        /// </summary>
        protected virtual BaseFirefoxProfile FirefoxProfile
        {
            get { return new BaseFirefoxProfile(); }
        }

        /// <summary>
        /// Provides a way to customize the Firefox options.
        /// </summary>
        protected virtual BaseFirefoxOptions FirefoxOptions
        {
            get { return new BaseFirefoxOptions(); }
        }

        /// <summary>
        /// Provides a way to customize the Edge options.
        /// </summary>
        protected virtual BaseEdgeOptions EdgeOptions
        {
            get { return new BaseEdgeOptions(); }
        }

        /// <summary>
        /// Provides a way to customize the InternetExplorer configuration
        /// </summary>
        protected virtual BaseSafariOptions SafariOptions
        {
            get { return new BaseSafariOptions(); }
        }

        /// <summary>
        /// Gets the message that is logged when the test begins.  
        /// The default is "BeginTest &gt;&gt; (TestName) - IEDRiverServer: (processcount), chromedriver: (processcount)"
        /// </summary>
        /// <returns></returns>
        protected string TestSetupLogMessage(TestContext context)
        {
            string message = string.Format("BeginTest >> {0} - {1}", context.Test.FullName, GetDriverCountMessage());
            return message;
        }

        /// <summary>
        /// Gets the message that is logged when the test ends.  
        /// The default is "EndTest &lt;&lt; (TestName) - (testStatus) - (elapsedTime) - IEDRiverServer: (processcount), chromedriver: (processcount)"
        /// </summary>
        /// <returns></returns>
        protected string TestTeardownLogMessage(TestContext context, TimeSpan elapsedTime)
        {
            string message = string.Format("EndTest << {0} - {1} - {2} - {4}", context.Test.FullName, context.Result.Outcome, elapsedTime, GetDriverCountMessage());
            return message;
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Gets the CURRENT BrowserMode based on attributes applied to either the Test or the TestFixture.
        /// If no attributes are found, it defaults to TestProperties.BrowserModeDefault.
        /// </summary>
        /// <returns></returns>
        protected BrowserMode GetBrowserMode()
        {
            // The below block of code broke when upgrading from Nunit 2.6.4 to Nunit 3. So for now, we will just return Browsermode.New
            //BrowserMode? mode = GetContextProperty<BrowserMode?>(SeleniumCoreSettings.BrowserModeKey, p => (BrowserMode)Enum.Parse(typeof(BrowserMode), (string)p));
            //if (mode.HasValue)
            //    return mode.Value;

            //return SeleniumCoreSettings.BrowserModeDefault;

            return BrowserMode.New;
        }

        /// <summary>
        /// Gets a property value as applied to EITHER the Test OR the TestFixture.
        /// This allows the developer to apply attributes in EITHER location.  Attributes
        /// applied to a Test will take precedence over attributes applied to a TestFixture.
        /// <![CDATA[http://www.nunit.org/index.php?p=property&r=2.4.8]]>
        /// </summary>
        /// <typeparam name="T">The type of property value.  This is typically string, int, or double, or an enum.</typeparam>
        /// <param name="propertyName">Name of the property to find.</param>
        /// <param name="converter">(Optional): The converter that can convert from the primitive type (since NUnit only recognized string, int, double) into the specified type.  If this property is omitted, the result will simply be casted to type T.</param>
        /// <returns>The property (if specified) or default(T) if not.</returns>
        protected T GetContextProperty<T>(string propertyName, Func<object, T> converter = null)
        {
            // If no converter is specified, attempt to simply cast the property to the specified type
            var convert = converter ?? new Func<object, T>(p => (T)p);

            // First check the CURRENT context to see if the BrowserMode was specified
            if (TestContext.CurrentContext.Test.Properties.ContainsKey(propertyName))
                return convert(TestContext.CurrentContext.Test.Properties[propertyName]);

            // If the property was not found on the current context, next check the FIXTURE CONTEXT.
            if (FixtureContext.Test.Properties.ContainsKey(propertyName))
                return convert(FixtureContext.Test.Properties[propertyName]);

            //// If the property was not found on the fixture context, next check the app.config (or appsettings.json in .NET Core)'s AppSettings
            //if (AppSettings.Config.AllKeys.Contains(propertyName))
            //    return convert(AppSettings.Config[propertyName]);

            // If all else fails, just return the default
            return default(T);
        }

        private void CreateLocalBrowser(TimeSpan commandTimeout)
        {
            SeleniumCoreSettings.DefaultDownloadDirectory = "C:\\seleniumdownloads\\";
            FileUtils.CreateDirectoryIfNotExists(SeleniumCoreSettings.DefaultDownloadDirectory);
            var currentTest = TestContext.CurrentContext;

            if (BrowserName == BrowserNames.Chrome)
            {
                if (string.IsNullOrEmpty(EmulationDevice))
                {
                    var cService = ChromeDriverService.CreateDefaultService(_driverPathChrome);
                    Browser = new ChromeDriver(cService, new BaseChromeOptions(), commandTimeout);
                }
                else // If the tester wants to use an emulation device for the test, then set it here
                {
                    Browser = new ChromeDriver(_driverPathChrome, ChromeOptions, commandTimeout);
                }
            }

            else if (BrowserName == BrowserNames.Edge)
            {
                var cService = EdgeDriverService.CreateDefaultService(_driverPathChrome);
                Browser = new EdgeDriver(_driverPathEdge, EdgeOptions, commandTimeout);
            }

            //else if (BrowserName == BrowserNames.InternetExplorer)
            //{
            //    var service = InternetExplorerDriverService.CreateDefaultService(driverPath);
            //    //service.LogFile = Screenshots_GetFullPathWithFileName(SeleniumCoreSettings.DriverLogsLocation, "IEDriverServer.log");
            //    //service.LoggingLevel = InternetExplorerDriverLogLevel.Trace;

            //    Browser = new InternetExplorerDriver(service, InternetExplorerOptions, commandTimeout);
            //    // Note: This may not be imporant anymore if we're using javascript events (enablenativeevents=false)
            //    // but we'll leave it for now.
            //    ForceIEZoomLevel(Browser);
            //}
            else if (BrowserName == BrowserNames.Firefox)
            {
                var service = FirefoxDriverService.CreateDefaultService(_driverPathFirefox, "geckodriver.exe");
                service.FirefoxBinaryPath = @"C:\Program Files\Mozilla Firefox\firefox.exe";

                // Below is needed or we get a 'No data is available for encoding' 437 error
                // First install NuGet package Add NuGet Package System.Text.Encoding.CodePages, then below 2 lines of code
                // will work
                // Further reading: https://stackoverflow.com/questions/56802715/firefoxwebdriver-no-data-is-available-for-encoding-437
                CodePagesEncodingProvider.Instance.GetEncoding(437);
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                var options = new BaseFirefoxOptions();
                options.Profile = FirefoxProfile;
                Browser = new FirefoxDriver(service, options, commandTimeout);
            }
            else if (BrowserName == BrowserNames.Safari)
            {
                Browser = new SafariDriver();
            }
            else
            {
                throw new InvalidOperationException(string.Format("Browser name of {0} not recognized.", BrowserName));
            }

            LogHelper(currentTest, Level.Info, "Starting local session");
        }

        private void CreateRemoteBrowser(TimeSpan commandTimeout)
        {
            var currentTest = TestContext.CurrentContext;
            string hubName = AppSettings.Config["HubName"];
            SeleniumCoreSettings.DefaultDownloadDirectory = string.Format("\\\\{0}\\seleniumdownloads\\", hubName);
            //DownloadDirectory = SeleniumCoreSettings.DefaultDownloadDirectory_RemoteDownloads;

            // 11/7/19: After the conversion from NET FRAMEWORK to NET CORE, it was required to update Selenium as well. 
            // The new selenium C# bindings made DesiredCapabilities obsolete. We now have to use RemoteSessionSettings 
            // or Options class. See:
            // https://help.crossbrowsertesting.com/faqs/testing/why-do-i-get-the-message-use-of-desiredcapabilities-has-been-deprecated-in-c/
            var RemoteSettings = new RemoteSessionSettings();

            // Two issues solved by adding below code:
            // 1. When we switched from NET Framework to NET Core, we got an error using SendKeys to upload files on the Grid.
            // The error was System.NotSupportedException : No data is available for encoding 437. Per Github community:
            // "Looks like the ZipStorer code needs encoding 437. .NET core removes built-in support for many less common 
            // encodings, including 437. To make the encoding available in .NET core, you need to reference the 
            // System.Text.Encoding System.Text.Encoding.CodePages Nuget package and somewhere before Encoding.GetEncoding(437) 
            // gets called, make sure to call Encoding.RegisterProvider(CodePagesEncodingProvider.Instance)"
            // After following the above, adding System.Text.Encoding.CodePages in NuGet then adding below line of code, it 
            // is working
            // 2. Below is needed or we get a 'No data is available for encoding' 437 error
            // First install NuGet package Add NuGet Package System.Text.Encoding.CodePages, then below 2 lines of code
            // will work
            // Further reading: https://stackoverflow.com/questions/56802715/firefoxwebdriver-no-data-is-available-for-encoding-437
            CodePagesEncodingProvider.Instance.GetEncoding(437);
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            if (BrowserName == BrowserNames.Chrome)
            {
                if (string.IsNullOrEmpty(EmulationDevice))
                {
                    //RemoteSettings.AddFirstMatchDriverOption(new BaseChromeOptions());

                    // Mike Johnston 7/23/2021: Mainpro on Prod was throwing the following error: "The I/O operation has been
                    // aborted because of either a thread exit or an application request." This occurred when clicking the
                    // login button. The application was slow and would take over 120 seconds to login after clicking that
                    // button, so Selenium's default 120 second timeout would be hit and then throw that error. The reason why
                    // the framework had the default 120 second timeout is because we used this line of code...
                    // Browser = new RemoteWebDriver(new Uri(_hubUri), new BaseChromeOptions());
                    // I added the above line of code when refactoring this test class to use Options instead of capabilities
                    // Originally we used capabilities as shown in the below line of code...
                    // Browser = new RemoteWebDriver(new Uri(_hubUri), caps2, commandTimeout);
                    // Notice there is a commandTimeout parameter in that constructor. This was our custom commantTimeout set at
                    // 360 seconds, which overrides Seleniums 120 second timeout. This 360 seconds is needed as shown with this
                    // error in Mainpro above. Now notice that in the line of code we changed to (the line of code 5 lines above
                    // this line), I did not include that custom commandTimeout. This is because there is no overload for this 
                    // constructor that takes in both a Options and a timeout parameter. So today to fix this, I had to add the 
                    // below line of code, which converts the options to Capabilities. Hopefully this works for all tests, 
                    // because I think capabilities is depracated, but not sure if that deprecation affects when you do an 
                    // options.tocapabilities converion. Will monitor going forward
                    //new BaseChromeOptions().SetDefaultDownloadDirectory(DownloadDirectory);
                    Browser = new RemoteWebDriver(new Uri(_hubUri), new BaseChromeOptions().ToCapabilities(), commandTimeout);
                }
                else // If the tester wants to use an emulation device for the test, then set it here
                {
                    // emulation
                    //RemoteSettings.AddFirstMatchDriverOption(ChromeOptions);
                    Browser = new RemoteWebDriver(new Uri(_hubUri), ChromeOptions.ToCapabilities(), commandTimeout);
                }
            }
            else if (BrowserName == BrowserNames.Edge)
            {
                Browser = new RemoteWebDriver(new Uri(_hubUri), EdgeOptions.ToCapabilities(), commandTimeout);
            }
            else if (BrowserName == BrowserNames.InternetExplorer)
            {
                //RemoteSettings.AddFirstMatchDriverOption(InternetExplorerOptions);
                Browser = new RemoteWebDriver(new Uri(_hubUri), new BaseInternetExplorerOptions().ToCapabilities(), commandTimeout);

                // Got infrequent errors on IE where it says "Unexpected error launching Internet Explorer. IELaunchURL() returned
                // HRESULT 80070012 ('There are no more files.')". I am trying the below fix. 
                // See https://stackoverflow.com/questions/46647277/ielaunchurl-returned-hresult-80070012-there-are-no-more-files
                //caps.SetCapability("ie.ensureCleanSession", true);
            }
            else if (BrowserName == BrowserNames.Firefox)
            {
                //RemoteSettings.AddFirstMatchDriverOption(FirefoxOptions);
                //RemoteSettings.AddMetadataSetting("acceptInsecureCerts", true);
                //RemoteSettings.AddMetadataSetting("acceptUntrustedCertificates", true);
                //RemoteSettings.AddMetadataSetting("assumeUntrustedCertificateIssuer", false);
                var options = new BaseFirefoxOptions();
                options.Profile = FirefoxProfile;
                options.BrowserExecutableLocation = @"C:\Program Files\Mozilla Firefox\firefox.exe";

                //Browser = new RemoteWebDriver(new Uri(_hubUri), RemoteSettings, commandTimeout);
                Browser = new RemoteWebDriver(new Uri(_hubUri), options.ToCapabilities(), commandTimeout);
            }
            else if (BrowserName == BrowserNames.Safari)
            {
                //caps = SafariOptions.ToCapabilities() as DesiredCapabilities;
                //RemoteSettings.AddFirstMatchDriverOption(SafariOptions);
                Browser = new RemoteWebDriver(new Uri(_hubUri), new BaseSafariOptions().ToCapabilities(), commandTimeout);
            }
            else
            {
                throw new InvalidOperationException(string.Format("Uknonwn browser name {0}", BrowserName));
            }

            // 04/04/2015 - These go unused.... for now...
            //var buildNumber = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            //RemoteSettings.AddMetadataSetting("build", buildNumber);
            //RemoteSettings.AddMetadataSetting("name", TestContext.CurrentContext.Test.Name);
            //RemoteSettings.AddMetadataSetting(CapabilityType.Version, _version);
            //RemoteSettings.AddMetadataSetting(CapabilityType.Platform, _platform);

            Console.WriteLine(_hubUri);

            //Browser = new RemoteWebDriver(new Uri(_hubUri), RemoteSettings, commandTimeout);
            ((RemoteWebDriver)Browser).FileDetector = new LocalFileDetector();
            LogHelper(currentTest, Level.Info, "Starting remote session");

            /// Below is to allow a tester to upload a local file to a browser instance on the Selenium Grid. This is being 
            /// used in <see cref="FileUtils.UploadFileUsingSendKeys(IWebDriver, IWebElement, string, Page, By, int)"/> 
            /// Further reading: https://stackoverflow.com/questions/62595459/how-to-upload-a-file-by-transfering-the-file-from-the-local-machine-to-the-remot
            var allowsDetection = this.Browser as IAllowsFileDetection;
            if (allowsDetection != null)
            {
                allowsDetection.FileDetector = new LocalFileDetector();
            }

            // Remove (or comment) the below 2 lines after upgrading to Selenium 4, as Selenium 4 has the official fix. Below
            // is just a workaround until then. See https://github.com/SeleniumHQ/selenium/issues/4162. Also remove the classes 
            // I added at the bottom of this file. The classes are called HttpClientCommandExecutor and
            // // SeleniumHttpClientExecutionExtensions
            //  SeleniumHttpClientExecutionExtensions.UseHttpClient(Browser);
            //  Browser.UseHttpClient();
        }

        /// <summary>
        /// Creates and initializes the browser based on the _driverType.
        /// </summary>
        protected void CreateBrowser()
        {
            var commandTimeout = SeleniumCoreSettings.CommandTimeout;

            if (!_isRemote)
            {
                CreateLocalBrowser(commandTimeout);
            }
            else
            {
                CheckAllRequiredConfigPropertiesForRemoteTests();
                CreateRemoteBrowser(commandTimeout);
            }

            // Firefox (in particular) will sometimes throw an ElementNotVisibleException if the desired element is off screen (scrolled out of view).
            // For consistency, I want all tests to run in a maximized window.  If a test needs other behavior, it can override this locally
            Browser.Manage().Window.Maximize();

            // Store the initial url so I can reset the browser back to this url after each test
            // this makes it "safer" to Reuse the browser session by ensuring a consisten state for each test.
            initialUrl = Browser.Url;
        }

        /// <summary>
        /// Disposes the Browser if it still exists.
        /// </summary>
        protected void CleanupBrowser()
        {
            var currentTest = TestContext.CurrentContext;

            // If there is not a current Browser session, then we dont need to close it
            if (Browser == null)
                return;

            try
            {   // Need to add this If statement workaround to close firefox with geckodriver, as geckodriver has a known issue when closing
                // without the below workaround.
                // See https://github.com/mozilla/geckodriver/issues/225 and https://github.com/mozilla/geckodriver/issues/285
                if (BrowserName == BrowserNames.Firefox)
                {
                    Browser.Navigate().GoToUrl("about:config");
                    Browser.Navigate().GoToUrl("about:blank");
                }

                // Dispose and Quit are the same thing.
                // Browser.Close(); //only closes the current tab of a browser window
                Browser.Dispose(); // Closes all browser windows and safely ends the session
                Browser.Quit(); // Calls Dispose()
            }
            catch (Exception ex)
            {
                // Sometimes Browser.Quit/Dispose completes (Doesnt reach this Catch block) but the browser window stays open. So far, this
                // only occurs when the Browser becomes unresponsive. See the method IfBrowserUnresponsiveThenEndTest for more info.
                // Ideally we would want to find a way to close the browser window in this case, but when the Browser/Driver is
                // unresponsive, then nothing seems to work, not even using Javascript. Revisit to find a way to close window when Browser
                // is unresponsive, then put it in the Try block above
                // Browser.ExecuteScript("javascript:window.close('','_parent','');");   
                LogHelper(currentTest, Level.Error, "Failed to close the driver: " + ex);
            }
        }

        /// <summary>
        /// Gets the count of running processes that match the given name.  Do NOT include the file extension
        /// (i.e. specify "explorer" instead of "explorer.exe").
        /// </summary>
        /// <param name="processName">Name of the process.</param>
        /// <returns></returns>
        protected int GetProcessCount(string processName)
        {
            var currentTest = TestContext.CurrentContext;

            try
            {
                var procs = System.Diagnostics.Process.GetProcessesByName(processName);
                return procs.Count();
            }
            catch (Exception ex)
            {
                // _log.DebugFormat("Unable to log process information for {0}: {1}", processName, ex);
                LogHelper(currentTest, Level.Error, string.Format("Unable to log process information for {0}: {1}", processName, ex));

                return -1;
            }
        }
        #endregion

        #region Private Methods  

        /// <summary>
        /// Checking to ensure the tester has added the required properties. If the tester did not add these properties, 
        /// unexpected things can occur, such as the Windows Download prompt appearing after clicking a download link. This 
        /// will fail the test. So we will fail the test before those unexpected things occur, and tell the 
        /// tester to add these properties
        /// </summary>
        private void CheckAllRequiredConfigPropertiesForRemoteTests()
        {
            // Check that all required properties are populated
            if (AppSettings.Config["HubUri"].IsNullOrEmpty() || AppSettings.Config["HubName"].IsNullOrEmpty())
            {
                throw new Exception("Your appsettings.json file is missing required properties. To view the list of " +
                    "required properties, download the Browser.Base.Framework code and open the appsettings.json file " +
                    "from the Wikipedia.UITest project");
            }
            else
            {
                // Check that the tester did not specify an incorrect HubUri
                if (!string.Equals(AppSettings.Config["HubUri"].ToString(), "http://10.91.4.57:8888/wd/hub",
                    StringComparison.CurrentCultureIgnoreCase))
                {
                    if (!string.Equals(AppSettings.Config["HubUri"].ToString(), "http://10.91.16.29:8888/wd/hub",
                        StringComparison.CurrentCultureIgnoreCase))
                    {
                        throw new Exception("The HubUri property within your appsettings.json file is not correct. We have 2 Selenium " +
                            "Grids. Their Uri's are  'http://10.91.4.57:8888/wd/hub' and 'http://10.91.16.29:8888/wd/hub'");
                    }
                }
                // Check that the tester did not specify an incorrect HubName
                if (!string.Equals(AppSettings.Config["HubName"].ToString(), "c3dilmssg01", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (!string.Equals(AppSettings.Config["HubName"].ToString(), "c3dierpdevsel01", StringComparison.CurrentCultureIgnoreCase))
                    {
                        throw new Exception("The HubName property within your appsettings.json file is not correct. We have 2 Selenium " +
                            "Grids. Their Hub names are 'c3dilmssg01' and 'c3dierpdevsel01'");
                    }
                }
                // Check that the tester specified a matching HubUri to HubName. 
                if (string.Equals(AppSettings.Config["HubUri"].ToString(), "http://10.91.4.57:8888/wd/hub",
                    StringComparison.CurrentCultureIgnoreCase))
                {
                    if (!string.Equals(AppSettings.Config["HubName"].ToString(), "c3dilmssg01", StringComparison.CurrentCultureIgnoreCase))
                    {
                        throw new Exception("You specified 'http://10.91.4.57:8888/wd/hub' for the HubUri in your appsettings.json file, " +
                            "but you specified 'c3dierpdevsel01' as the HubName. The HubName for this HubUri is instead 'c3dilmssg01''");
                    }
                }
                if (string.Equals(AppSettings.Config["HubUri"].ToString(), "http://10.91.16.29:8888/wd/hub",
                   StringComparison.CurrentCultureIgnoreCase))
                {
                    if (!string.Equals(AppSettings.Config["HubName"].ToString(), "c3dierpdevsel01", StringComparison.CurrentCultureIgnoreCase))
                    {
                        throw new Exception("You specified 'http://10.91.16.29:8888/wd/hub' for the HubUri in your appsettings.json file, " +
                            "but you specified 'c3dilmssg01' as the HubName. The HubName for this HubUri is instead 'c3dierpdevsel01''");
                    }
                }
            }
        }

        /// <summary>
        /// Automatically downloads the web driver versions that match the PCs browser version, then sets the driver path variable to 
        /// that drivers location. 
        /// </summary>
        private void SetWebDrivers()
        {
            DriverManager driverManager = new DriverManager();

            // If the tester wants to manually download and install the web drivers, do nothing, else install them automatically
            if (AppSettings.Config["ManualDriverInstallationForChrome"].IsNullOrEmpty())
            {
                _driverPathChrome = driverManager.SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser).Replace("chromedriver.exe", "");
            }
            else if (AppSettings.Config["ManualDriverInstallationForChrome"].ToString() == "false")
            {
                _driverPathChrome = driverManager.SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser).Replace("chromedriver.exe", "");
            }

            if (AppSettings.Config["ManualDriverInstallationForEdge"].IsNullOrEmpty())
            {
                _driverPathEdge = driverManager.SetUpDriver(new EdgeConfig(), VersionResolveStrategy.MatchingBrowser).Replace("msedgedriver.exe", "");
            }
            else if (AppSettings.Config["ManualDriverInstallationForEdge"].ToString() == "false")
            {
                _driverPathEdge = driverManager.SetUpDriver(new EdgeConfig(), VersionResolveStrategy.MatchingBrowser).Replace("msedgedriver.exe", "");
            }

            if (AppSettings.Config["ManualDriverInstallationForFirefox"].IsNullOrEmpty())
            {
                _driverPathFirefox = driverManager.SetUpDriver(new FirefoxConfig(), VersionResolveStrategy.Latest).Replace("geckodriver.exe", "");
            }
            else if (AppSettings.Config["ManualDriverInstallationForFirefox"].ToString() == "false")
            {
                _driverPathFirefox = driverManager.SetUpDriver(new FirefoxConfig(), VersionResolveStrategy.Latest).Replace("geckodriver.exe", "");
            }
        }


        /// <summary>
        /// We need to get the relative path of screenshots for Extent reports because if we
        /// instead use the absolute path, the build output Extent reports dont show screenshots. Further reading:
        // https://code.premierinc.com/docs/display/PQA/Issues+Extent+Reports#IssuesExtentReports-ScreenshotsNotViewableAfterRemotePipelineExecutions 
        /// </summary>
        /// <param name="screenshotAbsolutePath"></param>
        /// <returns></returns>
        private string GetScreenShotRelativePath(string screenshotAbsolutePath)
        {
            int pos = screenshotAbsolutePath.LastIndexOf("\\") + 1;
            string screenshotFileName = screenshotAbsolutePath.Substring(pos, screenshotAbsolutePath.Length - pos);
            string screenshotRelativePath = string.Format("ScreenshotsOfFailedTests/{0}", screenshotFileName);

            return screenshotRelativePath;


        }

        /// <summary>
        /// If the Browser/Driver becomes unresponsive during a test, then the test will fail and will hit a timeout exception. The code will 
        /// then reach the AfterTest method within this class file and then will fail to perform any Teardown functions, including closing 
        /// the Browser window (although the driver instance does close). Even Browser.Close/Quit/Dispose fails to close the browser window.
        /// And even if the test-level code completes all steps 
        /// and the test passes, if the test had an alert appear on a tab and the tester did not close that alert then switched to another 
        /// tab and then his test purposefully completes after switching to that tab, the Teardown functions will still fail because 
        /// if an Alert window is open on a hidden tab, Selenium fails to perform any functions on the Browser window, rendering it 
        /// unresponsive (This does not apply to Firefox, as Firefox is still responsive even after switching tabs from an open Alert). 
        /// This method checks to see if the Browser is unresponsive and if so, we will stop the test and inform the tester 
        /// of everything that happened, and will also warn the user to clean up the alerts in their test so there will be no future issues. 
        /// For the majority of the times the Browser has become unresponsive, it was because of hidden Alert windows. See: 
        /// https://code.premierinc.com/docs/display/PQA/Issues+Timeouts+Via+Browser+Base+DefaultCommandTimeout+Parameter#IssuesTimeoutsViaBrowserBaseDefaultCommandTimeoutParameter-Scenario:BrowserUnresponsive
        /// I saw unresponsiveness only one other time when it was not due to an Alert window. See: 
        /// https://code.premierinc.com/docs/display/PQA/Issues+Timeouts+Via+Browser+Base+DefaultCommandTimeout+Parameter#IssuesTimeoutsViaBrowserBaseDefaultCommandTimeoutParameter-Cause:WebPageDidNotLoad/ThrewExceptions
        /// Note: Alert popups do not get displayed on failed test screenshots, even if you are on the tab which is displaying the Alert. 
        /// </summary>
        private void IfBrowserUnresponsiveThenEndTest(TestContext currentTest)
        {
            // Check to see if Browser is responsive. If the line of code in the Try block passes, that means the browser is 
            // responsive and then it will immediately return to AfterTest and tear the test down normally. If the line of
            // code fails, then we will catch the exception and if the exception indicates it hit the timeout
            // (The SeleniumCoreSettings.cs CommandTimeOut or the Node timeout (whichever timeout is set to be less seconds)),
            // then that means the Browser was not responsive, we will stop test and notify tester. Note that Browser.SwitchTo().DefaultContent()
            // switch can still fail in other cases, such as:
            // The test-level code completes but one of the Browsers tabs is being closed at the same time we call
            // Browser.SwitchTo().DefaultContent();. In this case, we get a "no such window: target window already closed" or
            // a "NoSuchWindowException" exception, but in this case, we dont care, the Browser is still responsive and so 
            // we still go back to AfterTest
            try
            {
                Browser.SwitchTo().DefaultContent();
                return;
            }
            catch (Exception ex)
            {
                if (ex.ToString().Contains("TimeoutException") || ex.ToString().Contains("The HTTP request to the remote WebDriver"))
                {
                    ExceptionInTeardown_StopTestLogErrors(currentTest, "Your Browser/Driver instance became " +
                    "unresponsive during the Teardown within Browser Base framework. This will result in a timeout on either " +
                    "the Selenium Grid side (Java.Util.Concurrent.TimeoutException), or a timeout on our built-in Selenium " +
                    "CommandTimeout parameter (The HTTP request to the remote WebDriver...). This will also result in the Browser " +
                    "window not closing after your test. There are a few reasons I have found that makes the Browser/Driver " +
                    "become unresponsive. One of them being that that an Alert window was not closed before switching to another " +
                    "tab. There are other causes as well. See the following link for all causes:" +
                    "https://code.premierinc.com/docs/display/PQA/Issues+Timeouts+Via+Browser+Base+DefaultCommandTimeout+Parameter#IssuesTimeoutsViaBrowserBaseDefaultCommandTimeoutParameter-Scenario:BrowserUnresponsive. " +
                    "If the reason that this Browser became unresponsive is due to one of the reasons at that link, then " +
                    "please fix your code so that it does not result in an unresponsive Browser. If you have found a new cause " +
                    "of why your Browser/Driver instance became unresponsive, please contact Mike " +
                    "Johnston to inform me of the new cause so I can document it, then fix your code. The exact exception " +
                    "that was thrown within the Teardown is: " + ex.ToString());
                }
            }
        }

        /// <summary>
        /// Checks to see if the driver instance was started. If not started, log errors and end test. If the driver instance
        /// failed to start and we did not log errors now, then the AfterTest() method would throw a generic exception 
        /// "object reference not set to an instance of an object" whenever a line of code tries to access the driver
        /// instance which never started. 
        /// </summary>
        /// <param name="testLevelTestResultMessage"></param>
        private void IfDriverInstanceFailedToStartThenEndTest(TestContext currentTest)
        {
            // If the test passed then this variable will be null. If failed then this will populate test level exception.
            // This can also contain a custom message from the QA Engineer when using Assert.Warning/Ignore/Etc.
            string testLevelTestResultMessage = currentTest.Result.Message;

            // If the test passed, this variable will be null. If we call the Contains function on a string thats null,
            // it throws "System.NullReferenceException: Object reference not set to an instance of an object"
            // So first we have to check if string is not null, then do the Contains functions. This also helps with performance,
            // as the code will skip the Contains functions when tests pass
            if (!testLevelTestResultMessage.IsNullOrEmpty())
            {
                // The driver instance can fail to start because of many things.For example,
                // the driver executable was not in the path specified, or the driver version did not match the browser version, etc.
                if (Browser == null)
                {
                    // Firefox is not installed
                    if (testLevelTestResultMessage.Contains("binary is not a Firefox executable"))
                    {
                        ExceptionInTeardown_StopTestLogErrors(currentTest, "The web driver instance failed to start because Firefox was not " +
                            "installed");
                    }

                    // Chrome is not installed
                    if (testLevelTestResultMessage.Contains("binary is not a Chrome executable"))
                    {
                        ExceptionInTeardown_StopTestLogErrors(currentTest, "The web driver instance failed to start because Chrome was not " +
                            "installed");
                    }

                    // Edge is not installed
                    if (testLevelTestResultMessage.Contains("binary is not a Edge executable"))
                    {
                        ExceptionInTeardown_StopTestLogErrors(currentTest, "The web driver instance failed to start because Edge was not " +
                            "installed");
                    }

                    // Local executions
                    if (!_isRemote)
                    {
                        // Web driver file is missing or code is not pointing to the correct place
                        if (testLevelTestResultMessage.Contains(".exe does not exist. The driver can be downloaded at"))
                        {
                            ExceptionInTeardown_StopTestLogErrors(currentTest, "The web driver instance failed to start because Selenium did not find " +
                                "the web driver.exe file within the path specified. A Browser window should not have opened in this case, so there is " +
                                "nothing to close or Tear Down. If you do see a Browser window open, contact Mike Johnston, as I have to update this " +
                                "code, as I may have misinterpreted the exception");
                        }

                        // Timeout is hit locally
                        if (testLevelTestResultMessage.Contains("The HTTP request to the remote WebDriver server for URL"))
                        {
                            ExceptionInTeardown_StopTestLogErrors(currentTest, "The web driver instance failed to get assigned to the 'Browser' " +
                                "property and/or failed to start at all, within the timeout period. This occurs sometimes randomly or when you " +
                                "are executing too many tests in parallel locally and your CPU gets maxed out. The actual Browser window may " +
                                "have opened and the driver may have eventually started (shown in your Task Manager within processes), but the " +
                                "timeout was hit before the driver was able to successfully send requests to the Browser and so the test failed. " +
                                "You may see a blank open Browser window that failed to maximize " +
                                "and/or you may also see a driver.exe process within the nodes Task Manager. There is currently no way inside " +
                                "the code to Tear Down (close) the Browser window or terminate driver process, because the code did not get " +
                                "to assign the driver instance to the 'Browser' property, so you will have to manually close the Browser " +
                                "and webdriver.exe process in the nodes Task Manager. Note that if you dont manually close these processes, " +
                                "your PCs CPU may fill up and will slow down");
                        }
                    }

                    // Grid executions
                    if (_isRemote)
                    {
                        // Timeout is hit on the Grid
                        if (testLevelTestResultMessage.Contains("The HTTP request to the remote WebDriver server for URL") ||
                            testLevelTestResultMessage.Contains("java.util.concurrent.TimeoutException"))
                        {
                            ExceptionInTeardown_StopTestLogErrors(currentTest, "The web driver instance failed to get assigned to the 'Browser' " +
                                "property and/or failed to start at all, within the timeout period. This is due to one of 2 reasons: " +
                                "1. The Grid Nodes were down. In this case, the Browser window would not open so there is nothing to Tear Down. " +
                                "2. If the Grid Nodes were up, then the actual Browser window may have opened and the driver may have eventually" +
                                "started (shown in your Task Manager within processes), but the timeout was hit before the driver was able " +
                                "to successfully send requests to the Browser and so the test failed." +
                                "and so the test failed. You may see a blank open Browser window that failed to maximize on the node. " +
                                "and/or you may also see a driver.exe process within the nodes Task Manager. There is currently no way inside the code " +
                                "to Tear Down (close) the Browser window or terminate driver process, because the code did not get to assign " +
                                "the driver instance to the 'Browser' property, so you will have to manually close the Browser and webdriver.exe " +
                                "process in the nodes Task Manager. Note that if you dont manually close these processes, the nodes CPU may fill " +
                                "up and the Node will slow down. However, you can also just wait for the Grid's Task Scheduler task to " +
                                "terminate all browsers and driver.exe processes. That is scheduled nightly");
                        }

                        // Grid Hub is down
                        if (testLevelTestResultMessage.Contains("An unknown exception was encountered sending an HTTP request to the remote WebDriver"))
                        {
                            ExceptionInTeardown_StopTestLogErrors(currentTest, "The web driver instance failed to start because the Grid Hub was down. " +
                                "In this case, the Browser wndow does not open, so there is nothing to close or Tear Down");
                        }

                        // Either Nodes are filled up, not accepting new browser sessions. Or Node has been shut down but has yet to 
                        // been unregistered from the Hub
                        if (testLevelTestResultMessage.Contains("Could not start a new session. java.net.ConnectException: Connection refused"))
                        {
                            ExceptionInTeardown_StopTestLogErrors(currentTest, "The web driver instance failed to start because the Node(s) rejected " +
                                "a new session. This can be due to multiple reasons: 1) All available nodes were shut down, the Hub was not shut down, then " +
                                "you executed a test directly after. The Hub needs time (about 3 mins) to unregister a node after the node is shut down, " +
                                "so if you execute " +
                                "a test within this time period, the Hub will send out a request to the node but the node is down. To solve this, you must " +
                                "start the nodes again 2) The Node's queue being full and not accepting any more sessions. To solve this the second, " +
                                "you will have to either 1. Wait until the Node's queue gets freed up. 2. There may be stale sessions in " +
                                "the queue and if so, you will have to force terminate those driver.exe processes within Task Manager. A Browser " +
                                "window should not have opened in this case, so there is nothing to close or Tear Down. If you do see a Browser " +
                                "window open and stay open after the test, contact Mike Johnston, as I have to update this code, " +
                                "as I may have misinterpreted the exception");
                        }
                    }

                    // Miscellaneous unsorted issues. I still need to differentiate these, but will just lump them together for now
                    if (testLevelTestResultMessage.Contains("Error while creating session with the driver service") ||
                        testLevelTestResultMessage.Contains("Cannot start the driver service"))
                    {
                        ExceptionInTeardown_StopTestLogErrors(currentTest, "The web driver instance failed to start. The reason is unknown at this " +
                            "point until the PQA team does further investigation. We dont believe a Browser window should not have opened in " +
                            "this case, so there is nothing to close or Tear Down. Please contact Mike Johnston when you receive this error and " +
                            "inform him of what caused this error, then I will update this message");
                    }

                    // The below line is just to catch any unknown testLevelTestResultMessage messages when Browser = null. If I dont include
                    // the below line, and a testLevelTestResultMessage message occurs that is not accounted for (all the ones in the above code),
                    // then the given test is not output to the Extent report. So if this line of code is ever hit, get that testLevelTestResultMessage
                    // then place it within an If statement above
                    ExceptionInTeardown_StopTestLogErrors(currentTest, "The web driver instance failed to start. The reason is unknown at this " +
                            "point until the PQA team does further investigation. We dont believe a Browser window should not have opened in " +
                            "this case, so there is nothing to close or Tear Down. Please contact Mike Johnston when you receive this error and " +
                            "inform him of what caused this error, then I will update this message");
                }
                // Sometimes the driver does start (so it did get assigned to the Browser property and would not be null) then dies
                else
                {
                    // Not sure yet why exactly this happens
                    if (testLevelTestResultMessage.Contains("Stopping driver service: Driver server process died prematurely."))
                    {
                        ExceptionInTeardown_StopTestLogErrors(currentTest, "The web driver instance started then it died. The reason is unknown " +
                            "at this point until the PQA team does further investigation. We dont believe a Browser window should not have opened in " +
                            "this case, so there is nothing to close or Tear Down. Please contact Mike Johnston when you receive this error and " +
                            "inform him of what caused this error, then I will update this message");
                    }
                }
            }
        }

        /// <summary>
        /// Attach the screenshot to the NUnit TestContext instance. This will make it appear in the test output of the 
        /// Visual Studio Test Detail Summary window, as well as within the build results Attachments tab
        /// </summary>
        /// <param name="screenshotAbsolutePath">>The screenshot file's absolute directory with file name appended</param>
        /// <param name="testLevelTestResultMessage"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void AddTestAttachmentToNunitTestContextInstance(string screenshotAbsolutePath, TestContext currentTest)
        {
            // We are using a Try Catch here because:
            // https://code.premierinc.com/docs/display/PQA/Issues+Extent+Reports#IssuesExtentReports-ReportFailsToGenerate
            try
            {
                TestContext.AddTestAttachment(screenshotAbsolutePath, "screenshot");
            }
            catch (Exception e)
            {
                ExceptionInTeardown_StopTestLogErrors(currentTest,
                    "See Browser Base AfterTest method titled AddTestAttachmentToNunitTestContextInstance. Most likely the " +
                    "screenshot did not save properly, so the path of the screenshot does not exist and Extent failed to attach it. " +
                    "The specific NUnit error is: " + e.Message);
            }
        }

        /// <summary>
        /// If any exception is thrown after a test during the AfterTest() method, the entire execution stops and so the Extent Report code in 
        /// that method will never be reached, resulting in the extent report never being populated for that test. so if there is potential 
        /// for an exception in any line of code within AfterTest, be sure to use a Try Catch block for that line of code, then inside
        /// the Catch block, we call this method which will print the test level error messages to extent, if applicable, as well 
        /// print custom error messages telling the tester what happened inside AfterTest that caused an exception. Further reading: 
        /// https://code.premierinc.com/docs/display/PQA/Issues+Extent+Reports#IssuesExtentReports-ReportFailsToGenerate
        /// </summary>
        /// <param name="testLevelTestResultMessage">The test level test result message which is populated within the AfterTest method.
        /// This message will be printed to the Extent Report as well as to the Message within Test Summary Detail window 
        /// of VS. If the test passes, this will be null and not print at all</param>
        /// <param name="customAfterTestErrorMessage">A custom error message explaining why the line of code inside the AfterTest method
        /// has failed. This will be printed to the Extent Report before testLevelErrorMessage (If the test failed), but if the test 
        /// passed, this will be printed to Extent Report on its own without testLevelErrorMessage</param>
        private void ExceptionInTeardown_StopTestLogErrors(TestContext currentTest, string customAfterTestErrorMessage)
        {
            string testLevelTestResultMessage = currentTest.Result.Message; // This will be null if the test passes. 
            string stackTrace = currentTest.Result.StackTrace;
            var stacktrace = string.IsNullOrEmpty(stackTrace) ? "" : stackTrace;

            // If the test level code produced an error, populate this variable then send it to the TESTSTEP line of code below
            // If it didnt populate an error, that means the test did not fail, and so we will leave this blank
            testLevelTestResultMessage = testLevelTestResultMessage.IsNullOrEmpty() ? "The test-level did not produce a message. This means " +
                "your test-level code has passed." : "Your test-level result message is:" + testLevelTestResultMessage;

            // Print to the Extent report
            var logMessage = string.Format("<pre>Your test execution failed during the Teardown phase. See the following error message " +
                "for details of what happened during the Teardown: {0}</pre> " +
                "<pre>Your test-level code may or may not have passed. See the following test-level result message to determine this: {1}</pre> " +
                "<pre>Stack Trace: {2}</pre> ",
                customAfterTestErrorMessage,
                testLevelTestResultMessage,
                stacktrace);

            TESTSTEP.Log(Status.Fail, logMessage);

            // Flush the Extent report
            try { ExtentManager.Instance.Flush(); }
            catch { } // See comments at the bottom of AfterTest() method on why we use try catch here

            // Print the custom teardown message to the Test Summary Detail window in VS
            throw new Exception(customAfterTestErrorMessage);
        }

        public void LogHelper(TestContext context, log4net.Core.Level level, string message)
        {
            // Uncomment this and see log4net.config to see how I can print sessionID in the config file instead
            //log4net.GlobalContext.Properties["sessionID"] = GetSessionId();

            string testName = context.Test.FullName;

            if (level == Level.Info)
            {
                _log.Info(string.Format(" {0} {1} {2}", GetSessionId(), testName, message));
            }
            if (level == Level.Error)
            {
                _log.Error(string.Format("{0} {1} {2}", GetSessionId(), testName, message));
            }
            if (level == Level.Warn)
            {
                _log.Warn(string.Format("{0} {1} {2}", GetSessionId(), testName, message));
            }
        }

        private string GetSessionId()
        {
            return ((IHasSessionId)Browser).SessionId.ToString();
        }

        /// <summary>
        /// If I lost communication with the RemoteWebDriver, I may not be able to get the url.
        /// I don't want to interfere with properly closing the driver, so I wrap it in a try/catch
        /// </summary>
        /// <returns></returns>
        private string GetUrlOrDefault()
        {
            try
            {
                return Browser.Url;
            }
            catch (Exception ex)
            {
                return string.Format("Unable to get url: {0}", ex);
            }
        }

        /// <summary>
        /// Forces the zoom level to be 100%.  This is important so that clicks and locations are reported
        /// accurately.
        /// </summary>
        /// <param name="Driver"></param>
        private void ForceIEZoomLevel(IWebDriver Driver)
        {
            var body = Driver.WaitForElement(By.TagName("html"));
            // Ctrl+0 is the IE shortcut to reset the zoom level to 100%
            body.SendKeys(Keys.Control + "0");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GetDriverCountMessage()
        {
            var browsers = new[] { "IEDriverServer", "chromedriver", "geckodriver", "msedgedriver" };
            List<string> values = new List<string>();
            foreach (var i in browsers)
            {
                values.Add(string.Format("{0}: {1}", i, GetProcessCount(i)));
            }

            return string.Join(", ", values);
        }

        #region Print Screen

        /// <summary>
        /// Takes a screenshot of the Browser's viewport (the viewable area, not the entire scrollable page). The name 
        /// of the screenshot will be Test ClassName>TestMethodName>BrowserName>DateTime.PNG. The path to the screenshot will 
        /// be in the TestOutput folder of the Solution directory
        /// </summary>
        /// <param name="currentTest"></param>
        /// <returns>The screenshot file's absolute directory with file name appended</returns>
        private string TakeFailedTestScreenshot(TestContext.TestAdapter currentTest)
        {
            var absolutePath = "";

            var tsc = Browser as ITakesScreenshot;

            if (tsc != null)
            {
                // This needs to be in a Try block so we can log what happened. Also because of this:
                // https://code.premierinc.com/docs/display/PQA/Issues+Extent+Reports#IssuesExtentReports-ReportFailsToGenerate
                try
                {
                    var screenshotFileName = GetScreenshotFileName(currentTest);
                    absolutePath = FileUtils.GetFolderPathPlusFileName_Screenshots_FailedTest(screenshotFileName);

                    FileUtils.CreateDirectoryIfNotExists(absolutePath);
                    var ss = tsc.GetScreenshot();
                    ss.SaveAsFile(absolutePath, ScreenshotImageFormat.Png);
                    LogHelper(TestContext.CurrentContext, Level.Info, string.Format("Browser saved the Screenshot: {0}", absolutePath));

                }
                catch (Exception ex)
                {
                    LogHelper(TestContext.CurrentContext, Level.Error, string.Format("Failed to save screenshot: {0}", ex));
                    return "Could not get absolute path because it failed to save screenshot";
                }
            }
            else
            {
                _log.Error("Browser (as ITakesScreenshot) could not take the Screenshot ()");
                LogHelper(TestContext.CurrentContext, Level.Error, "Browser (as ITakesScreenshot) could not take the Screenshot ()");
                return "Could not get absolute path because it failed to save screenshot";
            }

            return absolutePath;
        }

        /// <summary>
        /// Builds then returns the file name of a screenshot, formatted as ClassName>TestMethodName>BrowserName>DateTime.PNG
        /// </summary>
        /// <param name="test"></param>
        /// <returns></returns>
        private string GetScreenshotFileName(TestContext.TestAdapter test)
        {
            var filename = SeleniumCoreSettings.ScreenShotNameFormat;

            // Not currently using these. See SeleniumCoreSettings.ScreenShotNameFormat property to determine what we are using
            //var driverName = Browser.GetType().Name;
            //var fullDriverName = Browser.GetType().FullName;
            //var fullClassName = this.GetType().FullName;
            //var sessionID = GetSessionId();
            //var fullTestNameWithDriver = test.FullName;
            //filename = filename.Replace("{driverName}", driverName);
            //filename = filename.Replace("{fullDriverName}", fullDriverName);
            //filename = filename.Replace("{fullClassName}", fullClassName);
            //filename = filename.Replace("{sessionId}", sessionID);
            //filename = filename.Replace("{fullTestNameWithDriver}", fullTestNameWithDriver);

            // We are using these when saving the screenshot
            var className = this.GetType().Name;
            filename = filename.Replace("{className}", className);
            filename = filename.Replace("{testName}", test.Name);
            // If this isnt an emulation test, then the Emulation property will be null, so the screenshot filename will only have "emulation" 
            // in it's title if this test was run through emulation
            filename = filename.Replace("{browserName}", BrowserName + EmulationDevice);

            filename = string.Concat(filename, "_(", DateTime.Now.ToString("yyyy-MM-dd_HH.mm.ss",
                CultureInfo.InvariantCulture), ").png").Replace(" ", string.Empty);

            return filename;
        }


        #endregion Print Screen



        #endregion



        #region workarounds


        private static ConcurrentDictionary<IWebDriver, ICommandExecutor> _commandExecutors = new ConcurrentDictionary<IWebDriver, ICommandExecutor>();

        /// <summary>
        /// Gets the Browser logs. We used to use the following: Browser.Manage().Logs.GetLog(LogType.Browser) to get logs, but 
        /// a bug prevented logs from returning when executing on the Selenium Grid using that method. So this method was 
        /// created as a workaround. The bug details can be seen at https://github.com/SeleniumHQ/selenium/issues/8229. 
        /// Remove this method and uncomment Browser.Manage().Logs.GetLog(LogType.Browser) when this bug is fixed
        /// As of 2022-08-01 testing on Selenium WebDriver version 4.3.0, this has not been fixed yet
        /// </summary>
        /// <param name="webDriver"></param>
        /// <returns></returns>
        private static List<LogEntry> GetLogs(IWebDriver webDriver)
        {
            Response response = null;
            List<LogEntry> logEntries = null;

            // Workaround for bug https://github.com/SeleniumHQ/selenium/issues/8229 .
            if (!_commandExecutors.TryGetValue(webDriver, out var commandExecutor))
            {
                commandExecutor = (ICommandExecutor)typeof(RemoteWebDriver)
                    .GetProperty("CommandExecutor", BindingFlags.Public | BindingFlags.Instance)
                    .GetValue(webDriver);

                commandExecutor.TryAddCommand(DriverCommand.GetAvailableLogTypes, new HttpCommandInfo(HttpCommandInfo.GetCommand, "/session/{sessionId}/se/log/types"));
                commandExecutor.TryAddCommand(DriverCommand.GetLog, new HttpCommandInfo(HttpCommandInfo.PostCommand, "/session/{sessionId}/se/log"));

                _commandExecutors[webDriver] = commandExecutor;
            }

            // Sometimes this throws an exception and causes issues. This does not occur frequently, so we will just print an 
            // empty log if this ever happens. Ideally, we want to figure out the cause and fix it and/or print a more informative
            // message instead of printing null in the log, but I dont have time right now to figure out how to populate a custom 
            // value for logEntries. Further reading:
            // https://code.premierinc.com/docs/display/PQA/Issues+General#IssuesGeneral-GetLogsMethodWithinBrowserTestThrowsTeardownException
            // This Try Catch is always needed. See:
            // https://code.premierinc.com/docs/display/PQA/Issues+Extent+Reports#IssuesExtentReports-ReportFailsToGenerate
            try
            {
                response = commandExecutor.Execute(new Command(((IHasSessionId)webDriver).SessionId, DriverCommand.GetLog, new Dictionary<string, object> { ["type"] = "browser" }));
            }
            catch
            {

            }

            var fromDictionaryMethod = typeof(LogEntry).GetMethod("FromDictionary", BindingFlags.Static | BindingFlags.NonPublic);

            logEntries = ((IEnumerable<object>)response.Value)
                .Select(v => fromDictionaryMethod.Invoke(null, new[] { v }))
                .OfType<LogEntry>()
                .ToList();

            return logEntries;
        }


        #endregion workarounds

    }


































    //// Remove (or comment) the below classes after upgrading to Selenium 4, as Selenium 4 has the official fix. Below is just
    //// a workaround until then. See https://github.com/SeleniumHQ/selenium/issues/4162. 
    //public static class SeleniumHttpClientExecutionExtensions
    //{
    //    public static IWebDriver UseHttpClient(this IWebDriver driver)
    //    {
    //        var executor = driver.GetFieldValue<ICommandExecutor>("executor");

    //        if (executor is DriverServiceCommandExecutor driverServiceExecutor)
    //        {
    //            driverServiceExecutor.SetFieldValue("internalExecutor",
    //                driverServiceExecutor.HttpExecutor.ToHttpClientExecutor());
    //        }
    //        else if (executor is HttpCommandExecutor httpCommandExecutor)
    //        {
    //            driver.SetFieldValue("executor", httpCommandExecutor.ToHttpClientExecutor());
    //        }
    //        else
    //        {
    //            throw new ApplicationException("Could not find the http command executor");
    //        }

    //        return driver;
    //    }


    //}

    //internal class HttpClientCommandExecutor : HttpCommandExecutor
    //{
    //    private const string Utf8CharsetType = "utf-8";
    //    private const string JsonMimeType = "application/json";

    //    private Uri RemoteServerUri { get; }
    //    private TimeSpan ServerResponseTimeout { get; }
    //    private Lazy<HttpClient> HttpClient { get; }


    //    public HttpClientCommandExecutor(Uri addressOfRemoteServer, TimeSpan timeout, bool enableKeepAlive) : base(addressOfRemoteServer, timeout, enableKeepAlive)
    //    {
    //        RemoteServerUri = this.GetFieldValue<Uri>("remoteServerUri");
    //        ServerResponseTimeout = this.GetFieldValue<TimeSpan>("serverResponseTimeout");
    //        this.HttpClient = new Lazy<HttpClient>(this.CreateClient);
    //    }

    //    public override Response Execute(Command commandToExecute)
    //    {
    //        if (commandToExecute == null)
    //            throw new ArgumentNullException(nameof(commandToExecute), "commandToExecute cannot be null");
    //        CommandInfo commandInfo = this.CommandInfoRepository.GetCommandInfo(commandToExecute.Name);
    //        Response response = this.CreateResponse(this.MakeHttpRequest(new HttpRequestInfo(RemoteServerUri, commandToExecute, commandInfo)).GetAwaiter().GetResult());
    //        if (commandToExecute.Name == DriverCommand.NewSession && response.IsSpecificationCompliant)
    //            this.SetFieldValue<CommandInfoRepository>("commandInfoRepository", new W3CWireProtocolCommandInfoRepository());
    //        return response;
    //    }


    //    private HttpClient CreateClient()
    //    {
    //        var httpClientHandler = new HttpClientHandler();
    //        var client = new HttpClient(httpClientHandler);
    //        var requestHeaders = client.DefaultRequestHeaders;

    //        var userInfo = this.RemoteServerUri.UserInfo;
    //        if (!string.IsNullOrEmpty(userInfo) && userInfo.Contains(":"))
    //        {
    //            string[] userInfoComponents = userInfo.Split(new[] { ':' }, 2);
    //            httpClientHandler.Credentials = new NetworkCredential(userInfoComponents[0], userInfoComponents[1]);
    //            httpClientHandler.PreAuthenticate = true;
    //        }

    //        var str = string.Format(CultureInfo.InvariantCulture,
    //            "selenium/{0} (.net {1})", ResourceUtilities.AssemblyVersion,
    //            ResourceUtilities.PlatformFamily);

    //        requestHeaders.UserAgent.ParseAdd(str);
    //        client.Timeout = this.ServerResponseTimeout;

    //        requestHeaders.Accept.ParseAdd("application/json, image/png");
    //        requestHeaders.Connection.ParseAdd(this.IsKeepAliveEnabled ? "keep-alive" : "close");

    //        httpClientHandler.Proxy = this.Proxy;
    //        httpClientHandler.MaxConnectionsPerServer = 2000;

    //        return client;
    //    }

    //    private async Task<HttpResponseInfo> MakeHttpRequest(HttpRequestInfo requestInfo)
    //    {
    //        var eventArgs = new SendingRemoteHttpRequestEventArgs(null, requestInfo.RequestBody);
    //        this.OnSendingRemoteHttpRequest(eventArgs);

    //        var requestMessage = new HttpRequestMessage
    //        {
    //            Method = new HttpMethod(requestInfo.HttpMethod),
    //            RequestUri = requestInfo.FullUri,
    //        };

    //        if (requestInfo.HttpMethod == CommandInfo.GetCommand)
    //        {
    //            var cacheControlHeader = new CacheControlHeaderValue();
    //            cacheControlHeader.NoCache = true;
    //            requestMessage.Headers.CacheControl = cacheControlHeader;
    //        }
    //        else if (requestInfo.HttpMethod == CommandInfo.PostCommand)
    //        {
    //            var acceptHeader = new MediaTypeWithQualityHeaderValue(JsonMimeType);
    //            acceptHeader.CharSet = Utf8CharsetType;
    //            requestMessage.Headers.Accept.Add(acceptHeader);

    //            byte[] bytes = Encoding.UTF8.GetBytes(eventArgs.RequestBody);
    //            requestMessage.Content = new ByteArrayContent(bytes, 0, bytes.Length);
    //        }

    //        var response = await this.HttpClient.Value.SendAsync(requestMessage);

    //        if (response.StatusCode == HttpStatusCode.RequestTimeout)
    //        {
    //            throw new WebDriverException(string.Format(CultureInfo.InvariantCulture, "The HTTP request to the remote WebDriver server for URL {0} timed out after {1} seconds.", requestInfo.FullUri, this.ServerResponseTimeout.TotalSeconds));
    //        }

    //        if (response.Content == null)
    //        {
    //            throw new WebDriverException(string.Format(CultureInfo.InvariantCulture, "A exception with a null response was thrown sending an HTTP request to the remote WebDriver server for URL {0}. The status of the exception was {1}", requestInfo.FullUri, response.StatusCode));
    //        }

    //        var httpResponseInfo = new HttpResponseInfo
    //        {
    //            Body = Encoding.UTF8.GetString(await response.Content.ReadAsByteArrayAsync()),
    //            ContentType = response.Content.Headers.ContentType.ToString(),
    //            StatusCode = response.StatusCode
    //        };
    //        return httpResponseInfo;
    //    }

    //    private Response CreateResponse(HttpResponseInfo stuff)
    //    {
    //        var response = new Response();
    //        string body = stuff.Body;
    //        if (stuff.ContentType != null && stuff.ContentType.StartsWith("application/json", StringComparison.OrdinalIgnoreCase))
    //            response = Response.FromJson(body);
    //        else
    //            response.Value = body;
    //        if (this.CommandInfoRepository.SpecificationLevel < 1 && (stuff.StatusCode < HttpStatusCode.OK || stuff.StatusCode >= HttpStatusCode.BadRequest))
    //        {
    //            if (stuff.StatusCode >= HttpStatusCode.BadRequest && stuff.StatusCode < HttpStatusCode.InternalServerError)
    //                response.Status = WebDriverResult.UnhandledError;
    //            else if (stuff.StatusCode >= HttpStatusCode.InternalServerError)
    //            {
    //                if (stuff.StatusCode == HttpStatusCode.NotImplemented)
    //                    response.Status = WebDriverResult.UnknownCommand;
    //                else if (response.Status == WebDriverResult.Success)
    //                    response.Status = WebDriverResult.UnhandledError;
    //            }
    //            else
    //                response.Status = WebDriverResult.UnhandledError;
    //        }
    //        if (response.Value is string)
    //            response.Value = ((string)response.Value).Replace("\r\n", "\n").Replace("\n", Environment.NewLine);
    //        return response;
    //    }

    //    private class HttpRequestInfo
    //    {
    //        public HttpRequestInfo(Uri serverUri, Command commandToExecute, CommandInfo commandInfo)
    //        {
    //            this.FullUri = commandInfo.CreateCommandUri(serverUri, commandToExecute);
    //            this.HttpMethod = commandInfo.Method;
    //            this.RequestBody = commandToExecute.ParametersAsJsonString;
    //        }

    //        public Uri FullUri { get; set; }

    //        public string HttpMethod { get; set; }

    //        public string RequestBody { get; set; }
    //    }

    //    private class HttpResponseInfo
    //    {
    //        public HttpStatusCode StatusCode { get; set; }

    //        public string Body { get; set; }

    //        public string ContentType { get; set; }
    //    }

    //}

    //internal static class InternalExtensions
    //{
    //    private static IEnumerable<Type> BaseTypes(this Type type)
    //    {
    //        while (type.BaseType != null)
    //        {
    //            yield return type.BaseType;
    //            type = type.BaseType;
    //        }
    //    }
    //    internal static FieldInfo GetFieldInfo(this Object value, String memberName, BindingFlags? bindingFlags = null)
    //    {
    //        bindingFlags = bindingFlags ?? BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance;
    //        var type = value.GetType();
    //        return new[] { type }.Concat(type.BaseTypes()).Select(t => t.GetField(memberName, bindingFlags.Value)).First(
    //            f => f != null);
    //    }

    //    internal static TValue GetFieldValue<TValue>(this Object instance, string fieldName)
    //    {
    //        if (instance == null) throw new ArgumentNullException(nameof(instance));
    //        return (TValue)(instance.GetFieldInfo(fieldName)).GetValue(instance);
    //    }
    //    internal static void SetFieldValue<TValue>(this Object instance, string fieldName, TValue value)
    //    {
    //        (instance.GetFieldInfo(fieldName)).SetValue(instance, value);
    //    }
    //    internal static HttpClientCommandExecutor ToHttpClientExecutor(this HttpCommandExecutor httpExecutor)
    //    {
    //        var result = new HttpClientCommandExecutor(
    //            httpExecutor.GetFieldValue<Uri>("remoteServerUri"),
    //            httpExecutor.GetFieldValue<TimeSpan>("serverResponseTimeout"),
    //            httpExecutor.IsKeepAliveEnabled);
    //        result.SetFieldValue("commandInfoRepository", httpExecutor.CommandInfoRepository);
    //        return result;

    //    }

    //}
}

