using Browser.Core.Framework;
using LMS.Data;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using LOG4NET = log4net.ILog;

namespace LMS.AppFramework
{
    public class ActivitiesInProgressPage : LMSPage, IDisposable
    {
        #region constructors
        public ActivitiesInProgressPage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that WE start so we can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        public override string PageUrl { get { return "lms/curriculum"; } }

        #endregion properties

        #region elements

        public IWebElement ActivitiesTblFirstLnk { get { return this.FindElement(Bys.ActivitiesInProgressPage.ActivitiesTblFirstLnk); } }
        public IWebElement ActivitiesTbl { get { return this.FindElement(Bys.ActivitiesInProgressPage.ActivitiesTbl); } }
        public IWebElement ActivitiesTblBody { get { return this.FindElement(Bys.ActivitiesInProgressPage.ActivitiesTblBody); } }
        public IWebElement ActivitiesInProgressLbl { get { return this.FindElement(Bys.ActivitiesInProgressPage.ActivitiesInProgressLbl); } }

        public IList<IWebElement> ActivitiesTblExpandBtns { get { return this.FindElements(Bys.ActivitiesInProgressPage.ActivitiesTblExpandBtns); } }


        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(90), Criteria.ActivitiesInProgressPage.PageReady);
            Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            try { activeRequests.Clear(); }
            catch (Exception ex) { _log.ErrorFormat("Failed to dispose ActivityPreviewPage", activeRequests.Count, ex); }
        }

        #endregion methods: repeated per page

        #region methods: page specific

        /// <summary>
        /// Clicks the user-specified button, link, tab, etc. and then waits for a window/element to close or open, or a page to load,
        /// depending on the element that was clicked. Once the Wait Criteria is satisfied, the test continues, and the method returns
        /// either a new Page class instance or nothing at all (hence the 'dynamic' return type). For a thorough explanation of how this
        /// type of method works, and how to use this method <see cref="LMSPage.ClickAndWaitBasePage(IWebElement)"/>
        /// </summary>
        /// <param name="elem">The element to click on</param>
        public dynamic ClickAndWait(IWebElement elem)
        {
            if (Browser.Exists(Bys.LMSPage.LoginLnk))
            {
                if (elem.GetAttribute("outerHTML") == LoginLnk.GetAttribute("outerHTML"))
                {
                    LoginLnk.Click(Browser);
                    LoginPage Page = new LoginPage(Browser);
                    Page.WaitForInitialize();
                    return Page;
                }
            }

            throw new Exception(string.Format("No element was found with your passed parameter, which was the '{0}' element. You either need to add " +
                "this element to a new If statement, or if the element is already added, then the page you were on did not contain the element.",
                elem.GetAttribute("innerText")));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="activityName"></param>
        public void DeleteActivity(IWebDriver browser, string activityName)
        {
            string Xpath = string.Format("//div[.='{0}']/../../..//div[@class='button-icon glyphicon glyphicon-trash center']", activityName);
            IWebElement deleteBtn = browser.FindElement(By.XPath(Xpath));
            deleteBtn.Click(Browser);
            this.WaitUntil(Criteria.ActivitiesInProgressPage.LoadIconNotExists);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="activityName"></param>
        /// <returns></returns>
        public Constants.ActivityInProgress GetActivityInProgressInfo(string activityName)
        {
            List<Constants.ActivityInProgress> activitiesInProgress = new List<Constants.ActivityInProgress>();

            // We have to expand the row for the credits to appear in the HTML. So click the expand button
            string xpath =
                string.Format("//*[text()='{0}']/ancestor::tr/descendant::button[contains(@class, 'grid-expand-button')]", activityName);
            IWebElement ActivitiesTblExpandBtn = Browser.FindElement(By.XPath(xpath));
            ActivitiesTblExpandBtn.Click(Browser);

            try
            {
                ActivitiesTblBody.WaitForElement(
                    By.XPath("descendant::tr[contains(@class, 'parent')]/following-sibling::tr[contains(@class, 'child')]"),
                    TimeSpan.FromSeconds(10),
                    ElementCriteria.IsVisible);
            }
            catch (Exception)
            {
                ActivitiesTblExpandBtn.ClickJS(Browser);
                ActivitiesTblBody.WaitForElement(
                    By.XPath("descendant::tr[contains(@class, 'parent')]/following-sibling::tr[contains(@class, 'child')]"),
                    TimeSpan.FromSeconds(10),
                    ElementCriteria.IsVisible);
            }

            Constants.ActivityInProgress activityInProgressInfo = new Constants.ActivityInProgress();

            // Find the row with the information inside of it
            xpath = string.Format("//*[text()='{0}']/ancestor::tr[contains(@class, 'parent')]", activityName);
            IWebElement Row = ActivitiesTblBody.FindElement(By.XPath(xpath));

            activityInProgressInfo.ActivityTitle = Row.FindElement(By.XPath("descendant::*[@class='hyperlink']")).Text;
            activityInProgressInfo.ActivityType = Row.FindElement(By.XPath("descendant::span[2]")).Text;
            activityInProgressInfo.Address = Row.FindElement(By.XPath("descendant::span[6]")).Text.Trim();
            activityInProgressInfo.ExpirationDate = Row.FindElement(By.XPath("descendant::td[contains(@class, 'column-expirationDate')]")).Text.Trim();

            // Set the credit
            IWebElement ChildRow = Row.FindElement(By.XPath("following-sibling::tr[contains(@class, 'child')]"));
            activityInProgressInfo.Credit = new List<string>();
            activityInProgressInfo.Credit.AddRange(ChildRow.FindElements(
                By.XPath("descendant::div[contains(@data-fe-repeat, 'accreditations')]/span[2]")).Select(s => s.Text.Trim()).ToList());

            activityInProgressInfo.Credit.Sort();

            activitiesInProgress.Add(activityInProgressInfo);

            return activityInProgressInfo;
        }

        #endregion methods: page specific



    }
}