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

namespace LMS.AppFramework
{
    public class TranscriptPage : LMSPage, IDisposable
    {
        #region constructors
        public TranscriptPage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that WE start so we can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        public override string PageUrl { get { return "lms/transcript"; } }

        #endregion properties

        #region elements

        
        public IWebElement PrintHoldingAreaReportIFrame { get { return this.FindElement(Bys.TranscriptPage.PrintHoldingAreaReportIFrame); } }

        public IWebElement PrintHoldingAreaReportFormRunReportBtn { get { return this.FindElement(Bys.TranscriptPage.PrintHoldingAreaReportFormRunReportBtn); } }
        public IWebElement ConfirmFormOkBtn { get { return this.FindElement(Bys.TranscriptPage.ConfirmFormOkBtn); } }
        public IWebElement DeleteBtn { get { return this.FindElement(Bys.TranscriptPage.DeleteBtn); } }
        public IWebElement PrintHoldingAreaReportFormCloseBtn { get { return this.FindElement(Bys.TranscriptPage.PrintHoldingAreaReportFormCloseBtn); } }
        public IWebElement ActivityDetailsFormAddBtn { get { return this.FindElement(Bys.TranscriptPage.ActivityDetailsFormAddBtn); } }
        public IWebElement PrintHoldingAreaReportBtn { get { return this.FindElement(Bys.TranscriptPage.PrintHoldingAreaReportBtn); } }
        public IWebElement PrintReportBtn { get { return this.FindElement(Bys.TranscriptPage.PrintReportBtn); } }
        public IWebElement HoldingAreaTab { get { return this.FindElement(Bys.TranscriptPage.HoldingAreaTab); } }
        public IWebElement FromDateLbl { get { return this.FindElement(Bys.TranscriptPage.FromDateLbl); } }
        public IWebElement MyActivityBtn { get { return this.FindElement(Bys.TranscriptPage.MyActivityBtn); } }
        public IWebElement ToDateLbl { get { return this.FindElement(Bys.TranscriptPage.ToDateLbl); } }
        public IWebElement AddAnActivityLnk { get { return this.FindElement(Bys.TranscriptPage.AddAnActivityLnk); } }
        public IWebElement FilterByDateBtn { get { return this.FindElement(Bys.TranscriptPage.FilterByDateBtn); } }
        public IWebElement ResetFiltersBtn { get { return this.FindElement(Bys.TranscriptPage.ResetFiltersBtn); } }
        public IWebElement TranscriptLbl { get { return this.FindElement(Bys.TranscriptPage.TranscriptLbl); } }
        public IWebElement ActivitiesTbl { get { return this.FindElement(Bys.TranscriptPage.ActivitiesTbl); } }
        public IWebElement ActivitiesTblBody { get { return this.FindElement(Bys.TranscriptPage.ActivitiesTblBody); } }

        public IWebElement ActivitiesTblFirstLnk { get { return this.FindElement(Bys.TranscriptPage.ActivitiesTblFirstLnk); } }

        public IList<IWebElement> ActivitiesTblExpandBtns { get { return this.FindElements(Bys.TranscriptPage.ActivitiesTblExpandBtns); } }

        public IWebElement ActDetailsFormUploadElem { get { return this.FindElement(Bys.TranscriptPage.ActDetailsFormUploadElem); } }
        public IWebElement ActDetailsFormLocationOnlineRdo { get { return this.FindElement(Bys.TranscriptPage.ActDetailsFormLocationOnlineRdo); } }
        public IWebElement ActDetailsFormLocationLiveRdo { get { return this.FindElement(Bys.TranscriptPage.ActDetailsFormLocationLiveRdo); } }
        public IWebElement ActDetailsFormAccreditationProviderSelElem { get { return this.FindElement(Bys.TranscriptPage.ActDetailsFormAccreditationProviderSelElem); } }
        public IWebElement ActDetailsFormAccreditationProviderSelElemBtn { get { return this.FindElement(Bys.TranscriptPage.ActDetailsFormAccreditationProviderSelElemBtn); } }
        public IWebElement ActDetailsFormUnitsSelElem { get { return this.FindElement(Bys.TranscriptPage.ActDetailsFormUnitsSelElem); } }
        public IWebElement ActDetailsFormUnitsSelElemBtn { get { return this.FindElement(Bys.TranscriptPage.ActDetailsFormUnitsSelElemBtn); } }
        public IWebElement ActDetailsFormActNameTxt { get { return this.FindElement(Bys.TranscriptPage.ActDetailsFormActNameTxt); } }
        public IWebElement ActDetailsFormCompletedDateTxt { get { return this.FindElement(Bys.TranscriptPage.ActDetailsFormCompletedDateTxt); } }
        public IWebElement ActDetailsFormReferenceTxt { get { return this.FindElement(Bys.TranscriptPage.ActDetailsFormReferenceTxt); } }
        public IWebElement ActDetailsFormCreditAmountTxt { get { return this.FindElement(Bys.TranscriptPage.ActDetailsFormCreditAmountTxt); } }
        public IWebElement ActDetailsFormCreditTypeTxt { get { return this.FindElement(Bys.TranscriptPage.ActDetailsFormCreditTypeTxt); } }

