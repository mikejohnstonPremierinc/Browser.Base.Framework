using Browser.Core.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using LOG4NET = log4net.ILog;

namespace LS.AppFramework
{
    public class ProgramPage : Page, IDisposable
    {
        #region constructors
        public ProgramPage(IWebDriver driver) : base(driver)
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
        public IWebElement DetailsTab { get { return this.FindElement(Bys.ProgramPage.DetailsTab); } }
        public IWebElement ProgAdjustTabAddAdjustFormEffectiveDtTxt { get { return this.FindElement(Bys.ProgramPage.ProgAdjustTabAddAdjustFormEffectiveDtTxt); } }
        public IWebElement ProgAdjustTabAddAdjustFormIsVoluntNoRdo { get { return this.FindElement(Bys.ProgramPage.ProgAdjustTabAddAdjustFormIsVoluntNoRdo); } }
        public IWebElement ProgAdjustTabAddAdjustFormIsVoluntYesRdo { get { return this.FindElement(Bys.ProgramPage.ProgAdjustTabAddAdjustFormIsVoluntYesRdo); } }
        public SelectElement ProgAdjustTabAddAdjustFormLeaveCodeSelElem { get { return new SelectElement(this.FindElement(Bys.ProgramPage.ProgAdjustTabAddAdjustFormLeaveCodeSelElem)); } }
        public IWebElement ProgAdjustTabAddAdjustFormLeaveEndDtTxt { get { return this.FindElement(Bys.ProgramPage.ProgAdjustTabAddAdjustFormLeaveEndDtTxt); } }
        public IWebElement ProgAdjustTabAddAdjustFormLeaveStartDtTxt { get { return this.FindElement(Bys.ProgramPage.ProgAdjustTabAddAdjustFormLeaveStartDtTxt); } }
        public IWebElement ProgAdjustTabAddAdjustLnk { get { return this.FindElement(Bys.ProgramPage.ProgAdjustTabAddAdjustLnk); } }
        public IWebElement ProgAdjustTabAddAdjustFormIsIntnlNoRdo { get { return this.FindElement(Bys.ProgramPage.ProgAdjustTabAddAdjustFormIsIntnlNoRdo); } }
        public IWebElement ProgAdjustTabAddAdjustFormIsIntnlYesRdo { get { return this.FindElement(Bys.ProgramPage.ProgAdjustTabAddAdjustFormIsIntnlYesRdo); } }
        public IWebElement ProgAdjustTabAddAdjustFormAddAdjustBtn { get { return this.FindElement(Bys.ProgramPage.ProgAdjustTabAddAdjustFormAddAdjustBtn); } }
        public SelectElement ProgAdjustTabAddAdjustFormAdjustCodeSelElem { get { return new SelectElement(this.FindElement(Bys.ProgramPage.ProgAdjustTabAddAdjustFormAdjustCodeSelElem)); } }
        public SelectElement ProgAdjustTabAddAdjustFormAdjustCycleSelElem { get { return new SelectElement(this.FindElement(Bys.ProgramPage.ProgAdjustTabAddAdjustFormAdjustCycleSelElem)); } }
        public IWebElement ProgramAdjustmentsTab { get { return this.FindElement(Bys.ProgramPage.ProgramAdjustmentsTab); } }
        public IWebElement ProgAdjustTabAddAdjustFormAdjustCycleDateTxt { get { return this.FindElement(Bys.ProgramPage.ProgAdjustTabAddAdjustFormAdjustCycleDateTxt); } }
        public IWebElement DetailsTabProgramValueLbl { get { return this.FindElement(Bys.ProgramPage.DetailsTabProgramValueLbl); } }
        public IWebElement DetailsTabNameValueLbl { get { return this.FindElement(Bys.ProgramPage.DetailsTabNameValueLbl); } }
        public IWebElement DetailsTabStatusValueLbl { get { return this.FindElement(Bys.ProgramPage.DetailsTabStatusValueLbl); } }
        public IWebElement DetailsTabStartsValueLbl { get { return this.FindElement(Bys.ProgramPage.DetailsTabStartsValueLbl); } }
        public IWebElement DetailsTabEndsValueLbl { get { return this.FindElement(Bys.ProgramPage.DetailsTabEndsValueLbl); } }
        public IWebElement SelfReportActTab { get { return this.FindElement(Bys.ProgramPage.SelfReportActTab); } }
        public IWebElement SelfReportActTabValidStatusSelElem { get { return this.FindElement(Bys.ProgramPage.SelfReportActTabValidStatusSelElem); } }
        public IWebElement SelfReportActTabValidActivityTbl { get { return this.FindElement(Bys.ProgramPage.SelfReportActTabActivityTbl); } }
        public IWebElement SelfReportActTabValidActivityTblBody { get { return this.FindElement(Bys.ProgramPage.SelfReportActTabActivityTblBody); } }
        public IWebElement CreditValidationAcceptRdo { get { return this.FindElement(Bys.ProgramPage.CreditValidationAcceptRdo); } }
        public IWebElement CreditValidationRejectRdo { get { return this.FindElement(Bys.ProgramPage.CreditValidationRejectRdo); } }
        public IWebElement NeedsMoreInformationRdo { get { return this.FindElement(Bys.ProgramPage.NeedsMoreInformationRdo); } }
        public IWebElement CreditValidationSubmitBtn { get { return this.FindElement(Bys.ProgramPage.CreditValidationSubmitBtn); } }
        public IWebElement ApplyRecognitionCOCLnk { get { return this.FindElement(Bys.ProgramPage.ApplyRecognitionCOCLnk); } }
        public IWebElement ApplyCarryOverCreditsBtn { get { return this.FindElement(Bys.ProgramPage.ApplyCarryOverCreditsBtn); } }
        public IWebElement ProgramAdjustmentsActivityTbl { get { return this.FindElement(Bys.ProgramPage.ProgramAdjustmentsActivityTbl); } }
        public IWebElement ProgramAdjustmentsActivityTblBody { get { return this.FindElement(Bys.ProgramPage.ProgramAdjustmentsActivityTblBody); } }
        public IWebElement ReevaluateBtn { get { return this.FindElement(Bys.ProgramPage.ReevaluateBtn); } }
        public IWebElement ReevaluateLnk { get { return this.FindElement(Bys.ProgramPage.ReevaluateLnk); } }
        public IWebElement CommentTxt { get { return this.FindElement(Bys.ProgramPage.CommentTxt); } }

        
        #endregion elements

