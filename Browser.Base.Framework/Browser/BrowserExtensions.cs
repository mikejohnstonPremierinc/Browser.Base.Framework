using Browser.Core.Framework.Data;
using Newtonsoft.Json;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using WDSE;
using WDSE.Decorators;
using WDSE.ScreenshotMaker;

namespace Browser.Core.Framework
{
    /// <summary>
    /// Extension methods on the IWebDriver interface.
    /// </summary>
    public static class BrowserExtensions
    {
        private static readonly string igGrid = "igGrid";
        private static readonly string igDataChart = "igDataChart";

        /// <summary>
        /// Takes a screenshot and saves it in the SeleniumCoreSettings.ScreenshotLocation folder location.
        /// </summary>
        /// <param name="self">The driver instance</param>
        /// <param name="filenamePrefix">(Optional). You can specify a prefix to add onto the screenshot file name</param>
        /// <param name="fullPage">(Optional). Send 'true' to this parameter if you want to capture the entire scrollable area 
        /// in your screensheet. Else it will only capture the viewport, which is the visible area. Further reading: 
        /// https://github.com/Noksa/WebDriver.Screenshots.Extensions/wiki/Decorators
        /// https://www.youtube.com/watch?v=MUVCE550yw8 </param>
        public static string TakeScreenshot(this IWebDriver self, string filenamePrefix = null, bool fullPage = false)
        {
            // Get a string representing the (folder path + file name) where we will save the screenshot
            var fullPathWithFileName = FileUtils.GetFolderPathPlusFileName_Screenshots_DuringTest(self, filenamePrefix);

            // Take the screenshot and save it
            var tsc = self as ITakesScreenshot;
            if (tsc != null)
            {
                FileUtils.CreateDirectoryIfNotExists(fullPathWithFileName);

                if (fullPage)
                {
                    var verticalscreenshot = new VerticalCombineDecorator(new ScreenshotMaker());
                    var screen = self.TakeScreenshot(verticalscreenshot);
                    File.WriteAllBytes(fullPathWithFileName, screen);
                }
                else
                {
                    var ss = tsc.GetScreenshot();
                    ss.SaveAsFile(fullPathWithFileName, ScreenshotImageFormat.Png);
                }
            }
            else
            {
                throw new Exception("Failed to save screenshot");
            }

            // Files cant be saved with double quotes. Also, Extent Reports does not show screenshots maximized when using single quotes for some 
            // reason, so just replacing double quotes with a blank
            string fullPathWithFileNameWithoutDoubleQuotes = fullPathWithFileName.Replace("\"", "");
            string fullPathWithFileNameWithoutDoubleAndSingleQuotes = fullPathWithFileNameWithoutDoubleQuotes.Replace("'", "");
            return fullPathWithFileNameWithoutDoubleAndSingleQuotes;
        }

        /// <summary>
        /// Enters the URL and waits for a file to be downloaded. 
        /// If you are executing locally, this will check for the file in c:\Seleniumdownloads. If you are executing 
        /// a test on the Grid, this will check for the file in \\YourGridsHubServersName\seleniumdownloads. This will return the 
        /// The fully-qualified path of the downloaded file in String format
        /// </summary>
        /// <param name="self">The web Driver.</param>
        /// <param name="fileName">Name of the file which will be downloaded (this should be a file name only.  It should not include any path).</param>
        /// <param name="url"></param>
        /// <param name="deleteFirstIfExists">if set to <c>true</c> and the file already exists (prior to download), it will automatically be deleted.</param>
        /// <param name="fileWaitTimeout">The timeout for this operation to keep trying in milliseconds.  Default is 10000 (10 seconds).</param>
        /// <returns>The fully-qualified path of the downloaded file.</returns>
        /// <exception cref="System.TimeoutException">Thrown if the file does not exist within the timeout specified.</exception>
        public static string EnterURLAndWaitForDownload(this IWebDriver self, string fileName, string url, bool deleteFirstIfExists,
            double fileWaitTimeout = 10000)
        {
            var fullPath = SeleniumCoreSettings.DefaultDownloadDirectory + fileName;

            if (File.Exists(fullPath) && deleteFirstIfExists)
            {
                File.Delete(fullPath);
            }

            self.Navigate().GoToUrl(url);

            // Edge opens a new tab when clicking on a file that opens an Office document. I have not found an Option to disable this. See further notes 
            // in BaseChromeOptions.cs. Also, we can not add a workaround to click on the Download button of this new tab, as it does not have HTML DOM
            // for the Download button
            if (self.Type() == BrowserNames.Edge)
            {
                throw new Exception("At the moment, we can not download Edge files because Edge opens a new tab when trying to download a " +
                    "Microsoft Office file. I have not found a Chrome Option to disable the new tab, and the new tab is not interactable, " +
                    "so until Selenium releases a Chrome Option to disable this, please do not include an Edge test fixture for this test");
            }

            return self.WaitForDownload(fileName, fileWaitTimeout);
        }

