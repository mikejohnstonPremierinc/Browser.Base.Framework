namespace LMS.AppFramework
{
    /// <summary>
    /// Provides access to all the known "Criteria" for the application. Criteria are typically used when waiting
    /// for elements. I often wait until some "Criteria" is met before continuing with a test.
    /// </summary>
    public static class Criteria
    {
        public static readonly HomePageCriteria HomePage = new HomePageCriteria();
        public static readonly ActPreviewPageCriteria ActPreviewPage = new ActPreviewPageCriteria();
        public static readonly ActOverviewPageCriteria ActOverviewPage = new ActOverviewPageCriteria();
        public static readonly LoginPageCriteria LoginPage = new LoginPageCriteria();
        public static readonly ActivitiesInProgressPageCriteria ActivitiesInProgressPage = new ActivitiesInProgressPageCriteria(); 
        public static readonly TranscriptPageCriteria TranscriptPage = new TranscriptPageCriteria();
        public static readonly RegistrationPageCriteria RegistrationPage = new RegistrationPageCriteria();
        public static readonly ActMaterialPageCriteria ActivityMaterial = new ActMaterialPageCriteria(); 
        public static readonly ActCertificatePageCriteria ActCertificatePage = new ActCertificatePageCriteria();
        public static readonly ActAssessmentPageCriteria ActAssessmentPage = new ActAssessmentPageCriteria();
        public static readonly SearchPageCriteria SearchPage = new SearchPageCriteria();
        public static readonly ActRegistrationPageCriteria ActRegistrationPage = new ActRegistrationPageCriteria();
        public static readonly ProfilePageCriteria ProfilePage = new ProfilePageCriteria();
        public static readonly ActBundlePageCriteria ActBundlePage = new ActBundlePageCriteria();
        public static readonly ActOnHoldPageCriteria ActOnHoldPage = new ActOnHoldPageCriteria();
        public static readonly ActPaymentPageCriteria ActPaymentPage = new ActPaymentPageCriteria();
        public static readonly ActCPEMonitorPageCriteria ActCPEMonitorPage = new ActCPEMonitorPageCriteria();
        public static readonly ActOrderDetailsPageCriteria ActOrderDetailsPage = new ActOrderDetailsPageCriteria();
        public static readonly ActOrderReceiptPageCriteria ActOrderReceiptPage = new ActOrderReceiptPageCriteria();
        public static readonly ActeCommercePageCriteria ActeCommercePage = new ActeCommercePageCriteria();
        public static readonly ActSessionsPageCriteria ActSessionsPage = new ActSessionsPageCriteria();
        public static readonly ActPIMPageCriteria ActPIMPage = new ActPIMPageCriteria();
        public static readonly AllReceiptsPageCriteria AllReceiptsPage = new AllReceiptsPageCriteria();
        public static readonly ActClaimCreditPageCriteria ActClaimCreditPage = new ActClaimCreditPageCriteria();
        public static readonly GenericCustomPageCriteria GenericCustomPage = new GenericCustomPageCriteria();
        
        // UAMS custom pages (Fireball)
        public static readonly AngelsLivePageCriteria AngelsLivePage = new AngelsLivePageCriteria();
        public static readonly BreastFeedingPageCriteria BreastFeedingPage = new BreastFeedingPageCriteria();
        public static readonly ThreeInThirtyPageCriteria ThreeInThirtyPage = new ThreeInThirtyPageCriteria();
        public static readonly CERequestPageCriteria CERequestPage = new CERequestPageCriteria();
        public static readonly AdultSickleCellPageCriteria AdultSickleCellPage = new AdultSickleCellPageCriteria();
        public static readonly AngelsPageCriteria AngelsPage = new AngelsPageCriteria();
        public static readonly ARImpactPageCriteria ARImpactPage = new ARImpactPageCriteria();
        public static readonly ARSavesPageCriteria ARSavesPage = new ARSavesPageCriteria();
        public static readonly CAPPageCriteria CAPPage = new CAPPageCriteria();
        public static readonly SCTRCPageCriteria SCTRCPage = new SCTRCPageCriteria();
        public static readonly PCGAPageCriteria PCGAPage = new PCGAPageCriteria();
        public static readonly TeleconferencePageCriteria TeleconferencePage = new TeleconferencePageCriteria();
        public static readonly CDHPageCriteria CDHPage = new CDHPageCriteria();
        public static readonly PRIPageCriteria PRIPage = new PRIPageCriteria();
        public static readonly TraqPageCriteria TraqPage = new TraqPageCriteria();
        public static readonly TriumphPageCriteria TriumphPage = new TriumphPageCriteria();
        public static readonly HRSAPageCriteria HRSAPage = new HRSAPageCriteria();
        // UAMS custom pages (Community)
        public static readonly _AngelsLivePageCriteria _AngelsLivePage = new _AngelsLivePageCriteria();
        public static readonly _BreastFeedingPageCriteria _BreastFeedingPage = new _BreastFeedingPageCriteria();
        public static readonly _ThreeInThirtyPageCriteria _ThreeInThirtyPage = new _ThreeInThirtyPageCriteria();
        public static readonly _CERequestPageCriteria _CERequestPage = new _CERequestPageCriteria();
        public static readonly _AdultSickleCellPageCriteria _Page = new _AdultSickleCellPageCriteria();
        public static readonly _AdultSickleCellPageCriteria _AdultSickleCellPage = new _AdultSickleCellPageCriteria();
        public static readonly _AngelsPageCriteria _AngelsPage = new _AngelsPageCriteria();
        public static readonly _ARImpactPageCriteria _ARImpactPage = new _ARImpactPageCriteria();
        public static readonly _ARSavesPageCriteria _ARSavesPage = new _ARSavesPageCriteria();
        public static readonly _CAPPageCriteria _CAPPage = new _CAPPageCriteria();
        public static readonly _SCTRCPageCriteria _SCTRCPage = new _SCTRCPageCriteria();
        public static readonly _PCGAPageCriteria _PCGAPage = new _PCGAPageCriteria();
        public static readonly _TeleconferencePageCriteria _TeleconferencePage = new _TeleconferencePageCriteria();
        public static readonly _CDHPageCriteria _CDHPage = new _CDHPageCriteria();
        public static readonly _PRIPageCriteria _PRIPage = new _PRIPageCriteria();
        public static readonly _TraqPageCriteria _TraqPage = new _TraqPageCriteria();
        public static readonly _TriumphPageCriteria _TriumphPage = new _TriumphPageCriteria();
        public static readonly _HRSAPageCriteria _HRSAPage = new _HRSAPageCriteria();




    }
}