        #region methods: repeated per page

        /// <summary>
        /// Selects an item from a user-specified select element, then waits for a criteria to load fully
        /// </summary>
        /// <param name="selectElement">The select element to manipulate</param>
        /// <param name="selection">The exact text you want to choose from the item in the select elements</param>
        /// <returns></returns>
        public dynamic SelectAndWait(SelectElement selectElement, string selection)
        {
            if (Browser.Exists(Bys.ProgramPage.ProgAdjustTabAddAdjustFormAdjustCycleSelElem))
            {
                if (selectElement.Options[1].Text == ProgAdjustTabAddAdjustFormAdjustCycleSelElem.Options[1].Text)
                {
                    ProgAdjustTabAddAdjustFormAdjustCycleSelElem.SelectByText(selection);
                    Thread.Sleep(1000);
                    return null;
                }
            }

            if (Browser.Exists(Bys.ProgramPage.ProgAdjustTabAddAdjustFormAdjustCodeSelElem))
            {
                if (selectElement.Options[1].Text == ProgAdjustTabAddAdjustFormAdjustCodeSelElem.Options[1].Text)
                {
                    ProgAdjustTabAddAdjustFormAdjustCodeSelElem.SelectByText(selection);
                    Thread.Sleep(1000);
                    return null;
                }
            }
                 
            throw new Exception(string.Format("No element was found with your passed parameter. You either need to add " +
                "this element to a new If statement, or if the element is already added, then the page you were on did not contain the element."));
        }

        public override void WaitForInitialize()
        {
            try
            {
                this.WaitUntil(TimeSpan.FromSeconds(60), Criteria.ProgramPage.PageReady);
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
            this.WaitUntil(TimeSpan.FromSeconds(60), Criteria.ProgramPage.PageReady);
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
            if (Browser.Exists(Bys.ProgramPage.SelfReportActTab))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == SelfReportActTab.GetAttribute("outerHTML"))
                {
                    ElemSet.ClickAfterScroll(Browser, buttonOrLinkElem);
                    this.WaitUntil(Criteria.ProgramPage.SelfReportActTabValidActivityTblBodyVisible);
                    return null;
                }
            }

