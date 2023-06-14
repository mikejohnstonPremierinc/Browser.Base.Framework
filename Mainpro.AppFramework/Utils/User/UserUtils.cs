using Browser.Core.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using NUnit.Framework;

namespace Mainpro.AppFramework
{
    public static class UserUtils
    {
        #region properties

        public static string baseAPIUrl = AppSettings.Config["APIUrl"].ToString();
        public static string token = AppSettings.Config["APIToken"].ToString();

        public static TimeZoneInfo est = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
        public static DateTime currentDatetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, est);


        #region static users

        public static string Password = "test";

        #endregion static users



        #endregion properties

        #region methods

        ///// <summary>
        ///// Used inside TestBase for existing users. Populates the user info at runtime
        ///// </summary>
        ///// <param name="username"></param>
        ///// <param name="email"></param>
        ///// <returns></returns>
        //public static UserModel AddUserInfo(string username = null, string email = null)
        //{
        //    UserModel newUserModel = new UserModel();

        //    newUserModel.Username = username;
        //    newUserModel.EmailAddress = email;
        //    return newUserModel;
        //}

        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="userName">(Optional) If needed, you can specify a string that will be appended onto the end of the 
        /// randomly 
        /// generated username. If not needed, leave this null and the username will be generated for you</param>
        /// <param name="emailAddress">(Optional) If needed, you can specify an email of your choice. If not needed, leave
        /// this null and the username will be generated for you</param>
        /// <param name="firstName">(Optional) If needed, you can specify a first name of your choice. If not needed, leave 
        /// this null and the first name will be generated for you</param>
        /// <param name="lastName">(Optional) If needed, you can specify a last name of your choice. If not needed, leave 
        /// this null and the last name will be generated for you</param>
        /// <param name="effectiveDt">(Optional) Registers the user with this effective date. If you dont provide a date, 
        /// It will 
        /// register the user to July 01 of the current year - 1. You can customize the DateNow type as such 
        /// DateTime.Now.AddYears(-4)</param>
        public static UserModel CreateAndRegisterUser(string userName = null, string emailAddress = "", string
            firstName = null, string lastName = null, DateTime effectiveDt = default(DateTime),
            TestContext.TestAdapter currentTest = null)
        {
            UserModel newUserModel = BuildUserModel(userName, currentTest: currentTest, firstName: firstName, lastName: lastName);

            String fullAPIUrl = String.Format("{0}api/user", baseAPIUrl);
            string body = JsonConvert.SerializeObject(newUserModel);

            string resp = "";

            // We are using a try catch here for the following reason. Some of very first tests that gets run
            // during the build. For some reason, the CreateUser or RegisterUser API fails to complete and
            // throws a 500 error. So if the error happens, then we will just call the API again in the Catch
            // block and proceed
            for (int i = 0; i < 20; i++)
            {
                try
                {
                    resp = APIUtils.ExecuteAPI_Post(fullAPIUrl, body, token);
                    break;
                }
                catch
                {
                    // If we tried to create a user many times without success, then this will fail the test.
                    if (i == 19)
                    {
                        throw new Exception("The Create User API was tried 10 times with a 20 second pause in between each " +
                            "time and failed every time. This occurs " +
                            "sometimes when too many API calls are executed in parallel. If this continues to occur, " +
                            "DEV will have to tune the DB");
                    }
                }
                Thread.Sleep(20000);
            }

            dynamic data = JObject.Parse(resp);
            newUserModel.Guid = data.Id.ToString();

            RegisterUser_DefaultCycle(newUserModel.Username, effectiveDt);

            return newUserModel;
        }

