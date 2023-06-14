using Browser.Core.Framework;
using LMS.Data;
using OpenQA.Selenium;
using System;
using System.Configuration;
using System.Linq;

namespace LMS.AppFramework
{
    /// <summary>
    /// Responsible for basic page navigation and specific-page initialization.
    /// </summary>
    public static class Navigation
    {
        private static string baseURL = AppSettings.Config["urlwithoutsitecode"];
        private static string _baseUrlWithoutSiteCode;

        /// <summary>
        /// Reads the base url from a config file.  This config file is typically
        /// updated by the TFS Build Definition during deployment.
        /// </summary>
        public static string BaseUrlWithoutSiteCode
        {
            get
            {
                if (_baseUrlWithoutSiteCode == null) 
                    _baseUrlWithoutSiteCode = AppSettings.Config["urlwithoutsitecode"];
                return _baseUrlWithoutSiteCode;
            }
        }

        /// <summary>
        /// Navigates to the home page and waits for the home page to load by using the WaitForInitialize method that exists within
        /// the HomePage.cs class. It retrieves the base URL from App.Config->cmeqa.config (or appsettings.json in .NET Core) in the UITest
        /// project, then it appends the 'PageUrl' string property value from HomePage.cs onto the end of that base URL. 
        /// </summary>
        /// <param name="driver">The driver instance, i.e. 'Browser'</param>
        /// <param name="waitForInitialize">If set to true, this will wait for the page to fully load. <see cref="HomePage.WaitForInitialize"/></param>
        /// <returns></returns>
        public static HomePage GoToHomePage(this IWebDriver Browser, Constants.SiteCodes siteCode, bool waitForInitialize = true)
        {
            var page = Navigate(p => new HomePage(p), Browser, siteCode, waitForInitialize);
            return new HomePage(Browser);
        }

        /// <summary>
        /// If not logged in already, please use the following method instead: 
        /// <see cref="LMSHelperMethods.LMSHelperMethods.GoTo_Page(IWebDriver, Constants.SiteCodes, Constants.Page, bool, string, string)"/>
        /// </summary>
        /// <param name="Browser"></param>
        /// <param name="siteCode"></param>
        /// <param name="waitForInitialize"></param>
        /// <returns></returns>
        public static TranscriptPage GoToTranscriptPage(this IWebDriver Browser, Constants.SiteCodes siteCode, bool waitForInitialize = true)
        {
            var page = Navigate(p => new TranscriptPage(p), Browser, siteCode, waitForInitialize);
            return new TranscriptPage(Browser);
        }

        /// <summary>
        /// If not logged in already, please use the following method instead: 
        /// <see cref="LMSHelperMethods.LMSHelperMethods.GoTo_Page(IWebDriver, Constants.SiteCodes, Constants.Page, bool, string, string)"/>
        /// </summary>
        /// <param name="Browser"></param>
        /// <param name="siteCode"></param>
        /// <param name="waitForInitialize"></param>
        /// <returns></returns>
        public static ActivitiesInProgressPage GoToActivitiesInProgressPage(this IWebDriver Browser, Constants.SiteCodes siteCode, bool waitForInitialize = true)
        {
            var page = Navigate(p => new ActivitiesInProgressPage(p), Browser, siteCode, waitForInitialize);
            return new ActivitiesInProgressPage(Browser);
        }

        public static SearchPage GoToSearchPage(this IWebDriver Browser, Constants.SiteCodes siteCode, bool waitForInitialize = true)
        {
            Browser.Navigate().GoToUrl("about:blank");
            string baseSiteURL = baseURL.Insert(8, siteCode.GetDescription().ToLower());

            var offset = baseSiteURL.IndexOf('/');
            offset = baseSiteURL.IndexOf('/', offset + 1);
            var indexOfThirdSlash = baseSiteURL.IndexOf('/', offset + 1);

            string fullURL = "";

            // The search results page has a different page URL between sites, so we have to make this custom
            if (Constants.SitesWith_SearchURLIsEducation.Any(site => siteCode.GetDescription().Contains(site)))
            {
                fullURL = baseSiteURL.Insert(indexOfThirdSlash + 1, "lms/education");
            }
            else if (Constants.SitesWith_SearchURLIsLearningSearch.Any(site => siteCode.GetDescription().Contains(site)))
            {
                fullURL = baseSiteURL.Insert(indexOfThirdSlash + 1, "lms/learningsearch");
            }
            else if (Constants.SitesWith_SearchURLIsCatalog.Any(site => siteCode.GetDescription().Contains(site)))
            {
                fullURL = baseSiteURL.Insert(indexOfThirdSlash + 1, "lms/catalog");
            }

            Browser.Navigate().GoToUrl(fullURL);
            var page = new SearchPage(Browser);
            page.WaitForInitialize();
            return page;
        }


        public static LoginPage GoToLoginPage(this IWebDriver Browser, Constants.SiteCodes siteCode, bool waitForInitialize = true)
        {
            var page = Navigate(p => new LoginPage(p), Browser, siteCode, waitForInitialize);
            return new LoginPage(Browser);
        }

        public static ActPreviewPage GoToActivityPreviewPageById(this IWebDriver driver, string activityTitle)
        {
            string url = string.Format("{0}{1}{2}", AppSettings.Config["urlwithoutsitecode"].ToString(), "lms/activity?@activity.id=",
                DBUtils.GetActivityID(Constants.SiteCodes.UAMS, activityTitle));
            driver.Navigate().GoToUrl(url);
            ActPreviewPage APP = new ActPreviewPage(driver);
            APP.WaitForInitialize();
            return APP;
        }

        private static T Navigate<T>(Func<IWebDriver, T> createPage, IWebDriver Browser, Constants.SiteCodes siteCode, bool waitForInitialize) where T : Browser.Core.Framework.Page
        {
            var page = createPage(Browser);

            // 1/9/18: Added this next line to go to about:blank because when switching applications, going from LTS to RCP navigation sometimes would
            // not work. First going to about:blank fixed this. The tests in question which wouldnt navigate were in class RCP_Mainport_Cycle_Tests
            // Monitor going forward to see if this breaks any tests
            // 5/26/20: Commented this line out for LMS and PREM applications because going to about:blank between page navigations
            // would cause the userName variable in the javascript to disappear. See HomePage.GetUserName()
            Browser.Navigate().GoToUrl("about:blank");

            string url = "";

            url = BaseUrlWithoutSiteCode.Insert(8, siteCode.GetDescription()).ToLower() + page.PageUrl;

            Browser.Navigate().GoToUrl(url);

            if (waitForInitialize)
            {
                page.WaitForInitialize();
            }

            return page;
        }
    }
}
