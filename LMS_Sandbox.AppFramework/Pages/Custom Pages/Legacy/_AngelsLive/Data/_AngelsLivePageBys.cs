using OpenQA.Selenium;

namespace LMS.AppFramework
{
    /// <summary>
    /// This is where we locate our elements. Please use standard naming conventions and group your elements as shown below. Standard naming 
    /// conventions are defined here: https://code.premierinc.com/docs/display/PGHLMSDOCS/Best+Practices
    /// </summary>
    public class _AngelsLivePageBys
    {

        // Buttons

        // Charts

        // Check boxes

        // General

        // Labels                                              

        // Links
        public readonly By OBEmergenciesTblHdrLnks = By.XPath("(//span[contains(@id, 'UAMS')])[2]/../descendant::span//a[@href]");
        public readonly By FetalHeartMonitoringTblHdrLnks = By.XPath("(//span[contains(@id, 'UAMS')])[3]/../descendant::span//a[@href]");
        public readonly By SpecialEventsTblHdrLnks = By.XPath("(//span[contains(@id, 'UAMS')])[4]/../descendant::span//a[@href]");

        // Menu Items    

        // Radio buttons

        // Tables  
        public readonly By OBEmergenciesTbl = By.XPath("//div[@class='ActList2']");
        public readonly By OBEmergenciesTblFirstLnk = By.XPath("//div[@class='ActList2']//td/a | //div[@class='ActList2']//div[contains(text(), 'No custom list of activities') or contains(text(), 'No recent activities')]");
        public readonly By FetalHeartMonitoringTbl = By.XPath("//div[@class='ActList1']");
        public readonly By FetalHeartMonitoringTblFirstLnk = By.XPath("//div[@class='ActList1']//td/a | //div[@class='ActList1']//div[contains(text(), 'No custom list of activities') or contains(text(), 'No recent activities')]");
        public readonly By SpecialEventsTbl = By.XPath("//div[@class='ActList3']");
        public readonly By SpecialEventsTblFirstLnk = By.XPath("//div[@class='ActList3']//td/a | //div[@class='ActList3']//div[contains(text(), 'No custom list of activities') or contains(text(), 'No recent activities')]");

        // Tabs

        // Text boxes



    }
}