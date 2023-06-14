using Browser.Core.Framework;
using OpenQA.Selenium;
using System;
using System.Configuration;
using System.Threading;

namespace LMS.AppFramework
{
    /// <summary>
    /// Responsible for basic page navigation and specific-page initialization.
    /// </summary>
	public static class Navigation_CustomPages
	{
        private static T Navigate<T>(Func<IWebDriver, T> createPage, IWebDriver driver, bool waitForInitialize) where T : Browser.Core.Framework.Page
        {
            var page = createPage(driver);
            page.GoToPage(waitForInitialize);
            return page;
        }

        public static GenericCustomPage GoToGenericCustomPage(this IWebDriver driver, bool waitForInitialize = true, bool prod = true)
        {
            var page = Navigate(p => new GenericCustomPage(p), driver, waitForInitialize);
            return new GenericCustomPage(driver);
        }

        public static BreastFeedingPage GoToBreastFeedingPage(this IWebDriver driver, bool waitForInitialize = true, bool prod = true)
        {
            var page = Navigate(p => new BreastFeedingPage(p), driver, waitForInitialize);
            return new BreastFeedingPage(driver);
        }

        public static CERequestPage GoToCERequestPage(this IWebDriver driver, bool waitForInitialize = true, bool prod = true)
        {
            var page = Navigate(p => new CERequestPage(p), driver, waitForInitialize);
            return new CERequestPage(driver);
        }

        public static TeleconferencePage GoToTeleconferencePage(this IWebDriver driver, bool waitForInitialize = true, bool prod = true)
        {
            var page = Navigate(p => new TeleconferencePage(p), driver, waitForInitialize);
            return new TeleconferencePage(driver);
        }

        public static AngelsLivePage GoToAngelsLivePage(this IWebDriver driver, bool waitForInitialize = true, bool prod = true)
        {
            var page = Navigate(p => new AngelsLivePage(p), driver, waitForInitialize);
            return new AngelsLivePage(driver);
        }

        public static ThreeInThirtyPage GoToThreeInThirtyPage(this IWebDriver driver, bool waitForInitialize = true, bool prod = true)
        {
            var page = Navigate(p => new ThreeInThirtyPage(p), driver, waitForInitialize);
            return new ThreeInThirtyPage(driver);
        }

        public static AdultSickleCellPage GoToAdultSickleCellPage(this IWebDriver driver, bool waitForInitialize = true, bool prod = true)
        {
            var page = Navigate(p => new AdultSickleCellPage(p), driver, waitForInitialize);
            return new AdultSickleCellPage(driver);
        }

        public static AngelsPage GoToAngelsPage(this IWebDriver driver, bool waitForInitialize = true, bool prod = true)
        {
            var page = Navigate(p => new AngelsPage(p), driver, waitForInitialize);
            return new AngelsPage(driver);
        }
        public static ARImpactPage GoToARImpactPage(this IWebDriver driver, bool waitForInitialize = true, bool prod = true)
        {
            var page = Navigate(p => new ARImpactPage(p), driver, waitForInitialize);
            return new ARImpactPage(driver);
        }

        public static ARSavesPage GoToARSavesPage(this IWebDriver driver, bool waitForInitialize = true, bool prod = true)
        {
            var page = Navigate(p => new ARSavesPage(p), driver, waitForInitialize);
            return new ARSavesPage(driver);
        }

        public static HRSAPage GoToHRSAPage(this IWebDriver driver, bool waitForInitialize = true, bool prod = true)
        {
            var page = Navigate(p => new HRSAPage(p), driver, waitForInitialize);
            return new HRSAPage(driver);
        }

        public static CAPPage GoToCAPPage(this IWebDriver driver, bool waitForInitialize = true, bool prod = true)
        {
            var page = Navigate(p => new CAPPage(p), driver, waitForInitialize);
            return new CAPPage(driver);
        }

        public static SCTRCPage GoToSCTRCPage(this IWebDriver driver, bool waitForInitialize = true, bool prod = true)
        {
            var page = Navigate(p => new SCTRCPage(p), driver, waitForInitialize);
            return new SCTRCPage(driver);
        }

        public static PCGAPage GoToPCGAPage(this IWebDriver driver, bool waitForInitialize = true, bool prod = true)
        {
            var page = Navigate(p => new PCGAPage(p), driver, waitForInitialize);
            return new PCGAPage(driver);
        }

        public static CDHPage GoToCDHPage(this IWebDriver driver, bool waitForInitialize = true, bool prod = true)
        {
            var page = Navigate(p => new CDHPage(p), driver, waitForInitialize);
            return new CDHPage(driver);
        }

        public static PRIPage GoToPRIPage(this IWebDriver driver, bool waitForInitialize = true, bool prod = true)
        {
            var page = Navigate(p => new PRIPage(p), driver, waitForInitialize);
            return new PRIPage(driver);
        }

        public static TraqPage GoToTraqPage(this IWebDriver driver, bool waitForInitialize = true, bool prod = true)
        {
            var page = Navigate(p => new TraqPage(p), driver, waitForInitialize);
            return new TraqPage(driver);
        }

