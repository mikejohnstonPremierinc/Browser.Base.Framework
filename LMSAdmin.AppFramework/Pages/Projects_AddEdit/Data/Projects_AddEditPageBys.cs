using OpenQA.Selenium;

namespace LMSAdmin.AppFramework
{
    /// <summary>
    /// 
    /// </summary>
    public class Projects_AddEditPageBys
    {

        // Buttons
        public readonly By SaveBtn = By.XPath("//input[@value='Save']");

        // Charts

        // Check boxes

        // Labels                                              
        public readonly By AddProjectLbl = By.XPath("//p[contains(text(), 'Add Project')]");

        // Links

        // Menu Items    

        // Radio buttons

        // Tables       



        // Tabs

        // Text boxes
        public readonly By ProjectNameTxt = By.Id("ctl00_ProjectName");
        public readonly By ShortLabelTxt = By.Id("ctl00_ShortLabel");
        public readonly By DescriptionTxt = By.XPath("//span[@id='ctl00_DescValidator']/preceding-sibling::textarea");



    }
}