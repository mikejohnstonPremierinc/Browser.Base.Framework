using OpenQA.Selenium;

namespace LMS.AppFramework
{
    /// <summary>
    /// This is where we locate our elements. Please use standard naming conventions and group your elements as shown below. Standard naming 
    /// conventions are defined here: https://code.premierinc.com/docs/display/PGHLMSDOCS/Best+Practices
    /// </summary>
    public class ActPreviewPageBys
    {

        // Buttons
        public readonly By RegisterBtn = By.XPath("//span[text()='Register']|//a[text()='Register']|//button[text()='Register']");
        public readonly By ResumeBtn = By.XPath("//span[text()='Resume']|//a[text()='Resume']|//button[text()='Resume']");
        public readonly By LaunchBtn = By.XPath("//span[text()='Launch']|//a[text()='Launch']|//button[text()='Launch']");
        public readonly By NotAvailableBtn = By.XPath("//span[text()='Not Available']|//a[text()='Not Available']|//button[text()='Not Available']");
        public readonly By LaunchOrRegisterOrResumeBtn = By.XPath("//*[text()='Launch']|//*[text()='Register']|//*[text()='Resume']");
        public readonly By ActivityMaterialTabBtn = By.XPath("//div[contains(@class, 'startActivitySummaryTable')]//span[text()='Activity Material']/ancestor::tr//div[contains(@class, 'button-icon')]");
        public readonly By AccessCodeFormContinueBtn = By.XPath("//button[@aria-label='Continue']");



        // Charts

        // Check boxes

        // General       


        // Labels                                                      
        public readonly By IncTheseActsTab_RequiredLbls = By.XPath("//span[@class='required-true']");                                         
        public readonly By FrontMatterLbl = By.XPath("//div[contains(@class, 'activityDisclaimerContent')]//span");
        public readonly By CityStateZipCountryLbl = By.XPath("//div[contains(@class, 'addr-line2')]");
        public readonly By StreetAddressLbl = By.XPath("//div[contains(@class, 'addr-line1')]");
        public readonly By StartDateValueLbl = By.XPath("//div[contains(text(), 'Start')]/following-sibling::div");
        public readonly By EndDateValueLbl = By.XPath("//div[contains(text(), 'End')]/following-sibling::div");
        public readonly By AccreditationBodyNameLbls = By.XPath("//div[contains(@class, 'credit')]/span[5]");
        public readonly By ReleaseDateValueLbl = By.XPath("//h4[text()='Release Date']/following-sibling::div | //div[contains(@class, 'activity-details-container')]/div[2]/div[2]");
        public readonly By ExpirationDateValueLbl = By.XPath("//h4[text()='Expiration Date']/following-sibling::div | //div[contains(@class, 'activity-details-container')]/div[3]/div[2]");
        public readonly By ActivityMaterialFileExtensionLbls = By.XPath("//tr[contains(@class, 'child')]//span");
        public readonly By AccessCodeFormRequiredLbl = By.XPath("//div[@data-validation-msg='This field is required']|//div[contains(@id, 'accessCode-validation')] | //div[@data-validation-msg='This field is required']|//div[@id='activitySessionRegistrationCodeForm-accessCode-validation']");
        public readonly By PageWarningMessageLbl = By.XPath("//span[@class='notification-message']");


        // Links

        // Menu Items    


        // Radio buttons


        // Tables       
        public readonly By IncTheseActsTab_BundlesTbl = By.XPath("//div[contains(@class, 'activityBundleTable')]//table");
        public readonly By IncTheseActsTab_BundlesTblBody = By.XPath("//div[contains(@class, 'activityBundleTable')]//tbody");
        public readonly By IncTheseActsTab_BundlesTblBodyActivityLnks = By.XPath("//div[contains(@class, 'activityBundleTable')]//tbody//a | //div[contains(@class, 'activityBundleTable')]//tbody//td");
        public readonly By AccreditationRows = By.XPath("//*[@data-fe-repeat='credit']");




        // Tabs
        public readonly By InclTheseActsTab = By.XPath("//div[contains(@class, 'activityToggleBar')]//span[text()='Includes these activities']");

        

        // Text boxes
        public readonly By ActivityTypeLbl = By.XPath("//div[text()='Activity Type']");
        public readonly By AccessCodeFormAccessCodeTxt = By.XPath("//input[@name='accessCode']");


    }
}