using Browser.Core.Framework;

namespace Mainpro.AppFramework
{
    public class Step3PageCriteria
    {
        public readonly ICriteria<Step3Page> StepNumberLabelVisibleAndTextContainsStep = new Criteria<Step3Page>(p =>
        {
            return p.Exists(Bys.MainproPage.PLP_Header_StepNumberLabel, ElementCriteria.IsVisible,
                ElementCriteria.TextContains("Step"));

        }, "Step Number Label Visible And Text = Step");

        public readonly ICriteria<Step3Page> LoadIconNotExists = new Criteria<Step3Page>(p =>
        {
            return !p.Exists(Bys.MainproPage.LoadIcon);

        }, "Load icon not exists");

        public readonly ICriteria<Step3Page> LoadIconOverlayNotExists = new Criteria<Step3Page>(p =>
        {
            return !p.Exists(Bys.MainproPage.LoadIconOverlay);

        }, "Load overlay not exists");

        public readonly ICriteria<Step3Page> ActivityDetailFormDateTxtNotExists = new Criteria<Step3Page>(p =>
        {
            return !p.Exists(Bys.Step3Page.ActivityDetailFormDateTxt,ElementCriteria.IsVisible);

        }, "Activity Detail Form Date text box not exists");

        public readonly ICriteria<Step3Page> AreYouSureYouWantToDeleteFormDeleteBtnNotVisible = new Criteria<Step3Page>(p =>
        {
            return p.Exists(Bys.MainproPage.PLP_AreYouSureYouWantToDeleteFormDeleteBtn, ElementCriteria.IsVisible);

        }, "PLP Are you sure you want to delete? form Delete button not visible");

        public readonly ICriteria<Step3Page> WereSorryErrorLblNotExists = new Criteria<Step3Page>(p =>
        {
            return !p.Exists(Bys.MainproPage.WereSorryErrorLbl);

        }, "We're sorry, we cannot retrieve your information label not exists");

        public readonly ICriteria<Step3Page> DeleteFormYesBtnVisible = new Criteria<Step3Page>(p =>
        {
            return p.Exists(Bys.MainproPage.DeleteFormYesBtn, ElementCriteria.IsVisible);

        }, "Delete form Yes button exists and is visible");

        public readonly ICriteria<Step3Page> PLP_AreYouSureYouWantToDeleteFormDeleteBtnVisible = new Criteria<Step3Page>(p =>
        {
            return p.Exists(Bys.MainproPage.PLP_AreYouSureYouWantToDeleteFormDeleteBtn, ElementCriteria.IsVisible);

        }, "PLP Are you sure you want to delete? form Delete button exists and is visible");



        public readonly ICriteria<Step3Page> PageReady;

        public Step3PageCriteria()
        {
            PageReady = StepNumberLabelVisibleAndTextContainsStep.AND(LoadIconNotExists).
                AND(LoadIconOverlayNotExists);
        }
    }
}

