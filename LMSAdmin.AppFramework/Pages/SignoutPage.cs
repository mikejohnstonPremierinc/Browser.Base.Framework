using OpenQA.Selenium;
using Browser.Core.Framework;
using LMS.Data;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using LOG4NET = log4net.ILog;

namespace LMSAdmin.AppFramework
{

    public class SignoutPage : LoginPage, IDisposable
    {
        #region constructors
        public SignoutPage(IWebDriver driver) : base(driver)
        {
        }
        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that WE start so we can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        public override string PageUrl { get { return "Apps/Authenticate/SignOut.aspx?"; } }

        #endregion properties

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(240), Criteria.LoginPage.PageReady);            
            Thread.Sleep(0300);
        }
       

        #endregion methods: repeated per page

    }
}