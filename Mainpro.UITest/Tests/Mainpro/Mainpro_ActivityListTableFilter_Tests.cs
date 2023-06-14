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
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]

    [TestFixture]
    public class Mainpro_ActivityListTableFilter_Tests : TestBase
    {
        #region Constructors
        public Mainpro_ActivityListTableFilter_Tests(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public Mainpro_ActivityListTableFilter_Tests(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version , platform, hubUri, extrasUri)
        { }
        #endregion


        #region Tests

        [Test]
        [Description("Given I add an activity and edit it throughout the test, as such that it would satisfy the " +
            "various filter criteria of the CPD Activities List page table Select Element filters, then those " +
            "filters should filter the activity accordingly")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        [Category("UAT")]
        [Category("QA")]
        public void CFPCActivityFiltersTest()
        {
            /// 1. Add an activity with a Completion Date within this year, but not in this month and send 
            /// it to the Holding Area. For automation purposes, if the current month is January, make the month a 
            /// February date, else just make it in January
            DateTime completionDate = currentDatetime.Month == 1 ?
                new DateTime(currentDatetime.Year, 2, 1) :
                new DateTime(currentDatetime.Year, 1, 1);
            Activity Act = Help.AddActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.Assessment,
                Const_Mainpro.ActivityCertType.Certified,
                Const_Mainpro.ActivityType.ASMT_CERT_QuebecCategory1Credit_LO,
                Const_Mainpro.ActivityFormat.Live,
                actCompletionDt: completionDate,
                submitActivity: false);

            /// 2. On the CPD Activities List page, select Complete in the Activity Status Select Element and 
            /// verify the activity does not appear in the table. Then Select In Progress and verify the 
            /// activity appears
            CPDActivitiesListPage ALP = new CPDActivitiesListPage(browser);
            ALP.ClickAndWaitBasePage(ALP.CPDActivitiesListTab);
            ALP.SelectAndWait(ALP.ActTblStatusSelElem, "Complete");
            Help.VerifyGridDoesNotContainRecord(browser, Const_Mainpro.Table.CPDActitivitesListTabAct, Act.Title);
            ALP.SelectAndWait(ALP.ActTblStatusSelElem, "In Progress");
            Help.VerifyGridContainsRecord(browser, Const_Mainpro.Table.CPDActitivitesListTabAct, Act.Title);

            /// Select Activity Date = This Month, verify the activity disappears, then select
            /// Activity Date = This Year, verify the activity appears
            ALP.SelectAndWait(ALP.ActTblActivityDateSelElem, "This Month");
            Help.VerifyGridDoesNotContainRecord(browser, Const_Mainpro.Table.CPDActitivitesListTabAct, Act.Title);
            ALP.SelectAndWait(ALP.ActTblActivityDateSelElem, "This Year");
            Help.VerifyGridContainsRecord(browser, Const_Mainpro.Table.CPDActitivitesListTabAct, Act.Title);

            /// 3. Change the Completion Date of the activity to a date outside the cycle date, then Submit 
            /// the activity. On the activity table, select Cycle = current cycle, verify the activity does not
            /// show, then select Cycle = All and verify it shows
            EnterACPDActivityDetailsPage EADP = Help.Grid_ClickCellInTable(browser, 
                Const_Mainpro.Table.CPDActitivitesListTabAct, Act.Title,
                Const_Mainpro.TableButtonLinkOrCheckBox.CompleteActivity);
            EADP.FillActivityForm(actTitle: Act.Title, actStartDt: currentDatetime.AddYears(-7),
                actCompletionDt: currentDatetime.AddYears(-7));
            EADP.ClickAndWait(EADP.SubmitBtn);
            EADP.ClickAndWait(EADP.YourActivityHasBeenSubmittedFormGoToCPDActBtn);
            ALP.SelectAndWait(ALP.ActTblCycleSelElem, ALP.ActTblCycleSelElem.Options[1].Text);
            Help.VerifyGridDoesNotContainRecord(browser, Const_Mainpro.Table.CPDActitivitesListTabAct, Act.Title);
            ALP.SelectAndWait(ALP.ActTblCycleSelElem, "All");
            Help.VerifyGridContainsRecord(browser, Const_Mainpro.Table.CPDActitivitesListTabAct, Act.Title);
        }

        #endregion Tests
    }
}
