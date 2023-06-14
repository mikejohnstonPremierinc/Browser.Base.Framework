using Browser.Core.Framework;
using LMS.Data;

namespace LMSAdmin.AppFramework
{
    public class Legacy_SetupPageCriteria
    {
        public readonly ICriteria<Legacy_SetupPage> OrganizationSettingsLblVisible = new Criteria<Legacy_SetupPage>(p =>
        {
            return p.Exists(Bys.Legacy_SetupPage.OrganizationSettingsLbl, ElementCriteria.IsVisible);

        }, "OrganizationSettings label visible");


        public readonly ICriteria<Legacy_SetupPage> PageReady;

        public Legacy_SetupPageCriteria()
        {
            PageReady = OrganizationSettingsLblVisible;
        }
    }
}
