using Browser.Core.Framework;
using LMS.Data;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using LOG4NET = log4net.ILog;
using LMSAdmin.AppFramework.ConstantsLMSAdmin;
using LMSAdmin.AppFramework.HelperMethods;
using System.Globalization;

namespace LMSAdmin.AppFramework
{
    public class Legacy_ActAccreditationPage : Page, IDisposable
    {
        #region constructors
        public Legacy_ActAccreditationPage(IWebDriver driver) : base(driver)
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


        public IWebElement BackToAccreditationsLnk { get { return this.FindElement(Bys.Legacy_ActAccreditationPage.BackToAccreditationsLnk); } }

        public IWebElement FixedCreditsTxt { get { return this.FindElement(Bys.Legacy_ActAccreditationPage.FixedCreditsTxt); } }
        public IWebElement ScenarioNameTxt { get { return this.FindElement(Bys.Legacy_ActAccreditationPage.ScenarioNameTxt); } }
        public IWebElement ScenariosTbl { get { return this.FindElement(Bys.Legacy_ActAccreditationPage.ScenariosTbl); } }
        public IWebElement AvailableAccreditationTypesTbl { get { return this.FindElement(Bys.Legacy_ActAccreditationPage.AvailableAccreditationTypesTbl); } }
        public SelectElement SelectProfAvailSelElem { get { return new SelectElement(this.FindElement(Bys.Legacy_ActAccreditationPage.SelectProfAvailSelElem)); } }
        public IWebElement AddScenarioLnk { get { return this.FindElement(Bys.Legacy_ActAccreditationPage.AddScenarioLnk); } }
        public IWebElement AddAccreditationTypeLnk { get { return this.FindElement(Bys.Legacy_ActAccreditationPage.AddAccreditationTypeLnk); } }
        public IWebElement SelectProfAddBtn { get { return this.FindElement(Bys.Legacy_ActAccreditationPage.SelectProfAddBtn); } }

        public IWebElement AddScenarioSaveBtn { get { return this.FindElement(Bys.Legacy_ActAccreditationPage.AddScenarioSaveBtn); } }
        public IWebElement AvailableAccreditationTypesContinueBtn { get { return this.FindElement(Bys.Legacy_ActAccreditationPage.AvailableAccreditationTypesContinueBtn); } }


        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(30), Criteria.Legacy_ActAccreditationPage.PageReady);
        }

        /// <summary>
        /// Clicks the user-specified element, and then waits for a window to close or open, or a page to load, depending on the element that was clicked
        /// </summary>
        /// <param name="buttonOrLinkElem">The element to click on</param>
        public dynamic ClickAndWait(IWebElement buttonOrLinkElem)
        {
            if (Browser.Exists(Bys.Legacy_ActAccreditationPage.AddAccreditationTypeLnk))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == this.AddAccreditationTypeLnk.GetAttribute("outerHTML"))
                {
                    WebElementExtensions.ClickJS(this.AddAccreditationTypeLnk, (IWebDriver)Browser);
                    Browser.WaitForElement(Bys.Legacy_ActAccreditationPage.AvailableAccreditationTypesTbl, ElementCriteria.IsVisible);
                    return null;
                }
            }

            if (Browser.Exists(Bys.Legacy_ActAccreditationPage.SelectProfAddBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == this.SelectProfAddBtn.GetAttribute("outerHTML"))
                {
                    WebElementExtensions.ClickJS(this.SelectProfAddBtn, (IWebDriver)Browser);
                    this.SelectProfAddBtn.Click();
                    // ToDo: Add wait criteria
                    Thread.Sleep(3000);
                    return null;
                }
            }

            if (Browser.Exists(Bys.Legacy_ActAccreditationPage.AvailableAccreditationTypesContinueBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == this.AvailableAccreditationTypesContinueBtn.GetAttribute("outerHTML"))
                {
                    WebElementExtensions.ClickJS(this.AvailableAccreditationTypesContinueBtn, (IWebDriver)Browser);
                    Browser.WaitForElement(Bys.Legacy_ActAccreditationPage.AddScenarioLnk, ElementCriteria.IsVisible);
                    return null;
                }
            }

            if (Browser.Exists(Bys.Legacy_ActAccreditationPage.AddScenarioLnk))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == this.AddScenarioLnk.GetAttribute("outerHTML"))
                {
                    WebElementExtensions.ClickJS(this.AddScenarioLnk, (IWebDriver)Browser);
                    Browser.WaitForElement(Bys.Legacy_ActAccreditationPage.ScenarioNameTxt, ElementCriteria.IsVisible);
                    return null;
                }
            }

            if (Browser.Exists(Bys.Legacy_ActAccreditationPage.AddScenarioSaveBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == this.AddScenarioSaveBtn.GetAttribute("outerHTML"))
                {
                    WebElementExtensions.ClickJS(this.AddScenarioSaveBtn, (IWebDriver)Browser);
                    Browser.WaitForElement(Bys.Legacy_ActAccreditationPage.AddScenarioLnk, ElementCriteria.IsVisible);
                    return null;
                }
            }

            if (Browser.Exists(Bys.Legacy_ActAccreditationPage.BackToAccreditationsLnk))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == this.BackToAccreditationsLnk.GetAttribute("outerHTML"))
                {
                    WebElementExtensions.ClickJS(this.BackToAccreditationsLnk, (IWebDriver)Browser);
                    this.WaitForInitialize();
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
        /// 
        /// </summary>
        /// <param name="accreditationBody"></param>
        /// <param name="accreditationType"></param>
        /// <param name="amountOFCredits"></param>
        /// <returns></returns>
        public Scenario AddScenarioToAccreditation(string accreditationBody, string accreditationType, int amountOfCredits)
        {
            // If we are not on the specific Accreditation's body page, then go there
            if (!Browser.Exists(Bys.Legacy_ActAccreditationPage.ScenariosTbl))
            {
                base.ClickAndWaitBasePage(TreeLinks_Accreditation);
                this.ClickAndWait(this.AddAccreditationTypeLnk);

                IWebElement row = ElemGet_LMSAdmin.Grid_GetRowByRowNameAndAdditionalCellName(Browser, (IWebElement)this.AvailableAccreditationTypesTbl,
                    Bys.Legacy_ActAccreditationPage.AvailableAccreditationTypesTblFirstRow, accreditationBody, "td", accreditationType, "td");

                ElemSet_LMSAdmin.Grid_ClickElementWithoutTextInsideRow(row, "input", "Radio");
            }

            ClickAndWait(AddScenarioLnk);

            // Fill in the scenario fields
            string timeStamp = string.Format("{0}", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture));
            string scenarioName = string.Format("AutoGen Scenario {0}", timeStamp);
            ScenarioNameTxt.SendKeys(scenarioName);

            ElemSet_LMSAdmin.SelElem_SelectAll(Browser, SelectProfAvailSelElem);

            ClickAndWait(SelectProfAddBtn);

            FixedCreditsTxt.SendKeys(amountOfCredits.ToString());

            // Save the scenario
            ClickAndWait(AddScenarioSaveBtn);

            // Go back to the main Accreditations page
            ClickAndWait(BackToAccreditationsLnk);

            return new Scenario(scenarioName, new List<AssessmentRule>(), new List<Award>());
        }

        #endregion methods: page specific



    }
}