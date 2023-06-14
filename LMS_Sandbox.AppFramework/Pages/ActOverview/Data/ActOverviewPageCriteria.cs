using Browser.Core.Framework;

namespace LMS.AppFramework
{
    public class ActOverviewPageCriteria
    {
        public readonly ICriteria<ActOverviewPage> ContinueBtnVisible = new Criteria<ActOverviewPage>(p =>
        {
            return p.Exists(Bys.ActOverviewPage.ContinueBtn, ElementCriteria.IsVisible);

        }, "Continue button visible");

        public readonly ICriteria<ActOverviewPage> ActivityOverviewChkVisible = new Criteria<ActOverviewPage>(p =>
        {
            return p.Exists(Bys.ActOverviewPage.ActivityOverviewChk, ElementCriteria.IsVisible);

        }, "Check box visible");

        public readonly ICriteria<ActOverviewPage> FinishBtnVisible = new Criteria<ActOverviewPage>(p =>
        {
            return p.Exists(Bys.ActOverviewPage.FinishBtn, ElementCriteria.IsVisible);

        }, "Finish button visible");

        public readonly ICriteria<ActOverviewPage> LoadIconNotExists = new Criteria<ActOverviewPage>(p =>
        {
            return !p.Exists(Bys.LMSPage.LoadIcon);

        }, "Load icon not exists");

        public readonly ICriteria<ActOverviewPage> PageReady;

        public ActOverviewPageCriteria()
        {
            PageReady = (ContinueBtnVisible.AND(LoadIconNotExists)).
                OR(FinishBtnVisible.AND(LoadIconNotExists)).
                OR(ActivityOverviewChkVisible.AND(LoadIconNotExists));
        }
    }
}
