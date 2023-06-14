using Browser.Core.Framework;

namespace LMS.AppFramework
{
    public class ActClaimCreditPageCriteria
    {
        public readonly ICriteria<ActClaimCreditPage> ContinueBtnVisible = new Criteria<ActClaimCreditPage>(p =>
        {
            return p.Exists(Bys.ActClaimCreditPage.ContinueBtn, ElementCriteria.IsVisible);

        }, "Continue button visible");

        public readonly ICriteria<ActClaimCreditPage> FinishBtnVisible = new Criteria<ActClaimCreditPage>(p =>
        {
            return p.Exists(Bys.ActClaimCreditPage.FinishBtn, ElementCriteria.IsVisible);

        }, "Finish button visible");

        public readonly ICriteria<ActClaimCreditPage> LoadIconNotExists = new Criteria<ActClaimCreditPage>(p =>
        {
            return !p.Exists(Bys.LMSPage.LoadIcon);

        }, "Load icon not exists");

        public readonly ICriteria<ActClaimCreditPage> PageReady;

        public ActClaimCreditPageCriteria()
        {
            PageReady = (ContinueBtnVisible.AND(LoadIconNotExists)).
                OR(FinishBtnVisible.AND(LoadIconNotExists));
        }
    }
}
