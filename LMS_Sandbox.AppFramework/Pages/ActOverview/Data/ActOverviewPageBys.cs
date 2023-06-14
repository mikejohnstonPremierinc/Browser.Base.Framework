using OpenQA.Selenium;

namespace LMS.AppFramework
{
    /// <summary>
    /// This is where we locate our elements. Please use standard naming conventions and group your elements as shown below. Standard naming 
    /// conventions are defined here: https://code.premierinc.com/docs/display/PGHLMSDOCS/Best+Practices
    /// </summary>
    public class ActOverviewPageBys
    {

        // Buttons
        public readonly By ContinueBtn = By.XPath("//body[contains(@class, 'activity_overview')]//span[text()='Continue']/..");
        public readonly By FinishBtn = By.XPath("//body[contains(@class, 'activity_overview')]//span[text()='Finish']/..");

        // Charts


        // Check boxes
        //public readonly By PleaseClickHereChk = By.XPath("//div[contains(text(), 'Please click here to confirm you have read the above information')]/..//input");
        public readonly By ActivityOverviewChk = By.XPath("//body[@class='fireball activity_overview']//input[@type='checkbox']");



        // Labels                                              
        public readonly By FrontMatterLbl = By.XPath("//div[contains(@class, 'activityDisclaimerContent')]//span | //div[contains(@class, 'content-section')]");
        public readonly By ConfirmWithCheckBoxLbl = By.XPath("//div[@class='fireball-widget activityDisclaimerConfirm']//div[contains(@class, 'label')]");
        public readonly By CityStateZipCountryLbl = By.XPath("//div[contains(@class, 'addr-line2')]");
        public readonly By StreetAddressLbl = By.XPath("//div[contains(@class, 'addr-line1')]");
        public readonly By StartDateValueLbl = By.XPath("//div[contains(text(), 'Start')]/following-sibling::div");
        public readonly By EndDateValueLbl = By.XPath("//div[contains(text(), 'End')]/following-sibling::div");
        public readonly By AccreditationBodyNameLbls = By.XPath("//div[contains(@class, 'credit')]/span[5]"); 
        public readonly By AccreditationBodyName_NONACCRLbl = By.XPath("//div[contains(@class, 'credit')]/span[@class='acc-body']"); 
        public readonly By ReleaseDateValueLbl = By.XPath("//h4[text()='Release Date']/following-sibling::div | //div[contains(@class, 'activity-details-container')]/div[2]/div[2]");
        public readonly By ExpirationDateValueLbl = By.XPath("//h4[text()='Expiration Date']/following-sibling::div | //div[contains(@class, 'activity-details-container')]/div[3]/div[2]");

        // Links

        // Menu Items    

        // Radio buttons

        // Tables               
        public readonly By AccreditationRows = By.XPath("//li[@data-fe-repeat='credit'] | //div[@data-fe-repeat='credit']");

        // Tabs

        // Text boxes



    }
}