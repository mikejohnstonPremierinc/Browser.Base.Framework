using Browser.Core.Framework;

namespace LMS.AppFramework
{
    public class ActPIMPageCriteria
    {
        public readonly ICriteria<ActPIMPage> ContinueBtnVisible = new Criteria<ActPIMPage>(p =>
        {
            return p.Exists(Bys.ActPIMPage.ContinueBtn, ElementCriteria.IsVisible);

        }, "Continue button visible");

        public readonly ICriteria<ActPIMPage> FinishBtnVisible = new Criteria<ActPIMPage>(p =>
        {
            return p.Exists(Bys.ActPIMPage.FinishBtn, ElementCriteria.IsVisible);

        }, "Finish button visible");

        public readonly ICriteria<ActPIMPage> SubmitBtnVisible = new Criteria<ActPIMPage>(p =>
        {
            return p.Exists(Bys.ActPIMPage.SubmitBtn, ElementCriteria.IsVisible);

        }, "Submit button visible");

        public readonly ICriteria<ActPIMPage> LoadIconNotExists = new Criteria<ActPIMPage>(p =>
        {
            return !p.Exists(Bys.LMSPage.LoadIcon);

        }, "Load icon not exists");

        public readonly ICriteria<ActPIMPage> CalculationsInProgressLblNotExists = new Criteria<ActPIMPage>(p =>
        {
            return !p.Exists(Bys.ActPIMPage.CalculationsInProgressLbl);

        }, "Calculations in progress label not exists");

        public readonly ICriteria<ActPIMPage> PageReady;

        public ActPIMPageCriteria()
        {
            PageReady = (ContinueBtnVisible.AND(LoadIconNotExists)).
                OR(FinishBtnVisible.AND(LoadIconNotExists));
        }
    }
}
