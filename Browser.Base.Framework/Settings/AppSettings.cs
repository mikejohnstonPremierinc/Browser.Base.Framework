using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Browser.Core.Framework
{
    /// <summary>
    /// Wrapper for interacting with the App.config (or appsettings.json in .NET Core) file
    /// TODO: Should this live elsewhere?  Is there a better way to do this? See commented code inside individual .UITest level TestBase
    /// classes for another way to do it
    /// Private for now so it forces us to make this decision before using it elsewhere :)
    /// </summary>
    public static class AppSettings
    {
        public static IConfiguration Config
        {
            get { return InitConfiguration(); }
        }

        /// <summary>
        /// Gets the value from the app.config or web.config (or appsettings.json in .NET Core) file or returns a default value if none exists.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static T GetEnumOrDefault<T>(string key, T defaultValue)
            where T : struct
        {
            return GetValueOrDefault<T>(key, defaultValue, p => (T)Enum.Parse(typeof(T), p));
        }

        /// <summary>
        /// Gets the value from the app.config or web.config (or appsettings.json in .NET Core) file or returns a default value if none exists.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="ensureTrailing">(Optional) If specified, ensures that the returned string "EndsWith" this value</param>
        /// <returns></returns>
        public static string GetStringOrDefault(string key, string defaultValue, string ensureTrailing = null)
        {
            var value = GetValueOrDefault<string>(key, defaultValue, p => p);
            if (!string.IsNullOrEmpty(ensureTrailing) && !value.EndsWith(ensureTrailing))
                value += ensureTrailing;

            return value;
        }

        /// <summary>
        /// Gets the value from the app.config or web.config (or appsettings.json in .NET Core) file or returns a default value if none exists.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key in the AppSettings section of the config file.</param>
        /// <param name="defaultValue">The default value to return if the config value doesn't exist.</param>
        /// <param name="converter">The converter.</param>
        /// <returns></returns>
        public static T GetValueOrDefault<T>(string key, T defaultValue, Func<string, T> converter = null)
        {
            T value = defaultValue;
            if (AppSettings.Config[key] != null)
            {
                value = converter(AppSettings.Config[key]);
            }

            return value;
        }

        /// <summary>
        /// Returns any settings within appsettings.json and appsettings.'env'.json. For example, from your test, you can call 
        /// Appsettings.Config["url"] to get the url of the environment you are running the test from. A couple of 
        /// prerequisites need to be completed before this will work in your project. 
        /// 1. Add a file called appsettings.json in your project directory. Inside of it, add any global (non-environment-specific) 
        /// key>value pairs. For example, add a downloaddirectory. Then, add 1 (or multiple) environment-specific appsettings 
        /// files so that you can specify environment-specific URLs, etc. For example, add a file called 
        /// appsettings.prod.url, and inside of it, add your production specific URL or connection string, etc., as 
        /// key>value pairs. 
        /// 2. For the above files, do not change any Properties
        /// 3. Click on Build>Configuration Manager from the menu. IF you dont see an environment that you 
        /// will be running tests on in 'Active Solution Configuration' drop down, then add it. To add it, click on the drop down,
        /// click New, then name your environment ('Prod' for production, in this example). Choose 'Debug' in the Copy Settings From
        /// drop down. Click Ok and make sure 'Prod' is chosen in the Select Element under the Configuration column your project and 
        /// ALL other projecs, then click Close. If 
        /// successful, 'Prod' will appear in the Build Configuration drop down (The drop down to the left of the "Any CPU" 
        /// drop down, which is underneath the menu bar of the main VS window)
        /// 4. Right click on your project and click "Edit Project File". Inside this text editor, paste the below XML.
        /// That XML will tell Visual Studio to copy the global appsettings.json file, as well as ONLY the 
        /// appsettings.'environment'.json for the environment that is currently selected in the Build Configuration drop down
        /// (the drop down next to Any CPU). After selected, perform a build, and now your build output folder 
        /// (projecfolder/Bin/Prod...) will 
        /// have the correct json files (1 global json, and 1 environment json). The code below will return all of the key>value
        /// pairs inside those 2 files 
        ///   <ItemGroup>
        /// <None Update = "appsettings.uat.json" >
        ///  < CopyToOutputDirectory Condition=" '$(Configuration)'=='UAT' ">PreserveNewest</CopyToOutputDirectory>
        /// </None>
        /// <None Update = "appsettings.prod.json" >
        /// < CopyToOutputDirectory Condition=" '$(Configuration)'=='Production' ">PreserveNewest</CopyToOutputDirectory>
        ///  </None>
        ///  <None Update = "appsettings.qa.json" >
        ///    < CopyToOutputDirectory Condition=" '$(Configuration)'=='QA' ">PreserveNewest</CopyToOutputDirectory>
        ///  </None>
        ///  <None Update = "appsettings.json" >
        ///    < CopyToOutputDirectory > PreserveNewest </ CopyToOutputDirectory >
        ///  </ None >
        ///  </ ItemGroup >
        /// 5. After the above steps, you can select Prod in the Build drop down if you want to run against production,
        /// or QA, etc. Which means you will be able to get key/value pairs from both global and environment-specific json files
        /// Troubleshooting: If you receive an error message when executing this method, read the above carefully. If you are sure
        /// you performed all steps accurately, then make sure that the correct environment is chosen in the Select Element 
        /// under the Configuration column of the Configuration Manager window for YOUR project. If not, choose it
        /// TODO: Originally, I tried to implement through using the Profile dropdown  (The dropdown to the right of ANY CPU)
        /// and environment variables. This is how it is done in non Unit test projects for NET CORE. But Unit NET CORE projects 
        /// do not have this ability (VS does not pass the current launch profile to the test runner, so there is no way to
        /// set environment variables via launchSettings.json as of today). If/When support is added for that, we should switch to 
        /// doing it that way, which is the new proper way to do things in NET CORE. For now, the Build Configuration is similar
        /// to how we did it in NET FRAMEWORK
        /// Further reading: https://stackoverflow.com/questions/58738822/how-do-i-get-the-current-environment-qa-prod-dev-name-in-net-core-c
        /// https://github.com/aspnet/Tooling/issues/1089
        /// </summary>
        /// <returns></returns>
        public static IConfiguration InitConfiguration()
        {
            // Find all configuration-specific settings files in output folder
            var settingsFiles = Directory.GetFiles(Directory.GetCurrentDirectory(), "appsettings.*.json");

            // Throw error if not found
            if (settingsFiles.Length == 0) throw new Exception("Expected to have exactly one build configuration-specfic " +
                "settings file, inside " + Directory.GetCurrentDirectory() + ", but found none. Please see this method " +
                "description for the steps you should perform that results in copying the settings file to the output " +
                "directory. After reading, check your json file(s) property 'Copy To Output Directory'. Make sure you " +
                "did not explicitly set this value to something. The XML inside the Project properties file conditionally " +
                "copies these files, so we dont want to explicitly tell it to copy or not copy unconditionally. If you did " +
                "set this property, then there will be an option to Revert it back. Select that option and rebuild. If " +
                "instead you did not edit this file, be sure you have chosen the correct select item inside the build " +
                "configuration drop down at the top right corner of VS. The item that should be selected is the environment " +
                "that you want to execute your tests on. Also make sure that you have the appsettings.<environmentname>.json " +
                "file created in your root project directory");

            // Throw error if more than 1 found
            if (settingsFiles.Length != 1) throw new Exception($"Expect to have exactly one configuration-specfic settings file, but " +
                $"found {string.Join(", ", settingsFiles)}." + "Please see this method description for the steps " +
                 "you should perform that results in copying the settings file to the output directory. After reading, check your " +
                 "json file(s) property 'Copy To Output Directory'. Make sure you did not explicitly set this value to something. The " +
                 "XML inside the Project properties file conditionally copies these files, so we dont want to explicitly tell it " +
                 "to copy or not copy unconditionally). If you did set this property, then there will be an option to Revert it back. " +
                 "Select that option and rebuild. If you checked that and all looks good, then someone may have manually added " +
                 "a json file to the output directory, in which case Visual Studio would not clean/delete this file after you perform " +
                 "a build. If this is the case, manually delete this file");

            var settingsFile = settingsFiles.First();

            var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile(settingsFile)
            .AddEnvironmentVariables();

            var configuration = builder.Build();

            return configuration;
        }

        /// <summary>
        /// Gets the value from the variable inside appsettings.json or appsettings.environment.json
        /// </summary>
        /// <param name="variableName"></param>
        /// <returns></returns>
        internal static string GetConfigVariableValue(string variableName)
        {
            return string.IsNullOrEmpty(AppSettings.Config[variableName]) ? throw new Exception("Your" +
                "global appsettings.json or environment appsettings.environment.json file does not have " +
                "the '" + variableName + "' variable. Please add this variable if you want to call it") :
                AppSettings.Config[variableName];
        }
    }
}