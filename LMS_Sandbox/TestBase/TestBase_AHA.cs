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
using System.Threading;

namespace AHA.UITest
{
    /// <summary>
    /// Extending BrowserTest. Handles setup and configuration for all of selenium tests to run tests against multiple
    /// web browsers (Chrome, Firefox, IE).
    /// </summary>
    public abstract class TestBase_AHA : TestBase
    {
        // Activities
        public string Assessment_Questions_Feedback { get; set; }

        // Professions
        public string prof1 = Constants.Profession.Physician.GetDescription();
        public string prof2 = Constants.Profession.Nurse.GetDescription();
        public string prof3 = Constants.Profession.Pharmacist.GetDescription();
        public string password = UserUtils.Password_AllCaps;

        // Users
        public UserModel profession1User1 { get; set; }
        public UserModel profession1User2 { get; set; }
        public UserModel profession2User1 { get; set; }
        public UserModel profession2User2 { get; set; }
        public UserModel profession3User1 { get; set; }
        public UserModel professionMember1User1 { get; set; }
        public UserModel professionMember1User2 { get; set; }
        public UserModel professionMember1User3 { get; set; }
        public UserModel professionMember1User4 { get; set; }
        public UserModel professionMember1User5 { get; set; }
        public UserModel professionMember1User6 { get; set; }
        public UserModel professionMember1User7 { get; set; }
        public UserModel professionMember1User8 { get; set; }

        // Sites
        public const Constants.SiteCodes siteCodeAttribute = Constants.SiteCodes.AHA;
        public const string siteCodeCategory = "AHA";

        // Misc
        public string communityPageWorksheetName = "Community";
        public string fireballPageWorksheetName = "Fireball";

        #region Constructors
        // Local Selenium Test
        public TestBase_AHA(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }

        // Remote Selenium Grid Test
        public TestBase_AHA(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
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
                profession1User1 = UserUtils.AddUserInfo(UserUtils.AHA_Physician1Username_Mobile,
                    UserUtils.AHA_Physician1Email_Mobile,
                    prof1);
            }
            else if (BrowserName == BrowserNames.Chrome)
            {
                profession1User1 = UserUtils.AddUserInfo(UserUtils.AHA_Physician1Username,
                    UserUtils.AHA_Physician1Email,
                    prof1);
            }
            else if (BrowserName == BrowserNames.Firefox)
            {
                profession1User1 = UserUtils.AddUserInfo(UserUtils.AHA_Physician1Username_FF,
                    UserUtils.AHA_Physician1Email_FF,
                    prof1);
            }
            else if (BrowserName == BrowserNames.InternetExplorer)
            {
                profession1User1 = UserUtils.AddUserInfo(UserUtils.AHA_Physician1Username_IE,
                    UserUtils.AHA_Physician1Email_IE,
                    prof1);
            }



            if (EmulationDevice == EmulationDevices.iPhoneX)
            {
                profession1User2 = UserUtils.AddUserInfo(UserUtils.AHA_Physician2Username_Mobile,
                    UserUtils.AHA_Physician2Email_Mobile,
                    prof1);
            }
            else if (BrowserName == BrowserNames.Chrome)
            {
                profession1User2 = UserUtils.AddUserInfo(UserUtils.AHA_Physician2Username,
                    UserUtils.AHA_Physician2Email,
                    prof1);
            }
            else if (BrowserName == BrowserNames.Firefox)
            {
                profession1User2 = UserUtils.AddUserInfo(UserUtils.AHA_Physician2Username_FF,
                    UserUtils.AHA_Physician2Email_FF,
                    prof1);
            }
            else if (BrowserName == BrowserNames.InternetExplorer)
            {
                profession1User2 = UserUtils.AddUserInfo(UserUtils.AHA_Physician2Username_IE,
                    UserUtils.AHA_Physician2Email_IE,
                    prof1);
            }




