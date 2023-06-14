using Browser.Core.Framework;

namespace Wikipedia.AppFramework
{
    /// <summary>
    /// We can define various different criteria to wait for in this class. For example, we can wait for a Select Element to have items, or wait
    /// for an element to be visible, or to not be visible (wait for it to disappear). We then used this defined criteria in our Page classes or
    /// our Test classes. Note that this is a lot of copy and paste, and then modifying that paste to your specific element and the ElementCriteria
    /// that you want to use. Also make sure to add the custom exception messages as shown below. 
    /// </summary>
    public class ContributionsPageCriteria
    {
        /// <summary>
        /// To get an understanding of the way this works, if this Wait Criteria fails, then the exception will say "Timed out waiting for 1 
        /// Criteria 'Table Of Contents table visible' (False)"
        /// </summary>
        public readonly ICriteria<ContributionsPage> SearchForContributionsLblVisible = new Criteria<ContributionsPage>(p =>
        {
            return p.Exists(Bys.ContributionsPage.SearchForContributionsLbl, ElementCriteria.IsVisible);

        }, "Search for contributions label exists and is visible");


        /// <summary>
        /// We can define a PageReady property, which can be used in the WaitForInitialize method inside a given Page class, to wait for the entire
        /// page to be loaded. Note below that I am using the AND condition to combine the 2 criteria above. <see cref="HelpPage.WaitForInitialize"/>
        /// If only 1 of the Criteria fails to be met below, then the exception will say "Timed out waiting for 2 Criteria: 'Table Of Contents table visible' (False)
        /// AND 'Search text box visible' (True)"
        /// </summary>
        public readonly ICriteria<ContributionsPage> PageReady;
        public ContributionsPageCriteria()
        {
            PageReady = SearchForContributionsLblVisible;
        }
    }
}
