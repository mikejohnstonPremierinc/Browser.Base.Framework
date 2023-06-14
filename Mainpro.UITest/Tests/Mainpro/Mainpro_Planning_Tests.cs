using Browser.Core.Framework;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using Mainpro.AppFramework;
using System.Data;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using System.Collections.ObjectModel;
using System.Globalization;

namespace Mainpro.UITest
{
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]
    [TestFixture]
    public class Mainpro_Planning_Tests : TestBase
    {
        #region Constructors
        public Mainpro_Planning_Tests(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public Mainpro_Planning_Tests(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }
        #endregion

        #region Tests


        //[Test] - Add Goal functionality has been Removed From Applcication 
        [Description("Verifies a user can add a goal and it appears in the Goals table")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void AddGoal()
        {
            /// 1. Create a goal and verify it shows in the Goals table
            Help.AddGoal(browser, TestContext.CurrentContext.Test);
        }

        [Test]
        [Description("Given I am on Dashboard Page, Then Verify that " +
            "CPD Planning Tab Should not be Visible and Add Goal button " +
            "Should not be visible on Dashboard page")]
        [Property("Status", "Complete")]
        [Author("Bama Thangaraj")]
        public void NoCPDPlanningTabAndNoAddGoal()
        {
            UserModel user = UserUtils.CreateAndRegisterUser(currentTest: TestContext.CurrentContext.Test);
            LoginPage LP = Navigation.GoToLoginPage(browser);
            DashboardPage DP = LP.Login(user.Username, isNewUser: true);
            Assert.False(Browser.Exists(Bys.MainproPage.CPDPlanningTab), "CPD Planning Tab should not be displayed");
            Assert.False(Browser.Exists(Bys.CPDPlanningPage.CreateAPersonalLearningGoalBtn),
                "CPD Planning Goal button should not be displayed in Dashboard Page");

        }



        #endregion Tests
    }


}