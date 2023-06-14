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


namespace LMSAdmin.AppFramework
{
    public class ActAssessmentsPage : Page, IDisposable
    {
        #region constructors
        public ActAssessmentsPage(IWebDriver driver) : base(driver)
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

        public IWebElement ComplPathTabScenarioTbl { get { return this.FindElement(Bys.ActAssessmentsPage.ComplPathTabScenarioTbl); } }
        public IWebElement ComplPathTabScenarioTblBody { get { return this.FindElement(Bys.ActAssessmentsPage.ComplPathTabScenarioTblBody); } }
        public IWebElement ComplPathTabScenarioTblBodyRow { get { return this.FindElement(Bys.ActAssessmentsPage.ComplPathTabScenarioTblBodyRow); } }
        public IWebElement ComplPathTabAssessmentsTbl { get { return this.FindElement(Bys.ActAssessmentsPage.ComplPathTabAssessmentsTbl); } }
        public IWebElement ComplPathTabAssessmentsTblBody { get { return this.FindElement(Bys.ActAssessmentsPage.ComplPathTabAssessmentsTblBody); } }
        public IWebElement ComplPathTabAssessmentsTblBodyRow { get { return this.FindElement(Bys.ActAssessmentsPage.ComplPathTabAssessmentsTblBodyRow); } }
        public IWebElement ComplPathTab { get { return this.FindElement(Bys.ActAssessmentsPage.ComplPathTab); } }
        public IWebElement AssessmentsTab { get { return this.FindElement(Bys.ActAssessmentsPage.AssessmentsTab); } }

        public IWebElement ComplPathTabBackToComplPathwaySumLnk { get { return this.FindElement(Bys.ActAssessmentsPage.ComplPathTabBackToComplPathwaySumLnk); } }
        public IWebElement ComplPathTabSaveBtn { get { return this.FindElement(Bys.ActAssessmentsPage.ComplPathTabSaveBtn); } }


