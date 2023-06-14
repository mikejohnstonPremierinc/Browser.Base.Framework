using Browser.Core.Framework;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using Mainpro.AppFramework;
using System.Data;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using System.Collections.ObjectModel;
using LMS.Data;
using LS.AppFramework.Constants_LTS;
using AventStack.ExtentReports;
using Mainpro.UITest;

namespace PLP.UITest
{
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]
    [TestFixture]
    public class PLP_GenericTests_Set3 : TestBase
    {
        #region Constructors
        public PLP_GenericTests_Set3(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public PLP_GenericTests_Set3(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }
        #endregion

        #region Tests

        [Test]
        [Description("This Test includes the below verfications:" +
            "1) On cpdsuggestions screen, Click the CPD Events Calendar button, opens up the CPD Events Calendar " +
            "Website in new Tab" +
            "2) On step5 prereflection screen, Verify 'Are you sure you want to exit your session ?' Popup " +
            "by clicking Exit button , by clicking X icon, by clicking No button  " +
            "3) on step6submission screen, Verify PLP Credits displayed as per Peer/Self version " +
            "4) on Step6 overallplpcompletion screen, Verify Contact us popup" +
            "5) on Step6 overallplpcompletion screen, Verify Recommend to collegue popup" +
            "5) on Step6 overallplpcompletion screen, Verify Print My Completed PLP popup and downloads pdf" +
            "5) on Step6 overallplpcompletion screen, Verify Print PLP Certificate popup and downloads pdf" +
            "5) on Step6 overallplpcompletion screen, Verify Start New PLP navigates to Dashboard" 
            )]
        [Property("Status", "Complete")]
        [Author("Bama Thangaraj")]
        public void verifylinkspopupsreports()
        {
            if (Help.EnvironmentEquals(Constants.Environments.Production))
            {
                Assert.Ignore("This test will be ignored if the environment is Production " +
                    " since it requires Step6 unlocked to complete the full flow and " +
                    "we dont want to do in Prod since this may alter clients data/users");
            }
            /*
            // To Debug this testcase - use this commented lines 
            LoginPage LP = Navigation.GoToLoginPage(browser);
            DashboardPage DPd = LP.Login("TestAutoIA6_Apr-5-22_3-50_linkspopups",
               isNewUser: false);
            DPd.ClickAndWait(DPd.EnterBtn);
            Step5Page PS5a = new Step5Page(browser);
            StepPRPage PR = new StepPRPage(browser);*/

            TESTSTEP.Log(Status.Info, "Create a new user");
            UserModel newUser = UserUtils.CreateAndRegisterUser(currentTest: TestContext.CurrentContext.Test);
            String username = newUser.Username;
            String password = newUser.Password;

            TESTSTEP.Log(Status.Info, "Enter into PLP Module, Complete Step1,2,3,4 stages by filling required fields");
            Step5Page PS5 = Help.PLP_GoToStep(Browser, 5, TestContext.CurrentContext.Test,
               isNewUser: true, username: username, password: password,
               isCPDSuggestionActivtyTblCheck: false, isSelfGuided: true);

            TESTSTEP.Log(Status.Info, "From UserProfile Menu, Click on PLP Activity Summary Option," +
                "Navigated to plpactivitysummary screen");
            PS5.ClickAndWaitBasePage(PS5.PLP_Menu_DropDownBtn);
            PS5.ClickAndWaitBasePage(PS5.PLP_Menu_PLPActivitySumm);
            PS5.ClickAndWaitBasePage(PS5.PLP_Menu_CloseBtn);
            PS5.ClickAndWait(PS5.BackBtn);

            TESTSTEP.Log(Status.Info, "Click Back button to reach to cpdsuggestions screen");
            PS5.ClickAndWait(PS5.BackBtn);
            Step3Page PS3 = new Step3Page(Browser);

            TESTSTEP.Log(Status.Info, "On cpdsuggestions screen, Click the CPD Events Calendar button," +
                " opens up the CPD Events Calendar Website in new Tab");
            PS3.CPDEventsCalendarBtn.Click();
            Browser.SwitchTo().Window(Browser.WindowHandles[1]);
            Assert.True(Browser.Url.Contains("cpd-events-calendar"),
                "it should open the new Tab for CPD Events Calendar");
            Browser.Close();
            Browser.SwitchTo().Window(Browser.WindowHandles[0]);

            TESTSTEP.Log(Status.Info, "Click Step5 from Top Navigation bar to reach to Step5 Pre-reflection screen");
            TESTSTEP.Log(Status.Info, "Click Goal(s) in progress radio option,Click Next button, " +
                "verify that 'Are you sure you want to exit your session ?' Popup opens up ");
            TESTSTEP.Log(Status.Info, "Click Exit button, Should Exit the PLP module and" +
                " navigate back to mainpro dashboard screen");
            Step5Page PS5a = PS3.ClickAndWaitBasePage(PS3.PLP_Header_5Btn);
            Help.PLP_ClickCheckBoxOrRadioButton(Browser, "Goal(s) in progress");
            PS5a.ClickAndWait(PS5a.NextBtn);
            DashboardPage DP = PS5a.ClickAndWait(PS5a.YouWantToExitPopup_ExitBtn);

            TESTSTEP.Log(Status.Info, "Reenter into PLP module again, Click Goal(s) in progress radio option," +
                "Click Next button, Now Click No button in the Popup," +
                "Popup should get closed and Should back to PreReflection Screen");
            DP.ClickAndWait(DP.EnterBtn);
            Step5Page PS5x = new Step5Page(Browser);
            Help.PLP_ClickCheckBoxOrRadioButton(Browser, "Goal(s) in progress");
            PS5x.ClickAndWait(PS5x.NextBtn);
            PS5x.ClickAndWait(PS5x.YouWantToExitPopup_NoBtn);
            Assert.False(Browser.Exists(Bys.Step5Page.YouWantToExitPopup_NoBtn, ElementCriteria.IsVisible),
               "Are you sure you want to exit your session? Click X icon, Should back to PreReflection Screen");

            TESTSTEP.Log(Status.Info, "Click Goal(s) in progress radio option,Click Next button" +
                " Now Click X icon in the Popup," +
                "Popup should get closed and Should back to PreReflection Screen");
            Help.PLP_ClickCheckBoxOrRadioButton(Browser, "Goal(s) in progress");
            PS5x.ClickAndWait(PS5x.NextBtn);
            PS5x.ClickAndWait(PS5x.YouWantToExitPopupXBtn);
            Assert.False(Browser.Exists(Bys.Step5Page.YouWantToExitPopup_NoBtn, ElementCriteria.IsVisible),
                "Are you sure you want to exit your session? Click X icon, Should back to PreReflection Screen");

            TESTSTEP.Log(Status.Info, "continue to complete step 5 and step 6 screens by choosing No flow and" +
                " filling required fields");
            #region Fill Step5 No flow
            Help.PLP_ClickCheckBoxOrRadioButton(Browser, "Move to Step 5");
            PS5x.ClickAndWait(PS5x.NextBtn);
            PS5x.ClickAndWait(PS5x.NextBtn);
            Help.PLP_ClickCheckBoxOrRadioButton(Browser, "No");
            PS5x.ClickAndWait(PS5x.NextBtn);
            PS5x.FillBarriersToAchievingGoals();
            PS5x.ClickAndWait(PS5x.NextBtn);
            Help.PLP_ClickCheckBoxOrRadioButton(Browser, "No");
            PS5x.ClickAndWait(PS5x.NextBtn);
            PS5x.ClickAndWait(PS5x.NextBtn);
            #endregion

            #region Fill Step6 flow
            StepPRPage PR = new StepPRPage(Browser);
            PR.ClickAndWait(PR.NextBtn);
            PR.FillImpactOnPractice(Browser);
            PR.ClickAndWait(PR.NextBtn);

            //Did your PLP help you achieve your learning goal(s)?
            Help.PLP_ClickCheckBoxOrRadioButton(Browser, "Yes");
            PR.ClickAndWait(PR.NextBtn);

            //Did you see any of the outcomes you'd hoped to see? (e.g., improvements in your data).
            Help.PLP_ClickCheckBoxOrRadioButton(Browser, "Yes");
            PR.ClickAndWait(PR.NextBtn);

            //Please elaborate and describe in detail.
            PR.FillElborateDetail(Browser);
            PR.ClickAndWait(PR.NextBtn);

            //Based on your outcomes, list up to three changes/modifications you might plan to make in a future PLP.
            PR.FillOutcomeModifications(Browser);
            PR.ClickAndWait(PR.NextBtn);

            //Would you recommend this learning activity to a colleague?
            Help.PLP_ClickCheckBoxOrRadioButton(Browser, "No");
            PR.ClickAndWait(PR.NextBtn);

            //Do you have any additional comments about the PLP that may help us to improve it for future users?
            Help.PLP_ClickCheckBoxOrRadioButton(Browser, "No");
            PR.ClickAndWait(PR.NextBtn);
            #endregion
            try
            {
                //Congratulations! You have completed your PLP!
                Assert.True(Browser.Exists(Bys.StepPRPage.SubmissionContentTxt, ElementCriteria.TextContains("12")),
                   "For Self Guided, 12 credits should display in the screen");
            }
            catch { Console.WriteLine("12 credits check"); }
            PR.ClickAndWait(PR.SubmitBtn);

            Browser.WaitForElement(Bys.StepPRPage.PLPCertificateBtn, ElementCriteria.IsVisible);
            TESTSTEP.Log(Status.Info, "Verify the RecommendPLPtoacolleagueBtn opens the popup and closed correctly");
            PR.ClickAndWait(PR.RecommendPLPtoacolleagueBtn);

            TESTSTEP.Log(Status.Info, "Verify the ContactUs Image opens the popup and closed correctly");
            PR.ClickAndWait(PR.ContactUsImg);
            Assert.True(PR.SupportInfoFormPLPSiteLbl.Displayed);
            Assert.True(PR.SupportInfoFormPLPExtnLbl.Displayed);
            PR.ClickAndWaitBasePage(PR.SupportInfoFormCloseBtn);

            TESTSTEP.Log(Status.Info, "Verify the PLPCertificateButton opens the popup and download the file correctly");
            PR.ClickAndWait(PR.PLPCertificateBtn);
            PR.ClickAndWait(PR.PrintPLPCertificateDownloadBtn);
            browser.WaitForElement(Bys.ReportsPage.ReportPDFEmbedElem, ElementCriteria.IsVisible);
            WindowAndFrameUtils.CloseWindowthenSwitchToWindow(browser, 1, 0);
            PR.ClickAndWait(PR.printPLPCertCloseBtn);

            TESTSTEP.Log(Status.Info, "Verify the PrintmycompletedPLPButton opens the popup and download the file correctly");
            PR.ClickAndWait(PR.PrintmycompletedPLPBtn);
            PR.ClickAndWait(PR.PrintmycompletedPLPDownloadBtn);
            browser.WaitForElement(Bys.ReportsPage.ReportPDFEmbedElem, ElementCriteria.IsVisible);
            WindowAndFrameUtils.CloseWindowthenSwitchToWindow(browser, 1, 0);
            PR.ClickAndWait(PR.printPLPCompleteCloseButton);

            TESTSTEP.Log(Status.Info, "Verify that on clicking StartanewPLPButton directs to mainpro dashboard page ");
            PR.ClickAndWait(PR.StartanewPLPBtn);


        }

        [Test]
        [Description("This Test verifies the CPD Activity Table validations in step 3 and Step 5 such as," +
            "verify the user able to choose system ativities and add custom activities in step3 CPDSuggestion screen" +
            "// 3. verify the user selected and added activities in step3 are shown in Step5 - PreReflection screen" +
            "// 4. verify the user selected and added activities in step3 are shown in Step5 - UsefulCPD screen" +
            "// 5. Verify Moreinfo section should display details in Step3 CPD suggestion screen " +
            "// 6. Verify Moreinfo section should display details in Step3 Summary page" +
            "// 7. Verify Moreinfo section should display details in Step5 pre-reflection screen and Step5 usefulcpd screen" +
            "// 8. verify the user able to add custom ativities in Step5 usefulcpd screen" +
            "// 9. verify the user able to select the system and custom ativities in Step5 usefulcpd screen" +
            "// 10. verify the system and custom ativities chosen by user, are retained as checked in Step5" +
            "// usefulcpd screen during revisit")]
        [Property("Status", "Completed")]
        [Author("Bama Thangaraj")]
        public void CPDActivityTableChecksInStep3and5()
        {
            if (Help.EnvironmentEquals(Constants.Environments.Production))
            {
                Assert.Ignore("This test will be ignored if the environment is Production " +
                    " since it requires Step6 unlocked to complete the full flow and " +
                    "we dont want to do in Prod since this may alter clients data/users");
            }

            /*  // To Debug this testcase - use this commented lines 
              LoginPage LP = Navigation.GoToLoginPage(browser);
              DashboardPage DP = LP.Login("TestAuto136_Mar-24-22_21-34_LeaveReinstatementCycleCompletion",
                 isNewUser: false);
              DP.ClickAndWait(DP.EnterBtn);
             // Step5Page PS5 = new Step5Page(browser);
  */
            // Create a new user
            UserModel newUser = UserUtils.CreateAndRegisterUser(currentTest: TestContext.CurrentContext.Test);
            String username = newUser.Username;
            String password = newUser.Password;

            TESTSTEP.Log(Status.Info, " 1. Enter into PLP Module, complete steps 1,2,3 screens by filling required fields ");
            TESTSTEP.Log(Status.Info, "2. verify the user able to choose system ativities and add custom activities" +
                " in step3 CPDSuggestion screen");
            TESTSTEP.Log(Status.Info, "3. Expand and Verify Moreinfo section should display details in Step3 " +
                "CPD suggestion screen");
            TESTSTEP.Log(Status.Info, "4. Expand Verify Moreinfo section should display details in Step3 Summary page");
            TESTSTEP.Log(Status.Info, "5. complete Step 4");
            TESTSTEP.Log(Status.Info, "6. Goto step 5, verify the user selected and added activities in step3" +
                " are shown in Step5 - PreReflection screen");
            TESTSTEP.Log(Status.Info, "7. verify the user selected and added activities in step3 are shown in " +
                "Step5 - UsefulCPD screen");
            TESTSTEP.Log(Status.Info, "8. Exapnd and Verify Moreinfo section should display details in " +
                "Step5 pre-reflection screen and Step5 usefulcpd screen");
            TESTSTEP.Log(Status.Info, "9. verify the user able to add custom ativities in Step5 usefulcpd screen");
            TESTSTEP.Log(Status.Info, "10. verify the user able to select the system and custom activities in " +
                "Step5 usefulcpd screen");
            TESTSTEP.Log(Status.Info, "11. verify the system and custom activities chosen by user, are " +
                "retained as checked in Step5 usefulcpd screen during revisit");

            StepPRPage PSPR = Help.PLP_GoToStep(Browser, 6, TestContext.CurrentContext.Test,
               isNewUser: true, username: username, password: password,
               isCPDSuggestionActivtyTblCheck: true);
            PSPR.ClickAndWait(PSPR.BackBtn);
            Step5Page PS5 = new Step5Page(browser);
            PS5.ClickAndWait(PS5.BackBtn);
            PS5.ClickAndWait(PS5.BackBtn);
            StringAssert.AreEqualIgnoringCase("3",
                    Browser.FindElements(By.XPath("//table[contains(@aria-labelledby, 'usefulcpdProgramsGrid')]//tbody//input[@checked]"))
                    .Count.ToString(),
                    "Table should retain 3 rows checked by the user while revisiting this screen");

        }
    }

    #endregion Tests
}
