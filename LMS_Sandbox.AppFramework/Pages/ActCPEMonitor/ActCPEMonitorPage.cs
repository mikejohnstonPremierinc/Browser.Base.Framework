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
    public class ActCPEMonitorPage : LMSPage, IDisposable
    {
        #region constructors
        public ActCPEMonitorPage(IWebDriver driver) : base(driver)
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

        public SelectElement MonthOfBirthSelElem { get { return new SelectElement(this.FindElement(Bys.ActCPEMonitorPage.MonthOfBirthSelElem)); } }
        public IWebElement MonthOfBirthSelElemBtn { get { return this.FindElement(Bys.ActCPEMonitorPage.MonthOfBirthSelElemBtn); } }
        public SelectElement DayOfBirthSelElem { get { return new SelectElement(this.FindElement(Bys.ActCPEMonitorPage.DayOfBirthSelElem)); } }
        public IWebElement DayOfBirthSelElemBtn { get { return this.FindElement(Bys.ActCPEMonitorPage.DayOfBirthSelElemBtn); } }
        public IWebElement SubmitBtn { get { return this.FindElement(Bys.ActCPEMonitorPage.SubmitBtn); } }
        public IWebElement IAttestChk { get { return this.FindElement(Bys.ActCPEMonitorPage.IAttestChk); } }
        public IWebElement NABPEProfileIDTxt { get { return this.FindElement(Bys.ActCPEMonitorPage.NABPEProfileIDTxt); } }


        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(90), Criteria.ActCPEMonitorPage.PageReady);
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

            if (Browser.Exists(Bys.ActCPEMonitorPage.SubmitBtn))
            {
                if (elem.GetAttribute("outerHTML") == SubmitBtn.GetAttribute("outerHTML"))
                {
                    SubmitBtn.Click(Browser);
                    return Help.WaitForNextPage(Browser, Constants.PageURLs.Activity_CPEConsent);
                }
            }

            if (Browser.Exists(Bys.ActCPEMonitorPage.IAttestChk))
            {
                if (elem.GetAttribute("outerHTML") == IAttestChk.GetAttribute("outerHTML"))
                {
                    IAttestChk.ClickJS(Browser);
                    Browser.WaitForElement(Bys.ActCPEMonitorPage.NABPEProfileIDTxt, ElementCriteria.IsNotVisible);
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
            if (Browser.Exists(Bys.ActCPEMonitorPage.MonthOfBirthSelElem))
            {
                if (selectElement.Options[3].Text == MonthOfBirthSelElem.Options[3].Text)
                {
                    if (Browser.MobileEnabled() || Browser.GetCapabilities().GetCapability("browserName").ToString() == BrowserNames.Firefox)
                    {
                        ElemSet.DropdownMulti_Fireball_SelectByText(Browser, MonthOfBirthSelElemBtn, selection);
                    }
                    else
                    {
                        MonthOfBirthSelElem.SelectByText(selection);
                    }
                    this.WaitForInitialize();
                    return null;
                }
            }

            if (Browser.Exists(Bys.ActCPEMonitorPage.DayOfBirthSelElem))
            {
                if (selectElement.Options[3].Text == DayOfBirthSelElem.Options[3].Text)
                {
                    if (Browser.MobileEnabled() || Browser.GetCapabilities().GetCapability("browserName").ToString() == BrowserNames.Firefox)
                    {
                        ElemSet.DropdownMulti_Fireball_SelectByText(Browser, DayOfBirthSelElemBtn, selection);
                    }
                    else
                    {
                        DayOfBirthSelElem.SelectByText(selection);
                    }
                    this.WaitForInitialize();
                    return null;
                }
            }

            throw new Exception(string.Format("No element was found with your passed parameter. You either need to add " +
                "this element to a new If statement, or if the element is already added, then the page you were on did not contain the element."));
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
        /// Early stages of this method.Right now, we are just going to click the required CHES radio button, then click Submit
        /// </summary>
        public dynamic FillAndSubmitForm()
        {
            if (Browser.Exists(Bys.ActCPEMonitorPage.NABPEProfileIDTxt))
            {
                if (NABPEProfileIDTxt.GetAttribute("value") == "")
                {
                    NABPEProfileIDTxt.SendKeys(DataUtils.GetRandomIntegerWithinRange(1000000, 90000000).ToString());
                }
            }

            if (Browser.Exists(Bys.ActCPEMonitorPage.MonthOfBirthSelElem))
            {
                SelectAndWait(MonthOfBirthSelElem, MonthOfBirthSelElem.Options[3].Text);
            }

            if (Browser.Exists(Bys.ActCPEMonitorPage.DayOfBirthSelElem))
            {
                SelectAndWait(DayOfBirthSelElem, DayOfBirthSelElem.Options[3].Text);
            }

            return ClickAndWait(SubmitBtn);
        }

        #endregion methods: page specific



    }
}