using Browser.Core.Framework;
using LMS.Data;
using OpenQA.Selenium;

namespace LMSAdmin.AppFramework
{
    /// <summary>
    /// Entending BrowserTest. Handles setup and configuration for all of selenium tests to run tests against multiple
    /// web browsers (Chrome, Firefox, IE).
    /// </summary>
    public abstract class TestBase_ASNC : TestBase
    {        
        //Users        
        public string Autotest_user1 { get;set;}
        public string Autotest_user2 { get; set; }

        //Activities
        public string Autotest_Activity1 { get; set; }
        public string Autotest_LiveActivity1 { get; set; }
        public string Autotest_ActivityNegativeCase { get; set; }


        // Sites
        public const Constants.SiteCodes siteCodeAttribute = Constants.SiteCodes.ASNC;
        public const string siteCodeCategory = "ASNC";
        public const string password = "password";
        public const string siteName = "American Society of Nuclear Cardiology";

        #region Constructors
        // Local Selenium Test
        public TestBase_ASNC(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }

        // Remote Selenium Grid Test
        public TestBase_ASNC(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version , platform, hubUri, extrasUri) { }
        #endregion Constructors

        /// <summary>
        /// Use this to override TestSetup in BrowserTest to perform setup logic that should occur before EVERY TEST. Can use TestTearDown also
        /// </summary>
        public override void BeforeTest()
        {
            base.BeforeTest();
            browser = base.Browser;

            // Uncomment the below line of code to debug any build server resolution issues
            //Browser.Manage().Window.Size = new System.Drawing.Size(1040, 784);

            if (BrowserName == BrowserNames.Chrome)
            {
                 Autotest_user1 = UserUtils.ASNC_TestUser1_Ch;               
                 Autotest_Activity1 = UserUtils.Activity_Chrome;
                 Autotest_ActivityNegativeCase = UserUtils.ActivityNegativeCase_Chrome;
                 Autotest_LiveActivity1 = UserUtils.LiveActivity_Chrome;
            }
            else if (BrowserName == BrowserNames.Firefox)
            {
                 Autotest_user1 = UserUtils.ASNC_TestUser2_FF;                
                Autotest_Activity1 = UserUtils.Activity_Firefox;
                Autotest_ActivityNegativeCase = UserUtils.ActivityNegativeCase_Firefox;
                Autotest_LiveActivity1 = UserUtils.LiveActivity_Firefox;
            }

            if (BrowserName == BrowserNames.Chrome)
            {                
                Autotest_user2 = UserUtils.ASNC_TestUser3_Ch;
                Autotest_Activity1 = UserUtils.Activity_Chrome;
                Autotest_ActivityNegativeCase = UserUtils.ActivityNegativeCase_Chrome;
                Autotest_LiveActivity1 = UserUtils.LiveActivity_Chrome;
            }
            else if (BrowserName == BrowserNames.Firefox)
            {             
                Autotest_user2 = UserUtils.ASNC_TestUser4_FF;
                Autotest_Activity1 = UserUtils.Activity_Firefox;
                Autotest_ActivityNegativeCase = UserUtils.ActivityNegativeCase_Firefox;
                Autotest_LiveActivity1 = UserUtils.LiveActivity_Firefox;
            }


        }
    }
}