            if (EmulationDevice == EmulationDevices.iPhoneX)
            {
                profession2User1 = UserUtils.AddUserInfo(UserUtils.AHA_Nurse1Username_Mobile,
                    UserUtils.AHA_Nurse1Email_Mobile,
                    prof1);
            }
            else if (BrowserName == BrowserNames.Chrome)
            {
                profession2User1 = UserUtils.AddUserInfo(UserUtils.AHA_Nurse1Username,
                    UserUtils.AHA_Nurse1Email,
                    prof1);
            }
            else if (BrowserName == BrowserNames.Firefox)
            {
                profession2User1 = UserUtils.AddUserInfo(UserUtils.AHA_Nurse1Username_FF,
                    UserUtils.AHA_Nurse1Email_FF,
                    prof1);
            }
            else if (BrowserName == BrowserNames.InternetExplorer)
            {
                profession2User1 = UserUtils.AddUserInfo(UserUtils.AHA_Nurse1Username_IE,
                    UserUtils.AHA_Nurse1Email_IE,
                    prof1);
            }




            if (EmulationDevice == EmulationDevices.iPhoneX)
            {
                profession2User2 = UserUtils.AddUserInfo(UserUtils.AHA_Nurse2Username_Mobile,
                    UserUtils.AHA_Nurse2Email_Mobile,
                    prof1);
            }
            else if (BrowserName == BrowserNames.Chrome)
            {
                profession2User2 = UserUtils.AddUserInfo(UserUtils.AHA_Nurse2Username,
                    UserUtils.AHA_Nurse2Email,
                    prof1);
            }
            else if (BrowserName == BrowserNames.Firefox)
            {
                profession2User2 = UserUtils.AddUserInfo(UserUtils.AHA_Nurse2Username_FF,
                    UserUtils.AHA_Nurse2Email_FF,
                    prof1);
            }
            else if (BrowserName == BrowserNames.InternetExplorer)
            {
                profession2User2 = UserUtils.AddUserInfo(UserUtils.AHA_Nurse2Username_IE,
                    UserUtils.AHA_Nurse2Email_IE,
                    prof1);
            }





            if (EmulationDevice == EmulationDevices.iPhoneX)
            {
                profession2User2 = UserUtils.AddUserInfo(UserUtils.AHA_Nurse2Username_Mobile,
                    UserUtils.AHA_Nurse2Email_Mobile,
                    prof1);
            }
            else if (BrowserName == BrowserNames.Chrome)
            {
                profession2User2 = UserUtils.AddUserInfo(UserUtils.AHA_Nurse2Username,
                    UserUtils.AHA_Nurse2Email,
                    prof1);
            }
            else if (BrowserName == BrowserNames.Firefox)
            {
                profession2User2 = UserUtils.AddUserInfo(UserUtils.AHA_Nurse2Username_FF,
                    UserUtils.AHA_Nurse2Email_FF,
                    prof1);
            }
            else if (BrowserName == BrowserNames.InternetExplorer)
            {
                profession2User2 = UserUtils.AddUserInfo(UserUtils.AHA_Nurse2Username_IE,
                    UserUtils.AHA_Nurse2Email_IE,
                    prof1);
            }




            if (EmulationDevice == EmulationDevices.iPhoneX)
            {
                profession3User1 = UserUtils.AddUserInfo(UserUtils.AHA_Pharmacist1Username_Mobile,
                    UserUtils.AHA_Pharmacist1Email_Mobile,
                    prof1);
            }
            else if (BrowserName == BrowserNames.Chrome)
            {
                profession3User1 = UserUtils.AddUserInfo(UserUtils.AHA_Pharmacist1Username,
                    UserUtils.AHA_Pharmacist1Email,
                    prof1);
            }
            else if (BrowserName == BrowserNames.Firefox)
            {
                profession3User1 = UserUtils.AddUserInfo(UserUtils.AHA_Pharmacist1Username_FF,
                    UserUtils.AHA_Pharmacist1Email_FF,
                    prof1);
            }
            else if (BrowserName == BrowserNames.InternetExplorer)
            {
                profession3User1 = UserUtils.AddUserInfo(UserUtils.AHA_Pharmacist1Username_IE,
                    UserUtils.AHA_Pharmacist1Email_IE,
                    prof1);
            }





