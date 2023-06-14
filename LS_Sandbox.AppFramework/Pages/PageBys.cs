using OpenQA.Selenium;

namespace LS.AppFramework
{
    public class PageBys
    {
        // Banners
        public readonly By GreenBanner = By.XPath("//ul[@class='success']/li"); // this is the green banner at the top of any page whenever you submit something. For example, after you validate credit for an activity. We wait until this disappears before the test should proceed


        // Buttons

        // Charts

        // Check boxes

        // Frames



        // Labels    
        public readonly By RecentLbl = By.XPath("//label[@id='recentlabel']");
        public readonly By UserNameHdr = By.XPath("//legend/span");

        // Links
        public readonly By AllParticipantsLnk = By.LinkText("All Participants");
        public readonly By RCPSCLnk = By.LinkText("RCPSC");

        

        // public readonly By LogoutLnk = By.LinkText("Log Out");
        public readonly By LogoutLnk = By.XPath("//a[text()='Log Out']");

        //Menu Items    


        // Radio buttons

        // Tables       

        // Tabs
        public readonly By SitesTab = By.XPath("//li[@class='first']/a[text()='Sites']");

        
        // Text boxes














    }
}