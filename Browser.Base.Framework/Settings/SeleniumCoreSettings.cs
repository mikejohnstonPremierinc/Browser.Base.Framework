using System;
using System.IO;

namespace Browser.Core.Framework
{    
    /// <summary>
    /// Well-known settings for Selenium.Core.  These values are "keys" for retreiving the actual value
    /// 
    /// Typically, these are specified in the app.config (or appsettings.json in .NET Core) file, but some may also be accessible via an attribute
    /// that can be applied to a test or test class.
    /// </summary>
    public static class SeleniumCoreSettings
    {
        #region Driver

        /// <summary>
        /// Specifies the location of the web driver executables.
        /// </summary>        
        public static readonly string DriverLocationKey = "DriverLocation";
        /// <summary>
        /// The default driver location is .\Drivers
        /// </summary>
        public static readonly string DriverLocationDefault = @"c:\seleniumdrivers";
        /// <summary>
        /// Gets the location of the selenium drivers.  This property is guaranteed to have a trailing slash.
        /// </summary>        
        public static string DriverLocation { get; private set; }
        
        /// <summary>
        /// Specifies the location where driver logs will be saved.
        /// </summary>        
        public static readonly string DriverLogsLocationKey = "DriverLogsLocation";
        /// <summary>
        /// The default driver logs location is .\blah
        /// </summary>
        public static readonly string DriverLogsLocationDefault = @".\blah";
        /// <summary>
        /// Gets the driver logs location.  This property is guaranteed to have a trailing slash.
        /// </summary>        
        public static string DriverLogsLocation { get; private set; }

        #endregion Driver

        #region Mode

        /// <summary>
        /// Identifies whether to create a new browser instance or reuse an existing instance.  The default is Reuse.
        /// </summary>        
        public static readonly string BrowserModeKey = "BrowserMode";
        /// <summary>
        /// The default BrowserMode is BrowserMode.Reuse because it saves several seconds per test to avoid shutting down
        /// and re-initializing the browser.
        /// </summary>
        public static readonly BrowserMode BrowserModeDefault = Browser.Core.Framework.BrowserMode.New;
        /// <summary>
        /// Gets the browser mode to be used if not overridden at the Class or Method level by the BrowserModeAttribute.
        /// </summary>        
        public static BrowserMode BrowserMode { get; private set; }

        #endregion Mode

        #region Download
    
        ///// The default download directory that all local downloads will be saved to.
        ///// </summary>
        //public static string DefaultDownloadDirectory_LocalDownloads { get; private set; }

        ///// <summary>
        ///// The default download directory that all remote downloads will be saved to.
        ///// </summary>
        //public static string DefaultDownloadDirectory_RemoteDownloads { get; private set; }

        // because I did not make this getter "private set", this may cause race conditions if a tester executes both a local and a remote test 
        // within the same build instance. This is not a likely scenario at all and will probably never happen, but if a tester does this and 
        // continues to need to do this, then I may have to implement the above, separate download directories for local and remote, then will 
        // have to update BrowserTest, etc. See at the bottom of this class file for how I have local and remote hardcoded
        // Update: Download directory is now being conditioned within BrowserTest instead of hardcoded below
        /// <summary>
        /// The default download directory that all downloads will be saved to.
        /// </summary>
        public static string DefaultDownloadDirectory { get; set; }

        #endregion Download

        #region Grid

        /// <summary>
        /// Specifies the location of the Selenium Hub.
        /// </summary>
        public static readonly string HubUriKey = "HubUri";
        /// <summary>
        /// The default location of the Selenium Hub.
        /// </summary>
        public static string DefaultHubUri = "http://10.91.4.57:8888/wd/hub";  // Clinical Intelligence / Foundations server hub: 
        //public static string DefaultHubUri = "http://10.91.160.251:8888/wd/hub";  // temporary server hub: 

        /// <summary>
        /// Gets the location of the Selenium Hub.
        /// </summary>
        public static string HubUri { get; private set; }

        /// <summary>
        /// Specifies the location for Selenium Extras.
        /// </summary>
        public static readonly string ExtrasUriKey = "ExtrasUri";
        /// <summary>
        /// The default location of Selenium Extras.
        /// </summary>
        public static string DefaultExtrasUri = "http://10.0.0.115:3000/"; //"http://127.0.0.1:3000/";
        /// <summary>
        /// Gets the location of Selenium Extras.
        /// </summary>
        public static string ExtrasUri { get; private set; }

        #endregion Grid

        #region Timeout

