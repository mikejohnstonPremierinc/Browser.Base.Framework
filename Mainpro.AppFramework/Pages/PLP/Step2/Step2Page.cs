using Browser.Core.Framework;
using LMS.Data;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LOG4NET = log4net.ILog;

namespace Mainpro.AppFramework
{
    public class Step2Page : MainproPage, IDisposable
    {
        #region constructors
        public Step2Page(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that WE start so we can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        public override string PageUrl { get { return "login.aspx?action=enablelogin"; } }

        MainproHelperMethods Help = new MainproHelperMethods();
        #endregion properties

        #region elements  
        public IWebElement BackBtn { get { return this.FindElement(Bys.Step2Page.BackBtn); } }
        public IWebElement NextBtn { get { return this.FindElement(Bys.Step2Page.NextBtn); } }
        public IWebElement GapNextBtn { get { return this.FindElement(Bys.Step2Page.GapNextBtn); } }
        public IWebElement GapScreenViewModeNextBtn { get { return this.FindElement(Bys.Step2Page.GapScreenViewModeNextBtn); } }
        public IWebElement ExpandAllBtn { get { return this.FindElement(Bys.Step2Page.ExpandAllBtn); } }
        public IWebElement CollapseAllBtn { get { return this.FindElement(Bys.Step2Page.CollapseAllBtn); } }        
        public IWebElement GoToBottomBtn { get { return this.FindElement(Bys.Step2Page.GoToBottomBtn); } }
        public IWebElement AdditionalGapBtn { get { return this.FindElement(Bys.Step2Page.AdditionalGapBtn); } }
        public IWebElement Gap1EditBtn { get { return this.FindElement(Bys.Step2Page.Gap1EditBtn); } }
         public IWebElement Gap2EditBtn { get { return this.FindElement(Bys.Step2Page.Gap2EditBtn); } }
         public IWebElement Gap3EditBtn { get { return this.FindElement(Bys.Step2Page.Gap3EditBtn); } }
        public IWebElement Gap1DeleteBtn { get { return this.FindElement(Bys.Step2Page.Gap1DeleteBtn); } }
        public IWebElement Gap2DeleteBtn { get { return this.FindElement(Bys.Step2Page.Gap2DeleteBtn); } }
        public IWebElement Gap3DeleteBtn { get { return this.FindElement(Bys.Step2Page.Gap3DeleteBtn); } }
        public IWebElement GapsContinueBtn { get { return this.FindElement(Bys.Step2Page.GapsContinueBtn); } }

        public SelectElement SelectDomainOfCareSelElem { get { return new SelectElement(this.FindElement(Bys.Step2Page.SelectDomainOfCareSelElem)); } }
        public IWebElement SelectDomainOfCareSelElemBtn { get { return this.FindElement(Bys.Step2Page.SelectDomainOfCareSelElemBtn); } }
        public SelectElement SelectSubsetsSelElem { get { return new SelectElement (this.FindElement(Bys.Step2Page.SelectSubsetsSelElem)); } }
        public IWebElement SelectSubsetsSelElemBtn { get { return this.FindElement(Bys.Step2Page.SelectSubsetsSelElemBtn); } }
        public IList<IWebElement> GapDeleteBtn { get { return this.FindElements(Bys.Step2Page.GapDeleteBtn); } }
        public IWebElement OtherDataToAccessYourPracticeNeeds_Label { get { return this.FindElement(Bys.Step2Page.OtherDataToAccessYourPracticeNeeds_Label); } }
        public IWebElement OtherDataTxt { get { return this.FindElement(Bys.Step2Page.OtherDataTxt); } }
        public IList<IWebElement> GapEditBtn { get { return this.FindElements(Bys.Step2Page.GapEditBtn); } }






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
            this.WaitUntil(TimeSpan.FromSeconds(60), Criteria.Step2Page.PageReady);
            Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(60));
            Thread.Sleep(500);
            this.WaitUntil(TimeSpan.FromSeconds(60), Criteria.Step2Page.PageReady);
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
            if (Browser.Exists(Bys.Step2Page.GapsContinueBtn))
            {
                if (elem.GetAttribute("outerHTML") == GapsContinueBtn.GetAttribute("outerHTML"))
                {
                    GapsContinueBtn.Click();
                    this.WaitForInitialize();
                    return null;
                }
            }
            if (Browser.Exists(Bys.Step2Page.GapNextBtn))
            {
                if (elem.GetAttribute("outerHTML") == GapNextBtn.GetAttribute("outerHTML"))
                {
                    GapNextBtn.Click();
                    Browser.WaitForElement(Bys.Step2Page.GapsContinueBtn, ElementCriteria.IsVisible);
                    this.WaitForInitialize();
                    return null;
                }
            }

