using Browser.Core.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using Microsoft.CSharp;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using System;
using OpenQA.Selenium.Interactions;

namespace Wikipedia.AppFramework
{
    /// <summary>
    /// This is the base page for the HTML website. It contains all elements that appear on EVERY page. So these elements are not specific to 1 page. 
    /// It also includes methods can be called which will work on every page. It extends the Page class inside Browser.Core.Framework.
    /// </summary>
    public abstract class WikipediaBasePage : Page
    {
        #region Constructors

        /// <summary>
        /// You will need this constructor for every page class that you create
        /// </summary>
        /// <param name="driver"></param>
        public WikipediaBasePage(IWebDriver driver) : base(driver) { }

        #endregion

        #region Elements       

        /// <summary>
        /// We are retreiving these elements from the PageBy class. Specifically, <see cref="WikipediaBasePageBys"/>. That class is 
        /// where we locate all elements by using the By type (xpath, id's, class name, linktext, etc.). Once you locate a new 
        /// element inside a PageBy class, you then need to return it inside the respective Page class, as shown below.
        /// </summary>
        public IWebElement SearchTxt { get { return this.FindElement(Bys.WikipediaBasePage.SearchTxt); } }
        public IWebElement HelpLnk { get { return this.FindElement(Bys.WikipediaBasePage.HelpLnk); } }
        public IWebElement VectorMainMenuBtn { get { return this.FindElement(Bys.WikipediaBasePage.VectorMainMenuBtn); } }
        public IWebElement SearchBtn { get { return this.FindElement(Bys.WikipediaBasePage.SearchBtn); } }
        public IWebElement LogoImg { get { return this.FindElement(Bys.WikipediaBasePage.LogoImg); } }
        public IWebElement ContributionsLnk { get { return this.FindElement(Bys.WikipediaBasePage.ContributionsLnk); } }

        #endregion Elements

        #region methods

