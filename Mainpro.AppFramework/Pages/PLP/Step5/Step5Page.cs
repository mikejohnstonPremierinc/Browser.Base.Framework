using Browser.Core.Framework;
using LMS.Data;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using LOG4NET = log4net.ILog;

namespace Mainpro.AppFramework
{
    public class Step5Page : MainproPage, IDisposable
    {
        #region constructors
        public Step5Page(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that WE start so we can clean them up at the end.
        private List<string> activeRequests = new List<string>();
        MainproHelperMethods Help = new MainproHelperMethods();
        public override string PageUrl { get { return "login.aspx?action=enablelogin"; } }

        #endregion properties

        #region elements            
        public IWebElement NextBtn { get { return this.FindElement(Bys.Step5Page.NextBtn); } }
        public IWebElement BackBtn { get { return this.FindElement(Bys.Step5Page.BackBtn); } }
        public IWebElement YouWantToExitPopup_ExitBtn { get { return this.FindElement(Bys.Step5Page.YouWantToExitPopup_ExitBtn); } }
        public IWebElement YouWantToExitPopup_NoBtn { get { return this.FindElement(Bys.Step5Page.YouWantToExitPopup_NoBtn); } }
        public IWebElement YouWantToExitPopupXBtn { get { return this.FindElement(Bys.Step5Page.YouWantToExitPopupXBtn); } }
        public IWebElement PlusActivitiesBtn { get { return this.FindElement(Bys.Step5Page.PlusActivitiesBtn); } }
        public IWebElement ExpandAllBtn { get { return this.FindElement(Bys.Step5Page.ExpandAllBtn); } }
        public IWebElement CollapseAllBtn { get { return this.FindElement(Bys.Step5Page.CollapseAllBtn); } }        
        public IWebElement GoToBottomBtn { get { return this.FindElement(Bys.Step5Page.GoToBottomBtn); } }
        public IWebElement PreReflectionCPDActivitiesTblBodyFirstRow { get { return this.FindElement(Bys.Step5Page.PreReflectionCPDActivitiesTblBodyFirstRow); } }
        public IWebElement PreReflectionCPDActivitiesTblBody { get { return this.FindElement(Bys.Step5Page.PreReflectionCPDActivitiesTblBody); } }
        public IWebElement PreReflectionCPDActivitiesTblEmptyBody { get { return this.FindElement(Bys.Step5Page.PreReflectionCPDActivitiesTblEmptyBody); } }
        public IWebElement PreReflectionCPDActivitiesTblHdr { get { return this.FindElement(Bys.Step5Page.PreReflectionCPDActivitiesTblHdr); } }
        public IWebElement PreReflectionCPDActivitiesTbl { get { return this.FindElement(Bys.Step5Page.PreReflectionCPDActivitiesTbl); } }
        public IWebElement UsefulCPDActivitiesTblBodyFirstRow { get { return this.FindElement(Bys.Step5Page.UsefulCPDActivitiesTblBodyFirstRow); } }
        public IWebElement UsefulCPDActivitiesTblBody { get { return this.FindElement(Bys.Step5Page.UsefulCPDActivitiesTblBody); } }
        public IWebElement UsefulCPDActivitiesTblHdr { get { return this.FindElement(Bys.Step5Page.UsefulCPDActivitiesTblHdr); } }
        public IWebElement UsefulCPDActivitiesTbl { get { return this.FindElement(Bys.Step5Page.UsefulCPDActivitiesTbl); } }
        public IWebElement CPDActRecomYesCommentTxt { get { return this.FindElement(Bys.Step5Page.CPDActRecomYesCommentTxt); } }
        public IWebElement CPDActRecomPartialCommentTxt { get { return this.FindElement(Bys.Step5Page.CPDActRecomPartialCommentTxt); } }
        public IWebElement CPDActRecomNoCommentTxt { get { return this.FindElement(Bys.Step5Page.CPDActRecomNoCommentTxt); } }

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
            this.WaitUntil(TimeSpan.FromSeconds(60), Criteria.Step5Page.PageReady);
            Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(60));
            Thread.Sleep(500);
            this.WaitUntil(TimeSpan.FromSeconds(60), Criteria.Step5Page.PageReady);
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
            if (Browser.Exists(Bys.Step5Page.NextBtn))
            {
                if (elem.GetAttribute("outerHTML") == NextBtn.GetAttribute("outerHTML"))
                {
                    if (Browser.Url.Contains("step5submission"))
                    {
                        NextBtn.Click(); Browser.WaitJSAndJQuery();
                        Thread.Sleep(60);
                        StepPRPage page = new StepPRPage(Browser);
                        page.WaitForInitialize();
                        return page;
                    }
                    else
                    {
                        NextBtn.Click();
                        Browser.WaitJSAndJQuery();
                        if (PLP_Header_StepNumberLabel.Text.Contains("Step 5"))
                        {
                            this.WaitForInitialize();
                            return this;
                        }
                    }
                    
                }
            }