        /// <summary>
        /// Returns true if the driver instance is on mobile
        /// </summary>
        public static bool MobileEnabled(this IWebDriver self)
        {
            bool MobileEnabled = false;

            var emulationObject = self.GetCapabilities().GetCapability("mobileEmulation");

            if (emulationObject != null)
            {
                if ((bool)emulationObject == true)
                {
                    MobileEnabled = true;
                }
            }

            return MobileEnabled;
        }

        /// <summary>
        /// Returns true if the driver instance is on mobile
        /// </summary>
        public static bool MobileEnabled2(this IWebDriver self)
        {
            bool MobileEnabled = false;

            var emulationObject = self.GetCapabilities().GetCapability("mobileEmulationEnabled");

            if (emulationObject != null)
            {
                if ((bool)emulationObject == true)
                {
                    MobileEnabled = true;
                }
            }

            return MobileEnabled;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Browser"></param>
        /// <param name="sites"><see cref="Constants"/> for a list of sites with different compatabilties. Send the list of sites</param>
        public static void ThrowErrorIfCurrentSiteNotCompatibleWith(this IWebDriver self, List<string> sites)
        {
            // Only AHA has the All option. So if the tester is testing on another portal, fail the test and tell him/her
            if (!sites.Any(s => self.Url.Contains(s)))
            {
                throw new Exception(string.Format("The site you are testing on, {0}, is not compatible with the function you are " +
                    "trying to use", self.Url));
            }
        }

        /// <summary>
        /// Executes the specified javascript.
        /// Javascript objects that are returned are represented as an IDictionary&lt;string, object&gt;
        /// Javascript arrays that are returned are represented as a ReadonlyCollection&lt;object&gt;
        /// Therefore, if the javascript returns an array of objects, it would be represented as ReadonlyCollection&lt;IDictionary&lt;string, object&gt;&gt;
        /// </summary>        
        /// <param name="self">The the browser on which the javascript should be executed.</param>
        /// <param name="js">The javascript that should be executed.</param>
        /// <param name="parameters">(Optional) The parameters that should be passed to the script.  Note: Parameters can be accessed in your script via arguments[x] where x is the index of the parameter.  If you pass an IWebElement, it will be translated to an actual javascript reference to the DOM element.</param>
        /// <returns>The object returned by the javascript call.</returns>        
        /// <exception cref="System.ArgumentException">If the browser does not support javascript execution.</exception>
        public static object ExecuteScript(this IWebDriver self, string js, params object[] parameters)
        {
            if (self == null)
                throw new ArgumentNullException("self");
            if (string.IsNullOrEmpty(js))
                throw new ArgumentException("Your javascript string is empty");
            var jsExec = self as IJavaScriptExecutor;
            if (jsExec == null)
                throw new ArgumentException(string.Format("The current browser ({0}) does not support javascript", self.GetType().Name));

            return jsExec.ExecuteScript(js, parameters);
        }

        /// <summary>
        /// Waits for a file to be downloaded once every second until the file exists and is not locked or the timeout is reached.
        /// NOTE: If you want to delete the file first before downloading, use either
        /// <see cref="WebElementExtensions.ClickAndWaitForDownload(IWebElement, IWebDriver, string, bool, double)"/> 
        /// or use <see cref="EnterURLAndWaitForDownload(IWebDriver, string, string, bool, double)"/>
        /// If you are executing locally, this will check for the file in c:\Seleniumdownloads. If you are executing 
        /// a test on the Grid, this will check for the file in \\YourGridsHubServersName\seleniumdownloads. This will return the 
        /// The fully-qualified path of the downloaded file in String format
        /// </summary>
        /// <param name="self">The web driver.</param>
        /// <param name="fileName">The name of the file with its file extension that is being downloaded. This should be a file name only.
        /// It should not include any path.</param>
        /// <param name="fileWaitTimeout">The timeout for this operation to keep trying in milliseconds.  Default is 30000 (30 seconds).</param>
        /// <exception cref="System.TimeoutException">Thrown if the file does not exist within the timeout specified.</exception>
        /// <returns>The fully-qualified path to the file that was downloaded.</returns>
        public static string WaitForDownload(this IWebDriver self, string fileName, double fileWaitTimeout = 30000)
        {
            var fullPath = SeleniumCoreSettings.DefaultDownloadDirectory + fileName;

            /// See the BaseEdgeOptions.cs class for why we have this block of code
            //// Edge opens a new tab when clicking on a file that opens an Office document. I have not found an Option to disable this. See further notes 
            //// in BaseEdgeOptions.cs. Also, we can not add a workaround to click on the Download button of this new tab, as it does not have HTML DOM
            //// for the Download button
            //if (Browser.Type() == BrowserNames.Edge)
            //{
            //    throw new Exception("At the moment, we can not download Edge files because Edge opens a new tab when trying to download a " +
            //        "Microsoft Office file. I have not found a Chrome Option to disable the new tab, and the new tab is not interactable, " +
            //        "so until Selenium releases a Chrome Option to disable this, please do not include an Edge test fixture for this test");
            //}

            FileUtils.WaitForFile(fullPath, fileWaitTimeout);
            return fullPath;
        }

        /// <summary>
        /// Returns the type of browser, i.e. Chrome, Edge, etc.
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string Type(this IWebDriver self)
        {
            var currentBrowser = (self.GetCapabilities().GetCapability("browserName").ToString());

            if (currentBrowser == BrowserNames.Chrome)
            {
                return BrowserNames.Chrome;
            }
            else if (currentBrowser == BrowserNames.Firefox)
            {
                return BrowserNames.Firefox;
            }
            else if (currentBrowser == BrowserNames.Edge)
            {
                return BrowserNames.Edge;
            }
            else if (Regex.Replace(currentBrowser, @"\s+", "") ==
                Regex.Replace(BrowserNames.InternetExplorer, @"\s+", ""))
            {
                return BrowserNames.InternetExplorer;
            }
            else if (currentBrowser == BrowserNames.Safari)
            {
                return BrowserNames.Safari;
            }
            else
            {
                throw new Exception("Current browser instance not recognized. We may have to add another If statement here for the new browser");
            }

        }

        /// <summary>
        /// Waits until the URL matches the user-specified string
        /// </summary>
        /// <param name="self">The driver instance</param>
        /// <param name="url">The string you want to wait for in the URL</param>
        /// <param name="timeout">(Optional). If not specified, will use a default timeout of 60 seconds. After the timeout, 
        /// the test will fail if the URL fails to match the string</param>
        public static void WaitForURLToMatchString(this IWebDriver self, string url, TimeSpan timeout)
        {
            timeout = string.IsNullOrEmpty(url) ? TimeSpan.FromSeconds(60) : timeout;
            var wait = new WebDriverWait(self, timeout);

            try
            {
                wait.Until(browser =>
                {
                    return browser.Url = url;
                });
            }
            catch
            {
                throw new WebDriverTimeoutException(String.Format("The browser's URL failed to match '{0}' after waiting " +
                    "for the timeout", url));
            }
        }

        /// <summary>
        /// Waits until the URL contains the user-specified string
        /// </summary>
        /// <param name="self">The driver instance</param>
        /// <param name="url">The string you want to wait for in the URL</param>
        /// <param name="timeout">(Optional). If not specified, will use a default timeout of 60 seconds. After the timeout, 
        /// the test will fail if the URL fails to contain the string</param>
        public static void WaitForURLToContainString(this IWebDriver self, string url, TimeSpan timeout)
        {
            timeout = string.IsNullOrEmpty(url) ? TimeSpan.FromSeconds(60) : timeout;
            var wait = new WebDriverWait(self, timeout);

            try
            {
                wait.Until(browser =>
                {
                    return browser.Url.Contains(url);
                });
            }
            catch
            {
                throw new WebDriverTimeoutException(String.Format("The browser's URL failed to contain '{0}' after waiting " +
                    "for the timeout", url));
            }
        }



        /// <summary>
        /// Gets the capabilities of the browser.
        /// IMPORTANT: This method MIGHT return null!
        /// </summary>
        /// <param name="self">The self.</param>
        /// <returns></returns>
        public static ICapabilities GetCapabilities(this IWebDriver self)
        {
            var tmp = self as IHasCapabilities;
            if (tmp == null)
                return null;

            return tmp.Capabilities;
        }

        #region GoToPage



        #endregion

        #region GetDataTable

        /// <summary>        
        /// Gets the FORMATTED data from the html &lt;table/&gt; element as a .NET DataTable.  All cell values are represented
        /// as strings.
        /// - The table MUST have a header row &lt;thead&gt; with &lt;th&gt; elements, or you must pass in column definitions.
        /// 
        /// WARNING: This is an EXPENSIVE method call!  One benchmark took 20 seconds to retrieve 100 rows and 10 columns of data.
        /// Consider using GetDataFromIg* methods for better performance if you don't care about the FORMAT of the data.
        /// </summary>
        /// <param name="self">The Web Browser</param>
        /// <param name="table">An IWebElement that represents an html table element.</param>
        /// <param name="columnDefinitions">(Optional) The columns to be exported.  This parameter can be used if the table does not specify columns using the &lt;thead&gt; element, or if you only want to extract a subset of the columns.</param>
        /// <returns>A DataTable that can be indexed using the column headers.</returns>
        public static DataTable GetDataFromHtmlTable(this IWebDriver self, IWebElement table, params DataColumnDefinition[] columnDefinitions)
        {
            return new HtmlTableToDataTableAdapter()
                .GetDataTable(table, columnDefinitions);
        }

        /// <summary>
        /// Gets a DataTable from an igGrid control by using javascript to get the datasource and then
        /// converting the JSON notation into a .NET DataTable.
        /// </summary>
        /// <param name="self">The Web Browser</param>        
        /// <param name="element">An IWebElement that points to the specified control.</param>        
        /// <param name="columnDefinitions">(Optional) The columns to be retrieved.  The names and types of the columns must be compatible with the names of the properties on the javascript objects that back the ig-control</param>
        /// <returns>A DataTable with all of the data from the datasource of the igGrid control.</returns>
        public static DataTable GetDataFromIgGrid(this IWebDriver self, IWebElement element, params DataColumnDefinition[] columnDefinitions)
        {
            return GetDataFromIgControl(self, igGrid, element, columnDefinitions);
        }

        /// <summary>
        /// Gets a DataTable from an igGrid control by using javascript to get the datasource and then
        /// converting the JSON notation into a .NET DataTable.
        /// </summary>
        /// <param name="self">The Web Browser</param>        
        /// <param name="element">An IWebElement that points to the specified control.</param>
        /// <param name="settings">Settings that control how JSON data is deserialized.</param>
        /// <param name="columnDefinitions">(Optional) The columns to be retrieved.  The names and types of the columns must be compatible with the names of the properties on the javascript objects that back the ig-control</param>
        /// <returns>A DataTable with all of the data from the datasource of the igGrid control.</returns>
        public static DataTable GetDataFromIgGrid(this IWebDriver self, IWebElement element, JsonSerializerSettings settings, params DataColumnDefinition[] columnDefinitions)
        {
            return GetDataFromIgControl(self, igGrid, element, settings, columnDefinitions);
        }

        /// <summary>
        /// Gets a DataTable from an igGrid control by using javascript to get the datasource and then
        /// converting the JSON notation into a .NET DataTable.
        /// </summary>
        /// <param name="self">The Web Browser</param>        
        /// <param name="element">An IWebElement that points to the specified control.</param>
        /// <param name="jsonConverter">The json converter that converts a json string into a DataTable.</param>
        /// <param name="columnDefinitions">(Optional) The columns to be retrieved.  The names and types of the columns must be compatible with the names of the properties on the javascript objects that back the ig-control</param>
        /// <returns>A DataTable with all of the data from the datasource of the igGrid control.</returns>
        public static DataTable GetDataFromIgGrid(this IWebDriver self, IWebElement element, IJsonToDataTableConverter jsonConverter, params DataColumnDefinition[] columnDefinitions)
        {
            return GetDataFromIgControl(self, igGrid, element, jsonConverter, columnDefinitions);
        }

        /// <summary>
        /// Gets a DataTable from an igDataChart control by using javascript to get the datasource and then
        /// converting the JSON notation into a .NET DataTable.
        /// </summary>
        /// <param name="self">The Web Browser</param>
        /// <param name="element">An IWebElement that points to the specified control.</param>
        /// <param name="columnDefinitions">(Optional) The columns to be retrieved.  The names and types of the columns must be compatible with the names of the properties on the javascript objects that back the ig-control</param>
        /// <returns>
        /// A DataTable with all of the data from the datasource of the igDataChart control.
        /// </returns>
        public static DataTable GetDataFromIgDataChart(this IWebDriver self, IWebElement element, params DataColumnDefinition[] columnDefinitions)
        {
            return GetDataFromIgControl(self, igDataChart, element, columnDefinitions);
        }

        /// <summary>
        /// Gets a DataTable from an igDataChart control by using javascript to get the datasource and then
        /// converting the JSON notation into a .NET DataTable.
        /// </summary>
        /// <param name="self">The Web Browser</param>
        /// <param name="element">An IWebElement that points to the specified control.</param>
        /// <param name="settings">Settings that control how JSON data is deserialized.</param>
        /// <param name="columnDefinitions">(Optional) The columns to be retrieved.  The names and types of the columns must be compatible with the names of the properties on the javascript objects that back the ig-control</param>
        /// <returns>
        /// A DataTable with all of the data from the datasource of the igDataChart control.
        /// </returns>
        public static DataTable GetDataFromIgDataChart(this IWebDriver self, IWebElement element, JsonSerializerSettings settings, params DataColumnDefinition[] columnDefinitions)
        {
            return GetDataFromIgControl(self, igDataChart, element, settings, columnDefinitions);
        }

        /// <summary>
        /// Gets a DataTable from an igDataChart control by using javascript to get the datasource and then
        /// converting the JSON notation into a .NET DataTable.
        /// </summary>
        /// <param name="self">The Web Browser</param>
        /// <param name="element">An IWebElement that points to the specified control.</param>
        /// <param name="jsonConverter">The json converter that converts a json string into a DataTable.</param>
        /// <param name="columnDefinitions">(Optional) The columns to be retrieved.  The names and types of the columns must be compatible with the names of the properties on the javascript objects that back the ig-control</param>
        /// <returns>
        /// A DataTable with all of the data from the datasource of the igDataChart control.
        /// </returns>
        public static DataTable GetDataFromIgDataChart(this IWebDriver self, IWebElement element, IJsonToDataTableConverter jsonConverter, params DataColumnDefinition[] columnDefinitions)
        {
            return GetDataFromIgControl(self, igDataChart, element, jsonConverter, columnDefinitions);
        }

        /// <summary>
        /// Gets a DataTable from an Infragistics control by using javascript to get the datasource and then
        /// converting the JSON notation into a .NET DataTable.
        /// </summary>
        /// <param name="self">The Web Browser</param>
        /// <param name="elementType">The type of Infragistics control (igGrid, igDataChart, etc)</param>
        /// <param name="element">An IWebElement that points to the specified control.</param>
        /// <param name="columnDefinitions">(Optional) The columns to be retrieved.  The names and types of the columns must be compatible with the names of the properties on the javascript objects that back the ig-control</param>
        /// <returns></returns>
        public static DataTable GetDataFromIgControl(this IWebDriver self, string elementType, IWebElement element, params DataColumnDefinition[] columnDefinitions)
        {
            return new InfragisticsControlToDataTableAdapter(self, elementType, new DefaultJsonToDataTableConverter())
                .GetDataTable(element, columnDefinitions);
        }

        /// <summary>
        /// Gets a DataTable from an Infragistics control by using javascript to get the datasource and then
        /// converting the JSON notation into a .NET DataTable.
        /// </summary>
        /// <param name="self">The Web Browser</param>
        /// <param name="elementType">The type of Infragistics control (igGrid, igDataChart, etc)</param>
        /// <param name="element">An IWebElement that points to the specified control.</param>
        /// <param name="settings">Settings that control how JSON data is deserialized.</param>
        /// <param name="columnDefinitions">(Optional) The columns to be retrieved.  The names and types of the columns must be compatible with the names of the properties on the javascript objects that back the ig-control</param>
        /// <returns></returns>
        public static DataTable GetDataFromIgControl(this IWebDriver self, string elementType, IWebElement element, JsonSerializerSettings settings, params DataColumnDefinition[] columnDefinitions)
        {
            return new InfragisticsControlToDataTableAdapter(self, elementType, new DefaultJsonToDataTableConverter(settings))
                .GetDataTable(element, columnDefinitions);
        }

        /// <summary>
        /// Gets a DataTable from an Infragistics control by using javascript to get the datasource and then
        /// converting the JSON notation into a .NET DataTable.
        /// </summary>
        /// <param name="self">The Web Browser</param>
        /// <param name="elementType">The type of Infragistics control (igGrid, igDataChart, etc)</param>
        /// <param name="element">An IWebElement that points to the specified control.</param>
        /// <param name="jsonConverter">The json converter that converts a json string into a DataTable.</param>
        /// <param name="columnDefinitions">(Optional) The columns to be retrieved.  The names and types of the columns must be compatible with the names of the properties on the javascript objects that back the ig-control</param>
        /// <returns>
        /// A DataTable with all of the data from the datasource of the infragistics control.
        /// </returns>
        public static DataTable GetDataFromIgControl(this IWebDriver self, string elementType, IWebElement element, IJsonToDataTableConverter jsonConverter, params DataColumnDefinition[] columnDefinitions)
        {
            return new InfragisticsControlToDataTableAdapter(self, elementType, jsonConverter)
                .GetDataTable(element, columnDefinitions);
        }

        public static List<RandomChartRootObject> GetRandomChartData(IWebDriver browser, IWebElement chart)
        {
            string json = GetDataSourceJSON(browser, chart);
            return SerializationUtils.ChartDeserializerVerifReport(json);
        }

        public static List<CountryChartRootObject> GetCountryChartData(IWebDriver browser, IWebElement chart)
        {
            string json = GetDataSourceJSON(browser, chart);
            return SerializationUtils.ChartDeserializerCountry(json);
        }

        // ME 6/28/17: Using JQuery in the console. The below is the same as pasting the following into the Console tab of DEV tools of a browser:
        // JSON.stringify($(<IDOfElement>).igDataChart('option', 'dataSource'));
        // Or you could not stringify it, and see the Object Array itself by using: $(<IDOfElement>).igDataChart('option', 'dataSource')
        // See an example of a igDataChart here:
        // https://www.igniteui.com/data-chart/bar-and-column-series or here: https://www.igniteui.com/data-chart/composite-chart
        // We are stringifying it so that we can convert the resulting JSON to C# classes. You can go to http://json2csharp.com/ to see this manually
        private static string GetDataSourceJSON(IWebDriver browser, IWebElement _element)
        {
            // NOTE: For some charts, the text "dataSource" needs to be replaced with "series"
            string jsText = string.Format("return JSON.stringify($(arguments[0]).{0}('option', 'dataSource'));", "igDataChart");
            return browser.ExecuteScript(jsText, _element) as string;
        }
        #endregion

        #region Legacy Methods

        // This is no longer used. See the explanation located within commented out WaitForDownload method in the Legacy Methods region of
        // the BrowserExtensions class file
        /// <summary>
        /// Opens a new tab, navigates to chrome://downloads/ then returns 
        /// a list of strings representing the remote server's file location where all of the current Browser instance downloads 
        /// are located. For example: C:\seleniumdownloads\MyExcelDocument.xlsx. Then adds an Input element 
        /// on this downloads page with an attribute of Type=Value, then uploads the most recently downloaded file from the current Browser 
        /// session, then returns the contents of this uploaded file as a base64 encoded string, then converts/decodes the Base64 string 
        /// into an 8-bit integer array. Once the string is converted, it transfers this encoding from server to local and saves 
        /// the encoding text as the same name of the originally downloaded from on the server. You can then open/edit/verify/save 
        /// this local file as you would the originally downloaded file, as it is a copy/transfer of the original. This will then close
        /// the new tab and return focus back to the original tab. 
        /// Future: Firefox implementation: https://code.premierinc.com/issues/browse/PQA-59 
        /// https://stackoverflow.com/questions/47068912/how-to-download-a-file-using-the-remote-selenium-webdriver
        /// </summary>
        /// <param name="self"></param>
        /// <param name="downloadedFileInstance">Whichever download you want to transfer</param>
        //private static void TransferDownloadedRemoteFileToLocal(this IWebDriver self, int downloadedFileInstance = 0)
        //{
        //    //var files = FileUtils.GetDownloadedFiles(self);

        //    // list all the completed remote files (waits for at least one). Will need to implement more code if the tester wants to verify 
        //    // multiple downloads in the same Browser session
        //    var files = FileUtils.GetDownloadedFiles(self);

        //    // string content = 
        //    FileUtils.GetFileContentFromRemoteThenAddThisContentToAFileOnLocal(self, files[downloadedFileInstance]);
        //}

        // This is no longer used. Explanation:
        // I once solved the problem of Selenium Grid downloads by implementing a workaround, as explained below in the Summary, however
        // that still did not work because even if we can transfer the file from server to local as explained below, it doesnt solve the problem 
        // where if the file is ALREADY downloaded on a node from a prior test, we need to delete that file first, else the download appends a
        // number onto the end of the file name, i.e. mydownload (1).xlsx. In the end, I settled on creating 1 shared network drive that serves
        // as a file sharing drive, then we can just set our default download directory to there and delete/edit/download/upload files from that
        // drive when executing on the grid. See the ClickAndWaitForDownload() method that is not commented out for tha implementation. Also see:
        // https://code.premierinc.com/docs/display/PQA/Grid+Configuration on how to setup a share for a network server if we ever lose this one
        ///// <summary>
        ///// Checks for the file in the default download directory once every second until the file exists and is not locked 
        ///// or the timeout is reached. IMPORTANT: On the Selenium Grid, this is currently only compatible in Chrome, 
        ///// and currently only available to verify 1 download in a given Browser session. If you need to verify multiple files 
        ///// in the same Browser session, I will need to add more logic to this code or you can shut down current Browser session or clear cache 
        ///// then download again. The logic I will need to add is adding the parameter downloadedFileInstance, see that parameter inside
        ///// TransferDownloadedRemoteFileToLocal().
        ///// Please add a test category filter to your build definition that excludes non-chrome tests so the build definition will not 
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
        ///// is verified to be on the local machine, this will then close the new tab and return focus back to the original tab
        ///// Future: Firefox implementation: https://code.premierinc.com/issues/browse/PQA-59 
        ///// https://stackoverflow.com/questions/47068912/how-to-download-a-file-using-the-remote-selenium-webdriver
        ///// </summary>
        ///// <param name="self">The web driver.</param>
        ///// <param name="fileNameAndExtension">The name of the file with its file extension that is being downloaded. This should be a file name only.  It should not include any path.</param>
        ///// <param name="fileWaitTimeout">The timeout for this operation to keep trying in milliseconds.  Default is 10000 (10 seconds).</param>
        ///// <exception cref="System.TimeoutException">Thrown if the file does not exist within the timeout specified.</exception>
        ///// <returns>The fully-qualified path to the file that was downloaded.  Typically this file resides in the user's default download directory.</returns>
        //public static string WaitForDownload(this IWebDriver self, string fileNameAndExtension, double fileWaitTimeout = 10000)
        //{
        //    var filepath = self.DownloadDirectory + fileNameAndExtension;

        //    if (self.IsRemote())
        //    {
        //        TransferDownloadedRemoteFileToLocal(self);
        //    }

        //    FileUtils.WaitForFile(filepath, fileWaitTimeout);
        //    return filepath;
        //}

        #endregion Legacy Methods







    }
}
