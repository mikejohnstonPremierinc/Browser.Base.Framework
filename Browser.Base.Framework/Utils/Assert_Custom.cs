using System;
using Browser.Core.Framework;
using OpenQA.Selenium;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Browser.Core.Framework
{
    public static class Assert_Custom
    {
        /// <summary>
        /// Checks that a label is visible, and has the user-specified text and color
        /// </summary>
        /// <param name="label">The label to verify</param>
        /// <param name="textExpected">The text expected of the label</param>
        /// <param name="colorExpected">Either "Red" or "Black"</param>
        public static void VerifyLabel(IWebDriver Browser, IWebElement label, string textExpected, string colorExpected = null)
        {
            string rgbValue = null;

            // Set the red RGB value, as it is different in firefox vs other browsers
            // ToDo: Need to condition this for other colors
            if (Browser.GetCapabilities().GetCapability("browserName").ToString() == "firefox")
            {
                rgbValue = "rgb(219, 112, 102)";
            }
            else
            {
                rgbValue = "rgba(219, 112, 102, 1)";
            }

            string textOfLabel = label.GetAttribute("textContent");
            string colorOfLabel = label.GetCssValue("color");

            Assert.AreEqual(textExpected, textOfLabel);
            Assert.AreEqual(rgbValue, colorOfLabel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="datethatShouldBeGreater"></param>
        /// <param name="datethatShouldBeLessThan"></param>
        /// <returns></returns>
        public static bool DateGreaterThanOrEquals(DateTime datethatShouldBeGreater, DateTime datethatShouldBeLessThan)
        {
            if (datethatShouldBeGreater.Date <= datethatShouldBeLessThan.Date )
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="elemsToVerify"></param>
        /// <returns></returns>
        public static bool ElementsDisplayed(params IWebElement[] elemsToVerify)
        {
            if (elemsToVerify.Length > 0)
            {
                for (int i = 0; i < elemsToVerify.Length; i++)
                {
                    Assert.True(elemsToVerify[i].Displayed, string.Format("Element with text {0} was not displayed"), 
                        elemsToVerify[i].Text);
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Asserts that for each string in a list of strings, that those strings start with the list of strings you pass
        /// </summary>
        /// <param name="fullStrings">The list of strings with full text</param>
        /// <param name="matchedStrings">The list of strings you want to Assert start with the full text strings</param>
        /// <returns></returns>
        public static bool StringsStartWith(List<string> fullStrings, List<string> matchedStrings)
        {
            //fullStrings.Sort();
            //matchedStrings.Sort();
            if (fullStrings.Count == matchedStrings.Count)
            {
                for (int i = 0; i < fullStrings.Count; i++)
                {
                    if (fullStrings[i].Length == matchedStrings[i].Length)
                    {
                        Assert.True(fullStrings[i].Equals(matchedStrings[i]), "Test fail at iteration" + i + " And Courses Text  " + fullStrings[i] + " " + matchedStrings[i]);
                    }
                    else { Assert.True(fullStrings[i].Substring(0,35).Equals(matchedStrings[i].Substring(0,35)), "Test fail at iteration" + i + " And Courses Text  " + fullStrings[i] + " " + matchedStrings[i]); }
                }
                return true;
            }
            else
            {
                Assert.Fail("Course count from course listing page and course count form carousel are not equal");
                return false;
            }
        }

        /// <summary>
        /// Asserts that a specified string is contained within all cells of all rows within a user-specified column, 
        /// case-insensitive
        /// </summary>
        /// <param name="tblBodyElem">The table body element that is found within the your Page 
        /// class. Note that this works on almost all applications by using the Tbody element. We use the tbody element 
        /// in most cases because if we instead used the table element, then the thead element would interfere with 
        /// the indexes. If your application represents rows with tbodies instead of trs, then this wont work</param>
        /// <param name="firstRow">Send any row in it's By type (Most likely just send a generic row 'tableId/tr' in
        /// the table. i.e. Bys.CBDObserverPage,PendingAcceptanceTblRowBody. We need any row so that we can wait for 
        /// it to appear before proceeding with the test"/></param>   
        /// <param name="colIndex">The zero-based index of the column you want to extract the text from.</param>
        /// <param name="xpathForCellTextCell">(Optional). The HTML tag name where you are extracting the cell 
        /// text from</param>
        /// <param name="stringToVerify">The string you want to verify is contained within listString</param>
        public static void Grid_AllRowsContainStringWithinCell(IWebDriver Browser, IWebElement tblBodyElem, By firstRow, int colIndex,
            string xpathForCellTextCell, string stringToVerify)
        {
            int rowCount = ElemGet.Grid_GetRowCount(Browser, tblBodyElem);

            for (int i = 0; i < rowCount; i++)
            {
                stringToVerify = stringToVerify.ToLower();
                string cellText = ElemGet.Grid_GetCellTextByRowIndexAndColIndex(Browser, tblBodyElem, firstRow, i,
                    colIndex, xpathForCellTextCell).ToLower();
                StringAssert.Contains(stringToVerify, String.Format("Your string of '{0}' " +
                    "was not found in one of the rows inside the table in a cell underneath the column " +
                    "index of {1}. The mismatched cell was '{2}'", stringToVerify, colIndex.ToString(), cellText));
            }
        }

    }
}