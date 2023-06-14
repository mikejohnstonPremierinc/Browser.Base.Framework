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
    public class Distribution_PortalsPage : Page, IDisposable
    {
        #region constructors
        public Distribution_PortalsPage(IWebDriver driver) : base(driver)
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
        public IWebElement CatAndActTabSelCatalogTbl { get { return this.FindElement(Bys.Distribution_PortalsPage.CatAndActTabSelCatalogTbl); } }
        public IWebElement CatAndActTab { get { return this.FindElement(Bys.Distribution_PortalsPage.CatAndActTab); } }
        public IWebElement PortalLibraryTbl { get { return this.FindElement(Bys.Distribution_PortalsPage.PortalLibraryTbl); } }
        public IWebElement PortalLibraryTblTblBody { get { return this.FindElement(Bys.Distribution_PortalsPage.PortalLibraryTblBody); } }
        public IWebElement PortalLibraryTblBodyRow { get { return this.FindElement(Bys.Distribution_PortalsPage.PortalLibraryTblBodyRow); } }
        public IWebElement CatAndActTabSelCatalogsTbl { get { return this.FindElement(Bys.Distribution_PortalsPage.CatAndActTabSelCatalogsTbl); } }
        public IWebElement CatAndActTabSelCatalogsTblBody { get { return this.FindElement(Bys.Distribution_PortalsPage.CatAndActTabSelCatalogsTblBody); } }
        public IWebElement CatAndActTabSelCatalogsTblBodyRow { get { return this.FindElement(Bys.Distribution_PortalsPage.CatAndActTabSelCatalogsTblBodyRow); } }

        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(30), Criteria.Distribution_PortalsPage.PageReady);
        }

        /// <summary>
        /// Clicks the user-specified element, and then waits for a window to close or open, or a page to load, depending on the element that was clicked
        /// </summary>
        /// <param name="buttonOrLinkElem">The element to click on</param>
        public dynamic ClickAndWait(IWebElement buttonOrLinkElem)
        {
            // Error handler to make sure that the button that the tester passed in the parameter is actually on the page
            if (Browser.Exists(Bys.Distribution_PortalsPage.CatAndActTabSelCatalogTbl))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == CatAndActTabSelCatalogTbl.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    this.WaitUntil(Criteria.Distribution_PortalsPage.CatAndActTabSelCatalogTblVisible);
                    Browser.WaitForElement(Bys.Distribution_PortalsPage.CatAndActTabSelCatalogTbl, ElementCriteria.IsVisible);
                    return null;
                }
            }

            if (Browser.Exists(Bys.Distribution_PortalsPage.CatAndActTab))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == CatAndActTab.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    this.WaitUntil(Criteria.Distribution_PortalsPage.CatAndActTabVisible);
                    Browser.WaitForElement(Bys.Distribution_PortalsPage.PortalLibraryTbl, ElementCriteria.IsVisible);
                    return null;
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

        /// <summary>
        ///Clicks on either the pencil or X button for a user specified portal
        ///</summary>
        /// <param name = "portalName" > The portal name</param>
        ///<param name = "button" > "Edit" to click on the Pencil button, "Delete" to click on the X button or "View"</param>
        ///<returns></returns>
        public void AddCatalog(string portalName, string tagName, string button)
        {
            //ClickAndWait(PortalsLnk);
            IWebElement row = ElemGet_LMSAdmin.Grid_GetRowByRowName(PortalLibraryTbl, Bys.Distribution_PortalsPage.PortalLibraryTblBodyRow,
                portalName, "td");

            ElemSet_LMSAdmin.Grid_ClickElementWithoutTextInsideRow(row, tagName, button);

            //Distribution_PortalsPage page = new Distribution_PortalsPage(Browser);
            //page.WaitForInitialize();

            return;
        }

        /// <summary>
        ///Clicks on either the pencil or X button for a user specified portal
        ///</summary>
        ///<param name = "portalName" > The portal name</param>
        ///<param name = "tagName" > "input" "img" "a" </param>>
        ///<param name = "button" > "Edit" to click on the Pencil button, "Delete" to click on the X button or "View"</param>
        ///<returns></returns>
        public void RemoveCatalog(string portalName, string tagName, string button)
        {
            //ClickAndWait(PortalsLnk);
            IWebElement row = ElemGet_LMSAdmin.Grid_GetRowByRowName(CatAndActTabSelCatalogsTbl, Bys.Distribution_PortalsPage.CatAndActTabSelCatalogsTblBodyRow,
                portalName, "td");

            ElemSet_LMSAdmin.Grid_ClickElementWithoutTextInsideRow(row, tagName, button);

            Distribution_PortalsPage page = new Distribution_PortalsPage(Browser);
            page.WaitForInitialize();

            return;
        }

        /// <summary>
        /// Clicks on either the pencil or X button for a user specified portal
        /// </summary>
        /// <param name="portalName">The portal name</param>
        /// <param name="tagName"> "img" "input" "a"</param>
        /// <param name="button">"Edit" to click on the Pencil button, "Delete" to click on the X button or "View"</param>
        /// <returns></returns>
        public Distribution_PortalsPage GoToPortalDetails(string portalName, string tagName, string button)
        {
            IWebElement row = ElemGet_LMSAdmin.Grid_GetRowByRowName(PortalLibraryTbl, Bys.Distribution_PortalsPage.PortalLibraryTblBodyRow,
                portalName, "td");

            ElemSet_LMSAdmin.Grid_ClickElementWithoutTextInsideRow(row, tagName, button);

            Distribution_PortalsPage page = new Distribution_PortalsPage(Browser);
            page.WaitForInitialize();

            return page;
        }

        #endregion methods: page specific



    }
}