using Browser.Core.Framework;
using LMS.Data;
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

namespace LMS.AppFramework
{
    public static class UserUtils
    {
        #region properties

        //public static string Environment = AppSettings.Config["environment"].ToString();
        public static string Environment = AppSettings.Config["environment"];

        #region static users

        public static string Password = "Premier1@";
        public static string Password_AllCaps = "PREMIER1@";

        // Compatible portals: UAMS, 
        // Profession 1
        public static string UAMS_Physician1Username = "TA_UAMS_Physician1@mailinator.com";
        public static string UAMS_Physician1Email = "TA_UAMS_Physician1@mailinator.com";
        public static string UAMS_Physician1Username_Mobile = "TA_UAMS_Physician1_Mobile@mailinator.com";
        public static string UAMS_Physician1Email_Mobile = "ta_UAMS_Physician1@mailinator.com";
        public static string UAMS_Physician1Username_IE = "TA_UAMS_Physician1_IE@mailinator.com";
        public static string UAMS_Physician1Email_IE = "ta_UAMS_Physician1@mailinator.com";
        public static string UAMS_Physician1Username_FF = "TA_UAMS_Physician1_FF@mailinator.com";
        public static string UAMS_Physician1Email_FF = "ta_UAMS_Physician1@mailinator.com";

        public static string UAMS_Physician2Username = "TA_UAMS_Physician2@mailinator.com";
        public static string UAMS_Physician2Email = "ta_UAMS_Physician2@mailinator.com";
        public static string UAMS_Physician2Username_Mobile = "TA_UAMS_Physician2_Mobile@mailinator.com";
        public static string UAMS_Physician2Email_Mobile = "ta_UAMS_Physician2@mailinator.com";
        public static string UAMS_Physician2Username_IE = "TA_UAMS_Physician2_IE@mailinator.com";
        public static string UAMS_Physician2Email_IE = "ta_UAMS_Physician2@mailinator.com";
        public static string UAMS_Physician2Username_FF = "TA_UAMS_Physician2_FF@mailinator.com";
        public static string UAMS_Physician2Email_FF = "ta_UAMS_Physician2@mailinator.com";

        public static string UAMS_Physician3Username = "TA_UAMS_Physician3@mailinator.com";
        public static string UAMS_Physician3Email = "ta_UAMS_Physician3@mailinator.com";
        public static string UAMS_Physician3Username_Mobile = "TA_UAMS_Physician3_Mobile@mailinator.com";
        public static string UAMS_Physician3Email_Mobile = "ta_UAMS_Physician3@mailinator.com";
        public static string UAMS_Physician3Username_IE = "TA_UAMS_Physician3_IE@mailinator.com";
        public static string UAMS_Physician3Email_IE = "ta_UAMS_Physician3@mailinator.com";
        public static string UAMS_Physician3Username_FF = "TA_UAMS_Physician3_FF@mailinator.com";
        public static string UAMS_Physician3Email_FF = "ta_UAMS_Physician3@mailinator.com";

        public static string UAMS_Physician4Username = "TA_UAMS_Physician4@mailinator.com";
        public static string UAMS_Physician4Email = "ta_UAMS_Physician4@mailinator.com";
        public static string UAMS_Physician4Username_Mobile = "TA_UAMS_Physician4_Mobile@mailinator.com";
        public static string UAMS_Physician4Email_Mobile = "ta_UAMS_Physician4@mailinator.com";
        public static string UAMS_Physician4Username_IE = "TA_UAMS_Physician4_IE@mailinator.com";
        public static string UAMS_Physician4Email_IE = "ta_UAMS_Physician4@mailinator.com";
        public static string UAMS_Physician4Username_FF = "TA_UAMS_Physician4_FF@mailinator.com";
        public static string UAMS_Physician4Email_FF = "ta_UAMS_Physician4@mailinator.com";

        // Profession 2
        public static string UAMS_Pharmacist1Username = "TA_UAMS_Pharmacist1@mailinator.com";
        public static string UAMS_Pharmacist1Email = "ta_UAMS_Pharmacist1@mailinator.com";
        public static string UAMS_Pharmacist1Username_Mobile = "TA_UAMS_Pharmacist1_Mobile@mailinator.com";
        public static string UAMS_Pharmacist1Email_Mobile = "ta_UAMS_Pharmacist1@mailinator.com";
        public static string UAMS_Pharmacist1Username_IE = "TA_UAMS_Pharmacist1_IE@mailinator.com";
        public static string UAMS_Pharmacist1Email_IE = "ta_UAMS_Pharmacist1@mailinator.com";
        public static string UAMS_Pharmacist1Username_FF = "TA_UAMS_Pharmacist1_FF@mailinator.com";
        public static string UAMS_Pharmacist1Email_FF = "ta_UAMS_Pharmacist1@mailinator.com";

        public static string UAMS_Pharmacist2Username = "TA_UAMS_Pharmacist2@mailinator.com";
        public static string UAMS_Pharmacist2Email = "ta_UAMS_Pharmacist2@mailinator.com";
        public static string UAMS_Pharmacist2Username_Mobile = "TA_UAMS_Pharmacist2_Mobile@mailinator.com";
        public static string UAMS_Pharmacist2Email_Mobile = "ta_UAMS_Pharmacist2@mailinator.com";
        public static string UAMS_Pharmacist2Username_IE = "TA_UAMS_Pharmacist2_IE@mailinator.com";
        public static string UAMS_Pharmacist2Email_IE = "ta_UAMS_Pharmacist2@mailinator.com";
        public static string UAMS_Pharmacist2Username_FF = "TA_UAMS_Pharmacist2_FF@mailinator.com";
        public static string UAMS_Pharmacist2Email_FF = "ta_UAMS_Pharmacist2@mailinator.com";

        // Compatible portals: ONSLT, 
        // Profession 1 (ToDo: Add 4 of profession 1 here)
        public static string ONSLT_NurseScientist1Username = "ta_ONSLT_NurseScientist1";
        public static string ONSLT_NurseScientist1Email = "ta_ONSLT_NurseScientist1@mailinator.com";
        public static string ONSLT_NurseScientist1Username_Mobile = "ta_ONSLT_NurseScientist1_Mobile";
        public static string ONSLT_NurseScientist1Email_Mobile = "ta_ONSLT_NurseScientist1@mailinator.com";
        public static string ONSLT_NurseScientist1Username_IE = "ta_ONSLT_NurseScientist1_IE";
        public static string ONSLT_NurseScientist1Email_IE = "ta_ONSLT_NurseScientist1@mailinator.com";
        public static string ONSLT_NurseScientist1Username_FF = "ta_ONSLT_NurseScientist1_FF";
        public static string ONSLT_NurseScientist1Email_FF = "ta_ONSLT_NurseScientist1@mailinator.com";