        /// <summary>
        /// Builds the user information that then gets plugged into the API request's body
        /// </summary>
        /// <param name="userName">(Optional) If needed, you can specify a string that will be appended onto the end of the randomly 
        /// generated username. If not needed, leave this null and the username will be generated for you</param>
        /// <param name="emailAddress">(Optional) If needed, you can specify an email of your choice. If not needed, leave this null and the username will be generated for you</param>
        /// <param name="firstName">(Optional) If needed, you can specify a first name of your choice. If not needed, leave this null and the first name will be generated for you</param>
        /// <param name="lastName">(Optional) If needed, you can specify a last name of your choice. If not needed, leave this null and the last name will be generated for you</param>
        /// <returns></returns>
        private static UserModel BuildUserModel(string userName = "", string emailAddress = "", string firstName = null,
            string lastName = null, TestContext.TestAdapter currentTest = null)
        {
            string randomString = DataUtils.GetRandomString(3);
            DateTime dt = currentDatetime;//DateTime.Now;changed this for converting Date to EST
            Random rnd = new Random();

            string monthname = DateTimeFormatInfo.InvariantInfo.GetAbbreviatedMonthName(dt.Month);
            string dateString = String.Format("{0}-{1}-{2}_{3}-{4}", monthname, dt.Day, dt.Year.ToString().Substring(2),
                dt.Hour, dt.Minute);
            string randomUsername = "TestAuto" + randomString + "_" + dateString + "_" + currentTest.Name.ToString();

            // Set the email address, user name, first and last name
            userName = string.IsNullOrEmpty(userName) ? randomUsername : randomUsername + "_" + userName;
            emailAddress = string.IsNullOrEmpty(emailAddress) ? "cfpcuser@mailinator.com" : emailAddress;
            firstName = string.IsNullOrEmpty(firstName) ? "TestAuto" + randomString : firstName;
            lastName = string.IsNullOrEmpty(lastName) ? dateString : lastName;

            UserModel newUserModel = new UserModel();
            newUserModel.Address = "121 Lake Dr";
            newUserModel.Address2 = "Suite 100";
            newUserModel.City = "pittsburgh";
            newUserModel.CountryCode = "US";
            newUserModel.Degree = "MD";
            newUserModel.EmailAddress = emailAddress;
            newUserModel.FirstName = firstName;
            newUserModel.LastName = lastName;
            newUserModel.PostalCode = "12345";
            newUserModel.State = "AL";
            newUserModel.Username = userName;
            newUserModel.Password = Password;


            newUserModel.Fields = new Field[]
            {   new Field() { Name = "CFPC_Phone1", Value = "123-456-7890" },
                new Field() { Name = "CFPC_Category", Value = "P" },
                new Field() { Name = "CFPC_Enrollment_Date", Value = "1/30/2015" },
                new Field() { Name = "Phone_Number", Value = "123-456-7890" },
                new Field() { Name = "CFPC_Phone2", Value = "111-111-1111" },
                new Field() { Name = "CFPC_Salutation", Value = "Dr" },
                new Field() { Name = "CFPC_Sex", Value = "Male" },
                new Field() { Name = "CFPC_Mail_Language", Value = "E" },
                new Field() { Name = "CFPC_Province", Value = "Ontario" },
                new Field() { Name = "CFPC_Province_Description", Value = "Ontario" },
                new Field() { Name = "CFPC_Country_Code", Value = "USA" },
                new Field() { Name = "CFPC_Country_Dscription", Value = "USA" },
                new Field() { Name = "CFPC_Location_Code", Value = "ABC" },
                new Field() { Name = "CFPC_Name_Reference", Value = "XYZ" },
                new Field() { Name = "CFPC_Birthdate", Value = "1986-03-08" },
                new Field() { Name = "CFPC_Chapter", Value = "USA" },
                new Field() { Name = "CFPC_Chapter_Name", Value = "ABC" },
                new Field() { Name = "CFPC_Discontinued_Flag", Value = "0" },
                new Field() { Name = "CFPC_Discontinued_Reason", Value = "ABC" },
                new Field() { Name = "CFPC_Discontinued_Reason_Description", Value = "XYZ" },
                new Field() { Name = "CFPC_Discontinued_Date", Value = "1990-10-21" },
                new Field() { Name = "CFPC_Reinstatement_Date", Value = "1989-01-01" },
                new Field() { Name = "US_or_Foreign_Flag", Value = "1" },
                new Field() { Name = "CFPC_Licence_Number", Value = "1987110" },
                new Field() { Name = "CFPC_Subcategory", Value = "AB|CD|EF" },
                new Field() { Name = "Profession", Value = "PHY" },
                new Field() { Name = "PARTICIPANT_ID", Value = userName }};

            return newUserModel;
        }

