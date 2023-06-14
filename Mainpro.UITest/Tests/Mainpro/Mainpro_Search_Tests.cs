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
    public class Mainpro_Search_Tests : TestBase
    {
        #region Constructors
        public Mainpro_Search_Tests(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }

        // Remote Selenium Grid Test
        public Mainpro_Search_Tests(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }
        #endregion

        #region Tests


        [Test]
        [Description("Verifying the buttons, links and basic high-level search functionality including Session ID " +
            "search")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void BasicSearchPageFunctionalityAndSessions()
        {
            /// 1. On the Enter a CPD Activity page, choose Assessment > Certified > CFPC Certified Mainpro+ Activities
            /// > Online. Verify that City and Activity Date do not appear. Then choose Live and verify they appear
            EnterACPDActivityPage EAP = Help.ChooseActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.Assessment,
                Const_Mainpro.ActivityCertType.Certified,
                Const_Mainpro.ActivityType.ASMT_CERT_CFPCCertifiedMainproActivities_LO,
                Const_Mainpro.ActivityFormat.Online);
            Assert.False(browser.Exists(Bys.EnterACPDActivityPage.CityTxt, ElementCriteria.IsVisible),
                "The City text field appeared when the Online radio button was selected");
            Assert.False(browser.Exists(Bys.EnterACPDActivityPage.ActivityDateTxt, ElementCriteria.IsVisible),
                "The Activity Date text field appeared when the Online radio button was selected");
            EAP.ClickAndWait(EAP.LiveInPersonRdo);
            Assert.True(browser.Exists(Bys.EnterACPDActivityPage.CityTxt, ElementCriteria.IsVisible),
                "The City text field did not appear when the Live radio button was selected");
            Assert.True(browser.Exists(Bys.EnterACPDActivityPage.ActivityDateTxt, ElementCriteria.IsVisible),
                "The Activity Date text field did not appear when the Live radio button was selected");

            /// 2. Enter text into the program field that will return no results and verify an error message returns 
            /// notifying the user. 
            EAP.ProgramActivityTitleTxt.SendKeys("This will return no results");
            EAP.ClickAndWait(EAP.SearchBtn);
            Assert.True(EAP.SearchResultsTblNoResultsLbl.Displayed);
            Assert.AreEqual(EAP.SearchResultsTblNoResultsLbl.Text, "No items were found with the search criteria " +
                "you entered. Try changing one or more search options above and launch another search.");

            /// 3. Enter text into all fields, click Reset Search Criteria, then verify all fields are reset
            EAP.ProgramActivityTitleTxt.SendKeys("this text should be removed");
            EAP.SessionIDTxt.SendKeys("this text should be removed");
            EAP.CityTxt.SendKeys("this text should be removed");
            EAP.ActivityDateTxt.SendKeys("01/01/2021");
            EAP.ClickAndWait(EAP.ResetSearchCriteraBtn);
            Assert.AreEqual("", EAP.ProgramActivityTitleTxt.GetAttribute("value"));
            Assert.AreEqual("", EAP.SessionIDTxt.GetAttribute("value"));
            Assert.AreEqual("", EAP.CityTxt.GetAttribute("value"));
            Assert.AreEqual("", EAP.ActivityDateTxt.GetAttribute("value"));

            /// 4. Click the Need Help link and verify the help window appears
            EAP.ClickAndWait(EAP.NeedHelpLnk);
            EAP.ClickAndWait(EAP.SupportInfoFormCloseBtn);

            /// 5. Click the Close button and verify the Search page disappears
            EAP.ClickAndWait(EAP.CloseBtn);

            /// 6. Click the Dont See Your Activity Click Here link and verify the Activity Details page appears
            Help.ChooseActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.Assessment,
                Const_Mainpro.ActivityCertType.Certified,
                Const_Mainpro.ActivityType.ASMT_CERT_CFPCCertifiedMainproActivities_LO,
                Const_Mainpro.ActivityFormat.Online);
            string session = AppSettings.Config["SearchOnlineSessionIDString"].ToString();
            EAP.Search(Const_Mainpro.ActivitySearchField.SessionID, session);
            EnterACPDActivityDetailsPage EADP = EAP.ClickAndWait(EAP.ClickHereBtn);

            /// 7. Enter an invalid session ID, click Continue and verify the error message
            DashboardPage DP = EADP.ClickAndWaitBasePage(EADP.DashboardTab);
            DP.ClickAndWaitBasePage(DP.DashboardTab);
            DP.ClickAndWaitBasePage(DP.EnterCPDActBtn);
            EAP.DoYouKnowYourSessionIDSearchTxt.SendKeys("34534534534");
            EAP.DoYouKnowYourSessionIDSearchTxt.SendKeys(Keys.Tab);
            // Auto comment: For some reason, we have to wait here or else after clicking the Continue button, the 
            // application returns the wrong session warning label text
            Thread.Sleep(500);
            EAP.DoYouKnowYourSessionIDContinueBtn.Click();
            browser.WaitForElement(Bys.MainproPage.NotificationFormLbl);
            Assert.AreEqual("The Session ID you have entered does not exist.Please enter a different 9-digit " +
                "Session ID.Please use the following format xxxxxx-xxx.", EAP.NotificationLbl.Text);
            EAP.ClickAndWaitBasePage(EAP.NotificationFormXBtn);

            /// 8. Enter a valid session ID, click Continue and verify the Activity Details page loads with the 
            /// cooresponding activity info
            // Fixed defect https://code.premierinc.com/issues/browse/MAINPROREW-825.
            EAP.DoYouKnowYourSessionIDSearchTxt.SendKeys(session);
            EAP.ClickAndWait(EAP.DoYouKnowYourSessionIDContinueBtn);
            StringAssert.Contains(session, EADP.ProgramActivityIDTxt.GetAttribute("value"));

            /// 9. Fill the fields that are required and not filled in yet. Keep the activity title as it's default 
            /// prepopulated value (with session ID). Submit the activity and verify it shows in the Activity Lists page.
            Activity Act = EADP.FillActivityForm(actStartDt: currentDatetime, actCompletionDt: currentDatetime, 
                keepExistingActTitle: true);
            EADP.ClickAndWait(EADP.SubmitBtn);
            EADP.ClickAndWait(EADP.YourActivityHasBeenSubmittedFormGoToCPDActBtn);
            Help.VerifyGridContainsRecord(browser, Const_Mainpro.Table.CPDActitivitesListTabAct, Act.Title);

            /// 10. Go to the Enter An Activity page, enter the session ID that was just submitted, click Continue then 
            /// verify the application warns the user that this session was already submitted
            DP.ClickAndWaitBasePage(DP.DashboardTab);
            DP.ClickAndWaitBasePage(DP.EnterCPDActBtn);
            EAP.DoYouKnowYourSessionIDSearchTxt.SendKeys(session);
            EAP.DoYouKnowYourSessionIDSearchTxt.SendKeys(Keys.Tab);
            // Auto comment: For some reason, we have to wait here or else after clicking the Continue button, the 
            // application returns the wrong session warning label text
            Thread.Sleep(500);
            EAP.DoYouKnowYourSessionIDContinueBtn.Click();
            browser.WaitForElement(Bys.MainproPage.NotificationFormLbl);
            // Fixed defect https://code.premierinc.com/issues/browse/MAINPROREW-834.
            Assert.AreEqual("You have already claimed credit for this session. Please enter a different 9-digit Session ID. " +
                "Please use the following format xxxxxx-xxx.", EAP.NotificationLbl.Text);
        }

        [Test]
        [Description("Given I am on the Activity Search page, when I enter various different search criteria in the " +
            "text fields, Then the results should populate the Activity table accordingly")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void TextFieldSearches()
        {
            // Auto comment: Add the search strings, as well as the error message, into variables
            string program = AppSettings.Config["SearchLiveProgramString"].ToString();
            string session = AppSettings.Config["SearchLiveSessionIDString"].ToString();
            string sessionPartial = AppSettings.Config["SearchLivePartialSessionIDString"].ToString();
            string city = AppSettings.Config["SearcCityString"].ToString();
            string date = AppSettings.Config["SearchActivityDateString"].ToString();
            string programCombo = AppSettings.Config["SearchComboSearchLiveProgramString"].ToString();
            string sessionIDCombo = AppSettings.Config["SearchComboSearchLiveSessionIDString"].ToString();
            string sessionIDOnline = AppSettings.Config["SearchOnlineSessionIDString"].ToString();
            string commonNoResultsError = "The search string returned no results. When we created this test, this " +
                "search criteria returned results. So there may be a defect, or the activities from the search " +
                "results got removed from the DB. If the latter, then add a new search string for this specific " +
                "search criteria to the json config file";

            /// 1. On the Enter a CPD Activity page, choose Assessment > Certified > CFPC Certified Mainpro+ Activities
            /// > Live. 
            EnterACPDActivityPage EAP = Help.ChooseActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.Assessment,
                Const_Mainpro.ActivityCertType.Certified,
                Const_Mainpro.ActivityType.ASMT_CERT_CFPCCertifiedMainproActivities_LO,
                Const_Mainpro.ActivityFormat.Live);

            /// 2. Enter a string in each field separately, click Search and verify all records in the table have this 
            /// string under their respective column
            EAP.Search(Const_Mainpro.ActivitySearchField.ProgramActivityTitle, program);
            Assert.True(ElemGet.Grid_GetRowCount(Browser, EAP.SearchResultsTblBody) > 0, commonNoResultsError);
            Assert_Custom.Grid_AllRowsContainStringWithinCell(Browser, EAP.SearchResultsTblBody,
                Bys.EnterACPDActivityPage.SearchResultsTblFirstRow, 0, "//span", program);

            EAP.Search(Const_Mainpro.ActivitySearchField.SessionID, session);
            Assert.True(ElemGet.Grid_GetRowCount(Browser, EAP.SearchResultsTblBody) > 0, commonNoResultsError);
            Assert_Custom.Grid_AllRowsContainStringWithinCell(Browser, EAP.SearchResultsTblBody,
                Bys.EnterACPDActivityPage.SearchResultsTblFirstRow, 1, "//td", session);

            EAP.Search(Const_Mainpro.ActivitySearchField.SessionID, sessionPartial);
            Assert.True(ElemGet.Grid_GetRowCount(Browser, EAP.SearchResultsTblBody) > 0, commonNoResultsError);
            Assert_Custom.Grid_AllRowsContainStringWithinCell(Browser, EAP.SearchResultsTblBody,
                Bys.EnterACPDActivityPage.SearchResultsTblFirstRow, 1, "//td", sessionPartial);

            EAP.Search(Const_Mainpro.ActivitySearchField.City, city);
            Assert.True(ElemGet.Grid_GetRowCount(Browser, EAP.SearchResultsTblBody) > 0, commonNoResultsError);
            Assert_Custom.Grid_AllRowsContainStringWithinCell(Browser, EAP.SearchResultsTblBody,
                Bys.EnterACPDActivityPage.SearchResultsTblFirstRow, 2, "//td", city);

            EAP.Search(Const_Mainpro.ActivitySearchField.ActivityDate, date);
            Assert.True(ElemGet.Grid_GetRowCount(Browser, EAP.SearchResultsTblBody) > 0, commonNoResultsError);
            Assert_Custom.Grid_AllRowsContainStringWithinCell(Browser, EAP.SearchResultsTblBody,
                Bys.EnterACPDActivityPage.SearchResultsTblFirstRow, 3, "//td", date);

            /// 3. Perform a search on both Program Title and Session ID and verify the results contain both search 
            /// criteria      
            EAP.ActivityDateTxt.Clear();
            EAP.SessionIDTxt.SendKeys(sessionIDCombo);
            EAP.ProgramActivityTitleTxt.SendKeys(programCombo);
            EAP.ClickAndWait(EAP.SearchBtn);
            Assert.True(ElemGet.Grid_GetRowCount(Browser, EAP.SearchResultsTblBody) > 0, commonNoResultsError);
            Assert_Custom.Grid_AllRowsContainStringWithinCell(Browser, EAP.SearchResultsTblBody,
                Bys.EnterACPDActivityPage.SearchResultsTblFirstRow, 0, "//span", programCombo);
            Assert_Custom.Grid_AllRowsContainStringWithinCell(Browser, EAP.SearchResultsTblBody,
            Bys.EnterACPDActivityPage.SearchResultsTblFirstRow, 1, "//td", sessionIDCombo);

            /// 4. Click the Online radio button and verify the search returns an online activity
            EAP.ClickAndWait(EAP.OnlineRdo);
            EAP.Search(Const_Mainpro.ActivitySearchField.SessionID, sessionIDOnline);
            Assert.True(ElemGet.Grid_GetRowCount(Browser, EAP.SearchResultsTblBody) > 0, commonNoResultsError);
            Assert_Custom.Grid_AllRowsContainStringWithinCell(Browser, EAP.SearchResultsTblBody,
                Bys.EnterACPDActivityPage.SearchResultsTblFirstRow, 1, "//td", sessionIDOnline);

            /// 5. Click on the activity and verify the Activity Details page loads with the activity info
            EAP.ClickAndWait(EAP.LiveInPersonRdo);
            EAP.Search(Const_Mainpro.ActivitySearchField.ProgramActivityTitle, program);
            EnterACPDActivityDetailsPage EADP = Help.Grid_ClickCellInTable(browser, 
                Const_Mainpro.Table.ActivitySearchResults, program, Const_Mainpro.TableButtonLinkOrCheckBox.Select);
            StringAssert.Contains(program, EADP.ProgramActivityTitleOrIDOrDescriptionOrSessionEtcTxt.GetAttribute("value"));
        }


        #endregion Tests
    }


}