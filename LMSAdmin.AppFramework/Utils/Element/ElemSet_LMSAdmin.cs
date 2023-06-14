using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading;
using Browser.Core.Framework;

namespace LMSAdmin.AppFramework
{
    /// <summary>
    /// A utility class for manipulating elements within LMSAdmin. This class is an alternative to ElemSet.cs. I created this because 
    /// LMSAdmin's HTML design is really bad and obsolete in terms of newer HTML standards and practices, and so a majority of the 
    /// methods inside ElemSet will not work with LMSAdmin. If you find that a method inside ElemSet works, then use that, if not, 
    /// then you will have to create (or find) a custom method inside this class just for LMSAdmin
    /// </summary>
    public class ElemSet_LMSAdmin
    {

        #region Checkbox


        #endregion Checkbox

        #region dropdown custom
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Browser"></param>
        /// <param name="btnElem"></param>
        /// <param name="drpDpParentElem"></param>
        /// <param name="itemName"></param>
        public static void DropdownSingle_Fireball_SelectByText(IWebDriver Browser, IWebElement btnElem, string itemName)
        {
            // Expand the drop down and wait for the item to appear
            btnElem.Click(Browser);
            
            string XpathOfItemToWaitFor = string.Format("//div[contains(@class, 'open select-dropdown-undefined')]//span[@class='text' and text()='{0}']", itemName);
            Browser.WaitForElement(By.XPath(string.Format(XpathOfItemToWaitFor, itemName)), ElementCriteria.IsVisible);

            IWebElement itemToClick = Browser.FindElement(By.XPath(string.Format(XpathOfItemToWaitFor, itemName)));

            // If the item is not checked, click it to check it. To determine this, we will first try to find the child checkmark element that 
            // appears if an item is selected. If it doesnt exist and is not visible, then it is unchecked
            if (!itemToClick.Exists(By.XPath("following-sibling::span"), ElementCriteria.IsVisible))
            {
                itemToClick.Click(Browser);
            }

            // If the dropdown button did not close for some reason, close it
            if (btnElem.GetAttribute("aria-expanded") == "true")
            {
                btnElem.Click(Browser);
            }
        }


        #endregion dropdown custom

        #region Select Elements


        #endregion Select Elements

        #region Grids

        /// <summary>
        /// Clicks on a button (Either X, Edit, Pencil, or radio button) within a user-specified row
        /// </summary>
        /// <param name="row">The row that contains the element you want to click on. To get the row, <see cref="ElemGet_LMSAdmin.Grid_GetRowByRowName(IWebElement, By, string, string)"/></param>
        /// <param name="tagnameWhereElemExists">The tag name of the HTML element where the element you want to click on exists</param>
        /// <param name="action">Either "Edit" "Delete" "Remove" "Add" "View" or "Radio"</param>
        /// <param name="javaScriptClick">(Optional). If you want to click with javascript, then set this to true</param>
        /// <param name="Browser">(Optional). Only send this if javascriptClick is set to true</param>
        public static IWebElement Grid_ClickElementWithoutTextInsideRow(IWebElement row, string tagnameWhereElemExists, string action, 
            bool javaScriptClick = false, IWebDriver Browser = null)
        {
            string xpath = "";

            if (action == "Radio")
            {
                xpath = string.Format("descendant::{0}[@type='radio']", tagnameWhereElemExists);

            }
            else
            {
                xpath = string.Format("descendant::{0}[contains(@alt, '{1}')]", tagnameWhereElemExists, action);
            }

            var button = row.FindElement(By.XPath(xpath));

            if (javaScriptClick)
            {
                button.ClickJS(Browser);
            }
            else
            {
                button.Click();
            }

            return button;
        }

        /// <summary>
        /// Checks a check box in a table. If the checkbox is already checked, it will not check it
        /// </summary>
        /// <param name="row">The row that contains the element you want to click on. To get the row, <see cref="ElemGet_LMSAdmin.Grid_GetRowByRowName(IWebElement, By, string, string)"/></param>
        /// <param name="instanceOfCheckbox">Sometimes there can be more than 1 checkbox in a row. If you want to check the first one, send 1 to this parameter. For the second one, send 2, etc.</param>
        public static IWebElement Grid_TickCheckBox(IWebElement row, int instanceOfCheckbox)
        {
            string xpath = string.Format("descendant::input[@type='checkbox'][{0}]", instanceOfCheckbox);
            var button = row.FindElement(By.XPath(xpath));

            // If its not checked, check it
            if (button.GetAttribute("checked") == null)
            {
                button.Click();
            }

            return button;
        }