        public static string ONSLT_NurseScientist2Username = "ta_ONSLT_NurseScientist2";
        public static string ONSLT_NurseScientist2Email = "ta_ONSLT_NurseScientist2@mailinator.com";
        public static string ONSLT_NurseScientist2Username_Mobile = "ta_ONSLT_NurseScientist2_Mobile";
        public static string ONSLT_NurseScientist2Email_Mobile = "ta_ONSLT_NurseScientist2@mailinator.com";
        public static string ONSLT_NurseScientist2Username_IE = "ta_ONSLT_NurseScientist2_IE";
        public static string ONSLT_NurseScientist2Email_IE = "ta_ONSLT_NurseScientist2@mailinator.com";
        public static string ONSLT_NurseScientist2Username_FF = "ta_ONSLT_NurseScientist2_FF";
        public static string ONSLT_NurseScientist2Email_FF = "ta_ONSLT_NurseScientist2@mailinator.com";

        // Profession 2
        public static string ONSLT_NursePractitioner1Username = "ta_ONSLT_NursePractitioner1";
        public static string ONSLT_NursePractitioner1Email = "ta_ONSLT_NursePractitioner1@mailinator.com";
        public static string ONSLT_NursePractitioner1Username_Mobile = "ta_ONSLT_NursePractitioner1_Mobile";
        public static string ONSLT_NursePractitioner1Email_Mobile = "ta_ONSLT_NursePractitioner1@mailinator.com";
        public static string ONSLT_NursePractitioner1Username_IE = "ta_ONSLT_NursePractitioner1_IE";
        public static string ONSLT_NursePractitioner1Email_IE = "ta_ONSLT_NursePractitioner1@mailinator.com";
        public static string ONSLT_NursePractitioner1Username_FF = "ta_ONSLT_NursePractitioner1_FF";
        public static string ONSLT_NursePractitioner1Email_FF = "ta_ONSLT_NursePractitioner1@mailinator.com";

        public static string ONSLT_NursePractitioner2Username = "ta_ONSLT_NursePractitioner2";
        public static string ONSLT_NursePractitioner2Email = "ta_ONSLT_NursePractitioner2@mailinator.com";
        public static string ONSLT_NursePractitioner2Username_Mobile = "ta_ONSLT_NursePractitioner2_Mobile";
        public static string ONSLT_NursePractitioner2Email_Mobile = "ta_ONSLT_NursePractitioner2@mailinator.com";
        public static string ONSLT_NursePractitioner2Username_IE = "ta_ONSLT_NursePractitioner2_IE";
        public static string ONSLT_NursePractitioner2Email_IE = "ta_ONSLT_NursePractitioner2@mailinator.com";
        public static string ONSLT_NursePractitioner2Username_FF = "ta_ONSLT_NursePractitioner2_FF";
        public static string ONSLT_NursePractitioner2Email_FF = "ta_ONSLT_NursePractitioner2@mailinator.com";

        // Compatible portals: CMECA, 
        // Profession 1 (ToDo: Add 4 of profession 1 here)
        public static string CMECA_Physician1Username = "ta_CMECA_Physician1";
        public static string CMECA_Physician1Email = "ta_CMECA_Physician1@mailinator.com";
        public static string CMECA_Physician1Username_Mobile = "ta_CMECA_Physician1_Mobile";
        public static string CMECA_Physician1Email_Mobile = "ta_CMECA_Physician1@mailinator.com";
        public static string CMECA_Physician1Username_IE = "ta_CMECA_Physician1_IE";
        public static string CMECA_Physician1Email_IE = "ta_CMECA_Physician1@mailinator.com";
        public static string CMECA_Physician1Username_FF = "ta_CMECA_Physician1_FF";
        public static string CMECA_Physician1Email_FF = "ta_CMECA_Physician1@mailinator.com";

        public static string CMECA_Physician2Username = "ta_CMECA_Physician2";
        public static string CMECA_Physician2Email = "ta_CMECA_Physician2@mailinator.com";
        public static string CMECA_Physician2Username_Mobile = "ta_CMECA_Physician2_Mobile";
        public static string CMECA_Physician2Email_Mobile = "ta_CMECA_Physician2@mailinator.com";
        public static string CMECA_Physician2Username_IE = "ta_CMECA_Physician2_IE";
        public static string CMECA_Physician2Email_IE = "ta_CMECA_Physician2@mailinator.com";
        public static string CMECA_Physician2Username_FF = "ta_CMECA_Physician2_FF";
        public static string CMECA_Physician2Email_FF = "ta_CMECA_Physician2@mailinator.com";

        // Profession 2
        public static string CMECA_Nurse1Username = "ta_CMECA_Nurse1";
        public static string CMECA_Nurse1Email = "ta_CMECA_Nurse1@mailinator.com";
        public static string CMECA_Nurse1Username_Mobile = "ta_CMECA_Nurse1_Mobile";
        public static string CMECA_Nurse1Email_Mobile = "ta_CMECA_Nurse1@mailinator.com";
        public static string CMECA_Nurse1Username_IE = "ta_CMECA_Nurse1_IE";
        public static string CMECA_Nurse1Email_IE = "ta_CMECA_Nurse1@mailinator.com";
        public static string CMECA_Nurse1Username_FF = "ta_CMECA_Nurse1_FF";
        public static string CMECA_Nurse1Email_FF = "ta_CMECA_Nurse1@mailinator.com";

        public static string CMECA_Nurse2Username = "ta_CMECA_Nurse2";
        public static string CMECA_Nurse2Email = "ta_CMECA_Nurse2@mailinator.com";
        public static string CMECA_Nurse2Username_Mobile = "ta_CMECA_Nurse2_Mobile";
        public static string CMECA_Nurse2Email_Mobile = "ta_CMECA_Nurse2@mailinator.com";
        public static string CMECA_Nurse2Username_IE = "ta_CMECA_Nurse2_IE";
        public static string CMECA_Nurse2Email_IE = "ta_CMECA_Nurse2@mailinator.com";
        public static string CMECA_Nurse2Username_FF = "ta_CMECA_Nurse2_FF";
        public static string CMECA_Nurse2Email_FF = "ta_CMECA_Nurse2@mailinator.com";

        // Profession 3
        public static string CMECA_Pharmacist1Username = "ta_CMECA_Pharmacist1";
        public static string CMECA_Pharmacist1Email = "ta_CMECA_Pharmacist1@mailinator.com";
        public static string CMECA_Pharmacist1Username_Mobile = "ta_CMECA_Pharmacist1_Mobile";
        public static string CMECA_Pharmacist1Email_Mobile = "ta_CMECA_Pharmacist1@mailinator.com";
        public static string CMECA_Pharmacist1Username_IE = "ta_CMECA_Pharmacist1_IE";
        public static string CMECA_Pharmacist1Email_IE = "ta_CMECA_Pharmacist1@mailinator.com";
        public static string CMECA_Pharmacist1Username_FF = "ta_CMECA_Pharmacist1_FF";
        public static string CMECA_Pharmacist1Email_FF = "ta_CMECA_Pharmacist1@mailinator.com";

