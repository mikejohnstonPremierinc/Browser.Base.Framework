namespace Mainpro.AppFramework
{
    /// <summary>
    /// Provides access to all known "Bys" for the application. Bys are used to locate elements
    /// </summary>
    public static class Bys
    {
        /// <summary>
        /// Locators to find elements on the skeleton of the Mainpro page. i.e. The menu items, header items and footer items.
        /// </summary>
        public static readonly MainproPageBys MainproPage = new MainproPageBys();

        /// <summary>
        /// Locators to find elements on the login page
        /// </summary>
        public static readonly LoginPageBys LoginPage = new LoginPageBys();
        public static readonly DashboardPageBys DashboardPage = new DashboardPageBys();
        public static readonly EnterACPDActivityDetailsPageBys EnterACPDActivityDetailsPage = new EnterACPDActivityDetailsPageBys();
        public static readonly EnterACPDActivityPageBys EnterACPDActivityPage = new EnterACPDActivityPageBys();
        public static readonly CreditSummaryPageBys CreditSummaryPage = new CreditSummaryPageBys();
        public static readonly HoldingAreaPageBys HoldingAreaPage = new HoldingAreaPageBys();
        public static readonly CPDActivitiesListPageBys CPDActivitiesListPage = new CPDActivitiesListPageBys();
        public static readonly ReportsPageBys ReportsPage = new ReportsPageBys();
        public static readonly CPDPlanningPageBys CPDPlanningPage= new CPDPlanningPageBys();
        public static readonly PLPHubPageBys PLPHubPage = new PLPHubPageBys();
        public static readonly CACCPDActivitiesListBys CACCPDActivitiesListPage = new CACCPDActivitiesListBys();
        public static readonly CACCreditSummaryPageBys CACCreditSummaryPage = new CACCreditSummaryPageBys();
        public static readonly CACHoldingAreaPageBys CACHoldingAreaPage = new CACHoldingAreaPageBys();
        public static readonly CACReportsPageBys CACReportsPage = new CACReportsPageBys();
               
        public static readonly EntryCarouselPathwayPageBys EntryCarouselPathwayPage = new EntryCarouselPathwayPageBys();
        public static readonly Step1PageBys Step1Page = new Step1PageBys();
        public static readonly Step2PageBys Step2Page = new Step2PageBys();
        public static readonly Step3PageBys Step3Page = new Step3PageBys();
        public static readonly Step4PageBys Step4Page = new Step4PageBys();
        public static readonly Step5PageBys Step5Page = new Step5PageBys();
        public static readonly StepPRPageBys StepPRPage = new StepPRPageBys();
        

        
    }
}
