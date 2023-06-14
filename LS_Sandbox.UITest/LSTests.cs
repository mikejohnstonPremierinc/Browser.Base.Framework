using Browser.Core.Framework;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using LS.AppFramework;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using OpenQA.Selenium.Firefox;
using Browser.Core.Framework.Resources;
using OpenQA.Selenium.Remote;
using System.Reflection;
using System.IO;
using LS.AppFramework.HelperMethods;
using LS.AppFramework.Constants_LTS;

namespace LS.UITest
{
    //[BrowserMode(BrowserMode.New)]
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    //[LocalSeleniumTestFixture(BrowserNames.Firefox)]
    //[LocalSeleniumTestFixture(BrowserNames.InternetExplorer)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Firefox)]
    [RemoteSeleniumTestFixture(BrowserNames.InternetExplorer)]
    [TestFixture]
    public class LSTests : TestBase
    {
        #region Constructors
        public LSTests(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }

        // Remote Selenium Grid Test
        public LSTests(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }
        #endregion

        #region Tests

        [Test]
        [Description("")]
        [Property("Status", "")]
        [Author("Mike Johnston")]
        public void TestLoginPage()
        {
            for (int i = 0; i < 600; i++)
            {


            /// 1. Navigate to the login page
            LoginPage LP = new LoginPage(Browser);
            LSHelperMethods Help = new LSHelperMethods();

            Help.Login(browser, "jezykowsky", "jezykowsky");

            SearchPage SP = new SearchPage(browser);

            SP.ClickAndWaitBasePage(SP.SitesTab);

          //  SP.Search(Bys.SearchPage.SitesTblBody, "ama");
          //  SP.SearchAndSelect(Bys.SearchPage.SitesTblBody, Constants_LTS.SearchResults.Sites, "AMA Online Learning Center");

                Help.GoToParticipantPage(Browser, "AMA Online Learning Center", "A Schwartz");



            SP.LogoutLnk.Click();
            browser.WaitForElement(By.XPath("//a[text()='Log In']"), ElementCriteria.IsVisible);

            Navigation.GoToLoginPage(Browser);

            LP.WaitForInitialize();
            }

        }


        #endregion Tests
    }

    //[BrowserMode(BrowserMode.New)]
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    //[LocalSeleniumTestFixture(BrowserNames.Firefox)]
    //[LocalSeleniumTestFixture(BrowserNames.InternetExplorer)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Firefox)]
    [RemoteSeleniumTestFixture(BrowserNames.InternetExplorer)]
    [TestFixture]
    public class LSTests2 : TestBase
    {
        #region Constructors
        public LSTests2(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }

        // Remote Selenium Grid Test
        public LSTests2(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }
        #endregion

        #region Tests

        [Test]
        [Description("")]
        [Property("Status", "")]
        [Author("Mike Johnston")]
        public void TestLoginPage2()
        {
            for (int i = 0; i < 600; i++)
            {


                /// 1. Navigate to the login page
                LoginPage LP = new LoginPage(Browser);
                LSHelperMethods Help = new LSHelperMethods();

                Help.Login(browser, "jezykowsky", "jezykowsky");

                SearchPage SP = new SearchPage(browser);

                SP.ClickAndWaitBasePage(SP.SitesTab);

                //  SP.Search(Bys.SearchPage.SitesTblBody, "ama");
                //  SP.SearchAndSelect(Bys.SearchPage.SitesTblBody, Constants_LTS.SearchResults.Sites, "AMA Online Learning Center");

                Help.GoToParticipantPage(Browser, "AMA Online Learning Center", "A Schwartz");



                SP.LogoutLnk.Click();
                browser.WaitForElement(By.XPath("//a[text()='Log In']"), ElementCriteria.IsVisible);

                Navigation.GoToLoginPage(Browser);

                LP.WaitForInitialize();
            }

        }


        #endregion Tests
    }
}

