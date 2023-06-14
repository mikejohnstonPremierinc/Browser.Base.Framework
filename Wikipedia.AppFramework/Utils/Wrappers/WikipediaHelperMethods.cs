using Browser.Core.Framework;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Data.OleDb;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Dapper;
using System.Data;

namespace Wikipedia.AppFramework
{
    public class WikipediaHelperMethods
    {
        #region properties


        #endregion properties

        #region methods


        #region methods: random

        /// <summary>
        /// Extracts data from the excel file located at browser-base-framework\Wikipedia.UITest\DataFiles
        /// </summary>
        /// <param name="ID">The ID of the user in the excel file</param>
        /// <returns></returns>
        public Users GetUserData(string ID)
        {
            string filepath = FileUtils.GetSolutionDirectory() + "Wikipedia.UITest\\DataFiles_ConstantData\\MyExcelWorkbook.xlsx";
            var connectionString = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source = {0}; Extended Properties=Excel 12.0;", filepath);
            using (var connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                var query = string.Format("select * from [MySheetName$] where ID='{0}'", ID);
                var value = connection.Query<Users>(query).FirstOrDefault();
                connection.Close();
                return value;
            }
        }

        /// <summary>
        /// Extracts data from the excel file located at browser-base-framework\Wikipedia.UITest\DataFiles
        /// </summary>
        /// <param name="ID">The ID of the user in the excel file</param>
        /// <returns></returns>
        public DataTable GetAllDataFromExcelSheet()
        {
            string filepath = FileUtils.GetSolutionDirectory() + "Wikipedia.UITest\\DataFiles_ConstantData\\MyExcelWorkbook.xlsx";
            
            using (OleDbConnection conn = new OleDbConnection())
            {
                DataTable dt = new DataTable();
                conn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filepath
                + ";Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1;MAXSCANROWS=0'";
                using (OleDbCommand comm = new OleDbCommand())
                {
                    comm.CommandText = "Select * from [MySheetName$]";
                    comm.Connection = conn;
                    using (OleDbDataAdapter da = new OleDbDataAdapter())
                    {
                        da.SelectCommand = comm;
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        #endregion methods: random

        #region methods: workflows



        #endregion methods: workflows

        #region methods: tables



        #endregion methods: tables

        #region methods: Check boxes, text boxes, etc.



        #endregion methods: Check boxes, text boxes, etc.

        #endregion methods
    }

    public class Users
    {
        #region properties

        public string ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        #endregion properties

        #region constructors

        //public Users(string id, string firstname, string lastname)
        //{
        //    ID = id;
        //    FirstName = firstname;
        //    LastName = lastname;
        //}



        #endregion constructors

        #region methods


        #endregion methods

    }

}
