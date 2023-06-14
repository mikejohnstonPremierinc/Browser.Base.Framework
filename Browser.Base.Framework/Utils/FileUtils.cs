using AventStack.ExtentReports.Gherkin.Model;
using Grpc.Core;
using NUnit.Framework;
//using Microsoft.Office.Interop.Excel;
using OfficeOpenXml; // Add Epplus from nuget package for excel stuff
using OfficeOpenXml.Style;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Spire.Xls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Xml.Linq;
using ExcelHorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment;

namespace Browser.Core.Framework
{
    public static class FileUtils
    {
        #region Get Directories

        /// <summary>
        /// Returns the solution's directory. For example, C:\workspace_premier\SolutionName\
        /// </summary>
        /// <returns></returns>
        public static string GetSolutionDirectory()
        {
            var projectDirectory = GetProjectDirectory();

            var solutionDirectory = projectDirectory.Substring(0, projectDirectory.LastIndexOf("\\") + 1);

            return solutionDirectory;
        }

        /// <summary>
        /// Returns the project's directory. For example, C:\workspace_premier\SolutionName\ProjectName.UITest\
        /// </summary>
        /// <returns></returns>
        public static string GetProjectDirectory()
        {
            // So the above doesnt work at all anymore since we switched to .NET Core. The below method works for all
            // Get the project directory
            // AppContext.BaseDirectory locally returns C:\workspace_premier\SolutionName\ProjectName.UITest\bin\prod\netcoreapp3.0
            // We then substring it to get: C:\workspace_premier\SolutionName\ProjectName.UITest
            var projectDirectory = AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.LastIndexOf("\\bin"));

            return projectDirectory;
        }

        /// <summary>
        /// Gets the currently executing project name 
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentExecutingProjectName()
        {
            // The test instance name includes the full namespace, which includes the currently executing project name
            int endIndex = (TestContext.CurrentContext.Test.FullName.IndexOf("."));
            int length = endIndex - 0;
            string projectName = TestContext.CurrentContext.Test.FullName.Substring(0, length);
            return projectName;
        }

        /// <summary>
        /// Returns only the file name from a string that contains the (folder path + filename)
        /// </summary>
        /// <param name="folderPathPlusFileName">The full path (folder path + filename + extension). This can be the 
        /// relative path (A path within the current solution, i.e. \\TestOutput\\DataFiles\\MyFileName.txt,  
        /// or the current working directory \\ProjectName.UITest\\DataFiles_ConstantData\\MyFileName.txt) or the 
        /// absolute path (A path that starts with a drive letter, i.e. c:\\MyFolderName\\MyFileName.txt, or a path that 
        /// starts with a file share name, \\c3dilmssg01\\seleniumdownloads\\MyFileName.txt)</param>
        /// <returns></returns>
        public static string GetFileName_FromFullPath(string folderPathPlusFileName)
        {
            NetworkShareAccesser.Access();
            int pos = folderPathPlusFileName.LastIndexOf("\\") + 1;
            string fileName = folderPathPlusFileName.Substring(pos, folderPathPlusFileName.Length - pos);
            return fileName;
        }

        /// <summary>
        /// Returns only the folder path from a string that contains the (folder path + filename)
        /// </summary>
        /// <param name="folderPathPlusFileName">The full path (folder path + filename + extension). This can be the 
        /// relative path (A path within the current solution, i.e. \\TestOutput\\DataFiles\\MyFileName.txt,  
        /// or the current working directory \\ProjectName.UITest\\DataFiles_ConstantData\\MyFileName.txt) or the 
        /// absolute path (A path that starts with a drive letter, i.e. c:\\MyFolderName\\MyFileName.txt, or a path that 
        /// starts with a file share name, \\c3dilmssg01\\seleniumdownloads\\MyFileName.txt)</param>
        /// <returns></returns>
        public static string GetFolderPath_FromFullPath(string folderPathPlusFileName)
        {
            NetworkShareAccesser.Access();
            int pos = folderPathPlusFileName.LastIndexOf("\\") + 1;
            string folderPath = folderPathPlusFileName.Substring(0, pos - 0);
            return folderPath;
        }

        /// <summary>
        /// Returns a string that represents the path to ProjectName.UITest project's DataFiles_ConstantData folder. For 
        /// example, \\ProjectName.UITest\\DataFiles_ConstantData. If you do not have this folder, 
        /// you need to create one and store your 'constant' files within it. This folder should store only
        /// your files that will remain constant, meaning you should only add files to this folder that will stay 
        /// in this folder forever (Files or data that you can reference every time you execute a test, regardless of which PC 
        /// you execute a test from), because this folder gets Pushed to the online GitHub repository. This folder differs from 
        /// the DataFiles folder that is located within the solution's root directory, because that DataFiles folder stores files 
        /// that get generated 'during' the test execution, and as a result, will not be constant and will not be Pushed to a repo
        /// </summary>
        /// <returns></returns>
        public static string GetFolderPath_DataFilesConstantData()
        {
            return GetProjectDirectory() + "\\DataFiles_ConstantData\\";
        }

        /// <summary>
        /// Returns a string that represents the path to the TestOutput location's DataFiles folder. For example, 
        /// \\SolutionName\\TestOutput\\ProjectName\\DataFiles. This folder only contains files that get generated during 
        /// a test. The TestOutput folder does NOT get checked into your online GitHub repo, so these files are not constant. 
        /// This folder differs from the DataFiles_ConstantData folder that is located within the project's (not solution) root 
        /// directory, because that DataFiles_ConstantData folder stores Files/Data that you can reference every time you 
        /// execute a test, regardless of which PC you execute a test from, because that folder gets Pushed to the online GitHub 
        /// repository.
        /// </summary>
        /// <returns></returns>
        public static string GetFolderPath_DataFiles()
        {
            var testOutputDirectory = SeleniumCoreSettings.TestOutputLocation;

            var pathToDataFilesFolder = string.Format("{0}\\DataFiles\\{1}", testOutputDirectory, GetCurrentExecutingProjectName());

            ////// If we ever want to separate the results by project, environment and date...
            //// Build the folder structure within the TestOutput folder
            //var projectName = AppSettings.GetConfigVariableValue("projectname");
            //var environment = AppSettings.GetConfigVariableValue("environment");
            //var dateTime = DateTime.Now.ToString("yyyy-MM-dd");
            //var fullPath = string.Format("{0}{1}_{2}\\{3}\\DataFiles\\{4}", TestOutputDirectory,
            //    projectName, environment, dateTime, applicatioName);

            return pathToDataFilesFolder;
        }

        /// <summary>
        /// Returns a string that represents the path to the TestOutput location's DataFiles folder with a filename appended onto it. 
        /// The file name is based on the current executing test and is built as follows: 
        /// Custom prefix + Browser name + Emulation device if applicable + DateTimeStamp + Custom file extension 
        /// </summary>
        /// <param name="Browser">The driver</param>
        /// <param name="fileNamePrefix">You can specify a custom prefix to add onto the file name</param>
        /// <param name="fileExtension">(Optional). Default = .xlsx</param>
        /// <returns></returns>
        public static string GetFolderPathPlusFileName_DataFiles(IWebDriver Browser, string fileNamePrefix, string fileExtension = ".xlsx")
        {
            var fileName = CreateFileName_BasedOnCurrentTest(Browser, fileNamePrefix, fileExtension);

            var folderPathPlusFileName = string.Format("{0}\\{1}\\{2}",
                GetFolderPath_DataFiles(),
                TestContext.CurrentContext.Test.MethodName,
                fileName);

            return ResolveNamingCollisions(folderPathPlusFileName);
        }

        /// <summary>
        /// Returns a string that represents the path to the TestOutput location's DataFiles folder with a filename appended onto it. 
        /// The file name is based on the current executing test and is built as follows: 
        /// Custom prefix + DateTimeStamp + Custom file extension 
        /// This overload allows a tester to use this method if he/she does not open a Browser instance
        /// </summary>
        /// <param name="fileNamePrefix">You can specify a prefix to add onto the file name</param>
        /// <param name="fileExtension">(Optional). Default = .xlsx</param>
        public static string GetFolderPathPlusFileName_DataFiles(string fileNamePrefix, string fileExtension = ".xlsx")
        {
            var fileName = CreateFileName_BasedOnCurrentTest(fileNamePrefix, fileExtension);

            var fullPath = string.Format("{0}\\{1}", GetFolderPath_DataFiles(), fileName);

            return ResolveNamingCollisions(fullPath);
        }

