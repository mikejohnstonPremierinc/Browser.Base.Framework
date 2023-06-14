using NUnit.Framework;
using AventStack.ExtentReports.Reporter;
//using AventStack.ExtentReports.Reporter.Configuration;
using AventStack.ExtentReports;
using NUnit.Framework.Internal;
using Browser.Core.Framework;
using System.IO;
using System;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;
using WebDriverManager;

/// <summary>
/// This class (marked with SetupFixture) was originally intended to be a way to perform a line of code before ALL 
/// tests executing in parallel. I extended BrowserTest to this class, then executed the code and the code was performed,
/// but it was performed per fixture, not per global assembly. The "one time per assembly" that SetUpFixture allows for 
/// doesnt work when placing the code within browser.base.framework because whenever QA Engineers execute their tests, 
/// that execution is started at their .UITest assembly, not browser.base.framework assembly. One time global (assembly-wide) 
/// execution can only be accomplished by placing SetUpFixture inside their .UITest project levels. We either place it inside a 
/// namespace of .UITest and then that code will be performed once per namespace, or we place the code without a namespace and it will
/// be performed once across namespaces.
/// Also, SetupFixture was never intended to be used as a base class as noted by Charlie Poole here: https://github.com/nunit/nunit/issues/3937
/// https://stackoverflow.com/questions/3619735/nunit-global-initialization-bad-idea/52874663
/// https://docs.nunit.org/articles/nunit/writing-tests/attributes/setupfixture.html
/// https://stackoverflow.com/questions/48557488/how-to-perform-global-setup-teardown-in-xunit-and-run-tests-in-parallel
/// https://www.extentreports.com/docs/versions/3/net/#extent-nunit-setupfixture
/// https://atata.io/tutorials/reporting-to-extentreports/
/// https://stackoverflow.com/questions/60546805/extend-report-in-c-sharp-for-multiple-test-classes-is-not-working
/// </summary>

//namespace Browser.Core.Framework
//{
[SetUpFixture]
    public abstract class Test
    {
        /// <summary>
        /// Perform setup logic that should be performed once per global test execution. i.e. Before ALL parallel tests executed that are 
        /// within the same assembly. https://docs.nunit.org/articles/nunit/writing-tests/attributes/setupfixture.html
        /// </summary>
        [OneTimeSetUp]
        public void BeforeAllTests()
        {
            TestContext.Progress.WriteLine("blah3");
        }
    }
//}