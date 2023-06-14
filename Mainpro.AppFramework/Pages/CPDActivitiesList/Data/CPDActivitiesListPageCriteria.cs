using Browser.Core.Framework;

namespace Mainpro.AppFramework
{
    public class CPDActivitiesListPageCriteria
    {
        public readonly ICriteria<CPDActivitiesListPage> ActTblActivityColHdrVisible = new Criteria<CPDActivitiesListPage>(p =>
        {
            return p.Exists(Bys.CPDActivitiesListPage.ActTblActivityColHdr, ElementCriteria.IsVisible);

        }, "Activity table Activity column header visible");

        public readonly ICriteria<CPDActivitiesListPage> LoadIconNotExists = new Criteria<CPDActivitiesListPage>(p =>
        {
            return !p.Exists(Bys.MainproPage.LoadIcon);

        }, "Load icon not exists");

        public readonly ICriteria<CPDActivitiesListPage> LoadIconOverlayNotExists = new Criteria<CPDActivitiesListPage>(p =>
        {
            return !p.Exists(Bys.MainproPage.LoadIconOverlay);

        }, "Load overlay not exists");

        public readonly ICriteria<CPDActivitiesListPage> WereSorryErrorLblNotExists = new Criteria<CPDActivitiesListPage>(p =>
        {
            return !p.Exists(Bys.MainproPage.WereSorryErrorLbl);

        }, "We're sorry, we cannot retrieve your information label not exists");

        public readonly ICriteria<CPDActivitiesListPage> PageReady;

        public CPDActivitiesListPageCriteria()
        {
            PageReady = ActTblActivityColHdrVisible.AND(LoadIconNotExists).
                AND(LoadIconOverlayNotExists).AND(WereSorryErrorLblNotExists);
        }
    }
}
