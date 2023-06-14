namespace Wikipedia.AppFramework
{
    /// <summary>
    /// Provides access to all known "Bys" for the application. Bys are used to locate elements
    /// </summary>
    public static class Bys
    {
        public static readonly HomePageBys HomePage = new HomePageBys();
        public static readonly WikipediaBasePageBys WikipediaBasePage = new WikipediaBasePageBys();
        public static readonly SearchResultsPageBys SearchResultsPage = new SearchResultsPageBys();
        public static readonly HelpPageBys HelpPage = new HelpPageBys();
        public static readonly ContributionsPageBys ContributionsPage = new ContributionsPageBys();


        

    }
}