using Browser.Core.Framework;

namespace LMS.AppFramework
{
    public class SearchPageCriteria
    {
        public readonly ICriteria<SearchPage> NoDataAvailableLblVisible = new Criteria<SearchPage>(p =>
        {
            return p.Exists(Bys.SearchPage.NoDataAvailableLbl, ElementCriteria.IsVisible);

        }, "Activity table, No Data Available label visible");

        public readonly ICriteria<SearchPage> SearchResultsTblBodyActivityLnksVisiblAndHasText = new Criteria<SearchPage>(p =>
        {
            return p.Exists(Bys.SearchPage.SearchResultsTblBodyActivityLnks, ElementCriteria.IsVisible, ElementCriteria.HasText);

        }, "Activity table, first activity link visible and has text");

        public readonly ICriteria<SearchPage> PageReady;

        public SearchPageCriteria()
        {
            PageReady = NoDataAvailableLblVisible.OR(SearchResultsTblBodyActivityLnksVisiblAndHasText);
        }
    }
}
