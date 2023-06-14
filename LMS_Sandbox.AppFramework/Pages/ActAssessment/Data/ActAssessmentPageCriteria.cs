using Browser.Core.Framework;

namespace LMS.AppFramework
{
    public class ActAssessmentPageCriteria
    {
        public readonly ICriteria<ActAssessmentPage> SubmitBtnVisible = new Criteria<ActAssessmentPage>(p =>
        {
            return p.Exists(Bys.ActAssessmentPage.SubmitBtn, ElementCriteria.IsVisible);

        }, "Submit button visible");

        public readonly ICriteria<ActAssessmentPage> LaunchBtnVisible = new Criteria<ActAssessmentPage>(p =>
        {
            return p.Exists(Bys.ActAssessmentPage.LaunchBtn, ElementCriteria.IsVisible);

        }, "Launch button visible");

        public readonly ICriteria<ActAssessmentPage> NextBtnVisible = new Criteria<ActAssessmentPage>(p =>
        {
            return p.Exists(Bys.ActAssessmentPage.NextBtn, ElementCriteria.IsVisible);

        }, "Next button visible");

        public readonly ICriteria<ActAssessmentPage> AssessmentNameLblVisible = new Criteria<ActAssessmentPage>(p =>
        {
            return p.Exists(Bys.ActAssessmentPage.AssessmentNameLbl, ElementCriteria.IsVisible);

        }, "Assessment Name label visible");
        

        public readonly ICriteria<ActAssessmentPage> LoadIconNotExists = new Criteria<ActAssessmentPage>(p =>
        {
            return !p.Exists(Bys.LMSPage.LoadIcon);

        }, "Load icon not exists");

        public readonly ICriteria<ActAssessmentPage> FirstQuestionLblVisible = new Criteria<ActAssessmentPage>(p =>
        {
            return p.Exists(Bys.ActAssessmentPage.QuestionTextLbls, ElementCriteria.IsVisible);

        }, "First question label visible");

        public readonly ICriteria<ActAssessmentPage> ContinueBtnEnabled = new Criteria<ActAssessmentPage>(p =>
        {
            return p.Exists(Bys.ActAssessmentPage.ContinueBtn, ElementCriteria.IsEnabled);

        }, "Continue button enabled");

        public readonly ICriteria<ActAssessmentPage> YourStatusValueLblTextEqualsSubmitted = new Criteria<ActAssessmentPage>(p =>
        {
            return p.Exists(Bys.ActAssessmentPage.YourStatusValueLbl, ElementCriteria.Text("SUBMITTED", true));

        }, "Your Status value label text equal to 'Submitted'");

        public readonly ICriteria<ActAssessmentPage> RetakeBtnVisible = new Criteria<ActAssessmentPage>(p =>
        {
            return p.Exists(Bys.ActAssessmentPage.RetakeBtn, ElementCriteria.IsVisible);

        }, "Retake button visible");

        public readonly ICriteria<ActAssessmentPage> ReturnToSummaryBtnVisible = new Criteria<ActAssessmentPage>(p =>
        {
            return p.Exists(Bys.ActAssessmentPage.ReturnToSummaryBtn, ElementCriteria.IsVisible);

        }, "Return To Summary button visible");

        public readonly ICriteria<ActAssessmentPage> FinishBtnVisible = new Criteria<ActAssessmentPage>(p =>
        {
            return p.Exists(Bys.ActAssessmentPage.FinishBtn, ElementCriteria.IsVisible);

        }, "Finish button visible");

        public readonly ICriteria<ActAssessmentPage> PageReady;

        public ActAssessmentPageCriteria()
        {
            PageReady = (SubmitBtnVisible.AND(LoadIconNotExists).AND(FirstQuestionLblVisible)).
                OR((LaunchBtnVisible).AND(LoadIconNotExists)).
                OR((NextBtnVisible).AND(LoadIconNotExists).AND(FirstQuestionLblVisible)).
                OR((YourStatusValueLblTextEqualsSubmitted)).
                OR((RetakeBtnVisible));
        }
    }
}
