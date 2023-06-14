using Browser.Core.Framework;

namespace LMS.AppFramework
{
    public class ActOrderDetailsPageCriteria
    {
        public readonly ICriteria<ActOrderDetailsPage> LoadIconNotExists = new Criteria<ActOrderDetailsPage>(p =>
        {
            return !p.Exists(Bys.LMSPage.LoadIcon);

        }, "Load icon not exists");

        public readonly ICriteria<ActOrderDetailsPage> ApplyCodeBtnVisible = new Criteria<ActOrderDetailsPage>(p =>
        {
            return p.Exists(Bys.ActOrderDetailsPage.ApplyCodeBtn, ElementCriteria.IsVisible);

        }, "Apply Code button visible");

        public readonly ICriteria<ActOrderDetailsPage> RemoveCodeBtnVisible = new Criteria<ActOrderDetailsPage>(p =>
        {
            return p.Exists(Bys.ActOrderDetailsPage.RemoveCodeBtn, ElementCriteria.IsVisible);

        }, "Remove Code button visible");

        public readonly ICriteria<ActOrderDetailsPage> ConfirmFormCancelBtnNotExists = new Criteria<ActOrderDetailsPage>(p =>
        {
            return !p.Exists(Bys.ActOrderDetailsPage.ConfirmFormCancelBtn);

        }, "Confirm form, Cancel button not exists");

        public readonly ICriteria<ActOrderDetailsPage> PageReady;

        public ActOrderDetailsPageCriteria()
        {
            PageReady = (ApplyCodeBtnVisible.AND(LoadIconNotExists)).
                OR((RemoveCodeBtnVisible).AND(LoadIconNotExists));
        }
    }
}
