using Browser.Core.Framework;
using LS.AppFramework.Constants_LTS;
using Mainpro.AppFramework;
using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace Mainpro.UITest
{
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]
    [TestFixture]
    public class Mainpro_RemedialReinstatementCycle_Tests : TestBase
    {
        #region Constructors
        public Mainpro_RemedialReinstatementCycle_Tests(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public Mainpro_RemedialReinstatementCycle_Tests(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }
        #endregion

        #region Tests

        [Test]
        [Description("Verifying the following for a RemedialReinstatementCycle user: 2 year cycle Timeframe, " +
            "Credit Requirements criteria , AMA/RCP Link display,  Annual CreditSummaryTable display" +
            "No Leave Adjustment and AMA/RCP Max Credit Validation")]
        [Property("Status", "Completed")]
        [Author("Bama Thangaraj")]
        public void RemedialReinstatementCycle_PrimaryAndAMARCPValidations()
        {
            /// 1. Create a Default cycle user, then put the user into a 
            /// Remedial ReinstatementCycle using the Adjustment API
            UserModel user = UserUtils.CreateAndRegisterUser(currentTest: TestContext.CurrentContext.Test);
            LoginPage LP = Navigation.GoToLoginPage(browser);
            DashboardPage DP = LP.Login(user.Username, isNewUser: true);
            UserUtils.AdjustUserCycle(Browser, user.Username, Const_Mainpro.AdjustmentCodes.RR,
                effectiveDate: currentDatetime.AddYears(-1));

            ///2. Verify the user met all these condtions -  2 year cycle Timeframe, 
            ///  AMA/RCP Link is displayed, Annual Requirement Table is displayed
            Assert.True(DP.ClickHereToViewYourAmaRCPCreditsLnk.Displayed, "ClickHereToViewYourAmaRCPCreditsLink " +
                "should be displayed for Remedial Reinstatement cycle user");
            Help.VerifyCellTextMatches(browser, DP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                   rowName: Const_Mainpro.TableRowNames.Certified.GetDescription(),
                   colName: Const_Mainpro.TableColumnNames.Required.GetDescription(), cellTextExpected: "50");
            Help.VerifyCellTextMatches(browser, DP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                   rowName: Const_Mainpro.TableRowNames.Total.GetDescription(),
                   colName: Const_Mainpro.TableColumnNames.Required.GetDescription(), cellTextExpected: "100");

            Assert.True(Help.GetCycleYearsTotal(Browser) == 2, "Remedial Reinstatement user " +
                "should be in 2 year cycle range");
            
            DP.ClickAndWaitBasePage(DP.CreditSummaryTab);
            Assert.True(Browser.Exists(Bys.CreditSummaryPage.AnnualRequirementsTbl, ElementCriteria.IsVisible),
                "Remedial Reinstatement Cycle should display Annual Requirement Table in Credit Summary Page ");

            var actCategory1 = Const_Mainpro.ActivityCategory.SelfLearning;
            var actType1 = Const_Mainpro.ActivityType.SELFLRNING_CERT_AmericanMedicalAssociationAMAPRACategory1_LO;
            var actCertType = Const_Mainpro.ActivityCertType.Certified;
            var actCategory2 = Const_Mainpro.ActivityCategory.Assessment;
            var actType2 = Const_Mainpro.ActivityType.ASMT_CERT_RoyalCollegeMOCAccreditedSection3_LO;
            var actFormat = Const_Mainpro.ActivityFormat.Online;

            /// 3. Submit an AMA and RCP activty with 200 Credits 
            Activity Act1 = Help.AddActivity(browser, TestContext.CurrentContext.Test,
                actCategory1, actCertType, actType1, actFormat, creditsRequested: 200);

            /// 4. Validate the credit through LTS, go to the Activities List page, then verify that the CPD Activity 
            /// List page table and Credit Summary widget table contains the activity with the following credit amounts. 
            /// Credits Reported = 200, Credits Applied = 25
            Help.ValidateCreditReevaluateUserThenRelaunchMainpro(Browser, user.Username, Act1.Title, 
               TestContext.CurrentContext.Test.Name);
            CPDActivitiesListPage ALP = LP.ClickAndWaitBasePage(LP.CPDActivitiesListTab);
            ALP.SelectAndWait(ALP.ActTblCycleSelElem, "All");
            Help.VerifyGridContainsRecord(browser, Const_Mainpro.Table.CPDActitivitesListTabAct, Act1.Title);
            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CPDActitivitesListTabAct,
                rowName: Act1.Title, colName: "Credits Reported", cellTextExpected: "200");
            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CPDActitivitesListTabAct,
                rowName: Act1.Title, colName: "Credits Applied", cellTextExpected: "25");
            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                rowName: "Certified", colName: "Applied", cellTextExpected: "25");

            Activity Act2 = Help.AddActivity(browser, TestContext.CurrentContext.Test,
               actCategory2, actCertType, actType2, actFormat, creditsRequested: 200);
            Help.ValidateCreditReevaluateUserThenRelaunchMainpro(Browser, user.Username, Act2.Title, 
            TestContext.CurrentContext.Test.Name);

            /// 5. Click Enter a CPD Activity, choose the same AMA and RCP activities, click Continue 
            /// and verify that a form appears
            /// indicating the user has already added the maximum of 25 credits
            EnterACPDActivityPage EAP = ALP.ClickAndWaitBasePage(ALP.EnterCPDActBtn);
            EAP.SelectAndWait(EAP.CategorySelElem, actCategory1.GetDescription());
            EAP.ClickAndWait(EAP.CertifiedRdo);
            EAP.SelectAndWait(EAP.ActivityTypeSelElem, actType1.GetDescription());
            Assert.AreEqual("25", EAP.MaxCreditReachedFormClaimedLbl.Text);
            EAP.ClickAndWait(EAP.MaxCreditReachedFormStartOverBtn);
            EAP.SelectAndWait(EAP.CategorySelElem, actCategory2.GetDescription());
            EAP.ClickAndWait(EAP.CertifiedRdo);
            EAP.SelectAndWait(EAP.ActivityTypeSelElem, actType2.GetDescription());
            Assert.AreEqual("25", EAP.MaxCreditReachedFormClaimedLbl.Text);
            
            /// 6. Verify the user can not apply for leave adjustment 
            LSHelp.Login(Browser, 
               TestContext.CurrentContext.Test.Name, AppSettings.Config["ltspassword"]);

            LS.AppFramework.ProgramPage PP = LSHelp.GoToParticipantProgramPage(Browser, "College of Family Physician",
             user.Username, "Certification of Proficiency");

            String leaveStartDate = currentDatetime.AddYears(-3).ToShortDateString();
            String leaveEndDate = currentDatetime.AddYears(-2).ToShortDateString();

            PP.ClickAndWait(PP.ProgramAdjustmentsTab);

            PP.ClickAndWait(PP.ProgAdjustTabAddAdjustLnk);
            PP.SelectAndWait(PP.ProgAdjustTabAddAdjustFormAdjustCodeSelElem, Constants_LTS.AdjustmentCodes.LLeave.GetDescription());
            Browser.WaitForElement(LS.AppFramework.Bys.ProgramPage.ProgAdjustTabAddAdjustFormLeaveStartDtTxt, ElementCriteria.IsVisible);
            PP.ProgAdjustTabAddAdjustFormLeaveStartDtTxt.SendKeys(leaveStartDate);
            PP.ProgAdjustTabAddAdjustFormLeaveEndDtTxt.SendKeys(leaveEndDate);
            PP.ProgAdjustTabAddAdjustFormAddAdjustBtn.Click();           
            Browser.WaitForElement(By.XPath("//div[@class='error warning']"), TimeSpan.FromSeconds(120), ElementCriteria.IsVisible);
            Assert.True(Browser.FindElement(By.XPath("//div[@class='error warning']")).Text.
                Contains("The adjustment (Adjustment Code: CFPC_LEAVE) cannot be applied to this recognition"),
                "Leave Adjustments should not be applied to Remedial Reinstatement cycle user");

        }

        [Test]
        [Description("Given I adjust the user into Remedial Reinstatement cycle and Submitted an activity with " +
            "140 certified credits, When I complete the user's Current cycle, Then The user should be" +
           " rolled over to next new Default cycle with No Carryover Credits, and New Default Cycle " +
            "basic conditions should be met")]
        [Property("Status", "Completed")]
        [Author("Bama Thangaraj")]
        public void RemedialReinstatementCycleCompletion()
        {
            DateTime usercreationdate;
            DateTime RREffectiveDate;
            if (currentDatetime.Month > 6)
            {
                // 01/July/currentyear-1 (ex: currentyear =2021, then 01/july/2020
                usercreationdate = new DateTime(currentDatetime.Year - 1, 7, 1);
                RREffectiveDate = usercreationdate.AddYears(1);//   01/july/2019
            }
            else
            {
                // 01/July/currentyear-1 (ex: currentyear =2021, then 01/july/2020
                usercreationdate = new DateTime(currentDatetime.Year - 1, 7, 1); 
                 RREffectiveDate = currentDatetime;
            }
            
            /// 1. Create a Default cycle user, then put the user into a RemedialReinstatementCycle 
            /// using the Adjustment API.
            /// Register the user on currentyear-2 , and applied adjustment on currentyear-1
            /// because activity credits should be submitted before cycle completion date .
            /// And we are submitting the activities on within 1st year of cycle starts to meet the 
            /// cycle completion criteria 
            UserModel user = UserUtils.CreateAndRegisterUser(currentTest: TestContext.CurrentContext.Test,
                effectiveDt: usercreationdate);
            LoginPage LP = Navigation.GoToLoginPage(browser);
            DashboardPage DP = LP.Login(user.Username, isNewUser: true);
            UserUtils.AdjustUserCycle(Browser, user.Username, Const_Mainpro.AdjustmentCodes.RR, effectiveDate:
                 RREffectiveDate);

            /// 2. Submit an activity with certified = 140 credits and validate the credits through LTS.
            /// So current cycle will be automatically gets completed 
            Activity Act1 = Help.AddActivity(browser, TestContext.CurrentContext.Test,
               Const_Mainpro.ActivityCategory.Assessment,
               Const_Mainpro.ActivityCertType.Certified,
               Const_Mainpro.ActivityType.ASMT_CERT_OtherCFPCCertifiedMainproAssessmentActivities_LO,
               Const_Mainpro.ActivityFormat.Live,
               username: user.Username, isNewUser: false,
               creditsRequested: 140,
               actStartDt: RREffectiveDate,
               actCompletionDt: RREffectiveDate);
            Help.ValidateCreditReevaluateUserThenRelaunchMainpro(Browser, user.Username, Act1.Title,
                TestContext.CurrentContext.Test.Name);
            
            /// 3. On the Details tab of the Program page of LTS, assert that the new cycle is showing as 
            /// Default and is 5 years
            LSHelp.Login(browser, TestContext.CurrentContext.Test.Name, AppSettings.Config["ltspassword"]);
            LSHelp.GoToParticipantProgramPage(browser, "College of Family Physician", user.Username,
                "Certification of Proficiency");
            DateTime completiondate = RREffectiveDate;
            By ProgramLblBy = By.XPath("//td[contains(text(),'Program:')]/following-sibling::td[1]");
            LSHelp.VerifyLabelTextEquals(Browser, ProgramLblBy, "Default");
            String newcyclestartdate = completiondate.ToString("M/d/yyyy");
            Assert.AreEqual(newcyclestartdate, LSHelp.GetProgramDetail(browser, "Starts"),
                "Start Date is not correct in LTST-ProgramDetail");
            String newcycleenddate = String.Format("6/30/{0}", completiondate.AddYears(5).Year);
            Assert.AreEqual(newcycleenddate, LSHelp.GetProgramDetail(browser, "Ends"),
                "End Date is not correct in LTST-ProgramDetail");

            /// 4. Launch the user and verify the Current Cycle label shows a 5-year range, 
            /// the AMARCP link appears, the Certified Required label is 125           
            LSHelp.LaunchSiteFromParticipantPage(browser, "College of Family Physician", user.Username);
            DP = new DashboardPage(browser);
            DP.WaitForInitialize();
            Help.SwitchToRewriteAfterLaunchingFromLTST(browser);
            int cycleStartYr = Int32.Parse(DP.CurrentCycleDateLbl.Text.Substring(6, 4));
            int cycleEndYr = Int32.Parse(DP.CurrentCycleDateLbl.Text.Substring(19, 4));
            Assert.AreEqual(cycleStartYr, completiondate.Year,
                "DashboardPage - Cycle year Start Date is incorrect");
            Assert.AreEqual(cycleEndYr, completiondate.AddYears(5).Year,
                "DashboardPage - Cycle year End Date is incorrect");

            Assert.True(DP.ClickHereToViewYourAmaRCPCreditsLnk.Displayed,
                "ClickHereToViewYourAmaRCPCreditsLink should display for Default cycle user");
            Help.VerifyCellTextMatches(browser, DP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                   rowName: Const_Mainpro.TableRowNames.Certified.GetDescription(),
                   colName: Const_Mainpro.TableColumnNames.Required.GetDescription(), cellTextExpected: "125");
            
            /// 5. Verify the Credit Summary page should not display any Carryover credits 
            DP.ClickAndWaitBasePage(DP.CreditSummaryTab);
            Help.VerifyCellTextMatches(browser, DP, Const_Mainpro.Table.CreditSummaryTabOther,
                    rowName: "Carryover Certified Credit", colName: "Credits Applied", cellTextExpected: "-");

        }

        #endregion Tests
    }


}