        // Compatible portals: AHA, 
        // Profession 1 (ToDo: Add 4 of profession 1 here)
        public static string AHA_Physician1Username = "ta_AHA_Physician1";
        public static string AHA_Physician1Email = "ta_AHA_Physician1@mailinator.com";
        public static string AHA_Physician1Username_Mobile = "ta_AHA_Physician1_Mobile";
        public static string AHA_Physician1Email_Mobile = "ta_AHA_Physician1@mailinator.com";
        public static string AHA_Physician1Username_IE = "ta_AHA_Physician1_IE";
        public static string AHA_Physician1Email_IE = "ta_AHA_Physician1@mailinator.com";
        public static string AHA_Physician1Username_FF = "ta_AHA_Physician1_FF";
        public static string AHA_Physician1Email_FF = "ta_AHA_Physician1@mailinator.com";

        public static string AHA_Physician2Username = "ta_AHA_Physician2";
        public static string AHA_Physician2Email = "ta_AHA_Physician2@mailinator.com";
        public static string AHA_Physician2Username_Mobile = "ta_AHA_Physician2_Mobile";
        public static string AHA_Physician2Email_Mobile = "ta_AHA_Physician2@mailinator.com";
        public static string AHA_Physician2Username_IE = "ta_AHA_Physician2_IE";
        public static string AHA_Physician2Email_IE = "ta_AHA_Physician2@mailinator.com";
        public static string AHA_Physician2Username_FF = "ta_AHA_Physician2_FF";
        public static string AHA_Physician2Email_FF = "ta_AHA_Physician2@mailinator.com";

        // Profession 2
        public static string AHA_Nurse1Username = "ta_AHA_Nurse1";
        public static string AHA_Nurse1Email = "ta_AHA_Nurse1@mailinator.com";
        public static string AHA_Nurse1Username_Mobile = "ta_AHA_Nurse1_Mobile";
        public static string AHA_Nurse1Email_Mobile = "ta_AHA_Nurse1@mailinator.com";
        public static string AHA_Nurse1Username_IE = "ta_AHA_Nurse1_IE";
        public static string AHA_Nurse1Email_IE = "ta_AHA_Nurse1@mailinator.com";
        public static string AHA_Nurse1Username_FF = "ta_AHA_Nurse1_FF";
        public static string AHA_Nurse1Email_FF = "ta_AHA_Nurse1@mailinator.com";

        public static string AHA_Nurse2Username = "ta_AHA_Nurse2";
        public static string AHA_Nurse2Email = "ta_AHA_Nurse2@mailinator.com";
        public static string AHA_Nurse2Username_Mobile = "ta_AHA_Nurse2_Mobile";
        public static string AHA_Nurse2Email_Mobile = "ta_AHA_Nurse2@mailinator.com";
        public static string AHA_Nurse2Username_IE = "ta_AHA_Nurse2_IE";
        public static string AHA_Nurse2Email_IE = "ta_AHA_Nurse2@mailinator.com";
        public static string AHA_Nurse2Username_FF = "ta_AHA_Nurse2_FF";
        public static string AHA_Nurse2Email_FF = "ta_AHA_Nurse2@mailinator.com";

        // Profession 3
        public static string AHA_Pharmacist1Username = "ta_AHA_Pharmacist1";
        public static string AHA_Pharmacist1Email = "ta_AHA_Pharmacist1@mailinator.com";
        public static string AHA_Pharmacist1Username_Mobile = "ta_AHA_Pharmacist1_Mobile";
        public static string AHA_Pharmacist1Email_Mobile = "ta_AHA_Pharmacist1@mailinator.com";
        public static string AHA_Pharmacist1Username_IE = "ta_AHA_Pharmacist1_IE";
        public static string AHA_Pharmacist1Email_IE = "ta_AHA_Pharmacist1@mailinator.com";
        public static string AHA_Pharmacist1Username_FF = "ta_AHA_Pharmacist1_FF";
        public static string AHA_Pharmacist1Email_FF = "ta_AHA_Pharmacist1@mailinator.com";

        // Profession 1 (Member)
        public static string AHA_PhysicianMember1Username = "ta_AHA_PhysicianMember1";
        public static string AHA_PhysicianMember1Email = "ta_AHA_PhysicianMember1@mailinator.com";
        public static string AHA_PhysicianMember1Username_Mobile = "ta_AHA_PhysicianMember1_Mobile";
        public static string AHA_PhysicianMember1Email_Mobile = "ta_AHA_PhysicianMember1@mailinator.com";
        public static string AHA_PhysicianMember1Username_IE = "ta_AHA_PhysicianMember1_IE";
        public static string AHA_PhysicianMember1Email_IE = "ta_AHA_PhysicianMember1@mailinator.com";
        public static string AHA_PhysicianMember1Username_FF = "ta_AHA_PhysicianMember1_FF";
        public static string AHA_PhysicianMember1Email_FF = "ta_AHA_PhysicianMember1@mailinator.com";

        public static string AHA_PhysicianMember2Username = "ta_AHA_PhysicianMember2";
        public static string AHA_PhysicianMember2Email = "ta_AHA_PhysicianMember2@mailinator.com";
        public static string AHA_PhysicianMember2Username_Mobile = "ta_AHA_PhysicianMember2_Mobile";
        public static string AHA_PhysicianMember2Email_Mobile = "ta_AHA_PhysicianMember2@mailinator.com";
        public static string AHA_PhysicianMember2Username_IE = "ta_AHA_PhysicianMember2_IE";
        public static string AHA_PhysicianMember2Email_IE = "ta_AHA_PhysicianMember2@mailinator.com";
        public static string AHA_PhysicianMember2Username_FF = "ta_AHA_PhysicianMember2_FF";
        public static string AHA_PhysicianMember2Email_FF = "ta_AHA_PhysicianMember2@mailinator.com";

        public static string AHA_PhysicianMember3Username = "ta_AHA_PhysicianMember3";
        public static string AHA_PhysicianMember3Email = "ta_AHA_PhysicianMember3@mailinator.com";
        public static string AHA_PhysicianMember3Username_Mobile = "ta_AHA_PhysicianMember3_Mobile";
        public static string AHA_PhysicianMember3Email_Mobile = "ta_AHA_PhysicianMember3@mailinator.com";
        public static string AHA_PhysicianMember3Username_IE = "ta_AHA_PhysicianMember3_IE";
        public static string AHA_PhysicianMember3Email_IE = "ta_AHA_PhysicianMember3@mailinator.com";
        public static string AHA_PhysicianMember3Username_FF = "ta_AHA_PhysicianMember3_FF";
        public static string AHA_PhysicianMember3Email_FF = "ta_AHA_PhysicianMember3@mailinator.com";

