namespace LS.AppFramework
{
    /// <summary>
    /// Provides access to all the known "Criteria" for the the application.
    /// Criteria are typically used when waiting for elements. I often wait until some
    /// "Criteria" is met before continuing with a test.
    /// </summary>
    public static class Criteria
    {
        public static readonly LoginPageCriteria LoginPage = new LoginPageCriteria();
        public static readonly ParticipantsPageCriteria ParticipantsPage = new ParticipantsPageCriteria();
        public static readonly SearchPageCriteria SearchPage = new SearchPageCriteria();
        public static readonly ProgramPageCriteria ProgramPage = new ProgramPageCriteria();
        public static readonly SitePageCriteria SitePage = new SitePageCriteria();
        public static readonly ActivityUploadPageCriteria ActivityUploadPage = new ActivityUploadPageCriteria();



    }
}