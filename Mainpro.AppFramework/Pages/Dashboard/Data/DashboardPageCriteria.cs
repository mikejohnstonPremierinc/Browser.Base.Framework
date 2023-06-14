using Browser.Core.Framework;


namespace Mainpro.AppFramework
{
    public class DashboardPageCriteria
    {
        public readonly ICriteria<DashboardPage> ClickHereToViewAMARCPCreditsLnkVisible = new Criteria<DashboardPage>(p =>
        {
            return p.Exists(Bys.DashboardPage.ClickHereToViewAMARCPCreditsLnk, ElementCriteria.IsVisible);

        }, "Click Here To View Your Certified AMA... label visible");

        public readonly ICriteria<DashboardPage> ViewDetailsBtnEnabled = new Criteria<DashboardPage>(p =>
        {
            return p.Exists(Bys.MainproPage.ViewDetailsBtn, ElementCriteria.IsEnabled);

        }, "ViewDetails Button Enabled ");

        public readonly ICriteria<DashboardPage> EnterACPDActivityOldUIBtnEnabled = new Criteria<DashboardPage>(p =>
        {
            return p.Exists(Bys.DashboardPage.EnterCPDActBtn_OldUI, ElementCriteria.IsEnabled);

        }, "Enter A CPD Activity (Old UI) Button enabled");

        public readonly ICriteria<DashboardPage> iAcceptBtnNotVisible = new Criteria<DashboardPage>(p =>
        {
            return p.Exists(Bys.LoginPage.iAcceptBtn, ElementCriteria.IsNotVisible);

        }, "I Accept button not visible");

        public readonly ICriteria<DashboardPage> iAcceptBtnNotExists = new Criteria<DashboardPage>(p =>
        {
            return !p.Exists(Bys.LoginPage.iAcceptBtn);

        }, "I Accept button not exists");

        public readonly ICriteria<DashboardPage> LoadIconNotExists = new Criteria<DashboardPage>(p =>
        {
            return !p.Exists(Bys.MainproPage.LoadIcon);

        }, "Load icon not exists");

        public readonly ICriteria<DashboardPage> LoadIconOverlayNotExists = new Criteria<DashboardPage>(p =>
        {
            return !p.Exists(Bys.MainproPage.LoadIconOverlay);

        }, "Load overlay not exists");

        public readonly ICriteria<DashboardPage> WereSorryErrorLblNotExists = new Criteria<DashboardPage>(p =>
        {
            return !p.Exists(Bys.MainproPage.WereSorryErrorLbl);

        }, "We're sorry, we cannot retrieve your information label not exists");

        public readonly ICriteria<DashboardPage> PageReady;

        public DashboardPageCriteria()
        {
            PageReady = (ViewDetailsBtnEnabled.AND(iAcceptBtnNotExists).AND(LoadIconNotExists).
                AND(LoadIconOverlayNotExists).AND(WereSorryErrorLblNotExists)).
                OR(EnterACPDActivityOldUIBtnEnabled.AND(iAcceptBtnNotVisible).AND(LoadIconNotExists).
                AND(LoadIconOverlayNotExists).AND(WereSorryErrorLblNotExists));
        }

    }
}
