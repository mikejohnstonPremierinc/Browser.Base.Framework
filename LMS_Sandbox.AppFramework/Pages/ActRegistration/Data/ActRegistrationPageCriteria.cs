using Browser.Core.Framework;

namespace LMS.AppFramework
{
    public class ActRegistrationPageCriteria
    {
        public readonly ICriteria<ActRegistrationPage> VerifyYourProfessionFormSubmitBtnVisible = new Criteria<ActRegistrationPage>(p =>
        {
            return p.Exists(Bys.LMSPage.VerifyYourProfessionFormSubmitBtn, ElementCriteria.IsVisible);

        }, "Verify Your Profession form Submit button visible");

        public readonly ICriteria<ActRegistrationPage> FirstNameTxtVisible = new Criteria<ActRegistrationPage>(p =>
        {
            return p.Exists(Bys.ActRegistrationPage.FirstNameTxt, ElementCriteria.IsVisible);

        }, "First Name text box visible");

        public readonly ICriteria<ActRegistrationPage> VerifyYourProfessionFormSubmitBtnNotExists = new Criteria<ActRegistrationPage>(p =>
        {
            return !p.Exists(Bys.LMSPage.VerifyYourProfessionFormSubmitBtn);

        }, "Verify your profession form, Submit button not exists");

        public readonly ICriteria<ActRegistrationPage> StateProvinceSelElemHasMoreThan1Item = new Criteria<ActRegistrationPage>(p =>
        {
            return p.Exists(Bys.ActRegistrationPage.StateProvinceSelElem, ElementCriteria.SelectElementHasMoreThan1Item);

        }, "State Province select element has more than 1 item");

        public readonly ICriteria<ActRegistrationPage> AreYouCHESNoRdoVvisibledAndEnabled = new Criteria<ActRegistrationPage>(p =>
        {
            return p.Exists(Bys.ActRegistrationPage.AreYouCHESNoRdo, ElementCriteria.IsVisible, ElementCriteria.IsEnabled);

        }, "Are you CHES radio button visible and Enabled");

        public readonly ICriteria<ActRegistrationPage> LoadIconNotExists = new Criteria<ActRegistrationPage>(p =>
        {
            return !p.Exists(Bys.LMSPage.LoadIcon);

        }, "Load icon not exists");

        public readonly ICriteria<ActRegistrationPage> PageReady;

        public ActRegistrationPageCriteria()
        {
            PageReady = FirstNameTxtVisible;
        }
    }
}
