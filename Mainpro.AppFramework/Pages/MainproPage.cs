using Browser.Core.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using Microsoft.CSharp;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using System;
using OpenQA.Selenium.Interactions;

namespace Mainpro.AppFramework
{
    public abstract class MainproPage : Page
    {
        #region Constructors

        public MainproPage(IWebDriver driver) : base(driver) { }

        #endregion

        #region Properties
        
        public static TimeZoneInfo est = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
        public DateTime currentDatetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, est);

        #endregion

        #region Elements
        public IWebElement ZendeskChatFrame { get { return this.FindElement(Bys.MainproPage.ZendeskChatFrame); } }
        public IWebElement ZendeskChatMinimizeBtn { get { return this.FindElement(Bys.MainproPage.ZendeskChatMinimizeBtn); } }
        public IWebElement PLP_ActivityDetailFormCancelBtn { get { return this.FindElement(Bys.MainproPage.PLP_ActivityDetailFormCancelBtn); } }
        public IWebElement PLP_ActivityDetailFormAddBtn { get { return this.FindElement(Bys.MainproPage.PLP_ActivityDetailFormAddBtn); } }
        public IWebElement PLP_FormattedTextFormSaveAndCloseBtn { get { return this.FindElement(Bys.MainproPage.PLP_FormattedTextFormSaveAndCloseBtn); } }
        public IWebElement PLP_AreYouSureYouWantToDeleteFormDeleteBtn { get { return this.FindElement(Bys.MainproPage.PLP_AreYouSureYouWantToDeleteFormDeleteBtn); } }
        public IWebElement PLP_FormattedTextFormFrame { get { return this.FindElement(Bys.MainproPage.PLP_FormattedTextFormFrame); } }
        public IWebElement PLP_FormattedTextFormFrameTxt { get { return this.FindElement(Bys.MainproPage.PLP_FormattedTextFormFrameTxt); } }
        public IWebElement PLP_commentBoxTxt { get{ return this.FindElement(Bys.MainproPage.PLP_commentBoxTxt); } }

