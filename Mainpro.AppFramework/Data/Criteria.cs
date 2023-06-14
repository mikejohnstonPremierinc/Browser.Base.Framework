using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mainpro.AppFramework
{
    /// <summary>
    /// Provides access to all the known "Criteria" for the application. Criteria are typically used when waiting
    /// for elements. I often wait until some "Criteria" is met before continuing with a test.
    /// </summary>
    public static class Criteria
    {
        public static readonly LoginPageCriteria LoginPage = new LoginPageCriteria();
        public static readonly ReportsPageCriteria ReportsPage = new ReportsPageCriteria();
        public static readonly DashboardPageCriteria DashboardPage = new DashboardPageCriteria();
        public static readonly EnterACPDActivityPageCriteria EnterACPDActivityPage = new EnterACPDActivityPageCriteria();
        public static readonly EnterACPDActivityDetailsPageCriteria EnterACPDActivityDetailsPage = new EnterACPDActivityDetailsPageCriteria();
        public static readonly CreditSummaryPageCriteria CreditSummaryPage = new CreditSummaryPageCriteria();
        public static readonly HoldingAreaPageCriteria HoldingAreaPage = new HoldingAreaPageCriteria();
        public static readonly CACCreditSummaryPageCriteria CACCreditSummaryPage = new CACCreditSummaryPageCriteria();
        public static readonly CACCPDActivitiesListCriteria CACCPDActivitiesList = new CACCPDActivitiesListCriteria();
        public static readonly CACHoldingAreaPageCritieria CACHoldingAreaPage = new CACHoldingAreaPageCritieria();
        public static readonly CACReportsPageCriteria CACReportsPage = new CACReportsPageCriteria();
        public static readonly CPDActivitiesListPageCriteria CPDActivitiesListPage = new CPDActivitiesListPageCriteria();
        public static readonly CPDPlanningPageCriteria CPDPlanningPage = new CPDPlanningPageCriteria();
        public static readonly PLPHubPageCriteria PLPHubPage = new PLPHubPageCriteria();

        public static readonly EntryCarouselPathwayPageCriteria EntryCarouselPathwayPage = new EntryCarouselPathwayPageCriteria();
        public static readonly Step1PageCriteria Step1Page = new Step1PageCriteria();
        public static readonly Step2PageCriteria Step2Page = new Step2PageCriteria();
        public static readonly Step3PageCriteria Step3Page = new Step3PageCriteria();
        public static readonly Step4PageCriteria Step4Page = new Step4PageCriteria();
        public static readonly Step5PageCriteria Step5Page = new Step5PageCriteria();
        public static readonly StepPRPageCriteria StepPRPage = new StepPRPageCriteria();
      
    }
}
