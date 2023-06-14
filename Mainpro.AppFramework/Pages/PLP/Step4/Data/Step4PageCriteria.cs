using Browser.Core.Framework;

namespace Mainpro.AppFramework
{
    public class Step4PageCriteria
    {
        public readonly ICriteria<Step4Page> StepNumberLabelVisibleAndTextContainsStep = new Criteria<Step4Page>(p =>
        {
            return p.Exists(Bys.MainproPage.PLP_Header_StepNumberLabel, ElementCriteria.IsVisible,
                ElementCriteria.TextContains("Step"));

        }, "Step Number Label Visible And Text = Step");

        public readonly ICriteria<Step4Page> LoadIconNotExists = new Criteria<Step4Page>(p =>
        {
            return !p.Exists(Bys.MainproPage.LoadIcon);

        }, "Load icon not exists");

        public readonly ICriteria<Step4Page> LoadIconOverlayNotExists = new Criteria<Step4Page>(p =>
        {
            return !p.Exists(Bys.MainproPage.LoadIconOverlay);

        }, "Load overlay not exists");

        public readonly ICriteria<Step4Page> WereSorryErrorLblNotExists = new Criteria<Step4Page>(p =>
        {
            return !p.Exists(Bys.MainproPage.WereSorryErrorLbl);

        }, "We're sorry, we cannot retrieve your information label not exists");


        public readonly ICriteria<Step4Page> PageReady;

        public Step4PageCriteria()
        {
            PageReady = StepNumberLabelVisibleAndTextContainsStep.AND(LoadIconNotExists).
                AND(LoadIconOverlayNotExists);
        }
    }
}

