using Browser.Core.Framework;
using LMS.Data;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Threading;
using LOG4NET = log4net.ILog;
using LMS.AppFramework.LMSHelperMethods;

namespace LMS.AppFramework
{
    public class ActOrderReceiptPage : LMSPage, IDisposable
    {
        #region constructors
        public ActOrderReceiptPage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that WE start so we can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        public override string PageUrl { get { return "lms/#/activity"; } }
        LMSHelperMethods.LMSHelperMethods Help = new LMSHelperMethods.LMSHelperMethods();

        #endregion properties

        #region elements
        public IWebElement ExitBtn { get { return this.FindElement(Bys.ActOrderReceiptPage.ExitBtn); } }
        public IWebElement ActivityFeeLbl { get { return this.FindElement(Bys.ActOrderReceiptPage.ActivityFeeLbl); } }
        public IWebElement TotalAmountLbl { get { return this.FindElement(Bys.ActOrderReceiptPage.TotalAmountLbl); } }




        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(90), Criteria.ActOrderReceiptPage.PageReady);
            Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));
        }

        /// <summary>
        /// Clicks the user-specified element and then waits for a window to close or open, or a page to load,
        /// depending on the element that was clicked
        /// </summary>
        /// <param name="elem">The element to click on</param>
        public dynamic ClickAndWait(IWebElement elem)
        {     
            if (Browser.Exists(Bys.ActOrderReceiptPage.ExitBtn))
            {
                if (elem.GetAttribute("outerHTML") == ExitBtn.GetAttribute("outerHTML"))
                {
                    ExitBtn.Click(Browser);
                    return Help.WaitForNextPage(Browser, Constants.PageURLs.Activity_Order_Payment_Receipt);
                }
            }

            throw new Exception(string.Format("No element was found with your passed parameter, which was the '{0}' element. You either need to add " +
                "this element to a new If statement, or if the element is already added, then the page you were on did not contain the element.",
                elem.GetAttribute("innerText")));
        }

        /// <summary>
        /// Selects an item from a user-specified select element, then waits for a criteria to load fully
        /// </summary>
        /// <param name="selectElement">The select element to manipulate</param>
        /// <param name="selection">The exact text you want to choose from the item in the select elements</param>
        /// <returns></returns>
        public dynamic SelectAndWait(SelectElement selectElement, string selection)
        {


            return null;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            try { activeRequests.Clear(); }
            catch (Exception ex) { _log.ErrorFormat("Failed to dispose ActivityRegistrationPage", activeRequests.Count, ex); }
        }

        #endregion methods: repeated per page

        #region methods: page specific

     

        #endregion methods: page specific



    }
}