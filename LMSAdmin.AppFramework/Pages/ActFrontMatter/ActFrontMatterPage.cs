using Browser.Core.Framework;
using LMS.Data;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using LOG4NET = log4net.ILog;
using LMSAdmin.AppFramework.ConstantsLMSAdmin;
using OpenQA.Selenium.Interactions;

namespace LMSAdmin.AppFramework
{
    public class ActFrontMatterPage : Page, IDisposable
    {
        #region constructors
        public ActFrontMatterPage(IWebDriver driver) : base(driver)
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

        public IWebElement NameTxt { get { return this.FindElement(Bys.ActFrontMatterPage.NameTxt); } }
        public IWebElement ScenarioTblFirstRow { get { return this.FindElement(Bys.ActFrontMatterPage.ScenarioTblFirstRow); } }
        public IWebElement ScenarioTblBody { get { return this.FindElement(Bys.ActFrontMatterPage.ScenarioTblBody); } }
        public IWebElement ScenarioTbl { get { return this.FindElement(Bys.ActFrontMatterPage.ScenarioTbl); } }
        public IWebElement FrontMatterTblFirstRow { get { return this.FindElement(Bys.ActFrontMatterPage.FrontMatterTblFirstRow); } }
        public IWebElement FrontMatterTblBody { get { return this.FindElement(Bys.ActFrontMatterPage.FrontMatterTblBody); } }
        public IWebElement FrontMatterTbl { get { return this.FindElement(Bys.ActFrontMatterPage.FrontMatterTbl); } }
        public IWebElement AddFrontMatterLnk { get { return this.FindElement(Bys.ActFrontMatterPage.AddFrontMatterLnk); } }
        public IWebElement FrontMatterFrame { get { return this.FindElement(Bys.ActFrontMatterPage.FrontMatterFrame); } }
        public IWebElement FrontMatterDetailSaveBtn { get { return this.FindElement(Bys.ActFrontMatterPage.FrontMatterDetailSaveBtn); } }
        public IWebElement FrontMatterScenarioSaveBtn { get { return this.FindElement(Bys.ActFrontMatterPage.FrontMatterScenarioSaveBtn); } }


        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(30), Criteria.ActFrontMatterPage.PageReady);
        }

        /// <summary>
        /// Clicks the user-specified element, and then waits for a window to close or open, or a page to load, depending on the element that was clicked
        /// </summary>
        /// <param name="buttonOrLinkElem">The element to click on</param>
        public dynamic ClickAndWait(IWebElement buttonOrLinkElem)
        {
            if (Browser.Exists(Bys.ActFrontMatterPage.AddFrontMatterLnk))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == AddFrontMatterLnk.GetAttribute("outerHTML"))
                {
                    AddFrontMatterLnk.ClickJS(Browser);
                    Browser.WaitForElement(Bys.ActFrontMatterPage.NameTxt, ElementCriteria.IsVisible);
                    return null;
                }
            }

            if (Browser.Exists(Bys.ActFrontMatterPage.FrontMatterDetailSaveBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == FrontMatterDetailSaveBtn.GetAttribute("outerHTML"))
                {
                    FrontMatterDetailSaveBtn.ClickJS(Browser);
                    Browser.WaitForElement(Bys.ActFrontMatterPage.ScenarioTblBody, ElementCriteria.IsVisible);
                    return null;
                }
            }

            if (Browser.Exists(Bys.ActFrontMatterPage.FrontMatterScenarioSaveBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == FrontMatterScenarioSaveBtn.GetAttribute("outerHTML"))
                {
                    FrontMatterScenarioSaveBtn.ClickJS(Browser);
                    Browser.WaitForElement(Bys.ActFrontMatterPage.AddFrontMatterLnk, ElementCriteria.IsVisible);
                    return null;
                }
            }

            throw new Exception(string.Format("No element was found with your passed parameter, which was the '{0}' element. You either need to add " +
                "this element to a new If statement, or if the element is already added, then the page you were on did not contain the element.",
                buttonOrLinkElem.GetAttribute("innerText")));
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="frontMatterText"></param>
        public void AddFrontMatter(string frontMatterText = null)
        {
            ClickAndWait(AddFrontMatterLnk);
            NameTxt.SendKeys(DataUtils.GetRandomString(10));

            // The front matter text has to be entered in some weird HTML iframe thing which I dont have the time to figure out, so im just going to tab
            // 54 times to set focus in there, then send text that way
            Actions action = new Actions(Browser);
            for (var i = 0; i < 54; i++)
            {
                action.SendKeys(Keys.Tab);
            }
            action.Build().Perform();
            action.SendKeys(frontMatterText).Build().Perform();

            ClickAndWait(FrontMatterDetailSaveBtn);

            IList<IWebElement> rows = ElemGet_LMSAdmin.Grid_GetRows(ScenarioTblBody, Bys.ActFrontMatterPage.ScenarioTblFirstRow);
            foreach (var row in rows)
            {
                //ElemSet_LMSAdmin.Grid_ClickElementWithoutTextInsideRow(row, "input", "Checkbox");
                ElemSet_LMSAdmin.Grid_TickCheckBox(row, 1);
            }

            ClickAndWait(FrontMatterScenarioSaveBtn);
        }

        #endregion methods: repeated per page

        #region methods: page specific




        #endregion methods: page specific



    }
}