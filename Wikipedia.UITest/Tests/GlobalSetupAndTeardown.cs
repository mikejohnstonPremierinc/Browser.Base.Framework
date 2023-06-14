using NUnit.Framework;
using NUnit.Framework.Internal;
using Browser.Core.Framework;
using Wikipedia.AppFramework;

namespace Wikipedia.UITest
{
    // For further reading about SetUpFixture, see the Test.cs class within the Browser.Base.Framework project
    [SetUpFixture]
    public class GlobalSetupAndTeardown
    {

        /// <summary>
        /// Perform setup logic that should be performed once per global execution. i.e. Before ALL parallel tests that have been 
        /// executed that are contained within the same namespace. If you have multiple namespaces and you want this logic to be 
        /// performed once per project (not per namespace), then remove the namespace within this class
        /// https://docs.nunit.org/articles/nunit/writing-tests/attributes/setupfixture.html
        /// </summary>
        [Test]
        [OneTimeSetUp]        
        public void BeforeAllTests()
        {
            // 2023/06/08: The only way we can use BrowserTest within SetUpFixture is if we remove the 'abstract' designation
            // within the BrowserTest class file declaration. That class has been designated as abstract since the inception
            // way back in 2017, so I am not sure if removing it will cause any issues or not. I am now going to remove it
            // and then ask the teams to report any new unexpected issues to me. I did testing myself after removing it and
            // I did not see any issues. I think initially it was declared abstract because there may be some potential
            // Abstract 'methods' that would be created in that class, but it has been many years and no abstract methods 
            // were ever created
            BrowserTest browserTest = new BrowserTest(BrowserNames.Chrome, "");
            browserTest.BeforeFixture();
            browserTest.BeforeTest();
            Navigation.GoToHomePage(browserTest.Browser);
            browserTest.AfterTest();
            browserTest.AfterFixture();

            TestContext.Progress.WriteLine("When executing more than 1 test in parallel, this BEGIN TEST text should appear only once in the " +
                "Output window->Tests section");
        }

        /// <summary>
        /// Perform teardown logic that should be performed once per global execution. i.e. After ALL parallel tests have been 
        /// executed that are contained within the same namespace. If you have multiple namespaces and you want this logic to be 
        /// performed once per project (not per namespace), then remove the namespace within this class
        /// https://docs.nunit.org/articles/nunit/writing-tests/attributes/setupfixture.html
        /// </summary>
        [OneTimeTearDown]
        public void AfterAllTests()
        {
            TestContext.Progress.WriteLine("When executing more than 1 test in parallel, this END TEST text should appear only once in the " +
                "Output window->Tests section");
        }
    }
}