using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using Browser.Core.Framework;
using LMS.Data;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace LMSAdmin.AppFramework
{
    /// <summary>
    /// A utility class retrieving data and properties from elements within LMSAdmin. This class is an alternative to ElemGet.cs. I created this because 
    /// LMSAdmin's HTML design is really bad and obsolete in terms of newer HTML standards and practices, and so a majority of the 
    /// methods inside ElemGet will not work with LMSAdmin. If you find that a method inside ElemGet works, then use that, if not, 
    /// then you will have to create (or find) a custom method inside this class just for LMSAdmin
    /// </summary>
    public class ElemGet_LMSAdmin
    {
        #region Checkbox


        #endregion Checkbox

        #region dropdown custom



        #endregion dropdown custom

        #region Select Elements


        #endregion Select Elements

        #region Grids

        /// <summary>
        /// Returns the row that contains the user-specified cell
        /// </summary>
        /// <param name="tblElem">You table element that is found within the your Page class. i.e. OP.PendingAcceptanceTbl</param>
        /// <param name="firstRowby">Send any row (Most likely just send the iwebelement that is a generic row 'tableId/tr' in the table, as it exists in your By type. i.e. Bys.CBDObserverPage,PendingAcceptanceTblRowBody). We need any row so that we can wait for it to appear before proceeding with the test"/></param>
        /// <param name="firstColumnCellText">The name of the row. i.e. The exact text from cell inside the first column</param>
        /// <param name="tagNameWhereTextExists">Inspect the text in the first row cell element and extract the tag name</param>
        /// <returns></returns>
        public static IWebElement Grid_GetRowByRowName(IWebElement tblElem, By firstRowby, string firstColumnCellText, string tagNameWhereTextExists)
        {
            IWebElement firstColumnCell = null;

            // First wait for the table
            tblElem.WaitForElement(firstRowby, TimeSpan.FromSeconds(240), ElementCriteria.HasText, ElementCriteria.IsEnabled);

            // Sometimes the text of the cell is contained within the A tag, and sometimes it is contained within the td tag, and even
            // a div tag. We are using a parameter (tagNameWhereTextExists) to handle this conditions. We will use an IF statements to 
            // condition when cells have leading and trailing white space
            string xpath = string.Format(".//{0}[text()='{1}']", tagNameWhereTextExists, firstColumnCellText);
            string xpathWithExtraSpaces = string.Format(".//{0}[contains(., '{1}')]", tagNameWhereTextExists, firstColumnCellText);

            // If we find elements by using the TD tag text equals xpathstring for specific RCP table
            if (tblElem.FindElements(By.XPath(xpath)).Count > 0)
            {
                firstColumnCell = tblElem.FindElements(By.XPath(xpath))[0];
            }
            // If we find elements by using the TD tag text equals xpathstring
            else if (tblElem.FindElements(By.XPath(xpathWithExtraSpaces)).Count > 0)
            {
                firstColumnCell = tblElem.FindElements(By.XPath(xpathWithExtraSpaces))[0];
            }
        
            else
            {
                throw new Exception("The cell text for either column could not be found in the table you have specified. Either the row");
            }

            // Then get the 1st parent row element
            IWebElement row = XpathUtils.GetParentOrChildElemWithSpecifiedCriteria(firstColumnCell, "parent", "tr[1]");

            return row;
        }

       

        /// <summary>
        /// Returns the row that contains the user-specified cell text from 2 cells. This is useful for tables that can contain 
        /// non-unique rows (First column cell text can be the same)
        /// </summary>
        /// <param name="tblElem">You table element that is found within the your Page class. i.e. OP.PendingAcceptanceTbl</param>
        /// <param name="by">Your row element as it exists in your By type. i.e. Bys.CBDObserverPage,PendingAcceptanceTblRowBody"/></param>
        /// <param name="firstColumnCellText">The name of the row. i.e. The exact text from cell inside the first column</param>
        /// <param name="tagNameWhereFirstColCellTextExists">The HTML tag name where the firstColumnCellText exists</param>
        /// <param name="additionalColCellText">If the first column in your row does not have to be unique compared to other rows in your table, and you would want to specify an additional column value to find your row, you can do that here. Send the exact text of any other column.</param>
        /// <param name="tagNameWhereAddColCellTextExists">The HTML tag name where the additionalColumnCellText exists</param> 
        /// <returns></returns>
        public static IWebElement Grid_GetRowByRowNameAndAdditionalCellName(IWebDriver Browser, IWebElement tblElem, By rowBy, string firstColumnCellText,
            string tagNameWhereFirstColCellTextExists, string additionalColCellText, string tagNameWhereAddColCellTextExists)
        {
            IList<IWebElement> firstColumnCells = null;
            IList<IWebElement> additionalColumnCells = null;
            IWebElement row = null;

            // Now find all the cells that contain the firstColumnCellText
            firstColumnCells = ElemGet.Grid_GetCellsByCellText(Browser, tblElem, rowBy, firstColumnCellText, tagNameWhereFirstColCellTextExists);

            // Loop through each cell
            foreach (IWebElement cell in firstColumnCells)
            {
                string xpathStringAdditionalCell = string.Format(".//{0}[text()='{1}']", tagNameWhereAddColCellTextExists, additionalColCellText);

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

            // Mostly unreachable code, blah
            return null;
        }

        /// <summary>
        /// Returns the row based on its index
        /// </summary>
        /// <param name="TblBodyElem">You table body element that is found within the your Page class. i.e. OP.PendingAcceptanceTblBody</param>
        /// <param name="firstRowby">Send any row (Most likely just send the iwebelement that is a generic row 'tableId/tr' in the table, as it exists in your By type. i.e. Bys.CBDObserverPage,PendingAcceptanceTblRowBody). We need any row so that we can wait for it to appear before proceeding with the test"/></param>
        /// <param name="rowNum">The row number (not zero-based index) for the row you want to return</param>
        /// <returns></returns>
        public static IWebElement Grid_GetRowByRowIndex(IWebElement TblBodyElem, By firstRowby, int rowNum)
        {
            // First wait for the table
            TblBodyElem.WaitForElement(firstRowby, TimeSpan.FromSeconds(240), ElementCriteria.HasText, ElementCriteria.IsEnabled);

            
            // Get the row
            IWebElement row = TblBodyElem.FindElement(
                By.XPath(string.Format("./tr[@class='ccTableRow' or @class='ccTableRowAlt'][{0}]", rowNum)));

            return row;
        }

        /// <summary>
        /// Returns all rows in a table
        /// </summary>
        /// <param name="TblBodyElem">You table body element that is found within the your Page class. i.e. OP.PendingAcceptanceTblBody</param>
        /// <param name="firstRowby">Send any row (Most likely just send the iwebelement that is a generic row 'tableId/tr' in the table, as it exists in your By type. i.e. Bys.CBDObserverPage,PendingAcceptanceTblRowBody). We need any row so that we can wait for it to appear before proceeding with the test"/></param>
        /// <returns></returns>
        public static IList<IWebElement> Grid_GetRows(IWebElement TblBodyElem, By firstRowby)
        {
            // First wait for the table
            TblBodyElem.WaitForElement(firstRowby, TimeSpan.FromSeconds(240), ElementCriteria.HasText, ElementCriteria.IsEnabled);

            //// Get the index at which the tr tag exists that represents the first row
            //string firstRowByStringValue = firstRowby.ToString();
            //int indexOfLastSquareBracketFromFirstRowBy = firstRowByStringValue.ToString().LastIndexOf(']') - 1;
            //string trTagIndex = firstRowByStringValue.Substring(indexOfLastSquareBracketFromFirstRowBy, 1);

            //// Get the index of the row the user wants
            //string indexOfUserSpecifiedRow = (Int32.Parse(trTagIndex) + 1).ToString();
            IList<IWebElement> rows = null;
            if (TblBodyElem.FindElements(firstRowby).Count > 0)
            {
                rows = TblBodyElem.FindElements(firstRowby);
            }
           
            string legacyxpath = string.Format("./tr[@class='ccTableRow' or @class='ccTableRowAlt']");

           if (TblBodyElem.FindElements(By.XPath(legacyxpath)).Count>0 )
            {
               rows = TblBodyElem.FindElements(By.XPath(legacyxpath));
            }

            return rows;
        }

        /// <summary>
        /// If any of the row in any Table in the page contain the user-specified text in the given column type, return true
        /// </summary>
        /// <param name="tableElemBy"> Table element by type; as there similar multiple tables available in the same page</param>
        /// <param name="tableColumnEleBy">Send Table Column by type; where you are checking to make sure the text exists</param>        
        /// <param name="expectedText"></param>
        /// <returns></returns>
        public static bool Grid_CellTextFound(IWebDriver browser, IWebElement tableElem , By tableColumnEleBy, string expectedText)
        {
                foreach (var columncell in tableElem.FindElements(tableColumnEleBy))
                {
                    if (columncell.Text.Trim() == expectedText)
                    {
                        return true;
                    }
                }                           

            return false;
        }

        /// <summary>
        /// Get the element within a user-specified row, such as a check box, or an X icon, or a + icon, or a Pencil Icon
        /// </summary>
        /// <param name="row">The row that contains the element you want to get. To get the row, <see cref="ElemGet.Grid_GetRowByRowName(Browser, IWebElement, By, string)"/></param>
        /// <param name="tagNameOfElemToclick"></param>
        /// <param name="indexOfElemToClick">(Optional). If your row has multiple elements with the same tag name, you can specify the index at which your tag name you want to get. Default is 0 for the first instance</param>
        public static IWebElement Grid_GetButtonOrLinkWithoutTextWithinRow(IWebElement row, string tagNameOfElemToclick, int indexOfElemToClick = 0)
        {
            string xpathString = string.Format(string.Format("./descendant::{0}", tagNameOfElemToclick));

            var elem = row.FindElements(By.XPath(xpathString));

            return elem[indexOfElemToClick];
        }
        

        #endregion Grids

        #region Radio Buttons



        #endregion Radio Buttons


        #region General
        /// <summary>
        /// Switch the control to last or latest opened window by the browser
        /// </summary>
        /// <param name="browser"></param>
        /// <returns></returns>
        public static dynamic SwitchToLastOpenedWindow(IWebDriver browser)
        {
            try { browser.WaitJSAndJQuery(); } catch { }
            return browser.SwitchTo().Window(browser.WindowHandles.Last());
        }

        /// <summary>
        /// Switch the control back to the Parent or Main window
        /// </summary>
        /// <param name="browser"></param>
        /// <returns></returns>
        public static dynamic SwitchToParentWindow(IWebDriver browser)
        {
            try { browser.WaitJSAndJQuery(); } catch { }
            return browser.SwitchTo().Window(browser.WindowHandles.First());
        }


        #endregion General
    }


}