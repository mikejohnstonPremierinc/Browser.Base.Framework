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
    public class PLP_GenericTests_Set4 : TestBase
    {
        #region Constructors
        public PLP_GenericTests_Set4(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public PLP_GenericTests_Set4(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }
        #endregion

        #region Tests
        [Test]
        [Description("This Test Verifies All menu options under user profile Menu " +
            "1) on each screen, Open user profile menu, Click Toos and Resource option and checks whether the current Step section is opened  in Tools and resources page and all other sections are collapsed" +
            "2) From userprofile menu, click PLPCertificateButton option, Verify the PLPCertificateButton opens the popup and download the file correctly and close the popup" +
            "3) From userprofile menu, click PrintCompletedPLP Option, Verify the PrintmycompletedPLPButton opens the popup and download the file correctly and close the popup" +
            "4) From userprofile menu, Click on Activity Summary Option, Verify Activity Summary page is opened" +
            "5) From userprofile menu, click Contactus link and verify the contacus info popup opened" +
            "6) From userprofile menu, Click ExitToMainpro option, navigate to Dashboard page ")]
        [Property("Status", "Completed")]
        [Author("Bama Thangaraj")]
        public void UserProfCheckAllMenuOpt()
        {
            if (Help.EnvironmentEquals(Constants.Environments.Production))
            {
                Assert.Ignore("This test will be ignored if the environment is Production " +
                    " since it requires Step6 unlocked to complete the full flow and " +
                    "we dont want to do in Prod since this may alter clients data/users");
            }
            /* // To Debug this testcase - use this commented lines 
             LoginPage LP = Navigation.GoToLoginPage(browser);
             DashboardPage DP = LP.Login("TestAutoC6I_Apr-11-22_9-39_ToolsOptionClickFromUserProfile",
                isNewUser: false);
             DP.ClickAndWait(DP.EnterBtn);
             Step5Page PS5a = new Step5Page(browser);
             StepPRPage PR = new StepPRPage(browser);*/

            TESTSTEP.Log(Status.Info, "Create and Register a new user to mainpro+ ");
            UserModel newUser = UserUtils.CreateAndRegisterUser(currentTest: TestContext.CurrentContext.Test);
            String username = newUser.Username;
            String password = newUser.Password;

            TESTSTEP.Log(Status.Info, "Enter into PLP Module, Complete Step1,2,3,4,5,6 stages by filling required fields");
            TESTSTEP.Log(Status.Info, "on each screen, Open user profile menu , Click Toos and Resource option and checks " +
                "whether the current Step section is opened  in Tools and resources page  and all other sections are collapsed");
            StepPRPage PR = Help.PLP_GoToStep(Browser, 6, TestContext.CurrentContext.Test,
               isNewUser: true, username: username, password: password,
               ToolsOptionsClickFromUserProfileCheck: true, isSelfGuided: true);
            PR.FillStep6_YesFlow(Browser, isReturnFromOverallCompletionScreen: true);

            TESTSTEP.Log(Status.Info, "On OverallPLPCompletionScreen, Click Userprofile menu");
            PR.ClickAndWaitBasePage(PR.PLP_Menu_DropDownBtn);

            TESTSTEP.Log(Status.Info, "From userprofile menu, click PLPCertificateButton option, Verify the PLPCertificateButton opens the popup and download the file correctly and close the popup");
            PR.ClickAndWait(PR.PLP_Menu_PrintPLPCertificate);
            PR.ClickAndWait(PR.PrintPLPCertificateDownloadBtn);
            browser.WaitForElement(Bys.ReportsPage.ReportPDFEmbedElem, ElementCriteria.IsVisible);
            WindowAndFrameUtils.CloseWindowthenSwitchToWindow(browser, 1, 0);
            PR.ClickAndWait(PR.printPLPCertCloseBtn);

            TESTSTEP.Log(Status.Info, "From userprofile menu, click PrintCompletedPLP Option,Verify the PrintmycompletedPLPButton opens the popup and download the file correctly and close the popup");
            PR.ClickAndWaitBasePage(PR.PLP_Menu_DropDownBtn);
            PR.ClickAndWait(PR.PLP_Menu_PrintCompletedPLP);
            PR.ClickAndWait(PR.PrintmycompletedPLPDownloadBtn);
            browser.WaitForElement(Bys.ReportsPage.ReportPDFEmbedElem, ElementCriteria.IsVisible);
            WindowAndFrameUtils.CloseWindowthenSwitchToWindow(browser, 1, 0);
            PR.ClickAndWait(PR.printPLPCompleteCloseButton);

            TESTSTEP.Log(Status.Info, "From userprofile menu, Click on Activity Summary Option, Verify Activity Summary page is opened ");
            PR.ClickAndWaitBasePage(PR.PLP_Menu_DropDownBtn);
            PR.ClickAndWaitBasePage(PR.PLP_Menu_PLPActivitySumm);
            PR.ClickAndWaitBasePage(PR.PLP_Menu_CloseBtn);

            TESTSTEP.Log(Status.Info, "From userprofile menu, click Contactus link and verify the contacus info popup opened  ");
            PR.ClickAndWaitBasePage(PR.PLP_Menu_DropDownBtn);
            PR.ClickAndWaitBasePage(PR.PLP_Menu_ContactUs);
            Assert.True(PR.SupportInfoFormPLPSiteLbl.Displayed);
            Assert.True(PR.SupportInfoFormPLPExtnLbl.Displayed);
            PR.ClickAndWaitBasePage(PR.SupportInfoFormCloseBtn);

            TESTSTEP.Log(Status.Info, "From userprofile menu, Click ExitToMainpro option, navigate to Dashboard page");
            PR.ClickAndWaitBasePage(PR.PLP_Menu_DropDownBtn);
            DashboardPage DP = PR.ClickAndWaitBasePage(PR.PLP_Menu_ExitToMainpro);

        }
        [Test]
        [Description("Clicks i-icon on each screen and checks whether the current Step section is opened" +
           " in Tools and resources page " +
           "and all other sections are collapsed")]
        [Property("Status", "Completed")]
        [Author("Ganapathy")]
        public void i_IconClickinAllScreens()
        {
            if (Help.EnvironmentEquals(Constants.Environments.Production))
            {
                Assert.Ignore("This test will be ignored if the environment is Production " +
                    " since it requires Step6 unlocked to complete the full flow and " +
                    "we dont want to do in Prod since this may alter clients data/users");
            }

            StepPRPage PS6 = Help.PLP_GoToStep(Browser, 6, TestContext.CurrentContext.Test,
                isSelfGuided: false, iIconClickCheck: true, ToolsOptionsClickFromUserProfileCheck: true);

            //verify tools and resource section
            PLP_Event.PLP_ToolsAndResourcesSection(Browser, true);
            PS6.ClickAndWait(PS6.NextBtn);
            Thread.Sleep(500);
            PS6.FillImpactOnPractice(Browser);
            PS6.ClickAndWait(PS6.NextBtn);
            //Did your PLP help you achieve your learning goal(s)?
            Help.PLP_ClickCheckBoxOrRadioButton(Browser, "Yes");
            PS6.ClickAndWait(PS6.NextBtn);

            //Did you see any of the outcomes you'd hoped to see? (e.g., improvements in your data).
            Help.PLP_ClickCheckBoxOrRadioButton(Browser, "Yes");
            PS6.ClickAndWait(PS6.NextBtn);
            //verify tools and resource section
            PLP_Event.PLP_ToolsAndResourcesSection(Browser, true);
            //Please elaborate and describe in detail.
            PS6.FillElborateDetail(Browser);
            PS6.ClickAndWait(PS6.NextBtn);

            //Based on your outcomes, list up to three changes/modifications you might plan to make in a future PLP.
            PS6.FillOutcomeModifications(Browser);
            PS6.ClickAndWait(PS6.NextBtn);

            //Would you recommend this learning activity to a colleague?
            Help.PLP_ClickCheckBoxOrRadioButton(Browser, "Yes");
            PS6.ClickAndWait(PS6.RecommendMailIconBtn);
            PS6.ClickAndWait(PS6.NextBtn);

            //verify tools and resource section
            PLP_Event.PLP_ToolsAndResourcesSection(Browser, true);
            //Do you have any additional comments about the PLP that may help us to improve it for future users?
            Help.PLP_ClickCheckBoxOrRadioButton(Browser, "Yes");
            ElemSet.TextBox_EnterText(Browser, PS6.AdditionalFeedbackDetailTxt, true, "Yes Feedback");
            PS6.ClickAndWait(PS6.NextBtn);

            //Congratulations! You have completed your PLP!
            PS6.ClickAndWait(PS6.SubmitBtn);

            //overallplpcompletion
            PS6.ClickAndWait(PS6.ExitPLPBtn);


        }
    }
    #endregion
}