        /// <summary>
        /// Builds a string that represents the absolute folder path where screenshots of failed tests will be stored 
        /// (\TestOutput\ScreenshotsOfFailedTests) then appends your custom file name. 
        /// </summary>
        /// <param name="fileName">The custom file name you want to give your screenshot. Be sure to include the extension .png at the 
        /// end of the file name</param>
        /// <returns></returns>
        public static string GetFolderPathPlusFileName_Screenshots_FailedTest(string fileName)
        {
            // Get the TestOutput directory, which is at the root of the solution
            var testOutputDirectory = SeleniumCoreSettings.TestOutputLocation;

            var absolutePath = string.Format("{0}ScreenshotsOfFailedTests\\{1}", testOutputDirectory, fileName);

            //// If we ever want to separate the results by project, environment and date...
            // Build the folder structure within the TestOutput folder
            //string projectName = AppSettings.GetConfigVariableValue("projectname");
            //var environment = AppSettings.GetConfigVariableValue("environment");
            //var dateTime = DateTime.Now.ToString("yyyy-MM-dd");
            //var fullPath = string.Format("{0}{1}_{2}\\{3}\\ScreenshotsOfFailedTests\\{4}", TestOutputDirectory, projectName, environment,
            //    dateTime, fileName);

            // Handle naming collisions
            int x = 2;
            while (File.Exists(absolutePath))
            {
                absolutePath = String.Concat(absolutePath, " (", x.ToString(), ")");
                x++;
            }

            // Files cant be saved with double quotes. Also, Extent Reports does not show screenshots maximized when using single quotes for some 
            // reason, so just replacing double quotes with a blank
            string absolutePathWithoutDoubleQuotes = absolutePath.Replace("\"", "");
            string absolutePathWithoutDoubleAndSingleQuotes = absolutePathWithoutDoubleQuotes.Replace("'", "");
            return absolutePathWithoutDoubleAndSingleQuotes;
        }


        /// <summary>
        /// Builds a string that represents the absolute folder path + file name where on-demand screenshots will be stored.
        /// The folder path is \TestOutput\ScreenshotsCustom\ProjectName\TestMethodName\
        /// The filename is built as follows: Custom prefix + Browser name + Emulation device if applicable + DateTimeStamp + .PNG
        /// </summary>
        /// <param name="Browser"></param>
        /// <param name="fileNamePrefix">You can specify a prefix to add onto the file name</param>
        /// <returns></returns>
        public static string GetFolderPathPlusFileName_Screenshots_DuringTest(IWebDriver Browser, string fileNamePrefix)
        {
            int startIndex = 0;
            int endIndex = 0;
            int length = 0;

            // Get the test name
            var test = TestContext.CurrentContext.Test;
            string testName = test.MethodName;

            // Get the mobile emulation device, if applicable
            string emulationDevice = null;
            if (Browser.MobileEnabled())
            {
                // Get the emulation device name
                int firstIndexOfQuote = test.FullName.IndexOf('\"');
                int secondIndexOfQuote = test.FullName.IndexOf('\"', firstIndexOfQuote + 1);
                int thirdIndexOfQuote = test.FullName.IndexOf('\"', secondIndexOfQuote + 1);
                int fourthIndexOfQuote = test.FullName.IndexOf('\"', thirdIndexOfQuote + 1);
                startIndex = thirdIndexOfQuote + 1;
                endIndex = fourthIndexOfQuote;
                length = endIndex - startIndex;
                emulationDevice = test.FullName.Substring(startIndex, length);

            }

            // Not using testName or className (variables above) for the filename. For the test name, I instead made a 
            // folder with the test name (in fullPathWithFileName variable) to store the screenshots instead
            var fileName = string.Concat(fileNamePrefix, "_" +
                Browser.GetCapabilities().GetCapability("browserName").ToString() + "_" + emulationDevice, "_(",
                DateTime.Now.ToString("yyyy-MM-dd_HH.mm.ss.ff)",
                CultureInfo.InvariantCulture), ".png").Replace(" ", string.Empty);

            // Build and then get the full file path
            // Get the TestOutput directory, which is at the root of the solution
            var testOutputDirectory = SeleniumCoreSettings.TestOutputLocation;

            var fullPath = string.Format("{0}\\ScreenshotsCustom\\{1}\\TestName_{2}\\{3}", testOutputDirectory,
                GetCurrentExecutingProjectName(), testName, fileName);

            //// If we ever want to separate the results by project, environment and date...
            //// Build the folder structure within the TestOutput folder
            //var projectName = AppSettings.GetConfigVariableValue("projectname");
            //var environment = AppSettings.GetConfigVariableValue("environment");
            //var dateTime = DateTime.Now.ToString("yyyy-MM-dd");
            //var fullPath = string.Format("{0}{1}_{2}\\{3}\\ScreenshotsCustom\\{4}\\TestName_{5}\\{6}", TestOutputDirectory,
            //    projectName, environment, dateTime, applicatioName, testName, fileName);

            // Handle naming collisions
            int x = 2;
            while (File.Exists(fullPath))
            {
                fullPath = String.Concat(fullPath, " (", x.ToString(), ")");
                x++;
            }
            return fullPath;
        }


        /// <summary>
        /// If we are configuring a new build on new build servers, and we want to check if the new servers are building
        /// the directory in the place we want it to (i.e. lms-webautomation\TestOutput\LMS_Production\2019-11-26), 
        /// then we can call this method, which will output a custom error message that shows where the directory
        /// is built when executing on the build
        /// </summary>
        /// <param name="Browser"></param>
        public static void VerifyBuildDataFileDirectory(IWebDriver Browser)
        {
            var fullPath = FileUtils.GetFolderPathPlusFileName_Screenshots_FailedTest("CheckToSeeIfThisFileExists.png");
            var tsc = Browser as ITakesScreenshot;
            FileUtils.CreateDirectoryIfNotExists(fullPath);
            var ss = tsc.GetScreenshot();
            ss.SaveAsFile(fullPath, ScreenshotImageFormat.Png);
            Assert.Fail("After this test is executed, check to see if this file location exists: " + fullPath);
        }

        /// <summary>
        /// If locking fails returns true; otherwise, returns false.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static bool FileLocked(FileInfo file)
        {
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
            }
            catch (IOException)
            {
                // The file is unavailable because it is: still being written to or being processed by another thread
                // or does not exist (has already been processed)
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Dispose();
            }

