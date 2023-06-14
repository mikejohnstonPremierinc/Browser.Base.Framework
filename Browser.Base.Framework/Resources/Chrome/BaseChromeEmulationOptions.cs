//using OpenQA.Selenium.Chrome;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Browser.Core.Framework.Resources
//{
//    /// <summary>
//    /// Base ChromeOptions for a new ChromeDriver.
//    /// </summary>
//    public class BaseChromeGalaxyEmulationOptions : ChromeOptions
//    {
//        /// <summary>
//        /// Default constructor that sets some default options.
//        /// </summary>
//        public BaseChromeGalaxyEmulationOptions()
//        {
//            SetDefaultDownloadDirectory(SeleniumCoreSettings.DefaultDownloadDirectory);
//            SetPromptForDownload(false);
//            SetDownloadDirectoryUpgrade(true);
//            //AddArgument("");

//            // The names of the different devices can be seen when you open Chrome devtools and click the Responsive design mode, then
//            // click the Responsive dropdown at the top of the screen to see which options you can choose
//            EnableMobileEmulation("Galaxy S5");
//        }


//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="directory"></param>
//        public virtual void SetDefaultDownloadDirectory(string directory)
//        {
//            AddUserProfilePreference("download.default_directory", directory);
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="promptForDownload"></param>
//        public virtual void SetPromptForDownload(bool promptForDownload)
//        {
//            AddUserProfilePreference("download.prompt_for_download", promptForDownload);
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="downloadDirectoryUpgrade"></param>
//        public virtual void SetDownloadDirectoryUpgrade(bool downloadDirectoryUpgrade)
//        {
//            AddUserProfilePreference("download.directory_upgrade", downloadDirectoryUpgrade);
//        }
//    }
//}
