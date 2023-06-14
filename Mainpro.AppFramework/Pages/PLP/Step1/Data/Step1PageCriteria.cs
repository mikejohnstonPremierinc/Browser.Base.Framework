using Browser.Core.Framework;

namespace Mainpro.AppFramework
{
    public class Step1PageCriteria
    {
        public readonly ICriteria<Step1Page> StepNumberLabelVisibleAndTextContainsStep = new Criteria<Step1Page>(p =>
        {
            return p.Exists(Bys.MainproPage.PLP_Header_StepNumberLabel, ElementCriteria.IsVisible, 
                ElementCriteria.TextContains("Step"));

        }, "Step Number Label Visible And Text = Step");

        public readonly ICriteria<Step1Page> LoadIconNotExists = new Criteria<Step1Page>(p =>
        {
            return !p.Exists(Bys.MainproPage.LoadIcon);

        }, "Load icon not exists");

        public readonly ICriteria<Step1Page> LoadIconOverlayNotExists = new Criteria<Step1Page>(p =>
        {
            return !p.Exists(Bys.MainproPage.LoadIconOverlay);

        }, "Load overlay not exists");

        public readonly ICriteria<Step1Page> WereSorryErrorLblNotExists = new Criteria<Step1Page>(p =>
        {
            return !p.Exists(Bys.MainproPage.WereSorryErrorLbl);

        }, "We're sorry, we cannot retrieve your information label not exists");

        public readonly ICriteria<Step1Page> PageReady;

        public Step1PageCriteria()
        {
            PageReady = StepNumberLabelVisibleAndTextContainsStep.AND(LoadIconNotExists).
                AND(LoadIconOverlayNotExists);
        }
    }
}