        #endregion elements

        #region methods: repeated per page


        /// <summary>
        /// Clicks the user-specified element and then waits for a window to close or open, or a page to load,
        /// depending on the element that was clicked
        /// </summary>
        /// <param name="elem">The element to click on</param>
        public dynamic ClickAndWait(IWebElement elem)
        {
            
            if (Browser.Exists(Bys.TranscriptPage.ActivityDetailsFormAddBtn))
            {
                if (elem.GetAttribute("outerHTML") == ActivityDetailsFormAddBtn.GetAttribute("outerHTML"))
                {
                    ActivityDetailsFormAddBtn.Click(Browser);
                    Browser.WaitForElement(Bys.TranscriptPage.MyActivityBtn, ElementCriteria.IsVisible);
                    Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));
                    return null;
                }
            }

            if (Browser.Exists(Bys.TranscriptPage.DeleteBtn))
            {
                if (elem.GetAttribute("outerHTML") == DeleteBtn.GetAttribute("outerHTML"))
                {
                    DeleteBtn.Click(Browser);
                    Browser.WaitForElement(Bys.TranscriptPage.ConfirmFormOkBtn, ElementCriteria.IsVisible);
                    Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));
                    return null;
                }
            }

            if (Browser.Exists(Bys.TranscriptPage.PrintHoldingAreaReportFormCloseBtn))
            {
                if (elem.GetAttribute("outerHTML") == PrintHoldingAreaReportFormCloseBtn.GetAttribute("outerHTML"))
                {
                    PrintHoldingAreaReportFormCloseBtn.Click(Browser);
                    Browser.WaitForElement(Bys.TranscriptPage.MyActivityBtn, ElementCriteria.IsVisible);
                    Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));
                    return null;
                }
            }

            if (Browser.Exists(Bys.TranscriptPage.PrintHoldingAreaReportBtn))
            {
                if (elem.GetAttribute("outerHTML") == PrintHoldingAreaReportBtn.GetAttribute("outerHTML"))
                {
                    PrintHoldingAreaReportBtn.Click(Browser);
                    IWebElement PrintReportFrame = Browser.WaitForElement(Bys.TranscriptPage.PrintHoldingAreaReportIFrame);
                    Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));
                    return null;
                }
            }

            if (Browser.Exists(Bys.TranscriptPage.ConfirmFormOkBtn))
            {
                if (elem.GetAttribute("outerHTML") == ConfirmFormOkBtn.GetAttribute("outerHTML"))
                {
                    ConfirmFormOkBtn.Click(Browser);
                    Browser.WaitForElement(Bys.TranscriptPage.MyActivityBtn, ElementCriteria.IsVisible);
                    Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));
                    return null;
                }
            }

            if (Browser.Exists(Bys.TranscriptPage.MyActivityBtn))
            {
                if (elem.GetAttribute("outerHTML") == MyActivityBtn.GetAttribute("outerHTML"))
                {
                    MyActivityBtn.Click(Browser);
                    Browser.WaitForElement(Bys.TranscriptPage.ActDetailsFormCompletedDateTxt, ElementCriteria.IsVisible);
                    Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));
                    return null;
                }
            }

            throw new Exception(string.Format("No element was found with your passed parameter, which was the '{0}' element. You either need to add " +
                "this element to a new If statement, or if the element is already added, then the page you were on did not contain the element.",
                elem.GetAttribute("innerText")));
        }

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(90), Criteria.TranscriptPage.PageReady);
            Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));
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

        public List<Constants.Transcript> GetTranscript(Constants.SiteCodes siteCode)
        {
            List<Constants.Transcript> Transcripts = new List<Constants.Transcript>();

            string activityTitleXPath = "descendant::*[@class='hyperlink']";
            string cityStateCountryXPath = "descendant::span[1]";
            string activityTypeXPath = "descendant::span[2]";
            string creditBodyXPath = "descendant::td[4]";
            string creditAmountAndUnitXPath = "descendant::td[5]";
            string completionDateXPath = "descendant::td[6]";
            string certificateGeneratedXPath = "descendant::div[contains(@class, 'icon-credit')]";

            // SNMMI has an additional column and doesnt include the address underneath the activity title            
            switch (siteCode)
            {
                case Constants.SiteCodes.SNMMI:
                    // MJ 7/21/20: This is not the case anymore for Activity Type. Dont know why it was changed
                    // activityTypeXPath = "descendant::span[1]";
                    // Per Srilu, she thinks SNMMI doesnt want an address on their Transcript page
                    cityStateCountryXPath = "//tr";
                    completionDateXPath = "//tr/descendant::td[7]";
                    break;
            }

            // Loop through each row
            foreach (var Row in ActivitiesTblBody.FindElements(By.XPath("tr")))
            {
                Constants.Transcript Transcript = new Constants.Transcript();

                Transcript.ActivityTitle = Row.FindElement(By.XPath(activityTitleXPath)).Text;
                // Per Srilu, she thinks SNMMI doesnt want an address on their Transcript page
                Transcript.CityStateCountry = Row.FindElement(By.XPath(cityStateCountryXPath)).Text.Trim();
                Transcript.ActivityType = Row.FindElement(By.XPath(activityTypeXPath)).Text;
                Transcript.CreditBody = Row.FindElement(By.XPath(creditBodyXPath)).Text.Trim();
                Transcript.CreditAmountAndUnit = Row.FindElement(By.XPath(creditAmountAndUnitXPath)).Text.Trim();
                Transcript.CompletionDate = Row.FindElement(By.XPath(completionDateXPath)).Text.Trim();
                Transcript.CertificateGenerated = Row.FindElements(By.XPath(certificateGeneratedXPath)).Count == 1 ? true : false;

                Transcripts.Add(Transcript);
            }

            return Transcripts;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actName"></param>
        /// <param name="completeDate"></param>
        /// <param name="accProvider"></param>
        /// <param name="refNum"></param>
        /// <param name="creditAmt"></param>
        /// <param name="units"></param>
        /// <param name="creditType"></param>
        /// <param name="fullFilePath"></param>
        /// <returns></returns>
        public Constants.MyActivity AddMyActivity(string actName, string completeDate, string accProvider, string refNum, 
            string creditAmt, string units = null, string creditType = null, string fullFilePath = null)
        {
            ClickAndWait(MyActivityBtn);

            ActDetailsFormActNameTxt.SendKeys(actName);
            ActDetailsFormCompletedDateTxt.SendKeys(completeDate);
            ElemSet.DropdownSingle_Fireball_SelectByText(Browser, ActDetailsFormAccreditationProviderSelElemBtn, accProvider);
            ActDetailsFormReferenceTxt.SendKeys(refNum);
            ActDetailsFormCreditAmountTxt.Clear();
            ActDetailsFormCreditAmountTxt.SendKeys(creditAmt);

            if (!string.IsNullOrEmpty(fullFilePath))
            {
                if (fullFilePath == "Live")
                {
                    ActDetailsFormLocationLiveRdo.Click();
                }
                else
                {
                    ActDetailsFormLocationOnlineRdo.Click();
                    Browser.WaitForElement(Bys.TranscriptPage.ActDetailsFormCityTxt);
                }
            }

            FileUtils.UploadFileUsingSendKeys(Browser, ActDetailsFormUploadElem, fullFilePath);

            return ClickAndWait(ActivityDetailsFormAddBtn);
        }

        /// <summary>
        /// Clicks on a user-specified activity, then waits for the Activity Overview page to load
        /// </summary>
        /// <param name="activityName"></param>
        /// <returns></returns>
        public dynamic ClickActivity(string activityName)
        {
            ElemSet.Grid_ClickButtonOrLinkWithinRow(Browser, ActivitiesTbl, Bys.TranscriptPage.ActivitiesTblFirstLnk,
                activityName, "a", activityName, "a");

            // Wait until the page URL loads
            var wait = new WebDriverWait(Browser, TimeSpan.FromSeconds(91));
            wait.Until(Browser => { return Browser.Url.Contains(Constants.PageURLs.Activity_Overview.GetDescription()); });

            ActOverviewPage OP = new ActOverviewPage(Browser);
            OP.WaitForInitialize();
            Thread.Sleep(300);
            return OP;
        }

        #endregion methods: page specific



    }
}