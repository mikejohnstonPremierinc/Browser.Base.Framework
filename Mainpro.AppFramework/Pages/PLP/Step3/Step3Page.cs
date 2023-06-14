using Browser.Core.Framework;
using LMS.Data;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using LOG4NET = log4net.ILog;

namespace Mainpro.AppFramework
{
    public class Step3Page : MainproPage, IDisposable
    {
        #region constructors
        public Step3Page(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that WE start so we can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        public override string PageUrl { get { return "login.aspx?action=enablelogin"; } }

        MainproHelperMethods Help = new MainproHelperMethods();
        #endregion properties

        #region elements           

        public IWebElement PLPStep3IdentifiedGapsTbl { get { return this.FindElement(Bys.Step3Page.PLPStep3IdentifiedGapsTbl); } }
        public IWebElement PLPStep3IdentifiedGapsTblHdr { get { return this.FindElement(Bys.Step3Page.PLPStep3IdentifiedGapsTblHdr); } }
        public IWebElement PLPStep3IdentifiedGapsTblBody { get { return this.FindElement(Bys.Step3Page.PLPStep3IdentifiedGapsTblBody); } }
        public IWebElement SetYourGoalsSelectedActivitiesTbl { get { return this.FindElement(Bys.Step3Page.SetYourGoalsSelectedActivitiesTbl); } }
        public IWebElement SetYourGoalsSelectedActivitiesTblHdr { get { return this.FindElement(Bys.Step3Page.SetYourGoalsSelectedActivitiesTblHdr); } }
        public IWebElement SetYourGoalsSelectedActivitiesTblBody { get { return this.FindElement(Bys.Step3Page.SetYourGoalsSelectedActivitiesTblBody); } }
        public IWebElement SetYourGoalsSelectedActivitiesTblBodyFirstRow { get { return this.FindElement(Bys.Step3Page.SetYourGoalsSelectedActivitiesTblBodyFirstRow); } }
        public IWebElement CPDEventsTblBodyFirstRow { get { return this.FindElement(Bys.Step3Page.CPDEventsTblBodyFirstRow); } }
        public IWebElement CPDEventsTblBody { get { return this.FindElement(Bys.Step3Page.CPDEventsTblBody); } }
        public IWebElement CPDEventsTblHdr { get { return this.FindElement(Bys.Step3Page.CPDEventsTblHdr); } }
        public IWebElement CPDEventsTbl { get { return this.FindElement(Bys.Step3Page.CPDEventsTbl); } } 
        public IWebElement CPDEventsCalendarBtn { get { return this.FindElement(Bys.Step3Page.CPDEventsCalendarBtn); } } 
        public IWebElement CPDEvents_MoreInfoDetailsBtn { get { return this.FindElement(Bys.Step3Page.CPDEvents_MoreInfoDetailsBtn); } }
        public IWebElement CPDEvents_MoreinfoDetails_SessionIdLabel { get { return this.FindElement(Bys.Step3Page.CPDEvents_MoreinfoDetails_SessionIdLabel); } }
        public IWebElement AddAnotherGoalYesRdo { get { return this.FindElement(Bys.Step3Page.AddAnotherGoalYesRdo); } }
        public IWebElement AddAnotherGoalNoRdo { get { return this.FindElement(Bys.Step3Page.AddAnotherGoalNoRdo); } }
        public IWebElement NextBtn { get { return this.FindElement(Bys.Step3Page.NextBtn); } }
        public IWebElement BackBtn { get { return this.FindElement(Bys.Step3Page.BackBtn); } }
        public IWebElement ExpandAllBtn { get { return this.FindElement(Bys.Step3Page.ExpandAllBtn); } }
        public IWebElement CollapseAllBtn { get { return this.FindElement(Bys.Step3Page.CollapseAllBtn); } }
        public IWebElement GoToBottomBtn { get { return this.FindElement(Bys.Step3Page.GoToBottomBtn); } }
        public SelectElement ActivityDetailFormCategorySelElem { get { return new SelectElement(this.FindElement(Bys.Step3Page.ActivityDetailFormCategorySelElem)); } }
        public IWebElement ActivityDetailFormCategorySelElemBtn { get { return this.FindElement(Bys.Step3Page.ActivityDetailFormCategorySelElemBtn); } }
        public SelectElement ActivityDetailFormProvinceSelElem { get { return new SelectElement(this.FindElement(Bys.Step3Page.ActivityDetailFormProvinceSelElem)); } }
        public IWebElement ActivityDetailFormProvinceSelElemBtn { get { return this.FindElement(Bys.Step3Page.ActivityDetailFormProvinceSelElemBtn); } }
        public SelectElement ActivityDetailFormEventTypeSelElem { get { return new SelectElement(this.FindElement(Bys.Step3Page.ActivityDetailFormEventTypeSelElem)); } }
        public IWebElement ActivityDetailFormEventTypeSelElemBtn { get { return this.FindElement(Bys.Step3Page.ActivityDetailFormEventTypeSelElemBtn); } }
        public SelectElement ActivityDetailFormGapsSelElem { get { return new SelectElement(this.FindElement(Bys.Step3Page.ActivityDetailFormGapsSelElem)); } }
        public SelectElement ActivityTblGapsSelElem { get { return new SelectElement(this.FindElement(Bys.Step3Page.ActivityTblGapsSelElem)); } }
        public IWebElement ActivityDetailFormGapsSelElemBtn { get { return this.FindElement(Bys.Step3Page.ActivityDetailFormGapsSelElemBtn); } }
        public IWebElement PlusActivitiesBtn { get { return this.FindElement(Bys.Step3Page.PlusActivitiesBtn); } }
        public IWebElement ActivityDetailFormActivityTitleTxt { get { return this.FindElement(Bys.Step3Page.ActivityDetailFormActivityTitleTxt); } }
        public IWebElement ActivityDetailFormDateTxt { get { return this.FindElement(Bys.Step3Page.ActivityDetailFormDateTxt); } }
        public IWebElement ActivityDetailFormCityTxt { get { return this.FindElement(Bys.Step3Page.ActivityDetailFormCityTxt); } }
        public IWebElement CPDEvents_Gap1Label { get { return this.FindElement(Bys.Step3Page.CPDEvents_Gap1Label); } }
        public IWebElement CPDEvents_Gap2Label { get { return this.FindElement(Bys.Step3Page.CPDEvents_Gap2Label); } }
        public IWebElement CPDEvents_Gap3Label { get { return this.FindElement(Bys.Step3Page.CPDEvents_Gap3Label); } }
        public IWebElement PLP_SummaryTable_Gap1Label { get { return this.FindElement(Bys.Step3Page.PLP_SummaryTable_Gap1Label); } }
        public IWebElement PLP_SummaryTable_Gap2Label { get { return this.FindElement(Bys.Step3Page.PLP_SummaryTable_Gap2Label); } }
        public IWebElement PLP_SummaryTable_Gap3Label { get { return this.FindElement(Bys.Step3Page.PLP_SummaryTable_Gap3Label); } }
        public IWebElement Gap1Chk { get { return this.FindElement(Bys.Step3Page.Gap1Chk); } }


        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(60));
            if (Browser.Exists(Bys.MainproPage.WereSorryErrorLbl, ElementCriteria.IsVisible))
            {
                throw new Exception(string.Format("The application threw an error '{0}'. Check the Browsers console log for any " +
                    "javascript errors. Sometimes this error occurs intermittently and will not occur a second time when rerunning " +
                    "the same test", WereSorryErrorLbl.Text));
            }
            this.WaitUntil(TimeSpan.FromSeconds(60), Criteria.Step3Page.PageReady);
            Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(60));
            Thread.Sleep(500);
            this.WaitUntil(TimeSpan.FromSeconds(60), Criteria.Step3Page.PageReady);
            Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(60));
            if (Browser.Exists(Bys.MainproPage.WereSorryErrorLbl, ElementCriteria.IsVisible))
            {
                throw new Exception(string.Format("The application threw an error '{0}'. Check the Browsers console log for any " +
                    "javascript errors. Sometimes this error occurs intermittently and will not occur a second time when rerunning " +
                    "the same test", WereSorryErrorLbl.Text));
            }
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

        /// <summary>
        /// Clicks the user-specified element and then waits for a window to close or open, or a page to load,
        /// depending on the element that was clicked
        /// </summary>
        /// <param name="elem">The element to click on</param>
        public dynamic ClickAndWait(IWebElement elem)
        {
            if (Browser.Exists(Bys.Step3Page.BackBtn))
            {
                if (elem.GetAttribute("outerHTML") == BackBtn.GetAttribute("outerHTML"))
                {
                    BackBtn.Click();
                    this.WaitForInitialize();
                        return null;
                    
                }
            }
             if (Browser.Exists(Bys.Step3Page.NextBtn))
            {
                if (elem.GetAttribute("outerHTML") == NextBtn.GetAttribute("outerHTML"))
                {
                    NextBtn.Click();
                    this.WaitForInitialize();

                    if (PLP_Header_StepNumberLabel.Text.Contains("Step 3"))
                    {
                        return this;
                    }
                    else
                    {
                        Step4Page page = new Step4Page(Browser);
                        page.WaitForInitialize();
                        return page;
                    }
                }
            }

            if (Browser.Exists(Bys.Step3Page.PlusActivitiesBtn))
            {
                if (elem.GetAttribute("outerHTML") == PlusActivitiesBtn.GetAttribute("outerHTML"))
                {
                    PlusActivitiesBtn.Click();
                    Browser.WaitForElement(Bys.Step3Page.ActivityDetailFormDateTxt, ElementCriteria.IsVisible);
                    Browser.WaitJSAndJQuery();
                    return null;
                }
            }

            if (Browser.Exists(Bys.Step3Page.CPDEvents_MoreInfoDetailsBtn))
            {
                if (elem.GetAttribute("outerHTML") == CPDEvents_MoreInfoDetailsBtn.GetAttribute("outerHTML"))
                {
                    CPDEvents_MoreInfoDetailsBtn.Click();
                    Browser.WaitForElement(Bys.Step3Page.CPDEvents_MoreinfoDetails_SessionIdLabel, ElementCriteria.IsVisible);
                    Browser.WaitJSAndJQuery();
                    return null;
                }
            }
            
            if (Browser.Exists(Bys.MainproPage.PLP_ActivityDetailFormCancelBtn))
            {
                if (elem.GetAttribute("outerHTML") == PLP_ActivityDetailFormCancelBtn.GetAttribute("outerHTML"))
                {
                    
                    PLP_ActivityDetailFormAddBtn.Click();
                    Thread.Sleep(20);
                    Browser.WaitJSAndJQuery();
                    ElemSet.ClickAfterScroll(Browser,PLP_ActivityDetailFormCancelBtn);
                    if (!PlusActivitiesBtn.Displayed) { PLP_ActivityDetailFormCancelBtn.ClickJS(Browser); }
                   // PLP_ActivityDetailFormCancelBtn.ClickJS(Browser);
                    this.WaitUntil(Criteria.Step3Page.ActivityDetailFormDateTxtNotExists);
                    return null;
                }
            }




            throw new Exception(string.Format("No element was found with your passed parameter, which was the '{0}' element. " +
                "You either need to add this element to a new If statement, or if the element is already added, then the page " +
                "you were on did not contain the element.",
                elem.GetAttribute("innerText")));
        }




        #endregion methods: per page

        #region methods: page specific

        /// <summary>
        /// Fills the fields on the Activity Details Form then clicks Add and verifies the activity appears in the resulting table
        /// </summary>
        /// <param name="actTitle">(Optional). Specify an activity title. If not specified, a random activity title will be used</param>
        /// <param name="dt">(Optional). Default = today</param>
        /// <param name="category">(Optional). Specify a category. If not specified, a random category will be used</param>
        /// <param name="city">(Optional). Specify a city. If not specified, a random city will be used</param>
        /// <param name="province">(Optional). Specify a province. If not specified, a random province will be used</param>
        /// <param name="eventType">(Optional). Specify a category. If not specified, a random eventType will be used</param>
        /// <param name="gaps">(Optional). Specify a gap. If not specified, a random gap will be used</param>
        public PLP_Event AddEvent(Const_Mainpro.Table tbl,IWebElement tableElem, By tableElemBodyBy,
            string actTitle = null, DateTime dt = default(DateTime), string category = null, string
            city = null, string province = null, string eventType = null, List<string> gaps = null)
        {
            ClickAndWait(PlusActivitiesBtn);

            actTitle = string.IsNullOrEmpty(actTitle) ? "MyEventTitle" + DataUtils.GetRandomString(5) : actTitle;
            ActivityDetailFormActivityTitleTxt.SendKeys(actTitle);

            // If the user didnt specify a date, set the date to today. 
            if (dt == default(DateTime))
            {
                dt = currentDatetime;
            }
            ActivityDetailFormDateTxt.SendKeys(dt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture));
            //ActivityDetailFormDateTxt.SendKeys(dt.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));

            category = string.IsNullOrEmpty(category) ? "Assessment" : category;
            ActivityDetailFormCategorySelElem.SelectByText(category);

            city = string.IsNullOrEmpty(city) ? "MyCity" + DataUtils.GetRandomString(5) : city;
            ActivityDetailFormCityTxt.SendKeys(city);

            province = string.IsNullOrEmpty(province) ? "Alberta" : province;
            ActivityDetailFormProvinceSelElem.SelectByText(province);

            eventType = string.IsNullOrEmpty(eventType) ? "Live In-Person" : eventType;
            ActivityDetailFormEventTypeSelElem.SelectByText(eventType);

            if (gaps == null)
            {
                //gaps.Add(ActivityDetailFormGapsSelElem.Options[0].Text);
                ActivityDetailFormGapsSelElem.SelectByIndex(0);
            }
            else
            {
                foreach (var gap in gaps)
                {
                    ActivityDetailFormGapsSelElem.SelectByText(gap);
                }
            }

            ClickAndWaitBasePage(PLP_ActivityDetailFormAddBtn);

            if (!ElemGet.Grid_ContainsRecord(Browser, tableElem, tableElemBodyBy, 2, actTitle, "span"))
            {
                string goalTitleInTable = Help.Grid_GetRowCellTextByIndex(Browser, tbl, 0, 3, "//span") ;
                throw new Exception(string.Format("The method successfully filled the the form and clicked the Add " +
                    "button, but when the method tried to verify that the event appeared in the CPD Event " +
                    "table, it could not find it. The code is expecting the title of the goal to be {0}, however the " +
                    "goal table is displaying {1}. The cause is either that the " +
                    "application did not successfully populate this table, or the table has many events and the " +
                    "application incorrectly sorted the last submitted one at the bottom (this table has an infinite " +
                    "scroll which makes activities not appear until scrolled to)", actTitle, goalTitleInTable));
            }

            return new PLP_Event(actTitle, dt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture), category, city, province,
                eventType, gaps);
        }

        /// <summary>
        /// Checked the checkbox for an existing even within the CPD Events Calendar table, selects tester specified gaps
        /// </summary>
        /// <param name="actTitle">The activity to choose</param>
        /// <param name="gaps">(Optional). Specify a gap(s). If not specified, a random gap will be used</param>
        public PLP_Event ChooseEvent(string actTitle, List<string> gaps)
        {
            // Click the check box for the tester specified activity
            Help.Grid_ClickCellInTable(Browser,
                Const_Mainpro.Table.PLPStep3Events,
                actTitle, Const_Mainpro.TableButtonLinkOrCheckBox.CheckBox);

            // Choose the gaps within the Select Element. 
            foreach (var gap in gaps)
            {
                
                ElemSet.Grid_SelectItemWithinSelElem(Browser, CPDEventsTblBody,
                    Bys.Step3Page.CPDEventsTblBodyFirstRow, actTitle, "span", "", gap);
                
                // Need to work onby selecting future year  
                //ElemSet.Grid_SelectItemWithinSelElem(Browser, CPDEventsTblBody,
                //    Bys.Step3Page.CPDEventsTblBodyFirstRow, actTitle, "span", "", gap, "02/01/23",
                //    "td");
            }

            string eventType = ElemGet.Grid_GetCellTextByRowIndexAndColIndex(Browser, CPDEventsTblBody,
                Bys.Step3Page.CPDEventsTblBodyFirstRow, 1, 3);
            string dt = ElemGet.Grid_GetCellTextByRowIndexAndColIndex(Browser, CPDEventsTblBody,
                Bys.Step3Page.CPDEventsTblBodyFirstRow, 1, 4);
            string category = ElemGet.Grid_GetCellTextByRowIndexAndColIndex(Browser, CPDEventsTblBody,
                Bys.Step3Page.CPDEventsTblBodyFirstRow, 1, 5);
            string credit = ElemGet.Grid_GetCellTextByRowIndexAndColIndex(Browser, CPDEventsTblBody,
                Bys.Step3Page.CPDEventsTblBodyFirstRow, 1, 6);
            string city = ElemGet.Grid_GetCellTextByRowIndexAndColIndex(Browser, CPDEventsTblBody,
                Bys.Step3Page.CPDEventsTblBodyFirstRow, 1, 7);
            string province = ElemGet.Grid_GetCellTextByRowIndexAndColIndex(Browser, CPDEventsTblBody,
                Bys.Step3Page.CPDEventsTblBodyFirstRow, 1, 8);

            return new PLP_Event(actTitle, dt, category, city, province, eventType, gaps);
        }


        public void ChooseGaps(List<string> gaps)
        {
            foreach (string gap in gaps)
            {
                Help.PLP_ClickCheckBoxOrRadioButton(Browser, gap);
            }
           
        }

        public void FillSMARTGoalDetails( IWebDriver Browser, string goalTitle_TobeEntered,
            Const_Mainpro.PLP_TextboxlabelText PLP_TextboxlabelText =  Const_Mainpro.PLP_TextboxlabelText.None,
            string labelText=null)
        {
            Help.PLP_AddFormattedText(Browser, goalTitle_TobeEntered, PLP_TextboxlabelText);
            Help.PLP_AddFormattedText(Browser, "Testing Specific", 
                Const_Mainpro.PLP_TextboxlabelText.Step3_SMARTGoal_SpecificTxt);
            Help.PLP_AddFormattedText(Browser,  "Testing Measureable",
                Const_Mainpro.PLP_TextboxlabelText.Step3_SMARTGoal_MeasurableTxt);
            Help.PLP_AddFormattedText(Browser, "Testing Achievable",
                Const_Mainpro.PLP_TextboxlabelText.Step3_SMARTGoal_AchievableTxt);
            Help.PLP_AddFormattedText(Browser, "Testing Realistic",
                Const_Mainpro.PLP_TextboxlabelText.Step3_SMARTGoal_RealisticTxt);
            Help.PLP_AddFormattedText(Browser,  "Testing Timely",
                Const_Mainpro.PLP_TextboxlabelText.Step3_SMARTGoal_TimelyTxt);
        }

        public PLP_Event ChooseSystemCPDActivity()
        {
            // Click next, choose an existing even with 2 gaps, then add a new CPD event and assert that the event was added
            // to the table

            //need to work on code to select  the future year activity
            PLP_Event chosenEvent = ChooseEvent("PLP - This should ALWAYS return",
                //"University Program for testing in PLP",
                // "PLP - This should ALWAYS return",
                //"Professional Learning Plans: Closing the Gap", 
                new List<string>() { "Gap 1", "Gap 2" });
            return chosenEvent;

        }

        public PLP_Event AddEditDeleteCustomAddActivity(Const_Mainpro.Table ActivityTbl, 
            IWebElement tableElem, By tableElemBodyBy)
        {
            PLP_Event addedThenDeletedEvent = AddEvent(ActivityTbl, tableElem,tableElemBodyBy);

            // Edit the event, assert the edit in the table, Delete the event and assert it was removed from the table,
            // then add another event
            Help.Grid_ClickCellInTable(Browser, ActivityTbl, addedThenDeletedEvent.ActivityTitle,
                Const_Mainpro.TableButtonLinkOrCheckBox.Edit);
            ActivityDetailFormCityTxt.Clear();
            ActivityDetailFormCityTxt.SendKeys("city edited");
            ClickAndWaitBasePage(PLP_ActivityDetailFormAddBtn);
            Help.VerifyCellTextMatches(Browser, this, ActivityTbl,
                   rowName: addedThenDeletedEvent.ActivityTitle,
                   colName: "City",
                   cellTextExpected: "city edited");
            Help.Grid_ClickCellInTable(Browser, ActivityTbl, addedThenDeletedEvent.ActivityTitle,
                Const_Mainpro.TableButtonLinkOrCheckBox.Delete);
            ClickAndWaitBasePage(PLP_AreYouSureYouWantToDeleteFormDeleteBtn);
            Help.VerifyGridDoesNotContainRecord(Browser, ActivityTbl, addedThenDeletedEvent.ActivityTitle);
            PLP_Event addedEvent = AddEvent(ActivityTbl, tableElem, tableElemBodyBy);
            return addedEvent;

        }

        public void FillAddressingGoalNeeds(IWebDriver Browser)
        {
            Help.PLP_AddFormattedText(Browser, "Testing Goal Addressig your Gap",
                Const_Mainpro.PLP_TextboxlabelText.PleasewriteTxt);
        }

        public void FillGoalOutcomes(IWebDriver Browser)
        {
            Help.PLP_AddFormattedText(Browser, 
            "Testing Example: I am going to review all my patients over age 55 according to diabetic guidelines or I am " +
            "going to make improvements in compliance/efficiency.",
            Const_Mainpro.PLP_TextboxlabelText.Step3_GoalOutcomeReviewTxt);
            Help.PLP_AddFormattedText(Browser, 
                "Testing Example: a patient survey completed identified a shift in compliance.",
                Const_Mainpro.PLP_TextboxlabelText.Step3_GoalOutcomeSurveyTxt);
            Help.PLP_AddFormattedText(Browser, 
            "Testing Example: logistical, organizational, system changes, etc.",
            Const_Mainpro.PLP_TextboxlabelText.Step3_GoalOutcomeLogisticalTxt);
        }

        public bool VerfiyEnabledOrDisabled_PLP_Menu_PLPActivitySummmaryOption( bool isEnabled=false)
        {
            bool return_flag;
            ClickAndWaitBasePage(PLP_Menu_DropDownBtn);

            if (isEnabled)
            {
                Assert.True(PLP_Menu_PLPActivitySumm.GetCssValue("color").Contains("rgba(51, 51, 51, 1)"),
                "THE PLP_MENU_ACIVITYSUMMARY BUTTON should be Enabled");
                return_flag = true;
            }
            else
            {
                Assert.False(PLP_Menu_PLPActivitySumm.GetCssValue("color").Contains("rgba(51, 51, 51, 1)"),
               "THE PLP_MENU_ACIVITYSUMMARY BUTTON should be DISABLED");
                return_flag = false;
            }
            ClickAndWaitBasePage(PLP_Menu_CloseBtn);
            return return_flag;

        }

        #endregion methods: page specific



    }
}
