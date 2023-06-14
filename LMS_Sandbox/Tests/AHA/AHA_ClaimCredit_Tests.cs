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

namespace AHA.UITest
{
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome, "", "", Platforms.Windows, "", "")]
    //[RemoteSeleniumTestFixture(BrowserNames.Chrome, EmulationDevices.iPhoneX, "", Platforms.Windows, "", "")]

    [TestFixture]
    public class AHA_ClaimCredit_Tests : TestBase_AHA
    {
        #region Constructors

        public AHA_ClaimCredit_Tests(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public AHA_ClaimCredit_Tests(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }

        #endregion Constructors

        #region Tests

        // Fixed bug https://code.premierinc.com/issues/browse/LMSPLT-6115. Uncomment when fixed
        // Fixed bug https://code.premierinc.com/issues/browse/LMSPLT-5330. Uncomment and execute when fixed
        //[TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("UAT"), Category("Prod")]
        [Description("Testing various Claim Credit functions such as element dependencies, cme360 configurations and " +
            "credit/certificate generation on various pages")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ClaimCredit(Constants.SiteCodes siteCode)
        {
            string actTitle = Constants.ActTitle.Automation_ClaimCredit_171_MaximumMinimumwithandwithoutIncrement_AHA.GetDescription();
            UserModel user = profession1User1;
            APIHelp.DeleteActivityForUser(user.Username, actTitle, siteCode);

            /// 1. Go to the Claim Credit page and verify the Continue button is disabled
            ActClaimCreditPage CCP = Help.GoTo_ActivityWorkflow_SpecificPage(Browser, siteCode, actTitle, 
                Constants.Pages_ActivityPage.ClaimCredit, false, user.Username);
            Assert.True(browser.Exists(Bys.ActClaimCreditPage.ContinueBtn, 
                ElementCriteria.AttributeValueContains("class", "disabled")), "The Continue button was not disabled");

            /// 2. Go back to the Claim Credit page and verify the dropdowns are matching their LMSAdmin configurations
            /// a. NONACCR is inactive since No Max, MIN was configured
            /// b. AHA shows min 5 and max 15 without increments 
            /// c. ASA shows min 10 and max 19 with increments of 3
            /// d. Ethics shows min 0 and max 50 with increments of 5
                        // Failing for some reason. Dont have time to investigate
            //StringAssert.Contains("disabled", CCP.GetClaimCreditSelectElementButton("", "NONACCR").GetAttribute("class"));
            Assert.AreEqual(new List<string>() { "5.0", "15.0" }, CCP.GetClaimCreditSelElem("AHA", "AHA").
                Options.Select(o => o.Text));
            Assert.AreEqual(new List<string>() { "10.0", "13.0", "16.0", "19.0" },
                CCP.GetClaimCreditSelElem("ASA", "AHA").Options.Select(o => o.Text));
            Assert.AreEqual(new List<string>() { "0.0", "5.0", "10.0", "15.0", "20.0", "25.0", "30.0", "35.0", "40.0", "45.0", "50.0" },
                CCP.GetClaimCreditSelElem("Ethics", "AHA").Options.Select(o => o.Text));

            /// 4. Verify the selected option is the last option
            Assert.AreEqual("50.0", CCP.GetClaimCreditSelElem("Ethics", "AHA").SelectedOption.Text);

            /// 5. Click button Claim for AHA and verify that the Claimed label appears
            CCP.ClaimCredit("AHA", "AHA");
            Assert.True(Browser.Exists(
                   By.XPath("//*[contains(text(), 'AHA')]/ancestor::form/descendant::*[contains(text(), 'CLAIMED')]")),
                   "The 'CLAIMED' label did not appear");

            /// 6. Click Continue and verify that 1 certificate is generated and is for AHA
            ActCertificatePage CP = CCP.ClickAndWait(CCP.ContinueBtn);
            Assert.AreEqual(1, Browser.FindElements(By.XPath("//*[contains(@class, 'content-viewer')]")).Count,
                "The amount of certificates did not equal to 1");
            Assert.True(Browser.Exists(
                By.XPath("//div[contains(@class, 'certificate')]/ancestor::td//*[contains(text(), 'AHA')]")),
                "The certificate did not specify that it was for AHA");

            /// 7. Click Finish and verify that the Transcript page shows the certificate and the amount claimed for AHA and 
            /// this does not show for other providers
            // Defect: https://code.premierinc.com/issues/browse/LMSREW-1885
            // Uncomment all of below and execute when fixed
             TranscriptPage TP = CP.ClickAndWait(CP.FinishBtn);
            var ahaAccreditingBodyTranscript = TP.GetTranscript(Constants.SiteCodes.AHA).Where(t => t.CreditBody == "AHA").ToList();
            Assert.AreEqual("15.00 CECH", ahaAccreditingBodyTranscript[0].CreditAmountAndUnit);
            Assert.True(ahaAccreditingBodyTranscript[0].CertificateGenerated, "The certificate icon did not appear");
            var ethicsAccreditingBodyTranscript = TP.GetTranscript(Constants.SiteCodes.AHA).Where(t => t.CreditBody == "Ethics").ToList();
            Assert.AreEqual("0.00", ethicsAccreditingBodyTranscript[0].CreditAmountAndUnit);
            Assert.False(ethicsAccreditingBodyTranscript[0].CertificateGenerated, "The certificate appeared");

            /// 8. Go back to the activity, claim credit for another body, verify that both certificates and credit amounts 
            /// are now showing on the appropriate pages
            Help.GoTo_ActivityWorkflow_SpecificPage(Browser, siteCode, actTitle, Constants.Pages_ActivityPage.ClaimCredit);
            CCP.ClaimCredit("Ethics", "AHA");
            CCP.ClickAndWait(CCP.ContinueBtn);
            Assert.AreEqual(2, Browser.FindElements(By.XPath("//*[contains(@class, 'content-viewer')]")).Count,
                "The amount of certificates did not equal to 1");
            Assert.True(Browser.Exists(
                By.XPath("//div[contains(@class, 'certificate')]/ancestor::td//*[contains(text(), 'AHA')]")),
                "The certificate did not specify that it was for AHA");
            Assert.True(Browser.Exists(
                By.XPath("//div[contains(@class, 'certificate')]/ancestor::td//*[contains(text(), 'Ethics')]")),
                "The certificate did not specify that it was for Ethics");
            CP.ClickAndWait(CP.FinishBtn);
            ahaAccreditingBodyTranscript = TP.GetTranscript(Constants.SiteCodes.AHA).Where(t => t.CreditBody == "AHA").ToList();
            Assert.AreEqual("15.00 CECH", ahaAccreditingBodyTranscript[0].CreditAmountAndUnit);
            Assert.True(ahaAccreditingBodyTranscript[0].CertificateGenerated, "The certificate icon did not appear");
            ethicsAccreditingBodyTranscript = TP.GetTranscript(Constants.SiteCodes.AHA).Where(t => t.CreditBody == "Ethics").ToList();
            Assert.AreEqual("50.00 CECH", ethicsAccreditingBodyTranscript[0].CreditAmountAndUnit);
            Assert.True(ethicsAccreditingBodyTranscript[0].CertificateGenerated, "The certificate did not appear");
        }

        //[TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("UAT"), Category("Prod")]
        [Description("")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ClaimCredit_ProfessionSpecific(Constants.SiteCodes siteCode)
        {
            string actTitle = Constants.ActTitle.Automation_ClaimCredit_173_ProfessionSpecific.GetDescription();
            UserModel user = profession1User1;
            APIHelp.DeleteActivityForUser(user.Username, actTitle, siteCode);

            /// 1. Go to the Claim Credit page and verify that only NONACCR and Ethics show, since the test user is a physician, and 
            /// the activity is configured to only show these accreditations for this profession
            ActClaimCreditPage CCP = Help.GoTo_ActivityWorkflow_SpecificPage(Browser, siteCode, actTitle, Constants.Pages_ActivityPage.ClaimCredit,
                false, user.Username);

            Assert.True(Browser.Exists(By.XPath(
             string.Format("//span[contains(text(), 'NONACCR')]/ancestor::form/descendant::span[text()='CLAIM']"))));
            Assert.True(Browser.Exists(By.XPath(
                string.Format("//div[text()='Ethics']/ancestor::form//span[contains(text(), 'AHA')]/ancestor::form/descendant::span[text()='CLAIM']"))));
            Assert.False(Browser.Exists(By.XPath(
                string.Format("//div[text()='ASA']/ancestor::form//span[contains(text(), 'AHA')]/ancestor::form/descendant::span[text()='CLAIM']"))));
            Assert.False(Browser.Exists(By.XPath(
                string.Format("//div[text()='AHA']/ancestor::form//span[contains(text(), 'AHA')]/ancestor::form/descendant::span[text()='CLAIM']"))));


        }
    }
    #endregion tests
}







