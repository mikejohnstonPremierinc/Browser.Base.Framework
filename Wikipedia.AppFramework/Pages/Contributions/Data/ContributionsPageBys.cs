using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace Wikipedia.AppFramework
{
    /// <summary>
    /// This is where we locate our elements. Please use standard naming conventions and group your elements as shown below. Standard naming 
    /// conventions are defined here: https://code.premierinc.com/docs/display/PQA/PageBy+Class+and+Naming+Conventions
    /// </summary>
    public class ContributionsPageBys
    {

        // Buttons

        // Charts

        // Check boxes


        // Labels                                              
        public readonly By SearchForContributionsLbl = By.XPath("//span[contains(text(),'Search for contributions')]");
        public readonly By HideMinorEditsLbl = By.XPath("//label[text()='Hide minor edits']");

        // Links

        // Menu Items    

        // Radio buttons

        // Select Elements  

        // Tables       


        // Tabs

        // Text boxes
        public readonly By UsernameTxt = By.Id("ooui-php-2");

    }
}
