//using OpenQA.Selenium;
//using OpenQA.Selenium.Interactions;
//using OpenQA.Selenium.Support.UI;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Reflection;
//using System.Threading;

//namespace Browser.Core.Framework
//{
//    /// <summary>
//    /// A utility class for various operations on our LMS platform applications
//    /// </summary>
//    public static class EnvironmentUtils
//    {
//        /// <summary>
//        /// Gets the environment name, i.e. "cmeqaf" or "
//        /// </summary>
//        /// <param name="Page">The page to refresh</param>
//        /// <param name="creditLabelBy">the label which stores the amount of credits that you are waiting to be refreshed</param>
//        /// <param name="amountOfCredits">The amount of credits that will show when the windows service is complete</param>
//        public static void WaitForCreditsToBeApplied(Page Page, By creditLabelBy, string amountOfCredits)
//        {
//            for (int i = 1; i < 150; i++)
//            {
//                if (i == 40)
//                {
//                    throw new Exception("Failing this test because we refreshed 40 times at this point waiting for credits to be applied, " +
//                        "and that is too long. See the Note inside this test method for more information. 1 of 2 things caused this failure " +
//                        "and will need to be fixed. Either you hard-coded the wrong credit number to wait for, or put the user in the wrong cycle, " +
//                        " which would result in this method never finding your hard-coded credit number, or the Windows Service which queues a bunch " +
//                        "of records is overloaded and is taking forever. So either DEV will need to fix the refresh process again if it starts " +
//                        " taking too long, or you need to update your code");
//                }

//                Thread.Sleep(3000);
//                Page.RefreshPage(true);
//                try
//                {
//                    Page.WaitForElement(creditLabelBy, TimeSpan.FromMilliseconds(0100), ElementCriteria.AttributeValue("innerText", amountOfCredits));
//                    break;
//                }
//                catch
//                {
//                    continue;
//                }
//            }
//            // Adding one more refresh just in case. I noticed that even when we wait for 1 label to get these credits, other labels may
//            // still not be updated. So I am adding 1 more refresh maybe because the other labels need it. Monitor going forward
//            Page.RefreshPage(true);
//        }


//    }
//}