        /// <summary>
        /// Checks a check box in a table. If the checkbox is already checked, it will not check it
        /// </summary>
        /// <param name="row">The row that contains the element you want to click on. To get the row, <see cref="ElemGet_LMSAdmin.Grid_GetRowByRowName(IWebElement, By, string, string)"/></param>
        /// <param name="instanceOfCheckbox">Sometimes there can be more than 1 checkbox in a row. If you want to check the first one, send 1 to this parameter. For the second one, send 2, etc.</param>
        public static IWebElement Grid_SelectradioOpt(IWebElement row, int instanceOfCheckbox)
        {
            string xpath = string.Format("descendant::input[@type='radio'][{0}]", instanceOfCheckbox);
            var button = row.FindElement(By.XPath(xpath));

            // If its not checked, check it
            if (button.GetAttribute("checked") == null)
            {
                button.Click();
            }

            return button;
        }

        /// <summary>
        /// Enters text in the text box from a table row. 
        /// </summary>
        /// <param name="row">The row that contains the element you want to click on. To get the row, <see cref="ElemGet_LMSAdmin.Grid_GetRowByRowName(IWebElement, By, string, string)"/></param>
        /// <param name="instanceOfTextBox">Sometimes there can be more than 1 text box in a row. If you want to enter text in the first one, send 1 to this parameter. For the second one, send 2, etc.</param>
        public static void Grid_EnterTextInField(IWebElement row, int instanceOfTextBox, string textToEnter)
        {
            string xpath = string.Format("descendant::input[@type='text'][{0}]", instanceOfTextBox);

            var textbox = row.FindElement(By.XPath(xpath));

            textbox.Clear();
            textbox.SendKeys(textToEnter);
        }

        

        /// <summary>
        /// Selects all items in the list. Note this only works when you dont currently have any items selected in the list
        /// </summary>
        /// <param name="Browser"></param>
        /// <param name="selectProfAvailSelElem"></param>
        internal static void SelElem_SelectAll(IWebDriver Browser, SelectElement selectProfAvailSelElem)
        {
            selectProfAvailSelElem.SelectByIndex(1);
            Actions action = new Actions(Browser);
            action.KeyDown(Keys.Control).SendKeys("a").KeyUp(Keys.Control).Build().Perform();
        }

        /// <summary>
        /// Expands or collapses childrows under the parentrow within a table. 
        /// </summary>
        /// <param name="row">The row that contains the Collapse/Expand element . To get the row, <see cref="ElemGet.Grid_GetRowByRowName(Browser, IWebElement, By, string)"/></param>
        /// <param name="expandOrCollapse">Either "expand" or "collapse"</param>
        /// <param name="tagNameOfElemToclick">The tag name of the expansion/collapse icon</param>
        /// <param name="indexOfElemToClick">(Optional). If your row has multiple elements with the same tag name, you can specify the index at which your tag name you want to get. Default is 0 for the first instance</param>
        public static void Grid_ExpandOrCollapse(IWebElement row, string expandOrCollapse, string tagNameOfElemToclick, int indexOfElemToClick = 0)
        {
            IWebElement ExpandOrCollapseElement = ElemGet_LMSAdmin.Grid_GetButtonOrLinkWithoutTextWithinRow(row, tagNameOfElemToclick, indexOfElemToClick);

            // get whether the aria is visible or not
            string ariaVisible = ExpandOrCollapseElement.GetAttribute("aria-expanded");

            if (expandOrCollapse == "expand")
            {
                //if aria is not expanded already, then click to exapnd
                if (ariaVisible.Equals("false"))
                    ExpandOrCollapseElement.Click();
            }
            if (expandOrCollapse == "collapse")
            {
                //if aria is expanded already, then click to collapse
                if (ariaVisible.Equals("true"))
                    ExpandOrCollapseElement.Click();
            }


        }




        #endregion Grids

        #region Radio Buttons



        #endregion Radio Buttons


        #region General

        /// <summary>
        /// Highlighting the element by outlining it with Yellow backgroud and border it with red
        /// Useful while debug or error screenshots if this method is used
        /// </summary>
        /// <param name="element"></param>
        public static void highLighterMethod(IWebDriver Browser,IWebElement element)
        {
            ElemSet.ScrollToElement(Browser, element, false);
            IJavaScriptExecutor js = (IJavaScriptExecutor)Browser;
            js.ExecuteScript("arguments[0].setAttribute('style', 'background: yellow; border: 2px solid red;');", element);
        }

        #endregion General


    }
}

