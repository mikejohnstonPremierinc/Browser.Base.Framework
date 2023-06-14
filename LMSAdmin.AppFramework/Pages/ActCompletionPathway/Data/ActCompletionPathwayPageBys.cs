using OpenQA.Selenium;

namespace LMSAdmin.AppFramework
{
    /// <summary>
    /// 
    /// </summary>
    public class ActCompletionPathwayPageBys
    {

        // Buttons 
        public readonly By ScenarioSettingsSaveBtn = By.XPath("//div[contains(@class,'saveScenarioButton')]//button[@aria-label='SAVE']");
        public readonly By DeliverySettingsSaveBtn = By.XPath("//div[contains(@class,'delivery')]//button[@aria-label='SAVE']");
        public readonly By AsessmentDisplayAllBtn = By.XPath("//div[@title='DISPLAY ALL']");
        // Charts

        // Check boxes
        public readonly By AssessmentDisplayChkbox = By.XPath("//input[contains(@name,'display')]");

        // General
       

        // Labels                                                   
        public readonly By ScenarioSettingsTitleLbl = By.XPath("//div/p[@class='title-bar' and text()='Scenario Settings']");
        public readonly By DeliverySettingsTitleLbl = By.XPath("//div/p[@class='title-bar' and text()='Delivery Settings']");


        // Links

        // Menu Items    

        // Radio buttons

        // select elements
        public readonly By OrderOfAssessmentSelElemDropdown = By.XPath(".//select[contains(@name,'order')]");
        public readonly By OrderOfAssessmentSelElemDropdownBtn =  By.XPath(".//button[contains(@aria-labelledby,'order')]");
        public readonly By CompletionRequiredSelElemDropdownBtn = By.XPath(".//button[contains(@aria-labelledby,'completionRequired')]");
        public readonly By NumOfGradedQuesToPassSelElemDropdownBtn = By.XPath(".//button[contains(@aria-labelledby,'pass')]");
        public readonly By ActionTriggerSelElemDropdown = By.XPath(".//select[contains(@name,'action')]");
        public readonly By ActionTriggerSelElemDropdownBtn = By.XPath(".//button[contains(@aria-labelledby,'action')]");
        public readonly By ConditionTypeSelElemDropdown = By.XPath(".//select[contains(@name,'condition')]");
        public readonly By ConditionTypeSelElemDropdownBtn = By.XPath(".//button[contains(@aria-labelledby,'condition')]");
        public readonly By TimingTypeSelElemDropdown = By.XPath(".//select[contains(@name,'timing')]");
        public readonly By TimingTypeSelElemDropdownBtn = By.XPath(".//button[contains(@aria-labelledby,'timing')]");
        public readonly By UnitsTypeSelElemDropdown = By.XPath(".//select[contains(@name,'units')]");
        public readonly By UnitsTypeSelElemDropdownBtn = By.XPath(".//button[contains(@aria-labelledby,'units')]");
        public readonly By AssessmentNotificationSelElemDropdown = By.XPath(".//select[contains(@name,'assessmentNot')]");
        public readonly By AssessmentNotificationSelElemDropdownBtn = By.XPath(".//button[contains(@aria-labelledby,'assessmentNot')]");
        public readonly By AssessmentNotification_AssessmentSelElemDropdownBtn = By.XPath(".//button[contains(@aria-labelledby,'assessment')]");

        // Tables   
        public readonly By ScenarioSettingsAssessmentsTbl = By.XPath("//div[contains(@class,'scenarioSettingsAssessmentData')]//table");
        public readonly By ScenarioSettingsAssessmentsTblBodyRow = By.XPath("//div[contains(@class,'scenarioSettingsAssessmentData')]//table/tbody/tr");
        public readonly By DeliverySettingsAssessmentsTbl = By.XPath("//div[contains(@class,'deliveryAssessmentData')]//table");
        public readonly By DeliverySettingsAssessmentsTblBodyRow = By.XPath("//div[contains(@class,'deliveryAssessmentData')]//table/tbody/tr");


        // Tabs
        public readonly By ScenarioSettingsTab = By.XPath("//div[@data-toggle-value='scenarioSettingsContainer']");
        public readonly By DeliverySettingsTab = By.XPath("//div[@data-toggle-value='deliverySettingsContainer']");


        // Text boxes
        public readonly By NumOfAttemptsAllowedTxt = By.XPath(".//input[@aria-label='# ATTEMPTS ALLOWED']");
        public readonly By NumberofUnitsTxt = By.XPath(".//input[@aria-label='Number']");
    }
}