        /// <summary>
        /// Specifies the command timeout.
        /// </summary>      
        public static readonly string CommandTimeoutKey = "CommandTimeout";
        /// <summary>
        /// The default command timeout.
        /// </summary>
        public static TimeSpan DefaultCommandTimeout = TimeSpan.FromSeconds(296); // Setting this to 1 second less (296) than the
                                                                                  // default Node timeout (--session-timeout) at 297
                                                                                  // and default Hub timeout (--session-request-timeout)
                                                                                  // at 298 so that when a timeout is hit,
                                                                                  // messages shows a OpenQA.Selenium.WebDriverException
                                                                                  // instead of System.InvalidOperationException or  
                                                                                  // Java.util.concurrent.timeoutexception.
                                                                                  // This will create less confusion
                                                                                  // See: https://code.premierinc.com/docs/display/PQA/Issues+Selenium+Grid

        /// <summary>                                                             
        /// Gets the command timeout.
        /// </summary>
        public static TimeSpan CommandTimeout { get; private set; }

        #endregion Timeout

        #region Test Output

        /// <summary>
        /// Specifies the location where screenshots will be saved.
        /// </summary>        
        public static readonly string TestOutputLocationKey = "TestOutputLocation";
        /// <summary>                                        
        /// The default screenshot location is .\TestOutput
        /// </summary>
        public static readonly string TestOutputLocationDefault = FileUtils.GetSolutionDirectory() + "TestOutput";
        /// <summary>
        /// Gets the screenshot location.  This property is guaranteed to have a trailing slash.
        /// </summary>        
        public static string TestOutputLocation { get; private set; }

        /// <summary>
        /// Specifies the format to be used when naming screenshots.  Do NOT include the file extension.  All screenshots are saved as png files.
        /// Acceptable replacement tokens are:
        /// {driverName} - ChromeDriver
        /// {fullDriverName} - OpenQA.Selenium.Chrome.ChromeDriver
        /// {testName} - TestA
        /// {fullTestNameWithDriver} - CompName.Test.ClassA(OpenQA.Selenium.Chrome.ChromeDriver).TestA
        /// {className} - ClassA
        /// {fullClassName} - CompName.Test.ClassA
        /// {sessionId} - UUID of session
        /// {browserName} - e.g. chrome
        /// </summary>
        public static readonly string ScreenShotNameFormatKey = "ScreenShotNameFormat";
        /// <summary>
        /// The default value to use when formatting screenshot names
        /// </summary>
        public static readonly string ScreenShotNameFormatDefault = "{className}.{testName}.{browserName}";
        /// <summary>
        /// Gets the screen shot name format to use when saving screenshots.
        /// </summary>        
        public static string ScreenShotNameFormat { get; private set; }
 

        #endregion Test Output

        static SeleniumCoreSettings()
        {
            // Driver
            DriverLocation = Environment.ExpandEnvironmentVariables(
                AppSettings.GetStringOrDefault(DriverLocationKey, DriverLocationDefault, ensureTrailing: @"\"));
            DriverLogsLocation = Environment.ExpandEnvironmentVariables(
                AppSettings.GetStringOrDefault(DriverLogsLocationKey, DriverLogsLocationDefault, ensureTrailing: @"\"));

            // Mode
            BrowserMode = AppSettings.GetEnumOrDefault<BrowserMode>(BrowserModeKey, BrowserModeDefault);

            // Download
            //DefaultDownloadDirectory_LocalDownloads = "C:\\seleniumdownloads";
                //Environment.ExpandEnvironmentVariables(
                //AppSettings.GetStringOrDefault(DefaultDownloadDirectory_LocalDownloadsKey, Environment.CurrentDirectory, ensureTrailing: @"\"));

            // Download Remote Downloads
            //DefaultDownloadDirectory_RemoteDownloads = "\\\\c3dilmssg01\\seleniumdownloads\\";

            // Grid
            HubUri = AppSettings.GetStringOrDefault(HubUriKey, DefaultHubUri);
            ExtrasUri = AppSettings.GetStringOrDefault(ExtrasUriKey, DefaultExtrasUri, ensureTrailing: @"/");

            // Timeout
            CommandTimeout = AppSettings.GetValueOrDefault<TimeSpan>(CommandTimeoutKey, DefaultCommandTimeout);

            // Screenshot
            TestOutputLocation = AppSettings.GetStringOrDefault(TestOutputLocationKey, TestOutputLocationDefault, ensureTrailing: @"\");
            ScreenShotNameFormat = AppSettings.GetStringOrDefault(ScreenShotNameFormatKey, ScreenShotNameFormatDefault);
        }
    }
}