            if (Browser.Exists(Bys.Step2Page.NextBtn))
            {
                if (elem.GetAttribute("outerHTML") == NextBtn.GetAttribute("outerHTML"))
                {

                    try {
                        
                        NextBtn.Click(); //NextBtn.ClickJS(Browser);
                                           } catch
                    {
                        ElemSet.ClickAfterScroll(Browser, NextBtn);
                        Console.WriteLine("catch Next "); }
                    //NextBtn.Click();
                    this.WaitForInitialize();

                    if (PLP_Header_StepNumberLabel.Text.Contains("Step 2"))
                    {
                        return this;
                    }
                    else
                    {
                        Step3Page page = new Step3Page(Browser);
                        page.WaitForInitialize();
                        return page;
                    }
                }
            }
            if (Browser.Exists(Bys.Step2Page.GapScreenViewModeNextBtn))
            {
                if (elem.GetAttribute("outerHTML") == GapScreenViewModeNextBtn.GetAttribute("outerHTML"))
                {

                    try
                    {

                        GapScreenViewModeNextBtn.Click(); //NextBtn.ClickJS(Browser);
                    }
                    catch
                    {
                        ElemSet.ClickAfterScroll(Browser, GapScreenViewModeNextBtn);
                        Console.WriteLine("catch GapScreenViewModeNextBtn ");
                    }
                    //NextBtn.Click();
                    this.WaitForInitialize();

                    if (PLP_Header_StepNumberLabel.Text.Contains("Step 2"))
                    {
                        return this;
                    }
                    else
                    {
                        Step3Page page = new Step3Page(Browser);
                        page.WaitForInitialize();
                        return page;
                    }
                }
            }
            if (Browser.Exists(Bys.Step2Page.BackBtn))
            {
                if (elem.GetAttribute("outerHTML") == BackBtn.GetAttribute("outerHTML"))
                {
                    BackBtn.Click();
                    this.WaitForInitialize();

                    if (PLP_Header_StepNumberLabel.Text.Contains("Step 2"))
                    {
                        return this;
                    }
                    else
                    {
                        Step3Page page = new Step3Page(Browser);
                        page.WaitForInitialize();
                        return page;
                    }
                }
            }
            if (Browser.Exists(Bys.Step2Page.Gap1DeleteBtn))
            {
                if (elem.FindElement(By.XPath("./parent::div")).GetAttribute("outerHTML") == 
                    Gap1DeleteBtn.FindElement(By.XPath("./parent::div")).GetAttribute("outerHTML"))
                {
                    Gap1DeleteBtn.Click();
                    Browser.WaitJSAndJQuery();
                    Thread.Sleep(30);
                    this.WaitUntil(Criteria.Step2Page.AreYouSureYouWantToDeleteFormDeleteBtnVisible);
                    PLP_AreYouSureYouWantToDeleteFormDeleteBtn.ClickJS(Browser);
                    this.WaitUntil(Criteria.Step2Page.AreYouSureYouWantToDeleteFormDeleteBtnNotVisible);
                    return null;
                }
            } if (Browser.Exists(Bys.Step2Page.Gap2DeleteBtn))
            {
                if (elem.FindElement(By.XPath("./parent::div")).GetAttribute("outerHTML") ==
                    Gap2DeleteBtn.FindElement(By.XPath("./parent::div")).GetAttribute("outerHTML"))
                {
                    Gap2DeleteBtn.Click(); 
                    Browser.WaitJSAndJQuery();
                    Thread.Sleep(30);
                    this.WaitUntil(Criteria.Step2Page.AreYouSureYouWantToDeleteFormDeleteBtnVisible);
                    PLP_AreYouSureYouWantToDeleteFormDeleteBtn.ClickJS(Browser);
                    this.WaitUntil(Criteria.Step2Page.AreYouSureYouWantToDeleteFormDeleteBtnNotVisible);
                    return null;
                }
            }

            if (Browser.Exists(Bys.Step2Page.AdditionalGapBtn))
            {
                if (elem.GetAttribute("outerHTML") == AdditionalGapBtn.GetAttribute("outerHTML"))
                {
                    // Get the count of visible Gap elements, click the button, then wait for the additional gap button 
                    // to be visible
                    int initialGapBtnCount = Browser.FindElements(By.XPath(
                        "//div[contains(@class, 'form-input-Gap') and not(contains(@class, 'form-input-skip'))]//p[contains(@name, 'Gap')]"))
                        .Count;

                    AdditionalGapBtn.Click();
                   // AdditionalGapBtn.Click();

                    var wait = new WebDriverWait(Browser, TimeSpan.FromSeconds(15));
                    return wait.Until(drv => drv.FindElements(By.XPath(
                        "//div[contains(@class, 'form-input-Gap') and not(contains(@class, 'form-input-skip'))]//p[contains(@name, 'Gap')]"))
                    .Count == initialGapBtnCount + 1);
                }
            }

