using Browser.Core.Framework;
using LMS.Data;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using LOG4NET = log4net.ILog;

namespace Mainpro.AppFramework
{
    public class EntryCarouselPathwayPage : MainproPage, IDisposable
    {
        #region constructors
        public EntryCarouselPathwayPage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that WE start so we can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        public override string PageUrl { get { return "login.aspx?action=enablelogin"; } }

        #endregion properties

        #region elements
        
        public IWebElement PeerSupportedPleaseConfirmFormConfirmBtn { get { return this.FindElement(Bys.EntryCarouselPathwayPage.PeerSupportedPleaseConfirmFormConfirmBtn); } }
        public IWebElement PeerSupportedPleaseConfirmFormNoBtn { get { return this.FindElement(Bys.EntryCarouselPathwayPage.PeerSupportedPleaseConfirmFormNoBtn); } }
        public IWebElement SelfGuidedModalPleaseConfirmFormConfirmBtn { get { return this.FindElement(Bys.EntryCarouselPathwayPage.SelfGuidedModalPleaseConfirmFormConfirmBtn); } }
        public IWebElement SelfGuidedModalPleaseConfirmFormNoBtn { get { return this.FindElement(Bys.EntryCarouselPathwayPage.SelfGuidedModalPleaseConfirmFormNoBtn); } }
        public IWebElement PLPActivityOverviewLbl { get { return this.FindElement(Bys.EntryCarouselPathwayPage.PLPActivityOverviewLbl); } }
        public IWebElement EnterBtn { get { return this.FindElement(Bys.EntryCarouselPathwayPage.EnterBtn); } }
        public IWebElement PeerSupportedInfoBtn { get { return this.FindElement(Bys.EntryCarouselPathwayPage.PeerSupportedInfoBtn); } }
        public IWebElement SelfGuidedBeginBtn { get { return this.FindElement(Bys.EntryCarouselPathwayPage.SelfGuidedBeginBtn); } }
        public IWebElement PeerSupportedBeginBtn { get { return this.FindElement(Bys.EntryCarouselPathwayPage.PeerSupportedBeginBtn); } }
        public IWebElement CarouselCircleBtn1 { get { return this.FindElement(Bys.EntryCarouselPathwayPage.CarouselCircleBtn1); } }
        public IWebElement CarouselCircleBtn2 { get { return this.FindElement(Bys.EntryCarouselPathwayPage.CarouselCircleBtn2); } }
        public IWebElement CarouselCircleBtn3 { get { return this.FindElement(Bys.EntryCarouselPathwayPage.CarouselCircleBtn3); } }
        public IWebElement CarouselCircleBtn4 { get { return this.FindElement(Bys.EntryCarouselPathwayPage.CarouselCircleBtn4); } }
        public IWebElement CarouselCircleBtn5 { get { return this.FindElement(Bys.EntryCarouselPathwayPage.CarouselCircleBtn5); } }
        public IWebElement CarouselCircleBtn6 { get { return this.FindElement(Bys.EntryCarouselPathwayPage.CarouselCircleBtn6); } }
        public IWebElement CarouselCircleSlide1 { get { return this.FindElement(Bys.EntryCarouselPathwayPage.CarouselCircleSlide1); } }
        public IWebElement CarouselCircleSlide2 { get { return this.FindElement(Bys.EntryCarouselPathwayPage.CarouselCircleSlide2); } }
        public IWebElement CarouselCircleSlide3 { get { return this.FindElement(Bys.EntryCarouselPathwayPage.CarouselCircleSlide3); } }
        public IWebElement CarouselCircleSlide4 { get { return this.FindElement(Bys.EntryCarouselPathwayPage.CarouselCircleSlide4); } }
        public IWebElement CarouselCircleSlide5 { get { return this.FindElement(Bys.EntryCarouselPathwayPage.CarouselCircleSlide5); } }
        public IWebElement CarouselCircleSlide6 { get { return this.FindElement(Bys.EntryCarouselPathwayPage.CarouselCircleSlide6); } }
        public IWebElement PauseBtn { get { return this.FindElement(Bys.EntryCarouselPathwayPage.PauseBtn); } }
        public IWebElement PlayBtn { get { return this.FindElement(Bys.EntryCarouselPathwayPage.PlayBtn); } }
        public IWebElement NextBtn { get { return this.FindElement(Bys.EntryCarouselPathwayPage.NextBtn); } }
        public IWebElement BackBtn { get { return this.FindElement(Bys.EntryCarouselPathwayPage.BackBtn); } }
        public IWebElement NextArrowBtn { get { return this.FindElement(Bys.EntryCarouselPathwayPage.NextArrowBtn); } }
        public IWebElement PreviousArrowBtn { get { return this.FindElement(Bys.EntryCarouselPathwayPage.PreviousArrowBtn); } }
        public IWebElement SelfGuidedInfoBtn { get { return this.FindElement(Bys.EntryCarouselPathwayPage.SelfGuidedInfoBtn); } }
        public IWebElement ImageContainer { get { return this.FindElement(Bys.EntryCarouselPathwayPage.ImageContainer); } }

        

        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(60));
            if (Browser.Exists(Bys.MainproPage.WereSorryErrorLbl, ElementCriteria.IsVisible))
            {
                throw new Exception(string.Format("The application threw an error '{0}'. Check the Browsers console log for any " +
                    "javascript errors. Sometimes this error occurs intermittently and will not occur a second time when rerunning " +
                    "the same test", WereSorryErrorLbl.Text));
            }
            this.WaitUntil(TimeSpan.FromSeconds(60), Criteria.EntryCarouselPathwayPage.PageReady);
            Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(60));
            Thread.Sleep(500);
            this.WaitUntil(TimeSpan.FromSeconds(60), Criteria.EntryCarouselPathwayPage.PageReady);
            Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(60));
            if (Browser.Exists(Bys.MainproPage.WereSorryErrorLbl, ElementCriteria.IsVisible))
            {
                throw new Exception(string.Format("The application threw an error '{0}'. Check the Browsers console log for any " +
                    "javascript errors. Sometimes this error occurs intermittently and will not occur a second time when rerunning " +
                    "the same test", WereSorryErrorLbl.Text));
            }
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
        /// Clicks the user-specified element and then waits for a window to close or open, or a page to load,
        /// depending on the element that was clicked
        /// </summary>
        /// <param name="elem">The element to click on</param>
        public dynamic ClickAndWait(IWebElement elem)
        {
            if (Browser.Exists(Bys.EntryCarouselPathwayPage.EnterBtn))
            {
                if (elem.GetAttribute("outerHTML") == EnterBtn.GetAttribute("outerHTML"))
                {
                    EnterBtn.Click();
                    this.WaitForInitialize();
                    return null;
                }
            }

            if (Browser.Exists(Bys.EntryCarouselPathwayPage.NextBtn))
            {
                if (elem.GetAttribute("outerHTML") == NextBtn.GetAttribute("outerHTML"))
                {
                    NextBtn.Click();
                    Browser.WaitJSAndJQuery();
                    this.WaitUntilAny(Criteria.EntryCarouselPathwayPage.PageReady,
                        Criteria.EntryCarouselPathwayPage.PeerSupportedBeginBtnVisible);
                    return null;
                }
            }

            if (Browser.Exists(Bys.EntryCarouselPathwayPage.SelfGuidedBeginBtn))
            {
                if (elem.FindElement(By.XPath("./..")).GetAttribute("outerHTML")
                    == SelfGuidedBeginBtn.FindElement(By.XPath("./..")).GetAttribute("outerHTML"))
                {
                    SelfGuidedBeginBtn.Click();
                    Browser.WaitJSAndJQuery();
                    this.WaitUntilAny(Criteria.EntryCarouselPathwayPage.LoadIconNotExists,
                        Criteria.EntryCarouselPathwayPage.LoadIconOverlayNotExists,
                        Criteria.EntryCarouselPathwayPage.SelfGuidedModalPleaseConfirmFormConfirmBtnVisible);
                    Browser.WaitJSAndJQuery();
                    return null;
                }
            }

            if (Browser.Exists(Bys.EntryCarouselPathwayPage.SelfGuidedModalPleaseConfirmFormConfirmBtn))
            {
                if (elem.GetAttribute("outerHTML") == SelfGuidedModalPleaseConfirmFormConfirmBtn.GetAttribute("outerHTML"))
                {
                    SelfGuidedModalPleaseConfirmFormConfirmBtn.Click();
                    Browser.WaitJSAndJQuery();
                    Step1Page page = new Step1Page(Browser);
                    page.WaitForInitialize();
                    return page;
                }
            }

            if (Browser.Exists(Bys.EntryCarouselPathwayPage.PeerSupportedBeginBtn))
            {
                if (elem.FindElement(By.XPath("./..")).GetAttribute("outerHTML")
                    == PeerSupportedBeginBtn.FindElement(By.XPath("./..")).GetAttribute("outerHTML"))
                {
                    PeerSupportedBeginBtn.Click();
                    Browser.WaitJSAndJQuery();
                    this.WaitUntilAny(Criteria.EntryCarouselPathwayPage.LoadIconNotExists,
                        Criteria.EntryCarouselPathwayPage.LoadIconOverlayNotExists,
                        Criteria.EntryCarouselPathwayPage.PeerSupportedPleaseConfirmFormConfirmBtnVisible);
                    Browser.WaitJSAndJQuery();
                    return null;
                }
            }

            if (Browser.Exists(Bys.EntryCarouselPathwayPage.PeerSupportedPleaseConfirmFormConfirmBtn))
            {
                if (elem.GetAttribute("outerHTML") == PeerSupportedPleaseConfirmFormConfirmBtn.GetAttribute("outerHTML"))
                {
                    Thread.Sleep(TimeSpan.FromSeconds(120)); ;
                    PeerSupportedPleaseConfirmFormConfirmBtn.Click();
                    Browser.WaitJSAndJQuery();
                    Step1Page page = new Step1Page(Browser);
                    page.WaitForInitialize();
                    return page;
                }
            }

            throw new Exception(string.Format("No element was found with your passed parameter, which was the '{0}' element. " +
                "You either need to add this element to a new If statement, or if the element is already added, then the page " +
                "you were on did not contain the element.",
                elem.GetAttribute("innerText")));
        }

        #endregion methods: per page

        #region methods: page specific



        #endregion methods: page specific



    }
}
