using Browser.Core.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using Microsoft.CSharp;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using System;
using OpenQA.Selenium.Interactions;

namespace LS.AppFramework
{
    public abstract class Page : Browser.Core.Framework.Page
    {
        #region Constructors

        public Page(IWebDriver driver) : base(driver) { }

        #endregion

        #region Elements
        public IWebElement UserNameHdr { get { return this.FindElement(Bys.Page.UserNameHdr); } }

        public IWebElement AllParticipantsLnk { get { return this.FindElement(Bys.Page.AllParticipantsLnk); } }
        public IWebElement LogoutLnk { get { return this.FindElement(Bys.Page.LogoutLnk); } }
        public IWebElement RCPSCLnk { get { return this.FindElement(Bys.Page.RCPSCLnk); } }
        public IWebElement SitesTab { get { return this.FindElement(Bys.Page.SitesTab); } }

        #endregion Elements

        #region methods: page specific

        /// <summary>
        /// Clicks the user-specified element that exists on every page of RCP, and then waits for a window to close or open,
        /// or a page to load, depending on the element that was clicked
        /// </summary>
        /// <param name="buttonOrLinkElem">The element to click on</param>
        public dynamic ClickAndWaitBasePage(IWebElement buttonOrLinkElem)
        {
            // Error handler to make sure that the button that the tester passed in the parameter is actually on the page
            if (Browser.Exists(Bys.Page.AllParticipantsLnk))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == AllParticipantsLnk.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    SearchPage page = new SearchPage(Browser);
                    page.WaitUntilAll(Criteria.SearchPage.AllParticpantsTblBodyNotLoading, Criteria.SearchPage.SearchTxtVisible);
                    Thread.Sleep(1000);
                    return page;
                }
            }

            if (Browser.Exists(Bys.Page.SitesTab))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == SitesTab.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    Thread.Sleep(0200);

                    // If there is only 1 site in an environment, then the application will go to that sites landing page.
                    // If there are multiple sites, then it goes to the Search page. So we first need to wait for an
                    // element that appears on BOTH pages (The Sites breadcrumb link)
                    Browser.WaitForElement(Bys.SitePage.SitesBreadCrumbLnk, ElementCriteria.IsVisible);

                    // Then use a TRY to wait a split second for the Sites page Additional Information tab (This tab appears
                    // immediately along with the Sites breadcrumb link). If that doesnt appear, then that means we will
                    // be on the Search page, so we will use the Catch to wait for the Search page
                    try
                    {
                        Browser.WaitForElement(Bys.SitePage.AdditionalInfoTab, TimeSpan.FromMilliseconds(500), ElementCriteria.IsVisible);
                        SitePage page = new SitePage(Browser);
                        page.WaitForInitialize();
                        Thread.Sleep(0400);
                        return page;
                    }
                    catch
                    {
                        SearchPage page = new SearchPage(Browser);
                        page.WaitForInitialize();
                        Thread.Sleep(0400);
                        return page;
                    }
                }

            }

            throw new Exception("No button or link was found with your passed parameter. You either need to add this button to a new If statement, " +
                "or if the button is already added, then the page you were on did not contain the button.");

        }











        #endregion methods: page specific
    }
}