        public IWebElement PLP_Header_StepNumberLabel { get { return this.FindElement(Bys.MainproPage.PLP_Header_StepNumberLabel); } }
        public IWebElement PLP_Header_1Btn { get { return this.FindElement(Bys.MainproPage.PLP_Header_1Btn); } }
        public IWebElement PLP_Header_2Btn { get { return this.FindElement(Bys.MainproPage.PLP_Header_2Btn); } }
        public IWebElement PLP_Header_3Btn { get { return this.FindElement(Bys.MainproPage.PLP_Header_3Btn); } }
        public IWebElement PLP_Header_4Btn { get { return this.FindElement(Bys.MainproPage.PLP_Header_4Btn); } }
        public IWebElement PLP_Header_5Btn { get { return this.FindElement(Bys.MainproPage.PLP_Header_5Btn); } }
        public IWebElement PLP_Header_PRBtn { get { return this.FindElement(Bys.MainproPage.PLP_Header_PRBtn); } }
        public IWebElement PLP_Header_PLPCompleteGraphLabel { get { return this.FindElement(Bys.MainproPage.PLP_Header_PLPCompleteGraphLabel); } }
        public IWebElement PLP_Menu_DropDownBtn { get { return this.FindElement(Bys.MainproPage.PLP_Menu_DropDownBtn); } }
        public IWebElement PLP_Menu_CloseBtn { get { return this.FindElement(Bys.MainproPage.PLP_Menu_CloseBtn); } }
        public IWebElement PLP_Menu_PLPToolsAndResources { get { return this.FindElement(Bys.MainproPage.PLP_Menu_PLPToolsAndResources); } }
        public IWebElement PLP_Menu_PLPActivitySumm { get { return this.FindElement(Bys.MainproPage.PLP_Menu_PLPActivitySumm); } }
        public IWebElement PLP_Menu_PrintCompletedPLP { get { return this.FindElement(Bys.MainproPage.PLP_Menu_PrintCompletedPLP); } }
        public IWebElement PLP_Menu_PrintPLPCertificate { get { return this.FindElement(Bys.MainproPage.PLP_Menu_PrintPLPCertificate); } }
        public IWebElement PLP_Menu_ContactUs { get { return this.FindElement(Bys.MainproPage.PLP_Menu_ContactUs); } }
        public IWebElement PLP_Menu_ExitToMainpro { get { return this.FindElement(Bys.MainproPage.PLP_Menu_ExitToMainpro); } }
        public IWebElement SupportInfoFormCloseBtn { get { return this.FindElement(Bys.MainproPage.SupportInfoFormCloseBtn); } }
        public IWebElement SupportInfoFormPLPSiteLbl { get { return this.FindElement(Bys.MainproPage.SupportInfoFormPLPSiteLbl); } }
        public IWebElement SupportInfoFormPLPExtnLbl { get { return this.FindElement(Bys.MainproPage.SupportInfoFormPLPExtnLbl); } }
        public IWebElement PLPTopPercentChartTxt { get { return this.FindElement(Bys.MainproPage.PLPTopPercentChartTxt); } }
        public IWebElement CurrentCycleDateLbl { get { return this.FindElement(Bys.MainproPage.CurrentCycleDateLbl); } }
        public IWebElement EnterCPDActBtn { get { return this.FindElement(Bys.MainproPage.EnterCPDActBtn); } }
        public IWebElement NotificationFormXBtn { get { return this.FindElement(Bys.MainproPage.NotificationFormXBtn); } }
        public IWebElement NotificationLbl { get { return this.FindElement(Bys.MainproPage.NotificationFormLbl); } }
        public IWebElement Menu_About { get { return this.FindElement(Bys.MainproPage.Menu_MyDashboard); } }
        public IWebElement Menu_MyCPDActivitiesList { get { return this.FindElement(Bys.MainproPage.Menu_MyCPDActivitiesList); } }
        public IWebElement LoadIcon { get { return this.FindElement(Bys.MainproPage.LoadIcon); } }
        public IWebElement LogoutLnk { get { return this.FindElement(Bys.MainproPage.LogoutLnk); } }
        public IWebElement DashboardTab { get { return this.FindElement(Bys.MainproPage.DashboardTab); } }
        public IWebElement CreditSummaryTab { get { return this.FindElement(Bys.MainproPage.CreditSummaryTab); } }
        public IWebElement HoldingAreaTab { get { return this.FindElement(Bys.MainproPage.HoldingAreaTab); } }
        public IWebElement CPDActivitiesListTab { get { return this.FindElement(Bys.MainproPage.CPDActivitiesListTab); } }
        public IWebElement CPDPlanningTab { get { return this.FindElement(Bys.MainproPage.CPDPlanningTab); } }
        public IWebElement PlpHubTab { get { return this.FindElement(Bys.MainproPage.PlpHubTab); } }
        public IWebElement ReportsTab { get { return this.FindElement(Bys.MainproPage.ReportsTab); } }
        public IWebElement CACTab { get { return this.FindElement(Bys.MainproPage.CACTab); } }
        public IWebElement CACCreditSummaryTab { get { return this.FindElement(Bys.MainproPage.CACCreditSummaryTab); } }
        public IWebElement CACHoldingAreaTab { get { return this.FindElement(Bys.MainproPage.CACHoldingAreaTab); } }
        public IWebElement CACCPDActivitiesListTab { get { return this.FindElement(Bys.MainproPage.CACCPDActivitiesListTab); } }
        public IWebElement CACReportsTab { get { return this.FindElement(Bys.MainproPage.CACReportsTab); } }
        public IWebElement CredSummCycleTblFirstRow { get { return this.FindElement(Bys.MainproPage.CredSummCycleTblFirstRow); } }
        public IWebElement CredSummCycleTblBody { get { return this.FindElement(Bys.MainproPage.CredSummCycleTblBody); } }
        public IWebElement CredSummCycleTblHdr { get { return this.FindElement(Bys.MainproPage.CredSummCycleTblHdr); } }
        public IWebElement CredSummCycleTbl { get { return this.FindElement(Bys.MainproPage.CredSummCycleTbl); } }
        public IWebElement CredSummAnnualReqsTblFirstRow { get { return this.FindElement(Bys.MainproPage.CredSummAnnualReqsTblFirstRow); } }
        public IWebElement CredSummAnnualReqsTblBody { get { return this.FindElement(Bys.MainproPage.CredSummAnnualReqsTblBody); } }
        public IWebElement CredSummAnnualReqsTblHdr { get { return this.FindElement(Bys.MainproPage.CredSummAnnualReqsTblHdr); } }
        public IWebElement CredSummAnnualReqsTbl { get { return this.FindElement(Bys.MainproPage.CredSummAnnualReqsTbl); } }
        public IWebElement CredSummCurrentYrTblFirstRow { get { return this.FindElement(Bys.MainproPage.CredSummCurrentYrTblFirstRow); } }
        public IWebElement CredSummCurrentYrTblBody { get { return this.FindElement(Bys.MainproPage.CredSummCurrentYrTblBody); } }
        public IWebElement CredSummCurrentYrTblHdr { get { return this.FindElement(Bys.MainproPage.CredSummCurrentYrTblHdr); } }
        public IWebElement CredSummCurrentYrTbl { get { return this.FindElement(Bys.MainproPage.CredSummCurrentYrTbl); } }
        public IWebElement WereSorryErrorLbl { get { return this.FindElement(Bys.MainproPage.WereSorryErrorLbl); } }
        public IWebElement DeleteFormNoBtn { get { return this.FindElement(Bys.MainproPage.DeleteFormNoBtn); } }
        public IWebElement DeleteFormYesBtn { get { return this.FindElement(Bys.MainproPage.DeleteFormYesBtn); } }
        public IWebElement AMARCPMaxCreditFormTblFirstRow { get { return this.FindElement(Bys.MainproPage.AMARCPMaxCreditFormTblFirstRow); } }
        public IWebElement AMARCPMaxCreditFormTblBody { get { return this.FindElement(Bys.MainproPage.AMARCPMaxCreditFormTblBody); } }
        public IWebElement AMARCPMaxCreditFormTblHdr { get { return this.FindElement(Bys.MainproPage.AMARCPMaxCreditFormTblHdr); } }
        public IWebElement AMARCPMaxCreditFormTbl { get { return this.FindElement(Bys.MainproPage.AMARCPMaxCreditFormTbl); } }
        public IWebElement ClickHereToViewYourAmaRCPCreditsLnk { get { return this.FindElement(Bys.MainproPage.ClickHereToViewYourAmaRCPCreditsLnk); } }
        public IWebElement InfoBtn { get { return this.FindElement(Bys.MainproPage.InfoBtn); } }
        public IWebElement StepTitle { get { return this.FindElement(Bys.MainproPage.StepTitle); } }
        public IList<IWebElement> PanelBodyList { get { return this.FindElements(Bys.MainproPage.PanelBodyList); } }
        public IWebElement SelectYourPathway_PageTitle { get { return this.FindElement(Bys.MainproPage.SelectYourPathway_PageTitle); } }
        public IList<IWebElement> InfoButtons_SelectYourPathway { get { return this.FindElements(Bys.MainproPage.InfoButtons_SelectYourPathway); } }



