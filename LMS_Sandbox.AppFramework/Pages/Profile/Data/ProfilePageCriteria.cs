using Browser.Core.Framework;

namespace LMS.AppFramework
{
    public class ProfilePageCriteria
    {
        public readonly ICriteria<ProfilePage> FirstNameTxtVisible = new Criteria<ProfilePage>(p =>
        {
            return p.Exists(Bys.ProfilePage.FirstNameTxt, ElementCriteria.IsVisible);

        }, "First Name text box visible");

        public readonly ICriteria<ProfilePage> LoadIconNotExists = new Criteria<ProfilePage>(p =>
        {
            return !p.Exists(Bys.LMSPage.LoadIcon);

        }, "Load icon not exists");

        public readonly ICriteria<ProfilePage> LoadIconVisible = new Criteria<ProfilePage>(p =>
        {
        return p.Exists(Bys.ProfilePage.LoadIcon, ElementCriteria.IsVisible);

        }, "Load icon visible");

        public readonly ICriteria<ProfilePage> BackBtnVisibleAndEnabled = new Criteria<ProfilePage>(p =>
        {
            return p.Exists(Bys.ProfilePage.Backbtn, ElementCriteria.IsVisible, ElementCriteria.IsEnabled);

        }, "Back button visible and enabled");

        public readonly ICriteria<ProfilePage> LoadIconNotVisible = new Criteria<ProfilePage>(p =>
        {
            return p.Exists(Bys.ProfilePage.LoadIcon, ElementCriteria.IsNotVisible);

        }, "Load icon not visible");

        public readonly ICriteria<ProfilePage> VerifyYourProfessionFormProfessionSelElemNotExists = new Criteria<ProfilePage>(p =>
        {
            return !p.Exists(Bys.LMSPage.VerifyYourProfessionFormProfessionSelElem);

        }, "Verify Your Profession form, Profession select element not exists");

        public readonly ICriteria<ProfilePage> PageReady;

        public ProfilePageCriteria()
        {
            PageReady = LoadIconNotExists.OR(FirstNameTxtVisible);
        }
    }
}