        public static string AHA_PhysicianMember4Username = "ta_AHA_PhysicianMember4";
        public static string AHA_PhysicianMember4Email = "ta_AHA_PhysicianMember4@mailinator.com";
        public static string AHA_PhysicianMember4Username_Mobile = "ta_AHA_PhysicianMember4_Mobile";
        public static string AHA_PhysicianMember4Email_Mobile = "ta_AHA_PhysicianMember4@mailinator.com";
        public static string AHA_PhysicianMember4Username_IE = "ta_AHA_PhysicianMember4_IE";
        public static string AHA_PhysicianMember4Email_IE = "ta_AHA_PhysicianMember4@mailinator.com";
        public static string AHA_PhysicianMember4Username_FF = "ta_AHA_PhysicianMember4_FF";
        public static string AHA_PhysicianMember4Email_FF = "ta_AHA_PhysicianMember4@mailinator.com";

        public static string AHA_PhysicianMember5Username = "ta_AHA_PhysicianMember5";
        public static string AHA_PhysicianMember5Email = "ta_AHA_PhysicianMember5@mailinator.com";
        public static string AHA_PhysicianMember5Username_Mobile = "ta_AHA_PhysicianMember5_Mobile";
        public static string AHA_PhysicianMember5Email_Mobile = "ta_AHA_PhysicianMember5@mailinator.com";
        public static string AHA_PhysicianMember5Username_IE = "ta_AHA_PhysicianMember5_IE";
        public static string AHA_PhysicianMember5Email_IE = "ta_AHA_PhysicianMember5@mailinator.com";
        public static string AHA_PhysicianMember5Username_FF = "ta_AHA_PhysicianMember5_FF";
        public static string AHA_PhysicianMember5Email_FF = "ta_AHA_PhysicianMember5@mailinator.com";

        public static string AHA_PhysicianMember6Username = "ta_AHA_PhysicianMember6";
        public static string AHA_PhysicianMember6Email = "ta_AHA_PhysicianMember6@mailinator.com";
        public static string AHA_PhysicianMember6Username_Mobile = "ta_AHA_PhysicianMember6_Mobile";
        public static string AHA_PhysicianMember6Email_Mobile = "ta_AHA_PhysicianMember6@mailinator.com";
        public static string AHA_PhysicianMember6Username_IE = "ta_AHA_PhysicianMember6_IE";
        public static string AHA_PhysicianMember6Email_IE = "ta_AHA_PhysicianMember6@mailinator.com";
        public static string AHA_PhysicianMember6Username_FF = "ta_AHA_PhysicianMember6_FF";
        public static string AHA_PhysicianMember6Email_FF = "ta_AHA_PhysicianMember6@mailinator.com";

        public static string AHA_PhysicianMember7Username = "ta_AHA_PhysicianMember7";
        public static string AHA_PhysicianMember7Email = "ta_AHA_PhysicianMember7@mailinator.com";
        public static string AHA_PhysicianMember7Username_Mobile = "ta_AHA_PhysicianMember7_Mobile";
        public static string AHA_PhysicianMember7Email_Mobile = "ta_AHA_PhysicianMember7@mailinator.com";
        public static string AHA_PhysicianMember7Username_IE = "ta_AHA_PhysicianMember7_IE";
        public static string AHA_PhysicianMember7Email_IE = "ta_AHA_PhysicianMember7@mailinator.com";
        public static string AHA_PhysicianMember7Username_FF = "ta_AHA_PhysicianMember7_FF";
        public static string AHA_PhysicianMember7Email_FF = "ta_AHA_PhysicianMember7@mailinator.com";

        public static string AHA_PhysicianMember8Username = "ta_AHA_PhysicianMember8";
        public static string AHA_PhysicianMember8Email = "ta_AHA_PhysicianMember8@mailinator.com";
        public static string AHA_PhysicianMember8Username_Mobile = "ta_AHA_PhysicianMember8_Mobile";
        public static string AHA_PhysicianMember8Email_Mobile = "ta_AHA_PhysicianMember8@mailinator.com";
        public static string AHA_PhysicianMember8Username_IE = "ta_AHA_PhysicianMember8_IE";
        public static string AHA_PhysicianMember8Email_IE = "ta_AHA_PhysicianMember8@mailinator.com";
        public static string AHA_PhysicianMember8Username_FF = "ta_AHA_PhysicianMember8_FF";
        public static string AHA_PhysicianMember8Email_FF = "ta_AHA_PhysicianMember8@mailinator.com";

        // Compatible portals: ACR, 
        // Profession 1 
        public static string ACR_Nurse1Username = "ta_ACR_Nurse1";
        public static string ACR_Nurse1Email = "ta_ACR_Nurse1@mailinator.com";
        public static string ACR_Nurse1Username_Mobile = "ta_ACR_Nurse1_Mobile";
        public static string ACR_Nurse1Email_Mobile = "ta_ACR_Nurse1@mailinator.com";
        public static string ACR_Nurse1Username_IE = "ta_ACR_Nurse1_IE";
        public static string ACR_Nurse1Email_IE = "ta_ACR_Nurse1@mailinator.com";
        public static string ACR_Nurse1Username_FF = "ta_ACR_Nurse1_FF";
        public static string ACR_Nurse1Email_FF = "ta_ACR_Nurse1@mailinator.com";
        // Profession 2
        public static string ACR_Nurse2Username = "ta_ACR_Nurse2";
        public static string ACR_Nurse2Email = "ta_ACR_Nurse2@mailinator.com";
        public static string ACR_Nurse2Username_Mobile = "ta_ACR_Nurse2_Mobile";
        public static string ACR_Nurse2Email_Mobile = "ta_ACR_Nurse2@mailinator.com";
        public static string ACR_Nurse2Username_IE = "ta_ACR_Nurse2_IE";
        public static string ACR_Nurse2Email_IE = "ta_ACR_Nurse2@mailinator.com";
        public static string ACR_Nurse2Username_FF = "ta_ACR_Nurse2_FF";
        public static string ACR_Nurse2Email_FF = "ta_ACR_Nurse2@mailinator.com";

        // Compatible portals: ASNC, 
        public static string ASNC_Physician1Username = "ta_ASNC_Physician1";
        public static string ASNC_Physician1Email = "ta_ASNC_Physician1@mailinator.com";
        public static string ASNC_Physician1Username_Mobile = "ta_ASNC_Physician1_Mobile";
        public static string ASNC_Physician1Email_Mobile = "ta_ASNC_Physician1@mailinator.com";
        public static string ASNC_Physician1Username_IE = "ta_ASNC_Physician1_IE";
        public static string ASNC_Physician1Email_IE = "ta_ASNC_Physician1@mailinator.com";
        public static string ASNC_Physician1Username_FF = "ta_ASNC_Physician1_FF";
        public static string ASNC_Physician1Email_FF = "ta_ASNC_Physician1@mailinator.com";