            if (EmulationDevice == EmulationDevices.iPhoneX)
            {
                professionMember1User1 = UserUtils.AddUserInfo(UserUtils.AHA_PhysicianMember1Username_Mobile,
                    UserUtils.AHA_PhysicianMember1Email_Mobile,
                    prof1);
            }
            else if (BrowserName == BrowserNames.Chrome)
            {
                professionMember1User1 = UserUtils.AddUserInfo(UserUtils.AHA_PhysicianMember1Username,
                    UserUtils.AHA_PhysicianMember1Email,
                    prof1);
            }
            else if (BrowserName == BrowserNames.Firefox)
            {
                professionMember1User1 = UserUtils.AddUserInfo(UserUtils.AHA_PhysicianMember1Username_FF,
                    UserUtils.AHA_PhysicianMember1Email_FF,
                    prof1);
            }
            else if (BrowserName == BrowserNames.InternetExplorer)
            {
                professionMember1User1 = UserUtils.AddUserInfo(UserUtils.AHA_PhysicianMember1Username_IE,
                    UserUtils.AHA_PhysicianMember1Email_IE,
                    prof1);
            }



            if (EmulationDevice == EmulationDevices.iPhoneX)
            {
                professionMember1User2 = UserUtils.AddUserInfo(UserUtils.AHA_PhysicianMember2Username_Mobile,
                    UserUtils.AHA_PhysicianMember2Email_Mobile,
                    prof1);
            }
            else if (BrowserName == BrowserNames.Chrome)
            {
                professionMember1User2 = UserUtils.AddUserInfo(UserUtils.AHA_PhysicianMember2Username,
                    UserUtils.AHA_PhysicianMember2Email,
                    prof1);
            }
            else if (BrowserName == BrowserNames.Firefox)
            {
                professionMember1User2 = UserUtils.AddUserInfo(UserUtils.AHA_PhysicianMember2Username_FF,
                    UserUtils.AHA_PhysicianMember2Email_FF,
                    prof1);
            }
            else if (BrowserName == BrowserNames.InternetExplorer)
            {
                professionMember1User2 = UserUtils.AddUserInfo(UserUtils.AHA_PhysicianMember2Username_IE,
                    UserUtils.AHA_PhysicianMember2Email_IE,
                    prof1);
            }



            if (EmulationDevice == EmulationDevices.iPhoneX)
            {
                professionMember1User3 = UserUtils.AddUserInfo(UserUtils.AHA_PhysicianMember3Username_Mobile,
                    UserUtils.AHA_PhysicianMember3Email_Mobile,
                    prof1);
            }
            else if (BrowserName == BrowserNames.Chrome)
            {
                professionMember1User3 = UserUtils.AddUserInfo(UserUtils.AHA_PhysicianMember3Username,
                    UserUtils.AHA_PhysicianMember3Email,
                    prof1);
            }
            else if (BrowserName == BrowserNames.Firefox)
            {
                professionMember1User3 = UserUtils.AddUserInfo(UserUtils.AHA_PhysicianMember3Username_FF,
                    UserUtils.AHA_PhysicianMember3Email_FF,
                    prof1);
            }
            else if (BrowserName == BrowserNames.InternetExplorer)
            {
                professionMember1User3 = UserUtils.AddUserInfo(UserUtils.AHA_PhysicianMember3Username_IE,
                    UserUtils.AHA_PhysicianMember3Email_IE,
                    prof1);
            }



            if (EmulationDevice == EmulationDevices.iPhoneX)
            {
                professionMember1User4 = UserUtils.AddUserInfo(UserUtils.AHA_PhysicianMember4Username_Mobile,
                    UserUtils.AHA_PhysicianMember4Email_Mobile,
                    prof1);
            }
            else if (BrowserName == BrowserNames.Chrome)
            {
                professionMember1User4 = UserUtils.AddUserInfo(UserUtils.AHA_PhysicianMember4Username,
                    UserUtils.AHA_PhysicianMember4Email,
                    prof1);
            }
            else if (BrowserName == BrowserNames.Firefox)
            {
                professionMember1User4 = UserUtils.AddUserInfo(UserUtils.AHA_PhysicianMember4Username_FF,
                    UserUtils.AHA_PhysicianMember4Email_FF,
                    prof1);
            }
            else if (BrowserName == BrowserNames.InternetExplorer)
            {
                professionMember1User4 = UserUtils.AddUserInfo(UserUtils.AHA_PhysicianMember4Username_IE,
                    UserUtils.AHA_PhysicianMember4Email_IE,
                    prof1);
            }



