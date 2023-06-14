using Browser.Core.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using LOG4NET = log4net.ILog;

namespace LS.AppFramework
{
    public class ParticipantsPage : Page, IDisposable
    {
        #region constructors
        public ParticipantsPage(IWebDriver driver) : base(driver)
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

        public IWebElement RegeneratePasswordTab { get { return this.FindElement(Bys.ParticipantsPage.RegeneratePasswordTab); } }
        public IWebElement ProgramsTabRecognitionForm { get { return this.FindElement(Bys.ParticipantsPage.ProgramsTabRecognitionForm); } }
        public IWebElement ProgramsTabRecognitionFormYesBtn { get { return this.FindElement(Bys.ParticipantsPage.ProgramsTabRecognitionFormYesBtn); } }
        public IWebElement ProgramsTabRecognitionFormEndDtTxt { get { return this.FindElement(Bys.ParticipantsPage.ProgramsTabRecognitionFormEndDtTxt); } }
        public IWebElement ProgramsTabRecognitionFormStartDtTxt { get { return this.FindElement(Bys.ParticipantsPage.ProgramsTabRecognitionFormStartDtTxt); } }
        public IWebElement ProgramsTabProgramTblBodyRow { get { return this.FindElement(Bys.ParticipantsPage.ProgramsTabProgramTblBodyRow); } }
        public IWebElement ProgramsTabProgramTbl { get { return this.FindElement(Bys.ParticipantsPage.ProgramsTabProgramTbl); } }
        public IWebElement ProgramsTab { get { return this.FindElement(Bys.ParticipantsPage.ProgramsTab); } }
        public IWebElement DetailsTabGuidLbl { get { return this.FindElement(Bys.ParticipantsPage.DetailsTabGuidLbl); } }

        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            try
            {
                this.WaitUntil(TimeSpan.FromSeconds(60), Criteria.ParticipantsPage.PageReady);
            }
            catch
            {
                RefreshPage();
            }

        }

        /// Refreshes the page and then uses the wait criteria that is found within WaitForInitialize to wait for the page to load.
        /// This is used as a catch block inside WaitForInitialize, in case the page doesnt load initially. Can also be used to 
        /// randomly refresh the page
        /// </summary>
        public void RefreshPage()
        {
            Browser.Navigate().Refresh();
            this.WaitUntil(TimeSpan.FromSeconds(60), Criteria.ParticipantsPage.PageReady);
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
        /// Clicks the user-specified element and then waits for a window to close or open, or an element to be visible or enabled
        /// </summary>
        /// <param name="buttonOrLinkElem">The element to click on</param>
        public dynamic ClickAndWait(IWebElement buttonOrLinkElem)
        {
            if (Browser.Exists(Bys.ParticipantsPage.ProgramsTab))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == ProgramsTab.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    this.WaitUntil(Criteria.ParticipantsPage.ProgramsTabProgramTblBodyRowVisible);
                    return null;
                }
            }



            if (Browser.Exists(Bys.ParticipantsPage.ProgramsTabRecognitionFormYesBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == ProgramsTabRecognitionFormYesBtn.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    this.WaitUntilAll(Criteria.ParticipantsPage.ProgramsTabRecognitionFormNotExists,
                        Criteria.ParticipantsPage.RecognitionProgramCycleDateChangeBannerNotExists);
                    // Adding a little sleep here. For some reason, whenever the code proceeds after clicking this button (which invokes the green banner at the top),
                    // the next line of code doesnt execute. For example, Navigate.GoToLoginPage. That code gets past the navigation part, but if you view the test in
                    // progress, no URL is entered into the URL. Another example I have code to click on the "Sites" tab in LTS after this, and the code goes past the
                    // Click line, but if you view the test, it didnt click anything. I have never seen this before. So far, I think it only happened in Debug mode.
                    // Monitor going forward
                    Thread.Sleep(0600);
                    return null;
                }
            }

            throw new Exception("No button or link was found with your passed parameter. You either need to add this button to a new If statement, or if the button is already added, then the page you were on did not contain the button.");
        }

        /// <summary>
        /// Clicks on a program inside the program table from the Program tab of the Participants page, then waits for 
        /// the Program page to load
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="programName">The exact text from the Programs table of the program that you want to click on</param>
        /// <returns></returns>
        internal ProgramPage ClickProgramAndWait(IWebDriver browser, string programName, string status = null)
        {
            if (ElemGet.Grid_ContainsRecord(browser, ProgramsTabProgramTbl, Bys.ParticipantsPage.ProgramsTabProgramTblBodyRow,
                1,status,"td"))
            {
                ElemSet.Grid_ClickButtonOrLinkWithinRow(browser, ProgramsTabProgramTbl, 
                    Bys.ParticipantsPage.ProgramsTabProgramTblBodyRow, programName, "a", programName, "a", status, "td");              
            }
            else if (ElemGet.Grid_ContainsRecord(browser, ProgramsTabProgramTbl, 
                Bys.ParticipantsPage.ProgramsTabProgramTblBodyRow, 1, "Complete", "td"))
            {
                 ElemSet.Grid_ClickButtonOrLinkWithinRow(browser, ProgramsTabProgramTbl, 
                    Bys.ParticipantsPage.ProgramsTabProgramTblBodyRow, programName, "a", programName, "a", "Complete", "td");
            }
            else
            {
                ElemSet.Grid_ClickButtonOrLinkWithinRow(browser, ProgramsTabProgramTbl,
                    Bys.ParticipantsPage.ProgramsTabProgramTblBodyRow, programName, "a", programName, "a", "In Progress", "td");
            }

            Thread.Sleep(1000);
            ProgramPage page = new ProgramPage(Browser);
            page.WaitForInitialize();
            return page;
        }

        /// <summary>
        /// Clicks on the Programs tab, clcks Actions>Adjust Dates link for the Maintenance Of Certification program, fills in a user-specified start or end date, 
        /// then clicks the Yes button
        /// </summary>
        /// <param name="startOrEndDate">"Start" or "End"</param>
        /// <param name="date">The date to enter</param>
        internal void AdjustProgramCycleDates(string startOrEndDate, string date)
        {
            ClickAndWait(ProgramsTab);

            IWebElement btn = ElemSet.Grid_HoverButtonOrLinkWithinRow(Browser, ProgramsTabProgramTbl, Bys.ParticipantsPage.ProgramsTabProgramTblBodyRow,
                "Maintenance of Certification", "a", "Actions", "span");
            Thread.Sleep(0500);

            IWebElement btnParent = XpathUtils.GetNthParentElem(btn, 3);

            ElemSet.Grid_ClickMenuItemInsideDropdown(Browser, btnParent, "Adjust Dates");
            Browser.WaitForElement(Bys.ParticipantsPage.ProgramsTabRecognitionFormStartDtTxt, ElementCriteria.IsVisible);

            if (startOrEndDate == "Start")
            {
                ProgramsTabRecognitionFormStartDtTxt.Clear();
                ProgramsTabRecognitionFormStartDtTxt.SendKeys(date);
            }
            else
            {
                ProgramsTabRecognitionFormEndDtTxt.Clear();
                ProgramsTabRecognitionFormEndDtTxt.SendKeys(date);
            }

            ClickAndWait(ProgramsTabRecognitionFormYesBtn);
        }


        #endregion methods: page specific



    }
}