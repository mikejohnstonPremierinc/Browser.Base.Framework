////using Browser.Core.Framework;

////namespace LMS.AppFramework
////{
////    public class EvaluationPageCriteria
////    {
////        public readonly ICriteria<EvaluationPage> SubmitBtnVisible = new Criteria<EvaluationPage>(p =>
////        {
////            return p.Exists(Bys.EvaluationPage.SubmitBtn, ElementCriteria.IsVisible);

////        }, "Submit button visible");

////        public readonly ICriteria<EvaluationPage> LoadIconNotExists = new Criteria<EvaluationPage>(p =>
////        {
////            return !p.Exists(Bys.LMSPage.LoadIcon);

////        }, "Load icon not exists");

////        public readonly ICriteria<EvaluationPage> FirstQuestionLblVisible = new Criteria<EvaluationPage>(p =>
////        {
////            return p.Exists(Bys.EvaluationPage.FirstQuestionLbl, ElementCriteria.IsVisible);

////        }, "First question label visible");

////        public readonly ICriteria<EvaluationPage> ContinueBtnEnabled = new Criteria<EvaluationPage>(p =>
////        {
////            return p.Exists(Bys.EvaluationPage.ContinueBtn, ElementCriteria.IsEnabled);

////        }, "Continue button enabled");

////        public readonly ICriteria<EvaluationPage> YourStatusValueLblTextEqualsSubmitted = new Criteria<EvaluationPage>(p =>
////        {
////            return p.Exists(Bys.EvaluationPage.YourStatusValueLbl, ElementCriteria.Text("Submitted"));

////        }, "Your Status value label text equal to 'Submitted'");

////        public readonly ICriteria<EvaluationPage> RetakeBtnVisible = new Criteria<EvaluationPage>(p =>
////        {
////            return p.Exists(Bys.EvaluationPage.RetakeBtn, ElementCriteria.IsVisible);

////        }, "Retake button visible");

////        public readonly ICriteria<EvaluationPage> PageReady;
////        public EvaluationPageCriteria()
////        {
////            PageReady = SubmitBtnVisible.AND(LoadIconNotExists).AND(FirstQuestionLblVisible);
////        }
////    }
////}
