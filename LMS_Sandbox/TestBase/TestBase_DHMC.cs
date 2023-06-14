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

namespace DHMC.UITest
{
    /// <summary>
    /// Extending BrowserTest. Handles setup and configuration for all of selenium tests to run tests against multiple
    /// web browsers (Chrome, Firefox, IE).
    /// </summary>
    public abstract class TestBase_DHMC : TestBase
    {
        // Activities
        public string Assessment_Questions_Feedback { get; set; }

        // Professions
        public string prof1 = Constants.Profession.Physician.GetDescription();
        public string prof2 = Constants.Profession.Pharmacist.GetDescription();
        public string password = UserUtils.Password;

        // Users
        public UserModel profession1User1 { get; set; }
        public UserModel profession1User2 { get; set; }
        public UserModel profession1User3 { get; set; }
        public UserModel profession1User4 { get; set; }
        public UserModel profession2User1 { get; set; }
        public UserModel profession2User2 { get; set; }

        // Sites
        public const Constants.SiteCodes siteCodeAttribute = Constants.SiteCodes.DHMC;
        public const string siteCodeCategory = "DHMC";

        // Random
        public string communityPageWorksheetName = "Community";
        public string fireballPageWorksheetName = "Fireball";

        #region Constructors

        public TestBase_DHMC(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public TestBase_DHMC(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
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
                profession1User1 = UserUtils.AddUserInfo(UserUtils.DHMC_Physician1Username_Mobile, UserUtils.DHMC_Physician1Email_Mobile, prof1);
            }
            else if (BrowserName == BrowserNames.Chrome)
            {
                profession1User1 = UserUtils.AddUserInfo(UserUtils.DHMC_Physician1Username, UserUtils.DHMC_Physician1Email, prof1);
            }
            else if (BrowserName == BrowserNames.Firefox)
            {
                profession1User1 = UserUtils.AddUserInfo(UserUtils.DHMC_Physician1Username_FF, UserUtils.DHMC_Physician1Email_FF, prof1);
            }
            else if (BrowserName == BrowserNames.InternetExplorer)
            {
                profession1User1 = UserUtils.AddUserInfo(UserUtils.DHMC_Physician1Username_IE, UserUtils.DHMC_Physician1Email_IE, prof1);
            }




            if (EmulationDevice == EmulationDevices.iPhoneX)
            {
                profession1User2 = UserUtils.AddUserInfo(UserUtils.DHMC_Physician2Username_Mobile, UserUtils.DHMC_Physician2Email_Mobile, prof1);
            }
            else if (BrowserName == BrowserNames.Chrome)
            {
                profession1User2 = UserUtils.AddUserInfo(UserUtils.DHMC_Physician2Username, UserUtils.DHMC_Physician2Email, prof1);
            }
            else if (BrowserName == BrowserNames.Firefox)
            {
                profession1User2 = UserUtils.AddUserInfo(UserUtils.DHMC_Physician2Username_FF, UserUtils.DHMC_Physician2Email_FF, prof1);
            }
            else if (BrowserName == BrowserNames.InternetExplorer)
            {
                profession1User2 = UserUtils.AddUserInfo(UserUtils.DHMC_Physician2Username_IE, UserUtils.DHMC_Physician2Email_IE, prof1);
            }




            if (EmulationDevice == EmulationDevices.iPhoneX)
            {
                profession1User3 = UserUtils.AddUserInfo(UserUtils.DHMC_Physician3Username_Mobile, UserUtils.DHMC_Physician3Email_Mobile, prof1);
            }
            else if (BrowserName == BrowserNames.Chrome)
            {
                profession1User3 = UserUtils.AddUserInfo(UserUtils.DHMC_Physician3Username, UserUtils.DHMC_Physician3Email, prof1);
            }
            else if (BrowserName == BrowserNames.Firefox)
            {
                profession1User3 = UserUtils.AddUserInfo(UserUtils.DHMC_Physician3Username_FF, UserUtils.DHMC_Physician3Email_FF, prof1);
            }
            else if (BrowserName == BrowserNames.InternetExplorer)
            {
                profession1User3 = UserUtils.AddUserInfo(UserUtils.DHMC_Physician3Username_IE, UserUtils.DHMC_Physician3Email_IE, prof1);
            }




            if (EmulationDevice == EmulationDevices.iPhoneX)
            {
                profession1User4 = UserUtils.AddUserInfo(UserUtils.DHMC_Physician4Username_Mobile, UserUtils.DHMC_Physician4Email_Mobile, prof1);
            }
            else if (BrowserName == BrowserNames.Chrome)
            {
                profession1User4 = UserUtils.AddUserInfo(UserUtils.DHMC_Physician4Username, UserUtils.DHMC_Physician4Email, prof1);
            }
            else if (BrowserName == BrowserNames.Firefox)
            {
                profession1User4 = UserUtils.AddUserInfo(UserUtils.DHMC_Physician4Username_FF, UserUtils.DHMC_Physician4Email_FF, prof1);
            }
            else if (BrowserName == BrowserNames.InternetExplorer)
            {
                profession1User4 = UserUtils.AddUserInfo(UserUtils.DHMC_Physician4Username_IE, UserUtils.DHMC_Physician4Email_IE, prof1);
            }




            if (EmulationDevice == EmulationDevices.iPhoneX)
            {
                profession2User1 = UserUtils.AddUserInfo(UserUtils.DHMC_Pharmacist1Username_Mobile, UserUtils.DHMC_Pharmacist1Email_Mobile, prof2);
            }
            else if (BrowserName == BrowserNames.Chrome)
            {
                profession2User1 = UserUtils.AddUserInfo(UserUtils.DHMC_Pharmacist1Username, UserUtils.DHMC_Pharmacist1Email, prof2);
            }
            else if (BrowserName == BrowserNames.Firefox)
            {
                profession2User1 = UserUtils.AddUserInfo(UserUtils.DHMC_Pharmacist1Username_FF, UserUtils.DHMC_Pharmacist1Email_FF, prof2);
            }
            else if (BrowserName == BrowserNames.InternetExplorer)
            {
                profession2User1 = UserUtils.AddUserInfo(UserUtils.DHMC_Pharmacist1Username_IE, UserUtils.DHMC_Pharmacist1Email_IE, prof2);
            }




            if (EmulationDevice == EmulationDevices.iPhoneX)
            {
                profession2User2 = UserUtils.AddUserInfo(UserUtils.DHMC_Pharmacist2Username_Mobile, UserUtils.DHMC_Pharmacist2Email_Mobile, prof2);
            }
            else if (BrowserName == BrowserNames.Chrome)
            {
                profession2User2 = UserUtils.AddUserInfo(UserUtils.DHMC_Pharmacist2Username, UserUtils.DHMC_Pharmacist2Email, prof2);
            }
            else if (BrowserName == BrowserNames.Firefox)
            {
                profession2User2 = UserUtils.AddUserInfo(UserUtils.DHMC_Pharmacist2Username_FF, UserUtils.DHMC_Pharmacist2Email_FF, prof2);
            }
            else if (BrowserName == BrowserNames.InternetExplorer)
            {
                profession2User2 = UserUtils.AddUserInfo(UserUtils.DHMC_Pharmacist2Username_IE, UserUtils.DHMC_Pharmacist2Email_IE, prof2);
            }
        }

    }
}
