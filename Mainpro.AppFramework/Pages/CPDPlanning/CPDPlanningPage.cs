using Browser.Core.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Threading;
using LOG4NET = log4net.ILog;


namespace Mainpro.AppFramework
{
    public class CPDPlanningPage : MainproPage, IDisposable
    {
        #region constructors
        public CPDPlanningPage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that I start so I can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        public override string PageUrl { get { return "Default.aspx"; } }

        MainproHelperMethods Help = new MainproHelperMethods();

        #endregion properties

        #region elements
        public IWebElement CreateAPersonalLearningGoalBtn { get { return this.FindElement(Bys.CPDPlanningPage.CreateAPersonalLearningGoalBtn); } }
        public IWebElement CreateAGoalFormWhatIsTheGoalTxt { get { return this.FindElement(Bys.CPDPlanningPage.CreateAGoalFormWhatIsTheGoalTxt); } }
        public IWebElement CreateAGoalFormHowWillAccomplishTxt { get { return this.FindElement(Bys.CPDPlanningPage.CreateAGoalFormHowWillAccomplishTxt); } }
        public IWebElement CreateAGoalFormWhatIsPlannedDueDateTxt { get { return this.FindElement(Bys.CPDPlanningPage.CreateAGoalFormWhatIsPlannedDueDateTxt); } }
        public SelectElement CreateAGoalFormWhatIsTheGoalTypeSelElem { get { return new SelectElement(this.FindElement(Bys.CPDPlanningPage.CreateAGoalFormWhatIsTheGoalTypeSelElem)); } }
        public IWebElement CreateAGoalFormCreateBtn { get { return this.FindElement(Bys.CPDPlanningPage.CreateAGoalFormCreateBtn); } }
        public IWebElement CreateAGoalFormCloseButton { get { return this.FindElement(Bys.CPDPlanningPage.CreateAGoalFormCloseButton); } }
        public IWebElement UpdateProgressFormSaveBtn { get { return this.FindElement(Bys.CPDPlanningPage.UpdateProgressFormSaveBtn); } }
        public IWebElement UpdateProgressFormCancelBtn { get { return this.FindElement(Bys.CPDPlanningPage.UpdateProgressFormCancelBtn); } }
        public IWebElement UpdateProgressFormCompletionDateTxt { get { return this.FindElement(Bys.CPDPlanningPage.UpdateProgressFormCompletionDateTxt); } }
        public IWebElement UpdateProgressFormSlider { get { return this.FindElement(Bys.CPDPlanningPage.UpdateProgressFormSlider); } }
        public IWebElement UpdateProgress1Lnk { get { return this.FindElement(Bys.CPDPlanningPage.UpdateProgress1Lnk); } }
        public IWebElement UpdateProgress2Lnk { get { return this.FindElement(Bys.CPDPlanningPage.UpdateProgress2Lnk); } }
        public IWebElement UpdateProgress3Lnk { get { return this.FindElement(Bys.CPDPlanningPage.UpdateProgress3Lnk); } }
        public IWebElement UpdateProgress4Lnk { get { return this.FindElement(Bys.CPDPlanningPage.UpdateProgress4Lnk); } }
        public IWebElement UpdateProgress5Lnk { get { return this.FindElement(Bys.CPDPlanningPage.UpdateProgress5Lnk); } }
        public IWebElement GoalTblCPDGoalOrObjColHdr { get { return this.FindElement(Bys.CPDPlanningPage.GoalTblCPDGoalOrObjColHdr); } }
        public IWebElement GoalsTblBody { get { return this.FindElement(Bys.CPDPlanningPage.GoalsTblBody); } }
        public IWebElement GoalTblTypeSelElem { get { return this.FindElement(Bys.CPDPlanningPage.GoalTblTypeSelElem); } }
        public IWebElement GoalTblDueDateSelElemBtn { get { return this.FindElement(Bys.CPDPlanningPage.GoalTblDueDateSelElemBtn); } }
        public IWebElement GoalTblProgressSelElem { get { return this.FindElement(Bys.CPDPlanningPage.GoalTblProgressSelElem); } }
        public IWebElement DeleteGoalFormYesBtn { get { return this.FindElement(Bys.CPDPlanningPage.DeleteGoalFormYesBtn); } }
        public IWebElement GoalsTbl { get { return this.FindElement(Bys.CPDPlanningPage.GoalsTbl); } }
        public IWebElement GoalsTblHdr { get { return this.FindElement(Bys.CPDPlanningPage.GoalsTbl); } }
        
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
            this.WaitUntil(TimeSpan.FromSeconds(60), Criteria.CPDPlanningPage.PageReady);
            Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(60));
            Thread.Sleep(500);
            this.WaitUntil(TimeSpan.FromSeconds(60), Criteria.CPDPlanningPage.PageReady);
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
            catch (Exception ex) { _log.ErrorFormat("Failed to dispose CreditSummaryPge", activeRequests.Count, ex); }
        }
        /// <summary>
        /// Clicks the user-specified button, link, tab, etc. and then waits for a window to close or open, or a page to load,
        /// depending on the element that was clicked
        /// </summary>
        /// <param name="elem">The element to click on</param>
        public dynamic ClickAndWait(IWebElement elem)
        {
            if (Browser.Exists(Bys.CPDPlanningPage.CreateAPersonalLearningGoalBtn))
            {
                if (elem.GetAttribute("outerHTML") == CreateAPersonalLearningGoalBtn.GetAttribute("outerHTML"))
                {
                    elem.Click();
                    // Outstanding defect https://code.premierinc.com/issues/browse/MAINPROREW-831. I think I can remove
                    // the try block as well as the Thread.Sleep when this is fixed
                    try
                    {
                        Browser.WaitForElement(Bys.CPDPlanningPage.CreateAGoalFormCreateBtn, ElementCriteria.IsVisible);
                        Browser.WaitJSAndJQuery();
                        Thread.Sleep(600);
                        this.WaitUntil(Criteria.CPDPlanningPage.LoadIconNotExists);
                    }
                    catch
                    {
                        if (Browser.Exists(Bys.CPDPlanningPage.CreateAPersonalLearningGoalBtn, ElementCriteria.IsVisible))
                        {
                            elem.ClickJS(Browser);
                            Browser.WaitForElement(Bys.CPDPlanningPage.CreateAGoalFormCreateBtn, ElementCriteria.IsVisible);
                            Browser.WaitJSAndJQuery();
                            Thread.Sleep(600);
                        }

                    }
                    return null;
                }
            }

            if (Browser.Exists(Bys.CPDPlanningPage.CreateAGoalFormCreateBtn))
            {
                if (elem.GetAttribute("outerHTML") == CreateAGoalFormCreateBtn.GetAttribute("outerHTML"))
                {
                    elem.Click();
                    Browser.WaitForElement(Bys.CPDPlanningPage.CreateAGoalFormCloseButton, ElementCriteria.IsVisible);
                    Browser.WaitJSAndJQuery();
                    return null;
                }
            }

            if (Browser.Exists(Bys.CPDPlanningPage.CreateAGoalFormCloseButton))
            {
                if (elem.GetAttribute("outerHTML") == CreateAGoalFormCloseButton.GetAttribute("outerHTML"))
                {
                    elem.Click();
                    this.WaitForInitialize();
                    return null;
                }
            }

            throw new Exception(string.Format("No element was found with your passed parameter, which was the '{0}' element. " +
                "You either need to add this element to a new If statement, or if the element is already added, then the page " +
                "you were on did not contain the element.",
                elem.GetAttribute("innerText")));
        }

        #endregion methods: repeated per page

        #region methods: page specific


        /// <summary>
        /// Fills the required fields on the Create a Goal form
        /// </summary>
        /// <param name="title">(Optional). Specify a goal title. If not specified, a random goal title will be used</param>
        /// <param name="completionDt">(Optional). Default = today</param>
        public Goal FillGoalForm(string title = null, DateTime completionDt = default(DateTime))
        {
            title = string.IsNullOrEmpty(title) ? "MyGoal" + DataUtils.GetRandomString(5) : title;
            Help.ClearTextBox(CreateAGoalFormWhatIsTheGoalTxt);
            CreateAGoalFormWhatIsTheGoalTxt.SendKeys(title);

            Help.ClearTextBox(CreateAGoalFormHowWillAccomplishTxt);
            CreateAGoalFormHowWillAccomplishTxt.SendKeys("I dont know");

            // If the user didnt specify a date, set the date to today. 
            if (completionDt == default(DateTime))
            {
                completionDt = currentDatetime;
                    //DateTime.Today;
            }
            Help.ClearTextBox(CreateAGoalFormWhatIsPlannedDueDateTxt);
            CreateAGoalFormWhatIsPlannedDueDateTxt.SendKeys(completionDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture));

            CreateAGoalFormWhatIsTheGoalTypeSelElem.SelectByIndex(1);
 
            return new Goal(Browser, title, CreateAGoalFormWhatIsTheGoalTypeSelElem.Options[1].Text, 
                completionDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture));
        }

        #endregion methods: page specific


    }
}