        public static string ASNC_Physician2Username = "ta_ASNC_Physician2";
        public static string ASNC_Physician2Email = "ta_ASNC_Physician2@mailinator.com";
        public static string ASNC_Physician2Username_Mobile = "ta_ASNC_Physician2_Mobile";
        public static string ASNC_Physician2Email_Mobile = "ta_ASNC_Physician2@mailinator.com";
        public static string ASNC_Physician2Username_IE = "ta_ASNC_Physician2_IE";
        public static string ASNC_Physician2Email_IE = "ta_ASNC_Physician2@mailinator.com";
        public static string ASNC_Physician2Username_FF = "ta_ASNC_Physician2_FF";
        public static string ASNC_Physician2Email_FF = "ta_ASNC_Physician2@mailinator.com";
        // Profession 2
        public static string ASNC_Nurse1Username = "ta_ASNC_Nurse1";
        public static string ASNC_Nurse1Email = "ta_ASNC_Nurse1@mailinator.com";
        public static string ASNC_Nurse1Username_Mobile = "ta_ASNC_Nurse1_Mobile";
        public static string ASNC_Nurse1Email_Mobile = "ta_ASNC_Nurse1@mailinator.com";
        public static string ASNC_Nurse1Username_IE = "ta_ASNC_Nurse1_IE";
        public static string ASNC_Nurse1Email_IE = "ta_ASNC_Nurse1@mailinator.com";
        public static string ASNC_Nurse1Username_FF = "ta_ASNC_Nurse1_FF";
        public static string ASNC_Nurse1Email_FF = "ta_ASNC_Nurse1@mailinator.com";

        public static string ASNC_Nurse2Username = "ta_ASNC_Nurse2";
        public static string ASNC_Nurse2Email = "ta_ASNC_Nurse2@mailinator.com";
        public static string ASNC_Nurse2Username_Mobile = "ta_ASNC_Nurse2_Mobile";
        public static string ASNC_Nurse2Email_Mobile = "ta_ASNC_Nurse2@mailinator.com";
        public static string ASNC_Nurse2Username_IE = "ta_ASNC_Nurse2_IE";
        public static string ASNC_Nurse2Email_IE = "ta_ASNC_Nurse2@mailinator.com";
        public static string ASNC_Nurse2Username_FF = "ta_ASNC_Nurse2_FF";
        public static string ASNC_Nurse2Email_FF = "ta_ASNC_Nurse2@mailinator.com";

        // Compatible portals: CAP, 
        public static string CAP_Physician1Username = "ta_CAP_Physician1";
        public static string CAP_Physician1Email = "ta_CAP_Physician1@mailinator.com";
        public static string CAP_Physician1Username_Mobile = "ta_CAP_Physician1_Mobile";
        public static string CAP_Physician1Email_Mobile = "ta_CAP_Physician1@mailinator.com";
        public static string CAP_Physician1Username_IE = "ta_CAP_Physician1_IE";
        public static string CAP_Physician1Email_IE = "ta_CAP_Physician1@mailinator.com";
        public static string CAP_Physician1Username_FF = "ta_CAP_Physician1_FF";
        public static string CAP_Physician1Email_FF = "ta_CAP_Physician1@mailinator.com";

        public static string CAP_Physician2Username = "ta_CAP_Physician2";
        public static string CAP_Physician2Email = "ta_CAP_Physician2@mailinator.com";
        public static string CAP_Physician2Username_Mobile = "ta_CAP_Physician2_Mobile";
        public static string CAP_Physician2Email_Mobile = "ta_CAP_Physician2@mailinator.com";
        public static string CAP_Physician2Username_IE = "ta_CAP_Physician2_IE";
        public static string CAP_Physician2Email_IE = "ta_CAP_Physician2@mailinator.com";
        public static string CAP_Physician2Username_FF = "ta_CAP_Physician2_FF";
        public static string CAP_Physician2Email_FF = "ta_CAP_Physician2@mailinator.com";
        // Profession 2
        public static string CAP_Nurse1Username = "ta_CAP_Nurse1";
        public static string CAP_Nurse1Email = "ta_CAP_Nurse1@mailinator.com";
        public static string CAP_Nurse1Username_Mobile = "ta_CAP_Nurse1_Mobile";
        public static string CAP_Nurse1Email_Mobile = "ta_CAP_Nurse1@mailinator.com";
        public static string CAP_Nurse1Username_IE = "ta_CAP_Nurse1_IE";
        public static string CAP_Nurse1Email_IE = "ta_CAP_Nurse1@mailinator.com";
        public static string CAP_Nurse1Username_FF = "ta_CAP_Nurse1_FF";
        public static string CAP_Nurse1Email_FF = "ta_CAP_Nurse1@mailinator.com";

        public static string CAP_Nurse2Username = "ta_CAP_Nurse2";
        public static string CAP_Nurse2Email = "ta_CAP_Nurse2@mailinator.com";
        public static string CAP_Nurse2Username_Mobile = "ta_CAP_Nurse2_Mobile";
        public static string CAP_Nurse2Email_Mobile = "ta_CAP_Nurse2@mailinator.com";
        public static string CAP_Nurse2Username_IE = "ta_CAP_Nurse2_IE";
        public static string CAP_Nurse2Email_IE = "ta_CAP_Nurse2@mailinator.com";
        public static string CAP_Nurse2Username_FF = "ta_CAP_Nurse2_FF";
        public static string CAP_Nurse2Email_FF = "ta_CAP_Nurse2@mailinator.com";

        // Compatible portals: SNMMI, 
        public static string SNMMI_Physician1Username = "ta_SNMMI_Physician1";
        public static string SNMMI_Physician1Email = "ta_SNMMI_Physician1@mailinator.com";
        public static string SNMMI_Physician1Username_Mobile = "ta_SNMMI_Physician1_Mobile";
        public static string SNMMI_Physician1Email_Mobile = "ta_SNMMI_Physician1@mailinator.com";
        public static string SNMMI_Physician1Username_IE = "ta_SNMMI_Physician1_IE";
        public static string SNMMI_Physician1Email_IE = "ta_SNMMI_Physician1@mailinator.com";
        public static string SNMMI_Physician1Username_FF = "ta_SNMMI_Physician1_FF";
        public static string SNMMI_Physician1Email_FF = "ta_SNMMI_Physician1@mailinator.com";

        public static string SNMMI_Physician2Username = "ta_SNMMI_Physician2";
        public static string SNMMI_Physician2Email = "ta_SNMMI_Physician2@mailinator.com";
        public static string SNMMI_Physician2Username_Mobile = "ta_SNMMI_Physician2_Mobile";
        public static string SNMMI_Physician2Email_Mobile = "ta_SNMMI_Physician2@mailinator.com";
        public static string SNMMI_Physician2Username_IE = "ta_SNMMI_Physician2_IE";
        public static string SNMMI_Physician2Email_IE = "ta_SNMMI_Physician2@mailinator.com";
        public static string SNMMI_Physician2Username_FF = "ta_SNMMI_Physician2_FF";
        public static string SNMMI_Physician2Email_FF = "ta_SNMMI_Physician2@mailinator.com";
        // Profession 2
        public static string SNMMI_Nurse1Username = "ta_SNMMI_Nurse1";
        public static string SNMMI_Nurse1Email = "ta_SNMMI_Nurse1@mailinator.com";
        public static string SNMMI_Nurse1Username_Mobile = "ta_SNMMI_Nurse1_Mobile";
        public static string SNMMI_Nurse1Email_Mobile = "ta_SNMMI_Nurse1@mailinator.com";
        public static string SNMMI_Nurse1Username_IE = "ta_SNMMI_Nurse1_IE";
        public static string SNMMI_Nurse1Email_IE = "ta_SNMMI_Nurse1@mailinator.com";
        public static string SNMMI_Nurse1Username_FF = "ta_SNMMI_Nurse1_FF";
        public static string SNMMI_Nurse1Email_FF = "ta_SNMMI_Nurse1@mailinator.com";

