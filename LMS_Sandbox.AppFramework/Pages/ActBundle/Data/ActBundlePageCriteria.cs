using Browser.Core.Framework;

namespace LMS.AppFramework
{
    public class ActBundlePageCriteria
    {
        public readonly ICriteria<ActBundlePage> ActivityTblBodyActivityLnksVisible = new Criteria<ActBundlePage>(p =>
        {
            return p.Exists(Bys.ActBundlePage.ActivityTblBodyActivityLnks, ElementCriteria.IsVisible);

        }, "Activity table, Activity links visible");

        public readonly ICriteria<ActBundlePage> LoadIconNotExists = new Criteria<ActBundlePage>(p =>
        {
            return !p.Exists(Bys.LMSPage.LoadIcon);

        }, "Load icon not exists");

        public readonly ICriteria<ActBundlePage> PageReady;

        public ActBundlePageCriteria()
        {
            PageReady = ActivityTblBodyActivityLnksVisible.AND(LoadIconNotExists);
        }
    }
}
