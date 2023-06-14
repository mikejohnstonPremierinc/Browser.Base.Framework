using OpenQA.Selenium;

namespace LMS.AppFramework
{
    /// <summary>
    /// This is where we locate our elements. Please use standard naming conventions and group your elements as shown below. Standard naming 
    /// conventions are defined here: https://code.premierinc.com/docs/display/PGHLMSDOCS/Best+Practices
    /// </summary>
    public class _HRSAPageBys
    {

        // Buttons

        // Charts

        // Check boxes

        // General



        // Labels                                              

        // Links
        public readonly By GeriatricKnowledgeTblHdrLnks = By.XPath("(//span[contains(@id, 'UAMS')])[2]/../descendant::span//a[@href]");
        public readonly By InterprofessionalEducationTblHdrLnks = By.XPath("(//span[contains(@id, 'UAMS')])[3]/../descendant::span//a[@href]");
        public readonly By EducationCompetenciesTblHdrLnks = By.XPath("(//span[contains(@id, 'UAMS')])[4]/../descendant::span//a[@href]");


        // Menu Items    

        // Radio buttons

        // Tables  
        public readonly By GeriatricKnowledgeTbl = By.XPath("//div[@class='ActList2']");
        public readonly By GeriatricKnowledgeTblFirstLnk = By.XPath("//div[@class='ActList2']//td/a | //div[@class='ActList2']//div[contains(text(), 'No custom list of activities') or contains(text(), 'No recent activities')]");
        public readonly By InterprofessionalEducationTbl = By.XPath("//div[@class='ActList1']");
        public readonly By InterprofessionalEducationTblFirstLnk = By.XPath("//div[@class='ActList1']//td/a | //div[@class='ActList1']//div[contains(text(), 'No custom list of activities') or contains(text(), 'No recent activities')]");
        public readonly By EducationCompetenciesTbl = By.XPath("//div[@class='ActList3']");
        public readonly By EducationCompetenciesTblFirstLnk = By.XPath("//div[@class='ActList3']//td/a | //div[@class='ActList3']//div[contains(text(), 'No custom list of activities') or contains(text(), 'No recent activities')]");

        // Tabs

        // Text boxes
    }
}