        public static TriumphPage GoToTriumphPage(this IWebDriver driver, bool waitForInitialize = true, bool prod = true)
        {
            var page = Navigate(p => new TriumphPage(p), driver, waitForInitialize);
            return new TriumphPage(driver);
        }

        public static _BreastFeedingPage GoToBreastFeedingLegacyPage(this IWebDriver driver, bool waitForInitialize = true, bool prod = true)
        {
            var page = Navigate(p => new _BreastFeedingPage(p), driver, waitForInitialize);
            return new _BreastFeedingPage(driver);
        }

        public static _CERequestPage GoToCERequestLegacyPage(this IWebDriver driver, bool waitForInitialize = true, bool prod = true)
        {
            var page = Navigate(p => new _CERequestPage(p), driver, waitForInitialize);
            return new _CERequestPage(driver);
        }

        public static _TeleconferencePage GoToTeleconferenceLegacyPage(this IWebDriver driver, bool waitForInitialize = true, bool prod = true)
        {
            var page = Navigate(p => new _TeleconferencePage(p), driver, waitForInitialize);
            return new _TeleconferencePage(driver);
        }

        public static _AngelsLivePage GoToAngelsLiveLegacyPage(this IWebDriver driver, bool waitForInitialize = true, bool prod = true)
        {
            var page = Navigate(p => new _AngelsLivePage(p), driver, waitForInitialize);
            return new _AngelsLivePage(driver);
        }
        public static _ThreeInThirtyPage GoToThreeInThirtyLegacyPage(this IWebDriver driver, bool waitForInitialize = true, bool prod = true)
        {
            var page = Navigate(p => new _ThreeInThirtyPage(p), driver, waitForInitialize);
            return new _ThreeInThirtyPage(driver);
        }

        public static _AdultSickleCellPage GoToAdultSickleCellLegacyPage(this IWebDriver driver, bool waitForInitialize = true, bool prod = true)
        {
            var page = Navigate(p => new _AdultSickleCellPage(p), driver, waitForInitialize);
            return new _AdultSickleCellPage(driver);
        }

        public static _AngelsPage GoToAngelsLegacyPage(this IWebDriver driver, bool waitForInitialize = true, bool prod = true)
        {
            var page = Navigate(p => new _AngelsPage(p), driver, waitForInitialize);
            return new _AngelsPage(driver);
        }

        public static _ARImpactPage GoToARImpactLegacyPage(this IWebDriver driver, bool waitForInitialize = true, bool prod = true)
        {
            var page = Navigate(p => new _ARImpactPage(p), driver, waitForInitialize);
            return new _ARImpactPage(driver);
        }

        public static _ARSavesPage GoToARSavesLegacyPage(this IWebDriver driver, bool waitForInitialize = true, bool prod = true)
        {
            var page = Navigate(p => new _ARSavesPage(p), driver, waitForInitialize);
            return new _ARSavesPage(driver);
        }

        public static _HRSAPage GoToHRSALegacyPage(this IWebDriver driver, bool waitForInitialize = true, bool prod = true)
        {
            var page = Navigate(p => new _HRSAPage(p), driver, waitForInitialize);
            return new _HRSAPage(driver);
        }

        public static _CAPPage GoToCAPLegacyPage(this IWebDriver driver, bool waitForInitialize = true, bool prod = true)
        {
            var page = Navigate(p => new _CAPPage(p), driver, waitForInitialize);
            return new _CAPPage(driver);
        }

        public static _SCTRCPage GoToSCTRCLegacyPage(this IWebDriver driver, bool waitForInitialize = true, bool prod = true)
        {
            var page = Navigate(p => new _SCTRCPage(p), driver, waitForInitialize);
            return new _SCTRCPage(driver);
        }

        public static _PCGAPage GoToPCGALegacyPage(this IWebDriver driver, bool waitForInitialize = true, bool prod = true)
        {
            var page = Navigate(p => new _PCGAPage(p), driver, waitForInitialize);
            return new _PCGAPage(driver);
        }

        public static _CDHPage GoToCDHLegacyPage(this IWebDriver driver, bool waitForInitialize = true, bool prod = true)
        {
            var page = Navigate(p => new _CDHPage(p), driver, waitForInitialize);
            return new _CDHPage(driver);
        }

        public static _PRIPage GoToPRILegacyPage(this IWebDriver driver, bool waitForInitialize = true, bool prod = true)
        {
            var page = Navigate(p => new _PRIPage(p), driver, waitForInitialize);
            return new _PRIPage(driver);
        }

        public static _TraqPage GoToTraqLegacyPage(this IWebDriver driver, bool waitForInitialize = true, bool prod = true)
        {
            var page = Navigate(p => new _TraqPage(p), driver, waitForInitialize);
            return new _TraqPage(driver);
        }

        public static _TriumphPage GoToTriumphLegacyPage(this IWebDriver driver, bool waitForInitialize = true, bool prod = true)
        {
            var page = Navigate(p => new _TriumphPage(p), driver, waitForInitialize);
            return new _TriumphPage(driver);
        }


    }
}
