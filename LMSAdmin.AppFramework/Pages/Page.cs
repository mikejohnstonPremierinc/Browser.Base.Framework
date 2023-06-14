using Browser.Core.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using Microsoft.CSharp;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using System;
using OpenQA.Selenium.Interactions;
using LMSAdmin.AppFramework.ConstantsLMSAdmin;

namespace LMSAdmin.AppFramework
{
    public abstract class Page : Browser.Core.Framework.Page
    {
        #region Constructors

        public Page(IWebDriver driver) : base(driver) { }

        #endregion

        #region Elements

        public IWebElement BackToActivityBtn { get { return this.FindElement(Bys.Page.BackToActivityBtn); } }
        public IWebElement LogoutLnk { get { return this.FindElement(Bys.Page.LogoutLnk); } }

        public IWebElement TreeLinks_FrontMatter { get { return this.FindElement(Bys.Page.TreeLinks_FrontMatter); } }
        public IWebElement TreeLinks_Awards { get { return this.FindElement(Bys.Page.TreeLinks_Awards); } }

        public IWebElement TreeLinks_Assessments { get { return this.FindElement(Bys.Page.TreeLinks_Assessments); } }

        
        

        public IWebElement TreeLinks_Accreditation { get { return this.FindElement(Bys.Page.TreeLinks_Accreditation); } }
        public IWebElement TreeLinks_Legacy_Accreditation { get { return this.FindElement(Bys.Page.TreeLinks_Legacy_Accreditation); } }

        public IWebElement TreeLinks_Activities { get { return this.FindElement(Bys.Page.TreeLinks_Activities); } }

        public IWebElement TreeLinks_MainActivity { get { return this.FindElement(Bys.Page.TreeLinks_MainActivity); } }
        
        public IWebElement TermsAndConditionsLnk { get { return this.FindElement(Bys.Page.TermsAndConditionsLnk); } }

        public IWebElement SearchBtn { get { return this.FindElement(Bys.Page.SearchBtn); } }
        public IWebElement AcceptBtn { get { return this.FindElement(Bys.Page.AcceptBtn); } }

        public IWebElement RecentItemsTbl { get { return this.FindElement(Bys.Page.RecentItemsTbl); } }

        public IWebElement SearchTxt { get { return this.FindElement(Bys.Page.SearchTxt); } }
        public IWebElement ProjectsTab { get { return this.FindElement(Bys.Page.ProjectsTab); } }
        public IWebElement SetUpTab { get { return this.FindElement(Bys.Page.SetUpTab); } }
        public IWebElement PlanningTab { get { return this.FindElement(Bys.Page.PlanningTab); } }
        public IWebElement DistributionTab { get { return this.FindElement(Bys.Page.DistributionTab); } }

        public IWebElement AlertNotificationIconMsg { get { return this.FindElement(Bys.Page.AlertNotificationIconMsg); } }

        public IWebElement ConfirmtationPopUpMsg { get { return this.FindElement(Bys.Page.ConfirmtationPopUpMsg); } }
        public IWebElement confirmMsgPopupOkBtn { get { return this.FindElement(Bys.Page.confirmMsgPopupOkBtn); } }

        public IWebElement ActivityStageLbl { get { return this.FindElement(Bys.Page.ActivityStageLbl); } }
        public IWebElement Legacy_SetupLnk { get { return this.FindElement(Bys.Page.SetupLnk); } }
        public IWebElement Steps_AccreditationLbl { get { return this.FindElement(Bys.Page.Steps_AccreditationLbl); } }
        public IWebElement Steps_CompletionPathwayLbl { get { return this.FindElement(Bys.Page.Steps_CompletionPathwayLbl); } }
        public IWebElement Steps_AwardLbl { get { return this.FindElement(Bys.Page.Steps_AwardLbl); } }
        public IWebElement TreeLinks_Content { get { return this.FindElement(Bys.Page.TreeLinks_Content); } }

        #endregion Elements

        #region methods: page specific

