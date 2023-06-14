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
using System.IO;
using System.Text;
using System.Web;
using System.Data.OleDb;
using OfficeOpenXml;
using System.Security.Cryptography.X509Certificates;
using System.Drawing;
using OfficeOpenXml.Style;
using OfficeOpenXml.Drawing.Chart;



namespace CMECA.UITest
{
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome, "", "", Platforms.Windows, "", "")]

    [TestFixture]
    public class CMECA_CustomPages : TestBase_CMECA
    {
        #region Constructors

        public CMECA_CustomPages(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public CMECA_CustomPages(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }

        #endregion Constructors

        public string baseURL = AppSettings.Config["urlwithoutsitecode"].Insert(8, "cmeca");

        #region Tests

        [Test, Category("Prod"), Category(siteCodeCategory), Category("CustomPage")]
        [Description("Validating the groupings and order of the custom page")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ComponentOrder_HomePage()
        {
            /// 1. Navigate to the custom page and verify the order of the HTML components. 
            Browser.Navigate().GoToUrl(baseURL + "lms/home");
            GenericCustomPage page = new GenericCustomPage(Browser);
            page.WaitForInitialize();
            Help.CustomPage_VerifyOrderOfHTMLComponents(Browser);
        }

        [Test, Category("Prod"), Category(siteCodeCategory), Category("CustomPage")]
        [Description("Validating the groupings and order of the custom page")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ComponentOrder_UCDavis()
        {
            /// 1. Navigate to the custom page and verify the order of the HTML components. 
            Browser.Navigate().GoToUrl(baseURL + "lms/ucdavis");
            GenericCustomPage page = new GenericCustomPage(Browser);
            page.WaitForInitialize();
            Help.CustomPage_VerifyOrderOfHTMLComponents(Browser);
        }

        [Test, Category("Prod"), Category(siteCodeCategory), Category("CustomPage")]
        [Description("Validating the groupings and order of the custom page")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ComponentOrder_UCIrvine()
        {
            /// 1. Navigate to the custom page and verify the order of the HTML components. 
            Browser.Navigate().GoToUrl(baseURL + "lms/ucirvine");
            GenericCustomPage page = new GenericCustomPage(Browser);
            page.WaitForInitialize();
            Help.CustomPage_VerifyOrderOfHTMLComponents(Browser);
        }

        [Test, Category("Prod"), Category(siteCodeCategory), Category("CustomPage")]
        [Description("Validating the groupings and order of the custom page")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ComponentOrder_UCLA()
        {
            /// 1. Navigate to the custom page and verify the order of the HTML components. 
            Browser.Navigate().GoToUrl(baseURL + "lms/ucla");
            GenericCustomPage page = new GenericCustomPage(Browser);
            page.WaitForInitialize();
            Help.CustomPage_VerifyOrderOfHTMLComponents(Browser);
        }

        [Test, Category("Prod"), Category(siteCodeCategory), Category("CustomPage")]
        [Description("Validating the groupings and order of the custom page")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ComponentOrder_UCSanDiego()
        {
            /// 1. Navigate to the custom page and verify the order of the HTML components. 
            Browser.Navigate().GoToUrl(baseURL + "lms/ucsandiego");
            GenericCustomPage page = new GenericCustomPage(Browser);
            page.WaitForInitialize();
            Help.CustomPage_VerifyOrderOfHTMLComponents(Browser);
        }

        [Test, Category("Prod"), Category(siteCodeCategory), Category("CustomPage")]
        [Description("Validating the groupings and order of the custom page")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ComponentOrder_UCSF()
        {
            /// 1. Navigate to the custom page and verify the order of the HTML components. 
            Browser.Navigate().GoToUrl(baseURL + "lms/ucsf");
            GenericCustomPage page = new GenericCustomPage(Browser);
            page.WaitForInitialize();
            Help.CustomPage_VerifyOrderOfHTMLComponents(Browser);
        }
        #endregion tests
    }  
}










































