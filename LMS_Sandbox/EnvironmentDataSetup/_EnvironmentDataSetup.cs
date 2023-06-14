using Browser.Core.Framework;
using LMS.Data;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using LMS.AppFramework;
using LMS.AppFramework.LMSHelperMethods;
using LMS.AppFramework.Constants_;
////
////
using System.Data;
using System.Configuration;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;

namespace LMS.UITest
{
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [TestFixture]
    public class _EnvironmentDataSetup : TestBase
    {
        #region Constructors
        public _EnvironmentDataSetup(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        #endregion

        #region Properties

        //   LMSHelperMethods Help = new LMSHelperMethods();
        //    CMEHelperGeneral CMEHelp = new CMEHelperGeneral();
        //  private const Constants.SiteCodes siteCodeAttribute = Constants.SiteCodes.UAMS;
        //  private const string siteCodeCategory = "ONSLT";

        #endregion Properties

        #region Tests

        [Description("The below method will need executed on a new environment to create the necessary users for all tests. Also, " +
               "an API account needs created (if one has not been created already) on this new environment. When creating the " +
               "account, you need to do 3 things " +
               "1. Copy all of the IP addresses and Endpoints from an existing automation API account, then paste them in the " +
               "cooresponding fields when creating this account " +
               "2. Execute the below script on this new API account. Note that for Prod, you will need to create a ticket and " +
               "assign it to a DEV for him to apply the script to Prod. " +
               "UPDATE security.api.Account SET ApiSecuritySettings_CheckTokenEnvironment = 1, ApiSecuritySettings_CheckTokenExpiration = 1, ApiSecuritySettings_CheckSiteToken = 1, ApiSecuritySettings_CheckSiteCodesValue = NULL, ApiSecuritySettings_CheckToken = 1, ApiSecuritySettings_IsSet = 1, SecurityProviderSettings_SecretKey = NULL, SecurityProviderSettings_x509Certificate = NULL, ApiSecuritySettings_CheckAllowOriginCorsHeader = 1, AccountPreferences_IsTokenPersistenceEnabled = 1, AccountType = 3 WHERE[Key] = 'TestAuto_UAMS' " +
               "If you are copying and pasting this method for a new site (not a new environment), then you will have to perform the " +
                "above steps, along with the below step also" +
               "1. Go to UserUtils->BuildUserModel and add code for UAMS. This will drive the code in the method below to add users")]
        [Test]
        public void CreateAllStaticUsers_UAMS()
        {
            LoginPage LP = new LoginPage(Browser);
            Constants.SiteCodes siteCode = Constants.SiteCodes.UAMS;

            Assert.Fail("Dont run this unless you need new users for a new environment. If so, comment this Assert and run");

            // *************************************** Profession 1 ******************************************************
            // Chrome
            UserUtils.CreateUser(siteCode, UserUtils.UAMS_Physician1Username, UserUtils.UAMS_Physician1Email, null, null,
                Constants.ProfessionCode.Physician_PHY);
            UserUtils.CreateUser(siteCode, UserUtils.UAMS_Physician2Username, UserUtils.UAMS_Physician2Email, null, null,
                Constants.ProfessionCode.Physician_PHY);
            UserUtils.CreateUser(siteCode, UserUtils.UAMS_Physician3Username, UserUtils.UAMS_Physician3Email, null, null,
                Constants.ProfessionCode.Physician_PHY);
            UserUtils.CreateUser(siteCode, UserUtils.UAMS_Physician4Username, UserUtils.UAMS_Physician4Email, null, null,
                Constants.ProfessionCode.Physician_PHY);
            // Mobile
            UserUtils.CreateUser(siteCode, UserUtils.UAMS_Physician1Username_Mobile, UserUtils.UAMS_Physician1Email, null, null,
                Constants.ProfessionCode.Physician_PHY);
            UserUtils.CreateUser(siteCode, UserUtils.UAMS_Physician2Username_Mobile, UserUtils.UAMS_Physician2Email, null, null,
                Constants.ProfessionCode.Physician_PHY);
            UserUtils.CreateUser(siteCode, UserUtils.UAMS_Physician3Username_Mobile, UserUtils.UAMS_Physician3Email, null, null,
                Constants.ProfessionCode.Physician_PHY);
            UserUtils.CreateUser(siteCode, UserUtils.UAMS_Physician4Username_Mobile, UserUtils.UAMS_Physician4Email, null, null,
                Constants.ProfessionCode.Physician_PHY);
            // IE
            UserUtils.CreateUser(siteCode, UserUtils.UAMS_Physician1Username_IE, UserUtils.UAMS_Physician1Email, null, null,
                Constants.ProfessionCode.Physician_PHY);
            UserUtils.CreateUser(siteCode, UserUtils.UAMS_Physician2Username_IE, UserUtils.UAMS_Physician2Email, null, null,
                Constants.ProfessionCode.Physician_PHY);
            UserUtils.CreateUser(siteCode, UserUtils.UAMS_Physician3Username_IE, UserUtils.UAMS_Physician3Email, null, null,
                Constants.ProfessionCode.Physician_PHY);
            UserUtils.CreateUser(siteCode, UserUtils.UAMS_Physician4Username_IE, UserUtils.UAMS_Physician4Email, null, null,
                Constants.ProfessionCode.Physician_PHY);
            // FF
            UserUtils.CreateUser(siteCode, UserUtils.UAMS_Physician1Username_FF, UserUtils.UAMS_Physician1Email, null, null,
                Constants.ProfessionCode.Physician_PHY);
            UserUtils.CreateUser(siteCode, UserUtils.UAMS_Physician2Username_FF, UserUtils.UAMS_Physician2Email, null, null,
                Constants.ProfessionCode.Physician_PHY);
            UserUtils.CreateUser(siteCode, UserUtils.UAMS_Physician3Username_FF, UserUtils.UAMS_Physician3Email, null, null,
                Constants.ProfessionCode.Physician_PHY);
            UserUtils.CreateUser(siteCode, UserUtils.UAMS_Physician4Username_FF, UserUtils.UAMS_Physician4Email, null, null,
                Constants.ProfessionCode.Physician_PHY);

            // Log in to each new user
            // Chrome
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.UAMS_Physician1Username, newUser: true);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.UAMS_Physician2Username, newUser: true);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.UAMS_Physician3Username, newUser: true);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.UAMS_Physician4Username, newUser: true);
            LP.LogOut(siteCode);

            // Mobile
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.UAMS_Physician1Username_Mobile, newUser: true);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.UAMS_Physician2Username_Mobile, newUser: true);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.UAMS_Physician3Username_Mobile, newUser: true);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.UAMS_Physician4Username_Mobile, newUser: true);
            LP.LogOut(siteCode);

            // IE
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.UAMS_Physician1Username_IE, newUser: true);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.UAMS_Physician2Username_IE, newUser: true);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.UAMS_Physician3Username_IE, newUser: true);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.UAMS_Physician4Username_IE, newUser: true);
            LP.LogOut(siteCode);

            // FF
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.UAMS_Physician1Username_FF, newUser: true);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.UAMS_Physician2Username_FF, newUser: true);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.UAMS_Physician3Username_FF, newUser: true);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.UAMS_Physician4Username_FF, newUser: true);
            LP.LogOut(siteCode);

            // *************************************** Profession 2 ******************************************************
            // Chrome
            UserUtils.CreateUser(siteCode, UserUtils.UAMS_Pharmacist1Username, UserUtils.UAMS_Pharmacist1Email, null, null,
                Constants.ProfessionCode.Pharmacist_RPH);
            UserUtils.CreateUser(siteCode, UserUtils.UAMS_Pharmacist2Username, UserUtils.UAMS_Pharmacist2Email, null, null,
                Constants.ProfessionCode.Pharmacist_RPH);
            // Mobile
            UserUtils.CreateUser(siteCode, UserUtils.UAMS_Pharmacist1Username_Mobile, UserUtils.UAMS_Pharmacist1Email, null, null,
                Constants.ProfessionCode.Pharmacist_RPH);
            UserUtils.CreateUser(siteCode, UserUtils.UAMS_Pharmacist2Username_Mobile, UserUtils.UAMS_Pharmacist2Email, null, null,
                Constants.ProfessionCode.Pharmacist_RPH);
            // IE
            UserUtils.CreateUser(siteCode, UserUtils.UAMS_Pharmacist1Username_IE, UserUtils.UAMS_Pharmacist1Email, null, null,
                Constants.ProfessionCode.Pharmacist_RPH);
            UserUtils.CreateUser(siteCode, UserUtils.UAMS_Pharmacist2Username_IE, UserUtils.UAMS_Pharmacist2Email, null, null,
                Constants.ProfessionCode.Pharmacist_RPH);
            // FF
            UserUtils.CreateUser(siteCode, UserUtils.UAMS_Pharmacist1Username_FF, UserUtils.UAMS_Pharmacist1Email, null, null,
                Constants.ProfessionCode.Pharmacist_RPH);
            UserUtils.CreateUser(siteCode, UserUtils.UAMS_Pharmacist2Username_FF, UserUtils.UAMS_Pharmacist2Email, null, null,
                Constants.ProfessionCode.Pharmacist_RPH);


            // Log in to each new user
            // Chrome
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.UAMS_Pharmacist1Username, newUser: true);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.UAMS_Pharmacist2Username, newUser: true);
            LP.LogOut(siteCode);

            // Mobile
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.UAMS_Pharmacist1Username_Mobile, newUser: true);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.UAMS_Pharmacist2Username_Mobile, newUser: true);
            LP.LogOut(siteCode);

            // IE
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.UAMS_Pharmacist1Username_IE, newUser: true);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.UAMS_Pharmacist2Username_IE, newUser: true);
            LP.LogOut(siteCode);

            // FF
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.UAMS_Pharmacist1Username_FF, newUser: true);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.UAMS_Pharmacist2Username_FF, newUser: true);
            LP.LogOut(siteCode);


        }

        [Description("The below method will need executed on a new environment to create the necessary users for all tests. Also, " +
               "an API account needs created (if one has not been created already) on this new environment. When creating the " +
               "account, you need to do 3 things " +
               "1. Copy all of the IP addresses and Endpoints from an existing automation API account, then paste them in the " +
               "cooresponding fields when creating this account " +
               "2. Execute the below script on this new API account. Note that for Prod, you will need to create a ticket and " +
               "assign it to a DEV for him to apply the script to Prod. " +
               "UPDATE security.api.Account SET ApiSecuritySettings_CheckTokenEnvironment = 1, ApiSecuritySettings_CheckTokenExpiration = 1, ApiSecuritySettings_CheckSiteToken = 1, ApiSecuritySettings_CheckSiteCodesValue = NULL, ApiSecuritySettings_CheckToken = 1, ApiSecuritySettings_IsSet = 1, SecurityProviderSettings_SecretKey = NULL, SecurityProviderSettings_x509Certificate = NULL, ApiSecuritySettings_CheckAllowOriginCorsHeader = 1, AccountPreferences_IsTokenPersistenceEnabled = 1, AccountType = 3 WHERE[Key] = 'TestAuto_UAMS' " +
               "If you are copying and pasting this method for a new site (not a new environment), then you will have to perform the " +
                "above steps, along with the below step also" +
               "1. Go to UserUtils->BuildUserModel and add code for ASNC. This will drive the code in the method below to add users")]
        [Test]
        public void CreateAllStaticUsers_ONSLT()
        {
            LoginPage LP = new LoginPage(Browser);
            Constants.SiteCodes siteCode = Constants.SiteCodes.ONSLT;

            Assert.Fail("Dont run this unless you need new users for a new environment. If so, comment this Assert and run");

            // *************************************** Profession 1 ******************************************************
            UserUtils.CreateUser(siteCode, UserUtils.ONSLT_NurseScientist1Username, UserUtils.ONSLT_NurseScientist1Email, null, null,
                Constants.ProfessionCode.NurseScientist_NS);
            UserUtils.CreateUser(siteCode, UserUtils.ONSLT_NurseScientist2Username, UserUtils.ONSLT_NurseScientist2Email, null, null,
                Constants.ProfessionCode.NurseScientist_NS);

            // Mobile
            UserUtils.CreateUser(siteCode, UserUtils.ONSLT_NurseScientist1Username_Mobile, UserUtils.ONSLT_NurseScientist1Email, null, null,
                Constants.ProfessionCode.NurseScientist_NS);
            UserUtils.CreateUser(siteCode, UserUtils.ONSLT_NurseScientist2Username_Mobile, UserUtils.ONSLT_NurseScientist2Email, null, null,
                Constants.ProfessionCode.NurseScientist_NS);

            // IE
            UserUtils.CreateUser(siteCode, UserUtils.ONSLT_NurseScientist1Username_IE, UserUtils.ONSLT_NurseScientist1Email, null, null,
                Constants.ProfessionCode.NurseScientist_NS);
            UserUtils.CreateUser(siteCode, UserUtils.ONSLT_NurseScientist2Username_IE, UserUtils.ONSLT_NurseScientist2Email, null, null,
                Constants.ProfessionCode.NurseScientist_NS);

            // FF
            UserUtils.CreateUser(siteCode, UserUtils.ONSLT_NurseScientist1Username_FF, UserUtils.ONSLT_NurseScientist1Email, null, null,
                Constants.ProfessionCode.NurseScientist_NS);
            UserUtils.CreateUser(siteCode, UserUtils.ONSLT_NurseScientist2Username_FF, UserUtils.ONSLT_NurseScientist2Email, null, null,
                Constants.ProfessionCode.NurseScientist_NS);

            // Log in to each new user
            // Chrome
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.ONSLT_NurseScientist1Username, newUser: true);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.ONSLT_NurseScientist2Username, newUser: true);
            LP.LogOut(siteCode);

            // Mobile
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.ONSLT_NurseScientist1Username_Mobile, newUser: true);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.ONSLT_NurseScientist2Username_Mobile, newUser: true);
            LP.LogOut(siteCode);

            // IE
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.ONSLT_NurseScientist1Username_IE, newUser: true);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.ONSLT_NurseScientist2Username_IE, newUser: true);
            LP.LogOut(siteCode);

            // FF
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.ONSLT_NurseScientist1Username_FF, newUser: true);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.ONSLT_NurseScientist2Username_FF, newUser: true);
            LP.LogOut(siteCode);

            // *************************************** Profession 2 ******************************************************
            // Chrome
            UserUtils.CreateUser(siteCode, UserUtils.ONSLT_NursePractitioner1Username, UserUtils.ONSLT_NursePractitioner1Email, null, null,
                Constants.ProfessionCode.NursePracticioner_NPR);
            UserUtils.CreateUser(siteCode, UserUtils.ONSLT_NursePractitioner2Username, UserUtils.ONSLT_NursePractitioner2Email, null, null,
                Constants.ProfessionCode.NursePracticioner_NPR);

            // Mobile
            UserUtils.CreateUser(siteCode, UserUtils.ONSLT_NursePractitioner1Username_Mobile, UserUtils.ONSLT_NursePractitioner1Email, null, null,
                Constants.ProfessionCode.NursePracticioner_NPR);
            UserUtils.CreateUser(siteCode, UserUtils.ONSLT_NursePractitioner2Username_Mobile, UserUtils.ONSLT_NursePractitioner2Email, null, null,
                Constants.ProfessionCode.NursePracticioner_NPR);

            // IE
            UserUtils.CreateUser(siteCode, UserUtils.ONSLT_NursePractitioner1Username_IE, UserUtils.ONSLT_NursePractitioner1Email, null, null,
                Constants.ProfessionCode.NursePracticioner_NPR);
            UserUtils.CreateUser(siteCode, UserUtils.ONSLT_NursePractitioner2Username_IE, UserUtils.ONSLT_NursePractitioner2Email, null, null,
                Constants.ProfessionCode.NursePracticioner_NPR);

            // FF
            UserUtils.CreateUser(siteCode, UserUtils.ONSLT_NursePractitioner1Username_FF, UserUtils.ONSLT_NursePractitioner1Email, null, null,
                Constants.ProfessionCode.NursePracticioner_NPR);
            UserUtils.CreateUser(siteCode, UserUtils.ONSLT_NursePractitioner2Username_FF, UserUtils.ONSLT_NursePractitioner2Email, null, null,
                Constants.ProfessionCode.NursePracticioner_NPR);

            // Log in to each new user
            // Chrome
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.ONSLT_NursePractitioner1Username, newUser: true);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.ONSLT_NursePractitioner2Username, newUser: true);
            LP.LogOut(siteCode);

            // Mobile
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.ONSLT_NursePractitioner1Username_Mobile, newUser: true);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.ONSLT_NursePractitioner2Username_Mobile, newUser: true);
            LP.LogOut(siteCode);

            // IE
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.ONSLT_NursePractitioner1Username_IE, newUser: true);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.ONSLT_NursePractitioner2Username_IE, newUser: true);
            LP.LogOut(siteCode);

            // FF
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.ONSLT_NursePractitioner1Username_FF, newUser: true);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.ONSLT_NursePractitioner2Username_FF, newUser: true);
            LP.LogOut(siteCode);
        }

        [Description("The below method will need executed on a new environment to create the necessary users for all tests. Also, " +
               "an API account needs created (if one has not been created already) on this new environment. When creating the " +
               "account, you need to do 3 things " +
               "1. Copy all of the IP addresses and Endpoints from an existing automation API account, then paste them in the " +
               "cooresponding fields when creating this account " +
               "2. Execute the below script on this new API account. Note that for Prod, you will need to create a ticket and " +
               "assign it to a DEV for him to apply the script to Prod. " +
               "UPDATE security.api.Account SET ApiSecuritySettings_CheckTokenEnvironment = 1, ApiSecuritySettings_CheckTokenExpiration = 1, ApiSecuritySettings_CheckSiteToken = 1, ApiSecuritySettings_CheckSiteCodesValue = NULL, ApiSecuritySettings_CheckToken = 1, ApiSecuritySettings_IsSet = 1, SecurityProviderSettings_SecretKey = NULL, SecurityProviderSettings_x509Certificate = NULL, ApiSecuritySettings_CheckAllowOriginCorsHeader = 1, AccountPreferences_IsTokenPersistenceEnabled = 1, AccountType = 3 WHERE[Key] = 'TestAuto_UAMS' " +
               "If you are copying and pasting this method for a new site (not a new environment), then you will have to perform the " +
                "above steps, along with the below step also" +
               "1. Go to UserUtils->BuildUserModel and add code for ASNC. This will drive the code in the method below to add users")]
        [Test]
        public void CreateAllStaticUsers_CMECA()
        {
            LoginPage LP = new LoginPage(Browser);
            Constants.SiteCodes siteCode = Constants.SiteCodes.CMECA;

            Assert.Fail("Dont run this unless you need new users for a new environment. If so, comment this Assert and run");

            // *************************************** Profession 1 ******************************************************
            UserUtils.CreateUser(siteCode, UserUtils.CMECA_Physician1Username, UserUtils.CMECA_Physician1Email, null, null,
                Constants.ProfessionCode.Physician_PHY);
            UserUtils.CreateUser(siteCode, UserUtils.CMECA_Physician2Username, UserUtils.CMECA_Physician2Email, null, null,
                Constants.ProfessionCode.Physician_PHY);

            // Mobile
            UserUtils.CreateUser(siteCode, UserUtils.CMECA_Physician1Username_Mobile, UserUtils.CMECA_Physician1Email, null, null,
                Constants.ProfessionCode.Physician_PHY);
            UserUtils.CreateUser(siteCode, UserUtils.CMECA_Physician2Username_Mobile, UserUtils.CMECA_Physician2Email, null, null,
                Constants.ProfessionCode.Physician_PHY);

            // IE
            UserUtils.CreateUser(siteCode, UserUtils.CMECA_Physician1Username_IE, UserUtils.CMECA_Physician1Email, null, null,
                Constants.ProfessionCode.Physician_PHY);
            UserUtils.CreateUser(siteCode, UserUtils.CMECA_Physician2Username_IE, UserUtils.CMECA_Physician2Email, null, null,
                Constants.ProfessionCode.Physician_PHY);

            // FF
            UserUtils.CreateUser(siteCode, UserUtils.CMECA_Physician1Username_FF, UserUtils.CMECA_Physician1Email, null, null,
                Constants.ProfessionCode.Physician_PHY);
            UserUtils.CreateUser(siteCode, UserUtils.CMECA_Physician2Username_FF, UserUtils.CMECA_Physician2Email, null, null,
                Constants.ProfessionCode.Physician_PHY);

            // Log in to each new user
            // Chrome
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.CMECA_Physician1Username, newUser: true);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.CMECA_Physician2Username, newUser: true);
            LP.LogOut(siteCode);

            // Mobile
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.CMECA_Physician1Username_Mobile, newUser: true);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.CMECA_Physician2Username_Mobile, newUser: true);
            LP.LogOut(siteCode);

            // IE
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.CMECA_Physician1Username_IE, newUser: true);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.CMECA_Physician2Username_IE, newUser: true);
            LP.LogOut(siteCode);

            // FF
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.CMECA_Physician1Username_FF, newUser: true);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.CMECA_Physician2Username_FF, newUser: true);
            LP.LogOut(siteCode);

            // *************************************** Profession 2 ******************************************************
            // Chrome
            UserUtils.CreateUser(siteCode, UserUtils.CMECA_Nurse1Username, UserUtils.CMECA_Nurse1Email, null, null,
                Constants.ProfessionCode.Nurse_RN);
            UserUtils.CreateUser(siteCode, UserUtils.CMECA_Nurse2Username, UserUtils.CMECA_Nurse2Email, null, null,
                Constants.ProfessionCode.Nurse_RN);

            // Mobile
            UserUtils.CreateUser(siteCode, UserUtils.CMECA_Nurse1Username_Mobile, UserUtils.CMECA_Nurse1Email, null, null,
                Constants.ProfessionCode.Nurse_RN);
            UserUtils.CreateUser(siteCode, UserUtils.CMECA_Nurse2Username_Mobile, UserUtils.CMECA_Nurse2Email, null, null,
                Constants.ProfessionCode.Nurse_RN);

            // IE
            UserUtils.CreateUser(siteCode, UserUtils.CMECA_Nurse1Username_IE, UserUtils.CMECA_Nurse1Email, null, null,
                Constants.ProfessionCode.Nurse_RN);
            UserUtils.CreateUser(siteCode, UserUtils.CMECA_Nurse2Username_IE, UserUtils.CMECA_Nurse2Email, null, null,
                Constants.ProfessionCode.Nurse_RN);

            // FF
            UserUtils.CreateUser(siteCode, UserUtils.CMECA_Nurse1Username_FF, UserUtils.CMECA_Nurse1Email, null, null,
                Constants.ProfessionCode.Nurse_RN);
            UserUtils.CreateUser(siteCode, UserUtils.CMECA_Nurse2Username_FF, UserUtils.CMECA_Nurse2Email, null, null,
                Constants.ProfessionCode.Nurse_RN);

            // Log in to each new user
            // Chrome
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.CMECA_Nurse1Username, newUser: true);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.CMECA_Nurse2Username, newUser: true);
            LP.LogOut(siteCode);

            // Mobile
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.CMECA_Nurse1Username_Mobile, newUser: true);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.CMECA_Nurse2Username_Mobile, newUser: true);
            LP.LogOut(siteCode);

            // IE
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.CMECA_Nurse1Username_IE, newUser: true);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.CMECA_Nurse2Username_IE, newUser: true);
            LP.LogOut(siteCode);

            // FF
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.CMECA_Nurse1Username_FF, newUser: true);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.CMECA_Nurse2Username_FF, newUser: true);
            LP.LogOut(siteCode);

            // *************************************** Profession 3 ******************************************************
            // Chrome
            UserUtils.CreateUser(siteCode, UserUtils.CMECA_Pharmacist1Username, 
                UserUtils.CMECA_Pharmacist1Email, null, null, Constants.ProfessionCode.Pharmacist_RPH);
            UserUtils.CreateUser(siteCode, UserUtils.CMECA_Pharmacist1Username_Mobile,
                UserUtils.CMECA_Pharmacist1Email_Mobile, null, null, Constants.ProfessionCode.Pharmacist_RPH);
            UserUtils.CreateUser(siteCode, UserUtils.CMECA_Pharmacist1Username_IE,
                UserUtils.CMECA_Pharmacist1Email_IE, null, null, Constants.ProfessionCode.Pharmacist_RPH);
            UserUtils.CreateUser(siteCode, UserUtils.CMECA_Pharmacist1Username_FF,
                UserUtils.CMECA_Pharmacist1Email_FF, null, null, Constants.ProfessionCode.Pharmacist_RPH);

            // Log in to each new user
            // Chrome
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.CMECA_Pharmacist1Username, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.CMECA_Pharmacist1Username_Mobile, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.CMECA_Pharmacist1Username_IE, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.CMECA_Pharmacist1Username_FF, newUser: false);
            LP.LogOut(siteCode);
        }

        [Description("The below method will need executed on a new environment to create the necessary users for all tests. Also, " +
               "an API account needs created (if one has not been created already) on this new environment. When creating the " +
               "account, you need to do 3 things " +
               "1. Copy all of the IP addresses and Endpoints from an existing automation API account, then paste them in the " +
               "cooresponding fields when creating this account " +
               "2. Execute the below script on this new API account. Note that for Prod, you will need to create a ticket and " +
               "assign it to a DEV for him to apply the script to Prod. " +
               "UPDATE security.api.Account SET ApiSecuritySettings_CheckTokenEnvironment = 1, ApiSecuritySettings_CheckTokenExpiration = 1, ApiSecuritySettings_CheckSiteToken = 1, ApiSecuritySettings_CheckSiteCodesValue = NULL, ApiSecuritySettings_CheckToken = 1, ApiSecuritySettings_IsSet = 1, SecurityProviderSettings_SecretKey = NULL, SecurityProviderSettings_x509Certificate = NULL, ApiSecuritySettings_CheckAllowOriginCorsHeader = 1, AccountPreferences_IsTokenPersistenceEnabled = 1, AccountType = 3 WHERE[Key] = 'TestAuto_UAMS' " +
               "If you are copying and pasting this method for a new site (not a new environment), then you will have to perform the " +
                "above steps, along with the below step also" +
               "1. Go to UserUtils->BuildUserModel and add code for ASNC. This will drive the code in the method below to add users")]
        [Test]
        public void CreateAllStaticUsers_AHA()
        {
            LoginPage LP = new LoginPage(Browser);
            Constants.SiteCodes siteCode = Constants.SiteCodes.AHA;

            Assert.Fail("Dont run this unless you need new users for a new environment. If so, comment this Assert and run");

            // *************************************** Profession 1 ******************************************************
            UserUtils.CreateUser(siteCode, UserUtils.AHA_Physician1Username, UserUtils.AHA_Physician1Email, null, null,
                Constants.ProfessionCode.Physician_PHY);
            UserUtils.CreateUser(siteCode, UserUtils.AHA_Physician2Username, UserUtils.AHA_Physician2Email, null, null,
                Constants.ProfessionCode.Physician_PHY);

            // Mobile
            UserUtils.CreateUser(siteCode, UserUtils.AHA_Physician1Username_Mobile, UserUtils.AHA_Physician1Email, null, null,
                Constants.ProfessionCode.Physician_PHY);
            UserUtils.CreateUser(siteCode, UserUtils.AHA_Physician2Username_Mobile, UserUtils.AHA_Physician2Email, null, null,
                Constants.ProfessionCode.Physician_PHY);

            // IE
            UserUtils.CreateUser(siteCode, UserUtils.AHA_Physician1Username_IE, UserUtils.AHA_Physician1Email, null, null,
                Constants.ProfessionCode.Physician_PHY);
            UserUtils.CreateUser(siteCode, UserUtils.AHA_Physician2Username_IE, UserUtils.AHA_Physician2Email, null, null,
                Constants.ProfessionCode.Physician_PHY);

            // FF
            UserUtils.CreateUser(siteCode, UserUtils.AHA_Physician1Username_FF, UserUtils.AHA_Physician1Email, null, null,
                Constants.ProfessionCode.Physician_PHY);
            UserUtils.CreateUser(siteCode, UserUtils.AHA_Physician2Username_FF, UserUtils.AHA_Physician2Email, null, null,
                Constants.ProfessionCode.Physician_PHY);

            // Log in to each new user
            // Chrome
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.AHA_Physician1Username, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.AHA_Physician2Username, newUser: false);
            LP.LogOut(siteCode);

            // Mobile
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.AHA_Physician1Username_Mobile, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.AHA_Physician2Username_Mobile, newUser: false);
            LP.LogOut(siteCode);

            // IE
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.AHA_Physician1Username_IE, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.AHA_Physician2Username_IE, newUser: false);
            LP.LogOut(siteCode);

            // FF
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.AHA_Physician1Username_FF, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.AHA_Physician2Username_FF, newUser: false);
            LP.LogOut(siteCode);

            // *************************************** Profession 2 ******************************************************
            // Chrome
            UserUtils.CreateUser(siteCode, UserUtils.AHA_Nurse1Username, UserUtils.AHA_Nurse1Email, null, null,
                Constants.ProfessionCode.Nurse_RN);
            UserUtils.CreateUser(siteCode, UserUtils.AHA_Nurse2Username, UserUtils.AHA_Nurse2Email, null, null,
                Constants.ProfessionCode.Nurse_RN);

            // Mobile
            UserUtils.CreateUser(siteCode, UserUtils.AHA_Nurse1Username_Mobile, UserUtils.AHA_Nurse1Email, null, null,
                Constants.ProfessionCode.Nurse_RN);
            UserUtils.CreateUser(siteCode, UserUtils.AHA_Nurse2Username_Mobile, UserUtils.AHA_Nurse2Email, null, null,
                Constants.ProfessionCode.Nurse_RN);

            // IE
            UserUtils.CreateUser(siteCode, UserUtils.AHA_Nurse1Username_IE, UserUtils.AHA_Nurse1Email, null, null,
                Constants.ProfessionCode.Nurse_RN);
            UserUtils.CreateUser(siteCode, UserUtils.AHA_Nurse2Username_IE, UserUtils.AHA_Nurse2Email, null, null,
                Constants.ProfessionCode.Nurse_RN);

            // FF
            UserUtils.CreateUser(siteCode, UserUtils.AHA_Nurse1Username_FF, UserUtils.AHA_Nurse1Email, null, null,
                Constants.ProfessionCode.Nurse_RN);
            UserUtils.CreateUser(siteCode, UserUtils.AHA_Nurse2Username_FF, UserUtils.AHA_Nurse2Email, null, null,
                Constants.ProfessionCode.Nurse_RN);

            // Log in to each new user
            // Chrome
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.AHA_Nurse1Username, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.AHA_Nurse2Username, newUser: false);
            LP.LogOut(siteCode);

            // Mobile
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.AHA_Nurse1Username_Mobile, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.AHA_Nurse2Username_Mobile, newUser: false);
            LP.LogOut(siteCode);

            // IE
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.AHA_Nurse1Username_IE, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.AHA_Nurse2Username_IE, newUser: false);
            LP.LogOut(siteCode);

            // FF
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.AHA_Nurse1Username_FF, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.AHA_Nurse2Username_FF, newUser: false);
            LP.LogOut(siteCode);

            // *************************************** Profession 3 ******************************************************
            // Chrome
            UserUtils.CreateUser(siteCode, UserUtils.AHA_Pharmacist1Username,
                UserUtils.AHA_Pharmacist1Email, null, null, Constants.ProfessionCode.Pharmacist_RPH);
            UserUtils.CreateUser(siteCode, UserUtils.AHA_Pharmacist1Username_Mobile,
                UserUtils.AHA_Pharmacist1Email_Mobile, null, null, Constants.ProfessionCode.Pharmacist_RPH);
            UserUtils.CreateUser(siteCode, UserUtils.AHA_Pharmacist1Username_IE,
                UserUtils.AHA_Pharmacist1Email_IE, null, null, Constants.ProfessionCode.Pharmacist_RPH);
            UserUtils.CreateUser(siteCode, UserUtils.AHA_Pharmacist1Username_FF,
                UserUtils.AHA_Pharmacist1Email_FF, null, null, Constants.ProfessionCode.Pharmacist_RPH);

            // Log in to each new user
            // Chrome
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.AHA_Pharmacist1Username, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.AHA_Pharmacist1Username_Mobile, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.AHA_Pharmacist1Username_IE, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.AHA_Pharmacist1Username_FF, newUser: false);
            LP.LogOut(siteCode);

            // *************************************** Profession 1 (Member) ******************************************************
            UserUtils.CreateUser(siteCode, UserUtils.AHA_PhysicianMember1Username, UserUtils.AHA_PhysicianMember1Email, null, null,
                Constants.ProfessionCode.Physician_PHY, true);
            UserUtils.CreateUser(siteCode, UserUtils.AHA_PhysicianMember2Username, UserUtils.AHA_PhysicianMember2Email, null, null,
                Constants.ProfessionCode.Physician_PHY, true);
            UserUtils.CreateUser(siteCode, UserUtils.AHA_PhysicianMember3Username, UserUtils.AHA_PhysicianMember3Email, null, null,
                Constants.ProfessionCode.Physician_PHY, true);
            UserUtils.CreateUser(siteCode, UserUtils.AHA_PhysicianMember4Username, UserUtils.AHA_PhysicianMember4Email, null, null,
                Constants.ProfessionCode.Physician_PHY, true);
            UserUtils.CreateUser(siteCode, UserUtils.AHA_PhysicianMember5Username, UserUtils.AHA_PhysicianMember5Email, null, null,
                Constants.ProfessionCode.Physician_PHY, true);
            UserUtils.CreateUser(siteCode, UserUtils.AHA_PhysicianMember6Username, UserUtils.AHA_PhysicianMember6Email, null, null,
    Constants.ProfessionCode.Physician_PHY, true);
            UserUtils.CreateUser(siteCode, UserUtils.AHA_PhysicianMember7Username, UserUtils.AHA_PhysicianMember7Email, null, null,
    Constants.ProfessionCode.Physician_PHY, true);
            UserUtils.CreateUser(siteCode, UserUtils.AHA_PhysicianMember8Username, UserUtils.AHA_PhysicianMember8Email, null, null,
    Constants.ProfessionCode.Physician_PHY, true);

            // Mobile
            UserUtils.CreateUser(siteCode, UserUtils.AHA_PhysicianMember1Username_Mobile, UserUtils.AHA_PhysicianMember1Email, null, null,
                Constants.ProfessionCode.Physician_PHY, true);
            UserUtils.CreateUser(siteCode, UserUtils.AHA_PhysicianMember2Username_Mobile, UserUtils.AHA_PhysicianMember2Email, null, null,
                Constants.ProfessionCode.Physician_PHY, true);
            UserUtils.CreateUser(siteCode, UserUtils.AHA_PhysicianMember3Username_Mobile, UserUtils.AHA_PhysicianMember3Email, null, null,
                Constants.ProfessionCode.Physician_PHY, true);
            UserUtils.CreateUser(siteCode, UserUtils.AHA_PhysicianMember4Username_Mobile, UserUtils.AHA_PhysicianMember4Email, null, null,
                Constants.ProfessionCode.Physician_PHY, true);
            UserUtils.CreateUser(siteCode, UserUtils.AHA_PhysicianMember5Username_Mobile, UserUtils.AHA_PhysicianMember5Email, null, null,
                Constants.ProfessionCode.Physician_PHY, true);
            UserUtils.CreateUser(siteCode, UserUtils.AHA_PhysicianMember6Username_Mobile, UserUtils.AHA_PhysicianMember6Email, null, null,
    Constants.ProfessionCode.Physician_PHY, true);
            UserUtils.CreateUser(siteCode, UserUtils.AHA_PhysicianMember7Username_Mobile, UserUtils.AHA_PhysicianMember7Email, null, null,
    Constants.ProfessionCode.Physician_PHY, true);
            UserUtils.CreateUser(siteCode, UserUtils.AHA_PhysicianMember8Username_Mobile, UserUtils.AHA_PhysicianMember8Email, null, null,
    Constants.ProfessionCode.Physician_PHY, true);

            // IE
            UserUtils.CreateUser(siteCode, UserUtils.AHA_PhysicianMember1Username_IE, UserUtils.AHA_PhysicianMember1Email, null, null,
                Constants.ProfessionCode.Physician_PHY, true);
            UserUtils.CreateUser(siteCode, UserUtils.AHA_PhysicianMember2Username_IE, UserUtils.AHA_PhysicianMember2Email, null, null,
                Constants.ProfessionCode.Physician_PHY, true);
            UserUtils.CreateUser(siteCode, UserUtils.AHA_PhysicianMember3Username_IE, UserUtils.AHA_PhysicianMember3Email, null, null,
                Constants.ProfessionCode.Physician_PHY, true);
            UserUtils.CreateUser(siteCode, UserUtils.AHA_PhysicianMember4Username_IE, UserUtils.AHA_PhysicianMember4Email, null, null,
                Constants.ProfessionCode.Physician_PHY, true);
            UserUtils.CreateUser(siteCode, UserUtils.AHA_PhysicianMember5Username_IE, UserUtils.AHA_PhysicianMember5Email, null, null,
                Constants.ProfessionCode.Physician_PHY, true);
            UserUtils.CreateUser(siteCode, UserUtils.AHA_PhysicianMember6Username_IE, UserUtils.AHA_PhysicianMember6Email, null, null,
    Constants.ProfessionCode.Physician_PHY, true);
            UserUtils.CreateUser(siteCode, UserUtils.AHA_PhysicianMember7Username_IE, UserUtils.AHA_PhysicianMember7Email, null, null,
    Constants.ProfessionCode.Physician_PHY, true);
            UserUtils.CreateUser(siteCode, UserUtils.AHA_PhysicianMember8Username_IE, UserUtils.AHA_PhysicianMember8Email, null, null,
    Constants.ProfessionCode.Physician_PHY, true);

            // FF
            UserUtils.CreateUser(siteCode, UserUtils.AHA_PhysicianMember1Username_FF, UserUtils.AHA_PhysicianMember1Email, null, null,
                Constants.ProfessionCode.Physician_PHY, true);
            UserUtils.CreateUser(siteCode, UserUtils.AHA_PhysicianMember2Username_FF, UserUtils.AHA_PhysicianMember2Email, null, null,
                Constants.ProfessionCode.Physician_PHY, true);
            UserUtils.CreateUser(siteCode, UserUtils.AHA_PhysicianMember3Username_FF, UserUtils.AHA_PhysicianMember3Email, null, null,
                Constants.ProfessionCode.Physician_PHY, true);
            UserUtils.CreateUser(siteCode, UserUtils.AHA_PhysicianMember4Username_FF, UserUtils.AHA_PhysicianMember4Email, null, null,
                Constants.ProfessionCode.Physician_PHY, true);
            UserUtils.CreateUser(siteCode, UserUtils.AHA_PhysicianMember5Username_FF, UserUtils.AHA_PhysicianMember5Email, null, null,
                Constants.ProfessionCode.Physician_PHY, true);
            UserUtils.CreateUser(siteCode, UserUtils.AHA_PhysicianMember6Username_FF, UserUtils.AHA_PhysicianMember6Email, null, null,
    Constants.ProfessionCode.Physician_PHY, true);
            UserUtils.CreateUser(siteCode, UserUtils.AHA_PhysicianMember7Username_FF, UserUtils.AHA_PhysicianMember7Email, null, null,
    Constants.ProfessionCode.Physician_PHY, true);
            UserUtils.CreateUser(siteCode, UserUtils.AHA_PhysicianMember8Username_FF, UserUtils.AHA_PhysicianMember8Email, null, null,
    Constants.ProfessionCode.Physician_PHY, true);

            // Log in to each new user
            // Chrome
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.AHA_PhysicianMember1Username, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.AHA_PhysicianMember2Username, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.AHA_PhysicianMember3Username, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.AHA_PhysicianMember4Username, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.AHA_PhysicianMember5Username, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.AHA_PhysicianMember6Username, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.AHA_PhysicianMember7Username, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.AHA_PhysicianMember8Username, newUser: false);
            LP.LogOut(siteCode);

            // Mobile
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.AHA_PhysicianMember1Username_Mobile, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.AHA_PhysicianMember2Username_Mobile, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.AHA_PhysicianMember3Username_Mobile, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.AHA_PhysicianMember4Username_Mobile, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.AHA_PhysicianMember5Username_Mobile, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.AHA_PhysicianMember6Username_Mobile, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.AHA_PhysicianMember7Username_Mobile, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.AHA_PhysicianMember8Username_Mobile, newUser: false);
            LP.LogOut(siteCode);

            // IE
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.AHA_PhysicianMember1Username_IE, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.AHA_PhysicianMember2Username_IE, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.AHA_PhysicianMember3Username_IE, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.AHA_PhysicianMember4Username_IE, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.AHA_PhysicianMember5Username_IE, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.AHA_PhysicianMember6Username_IE, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.AHA_PhysicianMember7Username_IE, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.AHA_PhysicianMember8Username_IE, newUser: false);
            LP.LogOut(siteCode);

            // FF
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.AHA_PhysicianMember1Username_FF, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.AHA_PhysicianMember2Username_FF, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.AHA_PhysicianMember3Username_FF, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.AHA_PhysicianMember4Username_FF, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.AHA_PhysicianMember5Username_FF, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.AHA_PhysicianMember6Username_FF, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.AHA_PhysicianMember7Username_FF, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.AHA_PhysicianMember8Username_FF, newUser: false);
            LP.LogOut(siteCode);

        }

        [Description("The below method will need executed on a new environment to create the necessary users for all tests. Also, " +
               "an API account needs created (if one has not been created already) on this new environment. When creating the " +
               "account, you need to do 3 things " +
               "1. Copy all of the IP addresses and Endpoints from an existing automation API account, then paste them in the " +
               "cooresponding fields when creating this account " +
               "2. Execute the below script on this new API account. Note that for Prod, you will need to create a ticket and " +
               "assign it to a DEV for him to apply the script to Prod. " +
               "UPDATE security.api.Account SET ApiSecuritySettings_CheckTokenEnvironment = 1, ApiSecuritySettings_CheckTokenExpiration = 1, ApiSecuritySettings_CheckSiteToken = 1, ApiSecuritySettings_CheckSiteCodesValue = NULL, ApiSecuritySettings_CheckToken = 1, ApiSecuritySettings_IsSet = 1, SecurityProviderSettings_SecretKey = NULL, SecurityProviderSettings_x509Certificate = NULL, ApiSecuritySettings_CheckAllowOriginCorsHeader = 1, AccountPreferences_IsTokenPersistenceEnabled = 1, AccountType = 3 WHERE[Key] = 'TestAuto_UAMS' " +
               "If you are copying and pasting this method for a new site (not a new environment), then you will have to perform the " +
                "above steps, along with the below step also" +
               "1. Go to UserUtils->BuildUserModel and add code for ASNC. This will drive the code in the method below to add users")]
        [Test]
        public void CreateAllStaticUsers_ACR()
        {
            LoginPage LP = new LoginPage(Browser);
            Constants.SiteCodes siteCode = Constants.SiteCodes.ACR;

            Assert.Fail("Dont run this unless you need new users for a new environment. If so, comment this Assert and run");

            // *************************************** Profession 1 ******************************************************
            // Chrome
            UserUtils.CreateUser(siteCode, UserUtils.ACR_Nurse1Username, UserUtils.ACR_Nurse1Email, null, null,
                Constants.ProfessionCode.Nurse_RN);
            UserUtils.CreateUser(siteCode, UserUtils.ACR_Nurse2Username, UserUtils.ACR_Nurse2Email, null, null,
                Constants.ProfessionCode.Nurse_RN);

            // Mobile
            UserUtils.CreateUser(siteCode, UserUtils.ACR_Nurse1Username_Mobile, UserUtils.ACR_Nurse1Email, null, null,
                Constants.ProfessionCode.Nurse_RN);
            UserUtils.CreateUser(siteCode, UserUtils.ACR_Nurse2Username_Mobile, UserUtils.ACR_Nurse2Email, null, null,
                Constants.ProfessionCode.Nurse_RN);

            // IE
            UserUtils.CreateUser(siteCode, UserUtils.ACR_Nurse1Username_IE, UserUtils.ACR_Nurse1Email, null, null,
                Constants.ProfessionCode.Nurse_RN);
            UserUtils.CreateUser(siteCode, UserUtils.ACR_Nurse2Username_IE, UserUtils.ACR_Nurse2Email, null, null,
                Constants.ProfessionCode.Nurse_RN);

            // FF
            UserUtils.CreateUser(siteCode, UserUtils.ACR_Nurse1Username_FF, UserUtils.ACR_Nurse1Email, null, null,
                Constants.ProfessionCode.Nurse_RN);
            UserUtils.CreateUser(siteCode, UserUtils.ACR_Nurse2Username_FF, UserUtils.ACR_Nurse2Email, null, null,
                Constants.ProfessionCode.Nurse_RN);

            // Log in to each new user
            // Chrome
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.ACR_Nurse1Username, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.ACR_Nurse2Username, newUser: false);
            LP.LogOut(siteCode);

            // Mobile
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.ACR_Nurse1Username_Mobile, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.ACR_Nurse2Username_Mobile, newUser: false);
            LP.LogOut(siteCode);

            // IE
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.ACR_Nurse1Username_IE, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.ACR_Nurse2Username_IE, newUser: false);
            LP.LogOut(siteCode);

            // FF
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.ACR_Nurse1Username_FF, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.ACR_Nurse2Username_FF, newUser: false);
            LP.LogOut(siteCode);

        }

        [Description("The below method will need executed on a new environment to create the necessary users for all tests. Also, " +
               "an API account needs created (if one has not been created already) on this new environment. When creating the " +
               "account, you need to do 3 things " +
               "1. Copy all of the IP addresses and Endpoints from an existing automation API account, then paste them in the " +
               "cooresponding fields when creating this account " +
               "2. Execute the below script on this new API account. Note that for Prod, you will need to create a ticket and " +
               "assign it to a DEV for him to apply the script to Prod. " +
               "UPDATE security.api.Account SET ApiSecuritySettings_CheckTokenEnvironment = 1, ApiSecuritySettings_CheckTokenExpiration = 1, ApiSecuritySettings_CheckSiteToken = 1, ApiSecuritySettings_CheckSiteCodesValue = NULL, ApiSecuritySettings_CheckToken = 1, ApiSecuritySettings_IsSet = 1, SecurityProviderSettings_SecretKey = NULL, SecurityProviderSettings_x509Certificate = NULL, ApiSecuritySettings_CheckAllowOriginCorsHeader = 1, AccountPreferences_IsTokenPersistenceEnabled = 1, AccountType = 3 WHERE[Key] = 'TestAuto_UAMS' " +
               "If you are copying and pasting this method for a new site (not a new environment), then you will have to perform the " +
                "above steps, along with the below step also" +
               "1. Go to UserUtils->BuildUserModel and add code for ASNC. This will drive the code in the method below to add users")]
        [Test]
        public void CreateAllStaticUsers_ASNC()
        {
            LoginPage LP = new LoginPage(Browser);
            Constants.SiteCodes siteCode = Constants.SiteCodes.ASNC;

            Assert.Fail("Dont run this unless you need new users for a new environment. If so, comment this Assert and run");

            // *************************************** Profession 1 ******************************************************
            UserUtils.CreateUser(siteCode, UserUtils.ASNC_Physician1Username, UserUtils.ASNC_Physician1Email, null, null,
                Constants.ProfessionCode.Physician_PHY);
            UserUtils.CreateUser(siteCode, UserUtils.ASNC_Physician2Username, UserUtils.ASNC_Physician2Email, null, null,
                Constants.ProfessionCode.Physician_PHY);

            // Mobile
            UserUtils.CreateUser(siteCode, UserUtils.ASNC_Physician1Username_Mobile, UserUtils.ASNC_Physician1Email, null, null,
                Constants.ProfessionCode.Physician_PHY);
            UserUtils.CreateUser(siteCode, UserUtils.ASNC_Physician2Username_Mobile, UserUtils.ASNC_Physician2Email, null, null,
                Constants.ProfessionCode.Physician_PHY);

            // IE
            UserUtils.CreateUser(siteCode, UserUtils.ASNC_Physician1Username_IE, UserUtils.ASNC_Physician1Email, null, null,
                Constants.ProfessionCode.Physician_PHY);
            UserUtils.CreateUser(siteCode, UserUtils.ASNC_Physician2Username_IE, UserUtils.ASNC_Physician2Email, null, null,
                Constants.ProfessionCode.Physician_PHY);

            // FF
            UserUtils.CreateUser(siteCode, UserUtils.ASNC_Physician1Username_FF, UserUtils.ASNC_Physician1Email, null, null,
                Constants.ProfessionCode.Physician_PHY);
            UserUtils.CreateUser(siteCode, UserUtils.ASNC_Physician2Username_FF, UserUtils.ASNC_Physician2Email, null, null,
                Constants.ProfessionCode.Physician_PHY);

            // Log in to each new user
            // Chrome
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.ASNC_Physician1Username, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.ASNC_Physician2Username, newUser: false);
            LP.LogOut(siteCode);

            // Mobile
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.ASNC_Physician1Username_Mobile, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.ASNC_Physician2Username_Mobile, newUser: false);
            LP.LogOut(siteCode);

            // IE
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.ASNC_Physician1Username_IE, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.ASNC_Physician2Username_IE, newUser: false);
            LP.LogOut(siteCode);

            // FF
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.ASNC_Physician1Username_FF, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.ASNC_Physician2Username_FF, newUser: false);
            LP.LogOut(siteCode);

            // *************************************** Profession 2 ******************************************************
            // Chrome
            UserUtils.CreateUser(siteCode, UserUtils.ASNC_Nurse1Username, UserUtils.ASNC_Nurse1Email, null, null,
                Constants.ProfessionCode.Nurse_RN);
            UserUtils.CreateUser(siteCode, UserUtils.ASNC_Nurse2Username, UserUtils.ASNC_Nurse2Email, null, null,
                Constants.ProfessionCode.Nurse_RN);

            // Mobile
            UserUtils.CreateUser(siteCode, UserUtils.ASNC_Nurse1Username_Mobile, UserUtils.ASNC_Nurse1Email, null, null,
                Constants.ProfessionCode.Nurse_RN);
            UserUtils.CreateUser(siteCode, UserUtils.ASNC_Nurse2Username_Mobile, UserUtils.ASNC_Nurse2Email, null, null,
                Constants.ProfessionCode.Nurse_RN);

            // IE
            UserUtils.CreateUser(siteCode, UserUtils.ASNC_Nurse1Username_IE, UserUtils.ASNC_Nurse1Email, null, null,
                Constants.ProfessionCode.Nurse_RN);
            UserUtils.CreateUser(siteCode, UserUtils.ASNC_Nurse2Username_IE, UserUtils.ASNC_Nurse2Email, null, null,
                Constants.ProfessionCode.Nurse_RN);

            // FF
            UserUtils.CreateUser(siteCode, UserUtils.ASNC_Nurse1Username_FF, UserUtils.ASNC_Nurse1Email, null, null,
                Constants.ProfessionCode.Nurse_RN);
            UserUtils.CreateUser(siteCode, UserUtils.ASNC_Nurse2Username_FF, UserUtils.ASNC_Nurse2Email, null, null,
                Constants.ProfessionCode.Nurse_RN);

            // Log in to each new user
            // Chrome
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.ASNC_Nurse1Username, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.ASNC_Nurse2Username, newUser: false);
            LP.LogOut(siteCode);

            // Mobile
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.ASNC_Nurse1Username_Mobile, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.ASNC_Nurse2Username_Mobile, newUser: false);
            LP.LogOut(siteCode);

            // IE
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.ASNC_Nurse1Username_IE, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.ASNC_Nurse2Username_IE, newUser: false);
            LP.LogOut(siteCode);

            // FF
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.ASNC_Nurse1Username_FF, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.ASNC_Nurse2Username_FF, newUser: false);
            LP.LogOut(siteCode);
        }

        [Description("The below method will need executed on a new environment to create the necessary users for all tests. Also, " +
       "an API account needs created (if one has not been created already) on this new environment. When creating the " +
       "account, you need to do 3 things " +
       "1. Copy all of the IP addresses and Endpoints from an existing automation API account, then paste them in the " +
       "cooresponding fields when creating this account " +
       "2. Execute the below script on this new API account. Note that for Prod, you will need to create a ticket and " +
       "assign it to a DEV for him to apply the script to Prod. " +
       "UPDATE security.api.Account SET ApiSecuritySettings_CheckTokenEnvironment = 1, ApiSecuritySettings_CheckTokenExpiration = 1, ApiSecuritySettings_CheckSiteToken = 1, ApiSecuritySettings_CheckSiteCodesValue = NULL, ApiSecuritySettings_CheckToken = 1, ApiSecuritySettings_IsSet = 1, SecurityProviderSettings_SecretKey = NULL, SecurityProviderSettings_x509Certificate = NULL, ApiSecuritySettings_CheckAllowOriginCorsHeader = 1, AccountPreferences_IsTokenPersistenceEnabled = 1, AccountType = 3 WHERE[Key] = 'TestAuto_UAMS' " +
       "If you are copying and pasting this method for a new site (not a new environment), then you will have to perform the " +
        "above steps, along with the below step also" +
       "1. Go to UserUtils->BuildUserModel and add code for CAP. This will drive the code in the method below to add users")]
        [Test]
        public void CreateAllStaticUsers_CAP()
        {
            LoginPage LP = new LoginPage(Browser);
            Constants.SiteCodes siteCode = Constants.SiteCodes.CAP;

            Assert.Fail("Dont run this unless you need new users for a new environment. If so, comment this Assert and run");

            // *************************************** Profession 1 ******************************************************
            UserUtils.CreateUser(siteCode, UserUtils.CAP_Physician1Username, UserUtils.CAP_Physician1Email, null, null,
                Constants.ProfessionCode.Physician_PHY);
            UserUtils.CreateUser(siteCode, UserUtils.CAP_Physician2Username, UserUtils.CAP_Physician2Email, null, null,
                Constants.ProfessionCode.Physician_PHY);

            // Mobile
            UserUtils.CreateUser(siteCode, UserUtils.CAP_Physician1Username_Mobile, UserUtils.CAP_Physician1Email, null, null,
                Constants.ProfessionCode.Physician_PHY);
            UserUtils.CreateUser(siteCode, UserUtils.CAP_Physician2Username_Mobile, UserUtils.CAP_Physician2Email, null, null,
                Constants.ProfessionCode.Physician_PHY);

            // IE
            UserUtils.CreateUser(siteCode, UserUtils.CAP_Physician1Username_IE, UserUtils.CAP_Physician1Email, null, null,
                Constants.ProfessionCode.Physician_PHY);
            UserUtils.CreateUser(siteCode, UserUtils.CAP_Physician2Username_IE, UserUtils.CAP_Physician2Email, null, null,
                Constants.ProfessionCode.Physician_PHY);

            // FF
            UserUtils.CreateUser(siteCode, UserUtils.CAP_Physician1Username_FF, UserUtils.CAP_Physician1Email, null, null,
                Constants.ProfessionCode.Physician_PHY);
            UserUtils.CreateUser(siteCode, UserUtils.CAP_Physician2Username_FF, UserUtils.CAP_Physician2Email, null, null,
                Constants.ProfessionCode.Physician_PHY);

            // Log in to each new user
            // Chrome
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.CAP_Physician1Username, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.CAP_Physician2Username, newUser: false);
            LP.LogOut(siteCode);

            // Mobile
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.CAP_Physician1Username_Mobile, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.CAP_Physician2Username_Mobile, newUser: false);
            LP.LogOut(siteCode);

            // IE
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.CAP_Physician1Username_IE, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.CAP_Physician2Username_IE, newUser: false);
            LP.LogOut(siteCode);

            // FF
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.CAP_Physician1Username_FF, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.CAP_Physician2Username_FF, newUser: false);
            LP.LogOut(siteCode);

            // *************************************** Profession 2 ******************************************************
            // Chrome
            UserUtils.CreateUser(siteCode, UserUtils.CAP_Nurse1Username, UserUtils.CAP_Nurse1Email, null, null,
                Constants.ProfessionCode.Nurse_RN);
            UserUtils.CreateUser(siteCode, UserUtils.CAP_Nurse2Username, UserUtils.CAP_Nurse2Email, null, null,
                Constants.ProfessionCode.Nurse_RN);

            // Mobile
            UserUtils.CreateUser(siteCode, UserUtils.CAP_Nurse1Username_Mobile, UserUtils.CAP_Nurse1Email, null, null,
                Constants.ProfessionCode.Nurse_RN);
            UserUtils.CreateUser(siteCode, UserUtils.CAP_Nurse2Username_Mobile, UserUtils.CAP_Nurse2Email, null, null,
                Constants.ProfessionCode.Nurse_RN);

            // IE
            UserUtils.CreateUser(siteCode, UserUtils.CAP_Nurse1Username_IE, UserUtils.CAP_Nurse1Email, null, null,
                Constants.ProfessionCode.Nurse_RN);
            UserUtils.CreateUser(siteCode, UserUtils.CAP_Nurse2Username_IE, UserUtils.CAP_Nurse2Email, null, null,
                Constants.ProfessionCode.Nurse_RN);

            // FF
            UserUtils.CreateUser(siteCode, UserUtils.CAP_Nurse1Username_FF, UserUtils.CAP_Nurse1Email, null, null,
                Constants.ProfessionCode.Nurse_RN);
            UserUtils.CreateUser(siteCode, UserUtils.CAP_Nurse2Username_FF, UserUtils.CAP_Nurse2Email, null, null,
                Constants.ProfessionCode.Nurse_RN);

            // Log in to each new user
            // Chrome
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.CAP_Nurse1Username, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.CAP_Nurse2Username, newUser: false);
            LP.LogOut(siteCode);

            // Mobile
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.CAP_Nurse1Username_Mobile, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.CAP_Nurse2Username_Mobile, newUser: false);
            LP.LogOut(siteCode);

            // IE
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.CAP_Nurse1Username_IE, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.CAP_Nurse2Username_IE, newUser: false);
            LP.LogOut(siteCode);

            // FF
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.CAP_Nurse1Username_FF, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.CAP_Nurse2Username_FF, newUser: false);
            LP.LogOut(siteCode);
        }

        [Description("The below method will need executed on a new environment to create the necessary users for all tests. Also, " +
                    "an API account needs created (if one has not been created already) on this new environment. When creating the " +
                    "account, you need to do 3 things " +
                    "1. Copy all of the IP addresses and Endpoints from an existing automation API account, then paste them in the " +
                    "cooresponding fields when creating this account " +
                    "2. Execute the below script on this new API account. Note that for Prod, you will need to create a ticket and " +
                    "assign it to a DEV for him to apply the script to Prod. " +
                    "UPDATE security.api.Account SET ApiSecuritySettings_CheckTokenEnvironment = 1, ApiSecuritySettings_CheckTokenExpiration = 1, ApiSecuritySettings_CheckSiteToken = 1, ApiSecuritySettings_CheckSiteCodesValue = NULL, ApiSecuritySettings_CheckToken = 1, ApiSecuritySettings_IsSet = 1, SecurityProviderSettings_SecretKey = NULL, SecurityProviderSettings_x509Certificate = NULL, ApiSecuritySettings_CheckAllowOriginCorsHeader = 1, AccountPreferences_IsTokenPersistenceEnabled = 1, AccountType = 3 WHERE[Key] = 'TestAuto_UAMS' " +
                    "If you are copying and pasting this method for a new site (not a new environment), then you will have to perform the " +
                    "above steps, along with the below step also" +
                    "1. Go to UserUtils->BuildUserModel and add code for SNMMI. This will drive the code in the method below to add users")]
        [Test]
        public void CreateAllStaticUsers_SNMMI()
        {
            LoginPage LP = new LoginPage(Browser);
            Constants.SiteCodes siteCode = Constants.SiteCodes.SNMMI;

            Assert.Fail("Dont run this unless you need new users for a new environment. If so, comment this Assert and run");

            // *************************************** Profession 1 ******************************************************
            UserUtils.CreateUser(siteCode, UserUtils.SNMMI_Physician1Username, UserUtils.SNMMI_Physician1Email, null, null,
                Constants.ProfessionCode.Physician_PHY);
            UserUtils.CreateUser(siteCode, UserUtils.SNMMI_Physician2Username, UserUtils.SNMMI_Physician2Email, null, null,
                Constants.ProfessionCode.Physician_PHY);

            // Mobile
            UserUtils.CreateUser(siteCode, UserUtils.SNMMI_Physician1Username_Mobile, UserUtils.SNMMI_Physician1Email, null, null,
                Constants.ProfessionCode.Physician_PHY);
            UserUtils.CreateUser(siteCode, UserUtils.SNMMI_Physician2Username_Mobile, UserUtils.SNMMI_Physician2Email, null, null,
                Constants.ProfessionCode.Physician_PHY);

            // IE
            UserUtils.CreateUser(siteCode, UserUtils.SNMMI_Physician1Username_IE, UserUtils.SNMMI_Physician1Email, null, null,
                Constants.ProfessionCode.Physician_PHY);
            UserUtils.CreateUser(siteCode, UserUtils.SNMMI_Physician2Username_IE, UserUtils.SNMMI_Physician2Email, null, null,
                Constants.ProfessionCode.Physician_PHY);

            // FF
            UserUtils.CreateUser(siteCode, UserUtils.SNMMI_Physician1Username_FF, UserUtils.SNMMI_Physician1Email, null, null,
                Constants.ProfessionCode.Physician_PHY);
            UserUtils.CreateUser(siteCode, UserUtils.SNMMI_Physician2Username_FF, UserUtils.SNMMI_Physician2Email, null, null,
                Constants.ProfessionCode.Physician_PHY);

            // Log in to each new user
            // Chrome
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.SNMMI_Physician1Username, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.SNMMI_Physician2Username, newUser: false);
            LP.LogOut(siteCode);

            // Mobile
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.SNMMI_Physician1Username_Mobile, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.SNMMI_Physician2Username_Mobile, newUser: false);
            LP.LogOut(siteCode);

            // IE
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.SNMMI_Physician1Username_IE, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.SNMMI_Physician2Username_IE, newUser: false);
            LP.LogOut(siteCode);

            // FF
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.SNMMI_Physician1Username_FF, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.SNMMI_Physician2Username_FF, newUser: false);
            LP.LogOut(siteCode);

            // *************************************** Profession 2 ******************************************************
            // Chrome
            UserUtils.CreateUser(siteCode, UserUtils.SNMMI_Nurse1Username, UserUtils.SNMMI_Nurse1Email, null, null,
                Constants.ProfessionCode.Nurse_RN);
            UserUtils.CreateUser(siteCode, UserUtils.SNMMI_Nurse2Username, UserUtils.SNMMI_Nurse2Email, null, null,
                Constants.ProfessionCode.Nurse_RN);

            // Mobile
            UserUtils.CreateUser(siteCode, UserUtils.SNMMI_Nurse1Username_Mobile, UserUtils.SNMMI_Nurse1Email, null, null,
                Constants.ProfessionCode.Nurse_RN);
            UserUtils.CreateUser(siteCode, UserUtils.SNMMI_Nurse2Username_Mobile, UserUtils.SNMMI_Nurse2Email, null, null,
                Constants.ProfessionCode.Nurse_RN);

            // IE
            UserUtils.CreateUser(siteCode, UserUtils.SNMMI_Nurse1Username_IE, UserUtils.SNMMI_Nurse1Email, null, null,
                Constants.ProfessionCode.Nurse_RN);
            UserUtils.CreateUser(siteCode, UserUtils.SNMMI_Nurse2Username_IE, UserUtils.SNMMI_Nurse2Email, null, null,
                Constants.ProfessionCode.Nurse_RN);

            // FF
            UserUtils.CreateUser(siteCode, UserUtils.SNMMI_Nurse1Username_FF, UserUtils.SNMMI_Nurse1Email, null, null,
                Constants.ProfessionCode.Nurse_RN);
            UserUtils.CreateUser(siteCode, UserUtils.SNMMI_Nurse2Username_FF, UserUtils.SNMMI_Nurse2Email, null, null,
                Constants.ProfessionCode.Nurse_RN);

            // Log in to each new user
            // Chrome
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.SNMMI_Nurse1Username, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.SNMMI_Nurse2Username, newUser: false);
            LP.LogOut(siteCode);

            // Mobile
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.SNMMI_Nurse1Username_Mobile, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.SNMMI_Nurse2Username_Mobile, newUser: false);
            LP.LogOut(siteCode);

            // IE
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.SNMMI_Nurse1Username_IE, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.SNMMI_Nurse2Username_IE, newUser: false);
            LP.LogOut(siteCode);

            // FF
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.SNMMI_Nurse1Username_FF, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.SNMMI_Nurse2Username_FF, newUser: false);
            LP.LogOut(siteCode);


            // *************************************** Profession 1 (Member) ******************************************************
            UserUtils.CreateUser(siteCode, UserUtils.SNMMI_PhysicianMember1Username, UserUtils.SNMMI_PhysicianMember1Email, null, null,
                Constants.ProfessionCode.Physician_PHY, true);
            UserUtils.CreateUser(siteCode, UserUtils.SNMMI_PhysicianMember2Username, UserUtils.SNMMI_PhysicianMember2Email, null, null,
                Constants.ProfessionCode.Physician_PHY, true);
            UserUtils.CreateUser(siteCode, UserUtils.SNMMI_PhysicianMember3Username, UserUtils.SNMMI_PhysicianMember3Email, null, null,
                Constants.ProfessionCode.Physician_PHY, true);

            // Mobile
            UserUtils.CreateUser(siteCode, UserUtils.SNMMI_PhysicianMember1Username_Mobile, UserUtils.SNMMI_PhysicianMember1Email, 
                null, null, Constants.ProfessionCode.Physician_PHY, true);
            UserUtils.CreateUser(siteCode, UserUtils.SNMMI_PhysicianMember2Username_Mobile, UserUtils.SNMMI_PhysicianMember2Email, 
                null, null, Constants.ProfessionCode.Physician_PHY, true);
            UserUtils.CreateUser(siteCode, UserUtils.SNMMI_PhysicianMember3Username_Mobile, UserUtils.SNMMI_PhysicianMember3Email,
                null, null, Constants.ProfessionCode.Physician_PHY, true);

            // IE
            UserUtils.CreateUser(siteCode, UserUtils.SNMMI_PhysicianMember1Username_IE, UserUtils.SNMMI_PhysicianMember1Email, 
                null, null, Constants.ProfessionCode.Physician_PHY, true);
            UserUtils.CreateUser(siteCode, UserUtils.SNMMI_PhysicianMember2Username_IE, UserUtils.SNMMI_PhysicianMember2Email, 
                null, null, Constants.ProfessionCode.Physician_PHY, true);
            UserUtils.CreateUser(siteCode, UserUtils.SNMMI_PhysicianMember3Username_IE, UserUtils.SNMMI_PhysicianMember3Email,
                null, null, Constants.ProfessionCode.Physician_PHY, true);

            // FF
            UserUtils.CreateUser(siteCode, UserUtils.SNMMI_PhysicianMember1Username_FF, UserUtils.SNMMI_PhysicianMember1Email, 
                null, null, Constants.ProfessionCode.Physician_PHY, true);
            UserUtils.CreateUser(siteCode, UserUtils.SNMMI_PhysicianMember2Username_FF, UserUtils.SNMMI_PhysicianMember2Email, 
                null, null, Constants.ProfessionCode.Physician_PHY, true);
            UserUtils.CreateUser(siteCode, UserUtils.SNMMI_PhysicianMember3Username_FF, UserUtils.SNMMI_PhysicianMember3Email,
                null, null, Constants.ProfessionCode.Physician_PHY, true);

            // Log in to each new user
            // Chrome
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.SNMMI_PhysicianMember1Username, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.SNMMI_PhysicianMember2Username, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.SNMMI_PhysicianMember3Username, newUser: false);
            LP.LogOut(siteCode);

            // Mobile
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.SNMMI_PhysicianMember1Username_Mobile, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.SNMMI_PhysicianMember2Username_Mobile, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.SNMMI_PhysicianMember3Username_Mobile, newUser: false);
            LP.LogOut(siteCode);

            // IE
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.SNMMI_PhysicianMember1Username_IE, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.SNMMI_PhysicianMember2Username_IE, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.SNMMI_PhysicianMember3Username_IE, newUser: false);
            LP.LogOut(siteCode);

            // FF
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.SNMMI_PhysicianMember1Username_FF, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.SNMMI_PhysicianMember2Username_FF, newUser: false);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.SNMMI_PhysicianMember3Username_FF, newUser: false);
            LP.LogOut(siteCode);
        }


        [Description("The below method will need executed on a new environment to create the necessary users for all tests. Also, " +
               "an API account needs created (if one has not been created already) on this new environment. When creating the " +
               "account, you need to do 3 things " +
               "1. Copy all of the IP addresses and Endpoints from an existing automation API account, then paste them in the " +
               "cooresponding fields when creating this account " +
               "2. Execute the below script on this new API account. Note that for Prod, you will need to create a ticket and " +
               "assign it to a DEV for him to apply the script to Prod. " +
               "UPDATE security.api.Account SET ApiSecuritySettings_CheckTokenEnvironment = 1, ApiSecuritySettings_CheckTokenExpiration = 1, ApiSecuritySettings_CheckSiteToken = 1, ApiSecuritySettings_CheckSiteCodesValue = NULL, ApiSecuritySettings_CheckToken = 1, ApiSecuritySettings_IsSet = 1, SecurityProviderSettings_SecretKey = NULL, SecurityProviderSettings_x509Certificate = NULL, ApiSecuritySettings_CheckAllowOriginCorsHeader = 1, AccountPreferences_IsTokenPersistenceEnabled = 1, AccountType = 3 WHERE[Key] = 'TestAuto_DHMC' " +
               "If you are copying and pasting this method for a new site (not a new environment), then you will have to perform the " +
                "above steps, along with the below step also" +
               "1. Go to UserUtils->BuildUserModel and add code for DHMC. This will drive the code in the method below to add users")]
        [Test]
        public void CreateAllStaticUsers_DHMC()
        {
            LoginPage LP = new LoginPage(Browser);
            Constants.SiteCodes siteCode = Constants.SiteCodes.DHMC;

            Assert.Fail("Dont run this unless you need new users for a new environment. If so, comment this Assert and run");

            // *************************************** Profession 1 ******************************************************
            // Chrome
            UserUtils.CreateUser(siteCode, UserUtils.DHMC_Physician1Username, UserUtils.DHMC_Physician1Email, null, null,
                Constants.ProfessionCode.Physician_PHY);
            UserUtils.CreateUser(siteCode, UserUtils.DHMC_Physician2Username, UserUtils.DHMC_Physician2Email, null, null,
                Constants.ProfessionCode.Physician_PHY);
            UserUtils.CreateUser(siteCode, UserUtils.DHMC_Physician3Username, UserUtils.DHMC_Physician3Email, null, null,
                Constants.ProfessionCode.Physician_PHY);
            UserUtils.CreateUser(siteCode, UserUtils.DHMC_Physician4Username, UserUtils.DHMC_Physician4Email, null, null,
                Constants.ProfessionCode.Physician_PHY);
            // Mobile
            UserUtils.CreateUser(siteCode, UserUtils.DHMC_Physician1Username_Mobile, UserUtils.DHMC_Physician1Email, null, null,
                Constants.ProfessionCode.Physician_PHY);
            UserUtils.CreateUser(siteCode, UserUtils.DHMC_Physician2Username_Mobile, UserUtils.DHMC_Physician2Email, null, null,
                Constants.ProfessionCode.Physician_PHY);
            UserUtils.CreateUser(siteCode, UserUtils.DHMC_Physician3Username_Mobile, UserUtils.DHMC_Physician3Email, null, null,
                Constants.ProfessionCode.Physician_PHY);
            UserUtils.CreateUser(siteCode, UserUtils.DHMC_Physician4Username_Mobile, UserUtils.DHMC_Physician4Email, null, null,
                Constants.ProfessionCode.Physician_PHY);
            // IE
            UserUtils.CreateUser(siteCode, UserUtils.DHMC_Physician1Username_IE, UserUtils.DHMC_Physician1Email, null, null,
                Constants.ProfessionCode.Physician_PHY);
            UserUtils.CreateUser(siteCode, UserUtils.DHMC_Physician2Username_IE, UserUtils.DHMC_Physician2Email, null, null,
                Constants.ProfessionCode.Physician_PHY);
            UserUtils.CreateUser(siteCode, UserUtils.DHMC_Physician3Username_IE, UserUtils.DHMC_Physician3Email, null, null,
                Constants.ProfessionCode.Physician_PHY);
            UserUtils.CreateUser(siteCode, UserUtils.DHMC_Physician4Username_IE, UserUtils.DHMC_Physician4Email, null, null,
                Constants.ProfessionCode.Physician_PHY);
            // FF
            UserUtils.CreateUser(siteCode, UserUtils.DHMC_Physician1Username_FF, UserUtils.DHMC_Physician1Email, null, null,
                Constants.ProfessionCode.Physician_PHY);
            UserUtils.CreateUser(siteCode, UserUtils.DHMC_Physician2Username_FF, UserUtils.DHMC_Physician2Email, null, null,
                Constants.ProfessionCode.Physician_PHY);
            UserUtils.CreateUser(siteCode, UserUtils.DHMC_Physician3Username_FF, UserUtils.DHMC_Physician3Email, null, null,
                Constants.ProfessionCode.Physician_PHY);
            UserUtils.CreateUser(siteCode, UserUtils.DHMC_Physician4Username_FF, UserUtils.DHMC_Physician4Email, null, null,
                Constants.ProfessionCode.Physician_PHY);

            // Log in to each new user
            // Chrome
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.DHMC_Physician1Username, newUser: true, removePasswordAndSecurityQuestionPrompt: true);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.DHMC_Physician2Username, newUser: true, removePasswordAndSecurityQuestionPrompt: true);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.DHMC_Physician3Username, newUser: true, removePasswordAndSecurityQuestionPrompt: true);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.DHMC_Physician4Username, newUser: true, removePasswordAndSecurityQuestionPrompt: true);
            LP.LogOut(siteCode);

            // Mobile
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.DHMC_Physician1Username_Mobile, newUser: true, removePasswordAndSecurityQuestionPrompt: true);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.DHMC_Physician2Username_Mobile, newUser: true, removePasswordAndSecurityQuestionPrompt: true);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.DHMC_Physician3Username_Mobile, newUser: true, removePasswordAndSecurityQuestionPrompt: true);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.DHMC_Physician4Username_Mobile, newUser: true, removePasswordAndSecurityQuestionPrompt: true);
            LP.LogOut(siteCode);

            // IE
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.DHMC_Physician1Username_IE, newUser: true, removePasswordAndSecurityQuestionPrompt: true);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.DHMC_Physician2Username_IE, newUser: true, removePasswordAndSecurityQuestionPrompt: true);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.DHMC_Physician3Username_IE, newUser: true, removePasswordAndSecurityQuestionPrompt: true);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.DHMC_Physician4Username_IE, newUser: true, removePasswordAndSecurityQuestionPrompt: true);
            LP.LogOut(siteCode);

            // FF
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.DHMC_Physician1Username_FF, newUser: true, removePasswordAndSecurityQuestionPrompt: true);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.DHMC_Physician2Username_FF, newUser: true, removePasswordAndSecurityQuestionPrompt: true);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.DHMC_Physician3Username_FF, newUser: true, removePasswordAndSecurityQuestionPrompt: true);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.DHMC_Physician4Username_FF, newUser: true, removePasswordAndSecurityQuestionPrompt: true);
            LP.LogOut(siteCode);

            // *************************************** Profession 2 ******************************************************
            // Chrome
            UserUtils.CreateUser(siteCode, UserUtils.DHMC_Pharmacist1Username, UserUtils.DHMC_Pharmacist1Email, null, null,
                Constants.ProfessionCode.Pharmacist_RPH);
            UserUtils.CreateUser(siteCode, UserUtils.DHMC_Pharmacist2Username, UserUtils.DHMC_Pharmacist2Email, null, null,
                Constants.ProfessionCode.Pharmacist_RPH);
            // Mobile
            UserUtils.CreateUser(siteCode, UserUtils.DHMC_Pharmacist1Username_Mobile, UserUtils.DHMC_Pharmacist1Email, null, null,
                Constants.ProfessionCode.Pharmacist_RPH);
            UserUtils.CreateUser(siteCode, UserUtils.DHMC_Pharmacist2Username_Mobile, UserUtils.DHMC_Pharmacist2Email, null, null,
                Constants.ProfessionCode.Pharmacist_RPH);
            // IE
            UserUtils.CreateUser(siteCode, UserUtils.DHMC_Pharmacist1Username_IE, UserUtils.DHMC_Pharmacist1Email, null, null,
                Constants.ProfessionCode.Pharmacist_RPH);
            UserUtils.CreateUser(siteCode, UserUtils.DHMC_Pharmacist2Username_IE, UserUtils.DHMC_Pharmacist2Email, null, null,
                Constants.ProfessionCode.Pharmacist_RPH);
            // FF
            UserUtils.CreateUser(siteCode, UserUtils.DHMC_Pharmacist1Username_FF, UserUtils.DHMC_Pharmacist1Email, null, null,
                Constants.ProfessionCode.Pharmacist_RPH);
            UserUtils.CreateUser(siteCode, UserUtils.DHMC_Pharmacist2Username_FF, UserUtils.DHMC_Pharmacist2Email, null, null,
                Constants.ProfessionCode.Pharmacist_RPH);


            // Log in to each new user
            // Chrome
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.DHMC_Pharmacist1Username, newUser: true, removePasswordAndSecurityQuestionPrompt: true);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.DHMC_Pharmacist2Username, newUser: true, removePasswordAndSecurityQuestionPrompt: true);
            LP.LogOut(siteCode);

            // Mobile
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.DHMC_Pharmacist1Username_Mobile, newUser: true, removePasswordAndSecurityQuestionPrompt: true);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.DHMC_Pharmacist2Username_Mobile, newUser: true, removePasswordAndSecurityQuestionPrompt: true);
            LP.LogOut(siteCode);

            // IE
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.DHMC_Pharmacist1Username_IE, newUser: true, removePasswordAndSecurityQuestionPrompt: true);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.DHMC_Pharmacist2Username_IE, newUser: true, removePasswordAndSecurityQuestionPrompt: true);
            LP.LogOut(siteCode);

            // FF
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.DHMC_Pharmacist1Username_FF, newUser: true, removePasswordAndSecurityQuestionPrompt: true);
            LP.LogOut(siteCode);
            Navigation.GoToLoginPage(browser, siteCode);
            LP.Login(UserUtils.DHMC_Pharmacist2Username_FF, newUser: true, removePasswordAndSecurityQuestionPrompt: true);
            LP.LogOut(siteCode);


        }

        [Description("")]
        [Test]
        public void CreateRandomUsers()
        {
            var numberList = Enumerable.Range(10, 100).ToList();

            List<string> profession1Username1s = new List<string>();

            Constants.SiteCodes siteCode = Constants.SiteCodes.ONSLT;


            for (int i = 0; i < numberList.Count; i++)
            {
                profession1Username1s.Add(string.Format("Myprofession1Username1{0}", numberList[i]));
                //profession1Username1s.Add(string.Format("MyEmail{0}@mailinator.com", numberList[i]));
            }

            foreach (var profession1Username1 in profession1Username1s)
            {
                HomePage HP = Help.RegisterUser(Browser, siteCode, null, profession1Username1);
                HP.LogOut(siteCode);


                //HP.ClickAndWaitBasePage(HP.FullNameLbl);
                //HP.Menu_UserProfile_SignOutBtn.Click(Browser);
                //IAlert Alert = Browser.SwitchTo().Alert();
                //Alert.Accept();
                //Browser.WaitForElement(By.XPath("//h1[contains(text(), 'upgraded')]"));
            }
        }



        #endregion Tests
    }
}

