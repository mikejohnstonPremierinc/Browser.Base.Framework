using OpenQA.Selenium;

namespace Mainpro.AppFramework
{
    /// <summary>
    /// Elements that will exist on the login page
    /// </summary>
    public class Step1PageBys
    {

        // Buttons
        public readonly By BackBtn = By.XPath("//span[text()='< Back']/..");
        public readonly By NextBtn = By.XPath("//div[contains(@class,'plpContinueButton')]//span[text()='Next >']/..");
        public readonly By InfoBtn = By.XPath("//img[@class='info-icon']");

        public readonly By GoToBottomBtn = By.XPath("//span[contains(@class, 'goto-bottom')]");
        public readonly By LeaderAdminPlusBtn = By.XPath("(//span[@class='sprit_ico']/img[contains(@src,'plus')])[1]");
        public readonly By ScholarshipEduPlusBtn = By.XPath("(//span[@class='sprit_ico']/img[contains(@src,'plus')])[2]");
        public readonly By ScholarshipQualityPlusBtn = By.XPath("(//span[@class='sprit_ico']/img[contains(@src,'plus')])[3]");
        public readonly By ResearchPlusBtn = By.XPath("(//span[@class='sprit_ico']/img[contains(@src,'plus')])[4]");
        public readonly By ClinicalCarePlusBtn = By.XPath("(//span[@class='sprit_ico']/img[contains(@src,'plus')])[5]");

        public readonly By LeaderAdminMinusBtn = By.XPath("(//span[@class='sprit_ico']/img[contains(@src,'plus')])[1]");
        public readonly By ScholarshipEduMinusBtn = By.XPath("(//span[@class='sprit_ico']/img[contains(@src,'plus')])[2]");
        public readonly By ScholarshipQualityMinusBtn = By.XPath("(//span[@class='sprit_ico']/img[contains(@src,'plus')])[3]");
        public readonly By ResearchMinusBtn = By.XPath("(//span[@class='sprit_ico']/img[contains(@src,'plus')])[4]");
        public readonly By ClinicalCareMinusBtn = By.XPath("(//span[@class='sprit_ico']/img[contains(@src,'plus')])[5]");

        public readonly By CollapseAllBtn = By.XPath("//button[contains(text(),'Collapse All')]");
        public readonly By ExpandAllBtn = By.XPath("//button[contains(text(),'Expand All')]");




        // Charts
        // See the following for SVG elements: https://stackoverflow.com/questions/31520642/how-to-use-xpath-in-selenium-webdriver-to-grab-svg-elements
        public readonly By TimeBreakdownGraphLabel = By.XPath("//div[contains(@class, 'timeBreakdownChart')]//*[name()='svg']//*[@class='custom value']");
        public readonly By TimeBreakdownGraphGreyColorLabel = By.XPath("//div[contains(@class, 'timeBreakdownChart')]//*[name()='svg']//*[@class='donut-layer']//*[@fill='#D3D3D3']");

        // Check boxes


        // Labels

        // Links

        // Menu Items    

        // Radio buttons

        // Tables       

        // Tabs

        // Text boxes
        public readonly By Txt = By.XPath("");




    }
}
