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
    public class ActPaymentPage : LMSPage, IDisposable
    {
        #region constructors
        public ActPaymentPage(IWebDriver driver) : base(driver)
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


        public IWebElement SubmitAccessCodeBtn { get { return this.FindElement(Bys.ActPaymentPage.SubmitAccessCodeBtn); } }
        public IWebElement AccessCodeTxt { get { return this.FindElement(Bys.ActPaymentPage.AccessCodeTxt); } }

        public IWebElement FirstNameRegViewTxt { get { return this.FindElement(Bys.ActPaymentPage.FirstNameRegViewTxt); } }
        public IWebElement LastNameRegViewTxt { get { return this.FindElement(Bys.ActPaymentPage.LastNameRegViewTxt); } }

        public IWebElement SubmitRegistrationBtn { get { return this.FindElement(Bys.ActPaymentPage.SubmitRegistrationBtn); } }
        public IWebElement AreYouCHESNoRdo { get { return this.FindElement(Bys.ActPaymentPage.AreYouCHESNoRdo); } }
        public IWebElement FeeAmountValueLbl { get { return this.FindElement(Bys.ActPaymentPage.FeeAmountValueLbl); } }
        public IWebElement UseADiscountCodeLnk { get { return this.FindElement(Bys.ActPaymentPage.UseADiscountCodeLnk); } }
        public SelectElement StateSelElem { get { return new SelectElement(this.FindElement(Bys.ActPaymentPage.StateSelElem)); } }
        public SelectElement CountrySelElem { get { return new SelectElement(this.FindElement(Bys.ActPaymentPage.CountrySelElem)); } }


        public SelectElement CreditCardTypeSelElem { get { return new SelectElement(this.FindElement(Bys.ActPaymentPage.CreditCardTypeSelElem)); } }
        public SelectElement ExpDateMonthSelElem { get { return new SelectElement(this.FindElement(Bys.ActPaymentPage.ExpDateMonthSelElem)); } }
        public SelectElement ExpDateYearSelElem { get { return new SelectElement(this.FindElement(Bys.ActPaymentPage.ExpDateYearSelElem)); } }
        public IWebElement FirstNameTxt { get { return this.FindElement(Bys.ActPaymentPage.FirstNameTxt); } }
        public IWebElement LastNameTxt { get { return this.FindElement(Bys.ActPaymentPage.LastNameTxt); } }
        public IWebElement AddLine1Txt { get { return this.FindElement(Bys.ActPaymentPage.AddLine1Txt); } }
        public IWebElement TownCityTxt { get { return this.FindElement(Bys.ActPaymentPage.TownCityTxt); } }
        public IWebElement ZipCodeTxt { get { return this.FindElement(Bys.ActPaymentPage.ZipCodeTxt); } }
        public IWebElement DaytimePhoneTxt { get { return this.FindElement(Bys.ActPaymentPage.DaytimePhoneTxt); } }
        public IWebElement CreditCardNumTxt { get { return this.FindElement(Bys.ActPaymentPage.CreditCardNumTxt); } }
        public IWebElement SecurityCodeTxt { get { return this.FindElement(Bys.ActPaymentPage.SecurityCodeTxt); } }
        public IWebElement SubmitBtn { get { return this.FindElement(Bys.ActPaymentPage.SubmitBtn); } }
        public IWebElement ApplyBtn { get { return this.FindElement(Bys.ActPaymentPage.ApplyBtn); } }
        public IWebElement DiscountCodeTxt { get { return this.FindElement(Bys.ActPaymentPage.DiscountCodeTxt); } }
        public IWebElement LoadIcon_Payment { get { return this.FindElement(Bys.ActPaymentPage.LoadIcon_Payment); } }
        public IWebElement DeleteDiscountCodeXBtn { get { return this.FindElement(Bys.ActPaymentPage.DeleteDiscountCodeXBtn); } }
        public IWebElement PaymentInvoiceLbl { get { return this.FindElement(Bys.ActPaymentPage.PaymentInvoiceLbl); } }
        public IWebElement NextBtn { get { return this.FindElement(Bys.ActPaymentPage.NextBtn); } }
        public IWebElement PaymentConfirmationLbl { get { return this.FindElement(Bys.ActPaymentPage.PaymentConfirmationLbl); } }
        public IWebElement OkBtn { get { return this.FindElement(Bys.ActPaymentPage.OkBtn); } }
        public IWebElement ParticipantIdTxt { get { return this.FindElement(Bys.ActPaymentPage.ParticipantIdTxt); } }
        public IWebElement Address01Txt { get { return this.FindElement(Bys.ActPaymentPage.Address01Txt); } }
        public IWebElement CityTxt { get { return this.FindElement(Bys.ActPaymentPage.CityTxt); } }
        public IWebElement PostalCodeTxt { get { return this.FindElement(Bys.ActPaymentPage.PostalCodeTxt); } }
        public IWebElement EmailTxt { get { return this.FindElement(Bys.ActPaymentPage.EmailTxt); } }
        public IWebElement OrganiztionorCompanyTxt { get { return this.FindElement(Bys.ActPaymentPage.OrganiztionorCompanyTxt); } }
        public IWebElement PasswordTxt { get { return this.FindElement(Bys.ActPaymentPage.PasswordTxt); } }
        public IWebElement ConfirmPasswordTxt { get { return this.FindElement(Bys.ActPaymentPage.ConfirmPasswordTxt); } }
        public IWebElement SecurityAnswerTxt { get { return this.FindElement(Bys.ActPaymentPage.SecurityAnswerTxt); } }
        public SelectElement ProfessionSelElem { get { return new SelectElement(this.FindElement(Bys.ActPaymentPage.ProfessionSelElem)); } }



        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(90), Criteria.ActPaymentPage.PageReady);
            Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));

            //// Sometimes this button appears on this page after you click the Resume/Register button on the Preview page
            //if (Browser.Exists(Bys.LMSPage.VerifyYourProfessionFormSubmitBtn))
            //{
            //    ClickAndWait(VerifyYourProfessionFormSubmitBtn);
            //}
        }

        /// <summary>
        /// Clicks the user-specified element and then waits for a window to close or open, or a page to load,
        /// depending on the element that was clicked
        /// </summary>
        /// <param name="elem">The element to click on</param>
        public dynamic ClickAndWait(IWebElement elem)
        {
            if (Browser.Exists(Bys.ActPaymentPage.ApplyBtn))
            {
                if (elem.GetAttribute("outerHTML") == ApplyBtn.GetAttribute("outerHTML"))
                {
                    ApplyBtn.Click(Browser);
                    Browser.WaitForElement(Bys.ActPaymentPage.DeleteDiscountCodeXBtn, ElementCriteria.IsVisible, ElementCriteria.IsEnabled);
                    return null;
                }
            }

            if (Browser.Exists(Bys.ActPaymentPage.UseADiscountCodeLnk))
            {
                if (elem.GetAttribute("outerHTML") == UseADiscountCodeLnk.GetAttribute("outerHTML"))
                {
                    UseADiscountCodeLnk.Click(Browser);
                    Browser.WaitForElement(Bys.ActPaymentPage.DiscountCodeTxt, TimeSpan.FromSeconds(90), ElementCriteria.IsVisible);
                    return null;
                }
            }

            if (Browser.Exists(Bys.ActPaymentPage.ApplyBtn))
            {
                if (elem.GetAttribute("outerHTML") == ApplyBtn.GetAttribute("outerHTML"))
                {
                    ApplyBtn.Click(Browser);
                    this.WaitUntil(Criteria.ActPaymentPage.PageReady);
                    return null;
                }
            }


            if (Browser.Exists(Bys.ActPaymentPage.SubmitBtn))
            {
                if (elem.GetAttribute("outerHTML") == SubmitBtn.GetAttribute("outerHTML"))
                {
                    SubmitBtn.Click(Browser);
                    this.WaitUntil(Criteria.ActPaymentPage.LoadIcon_PaymentVisible);
                    this.WaitUntil(TimeSpan.FromSeconds(90), Criteria.ActPaymentPage.LoadIcon_PaymentNotVisible);
                    Browser.WaitForElement(Bys.ActPaymentPage.OkBtn);
                    return null;
                }
            }


            if (Browser.Exists(Bys.ActPaymentPage.OkBtn))
            {
                if (elem.GetAttribute("outerHTML") == OkBtn.GetAttribute("outerHTML"))
                {
                    OkBtn.Click(Browser);
                    Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));

                    // Because Fireball payment page is not ready yet, after we click this button, then a bunch of URL redirects occur,
                    // So we will wait until all of those redirects are completed by waiting for the URLs to go away
                    var wait = new WebDriverWait(Browser, TimeSpan.FromSeconds(30));

                    wait.Until(browser =>
                    {
                        return !browser.Url.Contains("lms/?v=activity&@activity.id=");
                    });

                    wait.Until(browser =>
                    {
                        return !browser.Url.Contains("activity?@activity.id=");
                    });

                    Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));
                    Thread.Sleep(5000);

                    wait.Until(browser =>
                    {
                        return !browser.Url.Contains(Constants.PageURLs.Activity_Payment_Legacy.GetDescription().ToLower());
                    });

                    return Help.WaitForNextPage(Browser, Constants.PageURLs.Activity_Payment_Legacy);
                }
            }

            if (Browser.Exists(Bys.LMSPage.VerifyYourProfessionFormSubmitBtn))
            {
                if (elem.Text == VerifyYourProfessionFormSubmitBtn.Text)
                {
                    VerifyYourProfessionFormSubmitBtn.ClickJS(Browser);
                    try
                    {
                        Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));
                        this.WaitUntilAll(TimeSpan.FromSeconds(60), Criteria.ActPaymentPage.VerifyYourProfessionFormSubmitBtnNotExists);
                        Thread.Sleep(1000);
                    }
                    catch
                    {
                        VerifyYourProfessionFormSubmitBtn.ClickJS(Browser);
                        Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));
                        this.WaitUntilAll(TimeSpan.FromSeconds(60), Criteria.ActPaymentPage.VerifyYourProfessionFormSubmitBtnNotExists);
                        Thread.Sleep(1000);
                    }
                    // This verify your profession button showed up in front of the payment page once. So
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
        public void FillPaymentFormWithRandomData()
        {
            FirstNameTxt.SendKeys(DataUtils.GetRandomString(10));
            LastNameTxt.SendKeys(DataUtils.GetRandomString(10));
            AddLine1Txt.SendKeys(DataUtils.GetRandomString(10));
            TownCityTxt.SendKeys(DataUtils.GetRandomString(10));
            ZipCodeTxt.SendKeys("15206");
            CreditCardNumTxt.SendKeys("4111111111111111");
            SecurityCodeTxt.SendKeys("111");
            StateSelElem.SelectByIndex(2);
            CountrySelElem.SelectByIndex(2);
            CreditCardTypeSelElem.SelectByIndex(2);
            ExpDateMonthSelElem.SelectByIndex(2);
            ExpDateYearSelElem.SelectByIndex(2);
        }

        /// <summary>
        /// Need to add description comments and format the code better for this method
        /// </summary>
        public dynamic CompletePayment(string discountCode = null)
        {
            if (!string.IsNullOrEmpty(discountCode))
            {
                ClickAndWait(UseADiscountCodeLnk);
                DiscountCodeTxt.SendKeys("FULL");
                ClickAndWait(ApplyBtn);

            }
            else
            {
                FillPaymentFormWithRandomData();
            }

            ClickAndWait(SubmitBtn);

            return ClickAndWait(OkBtn);
        }

        #endregion methods: page specific



    }
}