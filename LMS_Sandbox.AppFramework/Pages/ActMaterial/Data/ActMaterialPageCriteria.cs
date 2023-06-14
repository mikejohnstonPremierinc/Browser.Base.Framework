using Browser.Core.Framework;

namespace LMS.AppFramework
{
    public class ActMaterialPageCriteria
    {
        public readonly ICriteria<ActMaterialPage> ContinueBtnVisible = new Criteria<ActMaterialPage>(p =>
        {
            return p.Exists(Bys.ActMaterialPage.ContinueBtn, ElementCriteria.IsVisible);

        }, "Continue button visible");

        public readonly ICriteria<ActMaterialPage> PleaseClickTheNameLblVisible = new Criteria<ActMaterialPage>(p =>
        {
            return p.Exists(Bys.ActMaterialPage.PleaseClickTheNameLbl, ElementCriteria.IsVisible);

        }, "Please click the name of the item label visible");

        public readonly ICriteria<ActMaterialPage> ActivityOverviewChkVisible = new Criteria<ActMaterialPage>(p =>
        {
            return p.Exists(Bys.ActMaterialPage.ActivityMaterialChk, ElementCriteria.IsVisible);

        }, "Check box visible");

        public readonly ICriteria<ActMaterialPage> FinishBtnVisible = new Criteria<ActMaterialPage>(p =>
        {
            return p.Exists(Bys.ActMaterialPage.FinishBtn, ElementCriteria.IsVisible);

        }, "Finish button visible and enabled");

        public readonly ICriteria<ActMaterialPage> LoadIconNotExists = new Criteria<ActMaterialPage>(p =>
        {
            return !p.Exists(Bys.LMSPage.LoadIcon);

        }, "Load icon not exists");

        public readonly ICriteria<ActMaterialPage> PageReady;

        public ActMaterialPageCriteria()
        {
            PageReady = ContinueBtnVisible.AND(LoadIconNotExists).AND(PleaseClickTheNameLblVisible)
                .OR(ActivityOverviewChkVisible.AND(LoadIconNotExists).AND(PleaseClickTheNameLblVisible)
                .OR(FinishBtnVisible.AND(LoadIconNotExists).AND(PleaseClickTheNameLblVisible)));


        }
    }
}