        public static string SNMMI_Nurse2Username = "ta_SNMMI_Nurse2";
        public static string SNMMI_Nurse2Email = "ta_SNMMI_Nurse2@mailinator.com";
        public static string SNMMI_Nurse2Username_Mobile = "ta_SNMMI_Nurse2_Mobile";
        public static string SNMMI_Nurse2Email_Mobile = "ta_SNMMI_Nurse2@mailinator.com";
        public static string SNMMI_Nurse2Username_IE = "ta_SNMMI_Nurse2_IE";
        public static string SNMMI_Nurse2Email_IE = "ta_SNMMI_Nurse2@mailinator.com";
        public static string SNMMI_Nurse2Username_FF = "ta_SNMMI_Nurse2_FF";
        public static string SNMMI_Nurse2Email_FF = "ta_SNMMI_Nurse2@mailinator.com";

        // Profession 1 (Member)
        public static string SNMMI_PhysicianMember1Username = "ta_SNMMI_PhysicianMember1";
        public static string SNMMI_PhysicianMember1Email = "ta_SNMMI_PhysicianMember1@mailinator.com";
        public static string SNMMI_PhysicianMember1Username_Mobile = "ta_SNMMI_PhysicianMember1_Mobile";
        public static string SNMMI_PhysicianMember1Email_Mobile = "ta_SNMMI_PhysicianMember1@mailinator.com";
        public static string SNMMI_PhysicianMember1Username_IE = "ta_SNMMI_PhysicianMember1_IE";
        public static string SNMMI_PhysicianMember1Email_IE = "ta_SNMMI_PhysicianMember1@mailinator.com";
        public static string SNMMI_PhysicianMember1Username_FF = "ta_SNMMI_PhysicianMember1_FF";
        public static string SNMMI_PhysicianMember1Email_FF = "ta_SNMMI_PhysicianMember1@mailinator.com";

        public static string SNMMI_PhysicianMember2Username = "ta_SNMMI_PhysicianMember2";
        public static string SNMMI_PhysicianMember2Email = "ta_SNMMI_PhysicianMember2@mailinator.com";
        public static string SNMMI_PhysicianMember2Username_Mobile = "ta_SNMMI_PhysicianMember2_Mobile";
        public static string SNMMI_PhysicianMember2Email_Mobile = "ta_SNMMI_PhysicianMember2@mailinator.com";
        public static string SNMMI_PhysicianMember2Username_IE = "ta_SNMMI_PhysicianMember2_IE";
        public static string SNMMI_PhysicianMember2Email_IE = "ta_SNMMI_PhysicianMember2@mailinator.com";
        public static string SNMMI_PhysicianMember2Username_FF = "ta_SNMMI_PhysicianMember2_FF";
        public static string SNMMI_PhysicianMember2Email_FF = "ta_SNMMI_PhysicianMember2@mailinator.com";

        public static string SNMMI_PhysicianMember3Username = "ta_SNMMI_PhysicianMember3";
        public static string SNMMI_PhysicianMember3Email = "ta_SNMMI_PhysicianMember3@mailinator.com";
        public static string SNMMI_PhysicianMember3Username_Mobile = "ta_SNMMI_PhysicianMember3_Mobile";
        public static string SNMMI_PhysicianMember3Email_Mobile = "ta_SNMMI_PhysicianMember3@mailinator.com";
        public static string SNMMI_PhysicianMember3Username_IE = "ta_SNMMI_PhysicianMember3_IE";
        public static string SNMMI_PhysicianMember3Email_IE = "ta_SNMMI_PhysicianMember3@mailinator.com";
        public static string SNMMI_PhysicianMember3Username_FF = "ta_SNMMI_PhysicianMember3_FF";
        public static string SNMMI_PhysicianMember3Email_FF = "ta_SNMMI_PhysicianMember3@mailinator.com";

        // Compatible portals: DHMC, 
        // Profession 1
        public static string DHMC_Physician1Username = "TA_DHMC_Physician1@mailinator.com";
        public static string DHMC_Physician1Email = "TA_DHMC_Physician1@mailinator.com";
        public static string DHMC_Physician1Username_Mobile = "TA_DHMC_Physician1_Mobile@mailinator.com";
        public static string DHMC_Physician1Email_Mobile = "ta_DHMC_Physician1@mailinator.com";
        public static string DHMC_Physician1Username_IE = "TA_DHMC_Physician1_IE@mailinator.com";
        public static string DHMC_Physician1Email_IE = "ta_DHMC_Physician1@mailinator.com";
        public static string DHMC_Physician1Username_FF = "TA_DHMC_Physician1_FF@mailinator.com";
        public static string DHMC_Physician1Email_FF = "ta_DHMC_Physician1@mailinator.com";

        public static string DHMC_Physician2Username = "TA_DHMC_Physician2@mailinator.com";
        public static string DHMC_Physician2Email = "ta_DHMC_Physician2@mailinator.com";
        public static string DHMC_Physician2Username_Mobile = "TA_DHMC_Physician2_Mobile@mailinator.com";
        public static string DHMC_Physician2Email_Mobile = "ta_DHMC_Physician2@mailinator.com";
        public static string DHMC_Physician2Username_IE = "TA_DHMC_Physician2_IE@mailinator.com";
        public static string DHMC_Physician2Email_IE = "ta_DHMC_Physician2@mailinator.com";
        public static string DHMC_Physician2Username_FF = "TA_DHMC_Physician2_FF@mailinator.com";
        public static string DHMC_Physician2Email_FF = "ta_DHMC_Physician2@mailinator.com";

        public static string DHMC_Physician3Username = "TA_DHMC_Physician3@mailinator.com";
        public static string DHMC_Physician3Email = "ta_DHMC_Physician3@mailinator.com";
        public static string DHMC_Physician3Username_Mobile = "TA_DHMC_Physician3_Mobile@mailinator.com";
        public static string DHMC_Physician3Email_Mobile = "ta_DHMC_Physician3@mailinator.com";
        public static string DHMC_Physician3Username_IE = "TA_DHMC_Physician3_IE@mailinator.com";
        public static string DHMC_Physician3Email_IE = "ta_DHMC_Physician3@mailinator.com";
        public static string DHMC_Physician3Username_FF = "TA_DHMC_Physician3_FF@mailinator.com";
        public static string DHMC_Physician3Email_FF = "ta_DHMC_Physician3@mailinator.com";

