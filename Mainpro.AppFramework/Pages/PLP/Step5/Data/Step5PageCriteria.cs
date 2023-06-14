using Browser.Core.Framework;

namespace Mainpro.AppFramework
{
    public class Step5PageCriteria
    {
        public readonly ICriteria<Step5Page> StepNumberLabelVisibleAndTextContainsStep = new Criteria<Step5Page>(p =>
        {
            return p.Exists(Bys.MainproPage.PLP_Header_StepNumberLabel, ElementCriteria.IsVisible,
                ElementCriteria.TextContains("Step"));

        }, "Step Number Label Visible And Text = Step");

        public readonly ICriteria<Step5Page> LoadIconNotExists = new Criteria<Step5Page>(p =>
        {
            return !p.Exists(Bys.MainproPage.LoadIcon);

        }, "Load icon not exists");

        public readonly ICriteria<Step5Page> LoadIconOverlayNotExists = new Criteria<Step5Page>(p =>
        {
            return !p.Exists(Bys.MainproPage.LoadIconOverlay);

        }, "Load overlay not exists");

        public readonly ICriteria<Step5Page> WereSorryErrorLblNotExists = new Criteria<Step5Page>(p =>
        {
            return !p.Exists(Bys.MainproPage.WereSorryErrorLbl);

        }, "We're sorry, we cannot retrieve your information label not exists");


        public readonly ICriteria<Step5Page> PageReady;

        public Step5PageCriteria()
        {
            PageReady = StepNumberLabelVisibleAndTextContainsStep.AND(LoadIconNotExists).
                AND(LoadIconOverlayNotExists);
        }
    }
}

