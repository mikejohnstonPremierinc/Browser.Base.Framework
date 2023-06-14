using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
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
    /// A utility class for various operations on our LMS platform applications
    /// </summary>
    public static class AppUtils
    {
        /// <summary>
        /// Checks to see if your application uses JQuery
        /// </summary>
        /// <param name="timeout">The timeout</param>
        public static bool ApplicationHasJQuery(IWebDriver Browser, TimeSpan timeout)
        {
            bool JQueryExists = false;

            if (true)
            {
                JQueryExists = (bool)Browser.ExecuteScript("return (window.jQuery != undefined) && (jQuery.active==0)");
            }

            return JQueryExists;
        }

        ///// <summary>
        ///// If you add an activity which gives you credits, this method can be called to wait for a user-specified label on your application
        ///// to get updated with those credits after that activity was added. Once an activity is submitted, a record gets put into a windows service
        ///// queue, and then waits for that service to push the activity through. Because of this, we need to wait in our code. Note that there
        ///// is not a database flag to check, so we have to just randomly refresh every couple of seconds. Right now, we will refresh every 4 seconds,
        ///// 40 times. If your application ever takes longer than that, then see the below note.
        ///// NOTE: For credits to be applied in our application, a Windows Service needs executed. Sometimes this windows service takes an
        ///// unacceptable amount of time because a certain folder on a DevOps server gets filled up with 0KB cache files, and/or the windows
        ///// service has a huge amount of credit requests already inside it. Manik, Kiran and DevOps figured this out, and then set a daily job to 
        ///// clear this folder out. They also stopped some recognition services so that the queue doesnt get filled up every morning. If this ever
        ///// creeps up again, and the 40 refreshes is not long enough, we will have to notify DEV and DevOps again.
        ///// </summary>
        ///// <param name="Page">The page to refresh</param>
        ///// <param name="creditLabelBy">the label which stores the amount of credits that you are waiting to be refreshed</param>
        ///// <param name="amountOfCredits">The amount of credits that will show when the windows service is complete</param>
        //public static void WaitForCreditsToBeApplied(Page Page, By creditLabelBy, string amountOfCredits)
        //{
        //    for (int i = 1; i < 100; i++)
        //    {
        //        if (i == 40)
        //        {
        //            throw new Exception("Failing this test because we refreshed 40 times at this point waiting for credits to be applied, " +
        //                "and that is too long. See the Note inside this test method for more information. 1 of 2 things caused this failure " +
        //                "and will need to be fixed. Either you hard-coded the wrong credit number to wait for, or put the user in the wrong cycle, " +
        //                " which would result in this method never finding your hard-coded credit number, or the Windows Service which queues a bunch " +
        //                "of records is overloaded and is taking forever. So either DEV will need to fix the refresh process again if it starts " +
        //                " taking too long, or you need to update your code");
        //        }

        //        Thread.Sleep(3000);
        //        Page.RefreshPage(true);
        //        try
        //        {
        //            Page.WaitForElement(creditLabelBy, TimeSpan.FromMilliseconds(0100), ElementCriteria.AttributeValue("innerText", amountOfCredits));
        //            break;
        //        }
        //        catch
        //        {
        //            continue;
        //        }
        //    }
        //    // Adding one more refresh just in case. I noticed that even when we wait for 1 label to get these credits, other labels may
        //    // still not be updated. So I am adding 1 more refresh maybe because the other labels need it. Monitor going forward
        //    Page.RefreshPage(true);
        //}


    }
}

