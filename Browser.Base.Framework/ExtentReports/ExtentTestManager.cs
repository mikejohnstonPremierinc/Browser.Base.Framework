using AventStack.ExtentReports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

// See below link section titled "An example of a thread-safe manager for ExtentTests". Creating these separate classes was the
// only way I could produce these reports and not have the Environment section be duplicated when I executed tests in parallel
// https://www.extentreports.com/docs/versions/3/net/
// Random How To: https://www.lambdatest.com/blog/report-in-nunit/
namespace Browser.Core.Framework
{
    public class ExtentTestManager
    {
        private static ThreadLocal<ExtentTest> _test;
        private static ExtentReports _extent = ExtentManager.Instance;

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest GetTest()
        {
            return _test.Value;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest CreateTest(string name)
        {
            if (_test == null)
                _test = new ThreadLocal<ExtentTest>();

            var t = _extent.CreateTest(name);
            _test.Value = t;

            return t;
        }
    }
}