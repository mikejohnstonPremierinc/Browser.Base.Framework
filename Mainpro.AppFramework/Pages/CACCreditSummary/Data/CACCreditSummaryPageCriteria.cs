using Browser.Core.Framework;


namespace Mainpro.AppFramework
{
    public class CACCreditSummaryPageCriteria
    {

        public readonly ICriteria<CACCreditSummaryPage> EnterACPDActivityBtnEnabled = new Criteria<CACCreditSummaryPage>(p =>
        {
            return p.Exists(Bys.CACCreditSummaryPage.EnterCPDActBtn, ElementCriteria.IsEnabled);

        }, "Enter A CPD Activity Button enabled");
    }
}
