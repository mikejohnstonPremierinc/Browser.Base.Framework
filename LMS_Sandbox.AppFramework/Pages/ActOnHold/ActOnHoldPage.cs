using Browser.Core.Framework;
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
    public class ActOnHoldPage : LMSPage, IDisposable
    {
        #region constructors
        public ActOnHoldPage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that WE start so we can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        public override string PageUrl { get { return "lms/#/activity"; } }

        #endregion properties

        #region elements

        public IWebElement ContinueBtn { get { return this.FindElement(Bys.ActOnHoldPage.ContinueBtn); } }
        public IWebElement NotificationWarnIcon { get { return this.FindElement(Bys.ActOnHoldPage.NotificationWarnIcon); } }

        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(90), Criteria.ActOnHoldPage.PageReady);
            Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));
        }
     
        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            try { activeRequests.Clear(); }
            catch (Exception ex) { _log.ErrorFormat("Failed to dispose ActOnHoldPage", activeRequests.Count, ex); }
        }

        #endregion methods: repeated per page

        #region methods: page specific

    

        #endregion methods: page specific



    }
}