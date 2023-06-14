using Browser.Core.Framework;


namespace Mainpro.AppFramework
{
    public class CPDPlanningPageCriteria
    {
        public readonly ICriteria<CPDPlanningPage> CreateAPersonalLearningGoalBtnVisible = new Criteria<CPDPlanningPage>(p =>
        {
            return p.Exists(Bys.CPDPlanningPage.CreateAPersonalLearningGoalBtn, ElementCriteria.IsVisible);

        }, "Create A Personal Learning Goal button visible");


        public readonly ICriteria<CPDPlanningPage> LoadIconNotExists = new Criteria<CPDPlanningPage>(p =>
        {
            return !p.Exists(Bys.MainproPage.LoadIcon);

        }, "Load icon not exists");

        public readonly ICriteria<CPDPlanningPage> LoadIconOverlayNotExists = new Criteria<CPDPlanningPage>(p =>
        {
            return !p.Exists(Bys.MainproPage.LoadIconOverlay);

        }, "Load overlay not exists");

        public readonly ICriteria<CPDPlanningPage> WereSorryErrorLblNotExists = new Criteria<CPDPlanningPage>(p =>
        {
            return !p.Exists(Bys.MainproPage.WereSorryErrorLbl);

        }, "We're sorry, we cannot retrieve your information label not exists");

        public readonly ICriteria<CPDPlanningPage> PageReady;

        public CPDPlanningPageCriteria()
        {
            PageReady = CreateAPersonalLearningGoalBtnVisible.AND(LoadIconNotExists).
                AND(LoadIconOverlayNotExists).AND(WereSorryErrorLblNotExists);
        }
    }
}
