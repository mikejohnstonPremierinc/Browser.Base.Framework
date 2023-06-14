using Browser.Core.Framework;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using Mainpro.AppFramework;
using System.Data;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using LMS.Data;

namespace Mainpro.UITest
{
    [BrowserMode(BrowserMode.New)]
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    //[LocalSeleniumTestFixture(BrowserNames.Firefox)]
    [LocalSeleniumTestFixture(BrowserNames.InternetExplorer)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]
    //[RemoteSeleniumTestFixture(BrowserNames.Firefox)]
    // Not running these test in IE until defect <> is fixed, where you can not refresh in IE
    [RemoteSeleniumTestFixture(BrowserNames.InternetExplorer)]
    [TestFixture]
    public class TBD_Mainpro_CAC_CreditAllocation_Test : TestBase
    {
        #region Constructors
        public TBD_Mainpro_CAC_CreditAllocation_Test(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }

        // Remote Selenium Grid Test
        public TBD_Mainpro_CAC_CreditAllocation_Test(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version , platform, hubUri, extrasUri)
        { }
        #endregion

        /// <summary>
        /// Example of how to override the teardown at the test class level
        /// </summary>
        //public override void TearDown() 
        //{
        //    Browser.Manage().Window.Size = new System.Drawing.Size(1040, 784);
        //    CleanupBrowser();
        //}

        #region Tests




      //  [Test]
        [Description("This test verifies that a user is able to allocate credits to a single CAC")]
        [Property("Status", "Complete")]
        [Author("Daniel Nestor")]
        [Category("Activity")]
        public void CACSingleAllocationTest()
        {
            UserModel NewUser1 = UserUtils.CreateAndRegisterUser("CACAllocationUser");

            /// 2. Finally register the user to a CAC
            UserUtils.AddUserToCAC(NewUser1.Username,Const_Mainpro.DesignationCode.PC, currentDatetime);

            /// 3. Now Login to the user
            LoginPage LP = Navigation.GoToLoginPage(browser);
            //create the dashboard page, but do not initalize it yet
            DashboardPage DP = LP.Login(NewUser1.Username, password, true);

            /// 4. Click on the Enter a CPD Activity button and Select Group Learning Certified, Other Certified Group Learning Activities
            EnterACPDActivityPage EP = DP.ClickAndWaitBasePage(DP.EnterCPDActBtn);
            EnterACPDActivityDetailsPage EDP = EP.ChooseActAndCntToDetailsPage(Const_Mainpro.ActivityCategory.GroupLearning, 
                Const_Mainpro.ActivityCertType.Certified, 
                Const_Mainpro.ActivityType.GRPLRNING_CERT_OtherCFPCCertifiedMainproGroupLearningActivities_LO, 
                Const_Mainpro.ActivityFormat.Live);

            /// 5. Fill out the Self Learning Form
            EDP.FillActivityForm(credits: 10);
            Thread.Sleep(1000);
            DP.DashboardTab.Click();
            ApplicationUtils.WaitForCreditsToBeAppliedOrActivityToShowOnLTS(DP, Bys.DashboardPage.CredSummaryCycleTblTotalAppliedLbl, "10");


            /// 6. Now go to the CAC
            CACCreditSummaryPage CCSP = DP.ClickAndWaitBasePage(DP.CACTab);
            browser.WaitForElement(Bys.MainproPage.CACCPDActivitiesListTab,ElementCriteria.IsVisible);
            CACCPDActivitiesList CCLP = new CACCPDActivitiesList(browser); //CCSP.ClickAndWait(CCSP.CACCPDActivitiesListTab);
            ElemSet.ClickAfterScroll(browser,CCLP.CACCPDActivitiesListTab);
            Thread.Sleep(1000);

            /// 7. Now Allocate the Credits
            browser.WaitForElement(Bys.CACCPDActivitiesListPage.CreditsToApplyTopLnk, ElementCriteria.IsVisible);
            ElemSet.ClickAfterScroll(browser,CCLP.CreditsToApplyTopLnk);

            browser.WaitForElement(Bys.CACCPDActivitiesListPage.FirstCACAllocationTxt,ElementCriteria.IsVisible);
            ElemSet.SendKeysAfterScroll(browser,  CCLP.FirstCACAllocationTxt, "10");
            ElemSet.ClickAfterScroll(browser, CCLP.PopupSubmitBtn);

            //return back to the main page and then verify the credits are applied
            ElemSet.ClickAfterScroll(browser, CCLP.CACCreditSummaryTab);
            Assert.AreEqual("10", CCSP.AppliedCreditsLbl.Text);

            Thread.Sleep(1000);

        }


