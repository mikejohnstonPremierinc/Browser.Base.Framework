using Browser.Core.Framework;
using LMS.Data;
using NUnit.Framework;
using OpenQA.Selenium;
using LMSAdmin.AppFramework;
using System;
using System.Threading;
using LMSAdmin.AppFramework.ConstantsLMSAdmin;

namespace LMSAdmin.UITest
{
    //[NonParallelizable]
    [BrowserMode(BrowserMode.New)]
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    // [LocalSeleniumTestFixture(BrowserNames.Firefox)]
    //[LocalSeleniumTestFixture(BrowserNames.InternetExplorer)]

    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Firefox)]
    //[RemoteSeleniumTestFixture(BrowserNames.InternetExplorer)]

    [TestFixture]
    public class UserCreation : TestBase
    {
        #region Constructors
        public UserCreation(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }

        // Remote Selenium Grid Test
        public UserCreation(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }
        #endregion


        #region Tests

       // [TestCase(Constants_LMSAdmin.LoginUserNames.UCD)]
        [Description("")]
        [Property("Status", "Completed")]
        [Author("Bama Thangaraj")]
        public void CreateUsers()
        {
            /// 1. Launch as old cme 360 (ex: lmsadmin.cmeqaf.premierinc.com )
            LoginPage LP = Navigation.GoToLoginPage(Browser);
            /// 2. Username as Autotest_user1 to login
            MyDashboardPage MDP = LP.Login(Constants_LMSAdmin.LoginUserNames.UCD, "password");

            
           
        }

     



    }

    #endregion Tests

}