        /// <summary>
        /// Clicks the user-specified element that exists on the base page of cme, and then waits for a window to close or open,
        /// or a page to load, depending on the element that was clicked
        /// </summary>
        /// <param name="buttonOrLinkElem">The element to click on</param>
        public dynamic ClickAndWaitBasePage(IWebElement buttonOrLinkElem)
        {
            // Error handler to make sure that the button that the tester passed in the parameter is actually on the page
            if (Browser.Exists(Bys.Page.ProjectsTab))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == ProjectsTab.GetAttribute("outerHTML"))
                {
                    Thread.Sleep(5000);
                    buttonOrLinkElem.Click();
                    ProjectsPage page = new ProjectsPage(Browser);
                    page.WaitForInitialize();
                    return page;
                }
            }

            if (Browser.Exists(Bys.Page.DistributionTab))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == DistributionTab.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    DistributionPage page = new DistributionPage(Browser);
                    page.WaitForInitialize();
                    return page;
                }
            }

            if (Browser.Exists(Bys.Page.TreeLinks_MainActivity))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == TreeLinks_MainActivity.GetAttribute("outerHTML"))
                {
                    TreeLinks_MainActivity.Click();
                    ActMainPage page = new ActMainPage(Browser);
                    page.WaitForInitialize();
                    // For some reason, sometimes on certain portals, it defaults to the Publishing Details tab. So we are clicking the Details tab
                    // here just in case
                    ActMainPage AMP = new ActMainPage(Browser);
                    AMP.ClickAndWait(AMP.DetailsTab);
                    return page;
                }
            }          

            if (Browser.Exists(Bys.Page.TreeLinks_Accreditation))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == TreeLinks_Accreditation.GetAttribute("outerHTML"))
                {
                    TreeLinks_Accreditation.Click();
                    try
                    { 
                        Browser.WaitForElement(Bys.Page.AcceptBtn, TimeSpan.FromSeconds(20), ElementCriteria.IsVisible); 
                        AcceptBtn.Click(Browser);
                        ActAccreditationPage page = new ActAccreditationPage(Browser);
                        page.WaitForInitialize();
                        return page;
                    }
                    catch 
                    {
                        ActAccreditationPage page = new ActAccreditationPage(Browser);
                        page.WaitForInitialize();
                        return page;
                    }
                    
                }
            }

            if (Browser.Exists(Bys.Page.Steps_AccreditationLbl))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == Steps_AccreditationLbl.GetAttribute("outerHTML"))
                {
                    Steps_AccreditationLbl.Click();
                    ActAccreditationPage page = new ActAccreditationPage(Browser);
                    page.WaitForInitialize();
                    return page;
                }
            }

            if (Browser.Exists(Bys.Page.Steps_CompletionPathwayLbl))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == Steps_CompletionPathwayLbl.GetAttribute("outerHTML"))
                {
                    Steps_CompletionPathwayLbl.Click();
                    ActCompletionPathwayPage page = new ActCompletionPathwayPage(Browser);
                    page.WaitForInitialize();
                    return page;
                }
            }
            if (Browser.Exists(Bys.Page.Steps_AwardLbl))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == Steps_AwardLbl.GetAttribute("outerHTML"))
                {
                    Steps_AwardLbl.Click();
                    ActAwardsPage page = new ActAwardsPage(Browser);
                    page.WaitForInitialize();
                    return page;
                }
            }

            if (Browser.Exists(Bys.Page.BackToActivityBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == BackToActivityBtn.GetAttribute("outerHTML"))
                {                 
                    BackToActivityBtn.Click();
                    ActMainPage page = new ActMainPage(Browser);
                    page.WaitForInitialize();
                    return page;
                }
            }

            if (Browser.Exists(Bys.Page.LogoutLnk))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == LogoutLnk.GetAttribute("outerHTML"))
                {
                    LogoutLnk.Click();
                    LoginPage page = new LoginPage(Browser);
                    page.WaitForInitialize();
                    return page;
                }
            }

            if (Browser.Exists(Bys.Page.TreeLinks_FrontMatter))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == TreeLinks_FrontMatter.GetAttribute("outerHTML"))
                {
                    TreeLinks_FrontMatter.Click();
                    ActFrontMatterPage page = new ActFrontMatterPage(Browser);
                    page.WaitForInitialize();
                    return page;
                }
            }            

            if (Browser.Exists(Bys.Page.TreeLinks_Assessments))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == TreeLinks_Assessments.GetAttribute("outerHTML"))
                {
                    TreeLinks_Assessments.Click();
                    ActAssessmentsPage page = new ActAssessmentsPage(Browser);
                    page.WaitForInitialize();
                    return page;
                }
            }
            if (Browser.Exists(Bys.Page.TreeLinks_Content))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == TreeLinks_Content.GetAttribute("outerHTML"))
                {
                    TreeLinks_Content.Click();
                    ActContentPage page = new ActContentPage(Browser);
                    page.WaitForInitialize();
                    return page;
                }
            }
            if (Browser.Exists(Bys.Page.TreeLinks_Awards))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == TreeLinks_Awards.GetAttribute("outerHTML"))
                {
                    TreeLinks_Awards.Click();
                    ActAwardsPage page = new ActAwardsPage(Browser);
                    page.WaitForInitialize();
                    return page;
                }
            }

            if (Browser.Exists(Bys.Page.SetupLnk))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == Legacy_SetupLnk.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    Legacy_SetupPage page = new Legacy_SetupPage(Browser);
                    page.WaitForInitialize();
                    return page;
                }
            }

           
            throw new Exception(string.Format("No element was found with your passed parameter, which was the '{0}' element. You either need to add " +
                "this element to a new If statement, or if the element is already added, then the page you were on did not contain the element.",
                buttonOrLinkElem.GetAttribute("innerText")));
        }

        /// <summary>
        /// Clicks the user-specified element that exists on the base page of old cme360 , and then waits for a window to close or open,
        /// or a page to load, depending on the element that was clicked
        /// </summary>
        /// <param name="buttonOrLinkElem">The element to click on</param>
        public dynamic ClickAndWaitLegacyBasePage(IWebElement buttonOrLinkElem)
        {
            // Error handler to make sure that the button that the tester passed in the parameter is actually on the page
            
            if (Browser.Exists(Bys.Page.TreeLinks_Legacy_Accreditation))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == TreeLinks_Legacy_Accreditation.GetAttribute("outerHTML"))
                {
                    TreeLinks_Legacy_Accreditation.Click();
                    Legacy_ActAccreditationPage page = new Legacy_ActAccreditationPage(Browser);
                    page.WaitForInitialize();
                    return page;
                }
            }
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == TreeLinks_Awards.GetAttribute("outerHTML"))
                {
                    TreeLinks_Awards.Click();
                    Legacy_ActAwardsPage page = new Legacy_ActAwardsPage(Browser);
                    page.WaitForInitialize();
                    return page;
                }
            }
            throw new Exception(string.Format("No element was found with your passed parameter, which was the '{0}' element. You either need to add " +
               "this element to a new If statement, or if the element is already added, then the page you were on did not contain the element.",
               buttonOrLinkElem.GetAttribute("innerText")));

        }

            /// <summary>
            /// Clicks on a link in the Recent Items frame on the left-hand side, then waits for that cooresponding page to load
            /// </summary>
            /// <param name="category"></param>
            /// <param name="itemText"></param>
            /// <returns></returns>
            public dynamic GoToRecentItem(Constants_LMSAdmin.RecentItemCategory category, string itemText)
        {
            IWebElement link = RecentItemsTbl.FindElement(By.LinkText(itemText));

            switch (category)
            {
                case Constants_LMSAdmin.RecentItemCategory.Activity:
                    link.Click();
                    ActMainPage page = new ActMainPage(Browser);
                    page.WaitForInitialize();
                    return page;


                case Constants_LMSAdmin.RecentItemCategory.Project:
                    return null;
            }

            return null;
        }



        #endregion methods: page specific
    }
}