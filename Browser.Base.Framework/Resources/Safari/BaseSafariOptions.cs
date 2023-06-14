using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Safari;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Browser.Core.Framework.Resources
{
    /// <summary>
    /// Base SafariOptions for a new SafariDriver
    /// </summary>
    public class BaseSafariOptions : SafariOptions
    {
        /// <summary>
        /// Default constructor that sets some default options.
        /// </summary>
        public BaseSafariOptions()
            : base()
        {
            // MJ: Do I need these?
            AddAdditionalOption(CapabilityType.AcceptSslCertificates, true);
            AddAdditionalOption(CapabilityType.AcceptInsecureCertificates, true);
            AddAdditionalOption("cleanSession", true);
            AddAdditionalOption(CapabilityType.Platform, new Platform(PlatformType.Mac));

        }

    }
}
