using Browser.Core.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Data;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using Browser.Core.Framework.Utils;
using System.Diagnostics;
using OpenQA.Selenium.Firefox;
using LS.AppFramework.Constants_LTS;
using LS.AppFramework;
using LMS.Data;
using System.Linq;

namespace LS.AppFramework.HelperMethods
{
    /// <summary>
    /// A class that consists of methods which combine custom page methods to accomplish various tasks for this application. This is mainly
    /// called/used when a tester is automating another application, and needs to also access this application to setup data or verify functionality
    /// </summary>
    public class LSHelperMethods
    {
        #region properties



        #endregion properties

        #region methods

        #region RCP

        /// <summary>
        /// Goes to the Participant Program page if we are not already there, clicks on the the Self Reporting tab, 
        /// clicks the Actions>Validate link for a user-specified activity, waits for the Credit Validation page to 
        /// appear, clicks either the Accept/Reject/Needs More Information radio button, clicks the Submit button, 
        /// and waits for the page be done loading
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="siteName">The exact text of the site inside sites table that you want to click on</param>
        /// <param name="username">The exact text of the participant inside the participants table that you want to click on</param>
        /// <param name="program">The exact text of the program inside the programs table that you want to click on</param>
        /// <param name="activityName">The exact text of the activity inside the Self Reported Activities table table that you want to click on</param>
        /// <param name="option"><see cref="Constants_LTS.Constants_LTS.CreditValidationOptions"/></param>
        public ProgramPage ValidateCredit(IWebDriver browser, string siteName, string username, string program,
            string activityName, Constants_LTS.Constants_LTS.CreditValidationOptions option)
        {
            ProgramPage PP = new ProgramPage(browser);

            // If we are not on the participant program page, then go there
            if (browser.FindElements(Bys.ProgramPage.SelfReportActTab).Count == 0)
            {
                GoToParticipantProgramPage(browser, siteName, username, program);
            }

            PP.ChooseActivityAndValidateCredit(activityName, option);

            return PP;
        }

        /// <summary>
        /// Goes to the Site page that the tester specifies, clicks on the Activity Upload 
        /// link, uploads the tester-specified file, then waits for the Completed label to appear. The file should be added 
        /// to a folder titled DateFiles inside the root of your UITest project and it should be added inside Visual 
        /// Studio Solution Explorer
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="siteName">The exact text of the site inside Sites table that you want to click on</param>
        /// <param name="filePath">The file path including the file name and extension of the excel file you want to upload</param>
        public ActivityUploadPage UploadActivity(IWebDriver browser, string siteName, string filePath)
        {
            ProgramPage PP = new ProgramPage(browser);

            SitePage SP = GoToSitePage(browser, siteName);

            ActivityUploadPage AUP = SP.ClickAndWait(SP.ActivityUploadLnk);

            AUP.UploadActivity(filePath);

            return AUP;
        }

        /// <summary>
        /// Clicks on the Details tab of the Program page and then returns a user-specified label value from the details tab 
        /// on the Program page
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="detail">The name of the label on the Detail tab for which you want the value to return</param>
        /// <returns></returns>
        public string GetProgramDetail(IWebDriver browser, string detail)
        {
            ProgramPage PP = new ProgramPage(browser);
            return PP.GetProgramDetail(browser, detail);
        }

