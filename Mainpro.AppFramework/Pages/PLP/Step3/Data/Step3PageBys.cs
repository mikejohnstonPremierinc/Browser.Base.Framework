using OpenQA.Selenium;

namespace Mainpro.AppFramework
{
    /// <summary>
    /// Elements that will exist on the login page
    /// </summary>
    public class Step3PageBys
    {

        // Buttons
        public readonly By BackBtn = By.XPath("//span[text()='< Back']");
        public readonly By NextBtn = By.XPath("//div[contains(@class,'plpContinueButton')]//span[text()='Next >']/..");      
        public readonly By GoToBottomBtn = By.XPath("//span[contains(@class, 'goto-bottom')]");    
        public readonly By CollapseAllBtn = By.XPath("//button[text()='Collapse All']");
        public readonly By ExpandAllBtn = By.XPath("//button[text()='Expand All']");
        public readonly By PlusActivitiesBtn = By.XPath("//span[text()='+ Activities']");
        public readonly By CPDEvents_MoreInfoDetailsBtn = By.XPath("//button[@aria-label='More Details']");
        public readonly By CPDEventsCalendarBtn = By.XPath("//div[@aria-label='CPD Events Calendar']");
        

        // Charts

        // Check boxes
        public readonly By Gap1Chk = By.XPath("//input[@name='Gap1GoalSelection']/following-sibling::span");
        public readonly By Gap2Chk = By.XPath("//input[@name='Gap2GoalSelection']/following-sibling::span");
        public readonly By Gap3Chk = By.XPath("//input[@name='Gap3GoalSelection']/following-sibling::span");


        // Labels
        public readonly By CPDEvents_Gap1Label = By.XPath("//span[text()='Gap 1: ']/following-sibling::span");
        public readonly By CPDEvents_Gap2Label = By.XPath("//span[text()='Gap 2: ']/following-sibling::span");
        public readonly By CPDEvents_Gap3Label = By.XPath("//span[text()='Gap 3: ']/following-sibling::span");
        public readonly By PLP_SummaryTable_Gap1Label = By.XPath("//span[text()='Gap 1: ']");
        public readonly By PLP_SummaryTable_Gap2Label = By.XPath("//span[text()='Gap 2: ']");
        public readonly By PLP_SummaryTable_Gap3Label = By.XPath("//span[text()='Gap 3: ']");
        public readonly By CPDEvents_MoreinfoDetails_SessionIdLabel = By.XPath("//span[contains(@class,'sessionId')]/following-sibling::span");
        

        // Links

        // Menu Items    

        // Radio buttons
        public readonly By AddAnotherGoalYesRdo = By.XPath("//span[text()='Add another goal?']/../descendant::label[1]");
        public readonly By AddAnotherGoalNoRdo = By.XPath("//span[text()='Add another goal?']/../descendant::label[2]");

        // Select Elements
        public readonly By ActivityDetailFormCategorySelElem = By.XPath("//div[contains(text(), 'Category')]/following-sibling::div//select");
        public readonly By ActivityDetailFormCategorySelElemBtn = By.XPath("//div[contains(text(), 'Category')]/following-sibling::div//button");
        public readonly By ActivityDetailFormProvinceSelElem = By.XPath("//div[contains(text(), 'Province')]/following-sibling::div//select");
        public readonly By ActivityDetailFormProvinceSelElemBtn = By.XPath("//div[contains(text(), 'Province')]/following-sibling::div//button");
        public readonly By ActivityDetailFormEventTypeSelElem = By.XPath("//div[contains(text(), 'Event type')]/following-sibling::div//select");
        public readonly By ActivityDetailFormEventTypeSelElemBtn = By.XPath("//div[contains(text(), 'Event type')]/following-sibling::div//button");
        public readonly By ActivityDetailFormGapsSelElem = By.XPath("//div[contains(text(), 'Gap(s)')]/following-sibling::div//select");
        public readonly By ActivityTblGapsSelElem = By.XPath("//select[@name='Gaps']");
        public readonly By ActivityDetailFormGapsSelElemBtn = By.XPath("//div[contains(text(), 'Gap(s)')]/following-sibling::div//button");


        // Tables       
        public readonly By CPDEventsTbl = By.XPath("//table[contains(@aria-labelledby, 'cpdSuggestionsGrid')]");
        public readonly By CPDEventsTblHdr = By.XPath("//table[contains(@aria-labelledby, 'cpdSuggestionsGrid')]//thead[not(contains(@aria-hidden, 'true'))]");
        public readonly By CPDEventsTblBody = By.XPath("//table[contains(@aria-labelledby, 'cpdSuggestionsGrid')]//tbody");
        public readonly By CPDEventsTblBodyFirstRow = By.XPath("//table[contains(@aria-labelledby, 'cpdSuggestionsGrid')]//tbody/tr");
        public readonly By SetYourGoalsSelectedActivitiesTbl = By.XPath("//table[contains(@aria-labelledby, 'plpActivitiesGrid')]");
        public readonly By SetYourGoalsSelectedActivitiesTblHdr = By.XPath("//table[contains(@aria-labelledby, 'plpActivitiesGrid')]//thead[not(contains(@aria-hidden, 'true'))]");
        public readonly By SetYourGoalsSelectedActivitiesTblBody = By.XPath("//table[contains(@aria-labelledby, 'plpActivitiesGrid')]//tbody");
        public readonly By SetYourGoalsSelectedActivitiesTblBodyFirstRow = By.XPath("//table[contains(@aria-labelledby, 'plpActivitiesGrid')]//tbody/tr");
         public readonly By PLPStep3IdentifiedGapsTbl = By.XPath("//table[contains(@aria-labelledby, 'IdentifiedGapsGrid')]");
        public readonly By PLPStep3IdentifiedGapsTblHdr = By.XPath("//table[contains(@aria-labelledby, 'IdentifiedGapsGrid')]//thead[not(contains(@aria-hidden, 'true'))]");
        public readonly By PLPStep3IdentifiedGapsTblBody = By.XPath("//table[contains(@aria-labelledby, 'IdentifiedGapsGrid')]//tbody");
        public readonly By PLPStep3IdentifiedGapsTblBodyFirstRow = By.XPath("//table[contains(@aria-labelledby, 'IdentifiedGapsGrid')]//tbody/tr");


        

        // Tabs

        // Text boxes
        public readonly By ActivityDetailFormActivityTitleTxt = By.XPath("//input[@aria-label='Activity Title']");
        public readonly By ActivityDetailFormDateTxt = By.XPath("//input[@aria-label='select day']");
        public readonly By ActivityDetailFormCityTxt = By.XPath("//input[@aria-label='City']");



    }
}
