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

namespace Mainpro.UITest
{
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome, "", "", Platforms.Windows, "", "")]
    //[RemoteSeleniumTestFixture(BrowserNames.Chrome, EmulationDevices.iPhoneX, "", Platforms.Windows, "", "")]
    [TestFixture]
    public class Mainpro_Dashboard_Tests : TestBase
    {
        #region Constructors
        public Mainpro_Dashboard_Tests(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public Mainpro_Dashboard_Tests(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }
        #endregion


        #region Tests

        [Test]
        [Description("Given I add 2 activities, send 1 to the Holding Area, while the other requires Credit Validation, " +
            "When I go to the Dashboard page these activities should show in the appropriate tables, and When I click " +
            "on each activity, Then the Activity Details page should appear with the activity information")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        [Category("UAT")]
        public void AccessActivitiesFromTables()
        {
            /// 1. Add an activity and Send to holding area
            Activity ActHoldingArea = Help.AddActivity(browser, TestContext.CurrentContext.Test, 
                Const_Mainpro.ActivityCategory.Assessment,
                Const_Mainpro.ActivityCertType.Certified,
                Const_Mainpro.ActivityType.ASMT_CERT_QuebecCategory1Credit_LO,
                Const_Mainpro.ActivityFormat.Live, submitActivity: false);

            /// 2. Go to the Dashboard page, verify that the holding area activity shows in the Holding Area table
            CPDActivitiesListPage ALP = new CPDActivitiesListPage(browser);
            DashboardPage DP = ALP.ClickAndWaitBasePage(ALP.DashboardTab);
            Help.VerifyGridContainsRecord(browser, Const_Mainpro.Table.DashboardTabInc, ActHoldingArea.Title);

            /// Verify that Activities Needs Credit Approval Table not displayed in Dashboard
            Assert.True(
                Browser.FindElement(By.XPath("//div[contains(@class, 'activitiesNeedCreditApprovalContainer ')]"))
                .GetAttribute("class").Contains("hidden"),
                "Activities Needs Credit Approval Table should not show in Dashboard");           


            /// 3. Click on activity and verify that it takes you to the Activity Detail page with the 
            /// activity information
            EnterACPDActivityDetailsPage EADP = Help.Grid_ClickCellInTable(browser, Const_Mainpro.Table.DashboardTabInc,
                cellText: ActHoldingArea.Title);
            Assert.AreEqual(ActHoldingArea.Title,
                EADP.ProgramActivityTitleOrIDOrDescriptionOrSessionEtcTxt.GetAttribute("value"));
            
        }




        #endregion Tests
    }


}