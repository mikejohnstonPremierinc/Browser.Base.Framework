using Browser.Core.Framework;
using LMS.Data;

namespace LMSAdmin.AppFramework
{
    public class DistributionPageCriteria
    {
        public readonly ICriteria<DistributionPage> CatalogsLnkVisible = new Criteria<DistributionPage>(p =>
        {
            return p.Exists(Bys.DistributionPage.CatalogsLnk, ElementCriteria.IsVisible);

        }, "Catalog Link visible");

        public readonly ICriteria<DistributionPage> PortalsLnkVisible = new Criteria<DistributionPage>(p =>
        {
            return p.Exists(Bys.DistributionPage.PortalsLnk, ElementCriteria.IsVisible);

        }, "Portals Link visible");

        public readonly ICriteria<DistributionPage> TCLnkVisible = new Criteria<DistributionPage>(p =>
        {
            return p.Exists(Bys.Page.TermsAndConditionsLnk, ElementCriteria.IsVisible);

        }, "AddnewPortals Link visible");

        public readonly ICriteria<DistributionPage> PageReady;
        
        public DistributionPageCriteria()
        {
            PageReady = CatalogsLnkVisible;
        }
    }
}
