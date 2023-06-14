using OpenQA.Selenium;

namespace Mainpro.AppFramework
{
    /// <summary>
    /// Elements that will exist on the login page
    /// </summary>
    public class Step2PageBys
    {

        // Buttons
        public readonly By BackBtn = By.XPath("//span[text()='< Back']");
        public readonly By NextBtn = By.XPath("//div[contains(@class,'plpContinueButton')]//span[text()='Next >']/..");   
        public readonly By GapNextBtn = By.XPath("//div[contains(@class,'step2Next')]//div[@role='button']");   
        public readonly By GapScreenViewModeNextBtn = By.XPath("//div[contains(@class,'step21Next')]//div[@role='button']");   


        public readonly By GoToBottomBtn = By.XPath("//span[contains(@class, 'goto-bottom')]");    
        public readonly By CollapseAllBtn = By.XPath("//button[text()='Collapse All']");
        public readonly By ExpandAllBtn = By.XPath("//button[text()='Expand All']");
        public readonly By GapsContinueBtn = By.XPath("//div[contains(@class,'identifyGapsConfirmButton')]//span");
        public readonly By AdditionalGapBtn = By.XPath("//span[text()='Additional Gap']/..");
        public readonly By GapDeleteBtn = By.XPath("//div[@class='button-icon glyphicon glyphicon-minus center left']/..");
        public readonly By GapEditBtn = By.XPath("//div[contains(@class,'EditButton')]//div[@title='Click to Edit']");
        public readonly By Gap1EditBtn = By.XPath("//div[contains(@class,'gap1EditButton')]/div[@role='button']");
        public readonly By Gap2EditBtn = By.XPath("//div[contains(@class,'gap2EditButton')]/div[@role='button']");
        public readonly By Gap3EditBtn = By.XPath("//div[contains(@class,'gap3EditButton')]/div[@role='button']");
        public readonly By Gap1DeleteBtn = By.XPath("//div[contains(@class,'gap1RemoveButton')]/div[@role='button']");
        public readonly By Gap2DeleteBtn = By.XPath("//div[contains(@class,'gap2RemoveButton')]/div[@role='button']");
        public readonly By Gap3DeleteBtn = By.XPath("//div[contains(@class,'gap3RemoveButton')]/div[@role='button']");



        // Charts

        // Check boxes
    
        // General


        // Labels
        public readonly By OtherDataToAccessYourPracticeNeeds_Label = By.XPath("//div[text()='What other data could be helpful to you to assess your practice needs?']");

        // Links

        // Menu Items    

        // Radio buttons

        // Select Elements
        public readonly By SelectDomainOfCareSelElem = By.XPath("//div[contains(text(), 'Select domain of care')]/following-sibling::div//select");
        public readonly By SelectDomainOfCareSelElemBtn = By.XPath("//div[contains(text(), 'Select domain of care')]/following-sibling::div//button");
        public readonly By SelectSubsetsSelElem = By.XPath("//div[contains(text(), 'Select subdomain(s)')]/following-sibling::div//select");
        public readonly By SelectSubsetsSelElemBtn = By.XPath("//div[contains(text(), 'Select subdomain(s)')]/following-sibling::div//button");

        // Tables       

        // Tabs

        // Text boxes
        public readonly By OtherDataTxt = By.XPath("//div[text()='What other data could be helpful to you to assess your practice needs?']/following-sibling::div/textarea");



    }
}
