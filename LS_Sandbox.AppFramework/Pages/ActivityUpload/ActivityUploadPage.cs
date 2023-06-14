using Browser.Core.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using LOG4NET = log4net.ILog;

namespace LS.AppFramework
{
    public class ActivityUploadPage : Page, IDisposable
    {
        #region constructors
        public ActivityUploadPage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that WE start so we can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        public override string PageUrl { get { return ""; } }

        #endregion properties

        #region elements
        
        public IWebElement UploadBtn { get { return this.FindElement(Bys.ActivityUploadPage.UploadBtn); } }
        public IWebElement ChooseFileUploadElem { get { return this.FindElement(Bys.ActivityUploadPage.ChooseFileUploadElem); } }
        public IWebElement UploadTblBodyFirstRow { get { return this.FindElement(Bys.ActivityUploadPage.UploadTblBodyFirstRow); } }
        public IWebElement UploadTblBody { get { return this.FindElement(Bys.ActivityUploadPage.UploadTblBody); } }
        public IWebElement UploadTblHdr { get { return this.FindElement(Bys.ActivityUploadPage.UploadTblHdr); } }
        public IWebElement UploadTbl { get { return this.FindElement(Bys.ActivityUploadPage.UploadTbl); } }

        #endregion elements

        #region methods: repeated per page

        /// <summary>
        /// Clicks the user-specified button, link, tab, etc. and then waits for a window to close or open, or a page to load,
        /// depending on the element that was clicked
        /// </summary>
        /// <param name="elem">The element to click on</param>
        public dynamic ClickAndWait(IWebElement elem)
        {
            //if (Browser.Exists(Bys.SitePage.ActivityUploadLnk))
            //{
            //    if (elem.GetAttribute("outerHTML") == ActivityUploadLnk.GetAttribute("outerHTML"))
            //    {
            //        elem.Click();
            //        ActivityUploadPage AUP = new ActivityUploadPage(Browser);
            //        AUP.WaitForInitialize();
            //        return AUP;
            //    }
            //}

            //if (Browser.Exists(Bys.EnterACPDActivityPage.DoYouKnowYourSessionIDContinueBtn))
            //{
            //    if (elem.GetAttribute("outerHTML") == DoYouKnowYourSessionIDContinueBtn.GetAttribute("outerHTML"))
            //    {
            //        elem.Click();
            //        EnterACPDActivityDetailsPage EADP = new EnterACPDActivityDetailsPage(Browser);
            //        EADP.WaitForInitialize();
            //        return EADP;
            //    }
            //}

            throw new Exception(string.Format("No element was found with your passed parameter, which was the '{0}' element. " +
                "You either need to add this element to a new If statement, or if the element is already added, then the page " +
                "you were on did not contain the element.",
                elem.GetAttribute("innerText")));
        }

        public override void WaitForInitialize()
        {
            try
            {
                this.WaitUntil(TimeSpan.FromSeconds(30), Criteria.ActivityUploadPage.PageReady);
            }
            catch
            {
                RefreshPage();
            }

        }

        /// Refreshes the page and then uses the wait criteria that is found within WaitForInitialize to wait for the page
        /// to load. This is used as a catch block inside WaitForInitialize, in case the page doesnt load initially. 
        /// Can also be used to randomly refresh the page
        /// </summary>
        public void RefreshPage()
        {
            Browser.Navigate().Refresh();
            this.WaitUntil(TimeSpan.FromSeconds(30), Criteria.ActivityUploadPage.PageReady);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            try { activeRequests.Clear(); }
            catch (Exception ex) { _log.ErrorFormat("Failed to dispose LoginPage", activeRequests.Count, ex); }
        }

        #endregion methods: repeated per page

        #region methods: page specific

        /// <summary>
        /// Uploads the tester-specified file, then waits for the Completed label to appear. The file should 
        /// be added to a folder titled DateFiles inside the root of your UITest project and it should be added 
        /// inside Visual Studio Solution Explorer
        /// </summary>
        /// <param name="fileName">File name including extension</param>
        internal void UploadActivity(string fileName)
        {
            string pathToFile = FileUtils.GetProjectDirectory() + "\\DataFiles\\" + fileName;

            FileUtils.UploadFileUsingSendKeys(Browser, ChooseFileUploadElem, pathToFile, this);

            UploadBtn.Click();
            // Have to wait for the record to be populated into the table before we execute the For statement below
            Thread.Sleep(5000);

            for (int i = 0; i < 60; i++)
            {
                string recordsProcessedCellText = ElemGet.Grid_GetCellTextByRowIndexAndColIndex(Browser, UploadTblBody,
                    Bys.ActivityUploadPage.UploadTblBodyFirstRow, 0, 3, "//td");
                // Outstanding defect https://code.premierinc.com/issues/browse/MAINPROREW-883. Remove the Warning
                // condition when this is fixed and run the UploadActivity test
                if (recordsProcessedCellText == "Completed" || recordsProcessedCellText == "Warning")
                {
                    break;
                }
                else
                {
                    if (i == 59)
                    {
                        throw new Exception("We waited 5 minutes for the Records Processed column to change to " +
                            "'Completed' and it did not. Your upload may have failed");
                    }
                    else
                    {
                        Thread.Sleep(5000);
                        this.RefreshPage();
                        continue;
                    }
                }
            }
        }

        #endregion methods: page specific



    }
}