        public IWebElement AddNewAssessmentLnk { get { return this.FindElement(Bys.ActAssessmentsPage.AddNewAssessmentLnk); } }
        public IWebElement ContinueBtn { get { return this.FindElement(Bys.ActAssessmentsPage.ContinueBtn); } }
        public IWebElement AssTemplateSearchTxt { get { return this.FindElement(Bys.ActAssessmentsPage.AssTemplateSearchTxt); } }
        public IWebElement AssSearchBtn { get { return this.FindElement(Bys.ActAssessmentsPage.AssSearchBtn); } }
        public IWebElement SearchAssTbl { get { return this.FindElement(Bys.ActAssessmentsPage.SearchAssTbl); } }
        public IWebElement SearchAssTblBodyRow { get { return this.FindElement(Bys.ActAssessmentsPage.SearchAssTblBodyRow); } }


        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(30), Criteria.ActAssessmentsPage.PageReady);
        }

        /// <summary>
        /// Clicks the user-specified element, and then waits for a window to close or open, or a page to load, depending on the element that was clicked
        /// </summary>
        /// <param name="buttonOrLinkElem">The element to click on</param>
        public dynamic ClickAndWait(IWebElement buttonOrLinkElem)
        {
            if (Browser.Exists(Bys.ActAssessmentsPage.AddNewAssessmentLnk))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == AddNewAssessmentLnk.GetAttribute("outerHTML"))
                {
                    AddNewAssessmentLnk.ClickJS(Browser);
                    Browser.WaitForElement(Bys.ActAssessmentsPage.ContinueBtn, ElementCriteria.IsVisible);
                    return null;
                }
            }

            if (Browser.Exists(Bys.ActAssessmentsPage.ComplPathTabSaveBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == ComplPathTabSaveBtn.GetAttribute("outerHTML"))
                {
                    ComplPathTabSaveBtn.ClickJS(Browser);
                    return null;
                }
            }

            if (Browser.Exists(Bys.ActAssessmentsPage.ComplPathTabBackToComplPathwaySumLnk))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == ComplPathTabBackToComplPathwaySumLnk.GetAttribute("outerHTML"))
                {
                    ComplPathTabBackToComplPathwaySumLnk.ClickJS(Browser);
                    Browser.WaitForElement(Bys.ActAssessmentsPage.ComplPathTabScenarioTbl);
                    return null;
                }
            }

            if (Browser.Exists(Bys.ActAssessmentsPage.AssessmentsTab))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == AssessmentsTab.GetAttribute("outerHTML"))
                {
                    AssessmentsTab.ClickJS(Browser);
                    Browser.WaitForElement(Bys.ActAssessmentsPage.AddNewAssessmentLnk, ElementCriteria.IsVisible);
                    return null;
                }
            }

            if (Browser.Exists(Bys.ActAssessmentsPage.ComplPathTab))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == ComplPathTab.GetAttribute("outerHTML"))
                {
                    ComplPathTab.ClickJS(Browser);
                    Browser.WaitForElement(Bys.ActAssessmentsPage.ComplPathTabScenarioTbl, ElementCriteria.IsVisible);
                    return null;
                }
            }

            if (Browser.Exists(Bys.ActAssessmentsPage.ContinueBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == ContinueBtn.GetAttribute("outerHTML"))
                {
                    ContinueBtn.ClickJS(Browser);
                    ActAssessmentDetailsPage Page = new ActAssessmentDetailsPage(Browser);
                    Page.WaitForInitialize();
                    return Page;
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
        /// Clicks the Add New Assessment link, clicks the Create New Assessment link, clicks Continue, then returns the ActAssessmentDetailsPage page
        /// if existing community assessment template has to be choosen then send the template name as parameter
        /// </summary>
        /// <returns></returns>
        public ActAssessmentDetailsPage GoToActAssessmentDetailsPage(string assTemplateName=null)
        {
            ClickAndWait(AddNewAssessmentLnk);

            if (assTemplateName != null)
            { 
                ElemSet.RdoBtn_ClickByText(Browser, "Search for a Template within the Community.");
                ContinueBtn.Click();
                ElemSet.TextBox_EnterText(Browser, AssTemplateSearchTxt, true, assTemplateName);
                AssSearchBtn.Click();
                IWebElement assessmentRow = ElemGet_LMSAdmin.Grid_GetRowByRowName(SearchAssTbl,
                Bys.ActAssessmentsPage.SearchAssTblBodyRow, assTemplateName, "td");
                ElemSet.Grid_ClickButtonOrLinkWithoutTextWithinRow(Browser, assessmentRow, "input");
            }

            else 
            { 
                ElemSet.RdoBtn_ClickByText(Browser, "Create New Assessment.");
                ClickAndWait(ContinueBtn);
            }                      

            ActAssessmentDetailsPage Page = new ActAssessmentDetailsPage(Browser);
            Page.WaitForInitialize();
            return Page;
        }

        /// <summary>
        /// In Progress. Scenario logic is confusing how you can have multiple scenarios per accreditation, and all those scenarios tie
        /// in with Assessments, awards, etc. Right now this takes in a list of scenario names and then checks 
        /// "Display for this scenario" "Completion Required" checkbox, sets the # of attempts allowed to 2, sets the # Graded Questions
        /// to 2 on every single scenario, and this also loads and then returns the CompletionPathwayScenario object. For some of the
        /// properties in CompletionPathwayScenarioAssessmentRules, I am just setting a default value instead of creating element 
        /// methods to extract exact values from those rows. Will have to revisit.
        /// </summary>
        internal List<AssessmentRule> EditAndGetCompletionPathways(string scenarioName, int numberOfQuestionsNeededToPass)
        {
            List<AssessmentRule> assessmentRules = new List<AssessmentRule>();

            ClickAndWait(ComplPathTab);

            // Get the scenario row and click on Edit to view the Assessments table for this scenario
            string complPathScenarioName = ElemGet.Grid_GetCellTextByRowNameAndColIndex(Browser, ComplPathTabScenarioTbl,
                Bys.ActAssessmentsPage.ComplPathTabScenarioTblBodyRow, scenarioName,
                "td", 0).Trim();

            IWebElement scenarioRow = ElemGet_LMSAdmin.Grid_GetRowByRowName(ComplPathTabScenarioTbl,
                Bys.ActAssessmentsPage.ComplPathTabScenarioTblBodyRow, scenarioName, "td");

            ElemSet_LMSAdmin.Grid_ClickElementWithoutTextInsideRow(scenarioRow, "img", "Edit");

            Browser.WaitForElement(Bys.ActAssessmentsPage.ComplPathTabAssessmentsTblBodyRow);

            // Get all assessment rows in this scenario 
            IList<IWebElement> assessmentRows = ElemGet_LMSAdmin.Grid_GetRows(ComplPathTabAssessmentsTblBody,
                Bys.ActAssessmentsPage.ComplPathTabAssessmentsTblBodyRow);

            // For each row, check the "Display For This Scenario" and "Completion Required" checkboxes. Also set the "# Attempts Allowed" and
            // the "# of Graded Questions"
            for (int i = 0; i < assessmentRows.Count; i++)
            {
                // Whenever you check a check box, the table reloads, so that is why we are re-finding the rows here, and using the i instance 
                // to check the boxes and fields
                IList<IWebElement> assessmentRowsAgain = ElemGet_LMSAdmin.Grid_GetRows(ComplPathTabAssessmentsTblBody,
                    Bys.ActAssessmentsPage.ComplPathTabAssessmentsTblBodyRow);
                ElemSet_LMSAdmin.Grid_TickCheckBox(assessmentRowsAgain[i], 1);

                IList<IWebElement> assessmentRowsAgain2 = ElemGet_LMSAdmin.Grid_GetRows(ComplPathTabAssessmentsTblBody,
                    Bys.ActAssessmentsPage.ComplPathTabAssessmentsTblBodyRow);
                ElemSet_LMSAdmin.Grid_TickCheckBox(assessmentRowsAgain2[i], 2);

                IList<IWebElement> assessmentRowsAgain3 = ElemGet_LMSAdmin.Grid_GetRows(ComplPathTabAssessmentsTblBody,
                    Bys.ActAssessmentsPage.ComplPathTabAssessmentsTblBodyRow);
                ElemSet_LMSAdmin.Grid_EnterTextInField(assessmentRowsAgain3[i], 1, "2");

                IList<IWebElement> assessmentRowsAgain4 = ElemGet_LMSAdmin.Grid_GetRows(ComplPathTabAssessmentsTblBody,
                    Bys.ActAssessmentsPage.ComplPathTabAssessmentsTblBodyRow);
                ElemSet_LMSAdmin.Grid_EnterTextInField(assessmentRowsAgain4[i], 2, numberOfQuestionsNeededToPass.ToString());
            }

            // For each row, load and fill the completionPathwayPerScenarioRule object
            for (var i = 0; i < assessmentRows.Count; i++)
            {
                int assessmentOrderNumber = (i + 1);
                string assessName = ElemGet.Grid_GetCellTextByRowIndexAndColIndex(Browser, ComplPathTabAssessmentsTbl,
                    Bys.ActAssessmentsPage.ComplPathTabAssessmentsTblBodyRow, (i + 1), 1);
                string assessType = ElemGet.Grid_GetCellTextByRowIndexAndColIndex(Browser, ComplPathTabAssessmentsTbl,
                    Bys.ActAssessmentsPage.ComplPathTabAssessmentsTblBodyRow, (i + 1), 2);

                AssessmentRule assessmentRule = new AssessmentRule(assessmentOrderNumber, assessName, assessType, true, false, 0, 0, 0, "");
                assessmentRules.Add(assessmentRule);
            }

            ClickAndWait(ComplPathTabSaveBtn);
            ClickAndWait(ComplPathTabBackToComplPathwaySumLnk);

            ClickAndWait(AssessmentsTab);

            return assessmentRules;
        }



        #endregion methods: page specific



    }
}