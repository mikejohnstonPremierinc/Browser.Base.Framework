using Browser.Core.Framework;
using LMS.Data;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using LOG4NET = log4net.ILog;

namespace LMSAdmin.AppFramework
{
    public class Distribution_CatalogsPage : Page, IDisposable
    {
        #region constructors
        public Distribution_CatalogsPage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that WE start so we can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        public override string PageUrl { get { return ""; } }

        #endregion properties

        #region elements

        public IWebElement DetailsTabCatalogNameTxt { get { return this.FindElement(Bys.Distribution_CatalogsPage.DetailsTabCatalogNameTxt); } }
        public IWebElement DetailsTabShortLabelTxt { get { return this.FindElement(Bys.Distribution_CatalogsPage.DetailsTabShortLabelTxt); } }
        public IWebElement DetailsTabDescriptionTxt { get { return this.FindElement(Bys.Distribution_CatalogsPage.DetailsTabDescriptionTxt); } }
        public IWebElement DetailsTabSaveBtn { get { return this.FindElement(Bys.Distribution_CatalogsPage.DetailsTabSaveBtn); } }

        public IWebElement DetailsTabCancelBtn { get { return this.FindElement(Bys.Distribution_CatalogsPage.DetailsTabCancelBtn); } }
        public IWebElement AddNewCatalogLnk { get { return this.FindElement(Bys.Distribution_CatalogsPage.AddNewCatalogLnk); } }


        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
                this.WaitUntil(TimeSpan.FromSeconds(30), Criteria.Distribution_CatalogsPage.PageReady);
        }

        /// <summary>
        /// Clicks the user-specified element that exists on the base page of cme, and then waits for a window to close or open,
        /// or a page to load, depending on the element that was clicked
        /// </summary>
        /// <param name="buttonOrLinkElem">The element to click on</param>
        public dynamic ClickAndWait(IWebElement buttonOrLinkElem)
        {
            // Error handler to make sure that the button that the tester passed in the parameter is actually on the page
            if (Browser.Exists(Bys.Distribution_CatalogsPage.AddNewCatalogLnk))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == AddNewCatalogLnk.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    this.WaitUntil(Criteria.Distribution_CatalogsPage.DetailsTabCatalogNameTxtVisible);
                }

            }

            if (Browser.Exists(Bys.Distribution_CatalogsPage.DetailsTabCancelBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == DetailsTabCancelBtn.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    this.WaitForInitialize();
                }
            }


            throw new Exception(string.Format("No element was found with your passed parameter, which was the '{0}' element. You either need to add " +
                "this element to a new If statement, or if the element is already added, then the page you were on did not contain the element.",
                buttonOrLinkElem.GetAttribute("innerText")));
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

      



        #endregion methods: page specific



    }
}