            if (EmulationDevice == EmulationDevices.iPhoneX)
            {
                professionMember1User5 = UserUtils.AddUserInfo(UserUtils.AHA_PhysicianMember5Username_Mobile,
                    UserUtils.AHA_PhysicianMember5Email_Mobile,
                    prof1);
            }
            else if (BrowserName == BrowserNames.Chrome)
            {
                professionMember1User5 = UserUtils.AddUserInfo(UserUtils.AHA_PhysicianMember5Username,
                    UserUtils.AHA_PhysicianMember5Email,
                    prof1);
            }
            else if (BrowserName == BrowserNames.Firefox)
            {
                professionMember1User5 = UserUtils.AddUserInfo(UserUtils.AHA_PhysicianMember5Username_FF,
                    UserUtils.AHA_PhysicianMember5Email_FF,
                    prof1);
            }
            else if (BrowserName == BrowserNames.InternetExplorer)
            {
                professionMember1User5 = UserUtils.AddUserInfo(UserUtils.AHA_PhysicianMember5Username_IE,
                    UserUtils.AHA_PhysicianMember5Email_IE,
                    prof1);
            }


            if (EmulationDevice == EmulationDevices.iPhoneX)
            {
                professionMember1User6 = UserUtils.AddUserInfo(UserUtils.AHA_PhysicianMember6Username_Mobile,
                    UserUtils.AHA_PhysicianMember6Email_Mobile,
                    prof1);
            }
            else if (BrowserName == BrowserNames.Chrome)
            {
                professionMember1User6 = UserUtils.AddUserInfo(UserUtils.AHA_PhysicianMember6Username,
                    UserUtils.AHA_PhysicianMember6Email,
                    prof1);
            }
            else if (BrowserName == BrowserNames.Firefox)
            {
                professionMember1User6 = UserUtils.AddUserInfo(UserUtils.AHA_PhysicianMember6Username_FF,
                    UserUtils.AHA_PhysicianMember6Email_FF,
                    prof1);
            }
            else if (BrowserName == BrowserNames.InternetExplorer)
            {
                professionMember1User6 = UserUtils.AddUserInfo(UserUtils.AHA_PhysicianMember6Username_IE,
                    UserUtils.AHA_PhysicianMember6Email_IE,
                    prof1);
            }


            if (EmulationDevice == EmulationDevices.iPhoneX)
            {
                professionMember1User7 = UserUtils.AddUserInfo(UserUtils.AHA_PhysicianMember7Username_Mobile,
                    UserUtils.AHA_PhysicianMember7Email_Mobile,
                    prof1);
            }
            else if (BrowserName == BrowserNames.Chrome)
            {
                professionMember1User7 = UserUtils.AddUserInfo(UserUtils.AHA_PhysicianMember7Username,
                    UserUtils.AHA_PhysicianMember7Email,
                    prof1);
            }
            else if (BrowserName == BrowserNames.Firefox)
            {
                professionMember1User7 = UserUtils.AddUserInfo(UserUtils.AHA_PhysicianMember7Username_FF,
                    UserUtils.AHA_PhysicianMember7Email_FF,
                    prof1);
            }
            else if (BrowserName == BrowserNames.InternetExplorer)
            {
                professionMember1User7 = UserUtils.AddUserInfo(UserUtils.AHA_PhysicianMember7Username_IE,
                    UserUtils.AHA_PhysicianMember7Email_IE,
                    prof1);
            }


            if (EmulationDevice == EmulationDevices.iPhoneX)
            {
                professionMember1User8 = UserUtils.AddUserInfo(UserUtils.AHA_PhysicianMember8Username_Mobile,
                    UserUtils.AHA_PhysicianMember8Email_Mobile,
                    prof1);
            }
            else if (BrowserName == BrowserNames.Chrome)
            {
                professionMember1User8 = UserUtils.AddUserInfo(UserUtils.AHA_PhysicianMember8Username,
                    UserUtils.AHA_PhysicianMember8Email,
                    prof1);
            }
            else if (BrowserName == BrowserNames.Firefox)
            {
                professionMember1User8 = UserUtils.AddUserInfo(UserUtils.AHA_PhysicianMember8Username_FF,
                    UserUtils.AHA_PhysicianMember8Email_FF,
                    prof1);
            }
            else if (BrowserName == BrowserNames.InternetExplorer)
            {
                professionMember1User8 = UserUtils.AddUserInfo(UserUtils.AHA_PhysicianMember8Username_IE,
                    UserUtils.AHA_PhysicianMember8Email_IE,
                    prof1);
            }



        }

    }
}