            //file is not locked
            return false;
        }

        #endregion Get Directories

        #region Create Directories

        /// <summary>
        /// Builds a file name based on the current test executions browser, date and time
        /// The filename is built as follows: 
        /// Custom prefix + Browser name + Emulation device if applicable + DateTimeStamp + Custom file extension
        /// </summary>
        /// <param name="Browser">The driver</param>
        /// <param name="fileNamePrefix">You can specify a custom prefix to add onto the file name</param>
        /// <param name="fileExtension">Thefile extension</param>
        /// <returns></returns>
        public static string CreateFileName_BasedOnCurrentTest(IWebDriver Browser, string fileNamePrefix, string fileExtension)
        {
            int startIndex = 0;
            int endIndex = 0;
            var test = TestContext.CurrentContext.Test;

            // Get the class name. It returns the namespace name, so we have to substring to get the class name
            startIndex = (test.ClassName.LastIndexOf('.') + 1);
            string testClassName = test.ClassName.Substring(startIndex);

            // Get the test method name
            string testName = test.MethodName;

            // Get the mobile emulation device, if applicable
            string emulationDevice = null;
            if (Browser.MobileEnabled())
            {
                // Get the emulation device name
                int firstIndexOfQuote = test.FullName.IndexOf('\"');
                int secondIndexOfQuote = test.FullName.IndexOf('\"', firstIndexOfQuote + 1);
                int thirdIndexOfQuote = test.FullName.IndexOf('\"', secondIndexOfQuote + 1);
                int fourthIndexOfQuote = test.FullName.IndexOf('\"', thirdIndexOfQuote + 1);
                startIndex = thirdIndexOfQuote + 1;
                endIndex = fourthIndexOfQuote;
                int length = endIndex - startIndex;
                emulationDevice = test.FullName.Substring(startIndex, length);
            }

            // Not using testName or className for now. For the test name, I made a folder with the test name to store the file instead
            // The below filename will be formatted as (TestName_BrowserType_EmulationDevice_DateTime)
            var filename = string.Concat(fileNamePrefix, "_" + Browser.GetCapabilities().GetCapability("browserName").ToString() + "_" +
                emulationDevice, "_(", DateTime.Now.ToString("yyyy-MM-dd_HH.mm.ss.ff)", CultureInfo.InvariantCulture), fileExtension)
                .Replace(" ", string.Empty);

            return filename;
        }

        /// <summary>
        /// Builds a file name based on the current test executions date and time
        /// The filename is built as follows: Custom prefix + DateTimeStamp + Custom file extension 
        /// This overload allows a tester to use this method if he/she does not open a Browser instance
        /// </summary>
        /// <param name="fileNamePrefix"></param>
        /// <param name="fileExtension">Thefile extension</param>
        /// <returns></returns>
        public static string CreateFileName_BasedOnCurrentTest(string fileNamePrefix, string fileExtension)
        {
            // Not using testName or className for now. For the test name, I made a folder with the test name to store the file instead
            // The below filename will be formatted as (TestName_BrowserType_EmulationDevice_DateTime)
            var filename = string.Concat(fileNamePrefix, "_" + "_(", DateTime.Now.ToString("yyyy-MM-dd_HH.mm.ss.ff)",
                CultureInfo.InvariantCulture), fileExtension).Replace(" ", string.Empty);

            return filename;
        }

        /// <summary>
        /// Creates a file with a tester-specified file name inside a tester-specified directory. 
        /// Creates the directory if it doesnt exist. If you are executing a test on the Grid, 
        /// you can place files within \\YourGridsHubServersName\seleniumdownloads. You can add a
        /// condition to your test code to check whether the current test instance is local or 
        /// remote by calling the RemoteExecution property
        /// </summary>
        /// <param name="folderLocation"></param>
        /// <param name="fileNameAndExtension"></param>
        /// <returns></returns>
        public static string CreateFile(string folderLocation, string fileNameAndExtension)
        {
            NetworkShareAccesser.Access();

            // Create the above directory folder if it doesnt exist
            if (!Directory.Exists(folderLocation))
            {
                Directory.CreateDirectory(folderLocation);
            }

            string folderPathPlusFileName = folderLocation + string.Format(@"\\{0}", fileNameAndExtension); //"\\\\Results.csv";

            // Create the above file if it doesnt exist
            if (!File.Exists(folderPathPlusFileName))
            {
                File.Create(folderPathPlusFileName).Close();
            }

            WaitForFile(folderPathPlusFileName);

            return folderPathPlusFileName;
        }

        /// <summary>
        /// Creates the directory if it doesnt exist. This can be either a folder path, or a folder path plus file name
        /// </summary>
        /// <param name="path"></param>
        public static void CreateDirectoryIfNotExists(string path)
        {
            NetworkShareAccesser.Access();

            var info = new DirectoryInfo(Path.GetDirectoryName(path));

            if (!info.Exists)
            {
                info.Create();
            }
        }

        #endregion Create Directories

        #region Saving Files

        /// <summary>
        /// Writes text to a text file. Saves the file to the root of the solution within: \TestOutput\DataFiles\ProjectName\TestMethodName
        /// </summary>
        /// <param name="Browser"></param>
        /// <param name="fileNamePrefix">You can specify a prefix to add onto the file name</param>
        /// <param name="textToAdd">The text to add to your file</param>
        public static void Notepad_StoreResults(IWebDriver Browser, string fileNamePrefix, string textToAdd)
        {
            // Build directory with file name then create the file
            string folderPathPlusFileName = GetFolderPathPlusFileName_DataFiles(Browser, fileNamePrefix, ".txt");
            FileUtils.CreateDirectoryIfNotExists(folderPathPlusFileName);

            System.IO.File.WriteAllText(string.Format(folderPathPlusFileName), textToAdd);
        }

        /// <summary>
        /// Writes text to a text file. Saves the file to the root of the solution within: \TestOutput\DataFiles\ProjectName\TestMethodName
        /// This overload allows a tester to use this method if he/she does not open a Browser instance
        /// </summary>
        /// <param name="fileNamePrefix">You can specify a prefix to add onto the file name</param>
        /// <param name="textToAdd">The text to add to your file</param>
        public static void Notepad_StoreResults(string fileNamePrefix, string textToAdd)
        {
            // Build directory with file name then create the file
            string folderPathPlusFileName = GetFolderPathPlusFileName_DataFiles(fileNamePrefix, ".txt");
            FileUtils.CreateDirectoryIfNotExists(folderPathPlusFileName);

            System.IO.File.WriteAllText(string.Format(folderPathPlusFileName), textToAdd);
        }


        /// <summary>
        /// Writes two list of strings (per worksheet) that have a parent>child relationship. This will print the lists in such a way 
        /// that the first list represents the parent items, and then second list represents the child items. 1 parent will be 
        /// printed on the first column, and the second column will group each children item in their respective parent. 
        /// Saves the file to the root of the solution within: \TestOutput\DataFiles\ProjectName\TestMethodName
        /// </summary>
        /// <param name="Browser"></param>
        /// <param name="fileNamePrefix">You can specify a prefix to add onto the file name</param>
        /// <param name="myWorkBook"><see cref="MyWorkBook"/></param>
        public static void Excel_StoreResults(IWebDriver Browser, string fileNamePrefix, MyWorkBook myWorkBook)
        {
            // Build directory with file name then create the file
            string folderPathPlusFileName = GetFolderPathPlusFileName_DataFiles(Browser, fileNamePrefix);
            FileUtils.CreateDirectoryIfNotExists(folderPathPlusFileName);

            ExcelWorksheet Sheet = null;

            var fi = new FileInfo(folderPathPlusFileName);
            using (var Excel = new ExcelPackage(fi))
            {
                // Loop through each sheet
                for (int i = 0; i < myWorkBook.Worksheets.Count; i++)
                {
                    // If first sheet, add it. If second or more sheet, add it then move it to the next position in order
                    if (i == 0)
                    {
                        Sheet = Excel.Workbook.Worksheets.Add(myWorkBook.Worksheets[i].Title);
                    }
                    else
                    {
                        Sheet = Excel.Workbook.Worksheets.Add(myWorkBook.Worksheets[i].Title);
                        Excel.Workbook.Worksheets.MoveAfter(Sheet.Index, i);
                    }

                    // Loop through the parent item
                    int rowToPrintParentString = 1;
                    foreach (var parentString in myWorkBook.Worksheets[i].ParentStrings)
                    {
                        Sheet.Cells[rowToPrintParentString, 1].Value = parentString.Title;

                        // Loop through the child strings
                        int rowToPrintChildStrings = rowToPrintParentString + 1;
                        foreach (var childString in parentString.Children)
                        {
                            Sheet.Cells[rowToPrintChildStrings, 2].Value = childString.Title;
                            rowToPrintChildStrings = rowToPrintChildStrings + 1;
                        }

                        rowToPrintParentString = parentString.Children.Count + rowToPrintParentString + 1;
                    }

                    // AutoFit columns 
                    // If the sheet is null, do nothing. This happened when testing Bins. One of the sites did not have bins
                    if (Sheet != null)
                    {
                        Sheet.Cells[Sheet.Dimension.Address].AutoFitColumns();
                    }
                    else
                    {
                        throw new Exception(string.Format("Worksheet #{0} is null. Please make sure all your worksheets have data", i.ToString()));
                    }
                }

                // If the workbook is null, tell the user.
                if (Sheet == null)
                {
                    throw new Exception(string.Format("None of your worksheets had any data. Please make sure all your worksheets have data"));
                }

                Excel.Save();
            }
        }

        /// <summary>
        /// Writes two list of strings (per worksheet) to an excel file for comparison. 
        /// Saves the file to the root of the solution within: \TestOutput\DataFiles\ProjectName\TestMethodName
        /// </summary>
        /// <param name="Browser"></param>
        /// <param name="fileNamePrefix">You can specify a prefix to add onto the file name</param>
        /// <param name="worksheetNames">The worksheet titles</param>
        /// <param name="firstListName">The name of the first list. This will be placed in the first column of the excel file</param>
        /// <param name="secondListName">The name of the second list. This will be placed in the third column of the excel file</param>
        /// <param name="listsPerWorksheet">The lists being printed to each worksheet</param>
        public static void Excel_StoreResults(IWebDriver Browser, string fileNamePrefix, List<string> worksheetNames,
            string firstListName, string secondListName, List<(List<string> list1String, List<string> list2Strings)> listsPerWorksheet)
        {
            // Build directory with file name then create the file
            string folderPathPlusFileName = GetFolderPathPlusFileName_DataFiles(Browser, fileNamePrefix);
            FileUtils.CreateDirectoryIfNotExists(folderPathPlusFileName);

            ExcelWorksheet Sheet = null;

            var fi = new FileInfo(folderPathPlusFileName);
            using (var Excel = new ExcelPackage(fi))
            {
                // Loop through each sheet
                for (int i = 0; i < worksheetNames.Count; i++)
                {
                    // If first sheet, add it. If second or more sheet, add it then move it to the next position in order
                    if (i == 0)
                    {
                        Sheet = Excel.Workbook.Worksheets.Add(worksheetNames[i]);
                    }
                    else
                    {
                        Sheet = Excel.Workbook.Worksheets.Add(worksheetNames[i]);
                        Excel.Workbook.Worksheets.MoveAfter(Sheet.Index, i);
                    }

                    // Add table headers going cell by cell.
                    Sheet.Cells[1, 1].Value = firstListName;
                    Sheet.Cells[1, 1].Style.Font.Bold = true;
                    Sheet.Cells[1, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    Sheet.Cells[1, 1].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    Sheet.Cells[1, 2].Value = secondListName;
                    Sheet.Cells[1, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    Sheet.Cells[1, 2].Style.Font.Bold = true;
                    Sheet.Cells[1, 2].Style.Fill.BackgroundColor.SetColor(Color.LightGray);

                    int countOfStrings = 0;

                    // We have to account for when one list might have more string instances than the other. So get a count of the greatest 
                    // amount of string instances in either list, then loop that many times
                    countOfStrings = listsPerWorksheet[i].list1String.Count >=
                        listsPerWorksheet[i].list2Strings.Count ?
                        listsPerWorksheet[i].list1String.Count :
                        listsPerWorksheet[i].list2Strings.Count;

                    // Loop through each row
                    for (int j = 0; j < countOfStrings; j++)
                    {
                        // If list1 has more strings than list2, or vice versa, then we have to handle that and make the string empty. Else the code will
                        // throw an error because the list at the current iteration will be null and not have a string
                        string list1CurrentInstance = listsPerWorksheet[i].list1String.Count > j ? listsPerWorksheet[i].list1String[j]
                            : string.Empty;
                        string list2CurrentInstance = listsPerWorksheet[i].list2Strings.Count > j ? listsPerWorksheet[i].list2Strings[j]
                            : string.Empty;

                        // Add non-header cell contents for each row
                        Sheet.Cells[j + 2, 1].Value = list1CurrentInstance;
                        Sheet.Cells[j + 2, 2].Value = list2CurrentInstance;
                    }

                    // AutoFit columns 
                    Sheet.Cells[Sheet.Dimension.Address].AutoFitColumns();

                    // Center align the header cells
                    Sheet.Cells["A1:B1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }

                Excel.Save();
            }
        }

        /// <summary>
        /// Writes 1 list of strings (per worksheet) to an excel file.
        /// Saves the file to the root of the solution within: \TestOutput\DataFiles\ProjectName\TestMethodName
        /// </summary>
        /// <param name="Browser"></param>
        /// <param name="fileNamePrefix">You can specify a prefix to add onto the file name</param>
        /// <param name="worksheetNames">The worksheet titles</param>
        /// <param name="listName">The name of the lists</param>
        /// <param name="listOfStringsPerWorksheet">The lists being printed to each worksheet</param>
        public static void Excel_StoreResults(IWebDriver Browser, string fileNamePrefix, List<string> worksheetNames,
            string listName, List<List<string>> listOfStringsPerWorksheet)
        {
            // Build directory with file name then create the file
            string folderPathPlusFileName = GetFolderPathPlusFileName_DataFiles(Browser, fileNamePrefix);
            FileUtils.CreateDirectoryIfNotExists(folderPathPlusFileName);

            ExcelWorksheet Sheet = null;

            // Excel has a 31 character limit forThe worksheet names, so shorten the names, and also handle naming collisions
            worksheetNames = Excel_GetWorksheetNames(worksheetNames);

            var fi = new FileInfo(folderPathPlusFileName);
            using (var Excel = new ExcelPackage(fi))
            {
                // Loop through each sheet
                for (int i = 0; i < worksheetNames.Count; i++)
                {
                    // If first sheet, add it. If second or more sheet, add it then move it to the next position in order
                    if (i == 0)
                    {
                        Sheet = Excel.Workbook.Worksheets.Add(worksheetNames[i]);
                    }
                    else
                    {
                        Sheet = Excel.Workbook.Worksheets.Add(worksheetNames[i]);
                        Excel.Workbook.Worksheets.MoveAfter(Sheet.Index, i);
                    }

                    // Add table headers going cell by cell.
                    Sheet.Cells[1, 1].Value = listName;
                    Sheet.Cells[1, 1].Style.Font.Bold = true;
                    Sheet.Cells[1, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    Sheet.Cells[1, 1].Style.Fill.BackgroundColor.SetColor(Color.LightGray);

                    // Loop through each row
                    for (int j = 0; j < listOfStringsPerWorksheet[i].Count; j++)
                    {
                        // Add non-header the cell contents for each row
                        Sheet.Cells[j + 2, 1].Value = listOfStringsPerWorksheet[i][j];
                    }

                    // AutoFit columns 
                    Sheet.Cells[Sheet.Dimension.Address].AutoFitColumns();

                    // Center align the header cells
                    Sheet.Cells["A1:B1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }

                Excel.Save();
            }
        }

        /// <summary>
        /// Writes two list of strings to a csv file for comparison. 
        /// Saves the file to the root of the solution within: \TestOutput\DataFiles\ProjectName\TestMethodName
        /// </summary>
        /// <param name="Browser"></param>
        /// <param name="fileNamePrefix">You can specify a prefix to add onto the file name</param>
        /// <param name="firstListName">The name of the first list. This will be placed in the first column of the csv file</param>
        /// <param name="list1">The first list of strings to print</param>
        /// <param name="secondListName">The name of the second list. This will be placed in the third column of the csv file</param>
        /// <param name="list2">The second list of strings to print</param>
        public static void CSV_StoreResults(IWebDriver Browser, string fileNamePrefix, string firstListName,
            List<string> list1, string secondListName, List<string> list2)
        {
            // Build directory with file name then create the file
            string folderPathPlusFileName = GetFolderPathPlusFileName_DataFiles(Browser, fileNamePrefix);
            FileUtils.CreateDirectoryIfNotExists(folderPathPlusFileName);

            // Write the column headers to the file
            using (StreamWriter sw = File.AppendText(folderPathPlusFileName))
            {
                sw.WriteLine(firstListName + "," + secondListName);
            }

            // We have to account for when one list might have more string instances than the other. So get a count of the greatest amount of 
            // string instances in either list, then loop that many times
            var count = list1.Count >= list2.Count ? list1.Count : list2.Count;

            for (int i = 0; i < count; i++)
            {
                // If list1 has more strings than list2, or vice versa, then we have to handle that and make the string empty. Else the code will
                // throw an error because the list at the current iteration will be null and not have a string
                string list1CurrentInstance = list1.Count > i ? list1[i].Replace(",", "") : string.Empty;
                string list2CurrentInstance = list2.Count > i ? list2[i].Replace(",", "") : string.Empty;

                // Write the current instance list values
                using (StreamWriter sw = File.AppendText(folderPathPlusFileName))
                {
                    sw.WriteLine(list1CurrentInstance + "," + list2CurrentInstance);
                }
            }
        }

        /// Writes two list of strings to a csv file for comparison. 
        /// Saves the file to the root of the solution within: \TestOutput\DataFiles\ProjectName\TestMethodName
        /// </summary>
        /// <param name="Browser"></param>
        /// <param name="fileNamePrefix">You can specify a prefix to add onto the file name</param>
        /// <param name="firstListName">The name of the first list. This will be placed in the first column of the csv file</param>
        /// <param name="list1">The first list of strings to print</param>
        public static void CSV_StoreResults(IWebDriver Browser, string fileNamePrefix, string firstListName, List<string> list1)
        {
            // Build directory with file name then create the file
            string folderPathPlusFileName = GetFolderPathPlusFileName_DataFiles(Browser, fileNamePrefix);
            FileUtils.CreateDirectoryIfNotExists(folderPathPlusFileName);

            // Write the column headers to the file
            using (StreamWriter sw = File.AppendText(folderPathPlusFileName))
            {
                sw.WriteLine(firstListName);
            }

            var count = list1.Count;

            for (int i = 0; i < count; i++)
            {
                // If list1 has more strings than list2, or vice versa, then we have to handle that and make the string empty. Else the code will
                // throw an error because the list at the current iteration will be null and not have a string
                //string list1CurrentInstance = list1.Count > i ? list1[i].Replace(",", "") : string.Empty;
                //string list2CurrentInstance = list2.Count > i ? list2[i].Replace(",", "") : string.Empty;

                // Write the current instance list values
                using (StreamWriter sw = File.AppendText(folderPathPlusFileName))
                {
                    sw.WriteLine(list1[i]);
                }
            }
        }

        #endregion Saving Files

        #region Renaming and Deleting Files

        /// <summary>
        /// Deletes a file.
        /// </summary>
        /// <param name="fileName">The file name + extension</param>
        /// <param name="folderPath">(Optional). If you dont specify this parameter, it will default to the path of your ProjectName.UITest
        /// DataFiles_ConstantData folder. For example, <see cref="GetFolderPath_DataFilesConstantData"/>. If you do specify
        /// a value for this parameter, it must be the following format, including a slash at the end C:\\MyFolderName\\</param>
        public static void DeleteFile(string fileName, string folderPath = null)
        {
            NetworkShareAccesser.Access();

            folderPath = string.IsNullOrEmpty(folderPath) ? GetFolderPath_DataFilesConstantData() : folderPath;
            string folderPathPlusFileName = folderPath + fileName;

            if (File.Exists(folderPathPlusFileName))
            {
                File.Delete(folderPathPlusFileName);
            }
        }

        /// <summary>
        /// Renames a file
        /// </summary>
        /// <param name="existingName">The existing file name + extension</param>
        /// <param name="newName">The new file name + extension</param>
        /// <param name="folderPath">(Optional). If you dont specify this parameter, it will default to the path of your ProjectName.UITest
        /// DataFiles_ConstantData folder. For example, <see cref="GetFolderPath_DataFilesConstantData"/>. If you do specify
        /// a value for this parameter, it must be the following format, including a slash at the end C:\\MyFolderName\\</param>
        public static void RenameFile(string existingName, string newName, string folderPath = null)
        {
            NetworkShareAccesser.Access();

            folderPath = string.IsNullOrEmpty(folderPath) ? GetFolderPath_DataFilesConstantData() : folderPath;

            System.IO.File.Move(folderPath + existingName, folderPath + newName);
        }

        /// <summary>
        /// If a file already exists with the same name in a certain folder, you can not save a file with this name, 
        /// so this will append a number onto the end of your file
        /// </summary>
        /// <param name="folderPathPlusFileName">The folder path + file name + extension</param>
        public static string ResolveNamingCollisions(string folderPathPlusFileName)
        {
            NetworkShareAccesser.Access();

            var folderPath = GetFolderPath_FromFullPath(folderPathPlusFileName);
            var fileName = System.IO.Path.GetFileNameWithoutExtension(folderPathPlusFileName);
            var extension = System.IO.Path.GetExtension(folderPathPlusFileName);
            var currentCount = 1;

            while (File.Exists(folderPathPlusFileName))
            {
                folderPathPlusFileName = string.Format("{0}{1} ({2}){3}", folderPath, fileName, ++currentCount, extension);
            }

            return folderPathPlusFileName;
        }

        #endregion Renaming and Deleting Files

        #region Downloading

        /// <summary>
        /// Checks for the file once every second until the file exists and is not locked or the timeout is reached.
        /// If you are executing locally, this will check for the file in c:\Seleniumdownloads. If you are executing 
        /// a test on the Grid, this will check for the file in \\YourGridsHubServersName\seleniumdownloads
        /// </summary>
        /// <param name="folderPathPlusFileName">The full path (folder path + filename + extension). This can be the 
        /// relative path (A path within the current solution, i.e. \\TestOutput\\DataFiles\\MyFileName.txt,  
        /// or the current working directory \\ProjectName.UITest\\DataFiles_ConstantData\\MyFileName.txt) or the 
        /// absolute path (A path that starts with a drive letter, i.e. c:\\MyFolderName\\MyFileName.txt, or a path that 
        /// starts with a file share name, \\c3dilmssg01\\seleniumdownloads\\MyFileName.txt)</param>
        /// <param name="fileWaitTimeout">The timeout for this operation to keep trying in milliseconds. Default is 30000 (30 seconds).</param>
        /// <exception cref="System.TimeoutException">Thrown if the file does not exist within the timeout specified.</exception>
        public static void WaitForFile(string folderPathPlusFileName, double fileWaitTimeout = 30000)
        {
            NetworkShareAccesser.Access();
            var sw = System.Diagnostics.Stopwatch.StartNew();
            var fi = new FileInfo(folderPathPlusFileName);

            while (!File.Exists(folderPathPlusFileName) || FileLocked(fi))
            {
                if (sw.ElapsedMilliseconds > fileWaitTimeout)
                {
                    var msg = "The file \"{0}\" was not found within the timeout period of {1} milliseconds.";
                    if (!File.Exists(folderPathPlusFileName))
                        msg += "  The file does not exist.";
                    else if (FileLocked(fi))
                        msg += "  The file is locked by another user or process.";
                    throw new TimeoutException(string.Format(msg, folderPathPlusFileName, fileWaitTimeout));
                }
                Thread.Sleep(1000);
            }
        }

        #endregion Downloading

        #region uploading

        /// <summary>
        /// Uploads a file through the windows upload file window using Autoit. For an explanation, 
        /// see: https://code.premierinc.com/docs/display/PGHLMSDOCS/Uploading+Files. It is not recommended to use this method 
        /// for your uploading needs because this only works in Chrome locally. For full compatibility of uploading files,
        /// <see cref="UploadFileUsingSendKeys(IWebDriver, IWebElement, string, Page, By, TimeSpan)(IWebElement, string, string, string)"/>
        /// </summary>
        /// <param name="browseBtnElem">The Browse button element</param>
        /// <param name="scriptFileLocation">The AutoIt script file. Our upload script is called FileUpload.au3 and should be located here: 
        /// C:\SeleniumAutoIt</param>
        /// <param name="browserName">Either "chrome", "firefox" or "internetexplorer"</param>
        /// <param name="fileToUploadLocation">The file location and file name. i.e. C:\SeleniumAutoIt\test.txt.txt</param>
        public static void UploadFileWithAutoIt(IWebElement browseBtnElem, string scriptFileLocation, string browserName,
            string fileToUploadLocation)
        {
            NetworkShareAccesser.Access();
            browseBtnElem.Click();
            AutoItUtils.RunAutoItScript(scriptFileLocation, browserName, fileToUploadLocation);
            Thread.Sleep(0300);
        }

        /// <summary>
        /// Uploads a file by directly sending the file path to the JQuery File Upload browse button. To 
        /// determine if your application is compatible (it uses the JQuery file upload system), there 
        /// should be an input tag in your HTML representing the browse/fileupload/etc button (Note
        /// that this button may be hidden under a browse button that doesnt meet these requirements).
        /// This Input element should have an attribute titled "type", and it's attribute value should be
        /// "file".
        /// </summary> 
        /// <param name="Browser">The driver instance</param>
        /// <param name="browseBtnElem">The browse button of IWebElement type, which should be the Input tag with an 
        /// attribute of "type", with an attribute value of "file". See summary above for further explanation </param>
        /// <param name="folderPathPlusFileName">The path of the file with the file name appended onto it. 
        /// If your test first downloads a file, then the test uploads this same file, you can simply download the file using 
        /// ElementName.ClickAndWaitForDownload, which will return the full file path string of the download, then you can send 
        /// that string to this parameter, and it will work for all execution types (local, remote, pipeline). If instead your 
        /// test is uploading a file that was not downloaded within the same test (a file that you manually created or manually 
        /// downloaded), then the best thing to do is add this file within your project's .UITest solution folders then Push the 
        /// file to your GitHub Repo, so the file will always be there on all your team's file systems as well as the Pipeline 
        /// agents file system, and it will work for all execution types (local, remote, pipeline). Similarly, when you use the 
        /// methods <see cref="Notepad_StoreResults(string, string)"/> or <see cref="Excel_StoreResults(IWebDriver, string, MyWorkBook)"/>, 
        /// then those methods will store results in your project's current directory 
        /// (Saves the file to \TestOutput\DataFiles\*AppName*\*FileName*).
        /// The following text explains how this framework finds and uploads files... 
        /// If you are executing a test locally (Not on the Grid) within your local Visual Studio instance, 
        /// for the file to be successfully uploaded, the file/filePath must be located on your local file system. If you are executing 
        /// a test remotely (on the Grid) within your local Visual Studio instance, for file to be successfully uploaded, the 
        /// file/filePath can be located at any of the following 1) Your local file system 2) The Selenium Grid hub File Share 
        /// https://code.premierinc.com/docs/display/PQA/File+Share 3) "ALL" Selenium Grid Nodes file systems. If you are executing 
        /// a test remotely (on the Grid) within the Pipeline agents Visual Studio instance, for file to be successfully uploaded, the 
        /// file/filePath can be located at any of the following 1) The pipeline agents local file system 2) The Selenium Grid hub 
        /// File Share https://code.premierinc.com/docs/display/PQA/File+Share 3) "ALL" Selenium Grid Nodes file systems. 
        /// When a test is executed against the Grid in any of the above situations, there is logic in this code that takes the 
        /// value of this parameter, then FIRST checks if the file exists within that value/filePath on the  Visual Studio instances 
        /// local file system, and if so, it transfers that file to the Node via JSon Wire Protocol then uploads the file. If the 
        /// code does not find the file within the Visual Studio instances local file system, it THEN checks if the file exists within 
        /// that value/filePath on the Nodes file system, and if so, it uploads the file. If the file does not exist on the Node at 
        /// this point, the test fails and says the file does not exist. See the "allowsDetection" line of code
        /// inside <see cref="BrowserTest.CreateRemoteBrowser"/> for the code that tells Selenium to upload the local version of 
        /// the file first even if the test is executing on a remote machine and even if the file is also on that remote machine. 
        /// Further reading: https://stackoverflow.com/questions/27331884/selenium-chromedriver-sendkeys-breaks-jquery-file-upload-plugin 
        /// Fireball: For Fireball apps, you must do two things. 1. Store the file path inside of a variable THEN pass that variable 
        /// to this argument. 2. Do not use String.Format or the @ symbol when forming your file path string. It must be a string 
        /// inside two double quotes and the back slashes should be escape sequenced. If you dont do these things, for some reason, it 
        /// throws an Invalid Argument. Note that if you sent a file path that did not contain the correct filename 
        /// or just an incorrect folder location, you would also get Invalid Argument</param>
        /// <param name="pageToWaitFor">(Optional). There are multiple options to wait after uploading. You can send a 
        /// Page object and wait for the page to load, or you can send an IWebElement and wait for it to become 
        /// visible, or wait a static amount of time</param>
        /// <param name="elemToWaitFor">(Optional). There are multiple options to wait after uploading. You can send a 
        /// Page object and wait for the page to load, or you can send an IWebElement and wait for it to become 
        /// visible, or wait a static amount of time</param>
        /// <param name="waitTime">(Optional). The amount of time the code will wait for the page Default = 30 seconds</param>
        public static void UploadFileUsingSendKeys(IWebDriver Browser, IWebElement browseBtnElem, string folderPathPlusFileName,
            Page pageToWaitFor = null, By elemToWaitFor = null, TimeSpan waitTime = default(TimeSpan))
        {
            NetworkShareAccesser.Access();

            // If this button is not visible, then we need to remove the style attribute, as the style attribute most likely has a value
            // of "display:none", which controls whether it is displayed or not
            Browser.ExecuteScript("arguments[0].removeAttribute(\"style\");", browseBtnElem);
            // We also have to remove the multiple attribute, if it has one. If you find this doesnt work, then tell your
            // developer to change this element from multiple to single
            Browser.ExecuteScript("arguments[0].removeAttribute(\"multiple\");", browseBtnElem);
            Thread.Sleep(300);

            waitTime = waitTime == default(TimeSpan) ? TimeSpan.FromSeconds(30) : waitTime;

            /// See the CodePagesEncodingProvider.Instance.GetEncoding(437) line of code inside 
            /// <see cref="BrowserTest.CreateRemoteBrowser"/> method for info on how this was once broken 
            /// then fixed on the Grid
            browseBtnElem.SendKeys(folderPathPlusFileName);

            if (pageToWaitFor != null)
            {
                pageToWaitFor.WaitForInitialize();
            }

            if (elemToWaitFor != null)
            {
                Browser.WaitForElement(elemToWaitFor, waitTime, ElementCriteria.IsVisible);
            }
        }

        #endregion uploading

        #region Excel


        /// <summary>
        /// Using Microsoft.Office.Interop to convert XLS to XLSX format, to work with EPPlus library
        /// </summary>
        /// <param name="fileName">The file name</param>
        /// <param name="folderPath">(Optional). If you dont specify this parameter, it will default to the path of your ProjectName.UITest
        /// DataFiles_ConstantData folder. For example, <see cref="GetFolderPath_DataFilesConstantData"/>. If you do specify
        /// a value for this parameter, it must be the following format, including a slash at the end C:\\MyFolderName\\</param>
        public static void ConvertXLS_XLSX(string fileName, string folderPath = null)
        {
            NetworkShareAccesser.Access();

            folderPath = string.IsNullOrEmpty(folderPath) ? GetFolderPath_DataFilesConstantData() : folderPath;

            Workbook workbook = new Workbook();
            workbook.LoadFromFile(fileName);
            workbook.SaveToFile(fileName, ExcelVersion.Version2016);
        }

        /// <summary>
        /// Returns an excel worksheet object if the given worksheet is found in given excel workbook. Additional logic 
        /// has been added to return an excel worksheet from a given csv file
        /// </summary>
        /// <param name="folderPathPlusFileName">The full file location path string and the filename with extension</param>
        /// <param name="sheetName">The worksheet name</param>
        /// <returns>excelWorksheet object</returns>

        public static dynamic Excel_GetWorksheet(string folderPathPlusFileName, string sheetName)
        {
            NetworkShareAccesser.Access();
            FileInfo file = new FileInfo(folderPathPlusFileName);
            ExcelWorksheet excelWorksheet = null;
            ExcelPackage excelPackage = new ExcelPackage(file);

            //Get a WorkSheet by name                
            excelWorksheet = excelPackage.Workbook.Worksheets.FirstOrDefault(sh => sh.Name == sheetName);
            if (excelWorksheet != null)
            {
                return excelWorksheet;
            }
            else
            {
                throw new Exception("Expected Worksheet [ " + sheetName + " ] is not found in the Excel document located here: [ " +
                    folderPathPlusFileName + " ]");
            }

            return excelWorksheet;
        }

        /// <summary>
        /// Copies a tester-specified row then pastes that row onto the next empty row
        /// </summary>
        /// <param name="fileName">The file name including the extension</param>
        /// <param name="sheetName">The worksheet name</param>
        /// <param name="rowNumberToCopy">The row number you want to copy the data from</param>
        /// <param name="folderPath">(Optional). If you dont specify this parameter, it will default to the path of your ProjectName.UITest
        /// DataFiles_ConstantData folder. For example, <see cref="GetFolderPath_DataFilesConstantData"/>. If you do specify
        /// a value for this parameter, it must be the following format, including a slash at the end C:\\MyFolderName\\</param>
        public static void Excel_CopyRowThenPasteOntoNextEmptyRow(string fileName, string sheetName, int rowNumberToCopy, string folderPath = null)
        {
            NetworkShareAccesser.Access();

            folderPath = string.IsNullOrEmpty(folderPath) ? GetFolderPath_DataFilesConstantData() : folderPath;

            int lastRow = Excel_GetTotalRowCount(fileName, sheetName, folderPath);
            int lastCol = Excel_GetTotalColumnCount(fileName, sheetName, folderPath);

            FileInfo file = new FileInfo(folderPath + fileName);
            using (ExcelPackage excelPackage = new ExcelPackage(file))
            {
                ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets.FirstOrDefault(sh => sh.Name == sheetName);

                // Get the data from the row the tester wants to copy
                ExcelRange rowValue = excelWorksheet.Cells[rowNumberToCopy, 1, rowNumberToCopy, lastCol];

                // Paste the data from the above into the targeted row and columns
                rowValue.Copy(excelWorksheet.Cells[lastRow + 1, 1, lastRow, lastCol]);

                excelPackage.Save();
            }
        }

        /// <summary>
        /// Copies a tester-specified row then inserts that rows data onto a new tester-specified row 
        /// </summary>
        /// <param name="fileName">The file name including the extension</param>
        /// <param name="sheetName">The worksheet name</param>
        /// <param name="rowNumberToCopy">The row number you want to copy the data from</param>
        /// <param name="rowNumberToInsertData">The row number you want to insert the data</param>
        /// <param name="folderPath">(Optional). If you dont specify this parameter, it will default to the path of your ProjectName.UITest
        /// DataFiles_ConstantData folder. For example, <see cref="GetFolderPath_DataFilesConstantData"/>. If you do specify
        /// a value for this parameter, it must be the following format, including a slash at the end C:\\MyFolderName\\</param>
        public static void Excel_CopyRowThenInsertOntoNewRow(string fileName, string sheetName,
            int rowNumberToCopy, int rowNumberToInsertData, string folderPath = null)
        {
            NetworkShareAccesser.Access();

            folderPath = string.IsNullOrEmpty(folderPath) ? GetFolderPath_DataFilesConstantData() : folderPath;

            int lastCol = Excel_GetTotalColumnCount(fileName, sheetName, folderPath);

            FileInfo file = new FileInfo(folderPath + fileName);
            using (ExcelPackage excelPackage = new ExcelPackage(file))
            {
                ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets.FirstOrDefault(sh => sh.Name == sheetName);

                // Get the data from the row the tester wants to copy
                ExcelRange rowValue = excelWorksheet.Cells[rowNumberToCopy, 1, rowNumberToCopy, lastCol];

                // Paste the data from the above into the newly inserted row
                excelWorksheet.InsertRow(rowNumberToInsertData, 1);
                rowValue.Copy(excelWorksheet.Cells[rowNumberToInsertData, 1, rowNumberToInsertData, lastCol]);

                excelPackage.Save();
            }
        }

        /// <summary>
        /// Writes to a cell by the tester-specified row number and column number
        /// </summary>
        /// <param name="fileName">The file name including the extension</param>
        /// <param name="sheetName">The worksheet name</param>
        /// <param name="rowNum">The row number</param>
        /// <param name="colNum">The column number</param>
        /// <param name="textToAdd">The text you want to enter into the cell</param>
        /// <param name="folderPath">(Optional). If you dont specify this parameter, it will default to the path of your ProjectName.UITest
        /// DataFiles_ConstantData folder. For example, <see cref="GetFolderPath_DataFilesConstantData"/>. If you do specify
        /// a value for this parameter, it must be the following format, including a slash at the end C:\\MyFolderName\\</param>
        public static void Excel_SetData_ByRowNumAndColNum(string fileName, string sheetName, int rowNum,
            int colNum, string textToAdd, string folderPath = null)
        {
            NetworkShareAccesser.Access();

            folderPath = string.IsNullOrEmpty(folderPath) ? GetFolderPath_DataFilesConstantData() : folderPath;

            FileInfo file = new FileInfo(folderPath + fileName);
            using (ExcelPackage excelPackage = new ExcelPackage(file))
            {
                ExcelWorkbook excelWorkBook = excelPackage.Workbook;
                ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets.FirstOrDefault(sh => sh.Name == sheetName);

                excelWorksheet.Cells[rowNum, colNum].Value = textToAdd;
                excelPackage.Save();
            }
        }

        /// <summary>
        /// Writes to a cell by the tester-specified row number and column name
        /// </summary>
        /// <param name="fileName">The file name including the extension</param>
        /// <param name="sheetName">The worksheet name</param>
        /// <param name="rowNum">The row number</param>
        /// <param name="colName">The column name</param>
        /// <param name="textToAdd">The text you want to enter into the cell</param>
        /// <param name="folderPath">(Optional). If you dont specify this parameter, it will default to the path of your ProjectName.UITest
        /// DataFiles_ConstantData folder. For example, <see cref="GetFolderPath_DataFilesConstantData"/>. If you do specify
        /// a value for this parameter, it must be the following format, including a slash at the end C:\\MyFolderName\\</param>
        public static void Excel_SetData_ByRowNumAndColName(string fileName, string sheetName, int rowNum,
            string colName, string textToAdd, string folderPath = null)
        {
            NetworkShareAccesser.Access();

            folderPath = string.IsNullOrEmpty(folderPath) ? GetFolderPath_DataFilesConstantData() : folderPath;

            int colNum = Excel_GetColumnNumber(fileName, sheetName, colName, folderPath);

            FileInfo file = new FileInfo(folderPath + fileName);
            using (ExcelPackage excelPackage = new ExcelPackage(file))
            {
                ExcelWorkbook excelWorkBook = excelPackage.Workbook;
                ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets.FirstOrDefault(sh => sh.Name == sheetName);

                excelWorksheet.Cells[rowNum, colNum].Value = textToAdd;
                excelPackage.Save();
            }
        }

        /// <summary>
        /// Returns the data/value in the cell by the tester-specified row number and column number
        /// </summary>
        /// <param name="fileName">The file name including the extension</param>
        /// <param name="sheetName">The worksheet name</param>
        /// <param name="rowNum">The row number</param>
        /// <param name="colNum">The column name</param>
        /// <param name="folderPath">(Optional). If you dont specify this parameter, it will default to the path of your ProjectName.UITest
        /// DataFiles_ConstantData folder. For example, <see cref="GetFolderPath_DataFilesConstantData"/>. If you do specify
        /// a value for this parameter, it must be the following format, including a slash at the end C:\\MyFolderName\\</param>
        /// <param name="csvDelimiter">(Optional) If your file is a csv file, specify the delimiter that the file is using.
        /// Default = ,</param>
        /// <returns>data value in string format</returns>
        public static string Excel_GetData_ByRowAndColumn(string fileName, string sheetName, int rowNum, int colNum,
            string folderPath = null)
        {
            NetworkShareAccesser.Access();

            folderPath = string.IsNullOrEmpty(folderPath) ? GetFolderPath_DataFilesConstantData() : folderPath;

            ExcelWorksheet excelWorksheet = Excel_GetWorksheet(folderPath + fileName, sheetName);

            string cellvalue = excelWorksheet.Cells[rowNum, colNum].Text.ToString();
            return cellvalue;
        }

        /// <summary>
        /// Excel has a 31 character limit forThe worksheet names. This will shorten the names to the limit while also handling any 
        /// naming collisions which occur when there are multipe instances of the same name
        /// </summary>
        /// <param name="worksheetNames"></param>
        /// <returns></returns>
        public static List<string> Excel_GetWorksheetNames(List<string> worksheetNames)
        {
            NetworkShareAccesser.Access();

            // Trim the last 5 characters if the string is greater than 26 characters
            List<string> worksheetsNamesTrimmed = worksheetNames.Select<string, string>(s => s.Length >= 26 ? s.Substring(0, 26) : s).ToList();

            // Handle naming collisions
            var result = worksheetsNamesTrimmed.Take(1).ToList();

            for (var i = 1; i < worksheetsNamesTrimmed.Count; i++)
            {
                var name = worksheetsNamesTrimmed[i];
                var count = worksheetsNamesTrimmed.Take(i).Where(n => n == name).Count() + 1;

                result.Add(count < 2 ? name : name + " (" + count.ToString() + ")");
            }

            return result;
        }

        /// <summary>
        /// Returns the index of the tester-specified column name
        /// </summary>
        /// <param name="fileName">The file name including the extension</param>
        /// <param name="sheetName">The worksheet name</param>
        /// <param name="colName">The column name</param>
        /// <param name="folderPath">(Optional). If you dont specify this parameter, it will default to the path of your ProjectName.UITest
        /// DataFiles_ConstantData folder. For example, <see cref="GetFolderPath_DataFilesConstantData"/>. If you do specify
        /// a value for this parameter, it must be the following format, including a slash at the end C:\\MyFolderName\\</param>
        /// <param name="csvDelimiter">(Optional) If your file is a csv file, specify the delimiter that the file is using.
        /// Default = ,</param>
        /// <returns> ColumnIndex </returns>
        public static int Excel_GetColumnNumber(string fileName, string sheetName, string colName, string folderPath = null,
            char csvDelimiter = ',')
        {
            NetworkShareAccesser.Access();

            int rowIndex = 1; //first row
            int columnIndex = 0;
            int totalNumOfColumns;

            folderPath = string.IsNullOrEmpty(folderPath) ? GetFolderPath_DataFilesConstantData() : folderPath;

            ExcelWorksheet excelWorksheet = null;
            excelWorksheet = Excel_GetWorksheet(folderPath + fileName, sheetName); // return the worksheet

            try
            {
                totalNumOfColumns = excelWorksheet.Dimension.End.Column;                // if no records (i.e) returns null, goto catch block
                for (int colIndex = 1; colIndex <= totalNumOfColumns; colIndex++)
                {
                    if (excelWorksheet.Cells[rowIndex, colIndex].Text == colName)
                    {
                        return columnIndex = colIndex;
                    }
                }
                if (columnIndex <= 0)
                {
                    throw new Exception("Given ColumnName [ " + colName + " ] is not found in the Excel Worksheet");

                }
                else
                {
                    return columnIndex;
                }
            }
            catch (NullReferenceException e)
            {
                throw new Exception("Excel Sheet [ " + sheetName + "] is empty ; No records found", e);
            }

        }

        /// <summary>
        /// Returns the total number of rows/records in the tester-specified sheet
        /// </summary>
        /// <param name="fileName">The file name including the extension</param>
        /// <param name="sheetName">The worksheet name</param>
        /// <param name="folderPath">(Optional). If you dont specify this parameter, it will default to the path of your ProjectName.UITest
        /// DataFiles_ConstantData folder. For example, <see cref="GetFolderPath_DataFilesConstantData"/>. If you do specify
        /// a value for this parameter, it must be the following format, including a slash at the end C:\\MyFolderName\\</param>
        /// <param name="csvDelimiter">(Optional) If your file is a csv file, specify the delimiter that the file is using.
        /// Default = ,</param>
        public static int Excel_GetTotalRowCount(string fileName, string sheetName, string folderPath = null)
        {
            NetworkShareAccesser.Access();

            ExcelWorksheet excelWorksheet = null;
            int totalRowCount = 0;

            folderPath = string.IsNullOrEmpty(folderPath) ? GetFolderPath_DataFilesConstantData() : folderPath;

            excelWorksheet = Excel_GetWorksheet(folderPath + fileName, sheetName); // returns excelsheet

            try
            {
                totalRowCount = excelWorksheet.Dimension.Rows;  // if no records then go to catch block
                return totalRowCount;
            }
            catch { throw new Exception("Excel Sheet [ " + sheetName + "] is empty ; No records found"); }
        }



        /// <summary>
        /// Returns the total number of columns in the tester-specified sheet
        /// </summary>
        /// <param name="fileName">The file name including the extension</param>
        /// <param name="sheetName">The worksheet name</param>
        /// <param name="folderPath">(Optional). If you dont specify this parameter, it will default to the path of your ProjectName.UITest
        /// DataFiles_ConstantData folder. For example, <see cref="GetFolderPath_DataFilesConstantData"/>. If you do specify
        /// a value for this parameter, it must be the following format, including a slash at the end C:\\MyFolderName\\</param>
        /// <param name="csvDelimiter">(Optional) If your file is a csv file, specify the delimiter that the file is using.
        /// Default = ,</param>
        /// <returns>totalColumnCount = total columns</returns>
        public static int Excel_GetTotalColumnCount(string fileName, string sheetName, string folderPath = null)
        {
            NetworkShareAccesser.Access();

            folderPath = string.IsNullOrEmpty(folderPath) ? GetFolderPath_DataFilesConstantData() : folderPath;

            ExcelWorksheet excelWorksheet = Excel_GetWorksheet(folderPath + fileName, sheetName);

            int totalCols = excelWorksheet.Dimension.End.Column;
            return totalCols;
        }


        #endregion Excel

        #region Legacy Methods

        // This is no longer used. See the explanation located within commented out WaitForDownload method in the Legacy Methods region of
        // the BrowserExtensions class file
        ///// <summary>
        ///// Add an input element on the downloads page with an attribute of Type=Value, then uploads the most recently downloaded file 
        ///// from the current Browser 
        ///// session, then returns the contents of this uploaded file as a base64 encoded string, then converts/decodes the Base64 string 
        ///// into an 8-bit integer array. Once the string is converted, it transfers this encoding from server to local and saves 
        ///// the encoding text as the same name of the originally downloaded from on the server. You can then open/edit/verify/save 
        ///// this local file as you would the originally downloaded file, as it is a copy/transfer of the original. This will then close
        ///// the new tab and return focus back to the original tab
        ///// Future: Firefox implementation: https://code.premierinc.com/issues/browse/PQA-59 
        ///// https://stackoverflow.com/questions/47068912/how-to-download-a-file-using-the-remote-selenium-webdriver
        ///// </summary>
        ///// <param name="Browser"></param>
        ///// <param name="path"></param>
        ///// <returns></returns>
        //public static void GetFileContentFromRemoteThenAddThisContentToAFileOnLocal(IWebDriver Browser, string path)
        //{
        //    IWebElement elem = (IWebElement)Browser.ExecuteScript("var input = window.document.createElement('INPUT'); input.setAttribute('type', 'file'); input.hidden = true; input.onchange = function (e) { e.stopPropagation() }; return window.document.documentElement.appendChild(input); ");
        //    elem.SendKeys(path);

        //    // The FileReader object lets web applications asynchronously read the contents of files (or raw data buffers) stored on the user's
        //    // computer, using File or Blob objects to specify the file or data to read
        //    string fileContent = (string)((IJavaScriptExecutor)Browser).ExecuteAsyncScript("var input = arguments[0], callback = arguments[1]; var reader = new FileReader(); reader.onload = function (ev) { callback(reader.result) }; reader.onerror = function (ex) { callback(ex.message) }; reader.readAsDataURL(input.files[0]); input.remove(); ", elem);

        //    // The above javascript should return a string beginning with "data:". If it does not, then it failed
        //    if (!fileContent.StartsWith("data:"))
        //    {
        //        throw new Exception(String.Format("Failed to get file content: %s", fileContent));
        //    }

        //    // Remove all of the non-Base64 text, so we can then convert it. The below line is removing all text starting at the comma
        //    // because text before the comman is non-Base64 text. i.e. 
        //    fileContent = fileContent.Substring(fileContent.IndexOf(',') + 1);

        //    // Convert Base64 string into an 8-bit integer array.           
        //    byte[] data = Convert.FromBase64String(fileContent);

        //    // The above line converted to a format that is treated/saved/opened as a file on an operating system
        //    File.WriteAllBytes(path, data);

        //    // We have to close the current tab because GetDownloadedFiles() opened the tab
        //    Browser.Close();
        //    Browser.SwitchTo().Window(Browser.WindowHandles.First());
        //}

        // This is no longer used. See the explanation located within commented out WaitForDownload method in the Legacy Methods region of
        // the BrowserExtensions class file
        ///// <summary>
        ///// Opens a new tab, navigates to chrome://downloads/, waits for at least 1 download to appear on this page, then returns 
        ///// a list of strings representing the remote server's file location where all of the current Browser instance downloads 
        ///// are located. For example: C:\seleniumdownloads\MyExcelDocument.xlsx. 
        ///// </summary>
        ///// <param name="Browser"></param>
        ///// <returns></returns>
        //public static dynamic GetDownloadedFiles(IWebDriver Browser)
        //{
        //    // Go to Chrome downloads page
        //    ((IJavaScriptExecutor)Browser).ExecuteScript("window.open();");
        //    Browser.SwitchTo().Window(Browser.WindowHandles.Last());
        //    Browser.Navigate().GoToUrl("chrome://downloads/");

        //    // Wait for at least 1 download to appear. To do this, we have to traverse the Shadow Root elements of this page to find the download link.
        //    // The Chrome downloads page contains Shadow Roots within the DOM. We can not locate Shadow Roots the normal way with Xpath and Selenium.
        //    // We first have to get the main shadow root at the top of the shadow root tree, then find the downloads-item link within this Shadow Root.
        //    // If a download has not occured, then this downloads-item element does not exist. 
        //    IWebElement ParentOfShadowRoot1 = Browser.FindElement(By.XPath("//downloads-manager"));
        //    ShadowRoot ShadowRoot1 = ParentOfShadowRoot1.GetShadowRoot(Browser);

        //    // If we ever wanted to verify multiple downloads, we would
        //    // have to wait for the indexed download link (If we are verifying second download, then wait for the second downliad link), and to do that,
        //    // we would have to get the child shadow root that contains the download links, then wait for that link. Instead of just waiting for 
        //    // DownloadItemsFrame. We can get child shadow roots by doing the folowing
        //    //IWebElement ParentOfShadowRoot2 = ShadowRoot1.FindElement(By.CssSelector("downloads-item"));
        //    //ShadowRoot ShadowRoot2_TheShadowRootThatContainsThedownloadLinks = ParentOfShadowRoot2.GetShadowRoot(Browser);
        //    //IWebElement DownloadLink = null;

        //    IWebElement DownloadItemsFrame = null;
        //    try
        //    {
        //        // We can only use CSS Selector when locating elements inside Shadow Roots
        //        DownloadItemsFrame = ShadowRoot1.WaitForElement(By.CssSelector("downloads-item"), TimeSpan.FromSeconds(90));
        //    }
        //    catch (Exception)
        //    {
        //        throw new Exception("After navigating to chrome://downloads/ and waiting for 90 seconds, a download did not appear. This is either because " +
        //            "1) The download took longer than 90 seconds 2) Your test did not click on the download link properly 3) Your application has a " +
        //            "defect which results in the download link not properly downloading the file 3) There is a bug in the automation code");
        //    }


        //    // Get all of the locations of the downloads for the current Browser session, represented as a list of string
        //    var downloadLocations = Browser.ExecuteScript("return  document.querySelector('downloads-manager').shadowRoot.querySelector('#downloadsList').items.filter(e => e.state === 'COMPLETE').map(e => e.filePath || e.file_path || e.fileUrl || e.file_url); ");
        //    return downloadLocations;
        //}

        #endregion Legacy Methods






    }

    #region objects

    /// <summary>
    /// 
    /// </summary>
    public class MyWorkBook
    {
        public List<MyWorkSheets> Worksheets { get; set; }

        public MyWorkBook()
        {

        }

        public MyWorkBook(List<MyWorkSheets> worksheets)
        {
            Worksheets = worksheets;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class MyWorkSheets
    {
        public string Title { get; set; }
        public List<Parent> ParentStrings { get; set; }

        public MyWorkSheets()
        {
        }

        public MyWorkSheets(string title)
        {
            Title = title;
        }

        public MyWorkSheets(string title, List<Parent> parentStrings)
        {
            Title = title;
            ParentStrings = parentStrings;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Parent
    {
        public string Title { get; set; }
        public List<Child> Children { get; set; }

        public Parent(string title)
        {
            Title = title;
        }

        public Parent(string title, List<Child> children)
        {
            Title = title;
            Children = children;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Child
    {
        public string Title { get; set; }

        public Child(string title)
        {
            Title = title;
        }
    }

    #endregion objects
}