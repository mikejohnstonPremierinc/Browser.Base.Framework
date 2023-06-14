using Browser.Core.Framework;

namespace LMS.AppFramework
{
    public class RegistrationPageCriteria
    {
        public readonly ICriteria<RegistrationPage> FirstNameTxtVisible = new Criteria<RegistrationPage>(p =>
        {
            return p.Exists(Bys.RegistrationPage.FirstNameTxt, ElementCriteria.IsVisible);

        }, "First Name text box visible");

        public readonly ICriteria<RegistrationPage> LoadIconNotExists = new Criteria<RegistrationPage>(p =>
        {
            return !p.Exists(Bys.LMSPage.LoadIcon);

        }, "Load icon not exists");

        public readonly ICriteria<RegistrationPage> LoadIcon_RegistrationVisible = new Criteria<RegistrationPage>(p =>
        {
        return p.Exists(Bys.RegistrationPage.LoadIcon_Registration, ElementCriteria.AttributeValue("aria-hidden", "false"));

        }, "Registration page load icon visible");

        public readonly ICriteria<RegistrationPage> LoadIcon_RegistrationNotVisible = new Criteria<RegistrationPage>(p =>
        {
            return p.Exists(Bys.RegistrationPage.LoadIcon_Registration, ElementCriteria.AttributeValue("aria-hidden", "true"));

        }, "Registration page load icon not visible");

        public readonly ICriteria<RegistrationPage> ContinueButtonVisibleEnabled = new Criteria<RegistrationPage>(p =>
        {
            return p.Exists(Bys.RegistrationPage.ContinueBtn, ElementCriteria.IsVisible);

        }, "Continue button visible and enabled");

        public readonly ICriteria<RegistrationPage> PageReady;

        public RegistrationPageCriteria()
        {
            PageReady = ContinueButtonVisibleEnabled.AND(LoadIconNotExists).OR(FirstNameTxtVisible);
        }
    }
}
