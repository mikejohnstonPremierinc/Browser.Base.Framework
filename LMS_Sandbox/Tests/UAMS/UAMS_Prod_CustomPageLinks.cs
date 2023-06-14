using Browser.Core.Framework;
using LMS.Data;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using LMS.AppFramework;
using LMS.AppFramework.LMSHelperMethods;
using LMS.AppFramework.Constants_;
//
//
using System.Data;
using System.Configuration;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Web;
using System.Data.OleDb;
using OfficeOpenXml;
using System.Security.Cryptography.X509Certificates;
using System.Drawing;
using OfficeOpenXml.Style;
using OfficeOpenXml.Drawing.Chart;



namespace UAMS.UITest
{
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome, "", "", Platforms.Windows, "", "")]

    [TestFixture]
    public class UAMS_CustomPage_CERequestPageLinks : TestBase_UAMS
    {
        #region Constructors

        public UAMS_CustomPage_CERequestPageLinks(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public UAMS_CustomPage_CERequestPageLinks(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }

        #endregion Constructors



        #region Tests

        [Test, Category("Prod"), Category(siteCodeCategory), Category("CustomPage")]
        [Description("Given I navigate to the Community CE Requests page and get all links and header text, When I navigate to the Fireball " +
            "CE Request page, Then the links should match")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void CERequestLinksFireballLinksVisibleHdrPresent()
        {
            if (Help.EnvironmentEquals(Constants.Environments.UAT) || Help.EnvironmentEquals(Constants.Environments.CMEQA))
            {
                Assert.Ignore("Only executing in Production as that is the environment these tests are designed for. Custom page " +
                    "testing was never requested to be executed in lower environments");
            }

            /// 1. Navigate to the legacy CE Request page and get the count of links
            Navigation_CustomPages.GoToCERequestLegacyPage(Browser);      
            int countOfLegacyPageLinks = Browser.FindElements(By.XPath("//p//a")).Count;

            /// 2. Navigate to the new page and get the count of links and assert that they match.
            CERequestPage CEP = Navigation_CustomPages.GoToCERequestPage(Browser);
            // The Click Here element is contained within a frame, so we have to switch inside there
            WebDriverWait wait = new WebDriverWait(Browser, TimeSpan.FromSeconds(90));
            wait.Until(driver =>
            {
                try
                {
                    return driver.SwitchTo().Frame(0);
                }
                catch (NoSuchFrameException)
                {
                    return null;
                }
            });
            CEP.WaitForElement(Bys.CERequestPage.VideographyLnk, ElementCriteria.IsVisible);

            int countOfNewPageLinks = Browser.FindElements(By.XPath("//a")).Count;
            Assert.AreEqual(countOfLegacyPageLinks, countOfNewPageLinks, "The count of links do not match");
        }

        [Test, Category("Prod"), Category(siteCodeCategory), Category("CustomPage")]
        [Description("Validating the groupings and order of the custom page")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ComponentOrder()
        {
            if (Help.EnvironmentEquals(Constants.Environments.UAT) || Help.EnvironmentEquals(Constants.Environments.CMEQA))
            {
                Assert.Ignore("Only executing in Production as that is the environment these tests are designed for. Custom page " +
                    "testing was never requested to be executed in lower environments");
            }

            if (Help.EnvironmentEquals(Constants.Environments.UAT) || Help.EnvironmentEquals(Constants.Environments.CMEQA))
            {
                Assert.Ignore("Only executing in Production as that is the environment these tests are designed for. Custom page " +
                    "testing was never requested to be executed in lower environments");
            }


            /// 1. Navigate to the custom page and verify the order of the HTML components. 
            CERequestPage CEP = Navigation_CustomPages.GoToCERequestPage(Browser);
            Help.CustomPage_VerifyOrderOfHTMLComponents(Browser);
        }

        #endregion tests
    }

    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome, "", "", Platforms.Windows, "", "")]

    [TestFixture]
    public class UAMS_CustomPage_TeleConfLinks : TestBase_UAMS
    {
        #region Constructors

        public UAMS_CustomPage_TeleConfLinks(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public UAMS_CustomPage_TeleConfLinks(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }

        #endregion Constructors



        #region Tests

        [Test, Category("Prod"), Category(siteCodeCategory), Category("CustomPage")]
        [Description("Validating the groupings and order of the custom page")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ComponentOrder()
        {
            if (Help.EnvironmentEquals(Constants.Environments.UAT) || Help.EnvironmentEquals(Constants.Environments.CMEQA))
            {
                Assert.Ignore("Only executing in Production as that is the environment these tests are designed for. Custom page " +
                    "testing was never requested to be executed in lower environments");
            }

            /// 1. Navigate to the custom page and verify the order of the HTML components. 
            TeleconferencePage TP = Navigation_CustomPages.GoToTeleconferencePage(Browser);
            Help.CustomPage_VerifyOrderOfHTMLComponents(Browser);
        }

        #endregion tests
    }

    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome, "", "", Platforms.Windows, "", "")]

    [TestFixture]
    public class UAMS_CustomPage_BreastfeedingPageLinks : TestBase_UAMS
    {
        #region Constructors

        public UAMS_CustomPage_BreastfeedingPageLinks(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public UAMS_CustomPage_BreastfeedingPageLinks(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }

        #endregion Constructors



        #region Tests

        [Test, Category("Prod"), Category(siteCodeCategory), Category("CustomPage")]
        [Description("Given I navigate to the Community Breastfeeding page and get the link count and text of all tables, When I navigate " +
     "to the Fireball Breastfeeding page, Then all link counts and text should match. Link text from Community and Fireball will be " +
     "saved as an excel file for manual review, if needed")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void BreastfeedingFireballPageLinksAppear()
        {
            if (Help.EnvironmentEquals(Constants.Environments.UAT) || Help.EnvironmentEquals(Constants.Environments.CMEQA))
            {
                Assert.Ignore("Only executing in Production as that is the environment these tests are designed for. Custom page " +
                    "testing was never requested to be executed in lower environments");
            }

            /// 1. Navigate to the legacy Breastfeeding page and get the count of links and link text of each table
            _BreastFeedingPage _Page = Navigation_CustomPages.GoToBreastFeedingLegacyPage(Browser);
            var _BreastFeedCurrPrgsTable =
                Help.GetLegacyCustomPageTableLinkCountAndText(Browser, _Page.BreastFeedingCurricTbl, _Page.BreastFeeingCurricTblHdrLnks);
            var _BundledActivitiesTable =
                Help.GetLegacyCustomPageTableLinkCountAndText(Browser, _Page.BundledActivitiesTbl, _Page.BundledActivitiesTblHdrLnks);
            var _BundledProgramsTable =
                Help.GetLegacyCustomPageTableLinkCountAndText(Browser, _Page.BundledProgramsTbl, _Page.BundledProgramsTblHdrLnks);
            var _FeaturedActivitiesTable =
                Help.GetLegacyCustomPageTableLinkCountAndText(Browser, _Page.FeaturedActivitiesTbl, _Page.FeaturedActivitiesTblHdrLnks);

            /// 2. Navigate to the new page and get the count of links and link text of each table. 
            BreastFeedingPage Page = Navigation_CustomPages.GoToBreastFeedingPage(Browser);
            var BreastFeedCurrPrgsTable =
                Help.GetFireballCustomPageTableLinkCountAndText(Browser, Page.BreastFeeingCurricTbl);
            var BundledActivitiesTable =
                Help.GetFireballCustomPageTableLinkCountAndText(Browser, Page.BundledActivitiesTbl);
            var BundledProgramsTable =
                Help.GetFireballCustomPageTableLinkCountAndText(Browser, Page.BundledProgramsTbl);
            var FeaturedActivitiesTable =
                Help.GetFireballCustomPageTableLinkCountAndText(Browser, Page.FeaturedActivitiesTbl);

            /// 3. Save the link text from each table to an excel file
            var workSheetNames = new List<string>() { "Curriculum Programs", "Bundled Activities", "Bundled Programs", "Featured Activities" };
            var linksPerTable = new List<(List<string> list1, List<string> list2)>();

            linksPerTable.Add((_BreastFeedCurrPrgsTable.LinkText, BreastFeedCurrPrgsTable.LinkText));
            linksPerTable.Add((_BundledActivitiesTable.LinkText, BundledActivitiesTable.LinkText));
            linksPerTable.Add((_BundledProgramsTable.LinkText, BundledProgramsTable.LinkText));
            linksPerTable.Add((_FeaturedActivitiesTable.LinkText, FeaturedActivitiesTable.LinkText));

            FileUtils.Excel_StoreResults(Browser, "BreastFeedingPageLinks", workSheetNames, communityPageWorksheetName,
                fireballPageWorksheetName, linksPerTable);

            /// 4. Assert that counts match for all tables
            Assert.AreEqual(_BreastFeedCurrPrgsTable.CountOfLinks, BreastFeedCurrPrgsTable.CountOfLinks, "The count of links on the Breastfeeding " +
                "Curriculum Programs table do not match");
            Assert.AreEqual(_BundledActivitiesTable.CountOfLinks, BundledActivitiesTable.CountOfLinks, "The count of links on the Bundled " +
                "Activities table do not match");
            Assert.AreEqual(_BundledProgramsTable.CountOfLinks, BundledProgramsTable.CountOfLinks, "The count of links on the Bundled " +
                 "Programs table do not match");
            Assert.AreEqual(_FeaturedActivitiesTable.CountOfLinks, FeaturedActivitiesTable.CountOfLinks, "The count of links on the Featured " +
                "Activities table do not match");
        }

        [Test, Category("Prod"), Category(siteCodeCategory), Category("CustomPage")]
        [Description("Validating the groupings and order of the custom page")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ComponentOrder()
        {
            if (Help.EnvironmentEquals(Constants.Environments.UAT) || Help.EnvironmentEquals(Constants.Environments.CMEQA))
            {
                Assert.Ignore("Only executing in Production as that is the environment these tests are designed for. Custom page " +
                    "testing was never requested to be executed in lower environments");
            }

            /// 1. Navigate to the custom page and verify the order of the HTML components. 
            BreastFeedingPage Page = Navigation_CustomPages.GoToBreastFeedingPage(Browser);
            Help.CustomPage_VerifyOrderOfHTMLComponents(Browser);
        }

        #endregion tests
    }

    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome, "", "", Platforms.Windows, "", "")]

    [TestFixture]
    public class UAMS_CustomPage_AngelsPageLiveLinks : TestBase_UAMS
    {
        #region Constructors

        public UAMS_CustomPage_AngelsPageLiveLinks(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public UAMS_CustomPage_AngelsPageLiveLinks(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }

        #endregion Constructors



        #region Tests

        [Test, Category("Prod"), Category(siteCodeCategory), Category("CustomPage")]
        [Description("Given I navigate to the Community Angels Live page and get the link count and text of all tables, When I navigate " +
             "to the Fireball Angels Live page, Then all link counts and text should match. Link text from Community and Fireball will be " +
             "saved as an excel file for manual review, if needed")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void AngelsLiveFireballPageLinksAppear()
        {
            if (Help.EnvironmentEquals(Constants.Environments.UAT) || Help.EnvironmentEquals(Constants.Environments.CMEQA))
            {
                Assert.Ignore("Only executing in Production as that is the environment these tests are designed for. Custom page " +
                    "testing was never requested to be executed in lower environments");
            }

            /// 1. Navigate to the legacy AngelsLive page and get the count of links and link text of each table
            _AngelsLivePage _Page = Navigation_CustomPages.GoToAngelsLiveLegacyPage(Browser);
            var _OBEmergenciesTable =
                Help.GetLegacyCustomPageTableLinkCountAndText(Browser, _Page.OBEmergenciesTbl, _Page.OBEmergenciesTblHdrLnks);
            var _FetalHeartMonitoringTable =
                Help.GetLegacyCustomPageTableLinkCountAndText(Browser, _Page.FetalHeartMonitoringTbl, _Page.FetalHeartMonitoringTblHdrLnks);
            var _SpecialEventsTable =
                Help.GetLegacyCustomPageTableLinkCountAndText(Browser, _Page.SpecialEventsTbl, _Page.SpecialEventsTblHdrLnks);

            /// 2. Navigate to the new page and get the count of links and link text of each table. 
            AngelsLivePage Page = Navigation_CustomPages.GoToAngelsLivePage(Browser);
            var OBEmergenciesTable =
                Help.GetFireballCustomPageTableLinkCountAndText(Browser, Page.OBEmergenciesTbl);
            var FetalHeartMonitoring =
                Help.GetFireballCustomPageTableLinkCountAndText(Browser, Page.FetalHeartMonitoringTbl);
            var SpecialEventsTable =
                Help.GetFireballCustomPageTableLinkCountAndText(Browser, Page.SpecialEventsTbl);

            /// 3. Save the link text from each table to an excel file
            var workSheetNames = new List<string>() { "OB Emergencies", "Fetal Heart Monitoring", "Special Events" };
            var linksPerTable = new List<(List<string> list1, List<string> list2)>();

            linksPerTable.Add((_OBEmergenciesTable.LinkText, OBEmergenciesTable.LinkText));
            linksPerTable.Add((_FetalHeartMonitoringTable.LinkText, FetalHeartMonitoring.LinkText));
            linksPerTable.Add((_SpecialEventsTable.LinkText, SpecialEventsTable.LinkText));

            FileUtils.Excel_StoreResults(Browser, "AngelsLivePageLinks", workSheetNames, communityPageWorksheetName,
                fireballPageWorksheetName, linksPerTable);

            /// 4. Assert that counts match for all tables
            Assert.AreEqual(_OBEmergenciesTable.CountOfLinks, OBEmergenciesTable.CountOfLinks, "The count of links on the " +
                "OB Emergencies table do not match");
            Assert.AreEqual(_FetalHeartMonitoringTable.CountOfLinks, FetalHeartMonitoring.CountOfLinks, "The count of links on the " +
                "Fetal Heart Monitoring table do not match");
            Assert.AreEqual(_SpecialEventsTable.CountOfLinks, SpecialEventsTable.CountOfLinks, "The count of links on the " +
                 "Special Events table do not match");
        }

        [Test, Category("Prod"), Category(siteCodeCategory), Category("CustomPage")]
        [Description("Validating the groupings and order of the custom page")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ComponentOrder()
        {
            if (Help.EnvironmentEquals(Constants.Environments.UAT) || Help.EnvironmentEquals(Constants.Environments.CMEQA))
            {
                Assert.Ignore("Only executing in Production as that is the environment these tests are designed for. Custom page " +
                    "testing was never requested to be executed in lower environments");
            }

            /// 1. Navigate to the custom page and verify the order of the HTML components. 
            AngelsLivePage Page = Navigation_CustomPages.GoToAngelsLivePage(Browser);
            Help.CustomPage_VerifyOrderOfHTMLComponents(Browser);
        }

        #endregion tests
    }

    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome, "", "", Platforms.Windows, "", "")]

    [TestFixture]
    public class UAMS_CustomPage_ThreeInThirtyPageLinks : TestBase_UAMS
    {
        #region Constructors

        public UAMS_CustomPage_ThreeInThirtyPageLinks(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public UAMS_CustomPage_ThreeInThirtyPageLinks(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }

        #endregion Constructors



        #region Tests

        [Test, Category("Prod"), Category(siteCodeCategory), Category("CustomPage")]
        [Description("Given I navigate to the Community Three In Thirty page and get the link count and text of all tables, When I navigate " +
       "to the Fireball Three In Thirty page, Then all link counts and text should match. Link text from Community and Fireball will be " +
       "saved as an excel file for manual review, if needed")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ThreeInThirtyFireballPageLinksAppear()
        {
            //****************************NOTE: This test and the cooresponding pages classes will need refactored***********************//
            //****************************once the client updates "Coming Soon" tables with real data and real labels********************//


            if (Help.EnvironmentEquals(Constants.Environments.UAT) || Help.EnvironmentEquals(Constants.Environments.CMEQA))
            {
                Assert.Ignore("Only executing in Production as that is the environment these tests are designed for. Custom page " +
                    "testing was never requested to be executed in lower environments");
            }

            /// 1. Navigate to the legacy page and get the count of links and link text of each table
            _ThreeInThirtyPage _Page = Navigation_CustomPages.GoToThreeInThirtyLegacyPage(Browser);
            var _ComingSoon1Table =
                Help.GetLegacyCustomPageTableLinkCountAndText(Browser, _Page.ComingSoon1Tbl, _Page.ComingSoon1TblHdrLnks);
            var _ComingSoon2Table =
                Help.GetLegacyCustomPageTableLinkCountAndText(Browser, _Page.ComingSoon2Tbl, _Page.ComingSoon2TblHdrLnks);
            var _ComingSoon3Table =
                Help.GetLegacyCustomPageTableLinkCountAndText(Browser, _Page.ComingSoon3Tbl, _Page.ComingSoon3TblHdrLnks);

            /// 2. Navigate to the new page and get the count of links and link text of each table.
            ThreeInThirtyPage Page = Navigation_CustomPages.GoToThreeInThirtyPage(Browser);
            var ComingSoon1Table =
                Help.GetFireballCustomPageTableLinkCountAndText(Browser, Page.ComingSoon1Tbl);
            var ComingSoon2Table =
                Help.GetFireballCustomPageTableLinkCountAndText(Browser, Page.ComingSoon2Tbl);
            var ComingSoon3Table =
                Help.GetFireballCustomPageTableLinkCountAndText(Browser, Page.ComingSoon3Tbl);

            /// 3. Save the link text from each table to an excel file
            var workSheetNames = new List<string>() { "Coming Soon 1", "Coming Soon 2", "Coming Soon 3" };
            var linksPerTable = new List<(List<string> list1, List<string> list2)>();

            linksPerTable.Add((_ComingSoon1Table.LinkText, ComingSoon1Table.LinkText));
            linksPerTable.Add((_ComingSoon2Table.LinkText, ComingSoon2Table.LinkText));
            linksPerTable.Add((_ComingSoon3Table.LinkText, ComingSoon3Table.LinkText));

            FileUtils.Excel_StoreResults(Browser, "ThreeInThirtyPageLinks", workSheetNames, communityPageWorksheetName,
                fireballPageWorksheetName, linksPerTable);

            /// 4. Assert that counts match for all tables
            Assert.AreEqual(_ComingSoon1Table.CountOfLinks, ComingSoon1Table.CountOfLinks, "The count of links on the " +
                "Coming Soon table 1 do not match");
            Assert.AreEqual(_ComingSoon2Table.CountOfLinks, ComingSoon2Table.CountOfLinks, "The count of links on the " +
                "Coming Soon table 2 do not match");
            Assert.AreEqual(_ComingSoon3Table.CountOfLinks, ComingSoon3Table.CountOfLinks, "The count of links on the " +
                "Coming Soon table 3 do not match");
        }

        [Test, Category("Prod"), Category(siteCodeCategory), Category("CustomPage")]
        [Description("Validating the groupings and order of the custom page")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ComponentOrder()
        {
            if (Help.EnvironmentEquals(Constants.Environments.UAT) || Help.EnvironmentEquals(Constants.Environments.CMEQA))
            {
                Assert.Ignore("Only executing in Production as that is the environment these tests are designed for. Custom page " +
                    "testing was never requested to be executed in lower environments");
            }

            /// 1. Navigate to the custom page and verify the order of the HTML components. 
            ThreeInThirtyPage Page = Navigation_CustomPages.GoToThreeInThirtyPage(Browser);
            Help.CustomPage_VerifyOrderOfHTMLComponents(Browser);
        }

        #endregion tests
    }

    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome, "", "", Platforms.Windows, "", "")]

    [TestFixture]
    public class UAMS_CustomPage_AdultSickleCellPageLinks : TestBase_UAMS
    {
        #region Constructors

        public UAMS_CustomPage_AdultSickleCellPageLinks(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public UAMS_CustomPage_AdultSickleCellPageLinks(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }

        #endregion Constructors

        #region Tests

        // Not executing per Lakshmi: https://code.premierinc.com/issues/browse/LMSPLT-6043
        //[Test, Category("Prod"), Category(siteCodeCategory), Category("CustomPage")]
        [Description("Given I navigate to the Community Adult Sickle Cell page and get the link count and text of all tables, When I navigate " +
  "to the Fireball Adult Sickle Cell page, Then all link counts and text should match. Link text from Community and Fireball will be " +
  "saved as an excel file for manual review, if needed")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void AdultSickleCellPageLinksAppear()
        {
            //****************************NOTE: This test and the cooresponding pages classes will need refactored***********************//
            //****************************once the client updates "Coming Soon" table with real data and real labels********************//


            if (Help.EnvironmentEquals(Constants.Environments.UAT) || Help.EnvironmentEquals(Constants.Environments.CMEQA))
            {
                Assert.Ignore("Only executing in Production as that is the environment these tests are designed for. Custom page " +
                    "testing was never requested to be executed in lower environments");
            }

            /// 1. Navigate to the legacy page and get the count of links and link text of each table
            _AdultSickleCellPage _Page = Navigation_CustomPages.GoToAdultSickleCellLegacyPage(Browser);
            var _ProfessionalEduTable =
                Help.GetLegacyCustomPageTableLinkCountAndText(Browser, _Page.ProfessionalEduTbl, _Page.ProfessionalEduTblHdrLnks);
            var _AdditionalResourcesTable =
                Help.GetLegacyCustomPageTableLinkCountAndText(Browser, _Page.AdditionalResourcesTbl, _Page.AdditionalResourcesTblHdrLnks);
            var _ComingSoonTable =
                Help.GetLegacyCustomPageTableLinkCountAndText(Browser, _Page.ComingSoonTbl, _Page.ComingSoonTblHdrLnks);

            /// 2. Navigate to the new page and get the count of links and link text of each table.
            AdultSickleCellPage Page = Navigation_CustomPages.GoToAdultSickleCellPage(Browser);
            var ProfessionalEduTable =
                Help.GetFireballCustomPageTableLinkCountAndText(Browser, Page.ProfessionalEduTbl);
            var AdditionalResourcesTable =
                Help.GetFireballCustomPageTableLinkCountAndText(Browser, Page.AdditionalResourcesTbl);
            var ComingSoonTable =
                Help.GetFireballCustomPageTableLinkCountAndText(Browser, Page.ComingSoonTbl);

            /// 3. Save the link text from each table to an excel file
            var workSheetNames = new List<string>() { "Professional Edu", "Additional Resources", "Coming Soon" };
            var linksPerTable = new List<(List<string> list1, List<string> list2)>();

            linksPerTable.Add((_ProfessionalEduTable.LinkText, ProfessionalEduTable.LinkText));
            linksPerTable.Add((_AdditionalResourcesTable.LinkText, AdditionalResourcesTable.LinkText));
            linksPerTable.Add((_ComingSoonTable.LinkText, ComingSoonTable.LinkText));

            FileUtils.Excel_StoreResults(Browser, "AdultSickleCellPageLinks", workSheetNames, communityPageWorksheetName,
                fireballPageWorksheetName, linksPerTable);

            /// 4. Assert that counts match for all tables
            Assert.AreEqual(_ProfessionalEduTable.CountOfLinks, ProfessionalEduTable.CountOfLinks, "The count of links on the " +
                "Professioanl Education table do not match");
            Assert.AreEqual(_AdditionalResourcesTable.CountOfLinks, AdditionalResourcesTable.CountOfLinks, "The count of links on the " +
                "Additional Resources table do not match");
            Assert.AreEqual(_ComingSoonTable.CountOfLinks, ComingSoonTable.CountOfLinks, "The count of links on the " +
                 "Coming Soon table do not match");
        }

        // Not executing per Lakshmi: https://code.premierinc.com/issues/browse/LMSPLT-6043
        //[Test, Category("Prod"), Category(siteCodeCategory), Category("CustomPage")]
        [Description("Validating the groupings and order of the custom page")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ComponentOrder()
        {
            if (Help.EnvironmentEquals(Constants.Environments.UAT) || Help.EnvironmentEquals(Constants.Environments.CMEQA))
            {
                Assert.Ignore("Only executing in Production as that is the environment these tests are designed for. Custom page " +
                    "testing was never requested to be executed in lower environments");
            }

            /// 1. Navigate to the custom page and verify the order of the HTML components. 
            AdultSickleCellPage Page = Navigation_CustomPages.GoToAdultSickleCellPage(Browser);
            Help.CustomPage_VerifyOrderOfHTMLComponents(Browser);
        }

        #endregion tests
    }

    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome, "", "", Platforms.Windows, "", "")]

    [TestFixture]
    public class UAMS_CustomPage_AngelsPageLinks : TestBase_UAMS
    {
        #region Constructors

        public UAMS_CustomPage_AngelsPageLinks(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public UAMS_CustomPage_AngelsPageLinks(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }

        #endregion Constructors



        #region Tests

        [Test, Category("Prod"), Category(siteCodeCategory), Category("CustomPage")]
        [Description("Given I navigate to the Community Angels page and get the link count and text of all tables, When I navigate " +
  "to the Fireball Angels page, Then all link counts and text should match. Link text from Community and Fireball will be " +
  "saved as an excel file for manual review, if needed")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void AngelsPageLinksAppear()
        {
            if (Help.EnvironmentEquals(Constants.Environments.UAT) || Help.EnvironmentEquals(Constants.Environments.CMEQA))
            {
                Assert.Ignore("Only executing in Production as that is the environment these tests are designed for. Custom page " +
                    "testing was never requested to be executed in lower environments");
            }

            /// 1. Navigate to the legacy page and get the count of links and link text of each table
            _AngelsPage _Page = Navigation_CustomPages.GoToAngelsLegacyPage(Browser);
            var _ProfessionalEduTable =
                Help.GetLegacyCustomPageTableLinkCountAndText(Browser, _Page.ProfessionalEduTbl, _Page.ProfessionalEduTblHdrLnks);
            var _MyRecentActsTable =
                Help.GetLegacyCustomPageTableLinkCountAndText(Browser, _Page.MyRecentActivitiesTbl, _Page.MyRecentActivitiesTblHdrLnks);
            var _FeatActsTable =
                Help.GetLegacyCustomPageTableLinkCountAndText(Browser, _Page.FeaturedActivitiesTbl, _Page.FeaturedActivitiesTblHdrLnks);

            /// 2. Navigate to the new page and get the count of links and link text of each table.
            AngelsPage Page = Navigation_CustomPages.GoToAngelsPage(Browser);
            var ProfessionalEduTable =
                Help.GetFireballCustomPageTableLinkCountAndText(Browser, Page.ProfessionalEduTbl);
            var MyRecentActsTable =
                Help.GetFireballCustomPageTableLinkCountAndText(Browser, Page.MyRecentActivitiesTbl);
            var FeatActsTable =
                Help.GetFireballCustomPageTableLinkCountAndText(Browser, Page.FeaturedActivitiesTbl);

            /// 3. Save the link text from each table to an excel file
            var workSheetNames = new List<string>() { "Professional Edu", "My Recent Activities", "Featured Activities" };
            var linksPerTable = new List<(List<string> list1, List<string> list2)>();

            linksPerTable.Add((_ProfessionalEduTable.LinkText, ProfessionalEduTable.LinkText));
            linksPerTable.Add((_MyRecentActsTable.LinkText, MyRecentActsTable.LinkText));
            linksPerTable.Add((_FeatActsTable.LinkText, FeatActsTable.LinkText));

            FileUtils.Excel_StoreResults(Browser, "AngelsPageLinks", workSheetNames, communityPageWorksheetName,
                fireballPageWorksheetName, linksPerTable);

            /// 4. Assert that counts match for all tables
            Assert.AreEqual(_ProfessionalEduTable.CountOfLinks, ProfessionalEduTable.CountOfLinks, "The count of links on the " +
                "Professional Education table do not match");
            Assert.AreEqual(_MyRecentActsTable.CountOfLinks, MyRecentActsTable.CountOfLinks, "The count of links on the " +
                "My Recent Activities table do not match");
            Assert.AreEqual(_FeatActsTable.CountOfLinks, FeatActsTable.CountOfLinks, "The count of links on the " +
                "Featured Activities table do not match");
        }

        [Test, Category("Prod"), Category(siteCodeCategory), Category("CustomPage")]
        [Description("Validating the groupings and order of the custom page")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ComponentOrder()
        {
            if (Help.EnvironmentEquals(Constants.Environments.UAT) || Help.EnvironmentEquals(Constants.Environments.CMEQA))
            {
                Assert.Ignore("Only executing in Production as that is the environment these tests are designed for. Custom page " +
                    "testing was never requested to be executed in lower environments");
            }

            /// 1. Navigate to the custom page and verify the order of the HTML components. 
            AngelsPage Page = Navigation_CustomPages.GoToAngelsPage(Browser);
            Help.CustomPage_VerifyOrderOfHTMLComponents(Browser);
        }

        #endregion tests
    }

    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome, "", "", Platforms.Windows, "", "")]

    [TestFixture]
    public class UAMS_CustomPage_ARImpactPageLinks : TestBase_UAMS
    {
        #region Constructors

        public UAMS_CustomPage_ARImpactPageLinks(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public UAMS_CustomPage_ARImpactPageLinks(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }

        #endregion Constructors



        #region Tests

        [Test, Category("Prod"), Category(siteCodeCategory), Category("CustomPage")]
        [Description("Given I navigate to the Community ARImpact page and get the link count and text of all tables, When I navigate " +
    "to the Fireball ARImpact page, Then all link counts and text should match. Link text from Community and Fireball will be " +
    "saved as an excel file for manual review, if needed")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ARImpactPageLinksAppear()
        {
            if (Help.EnvironmentEquals(Constants.Environments.UAT) || Help.EnvironmentEquals(Constants.Environments.CMEQA))
            {
                Assert.Ignore("Only executing in Production as that is the environment these tests are designed for. Custom page " +
                    "testing was never requested to be executed in lower environments");
            }

            /// 1. Navigate to the legacy page and get the count of links and link text of each table
            _ARImpactPage _Page = Navigation_CustomPages.GoToARImpactLegacyPage(Browser);
            var _ProfessionalEduTable =
                Help.GetLegacyCustomPageTableLinkCountAndText(Browser, _Page.ProfessionalEduTbl, _Page.ProfessionalEduTblHdrLnks);
            var _ResourcesTable =
                Help.GetLegacyCustomPageTableLinkCountAndText(Browser, _Page.ResourcesTbl, _Page.ResourcesTblHdrLnks);
            var _ComingSoonTable =
                Help.GetLegacyCustomPageTableLinkCountAndText(Browser, _Page.ComingSoonTbl, _Page.ComingSoonTblHdrLnks);

            /// 2. Navigate to the new page and get the count of links and link text of each table. 
            ARImpactPage Page = Navigation_CustomPages.GoToARImpactPage(Browser);
            var ProfessionalEduTable =
                Help.GetFireballCustomPageTableLinkCountAndText(Browser, Page.ProfessionalEduTbl);
            var ResourcesTable =
                Help.GetFireballCustomPageTableLinkCountAndText(Browser, Page.ResourcesTbl);
            var ComingSoonTable =
                Help.GetFireballCustomPageTableLinkCountAndText(Browser, Page.ComingSoonTbl);

            /// 3. Save the link text from each table to an excel file
            var workSheetNames = new List<string>() { "Professional Edu", "Resources", "Coming Soon" };
            var linksPerTable = new List<(List<string> list1, List<string> list2)>();

            linksPerTable.Add((_ProfessionalEduTable.LinkText, ProfessionalEduTable.LinkText));
            linksPerTable.Add((_ResourcesTable.LinkText, ResourcesTable.LinkText));
            linksPerTable.Add((_ComingSoonTable.LinkText, ComingSoonTable.LinkText));

            FileUtils.Excel_StoreResults(Browser, "ARImpactPageLinks", workSheetNames, communityPageWorksheetName,
                fireballPageWorksheetName, linksPerTable);

            /// 4. Assert that counts match for all tables
            Assert.AreEqual(_ProfessionalEduTable.CountOfLinks, ProfessionalEduTable.CountOfLinks, "The count of links on the " +
                "Professional Education table do not match");
            Assert.AreEqual(_ResourcesTable.CountOfLinks, ResourcesTable.CountOfLinks, "The count of links on the " +
                "Resources table do not match");
            Assert.AreEqual(_ComingSoonTable.CountOfLinks, ComingSoonTable.CountOfLinks, "The count of links on the " +
                 "Coming Soon table do not match");
        }

        [Test, Category("Prod"), Category(siteCodeCategory), Category("CustomPage")]
        [Description("Validating the groupings and order of the custom page")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ComponentOrder()
        {
            if (Help.EnvironmentEquals(Constants.Environments.UAT) || Help.EnvironmentEquals(Constants.Environments.CMEQA))
            {
                Assert.Ignore("Only executing in Production as that is the environment these tests are designed for. Custom page " +
                    "testing was never requested to be executed in lower environments");
            }

            /// 1. Navigate to the custom page and verify the order of the HTML components. 
            ARImpactPage Page = Navigation_CustomPages.GoToARImpactPage(Browser);
            Help.CustomPage_VerifyOrderOfHTMLComponents(Browser);
        }

        #endregion tests
    }

    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome, "", "", Platforms.Windows, "", "")]

    [TestFixture]
    public class UAMS_CustomPage_ARSavesPageLinks : TestBase_UAMS
    {
        #region Constructors

        public UAMS_CustomPage_ARSavesPageLinks(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public UAMS_CustomPage_ARSavesPageLinks(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }

        #endregion Constructors



        #region Tests

        [Test, Category("Prod"), Category(siteCodeCategory), Category("CustomPage")]
        [Description("Given I navigate to the Community ARSaves page and get the link count and text of all tables, When I navigate " +
"to the Fireball ARSaves page, Then all link counts and text should match. Link text from Community and Fireball will be " +
"saved as an excel file for manual review, if needed")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ARSavesPageLinksAppear()
        {
            if (Help.EnvironmentEquals(Constants.Environments.UAT) || Help.EnvironmentEquals(Constants.Environments.CMEQA))
            {
                Assert.Ignore("Only executing in Production as that is the environment these tests are designed for. Custom page " +
                    "testing was never requested to be executed in lower environments");
            }

            /// 1. Navigate to the legacy page and get the count of links and link text of each table
            _ARSavesPage _Page = Navigation_CustomPages.GoToARSavesLegacyPage(Browser);
            var _ProfessionalEduTable =
                Help.GetLegacyCustomPageTableLinkCountAndText(Browser, _Page.ProfessionalEduTbl, _Page.ProfessionalEduTblHdrLnks);
            var _MyRecentActsTable =
                Help.GetLegacyCustomPageTableLinkCountAndText(Browser, _Page.MyRecentActivitiesTbl, _Page.MyRecentActivitiesTblHdrLnks);
            var _FeatActsTable =
                Help.GetLegacyCustomPageTableLinkCountAndText(Browser, _Page.FeaturedActivitiesTbl, _Page.FeaturedActivitiesTblHdrLnks);

            /// 2. Navigate to the new page and get the count of links and link text of each table. 
            ARSavesPage Page = Navigation_CustomPages.GoToARSavesPage(Browser);
            var ProfessionalEduTable =
                Help.GetFireballCustomPageTableLinkCountAndText(Browser, Page.ProfessionalEduTbl);
            var MyRecentActsTable =
                Help.GetFireballCustomPageTableLinkCountAndText(Browser, Page.MyRecentActivitiesTbl);
            var FeatActsTable =
                Help.GetFireballCustomPageTableLinkCountAndText(Browser, Page.FeaturedActivitiesTbl);

            /// 3. Save the link text from each table to an excel file
            var workSheetNames = new List<string>() { "Professional Edu", "My Recent Activities", "Featured Activities" };
            var linksPerTable = new List<(List<string> list1, List<string> list2)>();

            linksPerTable.Add((_ProfessionalEduTable.LinkText, ProfessionalEduTable.LinkText));
            linksPerTable.Add((_MyRecentActsTable.LinkText, MyRecentActsTable.LinkText));
            linksPerTable.Add((_FeatActsTable.LinkText, FeatActsTable.LinkText));

            FileUtils.Excel_StoreResults(Browser, "ARSavesPageLinks", workSheetNames, communityPageWorksheetName,
                fireballPageWorksheetName, linksPerTable);

            /// 4. Assert that counts match for all tables
            Assert.AreEqual(_ProfessionalEduTable.CountOfLinks, ProfessionalEduTable.CountOfLinks, "The count of links on the " +
                "Professional Education table do not match");
            Assert.AreEqual(_MyRecentActsTable.CountOfLinks, MyRecentActsTable.CountOfLinks, "The count of links on the " +
                "My Recent Activities table do not match");
            Assert.AreEqual(_FeatActsTable.CountOfLinks, FeatActsTable.CountOfLinks, "The count of links on the " +
                 "Features Activities table do not match");
        }

        [Test, Category("Prod"), Category(siteCodeCategory), Category("CustomPage")]
        [Description("Validating the groupings and order of the custom page")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ComponentOrder()
        {
            if (Help.EnvironmentEquals(Constants.Environments.UAT) || Help.EnvironmentEquals(Constants.Environments.CMEQA))
            {
                Assert.Ignore("Only executing in Production as that is the environment these tests are designed for. Custom page " +
                    "testing was never requested to be executed in lower environments");
            }

            /// 1. Navigate to the custom page and verify the order of the HTML components. 
            ARSavesPage Page = Navigation_CustomPages.GoToARSavesPage(Browser);
            Help.CustomPage_VerifyOrderOfHTMLComponents(Browser);
        }

        #endregion tests
    }

    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome, "", "", Platforms.Windows, "", "")]

    [TestFixture]
    public class UAMS_CustomPage_CAPPageLinks : TestBase_UAMS
    {
        #region Constructors

        public UAMS_CustomPage_CAPPageLinks(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public UAMS_CustomPage_CAPPageLinks(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }

        #endregion Constructors



        #region Tests

        [Test, Category("Prod"), Category(siteCodeCategory), Category("CustomPage")]
        [Description("Given I navigate to the Community CAP page and get the link count and text of all tables, When I navigate " +
 "to the Fireball CAP page, Then all link counts and text should match. Link text from Community and Fireball will be " +
 "saved as an excel file for manual review, if needed")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void CAPPageLinksAppear()
        {
            if (Help.EnvironmentEquals(Constants.Environments.UAT) || Help.EnvironmentEquals(Constants.Environments.CMEQA))
            {
                Assert.Ignore("Only executing in Production as that is the environment these tests are designed for. Custom page " +
                    "testing was never requested to be executed in lower environments");
            }

            /// 1. Navigate to the legacy page and get the count of links and link text of each table
            _CAPPage _Page = Navigation_CustomPages.GoToCAPLegacyPage(Browser);
            var _UpcomingCapPresTable =
                Help.GetLegacyCustomPageTableLinkCountAndText(Browser, _Page.UpcomingCapPresTbl, _Page.UpcomingCapPresTblHdrLnks);
            var _RegSchedTeleconfTable =
                Help.GetLegacyCustomPageTableLinkCountAndText(Browser, _Page.RegSchedTeleconfTbl, _Page.RegSchedTeleconfTblHdrLnks);
            var _ProfessionalEduTable =
                Help.GetLegacyCustomPageTableLinkCountAndText(Browser, _Page.ProfessionalEduTbl, _Page.ProfessionalEduTblHdrLnks);

            /// 2. Navigate to the new page and get the count of links and link text of each table. 
            CAPPage Page = Navigation_CustomPages.GoToCAPPage(Browser);
            var UpcomingCapPresTable =
                Help.GetFireballCustomPageTableLinkCountAndText(Browser, Page.UpcomingCapPresTbl);
            var RegSchedTeleconfTable =
                Help.GetFireballCustomPageTableLinkCountAndText(Browser, Page.RegSchedTeleconfTbl);
            var ProfessionalEduTable =
                Help.GetFireballCustomPageTableLinkCountAndText(Browser, Page.ProfessionalEduTbl);

            /// 3. Save the link text from each table to an excel file
            var workSheetNames = new List<string>() { "Ucoming CAP Presentations", "Regularly Scheduled Confs", "Professional Edu" };
            var linksPerTable = new List<(List<string> list1, List<string> list2)>();

            linksPerTable.Add((_UpcomingCapPresTable.LinkText, UpcomingCapPresTable.LinkText));
            linksPerTable.Add((_RegSchedTeleconfTable.LinkText, RegSchedTeleconfTable.LinkText));
            linksPerTable.Add((_ProfessionalEduTable.LinkText, ProfessionalEduTable.LinkText));

            FileUtils.Excel_StoreResults(Browser, "CAPPageLinks", workSheetNames, communityPageWorksheetName,
                fireballPageWorksheetName, linksPerTable);

            /// 4. Assert that counts match for all tables
            Assert.AreEqual(_UpcomingCapPresTable.CountOfLinks, UpcomingCapPresTable.CountOfLinks, "The count of links on the " +
                "Upcoming Cap Presentations table do not match");
            Assert.AreEqual(_RegSchedTeleconfTable.CountOfLinks, RegSchedTeleconfTable.CountOfLinks, "The count of links on the " +
                "Regularly Scheduled Teleconference table do not match");
            Assert.AreEqual(_ProfessionalEduTable.CountOfLinks, ProfessionalEduTable.CountOfLinks, "The count of links on the " +
                "Professional Education table do not match");
        }

        [Test, Category("Prod"), Category(siteCodeCategory), Category("CustomPage")]
        [Description("Validating the groupings and order of the custom page")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ComponentOrder()
        {
            if (Help.EnvironmentEquals(Constants.Environments.UAT) || Help.EnvironmentEquals(Constants.Environments.CMEQA))
            {
                Assert.Ignore("Only executing in Production as that is the environment these tests are designed for. Custom page " +
                    "testing was never requested to be executed in lower environments");
            }

            /// 1. Navigate to the custom page and verify the order of the HTML components. 
            CAPPage Page = Navigation_CustomPages.GoToCAPPage(Browser);
            Help.CustomPage_VerifyOrderOfHTMLComponents(Browser);
        }

        #endregion tests
    }

    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome, "", "", Platforms.Windows, "", "")]

    [TestFixture]
    public class UAMS_CustomPage_SCTRCPageLinks : TestBase_UAMS
    {
        #region Constructors

        public UAMS_CustomPage_SCTRCPageLinks(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public UAMS_CustomPage_SCTRCPageLinks(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }

        #endregion Constructors



        #region Tests

        [Test, Category("Prod"), Category(siteCodeCategory), Category("CustomPage")]
        [Description("Given I navigate to the Community SCTRC page and get the link count and text of all tables, When I navigate " +
  "to the Fireball SCTRC page, Then all link counts and text should match. Link text from Community and Fireball will be " +
  "saved as an excel file for manual review, if needed")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void SCTRCPageLinksAppear()
        {
            if (Help.EnvironmentEquals(Constants.Environments.UAT) || Help.EnvironmentEquals(Constants.Environments.CMEQA))
            {
                Assert.Ignore("Only executing in Production as that is the environment these tests are designed for. Custom page " +
                    "testing was never requested to be executed in lower environments");
            }

            /// 1. Navigate to the legacy page and get the count of links and link text of each table
            _SCTRCPage _Page = Navigation_CustomPages.GoToSCTRCLegacyPage(Browser);
            var _ProfessionalEduTable =
                Help.GetLegacyCustomPageTableLinkCountAndText(Browser, _Page.ProfessionalEduTbl, _Page.ProfessionalEduTblHdrLnks);
            var _MyRecentActsTable =
                Help.GetLegacyCustomPageTableLinkCountAndText(Browser, _Page.MyRecentActivitiesTbl, _Page.MyRecentActivitiesTblHdrLnks);
            var _FeatActsTable =
                Help.GetLegacyCustomPageTableLinkCountAndText(Browser, _Page.FeaturedActivitiesTbl, _Page.FeaturedActivitiesTblHdrLnks);

            /// 2. Navigate to the new page and get the count of links and link text of each table.
            SCTRCPage Page = Navigation_CustomPages.GoToSCTRCPage(Browser);
            var ProfessionalEduTable =
                Help.GetFireballCustomPageTableLinkCountAndText(Browser, Page.ProfessionalEduTbl);
            var MyRecentActsTable =
                Help.GetFireballCustomPageTableLinkCountAndText(Browser, Page.MyRecentActivitiesTbl);
            var FeatActsTable =
                Help.GetFireballCustomPageTableLinkCountAndText(Browser, Page.FeaturedActivitiesTbl);

            /// 3. Save the link text from each table to an excel file
            var workSheetNames = new List<string>() { "Professional Edu", "My Recent Activities", "Featured Activities" };
            var linksPerTable = new List<(List<string> list1, List<string> list2)>();

            linksPerTable.Add((_ProfessionalEduTable.LinkText, ProfessionalEduTable.LinkText));
            linksPerTable.Add((_MyRecentActsTable.LinkText, MyRecentActsTable.LinkText));
            linksPerTable.Add((_FeatActsTable.LinkText, FeatActsTable.LinkText));

            FileUtils.Excel_StoreResults(Browser, "SCTRCPageLinks", workSheetNames, communityPageWorksheetName,
                fireballPageWorksheetName, linksPerTable);

            /// 4. Assert that counts match for all tables
            Assert.AreEqual(_ProfessionalEduTable.CountOfLinks, ProfessionalEduTable.CountOfLinks, "The count of links on the " +
                "Professional Education table do not match");
            Assert.AreEqual(_MyRecentActsTable.CountOfLinks, MyRecentActsTable.CountOfLinks, "The count of links on the " +
                "My Recent Activities table do not match");
            Assert.AreEqual(_FeatActsTable.CountOfLinks, FeatActsTable.CountOfLinks, "The count of links on the " +
                   "Features Activities table do not match");
        }

        [Test, Category("Prod"), Category(siteCodeCategory), Category("CustomPage")]
        [Description("Validating the groupings and order of the custom page")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ComponentOrder()
        {
            if (Help.EnvironmentEquals(Constants.Environments.UAT) || Help.EnvironmentEquals(Constants.Environments.CMEQA))
            {
                Assert.Ignore("Only executing in Production as that is the environment these tests are designed for. Custom page " +
                    "testing was never requested to be executed in lower environments");
            }

            /// 1. Navigate to the custom page and verify the order of the HTML components. 
            SCTRCPage Page = Navigation_CustomPages.GoToSCTRCPage(Browser);
            Help.CustomPage_VerifyOrderOfHTMLComponents(Browser);
        }

        #endregion tests
    }

    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome, "", "", Platforms.Windows, "", "")]

    [TestFixture]
    public class UAMS_CustomPage_PCGAPageLinks : TestBase_UAMS
    {
        #region Constructors

        public UAMS_CustomPage_PCGAPageLinks(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public UAMS_CustomPage_PCGAPageLinks(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }

        #endregion Constructors



        #region Tests

        [Test, Category("Prod"), Category(siteCodeCategory), Category("CustomPage")]
        [Description("Given I navigate to the Community PCGA page and get the link count and text of all tables, When I navigate " +
  "to the Fireball PCGA page, Then all link counts and text should match. Link text from Community and Fireball will be " +
  "saved as an excel file for manual review, if needed")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void PCGAPageLinksAppear()
        {
            if (Help.EnvironmentEquals(Constants.Environments.UAT) || Help.EnvironmentEquals(Constants.Environments.CMEQA))
            {
                Assert.Ignore("Only executing in Production as that is the environment these tests are designed for. Custom page " +
                    "testing was never requested to be executed in lower environments");
            }

            /// 1. Navigate to the legacy page and get the count of links and link text of each table
            _PCGAPage _Page = Navigation_CustomPages.GoToPCGALegacyPage(Browser);
            var _ProfessionalEduTable =
                Help.GetLegacyCustomPageTableLinkCountAndText(Browser, _Page.ProfessionalEduTbl, _Page.ProfessionalEduTblHdrLnks);
            var _ProfessionalEdu2Table =
                Help.GetLegacyCustomPageTableLinkCountAndText(Browser, _Page.ProfessionalEducation2Tbl, _Page.ProfessionalEducation2TblHdrLnks);
            var _ProfessionalEdu3Table =
                Help.GetLegacyCustomPageTableLinkCountAndText(Browser, _Page.ProfessionalEducation3Tbl, _Page.ProfessionalEducation3TblHdrLnks);

            /// 2. Navigate to the new page and get the count of links and link text of each table. 
            PCGAPage Page = Navigation_CustomPages.GoToPCGAPage(Browser);
            var ProfessionalEduTable =
                Help.GetFireballCustomPageTableLinkCountAndText(Browser, Page.ProfessionalEduTbl);
            var ProfessionalEdu2Table =
                Help.GetFireballCustomPageTableLinkCountAndText(Browser, Page.ProfessionalEdu2Tbl);
            var ProfessionalEdu3Table =
                Help.GetFireballCustomPageTableLinkCountAndText(Browser, Page.ProfessionalEdu3Tbl);

            /// 3. Save the link text from each table to an excel file
            var workSheetNames = new List<string>() { "Professional Edu 1", "Professional Edu 2", "Professional Edu 3" };
            var linksPerTable = new List<(List<string> list1, List<string> list2)>();

            linksPerTable.Add((_ProfessionalEduTable.LinkText, ProfessionalEduTable.LinkText));
            linksPerTable.Add((_ProfessionalEdu2Table.LinkText, ProfessionalEdu2Table.LinkText));
            linksPerTable.Add((_ProfessionalEdu3Table.LinkText, ProfessionalEdu3Table.LinkText));

            FileUtils.Excel_StoreResults(Browser, "PCGAPageLinks", workSheetNames, communityPageWorksheetName,
                fireballPageWorksheetName, linksPerTable);

            /// 4. Assert that counts match for all tables
            Assert.AreEqual(_ProfessionalEduTable.CountOfLinks, ProfessionalEduTable.CountOfLinks, "The count of links on the " +
                "Professional Education 1 table do not match");
            Assert.AreEqual(_ProfessionalEdu2Table.CountOfLinks, ProfessionalEdu2Table.CountOfLinks, "The count of links on the " +
                "Professional Education 2 table do not match");
            Assert.AreEqual(_ProfessionalEdu3Table.CountOfLinks, ProfessionalEdu3Table.CountOfLinks, "The count of links on the " +
                 "Professional Education 3 table do not match");
        }

        [Test, Category("Prod"), Category(siteCodeCategory), Category("CustomPage")]
        [Description("Validating the groupings and order of the custom page")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ComponentOrder()
        {
            if (Help.EnvironmentEquals(Constants.Environments.UAT) || Help.EnvironmentEquals(Constants.Environments.CMEQA))
            {
                Assert.Ignore("Only executing in Production as that is the environment these tests are designed for. Custom page " +
                    "testing was never requested to be executed in lower environments");
            }

            /// 1. Navigate to the custom page and verify the order of the HTML components. 
            PCGAPage Page = Navigation_CustomPages.GoToPCGAPage(Browser);
            Help.CustomPage_VerifyOrderOfHTMLComponents(Browser);
        }

        #endregion tests
    }

    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome, "", "", Platforms.Windows, "", "")]

    [TestFixture]
    public class UAMS_CustomPage_CDHPageLinks : TestBase_UAMS
    {
        #region Constructors

        public UAMS_CustomPage_CDHPageLinks(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public UAMS_CustomPage_CDHPageLinks(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }

        #endregion Constructors



        #region Tests

        [Test, Category("Prod"), Category(siteCodeCategory), Category("CustomPage")]
        [Description("Given I navigate to the Community CDH page and get the link count and text of all tables, When I navigate " +
  "to the Fireball CDH page, Then all link counts and text should match. Link text from Community and Fireball will be " +
  "saved as an excel file for manual review, if needed")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void CDHPageLinksAppear()
        {
            //****************************NOTE: This test and the cooresponding pages classes will need refactored***********************//
            //****************************once the client updates "Coming Soon" table with real data and real labels********************//


            if (Help.EnvironmentEquals(Constants.Environments.UAT) || Help.EnvironmentEquals(Constants.Environments.CMEQA))
            {
                Assert.Ignore("Only executing in Production as that is the environment these tests are designed for. Custom page " +
                    "testing was never requested to be executed in lower environments");
            }

            /// 1. Navigate to the legacy page and get the count of links and link text of each table
            _CDHPage _Page = Navigation_CustomPages.GoToCDHLegacyPage(Browser);
            var _ProfessionalEduTable =
                Help.GetLegacyCustomPageTableLinkCountAndText(Browser, _Page.ProfessionalEduTbl, _Page.ProfessionalEduTblHdrLnks);
            var _AdditionalResourcesTable =
                Help.GetLegacyCustomPageTableLinkCountAndText(Browser, _Page.AdditionalResourcesTbl, _Page.AdditionalResourcesTblHdrLnks);
            var _ComingSoonTable =
                Help.GetLegacyCustomPageTableLinkCountAndText(Browser, _Page.ComingSoonTbl, _Page.ComingSoonTblHdrLnks);

            /// 2. Navigate to the new page and get the count of links and link text of each table.
            CDHPage Page = Navigation_CustomPages.GoToCDHPage(Browser);
            var ProfessionalEduTable =
                Help.GetFireballCustomPageTableLinkCountAndText(Browser, Page.ProfessionalEduTbl);
            var AdditionalResourcesTable =
                Help.GetFireballCustomPageTableLinkCountAndText(Browser, Page.AdditionalResourcesTbl);
            var ComingSoonTable =
                Help.GetFireballCustomPageTableLinkCountAndText(Browser, Page.ComingSoonTbl);

            /// 3. Save the link text from each table to an excel file
            var workSheetNames = new List<string>() { "Professional Edu", "Addtional Resources", "Coming Soon" };
            var linksPerTable = new List<(List<string> list1, List<string> list2)>();

            linksPerTable.Add((_ProfessionalEduTable.LinkText, ProfessionalEduTable.LinkText));
            linksPerTable.Add((_AdditionalResourcesTable.LinkText, AdditionalResourcesTable.LinkText));
            linksPerTable.Add((_ComingSoonTable.LinkText, ComingSoonTable.LinkText));

            FileUtils.Excel_StoreResults(Browser, "CDHPageLinks", workSheetNames, communityPageWorksheetName,
                fireballPageWorksheetName, linksPerTable);

            /// 4. Assert that counts match for all tables
            Assert.AreEqual(_ProfessionalEduTable.CountOfLinks, ProfessionalEduTable.CountOfLinks, "The count of links on the " +
                "Professioanl Education table do not match");
            Assert.AreEqual(_AdditionalResourcesTable.CountOfLinks, AdditionalResourcesTable.CountOfLinks, "The count of links on the " +
                "Additional Resources table do not match");
            Assert.AreEqual(_ComingSoonTable.CountOfLinks, ComingSoonTable.CountOfLinks, "The count of links on the " +
                 "Coming Soon table do not match");
        }

        [Test, Category("Prod"), Category(siteCodeCategory), Category("CustomPage")]
        [Description("Validating the groupings and order of the custom page")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ComponentOrder()
        {
            if (Help.EnvironmentEquals(Constants.Environments.UAT) || Help.EnvironmentEquals(Constants.Environments.CMEQA))
            {
                Assert.Ignore("Only executing in Production as that is the environment these tests are designed for. Custom page " +
                    "testing was never requested to be executed in lower environments");
            }

            /// 1. Navigate to the custom page and verify the order of the HTML components. 
            CDHPage Page = Navigation_CustomPages.GoToCDHPage(Browser);
            Help.CustomPage_VerifyOrderOfHTMLComponents(Browser);
        }

        #endregion tests
    }

    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome, "", "", Platforms.Windows, "", "")]

    [TestFixture]
    public class UAMS_CustomPage_PRIPageLinks : TestBase_UAMS
    {
        #region Constructors

        public UAMS_CustomPage_PRIPageLinks(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public UAMS_CustomPage_PRIPageLinks(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }

        #endregion Constructors



        #region Tests

        [Test, Category("Prod"), Category(siteCodeCategory), Category("CustomPage")]
        [Description("Given I navigate to the Community PRI page and get the link count and text of all tables, When I navigate " +
"to the Fireball PRI page, Then all link counts and text should match. Link text from Community and Fireball will be " +
"saved as an excel file for manual review, if needed")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void PRIPageLinksAppear()
        {
            if (Help.EnvironmentEquals(Constants.Environments.UAT) || Help.EnvironmentEquals(Constants.Environments.CMEQA))
            {
                Assert.Ignore("Only executing in Production as that is the environment these tests are designed for. Custom page " +
                    "testing was never requested to be executed in lower environments");
            }

            /// 1. Navigate to the legacy page and get the count of links and link text of each table
            _PRIPage _Page = Navigation_CustomPages.GoToPRILegacyPage(Browser);
            var _ProfessionalEduTable =
                Help.GetLegacyCustomPageTableLinkCountAndText(Browser, _Page.ProfessionalEduTbl, _Page.ProfessionalEduTblHdrLnks);
            var _MyRecentActsTable =
                Help.GetLegacyCustomPageTableLinkCountAndText(Browser, _Page.MyRecentActivitiesTbl, _Page.MyRecentActivitiesTblHdrLnks);
            var _FeatActsTable =
                Help.GetLegacyCustomPageTableLinkCountAndText(Browser, _Page.FeaturedActivitiesTbl, _Page.FeaturedActivitiesTblHdrLnks);

            /// 2. Navigate to the new page and get the count of links and link text of each table. 
            PRIPage Page = Navigation_CustomPages.GoToPRIPage(Browser);
            var ProfessionalEduTable =
                Help.GetFireballCustomPageTableLinkCountAndText(Browser, Page.ProfessionalEduTbl);
            var MyRecentActsTable =
                Help.GetFireballCustomPageTableLinkCountAndText(Browser, Page.MyRecentActivitiesTbl);
            var FeatActsTable =
                Help.GetFireballCustomPageTableLinkCountAndText(Browser, Page.FeaturedActivitiesTbl);

            /// 3. Save the link text from each table to an excel file
            var workSheetNames = new List<string>() { "Professional Edu", "My Recent Activities", "Featured Activities" };
            var linksPerTable = new List<(List<string> list1, List<string> list2)>();

            linksPerTable.Add((_ProfessionalEduTable.LinkText, ProfessionalEduTable.LinkText));
            linksPerTable.Add((_MyRecentActsTable.LinkText, MyRecentActsTable.LinkText));
            linksPerTable.Add((_FeatActsTable.LinkText, FeatActsTable.LinkText));

            FileUtils.Excel_StoreResults(Browser, "PRIPageLinks", workSheetNames, communityPageWorksheetName,
                fireballPageWorksheetName, linksPerTable);

            /// 4. Assert that counts match for all tables. Not comparing link text right now because they are 
            /// sorting differently. Count comparison is sufficient
            Assert.AreEqual(_ProfessionalEduTable.CountOfLinks, ProfessionalEduTable.CountOfLinks, "The count of links on the " +
                "Professional Education table do not match");
            Assert.AreEqual(_MyRecentActsTable.CountOfLinks, MyRecentActsTable.CountOfLinks, "The count of links on the " +
                "My Recent Activities table do not match");
            Assert.AreEqual(_FeatActsTable.CountOfLinks, FeatActsTable.CountOfLinks, "The count of links on the " +
                 "Features Activities table do not match");
        }

        [Test, Category("Prod"), Category(siteCodeCategory), Category("CustomPage")]
        [Description("Validating the groupings and order of the custom page")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ComponentOrder()
        {
            if (Help.EnvironmentEquals(Constants.Environments.UAT) || Help.EnvironmentEquals(Constants.Environments.CMEQA))
            {
                Assert.Ignore("Only executing in Production as that is the environment these tests are designed for. Custom page " +
                    "testing was never requested to be executed in lower environments");
            }

            /// 1. Navigate to the custom page and verify the order of the HTML components. 
            PRIPage Page = Navigation_CustomPages.GoToPRIPage(Browser);
            Help.CustomPage_VerifyOrderOfHTMLComponents(Browser);
        }

        #endregion tests
    }

    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome, "", "", Platforms.Windows, "", "")]

    [TestFixture]
    public class UAMS_CustomPage_TraqPageLinks : TestBase_UAMS
    {
        #region Constructors

        public UAMS_CustomPage_TraqPageLinks(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public UAMS_CustomPage_TraqPageLinks(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }

        #endregion Constructors



        #region Tests

        [Test, Category("Prod"), Category(siteCodeCategory), Category("CustomPage")]
        [Description("Given I navigate to the Community Traq page and get the link count and text of all tables, When I navigate " +
 "to the Fireball Traq page, Then all link counts and text should match. Link text from Community and Fireball will be " +
 "saved as an excel file for manual review, if needed")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void TraqPageLinksAppear()
        {
            if (Help.EnvironmentEquals(Constants.Environments.UAT) || Help.EnvironmentEquals(Constants.Environments.CMEQA))
            {
                Assert.Ignore("Only executing in Production as that is the environment these tests are designed for. Custom page " +
                    "testing was never requested to be executed in lower environments");
            }

            /// 1. Navigate to the legacy page and get the count of links and link text of each table
            _TraqPage _Page = Navigation_CustomPages.GoToTraqLegacyPage(Browser);
            var _RecordedPresentationsTblTable =
                Help.GetLegacyCustomPageTableLinkCountAndText(Browser, _Page.RecordedPresentationsTbl, _Page.RecordedPresentationsTblHdrLnks);
            var _RegSchedTeleconfTable =
                Help.GetLegacyCustomPageTableLinkCountAndText(Browser, _Page.RegSchedTeleconfTbl, _Page.RegSchedTeleconfTblHdrLnks);
            var _ComingSoonTable =
                Help.GetLegacyCustomPageTableLinkCountAndText(Browser, _Page.ComingSoonTbl, _Page.ComingSoonTblHdrLnks);

            /// 2. Navigate to the new page and get the count of links and link text of each table. 
            TraqPage Page = Navigation_CustomPages.GoToTraqPage(Browser);
            var RecordedPresentationsTblTable =
                Help.GetFireballCustomPageTableLinkCountAndText(Browser, Page.RecordedPresentationsTbl);
            var RegSchedTeleconfTable =
                Help.GetFireballCustomPageTableLinkCountAndText(Browser, Page.RegSchedTeleconfTbl);
            var ComingSoonTable =
                Help.GetFireballCustomPageTableLinkCountAndText(Browser, Page.ComingSoonTbl);

            /// 3. Save the link text from each table to an excel file
            var workSheetNames = new List<string>() { "Recorded Presentations", "Regularly Scheduled Confs", "Coming Soon" };
            var linksPerTable = new List<(List<string> list1, List<string> list2)>();

            linksPerTable.Add((_RecordedPresentationsTblTable.LinkText, RecordedPresentationsTblTable.LinkText));
            linksPerTable.Add((_RegSchedTeleconfTable.LinkText, RegSchedTeleconfTable.LinkText));
            linksPerTable.Add((_ComingSoonTable.LinkText, ComingSoonTable.LinkText));

            FileUtils.Excel_StoreResults(Browser, "TRAQPageLinks", workSheetNames, communityPageWorksheetName,
                fireballPageWorksheetName, linksPerTable);

            /// 4. Assert that counts match for all tables
            Assert.AreEqual(_RecordedPresentationsTblTable.CountOfLinks, RecordedPresentationsTblTable.CountOfLinks, "The count of links on the " +
                "Recorded Presentations table do not match");
            Assert.AreEqual(_RegSchedTeleconfTable.CountOfLinks, RegSchedTeleconfTable.CountOfLinks, "The count of links on the " +
                "Regularly Scheduled Teleconference table do not match");
            Assert.AreEqual(_ComingSoonTable.CountOfLinks, ComingSoonTable.CountOfLinks, "The count of links on the " +
                "Coming Soon table do not match");
        }

        [Test, Category("Prod"), Category(siteCodeCategory), Category("CustomPage")]
        [Description("Validating the groupings and order of the custom page")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ComponentOrder()
        {
            if (Help.EnvironmentEquals(Constants.Environments.UAT) || Help.EnvironmentEquals(Constants.Environments.CMEQA))
            {
                Assert.Ignore("Only executing in Production as that is the environment these tests are designed for. Custom page " +
                    "testing was never requested to be executed in lower environments");
            }

            /// 1. Navigate to the custom page and verify the order of the HTML components. 
            TraqPage Page = Navigation_CustomPages.GoToTraqPage(Browser);
            Help.CustomPage_VerifyOrderOfHTMLComponents(Browser);
        }

        #endregion tests
    }

    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome, "", "", Platforms.Windows, "", "")]

    [TestFixture]
    public class UAMS_CustomPage_TriumphPageLinks : TestBase_UAMS
    {
        #region Constructors

        public UAMS_CustomPage_TriumphPageLinks(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public UAMS_CustomPage_TriumphPageLinks(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }

        #endregion Constructors



        #region Tests

        [Test, Category("Prod"), Category(siteCodeCategory), Category("CustomPage")]
        [Description("Given I navigate to the Community Triumph page and get the link count and text of all tables, When I navigate " +
  "to the Fireball Triumph page, Then all link counts and text should match. Link text from Community and Fireball will be " +
  "saved as an excel file for manual review, if needed")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void TriumphPageLinksAppear()
        {
            if (Help.EnvironmentEquals(Constants.Environments.UAT) || Help.EnvironmentEquals(Constants.Environments.CMEQA))
            {
                Assert.Ignore("Only executing in Production as that is the environment these tests are designed for. Custom page " +
                    "testing was never requested to be executed in lower environments");
            }
            
            /// 1. Navigate to the legacy page and get the count of links and link text of each table
            _TriumphPage _Page = Navigation_CustomPages.GoToTriumphLegacyPage(Browser);
            var _ProfessionalEduTable =
                Help.GetLegacyCustomPageTableLinkCountAndText(Browser, _Page.ProfessionalEduTbl, _Page.ProfessionalEduTblHdrLnks);
            var _MyRecentActsTable =
                Help.GetLegacyCustomPageTableLinkCountAndText(Browser, _Page.MyRecentActivitiesTbl, _Page.MyRecentActivitiesTblHdrLnks);
            var _FeatActsTable =
                Help.GetLegacyCustomPageTableLinkCountAndText(Browser, _Page.FeaturedActivitiesTbl, _Page.FeaturedActivitiesTblHdrLnks);

            /// 2. Navigate to the new page and get the count of links and link text of each table. 
            TriumphPage Page = Navigation_CustomPages.GoToTriumphPage(Browser);
            var ProfessionalEduTable =
                Help.GetFireballCustomPageTableLinkCountAndText(Browser, Page.ProfessionalEduTbl);
            var MyRecentActsTable =
                Help.GetFireballCustomPageTableLinkCountAndText(Browser, Page.MyRecentActivitiesTbl);
            var FeatActsTable =
                Help.GetFireballCustomPageTableLinkCountAndText(Browser, Page.FeaturedActivitiesTbl);

            /// 3. Save the link text from each table to an excel file
            var workSheetNames = new List<string>() { "Professional Edu", "My Recent Activities", "Featured Activities" };
            var linksPerTable = new List<(List<string> list1, List<string> list2)>();

            linksPerTable.Add((_ProfessionalEduTable.LinkText, ProfessionalEduTable.LinkText));
            linksPerTable.Add((_MyRecentActsTable.LinkText, MyRecentActsTable.LinkText));
            linksPerTable.Add((_FeatActsTable.LinkText, FeatActsTable.LinkText));

            FileUtils.Excel_StoreResults(Browser, "PRIPageLinks", workSheetNames, communityPageWorksheetName,
                fireballPageWorksheetName, linksPerTable);

            /// 4. Assert that counts match for all tables. Not comparing link text right now because they are 
            /// sorting differently. Count comparison is sufficient
            Assert.AreEqual(_ProfessionalEduTable.CountOfLinks, ProfessionalEduTable.CountOfLinks, "The count of links on the " +
                "Professional Education table do not match");
            Assert.AreEqual(_MyRecentActsTable.CountOfLinks, MyRecentActsTable.CountOfLinks, "The count of links on the " +
                "My Recent Activities table do not match");
            Assert.AreEqual(_FeatActsTable.CountOfLinks, FeatActsTable.CountOfLinks, "The count of links on the " +
                 "Features Activities table do not match");
        }

        [Test, Category("Prod"), Category(siteCodeCategory), Category("CustomPage")]
        [Description("Validating the groupings and order of the custom page")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ComponentOrder()
        {
            if (Help.EnvironmentEquals(Constants.Environments.UAT) || Help.EnvironmentEquals(Constants.Environments.CMEQA))
            {
                Assert.Ignore("Only executing in Production as that is the environment these tests are designed for. Custom page " +
                    "testing was never requested to be executed in lower environments");
            }

            /// 1. Navigate to the custom page and verify the order of the HTML components. 
            TriumphPage Page = Navigation_CustomPages.GoToTriumphPage(Browser);
            Help.CustomPage_VerifyOrderOfHTMLComponents(Browser);
        }

        #endregion tests
    }

    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome, "", "", Platforms.Windows, "", "")]

    [TestFixture]
    public class UAMS_CustomPage_HRSAPageLinks : TestBase_UAMS
    {
        #region Constructors

        public UAMS_CustomPage_HRSAPageLinks(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public UAMS_CustomPage_HRSAPageLinks(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }

        #endregion Constructors



        #region Tests

        [Test, Category("Prod"), Category(siteCodeCategory), Category("CustomPage")]
        [Description("Given I navigate to the Community HRSA page and get the link count and text of all tables, When I navigate " +
"to the Fireball HRSA page, Then all link counts and text should match. Link text from Community and Fireball will be " +
"saved as an excel file for manual review, if needed")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void HRSAPageLinksAppear()
        {
            if (Help.EnvironmentEquals(Constants.Environments.UAT) || Help.EnvironmentEquals(Constants.Environments.CMEQA))
            {
                Assert.Ignore("Only executing in Production as that is the environment these tests are designed for. Custom page " +
                    "testing was never requested to be executed in lower environments");
            }

            /// 1. Navigate to the legacy page and get the count of links and link text of each table
            _HRSAPage _Page = Navigation_CustomPages.GoToHRSALegacyPage(Browser);
            var _GeriatricKnowledgeTable =
                Help.GetLegacyCustomPageTableLinkCountAndText(Browser, _Page.GeriatricKnowledgeTbl, _Page.GeriatricKnowledgeTblHdrLnks);
            var _InterprofessionalEducationTable =
                Help.GetLegacyCustomPageTableLinkCountAndText(Browser, _Page.InterprofessionalEducationTbl, _Page.InterprofessionalEducationTblHdrLnks);
            var _EducationCompetenciesTable =
                Help.GetLegacyCustomPageTableLinkCountAndText(Browser, _Page.EducationCompetenciesTbl, _Page.EducationCompetenciesTblHdrLnks);

            /// 2. Navigate to the new page and get the count of links and link text of each table.
            HRSAPage Page = Navigation_CustomPages.GoToHRSAPage(Browser);
            var GeriatricKnowledgeTable =
                Help.GetFireballCustomPageTableLinkCountAndText(Browser, Page.GeriatricKnowledgeTbl);
            var InterprofessionalEducationTable =
                Help.GetFireballCustomPageTableLinkCountAndText(Browser, Page.InterprofessionalEducationTbl);
            var EducationCompetenciesTable =
                Help.GetFireballCustomPageTableLinkCountAndText(Browser, Page.EducationCompetenciesTbl);

            /// 3. Save the link text from each table to an excel file
            var workSheetNames = new List<string>() { "Professional Edu", "My Recent Activities", "Featured Activities" };
            var linksPerTable = new List<(List<string> list1, List<string> list2)>();

            linksPerTable.Add((_GeriatricKnowledgeTable.LinkText, GeriatricKnowledgeTable.LinkText));
            linksPerTable.Add((_InterprofessionalEducationTable.LinkText, InterprofessionalEducationTable.LinkText));
            linksPerTable.Add((_EducationCompetenciesTable.LinkText, EducationCompetenciesTable.LinkText));

            FileUtils.Excel_StoreResults(Browser, "HRSAPageLinks", workSheetNames, communityPageWorksheetName,
                fireballPageWorksheetName, linksPerTable);

            /// 4. Assert that counts match for all tables
            Assert.AreEqual(_GeriatricKnowledgeTable.CountOfLinks, GeriatricKnowledgeTable.CountOfLinks, "The count of links on the " +
                "Geriatric Knowledge table do not match");

            Assert.AreEqual(_InterprofessionalEducationTable.CountOfLinks, InterprofessionalEducationTable.CountOfLinks, "The count of links on the " +
                "Interprofessional Education table do not match");

            Assert.AreEqual(_EducationCompetenciesTable.CountOfLinks, EducationCompetenciesTable.CountOfLinks, "The count of links on the " +
                 "Education Competencies table do not match");            
        }

        [Test, Category("Prod"), Category(siteCodeCategory), Category("CustomPage")]
        [Description("Validating the groupings and order of the custom page")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ComponentOrder()
        {
            if (Help.EnvironmentEquals(Constants.Environments.UAT) || Help.EnvironmentEquals(Constants.Environments.CMEQA))
            {
                Assert.Ignore("Only executing in Production as that is the environment these tests are designed for. Custom page " +
                    "testing was never requested to be executed in lower environments");
            }

            /// 1. Navigate to the custom page and verify the order of the HTML components. 
            HRSAPage Page = Navigation_CustomPages.GoToHRSAPage(Browser);
            Help.CustomPage_VerifyOrderOfHTMLComponents(Browser);    
        }

        #endregion tests
    }



}










































