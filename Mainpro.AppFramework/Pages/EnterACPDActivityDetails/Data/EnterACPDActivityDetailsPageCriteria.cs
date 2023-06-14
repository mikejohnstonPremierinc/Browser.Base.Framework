using Browser.Core.Framework;


namespace Mainpro.AppFramework
{
    public class EnterACPDActivityDetailsPageCriteria
    {
        public readonly ICriteria<EnterACPDActivityDetailsPage> SubmitBtnVisible = new Criteria<EnterACPDActivityDetailsPage>(p =>
        {
            return p.Exists(Bys.EnterACPDActivityDetailsPage.SubmitBtn, ElementCriteria.IsVisible);

        }, "Submit button visible");

        public readonly ICriteria<EnterACPDActivityDetailsPage> YourActivityHasBeenSavedBannerLblExists = new Criteria<EnterACPDActivityDetailsPage>(p =>
        {
            return p.Exists(Bys.EnterACPDActivityDetailsPage.YourActivityHasBeenSavedBannerLbl, ElementCriteria.IsVisible);

        }, "Your activity has been saved banner label exists");

        public readonly ICriteria<EnterACPDActivityDetailsPage> YourActivityHasBeenSavedBannerLblNotExists = new Criteria<EnterACPDActivityDetailsPage>(p =>
        {
            return !p.Exists(Bys.EnterACPDActivityDetailsPage.YourActivityHasBeenSavedBannerLbl, ElementCriteria.IsVisible);

        }, "Your activity has been saved banner label exists");

        public readonly ICriteria<EnterACPDActivityDetailsPage> LoadIconNotExists = new Criteria<EnterACPDActivityDetailsPage>(p =>
        {
            return !p.Exists(Bys.MainproPage.LoadIcon);

        }, "Load icon not exists");

        public readonly ICriteria<EnterACPDActivityDetailsPage> LoadIconOverlayNotExists = new Criteria<EnterACPDActivityDetailsPage>(p =>
        {
            return !p.Exists(Bys.MainproPage.LoadIconOverlay);

        }, "Load overlay not exists");

        public readonly ICriteria<EnterACPDActivityDetailsPage> WereSorryErrorLblNotExists = new Criteria<EnterACPDActivityDetailsPage>(p =>
        {
            return !p.Exists(Bys.MainproPage.WereSorryErrorLbl);

        }, "We're sorry, we cannot retrieve your information label not exists");

        public readonly ICriteria<EnterACPDActivityDetailsPage> PageReady;

        public EnterACPDActivityDetailsPageCriteria()
        {
            PageReady = SubmitBtnVisible.AND(LoadIconNotExists).
                AND(LoadIconOverlayNotExists).AND(WereSorryErrorLblNotExists);
        }
    }
}
