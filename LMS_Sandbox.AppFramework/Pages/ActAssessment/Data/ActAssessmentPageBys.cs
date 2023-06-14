using OpenQA.Selenium;

namespace LMS.AppFramework
{
    /// <summary>
    /// 
    /// </summary>
    public class ActAssessmentPageBys
    {

        // Buttons
        public readonly By SubmitBtn = By.XPath("//body[contains(@class, 'activity_')]//button[text()='Submit'] | //span[text()='Submit']/..");
        public readonly By RetakeBtn = By.XPath("//body[contains(@class, 'activity_')]//button[text()='Retake'] | //span[text()='Retake']/..");
        public readonly By ContinueBtn = By.XPath("//body[contains(@class, 'activity_')]//span[text()='Continue']/..  | //span[text()='Continue']/..");
        public readonly By FinishBtn = By.XPath("//body[contains(@class, 'activity_')]//span[text()='Finish']/.. | //span[text()='Finish']/..");
        public readonly By LaunchBtn = By.XPath("//body[contains(@class, 'activity_')]//button[text()='Launch'] | //span[text()='Launch']/..");
        public readonly By NextBtn = By.XPath("//body[contains(@class, 'activity_')]//button[text()='Next'] | //span[text()='Next']/..");
        public readonly By SaveAndFinishLaterBtn = By.XPath("//body[contains(@class, 'activity_')]//button[text()='Save & Finish Later'] | //span[text()='Save & Finish Later']/..");
        public readonly By ReturnToSummaryBtn = By.XPath("//body[contains(@class, 'activity_')]//button[text()='Return to Summary'] | //span[text()='Return to Summary']/..");
        public readonly By Mobile_AssessmentListExpandBtn = By.XPath("//button[contains(@class, 'assessment-list-expand')]");
        public readonly By BackBtn = By.XPath("//body[contains(@class, 'activity_')]//button[text()='Back'] | //span[text()='Back']/..");


        // Charts

        // Check boxes

        //General
        public readonly By DisabledElems = By.XPath("//div[contains(@class, 'form-section')]/descendant::*[@disabled]");

        // Labels                                              
        public readonly By AssessmentNameLbl = By.XPath("//body[contains(@class, 'activity_')]//div[@class='assessment-list']//h3");
        public readonly By YourStatusValueLbl = By.XPath("descendant::div[text()='Your Status']/following-sibling::div");
        public readonly By YourScoreValueLbl = By.XPath("descendant::div[text()='Your Score']/following-sibling::span|div");
        public readonly By AttemptValueLbl = By.XPath("descendant::span[text()='Attempt:']/following-sibling::span|div");
        public readonly By NumberOfAttemptsRemainingLbl = By.XPath("descendant::span[text()='Attempt:']/following-sibling::span[contains(text(), 'attempts')]");
        public readonly By StatusPassFailLbls = By.XPath("descendant::div[@data-fe-repeat='assessmentCredits']/div[4]");
        public readonly By QuestionTextLbls = By.XPath("//body[contains(@class, 'activity_')]//div[@class='form-input-label control-label']");
        public readonly By QuestionTextHiddenLbls = By.XPath("//body[contains(@class, 'activity_')]//div[contains(@class, 'form-input-skip')]//div[@class='form-input-label control-label']");
        public readonly By QuestionTextNotHiddenLbls = By.XPath("//body[contains(@class, 'activity_')]//div[not(contains(@class, 'form-input-skip'))]/div/div[@class='form-input-label control-label']");
        public readonly By ThisFieldIsRequiredLbls = By.XPath("//div[@data-validation-msg='This field is required']|//div[text()='This field is required']");
        public readonly By FeedbackLbls = By.XPath("//div[@class='option-feedback']");


        // Links


        // Menu Items    

        // Radio buttons

        // Tables       

        // Tabs

        // Text boxes

    }
}