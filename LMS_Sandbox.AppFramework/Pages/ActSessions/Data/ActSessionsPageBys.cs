using OpenQA.Selenium;

namespace LMS.AppFramework
{
    /// <summary>
    /// This is where we locate our elements. Please use standard naming conventions and group your elements as shown below. Standard naming 
    /// conventions are defined here: https://code.premierinc.com/docs/display/PGHLMSDOCS/Best+Practices
    /// </summary>
    public class ActSessionsPageBys
    {

        // Buttons
        public readonly By ContinueBtn = By.XPath("//body[contains(@class, 'activity_sessions')]//span[text()='Continue']/..");
        public readonly By FinishBtn = By.XPath("//body[contains(@class, 'activity_sessions')]//span[text()='Finish']/..");
        public readonly By ResetFiltersBtn = By.XPath("//div[contains(@class, 'sessionsGrid')]//span[text()='Reset Filters']");
        public readonly By GoToMySessionsBtn = By.XPath("//button[contains(text(), 'Go To My Sessions')]");
        public readonly By RegisterSessionsBtn = By.XPath("//button[contains(text(), 'Register Sessions')]");
        public readonly By SelectMoreSessionsBtn = By.XPath("//button[contains(text(), 'Select More Sessions')]");
        public readonly By ReturnToMySessionsBtn = By.XPath("//span[contains(text(), 'Return to My Sessions')]");
        public readonly By AccessCodeFormXBtn = By.XPath("");
        
        public readonly By AccessCodeFormContinueBtn = By.XPath("//button[@aria-label='Continue']");

        // Charts

        // Check boxes

        // General



        // Labels                                              
        public readonly By AccessCodeFormRequiredLbl = By.XPath("//div[@data-validation-msg='This field is required']|//div[@id='activitySessionRegistrationCodeForm-accessCode-validation']");

        // Links

        // Menu Items    

        // Radio buttons

        // Tables       
        public readonly By AvailableSessionsTbl = By.XPath("//div[contains(@class, 'sessionsGrid')]//table[@class='grid-table']");
        public readonly By AvailableSessionsTblBody = By.XPath("//div[contains(@class, 'sessionsGrid')]//table[@class='grid-table']/tbody");
        public readonly By AvailableSessionsTblBodyRow = By.XPath("//div[contains(@class, 'sessionsGrid')]//table[@class='grid-table']/tbody/tr");
        public readonly By AvailableSessionsTblSelectBtns = By.XPath("//div[contains(@class, 'sessionsGrid')]//table[@class='grid-table']/tbody//button");
        public readonly By SelectedSessionsTbl = By.XPath("//div[contains(@class, 'selected-session-list')]//table");
        public readonly By AssessmentListTbl = By.XPath("//div[@class='assessment-list']");




        // Tabs

        // Text boxes
        public readonly By AvailableSessionsTbl_SearchTxt = By.XPath("(//div[contains(@class, 'sessionsGrid')]//input)[1]");
        public readonly By AvailableSessionsTbl_FromDtTxt = By.XPath("(//div[contains(@class, 'sessionsGrid')]//input)[2]");
        public readonly By AvailableSessionsTbl_ToDtTxt = By.XPath("(//div[contains(@class, 'sessionsGrid')]//input)[3]");
        public readonly By AccessCodeFormAccessCodeTxt = By.XPath("//input[@name='accessCode']");

    }
}