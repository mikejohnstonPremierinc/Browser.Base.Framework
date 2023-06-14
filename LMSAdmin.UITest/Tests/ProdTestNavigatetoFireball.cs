using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Browser.Core.Framework;
using LMS.Data;
using LMSAdmin.AppFramework;
using LMSAdmin.AppFramework.ConstantsLMSAdmin;
using NUnit.Framework;
using OpenQA.Selenium;

namespace LMSAdmin_ProdTest.UITest
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
    public class ProdTestNavigatetoFireball : TestBase
    {
        #region Constructor
       
        public ProdTestNavigatetoFireball(string browserName, string emulationDevice) : base(browserName, emulationDevice)
        {
        }

        public ProdTestNavigatetoFireball(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri) : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        {
        }
        #endregion

        #region Tests
        [Test]
        [Description (" Test to verify that for StandAlone and LiveMeeting Activity types, on clicking the Accreditation node ," +
            " the page should be navigated to New Fireball LMSADMIN and loads successfully," +
            " When Click on backtoactivity button, go back to oldcme and logout")]
        [Property("Status", "Completed")]
        [Author("Bama Thangaraj")]
        public void AHA_TestNavigationToFireballPage()
        {
            /// 1. Launch as old cme 360 (ex: lmsadmin.cme.premierinc.com/ )
            LoginPage LP = Navigation.GoToLoginPage(Browser);
            /// 2. Username as Prod user to Login
            MyDashboardPage MDP = LP.Login(Constants_LMSAdmin.LoginUserNames.AHA, prod_password);

            string standAloneActivityName = Constants_LMSAdmin.ActivityTitle.AHA_standAloneActivity.GetDescription();

            /// 3. Search for the given test standAloneActiviity 
            SearchResultsPage SP = MDP.Search(standAloneActivityName);
            ActMainPage AMP = SP.GoToActivity(standAloneActivityName);

            /// 4. Click on Accreditation node , Verify The page is redirected to New Fireball LMSadmin UI 
            ActAccreditationPage ACCP = AMP.ClickAndWaitBasePage(AMP.TreeLinks_Accreditation);

            /// 5. Click BacktoActivity button to redirect to main page
            ACCP.ClickAndWaitBasePage(ACCP.BackToActivityBtn);

            string LiveactivityName = Constants_LMSAdmin.ActivityTitle.AHA_LiveMeetingActivity.GetDescription();
            string SessionName = Constants_LMSAdmin.SessionTitle.AHA_SessionActivity.GetDescription();

            /// 6. Now, Search for the given test LiveActiviity
            MDP.Search(LiveactivityName);
            SP.GoToActivity(LiveactivityName);
            By SessionActivityBy = By.XPath("//span[contains(@class, 'TreeNode') and text()='Accreditations']/ancestor::div[2]/div/span[@class= 'TreeNode' and text()='"+ SessionName+"']");
            By SessionActivityAccreditationBy = By.XPath("//span[text()='"+SessionName+"']/parent::div/following-sibling::div/div/span[text()='Accreditations']");

            Browser.FindElement(SessionActivityBy).ClickJS(Browser);
            Thread.Sleep(TimeSpan.FromSeconds(10));
            AMP.WaitForInitialize();

            /// 7. Click on Session's accreditation node,  Verify The page is redirected to New Fireball LMSadmin UI 
            Browser.FindElement(SessionActivityAccreditationBy).ClickJS(Browser);
            ActAccreditationPage ACCPage = new ActAccreditationPage(Browser);
            ACCPage.WaitForInitialize();
            
            /// 8. Click BacktoActivity button to redirect to main page and logoff
            ACCP.ClickAndWaitBasePage(ACCP.BackToActivityBtn);
            AMP.ClickAndWaitBasePage(AMP.LogoutLnk);

        }
       
        [Test]
        [Description(" Test to verify that for StandAlone and LiveMeeting Activity types, on clicking the Accreditation node ," +
            " the page should be navigated to New Fireball LMSADMIN and loads successfully," +
            " When Click on backtoactivity button, go back to oldcme and logout")]
        [Property("Status", "Completed")]
        [Author("Bama Thangaraj")]
        public void UAMS_TestNavigationToFireballPage()
        {
            /// 1. Launch as old cme 360 (ex: lmsadmin.cme.premierinc.com/ )
            LoginPage LP = Navigation.GoToLoginPage(Browser);
            /// 2. Username as Prod user to Login
            MyDashboardPage MDP = LP.Login(Constants_LMSAdmin.LoginUserNames.UAMS, prod_password);

            string standAloneActivityName = Constants_LMSAdmin.ActivityTitle.UAMS_standAloneActivity.GetDescription();

            /// 3. Search for the given test standAloneActiviity 
            SearchResultsPage SP = MDP.Search(standAloneActivityName);
            ActMainPage AMP = SP.GoToActivity(standAloneActivityName);

            /// 4. Click on Accreditation node , Verify The page is redirected to New Fireball LMSadmin UI 
            ActAccreditationPage ACCP = AMP.ClickAndWaitBasePage(AMP.TreeLinks_Accreditation);

            /// 5. Click BacktoActivity button to redirect to main page
            ACCP.ClickAndWaitBasePage(ACCP.BackToActivityBtn);

            string LiveactivityName = Constants_LMSAdmin.ActivityTitle.UAMS_LiveMeetingActivity.GetDescription();
            string SessionName = Constants_LMSAdmin.SessionTitle.UAMS_SessionActivity.GetDescription();

            /// 6. Now, Search for the given test LiveActiviity
            MDP.Search(LiveactivityName);
            SP.GoToActivity(LiveactivityName);
            By SessionActivityBy = By.XPath("//span[contains(@class, 'TreeNode') and text()='Accreditations']/ancestor::div[2]/div/span[@class= 'TreeNode' and text()='" + SessionName + "']");
            By SessionActivityAccreditationBy = By.XPath("//span[text()='" + SessionName + "']/parent::div/following-sibling::div/div/span[text()='Accreditations']");

            Browser.FindElement(SessionActivityBy).ClickJS(Browser);
            Thread.Sleep(TimeSpan.FromSeconds(10));
            AMP.WaitForInitialize();

            /// 7. Click on Session's accreditation node,  Verify The page is redirected to New Fireball LMSadmin UI 
            Browser.FindElement(SessionActivityAccreditationBy).ClickJS(Browser);
            ActAccreditationPage ACCPage = new ActAccreditationPage(Browser);
            ACCPage.WaitForInitialize();

            /// 8. Click BacktoActivity button to redirect to main page and logoff
            ACCP.ClickAndWaitBasePage(ACCP.BackToActivityBtn);
            AMP.ClickAndWaitBasePage(AMP.LogoutLnk);

        }

        [Test]
        [Description(" Test to verify that for StandAlone and LiveMeeting Activity types, on clicking the Accreditation node ," +
            " the page should be navigated to New Fireball LMSADMIN and loads successfully," +
            " When Click on backtoactivity button, go back to oldcme and logout")]
        [Property("Status", "Completed")]
        [Author("Bama Thangaraj")]
        public void CAP_TestNavigationToFireballPage()
        {
            /// 1. Launch as old cme 360 (ex: lmsadmin.cme.premierinc.com/ )
            LoginPage LP = Navigation.GoToLoginPage(Browser);
            /// 2. Username as Prod user to Login
            MyDashboardPage MDP = LP.Login(Constants_LMSAdmin.LoginUserNames.CAP, prod_password);

            string standAloneActivityName = Constants_LMSAdmin.ActivityTitle.CAP_standAloneActivity.GetDescription();

            /// 3. Search for the given test standAloneActiviity 
            SearchResultsPage SP = MDP.Search(standAloneActivityName);
            ActMainPage AMP = SP.GoToActivity(standAloneActivityName);

            /// 4. Click on Accreditation node , Verify The page is redirected to New Fireball LMSadmin UI 
            ActAccreditationPage ACCP = AMP.ClickAndWaitBasePage(AMP.TreeLinks_Accreditation);

            /// 5. Click BacktoActivity button to redirect to main page
            ACCP.ClickAndWaitBasePage(ACCP.BackToActivityBtn);

            string LiveactivityName = Constants_LMSAdmin.ActivityTitle.CAP_LiveMeetingActivity.GetDescription();
            string SessionName = Constants_LMSAdmin.SessionTitle.CAP_SessionActivity.GetDescription();

            /// 6. Now, Search for the given test LiveActiviity
            MDP.Search(LiveactivityName);
            SP.GoToActivity(LiveactivityName);
            By SessionActivityBy = By.XPath("//span[contains(@class, 'TreeNode') and text()='Accreditations']/ancestor::div[2]/div/span[@class= 'TreeNode' and text()='" + SessionName + "']");
            By SessionActivityAccreditationBy = By.XPath("//span[text()='" + SessionName + "']/parent::div/following-sibling::div/div/span[text()='Accreditations']");

            Browser.FindElement(SessionActivityBy).ClickJS(Browser);
            Thread.Sleep(TimeSpan.FromSeconds(10));
            AMP.WaitForInitialize();

            /// 7. Click on Session's accreditation node,  Verify The page is redirected to New Fireball LMSadmin UI 
            Browser.FindElement(SessionActivityAccreditationBy).ClickJS(Browser);
            ActAccreditationPage ACCPage = new ActAccreditationPage(Browser);
            ACCPage.WaitForInitialize();

            /// 8. Click BacktoActivity button to redirect to main page and logoff
            ACCP.ClickAndWaitBasePage(ACCP.BackToActivityBtn);
            AMP.ClickAndWaitBasePage(AMP.LogoutLnk);

        }



        #endregion
    }
}
