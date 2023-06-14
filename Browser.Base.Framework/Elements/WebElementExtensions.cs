using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Browser.Core.Framework
{
    /// <summary>
    /// Extension methods for IWebElement
    /// </summary>
    public static class WebElementExtensions
    {
        /// <summary>
        /// Clicks the element and waits for a file to be downloaded once every second until the file exists and is not locked or the timeout is
        /// reached. If you are executing locally, this will check for the file in c:\Seleniumdownloads. If you are executing 
        /// a test on the Grid, this will check for the file in \\YourGridsHubServersName\seleniumdownloads. This will return the 
        /// The fully-qualified path of the downloaded file in String format
        /// </summary>
        /// <param name="self">The web element to be clicked.</param>
        /// <param name="Browser">The browser.</param>
        /// <param name="fileName">The name of the file with its file extension that is being downloaded. This should be a file name only. It should not include any path.</param>
        /// <param name="deleteFirstIfExists">if set to <c>true</c> and the file already exists (prior to download), it will automatically be deleted.</param>
        /// <param name="fileWaitTimeout">The timeout for this operation to keep trying in milliseconds.  Default is 30000 (30 seconds).</param>
        /// <returns>The fully-qualified path of the downloaded file</returns>
        /// <exception cref="System.TimeoutException">Thrown if the file does not exist within the timeout specified.</exception>
        public static string ClickAndWaitForDownload(this IWebElement self, IWebDriver Browser, string fileName, bool deleteFirstIfExists = true, 
            double fileWaitTimeout = 30000)
        {
            NetworkShareAccesser.Access();

            var fullPath = SeleniumCoreSettings.DefaultDownloadDirectory + fileName;

            // Delete the file if it exists and the tester wants to delete it 
            if (File.Exists(fullPath) && deleteFirstIfExists)
            {
                File.Delete(fullPath);
            }

            // Create the download directory (seleniumdownloads folder inside C drive) if it has not been created yet. I created the directory
            // on each Hub Server, so this logic wont be needed when executing tests on the Grid
            if (!File.Exists(SeleniumCoreSettings.DefaultDownloadDirectory))
            {
                FileUtils.CreateDirectoryIfNotExists(SeleniumCoreSettings.DefaultDownloadDirectory);
            }

            self.Click(Browser);

            return Browser.WaitForDownload(fileName, fileWaitTimeout);
        }

        // I once solved the problem of Selenium Grid downloads by implementing a workaround, as explained below in the Summary, however
        // that still did not work because even if we can transfer the file from server to local as explained below, it doesnt solve the problem 
        // where if the file is ALREADY downloaded on a node from a prior test, we need to delete that file first, else the download appends a
        // number onto the end of the file name, i.e. mydownload (1).xlsx. In the end, I settled on creating 1 shared network drive that serves
        // as a file sharing drive, then we can just set our default download directory to there and delete/edit/download/upload files from that
        // drive when executing on the grid. See the ClickAndWaitForDownload() method that is not commented out for tha implementation. Also see:
        // https://code.premierinc.com/docs/display/PQA/Grid+Configuration on how to setup a share for a network server if we ever lose this one
        ///// <summary>
        ///// Clicks the element and waits for a file to be downloaded in the default download directory once every second until the 
        ///// file exists and is not locked or the timeout is reached. IMPORTANT: On the Selenium Grid, this is currently only compatible in Chrome, 
        ///// and currently only available to verify 1 download in a given Browser session. If you need to verify multiple files 
        ///// in the same Browser session, I will need to add more logic to this code or you can shut down current Browser session or clear cache 
        ///// then download again. The logic I will need to add is adding the parameter downloadedFileInstance, see that aprameter inside
        ///// TransferDownloadedRemoteFileToLocal().
        ///// Please add a test category filter to your build definition that excludes non-chrome tests so the build deinfition will not 
        ///// execute your non-chrome tests that call this method when executing on the Grid.
        ///// For example, add an NUNIT category titled "OperatingSystemAutomation", then on the build definition add the following
        ///// condition to the testfiltercriteria variable (testFiltercriteria: '(Category=Remote.firefox & Category!=OperatingSystemAutomation)'
        ///// NOTE: If your test is being executed on the grid, this method will perform additional steps before verifying that the file was 
        ///// downloaded. These steps are as follows: Opens a new tab, navigates to chrome://downloads/ then returns 
        ///// a list of strings representing the remote server's file location where all of the current Browser instances downloads 
        ///// are located. For example: C:\seleniumdownloads\MyExcelDocument.xlsx. Then adds an Input element 
        ///// on this downloads page with an attribute of Type=Value, then uploads the most recently downloaded file from the current Browser 
        ///// session, then returns the contents of this uploaded file as a base64 encoded string, then converts/decodes the Base64 string 
        ///// into an 8-bit integer array. Once the string is converted, it trasnfers this encoding from server to local and saves 
        ///// the encoding text as the same name of the originally downloaded from on the server. You can then open/edit/verify/save 
        ///// this local file as you would the originally downloaded file, as it is a copy/transfer of the original. Once the transfer 
        ///// is verified to be on the local machine, this will then close the new tab and return focus back to the original tab.
        ///// Future: Firefox implementation: https://code.premierinc.com/issues/browse/PQA-59 
        ///// https://stackoverflow.com/questions/47068912/how-to-download-a-file-using-the-remote-selenium-webdriver
        ///// </summary>
        ///// <param name="self">The web element to be clicked.</param>
        ///// <param name="Browser">The browser.</param>
        ///// <param name="fileName">Name of the file which will be downloaded (this should be a file name only.  It should not include any path).</param>
        ///// <param name="deleteFirstIfExists">if set to <c>true</c> and the file already exists (prior to download), it will automatically be deleted.</param>
        ///// <param name="fileWaitTimeout">The timeout for this operation to keep trying in milliseconds.  Default is 10000 (10 seconds).</param>
        ///// <returns>The fully-qualified path of the downloaded file.</returns>
        ///// <exception cref="System.TimeoutException">Thrown if the file does not exist within the timeout specified.</exception>
        //public static string ClickAndWaitForDownload(this IWebElement self, IWebDriver Browser, string fileName, bool deleteFirstIfExists = true,
        //    double fileWaitTimeout = 30000)
        //{
        //    //var fullPath = self.DownloadDirectory + fileName;
        //    var fullPath = "\\\\LMS-NAS-NP\\lms_filestore\\AutomationBuildOutput\\" + fileName;

        //    if (File.Exists(fullPath) && deleteFirstIfExists)
        //    {
        //        File.Delete(fullPath);
        //    }

        //    self.Click(Browser);

        //    return Browser.WaitForDownload(fileName, fileWaitTimeout);
        //}

        /// <summary>
        /// Returns the Shadow-Root within a DOM. You can then locate an element within this returned Shadow-Root. NOTE: You can only use 
        /// CSS Selectors when locating elements within Shadow Roots
        /// https://stackoverflow.com/questions/55761810/how-to-automate-shadow-dom-elements-using-selenium
        /// </summary>
        /// <param name="Browser"></param>
        /// <param name="element">The parent element that contains the Shadow Root</param>
        /// <returns></returns>
        public static ShadowRoot GetShadowRoot(this IWebElement self, IWebDriver Browser)
        {
            ShadowRoot shadowRootElem = (ShadowRoot)((IJavaScriptExecutor)Browser).ExecuteScript("return arguments[0].shadowRoot", self);
            return shadowRootElem;
        }

        /// <summary>
        /// Performs a javascript click
        /// </summary>
        /// <param name="self">The web element to be clicked.</param>
        /// <param name="browser">The browser</param>
        /// <returns></returns>
        public static IWebElement ClickJS(this IWebElement self, IWebDriver browser)
        {
            browser.ExecuteScript("arguments[0].click();", self);
            return self;
        }

        /// <summary>
        /// If we are executing a test through emulation (Browser.MobileEnabled()), then this click will click using 
        /// Javascript. Else this clicks using the standard selenium built-in click. Right now, chromedriver has a
        /// bug where the selenium built-in click doesnt work consistently when running on grid through emulation. 
        /// When this bug is fixed, we can remove this method and refactor our test case to use the regular click 
        /// with no overload
        /// </summary>
        /// <param name="self">The web element to be clicked.</param>
        /// <param name="Browser">The browser</param>
        public static void Click(this IWebElement self, IWebDriver Browser)
        {
            var element = self as IWebElement;

            // If the emulationObject is null (meaning Firefox and IE do not have this capability) or if Chrome returns false for this
            // property, then do a regular selenium click.
            if (Browser.MobileEnabled())
            {
                element.ClickJS(Browser);
            }
            else
            {
                element.Click();
            }
        }

        /// <summary>
        /// Scrolls horizontally or vertically to a specified element that contains a scroll bar
        /// </summary>
        /// <param name="self"></param>
        /// <param name="Browser">The driver</param>
        /// <param name="divElem">(Optional) If your element is contained within a sub-window that has a scrollbar, 
        /// then you must pass this argument. This is the div element that contains the scroll bar</param>
        public static void ClickAfterScroll(this IWebElement self, IWebDriver Browser, IWebElement divElem = null)
        {

            if (divElem == null)
            {
                ElemSet.ScrollToElement(Browser, self);
            }
            else
            {
                ElemSet.ScrollToElementWithinScrollBar(Browser, divElem, self, "Vertical");
            }

            Browser.ExecuteScript("arguments[0].click();", self);
        }

        public static void SendKeysJS(this IWebElement self, IWebDriver Browser, string textToEnter)
        {
            Browser.ExecuteScript("arguments[0].value = arguments[1]", self, textToEnter);
        }

        /// <summary>
        /// Checks to see if your element exists, and if so, sends user-specified or random text to it
        /// </summary>
        /// <param name="self"></param>
        /// <param name="Browser"></param>
        /// <param name="by"></param>
        /// <param name="textToEnter"></param>
        public static void SendKeysIfElemExists(this IWebElement self, IWebDriver Browser, By by, string textToSend = null)
        {
            textToSend = textToSend.IsNullOrEmpty() ? DataUtils.GetRandomString(10) : textToSend;

            if (Browser.Exists(by))
            {
                self.SendKeys(textToSend);
            }
        }

        public static void MakeElementInvisible(this IWebElement self, IWebDriver Browser)
        {
            Browser.ExecuteScript("arguments[0].setAttribute('style', 'display: none');", self);
        }

        public static void MakeElementVisible(this IWebElement self, IWebDriver Browser)
        {
            Browser.ExecuteScript("arguments[0].setAttribute('style', 'display: block');", self);
        }

        /// <summary>
        /// Firefox has a bug which prevents some events from being executed while the browser window is out of focus. This could be an issue
        /// when you're running your automation tests - which might be typing even if the window is out of focus. You will notice this issue
        /// if you enter text in a required field, click a button which closes a modal, and see that the modal does not close, and the system
        /// warsn you that the required field is empty (the field cleared because of this bug). To fix this, we have to trigger an event through
        /// javascript. Use this method to trigger that event. For more info, see:
        /// https://stackoverflow.com/questions/9505588/selenium-webdriver-is-clearing-out-fields-after-sendkeys-had-previously-populate
        /// </summary>
        /// <param name="Browser"></param>
        /// <param name="whatDateTxt"></param>
        public static void TriggerChangeEvent(this IWebElement self, IWebDriver Browser)
        {
            Browser.ExecuteScript("$(arguments[0]).change();", self);
        }
    }
}
