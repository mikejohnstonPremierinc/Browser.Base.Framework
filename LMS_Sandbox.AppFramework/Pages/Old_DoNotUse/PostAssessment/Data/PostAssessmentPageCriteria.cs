//using Browser.Core.Framework;

//namespace LMS.AppFramework
//{
//    public class PostActAssessmentPageCriteria
//    {
//        public readonly ICriteria<PostActAssessmentPage> SubmitBtnVisible = new Criteria<PostActAssessmentPage>(p =>
//        {
//            return p.Exists(Bys.PostActAssessmentPage.SubmitBtn, ElementCriteria.IsVisible);

//        }, "Submit button visible");

//        public readonly ICriteria<PostActAssessmentPage> LoadIconNotExists = new Criteria<PostActAssessmentPage>(p =>
//        {
//            return !p.Exists(Bys.LMSPage.LoadIcon);

//        }, "Load icon not exists");

//        public readonly ICriteria<PostActAssessmentPage> FirstQuestionLblVisible = new Criteria<PostActAssessmentPage>(p =>
//        {
//            return p.Exists(Bys.PostActAssessmentPage.FirstQuestionLbl, ElementCriteria.IsVisible);

//        }, "First question label visible");

//        public readonly ICriteria<PostActAssessmentPage> ContinueBtnEnabled = new Criteria<PostActAssessmentPage>(p =>
//        {
//            return p.Exists(Bys.PostActAssessmentPage.ContinueBtn, ElementCriteria.IsEnabled);

//        }, "Continue button enabled");

//        public readonly ICriteria<PostActAssessmentPage> YourStatusValueLblTextEqualsSubmitted = new Criteria<PostActAssessmentPage>(p =>
//        {
//            return p.Exists(Bys.PostActAssessmentPage.YourStatusValueLbl, ElementCriteria.Text("Submitted"));

//        }, "Your Status value label text equal to 'Submitted'");

//        public readonly ICriteria<PostActAssessmentPage> RetakeBtnVisible = new Criteria<PostActAssessmentPage>(p =>
//        {
//            return p.Exists(Bys.PostActAssessmentPage.RetakeBtn, ElementCriteria.IsVisible);

//        }, "Retake button visible");

//        public readonly ICriteria<PostActAssessmentPage> PageReady;
//        public PostActAssessmentPageCriteria()
//        {
//            PageReady = SubmitBtnVisible.AND(LoadIconNotExists).AND(FirstQuestionLblVisible);
//        }
//    }
//}
