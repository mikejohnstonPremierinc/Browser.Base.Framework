using Browser.Core.Framework;

namespace LMS.AppFramework
{
    /// <summary>
    /// We can define various different criteria to wait for in this class. For example, we can wait for a Select Element to have items, or wait
    /// for an element to be visible, or to not be visible (wait for it to disappear). We then used this defined criteria in our Page classes or
    /// our Test classes. Note that this is a lot of copy and paste, and then modifying that paste to your specific element and the ElementCriteria
    /// that you want to use. Also make sure to add the custom exception messages as shown below. 
    /// </summary>
    public class HRSAPageCriteria
    {
        /// <summary>
        /// To get an understanding of the way this works, if this Wait Criteria fails, then the exception will say "Timed out waiting for 1 
        /// Criteria 'custom error text shown below' (False)"
        /// </summary>
        public readonly ICriteria<HRSAPage> GeriatricKnowledgeTblFirstLnkVisible = new Criteria<HRSAPage>(p =>
        {
            return p.Exists(Bys.HRSAPage.GeriatricKnowledgeTblFirstLnk, ElementCriteria.IsVisible);

        }, "The first link from the Geriatric Knowledge table exists and is visible");

        public readonly ICriteria<HRSAPage> InterprofessionalEducationTblFirstLnkVisible = new Criteria<HRSAPage>(p =>
        {
            return p.Exists(Bys.HRSAPage.InterprofessionalEducationTblFirstLnk, ElementCriteria.IsVisible);

        }, "The first link from the Interprofessional Education table exists and is visible");

        public readonly ICriteria<HRSAPage> EducationCompetenciesTblFirstLnkVisible = new Criteria<HRSAPage>(p =>
        {
            return p.Exists(Bys.HRSAPage.EducationCompetenciesTblFirstLnk, ElementCriteria.IsVisible);

        }, "The first link from the Education Competencies table exists and is visible");

        public readonly ICriteria<HRSAPage> GeriatricKnowledgeTblViewAllBtnVisible = new Criteria<HRSAPage>(p =>
        {
            return p.Exists(Bys.HRSAPage.GeriatricKnowledgeTblViewAllBtn, ElementCriteria.IsVisible);

        }, "The View All link from the Geriatric Knowledge table exists and is visible");

        public readonly ICriteria<HRSAPage> InterprofessionalEducationTblViewAllBtnVisible = new Criteria<HRSAPage>(p =>
        {
            return p.Exists(Bys.HRSAPage.InterprofessionalEducationTblViewAllBtn, ElementCriteria.IsVisible);

        }, "The View All link from the Interprofessional Education table exists and is visible");

        public readonly ICriteria<HRSAPage> EducationCompetenciesTblViewAllBtnVisible = new Criteria<HRSAPage>(p =>
        {
            return p.Exists(Bys.HRSAPage.EducationCompetenciesTblViewAllBtn, ElementCriteria.IsVisible);

        }, "The View All link from the Education Competencies table exists and is visible");

        public readonly ICriteria<HRSAPage> SearchBtnVisible = new Criteria<HRSAPage>(p =>
        {
            return p.Exists(Bys.LMSPage.SearchTxt, ElementCriteria.IsVisible);

        }, "Search button exists and is visible");

        public readonly ICriteria<HRSAPage> Mobile_SearchMagnifyingGlassBtnVisible = new Criteria<HRSAPage>(p =>
        {
            return p.Exists(Bys.LMSPage.Mobile_SearchMagnifyingGlassBtn, ElementCriteria.IsVisible);

        }, "Search Magnifying button on emulation exists and is visible");

        public readonly ICriteria<HRSAPage> LoadIconNotExists = new Criteria<HRSAPage>(p =>
        {
            return !p.Exists(Bys.LMSPage.LoadIcon);

        }, "Load icon not exists");

        /// <summary>
        /// We can define a PageReady property, which can be used in the WaitForInitialize method inside a given Page class, to wait for the entire
        /// page to be loaded. Note below that I am using the AND condition to combine the 3 criteria above. <see cref="HRSAPage.WaitForInitialize"/>
        /// If only 1 of the Criteria fails to be met below, then the exception will say "Timed out waiting for 3 Criteria:
        /// 'Main Header Lbl visible' (False) AND 'Search text box visible' (True) AND 'Hate HTML form, X button exists and is visible'
        /// </summary>
        public readonly ICriteria<HRSAPage> PageReady;

        public HRSAPageCriteria()
        {
            PageReady = InterprofessionalEducationTblViewAllBtnVisible.AND(GeriatricKnowledgeTblViewAllBtnVisible).
                AND(EducationCompetenciesTblViewAllBtnVisible).AND(LoadIconNotExists);
        }
    }
}