        public static string DHMC_Physician4Username = "TA_DHMC_Physician4@mailinator.com";
        public static string DHMC_Physician4Email = "ta_DHMC_Physician4@mailinator.com";
        public static string DHMC_Physician4Username_Mobile = "TA_DHMC_Physician4_Mobile@mailinator.com";
        public static string DHMC_Physician4Email_Mobile = "ta_DHMC_Physician4@mailinator.com";
        public static string DHMC_Physician4Username_IE = "TA_DHMC_Physician4_IE@mailinator.com";
        public static string DHMC_Physician4Email_IE = "ta_DHMC_Physician4@mailinator.com";
        public static string DHMC_Physician4Username_FF = "TA_DHMC_Physician4_FF@mailinator.com";
        public static string DHMC_Physician4Email_FF = "ta_DHMC_Physician4@mailinator.com";

        // Profession 2
        public static string DHMC_Pharmacist1Username = "TA_DHMC_Pharmacist1@mailinator.com";
        public static string DHMC_Pharmacist1Email = "ta_DHMC_Pharmacist1@mailinator.com";
        public static string DHMC_Pharmacist1Username_Mobile = "TA_DHMC_Pharmacist1_Mobile@mailinator.com";
        public static string DHMC_Pharmacist1Email_Mobile = "ta_DHMC_Pharmacist1@mailinator.com";
        public static string DHMC_Pharmacist1Username_IE = "TA_DHMC_Pharmacist1_IE@mailinator.com";
        public static string DHMC_Pharmacist1Email_IE = "ta_DHMC_Pharmacist1@mailinator.com";
        public static string DHMC_Pharmacist1Username_FF = "TA_DHMC_Pharmacist1_FF@mailinator.com";
        public static string DHMC_Pharmacist1Email_FF = "ta_DHMC_Pharmacist1@mailinator.com";

        public static string DHMC_Pharmacist2Username = "TA_DHMC_Pharmacist2@mailinator.com";
        public static string DHMC_Pharmacist2Email = "ta_DHMC_Pharmacist2@mailinator.com";
        public static string DHMC_Pharmacist2Username_Mobile = "TA_DHMC_Pharmacist2_Mobile@mailinator.com";
        public static string DHMC_Pharmacist2Email_Mobile = "ta_DHMC_Pharmacist2@mailinator.com";
        public static string DHMC_Pharmacist2Username_IE = "TA_DHMC_Pharmacist2_IE@mailinator.com";
        public static string DHMC_Pharmacist2Email_IE = "ta_DHMC_Pharmacist2@mailinator.com";
        public static string DHMC_Pharmacist2Username_FF = "TA_DHMC_Pharmacist2_FF@mailinator.com";
        public static string DHMC_Pharmacist2Email_FF = "ta_DHMC_Pharmacist2@mailinator.com";

        #endregion static users


        #endregion properties

        #region methods

        public static UserModel AddUserInfo(string username, string email, string profession)
        {
            UserModel newUserModel = new UserModel();

            newUserModel.Username = username;
            newUserModel.EmailAddress = email;
            newUserModel.Profession = profession;

            return newUserModel;
        }

        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="siteCode"><see cref="Constants.SiteCodes"/></param>
        /// <param name="userName">(Optional) If needed, you can specify a username of your choice. If not needed, leave this null and the username will be generated for you</param>
        /// <param name="emailAddress">(Optional) If needed, you can specify an email of your choice. If not needed, leave this null and the username will be generated for you</param>
        /// <param name="firstName">(Optional) If needed, you can specify a first name of your choice. If not needed, leave this null and the first name will be generated for you</param>
        /// <param name="lastName">(Optional) If needed, you can specify a last name of your choice. If not needed, leave this null and the last name will be generated for you</param>
        /// <param name="profCode"><see cref="Constants.ProfessionCode"/> By default, it will make the user a UAMS_Physician</param>
        /// <returns>An object that contains all the user's information, such as username, full name (first and last name), email, etc.</returns>
        public static UserModel CreateUser(Constants.SiteCodes siteCode, string userName = "", string emailAddress = "", string
            firstName = null, string lastName = null, Constants.ProfessionCode profCode = Constants.ProfessionCode.Physician_PHY,
            bool Member = false)
        {
            UserModel newUserModel = BuildUserModel(siteCode, userName, emailAddress, firstName, lastName, profCode, Member);

            if (AppSettings.Config["APIUrl"] == null) throw new Exception("Appsetting 'APIUrl' is missing.");

            String fullAPIUrl = String.Format("{0}api/user", AppSettings.Config["APIUrl"].ToString());

            string body = JsonConvert.SerializeObject(newUserModel);

            string token = APIHelp.GetToken(
                AppSettings.Config["APIUrl"].ToString(),
                siteCode.ToString(),
                string.Format("{0}_{1}", AppSettings.Config["APIAccountKey"], siteCode),
                AppSettings.Config["APIAccountPassword"]);

            string resp = APIUtils.ExecuteAPI_Post(fullAPIUrl, body, token);

            // Add the guid to the UserModel object
            dynamic data = JObject.Parse(resp);
            newUserModel.Guid = data.Id.ToString();

            return newUserModel;
        }

