using Browser.Core.Framework;

namespace LMS.AppFramework
{
    public class ActPreviewPageCriteria
    {
        public readonly ICriteria<ActPreviewPage> RegisterBtnVisible = new Criteria<ActPreviewPage>(p =>
        {
            return p.Exists(Bys.ActPreviewPage.RegisterBtn, ElementCriteria.IsVisible);

        }, "Register button visible");

        public readonly ICriteria<ActPreviewPage> LaunchBtnVisible = new Criteria<ActPreviewPage>(p =>
        {
            return p.Exists(Bys.ActPreviewPage.LaunchBtn, ElementCriteria.IsVisible);

        }, "Launch button visible");

        public readonly ICriteria<ActPreviewPage> ResumeBtnVisible = new Criteria<ActPreviewPage>(p =>
        {
            return p.Exists(Bys.ActPreviewPage.ResumeBtn, ElementCriteria.IsVisible);

        }, "Resume button visible");

        public readonly ICriteria<ActPreviewPage> NotAvailableBtnVisible = new Criteria<ActPreviewPage>(p =>
        {
            return p.Exists(Bys.ActPreviewPage.NotAvailableBtn, ElementCriteria.IsVisible);

        }, "Not Available button visible");

        public readonly ICriteria<ActPreviewPage> LoadIconNotExists = new Criteria<ActPreviewPage>(p =>
        {
            return !p.Exists(Bys.LMSPage.LoadIcon);

        }, "Load icon not exists");

        public readonly ICriteria<ActPreviewPage> ActivityTypeLblVisible = new Criteria<ActPreviewPage>(p =>
        {
            return p.Exists(Bys.ActPreviewPage.ActivityTypeLbl, ElementCriteria.IsVisible);

        }, "Activity Type label visible");

        public readonly ICriteria<ActPreviewPage> VerifyYourProfessionFormSubmitBtnVisible = new Criteria<ActPreviewPage>(p =>
        {
            return p.Exists(Bys.LMSPage.VerifyYourProfessionFormSubmitBtn, ElementCriteria.IsVisible);

        }, "Verify Your Profession, Submit button visible");

        public readonly ICriteria<ActPreviewPage> PageReady;

        public ActPreviewPageCriteria()
        {
            PageReady = RegisterBtnVisible.AND(LoadIconNotExists).
                OR(LaunchBtnVisible).AND(LoadIconNotExists).
                 OR(ResumeBtnVisible).AND(LoadIconNotExists).
                    OR(NotAvailableBtnVisible).AND(LoadIconNotExists).
                    OR(VerifyYourProfessionFormSubmitBtnVisible);


            
        }
    }
}
