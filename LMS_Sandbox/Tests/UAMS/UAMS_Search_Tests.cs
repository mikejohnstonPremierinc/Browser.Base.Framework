using Browser.Core.Framework;
using LMS.Data;
//
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using LMS.AppFramework;
using LMS.AppFramework.Constants_;
using LMS.AppFramework.LMSHelperMethods;
using System.Globalization;

namespace UAMS.UITest
{
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome, "", "", Platforms.Windows, "", "")]
    //[RemoteSeleniumTestFixture(BrowserNames.Chrome, EmulationDevices.iPhoneX, "", Platforms.Windows, "", "")]

    [TestFixture]
    public class UAMS_Search : TestBase_UAMS
    {
        #region Constructors

        public UAMS_Search(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public UAMS_Search(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }

        #endregion Constructors

        #region Tests

        [TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("QA"), Category("UAT"), Category("Prod"), Category("Temp")]
        [Description("Storing all Bin links into 2 excel files. One will be without activity counts, so that we can use an excel comparison " +
            "tool for verification against a baseline. The other will be if we need to check activity counts.")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]

        public void PrintBinsToExcel(Constants.SiteCodes siteCode)
        {
            /// 1. Navigate to the Search Results page for every site, perform an empty search, store the list of Bins 
            /// into excel. Save a version with the activity counts, and without them, so that we can then manually review 
            /// activity counts if needed, but also do a baseline comparison of the one without activity counts using an excel compare 
            /// tool.
            string environmentName = AppSettings.Config["environment"];
            List<string> workSheets = new List<string>() { "UAMS" };
            List<List<string>> binsWithActivityCount = new List<List<string>>() { };
            List<List<string>> binsWithoutActivityCount = new List<List<string>>() { };

            HomePage HP = Navigation.GoToHomePage(browser, siteCode);
            // Current production has 'All' in the dropdown. Keep this If statement until next SP
            SearchPage SRP = null;
            if (DateTime.Today < DateTime.ParseExact("06/26/2019", "MM/dd/yyyy", CultureInfo.InvariantCulture)
                && Help.EnvironmentEquals(Constants.Environments.Production))
            {
                SRP = HP.Search(Constants.ActivitySearchType.All);
            }
            else
            {
                SRP = HP.Search(Constants.ActivitySearchType.AllActivities);
            }

            if (Browser.MobileEnabled())
            {
                SRP.ClickAndWait(SRP.Mobile_ShowHideFiltersBtn);
            }

            binsWithActivityCount.Add(SRP.CreditTypeSelElem.Options.Select(t => t.Text).ToList());
            binsWithoutActivityCount.Add(SRP.CreditTypeSelElem.Options.Select(t => t.Text.Substring(0, t.Text.LastIndexOf("[") - 1)).ToList());

            FileUtils.Excel_StoreResults(browser, environmentName + "_BinsWithActivityCount_", workSheets, "Bins", binsWithActivityCount);
            FileUtils.Excel_StoreResults(browser, environmentName + "_BinsWithoutActivityCount_", workSheets, "Bins", binsWithoutActivityCount);
        }

        // Fixed Defect https://code.premierinc.com/issues/browse/LMSREW-961. Will have to redesign test once fixed, because the drop down items
        // are now designed to disappear if they dont have any activities associated to the previously selected Credit Type
        //[TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("QA"), Category("Prod"), Category("OnDemandOnly")]
        [Description("Given I am on the Search Results page, when I perform a blank search, When I click on the each bin, Then the Search Results " +
            "table should populate with at least 1 activity")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]

        public void BinsLoadResults(Constants.SiteCodes siteCode)
        {
            List<List<string>> activities = new List<List<string>>() { };

            /// 1. Navigate to the search page and perform a blank search 
            HomePage HP = Navigation.GoToHomePage(browser, siteCode);
            // Current production has 'All' in the dropdown. Keep this If statement until next SP
            SearchPage SRP = null;
            if (Help.EnvironmentReadyAfterDate(DateTime.ParseExact("06/24/2019", "MM/dd/yyyy", CultureInfo.InvariantCulture),
                Constants.Environments.Production))
            {
                SRP = HP.Search(Constants.ActivitySearchType.AllActivities);
            }
            else
            {
                SRP = HP.Search(Constants.ActivitySearchType.All);
            }

            /// 2. If on mobile, click the Show/Hide Filter button
            if (Browser.MobileEnabled())
            {
                SRP.ClickAndWait(SRP.Mobile_ShowHideFiltersBtn);
            }

            /// 3. Choose every bin, then assert that at least 1 activity loads into the search table.
            for (int i = 0; i < SRP.CreditTypeSelElem.Options.Count; i++)
            {
                SRP.SelectAndWait(SRP.CreditTypeSelElem, i);
                SRP.DeselectAndWait(SRP.CreditTypeSelElem, i);
            }
        }

        // Fixed Defect https://code.premierinc.com/issues/browse/LMSREW-961. Will have to redesign test once fixed, because the drop down items
        // are now designed to disappear if they dont have any activities associated to the previously selected Credit Type
        //[TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("QA"), Category("Prod"), Category("OnDemandOnly")]
        [Description("Given I am on the Search Results page, when I perform a blank search, Then the Credit Type bin count on the UI should " +
            "match the expected count from the database. This test will only be run on-demand, because it has potential for false negatives " +
            "during CI builds, due to the nature of the 'Crawl' that only pushes changes out hourly (not immediately).")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]

        public void BinMatchesDB(Constants.SiteCodes siteCode)
        {
            string environmentName = AppSettings.Config["environment"];
            List<string> binsList = new List<string>() { };
            List<List<string>> activities = new List<List<string>>() { };

            /// 1. Navigate to the search page and perform a blank search 
            HomePage HP = Navigation.GoToHomePage(browser, siteCode);
            // Current production has 'All' in the dropdown. Keep this If statement until next SP
            SearchPage SRP = null;
            if (Help.EnvironmentReadyAfterDate(DateTime.ParseExact("06/24/2019", "MM/dd/yyyy", CultureInfo.InvariantCulture),
                Constants.Environments.Production))
            {
                SRP = HP.Search(Constants.ActivitySearchType.AllActivities);
            }
            else
            {
                SRP = HP.Search(Constants.ActivitySearchType.All);
            }

            /// 2. If on mobile, click the Show/Hide Filter button
            if (Browser.MobileEnabled())
            {
                SRP.ClickAndWait(SRP.Mobile_ShowHideFiltersBtn);
            }

            /// 3. Choose every bin to get the list of activities
            for (int i = 0; i < SRP.CreditTypeSelElem.Options.Count; i++)
            {
                SRP.SelectAndWait(SRP.CreditTypeSelElem, i);
                // Add to the activities list
                activities.Add(SRP.SearchResultsTblBodyActivityLnks.Select(t => t.Text).ToList());

                SRP.DeselectAndWait(SRP.CreditTypeSelElem, i);
            }

            /// 4. Print the list of bins and their activities to excel
            binsList.AddRange(SRP.CreditTypeSelElem.Options.Select(t => t.GetAttribute("label")));
            FileUtils.Excel_StoreResults(browser, environmentName + "_BinsAndTheirActivities_", binsList, "Activities", activities);

            /// 5. Get the count of Credit Type bins on the UI and assert that they match the expected database count
            Assert.AreEqual(DBUtils.GetBinCount_CreditType(Constants.SiteCodes.UAMS, Constants.ActivitySearchType.AllActivities),
                SRP.CreditTypeSelElem.Options.Count);

        }

        //[TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("QA"), Category("UAT"), Category("Prod")]
        [Description("Testing that search function returns the correct results against the database when searching for Live vs Online vs All")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]

        public void SearchVerification(Constants.SiteCodes siteCode)
        {
            /// 1. Navigate to the default page, perform a search on the All Activities activity type and include a search term, then assert that 
            /// the correct results get returned in the list of activities
            HomePage HP = Navigation.GoToHomePage(browser, siteCode);
            // Current production has 'All' in the dropdown. Keep this If statement until next SP
            SearchPage SRP = null;
            if (Help.EnvironmentReadyAfterDate(DateTime.ParseExact("06/24/2019", "MM/dd/yyyy", CultureInfo.InvariantCulture),
                Constants.Environments.Production))
            {
                SRP = HP.Search(Constants.ActivitySearchType.AllActivities);
            }
            else
            {
                SRP = HP.Search(Constants.ActivitySearchType.All);
            }

            List<string> AllActivitiesFromDB = DBUtils.GetActivityTitles(Constants.OrgCodes.UAMS,
                Constants.ActivitySearchType.AllActivities, "AutomationSearchTest");
            List<string> AllActivitiesFromUI = SRP.GetActivityTitles();
            Assert.AreEqual(AllActivitiesFromDB, AllActivitiesFromUI, "The activities from the database and UI do not match");

            /// 2. Choose Online Activity in the activity type drop down, then assert that only the Online activities get returned
            HP.Search(Constants.ActivitySearchType.Online, "AutomationSearchTest");
            List<string> OnlineActivitiesFromDB = DBUtils.GetActivityTitles(Constants.OrgCodes.UAMS,
                Constants.ActivitySearchType.Online, "AutomationSearchTest");
            List<string> OnlineActivitiesFromUI = SRP.GetActivityTitles();
            Assert.AreEqual(OnlineActivitiesFromDB, OnlineActivitiesFromUI, "The activities from the database and UI do not match");

            /// 3. Choose Live Activity in the activity type drop down, include a location search term, then assert that only the Live 
            /// activities from that location get returned
            HP.Search(Constants.ActivitySearchType.Live, "AutomationSearchTest", "Portland");
            List<string> LivePortlandActivitiesFromDB = DBUtils.GetActivityTitles(Constants.OrgCodes.UAMS,
                Constants.ActivitySearchType.Live, "AutomationSearchTest", "Portland");
            List<string> LivePortlandActivitiesFromUI = SRP.GetActivityTitles();
            Assert.AreEqual(LivePortlandActivitiesFromDB, LivePortlandActivitiesFromUI, "The activities from the database and UI do not match");

            /// 4. Change the location to a different location and assert that none of the activities get returned
            HP.Search(Constants.ActivitySearchType.Live, "AutomationSearchTest", "Pittsburgh");
            Assert.True(Browser.Exists(Bys.SearchPage.NoDataAvailableLbl, ElementCriteria.IsVisible), "Activities have been returned");

        }

        #endregion tests
    }
}






