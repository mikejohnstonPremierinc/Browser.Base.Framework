using Browser.Core.Framework;

namespace Mainpro.AppFramework
{
    public class StepPRPageCriteria
    {
        public readonly ICriteria<StepPRPage> StepNumberLabelVisibleAndTextContainsStep = new Criteria<StepPRPage>(p =>
        {
            return p.Exists(Bys.MainproPage.PLP_Header_StepNumberLabel, ElementCriteria.IsVisible,
                ElementCriteria.TextContains("Post Reflection"));

        }, "Step Number Label Visible And Text = Step");

        public readonly ICriteria<StepPRPage> LoadIconNotExists = new Criteria<StepPRPage>(p =>
        {
            return !p.Exists(Bys.MainproPage.LoadIcon);

        }, "Load icon not exists");

        public readonly ICriteria<StepPRPage> LoadIconOverlayNotExists = new Criteria<StepPRPage>(p =>
        {
            return !p.Exists(Bys.MainproPage.LoadIconOverlay);

        }, "Load overlay not exists");

        public readonly ICriteria<StepPRPage> WereSorryErrorLblNotExists = new Criteria<StepPRPage>(p =>
        {
            return !p.Exists(Bys.MainproPage.WereSorryErrorLbl);

        }, "We're sorry, we cannot retrieve your information label not exists");


        public readonly ICriteria<StepPRPage> PageReady;

        public StepPRPageCriteria()
        {
            PageReady = StepNumberLabelVisibleAndTextContainsStep.AND(LoadIconNotExists).
                AND(LoadIconOverlayNotExists);
        }
    }
}