        /// <summary>
        /// Clicks the user-specified button, link, tab, etc. and then waits for a window/element to close or open, or a page to load,
        /// depending on the element that was clicked. Once the Wait Criteria is satisfied, the test continues, and the method returns
        /// either a new Page class instance or nothing at all (hence the 'dynamic' return type). This specific method is for Base page
        /// elements. You should also include this method inside each specific (non-base) page class
        /// </summary>
        /// <param name="elemToClick">The element to click on</param>
        public dynamic ClickAndWaitBasePage(IWebElement elemToClick)
        {
            // Error handler to make sure that the button that the tester passed in the parameter is actually on the page
            if (Browser.Exists(Bys.WikipediaBasePage.SearchBtn))
            {
                // If statement to get executed if the element that the tester passed in the parameter (left side element)
                // matches the hard coded (right side element) element
                if (elemToClick.GetAttribute("outerHTML") == SearchBtn.GetAttribute("outerHTML"))
                {
                    // If all If statements passed above, then we click the element
                    SearchBtn.Click();

                    // We then add the Wait Criteria below the click. In this specific example, if we click the Search button, 
                    // then a brand new page loads. So we can just instantiate that page class, then use it's WaitForInitialize
                    // method, then return that Page object, and the method is then completed. 
                    SearchResultsPage Page = new SearchResultsPage(Browser);
                    Page.WaitForInitialize();
                    return Page;

                    // Below are other examples of how to use Wait Criteria. You can see Wait Criteria being used in this project 
                    // in other Page classes, or at the test class level, but below I will give you some examples
                    // Example #1: Above, I instantiate SearchResultsPage, which allows me to call the WaitUntil method, which
                    // lives in Browser.Core.Framework at the very lowest Base Page class of this framework. I can call this 
                    // low level base class method because this WikipediaBasePage class extends that Base Page class (Scroll to 
                    // the top of this class and you will see that it extends 'Page'). I included the optional TimeSpan 
                    // parameter, which allows me to specify a timeout in seconds before the test should fail if the Wait 
                    // Criteria is not met
                    //Page.WaitUntil(TimeSpan.FromSeconds(60), Criteria.SearchResultsPage.HelpContentsLnkVisible);

                    // Example #2: Similar to Example #1, but I use WaitUntilAll to combine Wait Criteria from the 
                    // SearchResultsPageCriteria class. All criteria below needs to be met before the default timeout or
                    // the test will fail
                    //Page.WaitUntilAll(TimeSpan.FromSeconds(10), Criteria.SearchResultsPage.LogoImgVisibleAndEnabled, 
                    //Criteria.SearchResultsPage.HelpContentsLnkVisible, Criteria.SearchResultsPage.SearchTxtVisible);

                    // Example #3: Similar to Example #1 and #2, but I use WaitUntilAny. This means that only 1 of the Criteria 
                    // needs to be met for the test to proceed.
                    // Page.WaitUntilAny(Criteria.SearchResultsPage.LogoImgVisibleAndEnabled,
                    //Criteria.SearchResultsPage.HelpContentsLnkVisible);

                    // Example #4: We can use the WaitForElement method, which lets us bypass the SearchResultsPageCriteria 
                    // class, and directly call an element to wait for in the SearchResultsPageBys class. This is useful in 
                    // some scenarios, but ideally we want to use the Criteria classes because we can give custom 
                    // exception messages in that class, and also because we will have 1 central location for all of the 
                    // waiting for a given page
                    // Page.WaitForElement(Bys.HomePage.WikipediaImg,
                    // TimeSpan.FromSeconds(3), ElementCriteria.IsVisible, ElementCriteria.IsEnabled);

                    // Example #5: We can use the WaitForElement method in combination with a custom/dynamic By type 
                    // (Specifically, a By type that doesnt exist inside SearchResultsPage, because it is a dynamic element, 
                    // so we can not statically store the locator)
                    // Page.WaitForElement(By.XPath("SomeDynamicXpath"),
                    // TimeSpan.FromSeconds(3), ElementCriteria.IsVisible, ElementCriteria.IsEnabled);

                    // Example #6: We can use the WaitForElement method from the Browser object instead of the Page object
                    // Browser.WaitForElement(By.XPath("SomeDynamicXpath"),
                    // TimeSpan.FromSeconds(3), ElementCriteria.IsVisible, ElementCriteria.IsEnabled);

                }
            }

            // Repeat the same If statements for each new element that you want to add in this method
            if (Browser.Exists(Bys.WikipediaBasePage.LogoImg))
            {
                if (elemToClick.GetAttribute("outerHTML") == LogoImg.GetAttribute("outerHTML"))
                {
                    LogoImg.Click();
                    HomePage HP = new HomePage(Browser);
                    HP.WaitForInitialize();
                    return HP;
                }
            }

            if (Browser.Exists(Bys.WikipediaBasePage.VectorMainMenuBtn))
            {
                if (elemToClick.GetAttribute("outerHTML") == VectorMainMenuBtn.GetAttribute("outerHTML"))
                {
                    VectorMainMenuBtn.Click();
                    Browser.WaitForElement(Bys.WikipediaBasePage.HelpLnk, ElementCriteria.IsVisible);
                    return null;
                }
            }

            if (Browser.Exists(Bys.WikipediaBasePage.ContributionsLnk))
            {
                if (elemToClick.GetAttribute("outerHTML") == ContributionsLnk.GetAttribute("outerHTML"))
                {
                    elemToClick.Click();
                    ContributionsPage page = new ContributionsPage(Browser);
                    page.WaitForInitialize();
                    return page;
                }
            }

            throw new Exception(string.Format("No element was found with your passed parameter, which was the '{0}' element. You either need to add " +
                "this element to a new If statement, or if the element is already added, then the page you were on did not contain the element.",
                elemToClick.GetAttribute("innerText")));
        }


        /// <summary>
        /// Enters the user-specified text into the Search text box, clicks on the Search button, then waits for the Search Results
        /// page to load
        /// </summary>
        /// <param name="searchTxt">The text you want to enter into the Search text box</param>
        /// <returns></returns>
        public SearchResultsPage Search(string searchTxt)
        {
            // Review the naming convention I have used for the Search text box in this line of code. I spelled out the label of
            // the control "Search", then I appended "Txt" onto the end of it. When naming elements, name them according 
            // to our Standard Naming Convention as explained here:
            // https://code.premierinc.com/docs/display/PQA/PageBy+Class+and+Naming+Conventions
            SearchTxt.SendKeys(searchTxt);
            //SearchTxt.SendKeys(Keys.Tab);
            // Mike Johnston 4/1/2021: For some reason I need to add a static sleep here or the search button becomes stale
            // within the ClickAndWaitBasePage method. If you are onboarding/training, ignore this comment. Will have to 
            // investigate when I get time and then implement a dynamic wait instead
            Thread.Sleep(2000);

            // When we click on buttons, links, etc., that result in us having to wait for a page to load, or a window/element
            // to appear/disappear, we want to use the methods clickAndWait (for specific pages) and ClickAndWaitBasePage
            // (for the base page), so that all waiting is handled inside that method. This reduces redundant lines of code, 
            // and also ensures that we wont have to spend time figuring out what to wait for whenever we click certain 
            // elements, since it will already be coded for us inside the ClickAndwait/ClickAndWaitBasePage method.
            // Go to this method for more details
            return ClickAndWaitBasePage(SearchBtn);
        }

        #endregion methods
    }
}