        /// <summary>
        /// Searches for a user-specified site, clicks on that site, clicks on the All Particpants link, searches for a user-specified participant, 
        /// clicks on the participant, clicks on the Programs tab of the particpant, then clicks on a user-specified program and waits for that program page
        /// to load
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="siteName">The exact text of the site inside sites table that you want to click on</param>
        /// <param name="participantFirstAndLastName">The exact text of the participant inside participants table that you want to click on</param>
        /// <param name="program">The exact text of the program inside programs table that you want to click on</param>
        public ProgramPage GoToParticipantProgramPage(IWebDriver browser, string siteName, string username,
            string program, string status = null)
        {
            ParticipantsPage PAP = GoToParticipantPage(browser, siteName, username);

            PAP.ClickAndWait(PAP.ProgramsTab);

            return PAP.ClickProgramAndWait(browser, program, status);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="siteName"></param>
        /// <param name="participantFirstAndLastName"></param>
        /// <param name="userName"></param>
        public SitePage GoToSitePage(IWebDriver browser, string siteName)
        {            
            // Sometimes clicking on the Sites tab results in the Search page appearing, sometimes it results in the Sites
            // page appearing
            SearchPage SP = new SearchPage(browser);
            SitePage SiteP = new SitePage(browser);
            SP.ClickAndWaitBasePage(SP.SitesTab);

            // If there is only 1 site in the environment, then the Sites table will not appear, so we won't need to
            // search for a site because that site page will immediately show
            if (browser.Exists(Bys.SearchPage.SitesTbl))
            {
                return SP.SearchAndSelect(Bys.SearchPage.SitesTblBody, Constants_LTS.Constants_LTS.SearchResults.Sites,
                    siteName);
            }
            else
            {
                return SiteP;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Browser"></param>
        /// <param name="By"></param>
        /// <param name="expectedText"></param>
        public void VerifyLabelTextEquals(IWebDriver Browser, By By, string expectedText)
        {
            ProgramPage PP = new ProgramPage(Browser);
            ApplicationUtils.WaitForCreditsToBeAppliedOrActivityToShowOnLTS(PP, By, expectedText);
        }

        public void GoToParticipantProgramPage_ViaURL(IWebDriver browser, string siteName, string program,
            string status = null, string participantID = null)
        {
            String URL = string.Format("{0}sites/RCPSC/participants/{1}#Recognitions", AppSettings.Config["LSURL"].ToString(), participantID);
            browser.Navigate().GoToUrl(URL);
            Thread.Sleep(1000);
            ParticipantsPage PAP = new ParticipantsPage(browser);
            PAP.WaitUntil(Criteria.ParticipantsPage.ProgramsTabProgramTblBodyRowVisible);
            PAP.ClickProgramAndWait(browser, program, status);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ProgramPage ApplyCarryCredits(IWebDriver browser)
        {
            ProgramPage PP = new ProgramPage(browser);
            PP.ClickApplyRecognitionCOCAndApplyCarryOverCredits();
            return PP;
        }

        /// <summary>
        /// Goes to the Participant Program page, clickc on the Reevaluate link then clicks the Reavluate button
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="siteName">The exact text of the site inside sites table that you want to click on</param>
        /// <param name="username">The exact text of the participant inside the participants table that you want to click on</param>
        /// <param name="program">The exact text of the program inside the programs table that you want to click on</param>
        public void ReevaluateUser(IWebDriver browser, string siteName, string username, string program)
        {
            ProgramPage PP = new ProgramPage(browser);

            // Sometimes after clicking the Reevaluate link, LTS throws an error. This error does not occur if we first navigate 
            // to the Program page, so I am adding this code to go to the Program page first
            GoToParticipantProgramPage(browser, siteName, username, program);

            Thread.Sleep(0500);
            PP.ClickAndWait(PP.ReevaluateLnk);
            Thread.Sleep(0500);
            PP.ClickAndWait(PP.ReevaluateBtn);
        }

        /// <summary>
        /// Refreshes the page every 3 seconds until a label of your choice is updated
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="elemToWaitFor">The label's title on this page. For example, 'Starts', or 'Program'</param>
        /// <param name="elemTextToWaitFor">The text you are waiting for the label to update with</param>
        public void WaitForProgramPageElementTextToUpdate(IWebDriver browser, string elemToWaitFor, string elemTextToWaitFor)
        {
            ProgramPage PP = new ProgramPage(browser);
            PP.ClickAndWait(PP.DetailsTab);

            if (elemToWaitFor == "Starts")
            {
                ApplicationUtils.WaitForCreditsToBeAppliedOrActivityToShowOnLTS(PP, Bys.ProgramPage.DetailsTabStartsValueLbl, elemTextToWaitFor);
            }
            else if (elemToWaitFor == "Credits Applied")
            {
                ApplicationUtils.WaitForCreditsToBeAppliedOrActivityToShowOnLTS(PP, Bys.ProgramPage.DetailsTabCreditsValueLbl, elemTextToWaitFor);
            }
            else if (elemToWaitFor == "Program")
            {
                ApplicationUtils.WaitForCreditsToBeAppliedOrActivityToShowOnLTS(PP, Bys.ProgramPage.DetailsTabProgramValueLbl, elemTextToWaitFor);
            }
        }

        /// <summary>
        /// Searches for a user-specified site, clicks on that site, clicks on the All Particpants link, searches for a user-specified participant, 
        /// clicks on the participant and waits for the Participant page to load
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="siteName">The exact text of the site inside sites table that you want to click on</param>
        /// <param name="userName">The username of the participant inside participants table that you want to click on</param>
        /// <returns></returns>
        public ParticipantsPage GoToParticipantPage(IWebDriver browser, string siteName, string userName = null)
        {
            SearchPage SP = new SearchPage(browser);

            SP.ClickAndWaitBasePage(SP.SitesTab);

            // If there is only 1 site in the environment, then the Sites table will not appear, so we won't need to search for a site because that site 
            // page will immediately show
            if (browser.Exists(Bys.SearchPage.SitesTbl))
            {
                SP.SearchAndSelect(Bys.SearchPage.SitesTblBody, Constants_LTS.Constants_LTS.SearchResults.Sites, siteName);
            }

            SP.ClickAndWaitBasePage(SP.AllParticipantsLnk);

            return SP.SearchAndSelect(Bys.SearchPage.AllParticpantsTblBody, Constants_LTS.Constants_LTS.SearchResults.Participants,
                userName);

        }

        /// <summary>
        /// Searches for a user-specified site, clicks on that site, clicks on the All Particpants link, 
        /// searches for a user-specified participant, clicks on the participant, waits for the 
        /// Participant page to load, then clicks Launch and switches tabs. You will have to include the Wait criteria
        /// inside your own project after you call this method, will also have to instantiate your 
        /// new page that gets loaded. This method will not handle those two things
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="siteName">The exact text of the site inside sites table that you want to click on</param>
        /// <param name="userName">(Optional) If your application has duplicate users with the same first and last name, pass the username here</param>
        /// <returns></returns>
        public void LaunchSiteFromParticipantPage(IWebDriver browser, string siteName, string userName = null)
        {
            ParticipantsPage PAP = GoToParticipantPage(browser, siteName, userName);

            // See comment at bottom of this method above If statement for why we have this TakeScreenshot line
            //if (browser.Url.Contains("CFPC"))
            //{
            //    browser.TakeScreenshot("LSHelperMethods_LaunchSiteFromParticipantPage");
            //}
            
            browser.FindElement(By.XPath("//li/a[@class='launch']")).Click();
            browser.SwitchTo().Window(browser.WindowHandles.Last());

            // For CFPC: Make sure we launch the correct user. If a test fails here, then we know the above GoToParticipantPage
            // or the click of the Launch button failed to get us to the correct user page on Mainpro. See bug
            // https://code.premierinc.com/issues/browse/MAINPROREW-915 for more info on why we are checking this.
            // We put in a workaround in Mainpro (using unique LTST user per every test), so I am commenting the TakeScreenshot
            // lines of code and the below If statement since its not needed right now since tests are no longer failing
            // with workaround. See LS.AppFramework>SearchPage>ClickParticpantAndWait method also
            //if (browser.Url.Contains("CFPC"))
            //{
            //    browser.WaitForElement(By.XPath("//span[text()='ENTER A CPD ACTIVITY']"), ElementCriteria.IsVisible);
            //    string userThatWasLaunched = APIHelp.GetUserName(browser);
            //    string userThatShouldHaveBeenLaunched = userName;
            //    if (userThatWasLaunched != userThatShouldHaveBeenLaunched)
            //    {
            //        browser.TakeScreenshot("LSHelperMethods_LaunchSiteFromParticipantPage");
            //        throw new Exception(string.Format("After clicking on Launch, LTS failed to launch the correct user. The " +
            //            "user that was launched was {0}, but the user that should have been launched was {1}. See code " +
            //            "for more info", userThatWasLaunched, userThatShouldHaveBeenLaunched));
            //    }
            //}
        }

        /// <summary>
        /// Goes to the Participant page if we are not already there, clicks on the Programs tab of the Participants, clcks Actions>Adjust 
        /// Dates link for the Maintenance Of Certification program,fills in a user-specified start or end date, then clicks the Yes button
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="username">The username of the participant inside participants table that you want to click on</param>
        /// <param name="startOrEndDate">"Start" or "End"</param>
        /// <param name="date">The date to enter, in the format "yyyy-mm-dd"</param>
        /// ToDo: Make this more universal by putting the Site name as a parameter, then refactoring this method and code which calls this method
        public void RCP_AdjustMOCDate(IWebDriver browser, string username, string startOrEndDate, string date)
        {
            ParticipantsPage PAP = new ParticipantsPage(browser);

            // If we are not on the participant page, then go there
            if (browser.FindElements(Bys.ParticipantsPage.RegeneratePasswordTab).Count == 0)
            {
                GoToParticipantPage(browser, "Royal College of Physicians", username);
            }

            PAP.AdjustProgramCycleDates(startOrEndDate, date);
        }

        #endregion RCP 

        #region add adjustments

        /// <summary>
        /// Clicks the Add Adjustment tab if we are not already there, clicks the Add Adjustment link, chooses an
        /// Adjustment Code from the select element (This overload is for any adjustment that only has the Add Adjustment 
        /// button on the Add Adjustment form and does not have additional fields on that form. For example in RCP, 
        /// ext1, ext2, ext2f, pra, per and temp adjustments. For CFPC, remedial, etc.). Then clicks the Add Adjustment button
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="participantFirstAndLastName">The exact text of the participant inside participants table that 
        /// you want to click on</param>
        /// <param name="adjustmentCode">The exact text of the adjustment code that you want to chooose in the Adjustment
        /// Code select element</param>
        public void AddProgramAdjustment(IWebDriver browser, string participantFirstAndLastName,
            Constants_LTS.Constants_LTS.AdjustmentCodes adjustmentCode)
        {
            ProgramPage PP = new ProgramPage(browser);
            PP.AddProgramAdjustment(adjustmentCode);
        }

        /// <summary>
        /// Clicks the Add Adjustment tab if we are not already there, clicks the Add Adjustment link, chooses an 
        /// Adjustment Code from the select element, and clicks on the Yes or No radio button (this overload is for
        /// the INTNL and Voluntary program adjustments). Then clicks the Add Adjustment button
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="participantFirstAndLastName">The exact text of the participant inside participants table that
        /// you want to click on</param>
        /// <param name="adjustmentCode">The exact text of the adjustment code that you want to chooose in the Adjustment
        /// Code select element</param>
        /// <param name="Rdo">The yes or no radio button element for INTNL or VOLUNTARY program adjustment</param>
        public void AddProgramAdjustment(IWebDriver browser, string participantFirstAndLastName,
            Constants_LTS.Constants_LTS.AdjustmentCodes adjustmentCode, IWebElement Rdo)
        {
            ProgramPage PP = new ProgramPage(browser);
            PP.AddProgramAdjustment(adjustmentCode, Rdo);
        }

        /// <summary>
        /// Clicks the Add Adjustment tab if we are not already there, clicks the Add Adjustment link, chooses an 
        /// Adjustment Code from the select element, enters a start and end date, selects a leave code (this overload 
        /// is for the Leave program adjustment). Then clicks the Add Adjustment button
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="participantFirstAndLastName">The exact text of the participant inside participants table that
        /// you want to click on</param>
        /// <param name="adjustmentCode">The exact text of the adjustment code that you want to chooose in the Adjustment 
        /// Code select element</param>
        /// <param name="leaveStartDate"></param>
        /// <param name="leaveEndDate"></param>
        /// <param name="leaveCode"></param>
        public void AddProgramAdjustment(IWebDriver browser, string participantFirstAndLastName,
            Constants_LTS.Constants_LTS.AdjustmentCodes adjustmentCode, string leaveStartDate,
            string leaveEndDate, string leaveCode)
        {
            ProgramPage PP = new ProgramPage(browser);
            PP.AddProgramAdjustment(adjustmentCode, leaveStartDate, leaveEndDate, leaveCode);
        }

        /// <summary>
        /// Goes to the Add Adjustment tab if we are not already there, clicks the Add Adjustment link, chooses an 
        /// Adjustment Code from the select element, then enters an effective date. Then clicks the Add Adjustment button.
        /// This overload is for any adjustment that has ONLY an Effective Date Select Element on the Add Adjustment popup.
        /// Meaning additional controls dont appear after selecting the adjustment code. Codes such as 
        /// Reinstated - Other, Reinstated - Non Compliance, PER program, PER program, Voluntary Program, 
        /// International Program, Main Program, Resident Program, A-Active (Default), etc.
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="participantFirstAndLastName">The exact text of the participant inside participants table that 
        /// you want to click on</param>
        /// <param name="adjustmentCode">The exact text of the adjustment code that you want to chooose in the Adjustment 
        /// Code select element</param>
        /// <param name="effectiveDate"></param>
        public void AddProgramAdjustment(IWebDriver browser, string participantFirstAndLastName,
            Constants_LTS.Constants_LTS.AdjustmentCodes adjustmentCode, string effectiveDate)
        {
            ProgramPage PP = new ProgramPage(browser);
            PP.AddProgramAdjustment(browser, adjustmentCode, effectiveDate);
        }

        /// <summary>
        /// Clicks the Add Adjustment tab if we are not already there, clicks the Add Adjustment link, chooses an 
        /// Adjustment Code from the select element (this overload is for CFPC C-Custom adjustment), chooses 
        /// an item from the first dropdown and then enters a date. Then clicks the Add Adjustment button. NOTE: 
        /// I still need to add code for the other dropdown and the other items in the first dropdown
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="participantFirstAndLastName">The exact text of the participant inside participants table that 
        /// you want to click on</param>
        /// <param name="adjustmentCode">The exact text of the adjustment code that you want to chooose in the Adjustment 
        /// Code select element</param>
        /// <param name="adjustCycleDate">Quickly needed to add parameters for CFPC adjust cycle date.</param>
        /// <param name="adjustCycleDate">Quickly needed to add parameters for CFPC adjust cycle date.</param>
        public void AddProgramAdjustment(IWebDriver browser, string participantFirstAndLastName,
            Constants_LTS.Constants_LTS.AdjustmentCodes adjustmentCode,
            Constants_LTS.Constants_LTS.AddAdjustFormCFPCCustomAdjustFirstSelElemItem adjustCycleSelection =
            Constants_LTS.Constants_LTS.AddAdjustFormCFPCCustomAdjustFirstSelElemItem.AdjustCycleEndDate,
            DateTime adjustCycleDate = default(DateTime))
        {
            ProgramPage PP = new ProgramPage(browser);
            PP.AddProgramAdjustment(adjustmentCode, adjustCycleSelection, adjustCycleDate);
        }

        #endregion add adjustments

        #region general 

        /// <summary>
        /// Logs in then returns the SearchPage object. This Login method should only be called/used from within this class 
        /// (mainly when coding tests on another application). Otherwise, the Login page class's Login method should be used
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public void Login(IWebDriver browser, string username, string password)
        {
            LoginPage LP = new LoginPage(browser);
            LP.Login(browser, username, password);
        }

        /// <summary>
        /// Clicks the Add Adjustment tab
        /// </summary>
        /// <param name="browser">The driver instance</param>
        public void GoToProgramAdjustmentTab(IWebDriver browser)
        {
            ProgramPage PP = new ProgramPage(browser);
            PP.ClickAndWait(PP.ProgramAdjustmentsTab);
        }

        /// <summary>
        /// Can use this when coding you other project's tests and you need to wait for elements of this application 
        /// </summary>
        public void WaitForElement(IWebDriver browser, By by)
        {
            browser.WaitForElement(by, ElementCriteria.IsVisible);
        }

        #endregion general



        #endregion methods

    }
}
