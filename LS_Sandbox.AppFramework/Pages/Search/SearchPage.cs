using Browser.Core.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using LOG4NET = log4net.ILog;
using LS.AppFramework.Constants_LTS;

namespace LS.AppFramework
{
    public class SearchPage : Page, IDisposable
    {
        #region constructors
        public SearchPage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that WE start so we can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        public override string PageUrl { get { return "sites"; } }

        #endregion properties

        #region elements

        public IWebElement AllParticpantsTbl { get { return this.FindElement(Bys.SearchPage.AllParticpantsTbl); } }
        public IWebElement AllParticpantsTblBody { get { return this.FindElement(Bys.SearchPage.AllParticpantsTblBody); } }
        public IWebElement GoBtn { get { return this.FindElement(Bys.SearchPage.GoBtn); } }
        public IWebElement SitesTbl { get { return this.FindElement(Bys.SearchPage.SitesTbl); } }
        public IWebElement SearchTxt { get { return this.FindElement(Bys.SearchPage.SearchTxt); } }
        public IWebElement GenericTblBodyRow { get { return this.FindElement(Bys.SearchPage.GenericTblBodyRow); } }


        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(30), Criteria.SearchPage.PageReady);
        }

        /// Refreshes the page and then uses the wait criteria that is found within WaitForInitialize to wait for the page to load.
        /// This is used as a catch block inside WaitForInitialize, in case the page doesnt load initially. Can also be used to 
        /// randomly refresh the page
        /// </summary>
        public void RefreshPage()
        {
            Browser.Navigate().Refresh();
            this.WaitUntil(TimeSpan.FromSeconds(60), Criteria.SearchPage.PageReady);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            try { activeRequests.Clear(); }
            catch (Exception ex) { _log.ErrorFormat("Failed to dispose LoginPage", activeRequests.Count, ex); }
        }

        #endregion methods: repeated per page

        #region methods: page specific

        /// <summary>
        /// For any table within LS, this method enters text in the search box, then either clicks Go or hits Enter and waits for table to 
        /// get returned by waiting for the the tbody element's "class" attribute to not have a value of "loading"
        /// </summary>
        /// <param name="tblBody">The tbody element in your table</param>
        /// <param name="searchText">What you want to search for</param>
        public void Search(By tblBody, string searchText = null)
        {
            Thread.Sleep(300);
            SearchTxt.Clear();
            SearchTxt.SendKeys(searchText);
            Thread.Sleep(300);
            SearchTxt.SendKeys(Keys.Enter);
            Thread.Sleep(1000);

            Browser.WaitForElement(tblBody, TimeSpan.FromSeconds(30), ElementCriteria.AttributeValueNot("class", "loading"));
            Thread.Sleep(1000);

            // Sometimes, I dont know why cause I dont have time right now to debug, after we search a participant, it
            // does not return results. So if this happens, refresh the page and search again. If 
            if (Browser.Exists(By.XPath("//legend[text()='Participants']")) &&
                !Browser.Exists(By.XPath(string.Format("//span[@title='{0}']/../..//a", searchText))))
            {
                this.RefreshPage();
                SearchTxt.Clear();
                SearchTxt.SendKeys(searchText);
                Thread.Sleep(300);
                SearchTxt.SendKeys(Keys.Enter);
                Thread.Sleep(1000);
                Browser.WaitForElement(tblBody, TimeSpan.FromSeconds(30), ElementCriteria.AttributeValueNot("class", "loading"));
                Thread.Sleep(1000);

                // If it still didnt find the search criteria in the table, throw an error
                if (!Browser.Exists(By.XPath(string.Format("//span[@title='{0}']/../..//a", searchText))))
                {
                    throw new Exception("After searching once, not finding a participant, then refreshing the page, " +
                        "then searching again, the table did not return your participant. Check the spelling of your " +
                        "participant. If spelling is correct, then there may be a bug in LTS");
                }
            }
        }

        /// <summary>
        /// Clicks on any website within the sites table, then waits for the next page to load
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="siteName">The exact text of the link for the site that you want to click on</param>
        internal SitePage ClickSiteAndWait(IWebDriver browser, string siteName)
        {
            try
            {
                ElemSet.Grid_ClickButtonOrLinkWithinRow(browser, SitesTbl, Bys.SearchPage.SitesTblBodyRow,
                siteName + "...", "a", siteName + "...", "a");
            }
            catch
            {

                ElemSet.Grid_ClickButtonOrLinkWithinRow(browser, SitesTbl, Bys.SearchPage.SitesTblBodyRow,
                    siteName, "a", siteName, "a");
            }

            SitePage SP = new SitePage(browser);
            SP.WaitForInitialize();
            return SP;
            
            //browser.WaitForElement(Bys.Page.AllParticipantsLnk, ElementCriteria.IsVisible);
        }

        /// <summary>
        /// Clicks on any participant within the participant table, then waits for the Participant page to load
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="username">The username for the participant that you want to click on from the participants table</param>
        internal ParticipantsPage ClickParticpantAndWait(IWebDriver browser, string username = null)
        {
            // See comment at bottom of this method above If statement for why we have this TakeScreenshot line
            //browser.TakeScreenshot("LSAppFramework_SearchPage_ClickParticpantAndWait");
            IWebElement usernameLnk =
                Browser.FindElement(By.XPath(string.Format("(//span[@title='{0}']/../..//a)[1]", username)));
            usernameLnk.Click();

            ParticipantsPage page = new ParticipantsPage(browser);
            page.WaitForInitialize();

            // Make sure we got to the correct user page. If a test fails here, then we know the above click or somewhere 
            // before it failed to get us to the correct user page. See bug
            // https://code.premierinc.com/issues/browse/MAINPROREW-915 and LSHelperMethods>LaunchSiteFromParticipantPage
            // regarding CFPC bug for more info on why we are checking this. We put in a workaround in Mainpro (using unique
            // LTST user per every test), so I am commenting the TakeScreenshot lines of code since that takes up HDD space
            // and its not needed right now since tests are no longer failing with workaround
            //browser.TakeScreenshot("LSAppFramework_SearchPage_ClickParticpantAndWait");
            if (!Browser.Exists(By.XPath(string.Format("//td[text()='{0}']", username))))
            {
                browser.TakeScreenshot("LSAppFramework_SearchPage_ClickParticpantAndWait");
                throw new Exception(string.Format("After clicking on the usernameLnk, LTS failed to land on the correct users page. The " +
                    "user that it should have landed on was {0}. See screenshot of failed test for the user it actually " +
                    "landed on. See code for more info", username));
            }

            return page;
        }

        /// <summary>
        /// For any table within LS, this method enters text in the search box, then either clicks Go or hits Enter and 
        /// waits for table to get returned by waiting for the the tbody element's "class" attribute to not have a 
        /// value of "loading". Then it will click on a site, particpant, etc. and wait for the cooresponding table to appear
        /// </summary>
        /// <param name="tblBody">The tbody element in your table</param>
        /// <param name="searchResults"><see cref="Constants_LTS.Constants_LTS.SearchResults"/> for a list of criteria 
        /// that you can search for. For example, if you are on the Sites table, then you would obviously pass Sites as 
        /// your search result</param>
        /// <param name="recordName">The exact text of the link for the site/participant/program/activity that you want
        /// to click on inside the table</param>
        /// <param name="optionalRecordSearchFor">(Optional) If you want to search for username instead of Participant
        /// Name, then pass the username here</param>
        public dynamic SearchAndSelect(By tblBody, Constants_LTS.Constants_LTS.SearchResults searchResults,
            string recordName = null, string optionalRecordSearchFor = null)
        {
            string criteriaToSearchFor = string.IsNullOrEmpty(optionalRecordSearchFor) ? recordName : optionalRecordSearchFor;

            Search(tblBody, criteriaToSearchFor);

            switch (searchResults)
            {
                case Constants_LTS.Constants_LTS.SearchResults.Sites:
                    return ClickSiteAndWait(Browser, recordName);
                case Constants_LTS.Constants_LTS.SearchResults.Participants:
                    ParticipantsPage page = ClickParticpantAndWait(Browser, recordName);
                    page.WaitForInitialize();
                    return page;
            }

            return null;
        }


        #endregion methods: page specific



    }
}