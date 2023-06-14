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
using System.Collections.ObjectModel;
using LS.AppFramework.Constants_LTS;
using System.Globalization;

namespace Mainpro.UITest
{
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]

    [TestFixture]
    public class Mainpro_ActivityWarningsValidation_Tests : TestBase
    {
        #region Constructors
        public Mainpro_ActivityWarningsValidation_Tests(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public Mainpro_ActivityWarningsValidation_Tests(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }
        #endregion

        #region Tests

        //[Test]
        [Description("To verify The selected issue has already been used. Please select another." +
          "warning message is displayed when try to submit the Volume & Questions from holding area where already same Volume & Questions is submitted")]
        [Property("Status", "Complete")]
        [Author("Neea Anand")]
        public void VolumeActivityFormValidation()
        {
            /// 1. Create a Default cycle new user, using the API
            UserModel user = UserUtils.CreateAndRegisterUser(currentTest: TestContext.CurrentContext.Test);

            /// 2. Log in into the application directly, create volume activity to save into holding area
            LoginPage LP = Navigation.GoToLoginPage(browser);
            LP.Login(user.Username, isNewUser: true);
            DashboardPage DP = new DashboardPage(browser);
            Browser.WaitForElement(Bys.DashboardPage.EnterCPDActBtn, ElementCriteria.IsVisible);
            EnterACPDActivityPage EAP = new EnterACPDActivityPage(browser);
            EnterACPDActivityDetailsPage EADP = new EnterACPDActivityDetailsPage(browser);
            DP.EnterCPDActBtn.Click();
            Browser.WaitForElement(Bys.EnterACPDActivityPage.ContinueBtn, ElementCriteria.IsVisible); Thread.Sleep(3000);
            EAP = Help.ChooseActivity(browser, TestContext.CurrentContext.Test,
            Const_Mainpro.ActivityCategory.SelfLearning,
            Const_Mainpro.ActivityCertType.Certified,
            Const_Mainpro.ActivityType.SELFLRNING_CERT_SelfLearningProgramImpactAssessment_FC5);

            string volumeTile = EAP.VolumeSelElem.Options[1].Text;
            EAP.ClickAndWait(EAP.ContinueBtn);
            EADP.ClickAndWait(EADP.SendToHoldingAreaBtn);
            EADP.ClickAndWait(EADP.YourActivityHasBeenSavedGoToHoldingAreaBtn);

            /// 3. Now create same volume activity as above to submit
            EAP = Help.ChooseActivity(browser, TestContext.CurrentContext.Test,
            Const_Mainpro.ActivityCategory.SelfLearning,
            Const_Mainpro.ActivityCertType.Certified,
            Const_Mainpro.ActivityType.SELFLRNING_CERT_SelfLearningProgramImpactAssessment_FC5);
            EAP.ClickAndWait(EAP.ContinueBtn);
            EADP.HowWillThisQuestionChangeYourPractice1TheChangeWillBeLargeRdo.Click();
            EADP.HowWillThisQuestion2ChangeYourPractice1TheChangeWillBeLargeRdo.Click();
            EADP.IPerceiveAnyDegreeofBiasYesRdo.Click();
            EADP.ActivityCompletionDateTxt.SendKeys(DateTime.Today.AddDays(-50).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture));
            ElemSet.ClickAfterScroll(Browser, EADP.SubmitBtn);
            Browser.WaitForElement(Bys.EnterACPDActivityDetailsPage.YourActivityHasBeenSubmittedFormGoToCPDActBtn, ElementCriteria.IsVisible);
            EADP.ClickAndWait(EADP.YourActivityHasBeenSubmittedFormGoToCPDActBtn);

            /// 4. Then complete volume activity already saved in Holding Area
            HoldingAreaPage HP = EADP.ClickAndWaitBasePage(EADP.HoldingAreaTab);
            Browser.WaitForElement(Bys.HoldingAreaPage.SummTabIncompActTblFirstRowActivityCellLnk, ElementCriteria.IsVisible);
            EADP = Help.Grid_ClickCellInTable(browser,
            Const_Mainpro.Table.HoldingAreaSummTabInc, volumeTile,
            Const_Mainpro.TableButtonLinkOrCheckBox.CompleteActivity);

            EADP.HowWillThisQuestionChangeYourPractice1TheChangeWillBeLargeRdo.Click();
            EADP.HowWillThisQuestion2ChangeYourPractice1TheChangeWillBeLargeRdo.Click();
            EADP.IPerceiveAnyDegreeofBiasYesRdo.Click();
            EADP.ActivityCompletionDateTxt.SendKeys(DateTime.Today.AddDays(-50).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture));
            ElemSet.ClickAfterScroll(Browser, EADP.SubmitBtn);

            /// 5. After Submitting, warning message must displayed 
            browser.WaitForElement(Bys.MainproPage.NotificationFormLbl);
            Assert.AreEqual("The selected issue has already been used. Please select another.", EAP.NotificationLbl.Text);
            EAP.ClickAndWaitBasePage(EAP.NotificationFormXBtn);


        }      

        //[Test]
        [Description("To verify The selected article has already been claimed for credit. Please select another article." +
           "warning message is displayed when try to submit the article from holding area where already same article is submitted")]
        [Property("Status", "Complete")]
        [Author("Neea Anand")]
        public void ArticleFormValidation()
        {
            /// 1. Create a Default cycle new user, using the API
            UserModel user = UserUtils.CreateAndRegisterUser(currentTest: TestContext.CurrentContext.Test);

            /// 2. Log in into the application directly, create Article Activity to save into holding area
            LoginPage LP = Navigation.GoToLoginPage(browser);
            LP.Login(user.Username, isNewUser: true);
            DashboardPage DP = new DashboardPage(browser);
            Browser.WaitForElement(Bys.DashboardPage.EnterCPDActBtn, ElementCriteria.IsVisible);
            EnterACPDActivityPage EAP = new EnterACPDActivityPage(browser);
            EnterACPDActivityDetailsPage EADP = new EnterACPDActivityDetailsPage(browser);
            EAP = Help.ChooseActivity(browser, TestContext.CurrentContext.Test,
            Const_Mainpro.ActivityCategory.SelfLearning,
            Const_Mainpro.ActivityCertType.Certified,
            Const_Mainpro.ActivityType.SELFLRNING_CERT_CFPMainproArticles_V);
            string articleTile = EAP.ArticleSelElem.Options[1].Text;
            EAP.SelectAndWait(EAP.ArticleSelElem, articleTile);
            EAP.ClickAndWait(EAP.ContinueBtn);
            EADP.ClickAndWait(EADP.SendToHoldingAreaBtn);
            EADP.ClickAndWait(EADP.YourActivityHasBeenSavedGoToHoldingAreaBtn);

            /// 3. Now create same Article Activity as above to submit
            EAP = Help.ChooseActivity(browser, TestContext.CurrentContext.Test,
            Const_Mainpro.ActivityCategory.SelfLearning,
            Const_Mainpro.ActivityCertType.Certified,
            Const_Mainpro.ActivityType.SELFLRNING_CERT_CFPMainproArticles_V);
            EAP.SelectAndWait(EAP.ArticleSelElem, articleTile);
            EAP.ClickAndWait(EAP.ContinueBtn);
            EADP.ActivityStartDateTxt.SendKeys(DateTime.Today.AddDays(-50).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture));
            EADP.ActivityCompletionDateTxt.SendKeys(DateTime.Today.AddDays(-20).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture));
            ElemSet.ClickAfterScroll(Browser, EADP.SubmitBtn);
            Browser.WaitForElement(Bys.EnterACPDActivityDetailsPage.YourActivityHasBeenSubmittedFormGoToCPDActBtn, ElementCriteria.IsVisible);
            EADP.ClickAndWait(EADP.YourActivityHasBeenSubmittedFormGoToCPDActBtn);

            /// 4. Then complete volume activity already saved in Holding Area
            HoldingAreaPage HP = EADP.ClickAndWaitBasePage(EADP.HoldingAreaTab);
            Browser.WaitForElement(Bys.HoldingAreaPage.SummTabIncompActTblFirstRowActivityCellLnk, ElementCriteria.IsVisible);
            HP.SummTabIncompActTblFirstRowActivityCellLnk.Click();
            Browser.WaitForElement(Bys.EnterACPDActivityDetailsPage.ActivityStartDateTxt, ElementCriteria.IsVisible);
            Thread.Sleep(2000);
            EADP.ActivityStartDateTxt.SendKeys(DateTime.Today.AddDays(-50).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture));
            EADP.ActivityCompletionDateTxt.SendKeys(DateTime.Today.AddDays(-20).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture));
            ElemSet.ClickAfterScroll(Browser, EADP.SubmitBtn);

            /// 5. After Submitting, warning message must displayed 
            browser.WaitForElement(Bys.MainproPage.NotificationFormLbl);
            Assert.AreEqual("The selected article has already been claimed for credit. Please select another article.", EAP.NotificationLbl.Text);
            EAP.ClickAndWaitBasePage(EAP.NotificationFormXBtn);



        }
        #endregion Tests

    }
}
