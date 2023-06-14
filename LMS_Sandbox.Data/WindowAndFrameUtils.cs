//using OpenQA.Selenium;
//using OpenQA.Selenium.Interactions;
//using OpenQA.Selenium.Support.UI;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Reflection;
//using System.Text.RegularExpressions;
//using System.Threading;


//namespace Browser.Core.Framework
//{
//    /// <summary>
//    /// A utility class for various operations such as switching to different windows and iFrames
//    /// </summary>
//    public static class WindowAndFrameUtils
//    {
//        /// <summary>
//        /// Clicks a user-specified element that opens a new tab, waits for a URL containing a string and/or waits for
//        /// an element to appear in the HTML and be visible, then switches the drivers focus to that window
//        /// </summary>
//        /// <param name="Browser">The driver instance</param>
//        /// <param name="elemToClick">The iWebelement of the element you want to click</param>
//        /// <param name="xpathOfElementToWaitFor">(Optional) The By of an element to wait for on the new window.</param>
//        /// <param name="timeToWaitForElem">(Optional) The time to wait for the element. Default = 30 seconds</param>
//        /// <param name="URLToWaitFor">(Optional) A string to wait for the URL to contain</param>
//        /// <param name="timeToWaitForURL">(Optional) The time to wait for the URL to contain the string. Default = 30 seconds</param>
//        public static void ClickOnLinkAndSwitchToWindow(IWebDriver Browser, IWebElement elemToClick,
//            By xpathOfElementToWaitFor = null, TimeSpan? timeToWaitForElem = null,
//            string URLToWaitFor = null, TimeSpan? timeToWaitForURL = null)
//        {
//            elemToClick.Click(Browser);

//            // IE opens in a new window and not maximized
//            if (Regex.Replace(
//                Browser.GetCapabilities().GetCapability("browserName").ToString(), @"\s+", "") ==
//                Regex.Replace(BrowserNames.InternetExplorer, @"\s+", ""))
//            {
//                Browser.SwitchTo().Window(Browser.WindowHandles.Last());
//                Browser.Manage().Window.Maximize();
//            }
//            else
//            {
//                Browser.SwitchTo().Window(Browser.WindowHandles.Last());
//            }

//            if (xpathOfElementToWaitFor != null)
//            {
//                TimeSpan nonNullableTS = TimeSpan.FromSeconds(30);

//                if (timeToWaitForElem.HasValue)
//                {
//                    nonNullableTS = timeToWaitForElem.Value;
//                }

//                Browser.WaitForElement(xpathOfElementToWaitFor, nonNullableTS, ElementCriteria.IsVisible);
//            }

//            if (URLToWaitFor != null)
//            {
//                TimeSpan nonNullableTS = TimeSpan.FromSeconds(30);

//                if (timeToWaitForURL.HasValue)
//                {
//                    nonNullableTS = timeToWaitForURL.Value;
//                }
//                Browser.WaitForURLToContainString(URLToWaitFor, nonNullableTS);
//            }
//        }

//        /// <summary>
//        /// Closes a user-specified window, then switches Selenium focus to a user-specified window
//        /// </summary>
//        public static void CloseWindowthenSwitchToWindow(IWebDriver Browser, int indexofWindowToClose,
//            int indexOfWindowToSwitchTo)
//        {
//            var tabs = Browser.WindowHandles;

//            Browser.SwitchTo().Window(tabs[indexofWindowToClose]);
//            Browser.Close();
//            Browser.SwitchTo().Window(tabs[indexOfWindowToSwitchTo]);
//        }
//    }
//}

