using Browser.Core.Framework;
using LMS.Data;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using LOG4NET = log4net.ILog;

namespace LMSAdmin.AppFramework
{
    public class Projects_AddEditPage : Page, IDisposable
    {
        #region constructors
        public Projects_AddEditPage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that WE start so we can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        public override string PageUrl { get { return "Apps/CommandCenter/Dashboard/Dashboard.aspx"; } }

        #endregion properties

        #region elements

        public IWebElement AddProjectLbl { get { return this.FindElement(Bys.Projects_AddEditPage.AddProjectLbl); } }
        public IWebElement DescriptionTxt { get { return this.FindElement(Bys.Projects_AddEditPage.DescriptionTxt); } }
        public IWebElement ProjectNameTxt { get { return this.FindElement(Bys.Projects_AddEditPage.ProjectNameTxt); } }
        public IWebElement SaveBtn { get { return this.FindElement(Bys.Projects_AddEditPage.SaveBtn); } }
        public IWebElement ShortLabelTxt { get { return this.FindElement(Bys.Projects_AddEditPage.ShortLabelTxt); } }



        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(30), Criteria.Projects_AddEditPage.PageReady);
        }

        /// <summary>
        /// Clicks the user-specified element, and then waits for a window to close or open, or a page to load, depending on the element that was clicked
        /// </summary>
        /// <param name="buttonOrLinkElem">The element to click on</param>
        public dynamic ClickAndWait(IWebElement buttonOrLinkElem)
        {
            if (Browser.Exists(Bys.Projects_AddEditPage.SaveBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == SaveBtn.GetAttribute("outerHTML"))
                {
                    SaveBtn.Click();
                    Browser.WaitForElement(Bys.Page.TreeLinks_Activities, ElementCriteria.IsVisible);
                    return null;
                }
            }

            throw new Exception("No button or link was found with your passed parameter. You either need to add this button to a new If statement, " +
                "or if the button is already added, then the page you were on did not contain the button.");
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
        /// 
        /// </summary>
        public void FillAddProjectForm()
        {
            string timeStamp = string.Format("{0}", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture));
            
            ProjectNameTxt.Clear();
            ProjectNameTxt.SendKeys(string.Format("AutoTest PrjName {0}", timeStamp));
            ShortLabelTxt.Clear();
            ShortLabelTxt.SendKeys(string.Format("AutoTest ShtLbl {0}", timeStamp));
            DescriptionTxt.Clear();
            DescriptionTxt.SendKeys(string.Format("AutoTest PrjDescription {0}", timeStamp));
        }




        #endregion methods: page specific



    }
}