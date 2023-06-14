using Browser.Core.Framework;

namespace LMS.AppFramework
{
    public class ActOrderReceiptPageCriteria
    {
        public readonly ICriteria<ActOrderReceiptPage> LoadIconNotExists = new Criteria<ActOrderReceiptPage>(p =>
        {
            return !p.Exists(Bys.LMSPage.LoadIcon);

        }, "Load icon not exists");

        public readonly ICriteria<ActOrderReceiptPage> ExitBtnVisible = new Criteria<ActOrderReceiptPage>(p =>
        {
            return p.Exists(Bys.ActOrderReceiptPage.ExitBtn, ElementCriteria.IsVisible);

        }, "Exit button visible");

        public readonly ICriteria<ActOrderReceiptPage> PageReady;

        public ActOrderReceiptPageCriteria()
        {
            PageReady = ExitBtnVisible.AND(LoadIconNotExists);
        }
    }
}
