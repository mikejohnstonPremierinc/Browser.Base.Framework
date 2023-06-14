using Browser.Core.Framework;
using LMS.Data;

namespace LMSAdmin.AppFramework
{
    public class Legacy_ActAccreditationPageCriteria
    {
        public readonly ICriteria<Legacy_ActAccreditationPage> AddAccreditationTypeLnkVisible = new Criteria<Legacy_ActAccreditationPage>(p =>
        {
            return p.Exists(Bys.Legacy_ActAccreditationPage.AddAccreditationTypeLnk, ElementCriteria.IsVisible);

        }, "Add Accreditation Type Link visible");

        public readonly ICriteria<Legacy_ActAccreditationPage> PageReady;
        public Legacy_ActAccreditationPageCriteria()
        {
            PageReady = AddAccreditationTypeLnkVisible;
        }
    }
}
