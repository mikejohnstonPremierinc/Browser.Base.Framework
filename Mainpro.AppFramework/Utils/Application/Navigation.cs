using Browser.Core.Framework;
using OpenQA.Selenium;
using System;

namespace Mainpro.AppFramework
{
    /// <summary>
    /// Responsible for basic page navigation and specific-page initialization.
    /// </summary>
    public static class Navigation
    {

        public static LoginPage GoToLoginPage(this IWebDriver driver, bool waitForInitialize = true)
        {
            var page = Navigate(p => new LoginPage(p), driver, waitForInitialize);
            return new LoginPage(driver);
        }

        public static DashboardPage GoToDashboardPage(this IWebDriver driver, bool waitForInitialize = true)
        {
            var page = Navigate(p => new DashboardPage(p), driver, waitForInitialize);
            return new DashboardPage(driver);
        }

        private static T Navigate<T>(Func<IWebDriver, T> createPage, IWebDriver driver, bool waitForInitialize) where T : Page
        {
            var page = createPage(driver);
            page.GoToPage(waitForInitialize);
            return page;
        }



    }
}
