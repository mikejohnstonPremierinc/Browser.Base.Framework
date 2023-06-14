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
using System.Data;
using System.Configuration;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;

namespace CMECA.UITest
{
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome, "", "", Platforms.Windows, "", "")]
    //[RemoteSeleniumTestFixture(BrowserNames.Chrome, EmulationDevices.iPhoneX, "", Platforms.Windows, "", "")]

    [TestFixture]
    public class CMECA_ClaimCredit_Tests : TestBase_CMECA
    {
        #region Constructors

        public CMECA_ClaimCredit_Tests(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public CMECA_ClaimCredit_Tests(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }

        #endregion Constructors

        #region Tests

        // Fixed bug https://code.premierinc.com/issues/browse/LMSPLT-5330. Uncomment and execute when fixed
        [TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("UAT"), Category("Prod")]
        [Description("Given I configure an activity with credit amounts on a portal that has the Claim Credit feature at the " +
            "activities Assessment page level, When I view the activity workflow, the credit selections should appear on " +
            "the Assessment page, and When I claim various amounts, Then the Certificate and Transcript page should " +
            "appropriately display these amounts")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ClaimCredit(Constants.SiteCodes siteCode)
        {
            string actTitle = Constants.ActTitle.Automation_CreditClaim_170_SetUpAtActivityLevel.GetDescription();
            UserModel user = profession1User1;
            APIHelp.DeleteActivityForUser(user.Username, actTitle, siteCode);
            // Set the credit amounts we will be choosing then verifying throughout the test
            string scenarioWithVariableCredit1Amount = "999.99";
            string scenarioWithVariableCredit2Amount = "5";
            string scenarioWithVariableCredit3Amount = "0.75";
            string scenarioWithoutVariableCreditCreditAmount = "10.00";

            /// 1. Go to the Overview page and verify the credit amounts. The credit amounts should be what is on the Fixed 
            /// Credits section from CME360. It should show only the amounts from the Accreditation scenario with the most credits
            // For now, we will use the query and page method to dynamically verify these values. We dont have to, and instead can 
            // hard code instead (as we do when on the Assessment page in the second step of this test)
            Constants.Activity ActivityDB = DBUtils.GetActivity_GeneralInfo(siteCode, actTitle, Constants.ActType.StandaloneActivity);
            var accreditationWithHighestCreditDB = ActivityDB.Accreditations.OrderByDescending(i => i.CreditAmount).Take(1).ToList()[0];

            ActOverviewPage AOP = Help.GoTo_ActivityWorkflow_OverviewPage(Browser, siteCode, actTitle, false, user.Username);
            Constants.Activity ActivityUI = AOP.GetActivityDetails();
            var accreditationWithHighestCreditUI = ActivityUI.Accreditations.OrderByDescending(i => i.CreditAmount).Take(1).ToList()[0];

            Assert.AreEqual(accreditationWithHighestCreditDB.BodyName, accreditationWithHighestCreditUI.BodyName);
            Assert.AreEqual(accreditationWithHighestCreditDB.CreditAmount, accreditationWithHighestCreditUI.CreditAmount);
            Assert.AreEqual(accreditationWithHighestCreditDB.CreditUnit, accreditationWithHighestCreditUI.CreditUnit);

            /// 2. Go to the Assessment page and verify that the Claim Credit drop downs are showing. Specifically, there should 
            /// only be drop downs for Scenarios with Fixed Credits configured. In this activity, there are 3 Scenarios with 
            /// fixed credits, and 1 Scenario without. The drop down should contain only the credit amounts from the Variable
            /// Credits section within CME360
            ActAssessmentPage AAP = AOP.ClickAndWait(AOP.ContinueBtn);
            Assert.True(Browser.Exists(By.XPath(string.Format("//select/option[text()='{0}']", scenarioWithVariableCredit1Amount))));
            Assert.True(Browser.Exists(By.XPath(string.Format("//select/option[text()='{0}']", scenarioWithVariableCredit2Amount))));
            Assert.True(Browser.Exists(By.XPath(string.Format("//select/option[text()='{0}']", scenarioWithVariableCredit3Amount))));
            
            
            /// 3. Complete the assessment, choosing a credit amount for each drop down, then go to the Certificate page and verify that
            /// the credit amounts chosen are appearing. There should also be 1 more credit being given that was not on the assessment
            /// page (This is the Scenario that did not have a Variable Credits section configured in CME360. Verify that it shows
            /// the Fixed Credit amount for this Scenario)
            Browser.FindElement(By.XPath("//input[@type='radio' and @value='1']")).Click();
            Browser.FindElement(By.XPath("(//input[@type='radio' and @value='1'])[2]")).Click();
            ElemSet.DropdownSingle_Fireball_SelectByText(Browser, Browser.FindElement(
                By.XPath("//div[contains(text(), 'Claim Your Credit for 999')]/..//button")), scenarioWithVariableCredit1Amount);
            ElemSet.DropdownSingle_Fireball_SelectByText(Browser, Browser.FindElement(
                By.XPath("//div[contains(text(), 'Claim Your Credit 55')]/..//button")), scenarioWithVariableCredit2Amount);
            ElemSet.DropdownSingle_Fireball_SelectByText(Browser, Browser.FindElement(
                By.XPath("//div[contains(text(), 'Claim Your Credit 0.75')]/..//button")), scenarioWithVariableCredit3Amount);
            AAP.ClickAndWait(AAP.SubmitBtn);
            ActCertificatePage CP = AAP.ClickAndWait(AAP.ContinueBtn);
            Assert.True(Browser.Exists(By.XPath(string.Format("//*[text()='{0}']", scenarioWithVariableCredit1Amount))));
            Assert.True(Browser.Exists(By.XPath(string.Format("//*[text()='{0}']", "5.00"))));
            Assert.True(Browser.Exists(By.XPath(string.Format("//*[text()='{0}']", scenarioWithVariableCredit3Amount))));
            Assert.True(Browser.Exists(By.XPath(string.Format("//*[text()='{0}']", scenarioWithoutVariableCreditCreditAmount))));

            /// 4. Click Finish and verify the same outcomes as we did in the step above
            TranscriptPage TP = CP.ClickAndWait(CP.FinishBtn);
            Assert.True(Browser.Exists(By.XPath(string.Format("//*[text()='{0} CECH']", scenarioWithVariableCredit1Amount))));
            Assert.True(Browser.Exists(By.XPath(string.Format("//*[text()='{0} CECH']", "5.00"))));
            Assert.True(Browser.Exists(By.XPath(string.Format("//*[text()='{0} CECH']", scenarioWithVariableCredit3Amount))));
            Assert.True(Browser.Exists(By.XPath(string.Format("//*[text()='{0} CECH']", scenarioWithoutVariableCreditCreditAmount))));
        }
    }
    #endregion tests
}