            throw new Exception(string.Format("No element was found with your passed parameter, which was the '{0}' element. " +
                "You either need to add this element to a new If statement, or if the element is already added, then the page " +
                "you were on did not contain the element.",
                elem.GetAttribute("innerText")));
        }

        /// <summary>
        /// Selects an item from a user-specified select element, then waits for a criteria to load fully
        /// </summary>
        /// <param name="selectElement">The select element to manipulate</param>
        /// <param name="selection">The exact text you want to choose from the item in the select elements</param>
        /// <returns></returns>
        public dynamic SelectAndWait(SelectElement selectElement, string selection)
        {
            if (Browser.Exists(Bys.Step2Page.SelectDomainOfCareSelElem))
            {
                if (selectElement.Options[1].GetAttribute("text") == 
                    SelectDomainOfCareSelElem.Options[1].GetAttribute("text"))
                {
                    if (Browser.MobileEnabled() || Browser.GetCapabilities().GetCapability("browserName").ToString() ==
                        BrowserNames.Firefox)
                    {
                        ElemSet.DropdownMulti_Fireball_SelectByText(Browser, SelectDomainOfCareSelElemBtn, selection);
                    }
                    else
                    {
                        foreach(IWebElement option in SelectDomainOfCareSelElem.Options)
                        {
                            if (option.GetAttribute("text").Contains(selection))
                            {
                                option.Click();
                                break;
                            }
                        }
                       
                   }
                    this.WaitForInitialize();
                    return null;
                }
            }

            if (Browser.Exists(Bys.Step2Page.SelectSubsetsSelElem))
            {
                if (selectElement.Options[1].GetAttribute("text")
                    == SelectSubsetsSelElem.Options[1].GetAttribute("text"))
                {
                    if (Browser.MobileEnabled() || Browser.GetCapabilities().GetCapability("browserName").ToString() ==
                        BrowserNames.Firefox)
                    {
                        ElemSet.DropdownMulti_Fireball_SelectByText(Browser, SelectSubsetsSelElemBtn, selection);
                    }
                    else
                    {
                        foreach (IWebElement option in SelectSubsetsSelElem.Options)
                        {
                            if (option.GetAttribute("text").Contains(selection))
                            {
                                option.Click();
                                break;
                            }
                        }
                       // SelectSubsetsSelElem.SelectByText(selection);
                    }
                    this.WaitForInitialize();
                    return null;
                }
            }

            throw new Exception(string.Format("No element was found with your passed parameter. You either need to add " +
                "this element to a new If statement, or if the element is already added, then the page you were on did not contain the element."));
        }

        #endregion methods: per page

        #region methods: page specific

        

        public PLP_DomainsSelection SelectPrimaryandSubDomains(string primarydomain=null, List<string> subdomains
            =null)
        {
            if (primarydomain.IsNullOrEmpty())
            {
                primarydomain = "QA/QI";
                subdomains = new List<string>
                {
                    "Peer review",
                    "Performance appraisal",
                    "Self practice audit"
                };
            }

                SelectAndWait(SelectDomainOfCareSelElem, primarydomain);
                Thread.Sleep(50);
            foreach (string value in subdomains){
                SelectAndWait(SelectSubsetsSelElem, value);
                Thread.Sleep(50);
            }
            return new PLP_DomainsSelection(primarydomain, subdomains);
        }

        public void AddGaps(int numOfGaps, String TitleOfGap1 = "Testing gap 1",
            String TitleOfGap2 = "Testing gap 2", String TitleOfGap3 = "Testing gap 3")
        {
            AddGap1(TitleOfGap1);
           
            if (numOfGaps <=3 && numOfGaps!=1)
            {
                ClickAndWait(AdditionalGapBtn);
                AddGap2(TitleOfGap2);
            }
            if (numOfGaps == 3)
            {
                ClickAndWait(AdditionalGapBtn);
                AddGap3(TitleOfGap3);
            }

        }
        public void AddGap1(String TitleOfGap1 = "Testing gap 1")
        {
            Help.PLP_AddFormattedText(Browser, TitleOfGap1,
                Const_Mainpro.PLP_TextboxlabelText.Step2Gap1TitleTxt);
            Thread.Sleep(50);
        }
        public void AddGap2(String TitleOfGap2 = "Testing gap 2")
        {
            Help.PLP_AddFormattedText(Browser, TitleOfGap2,
                Const_Mainpro.PLP_TextboxlabelText.Step2Gap2TitleTxt);
            Thread.Sleep(50);
        }
         public void AddGap3(String TitleOfGap3 = "Testing gap 3")
        {
            Help.PLP_AddFormattedText(Browser, TitleOfGap3,
                Const_Mainpro.PLP_TextboxlabelText.Step2Gap3TitleTxt);
            Thread.Sleep(50);
        }
        
            

        #endregion methods: page specific



    }
}
