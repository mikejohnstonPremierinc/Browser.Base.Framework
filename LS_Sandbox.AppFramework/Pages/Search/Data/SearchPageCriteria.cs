using Browser.Core.Framework;

namespace LS.AppFramework
{
    public class SearchPageCriteria
    {
        public readonly ICriteria<SearchPage> SitesTblVisible = new Criteria<SearchPage>(p =>
        {
            return p.Exists(Bys.SearchPage.SitesTbl, ElementCriteria.IsVisible);

        }, "Sites table visible");

        public readonly ICriteria<SearchPage> GenericTblBodyNotLoading = new Criteria<SearchPage>(p =>
        {
            return !p.Exists(Bys.SearchPage.GenericTblBody, ElementCriteria.AttributeValue("class", "loading"));

        }, "Generic table body attribute value not equal to \"loading\"");

        public readonly ICriteria<SearchPage> GenericTblBodyRowVisible = new Criteria<SearchPage>(p =>
        {
            return p.Exists(Bys.SearchPage.GenericTblBodyRow, ElementCriteria.IsVisible);

        }, "Generic table, first row visible");

        public readonly ICriteria<SearchPage> AllParticpantsTblBodyLoading = new Criteria<SearchPage>(p =>
        {
            return p.Exists(Bys.SearchPage.AllParticpantsTblBody, ElementCriteria.AttributeValue("class", "loading"));

        }, "All Participants table body attribute value equal to \"loading\"");

        public readonly ICriteria<SearchPage> AllParticpantsTblBodyNotLoading = new Criteria<SearchPage>(p =>
        {
            return !p.Exists(Bys.SearchPage.AllParticpantsTblBody, ElementCriteria.AttributeValue("class", "loading"));

        }, "All Participants table body attribute value not equal to \"loading\"");

        public readonly ICriteria<SearchPage> SearchTxtVisible = new Criteria<SearchPage>(p =>
        {
            return p.Exists(Bys.SearchPage.SearchTxt, ElementCriteria.IsVisible);

        }, "Search Text box visible");

        public readonly ICriteria<SearchPage> PageReady;

        public SearchPageCriteria()
        {
            PageReady = SearchTxtVisible.AND(GenericTblBodyNotLoading).AND(GenericTblBodyRowVisible);
        }
    }
}
