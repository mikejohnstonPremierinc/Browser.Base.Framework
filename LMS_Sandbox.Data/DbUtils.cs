using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using Browser.Core.Framework;

namespace LMS.Data
{
    /// <summary>
    /// Queries/scripts for all core-platform LMS applications
    /// </summary>
    public static class DBUtils
    {
        #region properties

        private static DataAccess _WebAppDbAccess = null;

        private static string p_SQLconnString = string.Empty;
        public static string SQLconnString
        {
            get
            {
                return p_SQLconnString;
            }
            set
            {
                p_SQLconnString = value;
                _WebAppDbAccess = new DataAccess(new SqlServerDataAccessProvider(value));

            }
        }

        #endregion properties

        #region methods

        #region user methods

        /// <summary>
        /// Changes the username for a user
        /// </summary>
        /// <param name="siteCode"><see cref="Constants.SiteCodes"/></param>
        /// <param name="currentUsername"></param>
        /// <param name="newUsername"></param>
        /// <returns></returns>
        public static void ChangeEmail(Constants.SiteCodes siteCode, string currentUsername, string newUsername)
        {
            string siteSeq = GetSiteSequence(siteCode).ToString();

            string query = string.Format(@"
DECLARE @username varchar(100), @siteID int, @newEmail varchar(100), @newUserName varchar(100),@apFormInstance int, @partPortalFormInstance int, @partOrgFormInstance int

SET @siteID={0}
SET @username='{1}'
SET @newEmail='{2}'

UPDATE p
SET Email = @newEmail
FROM participant p
JOIN FRMK_MemberLinks ml ON ml.ParticipantID = p.participant_seq AND ml.Temporary=0
JOIN FRMK_Member m ON m.MemberID = ml.MemberID
WHERE m.Username = @username AND m.SiteID = @siteID

UPDATE m
SET email = @newEmail
FROM participant p
JOIN FRMK_MemberLinks ml ON ml.ParticipantID = p.participant_seq AND ml.Temporary=0
JOIN FRMK_Member m ON m.MemberID = ml.MemberID
WHERE m.Username = @username AND m.SiteID = @siteID


select @partPortalFormInstance = max(pfi.instance_seq) 
from participant_form_instance pfi with(nolock) 
join form_instance fi with(nolock) on pfi.instance_seq=fi.instance_seq
JOIN object o ON pfi.object_seq = o.object_seq
JOIN participant p ON p.participant_seq = pfi.participant_seq
JOIN FRMK_MemberLinks ml ON ml.ParticipantID = p.participant_seq AND ml.Temporary=0
JOIN FRMK_Member m ON m.MemberID = ml.MemberID
where m.Username = @username AND m.SiteID = @siteID AND o.object_type_seq=96

select @partOrgFormInstance = max(pfi.instance_seq) 
from participant_form_instance pfi with(nolock) 
join form_instance fi with(nolock) on pfi.instance_seq=fi.instance_seq
JOIN object o ON pfi.object_seq = o.object_seq
JOIN participant p ON p.participant_seq = pfi.participant_seq
JOIN FRMK_MemberLinks ml ON ml.ParticipantID = p.participant_seq AND ml.Temporary=0
JOIN FRMK_Member m ON m.MemberID = ml.MemberID
where m.Username = @username AND m.SiteID = @siteID AND o.object_type_seq=64


UPDATE form_field_text_data
SET data_value = @newEmail, LastModifiedDate=GETDATE()
WHERE instance_seq = @partOrgFormInstance AND data_field_seq=262

UPDATE form_field_text_data
SET data_value = @newEmail, LastModifiedDate = GETDATE()
WHERE instance_seq = @partPortalFormInstance AND data_field_seq=262




DECLARE @activityPart int, @instanceId int
DECLARE @apInstanceTable Table
(
	activityPartID int,
	formInstance int
)

INSERT INTO @apInstanceTable(activityPartID, formInstance)
select max(pfi.instance_seq) [activityPartID], ap.activity_participant_seq 
from activity_participant_form_instance pfi with(nolock) 
join form_instance fi with(nolock) on pfi.instance_seq=fi.instance_seq
JOIN activity_participant ap ON ap.activity_participant_seq = pfi.activity_participant_seq
JOIN participant p ON p.participant_seq = ap.participant_seq
JOIN FRMK_MemberLinks ml ON ml.ParticipantID = p.participant_seq AND ml.Temporary=0
JOIN FRMK_Member m ON m.MemberID = ml.MemberID
where m.Username = @username AND m.SiteID = @siteID
GROUP BY ap.activity_participant_seq


WHILE exists (SELECT 'x' FROM @apInstanceTable)
BEGIN
	SELECT top 1 @activityPart = activityPartID, @instanceId = formInstance FROM @apInstanceTable

	UPDATE form_field_text_data
	SET data_value = @newEmail, LastModifiedDate=GETDATE()
	WHERE instance_seq = @instanceId
	AND data_field_seq=262

	DELETE @apInstanceTable WHERE activityPartID = @activityPart AND formInstance = @instanceId
END
", siteSeq, currentUsername, newUsername);

            _WebAppDbAccess.ExecuteNonQuery(query, 120);
        }

        /// <summary>
        /// Changes the username for a user
        /// </summary>
        /// <param name="siteCode"><see cref="Constants.SiteCodes"/></param>
        /// <param name="currentUsername"></param>
        /// <param name="newUsername"></param>
        /// <returns></returns>
        public static void ChangeUserName(Constants.SiteCodes siteCode, string currentUsername, string newUsername)
        {
            string siteSeq = GetSiteSequence(siteCode).ToString();

            string query = string.Format(@"
DECLARE @username varchar(100), @siteID int, @newEmail varchar(100), @newUserName varchar(100),@apFormInstance int, @partPortalFormInstance int, @partOrgFormInstance int

SET @siteID={0}
SET @username='{1}'
SET @newUserName='{2}'

UPDATE m
SET Username = @newUserName, LoweredUsername = LOWER(@newUserName)
FROM participant p
JOIN FRMK_MemberLinks ml ON ml.ParticipantID = p.participant_seq AND ml.Temporary=0
JOIN FRMK_Member m ON m.MemberID = ml.MemberID
WHERE m.Username = @username AND m.SiteID = @siteID

UPDATE sp
SET Username = @newUserName
FROM participant p
JOIN site_participant sp ON p.participant_seq = sp.participant_seq
WHERE sp.Username = @username AND sp.Site_seq = @siteID


select @partPortalFormInstance = max(pfi.instance_seq) 
from participant_form_instance pfi with(nolock) 
join form_instance fi with(nolock) on pfi.instance_seq=fi.instance_seq
JOIN object o ON pfi.object_seq = o.object_seq
JOIN participant p ON p.participant_seq = pfi.participant_seq
JOIN FRMK_MemberLinks ml ON ml.ParticipantID = p.participant_seq AND ml.Temporary=0
JOIN FRMK_Member m ON m.MemberID = ml.MemberID
where m.Username = @username AND m.SiteID = @siteID AND o.object_type_seq=96

select @partOrgFormInstance = max(pfi.instance_seq) 
from participant_form_instance pfi with(nolock) 
join form_instance fi with(nolock) on pfi.instance_seq=fi.instance_seq
JOIN object o ON pfi.object_seq = o.object_seq
JOIN participant p ON p.participant_seq = pfi.participant_seq
JOIN FRMK_MemberLinks ml ON ml.ParticipantID = p.participant_seq AND ml.Temporary=0
JOIN FRMK_Member m ON m.MemberID = ml.MemberID
where m.Username = @username AND m.SiteID = @siteID AND o.object_type_seq=64


UPDATE form_field_text_data
SET data_value = @newEmail, LastModifiedDate=GETDATE()
WHERE instance_seq = @partOrgFormInstance AND data_field_seq=262

UPDATE form_field_text_data
SET data_value = @newEmail, LastModifiedDate = GETDATE()
WHERE instance_seq = @partPortalFormInstance AND data_field_seq=262




DECLARE @activityPart int, @instanceId int
DECLARE @apInstanceTable Table
(
	activityPartID int,
	formInstance int
)

INSERT INTO @apInstanceTable(activityPartID, formInstance)
select max(pfi.instance_seq) [activityPartID], ap.activity_participant_seq 
from activity_participant_form_instance pfi with(nolock) 
join form_instance fi with(nolock) on pfi.instance_seq=fi.instance_seq
JOIN activity_participant ap ON ap.activity_participant_seq = pfi.activity_participant_seq
JOIN participant p ON p.participant_seq = ap.participant_seq
JOIN FRMK_MemberLinks ml ON ml.ParticipantID = p.participant_seq AND ml.Temporary=0
JOIN FRMK_Member m ON m.MemberID = ml.MemberID
where m.Username = @username AND m.SiteID = @siteID
GROUP BY ap.activity_participant_seq


WHILE exists (SELECT 'x' FROM @apInstanceTable)
BEGIN
	SELECT top 1 @activityPart = activityPartID, @instanceId = formInstance FROM @apInstanceTable

	UPDATE form_field_text_data
	SET data_value = @newEmail, LastModifiedDate=GETDATE()
	WHERE instance_seq = @instanceId
	AND data_field_seq=262

	DELETE @apInstanceTable WHERE activityPartID = @activityPart AND formInstance = @instanceId
END
", siteSeq, currentUsername, newUsername);

            _WebAppDbAccess.ExecuteNonQuery(query, 120);
        }

        /// <summary>
        /// If its a new user, then we have to disable the security question, which sometimes appears in different environments
        /// ToDo: Get DEV to turn this off site-wide instead of running this query all the time
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="siteCode"><see cref="Constants.SiteCodes"/></param>
        public static void SetSecurityQuestionAndPasswordToFalse(string userName, Constants.SiteCodes siteCode)
        {
            string siteSeq = GetSiteSequence(siteCode).ToString();

            string query = string.Format(@"
UPDATE FRMK_Member SET ForceSecurityQuestion=0, ForcePasswordChange=0 WHERE Username='{0}' AND SiteID={1}
", userName, siteSeq);

            _WebAppDbAccess.ExecuteNonQuery(query, 120);
        }

        /// <summary>
        /// If its a new user, then we have to disable the force change password, which sometimes appears in different environments
        /// ToDo: Get DEV to turn this off site-wide instead of running this query all the time
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="siteCode"><see cref="Constants.SiteCodes"/></param>
        public static void SetForcePasswordToFalse(string userName, Constants.SiteCodes siteCode)
        {
            string siteSeq = GetSiteSequence(siteCode).ToString();

            string query = string.Format(@"
UPDATE FRMK_Member SET ForcePasswordChange=0 WHERE Username='{0}' AND SiteID={1}
", userName, siteSeq);

            _WebAppDbAccess.ExecuteNonQuery(query, 120);
        }

        /// <summary>
        /// Changes the username for a user
        /// </summary>
        /// <param name="siteCode"><see cref="Constants.SiteCodes"/></param>
        /// <param name="username"></param>
        /// <param name="participantCode"></param>
        /// <returns></returns>
        public static void ChangeParticipantCode(Constants.SiteCodes siteCode, string username, string participantCode)
        {
            string siteSeq = GetSiteSequence(siteCode).ToString();

            string query = string.Format(@"DECLARE @username varchar(100), @siteID int, @newEmail varchar(100), @participantcode varchar(100),@apFormInstance int, @partPortalFormInstance int, @partOrgFormInstance int

SET @siteID={0}
SET @username='{1}'
SET @participantcode='{2}'

UPDATE p
SET participant_code = @participantcode
FROM participant p
JOIN FRMK_MemberLinks ml ON ml.ParticipantID = p.participant_seq AND ml.Temporary=0
JOIN FRMK_Member m ON m.MemberID = ml.MemberID
WHERE m.Username = @username AND m.SiteID = @siteID


select @partPortalFormInstance = max(pfi.instance_seq) 
from participant_form_instance pfi with(nolock) 
join form_instance fi with(nolock) on pfi.instance_seq=fi.instance_seq
JOIN object o ON pfi.object_seq = o.object_seq
JOIN participant p ON p.participant_seq = pfi.participant_seq
JOIN FRMK_MemberLinks ml ON ml.ParticipantID = p.participant_seq AND ml.Temporary=0
JOIN FRMK_Member m ON m.MemberID = ml.MemberID
where m.Username = @username AND m.SiteID = @siteID AND o.object_type_seq=96

select @partOrgFormInstance = max(pfi.instance_seq) 
from participant_form_instance pfi with(nolock) 
join form_instance fi with(nolock) on pfi.instance_seq=fi.instance_seq
JOIN object o ON pfi.object_seq = o.object_seq
JOIN participant p ON p.participant_seq = pfi.participant_seq
JOIN FRMK_MemberLinks ml ON ml.ParticipantID = p.participant_seq AND ml.Temporary=0
JOIN FRMK_Member m ON m.MemberID = ml.MemberID
where m.Username = @username AND m.SiteID = @siteID AND o.object_type_seq=64


UPDATE form_field_text_data
SET data_value = @newEmail, LastModifiedDate=GETDATE()
WHERE instance_seq = @partOrgFormInstance AND data_field_seq=262

UPDATE form_field_text_data
SET data_value = @newEmail, LastModifiedDate = GETDATE()
WHERE instance_seq = @partPortalFormInstance AND data_field_seq=262


DECLARE @activityPart int, @instanceId int
DECLARE @apInstanceTable Table
(
       activityPartID int,
       formInstance int
)

INSERT INTO @apInstanceTable(activityPartID, formInstance)
select max(pfi.instance_seq) [activityPartID], ap.activity_participant_seq 
from activity_participant_form_instance pfi with(nolock) 
join form_instance fi with(nolock) on pfi.instance_seq=fi.instance_seq
JOIN activity_participant ap ON ap.activity_participant_seq = pfi.activity_participant_seq
JOIN participant p ON p.participant_seq = ap.participant_seq
JOIN FRMK_MemberLinks ml ON ml.ParticipantID = p.participant_seq AND ml.Temporary=0
JOIN FRMK_Member m ON m.MemberID = ml.MemberID
where m.Username = @username AND m.SiteID = @siteID
GROUP BY ap.activity_participant_seq


WHILE exists (SELECT 'x' FROM @apInstanceTable)
BEGIN
       SELECT top 1 @activityPart = activityPartID, @instanceId = formInstance FROM @apInstanceTable

       UPDATE form_field_text_data
       SET data_value = @newEmail, LastModifiedDate=GETDATE()
       WHERE instance_seq = @instanceId
       AND data_field_seq=262

       DELETE @apInstanceTable WHERE activityPartID = @activityPart AND formInstance = @instanceId
END
", siteSeq, username, participantCode);

            _WebAppDbAccess.ExecuteNonQuery(query, 120);
        }

        /// <summary>
        /// Gets the Activity Participant Sequence by the activity ID and participant sequence
        /// </summary>
        /// <param name="activityIDOrSequence">The activity ID of the activity. To get this, <see cref="GetActivityID(string)(string, string, string)"/></param>
        /// <param name="participantSequence">The participant ID or sequence. To get this, <see cref="GetParticipantSequence(string)"/></param>
        /// <returns></returns>
        public static int GetActivityParticipantSequence(string activityIDOrSequence, string participantSequence)
        {
            string query = string.Format(@"SELECT	ap.activity_participant_seq
FROM	dbo.activity_participant ap WITH (NOLOCK)
		INNER JOIN dbo.activity_participant_instance api WITH (NOLOCK) ON
			ap.activity_participant_instance_seq =api.activity_participant_instance_seq AND api.isCurrent=1
WHERE	ap.activity_seq = {0}
		AND	ap.participant_seq = {1}", activityIDOrSequence, participantSequence);

            return _WebAppDbAccess.GetDataValue<int>(query, 120);
        }

        /// <summary>
        /// Gets the latest adjustment that was applied to the participant based on username. i.e. 
        /// A-Active (Default), AF-Affiliate (Default), etc.
        /// </summary>
        /// <param name="username"></param>
        public static string GetParticpantLatestAdjustmentCode(string username)
        {
            string query = string.Format(@"SELECT top 1 rra.DisplayLabel FROM dbo.RKG_ObjectRecognitionAdjustment rora
INNER JOIN dbo.RKG_ObjectRecognition ror ON rora.RKG_ObjectRecognitionId = ror.RKG_ObjectRecognitionId
INNER JOIN dbo.RKG_RecognitionAdjustment rra ON rora.RKG_RecognitionAdjustmentId = rra.RKG_RecognitionAdjustmentId
INNER JOIN dbo.participant p ON p.object_seq = ror.object_seq
AND p.participant_code = '{0}'
ORDER BY rora.CreateDate DESC", username);

            return _WebAppDbAccess.GetDataValue<string>(query, 120);
        }

        /// <summary>
        /// Get the user's current total applied/validated credits for either AMA or RCP activities
        /// </summary>
        /// <param name="username"></param>
        /// <param name="amaOrRCP">Either "AMA" or "RCP"</param>
        /// <returns></returns>
        public static int GetParticpantsCurrentAppliedAMARCPCredits(string username, string amaOrRCP)
        {
            string amaOrRCPWhereClause = null;
            if (amaOrRCP == "AMA")
            {
                amaOrRCPWhereClause = "((r.RecognitionCode IN ('CFPC_MOC_GL_CA_APRA', 'CFPC_MOC_SL_CA_APRA')))";
            }
            else
            {
                amaOrRCPWhereClause = "((r.RecognitionCode IN ('CFPC_MOC_GL_CA_RCMC','CFPC_MOC_ASM_CA_RCMC')))";
            }

            string query = string.Format(@"		SELECT
			ISNULL(SUM(orr.ActualRequirementCount),0) As CreditTotal
       -- CASE WHEN r.RecognitionCode IN ('CFPC_MOC_GL_CA_APRA', 'CFPC_MOC_SL_CA_APRA') THEN 'AMA'
		-- WHEN r.RecognitionCode IN ('CFPC_MOC_GL_CA_RCMC','CFPC_MOC_ASM_CA_RCMC') THEN 'RCP' END As AmaOrRCP
    FROM
        RKG_ObjectRecognitionRequirement orr WITH (NOLOCK)
        INNER JOIN RKG_ObjectRecognition ror WITH (NOLOCK) ON orr.RKG_ObjectRecognitionId = ror.RKG_ObjectRecognitionId
        INNER JOIN RKG_Recognition r WITH (NOLOCK) ON ror.RKG_RecognitionId = r.RKG_RecognitionId
        INNER JOIN (
                SELECT ror.RKG_ObjectRecognitionId, rori.RKG_ObjectRecognitionInstanceId
                FROM dbo.RKG_ObjectRecognitionInstance rori
                INNER JOIN dbo.RKG_ObjectRecognition ror ON rori.RKG_ObjectRecognitionInstanceId = ror.RKG_ObjectRecognitionInstanceId AND rori.RKG_RecognitionId = ror.RKG_RecognitionId AND rori.IsCurrent = 1
                INNER JOIN dbo.participant p ON p.object_seq= ror.object_seq AND p.organization_seq = 926
                JOIN site_participant sp ON sp.participant_seq = p.participant_seq AND sp.site_seq=7438
                INNER JOIN dbo.RKG_ObjectRecognitionAttributeInstance rorai ON ror.RKG_ObjectRecognitionId = rorai.RKG_ObjectRecognitionId AND RKG_ObjectRecognitionAttributeId = 23
                INNER JOIN dbo.RKG_ObjectRecognitionAttributeSupportToolValue rorastv ON rorai.[Value] = rorastv.[Value] AND rorastv.RKG_ObjectRecognitionAttributeSupportToolId = 7
                WHERE sp.username='{0}'
        )c ON c.RKG_ObjectRecognitionInstanceId = ror.RKG_ObjectRecognitionInstanceId
    WHERE {1}
         
		--(r.RecognitionCode IN ('CFPC_MOC_GL_CA_RCMC','CFPC_MOC_ASM_CA_RCMC')))
        GROUP BY CASE WHEN r.RecognitionCode IN ('CFPC_MOC_GL_CA_APRA', 'CFPC_MOC_SL_CA_APRA') THEN 'AMA' WHEN r.RecognitionCode IN ('CFPC_MOC_GL_CA_RCMC','CFPC_MOC_ASM_CA_RCMC') THEN 'RCP' END", 
        username, amaOrRCPWhereClause);

            int creditAmount = Convert.ToInt32(_WebAppDbAccess.GetDataValue<decimal>(query, 120));
            return creditAmount;
        }

        /// <summary>
        /// Gets the latest cycle type that was applied to the participant based on username. i.e. Default/Voluntary/Resident
        /// in the Mainpro application
        /// </summary>
        /// <param name="username"></param>
        public static string GetParticpantLatestCycleType(string username)
        {
            string query = string.Format(@"			SELECT    --COALESCE(st.DisplayLabel,orad.[Name], ora.[Name]) AS [key],
                    COALESCE(stv.DisplayLabel,orai.[Value], orad.[DefaultValue], ora.[DefaultValue], '') AS [value]
            FROM    RKG_ObjectRecognitionAttribute ora
            INNER JOIN RKG_ObjectRecognitionAttributeSupportTool st on ora.RKG_ObjectRecognitionAttributeId = st.RKG_ObjectRecognitionAttributeId and st.RKG_RecognitionEntityId =32
            LEFT OUTER JOIN RKG_ObjectRecognitionAttributeInstance orai ON orai.RKG_ObjectRecognitionAttributeId = ora.RKG_ObjectRecognitionAttributeId
            LEFT OUTER JOIN RKG_ObjectRecognitionAttributeSupportToolValue stv on st.RKG_ObjectRecognitionAttributeSupportToolId=stv.RKG_ObjectRecognitionAttributeSupportToolId and isnull(orai.Value,ora.DefaultValue) = stv.value
            LEFT OUTER JOIN dbo.RKG_ObjectRecognitionAttributeDisplay orad WITH(NOLOCK) ON ora.RKG_ObjectRecognitionAttributeId = orad.RKG_ObjectRecognitionAttributeId AND orad.language_seq = 50
            JOIN (
                SELECT objr.islocked,objr.rkg_recognitionid,objr.rkg_objectrecognitioninstanceId, objr.Rkg_objectrecognitionid,objr.startdate,objr.enddate, m.Username
                FROM [RKG_Recognition]rc LEFT OUTER JOIN
                [RKG_Recognition]prc  on prc.RKG_RecognitionId=rc.ParentRecognitionId INNER JOIN
                [RKG_RecognitionRequirement] rr on rc.RKG_RecognitionId=rr.RKG_RecognitionId INNER JOIN
                [RKG_Requirement]r on rr.rkg_requirementid=r.rkg_requirementid  LEFT OUTER JOIN
                [dbo].[RKG_ObjectRecognition] objr on rc.RKG_RecognitionId=objr.RKG_RecognitionId
                JOIN [dbo].[RKG_ObjectRecognitionInstance] rori ON rori.RKG_ObjectRecognitionInstanceId = objr.RKG_ObjectRecognitionInstanceId AND rori.IsCurrent=1
                JOIN participant p ON p.object_seq = objr.object_seq
                JOIN FRMK_MemberLinks ml ON ml.ParticipantID = p.participant_seq
                JOIN FRMK_Member m ON m.MemberID = ml.MemberID AND m.SiteID=7438
                where objr.RKG_RecognitionId in (select RKG_RecognitionId  from RKG_Recognition where RecognitionCode in ('CFPC_MOC'))
            )u ON u.RKG_ObjectRecognitionId = orai.RKG_ObjectRecognitionId
            where COALESCE(orai.[Value], orad.[DefaultValue], ora.[DefaultValue], '')<>''
            AND COALESCE(st.DisplayLabel,orad.[Name], ora.[Name]) = 'Program'
            AND u.Username = '{0}'", username);

            return _WebAppDbAccess.GetDataValue<string>(query, 120);
        }

        /// <summary>
        /// Returns true or false depending on if a participant exists WHERE username =
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static bool ParticipantExists(string username)
        {
            string query = string.Format(@"if EXISTS(select m.Username from dbo.FRMK_Member m 
where m.Username = '{0}') select 1 as returnValue
Else(select 0 as returnValue);", username);

            int exists = _WebAppDbAccess.GetDataValue<int>(query, 120);

            if (exists == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Returns the full name of a user based on the username
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static string GetFullNameByUsername(string userName)
        {
            string query = string.Format(@"select 
p.first_name + ' ' + p.last_name
from frmk_user u
join frmk_memberlinks ml on ml.userid=u.userid
join frmk_member m on m.memberid=ml.memberid
join participant p on p.participant_seq=ml.participantid
where m.username = '{0}'", userName);

            DataAccess WebAppDbAccess = new DataAccess(new SqlServerDataAccessProvider(SQLconnString));

            return WebAppDbAccess.GetDataValue<string>(query, 90);
        }

        /// <summary>
        /// Removes the EULA popup after logging in with a new user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="siteCode"><see cref="Constants.SiteCodes"/></param>
        /// <returns></returns>
        public static void SetEULAToAccepted(string username, Constants.SiteCodes siteCode)
        {
            int participantSequence = GetParticipantSequence(username);
            int siteSequence = GetSiteSequence(siteCode);
            int siteEulaSequence = GetSiteEULASequenceBySiteSequence(siteSequence.ToString());

            string query = string.Format(@"IF NOT EXISTS(SELECT 1 FROM site_eula_accepted sa WHERE sa.participant_seq={0})
	INSERT INTO site_eula_accepted (participant_seq, site_eula_seq, date_accepted)
	VALUES ({0}, {1}, getdate())", participantSequence, siteEulaSequence);

            _WebAppDbAccess.ExecuteNonQuery(query, 120);
        }

        /// <summary>
        /// Returns the participant sequence/ID by username
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static int GetParticipantSequence(string username)
        {
            string query = string.Format(@"SELECT participant_seq FROM site_participant WHERE username='{0}'", username);

            return _WebAppDbAccess.GetDataValue<int>(query, 120);
        }

        #endregion user methods

        #region site methods

        /// <summary>
        /// Gets the EULA ID
        /// </summary>
        /// <param name="siteSequence"><see cref="GetSiteSequence"/></param>
        /// <returns></returns>
        public static int GetSiteEULASequenceBySiteSequence(string siteSequence)
        {
            string query = string.Format(@"select top 1 site_eula_seq
from site_eula 
where site_seq = {0}", siteSequence);

            return _WebAppDbAccess.GetDataValue<int>(query, 120);
        }

        /// <summary>
        /// Gets the site sequence from a user-specified site code
        /// </summary>
        /// <param name="siteCode"><see cref="Constants.SiteCodes"/></param>
        /// <returns></returns>
        public static int GetSiteSequence(Constants.SiteCodes siteCode)
        {
            string query = "";

            query = string.Format(@"select * 
from site_site 
where site_code = '{0}' 
and published_version_ind = 'N' 
and active_ind = 'Y' and deleted_ind = 'N'", siteCode.GetDescription());

            DataRow row = _WebAppDbAccess.GetDataRow(query, 120);

            return row.Field<int>("site_seq");
        }

        /// <summary>
        /// Gets the site Guid from a user-specified site code
        /// </summary>
        /// <param name="siteCode"><see cref="Constants.SiteCodes"/></param>
        /// <returns></returns>
        public static Guid GetSiteGuid(Constants.SiteCodes siteCode)
        {
            int siteID = GetSiteSequence(siteCode);

            string query = string.Format(@"select SiteGuid from vw_siteidentifiers where siteid={0}", siteID);

            return _WebAppDbAccess.GetDataValue<Guid>(query, 120);
        }

        /// <summary>
        /// Gets the org_code associated to the site
        /// </summary>
        /// <param name="siteCode"><see cref="Constants.SiteCodes"/></param>
        /// <returns></returns>
        public static string GetOrgCode(Constants.SiteCodes siteCode)
        {
            string query = string.Format(@"select o.org_code
from site_site ss
inner join dbo.organization o on ss.organization_seq = o.organization_seq
where ss.site_code = '{0}' 
and ss.published_version_ind = 'N' 
and ss.active_ind = 'Y' and ss.deleted_ind = 'N'", siteCode);

            return _WebAppDbAccess.GetDataValue<string>(query, 120);
        }

        #endregion site methods

        #region activity methods



        /// <summary>
        /// Gets the URL for a tester-specified content type
        /// </summary>
        /// <param name="siteCode"></param>
        /// <param name="actTitle"></param>
        /// <param name="contentTypeName"></param>
        /// <param name="field">The DB field you want to get for the content type</param>
        /// <returns></returns>
        public static string GetActivityContentTypeInfo(Constants.SiteCodes siteCode, string actTitle, Constants.ActContentType contentTypeName,
            string field)
        {
            string query = string.Format(@"SELECT ac.{0}
FROM activity a
INNER JOIN site_site_activity sa ON sa.activity_seq = a.activity_seq
INNER JOIN site_site s ON  s.site_seq = sa.site_seq
INNER JOIN activity_content ac ON ac.activity_seq = a.activity_seq
Where s.site_code = '{1}'
  and a.activity_status_code = 4
  and a.deleted_ind = 'N'
  and a.activity_title = '{2}'
  and sa.final_ind = 'Y'
AND ac.deleted_ind = 'N'
  AND ac.name = '{3}'", field, siteCode.GetDescription(), actTitle, contentTypeName.GetDescription());

            if (field == "display_order")
            {
                int displayOrder = _WebAppDbAccess.GetDataValue<int>(query, 120);
                return displayOrder.ToString();
            }
            else
            {
                return _WebAppDbAccess.GetDataValue<string>(query, 120);
            }
        }


        /// <summary>
        /// Gets the download filename for an activities content type
        /// </summary>
        /// <param name="siteCode"></param>
        /// <param name="actTitle"></param>
        /// <param name="contentTypeName"></param>
        /// <returns></returns>
        public static string GetActivityContentTypeFileName(Constants.SiteCodes siteCode, string actTitle,
            Constants.ActContentType contentTypeName)
        {
            int activity_seq = GetActivityID(siteCode, actTitle);

            string query = string.Format(@"SELECT fr.file_name FROM activity_content ac
JOIN file_repository fr ON ac.file_repository_file_seq = fr.file_seq
WHERE activity_seq = {0} 
and ac.deleted_ind = 'N'
AND ac.name = '{1}'", activity_seq, contentTypeName.GetDescription());

            return _WebAppDbAccess.GetDataValue<string>(query, 120);
        }

        /// <summary>
        /// Gets the text of required instructions for the Front Matter of an activity
        /// </summary>
        /// <param name="siteCode"></param>
        /// <param name="actTitle"></param>
        /// <returns></returns>
        public static string GetActivityFrontMatter(Constants.SiteCodes siteCode, string actTitle)
        {
            int activity_seq = GetActivityID(siteCode, actTitle);

            string query = string.Format(@"SELECT required_instruction
FROM dbo.award_template t   
INNER JOIN dbo.disclaimer d ON d.template_seq = t.award_template_seq 
WHERE activity_seq = {0}", activity_seq);

            return _WebAppDbAccess.GetDataValue<string>(query, 120);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="activityTitle"></param>
        /// <returns></returns>
        public static string GetAccessCode(string activityTitle)
        {
            string query = string.Format(@"declare @p_FormID int    
SELECT
   @p_FormID = acf.form_seq 
FROM
   dbo.object_access_code_form acf 
   inner join
      activity a 
      on acf.object_seq = a.object_seq 
WHERE
   a.activity_title = '{0}'       
      SELECT
                 top 1   coalesce(qd.choice_label_html, qds.choice_label_html, qc.choice_label) as choice_label
      FROM
             dbo.question_choices qc  with(nolock) 
         INNER JOIN
            question q with(nolock) 
            on q.question_seq = qc.question_seq 
         LEFT OUTER JOIN
            dbo.question_choices_display qd with(nolock) 
            on qc.question_seq = qd.question_seq 
            and qd.choice_num = qc.choice_num  
         LEFT OUTER JOIN
            dbo.question_choices_display_system qds with(nolock) 
            on q.question_type = qds.question_type_seq 
            and qds.choice_num = qc.choice_num  
      WHERE
            qc.is_active = 'Y' 
         AND      qc.question_seq in          (
         select
            df.question_seq           
         from
            dbo.data_field df with(nolock)           
            inner join
               dbo.form_data_field fdf with(nolock)              on df.data_field_seq = fdf.data_field_seq           
         where
            fdf.form_seq = @p_FormID)", activityTitle);

            return _WebAppDbAccess.GetDataValue<string>(query, 120);
        }


        /// <summary>
        /// Gets the text of required instructions for the Content Type (Activity Material page) of an activity
        /// </summary>
        /// <param name="siteCode"></param>
        /// <param name="actTitle"></param>
        /// <returns></returns>
        public static string GetActivityContentTypeCheckBoxText(Constants.SiteCodes siteCode, string actTitle)
        {
            int activity_seq = GetActivityID(siteCode, actTitle);

            string query = string.Format(@"SELECT RequiredInstruction 
FROM ACTIVITY_RequiredActivityContent 
WHERE activity_seq = {0}", activity_seq);

            return _WebAppDbAccess.GetDataValue<string>(query, 120);
        }

        /// <summary>
        /// Changes the display_order values of 2 content types in the activity_content table
        /// </summary>
        /// <param name="siteCode"><see cref="Constants.SiteCodes"/></param>
        /// <param name="actTitle">The activity title</param>
        /// <param name="content1"><see cref="Constants.ActContentType"/></param>
        /// <param name="content2"><see cref="Constants.ActContentType"/></param>

        public static void FlipOrderOf2ContentTypes(Constants.SiteCodes siteCode, string actTitle, Constants.ActContentType content1,
            Constants.ActContentType content2)
        {
            int activity_Seq = GetActivityID(siteCode, actTitle);

            string query = string.Format(@"Declare      
@content1 varchar(200) = '{0}',
@content2 varchar(200) = '{1}',
@activitySequence int = {2}

Declare
@content1OriginalOrder int = (select display_order from activity_content where activity_seq = @activitySequence and name = @content1),
@content2OriginalOrder int = (select display_order from activity_content where activity_seq = @activitySequence and name = @content2)


UPDATE activity_content SET display_order = @content2OriginalOrder WHERE name = @content1 AND activity_seq = @activitySequence;
UPDATE activity_content SET display_order = @content1OriginalOrder WHERE name = @content2 AND activity_seq = @activitySequence;",
content1.GetDescription(), content2.GetDescription(), activity_Seq);

            _WebAppDbAccess.ExecuteNonQuery(query, 120);
        }

        /// <summary>
        /// Returns a list of activity content file extensions in their respective database display_order
        /// </summary>
        /// <param name="siteCode"></param>
        /// <param name="actTitle"></param>
        /// <returns></returns>
        public static List<string> GetActivityContentList(Constants.SiteCodes siteCode, string actTitle)
        {
            string query = string.Format(@"SELECT ac.name
FROM activity a
INNER JOIN site_site_activity sa ON sa.activity_seq = a.activity_seq
INNER JOIN site_site s ON  s.site_seq = sa.site_seq
INNER JOIN activity_content ac ON ac.activity_seq = a.activity_seq
Where s.site_code = '{0}'
  and a.activity_status_code = 4
  and a.deleted_ind = 'N'
  and a.activity_title = '{1}'
  and sa.final_ind = 'Y'
  and ac.deleted_ind = 'N'
ORDER BY ac.display_order ASC;", siteCode.GetDescription(), actTitle);

            DataTable content = _WebAppDbAccess.GetDataTable(query, 90);

            return DataUtils.DataRowsToListString(content.Rows, null, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="siteCode"><see cref="Constants.SiteCodes"/></param>
        /// <param name="activityTitle"></param>
        /// <returns></returns>
        public static int GetActivityID(Constants.SiteCodes siteCode, string activityTitle)
        {
            string query = string.Format(@"SELECT top 1 a.activity_seq
  FROM [CommandCenter].[dbo].[activity] a with(nolock)
  INNER JOIN dbo.site_site_activity sa with(nolock) ON sa.activity_seq = a.activity_seq
  INNER JOIN dbo.site_site s with(nolock) ON  s.site_seq = sa.site_seq
  where s.site_code = '{0}'
  and a.activity_status_code = 4
  and a.deleted_ind = 'N'
  and a.activity_title = '{1}'
  and sa.final_ind = 'Y'
  order by a.activity_seq", siteCode.GetDescription(), activityTitle);

            //int queryResult2 = _WebAppDbAccess.GetDataValue<int>(query, 120);

            return _WebAppDbAccess.GetDataValue<int>(query, 120);
        }

        /// <summary>
        /// Gets a list of accrediting bodies for the activity
        /// </summary>
        /// <param name="siteCode"></param>
        /// <param name="activityTitle"></param>
        /// <returns></returns>
        public static List<Constants.Accreditation> GetActivity_Accreditation(Constants.SiteCodes siteCode, string activityTitle)
        {
            int actId = GetActivityID(siteCode, activityTitle);

            string query = string.Format(@"SELECT distinct a3.name, sc.credits as creditamount, cu.name as creditunit
FROM abt_scenario_credits sc
  JOIN activity_body_type_scenario a ON sc.abt_scenario_seq = a.abt_scenario_seq
  JOIN activity_body_type a2 ON a.activity_body_type_seq = a2.activity_body_type_seq
  JOIN accreditation_body_type a3 ON a2.accreditation_body_type_seq = a3.accreditation_body_type_seq
  JOIN credit_unit cu ON sc.credit_unit_seq = cu.credit_unit_seq
  JOIN organization o on a3.organization_seq = o.organization_seq
WHERE a2.activity_seq = {0}
and sc.order_seq = 1
ORDER BY a3.name", actId);

            DataTable dt = _WebAppDbAccess.GetDataTable(query, 120);



            IList<Constants.Accreditation> Accreditations = dt.AsEnumerable().Select(accreditationRow => new Constants.Accreditation
            {
                // If there is no accreditation, then the database will return a null value in the 'name' field. If so, we need to manually
                // enter NONACCR into the BodyName field here
                BodyName = string.IsNullOrEmpty(accreditationRow.Field<string>("name"))
                ? "NONACCR"
                :
                StripHTML(System.Text.RegularExpressions.Regex.Replace(accreditationRow.Field<string>("name"), @"\s+", " ")),

                CreditAmount = (double)accreditationRow.Field<decimal>("creditamount"),

                CreditUnit = accreditationRow.Field<string>("creditunit")
            }).ToList();

            // If/When we setup for multiple credit types...
            //IList<Constants_LMS.Accreditation> Accreditations = dt.AsEnumerable().Select(accreditationRow => new Constants_LMS.Accreditation
            //{
            //    BodyName = accreditationRow.Field<string>("name"),
            //    CreditTypes = dt.AsEnumerable().Select(creditRow => new Constants_LMS.Credits
            //    {
            //        Amount = creditRow.Field<int>("creditamount"),
            //        Unit = creditRow.Field<string>("creditunit")
            //    }).ToList()
            //}).ToList();

            Accreditations.ToList().OrderBy(x => x.BodyName);

            return Accreditations.ToList();
        }

        /// <summary>
        /// Gets all info for an activity from the activity table, the Address and Time Live meeting info, the front matter, 
        /// the accreditation info
        /// </summary>
        /// <param name="siteCode"></param>
        /// <param name="activityTitle"></param>
        /// <returns></returns>
        public static Constants.Activity GetActivity_GeneralInfo(Constants.SiteCodes siteCode, string activityTitle,
            Constants.ActType activityType)
        {
            string query = "";

            Constants.Activity Activity = new Constants.Activity();
            Constants.AddressAndLocation addAndLoc = null;

            int actId = GetActivityID(siteCode, activityTitle);

            // Query will need to be different if this is a Live Meeting
            if (activityType == Constants.ActType.LiveMeeting)
            {
                query = string.Format(@"SELECT  a.activity_title, html, a.date_posted, a.date_expiry, 
a.location_name, oa.addr_line_01, oa.addr_line_02, oa.city, oa.territory_code,
oa.postal_code, oa.country_code, start_date, end_date
 FROM dbo.award_template t
  INNER JOIN dbo.disclaimer d ON d.template_seq = t.award_template_seq
  INNER JOIN activity a ON d.activity_seq = a.activity_seq
  JOIN object_address oa ON a.object_seq = oa.object_seq
WHERE d.activity_seq = {0}", actId);
            }
            else
            {
                query = string.Format(@"SELECT  a.activity_title, html, a.date_posted, a.date_expiry
 FROM dbo.award_template t
  INNER JOIN dbo.disclaimer d ON d.template_seq = t.award_template_seq
  INNER JOIN activity a ON d.activity_seq = a.activity_seq
WHERE d.activity_seq = {0}", actId);
            }

                DataTable dt = _WebAppDbAccess.GetDataTable(query, 120);

            Activity.ActivityTitle = DataUtils.GetDatatableCellByRowNumAndColName(dt, 0, "activity_title");
            Activity.Release_Date = DataUtils.GetDatatableCellByRowNumAndColName(dt, 0, "date_posted");
            Activity.Expiration_Date = DataUtils.GetDatatableCellByRowNumAndColName(dt, 0, "date_expiry");
            Activity.FrontMatter = StripHTML(DataUtils.GetDatatableCellByRowNumAndColName(dt, 0, "html"));

            // Get Live meeting info, if applicable
            if (activityType == Constants.ActType.LiveMeeting)
            {
                addAndLoc = new Constants.AddressAndLocation();

                addAndLoc.Addr_Line_01 = DataUtils.GetDatatableCellByRowNumAndColName(dt, 0, "addr_line_01");
                addAndLoc.City = DataUtils.GetDatatableCellByRowNumAndColName(dt, 0, "city");
                addAndLoc.State = DataUtils.GetDatatableCellByRowNumAndColName(dt, 0, "territory_code");
                addAndLoc.ZipCode = DataUtils.GetDatatableCellByRowNumAndColName(dt, 0, "postal_code");
                addAndLoc.Country = DataUtils.GetDatatableCellByRowNumAndColName(dt, 0, "country_code");
                addAndLoc.StartDate = DataUtils.GetDatatableCellByRowNumAndColName(dt, 0, "start_date");
                addAndLoc.EndDate = DataUtils.GetDatatableCellByRowNumAndColName(dt, 0, "end_date");
            }

            Activity.AddressAndLocation = addAndLoc;

            // Get accreditation info
            Activity.Accreditations =
                DBUtils.GetActivity_Accreditation(siteCode, DataUtils.GetDatatableCellByRowNumAndColName(dt, 0, "activity_title"));

            return Activity;
        }

        /// <summary>
        /// Trims all html from a string (strings that begin with '<' and end with '>'). This also trims leading or trailing 
        /// whitespace if desired. It also replaces non-breaking spaces. See inside the method for how that is accomplished
        /// </summary>
        /// <param name="html"></param>
        /// <param name="trimLeadingAndTrailingWhiteSpace">(Optiona). Default = true. Send false if you dont want to trim leading/trailing whitespace</param>
        /// <param name="trimAnswerChoiceOptions">Right now in Fireball, for radio button answers that have formatting (formatting results 
        /// in HTML characters, so we need to trim them in this), the developers put numbers/alphabets/romannumerals that precede the 
        /// answer text in a separate Span element than the actual answer text itself. So you can set this to true and this method will 
        /// remove those numbers/alphabets/romannumerals so that the returned answer will be compatible with 
        /// <see cref="ElemGet.RdoBtn_GetRdoBtnByText(OpenQA.Selenium.IWebDriver, string, OpenQA.Selenium.IWebElement)(OpenQA.Selenium.IWebDriver, string)"/>
        /// Note that if there are multiple answers inside the html parameter, then we also trim between "Next Answer:" and "<"
        /// </param>
        /// <returns></returns>
        public static string StripHTML(string html, bool trimLeadingAndTrailingWhiteSpace = true, bool trimAnswerChoiceOptions = false)
        {
            string selectClause = "";

            // If the html has any single quotes in it, we have to escape them, as SQL uses single quotes as operators
            html = html.Replace("'", @"''");

            // If the HTML is using character encoding for the less-than < or greater-than > symbols, then replace that encoding with 
            // the less-than or greater-than symbol. We are doing this because our query below only works on less-than and greater-than
            // symbols, and doesnt work on their encoding equivalents.
            if (html.Contains("&lt;") || html.Contains("&gt;"))
            {
                html = html.Replace("&lt;", @"<");
                html = html.Replace("&gt;", @">");
            }

            //// See the XML documentation above this method (for trimAnswerChoiceOptions) for an explanation on why we are trimming every 
            //// before the < symbol here
            //// UPDATE: we are now doing this instead at the query level
            //if (trimAnswerChoiceOptions == true && html.Contains("<"))
            //{
            //    html = html.Substring(html.IndexOf('<'));
            //}

            // The following trims leading or trailing whitespace, and it also replaces any strings that contains
            // non-breaking space character sets, which look like this... &nbsp; 
            // It replaces those non breaking space sets with char(160). For an example, see the test Reg_Prerequisites
            // in LMS. We have to query question text and get it to match the innerText of the UI question label elements.
            // Initially I tried to replace the nonbreaking space with just a normal space with my keyboard, but when 
            // I compared the query result to the element innerText, it did not work and said it was not a match, even though
            // it looked to be a match to the naked eye. Nirav then diagnosed the problem. He put a breakpoint on the If 
            // statement that see if the strings match, then hit Control+Alt+I to bring up the Immediate Window in Visual Studio,
            // then he pasted the element, QuestionLabelElement.GetAttribute("innerText"), into the immediate window and then
            // typed QuestionLabelElement.GetAttribute("innerText").ToCharArray() and hit enter. This showed each characters
            // unci code. We then did this for the query result, questionText.GetAttribute("innerText").ToCharArray().
            // We then compared the results, and it showed that the spaces where the non-breaking spaces were did not match.
            // For example, the element showed as having 160 as the code for it's space (160' '), but the query result showed
            // it as having 32 (32' '). To solve this, we added the below line in the Replace function for SQL char(160)
            if (trimLeadingAndTrailingWhiteSpace)
            {
                selectClause = "select  LTRIM(RTRIM(replace(@HTMLText,'&nbsp;',char(160))))";
            }
            else
            {
                selectClause = "select  replace(@HTMLText,'&nbsp;',char(160))";
            }


            string query = string.Format(@"declare @HTMLText nvarchar(max)= '{0}'   
    DECLARE @Start INT
    DECLARE @End INT
    DECLARE @Length INT
    SET @Start = CHARINDEX('<', @HTMLText)
    SET @End = CHARINDEX('>', @HTMLText, CHARINDEX('<', @HTMLText))
    SET @Length = (@End - @Start) + 1
    WHILE @Start > 0 AND @End > 0 AND @Length > 0
    BEGIN
        SET @HTMLText = STUFF(@HTMLText, @Start, @Length, '')
        SET @Start = CHARINDEX('<', @HTMLText)
        SET @End = CHARINDEX('>', @HTMLText, CHARINDEX('<', @HTMLText))
        SET @Length = (@End - @Start) + 1
    END
    {1}",
    html, selectClause);

            return _WebAppDbAccess.GetDataValue<string>(query);
        }


        /// <summary>
        /// Returns an activities assessments high-level info (AssessmentName, AssessTypeName, isRequired, IsEnabled, AttemptsAllowed, 
        /// AttemptedAttempts, MinimumPercentageCorrect, AttemptCount, Score, Result, etc.)
        /// </summary>
        /// <param name="username"></param>
        /// <param name="sitecode"></param>
        /// <param name="activityTitle"></param>
        /// <param name="isRequired">(Optional). If you want to return required or non-required assessments. Meaning required for credit. Leave blank if you want to return both</param>
        /// <param name="returnDistinctAssessments">(Optional). Return distinct assessment rows</param>
        /// <returns></returns>
        public static List<Constants.Assessments> GetAssessmentsByActivityId(string username, Constants.SiteCodes sitecode, string activityTitle,
            bool? isRequired = null, bool returnDistinctAssessments = false)
        {
            // get the activity ID 
            string activityIDOrSequence = GetActivityID(sitecode, activityTitle).ToString();

            // Get the participant sequence
            int participantSeq = GetParticipantSequence(username);

            // Get the activity participant sequence
            int activityParticipantSeq = GetActivityParticipantSequence(activityIDOrSequence, participantSeq.ToString());

            // Set the flags
            string isRequiredFlag = isRequired == true ? "and abtsa.required_for_credit_ind = 'Y'" :
                                    isRequired == false ? "and abtsa.required_for_credit_ind = 'N'" : "";

            // If we are on Prod, then we dont have Execute priveleges, so we have to use the exact query text
            string GetCurrentActivityParticipantId = string.Format(@"EXEC PARTICIPANT_GetCurrentActivityParticipantId @activity_seq, 
@participant_seq, @activity_participant_seq OUT, @Caller");

            if (Constants.CurrentEnvironment == Constants.Environments.Production.GetDescription())
            {
                GetCurrentActivityParticipantId = string.Format(@"SELECT	@activity_participant_seq = ap.activity_participant_seq
				FROM	dbo.activity_participant ap WITH (NOLOCK)
				INNER JOIN dbo.activity_participant_instance api WITH (NOLOCK) ON
				ap.activity_participant_instance_seq =api.activity_participant_instance_seq AND api.isCurrent=1
				WHERE	ap.activity_seq = @activity_seq
				AND	ap.participant_seq = @participant_seq ");
            }


            // Get all of the assessments
            string query = string.Format(@"Declare
               @activity_seq       int = {0},
               @participant_seq    int = {1}


        DECLARE @ErrNo      int
        DECLARE @activity_participant_seq int = {2}


               DECLARE @Caller nvarchar(500)
               SELECT @Caller= OBJECT_NAME(@@PROCID)
               
{3}

               --SELECT a.activity_title AS 'ActivityTitle', 
               --             a.short_desc AS 'ActivityDescription'
               --FROM activity a WHERE a.activity_seq = @activity_seq

               SELECT abtsa.abt_scenario_assessment_seq AS 'ScenarioAssessmentId',
                            abtsa.abt_scenario_seq AS 'ScenarioId',
                            s.name AS 'ScenarioName',
                            a.assessment_seq AS 'AssessmentId',
                            a.assessment_name AS 'AssessmentName',
                            a.assessment_title AS 'AssessmentTitle',
                            a.assessment_desc AS 'AssessmentDescription',
                            a.form_seq AS 'FormId',
                            apaf.instance_seq AS 'FormInstanceId',
                            abt.accreditation_body_type_seq AS 'AccreditationBodyTypeId',
                            abt.name AS 'AccreditationBodyTypeName',
                            apaf.isformprocessed  AS 'IsFormProcessed',
                            CAST(apabtsaa.percent_correct AS VARCHAR(15)) AS 'Score',
                            (CASE WHEN (apabtsaa.pass_ind = 'Y') THEN 'Pass' WHEN (apabtsaa.pass_ind = 'N') THEN 'Fail' Else '' END) as 'Result',
                            --CASE WHEN sc.status_code_name = 'InProgress' THEN CASE WHEN (ISNULL(apabtsaa.attempt_count,0) <= 0) THEN CASE WHEN apaf.isformprocessed = 0 THEN 'In Progress' ELSE 'Not Started' END ELSE ISNULL(sc.status_code_desc,'Not Started') END ELSE ISNULL(sc.status_code_desc,'Not Started') END as 'Status',
                    CASE WHEN sc.status_code_name = 'InProgress' THEN 
                                    (CASE WHEN (ISNULL(apabtsaa.attempt_count,0) <= 0) THEN  
                                        (CASE WHEN apaf.isformprocessed = 0 THEN 
                                                'In Progress' 
                                        ELSE 'Not Started' 
                                        END) 
                                    ELSE ISNULL(sc.status_code_desc,'Not Started') 
                                    END) 
                    WHEN sc.status_code_name = 'NotStarted' THEN 
                                    (CASE WHEN (ISNULL(apaf.instance_seq,0) > 0) THEN  
                                        'In Progress' 
                                    ELSE ISNULL(sc.status_code_desc,'Not Started') 
                                    END) 
                    ELSE ISNULL(sc.status_code_desc,'Not Started') 
                    END as 'Status',
                            abtsa.required_for_credit_ind AS 'IsRequired',
                            (CASE WHEN ((apabtsa.display_date IS NOT NULL AND apabtsa.display_date <= GetDate())) THEN '1' ELSE '0' END) AS 'IsEnabled',
                            apabtsaa.attempt_count AS 'AttemptCount',
                            abtsa.attempts AS 'AttemptsAllowed',
                            apabtsaa.activity_participant_abts_assessment_attempt_seq AS 'AttemptedAttempts',
                            atype.assessment_type_seq as 'AssessmentType',
                            atype.assessment_type_name as 'AssessmentTypeName',
                            ISNULL(abtsa.percent_pass,0) as 'MinimumPercentageCorrect'
               FROM abt_scenario_assessment abtsa
               INNER JOIN    activity ac ON ac.activity_seq = @activity_seq
               INNER JOIN    assessment a ON abtsa.assessment_seq = a.assessment_seq
               INNER JOIN    assessment_type atype ON a.assessment_type_seq = atype.assessment_type_seq
               INNER JOIN    activity_participant_abt_scenario apabts ON abtsa.abt_scenario_seq = apabts.abt_scenario_seq
               INNER JOIN    activity_participant ap ON apabts.activity_participant_seq = ap.activity_participant_seq
               INNER JOIN  activity_participant_instance api ON ap.activity_participant_instance_seq=api.activity_participant_instance_seq 
               INNER JOIN    activity_participant_abts_assessment apabtsa ON apabts.activity_participant_abt_scenario_seq = apabtsa.activity_participant_abt_scenario_seq
               INNER JOIN    activity_assessment aa ON a.assessment_seq = aa.assessment_seq
               INNER JOIN    activity_body_type_scenario abts ON apabts.abt_scenario_seq = abts.abt_scenario_seq
               INNER JOIN    activity_body_type actbt ON abts.activity_body_type_seq = actbt.activity_body_type_seq
               INNER JOIN    accreditation_body_type abt ON actbt.accreditation_body_type_seq = abt.accreditation_body_type_seq
               INNER JOIN    scenario s ON abts.scenario_seq = s.scenario_seq
               LEFT OUTER JOIN ACTIVITY_PARTICIPANT_AssessmentFormInstance apaf ON ap.activity_participant_seq=apaf.activity_participant_seq 
                            AND a.form_seq=apaf.form_seq 
                            AND apaf.instance_seq = (SELECT MAX(instance_seq) FROM ACTIVITY_PARTICIPANT_AssessmentFormInstance apaf1 
                                                                          WHERE apaf1.activity_participant_seq=ap.activity_participant_seq AND apaf1.form_seq=a.form_seq)
            left outer join form_instance fi on apaf.instance_seq = fi.instance_seq
               LEFT OUTER JOIN status_code sc ON fi.status_code_seq = sc.status_code_seq
               LEFT OUTER JOIN (SELECT    b.activity_participant_abts_assessment_seq,
                                                       MAX(apai.percent_correct) as percent_correct,
                                                       MAX(b.attempt_count) AS attempt_count,
                                                       MAX(b.pass_ind) AS pass_ind,
                                                       MAX(activity_participant_abts_assessment_attempt_seq) AS activity_participant_abts_assessment_attempt_seq
                                                FROM  activity_participant_abts_assessment_attempt b 
                                                INNER JOIN       activity_participant_assessment_instance apai ON b.apa_instance_seq = apai.apa_instance_seq
                                                WHERE apai.void_ind = 'N' AND b.apa_instance_seq IS NOT NULL
                                                GROUP BY b.activity_participant_abts_assessment_seq) apabtsaa
                                         ON apabtsa.activity_participant_abts_assessment_seq = apabtsaa.activity_participant_abts_assessment_seq      
               WHERE ap.activity_participant_seq = @activity_participant_seq 
                     AND    apabtsa.assessment_seq = a.assessment_seq
                     AND (apabtsa.display_date IS NOT NULL AND apabtsa.display_date <= GetDate())
                     AND a.deleted_ind = 'N'
                     AND a.active_ind = 'Y'
                    {4}
               ORDER BY atype.assessment_type_seq asc, abtsa.display_order ASC, a.assessment_name ASC", activityIDOrSequence, participantSeq,
       activityParticipantSeq, GetCurrentActivityParticipantId, isRequiredFlag);

            DataTable table = _WebAppDbAccess.GetDataTable(query, 120);

            //int ScenarioAssessmentId = table.Rows[0].Field<int>("ScenarioAssessmentId");
            //int ScenarioId = table.Rows[0].Field<int>("ScenarioId");
            //int AssessmentId = table.Rows[0].Field<int>("AssessmentId");
            //int FormId = table.Rows[0].Field<int>("FormId");
            //Nullable<Int32> FormInstanceId = table.Rows[0].Field<Nullable<Int32>>("FormInstanceId");
            //int AccreditationBodyTypeId = table.Rows[0].Field<int>("AccreditationBodyTypeId");
            //bool? IsFormProcessed = table.Rows[0].Field<bool?>("IsFormProcessed");
            //Nullable<Int32> AttemptCount = table.Rows[0].Field<Nullable<Int32>>("AttemptCount");
            //Nullable<Int32> AttemptsAllowed = table.Rows[0].Field<Nullable<Int32>>("AttemptsAllowed");
            //Nullable<Int32> AttemptedAttempts = table.Rows[0].Field<Nullable<Int32>>("AttemptedAttempts");
            //int AssessmentType = table.Rows[0].Field<int>("AssessmentType");
            //string AssessmentTypeName = table.Rows[0].Field<string>("AssessmentTypeName");

            // Convert to object
            var assessments = table.AsEnumerable().Select(row => new Constants.Assessments()
            {
                ScenarioAssessmentId = row.Field<int>("ScenarioAssessmentId"),
                ScenarioId = row.Field<int>("ScenarioId"),
                ScenarioName = row.Field<string>("ScenarioName"),
                AssessmentId = row.Field<int>("AssessmentId"),
                AssessmentName = row.Field<string>("AssessmentName"),
                AssessmentTitle = row.Field<string>("AssessmentTitle"),
                AssessmentDescription = row.Field<string>("AssessmentDescription"),
                FormId = row.Field<int>("FormId"),
                FormInstanceId = row.Field<Nullable<Int32>>("FormInstanceId"),
                AccreditationBodyTypeId = row.Field<int>("AccreditationBodyTypeId"),
                AccreditationBodyTypeName = StripHTML(row.Field<string>("AccreditationBodyTypeName")),
                IsFormProcessed = row.Field<bool?>("IsFormProcessed"),
                Score = row.Field<string>("Score"),
                Result = row.Field<string>("Result"),
                Status = row.Field<string>("Status"),
                IsRequired = row.Field<string>("IsRequired"),
                IsEnabled = row.Field<string>("IsEnabled"),
                AttemptCount = row.Field<Nullable<Int32>>("AttemptCount"),
                AttemptsAllowed = row.Field<Nullable<Int32>>("AttemptsAllowed"),
                AttemptedAttempts = row.Field<Nullable<Int32>>("AttemptedAttempts"),
                AssessmentType = row.Field<int>("AssessmentType"),
                AssessmentTypeName = row.Field<string>("AssessmentTypeName"),
                MinimumPercentageCorrect = row.Field<decimal>("MinimumPercentageCorrect"),
            }).ToList();

            // If tester requested distinct assessments, return them, else dont
            List<Constants.Assessments> distinctAssessments = null;
            if (returnDistinctAssessments)
            {
                // Get distinct questions
                distinctAssessments = assessments.GroupBy(a => a.FormId).Select(group => group.First()).ToList();

                return distinctAssessments;
            }
            else
            {
                // sort
                return assessments;
            }
        }

        /// <summary>
        /// Gets the form instance ID, which is used to pass to 
        /// <see cref="GetAssessmentQuestionsAndAnswers(string, string, string, bool?, bool?, bool?, bool, bool)(string, string, string, string)"/>
        /// NOTE: You must register to the activity as a certain user and also load the assessment page on the UI for this 
        /// to work. Otherwise the FormInstanceId will not be available
        /// </summary>
        /// <param name="activityIdOrSequence">Get this from the URL or <see cref="GetActivityID(string)"/></param>
        /// <param name="activityParticipantSequence"><see cref=" GetActivityParticipantSequence(string, string)"/></param>
        /// <param name="assessmentName">You can get this when you are on the UI page of the assessment</param>
        /// <returns></returns>
        private static int GetFormInstanceID(string activityIdOrSequence, string activityParticipantSequence, string assessmentName)
        {
            string query = string.Format(@"declare
	@activity_seq		int = {0},
	@abt_scenario_seq	int = NULL,
	@activity_participant_seq  int = {1},
	@assessment_name varchar(1000) = '{2}'

	SELECT	abtsa.abt_scenario_assessment_seq AS 'ScenarioAssessmentId',
			abtsa.abt_scenario_seq AS 'ScenarioId',
			s.name AS 'ScenarioName',
			a.assessment_seq AS 'AssessmentId',
			a.assessment_name AS 'AssessmentName',
			a.assessment_title AS 'AssessmentTitle',
			a.assessment_desc AS 'AssessmentDescription',
			a.form_seq AS 'FormId',
			apaf.instance_seq AS 'FormInstanceId',
			abt.accreditation_body_type_seq AS 'AccreditationBodyTypeId',
			abt.name AS 'AccreditationBodyTypeName',
			apaf.isformprocessed  AS 'IsFormProcessed',
			CAST(apabtsaa.percent_correct AS VARCHAR(15)) AS 'Score',
			(CASE WHEN (apabtsaa.pass_ind = 'Y') THEN 'Pass' WHEN (apabtsaa.pass_ind = 'N') THEN 'Fail' Else '' END) as 'Result',
			--CASE WHEN sc.status_code_name = 'InProgress' THEN CASE WHEN (ISNULL(apabtsaa.attempt_count,0) <= 0) THEN CASE WHEN apaf.isformprocessed = 0 THEN 'In Progress' ELSE 'Not Started' END ELSE ISNULL(sc.status_code_desc,'Not Started') END ELSE ISNULL(sc.status_code_desc,'Not Started') END as 'Status',
            CASE WHEN sc.status_code_name = 'InProgress' THEN 
                            (CASE WHEN (ISNULL(apabtsaa.attempt_count,0) <= 0) THEN  
                                (CASE WHEN apaf.isformprocessed = 0 THEN 
                                        'In Progress' 
                                ELSE 'Not Started' 
                                END) 
                            ELSE ISNULL(sc.status_code_desc,'Not Started') 
                            END) 
            WHEN sc.status_code_name = 'NotStarted' THEN 
                            (CASE WHEN (ISNULL(apaf.instance_seq,0) > 0) THEN  
                                'In Progress' 
                            ELSE ISNULL(sc.status_code_desc,'Not Started') 
                            END) 
            ELSE ISNULL(sc.status_code_desc,'Not Started') 
            END as 'Status',
			abtsa.required_for_credit_ind AS 'IsRequired',
			(CASE WHEN ((apabtsa.display_date IS NOT NULL AND apabtsa.display_date <= GetDate())) THEN '1' ELSE '0' END) AS 'IsEnabled',
			apabtsaa.attempt_count AS 'AttemptCount',
			abtsa.attempts AS 'AttemptsAllowed',
			apabtsaa.activity_participant_abts_assessment_attempt_seq AS 'AttemptedAttempts',
			atype.assessment_type_seq as 'AssessmentType',
			atype.assessment_type_name as 'AssessmentTypeName',
			ISNULL(abtsa.percent_pass,0) as 'MinimumPercentageCorrect'
	FROM abt_scenario_assessment abtsa
	INNER JOIN	activity ac ON ac.activity_seq = @activity_seq
	INNER JOIN 	assessment a ON abtsa.assessment_seq = a.assessment_seq
	INNER JOIN 	assessment_type atype ON a.assessment_type_seq = atype.assessment_type_seq
	INNER JOIN 	activity_participant_abt_scenario apabts ON abtsa.abt_scenario_seq = apabts.abt_scenario_seq
	INNER JOIN 	activity_participant ap ON apabts.activity_participant_seq = ap.activity_participant_seq
	INNER JOIN  activity_participant_instance api ON ap.activity_participant_instance_seq=api.activity_participant_instance_seq 
	INNER JOIN 	activity_participant_abts_assessment apabtsa ON apabts.activity_participant_abt_scenario_seq = apabtsa.activity_participant_abt_scenario_seq
	INNER JOIN	activity_assessment aa ON a.assessment_seq = aa.assessment_seq
	INNER JOIN	activity_body_type_scenario abts ON apabts.abt_scenario_seq = abts.abt_scenario_seq
	INNER JOIN	activity_body_type actbt ON abts.activity_body_type_seq = actbt.activity_body_type_seq
	INNER JOIN	accreditation_body_type abt ON actbt.accreditation_body_type_seq = abt.accreditation_body_type_seq
	INNER JOIN	scenario s ON abts.scenario_seq = s.scenario_seq
	LEFT OUTER JOIN ACTIVITY_PARTICIPANT_AssessmentFormInstance apaf ON ap.activity_participant_seq=apaf.activity_participant_seq 
			AND a.form_seq=apaf.form_seq 
			AND apaf.instance_seq = (SELECT MAX(instance_seq) FROM ACTIVITY_PARTICIPANT_AssessmentFormInstance apaf1 
										WHERE apaf1.activity_participant_seq=ap.activity_participant_seq AND apaf1.form_seq=a.form_seq)
    left outer join form_instance fi on apaf.instance_seq = fi.instance_seq
	LEFT OUTER JOIN status_code sc ON fi.status_code_seq = sc.status_code_seq
	LEFT OUTER JOIN (SELECT	b.activity_participant_abts_assessment_seq,
							MAX(apai.percent_correct) as percent_correct,
							MAX(b.attempt_count) AS attempt_count,
							MAX(b.pass_ind) AS pass_ind,
							MAX(activity_participant_abts_assessment_attempt_seq) AS activity_participant_abts_assessment_attempt_seq
						FROM  activity_participant_abts_assessment_attempt b 
						INNER JOIN	activity_participant_assessment_instance apai ON b.apa_instance_seq = apai.apa_instance_seq
						WHERE apai.void_ind = 'N' AND b.apa_instance_seq IS NOT NULL
						GROUP BY b.activity_participant_abts_assessment_seq) apabtsaa
					ON apabtsa.activity_participant_abts_assessment_seq = apabtsaa.activity_participant_abts_assessment_seq	
	WHERE ap.activity_participant_seq = @activity_participant_seq 
		AND	apabtsa.assessment_seq = a.assessment_seq
		AND (apabtsa.display_date IS NOT NULL AND apabtsa.display_date <= GetDate())
		AND a.deleted_ind = 'N'
		AND a.active_ind = 'Y'
		AND ((ISNULL(@abt_scenario_seq,1) = 1) OR (abtsa.abt_scenario_seq = @abt_scenario_seq))
		and assessment_name = @assessment_name
	ORDER BY atype.assessment_type_seq asc, abtsa.display_order ASC, a.assessment_name ASC", activityIdOrSequence,
        activityParticipantSequence, assessmentName);

            DataTable dt = _WebAppDbAccess.GetDataTable(query, 120);

            string blah = DataUtils.GetDatatableCellByRowNumAndColName(dt, 0, "FormInstanceId");

            return int.Parse(blah);
        }

        /// <summary>
        /// Gets the form ID, which is used to pass to 
        /// <see cref="GetAssessmentQuestionsAndAnswers(string, string, string, bool?, bool?, bool?, bool, bool)(string, string, string, string)"/>.
        /// NOTE: You must register to the activity as a certain user and also load the assessment page on the UI for this 
        /// to work. Otherwise the FormId will not be available
        /// </summary>
        /// <param name="activityIdOrSequence">Get this from the URL or <see cref="GetActivityID(string)"/></param>
        /// <param name="activityParticipantSequence"><see cref=" GetActivityParticipantSequence(string, string)"/></param>
        /// <param name="assessmentName"></param>
        /// <returns></returns>
        private static int GetFormID(string activityIdOrSequence, string activityParticipantSequence, string assessmentName)
        {
            string query = string.Format(@"declare
	@activity_seq		int = {0},
	@abt_scenario_seq	int = NULL,
	@activity_participant_seq  int = {1},
	@assessment_name varchar(1000) = '{2}'

	SELECT	abtsa.abt_scenario_assessment_seq AS 'ScenarioAssessmentId',
			abtsa.abt_scenario_seq AS 'ScenarioId',
			s.name AS 'ScenarioName',
			a.assessment_seq AS 'AssessmentId',
			a.assessment_name AS 'AssessmentName',
			a.assessment_title AS 'AssessmentTitle',
			a.assessment_desc AS 'AssessmentDescription',
			a.form_seq AS 'FormId',
			apaf.instance_seq AS 'FormInstanceId',
			abt.accreditation_body_type_seq AS 'AccreditationBodyTypeId',
			abt.name AS 'AccreditationBodyTypeName',
			apaf.isformprocessed  AS 'IsFormProcessed',
			CAST(apabtsaa.percent_correct AS VARCHAR(15)) AS 'Score',
			(CASE WHEN (apabtsaa.pass_ind = 'Y') THEN 'Pass' WHEN (apabtsaa.pass_ind = 'N') THEN 'Fail' Else '' END) as 'Result',
			--CASE WHEN sc.status_code_name = 'InProgress' THEN CASE WHEN (ISNULL(apabtsaa.attempt_count,0) <= 0) THEN CASE WHEN apaf.isformprocessed = 0 THEN 'In Progress' ELSE 'Not Started' END ELSE ISNULL(sc.status_code_desc,'Not Started') END ELSE ISNULL(sc.status_code_desc,'Not Started') END as 'Status',
            CASE WHEN sc.status_code_name = 'InProgress' THEN 
                            (CASE WHEN (ISNULL(apabtsaa.attempt_count,0) <= 0) THEN  
                                (CASE WHEN apaf.isformprocessed = 0 THEN 
                                        'In Progress' 
                                ELSE 'Not Started' 
                                END) 
                            ELSE ISNULL(sc.status_code_desc,'Not Started') 
                            END) 
            WHEN sc.status_code_name = 'NotStarted' THEN 
                            (CASE WHEN (ISNULL(apaf.instance_seq,0) > 0) THEN  
                                'In Progress' 
                            ELSE ISNULL(sc.status_code_desc,'Not Started') 
                            END) 
            ELSE ISNULL(sc.status_code_desc,'Not Started') 
            END as 'Status',
			abtsa.required_for_credit_ind AS 'IsRequired',
			(CASE WHEN ((apabtsa.display_date IS NOT NULL AND apabtsa.display_date <= GetDate())) THEN '1' ELSE '0' END) AS 'IsEnabled',
			apabtsaa.attempt_count AS 'AttemptCount',
			abtsa.attempts AS 'AttemptsAllowed',
			apabtsaa.activity_participant_abts_assessment_attempt_seq AS 'AttemptedAttempts',
			atype.assessment_type_seq as 'AssessmentType',
			atype.assessment_type_name as 'AssessmentTypeName',
			ISNULL(abtsa.percent_pass,0) as 'MinimumPercentageCorrect'
	FROM abt_scenario_assessment abtsa
	INNER JOIN	activity ac ON ac.activity_seq = @activity_seq
	INNER JOIN 	assessment a ON abtsa.assessment_seq = a.assessment_seq
	INNER JOIN 	assessment_type atype ON a.assessment_type_seq = atype.assessment_type_seq
	INNER JOIN 	activity_participant_abt_scenario apabts ON abtsa.abt_scenario_seq = apabts.abt_scenario_seq
	INNER JOIN 	activity_participant ap ON apabts.activity_participant_seq = ap.activity_participant_seq
	INNER JOIN  activity_participant_instance api ON ap.activity_participant_instance_seq=api.activity_participant_instance_seq 
	INNER JOIN 	activity_participant_abts_assessment apabtsa ON apabts.activity_participant_abt_scenario_seq = apabtsa.activity_participant_abt_scenario_seq
	INNER JOIN	activity_assessment aa ON a.assessment_seq = aa.assessment_seq
	INNER JOIN	activity_body_type_scenario abts ON apabts.abt_scenario_seq = abts.abt_scenario_seq
	INNER JOIN	activity_body_type actbt ON abts.activity_body_type_seq = actbt.activity_body_type_seq
	INNER JOIN	accreditation_body_type abt ON actbt.accreditation_body_type_seq = abt.accreditation_body_type_seq
	INNER JOIN	scenario s ON abts.scenario_seq = s.scenario_seq
	LEFT OUTER JOIN ACTIVITY_PARTICIPANT_AssessmentFormInstance apaf ON ap.activity_participant_seq=apaf.activity_participant_seq 
			AND a.form_seq=apaf.form_seq 
			AND apaf.instance_seq = (SELECT MAX(instance_seq) FROM ACTIVITY_PARTICIPANT_AssessmentFormInstance apaf1 
										WHERE apaf1.activity_participant_seq=ap.activity_participant_seq AND apaf1.form_seq=a.form_seq)
    left outer join form_instance fi on apaf.instance_seq = fi.instance_seq
	LEFT OUTER JOIN status_code sc ON fi.status_code_seq = sc.status_code_seq
	LEFT OUTER JOIN (SELECT	b.activity_participant_abts_assessment_seq,
							MAX(apai.percent_correct) as percent_correct,
							MAX(b.attempt_count) AS attempt_count,
							MAX(b.pass_ind) AS pass_ind,
							MAX(activity_participant_abts_assessment_attempt_seq) AS activity_participant_abts_assessment_attempt_seq
						FROM  activity_participant_abts_assessment_attempt b 
						INNER JOIN	activity_participant_assessment_instance apai ON b.apa_instance_seq = apai.apa_instance_seq
						WHERE apai.void_ind = 'N' AND b.apa_instance_seq IS NOT NULL
						GROUP BY b.activity_participant_abts_assessment_seq) apabtsaa
					ON apabtsa.activity_participant_abts_assessment_seq = apabtsaa.activity_participant_abts_assessment_seq	
	WHERE ap.activity_participant_seq = @activity_participant_seq 
		AND	apabtsa.assessment_seq = a.assessment_seq
		AND (apabtsa.display_date IS NOT NULL AND apabtsa.display_date <= GetDate())
		AND a.deleted_ind = 'N'
		AND a.active_ind = 'Y'
		AND ((ISNULL(@abt_scenario_seq,1) = 1) OR (abtsa.abt_scenario_seq = @abt_scenario_seq))
		and assessment_name = @assessment_name
	ORDER BY atype.assessment_type_seq asc, abtsa.display_order ASC, a.assessment_name ASC", activityIdOrSequence,
        activityParticipantSequence, assessmentName);

            DataTable dt = _WebAppDbAccess.GetDataTable(query, 120);

            return int.Parse(DataUtils.GetDatatableCellByRowNumAndColName(dt, 0, "FormId"));
        }

        /// <summary>
        /// Returns questions/answers information for a tester-specified assessment, such as (question text, question type, answer text, 
        /// isgraded, isrequired, orderid). You can filter what is being returned by setting each parameter flag to true or false
        /// </summary>
        /// <param name="username">The user name</param>
        /// <param name="activityIDOrSequence">The activity ID of the activity. To get this, <see cref="GetActivityID(string)(string, string, string)"/></param>
        /// <param name="assessmentName">You can get this when you are on the UI page of the assessment</param>
        /// <param name="isQuestionGraded">true or false</param>
        /// <param name="isAnswerCorrect">true or false</param>
        /// <param name="isAnswerRequired">true or false</param>
        /// <param name="returnDistinctQuestionsWithOneAnswer">If true, this only returns 1 row per question (with 1 answer). If false, 
        /// returns all rows per questions, meaning all answers for a question will return. </param>
        /// <param name="returnDistinctQuestionsWithStuffedAnswers">If true, only returns 1 row per question, and inside that row(s), 
        /// the AnswerText field will contain all answers separated by a comma.</param>
        /// <returns></returns>
        public static List<Constants.AssQAndAs> GetAssessmentQuestionsAndAnswers(string username,
            string activityIDOrSequence, string assessmentName,
            bool? isQuestionGraded = null, bool? isAnswerCorrect = null, bool? isAnswerRequired = null,
            bool returnDistinctQuestionsWithOneAnswer = false, bool returnDistinctQuestionsWithStuffedAnswers = false)
        {
            // Get the participant sequence
            int participantSeq = GetParticipantSequence(username);

            // Get the activity participant sequence
            int activityParticipantSeq = GetActivityParticipantSequence(activityIDOrSequence, participantSeq.ToString());

            // Get the Form ID for the current participant's assessment
            int formID = GetFormID(activityIDOrSequence, activityParticipantSeq.ToString(), assessmentName);

            // Get the Form Instance ID for the current participant's assessment
            int formInstanceID = GetFormInstanceID(activityIDOrSequence, activityParticipantSeq.ToString(), assessmentName);

            // Set the flags
            string isCorrectFlag = isAnswerCorrect == true ? "and IsCorrect = 1" : isAnswerCorrect == false ? "and IsCorrect = 0" : "";
            string isAnswerRequiredFlag = isAnswerRequired == true ? "and IsRequired = 1" : isAnswerRequired == false ? "and IsRequired = 0" : "";
            string isQuestionGradedFlag = isQuestionGraded == true ? "and IsGraded = 1" : isQuestionGraded == false ? "and IsGraded = 0" : "";

            // Set the Where clause now. If tester wants distinct with answers stuffed, stuff it, else just return AnswerText field without stuffing
            string whereClause = "";
            if (returnDistinctQuestionsWithStuffedAnswers)
            {
                whereClause = string.Format(@"
            WHERE q.question_seq  = @questionId
             END 

             SELECT QuestionId, QuestionText, QuestionTypeName, IsSelected, IsCorrect, isGraded, isRequired, CorrectOrderSeq, 
			 IsOther, OtherValue, aTT.QuestionOrderId, group_question_seq, --Feedback

             -- Returning only 1 row per question, and combining all answers separated by commas into that row(s). Also we are adding 
             -- the ChoiceTag (numbered/alphabetical/romannumeral things) if the ChoiceTag is configured in CME360 and also if there is no 
             -- formatting setup (will include a 'span' tag if so) in CME360 for the answer
					    AnswerText =  STUFF((SELECT ', NextAnswer: ' + CASE WHEN (ChoiceTag is not null AND ChoiceTag != '' AND AnswerText not LIKE '%span%')  THEN (ChoiceTag + '. ' + AnswerText) ELSE AnswerText END
						FROM @answersTT aTT_b 
						WHERE aTT_b.QuestionId = aTT.QuestionId
                        and IsCorrect = 1
					    
					    and IsGraded = 1 
						FOR XML PATH('')), 1, 14, '')

             FROM @answersTT as aTT 
			 where IsCorrect is not null --To the left is just a dummy condition (IsCorrect will always be not null. I need to put it 
                                                --there because of the remaining conditions need the And keyword to work for those C# conditions
                    {0}
					{1}
					{2} 
					GROUP BY QuestionId, QuestionText, QuestionTypeName, IsSelected, IsCorrect, isGraded, isRequired, CorrectOrderSeq, 
			            IsOther, OtherValue, aTT.QuestionOrderId, group_question_seq --Feedback
			        order by isOther desc -- Sorting by isOther so that when we groupBy in the object later in this code to make a distinct list, it will pick up the isOther ones",
                    isCorrectFlag, isAnswerRequiredFlag, isQuestionGradedFlag);
            }
            // Else just add the AnswerText column without stuffing
            else
            {
                whereClause = string.Format(@"
            WHERE q.question_seq  = @questionId
             END 

             -- We are adding the ChoiceTag (numbered/alphabetical/romannumeral things) if the ChoiceTag is configured in CME360
             -- and also if there is no formatting setup (will include a 'span' tag if so) in CME360 for the answer
             SELECT QuestionId, QuestionText, QuestionTypeName, IsSelected, IsCorrect, isGraded, isRequired, CorrectOrderSeq, 
			 IsOther, OtherValue, aTT.QuestionOrderId, 
AnswerText =  CASE WHEN (ChoiceTag is not null AND ChoiceTag != '' AND AnswerText not LIKE '%span%')  THEN (ChoiceTag + '. ' + AnswerText) ELSE AnswerText END,
group_question_seq --Feedback

             FROM @answersTT as aTT 
			 where IsCorrect is not null --To the left is just a dummy condition (IsCorrect will always be not null. I need to put it 
                                                --there because of the remaining conditions need the And keyword to work for those C# conditions
                    {0}
					{1}
					{2} 
                    and group_question_seq is null
			        order by isOther desc -- Sorting by isOther so that when we groupBy in the object later in this code to make a distinct list, it will pick up the isOther ones",
                     isCorrectFlag, isAnswerRequiredFlag, isQuestionGradedFlag);
            }

            // If we are on Prod, then we dont have Insert priveleges, so instead of INSERTing into the DBO's form_instance_question_snapshot
            // table, we are going to create a temporary table that is defined the same way as form_instance_question_snapshot.
            // we have to use the exact query text. Actually, creating a temp table causes race conditions if 2 tests try to create
            // this same table at the same time. So instead of doing 'CREATE TABLE', we will 'declare' a form_instance_question_snapshot 
            // variable and mimick the table
            string formInstanceQuestionSnapshotTable = "form_instance_question_snapshot";
            string tempTableForFormInstanceQuestionsSnapshotTable = null;

            if (Constants.CurrentEnvironment == Constants.Environments.Production.GetDescription())
            {
                tempTableForFormInstanceQuestionsSnapshotTable = string.Format(@"declare @form_instance_question_snapshot TABLE(
	[form_instance_seq] [int] NOT NULL,
	[question_seq] [int] NOT NULL,
	[previous_question_seq] [int] NULL,
	[next_question_seq] [int] NULL,
	[show] [bit]  NULL,
	[IsSelected] [bit]  NULL,[order_seq] [int] NULL,
	[default_value] [varchar](100) NULL,
	[IsEnabled] [bit]  NULL
	)");
                formInstanceQuestionSnapshotTable = "@form_instance_question_snapshot";
            }


            // Get the questions and answers info
            string query = string.Format(@"-- ANSWERS INFORMATION FOR THE ASSESSMENT 
-- Plug in the formid and forminstanceid below to get the questions, answers, and more info about the assessment
use CommandCenter
{3}
Declare      
@formId int = {0},
@formInstanceId int = {1},
@requireMoreQuestionInfo bit = 1,
@requireAnswerTable bit = 1,
@errorDescription nvarchar(2000)      
      
      
-- DECLARE
---------------------------------------------------------------------------------------------------
DECLARE @enableDebugInfo int = '0'   -- ('0' is false,  '1' is true)

DECLARE @thisFormId int
DECLARE @count1 int =0
DECLARE @count2 int =0
DECLARE @questionId int
DECLARE @questionType int
DECLARE @questionText int
DECLARE @questionOrderId int
DECLARE @organization_ind bit
DECLARE @organizationId int
DECLARE @proc_name varchar(200)

DECLARE @questionsTT TABLE (id int IDENTITY(1, 1), order_seq int, question_seq int, group_question_seq int default null, 
                            show bit default 1, isSelected bit default 0, isAnswersProcessed bit default 0, question_type int)



---------------------------------------------------------------------------------------------------
-- PROCESSING 
SELECT @thisFormId = form_seq FROM form_instance WHERE instance_seq = @formInstanceId
---------------------------------------------------------------------------------------------------     


    IF (ISNULL(@thisFormId,0) = 0)
    BEGIN
            SELECT @errorDescription = 'Invalid Form Instance Id'
    END    

    --SELECT q.* FROM form_instance_question_snapshot s	
    --INNER JOIN question q ON q.question_seq = s.question_seq
    --WHERE form_instance_seq = @formInstanceId
       
    SELECT @count1 = count(*) FROM {4} WHERE form_instance_seq = @formInstanceId
       
    INSERT INTO @questionsTT (order_seq, question_seq, show, group_question_seq)
        SELECT
            order_seq = fdf.order_seq,
            question_seq = df.question_seq,
            show = 1,
            group_question_seq = NULL
        FROM form_data_field fdf
        INNER JOIN data_field df ON df.data_field_seq=fdf.data_field_seq
        INNER JOIN question q ON q.question_seq = df.question_seq
        WHERE fdf.form_seq = @thisFormId AND fdf.order_seq > 0 AND q.question_type NOT IN (31)
        ORDER BY fdf.order_seq

    INSERT INTO @questionsTT (order_seq, question_seq, show, group_question_seq)
        SELECT
            order_seq = (CASE WHEN (fdf.order_seq > 0 AND qg.group_order_seq IS NOT NULL) 
                                                    THEN NULL ELSE (qg.group_order_seq - 1) END),
            question_seq = df.question_seq,
            show = 0,
            group_question_seq = (CASE WHEN (fdf.order_seq > 0) THEN NULL ELSE qg.group_question_seq END)
        FROM form_data_field fdf
        INNER JOIN data_field df ON df.data_field_seq=fdf.data_field_seq
        LEFT OUTER JOIN question_group qg ON qg.question_seq = df.question_seq
        WHERE fdf.form_seq = @thisFormId AND qg.group_order_seq > 1
        ORDER BY qg.group_question_seq, qg.group_order_seq
                                
    SELECT @count2 = count(*) FROM @questionsTT
    --SELECT *  FROM @questionsTT

    UPDATE @questionsTT  SET 
    [IsSelected] = CASE when ((coalesce(ffcd.instance_seq, fftd.instance_seq, null)) IS NOT NULL) THEN 1 ELSE 0 END 
    from @questionsTT qTT
    inner join dbo.data_field df on qTT.question_seq = df.question_seq   
    left outer join dbo.form_field_choice_data ffcd on qTT.question_seq = ffcd.question_seq and  ffcd.instance_seq = @formInstanceId
    left outer join dbo.form_field_text_data fftd on df.data_field_seq = fftd.data_field_seq and  fftd.instance_seq = @formInstanceId
                                           
    IF (@count1 < 1)
        BEGIN
            INSERT INTO {4} 
                        ([form_instance_seq], [question_seq], [show], [order_seq])
            SELECT
                       [form_instance_seq] = @formInstanceId, [question_seq] = qTT.question_seq,
                        [show] = qTT.[show], [order_seq] = qTT.[order_seq]
            FROM @questionsTT qTT
            WHERE (qTT.show = 1 and qTT.[group_question_seq] Is NULL)
        END
    ELSE
        BEGIN
            INSERT INTO {4} 
                    ([form_instance_seq], [question_seq], [show], [order_seq])
            SELECT
                    [form_instance_seq] = @formInstanceId, [question_seq] = qTT.question_seq,
                    [show] = qTT.[show], [order_seq] = qTT.[order_seq]
            FROM @questionsTT qTT
            WHERE qTT.show = 1 and qTT.[group_question_seq] Is NULL 
                    AND qTT.question_seq NOT IN (SELECT question_seq FROM {4} 
                                                                    WHERE form_instance_seq = @formInstanceId)
        END
       
    --EXEC [dbo].[FORM_UpdatetQuestionListSnapShotBasedOnFormRules]      
    --            @formInstanceId = @formInstanceId, @questionId = null, @ErrorDescription = @ErrorDescription out 
                             
    UPDATE @questionsTT  SET 
    [show] = fiqs.show
    from @questionsTT qTT
    INNER JOIN {4} fiqs ON fiqs.question_seq = qTT.question_seq
    where fiqs.form_instance_seq = @formInstanceId
       
       UPDATE @questionsTT  SET 
       [isSelected] = gqs.IsSELECTED
       FROM @questionsTT qTT 
       INNER JOIN (SELECT group_question_seq AS 'group_question_seq', (CASE WHEN count(*) > 0 THEN 0 ELSE 1 END) AS 'IsSelected' FROM @questionsTT 
                                    where group_question_seq is not null  AND isSelected = 0
                                    group by group_question_seq
                             ) AS gqs ON qTT.question_seq = gqs.group_question_seq

    IF (@enableDebugInfo = '1')  --Debug
    BEGIN
        SELECT * FROM {4} WHERE form_instance_seq = @formInstanceId
        SELECT * FROM @questionsTT    
    END


       -- Answers
    IF (@requireAnswerTable = 1)
    BEGIN
             IF OBJECT_ID('TempDB..#dropdownChoices', 'U') IS NOT NULL
                    BEGIN
                           DROP TABLE #dropdownChoices
                    END

             CREATE TABLE #dropdownChoices (order_seq int IDENTITY(1, 1) primary key , question_seq int , 
                                               ListValue varchar(200), ListText varchar(200), value int, label varchar(200), country_code varchar(200))

             DECLARE @answersTT TABLE (id int IDENTITY(1, 1), QuestionOrderId int, QuestionId int, QuestionText varchar(max), QuestionTypeName varchar(max), Value varchar(200), 
                                               AnswerText varchar(max), IsSelected bit, IsCorrect bit, isGraded  bit default 0, isRequired  bit default 0, CorrectOrderSeq int, 
                                               ChoiceTag varchar(200), ChoiceOrderSeq int, IsOther bit, OtherValue varchar(max), group_question_seq int default null) --, Feedback varchar(max))
             
             INSERT INTO @answersTT (QuestionOrderId, QuestionId, QuestionText, QuestionTypeName, Value, AnswerText, IsSelected, IsCorrect, isGraded, isRequired,
                                               CorrectOrderSeq, ChoiceTag, ChoiceOrderSeq, IsOther, OtherValue, group_question_seq) --Feedback)
        SELECT 
                    qTT.Id AS 'QuestionOrderId',
                    q.question_seq AS 'QuestionId', 
                    (ISNULL(qd.question_text_html, q.question_text)  + (CASE WHEN fdf.required_ind='Y' THEN '*' ELSE '' END)+ q.custom_help_tag) AS 'QuestionText',
                                    qt.question_type_name AS 'QuestionTypeName',  
                    coalesce(CONVERT(varchar(50),qc.choice_num), fftd.data_value) AS 'Value',
                    ISNULL(qcd.choice_label_html,qc.choice_label) AS 'AnswerText',
                    (CASE WHEN ISNULL(ffcd.instance_seq,0)=0 THEN CONVERT(bit, 0) ELSE CONVERT(bit, 1) END) AS 'IsSelected',
                    (CASE WHEN qc.correct_ind = 'Y' THEN CONVERT(bit, 1) ELSE CONVERT(bit, 0) END) AS 'IsCorrect',
                    (CASE WHEN q.graded_ind='Y' THEN CONVERT(bit, 1) ELSE CONVERT(bit, 0) END) AS 'IsGraded',                              
                    (CASE WHEN fdf.required_ind='Y' THEN CONVERT(bit, 1) ELSE CONVERT(bit, 0) END) AS 'IsRequired',
                    coalesce(ISNUMERIC(ffcd.other_value),qc.correct_order_seq, 0) AS 'CorrectOrderSeq',
                (CASE WHEN ISNULL(q.choice_tag_type,0)=1 THEN CAST(qc.choice_num AS VARCHAR(10))
                             WHEN ISNULL(q.choice_tag_type,0)=2 THEN 
							    CASE WHEN qc.choice_num = '1' THEN 'A'  WHEN qc.choice_num = '2' THEN 'B'  WHEN qc.choice_num = '3' THEN 'C' 
                                     WHEN qc.choice_num = '4' THEN 'D' WHEN qc.choice_num = '5' THEN 'E' ELSE CAST(qc.choice_num AS VARCHAR(10))
                                END    
                             WHEN ISNULL(q.choice_tag_type,0)=3 THEN 
							    CASE WHEN qc.choice_num = '1' THEN 'a'  WHEN qc.choice_num = '2' THEN 'b'  WHEN qc.choice_num = '3' THEN 'c' 
                                     WHEN qc.choice_num = '4' THEN 'd' WHEN qc.choice_num = '5' THEN 'e' ELSE CAST(qc.choice_num AS VARCHAR(10))
                                END    
                             WHEN ISNULL(q.choice_tag_type,0)=4 THEN 
							    CASE WHEN qc.choice_num = '1' THEN 'I'  WHEN qc.choice_num = '2' THEN 'II'  WHEN qc.choice_num = '3' THEN 'III' 
                                     WHEN qc.choice_num = '4' THEN 'IV' WHEN qc.choice_num = '5' THEN 'V' ELSE CAST(qc.choice_num AS VARCHAR(10))
                                END    
                             WHEN ISNULL(q.choice_tag_type,0)=5 THEN 				                                                                        
                                CASE WHEN qc.choice_num = '1' THEN 'i'  WHEN qc.choice_num = '2' THEN 'ii'  WHEN qc.choice_num = '3' THEN 'iii' 
                                     WHEN qc.choice_num = '4' THEN 'iv' WHEN qc.choice_num = '5' THEN 'v' ELSE CAST(qc.choice_num AS VARCHAR(10))
                                END                                                                            
                             ELSE '' END)  AS 'ChoiceTag',   

                    qc.order_seq AS 'ChoiceOrderSeq',
                    (CASE WHEN ISNULL(qc.is_other_Choice,'N')= 'Y' THEN CONVERT(bit, 1) ELSE CONVERT(bit, 0) END) AS 'IsOther',
                    coalesce(ffcd.other_value,fftd.other_value, '') AS 'OtherValue',
                    qTT.group_question_seq
                   -- ISNULL(qcd.choice_feedback_html,'') AS 'Feedback'
        FROM @questionsTT qTT
             INNER JOIN question q ON qTT.question_seq = q.question_seq 
                         LEFT OUTER JOIN question_display qd ON qd.question_seq = q.question_seq
                                             LEFT OUTER JOIN question_type qt ON qt.question_type_seq = q.question_type
             INNER JOIN data_field df ON q.question_seq = df.question_seq  
             INNER JOIN form_data_field fdf ON fdf.data_field_seq=df.data_field_seq AND form_seq= @thisFormId
             LEFT OUTER JOIN question_choices qc on qc.question_seq = qTT.question_seq
             LEFT OUTER JOIN question_choices_display qcd on qc.question_seq = qcd.question_seq AND qc.choice_num = qcd.choice_num
             LEFT OUTER JOIN form_field_choice_data ffcd on qc.question_seq = ffcd.question_seq 
                                 AND qc.choice_num =ffcd.choice_num AND ffcd.instance_seq = @formInstanceId
             LEFT OUTER JOIN form_field_text_data fftd on fdf.data_field_seq = fftd.data_field_seq 
                                 AND fftd.instance_seq = @formInstanceId
             WHERE (qTT.[show] = 1 OR qTT.[group_question_seq] Is NOT NULL)
             AND q.question_type NOT IN (SELECT strval FROM dbo.Split('16, 17, 18, 19, 20, 21, 22, 24, 25, 27, 35', ','))
             ORDER BY qTT.[id]

             UPDATE @questionsTT SET question_type = q.question_Type 
             FROM @questionsTT AS qTT 
        INNER JOIN question q ON qTT.question_seq = q.question_seq 

             UPDATE @questionsTT SET isAnswersProcessed= 1
             FROM @questionsTT AS qTT 
        WHERE qTT.question_type NOT IN (SELECT strval FROM dbo.Split('16, 17, 18, 19, 20, 21, 22, 24, 25, 27, 35', ','))

             WHILE EXISTS (SELECT 'x' FROM @questionsTT AS qTT 
                                 WHERE qTT.isAnswersProcessed = 0 AND 
                                 qTT.question_type IN (SELECT strval FROM dbo.Split('16, 17, 18, 19, 20, 21, 22, 24, 25, 27, 35', ',')))
             BEGIN
                    SELECT TOP 1 @questionId = question_seq, @questionType = question_type, @questionOrderId = id 
                    FROM @questionsTT WHERE isAnswersProcessed = 0  

                    UPDATE @questionsTT SET isAnswersProcessed= 1 WHERE question_seq = @questionId
                    
                    TRUNCATE TABLE #dropdownChoices

                    SELECT @proc_name = proc_name, @organization_ind = (CASE WHEN UPPER(organization_ind) = 'Y' THEN 1 ELSE 0 END)
            FROM dbo.system_question_procs WHERE question_type_seq = @questionType
                    
                    IF (@organization_ind = 1) 
                BEGIN
                    SELECT @organizationId = organization_seq 
                                 FROM form f With(NoLock) WHERE f.form_seq = @thisFormId

                                 IF (@proc_name = 'FB_Quest_GetStateTerritoryList')
                                        BEGIN
                                               INSERT INTO #dropdownChoices (ListValue, ListText, value, label, country_code) EXECUTE @proc_name @organizationId
                                        END
                                 ELSE
                                        BEGIN
                                               INSERT INTO #dropdownChoices (ListValue, ListText, value, label) EXECUTE @proc_name @organizationId
                                        END
                           END
                    ELSE
                           BEGIN
                                 IF (@proc_name = 'FB_Quest_GetStateTerritoryList')
                                        BEGIN
                                               INSERT INTO #dropdownChoices (ListValue, ListText, value, label, country_code) EXECUTE @proc_name
                                        END
                                 ELSE
                                        BEGIN
                                               INSERT INTO #dropdownChoices (ListValue, ListText, value, label) EXECUTE @proc_name
                                        END
                           END                     
            UPDATE #dropdownChoices SET question_seq = @questionId

                    INSERT INTO @answersTT (QuestionOrderId, QuestionId, QuestionText, QuestionTypeName, Value, AnswerText, IsSelected, IsCorrect, isGraded, isRequired,
                                               CorrectOrderSeq, ChoiceTag, ChoiceOrderSeq, IsOther, OtherValue) --, Feedback)
                    SELECT 
                           @questionOrderId AS 'QuestionOrderId',
                @questionId AS 'QuestionId', 
                           (ISNULL(qd.question_text_html, q.question_text)  + (CASE WHEN fdf.required_ind='Y' THEN '*' ELSE '' END)+ q.custom_help_tag) AS 'QuestionText',
                                                               qt.question_type_name AS 'QuestionTypeName',  
                dc.Listvalue AS 'Value',
                dc.ListText AS 'AnswerText',
                (CASE WHEN ISNULL(fftd.instance_seq,0)=0 THEN CONVERT(bit, 0) ELSE CONVERT(bit, 1) END) AS 'IsSelected',
                (CASE WHEN qc.correct_ind = 'Y' THEN CONVERT(bit, 1) ELSE CONVERT(bit, 0) END) AS 'IsCorrect',
                           (CASE WHEN q.graded_ind='Y' THEN CONVERT(bit, 1) ELSE CONVERT(bit, 0) END) AS 'IsGraded',                              
                           (CASE WHEN fdf.required_ind='Y' THEN CONVERT(bit, 1) ELSE CONVERT(bit, 0) END) AS 'IsRequired',
                dc.order_seq AS 'CorrectOrderSeq',
                (CASE WHEN ISNULL(q.choice_tag_type,0)=1 THEN CAST(qc.choice_num AS VARCHAR(10))
                             WHEN ISNULL(q.choice_tag_type,0)=2 THEN 
							    CASE WHEN qc.choice_num = '1' THEN 'A'  WHEN qc.choice_num = '2' THEN 'B'  WHEN qc.choice_num = '3' THEN 'C' 
                                     WHEN qc.choice_num = '4' THEN 'D' WHEN qc.choice_num = '5' THEN 'E' ELSE CAST(qc.choice_num AS VARCHAR(10))
                                END    
                             WHEN ISNULL(q.choice_tag_type,0)=3 THEN 
							    CASE WHEN qc.choice_num = '1' THEN 'a'  WHEN qc.choice_num = '2' THEN 'b'  WHEN qc.choice_num = '3' THEN 'c' 
                                     WHEN qc.choice_num = '4' THEN 'd' WHEN qc.choice_num = '5' THEN 'e' ELSE CAST(qc.choice_num AS VARCHAR(10))
                                END    
                             WHEN ISNULL(q.choice_tag_type,0)=4 THEN 
							    CASE WHEN qc.choice_num = '1' THEN 'I'  WHEN qc.choice_num = '2' THEN 'II'  WHEN qc.choice_num = '3' THEN 'III' 
                                     WHEN qc.choice_num = '4' THEN 'IV' WHEN qc.choice_num = '5' THEN 'V' ELSE CAST(qc.choice_num AS VARCHAR(10))
                                END    
                             WHEN ISNULL(q.choice_tag_type,0)=5 THEN 				                                                                        
                                CASE WHEN qc.choice_num = '1' THEN 'i'  WHEN qc.choice_num = '2' THEN 'ii'  WHEN qc.choice_num = '3' THEN 'iii' 
                                     WHEN qc.choice_num = '4' THEN 'iv' WHEN qc.choice_num = '5' THEN 'v' ELSE CAST(qc.choice_num AS VARCHAR(10))
                                END                                                                            
                             ELSE '' END)  AS 'ChoiceTag',      
                dc.order_seq AS 'ChoiceOrderSeq',
                CONVERT(bit, 0) AS 'IsOther',
                '' AS 'OtherValue'
                           --'' AS 'Feedback'
            FROM #dropdownChoices dc 
            INNER JOIN question q ON q.question_seq = dc.question_seq
            INNER JOIN data_field df ON q.question_seq = df.question_seq
                    INNER JOIN form_data_field fdf ON fdf.data_field_seq=df.data_field_seq AND form_seq= @thisFormId
                    LEFT OUTER JOIN question_type qt ON qt.question_type_seq = q.question_type
            LEFT OUTER JOIN question_choices qc on qc.question_seq = q.question_seq and qc.choice_value = dc.value 
                                LEFT OUTER JOIN question_display qd ON qd.question_seq = q.question_seq
            LEFT OUTER JOIN form_field_text_data fftd ON fftd.data_value = dc.Listvalue 
                    AND fftd.data_field_seq=df.data_field_seq AND fftd.instance_seq = @formInstanceId
{2}

    END
", formID, formInstanceID, whereClause, tempTableForFormInstanceQuestionsSnapshotTable, formInstanceQuestionSnapshotTable);

            DataTable table = _WebAppDbAccess.GetDataTable(query, 120);


            // Convert to object
            var questionsAndAnswersInfo = table.AsEnumerable().Select(row => new Constants.AssQAndAs()
            {
                QuestionId = row.Field<int>("QuestionId"),

                // If the question is formatted (someone added color highlight to the question text in cme360, for example), then
                // that means the formatting will be done with HTML text. We need to strip that HTML. Also strip the tabs and 
                // leading and trailing whitespace if someone added any tabs or whitespace. Note that we only do this stripping
                // of the tabs/whitespace if the question is not required, because right now on the UI, the developers are not 
                // stripping the tabs/whitespaces inside this innerText when they have to add the asterisks for required questions
                QuestionText =
                row.IsNull("QuestionText") ? ""
                : (row.Field<bool>("IsRequired") == true ? StripHTML(row.Field<string>("QuestionText"), false).Replace("\t", " ")
                : StripHTML(row.Field<string>("QuestionText")).Replace("\t", " ").Trim()),

                // **********OLD****************
                // If the question is formatted (someone added color highlight to the question text in cme360, for example, then
                // that means the formatting will be done with HTML text. We need to strip that. Also strip if someone added any tabs
                // when creating the question                    
                //QuestionText = 
                //    row.IsNull("QuestionText") ? "" : StripHTML(row.Field<string>("QuestionText")).Replace("\t", " ").Trim(),

                QuestionTypeName = row.IsNull("QuestionTypeName") ? "" : row.Field<string>("QuestionTypeName"),
                //Value = row.IsNull("Value") ? "" : row.Field<string>("Value"),

                // If the answer is formatted (someone added color highlight to the answer text in cme360, for example), then
                // that means the formatting will be done with HTML text. We need to strip that HTML. Also strip the tabs and 
                // leading and trailing whitespace if someone added any tabs or whitespace. Also strip the 
                // numerical /alphabetical/romannumeral at the beginning of the answer is the answer is a radio button with 
                // AnswerChoice things (numbers/alphabets/romannumeral numberings) configured on it
                AnswerText = row.IsNull("AnswerText") ? "" : StripHTML(row.Field<string>("AnswerText"), true, true).Replace("\t", " ").Trim(),
                IsSelected = row.Field<bool>("IsSelected"),
                IsCorrect = row.Field<bool>("IsCorrect"),
                IsGraded = row.Field<bool>("IsGraded"),
                IsRequired = row.Field<bool>("IsRequired"),
                CorrectOrderSeq = row.Field<int>("CorrectOrderSeq"),
                //ChoiceTag = row.IsNull("ChoiceTag") ? "" : row.Field<string>("ChoiceTag"),
                //ChoiceOrderSeq = row.Field<Nullable<Int32>>("ChoiceOrderSeq"),
                IsOther = row.Field<bool>("IsOther"),
                OtherValue = row.IsNull("OtherValue") ? "" : row.Field<string>("OtherValue"),
                //Feedback = row.IsNull("Feedback") ? "" : row.Field<string>("Feedback"),
                QuestionOrderId = row.Field<int>("QuestionOrderId")

            }).ToList();

            // If test requested distinct questions, return them, else dont. Sort results by QuestionId regardless 
            List<Constants.AssQAndAs> distinctQuestionsAndAnswersInfo = null;
            if (returnDistinctQuestionsWithOneAnswer)
            {
                // Get distinct questions
                distinctQuestionsAndAnswersInfo = questionsAndAnswersInfo.GroupBy(a => a.QuestionId).Select(group => group.First()).ToList();

                // sort
                distinctQuestionsAndAnswersInfo.Sort((qoid1, qoid2) => qoid1.QuestionOrderId.CompareTo(qoid2.QuestionOrderId));

                return distinctQuestionsAndAnswersInfo;
            }
            else
            {
                // sort
                questionsAndAnswersInfo.Sort((qoid1, qoid2) => qoid1.QuestionOrderId.CompareTo(qoid2.QuestionOrderId));
                return questionsAndAnswersInfo;
            }
        }

        /// <summary>
        /// Returns a list of activity titles containing the user-specified type, title, city and bin name(s). This can be used to assert that the 
        /// search function is working on the UI.
        /// </summary>        
        /// <param name="orgCode"><see cref="Constants.OrgCodes"/></param>
        /// <param name="actType">Either "All Activities", "Live", "Online"</param>
        /// <param name="actTitle">Optional. Activity title</param>
        /// <param name="city">Optional. City where the live activity takes place</param>
        /// <returns></returns>
        public static List<string> GetActivityTitles(Constants.OrgCodes orgCode, Constants.ActivitySearchType actType,
            string actTitle = null, string city = null)
        {
            string actTypeLikeCondition = "";

            if (actType.GetDescription() == "All Activities" || actType.GetDescription() == "All")
            {
                actTypeLikeCondition = "";
            }
            else if (actType.GetDescription() == "Online")
            {
                actTypeLikeCondition = "and oa.address_type_seq is null";
            }
            else if (actType.GetDescription() == "Live")
            {
                actTypeLikeCondition = "and oa.address_type_seq is not null";
            }

            if (actTitle.IsNullOrEmpty())
            {
                actTitle = "%";
            }

            if (actTitle.IsNullOrEmpty())
            {
                actTitle = "%";
            }

            string cityLikeCondition = string.Format("and oa.city = '{0}'", city);

            if (city.IsNullOrEmpty())
            {
                cityLikeCondition = "";
            }

            string query = string.Format(@"SELECT a.activity_title
          FROM [CommandCenter].[dbo].[activity] a
          INNER JOIN dbo.organization o ON o.organization_seq = a.organization_seq
          INNER JOIN [CommandCenter].[dbo].[activity_type] acttype ON acttype.activity_type_seq = a.activity_type_seq
          LEFT JOIN [CommandCenter].[dbo].[object_address] oa ON oa.object_seq = a.object_seq
          where a.activity_title like '%{0} %'
          and o.org_code = '{1}'          
          {2}
          {3}
          order by LEN(a.activity_title), a.activity_title
        ", actTitle, orgCode.GetDescription(), actTypeLikeCondition, cityLikeCondition);

            DataTable activities = _WebAppDbAccess.GetDataTable(query, 90);

            return DataUtils.DataRowsToListString(activities.Rows);
        }

        /// <summary>
        /// Gets the bin count from the database for the user-specified activity type, activity title, and city if applicable. NOTE: This only works
        /// for the Credit Type bin group, as I didnt get a query for the other bin types
        /// </summary>
        /// <param name="orgCode"><see cref="Constants.OrgCodes"/></param>
        /// <param name="binGroup"></param>
        /// <param name="binName"></param>
        /// <param name="actType">Either "All Activities", "Live", "Online"</param>
        /// <param name="actTitle">Optional. Activity title</param>
        /// <param name="city">Optional. City where the live activity takes place</param>
        /// <returns></returns>
        public static int GetBinActivityCount(Constants.OrgCodes orgCode, string binGroup, string binName,
            Constants.ActivitySearchType actType, string actTitle = null, string city = null)
        {
            string actTypeLikeCondition = string.Format("and acttype.activity_type_desc = '{0}'", actType);

            // Remove the characters before the parenthesis
            int index = binName.LastIndexOf("(");
            if (index > 0)
                binName = binName.Substring(0, index - 1); // or index + 1 to keep slash


            if (actType.GetDescription() == "All Activities" || actType.GetDescription() == "All")
            {
                actTypeLikeCondition = "";
            }
            else if (actType.GetDescription() == "Online")
            {
                actTypeLikeCondition = "and oa.address_type_seq is null";
            }
            else if (actType.GetDescription() == "Live")
            {
                actTypeLikeCondition = "and oa.address_type_seq is not null";
            }

            string cityLikeCondition = string.Format("and oa.city = '{0}'", city);

            if (city.IsNullOrEmpty())
            {
                cityLikeCondition = "";
            }

            string query = "";

            if (binGroup.Contains("Credit Type"))
            {
                query = string.Format(@"SELECT --[AccreditationType] = 
           COUNT(CASE LEN ([acrbt].[name]) WHEN 0 THEN [o].[org_code] ELSE [o].[org_code] + ' > ' + [acrbt].[name] END)
           FROM   [dbo].[vw_ActivityIdentifiers] [ai] With(NoLock)
           JOIN   [dbo].[activity] [a] With(NoLock) ON [ai].[ActivityId] = [a].[activity_seq]
           JOIN   [dbo].[activity_body_type] [abt] (NoLock) ON (([a].[activity_seq] = [abt].[activity_seq])AND([abt].[deleted_ind] = 'N'))
           JOIN   [dbo].[accreditation_body_type] [acrbt] (NoLock) ON (([abt].[accreditation_body_type_seq] = [acrbt].[accreditation_body_type_seq])AND(([acrbt].[active_ind] = 'Y')AND([acrbt].[deleted_ind] = 'N')))
           JOIN   [dbo].[organization] [o] (NoLock) ON (([acrbt].[organization_seq] = [o].[organization_seq])AND(([o].[active_ind] = 'Y')AND([o].[deleted_ind] = 'N')))
           LEFT JOIN [CommandCenter].[dbo].[object_address] oa ON oa.object_seq = a.object_seq
           WHERE a.activity_title like '%{0} %'
           and CASE LEN ([acrbt].[name]) WHEN 0 THEN [o].[org_code] ELSE [o].[org_code] + ' > ' + [acrbt].[name] END like '%{1}%'
           {2}
           {3}
        ", actTitle, binName, actTypeLikeCondition, cityLikeCondition);
            }

            if (!binGroup.Contains("Credit Type"))
            {
                throw new Exception("This only works for the Credit Type bin group, as I didnt get a query for the other bin types");
            }

            int binCount = _WebAppDbAccess.GetDataValue<int>(query, 90);

            return binCount;
        }

        #endregion activity methods

        #region search activity methods

        /// <summary>
        /// Returns a list of bin parent groupings (standard and custom) that can be used to verify that the left-hand side bins on the 
        /// search page are accurate
        /// </summary>        
        /// <returns></returns>
        public static List<string> GetBinParents(Constants.SiteCodes siteCode)
        {
            Guid siteGuid = GetSiteGuid(siteCode);

            string query = string.Format(@"select BinLabel from vivisimo_bins_sitebins 
where siteguid='{0}' and IsActive = 1 
order by BinLabel", siteGuid.ToString());

            DataTable activities = _WebAppDbAccess.GetDataTable(query, 90);

            return DataUtils.DataRowsToListString(activities.Rows);
        }

        /// <summary>
        /// Returns a list of bin parent groupings (custom) that can be used to verify that the left-hand side bins on the 
        /// search page are accurate
        /// </summary>        
        /// <returns></returns>
        public static List<string> GetBinParents_Custom(Constants.SiteCodes siteCode)
        {
            Guid siteGuid = GetSiteGuid(siteCode);

            string query = string.Format(@"select BinLabel from vivisimo_bins_sitebins 
where siteguid='{0}' and IsActive = 1  AND BinKey LIKE 'key-%'
order by BinLabel", siteGuid.ToString());

            DataTable activities = _WebAppDbAccess.GetDataTable(query, 90);

            return DataUtils.DataRowsToListString(activities.Rows);
        }

        /// <summary>
        /// Returns the bins for Credit Type bins. NOTE: Some portals will have bin group parent labels as "Credit Type", but they are not actual
        /// standard Accreditation (Credit Type) bins. This query will only return records if the portal has 'standard' accreditation bins, and 
        /// will not return any custom "Credit Type" bins.
        /// </summary>
        /// <param name="siteCode"><see cref="Constants.SiteCodes"/></param>
        /// <param name="searchType"><see cref="Constants.ActivitySearchType"/></param>
        /// <returns></returns>
        public static List<string> GetBins_CreditType(Constants.SiteCodes siteCode, Constants.ActivitySearchType searchType)
        {
            string includeLive = "";
            string includeOnline = "";

            // Get the site Guid
            Guid siteGuid = GetSiteGuid(siteCode);

            // Condition the search type
            switch (searchType)
            {
                case Constants.ActivitySearchType.All:
                    includeLive = "true";
                    includeOnline = "true";
                    break;
                case Constants.ActivitySearchType.AllActivities:
                    includeLive = "true";
                    includeOnline = "true";
                    break;
                case Constants.ActivitySearchType.Online:
                    includeLive = "false";
                    includeOnline = "true";
                    break;
                case Constants.ActivitySearchType.Live:
                    includeLive = "true";
                    includeOnline = "false";
                    break;
                default:
                    includeLive = "true";
                    includeOnline = "true";
                    break;
            }

            string query = string.Format(@"
DECLARE @SiteGuid UniqueIdentifier, @IncludeOnline bit, @IncludeLive bit, @sqlWhereClause nvarchar(500)

--only change the site guid and IncludeOnline and IncludeLive to run for each site
SET @SiteGuid = '{0}' --change this
SET @IncludeLive = 'true' --change this
SET @IncludeOnline = 'true' --change this

IF exists(SELECT * FROM VIVISIMO_BINS_SiteBins WHERE BinKey='AccreditationType' AND IsActive=1 AND SiteGuid = @SiteGuid)
BEGIN

	IF @IncludeOnline = 'true' 
	begin
	SET @sqlWhereClause = ' AND (@IncludeOnline = ''true'' AND at.activity_type_name NOT IN (N''Live Meeting'', N''Standalone Live Meeting'', N''Live - External Activity''))'
	end
	if @IncludeLive = 'true'
	begin
		SET @sqlWhereClause = ' AND (@IncludeLive = ''true'' AND at.activity_type_name IN (N''Live Meeting'', N''Standalone Live Meeting'', N''Live - External Activity''))'
	end
	IF @IncludeOnline = 'true' and @IncludeLive = 'true'
	begin
		SET @sqlWhereClause = ''
	end

	CREATE table #tempActivities (activityGuid uniqueidentifier) 

	Declare @RemoveLiveMeetings bit
	SET @RemoveLiveMeetings = 0
	select @RemoveLiveMeetings = os.SettingValue
	from dbo.site_site ss
	inner join dbo.[object] o on ss.object_seq = o.object_seq
	inner join FRMK_Object fo on fo.siteId = ss.site_seq and fo.ObjectTypeId = 96
	inner join LCF_ObjectSettings os on os.ObjectId = fo.ObjectId
	where o.guid = @SiteGuid 
	and os.SettingID = 274

	DECLARE @sql nVARCHAR(MAX)

	IF @RemoveLiveMeetings = 1
	begin
	 

		SELECT @sql = 
		'insert INTO #tempActivities
			SELECT [a].[ActivityGuid]					 
					FROM [dbo].[vw_SiteIdentifiers] [s] With(NoLock)
					JOIN [dbo].[SITE_site_activity] [ssa] With(NoLock) ON (([s].[SiteGuid] = @SiteGuid)AND([s].[SiteId] = [ssa].[site_seq])AND([ssa].[final_ind] = ''Y''))
					JOIN [dbo].[vw_ActivityIdentifiers] [a] With(NoLock) ON [ssa].[activity_seq] = [a].[ActivityId]
					JOIN [dbo].[activity] act with(nolock) on ssa.activity_seq = act.activity_seq
					JOIN dbo.vw_ACTIVITY_InterventionActivities [ia] with(nolock) ON ssa.activity_seq = ia.activity_seq and ssa.site_seq = ia.site_seq
					JOIN activity_type [at] (NOLOCK) ON at.activity_type_seq = a.ActivityTypeId
					JOIN [dbo].[vw_DistributedActivities] [da] With(NoLock) ON [a].[ActivityGuid] = [da].[ActivityGuid]
					INNER JOIN dbo.LCF_CME360ObjectMapping com with(NoLock) on a.ActivityObjectId = com.CME360ObjectID
					INNER JOIN dbo.FRMK_Object fo with(nolock) on com.ObjectID = fo.ObjectID and fo.SiteID = ssa.site_seq
					LEFT JOIN dbo.LCF_ObjectRatingSummary ors with(NoLock) on com.ObjectID = ors.ObjectID and ors.SiteId = ssa.site_seq
					LEFT JOIN [dbo].[site_activity_override] sao With(NoLock) on [sao].[activity_seq] = [ssa].[activity_seq] and [sao].[site_seq] = [ssa].[site_seq]
					WHERE ([sao].[IsHidden] is null OR [sao].[IsHidden] = 0)	
					AND ((act.end_date > getdate()) OR (act.end_date is null AND (CONVERT (DATE, act.date_expiry) >= CONVERT (DATE, getdate()) or act.date_expiry is null)) 
						OR (act.activity_type_seq NOT IN (2,3) AND (CONVERT (DATE, act.date_expiry) >= CONVERT (DATE, getdate()) or act.date_expiry is null)))'

		IF @sqlWhereClause != ''
		BEGIN
			SELECT @sql = @sql + @sqlWhereClause
		END

		EXEC sys.sp_executesql @sql,
							   N'@SiteGuid uniqueidentifier, @IncludeOnline bit, @IncludeLive bit',
							   @SiteGuid,
							   @IncludeOnline,
							   @IncludeLive;


		SELECT DISTINCT
				REPLACE( CASE LEN ([acrbt].[name]) WHEN 0 THEN [o].[org_code] ELSE [o].[org_code] + ' > ' + [acrbt].[name] END, 'abim_c', 'ABIM') [Credit Type]
		   FROM   [dbo].[vw_ActivityIdentifiers] [ai] With(NoLock)
		   JOIN   [dbo].[activity] [a] With(NoLock) ON (([ai].[ActivityId] = [a].[activity_seq]))
		   JOIN   [dbo].[activity_body_type] [abt] (NoLock) ON (([a].[activity_seq] = [abt].[activity_seq])AND([abt].[deleted_ind] = 'N'))
		   JOIN   [dbo].[accreditation_body_type] [acrbt] (NoLock) ON (([abt].[accreditation_body_type_seq] = [acrbt].[accreditation_body_type_seq])AND(([acrbt].[active_ind] = 'Y')AND([acrbt].[deleted_ind] = 'N')))
		   JOIN   [dbo].[organization] [o] (NoLock) ON (([acrbt].[organization_seq] = [o].[organization_seq])AND(([o].[active_ind] = 'Y')AND([o].[deleted_ind] = 'N')))
		   JOIN #tempActivities	b ON b.ActivityGuid = ai.ActivityGuid

	end
	else
		begin
	
			SELECT @sql = 
			'insert INTO #tempActivities
			SELECT [a].[ActivityGuid]					 
						FROM [dbo].[vw_SiteIdentifiers] [s] With(NoLock)
						JOIN [dbo].[SITE_site_activity] [ssa] With(NoLock) ON (([s].[SiteGuid] = @SiteGuid )AND([s].[SiteId] = [ssa].[site_seq])AND([ssa].[final_ind] = ''Y''))
						JOIN [dbo].[vw_ActivityIdentifiers] [a] With(NoLock) ON [ssa].[activity_seq] = [a].[ActivityId]
						JOIN dbo.vw_ACTIVITY_InterventionActivities [ia] with(nolock) ON ssa.activity_seq = ia.activity_seq and ssa.site_seq = ia.site_seq
						JOIN activity_type [at] (NOLOCK) ON at.activity_type_seq = a.ActivityTypeId
						JOIN [dbo].[vw_DistributedActivities] [da] With(NoLock) ON [a].[ActivityGuid] = [da].[ActivityGuid]
						INNER JOIN dbo.LCF_CME360ObjectMapping com with(NoLock) on a.ActivityObjectId = com.CME360ObjectID
						INNER JOIN dbo.FRMK_Object fo with(nolock) on com.ObjectID = fo.ObjectID and fo.SiteID = ssa.site_seq
						LEFT JOIN dbo.LCF_ObjectRatingSummary ors with(NoLock) on com.ObjectID = ors.ObjectID and ors.SiteId = ssa.site_seq
						LEFT JOIN [dbo].[site_activity_override] sao With(NoLock) on [sao].[activity_seq] = [ssa].[activity_seq] and [sao].[site_seq] = [ssa].[site_seq]
						WHERE ([sao].[IsHidden] is null OR [sao].[IsHidden] = 0)'

		IF @sqlWhereClause != ''
		BEGIN
			SELECT @sql = @sql + @sqlWhereClause
		END

		EXEC sys.sp_executesql @sql,
							   N'@SiteGuid uniqueidentifier, @IncludeOnline bit, @IncludeLive bit',
							   @SiteGuid,
							   @IncludeOnline,
							   @IncludeLive;


		SELECT DISTINCT
				REPLACE( CASE LEN ([acrbt].[name]) WHEN 0 THEN [o].[org_code] ELSE [o].[org_code] + ' > ' + [acrbt].[name] END, 'abim_c', 'ABIM') [Credit Type]
		   FROM   [dbo].[vw_ActivityIdentifiers] [ai] With(NoLock)
		   JOIN   [dbo].[activity] [a] With(NoLock) ON (([ai].[ActivityId] = [a].[activity_seq]))
		   JOIN   [dbo].[activity_body_type] [abt] (NoLock) ON (([a].[activity_seq] = [abt].[activity_seq])AND([abt].[deleted_ind] = 'N'))
		   JOIN   [dbo].[accreditation_body_type] [acrbt] (NoLock) ON (([abt].[accreditation_body_type_seq] = [acrbt].[accreditation_body_type_seq])AND(([acrbt].[active_ind] = 'Y')AND([acrbt].[deleted_ind] = 'N')))
		   JOIN   [dbo].[organization] [o] (NoLock) ON (([acrbt].[organization_seq] = [o].[organization_seq])AND(([o].[active_ind] = 'Y')AND([o].[deleted_ind] = 'N')))
		   JOIN #tempActivities b ON b.ActivityGuid = ai.ActivityGuid
	END

	DROP TABLE #tempActivities

END
ELSE
BEGIN
	SELECT top 0 BinLabel from VIVISIMO_BINS_SiteBins
END

--SELECT * FROM vw_ActivityIdentifiers WHERE ActivityGuid='E9CCEC36-E3D9-44E9-B4F3-3AECC535AA1B'
--SELECT * FROM activity WHERE activity_seq=6430640

--SELECT * FROM activity_type

", siteGuid.ToString(), includeLive, includeOnline);

            DataTable binsDT = new DataTable();
            List<string> binsLS = new List<string>() { };

            try
            {
                binsDT = _WebAppDbAccess.GetDataTable(query, 120);
            }
            catch
            {
            }

            binsLS = DataUtils.DataRowsToListString(binsDT.Rows);

            return binsLS;
        }

        /// <summary>
        /// Returns the bin count from the database for Credit Type bins. NOTE: Some portals will have bin group parent labels as "Credit Type", 
        /// but they are not actual standard Accreditation (Credit Type) bins. This query will only return records if the portal has 'standard' 
        /// accreditation bins, and will not return any custom "Credit Type" bins.
        /// </summary>
        /// <param name="siteCode"><see cref="Constants.SiteCodes"/></param>
        /// <param name="searchType"><see cref="Constants.ActivitySearchType"/></param>
        /// <returns></returns>
        public static int GetBinCount_CreditType(Constants.SiteCodes siteCode, Constants.ActivitySearchType searchType)
        {
            return GetBins_CreditType(siteCode, searchType).Count;
        }

        /// <summary>
        /// Returns the bins for a tester-specified custom set of bins
        /// </summary>
        /// <param name="siteCode"><see cref="Constants.SiteCodes"/></param>
        /// <param name="searchType"><see cref="Constants.ActivitySearchType"/></param>
        /// <returns></returns>
        public static List<string> GetBins_Custom(Constants.SiteCodes siteCode, Constants.ActivitySearchType searchType,
            string customBinGroupName)
        {
            string includeLive = "";
            string includeOnline = "";

            // Get the site Guid
            Guid siteGuid = GetSiteGuid(siteCode);

            // Condition the search type
            switch (searchType)
            {
                case Constants.ActivitySearchType.All:
                    includeLive = "true";
                    includeOnline = "true";
                    break;
                case Constants.ActivitySearchType.AllActivities:
                    includeLive = "true";
                    includeOnline = "true";
                    break;
                case Constants.ActivitySearchType.Online:
                    includeLive = "false";
                    includeOnline = "true";
                    break;
                case Constants.ActivitySearchType.Live:
                    includeLive = "true";
                    includeOnline = "false";
                    break;
            }

            string query = string.Format(@"DECLARE @siteGuid uniqueidentifier

SELECT @siteGuid = si.SiteGuid 
FROM SITE_site s
JOIN vw_SiteIdentifiers si ON s.site_seq = si.SiteId AND s.published_version_ind='N'  AND s.active_ind = 'Y' and s.deleted_ind = 'N'
WHERE s.site_code = 'aha'


SELECT DISTINCT ig.GroupName [Parent Bin], it.TermID [Child Bin], count(1) [Activity count]
FROM VIVISIMO_BINS_SiteBins sb (NOLOCK)
JOIN INDEX_IndexGroup ig (NOLOCK) ON ig.IndexGroupID = right(sb.BinKey, CHARINDEX('-', REVERSE(sb.BinKey))-1) AND ig.IsActive=1 AND ig.IsDeleted = 0
JOIN INDEX_IndexTerm it (NOLOCK) ON ig.IndexGroupID = it.IndexGroupID AND it.IsActive = 1 AND it.IsDeleted = 0
JOIN INDEX_ObjectIndexTerm oit (NOLOCK) ON oit.IndexTermID = it.IndexTermID
JOIN (
	SELECT ia.*
		FROM [dbo].[vw_SiteIdentifiers] [s] With(NoLock)
		JOIN [dbo].[SITE_site_activity] [ssa] With(NoLock) ON (([s].[SiteGuid] = @siteGuid)AND([s].[SiteId] = [ssa].[site_seq])AND([ssa].[final_ind] = 'Y'))
		JOIN [dbo].[vw_ActivityIdentifiers] [a] With(NoLock) ON [ssa].[activity_seq] = [a].[ActivityId]
		JOIN dbo.vw_ACTIVITY_InterventionActivities [ia] with(nolock) ON ssa.activity_seq = ia.activity_seq and ssa.site_seq = ia.site_seq
		JOIN [dbo].[vw_DistributedActivities] [da] With(NoLock) ON [a].[ActivityGuid] = [da].[ActivityGuid]
		INNER JOIN dbo.LCF_CME360ObjectMapping com with(NoLock) on a.ActivityObjectId = com.CME360ObjectID
		INNER JOIN dbo.FRMK_Object fo with(nolock) on com.ObjectID = fo.ObjectID and fo.SiteID = ssa.site_seq
		LEFT JOIN dbo.LCF_ObjectRatingSummary ors with(NoLock) on com.ObjectID = ors.ObjectID and ors.SiteId = ssa.site_seq
		LEFT JOIN [dbo].[site_activity_override] sao With(NoLock) on [sao].[activity_seq] = [ssa].[activity_seq] and [sao].[site_seq] = [ssa].[site_seq]
		WHERE ([sao].[IsHidden] is null OR [sao].[IsHidden] = 0)
	)b ON b.object_seq = oit.ObjectID
WHERE sb.SiteGuid= @siteGuid
AND sb.BinKey LIKE 'key-%'
GROUP BY ig.GroupName, it.TermID
", siteGuid.ToString(), includeLive, includeOnline);

            DataTable binsDT = _WebAppDbAccess.GetDataTable(query, 120);

            List<string> binsLS = DataUtils.DataRowsToListString(binsDT.Rows);

            return binsLS;
        }

        /// <summary>
        /// Returns the bin count for a tester-specified custom set of bins
        /// </summary>
        /// <param name="siteCode"><see cref="Constants.SiteCodes"/></param>
        /// <param name="searchType"><see cref="Constants.ActivitySearchType"/></param>
        /// <param name="customBinGroupName">The name of the custom bin group</param>
        /// <returns></returns>
        public static int GetBinCount_Custom(Constants.SiteCodes siteCode, Constants.ActivitySearchType searchType,
            string customBinGroupName)
        {
            return GetBins_CreditType(siteCode, searchType).Count;
        }
        #endregion search activity methods

        #region security methods

        /// <summary>
        /// Whitelists an IP address for a given site. Once whitelisted, you can execute any API call while on the VPN
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="siteCode"></param>
        public static void WhitelistIPAddressForSite(string ipAddress, Constants.SiteCodes siteCode)
        {
            int siteSeq = GetSiteSequence(siteCode);

            // Get all the sites accounts in the API Account table
            var accountsTable = _WebAppDbAccess.GetDataTable(string.Format("SELECT id FROM [Security].[api].account WHERE siteId = {0}", siteSeq));
            List<int> accounts = DataUtils.DataRowsToListInt(accountsTable.Rows, "id");

            foreach (var account in accounts)
            {
                string query = string.Format(@"Insert into [Security].[api].[IpAddress]  
(AuditInfo_Created, AuditInfo_CreatedBy, AuditInfo_Modified, AuditInfo_ModifiedBy, Value, Account_Id, IpAddressType)
Values 
(GETDATE(), 1, GETDATE(), 1 , '{0}', {1}, 0)", ipAddress, account);

                _WebAppDbAccess.ExecuteNonQuery(query, 120);

            };
        }

        /// <summary>
        /// Turns off the IP Address Whitelist filter setting for a tester-specified API account. Use this before you execute the 
        /// Get Token call. After executing this, you can then generate a token that will not require your IP Address to be 
        /// whitelisted when executing any API call
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="siteCode"><see cref="Constants.SiteCodes"/></param>
        public static void SetIPWhitelistFilterOff(string APIAccountKeyUserName)
        {
            string query = string.Format(@"
UPDATE security.api.Account
SET ApiSecuritySettings_CheckAccountIpAddress = 0
WHERE [Key] = '{0}' 
", APIAccountKeyUserName);

            _WebAppDbAccess.ExecuteNonQuery(query, 120);
        }

        /// <summary>
        /// Inserts the IP address of a machine so that it can access the backdoor login
        /// </summary>
        /// <param name="ipAddress"></param>
        public static void InsertIPForBackdoorLogin(string ipAddress)
        {
            string query = string.Format(@"
INSERT INTO [Lifetime].[dbo].[FRMK_IpAddressWhiteList]  (IpAddress, DateCreated)
VALUES ('{0}', GETDATE());
", ipAddress);

            _WebAppDbAccess.ExecuteNonQuery(query, 120);
        }

        /// <summary>
        /// Gets the AccessToken assigned to a user that is currently logged into a portal. This token can be used in different ways. 
        /// For example, you can use this token to unregister a user from an activity. To do this, 
        /// <see cref="APIHelp.DeleteActivityForUser(string, string, Constants.SiteCodes)"/>
        /// For further info, see https://code.premierinc.com/docs/display/PGHLMSDOCS/Delete+Activity+For+User"/>
        /// </summary>
        /// <param name="username">The username of the current user you are logged in with</param>
        /// <param name="siteCode"><see cref="Constants.SiteCodes"/></param>
        /// <returns></returns>
        public static string GetUserAccessToken(string username, Constants.SiteCodes siteCode)
        {
            int site_Seq = GetSiteSequence(siteCode);
            string query = string.Format(@"select top 1 AccessToken 
from [Security].[api].[ApiAccessToken] 
where Username = '{0}'
and SiteId = {1}
order by Expiration desc", username, site_Seq);

            return _WebAppDbAccess.GetDataValue<string>(query, 120);
        }

        #endregion security methods

        #endregion methods


    }
}



