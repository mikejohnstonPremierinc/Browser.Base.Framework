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
using System.Globalization;

namespace Mainpro.UITest
{
    [BrowserMode(BrowserMode.New)]
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]

    [TestFixture]
    public class Mainpro_UploadActivity_Tests : TestBase
    {
        #region Constructors
        public Mainpro_UploadActivity_Tests(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }

        public Mainpro_UploadActivity_Tests(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }
        #endregion

        #region Tests


        [Test]
        [Description("Given I upload an activity through LTS with required fields filled in, When I view " +
            "the Mainpro UI, then this activity should appear on the Activities List page with an Edit button, " +
            "credits should be applied and I should be able to view the activity details on the Details page, " +
            "and When I upload another activity and leave a required field blank, Then this activity should " +
            "appear on the Holding Area page with a Complete Activity button and I should be able to complete " +
            "this activity")]
        [Property("Status", "In Progress")]
        [Author("Mike Johnston")]
        public void UploadActivity()
        {
            UserModel user = UserUtils.CreateAndRegisterUser(currentTest: TestContext.CurrentContext.Test);
            string actTitle1 = "my Quebec activity title for test name UploadActivity";
            string fileName1 = "CFPC_MOC_ASM_CA_QBEC.xlsx";
            string actTitle2 = "AAFP METRIC Program";
            string fileName2 = "CFPC_MOC_ASM_CA_AAFP.xlsx";
            string todaysDate = currentDatetime.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);

            /// 1. Upload an activity through LTS. On the excel file, fill in all required fields, enter a 
            /// the start/end date that falls within the cycle date range. Enter 1 credit. The filename of the file should 
            /// match one of the Recognition Codes from the following query: select RecognitionCode, * from 
            /// CommandCenter.dbo.rkg_recognition where RKG_recognitionEntityId = 32 order by Name. For this 
            /// test step, we will be uploading for the CFPC_MOC_ASM_CA_QBEC Recognition Code
            // NOTE: In the excel document, you can find the required excel fields for the Recognition Code by uploading
            // an existing excel file that you have for another Recognition Code. LTS will then return a warning which 
            // will tell you which fields are required. 
            FileUtils.Excel_SetData_ByRowNumAndColNum(fileName1, "Sheet1", 2, 1, user.Username);
            FileUtils.Excel_SetData_ByRowNumAndColNum(fileName1, "Sheet1", 2, 8, todaysDate);
            FileUtils.Excel_SetData_ByRowNumAndColNum(fileName1, "Sheet1", 2, 9, todaysDate);
            LSHelp.Login(browser, TestContext.CurrentContext.Test.Name, 
                AppSettings.Config["ltspassword"]);
            LSHelp.UploadActivity(browser, "College of Family Physician", fileName1);

            /// 2. Reeavluate the user through LTS, then verify the 1 credit got applied, the activity shows on the 
            /// Activity List page with an Edit button and that you can view the activity on the Details page
            LSHelp.ReevaluateUser(Browser, "College of Family Physician", user.Username, "Certification of Proficiency");
            LSHelp.LaunchSiteFromParticipantPage(browser, "College of Family Physician", user.Username);
            DashboardPage DP = new DashboardPage(browser);
            DP.WaitForInitialize();
            Help.SwitchToRewriteAfterLaunchingFromLTST(browser);
            CPDActivitiesListPage ALP = DP.ClickAndWaitBasePage(DP.CPDActivitiesListTab);
            // Fixed defect https://code.premierinc.com/issues/browse/MAINPROREW-904. 
            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                rowName: "Certified", colName: "Applied", cellTextExpected: "1");
            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CPDActitivitesListTabAct,
                rowName: actTitle1, colName: "Credits Applied", cellTextExpected: "1");
            Help.VerifyGridContainsRecord(browser, Const_Mainpro.Table.CPDActitivitesListTabAct, actTitle1);
            Help.Grid_ClickCellInTable(browser, Const_Mainpro.Table.CPDActitivitesListTabAct, actTitle1,
                Const_Mainpro.TableButtonLinkOrCheckBox.Edit);
            EnterACPDActivityDetailsPage EADP = new EnterACPDActivityDetailsPage(browser);
            EADP.WaitForInitialize();
            Assert.AreEqual(actTitle1, EADP.ProgramActivityTitleOrIDOrDescriptionOrSessionEtcTxt.GetAttribute("value"));

            /// 3. Upload an activity leaving a required field blank. Verify the activity appears on the holding area
            FileUtils.Excel_SetData_ByRowNumAndColNum(fileName2, "Sheet1", 2, 1, user.Username);
            FileUtils.Excel_SetData_ByRowNumAndColNum(fileName2, "Sheet1", 2, 6, todaysDate);
            FileUtils.Excel_SetData_ByRowNumAndColNum(fileName2, "Sheet1", 2, 7, todaysDate);
            Browser.SwitchTo().Window(Browser.WindowHandles[0]);
            LSHelp.UploadActivity(browser, "College of Family Physician", fileName2);
            LSHelp.LaunchSiteFromParticipantPage(browser, "College of Family Physician", user.Username);
            DP.WaitForInitialize();
            Help.SwitchToRewriteAfterLaunchingFromLTST(browser);
            HoldingAreaPage HP = DP.ClickAndWaitBasePage(DP.HoldingAreaTab);
            Help.VerifyGridContainsRecord(browser, Const_Mainpro.Table.HoldingAreaSummTabInc, actTitle2);

            /// 4. Click Complete Activity, fill in the required fields, click Submit, and verify the activity shows 
            /// on the Activity List page and credits are applied. 31 credits should be showing in the Credit Widget 
            /// table, 1 from the first activity and 30 from this activity
            Help.Grid_ClickCellInTable(browser, Const_Mainpro.Table.HoldingAreaSummTabInc, actTitle2,
                Const_Mainpro.TableButtonLinkOrCheckBox.CompleteActivity);
            EADP.WaitForInitialize();
            var blah = EADP.ProgramTitleSelElem.SelectedOption.Text;
            Assert.AreEqual(actTitle2, EADP.ProgramTitleSelElem.SelectedOption.Text);
            EADP.CityTxt.SendKeys("test city");
            EADP.ClickAndWait(EADP.SubmitBtn);
            EADP.ClickAndWait(EADP.YourActivityHasBeenSubmittedFormGoToCPDActBtn);
            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                rowName: "Certified", colName: "Applied", cellTextExpected: "31");
            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CPDActitivitesListTabAct,
                rowName: actTitle2, colName: "Credits Applied", cellTextExpected: "30");
            Help.VerifyGridContainsRecord(browser, Const_Mainpro.Table.CPDActitivitesListTabAct, actTitle2);
        }

        #endregion Tests
    }

}