
using OpenQA.Selenium;


namespace Mainpro.AppFramework
{
    public class EnterACPDActivityPageBys
    {
        // Buttons
        public readonly By ContinueBtn = By.XPath("//div[contains(@class, 'ActivitySearchForm')]//span[text()='CONTINUE']");
        public readonly By SearchBtn = By.XPath("//span[text()='SEARCH']");
        public readonly By CloseBtn = By.XPath("//span[text()='Close']");
        public readonly By ClickHereBtn = By.XPath("//span[text()='Click Here']");
        public readonly By ResetSearchCriteraBtn = By.XPath("//span[text()='Reset search criteria']");
        public readonly By DoYouKnowYourSessionIDContinueBtn = By.XPath("//div[contains(@class, 'SessionIdContinueButton')]//span");
        public readonly By SupportInfoFormCloseBtn = By.XPath("//div[contains(@class, 'supportInfoClose')]//div[@title='Close']");
        public readonly By MaxCreditReachedFormAddNonCertActBtn = By.XPath("//div[contains(@class,'addNonCertifiedActivityButton1stStep')]//span[text()='ADD NON-CERTIFIED ACTIVITY']");
        public readonly By MaxCreditReachedFormStartOverBtn = By.XPath("//div[@class='overlay']//span[text()='START OVER']|//span[text()='START OVER']");


        // Charts

        // Check boxes

        // Labels                                              
        public readonly By SupportInfoFormSupportInfoLbl = By.XPath("//h2[text()='Support Information']");
        //public readonly By MaxCreditReachedFormClaimedLbl = By.XPath("//div[@class='overlay']//span[contains(text(), 'credits claimed')]/following-sibling::span");
        public readonly By MaxCreditReachedFormClaimedLbl = By.XPath("//span[contains(text(), 'Total Royal College credits claimed :')]/following-sibling::span|//span[contains(text(), 'Total AMA credits claimed :')]/following-sibling::span");


        // Links
        public readonly By NeedHelpLnk = By.XPath("//span[text()='Need Help?']");


        // Menu Items    

        // Radio buttons
        public readonly By LiveInPersonRdo = By.XPath("//div[not(@class='fireball-widget ecpdActivitySearchForm hidden')]/div/div/form//input[contains(@id, 'deliveryFormat-0')]");
        public readonly By OnlineRdo = By.XPath("//div[not(@class='fireball-widget ecpdActivitySearchForm hidden')]/div/div/form//input[contains(@id, 'deliveryFormat-1')]");
        public readonly By CertifiedRdo = By.XPath("//div[not(@class='fireball-widget ecpdActivitySearchForm hidden')]/div/div/form//input[contains(@id, 'certificationType-0')]");
        public readonly By NonCertifiedRdo = By.XPath("//div[not(@class='fireball-widget ecpdActivitySearchForm hidden')]/div/div/form//input[contains(@id, 'certificationType-1')]");
        public readonly By IAgreeRdo = By.XPath("//div[not(@class='fireball-widget ecpdActivitySearchForm hidden')]/div/div/form//input[contains(@id, 'ecpdActivitySearchForm-nonProfitDisclaimer-0')]");

        // Select Element
        public readonly By CategorySelElem = By.XPath("//div[not(@class='fireball-widget ecpdActivitySearchForm hidden')]/div/div/form//div[contains(text(), 'Category:')]/following-sibling::div//select");
        public readonly By CategorySelElemBtn = By.XPath("//div[not(@class='fireball-widget ecpdActivitySearchForm hidden')]/div/div/form//div[contains(text(), 'Category:')]/following-sibling::div//button");
        public readonly By ActivityTypeSelElem = By.XPath("//div[not(@class='fireball-widget ecpdActivitySearchForm hidden')]/div/div/form//div[contains(text(), 'Activity Type:')]/following-sibling::div//select");
        public readonly By ActivityTypeSelElemBtn = By.XPath("//div[not(@class='fireball-widget ecpdActivitySearchForm hidden')]/div/div/form//div[contains(text(), 'Activity Type:')]/following-sibling::div//button");
        public readonly By ArticleSelElem = By.XPath("//div[contains(text(), 'Article')]/following-sibling::div//select");
        public readonly By ArticleSelElemBtn = By.XPath("//div[contains(text(), 'Article')]/following-sibling::div//button");
        public readonly By VolumeSelElem = By.XPath("//div[contains(text(), 'Volume')]/following-sibling::div//select");
        public readonly By VolumeSelElemBtn = By.XPath("//div[contains(text(), 'Volume')]/following-sibling::div//button");
        public readonly By QuestionsSelElem = By.XPath("//div[contains(text(), 'questions')]/following-sibling::div//select");
        public readonly By QuestionsSelElemBtn = By.XPath("//div[contains(text(), 'questions')]/following-sibling::div//button");

        // Tables 
        public readonly By SearchResultsTbl = By.XPath("//div[contains(@class, 'SearchResultContainer')]//table[@class='grid-table']");
        public readonly By SearchResultsTblHdr = By.XPath("//div[contains(@class, 'SearchResultContainer')]//table[@class='grid-table']/thead");
        public readonly By SearchResultsTblBody = By.XPath("//div[contains(@class, 'SearchResultContainer')]//table[@class='grid-table']//tbody");
        public readonly By SearchResultsTblFirstRow = By.XPath("//div[contains(@class, 'SearchResultContainer')]//table[@class='grid-table']/tbody/tr");
        public readonly By SearchResultsTblFirstCol= By.XPath("//div[contains(@class, 'SearchResultContainer')]//table[@class='grid-table']/tbody/tr/td");


        public readonly By SearchResultsTblProgActTitleColHdr = By.XPath("//div[contains(@class, 'SearchResultGrid')]//span[text()='Program/Activity Title']");
        public readonly By SearchResultsTblSessionIDColHdr = By.XPath("//div[contains(@class, 'SearchResultGrid')]//span[text()='Session ID']");
        public readonly By SearchResultsTblCityColHdr = By.XPath("//div[contains(@class, 'SearchResultGrid')]//span[text()='City']");

        public readonly By SearchResultsTblTooManyResultsLbl = By.XPath("//table[@class='grid-table']//td[contains(text(), 'Please enter search criteria in Step 2 then click the')]");
        public readonly By SearchResultsTblNoResultsLbl = By.XPath("//td[contains(text(), 'No items')]");


        // Tabs

        // Text boxes
        public readonly By DoYouKnowYourSessionIDSearchTxt = By.XPath("//div[contains(@class, 'SessionIdInput')]/input");
        public readonly By ProgramActivityTitleTxt = By.XPath("//input[@aria-label='Program/Activity Title:']");
        public readonly By SessionIDTxt = By.XPath("//input[@aria-label='Session ID:']");
        public readonly By CityTxt = By.XPath("//input[@aria-label='City:']");
        public readonly By ActivityDateTxt = By.XPath("//input[@aria-label='select day']");
    }
}
