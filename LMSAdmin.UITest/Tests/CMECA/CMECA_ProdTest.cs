using Browser.Core.Framework;
using LMS.Data;
using NUnit.Framework;
using OpenQA.Selenium;
using LMSAdmin.AppFramework;
using System;
using System.Threading;

namespace LMSAdmin_CMECA_Prod.UITest
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
    public class CMECA_ProdTest : TestBase_CMECA
    {
        #region Constructors
        public CMECA_ProdTest(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }

        // Remote Selenium Grid Test
        public CMECA_ProdTest(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }
        #endregion

        #region Tests

        [Test]
        [Description("The test will login Old CME360 and searches for the given test activity and upon selecting the Accreditation node ," +
            " the page will be navigating to New CME360, Verify the accredtation and completion pathway page is loaded, Select back to activity " +
            "and logout from OLDCME")]
        [Property("Status", "Completed")]
        [Author("Bama Thangaraj")]
        public void AccreditationClickthrough()
        {
            /// 1. Launch as old cme 360 (ex: lmsadmin.cme.premierinc.com/ )
            LoginPage LP = Navigation.GoToLoginPage(Browser);
            /// 2. Username as Prod user to Login
            MyDashboardPage MDP = LP.Login(Prod_User, Prod_password);             
            
            string activityName = Prod_TestActivity; 

            /// 3. Search for the given Autotest_Activity1 activity 
            SearchResultsPage SP = MDP.Search(activityName);
            ActMainPage AMP = SP.GoToActivity(activityName);
            

            /// 4. Select Accreditation node 
            /// 5. The page will be redirected to New LMSadmin UI and check whether the accreditation body already there if yes, then delete it
            ActAccreditationPage ACCPage = AMP.ClickAndWaitBasePage(AMP.TreeLinks_Accreditation);
            ActCompletionPathwayPage ACPL = ACCPage.ClickAndWaitBasePage(ACCPage.Steps_CompletionPathwayLbl);

            /// 6. Select "back to activity" and land on old cme360 and logout
            ACPL.ClickAndWaitBasePage(ACPL.BackToActivityBtn);            
            AMP.ClickAndWaitBasePage(AMP.LogoutLnk);
        }

 
   
       

    }

    #endregion Tests

}


