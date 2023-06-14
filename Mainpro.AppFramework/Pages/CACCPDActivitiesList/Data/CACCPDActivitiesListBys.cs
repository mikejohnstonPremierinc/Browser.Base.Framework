using OpenQA.Selenium;


namespace Mainpro.AppFramework
{
    public class CACCPDActivitiesListBys
    {
        //required elements for allocationg credits
        public readonly By CreditsToApplyTopLnk = By.XPath("//*[@class='slim-row']/td[3]/a/span");
        public readonly By CreditsToApplySecondLnk = By.XPath("//*[@id='index-page']/app-root/div/div/div[2]/div/section/div/div/div/cpd-activities/div/div[4]/div/div/div[2]/table/tbody/tr[2]/td[3]/a/span");
        public readonly By CreditsToApplyThirdLnk = By.XPath("//*[@id='index-page']/app-root/div/div/div[2]/div/section/div/div/div/cpd-activities/div/div[4]/div/div/div[2]/table/tbody/tr[3]/td[3]/a/span");
        public readonly By FirstCACAllocationTxt = By.Id("staticSpeciality");
        public readonly By PopupSubmitBtn = By.XPath("//*[@class='modal-footer']/button[2]");
    }
}
