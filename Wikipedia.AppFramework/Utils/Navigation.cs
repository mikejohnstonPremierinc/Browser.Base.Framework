using Browser.Core.Framework;
using OpenQA.Selenium;
using System;
using System.Threading;

namespace Wikipedia.AppFramework
{
    /// <summary>
    /// Responsible for basic page navigation and specific-page initialization.
    /// </summary>
	public static class Navigation
	{
        /// <summary>
        /// Navigates to the home page and waits for the home page to load by using the WaitForInitialize method that exists within
        /// the HomePage.cs class. It retrieves the base URL from App.Config-><environmentname>.config 
        /// (or appsettings.<environmentname>.json in .NET Core) in the UITest
        /// project, then it appends the 'PageUrl' string property value from HomePage.cs onto the end of that base URL. 
        /// </summary>
        /// <param name="driver">The driver instance, i.e. 'Browser'</param>
        /// <param name="waitForInitialize">If set to true, this will wait for the page to fully load. <see cref="HomePage.WaitForInitialize"/></param>
        /// <returns></returns>
        public static HomePage GoToHomePage(this IWebDriver driver, bool waitForInitialize = true)
        {
            var page = Navigate(p => new HomePage(p), driver, waitForInitialize);
            return new HomePage(driver);
        }

        /// <summary>
        /// Navigates to the Help page and waits for the Help page to load by using the WaitForInitialize method that exists within
        /// the HelpPage.cs class. It retrieves the base URL from App.Config-><environmentname>.config 
        /// (or appsettings.<environmentname>.json in .NET Core) in the UITest
        /// project, then it appends the 'PageUrl' string property value from HelpPage.cs onto the end of that base URL. 
        /// </summary>
        /// <param name="driver">The driver instance, i.e. 'Browser'</param>
        /// <param name="waitForInitialize">If set to true, this will wait for the page to fully load. <see cref="HelpPage.WaitForInitialize"/></param>
        /// <returns></returns>
        public static HelpPage GoToHelpPage(this IWebDriver driver, bool waitForInitialize = true)
        {
            var page = Navigate(p => new HelpPage(p), driver, waitForInitialize);
            return new HelpPage(driver);
        }

        /// <summary>
        /// This method will not need modified per project. Leave this method alone
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="createPage"></param>
        /// <param name="driver"></param>
        /// <param name="waitForInitialize"></param>
        /// <returns></returns>
        private static T Navigate<T>(Func<IWebDriver, T> createPage, IWebDriver driver, bool waitForInitialize) where T : Browser.Core.Framework.Page
        {
            var page = createPage(driver);
            page.GoToPage(waitForInitialize);
            return page;
        }    
    }
}
