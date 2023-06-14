using Browser.Core.Framework;

namespace LMS.AppFramework
{
    public class ActOnHoldPageCriteria
    {
        public readonly ICriteria<ActOnHoldPage> NotificationWarnIconVisible = new Criteria<ActOnHoldPage>(p =>
        {
            return p.Exists(Bys.ActOnHoldPage.NotificationWarnIcon, ElementCriteria.IsVisible);

        }, "'There is a hold on this activity' label visible");

        public readonly ICriteria<ActOnHoldPage> LoadIconNotExists = new Criteria<ActOnHoldPage>(p =>
        {
            return !p.Exists(Bys.LMSPage.LoadIcon);

        }, "Load icon not exists");

        public readonly ICriteria<ActOnHoldPage> PageReady;

        public ActOnHoldPageCriteria()
        {
            PageReady = NotificationWarnIconVisible.AND(LoadIconNotExists);
        }
    }
}
