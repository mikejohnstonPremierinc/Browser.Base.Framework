using Browser.Core.Framework;

namespace Wikipedia.AppFramework
{
    /// <summary>
    /// We can define various different criteria to wait for in this class. For example, we can wait for a Select Element to have items, or wait
    /// for an element to be visible, or to not be visible (wait for it to disappear). We then used this defined criteria in our Page classes or
    /// our Test classes. Note that this is a lot of copy and paste, and then modifying that paste to your specific element and the ElementCriteria
    /// that you want to use. Also make sure to add the custom exception messages as shown below. 
    /// </summary>
    public class SearchResultsPageCriteria
    {
        /// <summary>
        /// To get an understanding of the way this works, if this Wait Criteria fails, then the exception will say "Timed out waiting for 1 
        /// Criteria 'Help:Contents link exists and is visible' (False)"
        /// </summary>
        public readonly ICriteria<SearchResultsPage> HelpContentsLnkVisible = new Criteria<SearchResultsPage>(p =>
        {
            return p.Exists(Bys.SearchResultsPage.HelpContentsLnk, ElementCriteria.IsVisible);

        }, "Help:Contents link exists and is visible");

        public readonly ICriteria<SearchResultsPage> SearchTxtVisible = new Criteria<SearchResultsPage>(p =>
        {
            return p.Exists(Bys.WikipediaBasePage.SearchTxt, ElementCriteria.IsVisible);

        }, "Search text box exists and is visible");

        public readonly ICriteria<SearchResultsPage> LogoImgVisibleAndEnabled = new Criteria<SearchResultsPage>(p =>
        {
            return p.Exists(Bys.WikipediaBasePage.LogoImg, ElementCriteria.IsVisible, ElementCriteria.IsEnabled);

        }, "Logo Image visible and enabled");

        /// <summary>
        /// We can define a PageReady property, which can be used in the WaitForInitialize method inside a given Page class, to wait for the entire
        /// page to be loaded. Note below that I am using the AND condition to combine the 2 criteria above. <see cref="SearchResultsPage.WaitForInitialize"/>
        /// If only 1 of the Criteria fails to be met below, then the exception will say "Timed out waiting for 1 Criteria: 
        /// 'Help:Contents link exists and is visible' (False) AND 'Search text box visible' (True)"
        /// </summary>

        public readonly ICriteria<SearchResultsPage> PageReady;
        public SearchResultsPageCriteria()
        {
            PageReady = SearchTxtVisible.AND(LogoImgVisibleAndEnabled);
        }
    }
}
