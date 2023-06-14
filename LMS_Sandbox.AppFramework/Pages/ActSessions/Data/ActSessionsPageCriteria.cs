using Browser.Core.Framework;

namespace LMS.AppFramework
{
    public class ActSessionsPageCriteria
    {
        public readonly ICriteria<ActSessionsPage> ContinueBtnVisible = new Criteria<ActSessionsPage>(p =>
        {
            return p.Exists(Bys.ActSessionsPage.ContinueBtn, ElementCriteria.IsVisible);

        }, "Continue button visible");

        public readonly ICriteria<ActSessionsPage> FinishBtnVisible = new Criteria<ActSessionsPage>(p =>
        {
            return p.Exists(Bys.ActSessionsPage.FinishBtn, ElementCriteria.IsVisible);

        }, "Finish button visible");

        public readonly ICriteria<ActSessionsPage> AccessCodeFormAccessCodeTxtNotVisible = new Criteria<ActSessionsPage>(p =>
        {
            return p.Exists(Bys.ActSessionsPage.AccessCodeFormAccessCodeTxt, ElementCriteria.IsNotVisible);

        }, "Access Code form Access Code text box not exists");

        public readonly ICriteria<ActSessionsPage> LoadIconNotExists = new Criteria<ActSessionsPage>(p =>
        {
            return !p.Exists(Bys.LMSPage.LoadIcon);

        }, "Load icon not exists");

        public readonly ICriteria<ActSessionsPage> NotificationInfoMessageLblXBtnNotExists = new Criteria<ActSessionsPage>(p =>
        {
            return !p.Exists(Bys.LMSPage.NotificationInfoMessageLblXBtn);

        }, "Notification Info Message Label X button not exists");

        public readonly ICriteria<ActSessionsPage> PageReady;

        public ActSessionsPageCriteria()
        {
            PageReady = (ContinueBtnVisible.AND(LoadIconNotExists)).OR(FinishBtnVisible.AND(LoadIconNotExists));
        }
    }
}
