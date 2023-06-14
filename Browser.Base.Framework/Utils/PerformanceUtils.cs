using System;
using System.IO;

namespace Browser.Core.Framework
{
    public class PerformanceUtils
    {
        #region Methods

        /// <summary>
        /// Stores the performance timing information into a CSV file. Do not refactor this to use StopWatch, as the
        /// StopWatch class has issues with parallel testing. We are just subtracting end date from start date
        /// </summary>
        /// <param name="testDesc">The description of the test</param>
        /// <param name="startDt">The date and time when the performance requirement started <see cref=" DateTime.Now"/></param>
        /// <param name="endDt">The date and time when the performance requirement ended <see cref=" DateTime.Now"/></param>
        public static void StoreResultsInCSV(string testDesc, DateTime startDt, DateTime endDt)
        {
            NetworkShareAccesser.Access();

            // Get the TestOutput directory, which is at the root of the solution
            var testOutputDirectory = SeleniumCoreSettings.TestOutputLocation;

            string filePath = FileUtils.CreateFile(string.Format("{0}\\PerformanceResults", testOutputDirectory), "PerformanceResults.csv");

            // Create the above file if it doesnt exist
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }

            TimeSpan ts = endDt.TimeOfDay.Subtract(startDt.TimeOfDay);
            double seconds = ts.TotalSeconds;


            string line = String.Format("{0},{1},{2},{3}", startDt.ToString(), endDt.ToString(), testDesc, seconds);

            // write to excel the variables that were passed to this method
            using (StreamWriter sw = File.AppendText(filePath))
            {
                sw.WriteLine(line);
            }
        }


        #endregion Methods
    }
}