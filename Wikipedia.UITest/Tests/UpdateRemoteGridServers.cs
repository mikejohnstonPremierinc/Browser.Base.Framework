using AventStack.ExtentReports;
using Browser.Core.Framework;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.IO;
using Wikipedia.AppFramework;


namespace Wikipedia.UITest
{
    [TestFixture]
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]

    [TestFixture]
    public class UpdateRemoteGridServers : TestBase
    {
        #region Constructors

        public UpdateRemoteGridServers(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public UpdateRemoteGridServers(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }

        #endregion Constructors

        // These tests can be executed to bulk update the web driver versions on all of the Selenium Grid node servers, instead
        // of manually remoting into those servers one-by-one to update the drivers.
        // Pre-requisite #1: You must execute this test's local instance, because the local instance is the instance that will place the
        // latest web driver in the appropriate path to copy from, via WebDriverManager https://code.premierinc.com/docs/display/PQA/Browser+Configurations#BrowserConfigurations-AutomaticBrowserConfiguration
        // Pre-requisite #2: The c:\\seleniumdrivers folder on the node server must be a File Share and app_pqa_account1 must
        // have Full Control under Permissions. This pre-requisite has already been completed as of 2023/05/05

        [Test]
        public void UpdateGridWebDrivers_c3dilmssg_Chrome()
        {
            for (int i = 2; i <= 11; i++)
            {
                OpenNetworkConnection($"c3dilmssg{i:00}");

                File.Copy(DriverPathChrome + "chromedriver.exe", string.Format(@"\\{0}\seleniumdrivers\chromedriver.exe", $"c3dilmssg{i:00}"), true);
            }
        }

        [Test]
        public void UpdateGridWebDrivers_c3dierpdevsel_Chrome()
        {
            for (int i = 2; i <= 12; i++)
            {
                OpenNetworkConnection($"c3dierpdevsel{i:00}");

                File.Copy(DriverPathChrome + "chromedriver.exe", string.Format(@"\\{0}\seleniumdrivers\chromedriver.exe", $"c3dierpdevsel{i:00}"), true);
            }
        }

        [Test]
        public void UpdateGridWebDrivers_c3dilmssg_Edge()
        {
            for (int i = 2; i <= 11; i++)
            {
                OpenNetworkConnection($"c3dilmssg{i:00}");

                File.Copy(DriverPathEdge + "msedgedriver.exe", string.Format(@"\\{0}\seleniumdrivers\MicrosoftWebDriver.exe", $"c3dilmssg{i:00}"), true);
            }
        }

        [Test]
        public void UpdateGridWebDrivers_c3dierpdevsel_Edge()
        {
            for (int i = 2; i <= 12; i++)
            {
                OpenNetworkConnection($"c3dierpdevsel{i:00}");

                File.Copy(DriverPathEdge + "msedgedriver.exe", string.Format(@"\\{0}\seleniumdrivers\MicrosoftWebDriver.exe", $"c3dierpdevsel{i:00}"), true);
            }
        }

        [Test]
        public void UpdateGridWebDrivers_c3dilmssg_Firefox()
        {
            for (int i = 2; i <= 11; i++)
            {
                OpenNetworkConnection($"c3dilmssg{i:00}");

                File.Copy(DriverPathFirefox + "geckodriver.exe", string.Format(@"\\{0}\seleniumdrivers\geckodriver.exe", $"c3dilmssg{i:00}"), true);
            }
        }

        [Test]
        public void UpdateGridWebDrivers_c3dierpdevsel_Firefox()
        {
            for (int i = 2; i <= 12; i++)
            {
                OpenNetworkConnection($"c3dierpdevsel{i:00}");

                File.Copy(DriverPathFirefox + "geckodriver.exe", string.Format(@"\\{0}\seleniumdrivers\geckodriver.exe", $"c3dierpdevsel{i:00}"), true);
            }
        }

        public void OpenNetworkConnection(string serverName)
        {
            NetworkShareAccesser.Access(serverName, "app_pqa_account1", "lfczLQ0y798!gR5DZJ2t");
        }

        [Test]
        public void TransferBrowserInstallationFile_c3dilmssg()
        {
            for (int i = 2; i <= 11; i++)
            {
                OpenNetworkConnection($"c3dilmssg{i:00}");

                File.Copy(@"C:\Users\mjohnsto\Downloads\Firefox Installer.exe", 
                    string.Format(@"\\{0}\seleniumdrivers\Firefox Installer.exe", $"c3dilmssg{i:00}"), true);
            }
        }

        [Test]
        public void TransferBrowserInstallationFile_c3dierpdevsel()
        {
            for (int i = 2; i <= 12; i++)
            {
                OpenNetworkConnection($"c3dierpdevsel{i:00}");

                File.Copy(@"C:\Users\mjohnsto\Downloads\Firefox Installer.exe", 
                    string.Format(@"\\{0}\seleniumdrivers\Firefox Installer.exe", $"c3dierpdevsel{i:00}"), true);
            }
        }
    }
}





