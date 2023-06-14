using System;
using System.Collections.Generic;
using Browser.Core.Framework;
using OpenQA.Selenium;
using LOG4NET = log4net.ILog;

namespace LMSAdmin.AppFramework
{
    public class Legacy_SetupPage : Page , IDisposable
    {
        private IWebDriver browser;       

        public Legacy_SetupPage(IWebDriver driver) : base(driver)
        {
        }

        public override string PageUrl { get { return ""; } }

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that WE start so we can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            try { activeRequests.Clear(); }
            catch (Exception ex) { _log.ErrorFormat("Failed to dispose setupPage", activeRequests.Count, ex); }
        }

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(30), Criteria.Legacy_SetupPage.PageReady);
        }

        #region elements

        public IWebElement AccreditationTypesLnk { get { return this.FindElement(Bys.Legacy_SetupPage.AccreditationTypesLnk); } }
        public IWebElement AddAccreditationTypeLnk { get { return this.FindElement(Bys.Legacy_SetupPage.AddAccreditationTypeLnk); } }
        public IWebElement AccreditationTypeNameTxt { get { return this.FindElement(Bys.Legacy_SetupPage.AccreditationTypeNameTxt); } }
        public IWebElement AccreditationTypeSaveChangesBtn { get { return this.FindElement(Bys.Legacy_SetupPage.AccreditationTypeSaveChangesBtn); } }
        public IWebElement AccreditationTypesTbl { get { return this.FindElement(Bys.Legacy_SetupPage.AccreditationTypesTbl); } }

        #endregion

        #region  methods: page specific

        public void Setup_AccreditationType(IWebDriver Browser, string accreditationTypeName, int numberOfAccreditationTypesRequired)
        {
            AccreditationTypesLnk.Click();
            Browser.WaitForElement(Bys.Legacy_SetupPage.AddAccreditationTypeLnk, TimeSpan.FromSeconds(2), ElementCriteria.IsVisible);
            AccreditationTypeNameTxt.SendKeys(accreditationTypeName);
            AccreditationTypeSaveChangesBtn.Click();
            ElemGet.Grid_CellTextFound(Browser, AccreditationTypesTbl, 1, "td", accreditationTypeName);                        
        }
        #endregion

    }
}