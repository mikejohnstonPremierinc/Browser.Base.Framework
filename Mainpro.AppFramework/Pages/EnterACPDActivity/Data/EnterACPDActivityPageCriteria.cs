using Browser.Core.Framework;


namespace Mainpro.AppFramework
{
    public class EnterACPDActivityPageCriteria
    {
        public readonly ICriteria<EnterACPDActivityPage> CategorySelElemBtnEnabledAndVisible = new Criteria<EnterACPDActivityPage>(p =>
        {
            return p.Exists(Bys.EnterACPDActivityPage.CategorySelElemBtn, ElementCriteria.IsEnabled , ElementCriteria.IsVisible);

        }, "Category Select Element enabled and visible");

        public readonly ICriteria<EnterACPDActivityPage> SearchResultsTblProgActTitleColHdrEnabled = new Criteria<EnterACPDActivityPage>(p =>
        {
            return p.Exists(Bys.EnterACPDActivityPage.SearchResultsTblProgActTitleColHdr, ElementCriteria.IsEnabled);

        }, "Program Activity Title Text Box enabled");

        public readonly ICriteria<EnterACPDActivityPage> SearchBtnVisible = new Criteria<EnterACPDActivityPage>(p =>
        {
            return p.Exists(Bys.EnterACPDActivityPage.SearchBtn, ElementCriteria.IsVisible);

        }, "Search Button visible");

        public readonly ICriteria<EnterACPDActivityPage> SearchResultsTblFirstRowVisible = new Criteria<EnterACPDActivityPage>(p =>
        {
            return p.Exists(Bys.EnterACPDActivityPage.SearchResultsTblFirstRow, ElementCriteria.IsVisible);

        }, "Search Results Table first row visible");

        public readonly ICriteria<EnterACPDActivityPage> SearchResultsTblTooManyResultsLblVisible = new Criteria<EnterACPDActivityPage>(p =>
        {
            return p.Exists(Bys.EnterACPDActivityPage.SearchResultsTblTooManyResultsLbl, ElementCriteria.IsVisible);

        }, "Search Results Table, Too Many Results label visible");

        public readonly ICriteria<EnterACPDActivityPage> SearchResultsTblNoResultsLblVisible = new Criteria<EnterACPDActivityPage>(p =>
        {
            return p.Exists(Bys.EnterACPDActivityPage.SearchResultsTblNoResultsLbl, ElementCriteria.IsVisible);

        }, "Search Results Table, No Results label visible");

        public readonly ICriteria<EnterACPDActivityPage> ContinueBtnExists = new Criteria<EnterACPDActivityPage>(p =>
        {
            return p.Exists(Bys.EnterACPDActivityPage.ContinueBtn);

        }, "Continue button exists");

        public readonly ICriteria<EnterACPDActivityPage> MaxCreditReachedFormClaimedLblVisible = new Criteria<EnterACPDActivityPage>(p =>
        {
            return p.Exists(Bys.EnterACPDActivityPage.MaxCreditReachedFormClaimedLbl);

        }, "Max Credit form, Credits Claimed label visible");

        public readonly ICriteria<EnterACPDActivityPage> LoadIconNotExists = new Criteria<EnterACPDActivityPage>(p =>
        {
            return !p.Exists(Bys.MainproPage.LoadIcon);

        }, "Load icon not exists");

        public readonly ICriteria<EnterACPDActivityPage> LoadIconOverlayNotExists = new Criteria<EnterACPDActivityPage>(p =>
        {
            return !p.Exists(Bys.MainproPage.LoadIconOverlay);

        }, "Load overlay not exists");

        public readonly ICriteria<EnterACPDActivityPage> WereSorryErrorLblNotExists = new Criteria<EnterACPDActivityPage>(p =>
        {
            return !p.Exists(Bys.MainproPage.WereSorryErrorLbl);

        }, "We're sorry, we cannot retrieve your information label not exists");


        public readonly ICriteria<EnterACPDActivityPage> PageReady;

        public EnterACPDActivityPageCriteria()
        {
            PageReady = CategorySelElemBtnEnabledAndVisible.AND(LoadIconNotExists).
                AND(LoadIconOverlayNotExists).AND(WereSorryErrorLblNotExists);
        }
    }
}
