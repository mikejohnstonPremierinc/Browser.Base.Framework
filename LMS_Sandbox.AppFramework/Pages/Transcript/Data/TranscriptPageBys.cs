using OpenQA.Selenium;

namespace LMS.AppFramework
{
    /// <summary>
    /// Elements that will exist on the Transcript page
    /// </summary>
    public class TranscriptPageBys
    {

        // Buttons
        public readonly By FilterByDateBtn = By.XPath("//div[@title='Filter By Date']/span");
        public readonly By ResetFiltersBtn = By.XPath("//div[@title='Reset Filters']/span");
        public readonly By MyActivityBtn = By.XPath("//div[@title='My Activity']/span");
        public readonly By PrintHoldingAreaReportBtn = By.XPath("//div[contains(@class, 'transcriptHoldingAreaPrintBtn')]//div[@title='Print Report']/span");
        public readonly By ActivityDetailsFormAddBtn = By.XPath("//div[contains(@class, 'addYourActivityForm')]//button[@aria-label='Add']");
        public readonly By DeleteBtn = By.XPath("//div[contains(@class, 'trash')]");
        public readonly By PrintHoldingAreaReportFormRunReportBtn = By.XPath("//input[@value='Run Report']/..");
        public readonly By PrintHoldingAreaReportFormCloseBtn = By.XPath("//a[text()='Close']");
        public readonly By ConfirmFormOkBtn = By.XPath("//span[@data-bind-value='confirmButtonLabel']");
        public readonly By PrintReportBtn = By.XPath("//div[@title='Print Report']/span");

        // Charts

        // Check boxes


        // General
        public readonly By ActDetailsFormUploadElem = By.XPath("//div[contains(@class, 'addYourActivityForm')]//input[@aria-label='Upload Supporting Document']");
        public readonly By PrintHoldingAreaReportIFrame = By.XPath("//iframe[@title='Print Holding Area Report']");

        


        // Labels                                              
        public readonly By TranscriptLbl = By.XPath("//h2[contains(text(), 'Transcript')]");
        public readonly By YouDoNotHaveAnyComplActsLbl = By.XPath("//td[text()='You do not have any completed activities']");
        public readonly By FromDateLbl = By.XPath("//span[contains(@class, 'date-picker-format') and contains(@id, 'from')]");
        public readonly By ToDateLbl = By.XPath("//span[contains(@class, 'date-picker-format') and contains(@id, 'to')]");


        

        // Links
        public readonly By AddAnActivityLnk = By.XPath("//div[@title='add an activity']/span");


        // Menu Items    

        // Radio buttons
        public readonly By ActDetailsFormLocationOnlineRdo = By.XPath("//div[contains(@class, 'addYourActivityForm')]//input[@value='online']");
        public readonly By ActDetailsFormLocationLiveRdo = By.XPath("//div[contains(@class, 'addYourActivityForm')]//input[@value='live']");

        // Select Elements
        public readonly By ActDetailsFormAccreditationProviderSelElem = By.XPath("//div[contains(@class, 'addYourActivityForm')]//select[contains(@name, 'accreditationProvider')]");
        public readonly By ActDetailsFormAccreditationProviderSelElemBtn = By.XPath("//div[contains(@class, 'addYourActivityForm')]//button[contains(@aria-labelledby, 'accreditationProvider')]");
        public readonly By ActDetailsFormUnitsSelElem = By.XPath("//div[contains(@class, 'addYourActivityForm')]//select[contains(@name, 'unitId')]");
        public readonly By ActDetailsFormUnitsSelElemBtn = By.XPath("//div[contains(@class, 'addYourActivityForm')]//button[contains(@aria-labelledby, 'unitId')]");

        // Tables       
        public readonly By ActivitiesTbl = By.XPath("//table[@class='grid-table']");
        public readonly By ActivitiesTblBody = By.XPath("//table[@class='grid-table']//tbody");
        public readonly By ActivitiesTblFirstLnk = By.XPath("//table[@class='grid-table']//td[2]/ancestor::tr");
        public readonly By ActivitiesTblExpandBtns = By.XPath("//button[contains(@class, 'grid-expand-button')]/div");



        // Tabs
        public readonly By HoldingAreaTab = By.XPath("//span[text()='Holding Area']");

        // Text boxes
        public readonly By ActDetailsFormActNameTxt = By.XPath("//div[contains(@class, 'addYourActivityForm')]//input[@aria-label='Activity Name']");
        public readonly By ActDetailsFormCompletedDateTxt = By.XPath("//div[contains(@class, 'addYourActivityForm')]//input[@name='completeDate']");
        public readonly By ActDetailsFormReferenceTxt = By.XPath("//div[contains(@class, 'addYourActivityForm')]//input[@aria-label='Reference #']");
        public readonly By ActDetailsFormCreditAmountTxt = By.XPath("//div[contains(@class, 'addYourActivityForm')]//input[contains(@aria-labelledby, 'creditAmount')]");
        public readonly By ActDetailsFormCreditTypeTxt = By.XPath("//div[contains(@class, 'addYourActivityForm')]//input[contains(@aria-labelledby, 'creditType')]");
        public readonly By ActDetailsFormCityTxt = By.XPath("//div[contains(@class, 'addYourActivityForm')]//input[contains(@aria-labelledby, 'city')]");


        //div[contains(@class, 'addYourActivityForm')]//input[@aria-label='Activity Name']




    }
}