            if (Browser.Exists(Bys.Step5Page.BackBtn))
            {
                if (elem.GetAttribute("outerHTML") ==  BackBtn.GetAttribute("outerHTML"))
                {
                    BackBtn.Click();
                    Browser.WaitJSAndJQuery();
                    return null;
                }
            }
            if (Browser.Exists(Bys.Step5Page.YouWantToExitPopup_ExitBtn))
            {
                if (elem.GetAttribute("outerHTML") == YouWantToExitPopup_ExitBtn.GetAttribute("outerHTML"))
                {
                    YouWantToExitPopup_ExitBtn.Click();
                    DashboardPage DP = new DashboardPage(Browser);
                    DP.WaitForInitialize();
                    return DP;
                }
            }
             if (Browser.Exists(Bys.Step5Page.YouWantToExitPopup_NoBtn))
            {
                if (elem.GetAttribute("outerHTML") == YouWantToExitPopup_NoBtn.GetAttribute("outerHTML"))
                {
                    YouWantToExitPopup_NoBtn.Click();
                    Browser.WaitJSAndJQuery();
                    Thread.Sleep(TimeSpan.FromSeconds(60));
                    return null;
                }
            } if (Browser.Exists(Bys.Step5Page.YouWantToExitPopupXBtn))
            {
                if (elem.GetAttribute("outerHTML") == YouWantToExitPopupXBtn.GetAttribute("outerHTML"))
                {
                    elem.Click();
                    Browser.WaitJSAndJQuery();
                    Thread.Sleep(TimeSpan.FromSeconds(60));
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

        public void FillShareAnyBarrier()
        {
            Assert.True(Browser.Url.Contains("goalbarriers"), "Step 5> Share any Barriers> screen not loaded");
            Help.PLP_AddFormattedText(Browser, "Testing Share any Barrier",
                Const_Mainpro.PLP_TextboxlabelText.Pleasewritemax1000Txt);
        }
        public void FillBarriersToAchievingGoals()
        {
            Assert.True(Browser.Url.Contains("barrierstoachievinggoalsdetails"), "Step 5> Please describe any barriers prevented you from achieving your goals> screen not loaded");
            Help.PLP_AddFormattedText(Browser, "Testing Share any Barrier",
                Const_Mainpro.PLP_TextboxlabelText.Pleasewritemax1000Txt);
        }
        public void FillDetermineSuccess() 
        {
            Assert.True(Browser.Url.Contains("goalsuccesses"), "Step 5> Determine Success> screen not loaded");
            Help.PLP_AddFormattedText(Browser, "Testing Barriers I encountered",
                Const_Mainpro.PLP_TextboxlabelText.Pleasewritemax1000Txt);
        }

        public void SelectIfGoalAchieved(String OptionToBeSelected)
        {
            Assert.True(Browser.Url.Contains("goalachievement"), "Step 5> Goal achievement > screen not loaded");
            Help.PLP_ClickCheckBoxOrRadioButton(Browser, OptionToBeSelected);
        }
        public void SelectifCPD_SuggestedActivitiesHelpedGoals(String OptionToBeSelected)
        {
            Assert.True(Browser.Url.Contains("suggestedactivitieshelpedgoals"), "Step 5>Suggested CPD Activities Helpful > screen not loaded");
            Help.PLP_ClickCheckBoxOrRadioButton(Browser, OptionToBeSelected);
        }
        public void SelectifCPD_ActivityRecommendationsHelped(String OptionToBeSelected)
        {
            Assert.True(Browser.Url.Contains("activityrecommendationshelped"), "Step 5>CPC activities recommendations helpful > screen not loaded");
            Help.PLP_ClickCheckBoxOrRadioButton(Browser, OptionToBeSelected);
            //Once option selected, wait for the comment box to appear and enter value in the text box
            Browser.WaitForElement(Bys.MainproPage.PLP_commentBoxTxt, ElementCriteria.IsVisible);
            IWebElement commentBoxTxt = Browser.FindElement(Bys.MainproPage.PLP_commentBoxTxt);
            if (commentBoxTxt.Enabled)
            {
                commentBoxTxt.SendKeys("Testing Comment");
            }
            
        }

        #endregion methods: page specific



    }
}
