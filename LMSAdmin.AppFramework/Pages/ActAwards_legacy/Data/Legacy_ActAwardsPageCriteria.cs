using Browser.Core.Framework;
using LMS.Data;

namespace LMSAdmin.AppFramework
{
    public class Legacy_ActAwardsPageCriteria
    {
        public readonly ICriteria<Legacy_ActAwardsPage> AwardsTblBodyVisible = new Criteria<Legacy_ActAwardsPage>(p =>
        {
            return p.Exists(Bys.Legacy_ActAwardsPage.AwardsTblBody, ElementCriteria.IsVisible);

        }, "Awards table body visible");

        public readonly ICriteria<Legacy_ActAwardsPage> PageReady;
        public Legacy_ActAwardsPageCriteria()
        {
            PageReady = AwardsTblBodyVisible;
        }
    }
}
