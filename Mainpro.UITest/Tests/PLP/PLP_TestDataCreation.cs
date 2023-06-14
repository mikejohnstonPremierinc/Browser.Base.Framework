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
using LMS.Data;
using LS.AppFramework.Constants_LTS;
using AventStack.ExtentReports;
using Mainpro.UITest;

namespace PLP.TestData
{
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]
    [TestFixture]
    public class PLP_TestDataCreation : TestBase
    {
        #region Constructors
        public PLP_TestDataCreation(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public PLP_TestDataCreation(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }
        #endregion

        #region Tests

        [Test]
        [Description("This script creates a Data and stop at the mentioned stage," +
            "mention the stepnumber where you want to go and the version isSelfGuided: true/false")]
        public void TestUsers_1()
        {
            Help.PLP_GoToStep(Browser, 2, TestContext.CurrentContext.Test,
                isSelfGuided: true);
        }
        [Test]
        [Description("This script creates a Data and stop at the mentioned stage," +
            "mention the stepnumber where you want to go and the version isSelfGuided: true/false")]
        public void TestUsers_2()
        {
            Help.PLP_GoToStep(Browser, 3, TestContext.CurrentContext.Test,
                isSelfGuided: false);
        }
        [Test]
        [Description("This script creates a Data and stop at the mentioned stage," +
            "mention the stepnumber where you want to go and the version isSelfGuided: true/false")]
        public void TestUsers_3()
        {
            Help.PLP_GoToStep(Browser, 4, TestContext.CurrentContext.Test,
                isSelfGuided: true);
        }

    }

    #endregion Tests
}
