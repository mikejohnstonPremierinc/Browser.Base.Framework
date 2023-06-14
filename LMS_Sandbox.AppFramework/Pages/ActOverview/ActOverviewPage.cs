using Browser.Core.Framework;
using LMS.AppFramework.LMSHelperMethods;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using LMS.Data;
using System.Threading;
using LOG4NET = log4net.ILog;

namespace LMS.AppFramework
{
    public class ActOverviewPage : LMSPage, IDisposable
    {
        #region constructors
        public ActOverviewPage(IWebDriver driver) : base(driver)
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

        public IWebElement ContinueBtn { get { return this.FindElement(Bys.ActOverviewPage.ContinueBtn); } }
        //public IWebElement PleaseClickHereChk { get { return this.FindElement(Bys.ActOverviewPage.PleaseClickHereChk); } }
        public IWebElement ActivityOverviewChk { get { return this.FindElement(Bys.ActOverviewPage.ActivityOverviewChk); } }
        public IWebElement FrontMatterLbl { get { return this.FindElement(Bys.ActOverviewPage.FrontMatterLbl); } }
        public IWebElement FinishBtn { get { return this.FindElement(Bys.ActAssessmentPage.FinishBtn); } }
        public IWebElement CityStateZipCountryLbl { get { return this.FindElement(Bys.ActOverviewPage.CityStateZipCountryLbl); } }
        public IWebElement StreetAddressLbl { get { return this.FindElement(Bys.ActOverviewPage.StreetAddressLbl); } }
        public IWebElement StartDateValueLbl { get { return this.FindElement(Bys.ActOverviewPage.StartDateValueLbl); } }
        public IWebElement EndDateValueLbl { get { return this.FindElement(Bys.ActOverviewPage.EndDateValueLbl); } }
        public IList<IWebElement> AccreditationBodyNameLbls { get { return this.FindElements(Bys.ActOverviewPage.AccreditationBodyNameLbls); } }
        public IList<IWebElement> AccreditationBodyName_NONACCRLbl { get { return this.FindElements(Bys.ActOverviewPage.AccreditationBodyName_NONACCRLbl); } }

        public IList<IWebElement> AccreditationRows { get { return this.FindElements(Bys.ActOverviewPage.AccreditationRows); } }
        public IWebElement ReleaseDateValueLbl { get { return this.FindElement(Bys.ActOverviewPage.ReleaseDateValueLbl); } }
        public IWebElement ConfirmWithCheckBoxLbl { get { return this.FindElement(Bys.ActOverviewPage.ConfirmWithCheckBoxLbl); } }
        public IWebElement ExpirationDateValueLbl { get { return this.FindElement(Bys.ActOverviewPage.ExpirationDateValueLbl); } }
        
        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(90), Criteria.ActOverviewPage.PageReady);
            Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));
        }

        /// <summary>
        /// Clicks the user-specified element and then waits for a window to close or open, or a page to load,
        /// depending on the element that was clicked
        /// </summary>
        /// <param name="elem">The element to click on</param>
        public dynamic ClickAndWait(IWebElement elem)
        {
            if (Browser.Exists(Bys.ActOverviewPage.ActivityOverviewChk))
            {
                if (elem.GetAttribute("outerHTML") == ActivityOverviewChk.GetAttribute("outerHTML"))
                {
                    ActivityOverviewChk.Click(Browser);
                    Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));
                    return Help.WaitForNextPage(Browser, Constants.PageURLs.Activity_Overview);
                }
            }

            if (Browser.Exists(Bys.ActOverviewPage.ContinueBtn))
            {
                if (elem.GetAttribute("outerHTML") == ContinueBtn.GetAttribute("outerHTML"))
                {
                    // Using javascript click here for the following reason. When we use a regular click, IE then doesnt load 
                    // the page fully for some reason. This is not reproducable manually
                    ContinueBtn.ClickJS(Browser);
                    Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));

                    return Help.WaitForNextPage(Browser, Constants.PageURLs.Activity_Overview);             
                }
            }


            if (Browser.Exists(Bys.ActAssessmentPage.FinishBtn))
            {
                if (elem.GetAttribute("outerHTML") == FinishBtn.GetAttribute("outerHTML"))
                {
                    FinishBtn.Click(Browser);
                    TranscriptPage Page = new TranscriptPage(Browser);
                    Page.WaitForInitialize();
                    Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));
                    return Page;
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
            catch (Exception ex) { _log.ErrorFormat("Failed to dispose ActOverviewPage", activeRequests.Count, ex); }
        }

        #endregion methods: repeated per page

        #region methods: page specific

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Constants.Activity GetActivityDetails()
        {
            Constants.Activity Activity = new Constants.Activity();
            Constants.AddressAndLocation addAndLoc = null; ;

            // Set title and front matter
            Activity.ActivityTitle = ActivityTitleLbl.Text;
            Activity.FrontMatter = FrontMatterLbl.Text;
            Activity.Release_Date = ReleaseDateValueLbl.Text;
            Activity.Expiration_Date = ExpirationDateValueLbl.Text;

            // Set accreditations
            List<Constants.Accreditation> Accreditations = new List<Constants.Accreditation>();
            for (var i = 0; i < AccreditationRows.Count; i++)
            {
                Constants.Accreditation Accreditation = new Constants.Accreditation();

                // If there is no accreditation, then the row will not have an element for AccreditationBodyNameLbls in this row,
                // it will have a different xpath for this NONACCR element. So we will manually insert 'NONACCR' if that is the case
                Accreditation.BodyName = AccreditationRows[i].Exists(Bys.ActOverviewPage.AccreditationBodyNameLbls)
                    ? AccreditationBodyNameLbls[i].Text.Substring(2)
                    : "NONACCR";

                Accreditation.CreditAmount = double.Parse(AccreditationRows[i].FindElement(By.XPath("descendant::b")).Text);
                Accreditation.CreditUnit = AccreditationRows[i].FindElement(By.XPath("descendant::span")).Text;

                Accreditations.Add(Accreditation);
            }

            Accreditations.ToList().OrderBy(x => x.BodyName);

            Activity.Accreditations = Accreditations.ToList();

            // Set location if applicable
            if (Browser.Exists(Bys.ActOverviewPage.StartDateValueLbl))
            {
                addAndLoc = new Constants.AddressAndLocation();

                addAndLoc.Addr_Line_01 = StreetAddressLbl.Text;
                addAndLoc.StartDate = StartDateValueLbl.Text;
                addAndLoc.EndDate = EndDateValueLbl.Text;
            }

            Activity.AddressAndLocation = addAndLoc;

            return Activity;
        }

        #endregion methods: page specific



    }
}