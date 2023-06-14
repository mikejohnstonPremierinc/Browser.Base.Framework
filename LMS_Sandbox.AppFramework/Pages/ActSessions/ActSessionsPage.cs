using Browser.Core.Framework;
using LMS.Data;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using LOG4NET = log4net.ILog;
using LMS.AppFramework.LMSHelperMethods;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LMS.AppFramework
{
    public class ActSessionsPage : LMSPage, IDisposable
    {
        #region constructors
        public ActSessionsPage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that WE start so we can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        public override string PageUrl { get { return "lms/transcript"; } }
        LMSHelperMethods.LMSHelperMethods Help = new LMSHelperMethods.LMSHelperMethods();
        #endregion properties

        #region elements

        public IWebElement ContinueBtn { get { return this.FindElement(Bys.ActSessionsPage.ContinueBtn); } }
        public IWebElement AvailableSessionsTbl_FromDtTxt { get { return this.FindElement(Bys.ActSessionsPage.AvailableSessionsTbl_FromDtTxt); } }
        public IWebElement AvailableSessionsTbl_SearchTxt { get { return this.FindElement(Bys.ActSessionsPage.AvailableSessionsTbl_SearchTxt); } }
        public IWebElement AssessmentListTbl { get { return this.FindElement(Bys.ActSessionsPage.AssessmentListTbl); } }
        public IWebElement SelectedSessionsTbl { get { return this.FindElement(Bys.ActSessionsPage.SelectedSessionsTbl); } }
        public IWebElement AvailableSessionsTblSelectBtns { get { return this.FindElement(Bys.ActSessionsPage.AvailableSessionsTblSelectBtns); } }
        public IWebElement AvailableSessionsTblBody { get { return this.FindElement(Bys.ActSessionsPage.AvailableSessionsTblBody); } }
        public IWebElement AvailableSessionsTbl { get { return this.FindElement(Bys.ActSessionsPage.AvailableSessionsTbl); } }
        public IWebElement AccessCodeFormContinueBtn { get { return this.FindElement(Bys.ActSessionsPage.AccessCodeFormContinueBtn); } }
        public IWebElement ReturnToMySessionsBtn { get { return this.FindElement(Bys.ActSessionsPage.ReturnToMySessionsBtn); } }
        public IWebElement SelectMoreSessionsBtn { get { return this.FindElement(Bys.ActSessionsPage.SelectMoreSessionsBtn); } }
        public IWebElement RegisterSessionsBtn { get { return this.FindElement(Bys.ActSessionsPage.RegisterSessionsBtn); } }
        public IWebElement GoToMySessionsBtn { get { return this.FindElement(Bys.ActSessionsPage.GoToMySessionsBtn); } }
        public IWebElement ResetFiltersBtn { get { return this.FindElement(Bys.ActSessionsPage.ResetFiltersBtn); } }
        public IWebElement AccessCodeFormAccessCodeTxt { get { return this.FindElement(Bys.ActSessionsPage.AccessCodeFormAccessCodeTxt); } }
        public IWebElement AvailableSessionsTbl_ToDtTxt { get { return this.FindElement(Bys.ActSessionsPage.AvailableSessionsTbl_ToDtTxt); } }
        public IWebElement AccessCodeFormRequiredLbl { get { return this.FindElement(Bys.ActSessionsPage.AccessCodeFormRequiredLbl); } }
        public IWebElement AccessCodeFormXBtn { get { return this.FindElement(Bys.ActSessionsPage.AccessCodeFormXBtn); } }


        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(90), Criteria.ActSessionsPage.PageReady);
            Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));
        }

        /// <summary>
        /// Clicks the user-specified element and then waits for a window to close or open, or a page to load,
        /// depending on the element that was clicked
        /// </summary>
        /// <param name="elem">The element to click on</param>
        public dynamic ClickAndWait(IWebElement elem)
        {
            if (Browser.Exists(Bys.ActSessionsPage.ContinueBtn))
            {
                if (elem.GetAttribute("outerHTML") == ContinueBtn.GetAttribute("outerHTML"))
                {
                    // On IE< whenever this page has content rows expanded and we try to click on something at the bottom of the page, a weird
                    // issue happens and the page freezes. Scrolling first fixes this
                    ElemSet.ScrollToElement(Browser, ContinueBtn, false);

                    // Using javascript click here for the following reason. When we use a regular click, IE then doesnt load 
                    // the page fully for some reason. This is not reproducable manually
                    ContinueBtn.ClickJS(Browser);
                    Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));

                    return Help.WaitForNextPage(Browser, Constants.PageURLs.Activity_Sessions);
                }
            }

            throw new Exception(string.Format("No element was found with your passed parameter, which was the '{0}' element. You either need to add " +
                "this element to a new If statement, or if the element is already added, then the page you were on did not contain the element.",
                elem.GetAttribute("innerText")));
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            try { activeRequests.Clear(); }
            catch (Exception ex) { _log.ErrorFormat("Failed to dispose ActivityPreviewPage", activeRequests.Count, ex); }
        }

        #endregion methods: repeated per page

        #region methods: page specific

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionName"></param>
        public void SelectSession(string sessionName)
        {
            // First get the row count of the selected sessions, so that we can use this row count to wait for the row count
            // to increment after we click the Select button
            int initialRowCount = ElemGet.Grid_GetRowCount(Browser, SelectedSessionsTbl);

            // Click the Select button
            ElemSet.Grid_ClickButtonOrLinkWithinRow(Browser, AvailableSessionsTbl, Bys.ActSessionsPage.AvailableSessionsTblBody,
                sessionName, "a", "Select", "span");

            // The session might require an Access Code. So we will wait 2 seconds (it is quick) for the Access Code
            // modal. If it does not appear, then that means 
            Thread.Sleep(2000);
            if (Browser.Exists(Bys.ActSessionsPage.AccessCodeFormAccessCodeTxt, ElementCriteria.IsVisible))
            {
                string accessCode = DBUtils.GetAccessCode(sessionName);
                AccessCodeFormAccessCodeTxt.SendKeys(DBUtils.GetAccessCode(sessionName));
                AccessCodeFormContinueBtn.Click();
                this.WaitUntil(Criteria.ActSessionsPage.AccessCodeFormAccessCodeTxtNotVisible);
                Browser.WaitForElement(Bys.ActSessionsPage.AvailableSessionsTbl, ElementCriteria.IsVisible);
                Browser.WaitJSAndJQuery();
            }

            // Wait for the row to be added
            DateTime now = DateTime.Now;
            while (initialRowCount + 1 != ElemGet.Grid_GetRowCount(Browser, SelectedSessionsTbl))
            {
                if (DateTime.Now > now.AddSeconds(20))
                {
                    throw new Exception("After clicking the Select button, the session failed to add to the Selected list");
                }
            }

            // Wait for the notification message
            Browser.WaitForElement(Bys.LMSPage.NotificationInfoMessageLbl, ElementCriteria.HasText, ElementCriteria.IsVisible);

            // Click X on the notification message so it goes away. If this ever stops working, we can instead just add a wait 
            // to wait for it to disappear. Right now, if we dont click it or dont wait, it interferes with future clicks in a 
            // test
            // 3/25/20: This started failing because the notification would be really quick to disappear, so once the code
            // reached the If statements inside ClickAndWaitBasePage, it would say it was a StaleElementReferenceException
            // because its removed from the HTML. So now we are just waiting until it goes away
            //  ClickAndWaitBasePage(NotificationInfoMessageLblXBtn);    
            this.WaitUntil(Criteria.ActSessionsPage.NotificationInfoMessageLblXBtnNotExists);
        }



        #endregion methods: page specific

    }

}