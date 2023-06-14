//using Browser.Core.Framework;

//namespace LMS.AppFramework
//{
//    public class PreActAssessmentPageCriteria
//    {
//        public readonly ICriteria<PreActAssessmentPage> SubmitBtnVisible = new Criteria<PreActAssessmentPage>(p =>
//        {
//            return p.Exists(Bys.PreActAssessmentPage.SubmitBtn, ElementCriteria.IsVisible);

//        }, "Submit button visible");

//        public readonly ICriteria<PreActAssessmentPage> LoadIconNotExists = new Criteria<PreActAssessmentPage>(p =>
//        {
//            return !p.Exists(Bys.LMSPage.LoadIcon);

//        }, "Load icon not exists");

//        public readonly ICriteria<PreActAssessmentPage> FirstQuestionLblVisible = new Criteria<PreActAssessmentPage>(p =>
//        {
//            return p.Exists(Bys.PreActAssessmentPage.FirstQuestionLbl, ElementCriteria.IsVisible);

//        }, "First question label visible");

//        public readonly ICriteria<PreActAssessmentPage> ContinueBtnEnabled = new Criteria<PreActAssessmentPage>(p =>
//        {
//            return p.Exists(Bys.PreActAssessmentPage.ContinueBtn, ElementCriteria.IsEnabled);

//        }, "Continue button enabled");

//        public readonly ICriteria<PreActAssessmentPage> YourStatusValueLblTextEqualsSubmitted = new Criteria<PreActAssessmentPage>(p =>
//        {
//            return p.Exists(Bys.PreActAssessmentPage.YourStatusValueLbl, ElementCriteria.Text("Submitted"));

//        }, "Your Status value label text equal to 'Submitted'");

//        public readonly ICriteria<PreActAssessmentPage> RetakeBtnVisible = new Criteria<PreActAssessmentPage>(p =>
//        {
//            return p.Exists(Bys.PreActAssessmentPage.RetakeBtn, ElementCriteria.IsVisible);

//        }, "Retake button visible");

//        public readonly ICriteria<PreActAssessmentPage> PageReady;

//        public PreActAssessmentPageCriteria()
//        {
//            PageReady = SubmitBtnVisible.AND(LoadIconNotExists).AND(FirstQuestionLblVisible);
//        }
//    }
//}
