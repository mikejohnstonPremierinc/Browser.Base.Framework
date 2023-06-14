using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace Browser.Core.Framework
{
    /// <summary>
    /// A utility class retreiving data and attributes from elements
    /// </summary>
    public static class ElemGet
    {
        #region Checkbox

        public static IList<IWebElement> ChkBx_GetListOfChkBxsWithinForm(IWebElement chkBx)
        {
            // Get the first ancestor of the radio button with the tag name of div that has an attribute value of form-group. 
            // Check boxes in CBD are within these div tags. For other applications, we can add conditions
            //string xpath = "ancestor::tbody[1]";
            string xpath = "ancestor::div[@class='form-group']";
            IList<IWebElement> allChkBxs = null;

            try
            {
                IWebElement parentTableOfRdoBtns = chkBx.FindElement(By.XPath(xpath));
                allChkBxs = parentTableOfRdoBtns.FindElements(By.TagName("input"));

                //// Some radio buttons in RCP are located within spans, some are labels, so we need to condition this
                //if (allChkBxs.Count == 0)
                //{
                //    allChkBxs = parentTableOfRdoBtns.FindElements(By.TagName("label"));
                //}

                return allChkBxs;
            }
            catch
            {

            }
            return null;
        }

        /// <summary>
        /// Gets the check box element based on it's label text. 
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="textOfChkBx">The exact text of one of the radio buttons inside</param>
        /// <param name="indexOfCheckBoxWithSameText">If there are multiple check boxes on the same page with the same text, you can specify
        /// at which index you want to click</param>
        public static IWebElement ChkBx_GetCheckBox(IWebDriver browser, string textOfChkBx, int indexOfCheckBoxWithSameText = 0)
        {
            IWebElement Chk = null;

            // UAMS 
            string xpathStringUAMS = string.Format("descendant::span[contains(., '{0}') and @class='checkbox-input-label-text']//preceding-sibling::input", textOfChkBx);

            // RCP 
            // Right now in RCP, I need 2 different xpaths for radio buttons, as their tags are different
            // between learners and observers. Nirav is going to fix this. Once he does, I can implement the simpler solution
            string xpathRCPVersion1 = string.Format("//label/span[text()='{0}']/..", textOfChkBx);
            string xpathRCPVersion2 = string.Format("//label[text()='{0}']", textOfChkBx);

            // ABAM radio buttons
            string xpathABAM = string.Format("//input[@type='radio' and @value='{0}']", textOfChkBx);

            // Test Portal radio buttons
            string xpathTestPortal = string.Format("//span/label[text()='{0}']/preceding-sibling::input", textOfChkBx);

            string xpathStringRandom = string.Format("//div[contains(., '{0}')]/input", textOfChkBx);

            if (browser.FindElements(By.XPath(xpathStringUAMS)).Count > 0)
            {
                Chk = browser.FindElement(By.XPath(xpathStringUAMS));
                return Chk;
            }

            if (browser.FindElements(By.XPath(xpathRCPVersion1)).Count > 0)
            {
                Chk = browser.FindElement(By.XPath(xpathRCPVersion1));
                return Chk;
            }

            else if (browser.FindElements(By.XPath(xpathRCPVersion2)).Count > 0)
            {
                xpathRCPVersion2 = string.Format("//label[text()='{0}']", textOfChkBx);
                Chk = browser.FindElement(By.XPath(xpathRCPVersion2));
                return Chk;
            }

            else if (browser.FindElements(By.XPath(xpathABAM)).Count > 0)
            {
                Chk = browser.FindElement(By.XPath(xpathABAM));
                return Chk;
            }

            else if (browser.FindElements(By.XPath(xpathTestPortal)).Count > 0)
            {
                Chk = browser.FindElement(By.XPath(xpathTestPortal));
                return Chk;
            }

            else if (browser.FindElements(By.XPath(xpathStringRandom)).Count > 0)
            {
                Chk = browser.FindElement(By.XPath(xpathStringRandom));
                return Chk;
            }

            else
            {
                throw new Exception("The code could not find your radio button. It either failed to appear on the page you are testing (defect), or " +
                    "your radio button's xpath has not been coded in this method. Add a condition for your new radio button in the above method or create a defect");
            }
        }

        /// <summary>
        /// Gets the check box element based on it's label text within the parent element
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="textOfChkBx">The exact text of one of the radio buttons inside</param>
        /// <param name="ParentElem">The parent element to find your check box in</param>
        /// <param name="indexOfCheckBoxWithSameText">If there are multiple check boxes on the same page with the same text, you can specify
        /// at which index you want to click</param>
        public static IWebElement ChkBx_GetCheckBox(IWebDriver browser, string textOfChkBx, IWebElement ParentElem, int indexOfCheckBoxWithSameText = 0)
        {
            IWebElement Chk = null;

            // UAMS 
            string xpathStringUAMS = string.Format("descendant::span[contains(., '{0}') and @class='checkbox-input-label-text']//preceding-sibling::input", textOfChkBx);

            // RCP 
            // Right now in RCP, I need 2 different xpaths for radio buttons, as their tags are different
            // between learners and observers. Nirav is going to fix this. Once he does, I can implement the simpler solution
            string xpathRCPVersion1 = string.Format("descendant::label/span[text()='{0}']/..", textOfChkBx);
            string xpathRCPVersion2 = string.Format("descendant::label[text()='{0}']", textOfChkBx);

            // ABAM radio buttons
            string xpathABAM = string.Format("descendant::input[@type='radio' and @value='{0}']", textOfChkBx);

            // Test Portal radio buttons
            string xpathTestPortal = string.Format("descendant::span/label[text()='{0}']/preceding-sibling::input", textOfChkBx);

            string xpathStringRandom = string.Format("//div[contains(., '{0}')]/input", textOfChkBx);

            if (ParentElem.FindElements(By.XPath(xpathStringUAMS)).Count > 0)
            {
                Chk = ParentElem.FindElement(By.XPath(xpathStringUAMS));
                return Chk;
            }

            if (ParentElem.FindElements(By.XPath(xpathRCPVersion1)).Count > 0)
            {
                Chk = ParentElem.FindElement(By.XPath(xpathRCPVersion1));
                return Chk;
            }

            else if (ParentElem.FindElements(By.XPath(xpathRCPVersion2)).Count > 0)
            {
                xpathRCPVersion2 = string.Format("descendant::label[text()='{0}']", textOfChkBx);
                Chk = ParentElem.FindElement(By.XPath(xpathRCPVersion2));
                return Chk;
            }

            else if (ParentElem.FindElements(By.XPath(xpathABAM)).Count > 0)
            {
                Chk = ParentElem.FindElement(By.XPath(xpathABAM));
                return Chk;
            }

            else if (ParentElem.FindElements(By.XPath(xpathTestPortal)).Count > 0)
            {
                Chk = ParentElem.FindElement(By.XPath(xpathTestPortal));
                return Chk;
            }

            else if (browser.FindElements(By.XPath(xpathStringRandom)).Count > 0)
            {
                Chk = browser.FindElement(By.XPath(xpathStringRandom));
                return Chk;
            }

            else
            {
                throw new Exception("The code could not find your radio button. It either failed to appear on the page you are testing (defect), or " +
                    "your radio button's xpath has not been coded in this method. Add a condition for your new radio button in the above method or create a defect");
            }
        }

        /// <summary>
        /// Gets a list of check boxes elements based on it's label text within a parent element
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="textOfChkBx">The exact text of one of the radio buttons inside</param>
        /// <param name="ParentElem">The parent element to find your check box in</param>
        /// at which index you want to click</param>
        public static IList<IWebElement> ChkBx_GetCheckBoxes(IWebDriver browser, string textOfChkBx, IWebElement ParentElem)
        {
            IList<IWebElement> Chk = null;

            // UAMS 
            string xpathStringUAMS = string.Format("descendant::span[contains(., '{0}') and @class='checkbox-input-label-text']//preceding-sibling::input", textOfChkBx);

            // RCP 
            // Right now in RCP, I need 2 different xpaths for radio buttons, as their tags are different
            // between learners and observers. Nirav is going to fix this. Once he does, I can implement the simpler solution
            string xpathRCPVersion1 = string.Format("descendant::label/span[text()='{0}']/..", textOfChkBx);
            string xpathRCPVersion2 = string.Format("descendant::label[text()='{0}']", textOfChkBx);

            // ABAM radio buttons
            string xpathABAM = string.Format("descendant::input[@type='radio' and @value='{0}']", textOfChkBx);

            // Test Portal radio buttons
            string xpathTestPortal = string.Format("descendant::span/label[text()='{0}']/preceding-sibling::input", textOfChkBx);

            string xpathStringRandom = string.Format("descendant::div[contains(., '{0}')]/input", textOfChkBx);

            if (ParentElem.FindElements(By.XPath(xpathStringUAMS)).Count > 0)
            {
                Chk = ParentElem.FindElements(By.XPath(xpathStringUAMS));
                return Chk;
            }

            if (ParentElem.FindElements(By.XPath(xpathRCPVersion1)).Count > 0)
            {
                Chk = ParentElem.FindElements(By.XPath(xpathRCPVersion1));
                return Chk;
            }

            else if (ParentElem.FindElements(By.XPath(xpathRCPVersion2)).Count > 0)
            {
                xpathRCPVersion2 = string.Format("descendant::label[text()='{0}']", textOfChkBx);
                Chk = ParentElem.FindElements(By.XPath(xpathRCPVersion2));
                return Chk;
            }

            else if (ParentElem.FindElements(By.XPath(xpathABAM)).Count > 0)
            {
                Chk = ParentElem.FindElements(By.XPath(xpathABAM));
                return Chk;
            }

            else if (ParentElem.FindElements(By.XPath(xpathTestPortal)).Count > 0)
            {
                Chk = ParentElem.FindElements(By.XPath(xpathTestPortal));
                return Chk;
            }

            else if (browser.FindElements(By.XPath(xpathStringRandom)).Count > 0)
            {
                Chk = browser.FindElements(By.XPath(xpathStringRandom));
                return Chk;
            }

            else
            {
                throw new Exception("The code could not find your radio button. It either failed to appear on the page you are testing (defect), or " +
                    "your radio button's xpath has not been coded in this method. Add a condition for your new radio button in the above method or create a defect");
            }
        }

        /// <summary>
        /// Determines whether a list item within a dropdown has been checked or not
        /// </summary>
        /// <param name="elem">The dropdown element</param>
        /// <param name="itemName">The text attribute value of the list item element</param>
        /// <returns>True if checkbox is checked, false if not</returns>
        public static bool ChkBx_IsDisplayedOnListItem(IWebElement elem, string itemName)
        {
            bool result = false;

            // Find the parent element of the drop down. So we can then find a child DIV element
            var ParentElement = elem.FindElement(By.XPath(".."));
            // This DIV element contains the list items, whether filtered or not. If values are filtered, there is code in the line after
            // this to handle that. i.e. class<>hidden
            var DivElem = ParentElement.FindElement(By.CssSelector("ul[role=listbox]"));
            // Store all li elements within the Div element
            var lists = DivElem.FindElements(By.CssSelector("li:not([class=hidden])"));
            foreach (var list in lists)
            {
                // if the current list item's text value in the for loop equals the users passed parameter itemName
                if (list.Text == itemName)
                {
                    // Find the check mark element. find span class=glyphicon
                    var checkMarkElem = list.FindElement(By.ClassName("glyphicon"));
                    if (checkMarkElem.Displayed)
                    {
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }

        #endregion Checkbox

        #region dropdown custom

        /// <summary>
        /// Returns a Datatable representing multi-select select element. The control must be expanded for this method to work, as the Text property of the
        /// LI items do not get populated until the drop down is expanded
        /// </summary>
        /// <param name="elem">The element to grab the list of items from. It must be of type IWebElement, not SelectElement</param>
        public static DataTable DropdownCustom_MultiSelect_LITextToDataTable(IWebElement elem)
        {
            // Find the parent element of the drop down. So we can then find a child DIV element
            var ParentElement = elem.FindElement(By.XPath(".."));
            // This DIV element contains the list items, whether filtered or not. If values are filtered, there is code in the line after
            // this to handle that. i.e. class<>hidden
            var DivElem = ParentElement.FindElement(By.CssSelector("ul[role=listbox]"));
            // Store all li elements within the Div element that do not have a class that is either equal to "hidden" or "selected hidden"
            var lists = DivElem.FindElements(By.CssSelector("li:not([class=hidden]):not([class='selected hidden'])"));
            return lists.Select(p => p.Text.Trim()).ToDataTable<string>("Text");
        }

        /// <summary>
        /// Returns the text of an LI element from a dropdown control
        /// </summary>
        /// <param name="elem">The element from which you are selecting from. It must be of type IWebElement, not SelectElement</param>
        /// <param name="index">The index (zero based) of the LI (list item) that you want to grab the text from</param>
        public static string DropdownCustom_ListItemTextByIndex(IWebElement elem, int index)
        {
            // Find the parent element of the drop down. So we can then find a child DIV element
            var ParentElement = elem.FindElement(By.XPath(".."));

            // This DIV element contains the list items, whether filtered or not. If values are filtered, there is code in the line after
            // this to handle that. i.e. class<>hidden
            var DivElem = ParentElement.FindElement(By.CssSelector("ul[role=listbox]"));

            // Store all li elements within the Div element that do not have a class that is either equal to "hidden" or "selected hidden"
            var lists = DivElem.FindElements(By.CssSelector("li:not([class=hidden]):not([class='selected hidden'])"));

            return lists[index].Text;
        }

        /// <summary>
        /// Returns a List<string> of the contents of a multi-select IWebElement. The control must be expanded for this method to
        /// work, as the Text property of the LI items do not get populated until the drop down is expanded
        /// </summary>
        /// <param name="elem">The element to grab the list of items from. It must be of type IWebElement, not SelectElement</param>
        public static List<string> DropdownCustom_MultiSelect_LITextToListString(IWebElement elem)
        {
            // Find the parent element of the drop down. So we can then find a child DIV element
            var ParentElement = elem.FindElement(By.XPath(".."));

            // This DIV element contains the list items, whether filtered or not. If values are filtered, there is code in the line after
            // this to handle that. i.e. class<>hidden
            var DivElem = ParentElement.FindElement(By.CssSelector("ul[role=listbox]"));

            // Store all li elements within the Div element that do not have a class that is either equal to "hidden" or "selected hidden"
            var lists = DivElem.FindElements(By.CssSelector("li:not([class='hidden']):not([class='selected hidden'])"));

            return lists.Select(p => p.Text).ToList();
        }


        #endregion dropdown custom

        #region Select Elements

        /// <summary>
        /// Returns Datatable representing single-select SelectElement
        /// </summary>
        /// <param name="elem">The element to grab the list of items from. It must be of type SelectElement</param>
        public static DataTable SelElem_ListTextToDataTable(SelectElement elem)
        {
            return elem.Options.Select(p => p.Text.Trim()).ToDataTable<string>("Text");
        }

        /// <summary>
        /// Returns a Datatable from your Select Element. Note that this method trims consecutive spaces from the list of strings. So if you are comparing this
        /// list to anything else, make sure you trim the consecutive spaces off of that comparison object as well
        /// </summary>
        /// <param name="elem">The element to grab the list from. Must be an element with a Select tag in the HTML</param>
        public static List<string> SelElem_ListTextToListString(SelectElement elem)
        {
            return elem.Options.Select(p => p.GetAttribute("textContent")).ToList();
        }

        /// <summary>
        /// If any of the items in the select element contain the user specified text, return true, else return false
        /// </summary>
        /// <param name="elem">The select element</param>
        /// <param name="textToSearchFor">The text you want to verify exists in the select element</param>
        /// <returns></returns>
        public static bool SelElem_ContainsText(SelectElement elem, string textToSearchFor)
        {
            List<string> itemTextForAllItems = ElemGet.SelElem_ListTextToListString(elem);

            // If any of the items of the list<string> contain the text , return true
            foreach (string itemText in itemTextForAllItems)
            {
                if (itemText.Contains(textToSearchFor))

                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Gets the count of items in the select element containing the user specified text
        /// </summary>
        /// <param name="elem">The select element</param>
        /// <param name="textToSearchFor">The text you want to check for</param>
        /// <returns></returns>
        public static int SelElem_GetCountOfItemsContainingText(SelectElement elem, string textToSearchFor)
        {
            List<string> itemTextForAllItems = ElemGet.SelElem_ListTextToListString(elem);
            int countOfItems = 0;

            // If any of the items of the list<string> contain the text, add it to the list =
            foreach (string itemText in itemTextForAllItems)
            {
                if (itemText.Contains(textToSearchFor))
                {
                    countOfItems++;
                }

            }

            return countOfItems;
        }

        /// <summary>
        /// Gets the first element in a select element containing the user specified text
        /// </summary>
        /// <param name="elem">The select element</param>
        /// <param name="textToSearchFor">The text you want to check for</param>
        /// <returns></returns>
        public static IWebElement SelElem_GetFirstItemContainingText(SelectElement elem, string textToSearchFor)
        {
            List<string> itemTextForAllItems = ElemGet.SelElem_ListTextToListString(elem);
            IWebElement elemContainingText = null;

            // If any of the items of the list<string> contain the text, add it to the list =
            foreach (string itemText in itemTextForAllItems)
            {
                if (itemText.Contains(textToSearchFor))
                {
                    return elemContainingText;
                }

            }

            return elemContainingText;
        }

        /// <summary>
        /// Returns a Datatable from your Select Element, with a user-specified parameter to trim any text in the string
        /// </summary>
        /// <param name="elem">The element to grab the list from. Must be an element with a Select tag in the HTML</param>
        /// <param name="textToRemove">The text you want to remove from the string</param>
        public static DataTable SelElem_ListTextTrimmedToDataTable(SelectElement elem, string textToRemove)
        {
            return elem.Options.Select(p => p.Text.Replace(textToRemove, string.Empty)).ToDataTable<string>("Text");
        }

        /// <summary>
        /// Returns a list of strings from your Select Element. Until this code is refactored to become faster,  Only use this
        /// on a small list, preferably under 30 items. Otherwise, it will take a long time to complete. An alternative is to use
        /// the SelectElementListItemTextToDataTable method
        /// </summary>
        /// <param name="elem">The element to grab the list from. Must be anelement with a Select tag in the HTML</param>
        public static List<string> SelElem_ListTextToArray(SelectElement elem)
        {
            // IList<string> orgTypesActual = new List<string>();
            // orgTypesActual = OrgPage.GetDropDownItemsViaIList(OrgPage.CreateUpdateOrgFormOrgTypeSelectItem1);
            List<string> DropDownItems = new List<string>();
            for (var i = 0; i < elem.Options.Count; i++)
            {
                DropDownItems.Add(elem.Options[i].Text);
            }
            return DropDownItems;
        }

        #endregion Select Elements

        #region Grids

        /// <summary>
        /// Returns the cell text from a user-specified row name and column name
        /// </summary>
        /// <param name="Browser">The driver instance</param>
        /// <param name="tblElem">You table element that is found within the your Page class. i.e. Page.MyTableNameTbl</param>
        /// <param name="theadElem">You table header (thead) element that is found within the your Page class. 
        /// i.e. Page.MyTableNameTblHdr</param>
        /// <param name="rowBy">Send any row of your table in it's By type. i.e. Bys.MyPage.MyTableNameTblBodyRow</param>
        /// <param name="rowName">The name of the row. i.e. The exact text from the column which 
        /// contains the unique identifiers for this table. Every table has a column which contains unique strings. This parameter
        /// represents that string. IMPORTANT: Be sure to send the EXACT text from the text() attribute value within the 
        /// DevTools->Elements tab. In some cases, the cell text can have leading and 
        /// trailing whitespace. Include those whitespaces when you send this string to this parameter</param>
        /// <param name="htmlTagNameForRowName">The HTML tag for rowName. For example, inside DevTools, inspect the element 
        /// that you passed for the rowName parameter. The tagname for that element is what you pass here</param>
        /// <param name="colName">The name/text of the column header for the column you want to extract the text from</param>
        /// <param name="xpathForColumnHeaders">(Optional). The xpath for your table's column headers. For example, inside DevTools, 
        /// inspect the element that you passed for the colName parameter. The xpath for that element is what you pass here. If you 
        /// specific table's column headers are contained within multiple tags/xpaths, then you will pass all instances of those 
        /// xpaths separated by the pipe | symbol, as the pipe symbol represents an Or condition within xpath dialect. For example
        /// send '//span | //th' to this parameter if your specific application table headers are contained within both span and th tags.
        /// Default = '//th'</param>
        /// <param name="xpathForCellText">(Optional). The xpath that contains your requested cell text. Default = '//td'</param>
        /// <param name="pagesToScroll">(Optional) If your table has a virtual scroll bar, then the rows in your table that are not visible 
        /// on screen will not be populated into the HTML until the user scrolls those rows into view. If this is your table, then 
        /// this method contains additional logic to scroll one page down until the record is found/populated into the HTML, or scroll one page 
        /// down until the amount of times you pass to this parameter. If you dont pass this parameter, then the default number of times we 
        /// scroll will be 20. If we scrolled to the end of the scroll area and the record is not found, then the method will throw 
        /// an exception</param>
        /// <returns></returns>
        public static string Grid_GetCellTextByRowNameAndColName(IWebDriver Browser, IWebElement tblElem, IWebElement theadElem, By rowBy,
            string rowName, string htmlTagNameForRowName, string colName, string xpathForColumnHeaders = "//th",
            string xpathForCellText = "//td", int pagesToScroll = 20)
        {
            // Get the row that the user wants to grab the text from
            IWebElement row = Grid_GetRowByRowName(Browser, tblElem, rowBy, rowName, htmlTagNameForRowName, pagesToScroll);

            int colIndex = ReturnTrueColIndex(Browser, row, theadElem, xpathForColumnHeaders, colName);

            if (colIndex == -1)
            {
                throw new Exception("This table has static text under the first column for each row. I have not coded " +
                    "to get the cell text for this condition yet, and most likely will not because static text of a " +
                    "first column rarely ever needs to be verified. ");
            }

            // Within the row, find the cell text from the index of the column name that we found above. Have to add a dot before the xpath,
            // else Selenium looks inside entire HTML as opposed to only within the child elements of tblElemOrTblBodyElem
            string cellText = row.FindElements(By.XPath("." + xpathForCellText))[colIndex].Text;

            return cellText;
        }

        /// <summary>
        /// Returns the cell text from a user-specified row index and column name.
        /// </summary>
        /// <param name="Browser">The driver instance</param>
        /// <param name="tblBodyElem">You table body element that is found within the your Page class. i.e. Page.MyTableNameTblBody</param>
        /// <param name="theadElem">You table header (thead) element that is found within the your Page class. 
        /// i.e. Page.MyTableNameTblHdr</param>
        /// <param name="rowIndex">The zero-based index of the row you want to extract the text from</param>   
        /// <param name="colName">The name/text of the column header for the column you want to extract the text from</param>
        /// <param name="xpathForColumnHeaders">(Optional). The xpath for your table's column headers. For example, inside DevTools, 
        /// inspect the element that you passed for the colName parameter. The xpath for that element is what you pass here. If you 
        /// specific table's column headers are contained within multiple tags/xpaths, then you will pass all instances of those 
        /// xpaths separated by the pipe | symbol, as the pipe symbol represents an Or condition within xpath dialect. For example
        /// send '//span | //th' to this parameter if your specific application table headers are contained within both span and th tags.
        /// Default = '//th'</param>
        /// <param name="xpathForRows">(Optional). The xpath for your table's rows. Default = '//tr'</param>
        /// <param name="xpathForCellText">(Optional). The xpath that contains your requested cell text. Default = '//td'</param>
        public static string Grid_GetCellTextByRowIndexAndColName(IWebDriver Browser, IWebElement tblBodyElem, IWebElement theadElem,
            int rowIndex, string colName,
            string xpathForColumnHeaders = "//th",
            string xpathForRows = "//tr",
            string xpathForCellText = "//td")
        {
            // Get the specific row that the tester wants. Have to add a dot before the xpath, else Selenium looks inside entire
            // HTML as opposed to only within the child elements of tblElemOrTblBodyElem
            IWebElement row = tblBodyElem.FindElements(By.XPath("." + xpathForRows))[rowIndex];

            int colIndex = ReturnTrueColIndex(Browser, row, theadElem, xpathForColumnHeaders, colName);

            if (colIndex == -1)
            {
                throw new Exception("This table has static text under the first column for each row. I have not coded " +
                    "to get the cell text for this condition yet, and most likely will not because static text of a " +
                    "first column rarely ever needs to be verified. ");
            }

            // Within the row, find the cell text from the index of the column name that we found above. Have to add a dot before the xpath,
            // else Selenium looks inside entire HTML as opposed to only within the child elements of tblElemOrTblBodyElem
            string cellText = row.FindElements(By.XPath("." + xpathForCellText))[colIndex].Text;

            return cellText;
        }


        /// <summary>
        /// Get the column index based on the column name that the tester passed. 
        /// </summary>
        /// <param name="Browser">The driver instance</param>
        /// <param name="theadElem">You table header (thead) element that is found within the your Page class. 
        /// i.e. Page.MyTableNameTblHdr</param>
        /// <param name="xpathForColumnHeaders">The xpath for your table's column headers</param>
        /// <param name="colName">The name/text of the column header for the column you want to extract the text from</param>
        /// <returns></returns>
        private static int ReturnTrueColIndex(IWebDriver Browser, IWebElement row, IWebElement theadElem, string xpathForColumnHeaders,
            string colName)
        {
            // Get all column headers
            // Have to add a dot before the xpath, else Selenium looks inside entire HTML as opposed to only within the child elements of theadElem
            IList<IWebElement> columnHeaders = theadElem.FindElements(By.XPath("." + xpathForColumnHeaders));
            // Similarly, if the tester passed the Pipe symbol in the xpath, which would indicate there are multiple xpaths for his/her 
            // application's table headers, then we have to add a Dot after the pipe symbol also, which will place a Dot
            // before the second xpath instance


            int? colIndex = null;

            // Loop through column headers until the user-specified colName is reached. Once reached, break the loop and
            // store the index into colNum
            for (int i = 0; i < columnHeaders.Count; i++)
            {
                if (columnHeaders[i].Text == colName)
                {
                    colIndex = i;
                    break;
                }
            }

            // For tables that have static rows (i.e.Tables that you cant add or delete data from, resulting
            // in the amount of rows always being the same, the first column's cell text of each row always being
            // the same etc.). For these types of tables, the first column cell elements will have a tag name of TH,
            // and not TD, because each row will be considered a header. This will affect the colIndex. colIndex
            // should subtract 1 if this is the case because in this case, the second column cell text for each row 
            // is considered the first data cell within the row, since the first column cell text is considered the 
            // header
            if (row.FindElement(By.XPath("*[1]")).TagName == "th")
            {
                colIndex = colIndex - 1;
            }

            if (colIndex == null)
            {
                throw new Exception("Could not find the column header from the colName argument that you passed. " +
                    "Make sure you spelled it correctly or check the source code");
            }

            int nonNullableInt = colIndex == null ? default(int) : colIndex.Value;
            return nonNullableInt;
        }


        /// <summary>
        /// Get the column index based on index that the tester passed. 
        /// </summary>
        /// <param name="Browser">The driver instance</param>
        /// <param name="row"></param>
        /// <param name="colIndex"></param>
        /// <returns></returns>
        private static int ReturnTrueColIndex(IWebDriver Browser, IWebElement row, int colIndex)
        {
            /// For tables that have static rows (i.e. Tables that you cant add or delete data from, resulting
            /// in the amount of rows always being the same, the first column's cell text of each row always being
            /// the same etc.). For these types of tables, the first colum cell elements will have a tag name of TH,
            /// and not TD, because each row will be considered a header. This will affect the colIndex. colIndex
            /// should subtract 1 if this is the case because in this case, the second column cell text for each row 
            /// is considered the first data cell within the row, since the first column cell text is considered the 
            /// header
            if (row.FindElement(By.XPath("*[1]")).TagName == "th")
            {
                if (colIndex == 0)
                {
                    throw new Exception("This table has static text under the first column for each row. I have not coded " +
                        "to get the cell text for this condition yet, and most likely will not because static text of a " +
                        "first column rarely ever needs to be verified. ");
                }
                colIndex = colIndex - 1;
            }

            return colIndex;
        }

        /// <summary>
        /// Returns the cell text from a user-specified row name and column name. 
        /// </summary>
        /// <param name="Browser">The driver instance</param>
        /// <param name="tblElem">You table element that is found within the your Page class. i.e. Page.MyTableNameTbl</param>
        /// <param name="rowBy">Send any row of your table in it's By type. i.e. Bys.MyPage.MyTableNameTblBodyRow</param>
        /// <param name="rowName">The name of the row. i.e. The exact text from the column which 
        /// contains the unique identifiers for this table. Every table has a column which contains unique strings. This parameter
        /// represents that string. IMPORTANT: Be sure to send the EXACT text from the text() attribute value within the 
        /// DevTools->Elements tab. In some cases, the cell text can have leading and 
        /// trailing whitespace. Include those whitespaces when you send this string to this parameter</param>
        /// <param name="htmlTagNameForRowName">The HTML tag for rowName. For example, inside DevTools, inspect the element 
        /// that you passed for the rowName parameter. The tagname for that element is what you pass here</param>
        /// <param name="colIndex">The zero-based index of the column you want to extract the text form.</param>
        /// <param name="xpathForCellText">(Optional). The xpath that contains your requested cell text. Default = '//td'</param>
        /// <param name="pagesToScroll">(Optional) If your table has a virtual scroll bar, then the rows in your table that are not visible 
        /// on screen will not be populated into the HTML until the user scrolls those rows into view. If this is your table, then 
        /// this method contains additional logic to scroll one page down until the record is found/populated into the HTML, or scroll one page 
        /// down until the amount of times you pass to this parameter. If you dont pass this parameter, then the default number of times we 
        /// scroll will be 20. If we scrolled to the end of the scroll area and the record is not found, then the method will throw 
        /// an exception</param>
        public static string Grid_GetCellTextByRowNameAndColIndex(IWebDriver Browser, IWebElement tblElem, By rowBy, string rowName,
            string htmlTagNameForRowName, int colIndex, string xpathForCellText = "//td", int pagesToScroll = 20)
        {
            // Get the row that the user wants to grab the text from
            IWebElement row = Grid_GetRowByRowName(Browser, tblElem, rowBy, rowName, htmlTagNameForRowName, pagesToScroll);

            colIndex = ReturnTrueColIndex(Browser, row, colIndex);

            // Within the row, find the cell text from the index of the column name that we found above
            // Have to add a dot before the xpath, else Selenium looks inside entire HTML as opposed to only within the child elements
            // of the row
            string cellText = row.FindElements(By.XPath("." + xpathForCellText))[colIndex].Text;

            return cellText;
        }


        /// <summary>
        /// Returns the cell text from a user-specified row number and column number. 
        /// </summary>
        /// <param name="Browser">The driver instance</param>
        /// <param name="tblBodyElem">You table body element that is found within the your Page class. i.e. Page.MyTableNameTblBody</param>
        /// <param name="rowBy">Send any row of your table in it's By type. i.e. Bys.MyPage.MyTableNameTblBodyRow</param>
        /// <param name="rowIndex">The zero-based index of the row you want to extract the text from</param>      
        /// <param name="colIndex">The zero-based index of the column you want to extract the text from.</param>
        /// <param name="xpathForCellText">(Optional). The xpath that contains your requested cell text. Default = '//td'</param>
        /// <param name="xpathForRows">(Optional). The xpath for your table's rows. Default = '//tr'</param>
        /// <returns></returns>
        public static string Grid_GetCellTextByRowIndexAndColIndex(IWebDriver Browser, IWebElement tblBodyElem, By rowBy, int rowIndex,
            int colIndex, string xpathForCellText = "//td", string xpathForRows = "//tr")
        {
            // Wait for the table's Body element
            tblBodyElem.WaitForElement(rowBy, TimeSpan.FromSeconds(30), ElementCriteria.HasText, ElementCriteria.IsEnabled);

            // Wait for the table's Row element
            tblBodyElem.WaitForElement(By.XPath("." + xpathForRows));

            // Get the specific row that the tester wants
            IWebElement row = tblBodyElem.FindElements(By.XPath("." + xpathForRows))[rowIndex];

            colIndex = ReturnTrueColIndex(Browser, row, colIndex);

            // Find and return the column text
            // Have to add a dot before the xpath, else Selenium looks inside entire HTML as opposed to only within the child elements
            // of the row
            string colTxt = row.FindElements(By.XPath("." + xpathForCellText))[colIndex].Text;

            return colTxt;
        }

        /// <summary>
        /// Returns the row count of any grid
        /// </summary>
        /// <param name="Browser">The driver instance</param>
        /// <param name="tblBody">The table's tbody element</param>
        /// <param name="xpathForRows">(Optional). The xpath for your table's rows. Default = '//tr'</param>
        public static int Grid_GetRowCount(IWebDriver Browser, IWebElement tblBody, string xpathForRows = "//tr")
        {
            int rowCount = tblBody.FindElements(By.XPath("." + xpathForRows)).Count;
            return rowCount;
        }

        /// <summary>
        /// Gets the user-specified Select Element within a table by the ID of the Select Element
        /// </summary>
        /// <param name="Browser">The driver instance</param>
        /// <param name="tblElem">You table element that is found within the your Page class. i.e. Page.MyTableNameTbl</param>
        /// <param name="rowBy">Send any row of your table in it's By type. i.e. Bys.MyPage.MyTableNameTblBodyRow</param>
        /// <param name="idOfSelElem">The exact text of the ID of the Select Element, however, if your select element is dynamically numbered 
        /// per row, then only send the text before the number. For example, is the select tag has an ID of "Priority_0", 
        /// only send "Priority"</param>
        /// <param name="rowName">The name of the row. i.e. The exact text from the column which 
        /// contains the unique identifiers for this table. Every table has a column which contains unique strings. This parameter
        /// represents that string. IMPORTANT: Be sure to send the EXACT text from the text() attribute value within the 
        /// DevTools->Elements tab. In some cases, the cell text can have leading and 
        /// trailing whitespace. Include those whitespaces when you send this string to this parameter</param>
        /// <param name="htmlTagNameForRowName">The HTML tag for rowName. For example, inside DevTools, inspect the element 
        /// that you passed for the rowName parameter. The tagname for that element is what you pass here</param>
        /// <param name="additionalColCellText">(Optional) If your rowName does not have to be unique compared to other rows in 
        /// your table and/or you would want to specify an additional column value to find your row, you can do that here. Send the 
        /// exact text of any other column's cell text for your row.</param>
        /// <param name="htmlTagNameForAdditionalColCellText">(Optional) The HTML tag for additionalColumnCellText. Default = null</param> 
        public static SelectElement Grid_GetSelElemInsideRowByID(IWebDriver Browser, IWebElement tblElem, By rowBy, string rowName,
            string htmlTagNameForRowName, string idOfSelElem, string additionalColCellText = null,
            string htmlTagNameForAdditionalColCellText = null)
        {
            IWebElement row = null;

            // Get the row element
            if (additionalColCellText.IsNullOrEmpty())
            {
                row = ElemGet.Grid_GetRowByRowName(Browser, tblElem, rowBy, rowName, htmlTagNameForRowName);
            }
            else
            {
                row = ElemGet.Grid_GetRowByRowNameAndAdditionalCellName(Browser, tblElem, rowBy, rowName, additionalColCellText,
                    htmlTagNameForRowName, htmlTagNameForAdditionalColCellText);
            }

            SelectElement selElem = new SelectElement(row.FindElement(By.XPath(string.Format(".//select[contains(@id, '{0}')]", idOfSelElem))));

            return selElem;
        }

        /// <summary>
        /// Gets the user-specified element (button, link) within a table by the user-specified button/link text of your user-specified row 
        /// </summary>
        /// <param name="Browser">The driver instance</param>
        /// <param name="tblElem">You table element that is found within the your Page class. i.e. Page.MyTableNameTbl</param>
        /// <param name="rowBy">Send any row of your table in it's By type. i.e. Bys.MyPage.MyTableNameTblBodyRow</param>
        /// <param name="rowName">The name of the row. i.e. The exact text from the column which 
        /// contains the unique identifiers for this table. Every table has a column which contains unique strings. This parameter
        /// represents that string. IMPORTANT: Be sure to send the EXACT text from the text() attribute value within the 
        /// DevTools->Elements tab. In some cases, the cell text can have leading and 
        /// trailing whitespace. Include those whitespaces when you send this string to this parameter</param>
        /// <param name="htmlTagNameForRowName">The HTML tag for rowName. For example, inside DevTools, inspect the element 
        /// that you passed for the rowName parameter. The tagname for that element is what you pass here</param>
        /// <param name="btnText">The text of the button you want to click on</param>
        /// <param name="htmlTagNameForBtnTxt">The HTML tag for btnText</param>
        /// <param name="additionalColCellText">(Optional) If your rowName does not have to be unique compared to other rows in 
        /// your table and/or you would want to specify an additional column value to find your row, you can do that here. Send the 
        /// exact text of any other column's cell text for your row.</param>
        /// <param name="htmlTagNameForAdditionalColCellText">(Optional) The HTML tag for additionalColumnCellText. Default = null</param> 
        /// <param name="insideiFrame">(Optional) Send 'true' if the button or link is inside an iFrame</param>
        public static IWebElement Grid_GetButtonOrLinkInsideRowByText(IWebDriver Browser, IWebElement tblElem, By rowBy, string rowName,
            string htmlTagNameForRowName, string btnText, string htmlTagNameForBtnTxt, string additionalColCellText = null,
            string htmlTagNameForAdditionalColCellText = null, bool insideiFrame = false)
        {
            IWebElement row = null;
            IWebElement btnLink = null;

            // Get the row element
            if (additionalColCellText.IsNullOrEmpty())
            {
                row = ElemGet.Grid_GetRowByRowName(Browser, tblElem, rowBy, rowName, htmlTagNameForRowName);
            }
            else
            {
                row = ElemGet.Grid_GetRowByRowNameAndAdditionalCellName(Browser, tblElem, rowBy, rowName,
                    htmlTagNameForRowName, additionalColCellText, htmlTagNameForAdditionalColCellText);
            }

            // Scroll to the row
            ElemSet.ScrollToElement(Browser, row, insideiFrame);

            // Sometimes in Fireball applications, whenever we scroll to an element (above line of code), the applications 
            // reloads. I have no idea why this happens. But it does, and to make this work, we have to wait until
            // the JQuery is finished loading. Note that some applications dont use JQuery, so we have to check if the 
            // application has JQuery first,  if so, then wait for it, otherwise if the application does not have it and 
            // we call the WaitJSandJQuery method, it will fail
            if (AppUtils.ApplicationHasJQuery(Browser, TimeSpan.FromSeconds(10)))
            {
                Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));
            }

            // Get the button element with the user-specified text and click on it
            string textOfButtonWithoutSpaces = btnText.Replace(" ", "");

            // Sometimes the button includes leading and trailing whitespace. Sometimes additional attributes are needed to find the
            // button. We will use IF statements below for these conditions. 
            string xpathStringForFirstTypeOfButton = string.Format(".//{0}[text()='{1}' and @data-i18n='_{2}_']", htmlTagNameForBtnTxt, btnText, textOfButtonWithoutSpaces);
            string xpathStringForSecondTypeOfButton = string.Format(".//{0}[text()='{1}' and @role='button']", htmlTagNameForBtnTxt, btnText);
            string xpathStringForThirdTypeOfButton = string.Format(".//{0}[text()='{1}']", htmlTagNameForBtnTxt, btnText);
            // For when elements have extra white space at the end of the text.
            string xpathStringForFourthTypeOfButton = string.Format(".//{0}[starts-with(text(),'{1}')]", htmlTagNameForBtnTxt, btnText);
            string xpathStringForFifthTypeOfButton = string.Format(".//{0}[contains(text(),'{1}')]", htmlTagNameForBtnTxt, btnText);

            if (row.FindElements(By.XPath(xpathStringForFirstTypeOfButton)).Count > 0)
            {
                btnLink = row.FindElement(By.XPath(xpathStringForFirstTypeOfButton));
            }
            else if (row.FindElements(By.XPath(xpathStringForSecondTypeOfButton)).Count > 0)
            {
                btnLink = row.FindElement(By.XPath(xpathStringForSecondTypeOfButton));
            }
            else if (row.FindElements(By.XPath(xpathStringForThirdTypeOfButton)).Count > 0)
            {
                btnLink = row.FindElement(By.XPath(xpathStringForThirdTypeOfButton));
            }
            else if (row.FindElements(By.XPath(xpathStringForFourthTypeOfButton)).Count > 0)
            {
                btnLink = row.FindElement(By.XPath(xpathStringForFourthTypeOfButton));
            }
            else if (row.FindElements(By.XPath(xpathStringForFifthTypeOfButton)).Count > 0)
            {
                btnLink = row.FindElement(By.XPath(xpathStringForFifthTypeOfButton));
            }

            else
            {
                throw new Exception(string.Format("The button/link with text of '{0}' could not be found inside the table you have specified with the first column " +
                    "celltext of '{1}' and additional column cell text of '{2}' that you specified", btnText, rowName, additionalColCellText));
            }

            return btnLink;
        }

        /// <summary>
        /// Gets the user-specified element within a table by specifying the row name as well as the tag names
        /// </summary>
        /// <param name="Browser">The driver instance</param>
        /// <param name="tblElem">You table element that is found within the your Page class. i.e. Page.MyTableNameTbl</param>
        /// <param name="rowBy">Send any row of your table in it's By type. i.e. Bys.MyPage.MyTableNameTblBodyRow</param>
        /// <param name="rowName">The name of the row. i.e. The exact text from the column which 
        /// contains the unique identifiers for this table. Every table has a column which contains unique strings. This parameter
        /// represents that string. IMPORTANT: Be sure to send the EXACT text from the text() attribute value within the 
        /// DevTools->Elements tab. In some cases, the cell text can have leading and 
        /// trailing whitespace. Include those whitespaces when you send this string to this parameter</param>
        /// <param name="htmlTagNameForRowName">The HTML tag for rowName. For example, inside DevTools, inspect the element 
        /// that you passed for the rowName parameter. The tagname for that element is what you pass here</param>
        /// <param name="htmlTagNameForButtonOrLink">The HTML tag name for the button/link you want to get</param>
        /// <param name="partialClassAttributeValueForButtonOrLink">The Class attribute value of the button element. This will be 
        /// used in a contains function, so a partial string of that Class attribute value can be used if needed</param>
        public static IWebElement Grid_GetButtonOrLinkInsideRowByPartialClassName(IWebDriver Browser, IWebElement tblElem,
            By rowBy, string rowName, string htmlTagNameForRowName,
            string htmlTagNameForButtonOrLink, string partialClassAttributeValueForButtonOrLink)
        {
            IWebElement row = null;
            IWebElement btnLink = null;

            row = ElemGet.Grid_GetRowByRowName(Browser, tblElem, rowBy, rowName, htmlTagNameForRowName);

            // Get the button element
            string xpathStringForFirstTypeOfButton = string.Format(".//{0}[contains(@class, '{1}')]",
                htmlTagNameForButtonOrLink, partialClassAttributeValueForButtonOrLink);

            if (row.FindElements(By.XPath(xpathStringForFirstTypeOfButton)).Count > 0)
            {
                btnLink = row.FindElement(By.XPath(xpathStringForFirstTypeOfButton));
            }

            else
            {
                throw new Exception(string.Format("The button/link could not be found inside the table you have specified with " +
                    "the first column celltext of '{1}'. You may have sent the wrong paramaters for the tag names " +
                    "or you may have a typo in the firstColumnCellText parameter", rowName));
            }

            return btnLink;
        }

        /// <summary>
        /// Returns a datatable from a UI grid. 
        /// Alternative to <see cref="BrowserExtensions.GetDataFromIgGrid(IWebDriver, IWebElement, DataColumnDefinition[])"/>
        /// </summary>
        /// <param name="Browser">The driver instance</param>
        /// <param name="gridColumnsClass">A C# class object must be created that contains the columns names of the grid of type string. 
        /// Once this class is created, then pass the text: "typeof(NameOfYourClass)"</param>
        /// <param name="tblBodyElem">You table body that is found within the your Page class. i.e. Page.MyTableNameTblBody</param>
        /// <param name="xpathForRows">The xpath for your rows</param>
        /// <param name="xpathForCellText">The xpath that contains all of the cell's cell text</param>
        public static DataTable Grid_ToDatatable(IWebDriver Browser, Type gridColumnsClass, IWebElement tblBodyElem,
            string xpathForRows, string xpathForCellText)
        {
            //Build DataTable with columns relative to passed class
            DataTable gridData = new DataTable();
            var fields = ((TypeInfo)gridColumnsClass).DeclaredFields;
            foreach (FieldInfo item in fields)
            {
                gridData.Columns.Add(item.Name);
            }

            //Get table rows 
            IList<IWebElement> allRows = tblBodyElem.FindElements(By.XPath("." + xpathForRows));

            foreach (IWebElement row in allRows)
            {
                //Find all cells in the row
                IList<IWebElement> cells = row.FindElements(By.XPath("." + xpathForCellText));
                List<string> cellContent = new List<string>();

                foreach (IWebElement cell in cells)
                {
                    cellContent.Add(cell.Text);
                }

                //Add row content to data table
                gridData.Rows.Add(cellContent.ToArray());
                cellContent.Clear();
            }

            return gridData;
        }

        /// <summary>
        /// Returns the row that contains the user-specified row name. NOTE: If your table does not force unique row names, 
        /// then this will just return the first instance of those rows). You can instead 
        /// call <see cref="Grid_GetRowByRowNameAndAdditionalCellName"/> if your table does not force unique row names
        /// </summary>
        /// <param name="Browser">The driver instance</param>
        /// <param name="tblElem">You table element that is found within the your Page class. i.e. Page.MyTableNameTbl</param>
        /// <param name="rowBy">Send any row of your table in it's By type. i.e. Bys.MyPage.MyTableNameTblBodyRow</param>
        /// <param name="rowName">The name of the row. i.e. The exact text from the column which 
        /// contains the unique identifiers for this table. Every table has a column which contains unique strings. This parameter
        /// represents that string. IMPORTANT: Be sure to send the EXACT text from the text() attribute value within the 
        /// DevTools->Elements tab. In some cases, the cell text can have leading and 
        /// trailing whitespace. Include those whitespaces when you send this string to this parameter</param>
        /// <param name="htmlTagNameForRowName">The HTML tag for rowName. For example, inside DevTools, inspect the element 
        /// that you passed for the rowName parameter. The tagname for that element is what you pass here</param>
        /// <param name="pagesToScroll">(Optional) If your table has a virtual scroll bar, then the rows in your table that are not visible 
        /// on screen will not be populated into the HTML until the user scrolls those rows into view. If this is your table, then 
        /// this method contains additional logic to scroll one page down until the record is found/populated into the HTML, or scroll one page 
        /// down until the amount of times you pass to this parameter. If you dont pass this parameter, then the default number of times we 
        /// scroll will be 20. If we scrolled to the end of the scroll area and the record is not found, then the method will throw 
        /// an exception</param>
        /// <returns></returns>
        public static IWebElement Grid_GetRowByRowName(IWebDriver Browser, IWebElement tblElem, By rowBy, string rowName,
            string htmlTagNameForRowName, int pagesToScroll = 20)
        {
            IWebElement firstColumnCell = Grid_GetCellsByCellText(Browser, tblElem, rowBy, rowName, htmlTagNameForRowName,
                pagesToScroll)[0];

            // Then get the 1st parent row element
            IWebElement row = null;

            // Mostly all tables add rows within TR tags. But some add them in DIV tags.
            if (firstColumnCell.FindElements(By.XPath("ancestor::tr[1]")).Count == 1)
            {
                row = firstColumnCell.FindElement(By.XPath("ancestor::tr[1]"));
            }
            // The below is found within the following tables: SCM UserFavories, HRP application's Organization Units
            else if (firstColumnCell.FindElements(By.XPath("ancestor::div[contains(@class, 'row')][1]")).Count == 1)
            {
                row = firstColumnCell.FindElement(By.XPath("ancestor::div[contains(@class, 'row')][1]"));
            }
            else
            {
                throw new Exception("Your table's row elements are not contained within a TR or DIV tag. Inspect your table's " +
                    "row element to determine the tag name they are contained within, then add an extra Else If statement above");
            }

            return row;
        }

        /// <summary>
        /// Returns the cells that contains the user-specified cell text
        /// </summary>
        /// <param name="Browser">The driver instance</param>
        /// <param name="tblElem">You table element that is found within the your Page class. i.e. Page.MyTableNameTbl</param>
        /// <param name="rowBy">Send any row of your table in it's By type. i.e. Bys.MyPage.MyTableNameTblBodyRow</param>
        /// <param name="cellText">The name of the cell. i.e. The exact text from cell you want to get returned</param>
        /// <param name="tagNameWhereCellTextTextExists">The HTML tag name where the cellText exists</param>
        /// <param name="pagesToScroll">(Optional) If your table has a virtual scroll bar, then the rows in your table that are not visible 
        /// on screen will not be populated into the HTML until the user scrolls those rows into view. If this is your table, then 
        /// this method contains additional logic to scroll one page down until the record is found/populated into the HTML, or scroll one page 
        /// down until the amount of times you pass to this parameter. If you dont pass this parameter, then the default number of times we 
        /// scroll will be 20. If we scrolled to the end of the scroll area and the record is not found, then the method will throw 
        /// an exception</param>
        /// <returns></returns>
        public static IList<IWebElement> Grid_GetCellsByCellText(IWebDriver Browser, IWebElement tblElem, By rowBy, string cellText,
            string tagNameWhereCellTextTextExists, int pagesToScroll = 20)
        {
            // First wait for the table
            tblElem.WaitForElement(rowBy, TimeSpan.FromSeconds(60), ElementCriteria.HasText, ElementCriteria.IsEnabled);
            Thread.Sleep(0400);

            // Build the xpaths of all different table types
            // RCP Learners table in the Program Dean page of CBD. This is a weird table and I had to come up with a special way to locate the cell. 
            string xpathStringSpecificRCPTable = string.Format("td/a/div[@class='row']/div[contains(., '{0}')]", cellText);
            // General table (No extra whitespace anywhere in the string)
            string xpathStringGeneralTable = string.Format(".//{0}[text()='{1}']", tagNameWhereCellTextTextExists, cellText);
            // General table (When cells have whitespaces between words, or leading or trailing white space)
            string xpathStringGeneralTable_RemoveWhiteSpace = string.Format(".//{0}[normalize-space(text())='{1}']", tagNameWhereCellTextTextExists, cellText);
            // General table (Starts with and normalize space (hoever I dont think I used normalize-space correctly in this xpath string, as you
            // can see in the above xpath the correct way to use it. This xpath is when there are unreceognizable characters at the end of the string.
            // This can be seen in MedConcert as a staff user when you schedule a survey)
            string xpathStringGeneralTable_StartsWith = string.Format("descendant::{0}[starts-with(normalize-space(),'{1}')]",
                tagNameWhereCellTextTextExists, cellText);
            // General table (Contains function. This is a last resort and bad idea to rely on a contains function for trying to locate a specific cell text.
            // Not sure if any application's table needs this, but I put it here at one point, so im leaving it here)
            string xpathStringGeneralTable_Contains = string.Format(".//{0}[contains(., '{1}')]", tagNameWhereCellTextTextExists, cellText);

            // If the table dynamically populates the HTML based on the user scrolling. For example, some tables have virtual scroll bars which
            // only populate the rows that are visible on screen into the HTML. A row that is not visible and not scrolled into view will not be
            // populated into the HTML. These kinds of tables are usually within ajax loading Angular applications. The table's
            // HTML tag will be cdk-virtual-scroll-viewport. If this is the case, then the loopCount will be equal to the pagesToScroll 
            // parameter, which will then cause the method to scroll down within the virtual scroll bar until the record is found or until 
            // the pagesToScroll loop number has reached the end.
            bool virtualScrollFound = false;
            if (tblElem.FindElements(By.XPath("descendant::cdk-virtual-scroll-viewport")).Count > 0)
            {
                virtualScrollFound = true;
            }

            bool cellTextFound = false;
            IList<IWebElement> cells = null;

            for (int i = 0; i < pagesToScroll; i++)
            {
                // NOTE: THE ORDER OF THE IF STATEMENTS IS CRUCIAL. DONT CHANGE THE ORDER. First RCP specific is needed. Then we need the text= before
                // the textContains  
                if (tblElem.FindElements(By.XPath(xpathStringSpecificRCPTable)).Count > 0)
                {
                    cells = tblElem.FindElements(By.XPath(xpathStringSpecificRCPTable));
                    cellTextFound = true;
                }

                else if (tblElem.FindElements(By.XPath(xpathStringGeneralTable)).Count > 0)
                {
                    cells = tblElem.FindElements(By.XPath(xpathStringGeneralTable));
                    cellTextFound = true;
                }

                else if (tblElem.FindElements(By.XPath(xpathStringGeneralTable_RemoveWhiteSpace)).Count > 0)
                {
                    cells = tblElem.FindElements(By.XPath(xpathStringGeneralTable_RemoveWhiteSpace));
                    cellTextFound = true;
                }

                else if (tblElem.FindElements(By.XPath(xpathStringGeneralTable_StartsWith)).Count > 0)
                {
                    cells = tblElem.FindElements(By.XPath(xpathStringGeneralTable_StartsWith));
                    cellTextFound = true;
                }

                else if (tblElem.FindElements(By.XPath(xpathStringGeneralTable_Contains)).Count > 0)
                {
                    cells = tblElem.FindElements(By.XPath(xpathStringGeneralTable_Contains));
                    cellTextFound = true;
                }

                if (cellTextFound == true)
                {
                    break;
                }

                if (!virtualScrollFound)
                {
                    break;
                }

                if (virtualScrollFound && !cellTextFound)
                {
                    ElemSet.ScrollPageDownWIthinGrid(Browser, tblElem);
                    // If we dont add a sleep here, then we get a Stale Element exception
                    Thread.Sleep(500);
                }


            }

            if (!cellTextFound)
            {
                throw new Exception(string.Format("The cell text of '{0}' could not be found in any of the columns inside the table you have specified. " +
                    "Either the cell text does not exist in your table, or you may have to add an extra IF statement above in this method for your " +
                    "specific table", cellText));
            }

            return cells;
        }

        /// <summary>
        /// Returns the row that contains the user-specified cell text from 2 cells. This is useful for tables that can contain 
        /// non-unique rows (First column cell text can be the same)
        /// </summary>
        /// <param name="Browser">The driver instance</param>
        /// <param name="tblElem">You table element that is found within the your Page class. i.e. Page.MyTableNameTbl</param>
        /// <param name="rowBy">Send any row of your table in it's By type. i.e. Bys.MyPage.MyTableNameTblBodyRow</param>
        /// <param name="rowName">The name of the row. i.e. The exact text from the column which 
        /// contains the unique identifiers for this table. Every table has a column which contains unique strings. This parameter
        /// represents that string. IMPORTANT: Be sure to send the EXACT text from the text() attribute value within the 
        /// DevTools->Elements tab. In some cases, the cell text can have leading and 
        /// trailing whitespace. Include those whitespaces when you send this string to this parameter</param>
        /// <param name="htmlTagNameForRowName">The HTML tag for rowName. For example, inside DevTools, inspect the element 
        /// that you passed for the rowName parameter. The tagname for that element is what you pass here</param>
        /// <param name="additionalColCellText">When your rowName does not have to be unique compared to other rows in 
        /// your table, you need to specify an additional column value to find your row, you can do that here. Send the 
        /// exact text of any other column's cell text for your row.</param>
        /// <param name="htmlTagNameForAdditionalColCellText">The HTML tag for additionalColumnCellText</param> 
        /// <returns></returns>
        public static IWebElement Grid_GetRowByRowNameAndAdditionalCellName(IWebDriver Browser, IWebElement tblElem, By rowBy,
            string rowName, string htmlTagNameForRowName,
            string additionalColCellText, string htmlTagNameForAdditionalColCellText)
        {
            IList<IWebElement> firstColumnCells = null;
            IList<IWebElement> additionalColumnCells = null;
            IWebElement row = null;
            string correctXpathForApplication = "";

            // First wait for the table
            tblElem.WaitForElement(rowBy, TimeSpan.FromSeconds(120), ElementCriteria.HasText, ElementCriteria.IsEnabled, ElementCriteria.IsVisible);
            Thread.Sleep(0400);

            // We will use IF statements to condition when cells have leading and trailing white space
            string xpathStringAllOtherTables = string.Format(".//{0}[text()='{1}']", htmlTagNameForRowName, rowName);
            string xpathStringAllOtherTablesContains = string.Format(".//{0}[contains(., '{1}')]", htmlTagNameForRowName, rowName);

            // For when cells have extra white spaces BETWEEN words. And also when there are unreceognizable characters at the end of the string.
            // This can be seen in MedConcert as a staff user when you schedule a survey
            string xpathStringAllOtherTablesStartsWithNormalize = string.Format("descendant::{0}[starts-with(normalize-space(),'{1}')]", htmlTagNameForRowName, rowName);

            // The first IF statement will use this xpath. This is for the Learners table in the Program Dean page of CBD. This is a weird
            // table and I had to come up with a special way to locate the cell. We are not using tagNameWhereFirstColCellTextExists, but still
            // need the {1} in the string.format, because the ForEach loop will set a variable that uses both tagNameWhereFirstColCellTextExists
            // firstColumnCellText
            // NOTE: THE ORDER OF THE IF STATEMENTS IS CRUCIAL. DONT CHANGE THE ORDER. First RCP specific is needed. Then we need the text= before
            // the textContains because 
            string xpathStringForSpecificRCPTable = string.Format("td/a/div[@class='row']/div[contains(., '{0}{1}')]", "", rowName);

            // Specific RCP table
            if (tblElem.FindElements(By.XPath(xpathStringForSpecificRCPTable)).Count > 0)
            {
                firstColumnCells = tblElem.FindElements(By.XPath(xpathStringForSpecificRCPTable));
                correctXpathForApplication = "td/a/div[@class='row']/div[contains(., '{0}')]";
            }

            // All other tables (no spaces)
            else if (tblElem.FindElements(By.XPath(xpathStringAllOtherTables)).Count > 0)
            {
                firstColumnCells = tblElem.FindElements(By.XPath(xpathStringAllOtherTables));
                correctXpathForApplication = ".//{0}[text()='{1}']";
            }

            // All other tables (leading and trailing spaces)
            else if (tblElem.FindElements(By.XPath(xpathStringAllOtherTablesContains)).Count > 0)
            {
                firstColumnCells = tblElem.FindElements(By.XPath(xpathStringAllOtherTablesContains));
                correctXpathForApplication = ".//{0}[contains(., '{1}')]";
            }

            // All other tables (starts with and normalize space)
            else if (tblElem.FindElements(By.XPath(xpathStringAllOtherTablesStartsWithNormalize)).Count > 0)
            {
                firstColumnCells = tblElem.FindElements(By.XPath(xpathStringAllOtherTablesStartsWithNormalize));
                correctXpathForApplication = "descendant::{0}[starts-with(normalize-space(),'{1}')]";
            }

            else
            {
                throw new Exception(string.Format("The cell text of '{0}' could not be found in any of the columns inside the table you have specified. " +
                    "Either the cell text does not exist in your table, or you may have to add an extra IF statement above in this method for your " +
                    "specific table", rowName));
            }

            // Loop through each cell
            foreach (IWebElement cell in firstColumnCells)
            {
                string xpathStringAdditionalCell = string.Format(correctXpathForApplication, htmlTagNameForAdditionalColCellText,
                    additionalColCellText);

                // Get the row for the current cell in the loop
                row = XpathUtils.GetParentOrChildElemWithSpecifiedCriteria(cell, "parent", "tr[1]");

                // If we find the cell with the additional cell text
                if (row.FindElements(By.XPath(xpathStringAdditionalCell)).Count > 0)
                {
                    additionalColumnCells = row.FindElements(By.XPath(xpathStringAdditionalCell));

                    // Return the row of this cell
                    return row;
                }
            }

            // NOTE: THE BELOW IS VERY SLOPPY CODE. I was in a rush and just whipped this up. If it causes issues with other projects in 
            // the future, revisit and make a better fix. For Lifetime Support, sometimes it finds the firstColumnCell by using text equals, 
            // so this method will then go to the ForEach loop and use the text equals condition for the additionalCell. Unfortunately in 
            // LTS, that additionalCell sometimes needs the text contains function. So if that happens, we will try to find 
            // additionalCell with text contains here. This can be seen in the RCP test titled ApplyCarryOverCredits
            if (additionalColumnCells is null)
            {
                if (tblElem.FindElements(By.XPath(xpathStringAllOtherTablesContains)).Count > 0)
                {
                    firstColumnCells = tblElem.FindElements(By.XPath(xpathStringAllOtherTablesContains));
                    correctXpathForApplication = ".//{0}[contains(., '{1}')]";

                    // Loop through each cell
                    foreach (IWebElement cell in firstColumnCells)
                    {
                        string xpathStringAdditionalCell = string.Format(correctXpathForApplication, htmlTagNameForAdditionalColCellText, additionalColCellText);

                        // Get the row for the current cell in the loop
                        row = XpathUtils.GetParentOrChildElemWithSpecifiedCriteria(cell, "parent", "tr[1]");

                        // If we find the cell with the additional cell text
                        if (row.FindElements(By.XPath(xpathStringAdditionalCell)).Count > 0)
                        {
                            additionalColumnCells = row.FindElements(By.XPath(xpathStringAdditionalCell));

                            // Return the row of this cell
                            return row;
                        }
                    }
                }
            }

            // Mostly unreachable code, blah
            throw new Exception("The row with your specified cell text was not found");
        }

        /// <summary>
        /// Returns the rows that contains the user-specified row name
        /// </summary>
        /// <param name="Browser">The driver instance</param>
        /// <param name="tblBodyElem">You table body element that is found within the your Page class. i.e. Page.MyTableNameTblBody</param>
        /// <param name="rowBy">Send any row of your table in it's By type. i.e. Bys.MyPage.MyTableNameTblBodyRow</param>
        /// <param name="rowName">The name of the row. IMPORTANT: Be sure to send the EXACT text from the text() attribute value within the 
        /// DevTools->Elements tab. In some cases, the cell text can have leading and 
        /// trailing whitespace. Include those whitespaces when you send this string to this parameter</param>
        /// <param name="xpathForRows">(Optional) The xpath for rowName minus the first two slashes. Default = "tr"</param>
        /// <returns></returns>
        public static IList<IWebElement> Grid_GetRowsByRowName(IWebDriver Browser, IWebElement tblBodyElem, By rowBy, string rowName,
            string xpathForRows = "tr")
        {
            // We need to instantiate this rows object, because if we dont, then when we use the Add method for this object, it will say Object
            // Reference Not Set To An Instance Of An Object. But to instantiate it, we have to use List, not iList. This is because using new()
            // means creating Objects and since IList is an interface, we can not create objects of it. 
            IList<IWebElement> rows = new List<IWebElement>();

            // First wait for the table body
            tblBodyElem.WaitForElement(rowBy, ElementCriteria.HasText, ElementCriteria.IsEnabled, ElementCriteria.IsEnabled);

            string xpathForParentRow = string.Format("ancestor::" + xpathForRows + "[1]");
            IList<IWebElement> cellsContainingRowName = Grid_GetCellsByCellText(Browser, tblBodyElem, rowBy, rowName, xpathForRows);

            if (cellsContainingRowName[0].FindElements(By.XPath(xpathForParentRow)).Count == 0)
            {
                throw new Exception("Could not find any rows");

            }

            foreach (IWebElement cell in cellsContainingRowName)
            {
                rows.Add(cell.FindElement(By.XPath(xpathForParentRow)));
            }

            return rows;
        }

        /// <summary>
        /// Returns true or false depending on if we find a row that contains the user-specified cell text from 2 cells. This is useful for tables
        /// that can contain non-unique rows (First column cell text can be the same)
        /// </summary>
        /// <param name="Browser">The driver instance</param>
        /// <param name="tblElem">You table element that is found within the your Page class. i.e. Page.MyTableNameTbl</param>
        /// <param name="rowBy">Send any row of your table in it's By type. i.e. Bys.MyPage.MyTableNameTblBodyRow</param>
        /// <param name="rowName">The name of the row. IMPORTANT: Be sure to send the EXACT text from the text() attribute value within the 
        /// DevTools->Elements tab. In some cases, the cell text can have leading and 
        /// trailing whitespace. Include those whitespaces when you send this string to this parameter</param>
        /// <param name="xpathForRows">The HTML tag for rowName</param>
        /// <param name="additionalColCellText">When your rowName does not have to be unique compared to other rows in 
        /// your table, you need to specify an additional column value to find your row, you can do that here. Send the 
        /// exact text of any other column's cell text for your row.</param>
        /// <param name="htmlTagNameForAdditionalColCellText">The HTML tag for additionalColumnCellText</param> 
        /// <returns></returns>
        public static bool Grid_ContainsRecord_WithAdditionalCellText(IWebDriver Browser, IWebElement tblElem, By rowBy, string rowName,
            string htmlTagNameForRowName, string additionalColCellText, string htmlTagNameForAdditionalColCellText)
        {
            bool recordFound = false;
            IList<IWebElement> firstColumnCells = null;
            IWebElement row = null;

            firstColumnCells = Grid_GetCellsByCellText(Browser, tblElem, rowBy, rowName, htmlTagNameForRowName);

            // Loop through each cell, get the row of that cell, then determine if that row contains the additionalColCellText
            foreach (IWebElement cell in firstColumnCells)
            {
                string xpathStringAdditionalCell = string.Format(".//{0}[text()='{1}']", htmlTagNameForAdditionalColCellText, additionalColCellText);

                // Get the row for the current cell in the loop
                row = XpathUtils.GetParentOrChildElemWithSpecifiedCriteria(cell, "parent", "tr[1]");

                // If we find the cell with the additional cell text
                if (row.FindElements(By.XPath(xpathStringAdditionalCell)).Count > 0)
                {
                    recordFound = true;
                    return recordFound;
                }
            }

            return recordFound;
        }

        /// <summary>
        /// Returns true if the text is found under the user-specified column of the user-specified table
        /// </summary>
        /// <param name="Browser">The driver instance</param>
        /// <param name="tblElem">You table element that is found within the your Page class. i.e. Page.MyTableNameTbl</param>
        /// <param name="tblBodyBy">You table's body element in its By type that is found within the your Page class.
        /// i.e. Bys.MyPage.MyTableNameTblBody</param>
        /// <param name="colIndex">Send the column index (starting with zero) where you are checking to make sure the text exists</param>
        /// <param name="expectedText">The text you expect to be under the specified column of your table</param>
        /// <param name="htmlTagNameForExpectedText">The HTML tag name for expectedText</param>
        /// <param name="FirstBtn">(Optional) If your table contains first and next pagination buttons, then pass the First button element first, and then the Next button element. For example, pass "Bys.CBDLearnerPage.TableFirstBtn"</param>
        /// <param name="NextBtn">(Optional) If your table contains first and next pagination buttons, then pass the First button element first, and then the Next button element. For example, pass "Bys.CBDLearnerPage.TableFirstBtn"</param>
        /// <param name="xpathForRows">(Optional) If your table's row elements are not stores within TR tags, then you need to pass the xpath of your
        /// tables row elements. That is because this method, by default, searches only for TR tagged elements when looking for rows, so if your 
        /// developers do not store your tables rows inside TR tags, then pass the xpath which will satisfy all rows in your table, then this 
        /// method will use this xpath instead when looking for rows. For example, your rows might be stored within the div tag and 
        /// might include a "role" attribute with an attribute value of "row", so you would pass //div[@role='row']</param>
        /// <param name="xpathForCells">(Optional) If your table's cell elements are not stored within TD tags, then you need to pass the xpath of your
        /// tables cell elements. That is because this method, by default, searches only for TD tagged elements when looking for cells, so if your 
        /// developers do not store your tables cells inside TD tags, then pass the xpath which will satisfy all cells in your table, then this 
        /// method will use this xpath instead when looking for cells. For example, your cell might be stored within the div tag and 
        /// might include a "role" attribute with an attribute value of "gridCell", so you would pass //div[@role='gridcell']. 
        /// Default = //tr</param>
        /// <param name="pagesToScroll">(Optional) If your table has a virtual scroll bar, then the rows in your table that are not visible 
        /// on screen will not be populated into the HTML until the user scrolls those rows into view. If this is your table, then 
        /// this method contains additional logic to scroll one page down until the record is found, or scroll one page down until the 
        /// amount of times you pass to this parameter. If you pass 10 to this parameter, and the grid does not contain the record, this 
        /// method will scroll 10 times then return false. If you dont pass this parameter, then the default = 20</param>
        /// <returns></returns>
        public static bool Grid_ContainsRecord(IWebDriver Browser, IWebElement tblElem, By tblBodyBy, int colIndex, string expectedText,
            string htmlTagNameForExpectedText, By FirstBtn = null, By NextBtn = null, string xpathForRows = "//tr", string xpathForCells = "//td",
            int pagesToScroll = 20)
        {
            Browser.WaitForElement(tblBodyBy, TimeSpan.FromSeconds(15), ElementCriteria.HasText, ElementCriteria.IsEnabled, ElementCriteria.IsEnabled);
            
            // If the table dynamically populates the HTML based on the user scrolling. For example, some tables have virtual scroll bars which
            // only populate the rows that are visible on screen into the HTML. A row that is not visible and not scrolled into view will not be
            // populated into the HTML. These kinds of tables are usually within ajax loading Angular applications. The table's
            // HTML tag will be cdk-virtual-scroll-viewport. If this is the case, then the loopCount will be equal to the pagesToScroll 
            // parameter, which will then cause the method to scroll down within the virtual scroll bar until the record is found or until 
            // the pagesToScroll loop number has reached the end. 
            bool virtualScrollFound = false;
            if (tblElem.FindElements(By.XPath("descendant::cdk-virtual-scroll-viewport")).Count > 0)
            {
                virtualScrollFound = true;
            }

            // If the table dynamically populates the HTML based on the user scrolling. For example, some tables have virtual scroll bars which
            // only populate the rows that are visible on screen into the HTML. A row that is not visible and not scrolled into view will not be
            // populated into the HTML. These kinds of tables are usually within ajax loading Angular applications. The table's
            // HTML tag will be cdk-virtual-scroll-viewport. 
            if (tblElem.FindElements(By.XPath("descendant::cdk-virtual-scroll-viewport")).Count > 0)
            {
                for (int i = 0; i < pagesToScroll; i++)
                {
                    if (Grid_CellTextFound(Browser, tblElem, colIndex, htmlTagNameForExpectedText, expectedText, xpathForRows, xpathForCells))
                    {
                        return true;
                    }

                    if (!virtualScrollFound)
                    {
                        break;
                    }

                    ElemSet.ScrollPageDownWIthinGrid(Browser, tblElem);
                    // If we dont add a sleep here, then we get a Stale Element exception within Grid_CellTextFound for some reason
                    Thread.Sleep(500);
                }
                return false;
            }

            // If the table does not contain first, next, previous and last buttons. Or if the table does contains them, 
            // but there are not enough records to have multiple pages resulting in the Next button not being visible, then
            // we only need to check one page of results
            if (FirstBtn == null || !Browser.Exists(NextBtn, ElementCriteria.IsVisible))
            {
                if (Grid_CellTextFound(Browser, tblElem, colIndex, htmlTagNameForExpectedText, expectedText, xpathForRows, xpathForCells))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            // If the table has First, Previous, Next and Last buttons. And if those buttons are visible and enabled (The class attribute would equal "first cdisabled" if
            // it was disabled), then click the First button to go to the first listing of records
            if (Browser.Exists(FirstBtn, ElementCriteria.IsVisible, ElementCriteria.AttributeValue("class", "first")))
            {
                Browser.FindElement(FirstBtn).Click();
                Thread.Sleep(2000);      // TODO: Implement logic for dynamic wait instead of sleep
                                         // Check to see if the cell is found on the first page of results
                if (Grid_CellTextFound(Browser, tblElem, colIndex, htmlTagNameForExpectedText, expectedText, xpathForRows, xpathForCells))
                {
                    return true;
                }
            }

            // I dont know why this extra If statement is here, but I am keeping it cause I dont have time to investigate it is here for a reason or 
            // not, and it has not caused any issues.
            if (Grid_CellTextFound(Browser, tblElem, colIndex, htmlTagNameForExpectedText, expectedText, xpathForRows, xpathForCells))
            {
                return true;
            }

            // If code reaches here, the cell text was not found yet, and so we should click the next button if there is one and 
            // try to find the cell text on the next page, and continue until there are no pages left
            if (Browser.Exists(NextBtn, ElementCriteria.IsVisible))
            {
                while (Browser.Exists(NextBtn, ElementCriteria.AttributeValue("class", "next"))) // While the next button is not disabled
                {
                    Browser.FindElement(NextBtn).Click(); // Click the next button
                    Thread.Sleep(2000);  // TODO: Implement logic for dynamic wait instead of sleep

                    if (Grid_CellTextFound(Browser, tblElem, colIndex, htmlTagNameForExpectedText, expectedText, xpathForRows, xpathForCells))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// If any rows in the grid contain the user-specified text, return true
        /// </summary>
        /// <param name="Browser">The driver instance</param>
        /// <param name="tblElem">You table element that is found within the your Page class. i.e. Page.MyTableNameTbl</param>
        /// <param name="colIndex">Send the column number index (starting with zero) where you are checking to make sure the text exists</param>
        /// <param name="htmlTagNameForExpectedText">The HTML tag name for expectedText</param>
        /// <param name="expectedText">The expected text</param>
        /// <param name="xpathForRows">(Optional) If your table's row elements do not get tagged with TR, then you need to pass the xpath of your
        /// tables row elements. That is because This method, by default, searches only for TR tagged elements when looking for rows, so if your 
        /// developers do not store your tables rows inside TR tags, then pass the xpath which will satisfy all rows in your table, then this 
        /// method will use this xpath instead when looking for rows. For example, your rows might be stored within the div tag and 
        /// might include a "role" attribute with an attribute value of "row", so you would pass //div[@role='row'].
        /// Default = //tr</param>
        /// <param name="xpathForCells">(Optional) If your table's cell elements do not get tagged with TD, then you need to pass the xpath of your
        /// tables cell elements. That is because this method, by default, searches only for TD tagged elements when looking for cells, so if your 
        /// developers do not store your tables cells inside TD tags, then pass the xpath which will satisfy all cells in your table, then this 
        /// method will use this xpath instead when looking for cells. For example, your cell might be stored within the div tag and 
        /// might include a "role" attribute with an attribute value of "gridCell", so you would pass //div[@role='gridcell'].
        /// Default = //td</param>
        /// <returns></returns>
        public static bool Grid_CellTextFound(IWebDriver Browser, IWebElement tblElem, int colIndex, string htmlTagNameForExpectedText,
            string expectedText, string xpathForRows = "//tr", string xpathForCells = "//td")
        {
            // Store all rows from the table into a variable
            // Have to add a dot before the xpath, else Selenium looks inside entire HTML as opposed to only within the child elements of tableElem
            IList<IWebElement> allRows = tblElem.FindElements(By.XPath("." + xpathForRows));

            // Loop through each row
            foreach (var row in allRows)
            {
                // If the given row contains more than 1 cell, if not continue For loop. Whenever the table doesnt contain a row with data 
                // (search results returned 0 records), sometimes the row contains 0 TD tags and sometimes it contains 1 TD tag 
                if (row.FindElements(By.XPath("." + xpathForCells)).Count > 1)
                {
                    //colNumber = 2;

                    // If the current row does not contain enough columns to look in regards to the index/number of the column the 
                    // tester wants, then this is obviously not the row that the tester wants to verify. We have to continue to next 
                    // iteration in this case, else it would throw an error when getting the IWebElement cell variable on the next 
                    // block of code
                    if (row.FindElements(By.XPath("." + xpathForCells)).Count - 1 < colIndex)
                    {
                        continue;
                    }

                    // Get the cell under the specified column
                    IWebElement cell = row.FindElements(By.XPath("." + xpathForCells))[colIndex];

                    if (cell.Text == expectedText)
                    {
                        return true;
                    }
                    // Sometimes a table can:
                    // 1) Represent cell text in not only the td tag or the div tag, but also tags within tags. For example, the A tag within the
                    // td tag, and also the span tag within an a tag within a td tag.
                    // 2) Have rows with certain tags, while this same table on the next row wont contain this tag. 
                    // Because of this, we have to make sure we find this before we proceed
                    if (cell.FindElements(By.TagName(htmlTagNameForExpectedText)).Count > 0)
                    {
                        // There may be multiple tags with the same name within the same cell. So find them, then loop through them 
                        // to see if the cell text exists within any of these tags
                        List<IWebElement> elemTextExistsWithin = cell.FindElements(By.TagName(htmlTagNameForExpectedText)).ToList();

                        foreach (var item in elemTextExistsWithin)
                        {
                            if (item.Text == expectedText)
                            {
                                return true;
                            }
                        }
                    }
                }

                // This IF statement is striclty for the sandbox project in our code (Wikipedia). Wikipedia does not have any proper tables, 
                // so we can not use the above IF statement. I just put this IF statement here so it works in Wikipedia
                if (expectedText == "Who writes Wikipedia?")
                {
                    // Get the cell under the specified column
                    IWebElement cell = row.FindElements(By.TagName("ul"))[colIndex];

                    // So far in different LMS applications, I have seen the cell text represented in not only the td tag, but also the a tag within
                    // td, and also the span tag within an a tag within a td tag. So we will handle those conditions here:
                    if (htmlTagNameForExpectedText == "td")
                    {
                        if (cell.Text == expectedText)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        // Sometimes a table can have rows with certain tags, while this same table on the next row wont contain this tag. 
                        // Because of this, we have to make sure we find this tag before we proceed
                        if (cell.FindElements(By.TagName(htmlTagNameForExpectedText)).Count > 0)
                        {
                            // There may be multiple tags with the same name within the same cell. So find them, then loop through them 
                            // to see if the cell text exists within any of these tags
                            List<IWebElement> elemTextExistsWithin = cell.FindElements(By.TagName(htmlTagNameForExpectedText)).ToList();

                            foreach (var item in elemTextExistsWithin)
                            {
                                if (item.Text == expectedText)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Browser"></param>
        /// <param name="btnParent"></param>
        /// <param name="menuItemText"></param>
        /// <returns></returns>
        public static IWebElement Grid_GetMenuItemOnRowButton(IWebDriver Browser, IWebElement btnParent, string menuItemText)
        {
            IWebElement dropdownMenuItem = null;

            // Sometimes the menu items are inside span tags, and sometimes a tags. We will check for both
            string xpathString = string.Format(".//span[text()='{0}']", menuItemText);
            string xpathString2 = string.Format(".//a[text()='{0}']", menuItemText);

            if (btnParent.FindElements(By.XPath(xpathString)).Count > 0)
            {
                dropdownMenuItem = btnParent.FindElement(By.XPath(xpathString), ElementCriteria.IsVisible);
            }
            else if (btnParent.FindElements(By.XPath(xpathString2)).Count > 0)
            {
                dropdownMenuItem = btnParent.FindElement(By.XPath(xpathString2), ElementCriteria.IsVisible);
            }

            else
            {
                throw new Exception("The menu item could not be found in the table you have specified with the celltext you specified");
            }

            return dropdownMenuItem;
        }

        #endregion Grids

        #region Radio Buttons

        /// <summary>
        /// Gets the radio button element based on it's label containing text. Use this instead of 
        /// <see cref="RdoBtn_GetRdoBtnByText(IWebDriver, string)"/> if your applications radio button has leading or trailing
        /// whitespaces, end lines, etc.
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="textOfRadioBtn">The exact text of one of the radio buttons inside</param>
        public static IWebElement RdoBtn_GetRdoBtnByTextContains(IWebDriver browser, string textOfRadioBtn)
        {
            IWebElement rdoBtn = null;

            string xpath = string.Format("//label[contains(text(), '{0}')]", textOfRadioBtn);

            if (browser.FindElements(By.XPath(xpath)).Count > 0)
            {
                rdoBtn = browser.FindElement(By.XPath(xpath));
                return rdoBtn;
            }

            else
            {
                throw new Exception("The code could not find your radio button. It either failed to appear on the page you are testing (defect), or " +
                    "your radio button's xpath has not been coded in this method. Add a condition for your new radio button in the above method or create a defect");
            }
        }

        /// <summary>
        /// Gets the radio button element based on it's label text. Currently developed for RCP, ABAM and the Test Portal. If it is not 
        /// compatible with your application, then add code below to condition it so it is
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="textOfRadioBtn">The exact text of one of the radio buttons inside</param>
        public static IWebElement RdoBtn_GetRdoBtnByText(IWebDriver browser, string textOfRadioBtn)
        {
            IWebElement rdoBtn = null;

            // RCP radio buttons
            // Right now in RCP, I need 2 different xpaths for radio buttons, as their tags are different
            // between learners and observers. Nirav is going to fix this. Once he does, I can implement the simpler solution
            string xpathRCPVersion1 = string.Format("//label/span[text()='{0}']/..", textOfRadioBtn);
            string xpathRCPVersion2 = string.Format("//label[text()='{0}']", textOfRadioBtn);

            // ABAM radio buttons
            string xpathABAM = string.Format("//input[@type='radio' and @value='{0}']", textOfRadioBtn);

            // Test Portal radio buttons
            string xpathTestPortal = string.Format("//span/label[text()='{0}']/preceding-sibling::input", textOfRadioBtn);


            if (browser.FindElements(By.XPath(xpathRCPVersion1)).Count > 0)
            {
                rdoBtn = browser.FindElement(By.XPath(xpathRCPVersion1));
                return rdoBtn;
            }

            else if (browser.FindElements(By.XPath(xpathRCPVersion2)).Count > 0)
            {
                xpathRCPVersion2 = string.Format("//label[text()='{0}']", textOfRadioBtn);
                rdoBtn = browser.FindElement(By.XPath(xpathRCPVersion2));
                return rdoBtn;
            }

            else if (browser.FindElements(By.XPath(xpathABAM)).Count > 0)
            {
                rdoBtn = browser.FindElement(By.XPath(xpathABAM));
                return rdoBtn;
            }

            else if (browser.FindElements(By.XPath(xpathTestPortal)).Count > 0)
            {
                rdoBtn = browser.FindElement(By.XPath(xpathTestPortal));
                return rdoBtn;
            }

            else
            {
                throw new Exception("The code could not find your radio button. It either failed to appear on the page you are testing (defect), or " +
                    "your radio button's xpath has not been coded in this method. Add a condition for your new radio button in the above method or create a defect");
            }
        }

        /// <summary>
        /// Gets the radio button element within a specified parent element based on it's label text. Currently developed for RCP, ABAM, UAMS,
        /// and the Test Portal. If it is not compatible with your application, then add code below to condition it so it is
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="textOfRadioBtn">The exact text of one of the radio buttons inside</param>
        /// <param name="ParentElem">The parent element to find your check box in</param>
        public static IWebElement RdoBtn_GetRdoBtnByText(IWebDriver browser, string textOfRadioBtn, IWebElement ParentElem)
        {
            IWebElement rdoBtn = null;

            // LMS Fireball radio buttons
            string fireballAssessmentAnswers = string.Format("descendant::span[text()='{0}']/ancestor::label[@class='radio-input-label']", textOfRadioBtn);

            // RCP radio buttons
            // Right now in RCP, I need 2 different xpaths for radio buttons, as their tags are different
            // between learners and observers. Nirav is going to fix this. Once he does, I can implement the simpler solution
            string xpathRCPVersion1 = string.Format("descendant::label/span[text()='{0}']/..", textOfRadioBtn);
            string xpathRCPVersion2 = string.Format("descendant::label[text()='{0}']", textOfRadioBtn);

            // ABAM radio buttons
            string xpathABAM = string.Format("descendant::input[@type='radio' and @value='{0}']", textOfRadioBtn);

            // Test Portal radio buttons
            string xpathTestPortal = string.Format("descendant::span/label[text()='{0}']/preceding-sibling::input", textOfRadioBtn);

            // If the xpath string contains an apostrophe, we have to switch to using double quotes in the xpath
            if (textOfRadioBtn.Contains("'"))
            {
                fireballAssessmentAnswers = string.Format("descendant::span[text()=\"{0}\"]/ancestor::label[@class='radio-input-label']", textOfRadioBtn);
                xpathRCPVersion1 = string.Format("descendant::label/span[text()=\"{0}\"]/..", textOfRadioBtn);
                xpathRCPVersion2 = string.Format("descendant::label[text()=\"{0}\"]", textOfRadioBtn);
                xpathABAM = string.Format("descendant::input[@type='radio' and @value=\"{0}\"]", textOfRadioBtn);
                xpathTestPortal = string.Format("descendant::span/label[text()=\"{0}\"]/preceding-sibling::input", textOfRadioBtn);
            }


            // Mike Johnston 7/14/19: I had to put this If statement first because LMS Fireball works with both fireballAssessmentAnswers
            // and with xpathRCPVersion1. But they find different elements, and so the conditioning I place on radio buttons at the AppFramework
            // level will not work if the below is inconsistent in it's finding. Now Fireball will always return the first If statement
            if (ParentElem.FindElements(By.XPath(fireballAssessmentAnswers)).Count > 0)
            {
                rdoBtn = ParentElem.FindElement(By.XPath(fireballAssessmentAnswers));
                return rdoBtn;
            }

            else if (ParentElem.FindElements(By.XPath(xpathRCPVersion1)).Count > 0)
            {
                rdoBtn = ParentElem.FindElement(By.XPath(xpathRCPVersion1));
                return rdoBtn;
            }

            else if (ParentElem.FindElements(By.XPath(xpathRCPVersion2)).Count > 0)
            {
                xpathRCPVersion2 = string.Format("descendant::label[text()='{0}']", textOfRadioBtn);
                rdoBtn = ParentElem.FindElement(By.XPath(xpathRCPVersion2));
                return rdoBtn;
            }

            else if (ParentElem.FindElements(By.XPath(xpathABAM)).Count > 0)
            {
                rdoBtn = ParentElem.FindElement(By.XPath(xpathABAM));
                return rdoBtn;
            }

            else if (ParentElem.FindElements(By.XPath(xpathTestPortal)).Count > 0)
            {
                rdoBtn = ParentElem.FindElement(By.XPath(xpathTestPortal));
                return rdoBtn;
            }

            else
            {
                throw new Exception(string.Format("The code could not find your radio button with text of {0}. It either failed to appear " +
                    "on the page you are testing (defect), or your radio button's xpath has not been coded in this method. Add a condition " +
                    "for your new radio button in the above method or create a defect", textOfRadioBtn));
            }

        }

        public static IList<IWebElement> RdoBtn_GetRdoBtns(IWebElement rdoBtn)
        {
            // Get the first ancestor of the radio button with the tag name of tbody. Radio buttons
            // in CBD are within tables
            //string xpath = "ancestor::tbody[1]";
            string xpath = "ancestor::div[@role='radiogroup']";
            IList<IWebElement> allRdoBtns = null;

            try
            {
                IWebElement parentTableOfRdoBtns = rdoBtn.FindElement(By.XPath(xpath));
                allRdoBtns = parentTableOfRdoBtns.FindElements(By.TagName("span"));

                // Some radio buttons in RCP are located within spans, some are labels, so we need to condition this
                if (allRdoBtns.Count == 0)
                {
                    allRdoBtns = parentTableOfRdoBtns.FindElements(By.TagName("label"));
                }

                return allRdoBtns;
            }
            catch
            {

            }

            return null;
        }

        #endregion Radio Buttons

        #region General

        /// <summary>
        /// This method checks whether an element is currently visible to the eye on the screen. Selenium's Display property did not accomplish this,
        /// so I created this method.
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="elem">The element to verify</param>
        public static bool GeneralElemVisibleOnScreen(IWebDriver browser, IWebElement elem)
        {
            // See: http://darrellgrainger.blogspot.com/2013/05/is-element-on-visible-screen.html
            int x = browser.Manage().Window.Size.Width;
            int y = browser.Manage().Window.Size.Height;
            int x2 = elem.Size.Width + elem.Location.X;
            int y2 = elem.Size.Height + elem.Location.Y;

            return x2 <= x && y2 <= y;
        }


        #endregion General
    }


}