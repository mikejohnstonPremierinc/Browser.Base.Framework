using Browser.Core.Framework;

namespace LMS.AppFramework
{
    public class ActCPEMonitorPageCriteria
    {
        public readonly ICriteria<ActCPEMonitorPage> NABPEProfileIDTxtVisible = new Criteria<ActCPEMonitorPage>(p =>
        {
            return p.Exists(Bys.ActCPEMonitorPage.NABPEProfileIDTxt, ElementCriteria.IsVisible);

        }, "NAPB text box visible");


        public readonly ICriteria<ActCPEMonitorPage> LoadIconNotExists = new Criteria<ActCPEMonitorPage>(p =>
        {
            return !p.Exists(Bys.LMSPage.LoadIcon);

        }, "Load icon not exists");

        public readonly ICriteria<ActCPEMonitorPage> PageReady;

        public ActCPEMonitorPageCriteria()
        {
            PageReady = NABPEProfileIDTxtVisible;
        }
    }
}
