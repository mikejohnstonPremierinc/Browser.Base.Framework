using Browser.Core.Framework;


namespace Mainpro.AppFramework
{
    public class HoldingAreaPageCriteria
    {
        public readonly ICriteria<HoldingAreaPage> CreditValTabVisible = new Criteria<HoldingAreaPage>(p =>
        {
            return p.Exists(Bys.HoldingAreaPage.CredValTab, ElementCriteria.IsVisible);

        }, "Credit Validation Tab visible");

        public readonly ICriteria<HoldingAreaPage> LoadIconNotExists = new Criteria<HoldingAreaPage>(p =>
        {
            return !p.Exists(Bys.MainproPage.LoadIcon);

        }, "Load icon not exists");

        public readonly ICriteria<HoldingAreaPage> LoadIconOverlayNotExists = new Criteria<HoldingAreaPage>(p =>
        {
            return !p.Exists(Bys.MainproPage.LoadIconOverlay);

        }, "Load overlay not exists");

        public readonly ICriteria<HoldingAreaPage> WereSorryErrorLblNotExists = new Criteria<HoldingAreaPage>(p =>
        {
            return !p.Exists(Bys.MainproPage.WereSorryErrorLbl);

        }, "We're sorry, we cannot retrieve your information label not exists");


        public readonly ICriteria<HoldingAreaPage> PageReady;

        public HoldingAreaPageCriteria()
        {
            PageReady = CreditValTabVisible.AND(LoadIconNotExists).
                AND(LoadIconOverlayNotExists).AND(WereSorryErrorLblNotExists);
        }
    }
}
