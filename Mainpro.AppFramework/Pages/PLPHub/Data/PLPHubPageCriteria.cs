using Browser.Core.Framework;


namespace Mainpro.AppFramework
{
    public class PLPHubPageCriteria
    {
        public readonly ICriteria<PLPHubPage> PlpHubPlpEnterBtnVisible = new Criteria<PLPHubPage>(p =>
        {
            return p.Exists(Bys.PLPHubPage.PlpHubPlpEnterBtn, ElementCriteria.IsVisible);

        }, "PlpHubPlpEnter button visible");


        public readonly ICriteria<PLPHubPage> LoadIconNotExists = new Criteria<PLPHubPage>(p =>
        {
            return !p.Exists(Bys.MainproPage.LoadIcon);

        }, "Load icon not exists");

        public readonly ICriteria<PLPHubPage> LoadIconOverlayNotExists = new Criteria<PLPHubPage>(p =>
        {
            return !p.Exists(Bys.MainproPage.LoadIconOverlay);

        }, "Load overlay not exists");

        public readonly ICriteria<PLPHubPage> WereSorryErrorLblNotExists = new Criteria<PLPHubPage>(p =>
        {
            return !p.Exists(Bys.MainproPage.WereSorryErrorLbl);

        }, "We're sorry, we cannot retrieve your information label not exists");

        public readonly ICriteria<PLPHubPage> PageReady;

        public PLPHubPageCriteria()
        {
            PageReady = PlpHubPlpEnterBtnVisible.AND(LoadIconNotExists).
                AND(LoadIconOverlayNotExists).AND(WereSorryErrorLblNotExists);
        }
    }
}
