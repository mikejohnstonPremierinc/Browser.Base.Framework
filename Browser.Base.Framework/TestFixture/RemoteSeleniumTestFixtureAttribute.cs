﻿using NUnit.Framework;
using System;

namespace Browser.Core.Framework
{
    // TODO: I can subclass this attribute if I decide to implement BrowserStack or SauceLabs?

    /// <summary>
    /// An NUnit test that intends to remotely connect to a Selenium Grid.
    /// </summary>
    public class RemoteSeleniumTestFixtureAttribute : TestFixtureAttribute
    {
        /// <summary>
        /// Constructor of a remote test.
        /// </summary>
        /// <param name="browserName">The name of the browser to test from <see cref="BrowserNames"/></param>
        /// <param name="version">The version of the browser to test.</param>
        /// <param name="platform">The platform on which the browser should run.</param>
        /// <param name="hubUri">The uri of the Selenium Hub.</param>
        /// <param name="extrasUri">The uri for Selenium Extras.</param>
        public RemoteSeleniumTestFixtureAttribute(
            String browserName, string emulationDevice = "", String version = "", String platform = "", 
            string hubUri = "", string extrasUri = "")
            : base(browserName, emulationDevice, version , platform, hubUri, extrasUri)
        {
            Category = string.Format("Remote.{0}", browserName);

            if (!string.IsNullOrEmpty(version))
            {
                Category += string.Format(".{0}", version);
            }

            if (!string.IsNullOrEmpty(platform))
            {
                Category += string.Format(".{0}", platform);
            }
        }
    }
}
