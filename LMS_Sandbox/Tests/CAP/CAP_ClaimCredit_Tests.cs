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

namespace CAP.UITest
{
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome, "", "", Platforms.Windows, "", "")]
    //[RemoteSeleniumTestFixture(BrowserNames.Chrome, EmulationDevices.iPhoneX, "", Platforms.Windows, "", "")]

    [TestFixture]
    public class CAP_ClaimCredit_Tests : TestBase_CAP
    {
        #region Constructors

        public CAP_ClaimCredit_Tests(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public CAP_ClaimCredit_Tests(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }

        #endregion Constructors

        #region Tests

        // Fixed bug https://code.premierinc.com/issues/browse/LMSPLT-5330. Uncomment and execute when fixed
        [TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("UAT"), Category("Prod")]
        [Description("Testing various Claim Credit functions such as element dependencies, cme360 configurations and " +
            "credit/certificate generation on various pages")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ClaimCredit(Constants.SiteCodes siteCode)
        {
            string actTitle = Constants.ActTitle.Automation_ClaimCredit_171_MaximumMinimumwithandwithoutIncrement_CAP.GetDescription();
            UserModel user = profession1User2;
            APIHelp.DeleteActivityForUser(user.Username, actTitle, siteCode);

            /// 1. Go to the Claim Credit page and verify the Continue button is disabled
            ActClaimCreditPage CCP = Help.GoTo_ActivityWorkflow_SpecificPage(Browser, siteCode, actTitle,
                Constants.Pages_ActivityPage.ClaimCredit, false, user.Username);
            Assert.True(browser.Exists(Bys.ActClaimCreditPage.ContinueBtn,
                ElementCriteria.AttributeValueContains("class", "disabled")), "The Continue button was not disabled");

            /// 2. Click button Claim for CAP and verify that the Claimed label appears
            CCP.ClaimCredit("CE", "CAP");
            Assert.True(Browser.Exists(
                   By.XPath("//*[contains(text(), 'CAP')]/ancestor::form/descendant::*[contains(text(), 'CLAIMED')]")),
                   "The 'CLAIMED' label did not appear");

            /// 3. Click Continue and verify that 1 certificate is generated and is for CAP
            ActCertificatePage CP = CCP.ClickAndWait(CCP.ContinueBtn);
            Assert.AreEqual(1, Browser.FindElements(By.XPath("//*[contains(@class, 'content-viewer')]")).Count,
                "The amount of certificates did not equal to 1");
            Assert.True(Browser.Exists(
                By.XPath("//div[contains(@class, 'certificate')]/ancestor::td//*[contains(text(), 'CAP')]")),
                "The certificate did not specify that it was for CAP");

            /// 4. Click Finish and verify that the Transcript page shows the certificate and the amount claimed for CAP 
            TranscriptPage TP = CP.ClickAndWait(CP.FinishBtn);
            var CAPAccreditingBodyTranscript = 
                TP.GetTranscript(Constants.SiteCodes.CAP).Where(t => t.CreditBody == "CE").ToList();
            // NOTE: This test will fail if you use a user that has completed a different activitiy. If this is
            // the case, delete the activity from the user that is being tested, or use a user that doesnt have 
            // any activities completed yet
            Assert.AreEqual("999.00 Units", CAPAccreditingBodyTranscript[0].CreditAmountAndUnit);
            Assert.True(CAPAccreditingBodyTranscript[0].CertificateGenerated, "The certificate icon did not appear");

            /// 5. Go back to the activity, claim credit for another body, verify that both certificates and credit amounts 
            /// are now showing on the appropriate pages
            Help.GoTo_ActivityWorkflow_SpecificPage(Browser, siteCode, actTitle, Constants.Pages_ActivityPage.ClaimCredit);
            CCP.ClaimCredit("", "NONACCR");
            CCP.ClickAndWait(CCP.ContinueBtn);
            Assert.AreEqual(2, Browser.FindElements(By.XPath("//*[contains(@class, 'content-viewer')]")).Count,
                "The amount of certificates did not equal to 1");
            Assert.True(Browser.Exists(
                By.XPath("//div[contains(@class, 'certificate')]/ancestor::td//*[contains(text(), 'CAP')]")),
                "The certificate did not specify that it was for CAP");
            Assert.True(Browser.Exists(
                By.XPath("//div[contains(@class, 'certificate')]/ancestor::td//*[contains(text(), 'NONACCR')]")),
                "The certificate did not specify that it was for Ethics");
            CP.ClickAndWait(CP.FinishBtn);
            CAPAccreditingBodyTranscript = 
                TP.GetTranscript(Constants.SiteCodes.CAP).Where(t => t.CreditBody == "CE").ToList();
            Assert.AreEqual("999.00 Units", CAPAccreditingBodyTranscript[0].CreditAmountAndUnit);
            Assert.True(CAPAccreditingBodyTranscript[0].CertificateGenerated, "The certificate icon did not appear");
            CAPAccreditingBodyTranscript =
                TP.GetTranscript(Constants.SiteCodes.CAP).Where(t => t.CreditBody == "N/A").ToList();
            // NOTE: This test will fail if you use a user that has completed a different activitiy. If this is
            // the case, delete the activity from the user that is being tested, or use a user that doesnt have 
            // any activities completed yet
            Assert.AreEqual("19.00 MPCEC", CAPAccreditingBodyTranscript[0].CreditAmountAndUnit);
            Assert.True(CAPAccreditingBodyTranscript[0].CertificateGenerated, "The certificate icon did not appear");
        }

        // Currently the activity is not in UAT or PROD, Srilu has not gotten a chance to add it. She is creating a story
        // and will let me know when the activity gets added. Until then, this is commented out
        //[TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("UAT"), Category("Prod")]
        //[Description("")]
        //[Property("Status", "Complete")]
        //[Author("Mike Johnston")]
        //public void ClaimCredit_ProfessionSpecific(Constants.SiteCodes siteCode)
        //{
        //    string actTitle = Constants.ActTitle.Automation_ClaimCredit_173_ProfessionSpecific.GetDescription();
        //    UserModel user = profession1User1;
        //    APIHelp.DeleteActivityForUser(user.Username, actTitle, siteCode);

        //    /// 1. Go to the Claim Credit page and verify that only NONACCR and Ethics show, since the test user is a physician, and 
        //    /// the activity is configured to only show these accreditations for this profession
        //    ActClaimCreditPage CCP = Help.GoToActivity_NonOverviewPage(Browser, siteCode, actTitle, Constants.ActPages.ClaimCredit,
        //        false, user.Username);

        //    Assert.True(Browser.Exists(By.XPath(
        //     string.Format("//span[contains(text(), 'NONACCR')]/ancestor::form/descendant::span[text()='CLAIM']"))));
        //    Assert.True(Browser.Exists(By.XPath(
        //        string.Format("//div[text()='Ethics']/ancestor::form//span[contains(text(), 'CAP')]/ancestor::form/descendant::span[text()='CLAIM']"))));
        //    Assert.False(Browser.Exists(By.XPath(
        //        string.Format("//div[text()='ASA']/ancestor::form//span[contains(text(), 'CAP')]/ancestor::form/descendant::span[text()='CLAIM']"))));
        //    Assert.False(Browser.Exists(By.XPath(
        //        string.Format("//div[text()='CAP']/ancestor::form//span[contains(text(), 'CAP')]/ancestor::form/descendant::span[text()='CLAIM']"))));
        //}
    }
    #endregion tests
}







