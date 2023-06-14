using Browser.Core.Framework;

namespace LS.AppFramework
{
    public class ProgramPageCriteria
    {
        public readonly ICriteria<ProgramPage> SelfReportActTabValidActivityTblBodyVisible = new Criteria<ProgramPage>(p =>
        {
            return p.Exists(Bys.ProgramPage.SelfReportActTabActivityTblBody, ElementCriteria.IsVisible);

        }, "Self Report Activities tab, Activity table body visible");

        public readonly ICriteria<ProgramPage> ProgramAdjustmentsActivityTblBodyRowVisible = new Criteria<ProgramPage>(p =>
        {
            return p.Exists(Bys.ProgramPage.ProgramAdjustmentsActivityTblBodyRow, ElementCriteria.IsVisible);

        }, "Program Adjustments tab table body row visible");

        public readonly ICriteria<ProgramPage> ProgAdjustTabAddAdjustFormAdjustCodeSelElemVisible = new Criteria<ProgramPage>(p =>
        {
            return p.Exists(Bys.ProgramPage.ProgAdjustTabAddAdjustFormAdjustCodeSelElem, ElementCriteria.IsVisible);

        }, "Program Adjustments tab, Add Adjustment form, Adjustment Code select element visible");

        public readonly ICriteria<ProgramPage> ApplyCarryOverCreditsButtonVisibleAndEnable = new Criteria<ProgramPage>(p =>
        {
            return p.Exists(Bys.ProgramPage.ApplyCarryOverCreditsBtn, ElementCriteria.IsVisible, ElementCriteria.IsEnabled);

        }, "Apply Carry Over Credits button visible and enabled");

        public readonly ICriteria<ProgramPage> RecognitionCarryOverGreenTextlabelAppeared = new Criteria<ProgramPage>(p =>
        {
            return p.Exists(Bys.ProgramPage.RecognitionCarryOverGreenTextLbl, ElementCriteria.IsVisible);

        }, "Appeared");

        public readonly ICriteria<ProgramPage> RecognitionCarryOverGreenTextlabelDisappeared = new Criteria<ProgramPage>(p =>
        {
            return !p.Exists(Bys.ProgramPage.RecognitionCarryOverGreenTextLbl, ElementCriteria.IsVisible);

        }, "Disappeared");

        public readonly ICriteria<ProgramPage> ProgAdjustTabAddAdjustFormNotExists = new Criteria<ProgramPage>(p =>
        {
            return !p.Exists(Bys.ProgramPage.ProgAdjustTabAddAdjustForm);

        }, "Program Adjustments tab, Add Adjustment form not exists");

        public readonly ICriteria<ProgramPage> SelfReportActTabVisible = new Criteria<ProgramPage>(p =>
        {
            return p.Exists(Bys.ProgramPage.SelfReportActTab, ElementCriteria.IsVisible);

        }, "Self Report Activities tab visible");

        public readonly ICriteria<ProgramPage> DetailsTabStatusValueLblVisible = new Criteria<ProgramPage>(p =>
        {
            return p.Exists(Bys.ProgramPage.DetailsTabStatusValueLbl, ElementCriteria.IsVisible);

        }, "Details tab, Status label visible");

        public readonly ICriteria<ProgramPage> GreenBannerNotExists = new Criteria<ProgramPage>(p =>
        {
            return !p.Exists(Bys.Page.GreenBanner);

        }, "Self Reported Activities tab, External Activity Has Been Accepted Banner not exists");

        public readonly ICriteria<ProgramPage> DetailsTabProgramValueLblVisible = new Criteria<ProgramPage>(p =>
        {
        return p.Exists(Bys.ProgramPage.DetailsTabProgramValueLbl, ElementCriteria.IsVisible);

        }, "Details tab, Program label value visible");

        public readonly ICriteria<ProgramPage> PageReady;
        public readonly ICriteria<ProgramPage> ApplyCredits;

        public ProgramPageCriteria()
        {
            //ApplyCredits = RecognitionCarryOverGreenTextlabelAppeared.AND(RecognitionCarryOverGreenTextlabelDisappeared);
            PageReady = SelfReportActTabVisible.AND(DetailsTabStatusValueLblVisible).AND(DetailsTabProgramValueLblVisible);
        }
    }
}