        /// <summary>
        /// Builds the user information that then gets plugged into the API request's body
        /// </summary>
        /// <param name="siteCode"><see cref="Constants.SiteCodes"/></param>
        /// <param name="userName">(Optional) If needed, you can specify a username of your choice. If not needed, leave this null and the username will be generated for you</param>
        /// <param name="emailAddress">(Optional) If needed, you can specify an email of your choice. If not needed, leave this null and the username will be generated for you</param>
        /// <param name="firstName">(Optional) If needed, you can specify a first name of your choice. If not needed, leave this null and the first name will be generated for you</param>
        /// <param name="lastName">(Optional) If needed, you can specify a last name of your choice. If not needed, leave this null and the last name will be generated for you</param>
        /// <param name="profCode"><see cref="Constants.ProfessionCode"/> By default, it will make the user a UAMS_Physician</param>
        /// <returns></returns>
        private static UserModel BuildUserModel(Constants.SiteCodes siteCode, string userName = "", string emailAddress = "",
            string firstName = null, string lastName = null, Constants.ProfessionCode profCode = Constants.ProfessionCode.Physician_PHY,
            bool Member = false)
        {
            string MemberStatus = Member ? "Member" : "Non-Member";

            string nationality = "";
            string currentDate = string.Format("{0}_", DateTime.Now.ToString("MdyyyyHHmmss", CultureInfo.InvariantCulture));
            string randomUsername = "TestAuto_" + currentDate + DataUtils.GetRandomString(3);

            // Set the email address, user name, first and last name
            emailAddress = string.IsNullOrEmpty(emailAddress) ? "blah@mailinator.com" : emailAddress;
            userName = string.IsNullOrEmpty(userName) ? randomUsername : userName;
            firstName = string.IsNullOrEmpty(firstName) ? randomUsername.Substring(0, 11) : firstName;
            lastName = string.IsNullOrEmpty(lastName) ? randomUsername.Substring(11) : lastName;

            UserModel newUserModel = new UserModel();
            newUserModel.Username = userName;
            newUserModel.FirstName = firstName;
            newUserModel.LastName = lastName;
            newUserModel.FullName = string.Format("{0} {1}", newUserModel.FirstName, newUserModel.LastName);
            newUserModel.Degree = "test";
            newUserModel.EmailAddress = emailAddress;
            newUserModel.Address = "Address01";
            newUserModel.Address2 = "Address02";
            newUserModel.City = "Pittsburgh";
            newUserModel.State = "PA";
            newUserModel.PostalCode = "15206";
            newUserModel.CountryCode = "US";
            switch (siteCode)
            {
                case Constants.SiteCodes.ACR:
                    newUserModel.Password = Password;
                    newUserModel.Fields = new Field[] {
                new Field() { Name = "profession", Value = profCode.GetDescription() } };
                    break;

                case Constants.SiteCodes.AHA:
                    newUserModel.Password = Password;
                    newUserModel.Fields = new Field[] {
                new Field() { Name = "profession", Value = profCode.GetDescription() },
                new Field() { Name = "AHA_CERTIFICATION", Value = "" },
                new Field() { Name = "Member_Status", Value = MemberStatus },
                new Field() { Name = "MEMBERSHIP_LEVEL", Value = "" },
                new Field() { Name = "PARTICIPANT_ID", Value = userName } };
                    break;

                case Constants.SiteCodes.ASNC:
                    newUserModel.Password = Password;
                    newUserModel.Fields = new Field[] {
                new Field() { Name = "profession", Value = profCode.GetDescription() },
                new Field() { Name = "ASNC_Member_Status", Value = "Full Domestic Member" },
                new Field() { Name = "PARTICIPANT_ID", Value = userName } };
                    break;

                case Constants.SiteCodes.CAP:
                    newUserModel.Password = Password;
                    newUserModel.Fields = new Field[] {
                new Field() { Name = "profession", Value = profCode.GetDescription() },
                new Field() { Name = "active_status_v2", Value = "Active" },
                new Field() { Name = "Member_Status", Value = "Member" },
                new Field() { Name = "Participant_Status", Value = "OT" },
                new Field() { Name = "PARTICIPANT_ID", Value = userName } };
                    break;

                case Constants.SiteCodes.CMECA:
                    newUserModel.Password = Password_AllCaps;
                    newUserModel.Fields = new Field[] {
                new Field() { Name = "profession", Value = profCode.GetDescription() },
                new Field() { Name = "ceca_specialty", Value = "Pathology" },
                new Field() { Name = "PARTICIPANT_ID", Value = userName },
                new Field() { Name = "ceca_professional_designation", Value = "MD" } };
                    break;

                case Constants.SiteCodes.DHMC:
                    newUserModel.Password = Password;
                    newUserModel.Fields = new Field[] {
                new Field() { Name = "profession", Value = profCode.GetDescription() },
                new Field() { Name = "active_status_v2", Value = "Active" },
                new Field() { Name = "Member_Status", Value = "Member" },
                new Field() { Name = "Participant_Status", Value = "OT" },
                new Field() { Name = "PARTICIPANT_ID", Value = userName },
                new Field() { Name = "dhmc__work_city", Value = "jhkjh" },
                new Field() { Name = "dhmc__work_state", Value = "AZ" },
                new Field() { Name = "dhmc_preferred_address", Value = "Work" },
                new Field() { Name = "PRACTICE_ORGANIZATION", Value = "fdfghdg" } };
                    break;

                case Constants.SiteCodes.ONSLT:
                    newUserModel.Password = Password;
                    // If the tester did not specify a profession, we must set the profession to a ONS compatible profession. There
                    // are no physicians in ONS, so switch the default profCode value to Nurse Practicioner
                    profCode = profCode.Equals(Constants.ProfessionCode.Physician_PHY) ? Constants.ProfessionCode.NursePracticioner_NPR
                        : profCode;

                    newUserModel.Fields = new Field[] {
                new Field() { Name = "profession", Value = profCode.GetDescription() },
                new Field() { Name = "PARTICIPANT_ID", Value = userName },
                new Field() { Name = "PRACTICE_ORGANIZATION", Value = "organization" },
                new Field() { Name = "ONS_Reg_Are_You_Oncology_Nurse", Value = "Yes" },
                new Field() { Name = "ONS_Reg_Primary_Position", Value = "Academic Educator" },
                new Field() { Name = "ONS_Reg_Primary_Specialty", Value = "Blood & Marrow Transplant" },
                new Field() { Name = "ONS_Reg_Primary_Work_Setting", Value = "Bone Marrow Transplant Unit" } };
                    break;

                case Constants.SiteCodes.SNMMI:
                    newUserModel.Password = Password;
                    newUserModel.Fields = new Field[] {
                new Field() { Name = "profession", Value = profCode.GetDescription() },
                new Field() { Name = "SNMMI_Member_Status", Value = MemberStatus },
                new Field() { Name = "PARTICIPANT_ID", Value = userName } };
                    break;

                case Constants.SiteCodes.UAMS:
                    newUserModel.Password = Password;
                    newUserModel.Fields = new Field[] {
                new Field() { Name = "profession", Value = profCode.GetDescription() },
                new Field() { Name = "language", Value = nationality },
                new Field() { Name = "uams_hspanic_latino", Value = "No" },
                new Field() { Name = "PARTICIPANT_ID", Value = userName },
                new Field() { Name = "Specialty", Value = "Phy" },
                new Field() { Name = "CHES Certified", Value = "No" },
                new Field() { Name = "CHES ID", Value = DataUtils.GetRandomString(10) },
                new Field() { Name = "uams_counties", Value = "Arkansas" },
                new Field() { Name = "PRACTICE_ORGANIZATION", Value = "organization" },
                new Field() { Name = "uams_ethnicity", Value = "1" },
                new Field() { Name = "uams_employee_nonemployee", Value = "1" },
                new Field() { Name = "uams_division", Value = "division" },
                new Field() { Name = "uams_department", Value = "department" },
                new Field() { Name = "uams_primary_functional_role", Value = "1" },
                new Field() { Name = "uams_badge_number", Value = "12345" },
                new Field() { Name = "uams_sap_number", Value = "12345" },
                new Field() { Name = "UAMS - Credit Type", Value = "AMA PRA Category 1" },
                new Field() { Name = "uams_position", Value = "12345" } };
                    break;
            }


            return newUserModel;
        }

        #endregion methods


    }
}