        /// <summary>
        /// Registers the user to the default cycle
        /// </summary>
        /// <param name="userName">(Optional) If needed, you can specify a username of your choice. If not needed, leave this 
        /// null and the username will be generated for you</param>
        /// <param name="effectiveDt">(Optional) Registers the user with this effective date. If you dont provide a date, It will 
        /// register the user to July 01 of the current year - 1. You can customize the DateNow type as such 
        /// DateTime.Now.AddYears(-4)</param>
        private static void RegisterUser_DefaultCycle(string userName, DateTime effectiveDt = default(DateTime))
        {
            // If user didnt pass anything for effectiveDt, then make it current year - 1, July 1
            effectiveDt = effectiveDt == default(DateTime) ? new DateTime(currentDatetime.Year - 1, 7, 1) : effectiveDt;

            string fullAPIUrl = String.Format("{0}api/Activity/841ECFA4-4D5F-4A1F-9840-5E364E4B86AB/user", baseAPIUrl);
            string body = JsonConvert.SerializeObject(new
            {
                UserIdentifier = userName,
                MembershipType = "Individual",
                MembershipRole = "User",
                StartDate = effectiveDt.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                Category = "AM"
            });

            // We are using a try catch here for the following reason. Some of very first tests that gets run during the build. For 
            // some reason, the CreateUser or RegisterUser API fails to complete and throws a 500 error. So if 
            // the error happens, then we will just call the API again in the Catch block and proceed
            try
            {
                APIUtils.ExecuteAPI_Post(fullAPIUrl, body, token);
            }
            catch
            {
                APIUtils.ExecuteAPI_Post(fullAPIUrl, body, token);
            }
        }

