using Bogus;
using Bogus.DataSets;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using static Bogus.DataSets.Name;
using static Bogus.Person;

namespace Browser.Core.Framework
{
    public static class DataUtils
    {
        #region Properties

        private static Random random = new Random();

        #endregion Properties

        #region Person

        /// <summary>
        /// Returns an object containing a random person, with details such as first name, address, birth date, etc.
        /// The address will be a real-looking address but it will not be an exact real-world address
        /// </summary>
        /// <returns></returns>
        public static Bogus.Person GetPerson()
        {
            var faker = new Faker(locale: "en_US");
            var newPerson = faker.Person;
            return newPerson;

            //string gender = faker.Person.Gender.ToString();
            //string company = faker.Person.Company.ToString();
            //string firstName = faker.Person.FirstName;
            //string lastName = faker.Person.LastName;
            //DateTime dateOfBirth = faker.Person.DateOfBirth;
            //CardAddress address = faker.Person.Address;
            //string city = faker.Person.Address.City;
            //string address2 = faker.Person.Address.State;
            //string zipCode = faker.Person.Address.ZipCode;
            //return new Person(firstName, lastName, dateOfBirth, address);
        }

        /// <summary>
        /// Returns the full name of the state based on the abbreviation
        /// </summary>
        /// <param name="state">The abbreviation of the state</param>
        /// <returns></returns>
        public static string ConvertStateAbbreviationToName(string state)
        {
            switch (state)
            {
                case "AL":
                    return "Alabama";
                case "AK":
                    return "Alaska";
                case "AR":
                    return "Arkansas";
                case "AZ":
                    return "Arizona";
                case "CA":
                    return "California";
                case "CO":
                    return "Colorado";
                case "CT":
                    return "Connecticut";
                case "DE":
                    return "Delaware";
                case "FL":
                    return "Florida";
                case "GA":
                    return "Georgia";
                case "HI":
                    return "Hawaii";
                case "ID":
                    return "Idaho";
                case "IL":
                    return "Illinois";
                case "IN":
                    return "Indiana";
                case "IA":
                    return "Iowa";
                case "KS":
                    return "Kansas";
                case "KY":
                    return "Kentucky";
                case "LA":
                    return "Louisiana";
                case "ME":
                    return "Maine";
                case "MD":
                    return "Maryland";
                case "MA":
                    return "Massachusetts";
                case "MI":
                    return "Michigan";
                case "MN":
                    return "Minnesota";
                case "MS":
                    return "Mississippi";
                case "MO":
                    return "Missouri";
                case "MT":
                    return "Montana";
                case "NE":
                    return "Nebraska";
                case "NV":
                    return "Nevada";
                case "NH":
                    return "New Hampshire";
                case "NJ":
                    return "New Jersey";
                case "NM":
                    return "New Mexico";
                case "NY":
                    return "New York";
                case "NC":
                    return "North Carolina";
                case "ND":
                    return "North Dakota";
                case "OH":
                    return "Ohio";
                case "OK":
                    return "Oklahoma";
                case "OR":
                    return "Oregon";
                case "PA":
                    return "Pennsylvania";
                case "RI":
                    return "Rhode Island";
                case "SC":
                    return "South Carolina";
                case "SD":
                    return "South Dakota";
                case "TN":
                    return "Tennessee";
                case "TX":
                    return "Texas";
                case "UT":
                    return "Utah";
                case "VT":
                    return "Vermont";
                case "VA":
                    return "Virginia";
                case "WA":
                    return "Washington";
                case "WV":
                    return "West Virginia";
                case "WI":
                    return "Wisconsin";
                case "WY":
                    return "Wyoming";
                default:
                    return state;
            }
        }

        #endregion Person

        #region Dates

        /// <summary>
        /// Returns 3 key-value pairs to use with our Date controLifetimeSupport_Sandbox. 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetDateForCalendarElem(DateTime date)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            dictionary.Add("year", date.ToString("yy"));
            dictionary.Add("month", date.ToString("MMMM"));
            dictionary.Add("day", date.Day.ToString("00"));

            return dictionary;
        }

        public static Dictionary<string, string> GetDateForNORCalendarElem(DateTime date)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            dictionary.Add("year", date.ToString("yyyy"));
            dictionary.Add("month", date.ToString("MMMM"));
            dictionary.Add("day", date.Day.ToString("0"));

