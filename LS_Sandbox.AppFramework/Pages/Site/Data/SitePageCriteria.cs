using Browser.Core.Framework;

namespace LS.AppFramework
{
    public class SitePageCriteria
    {
        public readonly ICriteria<SitePage> AdditionalInfoTabVisible = new Criteria<SitePage>(p =>
        {
            return p.Exists(Bys.SitePage.AdditionalInfoTab, ElementCriteria.IsVisible);

        }, "Additional Information tab visible");


        public readonly ICriteria<SitePage> PageReady;

        public SitePageCriteria()
        {
            PageReady = AdditionalInfoTabVisible;
        }
    }
}