            if (Browser.Exists(Bys.ProgramPage.ProgramAdjustmentsTab))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == ProgramAdjustmentsTab.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    this.WaitUntil(Criteria.ProgramPage.ProgramAdjustmentsActivityTblBodyRowVisible);
                    return null;
                }
            }

            if (Browser.Exists(Bys.ProgramPage.DetailsTab))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == DetailsTab.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    this.WaitUntil(Criteria.ProgramPage.DetailsTabStatusValueLblVisible);
                    return null;
                }
            }

            if (Browser.Exists(Bys.ProgramPage.ProgAdjustTabAddAdjustLnk))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == ProgAdjustTabAddAdjustLnk.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    this.WaitUntil(Criteria.ProgramPage.ProgAdjustTabAddAdjustFormAdjustCodeSelElemVisible);
                    return null;
                }
            }

            if (Browser.Exists(Bys.ProgramPage.ApplyRecognitionCOCLnk))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == ApplyRecognitionCOCLnk.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    this.WaitUntilAll(TimeSpan.FromSeconds(120), Criteria.ProgramPage.ApplyCarryOverCreditsButtonVisibleAndEnable);                   
                    //Browser.WaitForElement(Bys.ProgramPage.ApplyCarryOverCreditsBtn, ElementCriteria.IsVisible, ElementCriteria.IsEnabled);
                    return null;
                }
            }

            if (Browser.Exists(Bys.ProgramPage.ApplyCarryOverCreditsBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == ApplyCarryOverCreditsBtn.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    //this.WaitUntil(TimeSpan.FromSeconds(60), Criteria.ProgramPage.RecognitionCarryOverGreenTextlabelAppeared);
                    //this.WaitUntil(TimeSpan.FromSeconds(60),Criteria.ProgramPage.RecognitionCarryOverGreenTextlabelDisappeared);
                    this.WaitUntilAll(TimeSpan.FromSeconds(180), Criteria.ProgramPage.ProgAdjustTabAddAdjustFormNotExists,
                       Criteria.ProgramPage.GreenBannerNotExists);
                    this.WaitForInitialize();                    
                    return null;
                }
            }

            if (Browser.Exists(Bys.ProgramPage.ProgAdjustTabAddAdjustFormAddAdjustBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == ProgAdjustTabAddAdjustFormAddAdjustBtn.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();

                    try
                    {
                        this.WaitUntilAll(TimeSpan.FromSeconds(120), Criteria.ProgramPage.ProgAdjustTabAddAdjustFormNotExists,
                            Criteria.ProgramPage.GreenBannerNotExists);
                        // this.WaitUntil(Criteria.ProgramPage.ProgramAdjustmentsActivityTblBodyRowVisible);
                        // Adding a little sleep here. For some reason, whenever the code proceeds after clicking this button, the next
                        // line of code doesnt execute. For example, Navigate.GoToLoginPage. That code gets past the navigation
                        // part, but if you view the test in progress, no URL is entered into the URL. Another example I have
                        // code to click on the "Sites" tab in LTS after this, and the code goes past the Click line, but if you view
                        // the test, it didnt click anything. I have never seen this before. So far, I think it only happened
                        // in Debug mode. Monitor going forward
                        Thread.Sleep(1000);
                    }
                    catch 
                    {
                        buttonOrLinkElem.ClickJS(Browser);
                        this.WaitUntilAll(TimeSpan.FromSeconds(120), Criteria.ProgramPage.ProgAdjustTabAddAdjustFormNotExists,
                            Criteria.ProgramPage.GreenBannerNotExists);
                        // this.WaitUntil(Criteria.ProgramPage.ProgramAdjustmentsActivityTblBodyRowVisible);
                        // Adding a little sleep here. For some reason, whenever the code proceeds after clicking this button, the next
                        // line of code doesnt execute. For example, Navigate.GoToLoginPage. That code gets past the navigation
                        // part, but if you view the test in progress, no URL is entered into the URL. Another example I have
                        // code to click on the "Sites" tab in LTS after this, and the code goes past the Click line, but if you view
                        // the test, it didnt click anything. I have never seen this before. So far, I think it only happened
                        // in Debug mode. Monitor going forward
                        Thread.Sleep(1000);
                    }

                    return null;
                }
            }

            if (Browser.Exists(Bys.ProgramPage.CreditValidationSubmitBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == CreditValidationSubmitBtn.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    this.WaitUntil(Criteria.ProgramPage.SelfReportActTabValidActivityTblBodyVisible);
                    Thread.Sleep(0500);
                    return null;
                }
            }

            if (Browser.Exists(Bys.ProgramPage.ReevaluateLnk))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == ReevaluateLnk.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click(Browser);
                    Browser.WaitForElement(Bys.ProgramPage.ReevaluateBtn, ElementCriteria.IsVisible);
                    return null;
                }
            }

            if (Browser.Exists(Bys.ProgramPage.ReevaluateBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == ReevaluateBtn.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    Browser.WaitForElement(Bys.ProgramPage.DetailsTabProgramValueLbl, ElementCriteria.IsVisible);
                    //this.WaitUntil(TimeSpan.FromSeconds(60), Criteria.ProgramPage.RecognitionCarryOverGreenTextlabelAppeared);
                    //this.WaitUntil(TimeSpan.FromSeconds(60),Criteria.ProgramPage.RecognitionCarryOverGreenTextlabelDisappeared);
                    this.WaitUntilAll(TimeSpan.FromSeconds(180), Criteria.ProgramPage.ProgAdjustTabAddAdjustFormNotExists,
                       Criteria.ProgramPage.GreenBannerNotExists);
                    return null;
                }
            }

            throw new Exception("No button or link was found with your passed parameter. You either need to add this button to a new If statement, or if the button is already added, then the page you were on did not contain the button.");
        }     

        /// <summary>
        /// 
        /// </summary>
        internal void ClickApplyRecognitionCOCAndApplyCarryOverCredits()
        {
            ClickAndWait(ApplyRecognitionCOCLnk);
            Thread.Sleep(1000);
            ClickAndWait(ApplyCarryOverCreditsBtn);
        }

        /// <summary>
        /// Goes to the Self Reporting tab if we are not already there, clicks the Actions>Validate link for a 
        /// user-specified activity, waits for the Credit Validation page to appear, clicks either the 
        /// Accept/Reject/Needs More Information radio button, clicks the Submit button, 
        /// and waits for the page be done loading
        /// </summary>
        /// <param name="activityName">The exact text of the activity inside the Self Reported Activities table table that you want to click on</param>
        /// <param name="option"><see cref="Constants_LTS.Constants_LTS.CreditValidationOptions"/></param>
        internal void ChooseActivityAndValidateCredit(string activityName, 
            Constants_LTS.Constants_LTS.CreditValidationOptions option)
        {
            ClickAndWait(SelfReportActTab);

            // MJ 3/10/21: Wait for the activity to appear. I noticed sometimes it doesnt appear right after you add 
            // an activity in CFPC
            for (int i = 0; i < 20; i++)
            {
                try
                {
                    Browser.WaitForElement(By.XPath(
                        string.Format("//table[@id='externalActivitiesRepeater']/tbody//td[text()='{0}']", activityName)),
                        TimeSpan.FromSeconds(90), ElementCriteria.IsVisible);
                    break;
                }
                catch
                {
                    if (i == 19)
                    {
                        throw new Exception("Refreshed LTS 19 times and the activity still did not show up in " +
                            "the activity table. It should have shown up by now");                    
                    }
                }

                ClickAndWait(DetailsTab);
                this.RefreshPage();
                ClickAndWait(SelfReportActTab);
            }

            IWebElement btn = ElemSet.Grid_HoverButtonOrLinkWithinRow(Browser, SelfReportActTabValidActivityTbl, 
                Bys.ProgramPage.SelfReportActTabActivityTblBodyRow, activityName, "td", "Actions", "span");
            Thread.Sleep(0500);

            IWebElement btnParent = XpathUtils.GetNthParentElem(btn, 3);

            ElemSet.Grid_ClickMenuItemInsideDropdown(Browser, btnParent, "Validate");

            Browser.WaitForElement(Bys.ProgramPage.CreditValidationAcceptRdo, ElementCriteria.IsEnabled);
            Browser.WaitForElement(Bys.ProgramPage.CreditValidationAcceptRdo, ElementCriteria.IsVisible);
            Browser.WaitForElement(Bys.ProgramPage.CreditValidationRejectRdo, ElementCriteria.IsEnabled);
            Browser.WaitForElement(Bys.ProgramPage.CreditValidationRejectRdo, ElementCriteria.IsVisible);
            Thread.Sleep(1000);

            if (option == Constants_LTS.Constants_LTS.CreditValidationOptions.Accept)
            {
                CreditValidationAcceptRdo.Click();
            }
            else if (option == Constants_LTS.Constants_LTS.CreditValidationOptions.Reject)
            {
                // MJ: Sometimes this is failing to click I think because CreditValidationRejection test on Mainpro
                // is failing when it tries to validate that the activity got rejected and should not appear on the 
                // Mainpro UI. When I go to the user manually in LTS, I see that it got Accepted, not Rejected. So 
                // maybe it failed to click this radio button. Adding a javascript click here now
                // 5/18/21: The javascript did not work. Maybe the application has a defect where sometimes it
                // Accepts the activity even though the user clicked on Reject radio button. I am going to do a
                // couple more things: 1. Click on Accept then click on Reject. 2. Add comments in the comments
                // text field now after clicking. 3. Throw error if it does not get selected after clicking it
                // If these 3 things still show a failed test, then its def an applicatio bug and it is intermittent
                // Will have to comment test out forever because I cant consistenyly reproduce so DEV wont fix
                CreditValidationRejectRdo.Click();
                CreditValidationRejectRdo.ClickJS(Browser);
                CreditValidationAcceptRdo.Click();
                CommentTxt.SendKeys("dswfwr");
                CreditValidationRejectRdo.Click();
                CreditValidationRejectRdo.ClickJS(Browser);
                if (!CreditValidationRejectRdo.Selected)
                {
                    throw new Exception("Failed to click on the Reject radio button. See inside code for more info");
                }

            }
            else if (option == Constants_LTS.Constants_LTS.CreditValidationOptions.NeedMoreInformation)
            {
                NeedsMoreInformationRdo.Click();
                NeedsMoreInformationRdo.ClickJS(Browser);
            }

            

            ClickAndWait(CreditValidationSubmitBtn);
            Thread.Sleep(0300);

            this.WaitUntil(Criteria.ProgramPage.GreenBannerNotExists);
            
            // Adding a little sleep here. For some reason, whenever the code proceeds after clicking this button (which invokes the green banner at the top),
            // the next line of code doesnt execute. For example, Navigate.GoToLoginPage. That code gets past the navigation part, but if you view the test in
            // progress, no URL is entered into the URL. Another example I have code to click on the "Sites" tab in LTS after this, and the code goes past the
            // Click line, but if you view the test, it didnt click anything. I have never seen this before. So far, I think it only happened in Debug mode.
            // Monitor going forward
            Thread.Sleep(0600);

            // Have to refresh the page sometimes so the credits appear on the details tab
            // MJ 11/3/21: Had to add a line to click on the Details tab here because today after clicking Submit on the  
            // Credit Validation window, the application did not go to the Details tab, and instead went to the Self Reported 
            // Activities tab, so when we Refresh below, it does not work cause the Refresh expects the Details tab after 
            // clicking Refresh
            ClickAndWait(DetailsTab);
            this.RefreshPage(true);
        }

        /// <summary>
        /// Clicks on the Details tab then returns the value label of the user-specified label on the Details tab
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="detail">The name of the label on the Detail tab for which you want the value to return</param>
        internal string GetProgramDetail(IWebDriver browser, string detail)
        {
            string detailToReturn = "";

            ClickAndWait(DetailsTab);

            if (detail == "Name")
            {
                detailToReturn = DetailsTabNameValueLbl.Text;
            }
            else if (detail == "Status")
            {
                detailToReturn = DetailsTabStatusValueLbl.Text;
            }
            else if (detail == "Starts")
            {
                detailToReturn = DetailsTabStartsValueLbl.Text;
            }
            else if (detail == "Ends")
            {
                detailToReturn = DetailsTabEndsValueLbl.Text;
            }
            else if (detail == "Program")
            {
                detailToReturn = DetailsTabProgramValueLbl.Text;
            }

            return detailToReturn;
        }

        #region add adjustments

        /// <summary>
        /// Goes to the Add Adjustment tab if we are not already there, clicks the Add Adjustment link, chooses an 
        /// Adjustment Code from the select element. Then clicks the Add Adjustment button. This overload is for any
        /// adjustment that has ONLY an Adjustment Code Select Element on the Add Adjustment popup. Meaning additional 
        /// controls (date fields) dont appear after selecting the adjustment code. Codes such as 
        /// ext1, ext2, ext2f, pra, per temp, etc. 
        /// </summary>
        /// <param name="adjustmentCode">The exact text of te adjustment code that you want to chooose in the Adjustment Code select element</param>
        public void AddProgramAdjustment(Constants_LTS.Constants_LTS.AdjustmentCodes adjustmentCode)
        {
            ClickAndWait(ProgramAdjustmentsTab);

            ClickAndWait(ProgAdjustTabAddAdjustLnk);

            ProgAdjustTabAddAdjustFormAdjustCodeSelElem.SelectByText(adjustmentCode.GetDescription());

            ClickAndWait(ProgAdjustTabAddAdjustFormAddAdjustBtn);
            // A test once failed here from Mainpro. DefaultCycleCompletion. Screenshot showed that after refresh, the 
            // UI didnt go to the Details tab of this page, and instead stayed on the Program Adjustments tab. Do we 
            // even need a refresh on this line of code? If this occurs more frequently, maybe remove this line, or 
            // click on the Details tab first. Update: Gonna comment this.RefreshPage in all AddAdjustment methods
            // then see if it affects any tests negatively. If not, remove this code. If it does affect tests, then
            // determine why and fix it, because I dont think we should refresh here because the refresh goes to the
            // Details tab and we should not expect the Details tab at this point after making an adjustment
            //ClickAndWait(DetailsTab);
            //this.RefreshPage(true);
        }

        /// <summary>
        /// Clicks the Add Adjustment tab if we are not already there, clicks the Add Adjustment link, chooses an 
        /// Adjustment Code from the select element, chooses an item from the first dropdown and then enters a date. 
        /// Then clicks the Add Adjustment button. This overload is for CFPC C-Custom adjustment. 
        /// NOTE: Right now, this method only has code to adjust the date field on the Add Adjustment popup when the 
        /// user selects one of the date items in the first dropdown. I still need to add code for the second 
        /// dropdown on the popup, as well as the other items (i.e. Adjust End Of Cycle By Days) within the first dropdown
        /// </summary>
        /// <param name="adjustmentCode">The exact text of te adjustment code that you want to chooose in the 
        /// Adjustment Code select element</param>
        /// <param name="adjustCycleDate">Quickly needed to add parameters for CFPC adjust cycle date.</param>
        /// <param name="adjustCycleDate">Quickly needed to add parameters for CFPC adjust cycle date.</param>
        public void AddProgramAdjustment(Constants_LTS.Constants_LTS.AdjustmentCodes adjustmentCode,
            Constants_LTS.Constants_LTS.AddAdjustFormCFPCCustomAdjustFirstSelElemItem adjustCycleSelection =
            Constants_LTS.Constants_LTS.AddAdjustFormCFPCCustomAdjustFirstSelElemItem.AdjustCycleEndDate,
            DateTime adjustCycleDate = default(DateTime))
        {
            ClickAndWait(ProgramAdjustmentsTab);

            ClickAndWait(ProgAdjustTabAddAdjustLnk);

            switch (adjustmentCode)
            {
                case Constants_LTS.Constants_LTS.AdjustmentCodes.CUSTOM:
                    SelectAndWait(ProgAdjustTabAddAdjustFormAdjustCodeSelElem, "C-CUSTOM");
                    SelectAndWait(ProgAdjustTabAddAdjustFormAdjustCycleSelElem, adjustCycleSelection.GetDescription());
                    // If user didnt send a date, set it to yesterday
                    if (adjustCycleDate == default(DateTime))
                    {
                        adjustCycleDate = DateTime.Today.AddDays(-1);
                    }
                    ProgAdjustTabAddAdjustFormAdjustCycleDateTxt.Clear();
                    ProgAdjustTabAddAdjustFormAdjustCycleDateTxt.
                        SendKeys(adjustCycleDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
                    Thread.Sleep(300);
                    break;
            }

            ClickAndWait(ProgAdjustTabAddAdjustFormAddAdjustBtn);
            // A test once failed here from Mainpro. DefaultCycleCompletion. Screenshot showed that after refresh, the 
            // UI didnt go to the Details tab of this page, and instead stayed on the Program Adjustments tab. Do we 
            // even need a refresh on this line of code? If this occurs more frequently, maybe remove this line, or 
            // click on the Details tab first. Update: Gonna comment this.RefreshPage in all AddAdjustment methods
            // then see if it affects any tests negatively. If not, remove this code. If it does affect tests, then
            // determine why and fix it, because I dont think we should refresh here because the refresh goes to the
            // Details tab and we should not expect the Details tab at this point after making an adjustment
            //ClickAndWait(DetailsTab);
            //this.RefreshPage(true);
        }

        /// <summary>
        /// Goes to the Add Adjustment tab if we are not already there, clicks the Add Adjustment link, chooses an 
        /// Adjustment Code from the select element, and clicks on the Yes or No radio button. 
        /// Then clicks the Add Adjustment button. This overload is for 
        /// the INTNL and Voluntary program adjustments
        /// </summary>
        /// <param name="adjustmentCode">The exact text of the adjustment code that you want to chooose in the 
        /// Adjustment Code select element</param>
        /// <param name="Rdo">The yes or no radio button element for INTNL or VOLUNTARY program adjustment</param>
        internal void AddProgramAdjustment(Constants_LTS.Constants_LTS.AdjustmentCodes adjustmentCode, IWebElement Rdo)
        {
            ClickAndWait(ProgramAdjustmentsTab);

            ClickAndWait(ProgAdjustTabAddAdjustLnk);

            ProgAdjustTabAddAdjustFormAdjustCodeSelElem.SelectByText(adjustmentCode.GetDescription());

            Thread.Sleep(1000);
            Rdo.Click();

            ClickAndWait(ProgAdjustTabAddAdjustFormAddAdjustBtn);
        }

        /// <summary>
        /// Goes to the Add Adjustment tab if we are not already there, clicks the Add Adjustment link, chooses an
        /// Adjustment Code from the select element, enters a Leave start and end date, selects a Leave code. 
        /// Then clicks the Add Adjustment button. This overload is for the Leave program adjustment.
        /// </summary>
        /// <param name="adjustmentCode">The exact text of the adjustment code that you want to chooose in the
        /// Adjustment Code select element</param>
        /// <param name="leaveStartDate"></param>
        /// <param name="leaveEndDate"></param>
        /// <param name="leaveCode"></param>
        internal void AddProgramAdjustment(Constants_LTS.Constants_LTS.AdjustmentCodes adjustmentCode, 
            string leaveStartDate, string leaveEndDate, string leaveCode)
        {
            ClickAndWait(ProgramAdjustmentsTab);

            ClickAndWait(ProgAdjustTabAddAdjustLnk);

            ProgAdjustTabAddAdjustFormAdjustCodeSelElem.SelectByText("LEAVE");

            Browser.WaitForElement(Bys.ProgramPage.ProgAdjustTabAddAdjustFormLeaveStartDtTxt, ElementCriteria.IsVisible);
            ProgAdjustTabAddAdjustFormLeaveStartDtTxt.SendKeys(leaveStartDate);
            ProgAdjustTabAddAdjustFormLeaveEndDtTxt.SendKeys(leaveEndDate);
            ProgAdjustTabAddAdjustFormLeaveCodeSelElem.SelectByText(leaveCode);

            ClickAndWait(ProgAdjustTabAddAdjustFormAddAdjustBtn);
        }
        /// <summary>
        /// Goes to the Add Adjustment tab if we are not already there, clicks the Add Adjustment link, chooses an
        /// Adjustment Code from the select element, enters a Leave start and end date
        /// Then clicks the Add Adjustment button. This overload is for the Leave program adjustment.
        /// </summary>
        /// <param name="adjustmentCode">The exact text of the adjustment code that you want to chooose in the
        /// Adjustment Code select element</param>
        /// <param name="leaveStartDate"></param>
        /// <param name="leaveEndDate"></param>
        /// <param name="leaveCode"></param>
        internal void AddProgramAdjustment(Constants_LTS.Constants_LTS.AdjustmentCodes adjustmentCode,
            string leaveStartDate, string leaveEndDate)
        {
            ClickAndWait(ProgramAdjustmentsTab);

            ClickAndWait(ProgAdjustTabAddAdjustLnk);

            ProgAdjustTabAddAdjustFormAdjustCodeSelElem.SelectByText(adjustmentCode.GetDescription());

            Browser.WaitForElement(Bys.ProgramPage.ProgAdjustTabAddAdjustFormLeaveStartDtTxt, ElementCriteria.IsVisible);
            ProgAdjustTabAddAdjustFormLeaveStartDtTxt.SendKeys(leaveStartDate);
            ProgAdjustTabAddAdjustFormLeaveEndDtTxt.SendKeys(leaveEndDate);
            
            ClickAndWait(ProgAdjustTabAddAdjustFormAddAdjustBtn);
        }

        /// <summary>
        /// Goes to the Add Adjustment tab if we are not already there, clicks the Add Adjustment link, chooses an 
        /// Adjustment Code from the select element, then enters an effective date. Then clicks the Add Adjustment button.
        /// This overload is for any adjustment that has ONLY an Effective Date Select Element on the Add Adjustment popup.
        /// Meaning additional controls dont appear after selecting the adjustment code. Codes such as 
        /// Reinstated - Other, Reinstated - Non Compliance, PER program, PER program, Voluntary Program, 
        /// International Program, Main Program, Resident Program, A-Active (Default), etc.
        /// </summary>
        /// <param name="adjustmentCode">The exact text of the adjustment code that you want to chooose in the 
        /// Adjustment Code select element</param>
        /// <param name="effectiveDate"></param>
        internal void AddProgramAdjustment(IWebDriver browser, Constants_LTS.Constants_LTS.AdjustmentCodes 
            adjustmentCode, string effectiveDate)
        {
            ClickAndWait(ProgramAdjustmentsTab);

            string firstRowFirstColTxt = ElemGet.Grid_GetCellTextByRowIndexAndColIndex(Browser, ProgramAdjustmentsActivityTblBody, Bys.ProgramPage.ProgramAdjustmentsActivityTblBodyRow, 0, 0);
            
            // If the Program Adjustments activity table contains NOCYCLE SYSTEM record in the fisrt row, then we need
            // to delete that NOCYCLE SYSTEM record and continue. Note this is only for an RCP test (ApplyCarryOverCredits)
            // right now
            if (firstRowFirstColTxt == "NOCYCLE SYSTEM")
            {
                ElemSet.Grid_ClickButtonOrLinkWithinRow(browser, ProgramAdjustmentsActivityTbl,
                    Bys.ProgramPage.ProgramAdjustmentsActivityTblBody, "NOCYCLE SYSTEM", "td", "Delete", "a");
                IAlert alert = browser.SwitchTo().Alert();
                alert.Accept();
                this.WaitUntilAll(TimeSpan.FromSeconds(120), Criteria.ProgramPage.ProgAdjustTabAddAdjustFormNotExists,
                       Criteria.ProgramPage.GreenBannerNotExists);
                Thread.Sleep(0500);
                ClickAndWait(ProgramAdjustmentsTab);
            }

            ClickAndWait(ProgAdjustTabAddAdjustLnk);

            ProgAdjustTabAddAdjustFormAdjustCodeSelElem.SelectByText(adjustmentCode.GetDescription());

            Browser.WaitForElement(Bys.ProgramPage.ProgAdjustTabAddAdjustFormEffectiveDtTxt, ElementCriteria.IsVisible);
            ProgAdjustTabAddAdjustFormEffectiveDtTxt.Clear();
            ProgAdjustTabAddAdjustFormEffectiveDtTxt.SendKeys(effectiveDate);
            ProgAdjustTabAddAdjustFormEffectiveDtTxt.SendKeys(Keys.Tab);

            ClickAndWait(ProgAdjustTabAddAdjustFormAddAdjustBtn);
        }

        #endregion add adjustments

        #endregion methods: page specific
    }

    #region additional classes

    public class ProgramCycle
    {
        public string Name { get; set; }
        public string Status { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public string Program { get; set; }

        public ProgramCycle(string name, string status, string start, string end, string program)
        {
            Name = name;
            Status = status;
            Start = start;
            End = end;
            Program = program;
        }

        #endregion additional classes

    }
}