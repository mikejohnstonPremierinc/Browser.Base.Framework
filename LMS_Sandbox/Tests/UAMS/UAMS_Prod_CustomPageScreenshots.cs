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

namespace UAMS.UITest
{
    // Local
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    // Remote
    [RemoteSeleniumTestFixture(BrowserNames.Chrome, "", "", Platforms.Windows, "", "")]

    [TestFixture]
    public class UAMS_Prod_CustomPageScreenshots : TestBase_UAMS
    {
        #region Constructors

        public UAMS_Prod_CustomPageScreenshots(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public UAMS_Prod_CustomPageScreenshots(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }

        #endregion Constructors

        #region Tests

        [Test, Category("Prod"), Category(siteCodeCategory), Category("CustomPage")]
        [Description("Given I navigate to all custom pages on a Production environment, When the pages finish loading, Then the elements should " +
        "appear without error. Screenshots will also be saved for manual review")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void CustomPageScreenshots()
        {
            if (Help.EnvironmentEquals(Constants.Environments.UAT) || Help.EnvironmentEquals(Constants.Environments.CMEQA))
            {
                Assert.Ignore("Only executing in Production as that is the environment these tests are designed for. Custom page " +
                    "testing was never requested to be executed in lower environments");
            }

            /// 1. Navigate to every custom page and save screenshots for further review
            Navigation_CustomPages.GoToAngelsLegacyPage(Browser);
            Browser.TakeScreenshot("AngelsPage_Legacy");
            Navigation_CustomPages.GoToAngelsPage(Browser);
            Browser.TakeScreenshot("AngelsPage_Fireball");

            Navigation_CustomPages.GoToAngelsLiveLegacyPage(Browser);
            Browser.TakeScreenshot("AngelsLivePage_Legacy");
            Navigation_CustomPages.GoToAngelsLivePage(Browser);
            Browser.TakeScreenshot("AngelsLivePage_Fireball");

            Navigation_CustomPages.GoToARImpactLegacyPage(Browser);
            Browser.TakeScreenshot("ARImpactPage_Legacy");
            Navigation_CustomPages.GoToARImpactPage(Browser);
            Browser.TakeScreenshot("ARImpactPage_Fireball");

            Navigation_CustomPages.GoToARSavesLegacyPage(Browser);
            Browser.TakeScreenshot("ARSavesPage_Legacy");
            Navigation_CustomPages.GoToARSavesPage(Browser);
            Browser.TakeScreenshot("ARSavesPage_Fireball");

            Navigation_CustomPages.GoToAdultSickleCellLegacyPage(Browser);
            Browser.TakeScreenshot("AdultSickleCellPage_Legacy");
            Navigation_CustomPages.GoToAdultSickleCellPage(Browser);
            Browser.TakeScreenshot("AdultSickleCellPage_Fireball");

            Navigation_CustomPages.GoToBreastFeedingLegacyPage(Browser);
            Browser.TakeScreenshot("BreastFeedingPage_Legacy");
            Navigation_CustomPages.GoToBreastFeedingPage(Browser);
            Browser.TakeScreenshot("BreastFeedingPage_Fireball");

            Navigation_CustomPages.GoToCAPLegacyPage(Browser);
            Browser.TakeScreenshot("CAPPage_Legacy");
            Navigation_CustomPages.GoToCAPPage(Browser);
            Browser.TakeScreenshot("CAPPage_Fireball");

            Navigation_CustomPages.GoToCDHLegacyPage(Browser);
            Browser.TakeScreenshot("CDHPage_Legacy");
            Navigation_CustomPages.GoToCDHPage(Browser);
            Browser.TakeScreenshot("CDHPage_Fireball");

            Navigation_CustomPages.GoToCERequestLegacyPage(Browser);
            Browser.TakeScreenshot("CERequestsPage_Legacy");
            Navigation_CustomPages.GoToCERequestPage(Browser);
            Browser.TakeScreenshot("CERequestsPage_Fireball");

            Navigation_CustomPages.GoToHRSALegacyPage(Browser);
            Browser.TakeScreenshot("HRSAPage_Legacy");
            Navigation_CustomPages.GoToHRSAPage(Browser);
            Browser.TakeScreenshot("HRSAPage_Fireball");

            Navigation_CustomPages.GoToPCGALegacyPage(Browser);
            Browser.TakeScreenshot("PCGAPage_Legacy");
            Navigation_CustomPages.GoToPCGAPage(Browser);
            Browser.TakeScreenshot("PCGAPage_Fireball");

            Navigation_CustomPages.GoToPRILegacyPage(Browser);
            Browser.TakeScreenshot("PRIPage_Legacy");
            Navigation_CustomPages.GoToPRIPage(Browser);
            Browser.TakeScreenshot("PRIPage_Fireball");

            Navigation_CustomPages.GoToSCTRCLegacyPage(Browser);
            Browser.TakeScreenshot("SCTRCPage_Legacy");
            Navigation_CustomPages.GoToSCTRCPage(Browser);
            Browser.TakeScreenshot("SCTRCPage_Fireball");

            Navigation_CustomPages.GoToTeleconferencePage(Browser);
            Browser.TakeScreenshot("TeleconferencePage_Fireball");

            Navigation_CustomPages.GoToThreeInThirtyLegacyPage(Browser);
            Browser.TakeScreenshot("ThreeInThirtyPage_Legacy");
            Navigation_CustomPages.GoToThreeInThirtyPage(Browser);
            Browser.TakeScreenshot("ThreeInThirtyPage_Fireball");

            Navigation_CustomPages.GoToTraqLegacyPage(Browser);
            Browser.TakeScreenshot("TraqPage_Legacy");
            Navigation_CustomPages.GoToTraqPage(Browser);
            Browser.TakeScreenshot("TraqPage_Fireball");

            Navigation_CustomPages.GoToTriumphLegacyPage(Browser);
            Browser.TakeScreenshot("TriumphPage_Legacy");
            Navigation_CustomPages.GoToTriumphPage(Browser);
            Browser.TakeScreenshot("TriumphPage_Fireball");
        }

        #endregion tests
    }
}






