using OpenQA.Selenium;

namespace LMS.AppFramework
{
    /// <summary>
    /// This is where we locate our elements. Please use standard naming conventions and group your elements as shown below. Standard naming 
    /// conventions are defined here: https://code.premierinc.com/docs/display/PGHLMSDOCS/Best+Practices
    /// </summary>
    public class _BreastFeedingPageBys
    {

        // Buttons

        // Charts

        // Check boxes

        // General
        public readonly By MainHdr = By.XPath("//span[contains(@id, 'UAMSDashboard')]");


        // Labels                                              

        // Links
        public readonly By BreastFeeingCurricTblHdrLnks = By.XPath("(//span[contains(@id, 'UAMS')])[2]/../descendant::span//a[@href]");
        public readonly By BundledProgramsTblHdrLnks = By.XPath("(//span[contains(@id, 'UAMS')])[3]/../descendant::span//a[@href]");
        public readonly By BundledActivitiesTblHdrLnks = By.XPath("(//span[contains(@id, 'UAMS')])[4]/../descendant::span//a[@href]");
        public readonly By FeaturedActivitiesTblHdrLnks = By.XPath("(//span[contains(@id, 'UAMS')])[5]/../descendant::span//a[@href]");

        // Menu Items    

        // Radio buttons

        // Tables  
        public readonly By BreastFeeingCurricTbl = By.XPath("//div[@class='ActList2']");
        public readonly By BreastFeeingCurricTblFirstLnk = By.XPath("//div[@class='ActList2']//td/a | //div[@class='ActList2']//div[contains(text(), 'No custom list of activities') or contains(text(), 'No recent activities')]");
        public readonly By BundledActivitiesTbl = By.XPath("//div[@class='ActList1']");
        public readonly By BundledActivitiesTblFirstLnk = By.XPath("//div[@class='ActList1']//td/a | //div[@class='ActList1']//div[contains(text(), 'No custom list of activities') or contains(text(), 'No recent activities')]");
        public readonly By BundledProgramsTbl = By.XPath("//div[@class='ActList3']");
        public readonly By BundledProgramsTblFirstLnk = By.XPath("//div[@class='ActList3']//td/a | //div[@class='ActList3']//div[contains(text(), 'No custom list of activities') or contains(text(), 'No recent activities')]");
        public readonly By FeaturedActivitiesTbl = By.XPath("//div[@class='ActList4']");
        public readonly By FeaturedActivitiesTblFirstLnk = By.XPath("//div[@class='ActList4']//td/a | //div[@class='ActList4']//div[contains(text(), 'No custom list of activities') or contains(text(), 'No recent activities')]");

        // Tabs

        // Text boxes



    }
}