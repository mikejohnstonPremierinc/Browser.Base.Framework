using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace Browser.Core.Framework
{
    /// <summary>
    /// A utility class for manipulating elements
    /// </summary>
    public static class ElemSet
    {
        #region select elements - single select

        /// <summary>
        /// Selects a random item in a single select dropdown. It will not select the already selected item.
        /// </summary>
        /// <param name="selectElem">The SelectElement that contains the items</param>
        public static string SelElem_Single_SelectRandomItem(SelectElement selectElem)
        {
            for (var i = 0; i < 100; i++)
            {
                Random random = new Random();
                int randomInt = DataUtils.GetRandomIntegerWithinRange(0, selectElem.Options.Count);
                if (selectElem.SelectedOption.Text != selectElem.Options[randomInt].Text)
                {
                    selectElem.SelectByIndex(randomInt);
                    break;
                }
            }
            return selectElem.SelectedOption.Text;
        }

        /// <summary>
        /// Selects the first indexed item in the dropdown that contains the user-specified text
        /// </summary>
        /// <param name="elem">The element</param>
        /// <param name="text">The text to search for</param>
        /// <returns></returns>
        public static string SelElem_SelectItemContainingText(SelectElement elem, string text)
        {
            List<string> itemStrings = ElemGet.SelElem_ListTextToListString(elem);
            string selectedString = "";

            // For each item's string in the dropdown
            foreach (string itemString in itemStrings)
            {
                // If the string contains the user-specified text, then select it
                if (itemString.Contains(text))
                {
                    elem.SelectByText(itemString);
                    selectedString = itemString;
                    break;
                }
            }
            Thread.Sleep(1000);
            return selectedString;
        }

        #endregion select elements - single select

        #region select elements - multi select

        /// <summary>
        /// Selects a user-specified number of random items in a multi-select SelectElement. If you need to select a lot of items in a dropdown that
        /// contains a large list, then this method might take a while. In that case, you can Use DropdownCustomClickRandomItems with IWebElement instead.
        /// </summary>
        /// <param name="selectElem">The IWebElement that contains the items</param>
        /// <param name="numberOfItemsToSelect"></param>
        public static string SelElem_Multi_SelectRandomItems(SelectElement selectElem, int numberOfItemsToSelect)
        {
            string selectedOptions = null;
            List<string> listOfSelectedOptions = new List<string>();
            Random random = new Random();
            int randomInt = 0;
            List<int> listOfIntsUsed = new List<int>();
            int countOfItemsInList = selectElem.Options.Count;

            for (var i = 0; i < numberOfItemsToSelect; i++)
            {
                // If all the items in the list are already selected, exit above for loop
                if (selectElem.AllSelectedOptions.Count == countOfItemsInList)
                {
                    break;
                }

                for (var j = 0; j < 100; j++)
                {
                    if (listOfIntsUsed.Contains(randomInt))
                    {
                        randomInt = DataUtils.GetRandomIntegerWithinRange(0, selectElem.Options.Count);
                    }
                    if (!listOfIntsUsed.Contains(randomInt))
                    {
                        break;
                    }
                }

                selectElem.SelectByIndex(randomInt);
                listOfIntsUsed.Add(randomInt);
            }

            // Store all the elements selected above into a comma separated string
            IList<IWebElement> selectedElements = selectElem.AllSelectedOptions;

            foreach (var elem in selectedElements)
            {
                listOfSelectedOptions.Add(elem.Text);
            }

            selectedOptions = string.Join(", ", listOfSelectedOptions);
            return selectedOptions;
        }



        #endregion select elements - multi select

        #region dropdown community        

        /// <summary>
        /// Clicks on the dropdown button to expand it, clicks on the selected item to deselect it (if it is not already deselected), 
        /// then closes the dropdown.
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="btnElem">The button you are selecting the item from. It must be of type IWebElement, not SelectElement</param>
        /// <param name="drpDpParentElem">The parent element that contains the UL tag element which contains your drop down items</param>
        /// <param name="itemName">The name of the item you want to click on</param>
        public static void DropdownCommunity_DeselectItemByName(IWebDriver browser, IWebElement btnElem, IWebElement drpDpParentElem, string itemName)
        {
            ElemSet.DropdownCommunity_Click(browser, btnElem);

            // This DIV element contains the list items, whether filtered or not. If values are filtered, there is code in the line after
            // this to handle that. i.e. class<>hidden
            var ulElem = drpDpParentElem.FindElement(By.CssSelector("ul"));

            // Store all items within the Div element
            var items = ulElem.FindElements(By.TagName("label")).ToList();

            foreach (var item in items)
            {
                // if the current list item's text value in the for loop equals the users passed parameter itemName
                if (item.Text == itemName)
                {
                    // If the item is not selected already, then select it
                    if (item.GetAttribute("class") == "checkbox clickable ng-binding text-success")
                    {
                        item.Click();
                        break;
                    }
                }
            }

            ElemSet.DropdownCommunity_Click(browser, btnElem);
        }

        /// <summary>
        /// Chrome browser is failing in tests when simply using multiselectdropdown.click(). For some reason in this browser, whenever a dropdown
        /// gets expanded, a new element is created in the HTML with a classname of dropdown-backdrop. This new element blocks Selenium from all of the
        /// controls within the form, so if the user tries to do a multiselectdropdown.click(), selenium says it failed to click on that dropdown, and
        /// instead says the dropdown-backdrop would get the click. So the below method takes that into account and works to open and close the control
        /// </summary>
        /// <param name="elem">The button element of the multi-select dropdown</param>
        public static void DropdownCommunity_Click(IWebDriver browser, IWebElement elem)
        {
            elem.Click();
        }

        /// <summary>
        /// Clicks on the dropdown button to expand it, selects an item by name, then closes the dropdown. This is an extension to 
        /// DropdownCustomSelectListItemByIndex. 
        /// NOTE: If the item is already selected, it will stay selected because the method conditions it so the item will be clicked twice in this case.
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="btnElem">The button you are selecting the item from. It must be of type IWebElement, not SelectElement</param>
        /// <param name="drpDpParentElem">The parent element that contains the UL tag element which contains your drop down items</param>
        /// <param name="itemName">The name of the item you want to click on</param>
        public static void DropdownCommunity_SelectItemByName(IWebDriver browser, IWebElement btnElem, IWebElement drpDpParentElem, string itemName)
        {
            ElemSet.DropdownCommunity_Click(browser, btnElem);

            // This DIV element contains the list items, whether filtered or not. If values are filtered, there is code in the line after
            // this to handle that. i.e. class<>hidden
            var ulElem = drpDpParentElem.FindElement(By.CssSelector("ul"));

            // Store all items within the Div element. RCP uses the 'label' tag and CFPC uses the 'li' tag. Do not change
            // the order of the try catch below. We should look for RCP first. If the code doesnt find child label elements
            // then this means it is CFPC, so the Catch block will then find the CFPC items.
            List<IWebElement> items = null;
            try
            {
                items = ulElem.FindElements(By.XPath("descendant::label")).ToList();
                if (items.Count == 0)
                {
                    throw new Exception("Lets go to the Catch block :)");
                }

            }
            catch
            {
                items = ulElem.FindElements(By.XPath("descendant::li")).ToList();
            }

            foreach (var item in items)
            {
                // if the current list item's text value in the for loop equals the users passed parameter itemName
                if (item.Text == itemName)
                {
                    // If the item is not selected already, then select it
                    if (item.GetAttribute("class") != "checkbox clickable ng-binding text-success")
                    {
                        item.Click();
                        break;
                    }
                }
            }

            ElemSet.DropdownCommunity_Click(browser, btnElem);
        }

        #endregion dropdown community

        #region dropdown fireball

        /// <summary>
        /// Selects an item within Fireballs single select Select Element by the tester-specified text. If you are only executing Fireball tests 
        /// in Chrome and Edge, you can instead just use 
        /// Selenium's SelectElement class existing methods. However, if you are executing Fireball tests in Firefox, you will notice that 
        /// Selenium's SelectElement class existing methods do not work in Firefox. This method works with Firefox
        /// </summary>
        /// <param name="Browser">The driver instance</param>
        /// <param name="btnElem">The Button element that is always beneath the Select element in the HTML of Fireball applications</param>
        /// <param name="itemName">The text of the item to click</param>
        public static void DropdownSingle_Fireball_SelectByText(IWebDriver Browser, IWebElement btnElem, string itemName)
        {
            // Expand the drop down and wait for the item to appear
            btnElem.Click(Browser);
            // Note that sometimes the dropdown items have the @class=text attribute/value and sometimes they do 
            // not (when the items have color). So we have 2 xpaths on the next line in an Or condition
            string XpathOfItemToWaitFor =
                string.Format("//div[contains(@class, 'open select-dropdown-undefined')]//span[@class='text' and text()='{0}'] | //div[contains(@class, 'open select-dropdown-undefined')]//span[text()='{0}']", itemName);
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

        /// <summary>
        /// Deselects an item within Fireballs single select Select Element by the tester-specified text. If you are only executing Fireball 
        /// tests in Chrome and Edge, you can instead just use 
        /// Selenium's SelectElement class existing methods. However, if you are executing Fireball tests in Firefox, you will notice that 
        /// Selenium's SelectElement class existing methods do not work in Firefox. This method works with Firefox
        /// </summary>
        /// <param name="Browser">The driver instance</param>
        /// <param name="btnElem">The Button element that is always beneath the Select element in the HTML of Fireball applications</param>
        /// <param name="itemName">The text of the item to click</param>
        public static void DropdownSingle_Fireball_DeselectByText(IWebDriver Browser, IWebElement btnElem, string itemName)
        {
            // Expand the drop down and wait for the item to appear
            btnElem.Click(Browser);
            Browser.WaitForElement(
                By.XPath(string.Format("//span[@class='text' and text()='{0}']", itemName)),
                ElementCriteria.IsVisible);

            IWebElement itemToClick = Browser.FindElement(By.XPath(string.Format("//span[@class='text' and text()='{0}']", itemName)));

            // If the item is checked, click it to uncheck. To determine this, we will first try to find the child checkmark element that 
            // appears if an item is selected. If it exists and is visible, then it is checked
            if (itemToClick.Exists(By.XPath("following-sibling::span"), ElementCriteria.IsVisible))
            {
                itemToClick.Click(Browser);
            }
        }

        /// <summary>
        /// Selects an item within Fireballs single select Select Element by the tester-specified index. If you are only executing Fireball
        /// tests in Chrome and Edge, you can instead just use 
        /// Selenium's SelectElement class existing methods. However, if you are executing Fireball tests in Firefox, you will notice that 
        /// Selenium's SelectElement class existing methods do not work in Firefox. This method works with Firefox
        /// </summary>
        /// <param name="Browser">The driver instance</param>
        /// <param name="btnElem">The Button element that is always beneath the Select element in the HTML of Fireball applications</param>
        /// <param name="index">The index of the item you want to click</param>
        public static void DropdownSingle_Fireball_SelectByIndex(IWebDriver Browser, IWebElement btnElem, int index)
        {
            // Expand the drop down and wait for the items to appear
            btnElem.Click(Browser);
            Browser.WaitForElement(
                By.XPath(string.Format("//div[@class='dropdown-menu open']/ul[@aria-expanded='true']/li/a/span[@class='text']")),
                ElementCriteria.IsVisible);

            // Find all the list items, then click on the item with the index
            IList<IWebElement> items = Browser.FindElements(By.XPath(string.Format("//div[@class='dropdown-menu open']/ul[@aria-expanded='true']/li/a/span[@class='text'][1]")));

            // If the item is not checked, click it to check it. To determine this, we will first try to find the child checkmark element that 
            // appears if an item is selected. If it doesnt exist and is not visible, then it is unchecked
            if (!items[index].Exists(By.XPath("following-sibling::span"), ElementCriteria.IsVisible))
            {
                items[index].Click(Browser);
            }
        }

        /// <summary>
        /// Selects an item within Fireballs single select Select Element by the tester-specified index. If you are only executing Fireball
        /// tests in Chrome and Edge, you can instead just use 
        /// Selenium's SelectElement class existing methods. However, if you are executing Fireball tests in Firefox, you will notice that 
        /// Selenium's SelectElement class existing methods do not work in Firefox. This method works with Firefox
        /// </summary>
        /// <param name="Browser">The driver instance</param>
        /// <param name="btnElem">The Button element that is always beneath the Select element in the HTML of Fireball applications</param>
        /// <param name="index">The index of the item you want to click</param>
        public static void DropdownSingle_Fireball_DeselectByIndex(IWebDriver Browser, IWebElement btnElem, int index)
        {
            // Expand the drop down and wait for the items to appear
            btnElem.Click(Browser);
            Browser.WaitForElement(
                By.XPath(string.Format("//div[@class='dropdown-menu open']/ul[@aria-expanded='true']/li/a/span[@class='text']")),
                ElementCriteria.IsVisible);

            // Find all the list items, then click on the item with the index
            IList<IWebElement> items = Browser.FindElements(By.XPath(string.Format("//div[@class='dropdown-menu open']/ul[@aria-expanded='true']/li/a/span[@class='text'][1]")));

            // If the item is checked, click it to uncheck. To determine this, we will first try to find the child checkmark element that 
            // appears if an item is selected. If it exists and is visible, then it is checked
            if (items[index].Exists(By.XPath("following-sibling::span"), ElementCriteria.IsVisible))
            {
                items[index].Click(Browser);
            }
        }

        /// <summary>
        /// Selects an item within Fireballs multi select Select Element by the tester-specified text. If you are only executing Fireball tests 
        /// in Chrome and Edge, you can instead just use 
        /// Selenium's SelectElement class existing methods. However, if you are executing Fireball tests in Firefox, you will notice that 
        /// Selenium's SelectElement class existing methods do not work in Firefox. This method works with Firefox
        /// </summary>
        /// <param name="Browser">The driver instance</param>
        /// <param name="btnElem">The Button element that is always beneath the Select element in the HTML of Fireball applications</param>
        /// <param name="itemName">The text of the item to click</param>
        public static void DropdownMulti_Fireball_SelectByText(IWebDriver Browser, IWebElement btnElem, string itemName)
        {
            // For the below xpath, I had to add the 2 not(contains) functions due to Mainpro Select Elements having 2 instances of
            // Select Element, one being invisible (the first indexed one) and the next being the visible one we want to 
            // manipulate
            string selectElementItemXpath = string.Format
                ("//a[not(contains(@class, 'selected')) and not(contains(@tabindex, '-1'))]//span[@class='text' and text()='{0}']",
                itemName);

            // Expand the drop down and wait for the item to appear
            btnElem.Click(Browser);
            Browser.WaitForElement(By.XPath(selectElementItemXpath), ElementCriteria.IsVisible);
            IWebElement itemToClick = Browser.FindElement(By.XPath(selectElementItemXpath));

            // If the item is not checked, click it to check it. To determine this, we will first try to find the child checkmark element that 
            // appears if an item is selected. If it doesnt exist and is not visible, then it is unchecked
            if (!itemToClick.Exists(By.XPath("following-sibling::span"), ElementCriteria.IsVisible))
            {
                itemToClick.Click(Browser);
            }

            btnElem.SendKeys(Keys.Escape);
        }

        /// <summary>
        /// Selects an item within Fireballs multi select Select Element by the tester-specified index. If you are only executing Fireball tests 
        /// in Chrome and Edge, you can instead just use 
        /// Selenium's SelectElement class existing methods. However, if you are executing Fireball tests in Firefox, you will notice that 
        /// Selenium's SelectElement class existing methods do not work in Firefox. This method works with Firefox
        /// </summary>
        /// <param name="Browser">The driver instance</param>
        /// <param name="btnElem">The Button element that is always beneath the Select element in the HTML of Fireball applications</param>
        /// <param name="index">The index of the item to click</param>
        public static void DropdownMulti_Fireball_SelectByIndex(IWebDriver Browser, IWebElement btnElem, int index)
        {
            // Expand the drop down and wait for the items to appear
            btnElem.Click(Browser);
            Browser.WaitForElement(
                By.XPath(string.Format("//div[@class='dropdown-menu open']/ul[@aria-expanded='true']/li/a/span[@class='text']")),
                ElementCriteria.IsVisible);

            // Find all the list items, then click on the item with the index
            IList<IWebElement> items = Browser.FindElements(By.XPath(string.Format("//div[@class='dropdown-menu open']/ul[@aria-expanded='true']/li/a/span[@class='text'][1]")));

            // If the item is not checked, click it to check it. To determine this, we will first try to find the child checkmark element that 
            // appears if an item is selected. If it doesnt exist and is not visible, then it is unchecked
            if (!items[index].Exists(By.XPath("following-sibling::span"), ElementCriteria.IsVisible))
            {
                items[index].Click(Browser);
            }

            btnElem.SendKeys(Keys.Escape);
        }

        /// <summary>
        /// Deselects an item within Fireballs multi select Select Element by the tester-specified text. If you are only executing Fireball tests 
        /// in Chrome and Edge, you can instead just use 
        /// Selenium's SelectElement class existing methods. However, if you are executing Fireball tests in Firefox, you will notice that 
        /// Selenium's SelectElement class existing methods do not work in Firefox. This method works with Firefox
        /// </summary>
        /// <param name="Browser">The driver instance</param>
        /// <param name="btnElem">The Button element that is always beneath the Select element in the HTML of Fireball applications</param>
        /// <param name="itemName">The text of the item to click</param>
        public static void DropdownMulti_Fireball_DeselectByText(IWebDriver Browser, IWebElement btnElem, string itemName)
        {
            // Expand the drop down and wait for the item to appear
            btnElem.Click(Browser);
            Browser.WaitForElement(
                By.XPath(string.Format("//span[@class='text' and text()='{0}']", itemName)),
                ElementCriteria.IsVisible);

            IWebElement itemToClick = Browser.FindElement(By.XPath(string.Format("//span[@class='text' and text()='{0}']", itemName)));

            // If the item is checked, click it to uncheck. To determine this, we will first try to find the child checkmark element that 
            // appears if an item is selected. If it exists and is visible, then it is checked
            if (itemToClick.Exists(By.XPath("following-sibling::span"), ElementCriteria.IsVisible))
            {
                itemToClick.Click(Browser);
            }

            btnElem.SendKeys(Keys.Escape);
        }

        /// <summary>
        /// Deselects an item within Fireballs multi select Select Element by the tester-specified index. If you are only executing Fireball tests 
        /// in Chrome and Edge, you can instead just use 
        /// Selenium's SelectElement class existing methods. However, if you are executing Fireball tests in Firefox, you will notice that 
        /// Selenium's SelectElement class existing methods do not work in Firefox. This method works with Firefox
        /// </summary>
        /// <param name="Browser">The driver instance</param>
        /// <param name="btnElem">The Button element that is always beneath the Select element in the HTML of Fireball applications</param>
        /// <param name="index">The index of the item to click</param>
        public static void DropdownMulti_Fireball_DeselectByIndex(IWebDriver Browser, IWebElement btnElem, int index)
        {
            // Expand the drop down and wait for the items to appear
            btnElem.Click(Browser);
            Browser.WaitForElement(
                By.XPath(string.Format("//div[@class='dropdown-menu open']/ul[@aria-expanded='true']/li/a/span[@class='text']")),
                ElementCriteria.IsVisible);

            // Find all the list items, then click on the item with the index
            IList<IWebElement> items = Browser.FindElements(By.XPath(string.Format("//div[@class='dropdown-menu open']/ul[@aria-expanded='true']/li/a/span[@class='text'][1]")));

            // If the item is checked, click it to uncheck. To determine this, we will first try to find the child checkmark element that 
            // appears if an item is selected. If it exists and is visible, then it is checked
            if (items[index].Exists(By.XPath("following-sibling::span"), ElementCriteria.IsVisible))
            {
                items[index].Click(Browser);
            }

            btnElem.SendKeys(Keys.Escape);
        }

        /// <summary>
        /// Selects a user-specified numbers of random item within Fireballs multi select Select Element. If you are only executing Fireball tests 
        /// in Chrome and Edge, you can instead just use 
        /// Selenium's SelectElement class existing methods. However, if you are executing Fireball tests in Firefox, you will notice that 
        /// Selenium's SelectElement class existing methods do not work in Firefox. This method works with Firefox
        /// </summary>
        /// <param name="Browser">The driver instance</param>
        /// <param name="btnElem">The Button element that is always beneath the Select element in the HTML of Fireball applications</param>
        /// <param name="numberOfItemsToSelect">The number of random items you want to select</param>
        /// <returns></returns>
        public static string DropdownMulti_Fireball_SelectRandomItems(IWebDriver Browser, IWebElement btnElem, int numberOfItemsToSelect)
        {
            string selectedOptions = null;

            // Expand the drop down and wait for the items to appear
            btnElem.Click(Browser);
            Browser.WaitForElement(
                By.XPath(string.Format("//div[@class='dropdown-menu open']/ul[@aria-expanded='true']/li/a/span[@class='text']")),
                ElementCriteria.IsVisible);

            // Find all the list items, then click on the item with the index
            IList<IWebElement> items = Browser.FindElements(By.XPath(string.Format("//div[@class='dropdown-menu open']/ul[@aria-expanded='true']/li/a/span[@class='text'][1]")));


            List<string> listOfSelectedOptions = new List<string>();
            Random random = new Random();
            int randomInt = 0;
            List<int> listOfIntsUsed = new List<int>();
            int countOfItemsInList = items.Count;

            for (var i = 0; i < numberOfItemsToSelect; i++)
            {
                IList<IWebElement> SelectedEleOptions = Browser.FindElements(By.XPath(string.Format("//div[@class='dropdown-menu open']/ul[@aria-expanded='true']/li[@class='selected']")));
                // If all the items in the list are already selected, exit above for loop
                if (SelectedEleOptions.Count == countOfItemsInList)
                {
                    break;
                }

                for (var j = 0; j < 100; j++)
                {
                    if (listOfIntsUsed.Contains(randomInt))
                    {
                        randomInt = DataUtils.GetRandomIntegerWithinRange(0, countOfItemsInList);
                    }
                    if (!listOfIntsUsed.Contains(randomInt))
                    {
                        break;
                    }
                }

                // If the item is checked, click it to uncheck. To determine this, we will first try to find the child checkmark element that 
                // appears if an item is selected. If it exists and is visible, then it is checked
                if (!items[randomInt].Exists(By.XPath("following-sibling::span"), ElementCriteria.IsVisible))
                {
                    items[randomInt].Click(Browser);
                }

                listOfIntsUsed.Add(randomInt);
            }

            IList<IWebElement> SelectedEleOpts = Browser.FindElements(By.XPath(string.Format("//div[@class='dropdown-menu open']/ul[@aria-expanded='true']/li[@class='selected']")));

            // Store all the elements selected above into a comma separated string
            IList<IWebElement> selectedElements = SelectedEleOpts;

            foreach (var elem in selectedElements)
            {
                listOfSelectedOptions.Add(elem.FindElement(By.XPath("./a/span")).Text);
            }

            selectedOptions = string.Join(", ", listOfSelectedOptions);
            btnElem.SendKeys(Keys.Escape);
            return selectedOptions;
        }

        #endregion dropdown fireball

        #region TextBox

        /// <summary>
        /// This should be used to enter text into a text box for all text inside bootstrap forms. The javascript is needed to run so that the entered values
        /// do not get lost after the text is written into the text boxes. This issue occurs in IE all the time, and in Firefox less frequently. For background
        /// on the subject, see (http://stackoverflow.com/questions/9505588/selenium-webdriver-is-clearing-out-fields-after-sendkeys-had-previously-populate)
        /// If you want to see this issue, use SendKeys (without this method) inside a bootstrap form, then click Save. Notice
        /// that you receive an AJAX error, and the Web App log (if DEV provides one for your Web App) will show that there were no values in the text boxes when
        /// Save was clicked.
        /// </summary>
        /// <param name=browser">The driver instance"</param>
        /// <param name=elem">The element to enter text into"</param>
        /// <param name=clearText">Whether you want to clear the text before you enter text or not"</param>
        /// <param name=text">The exact string you want to enter"></param>
        public static void TextBox_EnterText(IWebDriver browser, IWebElement elem, bool clearText, string text)
        {
            elem.Click();

            if (clearText == true)
            {
                elem.Clear();
            }
            elem.SendKeys(text);

            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)browser;
            jsExecutor.ExecuteScript("$(arguments[0]).change();", elem);
        }

        #endregion TextBox

        #region Grids

        /// <summary>
        /// Expands or collapses an entire table, or row within a table. You may or may not have to add additional logic to make it work 
        /// with your individual application
        /// </summary>
        /// <param name="Browser">The driver instance</param>
        /// <param name="tblElem">You table element that is found within the your Page class. i.e. Page.MyTableNameTbl</param>
        /// <param name="groupdItemToExpandOrCollapse">The exact text of the row name with the the expansion/collapse icon</param>
        /// <param name="expandOrCollapse">Either "expand" or "collapse"</param>
        public static void Grid_ExpandOrCollapseButton(IWebDriver Browser, IWebElement tblElem, string groupdItemToExpandOrCollapse,
            string expandOrCollapse)
        {
            IWebElement rowToExpandOrCollapse = null;
            IWebElement expandableElem = null;
            IWebElement collapsableElem = null;

            // I had trouble locating this element. At first I used //td[contains(text(),'{0}')] But that did not work. After posting to stackoverflow, I was told 
            // that whenever an element has 2 text nodes (this one does), I need to use this the Xpath like this instead //td[contains(., '{0}')] as that one
            // searches all text nodes of an element. If you object inspect the "Transition to discipline" row in the Program Learning Plan tab of the Learner page,
            // you will see that the element has comment text (green text) before and after a span element within it. That means it has 2 text nodes
            // https://stackoverflow.com/questions/45446631/using-xpath-contains-function-to-find-element-that-contains-text?noredirect=1#comment77872147_45446631
            string xpath = string.Format("//td[contains(., '{0}')]", groupdItemToExpandOrCollapse);
            // string xpath = string.Format("//tr[td//text()[contains(., '{0}')]]", groupdItemToExpandOrCollapse);

            //a[text()='Sartaj Gill']/preceding-sibling::span//img

            // Get the group item to expand or collapse:        
            // If there are no elements found when using the xpath with the td tag, then that means we are trying to expand an entire table, so
            // we will then just locate the expand/collapse icon within the table
            int count = tblElem.FindElements(By.XPath(xpath)).Count;
            if (tblElem.FindElements(By.XPath(xpath)).Count == 0)
            {
                expandableElem = tblElem.FindElements(By.TagName("img"))[0];
                collapsableElem = tblElem.FindElements(By.TagName("img"))[1];
            }
            // else we need to locate the expand/collpase icon within the td tag
            else
            {
                rowToExpandOrCollapse = tblElem.FindElement(By.XPath(xpath));
                expandableElem = rowToExpandOrCollapse.FindElements(By.TagName("img"))[0];
                collapsableElem = rowToExpandOrCollapse.FindElements(By.TagName("img"))[1];
            }

            if (expandOrCollapse == "expand")
            {
                // If the expandable button is displayed, then we should click it to expand. If not, it is already expanded
                if (expandableElem.Displayed)
                {
                    expandableElem.Click();
                }
            }
            else
            {
                // If the collapsable button is displayed, then we should click it to collapse. If not, it is already collapsed
                if (collapsableElem.Displayed)
                {
                    collapsableElem.Click();
                }
            }
            Thread.Sleep(03000);
        }

        /// <summary>
        /// Clicks any cell inside of a grid by the cell text of that cell
        /// </summary>
        /// <param name="Browser">The driver instance</param>
        /// <param name="tblBodyElem">You table body element that is found within the your Page class. i.e. Page.MyTableNameTblBody</param>
        /// <param name="cellText">The exact cell text inside the cell"</param>
        /// <param name="rowNum">If you want to specify a row that the cell text exists on</param>
        /// <param name="xpathForRows">(Optional). The xpath for your table's rows. Default = '//tr'</param>
        /// <param name="xpathForCellText">(Optional). The xpath that contains your table's cells. 
        /// Default = //*[local-name(.)='th' or local-name(.)='td']</param>
        public static void Grid_ClickCellByCellText(IWebDriver Browser, IWebElement tblBodyElem, string cellText, int? rowNum = null,
            string xpathForRows = "//tr",
            string xpathForCellText = "//*[local-name(.)='th' or local-name(.)='td']")
        {
            //Get table rows. Have to add a dot before the xpath, else Selenium looks inside entire
            // HTML as opposed to only within the child elements of tblElemOrTblBodyElem
            IList<IWebElement> allRows = tblBodyElem.FindElements(By.XPath("." + xpathForRows));

            IWebElement cellToClick = null;
            bool cellFound = false;

            // If the tester specified he wants to find the cell within a specific row
            if (rowNum != null)
            {
                int nonNullableInt = rowNum.Value;
                IList<IWebElement> cells =
                    allRows[nonNullableInt].FindElements(By.XPath("." + xpathForCellText));

                foreach (IWebElement cell in cells)
                {
                    string blah = cell.GetAttribute("textContent").Trim();
                    if (cell.GetAttribute("textContent").Trim() == cellText)
                    {
                        cellToClick = cell;
                        cellFound = true;
                        break;
                    }
                }

                if (cellFound == false)
                {
                    throw new Exception("Unable to find the cell with the cell text you provided");
                }

            }

            // Else find the cell throughout all rows
            else
            {
                foreach (IWebElement row in allRows)
                {
                    //Find all cells in the row
                    IList<IWebElement> cells = row.FindElements(By.XPath("." + xpathForCellText));

                    foreach (IWebElement cell in cells)
                    {
                        if (cell.GetAttribute("textContent") == cellText)
                        {
                            cellToClick = cell;
                            cellFound = true;
                            break;
                        }
                    }

                    if (cellFound)
                    {
                        break;
                    }
                }
            }

            if (!cellFound)
            {
                throw new Exception(string.Format("Could not find the cell text of {0}", cellText));
            }

            cellToClick.Click();
        }

        /// <summary>
        /// Clicks a cell inside of a grid by the user-specified column index
        /// </summary>
        /// <param name="Browser">The driver instance</param>
        /// <param name="RowElem">The row element. To get the row, call
        /// <see cref="ElemGet.Grid_GetRowByRowName(Browser, IWebDriver, IWebElement, By, string, string, int)"/></param>
        /// <param name="colIndex">The column index starting with zero></param>
        /// <param name="xpathForCells">(Optional) If your table's cell elements are not stored within TD tags, then you need to pass the xpath of your
        /// tables cell elements. That is because this method, by default, searches only for TD tagged elements when looking for cells, so if your 
        /// developers do not store your tables cells inside TD tags, then pass the xpath which will satisfy all cells in your table, then this 
        /// method will use this xpath instead when looking for cells. For example, your cell might be stored within the div tag and 
        /// might include a "role" attribute with an attribute value of "gridCell", so you would pass //div[@role='gridcell']. 
        /// Default = //tr</param>
        public static void Grid_ClickCellByColIndex(IWebDriver Browser, IWebElement RowElem, int colIndex, string xpathForCells = "//td")
        {
            IWebElement cell = null;

            cell = RowElem.FindElements(By.XPath("." + xpathForCells))[colIndex];

            cell.Click();
        }

        /// <summary>
        /// Hovers over any cell inside of a grid by the cell text of that cell
        /// </summary>
        /// <param name="Browser">The driver instance</param>
        /// <param name="tblElem">You table element that is found within the your Page class. i.e. Page.MyTableNameTbl</param>
        /// <param name=cellText">The exact cell text inside the cell you want to click></param>
        /// <param name="xpathForRows">(Optional). The xpath for your table's rows. Default = '//tr'</param>
        /// <param name="xpathForCellText">(Optional). The xpath that contains your table's cells. 
        /// Default = //*[local-name(.)='th' or local-name(.)='td']</param>
        public static void Grid_HoverOverCellByCellText(IWebDriver Browser, IWebElement tblElem, string cellText, string xpathForRows = "//tr",
            string xpathForCellText = "//*[local-name(.)='th' or local-name(.)='td']")
        {
            IWebElement cellToHover = null;
            bool cellFound = false;

            //Get table rows. Have to add a dot before the xpath, else Selenium looks inside entire
            // HTML as opposed to only within the child elements of tblElemOrTblBodyElem
            IList<IWebElement> allRows = tblElem.FindElements(By.XPath("." + xpathForRows));

            // Loop through all rows
            foreach (IWebElement row in allRows)
            {
                //Find all cells in the current row
                IList<IWebElement> cells = row.FindElements(By.XPath("." + xpathForCellText));
                List<string> cellContent = new List<string>();

                foreach (IWebElement cell in cells)
                {
                    if (cell.GetAttribute("textContent") == cellText)
                    {
                        cellToHover = cell;
                        cellFound = true;
                        break;
                    }
                }

                if (cellFound)
                {
                    break;
                }
            }

            if (!cellFound)
            {
                throw new Exception(string.Format("Could not find the cell text of {0}", cellText));
            }

            Actions action = new Actions(Browser);
            action.MoveToElement(cellToHover).Perform();
        }

        /// <summary>
        /// Clicks a row by row number
        /// </summary>
        /// <param name="Browser">The driver instance</param>
        /// <param name="tblBodyElem">You table body element that is found within the your Page class. i.e. Page.MyTableNameTblBody</param>
        /// <param name="rowNumber">The non-zero-based row number</param>
        /// <param name="xpathForRows">(Optional). The xpath for your table's rows. Default = '//tr'</param>
        public static void Grid_ClickRowByRowNumber(IWebDriver Browser, IWebElement tblBodyElem, int rowNumber, string xpathForRows = "//tr")
        {
            if (tblBodyElem.FindElements(By.XPath("." + xpathForRows)).Count == 0)
            {
                throw new Exception(string.Format("Could not find any rows with the xpath of {0}", xpathForRows));
            }
            IWebElement rowToClick = tblBodyElem.FindElements(By.XPath("." + xpathForRows))[rowNumber - 1];
            rowToClick.Click();
        }

        /// <summary>
        /// Hovers over a row by row number
        /// </summary>
        /// <param name="Browser">The driver instance</param>
        /// <param name="tblBodyElem">You table body element that is found within the your Page class. i.e. Page.MyTableNameTblBody</param>
        /// <param name="rowNumber">The non-zero-based row number</param>
        /// <param name="xpathForRows">(Optional). The xpath for your table's rows. Default = '//tr'</param>
        public static void Grid_HoverOverRowByRowNumber(IWebDriver Browser, IWebElement tblBodyElem, int rowNumber, string xpathForRows = "//tr")
        {
            if (tblBodyElem.FindElements(By.XPath("." + xpathForRows)).Count == 0)
            {
                throw new Exception(string.Format("Could not find any rows with the xpath of {0}", xpathForRows));
            }
            IWebElement rowToClick = tblBodyElem.FindElements(By.XPath("." + xpathForRows))[rowNumber - 1];
            Actions action = new Actions(Browser);
            action.MoveToElement(rowToClick).Perform();
        }

        /// <summary>
        /// Clicks on a button within a row on a table and then clicks the user-specified menu item that appears from that button
        /// </summary>
        /// <param name="Browser">The driver instance</param>
        /// <param name="parentElem">The parent element that contains all of the menu items within the dropdown. </param>
        /// <param name="menuItemText">The exact text from the menu item after the button is clicked</param>
        public static void Grid_ClickMenuItemInsideDropdown(IWebDriver Browser, IWebElement parentElem, string menuItemText)
        {
            IWebElement menuItemElem = ElemGet.Grid_GetMenuItemOnRowButton(Browser, parentElem, menuItemText);

            // We need to use javascript based clicks because IE can not Selenium click dropdown menu items in some grids.
            // For example, the Lifetime Support grids
            menuItemElem.ClickJS(Browser);
        }

        /// <summary>
        /// Clicks the user-specified element (i.e. button/link) within a table by specifying the row name
        /// </summary>
        /// <param name="Browser">The driver instance</param>
        /// <param name="tblElem">You table element that is found within the your Page class. i.e. OP.PendingAcceptanceTbl</param>
        /// <param name="rowElemBy">Send any row of your table in it's By type. i.e. Bys.MyPage.MyTableNameTblBodyRow</param>
        /// <param name="rowName">The name of the row. i.e. The exact text from the column which 
        /// contains the unique identifiers for this table. Every table has a column which contains unique strings. This parameter
        /// represents that string. IMPORTANT: Be sure to send the EXACT text from the text() attribute value within the 
        /// DevTools->Elements tab. In some cases, the cell text can have leading and 
        /// trailing whitespace. Include those whitespaces when you send this string to this parameter</param>
        /// <param name="htmlTagNameForRowName">The HTML tag for rowName. For example, inside DevTools, inspect the element 
        /// that you passed for the rowName parameter. The tagname for that element is what you pass here</param>
        /// <param name="htmlTagNameForButtonOrLink">The HTML tag for the button or link</param>
        /// <param name="partialClassAttributeValueForButtonOrLink">The Class attribute value of the button element. This will be 
        /// used in a contains function, so a partial string of that Class attribute value can be used if needed</param>
        public static IWebElement Grid_ClickButtonOrLinkWithoutTextByPartialClassName(IWebDriver Browser, IWebElement tblElem,
            By rowElemBy, string rowName, string htmlTagNameForRowName,
            string htmlTagNameForButtonOrLink, string partialClassAttributeValueForButtonOrLink)
        {
            IWebElement btnOrLnk = ElemGet.Grid_GetButtonOrLinkInsideRowByPartialClassName(Browser, tblElem, rowElemBy, rowName,
               htmlTagNameForRowName, htmlTagNameForButtonOrLink, partialClassAttributeValueForButtonOrLink);

            ElemSet.ScrollToElement(Browser, btnOrLnk);
            Thread.Sleep(600);

            // Sometimes in Fireball applications, whenever we scroll to an element (above line of code), the applications 
            // reloads. I have no idea why this happens. But it does, and to make this work, we have to wait until
            // the JQuery is finished loading. Note that some applications dont use JQuery, so we have to check if the 
            // application has JQuery first,  if so, then wait for it, otherwise if the application does not have it and 
            // we call the WaitJSandJQuery method, it will fail
            if (AppUtils.ApplicationHasJQuery(Browser, TimeSpan.FromSeconds(10)))
            {
                Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));
            }

            btnOrLnk.Click();
            return btnOrLnk;
        }

        /// <summary>
        /// Clicks on any element (i.e. button/link) within a row on a table by specifying that element's cell text. 
        /// </summary>
        /// <param name="Browser">The driver instance</param>
        /// <param name="tblElem">You table element that is found within the your Page class. i.e. OP.PendingAcceptanceTbl</param>
        /// <param name="rowElemBy">Send any row of your table in it's By type. i.e. Bys.MyPage.MyTableNameTblBodyRow</param>
        /// <param name="rowName">The name of the row. i.e. The exact text from the column which 
        /// contains the unique identifiers for this table. Every table has a column which contains unique strings. This parameter
        /// represents that string. IMPORTANT: Be sure to send the EXACT text from the text() attribute value within the 
        /// DevTools->Elements tab. In some cases, the cell text can have leading and 
        /// trailing whitespace. Include those whitespaces when you send this string to this parameter</param>
        /// <param name="htmlTagNameForRowName">The HTML tag for rowName. For example, inside DevTools, inspect the element 
        /// that you passed for the rowName parameter. The tagname for that element is what you pass here</param>
        /// <param name="btnText">The exact text from the element (i.e. button/link) you want to click</param>
        /// <param name="htmlTagNameForBtnTxt">The HTML tag for btnText</param>
        /// <param name="additionalColCellText">(Optional) If your rowName does not have to be unique compared to other rows in 
        /// your table and/or you would want to specify an additional column value to find your row, you can do that here. Send the 
        /// exact text of any other column's cell text for your row.</param>
        /// <param name="htmlTagNameForAdditionalColCellText">(Optional) The HTML tag for additionalColumnCellText. Default = null</param> 
        /// <param name="insideiFrame">(Optional) Send 'true' if the button or link is inside an iFrame</param>
        public static IWebElement Grid_ClickButtonOrLinkWithinRow(IWebDriver Browser, IWebElement tblElem, By rowElemBy,
            string rowName, string htmlTagNameForRowName, string btnText, string htmlTagNameForBtnTxt,
            string additionalColCellText = null, string htmlTagNameForAdditionalColCellText = null, bool insideiFrame = false)
        {
            if (string.IsNullOrEmpty(rowName))
            {
                throw new Exception("You must send a value for the uniqueID parameter");
            }

            IWebElement btnOrLnkElem = ElemGet.Grid_GetButtonOrLinkInsideRowByText(Browser, tblElem, rowElemBy, rowName,
                htmlTagNameForRowName, btnText, htmlTagNameForBtnTxt, additionalColCellText, htmlTagNameForAdditionalColCellText,
                insideiFrame);

            ElemSet.ScrollToElement(Browser, btnOrLnkElem, insideiFrame);
            Thread.Sleep(600);

            // Sometimes in Fireball applications, whenever we scroll to an element (above line of code), the applications 
            // reloads. I have no idea why this happens. But it does, and to make this work, we have to wait until
            // the JQuery is finished loading. Note that some applications dont use JQuery, so we have to check if the 
            // application has JQuery first,  if so, then wait for it, otherwise if the application does not have it and 
            // we call the WaitJSandJQuery method, it will fail
            if (AppUtils.ApplicationHasJQuery(Browser, TimeSpan.FromSeconds(10)))
            {
                Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));
            }

            btnOrLnkElem.Click();
            return btnOrLnkElem;
        }

        /// <summary>
        /// Hovers over any element (i.e. button/link) within a row on a table by specifying that element's cell text. 
        /// </summary>
        /// <param name="Browser">The driver instance</param>
        /// <param name="tblElem">You table element that is found within the your Page class. i.e. OP.PendingAcceptanceTbl</param>
        /// <param name="rowElemBy">Send any row of your table in it's By type. i.e. Bys.MyPage.MyTableNameTblBodyRow</param>
        /// <param name="rowName">The name of the row. i.e. The exact text from the column which 
        /// contains the unique identifiers for this table. Every table has a column which contains unique strings. This parameter
        /// represents that string. IMPORTANT: Be sure to send the EXACT text from the text() attribute value within the 
        /// DevTools->Elements tab. In some cases, the cell text can have leading and 
        /// trailing whitespace. Include those whitespaces when you send this string to this parameter</param>
        /// <param name="htmlTagNameForRowName">The HTML tag for rowName. For example, inside DevTools, inspect the element 
        /// that you passed for the rowName parameter. The tagname for that element is what you pass here</param>
        /// <param name="btnText">The exact text from the element (i.e. button/link) you want to click</param>
        /// <param name="htmlTagNameForBtnTxt">The HTML tag for btnText</param>
        /// <param name="additionalColCellText">(Optional) If your rowName does not have to be unique compared to other rows in 
        /// your table and/or you would want to specify an additional column value to find your row, you can do that here. Send the 
        /// exact text of any other column's cell text for your row.</param>
        /// <param name="htmlTagNameForAdditionalColCellText">(Optional) The HTML tag for additionalColumnCellText. Default = null</param> 
        /// <param name="insideiFrame">(Optional) Send 'true' if the button or link is inside an iFrame</param>
        public static IWebElement Grid_HoverButtonOrLinkWithinRow(IWebDriver Browser, IWebElement tblElem, By rowElemBy, string rowName,
            string htmlTagNameForRowName, string btnText, string htmlTagNameForBtnTxt, string additionalColCellText = null,
            string htmlTagNameForAdditionalColCellText = null, bool insideiFrame = false)
        {
            IWebElement btnOrLnkElem = ElemGet.Grid_GetButtonOrLinkInsideRowByText(Browser, tblElem, rowElemBy, rowName,
                htmlTagNameForRowName, btnText, htmlTagNameForBtnTxt, additionalColCellText, htmlTagNameForAdditionalColCellText,
                insideiFrame);

            ElemSet.ScrollToElement(Browser, btnOrLnkElem, insideiFrame);
            Thread.Sleep(600);

            Actions builder = new Actions(Browser);
            builder.MoveToElement(btnOrLnkElem).Build().Perform();

            return btnOrLnkElem;
        }

        /// <summary>
        /// Selects an item in a Select Element within a row on a table by specifying the row name and Select Element ID
        /// </summary>
        /// <param name="Browser">The driver instance</param>
        /// <param name="tblElem">You table element that is found within the your Page class. i.e. OP.PendingAcceptanceTbl</param>
        /// <param name="rowElemBy">Send any row of your table in it's By type. i.e. Bys.MyPage.MyTableNameTblBodyRow</param>
        /// <param name="rowName">The name of the row. i.e. The exact text from the column which 
        /// contains the unique identifiers for this table. Every table has a column which contains unique strings. This parameter
        /// represents that string. IMPORTANT: Be sure to send the EXACT text from the text() attribute value within the 
        /// DevTools->Elements tab. In some cases, the cell text can have leading and 
        /// trailing whitespace. Include those whitespaces when you send this string to this parameter</param>
        /// <param name="htmlTagNameForRowName">The HTML tag for rowName. For example, inside DevTools, inspect the element 
        /// that you passed for the rowName parameter. The tagname for that element is what you pass here</param>
        /// <param name="idOfSelElem">The exact text of the ID of the Select Element, however, if your select element is dynamically numbered 
        /// per row, then only send the text before the number. For example, is the select tag has an ID of "Priority_0", 
        /// only send "Priority"</param>
        /// <param name="itemToChoose">The exact text of the item you want to choose</param>
        /// <param name="additionalColCellText">(Optional) If the first column in your row does not have to be unique compared to other rows in your table, and you would want to specify an additional column value to find your row, you can do that here. Send the exact text of any other column.</param>
        /// <param name="tagNameWhereAddColCellTextExists">(Optional) The HTML tag name where the additionalColumnCellText exists</param>
        public static void Grid_SelectItemWithinSelElem(IWebDriver Browser, IWebElement tblElem, By rowElemBy, string rowName, string htmlTagNameForRowName,
            string idOfSelElem, string itemToChoose, string additionalColCellText = null, string tagNameWhereAddColCellTextExists = null)
        {
            SelectElement selElem = ElemGet.Grid_GetSelElemInsideRowByID(Browser, tblElem, rowElemBy, rowName, htmlTagNameForRowName,
                idOfSelElem, additionalColCellText, tagNameWhereAddColCellTextExists);
            selElem.SelectByText(itemToChoose);
        }

        /// <summary>
        /// Selects an item in a Select Element within a row on a table by specifying Select Element ID
        /// </summary>
        /// <param name="Browser">The driver instance</param>
        /// <param name="idOfSelElem">The exact text of the ID of the Select Element</param>
        /// <param name="itemToChoose">The exact text of the item you want to choose</param>
        public static void Grid_SelectItemWithinSelElemBySelElemID(IWebDriver Browser, string idOfSelElem, string itemToChoose)
        {
            SelectElement selElem = new SelectElement(Browser.FindElement(By.Id(idOfSelElem)));
            selElem.SelectByText(itemToChoose);
        }

        /// <summary>
        /// Enters text within a text box within a row on a table by specifying the text box ID
        /// </summary>
        /// <param name="Browser">The driver instance</param>
        /// <param name="idOfTextBox">The exact text of the ID of the text box</param>
        /// <param name="textToEtner">The text you want to enter</param>
        public static void Grid_EnterTextWithinTextBoxByTextBoxID(IWebDriver Browser, string idOfTextBox, string textToEtner)
        {
            IWebElement txt = Browser.FindElement(By.Id(idOfTextBox));
            txt.SendKeys(textToEtner);
        }

        /// <summary>
        /// Clicks on an element within a user-specified row, such as a check box, or an X icon, or a + icon, or a Pencil Icon
        /// </summary>
        /// <param name="Browser">The driver instance</param>
        /// <param name="row">The row that contains the element you want to click on. To get the row, 
        /// <see cref="ElemGet.Grid_GetRowByRowName(Browser, IWebElement, By, string)"/></param>
        /// <param name="xpathOfElementToClick">The elements xpath.</param>
        /// <param name="indexOfElemToClick">(Optional). If your row has multiple elements with the same HTML, you can specify the index 
        /// at which you tag name you want to click. Default is 0 for the first instance</param>
        public static void Grid_ClickButtonOrLinkWithoutTextWithinRow(IWebDriver Browser, IWebElement row, string xpathOfElementToClick,
            int indexOfElemToClick = 0)
        {
            string xpathOfElementToClick_Modified = xpathOfElementToClick;

            // When I first created this, I made it so that the first 2 slashes were not needed. Since then, people have been sending 2
            // slashes, so im just going to remove them here
            if (xpathOfElementToClick.StartsWith("//"))
            {
                xpathOfElementToClick_Modified = xpathOfElementToClick.Remove(0, 2);
            }

            string xpathString = string.Format("./descendant::{0}[not(@type='hidden')]", xpathOfElementToClick_Modified);

            var elem = row.FindElements(By.XPath(xpathString));

            elem[indexOfElemToClick].Click();
        }

        #endregion Grids

        #region Buttons


        #endregion Buttons

        #region Radio buttons

        /// <summary>
        /// Clicks a radio button of your choice. 
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="textOfRadioBtn">The exact text as it appears in the HTML of the radio button to click</param>
        /// <returns></returns>
        public static string RdoBtn_ClickByText(IWebDriver browser, string textOfRadioBtn)
        {
            IWebElement rdoBtn = ElemGet.RdoBtn_GetRdoBtnByText(browser, textOfRadioBtn);
            // rdoBtn.Click();
            rdoBtn.ClickJS(browser);
            return textOfRadioBtn;
        }

        /// <summary>
        /// Clicks a radio button of your choice that contains the text you specified. Use this instead of
        /// <see cref="RdoBtn_ClickByText(IWebDriver, string)"/> if your applications radio buttons have leading
        /// or trailing white space, or end lines, etc.
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="textOfRadioBtn">The exact text as it appears in the HTML of the radio button to click</param>
        /// <returns></returns>
        public static string RdoBtn_ClickByTextContains(IWebDriver browser, string textOfRadioBtn)
        {
            IWebElement rdoBtn = ElemGet.RdoBtn_GetRdoBtnByTextContains(browser, textOfRadioBtn);
            // rdoBtn.Click();
            rdoBtn.ClickJS(browser);
            return textOfRadioBtn;
        }

        /// <summary>
        /// Clicks a radio button of your choice contained within a specified parent element
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="textOfRadioBtn">The exact text as it appears in the HTML of the radio button to click</param>
        /// <param name="ParentElem">The parent element to find your check box in</param>
        /// <returns></returns>
        public static string RdoBtn_ClickByText(IWebDriver browser, string textOfRadioBtn, IWebElement ParentElem)
        {
            IWebElement rdoBtn = ElemGet.RdoBtn_GetRdoBtnByText(browser, textOfRadioBtn, ParentElem);
            rdoBtn.ClickJS(browser);
            return textOfRadioBtn;
        }

        /// <summary>
        /// Selects multiple radio buttons of your choice
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="textOfRadioBtns">The exact text as it appears in the HTML of the radio buttons to click</param>
        /// <returns></returns>
        public static void RdoBtn_ClickMultipleByText(IWebDriver browser, params string[] textOfRadioBtns)
        {
            foreach (string textOfRadioBtn in textOfRadioBtns)
            {
                RdoBtn_ClickByText(browser, textOfRadioBtn);
            }
        }

        /// <summary>
        /// Clicks on a random radio button within a "table" tag
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="textOfRadioBtn">The exact text of one of the radio buttons inside</param>
        public static string RdoBtn_ClickRandom(IWebDriver browser, string textOfRadioBtn)
        {
            IWebElement rdoBtn = ElemGet.RdoBtn_GetRdoBtnByText(browser, textOfRadioBtn);
            IList<IWebElement> rdoBtns = ElemGet.RdoBtn_GetRdoBtns(rdoBtn);

            Random r = new Random();
            int randomIndex = r.Next(rdoBtns.Count); //Getting a random value that is between 0 and (list's size)-1
            rdoBtns[randomIndex].Click();
            return rdoBtns[randomIndex].Text;
        }

        #endregion Radio bnuttons

        #region Check boxes

        /// <summary>
        /// Clicks a check box of your choice. 
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="textOfCheckBox">The exact text as it appears in the HTML of the check box to click</param>
        /// <param name="indexOfCheckBoxWithSameText">If there are multiple check boxes on the same page with the same text, you can specify
        /// at which index you want to click</param>
        /// <returns></returns>
        public static string ChkBx_ClickByText(IWebDriver browser, string textOfCheckBox, int indexOfCheckBoxWithSameText = 0)
        {
            IWebElement chkBx = ElemGet.ChkBx_GetCheckBox(browser, textOfCheckBox, indexOfCheckBoxWithSameText);
            chkBx.Click();
            return textOfCheckBox;
        }

        /// <summary>
        /// Clicks a check box of your choice contained within a specified parent element
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="textOfCheckBox">The exact text as it appears in the HTML of the check box to click</param>
        /// <param name="ParentElem">The parent element to find your check box in</param>
        /// <param name="indexOfCheckBoxWithSameText">If there are multiple check boxes on the same page with the same text, you can specify
        /// at which index you want to click</param>
        /// <returns></returns>
        public static string ChkBx_ClickByText(IWebDriver browser, string textOfCheckBox, IWebElement ParentElem, int indexOfCheckBoxWithSameText = 0)
        {
            IWebElement chkBx = ElemGet.ChkBx_GetCheckBox(browser, textOfCheckBox, ParentElem, indexOfCheckBoxWithSameText);
            chkBx.Click();
            return textOfCheckBox;
        }

        /// <summary>
        /// Clicks on a random check box that is contained within a "div" tag with a class attribute value of "form-group"
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="textOfChkBx">The exact text of one of the check box that you want to click</param>
        public static string ChkBx_ChooseRandom(IWebDriver browser, string textOfChkBx)
        {
            IWebElement chkBx = ElemGet.ChkBx_GetCheckBox(browser, textOfChkBx);
            IList<IWebElement> chkBxs = ElemGet.ChkBx_GetListOfChkBxsWithinForm(chkBx);

            Random r = new Random();
            int randomIndex = r.Next(chkBxs.Count); //Getting a random value that is between 0 and count of items
            chkBxs[randomIndex].Click();
            return chkBxs[randomIndex].Text;
        }

        #endregion Check boxes

        #region General   

        /// <summary>
        /// Scrolls vertically to a specified element within the active window. This only scrolls on the main Browser window scroll bar,
        /// not any embedded scroll bars. 
        /// </summary>
        /// <param name="browser">The driver</param>
        /// <param name="divElem">The element to scroll to</param>
        /// <param name="insideiFrame">(Optional) send 'true' if the element you want to scroll to is inside of an iframe</param>
        public static void ScrollToElement(IWebDriver browser, IWebElement elem, bool insideiFrame = false)
        {
            // If the element is inside an iframe, then the javascript scroll will not work. In that case, we can use the MoveToElement method
            // Note that this method also hovers the mouse curser over the element, so it is not the ideal way to scroll, which is why we are
            // using javascript for all non-iframe elements
            if (insideiFrame)
            {
                Actions actions = new Actions(browser);
                actions.MoveToElement(elem).Build().Perform();
            }
            else
            {
                //((IJavaScriptExecutor)browser).ExecuteScript("window.scrollTo(0," + elem.Location.Y + ")");
                ((IJavaScriptExecutor)browser).ExecuteScript("arguments[0].scrollIntoView(true);", elem);
            }

            Thread.Sleep(0100);
        }

        /// <summary>
        /// Scrolls horizontally or vertically within an element that contains a scroll bar. This will scroll to the 
        /// tester-specified element
        /// </summary>
        /// <param name="browser">The driver</param>
        /// <param name="divElem">This is the div element that contains the scroll bar. It is NOT the 
        /// main Browser window's scroll bar</param>
        /// <param name="elemToScrollTo">The element the tester wants to scroll to</param>
        /// <param name="HorizontalOrVertical">'Horizontal' or 'Vertical'</param>
        public static void ScrollToElementWithinScrollBar(IWebDriver browser, IWebElement divElem, IWebElement elemToScrollTo, string HorizontalOrVertical)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)browser;

            if (HorizontalOrVertical == "Vertical")
            {
                js.ExecuteScript("arguments[0].scrollTop = arguments[1];", divElem, elemToScrollTo.Location.Y);
            }

            if (HorizontalOrVertical == "Horizontal")
            {
                // Scroll inside the popup frame element vertically. See the following...
                // http://stackoverflow.com/questions/22709200/selenium-webdriver-scrolling-inside-a-div-popup
                js.ExecuteScript("arguments[0].scrollLeft = arguments[1];", divElem, elemToScrollTo.Location.X);
            }
        }


        /// <summary>
        /// Scrolls horizontally or vertically within an element that contains a scroll bar. This will scroll to the 
        /// tester-specified X or Y coordinate 
        /// </summary>
        /// <param name="browser">The driver</param>
        /// <param name="divElem">This is the div element that contains the scroll bar. It is NOT the 
        /// main Browser window's scroll bar</param>
        /// <param name="xOrYCoordinate">The X or the Y coordinate</param>
        /// <param name="HorizontalOrVertical">'Horizontal' or 'Vertical'</param>
        public static void ScrollToXorYCoordinateWithinScrollBar(IWebDriver browser, IWebElement divElem, int xOrYCoordinate, string HorizontalOrVertical)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)browser;

            if (HorizontalOrVertical == "Vertical")
            {
                js.ExecuteScript("arguments[0].scrollTop = arguments[1];", divElem, xOrYCoordinate);
            }

            if (HorizontalOrVertical == "Horizontal")
            {
                // Scroll inside the popup frame element vertically. See the following...
                // http://stackoverflow.com/questions/22709200/selenium-webdriver-scrolling-inside-a-div-popup
                js.ExecuteScript("arguments[0].scrollLeft = arguments[1];", divElem, xOrYCoordinate);
            }
        }

        /// <summary>
        /// Scrolls down one page as if the manual user pressed the Page Down key on the keyboard
        /// </summary>
        /// <param name="browser">The driver</param>
        public static void ScrollPageDown(IWebDriver browser)
        {
            Actions action = new Actions(browser);
            action.SendKeys(Keys.PageDown).Perform();
        }

        /// <summary>
        /// Scrolls down one page inside of a grid that contains a scroll bar. This will scroll one page as if the manual user pressed the Page Down 
        /// key on the keyboard while the focus was inside of the grid
        /// </summary>
        /// <param name="browser">The driver</param>
        /// <param name="tableElem">This table element</param>
        public static void ScrollPageDownWIthinGrid(IWebDriver browser, IWebElement tableElem)
        {
            tableElem.Click();

            Actions action = new Actions(browser);
            action.MoveToElement(tableElem).Perform();
            action.SendKeys(Keys.PageDown).Perform();
        }

        /// <summary>
        /// Scrolls vertically to a specified Select element within the active window. This only scrolls on the main Browser window scroll bar,
        /// not any embedded scroll bars. 
        /// scroll bars
        /// <param name="browser">The driver</param>
        /// <param name="elem">The select element to scroll to</param>
        /// </summary>
        public static void ScrollToSelectElement(IWebDriver browser, SelectElement elem)
        {
            ((IJavaScriptExecutor)browser).ExecuteScript("arguments[0].scrollIntoView(true);", elem);
        }

        /// <summary>
        /// Scrolls to your element, then sends the string provided in the parameter. Use this when your SendKeys fails on small laptops or screens. 
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="elem">The element to scroll to then Send Keys to</param>
        /// <param name="keys">The String to be sent to the textbox</param>
        /// <param name="divElem">(Optional) If your element is inside of a sub-window or frame that contains a scroll bar, then you must send the scroll bar 
        /// element of the frame/window. This is usually a Div tag. If the element is on the main window, then leave this parameter blank</param>
        public static void SendKeysAfterScroll(IWebDriver browser, IWebElement elem, string keys, IWebElement divElem = null)
        {
            if (divElem == null)
            {
                ScrollToElement(browser, elem);
            }
            else
            {
                ScrollToElementWithinScrollBar(browser, divElem, elem, "Vertical");
            }

            elem.SendKeys(keys);
            elem.SendKeys(Keys.Tab);
        }

        /// <summary>
        /// Scrolls to your element, then clicks it. Use this when your click fails on small laptops or screens. The
        /// exception thrown in most of these cases is "Element is not clickable at point..."
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="elem">The element to scroll to then click on</param>
        /// <param name="divElem">(Optional) If your element is inside of a sub-window or frame that contains a scroll bar, then you must send the scroll bar 
        /// element of the frame/window. This is usually a Div tag. If the element is on the main window, then leave this parameter blank</param>
        public static void ClickAfterScroll(IWebDriver browser, IWebElement elem, IWebElement divElem = null)
        {
            if (divElem == null)
            {
                ScrollToElement(browser, elem);
            }
            else
            {
                ScrollToElementWithinScrollBar(browser, divElem, elem, "Vertical");
            }

            elem.Click();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="sourceElem">The element to drag</param>
        /// <param name="destinationElem">The element to drop</param>
        /// <param name="x">(Optional). If you want to drop to a specified coordinate on the drop element, then
        /// pass the X coordinate here</param>
        /// <param name="y">(Optional). If you want to drop to a specified coordinate on the drop element, then
        /// pass the Y coordinate here</param>
        public static void DragAndDropToElement(IWebDriver browser, IWebElement sourceElem, IWebElement destinationElem, int x = -1, int y = -1)
        {
            int Width = destinationElem.Size.Width;
            int Height = destinationElem.Size.Height;
            Console.WriteLine(Width);
            Console.WriteLine(Height);
            int MyX = (Width * x) / 100;//spot to drag to is at x of the width
            int MyY = (Height * y) / 100; ;//spot to drag to is at y of the height

            if (x == -1)
            {
                Actions builder = new Actions(browser);
                IAction dragAndDrop = builder.ClickAndHold(sourceElem)
                .MoveToElement(destinationElem)
                    .Release()
                    .Build();
                dragAndDrop.Perform();
            }
            else
            {
                Actions builder = new Actions(browser);
                IAction dragAndDrop = builder.ClickAndHold(sourceElem)
                .MoveToElement(destinationElem, MyX, MyY) // TODO: See the begining of this method for details on MyX and MyY
                   .Release(destinationElem)
                   .Build();
                dragAndDrop.Perform();
            }
        }

        #endregion General

        #region Date Picker

        /// <summary>
        /// Expands the date picker, clicks the upper middle button until the Year frame appears. If the year is not shown 
        /// on the current frame/button (See RCP Create Upcoming Agenda form), it will click the forward or back button m
        /// so the year shows, then it chooses the year, then month, then day of month, then closes the control
        /// </summary>
        /// <param name="dateElemTxt">The text box element of the date control. Most likely, the Xpath will be something like the following: //input[@type='text' and @name='Date']</param>
        /// <param name="yr">The 2 digit year. ie. "17"</param>
        /// <param name="monthName">The full month name. ie. "January"</param>
        /// <param name="dayOfMonth">The day of the month. ie. "24"</param>
        /// <returns></returns>
        public static string DatePicker_ChooseDate(IWebElement dateElemTxt, string yr, string monthName, string dayOfMonth)
        {
            IWebElement expandBtn = dateElemTxt.FindElement(By.XPath("..//span[@class='input-group-btn']//button[@class='btn btn-default']//span"));
            //input[@id='txtAccStartDate']/../div/button
            //..//span[@class='input-group-btn']//button[@class='btn btn-default']//span
            expandBtn.Click();
            Thread.Sleep(0300);

            IWebElement topMiddleBtn = dateElemTxt.FindElement(By.XPath(".././/strong/.."));
            topMiddleBtn.Click();

            IWebElement topMiddleBtn2 = dateElemTxt.FindElement(By.XPath(".././/strong/.."));
            topMiddleBtn2.Click();

            //IWebElement yearBtn = dateElemTxt.FindElement(By.XPath(string.Format(".././/span[text()='{0}' and contains(@class ,'binding')]", yr)));
            //yearBtn.Click();

            // If the year is not shown on the current frame/button (See RCP Create Upcoming Agenda form), it will click 
            // the forward or back button so the year shows, then it chooses the year
            for (int i = 0; i < 10; i++)
            {
                IWebElement startYearBtn = dateElemTxt.FindElement(By.XPath(string.Format("//table[@data-ng-switch-when='year']//tbody//tr[1]//td[1]", yr)));
                IWebElement endYearBtn = dateElemTxt.FindElement(By.XPath(string.Format("//table[@data-ng-switch-when='year']//tbody//tr[4]//td[5]", yr)));

                int year = Int32.Parse(yr);
                int startYear = Int32.Parse(startYearBtn.Text.Trim());
                int endYear = Int32.Parse(endYearBtn.Text.Trim());

                if (year >= startYear && year <= endYear)
                {
                    IWebElement yearBtn = dateElemTxt.FindElement(By.XPath(string.Format(".././/span[text()='{0}' and contains(@class ,'binding')]", yr)));
                    yearBtn.Click();
                    break;
                }
                else if (year < startYear)
                {
                    IWebElement backYearBtn = dateElemTxt.FindElement(By.XPath("//table[@data-ng-switch-when='year']//thead//tr[1]//td[1]//button"));
                    backYearBtn.Click();

                }
                else if (year > endYear)
                {
                    IWebElement nextYearBtn = dateElemTxt.FindElement(By.XPath("//table[@data-ng-switch-when='year']//thead//tr[1]//td[3]//button"));
                    nextYearBtn.Click();
                }
            }

            IWebElement monthBtn = dateElemTxt.FindElement(By.XPath(string.Format(".././/span[text()='{0}' and contains(@class ,'binding')]", monthName)));
            monthBtn.Click();

            IWebElement dayOfMonthBtn = dateElemTxt.FindElement(By.XPath(string.Format(".././/span[text()='{0}' and contains(@class ,'binding')]", dayOfMonth)));
            dayOfMonthBtn.Click();

            return dateElemTxt.GetAttribute("value");
        }

        public static string NorDatePicker_ChooseDate(IWebElement dateElemTxt, string yr, string monthName, string dayOfMonth)
        {

            IWebElement expandBtn = dateElemTxt.FindElement(By.XPath("../div/button "));
            expandBtn.Click();
            Thread.Sleep(0300);

            IWebElement monthBtn = dateElemTxt.FindElement(By.XPath(string.Format("../descendant::select/.//option[@aria-label='{0}']", monthName)));
            monthBtn.Click();

            IWebElement yearBtn = dateElemTxt.FindElement(By.XPath(string.Format("../descendant::select[2]/.//option[text()='{0}']", yr)));
            yearBtn.Click();

            IWebElement dayOfMonthBtn = dateElemTxt.FindElement(By.XPath(string.Format("../descendant::ngb-datepicker-month-view/.//div[text()='{0}']", dayOfMonth.Replace('0', ' ').Trim())));
            dayOfMonthBtn.Click();

            return dateElemTxt.GetAttribute("value");
        }

        #endregion Date Picker

    }
}

