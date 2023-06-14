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
    public class ActeCommercePage : LMSPage, IDisposable
    {
        #region constructors
        public ActeCommercePage(IWebDriver driver) : base(driver)
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
        public IWebElement PayBtn { get { return this.FindElement(Bys.ActeCommercePage.PayBtn); } }
        public IWebElement TotalAmountValueLbl { get { return this.FindElement(Bys.ActeCommercePage.TotalAmountValueLbl); } }
        public IWebElement VisaRdo { get { return this.FindElement(Bys.ActeCommercePage.VisaRdo); } }
        public IWebElement FirstNameTxt { get { return this.FindElement(Bys.ActeCommercePage.FirstNameTxt); } }
        public IWebElement LastNameTxt { get { return this.FindElement(Bys.ActeCommercePage.LastNameTxt); } }
        public IWebElement AddressTxt { get { return this.FindElement(Bys.ActeCommercePage.AddressTxt); } }
        public IWebElement CityTxt { get { return this.FindElement(Bys.ActeCommercePage.CityTxt); } }
        public IWebElement ZipTxt { get { return this.FindElement(Bys.ActeCommercePage.ZipTxt); } }
        public IWebElement CVNTxt { get { return this.FindElement(Bys.ActeCommercePage.CVNTxt); } }
        public IWebElement CardNumberTxt { get { return this.FindElement(Bys.ActeCommercePage.CardNumberTxt); } }
        public IWebElement PhoneNumberTxt { get { return this.FindElement(Bys.ActeCommercePage.PhoneNumberTxt); } }
        public IWebElement EmailTxt { get { return this.FindElement(Bys.ActeCommercePage.EmailTxt); } }
        
        public SelectElement ExirationDateYearSelElem { get { return new SelectElement(this.FindElement(Bys.ActeCommercePage.ExirationDateYearSelElem)); } }
        public SelectElement ExirationDateMonthSelElem { get { return new SelectElement(this.FindElement(Bys.ActeCommercePage.ExirationDateMonthSelElem)); } }
        public SelectElement CountrySelElem { get { return new SelectElement(this.FindElement(Bys.ActeCommercePage.CountrySelElem)); } }
        public IWebElement StateTxt { get { return this.FindElement(Bys.ActeCommercePage.StateTxt); } }

        
        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(90), Criteria.ActeCommercePage.PageReady);
        }

        /// <summary>
        /// Clicks the user-specified element and then waits for a window to close or open, or a page to load,
        /// depending on the element that was clicked
        /// </summary>
        /// <param name="elem">The element to click on</param>
        public dynamic ClickAndWait(IWebElement elem)
        {
            if (Browser.Exists(Bys.ActeCommercePage.PayBtn))
            {
                if (elem.GetAttribute("outerHTML") == PayBtn.GetAttribute("outerHTML"))
                {
                    PayBtn.Click(Browser);

                    return Help.WaitForNextPage(Browser, Constants.PageURLs.eCommerce_cybersource);
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
        public dynamic CompletePayment()
        {
            FirstNameTxt.SendKeys(DataUtils.GetRandomString(10));
            LastNameTxt.SendKeys(DataUtils.GetRandomString(10));
            AddressTxt.SendKeys(DataUtils.GetRandomString(10));
            CityTxt.SendKeys(DataUtils.GetRandomString(10));
            CountrySelElem.SelectByIndex(2);
            StateTxt.SendKeys("PA");
            ZipTxt.SendKeys("15206");
            PhoneNumberTxt.SendKeys("8888888888");
            EmailTxt.SendKeys("kjdnfiswbnfkswbf@mailinator.com");
            VisaRdo.Click();
            CardNumberTxt.SendKeys("4111111111111111");
            ExirationDateYearSelElem.SelectByIndex(2);
            ExirationDateMonthSelElem.SelectByIndex(10);
            CVNTxt.SendKeys("123");
            return ClickAndWait(PayBtn);
        }

        #endregion methods: page specific



    }
}