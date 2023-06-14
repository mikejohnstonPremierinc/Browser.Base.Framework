using Browser.Core.Framework;
using LMS.Data;
using OpenQA.Selenium;
using NUnit.Framework;
using LMS.AppFramework.Constants_;
using LMS.AppFramework.DBUtils_;
using LMS.AppFramework.LMSHelperMethods;
//
using LMS.AppFramework;
using LMS.UITest;
using System.Configuration;

namespace ACR.UITest
{
    /// <summary>
    /// Extending BrowserTest. Handles setup and configuration for all of selenium tests to run tests against multiple
    /// web browsers (Chrome, Firefox, IE).
    /// </summary>
    public abstract class TestBase_ACR : TestBase
    {
        // Activities
        public string Assessment_Questions_Feedback { get; set; }

        // Professions
        public string prof1 = Constants.Profession.Physician.GetDescription();

        // Users
        public UserModel profession1User1 { get; set; }
        public UserModel profession1User2 { get; set; }

        // Sites
        public const Constants.SiteCodes siteCodeAttribute = Constants.SiteCodes.ACR;
        public const string siteCodeCategory = "ACR";

        // Misc
        public string communityPageWorksheetName = "Community";
        public string fireballPageWorksheetName = "Fireball";

        #region Constructors
        // Local Selenium Test
        public TestBase_ACR(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }

        // Remote Selenium Grid Test
        public TestBase_ACR(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri) { }
        #endregion Constructors

        /// <summary>
        /// Use this to override TestSetup in BrowserTest to perform setup logic that should occur before EVERY TEST inside this project. 
        /// Can use TestTearDown also
        /// </summary>
        public override void BeforeTest()
        {
            base.BeforeTest();
            browser = base.Browser;
            DBUtils.SQLconnString = Constants_LMS.SQLconnString;

            // Activities:
            if (Help.EnvironmentEquals(Constants.Environments.Production))
            {
                Assessment_Questions_Feedback = Constants.ActTitle.Automation_Assessments_All_Questions_with_Feedback_NoGenderOrProfession.GetDescription();
            }
            else
            {
                Assessment_Questions_Feedback = Constants.ActTitle.Automation_Assessments_All_Questions_with_Feedback.GetDescription();
            }

            // Users: 

            // First assign the user information per browser. This is so that we can run these tests in parallel.
            if (EmulationDevice == EmulationDevices.iPhoneX)
            {
                profession1User1 = UserUtils.AddUserInfo(UserUtils.ACR_Nurse1Username_Mobile,
                    UserUtils.ACR_Nurse1Email_Mobile,
                    prof1);
            }
            else if (BrowserName == BrowserNames.Chrome)
            {
                profession1User1 = UserUtils.AddUserInfo(UserUtils.ACR_Nurse1Username,
                    UserUtils.ACR_Nurse1Email,
                    prof1);
            }
            else if (BrowserName == BrowserNames.Firefox)
            {
                profession1User1 = UserUtils.AddUserInfo(UserUtils.ACR_Nurse1Username_FF,
                    UserUtils.ACR_Nurse1Email_FF,
                    prof1);
            }
            else if (BrowserName == BrowserNames.InternetExplorer)
            {
                profession1User1 = UserUtils.AddUserInfo(UserUtils.ACR_Nurse1Username_IE,
                    UserUtils.ACR_Nurse1Email_IE,
                    prof1);
            }
            else if (BrowserName == BrowserNames.Edge)
            {
                profession1User1 = UserUtils.AddUserInfo(UserUtils.ACR_Nurse1Username_IE,
                    UserUtils.ACR_Nurse1Email_IE,
                    prof1);
            }



            if (EmulationDevice == EmulationDevices.iPhoneX)
            {
                profession1User2 = UserUtils.AddUserInfo(UserUtils.ACR_Nurse2Username_Mobile,
                    UserUtils.ACR_Nurse2Email_Mobile,
                    prof1);
            }
            else if (BrowserName == BrowserNames.Chrome)
            {
                profession1User2 = UserUtils.AddUserInfo(UserUtils.ACR_Nurse2Username,
                    UserUtils.ACR_Nurse2Email,
                    prof1);
            }
            else if (BrowserName == BrowserNames.Firefox)
            {
                profession1User2 = UserUtils.AddUserInfo(UserUtils.ACR_Nurse2Username_FF,
                    UserUtils.ACR_Nurse2Email_FF,
                    prof1);
            }
            else if (BrowserName == BrowserNames.InternetExplorer)
            {
                profession1User2 = UserUtils.AddUserInfo(UserUtils.ACR_Nurse2Username_IE,
                    UserUtils.ACR_Nurse2Email_IE,
                    prof1);
            }

        }

    }
}
