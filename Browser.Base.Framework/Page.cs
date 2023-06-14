using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Browser.Core.Framework
{
    /// <summary>
    /// Base class for all Selenium Page-Objects
    /// </summary>
	public abstract class Page : ISearchContext
	{
		#region Properties

        /// <summary>
        /// Gets the browser in which this page is running.
        /// </summary>
		public IWebDriver Browser { get; private set; }

		private static string _baseUrl;
        //private static string _baseUrlWithoutSiteCode;
        //private static string _baseurl_chi_CHI;
        //private static string _baseurl_chi_Others; 

        /// <summary>
        /// Reads the base url from a config file.  This config file is typically
        /// updated by the TFS Build Definition during deployment.
        /// </summary>
		public static string BaseUrl
		{
			get
			{
                if (_baseUrl == null) _baseUrl = AppSettings.Config["url"];
				return _baseUrl;
			}
		}

        ///// <summary>
        ///// Reads the base url from a config file.  This config file is typically
        ///// updated by the TFS Build Definition during deployment.
        ///// </summary>
        //public static string BaseUrlWithoutSiteCode
        //{
        //    get
        //    {
        //        if (_baseUrlWithoutSiteCode == null) _baseUrlWithoutSiteCode = ConfigurationManager.AppSettings["urlwithoutsitecode"];
        //        return _baseUrlWithoutSiteCode;
        //    }
        //}

        ///// <summary>
        ///// Reads the base url from a config file.  This config file is typically
        ///// updated by the TFS Build Definition during deployment.
        ///// </summary>
        //public static string Baseurl_chi_CHI
        //{
        //    get
        //    {
        //        if (_baseurl_chi_CHI == null) _baseurl_chi_CHI = ConfigurationManager.AppSettings["url_chi"];
        //        return _baseurl_chi_CHI;
        //    }
        //}

        ///// <summary>
        ///// Reads the base url from a config file.  This config file is typically
        ///// updated by the TFS Build Definition during deployment.
        ///// </summary>
        //public static string Baseurl_chi_Others
        //{
        //    get
        //    {
        //        if (_baseurl_chi_Others == null) _baseurl_chi_Others = ConfigurationManager.AppSettings["url_others"];
        //        return _baseurl_chi_Others;
        //    }
        //}

        /// <summary>
        /// Gets the relative URL for this page (in relation to the BaseUrl)
        /// </summary>
        public abstract string PageUrl { get; }

        /// <summary>
        /// Gets the fully qualified URL for this page
        /// </summary>
		public string Url
		{
			get
			{ return BaseUrl + PageUrl; }
		}

        ///// <summary>
        ///// Gets the fully qualified URL for this page, without the site code
        ///// </summary>
        //public string UrlWithoutSiteCode
        //{
        //    get
        //    { return BaseUrlWithoutSiteCode + PageUrl; }
        //}

        ///// <summary>
        ///// Gets the fully qualified URL for the CHI clients in the Premier Training Portal
        ///// </summary>
        //public string url_chi_CHI
        //{
        //    get
        //    { return Baseurl_chi_CHI + PageUrl; }
        //}

        ///// <summary>
        ///// Gets the fully qualified URL for the non-CHI clients in the Premier Training Portal
        ///// </summary>
        //public string url_chi_Others
        //{
        //    get
        //    { return Baseurl_chi_Others + "/" + PageUrl; }
        //}


        /// <summary>
        /// Gets the tile of the page (as displayed by the browser)
        /// </summary>
        public string Title { get { return Browser.Title; } }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Page"/> class.
        /// </summary>
        /// <param name="driver">The driver.</param>
        public Page(IWebDriver driver)
		{
			Browser = driver;
		}

		#endregion Constructors

		#region Public Methods

        /// <summary>
        /// Navigates the browser to this page
        /// </summary>
        /// <param name="waitForInitialize">if set to <c>true</c> waits for the page to be "fully initilized" (networks, dates, and active sessions are loaded).</param>
		public void GoToPage(bool waitForInitialize = true)
		{
            // 1/9/18: Added this next line to go to about:blank because when switching applications, going from LTS to RCP navigation sometimes would
            // not work. First going to about:blank fixed this. The tests in question which wouldnt navigate were in class RCP_Mainport_Cycle_Tests
            // Monitor going forward to see if this breaks any tests
            Browser.Navigate().GoToUrl("about:blank");

            Browser.Navigate().GoToUrl(Url);           

            if (waitForInitialize)
            {
                WaitForInitialize();
            }
		}

        /// <summary>
        /// Refreshes the page.        
        /// </summary>
        /// <param name="waitForInitialize">if set to <c>true</c> waits for the page to be "fully initilized" (networks, dates, and active sessions are loaded).</param>
        public void RefreshPage(bool waitForInitialize = true)
        {
            // TODO: How to we "encourage" tests to use this method INSTEAD of Browser.Navigate().Refresh()??
            Browser.Navigate().Refresh();
            if (waitForInitialize)
            {
                WaitForInitialize();
            }
        }


        ///// <summary>
        ///// Navigates the browser to this page
        ///// </summary>
        ///// <param name="waitForInitialize">if set to <c>true</c> waits for the page to be "fully initilized" (networks, dates, and active sessions are loaded).</param>
        //public void GoToPage_LMS(Constants.SiteCodes siteCode, bool waitForInitialize = true)
        //{
        //    // 1/9/18: Added this next line to go to about:blank because when switching applications, going from LTS to RCP navigation sometimes would
        //    // not work. First going to about:blank fixed this. The tests in question which wouldnt navigate were in class RCP_Mainport_Cycle_Tests
        //    // Monitor going forward to see if this breaks any tests
        //    Browser.Navigate().GoToUrl("about:blank");

        //    string url = "";

        //    switch (siteCode)
        //    {
        //        case Constants.SiteCodes.PREM:
        //            url = url_chi_CHI.Insert(8, siteCode.GetDescription()).ToLower();
        //            break;
        //        case Constants.SiteCodes.AWHONN:
        //            url = url_chi_Others.Insert(35, siteCode.GetDescription()).ToLower();
        //            break;
        //        // All non Premier Training Portal sites
        //        default:
        //            url = UrlWithoutSiteCode.Insert(8, siteCode.GetDescription()).ToLower();
        //            break;
        //    }

        //    Browser.Navigate().GoToUrl(url);
        //    if (waitForInitialize)
        //    {
        //        WaitForInitialize();
        //    }
        //}

        /// <summary>
        /// Waits for the page to be fully loaded.  Each page should override this method
        /// to determine what "fully loaded" means.
        /// </summary>
        public abstract void WaitForInitialize();

        /// <summary>
        /// Verifies that the specified element "exists" by using the default wait timeout.
        /// </summary>
        /// <param name="by">The by.</param>
        /// <returns></returns>
        [Obsolete("Use the ISearchContext.Exists() extension method instead.  It lives in the Browser.Core.Framework namespace.")]
        public bool Verify(By by)
		{
			var element = Browser.WaitForElement(by);
			if (element != null) return true;
			else return false;
		}

        #endregion Public Methods

        #region ISearchContext Members

        /// <summary>
        /// Finds the first <see cref="T:OpenQA.Selenium.IWebElement" /> using the given method.
        /// </summary>
        /// <param name="by">The locating mechanism to use.</param>
        /// <returns>
        /// The first matching <see cref="T:OpenQA.Selenium.IWebElement" /> on the current context.
        /// </returns>
        public IWebElement FindElement(By by)
        {
            return Browser.FindElement(by);
        }

        /// <summary>
        /// Finds all <see cref="T:OpenQA.Selenium.IWebElement">IWebElements</see> within the current context
        /// using the given mechanism.
        /// </summary>
        /// <param name="by">The locating mechanism to use.</param>
        /// <returns>
        /// A <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> of all <see cref="T:OpenQA.Selenium.IWebElement">WebElements</see>
        /// matching the current criteria, or an empty list if nothing matches.
        /// </returns>
        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
         return Browser.FindElements(by);
        }

        #endregion
    }
}
