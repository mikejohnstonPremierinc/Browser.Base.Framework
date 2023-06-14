using OpenQA.Selenium;

namespace LMS.AppFramework
{
    /// <summary>
    /// This is where we locate our elements. Please use standard naming conventions and group your elements as shown below. Standard naming 
    /// conventions are defined here: https://code.premierinc.com/docs/display/PGHLMSDOCS/Best+Practices
    /// </summary>
    public class _TraqPageBys
    {

        // Buttons

        // Charts

        // Check boxes

        // General

        // Labels                                              

        // Links
        public readonly By RegSchedTeleconfTblHdrLnks = By.XPath("(//span[contains(@id, 'UAMS')])[2]/../descendant::span//a[@href]");
        public readonly By RecordedPresentationsTblHdrLnks = By.XPath("(//span[contains(@id, 'UAMS')])[3]/../descendant::span//a[@href]");
        public readonly By ComingSoonTblHdrLnks = By.XPath("(//span[contains(@id, 'UAMS')])[4]/../descendant::span//a[@href]");


        // Menu Items    
        
        // Radio buttons

        // Tables  
        public readonly By RecordedPresentationsTbl = By.XPath("//div[@class='ActList1']");
        public readonly By RecordedPresentationsTblFirstLnk = By.XPath("//div[@class='ActList1']//td/a | //div[@class='ActList1']//div[contains(text(), 'No recent activities')] | //div[@class='ActList1']//div[contains(text(), 'No custom list of activities') or contains(text(), 'No recent activities')]");
        public readonly By ComingSoonTbl = By.XPath("//div[@class='ActList3']");
        public readonly By ComingSoonTblFirstLnk = By.XPath("//div[@class='ActList3']//td/a | //div[@class='ActList3']//div[contains(text(), 'No custom list of activities') or contains(text(), 'No recent activities')] | //div[@class='ActList3']//div[contains(text(), 'No custom list of activities') or contains(text(), 'No recent activities')]");
        public readonly By RegSchedTeleconfTbl = By.XPath("//div[@class='ActList2']");
        public readonly By RegSchedTeleconfTblFirstLnk = By.XPath("//div[@class='ActList2']//td/a | //div[@class='ActList2']//div[contains(text(), 'No recent activities')] | //div[@class='ActList2']//div[contains(text(), 'No custom list of activities') or contains(text(), 'No recent activities')]");


        // Tabs

        // Text boxes



    }
}