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
    public class Step4Page : MainproPage, IDisposable
    {
        #region constructors
        public Step4Page(IWebDriver driver) : base(driver)
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
        public IWebElement SubmitBtn { get { return this.FindElement(Bys.Step4Page.SubmitBtn); } }
        public IWebElement NextBtn { get { return this.FindElement(Bys.Step4Page.NextBtn); } }
        public IWebElement BackBtn { get { return this.FindElement(Bys.Step4Page.BackBtn); } }
        public IWebElement ExpandAllBtn { get { return this.FindElement(Bys.Step4Page.ExpandAllBtn); } }
        public IWebElement CollapseAllBtn { get { return this.FindElement(Bys.Step4Page.CollapseAllBtn); } }        
        public IWebElement GoToBottomBtn { get { return this.FindElement(Bys.Step4Page.GoToBottomBtn); } }


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
            this.WaitUntil(TimeSpan.FromSeconds(60), Criteria.Step4Page.PageReady);
            Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(60));
            Thread.Sleep(500);
            this.WaitUntil(TimeSpan.FromSeconds(60), Criteria.Step4Page.PageReady);
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
            if (Browser.Exists(Bys.Step4Page.SubmitBtn))
            {
                if (elem.GetAttribute("outerHTML") == SubmitBtn.GetAttribute("outerHTML"))
                {
                    SubmitBtn.Click();
                    this.WaitForInitialize();
                    Thread.Sleep(500);
                    return this;
                }
            }
            if (Browser.Exists(Bys.Step4Page.BackBtn))
            {
                if (elem.GetAttribute("outerHTML") == BackBtn.GetAttribute("outerHTML"))
                {
                    BackBtn.Click();
                    Browser.WaitJSAndJQuery();
                    return null;
                }
            }
            if (Browser.Exists(Bys.Step4Page.NextBtn))
            {
                if (elem.GetAttribute("outerHTML") == NextBtn.GetAttribute("outerHTML"))
                {
                    NextBtn.Click();
                    this.WaitForInitialize();
                    //try { NextBtn.Click(); }
                    //catch {
                    //    this.RefreshPage();
                    //    Browser.WaitJSAndJQuery();
                    //    NextBtn.ClickJS(Browser); }                    
                    //this.WaitForInitialize();

                    if (PLP_Header_StepNumberLabel.Text.Contains("Step 4"))
                    {
                        return this;
                    }
                    else
                    {
                        Step5Page page = new Step5Page(Browser);
                        page.WaitForInitialize();
                        return page;
                    }
                }
            }
            
            throw new Exception(string.Format("No element was found with your passed parameter, which was the '{0}' element. " +
                "You either need to add this element to a new If statement, or if the element is already added, then the page " +
                "you were on did not contain the element.",
                elem.GetAttribute("innerText")));
        }


        #endregion methods: per page

        #region methods: page specific


        public void FillGoalCTC(string forGoal)
        {
            Help.PLP_AddFormattedText(Browser,
                forGoal + " Testing Commitment to change",
                Const_Mainpro.PLP_TextboxlabelText.Step4CTCTxt);
        }
        
        public void FillStep4Screens(bool isSelfGuided=false, List<string> goalTitles = null, bool iIconClickCheck=false,
            bool ToolsOptionsClickFromUserProfileCheck=false)
        {
            //verify tools and resource section
            PLP_Event.PLP_ToolsAndResourcesSection(Browser, iIconClickCheck, ToolsOptionsClickFromUserProfileCheck);
            ClickAndWait(NextBtn);
            
            foreach(string goalTitle in goalTitles)
            {
                //verify tools and resource section
                PLP_Event.PLP_ToolsAndResourcesSection(Browser, iIconClickCheck, ToolsOptionsClickFromUserProfileCheck);
                Assert.True(Browser.Exists(By.XPath(string.Format("//span[text()='{0}']", goalTitle)),
                    ElementCriteria.IsVisible));
                FillGoalCTC(goalTitle);
                ClickAndWait(NextBtn);
            }                       
            //Attestation screen applicable for PeerVersion ONLY
            if (!isSelfGuided)
            {
                //verify tools and resource section
                PLP_Event.PLP_ToolsAndResourcesSection(Browser, iIconClickCheck, ToolsOptionsClickFromUserProfileCheck);
                Help.PLP_EnterText(Browser, "First Name", "testing first name");
                Help.PLP_EnterText(Browser, "Last Name", "testing last name");
                Thread.Sleep(500);
                ClickAndWait(NextBtn);
            }
            ClickAndWait(SubmitBtn);
            //verify tools and resource section
            PLP_Event.PLP_ToolsAndResourcesSection(Browser, iIconClickCheck, ToolsOptionsClickFromUserProfileCheck);
            ClickAndWait(NextBtn);
        }

        #endregion methods: page specific



    }
}
