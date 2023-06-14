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



namespace AHA.UITest
{
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome, "", "", Platforms.Windows, "", "")]

    [TestFixture]
    public class AHA_CustomPages : TestBase_AHA
    {
        #region Constructors

        public AHA_CustomPages(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public AHA_CustomPages(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }

        #endregion Constructors

        public string baseURL = AppSettings.Config["urlwithoutsitecode"].Insert(8, "aha");

        #region Tests

        [Test, Category("Prod"), Category(siteCodeCategory), Category("CustomPage")]
        [Description("Validating the groupings and order of the custom page")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ComponentOrder_Activities1()
        {
            /// 1. Navigate to the custom page and verify the order of the HTML components. 
            Browser.Navigate().GoToUrl(baseURL + "lms/activities1");
            GenericCustomPage page = new GenericCustomPage(Browser);
            page.WaitForInitialize();
            Help.CustomPage_VerifyOrderOfHTMLComponents(Browser);
        }

        [Test, Category("Prod"), Category(siteCodeCategory), Category("CustomPage")]
        [Description("Validating the groupings and order of the custom page")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ComponentOrder_Activities2()
        {
            /// 1. Navigate to the custom page and verify the order of the HTML components. 
            Browser.Navigate().GoToUrl(baseURL + "lms/activities2");
            GenericCustomPage page = new GenericCustomPage(Browser);
            page.WaitForInitialize();
            Help.CustomPage_VerifyOrderOfHTMLComponents(Browser);
        }

        [Test, Category("Prod"), Category(siteCodeCategory), Category("CustomPage")]
        [Description("Validating the groupings and order of the custom page")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ComponentOrder_Activities3()
        {
            /// 1. Navigate to the custom page and verify the order of the HTML components. 
            Browser.Navigate().GoToUrl(baseURL + "lms/activities3");
            GenericCustomPage page = new GenericCustomPage(Browser);
            page.WaitForInitialize();
            Help.CustomPage_VerifyOrderOfHTMLComponents(Browser);
        }

        [Test, Category("Prod"), Category(siteCodeCategory), Category("CustomPage")]
        [Description("Validating the groupings and order of the custom page")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ComponentOrder_Activities4()
        {
            /// 1. Navigate to the custom page and verify the order of the HTML components. 
            Browser.Navigate().GoToUrl(baseURL + "lms/activities4");
            GenericCustomPage page = new GenericCustomPage(Browser);
            page.WaitForInitialize();
            Help.CustomPage_VerifyOrderOfHTMLComponents(Browser);
        }

        [Test, Category("Prod"), Category(siteCodeCategory), Category("CustomPage")]
        [Description("Validating the groupings and order of the custom page")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ComponentOrder_Activities5()
        {
            /// 1. Navigate to the custom page and verify the order of the HTML components. 
            Browser.Navigate().GoToUrl(baseURL + "lms/activities5");
            GenericCustomPage page = new GenericCustomPage(Browser);
            page.WaitForInitialize();
            Help.CustomPage_VerifyOrderOfHTMLComponents(Browser);
        }

        [Test, Category("Prod"), Category(siteCodeCategory), Category("CustomPage")]
        [Description("Validating the groupings and order of the custom page")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ComponentOrder_Activities6()
        {
            /// 1. Navigate to the custom page and verify the order of the HTML components. 
            Browser.Navigate().GoToUrl(baseURL + "lms/activities6");
            GenericCustomPage page = new GenericCustomPage(Browser);
            page.WaitForInitialize();
            Help.CustomPage_VerifyOrderOfHTMLComponents(Browser);
        }

        #endregion tests
    }


    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome, "", "", Platforms.Windows, "", "")]

    [TestFixture]
    public class AHA_CustomPages2 : TestBase_AHA
    {
        #region Constructors

        public AHA_CustomPages2(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public AHA_CustomPages2(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }

        #endregion Constructors

        public string baseURL = AppSettings.Config["urlwithoutsitecode"].Insert(8, "aha");

        #region Tests

        [Test, Category("Prod"), Category(siteCodeCategory), Category("CustomPage")]
        [Description("Validating the groupings and order of the custom page")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ComponentOrder_Activities7()
        {
            /// 1. Navigate to the custom page and verify the order of the HTML components. 
            Browser.Navigate().GoToUrl(baseURL + "lms/activities7");
            GenericCustomPage page = new GenericCustomPage(Browser);
            page.WaitForInitialize();
            Help.CustomPage_VerifyOrderOfHTMLComponents(Browser);
        }

        [Test, Category("Prod"), Category(siteCodeCategory), Category("CustomPage")]
        [Description("Validating the groupings and order of the custom page")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ComponentOrder_Activities8()
        {
            /// 1. Navigate to the custom page and verify the order of the HTML components. 
            Browser.Navigate().GoToUrl(baseURL + "lms/activities8");
            GenericCustomPage page = new GenericCustomPage(Browser);
            page.WaitForInitialize();
            Help.CustomPage_VerifyOrderOfHTMLComponents(Browser);
        }

        [Test, Category("Prod"), Category(siteCodeCategory), Category("CustomPage")]
        [Description("Validating the groupings and order of the custom page")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ComponentOrder_webinars()
        {
            /// 1. Navigate to the custom page and verify the order of the HTML components. 
            Browser.Navigate().GoToUrl(baseURL + "lms/webinars");
            GenericCustomPage page = new GenericCustomPage(Browser);
            page.WaitForInitialize();
            Help.CustomPage_VerifyOrderOfHTMLComponents(Browser);
        }

        [Test, Category("Prod"), Category(siteCodeCategory), Category("CustomPage")]
        [Description("Validating the groupings and order of the custom page")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ComponentOrder_nihss()
        {
            /// 1. Navigate to the custom page and verify the order of the HTML components. 
            Browser.Navigate().GoToUrl(baseURL + "lms/nihss");
            GenericCustomPage page = new GenericCustomPage(Browser);
            page.WaitForInitialize();
            Help.CustomPage_VerifyOrderOfHTMLComponents(Browser);
        }

        [Test, Category("Prod"), Category(siteCodeCategory), Category("CustomPage")]
        [Description("Validating the groupings and order of the custom page")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ComponentOrder_cholesterol()
        {
            /// 1. Navigate to the custom page and verify the order of the HTML components. 
            Browser.Navigate().GoToUrl(baseURL + "lms/cholesterol");
            GenericCustomPage page = new GenericCustomPage(Browser);
            page.WaitForInitialize();
            Help.CustomPage_VerifyOrderOfHTMLComponents(Browser);
        }

        #endregion tests
    }
}










































