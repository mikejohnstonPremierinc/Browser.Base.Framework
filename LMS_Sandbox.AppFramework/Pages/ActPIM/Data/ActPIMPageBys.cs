using OpenQA.Selenium;

namespace LMS.AppFramework
{
    /// <summary>
    /// This is where we locate our elements. Please use standard naming conventions and group your elements as shown below. Standard naming 
    /// conventions are defined here: https://code.premierinc.com/docs/display/PGHLMSDOCS/Best+Practices
    /// </summary>
    public class ActPIMPageBys
    {

        // Buttons
        public readonly By ContinueBtn = By.XPath("//body[contains(@class, 'activity_pim')]//div[contains(@class, 'pim')]//span[text()='Continue']/..");
        public readonly By FinishBtn = By.XPath("//body[contains(@class, 'activity_pim')]//div[contains(@class, 'pim')]//span[text()='Finish']/..");
        public readonly By SubmitBtn = By.XPath("//body[contains(@class, 'activity_pim')]//div[contains(@class, 'pim')]//span[text()='Submit']/..");
        public readonly By SubmitDataBtn = By.XPath("//body[contains(@class, 'activity_pim')]//div[contains(@class, 'pim')]//span[text()='Submit Data']/..");
        public readonly By DataSubmittedBtn = By.XPath("//body[contains(@class, 'activity_pim')]//div[contains(@class, 'pim')]//span[text()='Data Submitted']/..");

        // Charts

        // Check boxes
        public readonly By ClaimCreditChks = By.XPath("//body[contains(@class, 'activity_pim')]//div[contains(@class, 'pim')]//input[@type='checkbox']");

        
        // General
        public readonly By EvaluationRdoBtnGroups = By.XPath("//div[@class='form-input-row-inner']//input[@type='radio']/ancestor::div[@class='form-input']");
        public readonly By VideoPlayers = By.XPath("//source[@type='video/mp4']/ancestor::video[1]");
        public readonly By SectionCompletionGreenCheckIcons = By.XPath("//i[@class='right icon-check']");


        

        // Labels                                              
        public readonly By ContentHdr = By.XPath("//div[contains(@class, 'activityDisclaimerContent')]//span | //div[contains(@class, 'content-section')]");
        //public readonly By ConfirmWithCheckBoxLbl = By.XPath("//div[@class='fireball-widget activityDisclaimerConfirm']//div[contains(@class, 'label')]");
        public readonly By AssessmentNameLbl = By.XPath("//body[contains(@class, 'activity_')]//div[@class='activitybox']/h2");
        public readonly By YourScoreLbl = By.XPath("//p[contains(., 'Your score')]");
        public readonly By CalculationsInProgressLbl = By.XPath("//td[contains(text(), 'Calculations in progress')]");
        public readonly By OverallTestGroupScoreLbl = By.XPath("//h2[contains(text(), 'Overall Test')]");
        public readonly By SectionNameLbl = By.XPath("//body[contains(@class, 'activity_')]//div[@class='fireball-app-view']//h2");
        public readonly By ThisFieldIsRequiredLbls = By.XPath("//div[@data-validation-msg='This field is required']|//div[text()='This field is required']");
        public readonly By PatientScorePercentageLbl = By.XPath("//div[@class='content-section ']//p");
        public readonly By OverallTestGroupScoreViewCumulativeScoreLbl = By.XPath("//*[contains(text(),'Cumulative Score')]");


        
        // Links

        // Menu Items    

        // Radio buttons
        public readonly By YesIAmSatisfiedRdo = By.XPath("//span[contains(text(), 'Yes, I am satisfied with my score')]");

        // Select Elements
        public readonly By SelectCreditSelElemBtn = By.XPath("//body[contains(@class, 'activity_pim')]//div[contains(@class, 'pim')]//button");
        public readonly By SelectCreditSelElem = By.XPath("//body[contains(@class, 'activity_pim')]//div[contains(@class, 'pim')]//select");

        // Tables               

        // Tabs

        // Text boxes



    }
}