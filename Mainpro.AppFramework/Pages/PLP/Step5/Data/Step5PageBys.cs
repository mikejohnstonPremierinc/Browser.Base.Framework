using OpenQA.Selenium;

namespace Mainpro.AppFramework
{
    /// <summary>
    /// Elements that will exist on the login page
    /// </summary>
    public class Step5PageBys
    {

        // Buttons
        public readonly By BackBtn = By.XPath("//span[text()='< Back']");
        public readonly By NextBtn = By.XPath("//div[contains(@class,'plpContinueButton')]//span[text()='Next >']/..");      
        public readonly By GoToBottomBtn = By.XPath("//span[contains(@class, 'goto-bottom')]");    
        public readonly By CollapseAllBtn = By.XPath("//button[text()='Collapse All']");
        public readonly By ExpandAllBtn = By.XPath("//button[text()='Expand All']");
        public readonly By PlusActivitiesBtn = By.XPath("//span[text()='+ Activities']");
        public readonly By YouWantToExitPopup_ExitBtn = By.XPath("//div[contains(@class,'preReflectExitButton')]//div[@title='Exit']");
        public readonly By YouWantToExitPopup_NoBtn = By.XPath("//div[contains(@class,'preReflectNoButton')]//div[@title='No']");
        public readonly By YouWantToExitPopupXBtn = By.XPath("//div[contains(@class,'prereflectionconfirmmodal')]//div[@aria-label='Close']");





        // Charts

        // Check boxes


        // Labels

        // Links

        // Menu Items    

        // Radio buttons

        // Tables       
        public readonly By PreReflectionCPDActivitiesTbl = By.XPath("//table[contains(@aria-labelledby, 'plpReflectionGrid')]");
        public readonly By PreReflectionCPDActivitiesTblHdr = By.XPath("//table[contains(@aria-labelledby, 'plpReflectionGrid')]//thead[not(contains(@aria-hidden, 'true'))]");
        public readonly By PreReflectionCPDActivitiesTblBody = By.XPath("//table[contains(@aria-labelledby, 'plpReflectionGrid')]//tbody");
        public readonly By PreReflectionCPDActivitiesTblEmptyBody = By.XPath("//table[@class='grid-table']//tbody");
        public readonly By PreReflectionCPDActivitiesTblBodyFirstRow = By.XPath("//table[contains(@aria-labelledby, 'plpReflectionGrid')]//tbody/tr");

        public readonly By UsefulCPDActivitiesTbl = By.XPath("//table[contains(@aria-labelledby, 'usefulcpdProgramsGrid')]");
        public readonly By UsefulCPDActivitiesTblHdr = By.XPath("//table[contains(@aria-labelledby, 'usefulcpdProgramsGrid')]//thead[not(contains(@aria-hidden, 'true'))]");
        public readonly By UsefulCPDActivitiesTblBody = By.XPath("//table[contains(@aria-labelledby, 'usefulcpdProgramsGrid')]//tbody");
        public readonly By UsefulCPDActivitiesTblBodyFirstRow = By.XPath("//table[contains(@aria-labelledby, 'usefulcpdProgramsGrid')]//tbody/tr");

        // Tabs

        // Text boxes
        public readonly By CPDActRecomYesCommentTxt = By.Name("CPDActivityRecommendationExplanation1");

         public readonly By CPDActRecomPartialCommentTxt = By.Name("CPDActivityRecommendationExplanation2");

         public readonly By CPDActRecomNoCommentTxt = By.Name("CPDActivityRecommendationExplanation3");


    }
}
