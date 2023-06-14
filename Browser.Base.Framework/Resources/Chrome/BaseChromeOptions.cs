using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Browser.Core.Framework.Resources
{
    /// <summary>
    /// Base ChromeOptions for a new ChromeDriver.
    /// </summary>
    public class BaseChromeOptions : ChromeOptions
    {
        /// <summary>
        /// Constructor that sets some default options without emulation
        /// </summary>
        public BaseChromeOptions()
        {
            // We are logically setting this inside CreateLocalBrowser and CreateRemoteBrowser because we have a different download directory between local 
            // and remote
            AddUserProfilePreference("download.default_directory", SeleniumCoreSettings.DefaultDownloadDirectory);

            AddUserProfilePreference("download.prompt_for_download", false);
            AddUserProfilePreference("download.directory_upgrade", true);
            AddArgument("no-sandbox");

            // Had to comment this out because Mainpro tests verify PDF content within Browser tabs
            //AddUserProfilePreference("plugins.always_open_pdf_externally", true);
            //BinaryLocation = @"C:\Users\mjohnsto\Downloads\chrome-win\chrome81.exe";

            // Chrome 87 caused an issue with parallel execution (tests fail and screenshots not being taken during parallel
            // execution. This is a workaround that was found in the below links:
            // https://code.premierinc.com/docs/display/PGHLMSDOCS/General+Issues
            // https://stackoverflow.com/questions/64917755/webdriver-io-selenium-tests-fail-when-the-window-is-in-the-background-on-chrome
            // https://bugs.chromium.org/p/chromedriver/issues/detail?id=3641&sort=-id
            // https://bugs.chromium.org/p/chromedriver/issues/detail?id=3657#c8
            // https://bugs.chromium.org/p/chromium/issues/detail?id=1150563s
            AddLocalStatePreference("browser", new { enabled_labs_experiments = new string[] { "calculate-native-win-occlusion@2" } });

        }

        /// <summary>
        /// Constructor that sets some default options with emulation
        /// </summary>
        public BaseChromeOptions(string deviceName)
        {
            // No downloads for emulation
            //SetDefaultDownloadDirectory(SeleniumCoreSettings.DefaultDownloadDirectory);           
            //SetPromptForDownload(false);
            //SetDownloadDirectoryUpgrade(true);

            AddArgument("no-sandbox");
            //SetLoggingPreference(LogType.Browser, LogLevel.All);
            // The names of the different devices can be seen when you open Chrome devtools and click the Responsive design mode, then
            // click the Responsive dropdown at the top of the screen to see which options you can choose
            EnableMobileEmulation(deviceName);
            // BinaryLocation = @"C:\Users\mjohnsto\Downloads\chrome-win\chrome81.exe";

            // Chrome 87 caused an issue with parallel execution (tests fail and screenshots not being taken during parallel
            // execution. This is a workaround that was found in the below links:
            // https://code.premierinc.com/docs/display/PGHLMSDOCS/General+Issues
            // https://stackoverflow.com/questions/64917755/webdriver-io-selenium-tests-fail-when-the-window-is-in-the-background-on-chrome
            // https://bugs.chromium.org/p/chromedriver/issues/detail?id=3641&sort=-id
            // https://bugs.chromium.org/p/chromedriver/issues/detail?id=3657#c8
            // https://bugs.chromium.org/p/chromium/issues/detail?id=1150563s
            AddLocalStatePreference("browser", new { enabled_labs_experiments = new string[] { "calculate-native-win-occlusion@2" } });

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="directory"></param>
        public virtual void SetDefaultDownloadDirectory(string directory)
        {
            AddUserProfilePreference("download.default_directory", directory);
        }



    }
}