            return dictionary;
        }

        /// <summary>
        /// Returns a random date between 2 dates
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static DateTime GetRandomDateBetween(DateTime startDate, DateTime endDate)
        {

            Random gen = new Random();
            int range = (endDate - startDate).Days;
            DateTime randonmDate = startDate.AddDays(gen.Next(range));
            return randonmDate;
        }

        #endregion Dates 

        #region .net types - conversions

        /// <summary>
        /// Gets the text of the Description for any enum that has a description
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name != null)
            {
                FieldInfo field = type.GetField(name);
                if (field != null)
                {
                    DescriptionAttribute attr =
                           Attribute.GetCustomAttribute(field,
                             typeof(DescriptionAttribute)) as DescriptionAttribute;
                    if (attr != null)
                    {
                        return attr.Description;
                    }
                }
            }
            return null;
        }

        // http://stackoverflow.com/questions/5093842/alphanumeric-sorting-using-linq
        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<string> CustomSortListWithDashes(List<string> list)
        {
            var sortedIOrderedEnumerableResult = list.OrderBy(x => PadNumbersForListWithDashes(x));
            return sortedIOrderedEnumerableResult.ToList();
        }

        public static string PadNumbersForListWithDashes(string input)
        {
            input = input.Replace('-', '#');
            return Regex.Replace(input, "[0-9]+", match => match.Value.PadLeft(10, '0'));
        }

        // http://stackoverflow.com/questions/5093842/alphanumeric-sorting-using-linq
        public static List<string> CustomSortForListWithUnderscores(List<string> list)
        {
            var sortedIOrderedEnumerableResult = list.OrderBy(x => PadNumbersForListWithUnderscores(x));
            return sortedIOrderedEnumerableResult.ToList();
        }

        public static string PadNumbersForListWithUnderscores(string input)
        {
            return Regex.Replace(input, "[0-9]+", match => match.Value.PadLeft(10, '0'));
        }


        /// <summary>
        /// Utility used for minor string conversions to match data found in pivot grids.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static string ConvertNumberToStringWithCommas(long x)
        {
            var numberConvertedToInt = (int)x;
            return numberConvertedToInt.ToString("N0");
        }

        public static bool GetRandomBool()
        {
            Random r = new Random();
            bool tf = false;

            for (int i = 1; i <= 15; i++)
            {
                tf = r.Next(2) == 0 ? false : true;
            }

            return tf;
        }


        public static List<string> RemoveConsecutiveSpacesFromList(SelectElement elem)
        {
            List<string> items = new List<string>();
            foreach (var item in elem.Options)
            {
                RegexOptions options = RegexOptions.None;
                Regex regex = new Regex("[ ]{2,}", options);
                items.Add(regex.Replace(item.Text, " "));
            }
            return items;
        }

        /// <summary>
        /// Converts a datatble with various different types (integer, string, etc) and converts all cells in datatable to strings
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DataTable ConvertVariousTypeDataTableToStringDataTable(DataTable dt)
        {
            DataTable dtClone = dt.Clone(); //just copy structure, no data
            for (int i = 0; i < dtClone.Columns.Count; i++)
            {
                if (dtClone.Columns[i].DataType != typeof(string))
                    dtClone.Columns[i].DataType = typeof(string);
            }

            foreach (DataRow dr in dt.Rows)
            {
                dtClone.ImportRow(dr);
            }
            return dtClone;
        }

        public static DataTable RemoveConsecutiveSpacesFromDatatable(DataTable table)
        {
            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex("[ ]{2,}", options);

            // Loop through each row of the table
            table.AsEnumerable().ToList().ForEach(row =>
            {
                var cellList = row.ItemArray.ToList();
                // Loop through each cell of the table
                foreach (var cell in cellList)
                {
                    // Remove consecutive spaces for each cell
                    var blah = cell.ToString();
                    regex.Replace(blah, " ");
                }
            });
            return table;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static List<string> DataRowsToListString(DataRowCollection rows, string columnName = null, bool trimLeadTrailWhiteSpace = false)
        {
            List<string> myList = new List<string>();
            foreach (DataRow row in rows)
            {
                if (string.IsNullOrEmpty(columnName))
                {
                    if (trimLeadTrailWhiteSpace)
                    {
                        myList.Add((string)row[0].ToString().Trim());
                    }
                    else
                    {
                        myList.Add((string)row[0].ToString());
                    }
                }
                else
                {
                    if (trimLeadTrailWhiteSpace)
                    {
                        myList.Add((string)row.Field<string>(columnName).ToString().Trim());

                    }
                    else
                    {
                        myList.Add((string)row.Field<string>(columnName).ToString());
                    }
                }
            }
            return myList;
        }

        public static List<int> DataRowsToListInt(DataRowCollection rows, string columnName = null)
        {
            List<int> myList = new List<int>();
            foreach (DataRow row in rows)
            {
                if (string.IsNullOrEmpty(columnName))
                {
                    myList.Add((int)row[0]);
                }
                else
                {
                    myList.Add((int)row.Field<int>(columnName));
                }
            }
            return myList;
        }

        #endregion .net types - conversions

        #region .net types - get

        public static string GetStringBetweenCharacters(string originalString, string firstCharacter, string secondCharacter)
        {
            int start = originalString.IndexOf(firstCharacter) + 1;
            int end = originalString.IndexOf(secondCharacter, start);
            string result = originalString.Substring(start, end - start);
            return result;
        }

        public static string GetStringAfterCharacter(string originalString, string characterBeforeStartOfNewString, int indexOfStartOfNewStringAfterCutoffCharacter)
        {
            string newString = originalString.Substring(originalString.IndexOf(characterBeforeStartOfNewString) + 2);
            return newString;
        }

        /// <summary>
        /// Returns the cell text in a datatable
        /// </summary>
        /// <param name="datatable">Your datatable</param>
        /// <param name="rowNumber">The zero-based index row number </param>
        /// <param name="columnName">The column name</param>
        public static string GetDatatableCellByRowNumAndColName(DataTable datatable, int rowNumber, string columnName)
        {
            var columnNameWithoutSpaces = Regex.Replace(columnName, @"\s+", "");
            return datatable.Rows[rowNumber][columnNameWithoutSpaces].ToString();
        }

        /// <summary>
        /// Returns the cell text in a datatable
        /// </summary>
        /// <param name="datatable">Your datatable</param>
        /// <param name="colNameBelongingToRecord">The column name of the first column from the DataTable</param>
        /// <param name="recordName">The row name, or in other words, the text inside the cell of the row you are targeting</param>
        /// <param name="colNameOfReturningCell">The column name of the column that you are targeting to get the cell text from</param>
        public static string GetDataTableCellByRecNameAndColName(DataTable datatable, string colNameBelongingToRecord, string recordName, string colNameOfReturningCell)
        {
            string cellText = datatable.AsEnumerable()
                                        .Where(f => f.Field<string>(colNameBelongingToRecord) == recordName)
                                        .Select(f => f.Field<string>(colNameOfReturningCell)).FirstOrDefault();
            return cellText;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sizeOfString"></param>
        /// <returns></returns>
        public static string GetRandomString(int sizeOfString)
        {
            const string chars = "AbCDeFgHI12345678";

            var randomChars =
                InitInfinite(() => chars[random.Next(chars.Length)])
                    .SkipWhile(c => c == ' ')
                    .Take(sizeOfString);

            return new string(randomChars.ToArray());
        }

        /// <summary>
        /// Returns a string with a tester-specified length of random characters
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetRandomInteger_SpecifyLengthAndConvertToString(int length)
        {
            const string chars = "0123456789";
            string randomString = new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            return randomString;
        }

        /// <summary>
        /// Returns a random integer from the range of values below the user-specified maximum value
        /// </summary>
        /// <param name="maxValue">The maximum value of integers that you want to return. i.e. if you pass 2, the method will return either 1 or 2</param>
        public static int GetRandomInteger(int maxValue)
        {
            Random random = new Random();
            int optionIndex = random.Next(maxValue);
            var randomInt = optionIndex++;
            return randomInt;
        }

        /// <summary>
        /// Returns a random integer from the range of values below the user-specified maximum value
        /// </summary>
        /// <param name="maxValue">The maximum value of integers that you want to return. i.e. if you pass 2, the method will return either 1 or 2</param>
        public static int GetRandomIntegerStartingAtZero(int maxValue)
        {
            Random random = new Random();
            int optionIndex = random.Next(0, maxValue);
            var randomInt = optionIndex++;
            return randomInt;
        }

        /// <summary>
        /// Returns a random integer from the range of values below the user-specified maximum value. 
        /// If 0 is passed for minValue and 3 for maxValue, then this will return either 0, 1, or 2
        /// </summary>
        /// <param name="minValue">The minimum value of the range</param>
        /// <param name="maxValue">The maximum value of the range</param>
        public static int GetRandomIntegerWithinRange(int minValue, int maxValue)
        {
            Random r = new Random();
            int rInt = r.Next(minValue, maxValue);
            return rInt;
        }

        #endregion .net types - get

        #region .net types - misc

        /// <summary>
        /// Compares a group of integers and returns true if they are equal
        /// </summary>
        /// <param name="ints"> integers</param>
        /// <returns> if all ints are equals will return true otherwise false</returns>
        public static bool intsEqual(params int[] ints)
        {
            if (ints.Length >= 2)
            {
                for (int i = 0; i < ints.Length - 1; i++)
                {
                    if (ints[i] != ints[i + 1])
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        #endregion .net types - misc

        #region misc

        public static string GetRandomSentence(int sizeOfString)
        {
            var sb = new StringBuilder();

            // Have to add 1 to sizeOfString here because at the end of this method we trim off any white space,
            // which is most likely only going to be one space
            while (sb.Length < sizeOfString + 1)
            {
                int wordLength = random.Next(8) + 1;
                sb.Append(GetRandomString(wordLength)).Append(" ");
            }

            sb.Length = sizeOfString;
            // We are trimming at the end just in case we have any white space character at the end of the 
            // randomized string. I then had add 1 to the sizeOfString variable everywhere else.
            return sb.ToString().TrimEnd();
        }


        public static Guid GetRandomUuid()
        {
            return new Guid();
        }

        public static IEnumerable<T> InitInfinite<T>(Func<T> selector)
        {
            while (true)
            {
                yield return selector();
            }
        }

        #endregion misc


    }

}