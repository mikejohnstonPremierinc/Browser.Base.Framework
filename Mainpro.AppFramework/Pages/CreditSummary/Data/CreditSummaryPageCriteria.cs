using Browser.Core.Framework;


namespace Mainpro.AppFramework
{
    public class CreditSummaryPageCriteria
    {
        public readonly ICriteria<CreditSummaryPage> AnnualRequirementsTblFirstRowVisible = new Criteria<CreditSummaryPage>(p =>
        {
            return p.Exists(Bys.CreditSummaryPage.AnnualRequirementsTblBodyRow, ElementCriteria.IsVisible);

        }, "Annual Requirements table first row visible");

        public readonly ICriteria<CreditSummaryPage> GroupLearningTblVisible= new Criteria<CreditSummaryPage>(p =>
        {
            return p.Exists(Bys.CreditSummaryPage.GroupLearningTbl, ElementCriteria.IsVisible);

        }, "GroupLearning Table visible");

        public readonly ICriteria<CreditSummaryPage> LoadIconNotExists = new Criteria<CreditSummaryPage>(p =>
        {
            return !p.Exists(Bys.MainproPage.LoadIcon);

        }, "Load icon not exists");

        public readonly ICriteria<CreditSummaryPage> LoadIconOverlayNotExists = new Criteria<CreditSummaryPage>(p =>
        {
            return !p.Exists(Bys.MainproPage.LoadIconOverlay);

        }, "Load overlay not exists");

        public readonly ICriteria<CreditSummaryPage> WereSorryErrorLblNotExists = new Criteria<CreditSummaryPage>(p =>
        {
            return !p.Exists(Bys.MainproPage.WereSorryErrorLbl);

        }, "We're sorry, we cannot retrieve your information label not exists");


        public readonly ICriteria<CreditSummaryPage> PageReady;

        public CreditSummaryPageCriteria()
        {
            PageReady = GroupLearningTblVisible.AND(LoadIconNotExists).
                AND(LoadIconOverlayNotExists).AND(WereSorryErrorLblNotExists);
        }
    }
}
