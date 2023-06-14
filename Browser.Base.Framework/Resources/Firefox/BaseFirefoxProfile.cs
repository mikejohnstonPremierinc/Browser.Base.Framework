using OpenQA.Selenium.Firefox;

namespace Browser.Core.Framework.Resources
{
    /// <summary>
    /// Base FirefoxProfile for a new FirefoxDriver.
    /// </summary>
    public class BaseFirefoxProfile : FirefoxProfile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseFirefoxProfile"/> class.
        /// </summary>
        public BaseFirefoxProfile() 
            : base()
        {
            // To see all the preferences you can change, open a firefox window, type about:config, then do a search for 
            // any preference
            SetPreference("browser.startup.homepage", "about:blank");
            
        }

        #region Proxy Preferences

        /// <summary>
        /// Set the profile proxy preferences.
        /// </summary>
        public virtual void SetProxyPreferences()
        {
            // NOTE: proxy settings needed before using the grid.

            //SetPreference("network.proxy.backup.ftp", "firewall..com");
            //SetPreference("network.proxy.backup.ftp_port", 8080);
            //SetPreference("network.proxy.backup.socks", "firewall..com");
            //SetPreference("network.proxy.backup.socks_port", 8080);
            //SetPreference("network.proxy.backup.ssl", "firewall..com");
            //SetPreference("network.proxy.backup.ssl_port", 8080);
            //SetPreference("network.proxy.ftp", "firewall..com");
            //SetPreference("network.proxy.ftp_port", 8080);
            //SetPreference("network.proxy.http", "firewall..com");
            //SetPreference("network.proxy.http_port", 8080);
            //SetPreference("network.proxy.no_proxies_on",
            //    "localhost, 127.0.0.1, *.com, *.com, *ftp*..com, , ,<local>");
            //SetPreference("network.proxy.share_proxy_settings", true);
            //SetPreference("network.proxy.socks", "firewall..com");
            //SetPreference("network.proxy.socks_port", 8080);
            //SetPreference("network.proxy.ssl", "firewall..com");
            //SetPreference("network.proxy.ssl_port", 8080);
            //SetPreference("network.proxy.type", 1);
        }

        #endregion Proxy Preferences
    }
}



