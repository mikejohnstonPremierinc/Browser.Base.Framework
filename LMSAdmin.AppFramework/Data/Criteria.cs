namespace LMSAdmin.AppFramework
{
    /// <summary>
    /// Provides access to all the known "Criteria" for the the application.
    /// Criteria are typically used when waiting for elements. I often wait until some
    /// "Criteria" is met before continuing with a test.
    /// </summary>
    public static class Criteria
    {
        public static readonly LoginPageCriteria LoginPage = new LoginPageCriteria();
        public static readonly MyDashboardPageCriteria MyDashboardPage = new MyDashboardPageCriteria();
        public static readonly ProjectsPageCriteria ProjectsPage = new ProjectsPageCriteria();
        public static readonly Projects_ManagePageCriteria Projects_ManagePage = new Projects_ManagePageCriteria();
        
        public static readonly Projects_AddEditPageCriteria Projects_AddEditPage = new Projects_AddEditPageCriteria();

        public static readonly ActMainPageCriteria ActMainPage = new ActMainPageCriteria();
        public static readonly ActAccreditationPageCriteria ActAccreditationPage = new ActAccreditationPageCriteria();
        public static readonly ActCompletionPathwayPageCriteria ActCompletionPathwayPage = new ActCompletionPathwayPageCriteria();        
        public static readonly Legacy_ActAccreditationPageCriteria Legacy_ActAccreditationPage = new Legacy_ActAccreditationPageCriteria();
        public static readonly ActAssessmentsPageCriteria ActAssessmentsPage = new ActAssessmentsPageCriteria();
        public static readonly ActAssessmentDetailsPageCriteria ActAssessmentDetailsPage = new ActAssessmentDetailsPageCriteria();
        public static readonly ActFrontMatterPageCriteria ActFrontMatterPage = new ActFrontMatterPageCriteria();
        public static readonly Legacy_ActAwardsPageCriteria Legacy_ActAwardsPage = new Legacy_ActAwardsPageCriteria();
        public static readonly ActAwardsPageCriteria ActAwardsPage = new ActAwardsPageCriteria();
        public static readonly ActContentPageCriteria ActContentPage = new ActContentPageCriteria();


        public static readonly DistributionPageCriteria DistributionPage = new DistributionPageCriteria();
        public static readonly SearchResultsPageCriteria SearchResultsPage = new SearchResultsPageCriteria();
        public static readonly Distribution_PortalsPageCriteria Distribution_PortalsPage = new Distribution_PortalsPageCriteria();
        public static readonly Distribution_CatalogsPageCriteria Distribution_CatalogsPage = new Distribution_CatalogsPageCriteria();

        public static readonly Legacy_SetupPageCriteria Legacy_SetupPage = new Legacy_SetupPageCriteria();

    }
}