        #endregion Elements

        #region Methods

        /// <summary>
        /// Clicks the user-specified button, link, tab, etc. and then waits for a window to close or open, or a page to load,
        /// depending on the element that was clicked
        /// </summary>
        /// <param name="elem">The element to click on</param>
        public dynamic ClickAndWaitBasePage(IWebElement elem)
        {
            if (Browser.Exists(Bys.MainproPage.PLP_Menu_PLPActivitySumm))
            {
                if (elem.GetAttribute("outerHTML") == PLP_Menu_PLPActivitySumm.GetAttribute("outerHTML"))
                {
                    elem.Click();
                    Browser.WaitForElement(Bys.MainproPage.PLPActivitySummaryLbl,
                        ElementCriteria.IsVisible);
                    Browser.WaitJSAndJQuery();
                    return null;
                }
            }
            if (Browser.Exists(Bys.MainproPage.PLP_Menu_ContactUs))
            {
                if (elem.GetAttribute("outerHTML") == PLP_Menu_ContactUs.GetAttribute("outerHTML"))
                {
                    elem.Click();
                    Browser.WaitForElement(Bys.MainproPage.SupportInfoFormSupportInfoLbl,
                        ElementCriteria.IsVisible);
                    Browser.WaitJSAndJQuery();
                    return null;
                }
            }

            if (Browser.Exists(Bys.MainproPage.SupportInfoFormCloseBtn))
            {
                if (elem.GetAttribute("outerHTML") == SupportInfoFormCloseBtn.GetAttribute("outerHTML"))
                {
                    elem.Click();
                    Browser.WaitJSAndJQuery();
                    return null;
                }
            }

            if (Browser.Exists(Bys.MainproPage.PLP_Header_2Btn))
            {
                if (elem.GetAttribute("outerHTML") == PLP_Header_2Btn.GetAttribute("outerHTML"))
                {
                    PLP_Header_2Btn.Click();
                    Step2Page PS2 = new Step2Page(Browser);
                    PS2.WaitForInitialize();
                    return PS2;
                }
            } 
            if (Browser.Exists(Bys.MainproPage.PLP_Header_3Btn))
            {
                if (elem.GetAttribute("outerHTML") == PLP_Header_3Btn.GetAttribute("outerHTML"))
                {
                    PLP_Header_3Btn.Click();
                    Step3Page PS3 = new Step3Page(Browser);
                    PS3.WaitForInitialize();
                    return PS3;
                }
            } 
            if (Browser.Exists(Bys.MainproPage.PLP_Header_4Btn))
            {
                if (elem.GetAttribute("outerHTML") == PLP_Header_4Btn.GetAttribute("outerHTML"))
                {
                    PLP_Header_4Btn.Click();
                    Step4Page PS4 = new Step4Page(Browser);
                    PS4.WaitForInitialize();
                    return PS4;
                }
            } 
            if (Browser.Exists(Bys.MainproPage.PLP_Header_5Btn))
            {
                if (elem.GetAttribute("outerHTML") == PLP_Header_5Btn.GetAttribute("outerHTML"))
                {
                    PLP_Header_5Btn.Click();
                    Step5Page PS5 = new Step5Page(Browser);
                    PS5.WaitForInitialize();
                    return PS5;
                }
            } 
            if (Browser.Exists(Bys.MainproPage.PLP_Header_PRBtn))
            {
                if (elem.GetAttribute("outerHTML") == PLP_Header_PRBtn.GetAttribute("outerHTML"))
                {
                    PLP_Header_PRBtn.Click();
                    StepPRPage PSPR = new StepPRPage(Browser);
                    PSPR.WaitForInitialize();
                    return PSPR;
                }
            }
            if (Browser.Exists(Bys.MainproPage.PLP_Menu_ExitToMainpro))
            {
                if (elem.GetAttribute("outerHTML") == PLP_Menu_ExitToMainpro.GetAttribute("outerHTML"))
                {
                    PLP_Menu_ExitToMainpro.Click();
                    DashboardPage DP = new DashboardPage(Browser);
                    DP.WaitForInitialize();
                    return DP;
                }
            }

            if (Browser.Exists(Bys.MainproPage.PLP_Menu_DropDownBtn))
            {
                if (elem.GetAttribute("outerHTML") == PLP_Menu_DropDownBtn.GetAttribute("outerHTML"))
                {
                    PLP_Menu_DropDownBtn.Click();
                    Browser.WaitForElement(Bys.MainproPage.PLP_Menu_ExitToMainpro, ElementCriteria.IsVisible);
                    Browser.WaitJSAndJQuery();
                    return null;
                }
            }
            if (Browser.Exists(Bys.MainproPage.PLP_Menu_CloseBtn))
            {
                if (elem.GetAttribute("outerHTML") == PLP_Menu_CloseBtn.GetAttribute("outerHTML"))
                {
                    PLP_Menu_CloseBtn.Click();
                    Browser.WaitJSAndJQuery();
                    Browser.WaitForElement(Bys.MainproPage.PLP_Menu_DropDownBtn, ElementCriteria.IsVisible);
                    return null;
                }
            }
            if (Browser.Exists(Bys.MainproPage.PLP_ActivityDetailFormAddBtn))
            {
                if (elem.GetAttribute("outerHTML") == PLP_ActivityDetailFormAddBtn.GetAttribute("outerHTML"))
                {
                    PLP_ActivityDetailFormAddBtn.Click();
                    Step3Page PS3 = new Step3Page(Browser);
                    PS3.WaitUntil(Criteria.Step3Page.ActivityDetailFormDateTxtNotExists);
                    this.WaitForInitialize();
                    return null;
                }
            } 

            if (Browser.Exists(Bys.MainproPage.PLP_AreYouSureYouWantToDeleteFormDeleteBtn))
            {
                if (elem.GetAttribute("outerHTML") == PLP_AreYouSureYouWantToDeleteFormDeleteBtn.GetAttribute("outerHTML"))
                {
                    PLP_AreYouSureYouWantToDeleteFormDeleteBtn.Click();
                    Step3Page PS3 = new Step3Page(Browser);
                    PS3.WaitUntil(Criteria.Step3Page.AreYouSureYouWantToDeleteFormDeleteBtnNotVisible);
                    this.WaitForInitialize();
                    return null;
                }
            }

            if (Browser.Exists(Bys.MainproPage.PLP_FormattedTextFormSaveAndCloseBtn))
            {
                if (elem.GetAttribute("outerHTML") == PLP_FormattedTextFormSaveAndCloseBtn.GetAttribute("outerHTML"))
                {
                    PLP_FormattedTextFormSaveAndCloseBtn.Click();
                    // Need to add a common wait here for all pages this control exists on 
                    Browser.WaitForElement(Bys.MainproPage.PLP_FormattedTextFormFrame, ElementCriteria.IsNotVisible);
                    Browser.WaitJSAndJQuery();
                    Thread.Sleep(500);
                    return null;
                }
            }

            if (Browser.Exists(Bys.MainproPage.DashboardTab))
            {
                if (elem.GetAttribute("outerHTML") == DashboardTab.GetAttribute("outerHTML"))
                {
                    elem.Click();
                    DashboardPage page = new DashboardPage(Browser);
                    page.WaitForInitialize();
                    return page;
                }
            }

            if (Browser.Exists(Bys.MainproPage.EnterCPDActBtn))
            {
                if (elem.Text == EnterCPDActBtn.Text)
                {
                    elem.Click();
                    EnterACPDActivityPage page = new EnterACPDActivityPage(Browser);
                    page.WaitForInitialize();
                    return page;
                }
            }

            if (Browser.Exists(Bys.MainproPage.NotificationFormXBtn))
            {
                if (elem.GetAttribute("outerHTML") == NotificationFormXBtn.GetAttribute("outerHTML"))
                {
                    elem.Click();
                    Thread.Sleep(500);
                    return null;
                }
            }

            if (Browser.Exists(Bys.MainproPage.HoldingAreaTab))
            {
                if (elem.GetAttribute("outerHTML") == HoldingAreaTab.GetAttribute("outerHTML"))
                {
                    elem.Click();
                    HoldingAreaPage page = new HoldingAreaPage(Browser);
                    page.WaitForInitialize();
                    return page;
                }
            }

            if (Browser.Exists(Bys.MainproPage.ReportsTab))
            {
                if (elem.GetAttribute("outerHTML") == ReportsTab.GetAttribute("outerHTML"))
                {
                    elem.Click();
                    ReportsPage page = new ReportsPage(Browser);
                    page.WaitForInitialize();
                    return page;
                }
            }

            if (Browser.Exists(Bys.MainproPage.CPDActivitiesListTab))
            {
                if (elem.GetAttribute("outerHTML") == CPDActivitiesListTab.GetAttribute("outerHTML"))
                {
                    elem.Click();
                    CPDActivitiesListPage page = new CPDActivitiesListPage(Browser);
                    page.WaitForInitialize();
                    return page;
                }
            }

            if (Browser.Exists(Bys.MainproPage.CPDPlanningTab))
            {
                if (elem.GetAttribute("outerHTML") == CPDPlanningTab.GetAttribute("outerHTML"))
                {
                    elem.Click();
                    CPDPlanningPage page = new CPDPlanningPage(Browser);
                    page.WaitForInitialize();
                    return page;
                }
            }
            if (Browser.Exists(Bys.MainproPage.PlpHubTab))
            {
                if (elem.GetAttribute("outerHTML") == PlpHubTab.GetAttribute("outerHTML"))
                {
                    elem.Click();
                    PLPHubPage page = new PLPHubPage(Browser);
                    page.WaitForInitialize();
                    return page;
                }
            }

            if (Browser.Exists(Bys.MainproPage.CreditSummaryTab))
            {
                if (elem.GetAttribute("outerHTML") == CreditSummaryTab.GetAttribute("outerHTML"))
                {
                    elem.Click();
                    CreditSummaryPage page = new CreditSummaryPage(Browser);
                    page.WaitForInitialize();
                    return page;
                }
            }

            if (Browser.Exists(Bys.MainproPage.LogoutLnk))
            {
                if (elem.GetAttribute("outerHTML") == LogoutLnk.GetAttribute("outerHTML"))
                {
                    elem.Click();
                    Browser.WaitForURLToContainString("cfpc.ca", TimeSpan.FromSeconds(30));
                    return null;
                }
            }

            if (Browser.Exists(Bys.MainproPage.ClickHereToViewYourAmaRCPCreditsLnk))
            {
                if (elem.GetAttribute("outerHTML") == ClickHereToViewYourAmaRCPCreditsLnk.GetAttribute("outerHTML"))
                {
                    elem.Click();
                    Browser.WaitForElement(Bys.MainproPage.AMARCPMaxCreditFormTblFirstRow, ElementCriteria.IsVisible);
                    Browser.WaitJSAndJQuery();
                    return null;
                }
            }

            throw new Exception(string.Format("No element was found with your passed parameter, which was the '{0}' element. You either need to add " +
                "this element to a new If statement, or if the element is already added, then the page you were on did not contain the element.",
                elem.GetAttribute("innerText")));
        }



        #endregion Methods
    }
}
