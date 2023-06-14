using OpenQA.Selenium;

namespace LMS.AppFramework
{
    /// <summary>
    /// This is where we locate our elements. Please use standard naming conventions and group your elements as shown below. Standard naming 
    /// conventions are defined here: https://code.premierinc.com/docs/display/PGHLMSDOCS/Best+Practices
    /// </summary>
    public class _PCGAPageBys
    {

        // Buttons

        // Charts

        // Check boxes

        // General

        // Labels                                              

        // Links
        public readonly By ProfessionalEduTblHdrLnks = By.XPath("(//span[contains(@id, 'UAMS')])[2]/../descendant::span//a[@href]");
        public readonly By ProfessionalEducation2TblHdrLnks = By.XPath("(//span[contains(@id, 'UAMS')])[3]/../descendant::span//a[@href]");
        public readonly By ProfessionalEducation3TblHdrLnks = By.XPath("(//span[contains(@id, 'UAMS')])[4]/../descendant::span//a[@href]");


        // Menu Items    

        // Radio buttons

        // Tables  
        public readonly By ProfessionalEduTbl = By.XPath("//div[@class='ActList2']");
        public readonly By ProfessionalEduTblFirstLnk = By.XPath("//div[@class='ActList2']//td/a | //div[@class='ActList2']//div[contains(text(), 'No recent activities')] | //div[@class='ActList2']//div[contains(text(), 'No custom list of activities to show.')]");
        public readonly By ProfessionalEducation3Tbl = By.XPath("//div[@class='ActList3']");
        public readonly By ProfessionalEducation3TblFirstLnk = By.XPath("//div[@class='ActList3']//td/a | //div[@class='ActList3']//div[contains(text(), 'No recent activities')] | //div[@class='ActList3']//div[contains(text(), 'No custom list of activities to show.')]");
        public readonly By ProfessionalEducation2Tbl = By.XPath("//div[@class='ActList1']");
        public readonly By ProfessionalEducation2TblFirstLnk = By.XPath("//div[@class='ActList1']//td/a | //div[@class='ActList1']//div[contains(text(), 'No recent activities')] | //div[@class='ActList1']//div[contains(text(), 'No custom list of activities to show.')]");


        // Tabs

        // Text boxes



    }
}