        /// <summary>
        /// Adjusts the user to the tester-specified cycle. You must login first before calling this method due to bug 
        /// https://code.premierinc.com/issues/browse/MAINPROREW-838. UPDATE: After more investigation, this is occuring 
        /// inside more tests, AffiliateRemedialCycle_AMARCPactivity_NoMaxLimit, even after logging in. In this test, we 
        /// login, adjust the user into Affiliate, then adjust the user into a Remedial. The bug occured after adjusting the 
        /// user into Remedial. Note that we were already logged in at this point. To try to solve this, I am now adding 
        /// a navigation to the Dashboard page after executing the API, which will essentially refresh the page in hopes 
        /// that this solves the issue. Also am adding a Sleep because maybe it is a timing issue. Note that this occurs only 
        /// intermittently. See defect for more info. The team said this is not a likely scenario (making adjustments 
        /// before ever logging in) so it wont be fixed. If this continues to occur even after logging in and refreshing 
        /// the page and Sleeping, then this needs to be fixed by DEV, else we will continue to have failed tests.
        /// // UPDATE: 5/23: Today this occured inside ResidentCycle_AMARCPactivity_NoMaxLimit and 
        /// AMAPRA2YearCycleAdd200CreditsCertifiedtest and this is after having all 
        /// of the above workarounds in place. DEV will need to fix this if we want to have a solid automation
        /// NOTE: Be sure to login to Mainpro before calling this method, unless you are specifically testing something 
        /// that requires you to not login before, then set the isUserLoggedIn to False
        /// </summary>
        /// <param name="username"></param>
        /// <param name="adjustmentCode"><see cref="Const_Mainpro.AdjustmentCodes"/></param>
        /// <param name="effectiveDate">(Optional). If you pass a date, be sure that the adjustment code you pass 
        /// works with the EffectiveDate API field. i.e. The 'R' remedial adjustment can not be used 
        /// with a date but the 'rr' Remedial Reinstatement adjustment needs a date. Format = MM/DD/YYYY</param>
        /// <param name="LeaveEndDate">(Optional). If you pass a date, be sure that the adjustment code you pass 
        /// works with the LeaveEndDate API field. </param>
        /// <param name="LeaveStartDate">(Optional). If you pass a date, be sure that the adjustment code you pass 
        /// works with the LeaveStartDate API field.</param>
        /// <param name="isUserLoggedIn">Set to false is not logged in. This will refresh the page back to the Dashboard page 
        /// if you are logged in. This is neede due to bug https://code.premierinc.com/issues/browse/MAINPROREW-838.
        /// Default = true</param>
        public static dynamic AdjustUserCycle(IWebDriver browser, string username,
            Const_Mainpro.AdjustmentCodes adjustmentCode,
            DateTime effectiveDate = default(DateTime), DateTime LeaveStartDate = default(DateTime),
            DateTime LeaveEndDate = default(DateTime), bool isUserLoggedIn = true)
        {
            string resp = string.Empty;

            // Build the API body
            AdjustmentModel adjustmentModel = new AdjustmentModel();
            adjustmentModel.TransactionId = Guid.NewGuid().ToString();
            adjustmentModel.RecognitionId = "A9C1A148-7CDB-487A-A689-9863B58E6114";
            adjustmentModel.AdjustmentCode = adjustmentCode.GetDescription();

            // If the tester passed a date, then include the date body parameters
            if (effectiveDate != default(DateTime))
            {
                string dateString = String.Format("{0}/{1}/{2}", effectiveDate.Month, effectiveDate.Day, effectiveDate.Year);
                adjustmentModel.Fields.Add(new AdjustmentFields() { Name = "EffectiveDate", Value = dateString });
            }
            if (LeaveStartDate != default(DateTime))
            {
                string dateString = String.Format("{0}/{1}/{2}", LeaveStartDate.Month, LeaveStartDate.Day, LeaveStartDate.Year);
                adjustmentModel.Fields.Add(new AdjustmentFields() { Name = "LeaveStartDate", Value = dateString });

            }
            if (LeaveEndDate != default(DateTime))
            {
                string dateString = String.Format("{0}/{1}/{2}", LeaveEndDate.Month, LeaveEndDate.Day, LeaveEndDate.Year);
                adjustmentModel.Fields.Add(new AdjustmentFields() { Name = "LeaveEndDate", Value = dateString });
            }

            string body = JsonConvert.SerializeObject(adjustmentModel);
            string fullAPIUrl = string.Format("{0}/api/user/{1}/activity/841ECFA4-4D5F-4A1F-9840-5E364E4B86AB/adjustment",
                baseAPIUrl, username);

            // We are using a try catch here for the following reason. Some of very first tests that gets run during the build. For 
            // some reason, the CreateUser or RegisterUser API fails to complete and throws a 500 error. So if 
            // the error happens, then we will just call the API again in the Catch block and proceed
            try
            {
                resp = APIUtils.ExecuteAPI_Post(fullAPIUrl, body, token);
            }
            catch
            {
                resp = APIUtils.ExecuteAPI_Post(fullAPIUrl, body, token);
            }

            if (isUserLoggedIn)
            {
                // Navigate to the Dashboard page, Sleep for 3 seconds, then Navigate to Dashboard page. We 
                // defect https://code.premierinc.com/issues/browse/MAINPROREW-838. See Description of this method for more info
                Thread.Sleep(3000);
                return Navigation.GoToDashboardPage(browser);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// adds a new CAC to the user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="designationCode"><see cref="Const_Mainpro.AdjustmentCodes"/></param>
        /// <param name="enrollmentDate">Enrollment date for CAC program. <see cref="DateTime.Now"></see>/></param>
        /// Default = DateTime.Now</param>
        public static void AddUserToCAC(string username, Const_Mainpro.DesignationCode designationCode,
            DateTime enrollmentDate = default(DateTime))
        {
            if (enrollmentDate == default(DateTime))
            {
                enrollmentDate = currentDatetime;                //DateTime.Now;changed this for converting Date to EST
            }

            string dateString = String.Format("{0}/{1}/{2}", enrollmentDate.Month, enrollmentDate.Day, enrollmentDate.Year);

            String fullAPIUrl = String.Format("{0}/api/apps/cfpc/cac/{1}", baseAPIUrl, username);
            string body = String.Format("{{CACDesignationCode:\"{0}\",EnrollmentDate:\"{1}\"}}", designationCode.GetDescription(),
                dateString);
            string resp = APIUtils.ExecuteAPI_Post(fullAPIUrl, body, token);
        }

        /// <summary>
        /// Assigns a existing volume to a user. The volume must already be created. 
        /// <see cref="UserUtils.CreateVolumesAndAddQuestions"/>
        /// </summary>
        /// <param name="username"></param>
        /// <param name="volumeTitle"></param>
        public static string AssignVolumeToUser(string username, string volumeTitle)
        {
            // Build the API body
            VolumeModel newVolumeModel = new VolumeModel();
            newVolumeModel.Volumes = new Volumes[] {
                    new Volumes()
                    {
                        CFPCId = "1425848",
                        Title = volumeTitle,
                        PublishedDate =  currentDatetime.AddDays(-1).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
                    },

                };

            string body = JsonConvert.SerializeObject(newVolumeModel);
            string fullAPIUrl = string.Format("{0}/api/apps/cfpc/participant/{1}/volume", baseAPIUrl, username);
            APIUtils.ExecuteAPI_Post(fullAPIUrl, body, token);

            return volumeTitle;
        }

        /// <summary>
        /// Creates a volume and assigns questions to it. IMPORTANT: volumeTitle must have the word "Volume" in it with 
        /// a capital V, else the application fails to assign questions to the volume
        /// </summary>
        /// <param name="volumeTitle">(Optional). The title of the volume you want to create. The title of the volume can
        /// not already be in the system, it has to be a new name. Also, it must have the word "Volume" in it with a 
        /// capital V, else the application fails to assign questions to the volume. Default = 1 randomly titled volume</param>
        /// <param name="questionsToAdd">(Optional). This must be a minimum of 2 questions. 
        /// Default = 2 randomly titled questions</param>
        public static List<string> CreateVolumesAndAddQuestions(string volumeTitle = null,
            List<string> questionsToAdd = null)
        {
            // Create a random volume title if the user did not specify one
            volumeTitle = string.IsNullOrEmpty(volumeTitle) ? "TestAutoVolume_" + DataUtils.GetRandomString(3) : volumeTitle;

            // If questionsToAdd was not passed, instanstiate a new instance, so we can base conditions on this instance.
            questionsToAdd = questionsToAdd ?? new List<string>();

            // If the user passed only 1 question
            if (questionsToAdd.Count == 1)
            {
                throw new Exception("You must specify at least 2 questions");
            }

            // Create 2 random questions if the user did not specify one
            if (questionsToAdd.Count == 0)
            {
                for (int i = 0; i < 2; i++)
                {
                    string newQuestion = volumeTitle + "_Question_" + DataUtils.GetRandomString(3);
                    questionsToAdd.Add(newQuestion);
                }
            }

            // Build the API body
            QuestionModel newQuestionModel = new QuestionModel();
            newQuestionModel.Volume = volumeTitle;
            newQuestionModel.Language = "En";

            var questions = new List<Questions>();
            for (int i = 0; i < questionsToAdd.Count; i++)
            {
                var question = new Questions();
                question.QuestionNumber = (i + 1).ToString();
                question.Question = questionsToAdd[i];
                questions.Add(question);
            }
            newQuestionModel.Questions = questions.ToArray();

            string body = JsonConvert.SerializeObject(newQuestionModel);
            string fullAPIUrl = string.Format("{0}/api/apps/cfpc/volume/questions", baseAPIUrl);
            APIUtils.ExecuteAPI_Post(fullAPIUrl, body, token);

            return questionsToAdd;
        }




        [Serializable]
        public class AdjustmentModel
        {
            public AdjustmentModel()
            {
                Fields = new List<AdjustmentFields>() { };
                //Fields = new AdjustmentFields[] { };
            }

            public string TransactionId { get; set; }
            public string RecognitionId { get; set; }
            public string AdjustmentCode { get; set; }
            public List<AdjustmentFields> Fields { get; set; }
            //public AdjustmentFields[] Fields { get; set; }
        }

        /// <summary>
        /// XtensibleInfo item. Used when we can specify multiple different Values for the Name/Key.
        /// </summary>
        [Serializable]
        public class AdjustmentFields
        {
            public string Name { get; set; }
            public string Value { get; set; }
        }


        [Serializable]
        public class VolumeModel
        {
            public VolumeModel()
            {
                Volumes = new Volumes[] { };
            }

            public Volumes[] Volumes { get; set; }
        }

        /// <summary>
        /// XtensibleInfo item. Used when we can specify multiple different Values for the Name/Key.
        /// </summary>
        [Serializable]
        public class Volumes
        {
            public string CFPCId { get; set; }
            public string Title { get; set; }
            public string PublishedDate { get; set; }
        }

        [Serializable]
        public class QuestionModel
        {
            public QuestionModel()
            {
                Questions = new Questions[] { };
            }

            public string Volume { get; set; }
            public string Language { get; set; }
            public Questions[] Questions { get; set; }
        }

        /// <summary>
        /// XtensibleInfo item. Used when we can specify multiple different Values for the Name/Key.
        /// </summary>
        [Serializable]
        public class Questions
        {
            public string QuestionNumber { get; set; }
            public string Question { get; set; }
        }


        #endregion methods
    }
}

