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
    public class Mainpro_HoldingArea_Tests : TestBase
    {
        #region Constructors
        public Mainpro_HoldingArea_Tests(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }

        public Mainpro_HoldingArea_Tests(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }
        #endregion

        #region Tests


        [Test]
        [Description("Given a user clicks both the Send To Holding Area button and the Save Progress button on the " +
            "Activity Details page, When the user views the Activites List page and the Holding Area page, then the " +
            "activity should appear in the appropriate tables with the appropriate buttons, and When the user clicks " +
            "the Delete or the Complete Activity button in the table to perform either function, Then the functions " +
            "should function appropriately")]
        [Property("Status", "Complete")]
        [Category("UAT")]
        [Author("Mike Johnston")]
        public void SendToHoldingArea()
        {
            /// 1. Add an activity, click the Save Progress button. Verify the activity gets placed in the incomplete 
            /// table on the Summary tab of the Holding Area page
            EnterACPDActivityDetailsPage EADP = Help.ChooseActivityContinueToDetailsPage(browser,
                TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.Assessment,
                Const_Mainpro.ActivityCertType.Certified,
                Const_Mainpro.ActivityType.ASMT_CERT_QuebecCategory1Credit_LO,
                Const_Mainpro.ActivityFormat.Online);
            Activity Act1 = EADP.FillActivityForm(credits: 2);
            EADP.ClickAndWait(EADP.SaveProgressBtn);
            EADP.ClickAndWait(EADP.YourActivityHasBeenSavedBannerXBtn);
            HoldingAreaPage HP = EADP.ClickAndWaitBasePage(EADP.HoldingAreaTab);
            Help.VerifyGridContainsRecord(browser, Const_Mainpro.Table.HoldingAreaSummTabInc, Act1.Title);

            /// 2. Click the Delete trach can icon and then click Yes to delete it. Verify it no longer appears 
            /// in the table
            Help.Grid_ClickCellInTable(browser, Const_Mainpro.Table.HoldingAreaSummTabInc,
                Act1.Title, Const_Mainpro.TableButtonLinkOrCheckBox.Delete);
            HP.ClickAndWait(HP.DeleteFormYesBtn);
            Help.VerifyGridDoesNotContainRecord(browser, Const_Mainpro.Table.HoldingAreaSummTabInc, Act1.Title);

            /// 3. Add another activity, fill in a text field, a select element and upload a document, click the 
            /// Send To Holding Area button, then the Go To Holding Area button, then Verify the activity gets
            /// placed in the incomplete tables on both the Summary and Incomplete tabs on the Holding Area page. 
            Help.ChooseActivityContinueToDetailsPage(browser,
                TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.Assessment,
                Const_Mainpro.ActivityCertType.Certified,
                Const_Mainpro.ActivityType.ASMT_CERT_AAFPandABFMActivities_L_FC30);
            string actTitle = "AAFP METRIC Program";
            string actDate = DateTime.Today.AddDays(-1).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            EADP.ProgramTitleSelElem.SelectByText(actTitle);
            EADP.ActivityCompletionDateTxt.SendKeys(actDate);
            string filePath = "C:\\SeleniumAutoIt\\book1.xlsx";
            FileUtils.UploadFileUsingSendKeys(browser, EADP.UploadFilesBtn, filePath, EADP);
            // For some reason, the line above failed to wait for the upload to complete before proceeding. For now, I am 
            // just adding a temporary sleep. I will fix the broken code within UploadFileUsingSendKeys in the future
            Thread.Sleep(5000);
            EADP.ClickAndWait(EADP.SendToHoldingAreaBtn);
            EADP.ClickAndWait(EADP.YourActivityHasBeenSavedGoToHoldingAreaBtn);

            /// 4. Verify the activity appears on the Holding Area page on the Summary tab within the Incomplete 
            /// table, but does not appear in the Pending Approval table
            Help.VerifyGridContainsRecord(browser, Const_Mainpro.Table.HoldingAreaSummTabInc, actTitle);
            Help.VerifyGridDoesNotContainRecord(browser, Const_Mainpro.Table.HoldingAreaSummTabPendAppr, actTitle);

            /// 5. Verify it got placed in the Activity table on the Activity list page, also verify the credits were
            /// not applied
            CPDActivitiesListPage ALP = HP.ClickAndWaitBasePage(HP.CPDActivitiesListTab);
            Help.VerifyGridContainsRecord(browser, Const_Mainpro.Table.CPDActitivitesListTabAct, actTitle);
            Assert.AreEqual("-", ElemGet.Grid_GetCellTextByRowNameAndColName(Browser, ALP.ActTbl, ALP.ActTblHdr,
                Bys.CPDActivitiesListPage.ActTblBodyFirstRow, actTitle, "span", "Credits Applied", "//span"));

            /// 6. Click the Complete Activity button from the Activity List page table. Verify the that the fields 
            /// that we filled in above are loaded with the data we provided
            Help.Grid_ClickCellInTable(browser, Const_Mainpro.Table.CPDActitivitesListTabAct, actTitle,
                Const_Mainpro.TableButtonLinkOrCheckBox.CompleteActivity);
            Assert.AreEqual(actTitle, EADP.ProgramTitleSelElem.SelectedOption.Text);
            Assert.AreEqual(actDate, EADP.ActivityCompletionDateTxt.GetAttribute("value"));
            Assert.True(browser.Exists(By.XPath("//span[contains(text(), 'ook1.xlsx')]"), ElementCriteria.IsVisible), "The " +
                "uploaded file was not loaded");

            /// 7. Fill in required fields and submit the activity, then verify it gets removed from the holding 
            /// area and placed in the Activities List page with an Edit button
            EADP.FillActivityForm();
            EADP.ClickAndWait(EADP.SubmitBtn);
            EADP.ClickAndWait(EADP.YourActivityHasBeenSubmittedFormGoToCPDActBtn);
            Help.VerifyGridContainsRecord(browser, Const_Mainpro.Table.CPDActitivitesListTabAct, actTitle);
            var EditBtn = ElemGet.Grid_GetButtonOrLinkInsideRowByPartialClassName(browser, ALP.ActTbl,
                Bys.CPDActivitiesListPage.ActTblBodyFirstRow, actTitle, "span", "div", "button-icon");
            Assert.False(EditBtn.GetAttribute("class").Contains("disabled"));
            EADP.ClickAndWaitBasePage(EADP.HoldingAreaTab);
            Help.VerifyGridDoesNotContainRecord(browser, Const_Mainpro.Table.HoldingAreaSummTabInc, actTitle);
        }

        #endregion Tests
    }

}