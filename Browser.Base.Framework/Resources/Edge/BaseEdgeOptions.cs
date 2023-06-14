using OpenQA.Selenium.Edge;
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
    public class BaseEdgeOptions : EdgeOptions
    {
        /// <summary>
        /// Constructor that sets some default options without emulation
        /// </summary>
        public BaseEdgeOptions()
        {
            // We are logically setting this inside CreateLocalBrowser and CreateRemoteBrowser because we have a different download directory between local 
            // and remote
            AddUserProfilePreference("download.default_directory", SeleniumCoreSettings.DefaultDownloadDirectory);

            AddUserProfilePreference("download.prompt_for_download", false);
            AddUserProfilePreference("download.directory_upgrade", true);
            AddArgument("no-sandbox");
            // Had to comment this out because Mainpro tests verify PDF content within Browser tabs
            //AddUserProfilePreference("plugins.always_open_pdf_externally", true);
            AddUserProfilePreference("profile.default_content_settings.popups", "0");
            AddUserProfilePreference("profile.content_settings.exceptions.automatic_downloads.*.setting", "1");

            // Downloads...
            // For certain download links (I think all download links that download Microsoft Office files), Edge opens the download into a new tab
            // within the Browser instance and does NOT download the file. I have not figured out a way to disable this setting in code. Manually,
            // to turn off: Click the three dots on the top right corner > Settings > Downloads > Turn off the option "Open Office files in the browser".
            // I googled and I think this is a new feature in Edge because I have not found anything on the web to change this setting in Selenium.
            // For now, I have added an if statement within BrowserExtensions->WaitForDownload to throw a custom error message to the tester if they try to 
            // download a file in Edge. This is commented out though because csv files and other non-office files work, and some existing Premier
            // projects are downloading csv files (PINC). 

        }
    }
}