       // [Test]
        [Description("This test allows the user to allocate credits to their CAC and that once allocated they all appear on the credit summary")]
        [Property("Status", "Complete")]
        [Author("Daniel Nestor")]
        [Category("Activity")]
        public void CACCreditSummaryAllocationTest()
        {
            UserModel NewUser1 = UserUtils.CreateAndRegisterUser("CACCreditSummaryUser");

            /// 2. Finally register the user to a CAC
            UserUtils.AddUserToCAC(NewUser1.Username, Const_Mainpro.DesignationCode.PC, currentDatetime);


            LoginPage LP = Navigation.GoToLoginPage(browser);
            DashboardPage DP = LP.Login(NewUser1.Username, password, true);

            /// 3. Next go about submitting the activities
            EnterACPDActivityPage EP = DP.ClickAndWaitBasePage(DP.EnterCPDActBtn);

            /// 4. create an activity that is a Certified Assessment, Other Activity
            EnterACPDActivityDetailsPage EDP = EP.ChooseActAndCntToDetailsPage(Const_Mainpro.ActivityCategory.Assessment, 
                Const_Mainpro.ActivityCertType.Certified, 
                Const_Mainpro.ActivityType.ASMT_CERT_OtherCFPCCertifiedMainproAssessmentActivities_LO,
                Const_Mainpro.ActivityFormat.Live);
      //      EDP.FillOutAndSubmitAssessmentForm(creditsToApply: 1);  
            DP.DashboardTab.Click();


            DP.ClickAndWaitBasePage(DP.EnterCPDActBtn);
            EP.ChooseActAndCntToDetailsPage(Const_Mainpro.ActivityCategory.GroupLearning, 
                Const_Mainpro.ActivityCertType.Certified, 
                Const_Mainpro.ActivityType.GRPLRNING_CERT_OtherCFPCCertifiedMainproGroupLearningActivities_LO, 
                Const_Mainpro.ActivityFormat.Live);
            EDP.FillActivityForm(credits: 1);
            Thread.Sleep(1000);
            DP.DashboardTab.Click();


            DP.ClickAndWaitBasePage(DP.EnterCPDActBtn);

            EP.ChooseActAndCntToDetailsPage(Const_Mainpro.ActivityCategory.SelfLearning, 
                Const_Mainpro.ActivityCertType.Certified, 
                Const_Mainpro.ActivityType.SELFLRNING_CERT_OtherCFPCCertifiedMainproSelfLearningActivities_LO, 
                Const_Mainpro.ActivityFormat.Live);
            EDP.FillActivityForm(credits: 1, actStartDt: DateTime.Today.AddDays(-1), actCompletionDt: DateTime.Today.AddDays(-1));
            Thread.Sleep(1000);
            DP.DashboardTab.Click();

            /// 5. now proceed to the CAC
            CACCreditSummaryPage CCSP = DP.ClickAndWaitBasePage(DP.CACTab);
            browser.WaitForElement(Bys.MainproPage.CACCPDActivitiesListTab, ElementCriteria.IsVisible);
            CACCPDActivitiesList CCLP = new CACCPDActivitiesList(browser); //CCSP.ClickAndWait(CCSP.CACCPDActivitiesListTab);
            ElemSet.ClickAfterScroll(browser, CCLP.CACCPDActivitiesListTab);
            Thread.Sleep(1000);

            /// 6. allocate the credits for the first cac
            browser.WaitForElement(Bys.CACCPDActivitiesListPage.CreditsToApplyTopLnk, ElementCriteria.IsVisible);
            ElemSet.ClickAfterScroll(browser, CCLP.CreditsToApplyTopLnk);

            browser.WaitForElement(Bys.CACCPDActivitiesListPage.FirstCACAllocationTxt, ElementCriteria.IsVisible);
            CCLP.ClearTextBox(CCLP.FirstCACAllocationTxt);
            ElemSet.SendKeysAfterScroll(browser, CCLP.FirstCACAllocationTxt, "1");
            ElemSet.ClickAfterScroll(browser, CCLP.PopupSubmitBtn);

            /// 7. allocate the credits for the second cac
            Thread.Sleep(5000);
            browser.WaitForElement(Bys.CACCPDActivitiesListPage.CreditsToApplySecondLnk, ElementCriteria.IsVisible);
            ElemSet.ClickAfterScroll(browser, CCLP.CreditsToApplySecondLnk);

            browser.WaitForElement(Bys.CACCPDActivitiesListPage.FirstCACAllocationTxt, ElementCriteria.IsVisible);
            CCLP.ClearTextBox(CCLP.FirstCACAllocationTxt);
            ElemSet.SendKeysAfterScroll(browser, CCLP.FirstCACAllocationTxt, "1");
            ElemSet.ClickAfterScroll(browser, CCLP.PopupSubmitBtn);

            /// 6. allocate the credits for the Third cac
            browser.WaitForElement(Bys.CACCPDActivitiesListPage.CreditsToApplyThirdLnk, ElementCriteria.IsVisible);
            ElemSet.ClickAfterScroll(browser, CCLP.CreditsToApplyThirdLnk);

            browser.WaitForElement(Bys.CACCPDActivitiesListPage.FirstCACAllocationTxt, ElementCriteria.IsVisible);
            CCLP.ClearTextBox(CCLP.FirstCACAllocationTxt);
            ElemSet.SendKeysAfterScroll(browser, CCLP.FirstCACAllocationTxt, "1");
            ElemSet.ClickAfterScroll(browser, CCLP.PopupSubmitBtn);

            /// 7. Next go to the Credit Summary Page
            ElemSet.ClickAfterScroll(browser, CCLP.CACCreditSummaryTab);

            /// 8. Verify that all of the credits are applied to the page
            int y = 0;
        }





        #endregion Tests
    }
}
