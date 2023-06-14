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
using NUnit.Framework;

namespace LMS.AppFramework
{
    public class ActOrderDetailsPage : LMSPage, IDisposable
    {
        #region constructors
        public ActOrderDetailsPage(IWebDriver driver) : base(driver)
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
        public IWebElement DiscountCodeTxt { get { return this.FindElement(Bys.ActOrderDetailsPage.DiscountCodeTxt); } }
        public IWebElement ContinueToPaymentBtn { get { return this.FindElement(Bys.ActOrderDetailsPage.ContinueToPaymentBtn); } }
        public IWebElement ApplyBtn { get { return this.FindElement(Bys.ActOrderDetailsPage.ApplyCodeBtn); } }
        public IWebElement ConfirmFormOkBtn { get { return this.FindElement(Bys.ActOrderDetailsPage.ConfirmFormOkBtn); } }
        public IWebElement ActivityFeeValueLbl { get { return this.FindElement(Bys.ActOrderDetailsPage.ActivityFeeValueLbl); } }
        public IWebElement TotalFeeValueLbl { get { return this.FindElement(Bys.ActOrderDetailsPage.TotalFeeValueLbl); } }
        public IWebElement DiscountFeeValueLbl { get { return this.FindElement(Bys.ActOrderDetailsPage.DiscountFeeValueLbl); } }
        public IWebElement ConfirmFormCancelBtn { get { return this.FindElement(Bys.ActOrderDetailsPage.ConfirmFormCancelBtn); } }
        public IWebElement CompleteOrderBtn { get { return this.FindElement(Bys.ActOrderDetailsPage.CompleteOrderBtn); } }



        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(90), Criteria.ActOrderDetailsPage.PageReady);
            Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));
        }

        /// <summary>
        /// Clicks the user-specified element and then waits for a window to close or open, or a page to load,
        /// depending on the element that was clicked
        /// </summary>
        /// <param name="elem">The element to click on</param>
        public dynamic ClickAndWait(IWebElement elem)
        {
            if (Browser.Exists(Bys.ActOrderDetailsPage.ContinueToPaymentBtn))
            {
                if (elem.GetAttribute("outerHTML") == ContinueToPaymentBtn.GetAttribute("outerHTML"))
                {
                    ContinueToPaymentBtn.Click(Browser);

                    // Production has the old payment system until next SP
                    if (DateTime.Today < DateTime.ParseExact("06/26/2019", "MM/dd/yyyy", CultureInfo.InvariantCulture)
                        && Constants.CurrentEnvironment == Constants.Environments.Production.GetDescription())
                    {
                        Browser.WaitForElement(Bys.ActOrderDetailsPage.ConfirmFormOkBtn, ElementCriteria.IsVisible);
                        return null;
                    }
                    else
                    {
                        return Help.WaitForNextPage(Browser, Constants.PageURLs.Activity_Order_Details);
                    }
                }
            }

            if (Browser.Exists(Bys.ActOrderDetailsPage.CompleteOrderBtn))
            {
                if (elem.GetAttribute("outerHTML") == CompleteOrderBtn.GetAttribute("outerHTML"))
                {
                    CompleteOrderBtn.Click(Browser);
                    return Help.WaitForNextPage(Browser, Constants.PageURLs.Activity_Order_Details);
                }
            }

            if (Browser.Exists(Bys.ActOrderDetailsPage.ApplyCodeBtn))
            {
                if (elem.GetAttribute("outerHTML") == ApplyBtn.GetAttribute("outerHTML"))
                {
                    ApplyBtn.Click(Browser);
                    this.WaitUntil(Criteria.ActOrderDetailsPage.PageReady);
                    Browser.WaitJSAndJQuery();
                    return null;
                }
            }

            if (Browser.Exists(Bys.ActOrderDetailsPage.ConfirmFormOkBtn))
            {
                if (elem.GetAttribute("outerHTML") == ConfirmFormOkBtn.GetAttribute("outerHTML"))
                {
                    ConfirmFormOkBtn.Click(Browser);
                    return Help.WaitForNextPage(Browser, Constants.PageURLs.Activity_Order_Details);
                }
            }

            if (Browser.Exists(Bys.ActOrderDetailsPage.ConfirmFormCancelBtn))
            {
                if (elem.GetAttribute("outerHTML") == ConfirmFormCancelBtn.GetAttribute("outerHTML"))
                {
                    ConfirmFormCancelBtn.Click(Browser);
                    this.WaitUntil(Criteria.ActOrderDetailsPage.ConfirmFormCancelBtnNotExists);
                    return null;
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

        /// <summary>
        /// Need to add description comments and format the code better for this method
        /// </summary>
        public dynamic SubmitDiscountCode(string discountCode)
        {
            DiscountCodeTxt.SendKeys(discountCode);
            ClickAndWait(ApplyBtn);

            // If discount code results in no payment, then Complete Order button will show
            if (Browser.Exists(Bys.ActOrderDetailsPage.CompleteOrderBtn))
            {
                ClickAndWait(CompleteOrderBtn);
            }
            else
            {
                ClickAndWait(ContinueToPaymentBtn);
                // Production has the old payment system until next SP
                if (DateTime.Today < DateTime.ParseExact("06/26/2019", "MM/dd/yyyy", CultureInfo.InvariantCulture)
                    && Constants.CurrentEnvironment == Constants.Environments.Production.GetDescription())
                {
                    return ClickAndWait(ConfirmFormOkBtn);
                }
            }

            if (Browser.Url.Contains(Constants.PageURLs.Activity_Order_Payment_Receipt.GetDescription()))
            {
                return new ActOrderReceiptPage(Browser);

            }

            // ToDo when Fireball payment pages come
            else if ("" == "")
            {
                return null;

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="discountCode"></param>
        /// <param name="errorMessage"></param>
        public void AssertDiscountCodeError(string discountCode, string errorMessage)
        {
            DiscountCodeTxt.Clear();
            DiscountCodeTxt.SendKeys(discountCode);
            ApplyBtn.Click();
            Browser.WaitJSAndJQuery();
            Browser.WaitForElement(Bys.LMSPage.NotificationErrorMessageLbl);
            Browser.WaitJSAndJQuery();
            Assert.AreEqual(errorMessage, NotificationErrorMessageLbl.Text);
        }

        #endregion methods: page specific



    }
}