using OpenQA.Selenium.Firefox;

namespace Browser.Core.Framework.Resources
{
    /// <summary>
    /// Base FirefoxOptions for a new FirefoxDriver.
    /// </summary>
    public class BaseFirefoxOptions : FirefoxOptions
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public BaseFirefoxOptions()
        {
            // To see all the preferences you can change, open a firefox window, type about:config, then do a search for 
            // any preference

            // Whenever we try to navigate (Browser.Navigate.GoToURL) to a premier site in FF, we get a "Warning: Potential
            // Security Risk Ahead" or "This website is not secure" error. This is due to Firefox not accepting unknown
            // certificates. To fix this, I first 
            // had to install the Premier certificates manually through the Settings of Firefox. But then that stopped working. I 
            // then had to add the AcceptInsecureCerts and AcceptUntrustedCertificates and AssumeUntrustedCertificateIssuer 
            // lines of code below, but then that stopped working. I then had to add 
            // the preferences security.cert_pinning.enforcement_level and security.enterprise_roots.enabled. This finally 
            // worked after installing the latest version of Firefox and downloading the latest version of GeckoDriver
            SetPreference("AcceptInsecureCerts", true);
            SetPreference("AcceptUntrustedCertificates", true);
            SetPreference("AssumeUntrustedCertificateIssuer", false);
            SetPreference("security.cert_pinning.enforcement_level", 0);
            SetPreference("security.enterprise_roots.enabled", true);
           

            // Firefox was opening the CFPC reports in a new window. Needed to change this preference here...
            SetPreference("browser.link.open_newwindow", "3");

            // We are logically setting this inside CreateLocalBrowser and CreateRemoteBrowser because we have a different download directory between local 
            // and remote
            SetPreference("browser.download.dir", SeleniumCoreSettings.DefaultDownloadDirectory);

            SetPreference("browser.download.folderList", 2);
            SetPreference("browser.helperApps.neverAsk.saveToDisk",
                "vnd.openxmlformats-officedocument.spreadsheetml.sheet,application/json)");
        }
    }
}
