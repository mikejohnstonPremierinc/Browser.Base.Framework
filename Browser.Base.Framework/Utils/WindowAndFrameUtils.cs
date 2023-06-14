using Microsoft.CodeAnalysis.CSharp.Syntax;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;


namespace Browser.Core.Framework
{
    /// <summary>
    /// A utility class for various operations such as switching to different windows and iFrames
    /// </summary>
    public static class WindowAndFrameUtils
    {
        /// <summary>
        /// Clicks a user-specified element that opens a new tab, waits for a URL containing a string and/or waits for
        /// an element to appear in the HTML and be visible, then switches the drivers focus to that window
        /// </summary>
        /// <param name="Browser">The driver instance</param>
        /// <param name="elemToClick">The element you want to click, in the elements iWebElement type</param>
        /// <param name="elemToWaitFor">(Optional) The element to wait for on the new window, in the element's By type</param>
        /// <param name="timeToWaitForElem">(Optional) The time to wait for the element. Default = 30 seconds</param>
        /// <param name="URLToWaitFor">(Optional) A string to wait for the URL to contain</param>
        /// <param name="timeToWaitForURL">(Optional) The time to wait for the URL to contain the string. Default = 30 seconds</param>
        public static void ClickOnLinkAndSwitchToWindow(IWebDriver Browser, IWebElement elemToClick, By elemToWaitFor = null,
            TimeSpan? timeToWaitForElem = null, string URLToWaitFor = null, TimeSpan? timeToWaitForURL = null)
        {
            TimeSpan nonNullableTS = TimeSpan.FromSeconds(30);

            // The ERP application's CreateEditPage has a weird design. After clicking the Submit button, a new tab briefly
            // opens for only about half of a second, then it closes the CreatEditPage, then the PC=Ctrl page loads with
            // a brand new tab. Because of this, Browser.SwitchTo().Window(Browser.WindowHandles.Last()
            // throws an error saying "no such window: window was already closed". There is no elegant or dynamic
            // way to handle this, so if this occurs, we are switching then sleeping for 1 second, then checking to
            // see if the Help button can be located. If it can not, we loop and wait another second, repeat until
            // it does not fail. ERP team has seen this issue up to 5 seconds, so we will loop 10 seconds just in case.
            // If this needs increased, we can increase at a later date
            bool onERPApplicationsCreateEditPageBeforeClick = Browser.Url.Contains("PC=Edits") ? true : false;
            bool clickingSubmitBtn = elemToClick.Text == "Submit" ? true : false;

            elemToClick.Click(Browser);

            if (onERPApplicationsCreateEditPageBeforeClick && clickingSubmitBtn)
            {
                for (int i = 0; i < 15; i++)
                {
                    try
                    {
                        Browser.SwitchTo().Window(Browser.WindowHandles.Last());
                        Thread.Sleep(1000);
                        Browser.WaitForURLToContainString("PC=Ctrl", TimeSpan.FromSeconds(15));
                        //Browser.FindElement(By.Id("butHelp"));
                        break;
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
            else
            {
                Browser.SwitchTo().Window(Browser.WindowHandles.Last());
            }

            if (elemToWaitFor != null)
            {
                if (timeToWaitForElem.HasValue)
                {
                    nonNullableTS = timeToWaitForElem.Value;
                }

                Browser.WaitForElement(elemToWaitFor, nonNullableTS, ElementCriteria.IsVisible);
            }

            if (URLToWaitFor != null)
            {

                if (timeToWaitForURL.HasValue)
                {
                    nonNullableTS = timeToWaitForURL.Value;
                }
                Browser.WaitForURLToContainString(URLToWaitFor, nonNullableTS);
            }
        }

        /// <summary>
        /// Closes a user-specified window, then switches Selenium focus to a user-specified window
        /// </summary>
        public static void CloseWindowthenSwitchToWindow(IWebDriver Browser, int indexofWindowToClose,
            int indexOfWindowToSwitchTo)
        {
            var tabs = Browser.WindowHandles;

            Browser.SwitchTo().Window(tabs[indexofWindowToClose]);
            Browser.Close();
            Browser.SwitchTo().Window(tabs[indexOfWindowToSwitchTo]);
        }

        /// <summary>
        /// Clicks a user-specified element that opens a frame, switches to default content (Outside of all frames), 
        /// waits for the frame, switches the drivers focus to that frame then waits for an element to appear in the
        /// frame
        /// </summary>
        /// <param name="Browser">The driver instance</param>
        /// <param name="elemToClick">The element you want to click, in the elements iWebElement type</param>
        /// <param name="frameToSwitchTo">The frame you want to switch to, in the frame's By type</param>
        /// <param name="timeToWaitForFrame">(Optional) The time to wait for the frame. Default = 30 seconds</param>
        /// <param name="elemToWaitFor">(Optional) The element to wait for within the new frame, in the element's By type</param>
        /// <param name="timeToWaitForElem">(Optional) The time to wait for the element. Default = 30 seconds</param>
        /// <param name="switchToFrame">(Optional) Switches to the frame. Default = false</param>
        public static void ClickOnElem_WaitForFrame_OptionallySwitchToFrame(IWebDriver Browser, IWebElement elemToClick, By frameToSwitchTo,
            TimeSpan? timeToWaitForFrame = null, By elemToWaitFor = null, TimeSpan? timeToWaitForElem = null, bool switchToFrame = false)
        {
            elemToClick.Click(Browser);

            WaitForFrame_OptionallySwitchToFrame(Browser, frameToSwitchTo, timeToWaitForFrame, switchToFrame);
        }


        /// <summary>
        /// Clicks a tester-specified element, then waits for the current tab to close, then switches to the last indexed tab
        /// </summary>
        /// <param name="Browser">The driver instance</param>
        /// <param name="elemToClick">The element to click that results in the tab closing</param>
        /// <param name="timeToWaitForWindowToClose">The maximum amount of time to wait for the window to close</param>
        /// <param name="alertWillAppearAfterButtonIsClicked">(Optional). Set to true if you know that clicking the element results in an Alert window to appear. This will
        /// wait for the alert then close it.
        /// Default = false</param>
        /// <param name="timeToWaitForAlert">(Optional).The maximum amount of time to wait for the alert window to appear. 
        /// Default = 5 seconds</param>
        /// <returns></returns>
        public static void ClickOnElem_WaitUntilWindowCloses_SwitchToLastWindow(IWebDriver Browser, IWebElement elemToClick, 
            int timeToWaitForWindowToClose, bool alertWillAppearAfterButtonIsClicked = false, TimeSpan? timeToWaitForAlert = null)
        {
            int originalWindowCount = Browser.WindowHandles.Count;
            int expectedWindowCount = originalWindowCount - 1;
            TimeSpan nonNullableTS = TimeSpan.FromSeconds(5);

            elemToClick.Click();

            // If the tester specified that he wants to wait for an Alert and close it
            if (alertWillAppearAfterButtonIsClicked)
            {
                if (timeToWaitForAlert.HasValue)
                {
                    nonNullableTS = timeToWaitForAlert.Value;
                }

                WaitUtils.WaitAndClickAlert(Browser, nonNullableTS);
            }

            bool windowClosed = false;

            int counter = 0;

            while (!windowClosed)
            {
                if (Browser.WindowHandles.Count == expectedWindowCount)
                {
                    windowClosed = true;
                    // Sleeping a second here just in case
                    Thread.Sleep(1000);
                    Browser.SwitchTo().Window(Browser.WindowHandles.Last());
                    return;
                }

                Thread.Sleep(1000);

                counter++;

                if (counter > timeToWaitForWindowToClose)
                {
                    throw new Exception(String.Format("Tab failed to close after the timeout of '{0}'", timeToWaitForWindowToClose));

                }
            }
        }

        /// <summary>
        /// Verifies the alert message label, closes an Alert, then waits for the current tab to close, then switches to the last indexed tab
        /// </summary>
        /// <param name="Browser">The driver instance</param>
        /// <param name="timeToWaitForWindowToClose">(Optional).The maximum amount of seconds to wait for the window to close. Default = 30</param>
        /// <param name="timeToWaitForAlert">(Optional).The maximum amount of time to wait for the alert window to appear. 
        /// Default = 5 seconds</param>
        /// <returns></returns>
        public static string CloseAlert_WaitUntilWindowCloses_SwitchToLastWindow_ReturnAlertText(IWebDriver Browser, 
            TimeSpan? timeToWaitForAlert = null, int timeToWaitForWindowToClose = 30)
        {
            int originalWindowCount = Browser.WindowHandles.Count;
            int expectedWindowCount = originalWindowCount - 1;
            TimeSpan nonNullableTS = TimeSpan.FromSeconds(5);

            if (timeToWaitForAlert.HasValue)
            {
                nonNullableTS = timeToWaitForAlert.Value;
            }

            IAlert alert = WaitUtils.WaitAndSwitchToAlert(Browser, nonNullableTS);

            string alertText = alert.Text;

            alert.Accept();

            bool windowClosed = false;

            int counter = 0;

            while (!windowClosed)
            {
                if (Browser.WindowHandles.Count == expectedWindowCount)
                {
                    windowClosed = true;
                    // Sleeping a second here just in case
                    Thread.Sleep(1000);
                    Browser.SwitchTo().Window(Browser.WindowHandles.Last());
                    break;
                }

                Thread.Sleep(1000);

                counter++;

                if (counter > timeToWaitForWindowToClose)
                {
                    throw new Exception(String.Format("Tab failed to close after the timeout of '{0}'", timeToWaitForWindowToClose));

                }
            }

            return alertText;
        }

        /// <summary>
        /// Clicks a user-specified element that opens a page with a frame, switches to default content (Outside of all frames), 
        /// waits for the parent frame, switches the drivers focus to that frame. Then optionally will 
        /// waites and switches to a child frame, then optionally wait for an element to appear in the child frame
        /// </summary>
        /// <param name="Browser">The driver instance</param>
        /// <param name="elemToClick">The element you want to click, in the elements iWebElement type</param>
        /// <param name="parentFrameToSwitchTo">The parent frame you want to switch to, in the frame's By type</param>
        /// <param name="childFrameToWaitFor">(Optional) The child frame to switch to, in the frame's By type.</param>
        /// <param name="timeToWaitForFrames">(Optional) The time to wait for each frame. Default = 30 seconds</param>
        /// <param name="switchToFrame">(Optional) Switches to the child frame. If false, switches to DefaultCOntent. Default = false</param>
        /// <param name="elemToWaitFor">(Optional) The element to wait for within the frame that you switched to, 
        /// in the element's By type</param>
        /// <param name="timeToWaitForElem">(Optional) The time to wait for the element within the child frame. Default = 30 seconds</param>
        public static void ClickOnElem_WaitForFrames_OptionallySwitchToChildFrame(IWebDriver Browser, IWebElement elemToClick,
            By parentFrameToSwitchTo, By childFrameToWaitFor, TimeSpan? timeToWaitForFrames = null, bool switchToFrame = false,
            By elemToWaitFor = null, TimeSpan? timeToWaitForElem = null)
        {
            elemToClick.Click(Browser);

            WaitForFrames_OptionallySwitchToChildFrame(Browser, parentFrameToSwitchTo, childFrameToWaitFor, timeToWaitForFrames,
                switchToFrame, elemToWaitFor, timeToWaitForElem);
        }

        /// <summary>
        /// Waits for a frame, optionally switches the drivers focus to that frame. This method works whether you are on the 
        /// DefaultContent, or whether you are already inside a frame and want to wait for a child frame
        /// </summary>
        /// <param name="Browser">The driver instance</param>
        /// <param name="FrameToSwitchTo">The frame you want to wait for and optionally switch to, in the frame's By type</param>
        /// <param name="timeToWaitForFrame">(Optional) The time to wait for each frame. Default = 30 seconds</param>
        /// <param name="switchToFrame">(Optional) Switches to the frame. Default = false</param>
        /// <param name="elemToWaitFor">(Optional) The element to wait for within the child frame that you switched to, 
        /// in the element's By type</param>
        /// <param name="timeToWaitForElem">(Optional) The time to wait for the element within the child frame. 
        /// Default = 30 seconds</param>
        public static void WaitForFrame_OptionallySwitchToFrame(IWebDriver Browser, By FrameToSwitchTo,
            TimeSpan? timeToWaitForFrame = null, bool switchToFrame = false, By elemToWaitFor = null, TimeSpan? timeToWaitForElem = null)
        {
            TimeSpan nonNullableTS = TimeSpan.FromSeconds(30);

            if (timeToWaitForFrame.HasValue)
            {
                nonNullableTS = timeToWaitForFrame.Value;

            }

            Browser.WaitForElement(FrameToSwitchTo, nonNullableTS);
            IWebElement Frame = Browser.WaitForElement(FrameToSwitchTo, nonNullableTS, ElementCriteria.IsVisible);

            if (switchToFrame)
            {
                Browser.SwitchTo().Frame(Frame);

                if (elemToWaitFor != null)
                {
                    if (timeToWaitForElem.HasValue)
                    {
                        nonNullableTS = timeToWaitForElem.Value;
                    }

                    Browser.WaitForElement(elemToWaitFor, nonNullableTS, ElementCriteria.IsVisible);
                }
            }
        }

        /// <summary>
        /// Switches to default content (Outside of all frames), waits for the parent frame, switches the drivers focus to that frame. 
        /// After this frame has focus, it will then wait for the child frame, then either switches into the child frame or switches
        /// back to DefaultContent
        /// </summary>
        /// <param name="Browser">The driver instance</param>
        /// <param name="parentFrameToSwitchTo">The first frame you want to switch to, in the frame's By type</param>
        /// <param name="childFrameToWaitFor">The child frame to wait for, in the frame's By type.</param>
        /// <param name="timeToWaitForFrames">(Optional) The time to wait for each frame. Default = 30 seconds</param>
        /// <param name="switchToFrame">(Optional) Switches to the child frame. If false, switches to DefaultCOntent. Default = false</param>
        /// <param name="elemToWaitFor">(Optional) The element to wait for within the child frame that you switched to, 
        /// in the element's By type</param>
        /// <param name="timeToWaitForElem">(Optional) The time to wait for the element within the child frame. 
        /// Default = 30 seconds</param>
        public static void WaitForFrames_OptionallySwitchToChildFrame(IWebDriver Browser, By parentFrameToSwitchTo, By childFrameToWaitFor,
            TimeSpan? timeToWaitForFrames = null, bool switchToFrame = false, By elemToWaitFor = null, TimeSpan? timeToWaitForElem = null)
        {
            TimeSpan nonNullableTS = TimeSpan.FromSeconds(30);

            if (timeToWaitForFrames.HasValue)
            {
                nonNullableTS = timeToWaitForFrames.Value;

            }
            Browser.SwitchTo().DefaultContent();

            Browser.SwitchTo().Frame(Browser.WaitForElement(parentFrameToSwitchTo, nonNullableTS, ElementCriteria.IsVisible));

            Browser.WaitForElement(childFrameToWaitFor, nonNullableTS, ElementCriteria.IsVisible);

            if (switchToFrame)
            {
                Browser.SwitchTo().Frame(Browser.FindElement(childFrameToWaitFor));

                if (elemToWaitFor != null)
                {
                    if (timeToWaitForElem.HasValue)
                    {
                        nonNullableTS = timeToWaitForElem.Value;
                    }

                    Browser.WaitForElement(elemToWaitFor, nonNullableTS, ElementCriteria.IsVisible);
                }
            }
            else
            {
                Browser.SwitchTo().DefaultContent();
            }
        }

    }
}