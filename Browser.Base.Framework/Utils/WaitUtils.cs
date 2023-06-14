using Browser.Core.Framework;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Browser.Core.Framework
{
    /// <summary>
    /// Extension methods that allow waiting for particular critieria to become true.
    /// </summary>

    public static class WaitUtils
    {
        /// <summary>
        /// The default timeout used for all methods is 30 seconds (unless otherwise specified).
        /// </summary>
        public static readonly TimeSpan Timeout = TimeSpan.FromSeconds(30);

        #region WaitUntil

        /// <summary>
        /// Waits the until the specified criteria is true. If the default timeout occurs, an exception will be thrown.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input">The input.</param>
        /// <param name="criteria">The criteria to be met.</param>
        public static void WaitUntil<T>(this T input, ICriteria<T> criteria)
        {
            WaitUntilImpl(CriteriaExtensions.MeetsAll, input, Timeout, criteria);
        }

        /// <summary>
        /// Waits the until the specified criteria is true. If the timeout occurs, an exception will be thrown.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input">The input.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="criteria">The criteria to be met.</param>
        public static void WaitUntil<T>(this T input, TimeSpan timeout, ICriteria<T> criteria)
        {
            WaitUntilImpl(CriteriaExtensions.MeetsAll, input, timeout, criteria);
        }

        /// <summary>
        /// Waits the until ALL of the specified criteria are true. If the default timeout occurs, an exception will be thrown.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input">The input.</param>
        /// <param name="criteria">The criteria to be met.</param>
        public static void WaitUntilAll<T>(this T input, params ICriteria<T>[] criteria)
        {
            WaitUntilImpl(CriteriaExtensions.MeetsAll, input, Timeout, criteria);
        }

        /// <summary>
        /// Waits the until ALL of the specified criteria are true. If the timeout occurs, an exception will be thrown.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input">The input.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="criteria">The criteria to be met.</param>
        public static void WaitUntilAll<T>(this T input, TimeSpan timeout, params ICriteria<T>[] criteria)
        {
            WaitUntilImpl(CriteriaExtensions.MeetsAll, input, timeout, criteria);
        }

        /// <summary>
        /// Waits the until ANY of the specified criteria are true. If the default timeout occurs, an exception will be thrown.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input">The input.</param>
        /// <param name="criteria">The criteria to be met.</param>
        public static void WaitUntilAny<T>(this T input, params ICriteria<T>[] criteria)
        {
            WaitUntilImpl(CriteriaExtensions.MeetsAny, input, Timeout, criteria);
        }

        /// <summary>
        /// Waits the until ANY of the specified criteria are true. If the timeout occurs, an exception will be thrown.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input">The input.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="criteria">The criteria to be met.</param>
        public static void WaitUntilAny<T>(this T input, TimeSpan timeout, params ICriteria<T>[] criteria)
        {
            WaitUntilImpl(CriteriaExtensions.MeetsAny, input, timeout, criteria);
        }

        private static void WaitUntilImpl<T>(Func<IEnumerable<ICriteria<T>>, T, bool> impl, T input, TimeSpan timeout, params ICriteria<T>[] criteria)
        {
            // Note: We ignore the input here, we're just using the timing functionality
            // of the wait
            var wait = new DefaultWait<object>(new object());
            wait.Timeout = timeout;
            try
            {
                wait.Until(p =>
                {
                    return impl(criteria, input);
                });
            }
            catch (WebDriverTimeoutException ex)
            {
                var failures = criteria.Failures(input);
                throw new WebDriverTimeoutException(
                    string.Format("Timed out after {0} seconds, waiting for {1} criteria.  The following criteria were not met: {2}",
                    wait.Timeout.TotalSeconds, criteria.Count(), failures.Description(input)), ex);
            }
        }

        #endregion

        #region WaitForElement

        /// <summary>
        /// Waits until a descendent element is available that matches the specified criteria.  If the default timeout expires before a matching element 
        /// is found, an exception is thrown.
        /// </summary>
        /// <param name="context">The context from which the search should be executed.</param>
        /// <param name="by">The by.</param>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        public static IWebElement WaitForElement(this ISearchContext context, By by, params ICriteria<IWebElement>[] criteria)
        {
            return WaitForElementImpl(context, by, Timeout, criteria);
        }

        /// <summary>
        /// Waits until a descendent element is available that matches the specified criteria.  If the timeout expires before a matching element 
        /// is found, an exception is thrown.
        /// </summary>
        /// <param name="context">The context from which the search should be executed.</param>
        /// <param name="by">The by.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        public static IWebElement WaitForElement(this ISearchContext context, By by, TimeSpan timeout,
            params ICriteria<IWebElement>[] criteria)
        {
            return WaitForElementImpl(context, by, timeout, criteria);
        }

        #endregion

        #region WaitForSelectElement

        /// <summary>
        /// Waits until a descendent element is available that matches the specified criteria.  If the default timeout expires before a matching element is found, an exception is thrown.
        /// </summary>
        /// <param name="context">The context from which the search should be executed.</param>
        /// <param name="by">The by.</param>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        public static SelectElement WaitForSelectElement(this ISearchContext context, By by, params ICriteria<IWebElement>[] criteria)
        {
            return WaitForSelectElementImpl(context, by, Timeout, criteria);
        }

        /// <summary>
        /// Waits until a descendent element is available that matches the specified criteria.  If the timeout expires before a matching element is found, an exception is thrown.
        /// </summary>
        /// <param name="context">The context from which the search should be executed.</param>
        /// <param name="by">The by.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        public static SelectElement WaitForSelectElement(this ISearchContext context, By by, TimeSpan timeout, params ICriteria<IWebElement>[] criteria)
        {
            return WaitForSelectElementImpl(context, by, timeout, criteria);
        }

        #endregion

        #region Private Helpers

        private static SelectElement WaitForSelectElementImpl(ISearchContext context, By by, TimeSpan timeout,
            params ICriteria<IWebElement>[] criteria)
        {
            var element = WaitForElementImpl(context, by, timeout, criteria);
            return new SelectElement(element);
        }

        private static IWebElement WaitForElementImpl(ISearchContext context, By by, TimeSpan timeout,
            IEnumerable<ICriteria<IWebElement>> criteria, bool retryStaleElement = false)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            if (by == null)
                throw new ArgumentNullException("by");

            var wait = new SearchContextWait(context, timeout);

            IWebElement element = null;
            IEnumerable<ElementCriteria> failures = Enumerable.Empty<ElementCriteria>();
            string lastMessage = null;
            try
            {
                //MIKE2
                wait.Until(x =>
                {
                    // Find ALL elements that match the By clause
                    try
                    {
                        element = context.FindElement(by, criteria.ToArray());
                        return element != null;
                    }
                    catch (NotFoundException ex)
                    {
                        lastMessage = ex.Message;
                        lastMessage = string.Format("{0} within the timeout period of {1} seconds", lastMessage, wait.Timeout.TotalSeconds);
                    }
                    return false;
                });
            }
            catch (WebDriverTimeoutException)
            {
                // The FindElement method takes care of formatting a nice failure message, so we re-use that message
                throw new NotFoundException(lastMessage);
            }

            return element;
        }

        #endregion

        #region WaitForCustom

        /// <summary>
        /// Waits for an Alert window. If not found, throws an exception. If found, accepts the Alert window. 
        /// To manually invoke an alert window for debugging purposes, enter this into the Console window... window.alert('hello')
        /// </summary>
        /// <param name="Browser"></param>
        /// <param name="timeout"></param>
        public static void WaitAndClickAlert(IWebDriver Browser, TimeSpan timeout)
        {
            IAlert alert = WaitAndSwitchToAlert(Browser, timeout);
            alert.Accept();
        }

        /// <summary>
        /// Waits for an  Alert window. If not found, throws an exception. To manually invoke an alert window for debugging purposes, 
        /// enter this into the Console window... window.alert('hello')
        /// </summary>
        /// <param name="Browser"></param>
        /// <param name="timeout"></param>
        public static IAlert WaitAndSwitchToAlert(IWebDriver Browser, TimeSpan timeout)
        {
            var wait = new WebDriverWait(Browser, timeout);
            IAlert alert = null;
            wait.Until(driver =>
            {
                try
                {
                    alert = Browser.SwitchTo().Alert();
                    return Browser.SwitchTo().Alert();                  
                }
                catch (NoAlertPresentException e)
                {
                    throw new Exception(string.Format("Alert did not appear: {0}", e.Message));
                }
            });

            return alert;
        }

        /// <summary>
        /// Waits for all Javascript and JQuery requests to be completed
        /// For details, see https://www.swtestacademy.com/selenium-wait-javascript-angular-ajax/
        /// https://stackoverflow.com/questions/25062969/testing-angularjs-with-selenium
        /// https://stackoverflow.com/questions/4189312/capturing-javascript-error-in-selenium
        /// </summary>
        /// <param name="timeout">The timeout.</param>
        public static void WaitJSAndJQuery(this IWebDriver Browser, TimeSpan timeout)
        {
            WebDriverWait wait = new WebDriverWait(Browser, timeout);

            // Wait for the javascript to complete
            wait.Until<bool>((driver) =>
            {
                return (bool)Browser.ExecuteScript("return document.readyState === 'complete'");
            });

            // wait for jQuery to be defined, then wait for the AJAX to be complete
            wait.Until<bool>((driver) =>
            {
                return (bool)Browser.ExecuteScript("return (window.jQuery != undefined) && (jQuery.active==0)");
            });

            // Thoughts: Look at the wait.until above this one. Am I doing the same exact thing "jQuery.active==0"?
            // Most likely, but for now I will leave it in. See the links I provided for background where it 
            // separates these two
            wait.Until<bool>((driver) =>
            {
                return (bool)Browser.ExecuteScript("return jQuery.active").Equals((Int64)0);
            });
        }

        /// <summary>
        /// Waits for all Javascript and JQuery requests to be completed
        /// For details, see https://www.swtestacademy.com/selenium-wait-javascript-angular-ajax/
        /// https://stackoverflow.com/questions/25062969/testing-angularjs-with-selenium
        /// https://stackoverflow.com/questions/4189312/capturing-javascript-error-in-selenium
        /// </summary>
        public static void WaitJSAndJQuery(this IWebDriver Browser)
        {
            WebDriverWait wait = new WebDriverWait(Browser, Timeout);

            // Wait for the javascript to complete
            wait.Until<bool>((driver) =>
            {
                return (bool)Browser.ExecuteScript("return document.readyState === 'complete'");
            });

            // wait for jQuery to be defined, then wait for the AJAX to be complete
            wait.Until<bool>((driver) =>
            {
                return (bool)Browser.ExecuteScript("return (window.jQuery != undefined) && (jQuery.active==0)");
            });

            // Thoughts: Look at the wait.until above this one. Am I doing the same exact thing "jQuery.active==0"?
            // Most likely, but for now I will leave it in. See the links I provided for background where it 
            // separates these two
            wait.Until<bool>((driver) =>
            {

                return (bool)Browser.ExecuteScript("return jQuery.active").Equals((Int64)0);
            });
        }

        /// <summary>
        /// Waits for all Javascript, JQuery and Angular requests to be completed
        /// For details, see https://www.swtestacademy.com/selenium-wait-javascript-angular-ajax/
        /// https://stackoverflow.com/questions/25062969/testing-angularjs-with-selenium
        /// https://stackoverflow.com/questions/4189312/capturing-javascript-error-in-selenium
        /// </summary>
        /// <param name="timeout">The timeout.</param>
        public static void WaitJSAndJQueryAndAngular(this IWebDriver Browser, TimeSpan timeout)
        {
            WebDriverWait wait = new WebDriverWait(Browser, timeout);

            // Wait for the javascript to complete
            wait.Until<bool>((driver) =>
            {
                return (bool)Browser.ExecuteScript("return document.readyState === 'complete'");
            });

            // Wait for Angular to be defined and for the injector in angular to be defined, and also
            // for the pending requests to be 0
            wait.Until<bool>((driver) =>
            {
                return (bool)Browser.ExecuteScript("return (window.angular !== undefined) && (angular.element(document).injector() !== undefined) && (angular.element(document).injector().get('$http').pendingRequests.length === 0)");
            });


            // wait for jQuery to be defined, then wait for the AJAX to be complete
            wait.Until<bool>((driver) =>
            {
                return (bool)Browser.ExecuteScript("return (window.jQuery != undefined) && (jQuery.active==0)");
            });

            // Thoughts: Look at the wait.until above this one. Am I doing the same exact thing "jQuery.active==0"?
            // Most likely, but for now I will leave it in. See the links I provided for background where it 
            // separates these two
            wait.Until<bool>((driver) =>
            {

                return (bool)Browser.ExecuteScript("return jQuery.active").Equals((Int64)0);
            });
        }

        /// <summary>
        /// Waits for all Javascript, JQuery and Angular requests to be completed
        /// For details, see https://www.swtestacademy.com/selenium-wait-javascript-angular-ajax/
        /// https://stackoverflow.com/questions/25062969/testing-angularjs-with-selenium
        /// https://stackoverflow.com/questions/4189312/capturing-javascript-error-in-selenium
        /// </summary>
        public static void WaitJSAndJQueryAndAngular(this IWebDriver Browser)
        {
            WebDriverWait wait = new WebDriverWait(Browser, Timeout);

            // Wait for the javascript to complete
            wait.Until<bool>((driver) =>
            {
                return (bool)Browser.ExecuteScript("return document.readyState === 'complete'");
            });

            // Wait for Angular to be defined and for the injector in angular to be defined, and also
            // for the pending requests to be 0
            wait.Until<bool>((driver) =>
            {
                return (bool)Browser.ExecuteScript("return (window.angular !== undefined) && (angular.element(document).injector() !== undefined) && (angular.element(document).injector().get('$http').pendingRequests.length === 0)");
            });


            // wait for jQuery to be defined, then wait for the AJAX to be complete
            wait.Until<bool>((driver) =>
            {
                return (bool)Browser.ExecuteScript("return (window.jQuery != undefined) && (jQuery.active==0)");
            });

            // Thoughts: Look at the wait.until above this one. Am I doing the same exact thing "jQuery.active==0"?
            // Most likely, but for now I will leave it in. See the links I provided for background where it 
            // separates these two
            wait.Until<bool>((driver) =>
            {

                return (bool)Browser.ExecuteScript("return jQuery.active").Equals((Int64)0);
            });
        }

        /// <summary>
        /// Waits for all Javascript, JQuery and Angular requests to be completed for NOR SAP application,
        /// which is developed using latest Angular version (ng-version="6.0.7")
        /// For details, see https://www.swtestacademy.com/selenium-wait-javascript-angular-ajax/
        /// </summary>
        /// <param name="timeout">The timeout.</param>
        public static void WaitJSAndAngularStable(this IWebDriver Browser, TimeSpan timeout)
        {
            WebDriverWait wait = new WebDriverWait(Browser, timeout);

            // Wait for the javascript to complete
            wait.Until<bool>((driver) =>
            {
                return (bool)Browser.ExecuteScript("return document.readyState === 'complete'");
            });

            // Wait for Angular to be defined and for the injector in angular to be defined, and also
            // for the pending requests to be 0
            wait.Until<bool>((driver) =>
            {
                return (bool)Browser.ExecuteScript("return window.getAllAngularTestabilities().findIndex(x=>!x.isStable()) === -1");
            });
        }

        /// <summary>
        /// Waits for all Angular (version 2 and maybe some other version) requests to be completed,
        /// For details, see https://www.swtestacademy.com/selenium-wait-javascript-angular-ajax/
        /// Also see the below...
        /// https://stackoverflow.com/questions/38111005/detecting-that-angular-2-is-done-running
        /// C# examples: https://stackoverflow.com/questions/25062969/testing-angularjs-with-selenium
        /// C# example: https://gist.github.com/npbenjohnson/2ca45e54bd656337caa3
        /// </summary>
        /// <param name="timeout">The timeout.</param>
        public static void WaitUntilAngularRequestsComplete(this IWebDriver Browser, TimeSpan timeout)
        {
            WebDriverWait wait = new WebDriverWait(Browser, timeout);

            // Wait for Angular to be defined and for the injector in angular to be defined, and also
            // for the pending requests to be 0
            wait.Until<bool>((driver) =>
            {
                return (bool)Browser.ExecuteScript("return window.getAllAngularTestabilities().findIndex(x=>!x.isStable()) === -1");
            });

        }

        #endregion WaitForCustom

        #region WaitWindow

        /// <summary>
        /// Waits for a popup to load before proceeding with the test. Specically, it waits for an element(s) on a popup to load
        /// </summary>
        /// <param name="by">Any elements on your popup, of type By. <see cref="CBDLearnerPageBys"/></param>
        /// <param name="browser"> The IWebDriver</param>
        /// <param name="ts"> The maximum amount of time you want to wait for the window to load. i.e. TimeSpan.FromSeconds(180)</param>
        public static void WaitForPopup(IWebDriver browser, TimeSpan ts, params By[] bys)
        {
            foreach (var by in bys)
            {
                browser.WaitForElement(by, ts, ElementCriteria.IsVisible, ElementCriteria.IsEnabled);
            }
        }

        #endregion WaitWindow



        /// <summary>
        /// Defines a Wait object that will wait on some criteria of an ISearchContext.
        /// This is preferable to the WebDriverWait, because there are many scenarios where
        /// we want to wait until we find a certain object within an ISearchContext, but we
        /// don't readily have access to the IWebDriver
        /// </summary>
        public class SearchContextWait : DefaultWait<ISearchContext>
        {
            /// <summary>
            /// Gets or sets the default polling interval to use when waiting for criteria.  The default is 500ms, which is the same
            /// as the WebDriverWait class.
            /// </summary>
            public static TimeSpan DefaultPollingInterval = TimeSpan.FromMilliseconds(500);

            /// <summary>
            /// Initializes a new instance of the <see cref="SearchContextWait"/> class.
            /// </summary>
            /// <param name="context">The context.</param>
            /// <param name="timeout">The timeout.</param>
            public SearchContextWait(ISearchContext context, TimeSpan timeout)
                : base(context, new SystemClock())
            {
                this.Timeout = timeout;
                this.PollingInterval = DefaultPollingInterval;
                // It's important to ignore the NotFoundException or calls to FindElement
                // will immediately halt the test when they throw the NotFoundException
                this.IgnoreExceptionTypes(typeof(NotFoundException));
            }
        }

        // WebDriverWait OR condition example

        // wait.Until(d => 
        //d.FindElements(Bys.Astro_FacilityPage.HomeTab_MedRecs_MedRecAbsForm_PleaseSelPhysSelElem).Count == 0
        //            || d.FindElement(Bys.Astro_FacilityPage.HomeTab_MedRecs_MedRecAbsForm_PleaseCorrectErrorsLbl).Displayed);
        //            WebDriverWait wait = new WebDriverWait(Browser, TimeSpan.FromSeconds(60));


    }



}
