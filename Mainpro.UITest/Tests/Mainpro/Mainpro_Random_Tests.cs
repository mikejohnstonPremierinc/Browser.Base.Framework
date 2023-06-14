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

namespace Mainpro.UITest
{
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]
    [TestFixture]
    public class Mainpro_Random_Tests : TestBase
    {
        #region Constructors
        public Mainpro_Random_Tests(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }

        // Remote Selenium Grid Test
        public Mainpro_Random_Tests(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }
        #endregion

        #region Tests


        [Test]
        [Description("Given a user fills out an activity form, When a user clicks Cancel, Then the application " +
            "should not save the activity")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void CancelActivity()
        {
            /// 1. Go to the activity details page, fill in required fields, click Cancel then verify the Cancellation 
            /// Confirmation form appears
            EnterACPDActivityDetailsPage EADP = Help.ChooseActivityContinueToDetailsPage(browser,
                TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.Assessment,
                Const_Mainpro.ActivityCertType.Certified,
                Const_Mainpro.ActivityType.ASMT_CERT_QuebecCategory1Credit_LO,
                Const_Mainpro.ActivityFormat.Live);
            Activity Act = EADP.FillActivityForm();

            /// 2. Click No on the form, repeat step #1 and click Yes, then verify the activity is not saved on the 
            /// Acitivity List page
            EADP.ClickAndWait(EADP.CancelBtn);
            EADP.ClickAndWait(EADP.CancelFormNoBtn);
            EADP.ClickAndWait(EADP.CancelBtn);
            DashboardPage DP = EADP.ClickAndWait(EADP.CancelFormYesBtn);
            CPDActivitiesListPage ALP = DP.ClickAndWaitBasePage(DP.CPDActivitiesListTab);
            Help.VerifyGridDoesNotContainRecord(browser, Const_Mainpro.Table.CPDActitivitesListTabAct, Act.Title);
        }


        [Test]
        [Description("Given I create 2 activities, When I sort these activites in the Holding Area and the Activities" +
            "List page, Then the activities should sort accordinly")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void TableSorting()
        {
            /// 1. Create 2 activities, go to the Holding Area and the Activities List page, click on the Activity column 
            /// header and verify the table is sorting accordingly
            Help.AddActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.Assessment,
                Const_Mainpro.ActivityCertType.Certified,
                Const_Mainpro.ActivityType.ASMT_CERT_QuebecCategory1Credit_LO,
                Const_Mainpro.ActivityFormat.Live, actName: "AAAA", submitActivity: false);
            Help.AddActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.Assessment,
                Const_Mainpro.ActivityCertType.Certified,
                Const_Mainpro.ActivityType.ASMT_CERT_QuebecCategory1Credit_LO,
                Const_Mainpro.ActivityFormat.Live, actName: "BBBB", submitActivity: false);
          
            HoldingAreaPage HP = new HoldingAreaPage(browser);
            HP.ClickAndWait(HP.SummTabIncompActTblActivityColHdr);
            string sortAscendingFirstRowCellText = Help.Grid_GetRowCellTextByIndex(browser, 
                Const_Mainpro.Table.HoldingAreaSummTabInc, 0, 0, "//span");
            Assert.AreEqual("AAAA", sortAscendingFirstRowCellText);
            HP.ClickAndWait(HP.SummTabIncompActTblActivityColHdr);
            string sortDescendingFirstRowCellText = Help.Grid_GetRowCellTextByIndex(browser,
                Const_Mainpro.Table.HoldingAreaSummTabInc, 0, 0, "//span");
            Assert.AreEqual("BBBB", sortDescendingFirstRowCellText);

            CPDActivitiesListPage ALP = HP.ClickAndWaitBasePage(HP.CPDActivitiesListTab);
            ALP.ClickAndWait(ALP.ActTblActivityColHdr);
            sortAscendingFirstRowCellText = Help.Grid_GetRowCellTextByIndex(browser,
                Const_Mainpro.Table.CPDActitivitesListTabAct, 0, 0, "//span");
            Assert.AreEqual("AAAA", sortAscendingFirstRowCellText);
            ALP.ClickAndWait(ALP.ActTblActivityColHdr);
            sortDescendingFirstRowCellText = Help.Grid_GetRowCellTextByIndex(browser,
                Const_Mainpro.Table.CPDActitivitesListTabAct, 0, 0, "//span");
            Assert.AreEqual("BBBB", sortDescendingFirstRowCellText);
        }

        [Test]
        [Description("Given a user chooses CFPC Certified Mainpro Activities in the Category Select Element, searches " +
            "for an activity, When the user clicks the Dont See Your Activity link, Then the Other CFPC Certified " +
            "Mainpro+ Assessment Activity form should be presented to the user")]
        [Property("Status", "Complete")]
        [Category("UAT"), Category("PROD")]
        [Author("Mike Johnston")]
        public void DontSeeYourActivity()
        {


            /// 1. Go to the Enter a CPD Activity page, chooses CFPC Certified Mainpro Activities in the Category 
            /// Select Element, perform a search then click the Dont See Your Activity button
            EnterACPDActivityPage EAP = Help.ChooseActivity(browser,
                TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.Assessment,
                Const_Mainpro.ActivityCertType.Certified,
                Const_Mainpro.ActivityType.ASMT_CERT_CFPCCertifiedMainproActivities_LO,
                Const_Mainpro.ActivityFormat.Live);
            EAP.ProgramActivityTitleTxt.SendKeys("dswfsf");
            EAP.ClickAndWait(EAP.SearchBtn);
            EnterACPDActivityDetailsPage EADP = EAP.ClickAndWait(EAP.ClickHereBtn);
            StringAssert.AreEqualIgnoringCase("Other CFPC Certified Mainpro+ Assessment Activities", 
                EADP.ActivityTypeSelElemBtn.GetAttribute("title"));
        }


        #endregion Tests
    }


}