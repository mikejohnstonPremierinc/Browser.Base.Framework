using Browser.Core.Framework;
using LMS.Data;

namespace LMSAdmin.AppFramework
{
    public class Distribution_PortalsPageCriteria
    {
        public readonly ICriteria<Distribution_PortalsPage> TCLnkVisible = new Criteria<Distribution_PortalsPage>(p =>
        {
            return p.Exists(Bys.Page.TermsAndConditionsLnk, ElementCriteria.IsVisible);

        }, "Catalog Link visible");

        public readonly ICriteria<Distribution_PortalsPage> CatAndActTabSelCatalogTblVisible = new Criteria<Distribution_PortalsPage>(p =>
        {
            return p.Exists(Bys.Distribution_PortalsPage.CatAndActTabSelCatalogTbl, ElementCriteria.IsVisible);

        }, "Catalogs And Activities tab, Selected Catalogs table visible");

        public readonly ICriteria<Distribution_PortalsPage> CatAndActTabVisible = new Criteria<Distribution_PortalsPage>(p =>
        {
            return p.Exists(Bys.Distribution_PortalsPage.CatAndActTab, ElementCriteria.IsVisible);

        }, "Catalogs And Activities tab visible");

        public readonly ICriteria<Distribution_PortalsPage> PageReady;

        public Distribution_PortalsPageCriteria()
        {
            PageReady = TCLnkVisible.AND(CatAndActTabVisible);
        }
    }
}
