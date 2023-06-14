using Browser.Core.Framework;

namespace Mainpro.AppFramework
{
    public class Step2PageCriteria
    {
        public readonly ICriteria<Step2Page> StepNumberLabelVisibleAndTextContainsStep = new Criteria<Step2Page>(p =>
        {
            return p.Exists(Bys.MainproPage.PLP_Header_StepNumberLabel, ElementCriteria.IsVisible,
                ElementCriteria.TextContains("Step"));

        }, "Step Number Label Visible And Text = Step");

        public readonly ICriteria<Step2Page> LoadIconNotExists = new Criteria<Step2Page>(p =>
        {
            return !p.Exists(Bys.MainproPage.LoadIcon);

        }, "Load icon not exists");

        public readonly ICriteria<Step2Page> LoadIconOverlayNotExists = new Criteria<Step2Page>(p =>
        {
            return !p.Exists(Bys.MainproPage.LoadIconOverlay);

        }, "Load overlay not exists");

        public readonly ICriteria<Step2Page> WereSorryErrorLblNotExists = new Criteria<Step2Page>(p =>
        {
            return !p.Exists(Bys.MainproPage.WereSorryErrorLbl);

        }, "We're sorry, we cannot retrieve your information label not exists");

        public readonly ICriteria<Step2Page> AreYouSureYouWantToDeleteFormDeleteBtnNotVisible = new Criteria<Step2Page>(p =>
        {
            return !p.Exists(Bys.MainproPage.PLP_AreYouSureYouWantToDeleteFormDeleteBtn, ElementCriteria.IsVisible);

        }, "PLP Are you sure you want to delete? form Delete button not visible");

         public readonly ICriteria<Step2Page> AreYouSureYouWantToDeleteFormDeleteBtnVisible = new Criteria<Step2Page>(p =>
        {
            return p.Exists(Bys.MainproPage.PLP_AreYouSureYouWantToDeleteFormDeleteBtn, ElementCriteria.IsVisible);

        }, "PLP Are you sure you want to delete? form Delete button visible");



        public readonly ICriteria<Step2Page> PageReady;

        public Step2PageCriteria()
        {
            PageReady = StepNumberLabelVisibleAndTextContainsStep.AND(LoadIconNotExists).
                AND(LoadIconOverlayNotExists);
        }
    }
}

