using Browser.Core.Framework;

namespace Mainpro.AppFramework
{
    public class ReportsPageCriteria
    {
        public readonly ICriteria<ReportsPage> MyCreditSummaryRunReportBtnVisible = new Criteria<ReportsPage>(p =>
        {
            return p.Exists(Bys.ReportsPage.MyCreditSummaryRunReportBtn, ElementCriteria.IsEnabled);

        }, "My Credit Summary Run Report utton visible");

        public readonly ICriteria<ReportsPage> MyCreditSummaryFormXBtnNotvisible = new Criteria<ReportsPage>(p =>
        {
            return p.Exists(Bys.ReportsPage.MyCreditSummaryFormXBtn, ElementCriteria.IsNotVisible);

        }, "My Credit Summary X button not visible");

        public readonly ICriteria<ReportsPage> MyTranscriptOfCPDActsFormXBtnNotvisible = new Criteria<ReportsPage>(p =>
        {
            return p.Exists(Bys.ReportsPage.MyTranscriptOfCPDActsFormXBtn, ElementCriteria.IsNotVisible);

        }, "My Transcript of CPD Activities X button not visible");

        public readonly ICriteria<ReportsPage> MyMainproCycleCompleteionCertFormXBtnNotvisible = new Criteria<ReportsPage>(p =>
        {
            return p.Exists(Bys.ReportsPage.MyMainproCycleCompleteionCertFormXBtn, ElementCriteria.IsNotVisible);

        }, "My Mainpro Cycle Completion Certificate X button not visible");

        public readonly ICriteria<ReportsPage> LoadIconNotExists = new Criteria<ReportsPage>(p =>
        {
            return !p.Exists(Bys.MainproPage.LoadIcon);

        }, "Load icon not exists");

        public readonly ICriteria<ReportsPage> LoadIconOverlayNotExists = new Criteria<ReportsPage>(p =>
        {
            return !p.Exists(Bys.MainproPage.LoadIconOverlay);

        }, "Load overlay not exists");

        public readonly ICriteria<ReportsPage> WereSorryErrorLblNotExists = new Criteria<ReportsPage>(p =>
        {
            return !p.Exists(Bys.MainproPage.WereSorryErrorLbl);

        }, "We're sorry, we cannot retrieve your information label not exists");

        public readonly ICriteria<ReportsPage> PageReady;

        public ReportsPageCriteria()
        {
            PageReady = MyCreditSummaryRunReportBtnVisible.AND(LoadIconNotExists).
                AND(LoadIconOverlayNotExists).AND(WereSorryErrorLblNotExists);
        }
    }
}
