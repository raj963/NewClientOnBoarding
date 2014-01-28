using ClientOnBoarding.Models;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using ClientOnBoarding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;


namespace ClientOnBoarding.BAL
{
    public class BLCustomer
    {

        public const string GET_TOOLINFO = "spGetTooInformations";
        public const string GET_CUSTOMER = "spGetCustomer";
        public const string GET_PATCHING = "spGetPatchingPolicy";
        public const string GET_ACCESSPLOY = "spGetAccessPolicy";
        public const string SET_CUSTOMER = "spSetCustomer";
        public const string GET_MAINTENANCE = "spGetMaintenancePolicies";
        public const string GET_AVPINDX = "spGetAntiVirusPolicies";
        public const string GET_BACKUPPOLICIES = "spGetBackUpPolicies";
        public const string GET_BACKUPPOLICY = "spGetBackUpPolicy";
        public const string GET_ANTIVIRUS = "spGetAntiVirusPolicy";
        public const string GET_MAINTENANCEEDIT = "spGetMaintenancePolicy";
        public const string DELETE_CUSTOMER = "spDelAdminCustomerDetails";
        public const string DELETE_BACKUPPOLICY = "spDelPolicy";

        public const string SET_ToolInfo = "spSettoolInfomations";
        public const string SET_MaintenancePolicy = "spSetMaintenancePolicy";
        public const string SET_PatchingPolicy = "spSetPatchingPolicy";
        public const string SET_AntiVirusPolicy = "spSetAntiVirusPolicy";
        public const string SET_AccessPolicy = "spSetAccessPolicy";
        public const string SET_BackUpPolicy = "spSetBackUpPolicy";

        public int SaveCustomer(tblCustomerDetails CustomerDetails)
        {
            int errorNum = 0;
            string errorDesc = "";
            DataAccess.resetParams();
            DataAccess.addSqlParam("@CustomerID", ParameterDirection.Input, 16, MySqlDbType.Int32, CustomerDetails.CustomerID);
            DataAccess.addSqlParam("@CustomerName", ParameterDirection.Input, 100, MySqlDbType.VarChar, CustomerDetails.CustomerName);
            DataAccess.addSqlParam("@EmailAddress", ParameterDirection.Input, 150, MySqlDbType.VarChar, CustomerDetails.EmailAddress);
            DataAccess.addSqlParam("@CustomerContactName", ParameterDirection.Input, 100, MySqlDbType.VarChar, CustomerDetails.CustomerContactName);
            DataAccess.addSqlParam("@oPassword", ParameterDirection.Input, 60, MySqlDbType.VarChar, CustomerDetails.Password);
            DataAccess.addSqlParam("@TimeZone", ParameterDirection.Input, 16, MySqlDbType.Int32, CustomerDetails.TimeZone.ID.Value);
            DataAccess.addSqlParam("@NOCCommunicationBy", ParameterDirection.Input, 16, MySqlDbType.Int32, CustomerDetails.NOCCommunication.ID);
            DataAccess.addSqlParam("@Identity", ParameterDirection.InputOutput, 16, MySqlDbType.Int32);
            DataAccess.ExecuteNonQuery(SET_CUSTOMER, ref errorNum, ref errorDesc);

            return Common.ConvertToInt(DataAccess.getSQLParam("@Identity").ToString());
        }

        public tbltoolInfomation GetToolInfo(int CustomerID)
        {
            int errorNum = 0;
            string errorDesc = "";
            DataAccess.resetParams();
            DataAccess.addSqlParam("@CustomerID", ParameterDirection.Input, 16, MySqlDbType.Int32, CustomerID);
            DataAccess.addSqlParam("@ToolID", ParameterDirection.Input, 16, MySqlDbType.Int32, 0);
            DataSet ds = DataAccess.ExecuteDataSet(GET_TOOLINFO, ref errorNum, ref errorDesc);
            tbltoolInfomation toolinformation = new tbltoolInfomation();
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dtContact = ds.Tables[0];
                if (dtContact != null && dtContact.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtContact.Rows)
                    {
                        if (Common.ConvertToInt(dr, "ToolType") == 1)
                        {


                            toolinformation.ToolIDRT = Common.ConvertToInt(dr, "ToolId");
                            toolinformation.RMMTool = Common.ConvertToString(dr, "ToolName");
                            toolinformation.RMMUrl = Common.ConvertToString(dr, "Tool_URL");
                            toolinformation.RMMToolUserName = Common.ConvertToString(dr, "UserName");
                            toolinformation.RMMToolPassword = Common.ConvertToString(dr, "Password");
                            toolinformation.ToolTypeRT = Common.ConvertToInt(dr, "ToolType");



                        }
                        else if (Common.ConvertToInt(dr, "ToolType") == 2)
                        {
                            toolinformation.ToolIDPT = Common.ConvertToInt(dr, "ToolId");
                            toolinformation.PSATool = Common.ConvertToString(dr, "ToolName");
                            toolinformation.PSAUrl = Common.ConvertToString(dr, "Tool_URL");
                            toolinformation.PSAToolUserName = Common.ConvertToString(dr, "UserName");
                            toolinformation.PSAToolPassword = Common.ConvertToString(dr, "Password");
                            toolinformation.ToolTypePT = Common.ConvertToInt(dr, "ToolType");

                        }

                    }

                }
            }
            return toolinformation;
        }

        public tbltoolInfomation GetToolInfoByID(int ToolID)
        {
            int errorNum = 0;
            string errorDesc = "";
            DataAccess.resetParams();
            DataAccess.addSqlParam("@CustomerID", ParameterDirection.Input, 16, MySqlDbType.Int32, 0);
            DataAccess.addSqlParam("@ToolID", ParameterDirection.Input, 16, MySqlDbType.Int32, ToolID);
            DataSet ds = DataAccess.ExecuteDataSet(GET_TOOLINFO, ref errorNum, ref errorDesc);
            tbltoolInfomation toolinformation = new tbltoolInfomation();
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dtContact = ds.Tables[0];
                if (dtContact != null && dtContact.Rows.Count > 0)
                {
                    DataRow dr = dtContact.Rows[0];
                    switch (Common.ConvertToInt(dr, "ToolType"))
                    {
                        case 1:
                            toolinformation.ToolIDRT = Common.ConvertToInt(dr, "ToolId");
                            toolinformation.RMMTool = Common.ConvertToString(dr, "ToolName");
                            toolinformation.RMMUrl = Common.ConvertToString(dr, "Tool_URL");
                            toolinformation.RMMToolUserName = Common.ConvertToString(dr, "UserName");
                            toolinformation.RMMToolPassword = Common.ConvertToString(dr, "Password");
                            toolinformation.ToolTypeRT = Common.ConvertToInt(dr, "ToolType");
                            break;
                        case 2:
                            toolinformation.ToolIDPT = Common.ConvertToInt(dr, "ToolId");
                            toolinformation.PSATool = Common.ConvertToString(dr, "ToolName");
                            toolinformation.PSAUrl = Common.ConvertToString(dr, "Tool_URL");
                            toolinformation.PSAToolUserName = Common.ConvertToString(dr, "UserName");
                            toolinformation.PSAToolPassword = Common.ConvertToString(dr, "Password");
                            toolinformation.ToolTypePT = Common.ConvertToInt(dr, "ToolType");
                            break;
                    }
                }
            }
            return toolinformation;
        }

        public int SaveToolInfo(tbltoolInfomation toolinfo)
        {
            int errorNum = 0;
            string errorDesc = "";

            DataAccess.resetParams();
            DataAccess.addSqlParam("@ToolId", ParameterDirection.Input, 16, MySqlDbType.Int32, toolinfo.ToolIDRT);
            DataAccess.addSqlParam("@CustomerID", ParameterDirection.Input, 16, MySqlDbType.Int32, toolinfo.CustomerID);
            DataAccess.addSqlParam("@ToolName", ParameterDirection.Input, 50, MySqlDbType.VarChar, toolinfo.RMMTool);
            DataAccess.addSqlParam("@Tool_URL", ParameterDirection.Input, 200, MySqlDbType.VarChar, toolinfo.RMMUrl);
            DataAccess.addSqlParam("@UserName", ParameterDirection.Input, 50, MySqlDbType.VarChar, toolinfo.RMMToolUserName);
            DataAccess.addSqlParam("@oPassword", ParameterDirection.Input, 50, MySqlDbType.VarChar, toolinfo.RMMToolPassword);
            DataAccess.addSqlParam("@ToolType", ParameterDirection.Input, 16, MySqlDbType.Int32, toolinfo.ToolTypeRT = 1);
            DataAccess.addSqlParam("@Identity", ParameterDirection.InputOutput, 16, MySqlDbType.Int32);
            DataAccess.ExecuteNonQuery(SET_ToolInfo, ref errorNum, ref errorDesc);

            DataAccess.resetParams();
            DataAccess.addSqlParam("@ToolId", ParameterDirection.Input, 16, MySqlDbType.Int32, toolinfo.ToolIDPT);
            DataAccess.addSqlParam("@CustomerID", ParameterDirection.Input, 16, MySqlDbType.Int32, toolinfo.CustomerID);
            DataAccess.addSqlParam("@ToolName", ParameterDirection.Input, 50, MySqlDbType.VarChar, toolinfo.PSATool);
            DataAccess.addSqlParam("@Tool_URL", ParameterDirection.Input, 200, MySqlDbType.VarChar, toolinfo.PSAUrl);
            DataAccess.addSqlParam("@UserName", ParameterDirection.Input, 50, MySqlDbType.VarChar, toolinfo.PSAToolUserName);
            DataAccess.addSqlParam("@oPassword", ParameterDirection.Input, 50, MySqlDbType.VarChar, toolinfo.PSAToolPassword);
            DataAccess.addSqlParam("@ToolType", ParameterDirection.Input, 16, MySqlDbType.Int32, toolinfo.ToolTypePT = 2);
            DataAccess.addSqlParam("@Identity", ParameterDirection.InputOutput, 16, MySqlDbType.Int32);
            DataAccess.ExecuteNonQuery(SET_ToolInfo, ref errorNum, ref errorDesc);

            return Common.ConvertToInt(DataAccess.getSQLParam("@Identity").ToString());
        }

        public int SaveAccessPolicy(tblAccessPolicy AccessPolicy)
        {

            int errorNum = 0;
            string errorDesc = "";
            AccessPolicy.AccessPolicyType = new PolicyType();


            if (AccessPolicy.RemoteControlPermissionWR.ID.Value == 2)
            {
                DataAccess.resetParams();
                DataAccess.addSqlParam("@AccessPolicyID", ParameterDirection.Input, 16, MySqlDbType.Int32, AccessPolicy.AccessPolicyIDWR);
                DataAccess.addSqlParam("@CustomerID", ParameterDirection.Input, 16, MySqlDbType.Int32, AccessPolicy.CustomerID);
                DataAccess.addSqlParam("@RemoteControlPermission", ParameterDirection.Input, 16, MySqlDbType.Int32, null);
                DataAccess.addSqlParam("@RemoteAccessStartTime", ParameterDirection.Input, 10, MySqlDbType.VarChar, null);
                DataAccess.addSqlParam("@RemoteAccessEndTime", ParameterDirection.Input, 10, MySqlDbType.VarChar, null);
                DataAccess.addSqlParam("@RemoteAccessTimeZone", ParameterDirection.Input, 16, MySqlDbType.Int32, null);
                DataAccess.addSqlParam("@RebootPermission", ParameterDirection.Input, 16, MySqlDbType.Int32, null);
                DataAccess.addSqlParam("@RebootWindowStartTime", ParameterDirection.Input, 10, MySqlDbType.VarChar, null);
                DataAccess.addSqlParam("@RebootWindowEndTime", ParameterDirection.Input, 10, MySqlDbType.VarChar, null);
                DataAccess.addSqlParam("@AccessPolicyType", ParameterDirection.Input, 16, MySqlDbType.Int32, AccessPolicy.AccessPolicyType.ID = 1);
                DataAccess.addSqlParam("@RebootTimeZone", ParameterDirection.Input, 16, MySqlDbType.Int32, null);
                DataAccess.addSqlParam("@Identity", ParameterDirection.InputOutput, 16, MySqlDbType.Int32);
                DataAccess.ExecuteNonQuery(SET_AccessPolicy, ref errorNum, ref errorDesc);
            }
            else
            {

                DataAccess.resetParams();
                DataAccess.addSqlParam("@AccessPolicyID", ParameterDirection.Input, 16, MySqlDbType.Int32, AccessPolicy.AccessPolicyIDWR);
                DataAccess.addSqlParam("@CustomerID", ParameterDirection.Input, 16, MySqlDbType.Int32, AccessPolicy.CustomerID);
                DataAccess.addSqlParam("@RemoteControlPermission", ParameterDirection.Input, 16, MySqlDbType.Int32, AccessPolicy.RemoteControlPermissionWR.ID);
                DataAccess.addSqlParam("@RemoteAccessStartTime", ParameterDirection.Input, 10, MySqlDbType.VarChar, AccessPolicy.RemoteAccessStartTimeWR);
                DataAccess.addSqlParam("@RemoteAccessEndTime", ParameterDirection.Input, 10, MySqlDbType.VarChar, AccessPolicy.RemoteAccessEndTimeWR);
                DataAccess.addSqlParam("@RemoteAccessTimeZone", ParameterDirection.Input, 16, MySqlDbType.Int32, AccessPolicy.RemoteAccessTimeZoneWR.ID);
                DataAccess.addSqlParam("@RebootPermission", ParameterDirection.Input, 16, MySqlDbType.Int32, AccessPolicy.RebootPermissionWR.ID);
                DataAccess.addSqlParam("@RebootWindowStartTime", ParameterDirection.Input, 10, MySqlDbType.VarChar, AccessPolicy.RebootWindowStartTimeWR);
                DataAccess.addSqlParam("@RebootWindowEndTime", ParameterDirection.Input, 10, MySqlDbType.VarChar, AccessPolicy.RebootWindowEndTimeWR);
                DataAccess.addSqlParam("@AccessPolicyType", ParameterDirection.Input, 16, MySqlDbType.Int32, AccessPolicy.AccessPolicyType.ID = 1);
                DataAccess.addSqlParam("@RebootTimeZone", ParameterDirection.Input, 16, MySqlDbType.Int32, AccessPolicy.RebootTimeZoneWR.ID);
                DataAccess.addSqlParam("@Identity", ParameterDirection.InputOutput, 16, MySqlDbType.Int32);
                DataAccess.ExecuteNonQuery(SET_AccessPolicy, ref errorNum, ref errorDesc);
            }

            if (AccessPolicy.RemoteControlPermissionSR.ID.Value == 2)
            {
                DataAccess.resetParams();
                DataAccess.addSqlParam("@AccessPolicyID", ParameterDirection.Input, 16, MySqlDbType.Int32, AccessPolicy.AccessPolicyIDSR);
                DataAccess.addSqlParam("@CustomerID", ParameterDirection.Input, 16, MySqlDbType.Int32, AccessPolicy.CustomerID);
                DataAccess.addSqlParam("@RemoteControlPermission", ParameterDirection.Input, 16, MySqlDbType.Int32, null);
                DataAccess.addSqlParam("@RemoteAccessStartTime", ParameterDirection.Input, 10, MySqlDbType.VarChar, null);
                DataAccess.addSqlParam("@RemoteAccessEndTime", ParameterDirection.Input, 10, MySqlDbType.VarChar, null);
                DataAccess.addSqlParam("@RemoteAccessTimeZone", ParameterDirection.Input, 16, MySqlDbType.Int32, null);
                DataAccess.addSqlParam("@RebootPermission", ParameterDirection.Input, 16, MySqlDbType.Int32, null);
                DataAccess.addSqlParam("@RebootWindowStartTime", ParameterDirection.Input, 10, MySqlDbType.VarChar, null);
                DataAccess.addSqlParam("@RebootWindowEndTime", ParameterDirection.Input, 10, MySqlDbType.VarChar, null);
                DataAccess.addSqlParam("@AccessPolicyType", ParameterDirection.Input, 16, MySqlDbType.Int32, AccessPolicy.AccessPolicyType.ID = 2);
                DataAccess.addSqlParam("@RebootTimeZone", ParameterDirection.Input, 16, MySqlDbType.Int32, null);
                DataAccess.addSqlParam("@Identity", ParameterDirection.InputOutput, 16, MySqlDbType.Int32);
                DataAccess.ExecuteNonQuery(SET_AccessPolicy, ref errorNum, ref errorDesc);
            }
            else
            {
                DataAccess.resetParams();
                DataAccess.addSqlParam("@AccessPolicyID", ParameterDirection.Input, 16, MySqlDbType.Int32, AccessPolicy.AccessPolicyIDSR);
                DataAccess.addSqlParam("@CustomerID", ParameterDirection.Input, 16, MySqlDbType.Int32, AccessPolicy.CustomerID);
                DataAccess.addSqlParam("@RemoteControlPermission", ParameterDirection.Input, 16, MySqlDbType.Int32, AccessPolicy.RemoteControlPermissionSR.ID);
                DataAccess.addSqlParam("@RemoteAccessStartTime", ParameterDirection.Input, 10, MySqlDbType.VarChar, AccessPolicy.RemoteAccessStartTimeSR);
                DataAccess.addSqlParam("@RemoteAccessEndTime", ParameterDirection.Input, 10, MySqlDbType.VarChar, AccessPolicy.RemoteAccessEndTimeSR);
                DataAccess.addSqlParam("@RemoteAccessTimeZone", ParameterDirection.Input, 16, MySqlDbType.Int32, AccessPolicy.RemoteAccessTimeZoneSR.ID);
                DataAccess.addSqlParam("@RebootPermission", ParameterDirection.Input, 16, MySqlDbType.Int32, AccessPolicy.RebootPermissionSR.ID);
                DataAccess.addSqlParam("@RebootWindowStartTime", ParameterDirection.Input, 10, MySqlDbType.VarChar, AccessPolicy.RebootWindowStartTimeSR);
                DataAccess.addSqlParam("@RebootWindowEndTime", ParameterDirection.Input, 10, MySqlDbType.VarChar, AccessPolicy.RebootWindowEndTimeSR);
                DataAccess.addSqlParam("@AccessPolicyType", ParameterDirection.Input, 16, MySqlDbType.Int32, AccessPolicy.AccessPolicyType.ID = 2);
                DataAccess.addSqlParam("@RebootTimeZone", ParameterDirection.Input, 16, MySqlDbType.Int32, AccessPolicy.RebootTimeZoneSR.ID);
                DataAccess.addSqlParam("@Identity", ParameterDirection.InputOutput, 16, MySqlDbType.Int32);
                DataAccess.ExecuteNonQuery(SET_AccessPolicy, ref errorNum, ref errorDesc);
            }
            if (AccessPolicy.RemoteControlPermissionND.ID.Value == 2)
            {
                DataAccess.resetParams();
                DataAccess.addSqlParam("@AccessPolicyID", ParameterDirection.Input, 16, MySqlDbType.Int32, AccessPolicy.AccessPolicyIDND);
                DataAccess.addSqlParam("@CustomerID", ParameterDirection.Input, 16, MySqlDbType.Int32, AccessPolicy.CustomerID);
                DataAccess.addSqlParam("@RemoteControlPermission", ParameterDirection.Input, 16, MySqlDbType.Int32, null);
                DataAccess.addSqlParam("@RemoteAccessStartTime", ParameterDirection.Input, 10, MySqlDbType.VarChar, null);
                DataAccess.addSqlParam("@RemoteAccessEndTime", ParameterDirection.Input, 10, MySqlDbType.VarChar, null);
                DataAccess.addSqlParam("@RemoteAccessTimeZone", ParameterDirection.Input, 16, MySqlDbType.Int32, null);
                DataAccess.addSqlParam("@RebootPermission", ParameterDirection.Input, 16, MySqlDbType.Int32, null);
                DataAccess.addSqlParam("@RebootWindowStartTime", ParameterDirection.Input, 10, MySqlDbType.VarChar, null);
                DataAccess.addSqlParam("@RebootWindowEndTime", ParameterDirection.Input, 10, MySqlDbType.VarChar, null);
                DataAccess.addSqlParam("@AccessPolicyType", ParameterDirection.Input, 16, MySqlDbType.Int32, AccessPolicy.AccessPolicyType.ID = 3);
                DataAccess.addSqlParam("@RebootTimeZone", ParameterDirection.Input, 16, MySqlDbType.Int32, null);
                DataAccess.addSqlParam("@Identity", ParameterDirection.InputOutput, 16, MySqlDbType.Int32);
                DataAccess.ExecuteNonQuery(SET_AccessPolicy, ref errorNum, ref errorDesc);
                return Common.ConvertToInt(DataAccess.getSQLParam("@Identity").ToString());
            }
            else
            {
                DataAccess.resetParams();
                DataAccess.addSqlParam("@AccessPolicyID", ParameterDirection.Input, 16, MySqlDbType.Int32, AccessPolicy.AccessPolicyIDND);
                DataAccess.addSqlParam("@CustomerID", ParameterDirection.Input, 16, MySqlDbType.Int32, AccessPolicy.CustomerID);
                DataAccess.addSqlParam("@RemoteControlPermission", ParameterDirection.Input, 16, MySqlDbType.Int32, AccessPolicy.RemoteControlPermissionND.ID);
                DataAccess.addSqlParam("@RemoteAccessStartTime", ParameterDirection.Input, 10, MySqlDbType.VarChar, AccessPolicy.RemoteAccessStartTimeND);
                DataAccess.addSqlParam("@RemoteAccessEndTime", ParameterDirection.Input, 10, MySqlDbType.VarChar, AccessPolicy.RemoteAccessEndTimeND);
                DataAccess.addSqlParam("@RemoteAccessTimeZone", ParameterDirection.Input, 16, MySqlDbType.Int32, AccessPolicy.RemoteAccessTimeZoneND.ID);
                DataAccess.addSqlParam("@RebootPermission", ParameterDirection.Input, 16, MySqlDbType.Int32, AccessPolicy.RebootPermissionND.ID);
                DataAccess.addSqlParam("@RebootWindowStartTime", ParameterDirection.Input, 10, MySqlDbType.VarChar, AccessPolicy.RebootWindowStartTimeND);
                DataAccess.addSqlParam("@RebootWindowEndTime", ParameterDirection.Input, 10, MySqlDbType.VarChar, AccessPolicy.RebootWindowEndTimeND);
                DataAccess.addSqlParam("@AccessPolicyType", ParameterDirection.Input, 16, MySqlDbType.Int32, AccessPolicy.AccessPolicyType.ID = 3);
                DataAccess.addSqlParam("@RebootTimeZone", ParameterDirection.Input, 16, MySqlDbType.Int32, AccessPolicy.RebootTimeZoneND.ID);
                DataAccess.addSqlParam("@Identity", ParameterDirection.InputOutput, 16, MySqlDbType.Int32);
                DataAccess.ExecuteNonQuery(SET_AccessPolicy, ref errorNum, ref errorDesc);
                return Common.ConvertToInt(DataAccess.getSQLParam("@Identity").ToString());
            }
        }

        public int SaveMaintenancePolicy(tblMaintenancePolicy maintenancePolicy)
        {
            int errorNum = 0;
            string errorDesc = "";
            DataAccess.resetParams();
            DataAccess.addSqlParam("@MaintenancePolicyID", ParameterDirection.Input, 16, MySqlDbType.Int32, maintenancePolicy.MaintenancePolicyID);
            DataAccess.addSqlParam("@CustomerID", ParameterDirection.Input, 16, MySqlDbType.Int32, maintenancePolicy.CustomerID);
            DataAccess.addSqlParam("@ActivityName", ParameterDirection.Input, 100, MySqlDbType.VarChar, maintenancePolicy.ActivityName);
            DataAccess.addSqlParam("@ScheduleType", ParameterDirection.Input, 16, MySqlDbType.Int32, maintenancePolicy.ScheduleType.ID);
            DataAccess.addSqlParam("@WeekOfDay", ParameterDirection.Input, 16, MySqlDbType.Int32, maintenancePolicy.WeekOfDays == null ? 0: maintenancePolicy.WeekOfDays.ID);
          //  DataAccess.addSqlParam("@MonthOfDay", ParameterDirection.Input, 16, MySqlDbType.Int32, maintenancePolicy.MonthOfDays.ID);
            DataAccess.addSqlParam("@ScheduledStartTime", ParameterDirection.Input, 10, MySqlDbType.VarChar, maintenancePolicy.ScheduledStartTime);
            DataAccess.addSqlParam("@ScheduledEndTime", ParameterDirection.Input, 10, MySqlDbType.VarChar, maintenancePolicy.ScheduledEndTime);
            DataAccess.addSqlParam("@ScheduledStartDate", ParameterDirection.Input, 20, MySqlDbType.DateTime, maintenancePolicy.ScheduledStartDate);
            DataAccess.addSqlParam("@WeekOfMonth", ParameterDirection.Input, 16, MySqlDbType.Int32, maintenancePolicy.WeekOfMonth);
            DataAccess.addSqlParam("@TimeZone", ParameterDirection.Input, 16, MySqlDbType.Int32, maintenancePolicy.TimeZone.ID);
            DataAccess.addSqlParam("@Identity", ParameterDirection.InputOutput, 16, MySqlDbType.Int32);
            DataAccess.ExecuteNonQuery(SET_MaintenancePolicy, ref errorNum, ref errorDesc);
            return Common.ConvertToInt(DataAccess.getSQLParam("@Identity").ToString());
        }

        public int SavePatchingPolicy(tblPatchingPolicy patchingPolicy)
        {
            int errorNum = 0;
            string errorDesc = "";

            DataAccess.resetParams();
            DataAccess.addSqlParam("@PatchingPolicyID", ParameterDirection.Input, 16, MySqlDbType.Int32, patchingPolicy.PatchingPolicyIDWR);
            DataAccess.addSqlParam("@CustomerID", ParameterDirection.Input, 16, MySqlDbType.Int32, patchingPolicy.CustomerID);
            DataAccess.addSqlParam("@PatchingPolicyType", ParameterDirection.Input, 16, MySqlDbType.Int32, patchingPolicy.PatchingPolicyTypeIDWR = 1);
            DataAccess.addSqlParam("@PolicyName", ParameterDirection.Input, 50, MySqlDbType.VarChar, patchingPolicy.PolicyNameWR);
            DataAccess.addSqlParam("@WindowTimeZone", ParameterDirection.Input, 16, MySqlDbType.Int32, patchingPolicy.WindowTimeZoneWR.ID);
          //  DataAccess.addSqlParam("@DefineScheduleType", ParameterDirection.Input, 16, MySqlDbType.Int32, patchingPolicy.DefineScheduleTypeWR.ID);
           // DataAccess.addSqlParam("@MonthOfDay", ParameterDirection.Input, 16, MySqlDbType.Int32, patchingPolicy.MonthOfDayWR.ID);
            DataAccess.addSqlParam("@WeekOfDay", ParameterDirection.Input, 16, MySqlDbType.Int32, patchingPolicy.WeekOfDayWR.ID);
            DataAccess.addSqlParam("@RebootPermission", ParameterDirection.Input, 16, MySqlDbType.Int32, patchingPolicy.RebootPermissionWR.ID);
            DataAccess.addSqlParam("@RebootWindowStartTime", ParameterDirection.Input, 10, MySqlDbType.VarChar, patchingPolicy.RebootWindowStartTimeWR);
            DataAccess.addSqlParam("@RebootWindowEndTime", ParameterDirection.Input, 10, MySqlDbType.VarChar, patchingPolicy.RebootWindowEndTimeWR);
            DataAccess.addSqlParam("@WindowStartTime", ParameterDirection.Input, 10, MySqlDbType.VarChar, patchingPolicy.WindowStartTimeWR);
            DataAccess.addSqlParam("@WindowEndTime", ParameterDirection.Input, 10, MySqlDbType.VarChar, patchingPolicy.WindowEndTimeWR);
            DataAccess.addSqlParam("@RebootWindowTimeZone", ParameterDirection.Input, 16, MySqlDbType.Int32, patchingPolicy.WindowTimeZoneWR.ID);
            DataAccess.addSqlParam("@Identity", ParameterDirection.InputOutput, 16, MySqlDbType.Int32);
            DataAccess.ExecuteNonQuery(SET_PatchingPolicy, ref errorNum, ref errorDesc);

            DataAccess.resetParams();
            DataAccess.addSqlParam("@PatchingPolicyID", ParameterDirection.Input, 16, MySqlDbType.Int32, patchingPolicy.PatchingPolicyIDSR);
            DataAccess.addSqlParam("@CustomerID", ParameterDirection.Input, 16, MySqlDbType.Int32, patchingPolicy.CustomerID);
            DataAccess.addSqlParam("@PatchingPolicyType", ParameterDirection.Input, 16, MySqlDbType.Int32, patchingPolicy.PatchingPolicyTypeIDSR = 2);
            DataAccess.addSqlParam("@PolicyName", ParameterDirection.Input, 50, MySqlDbType.VarChar, patchingPolicy.PolicyNameSR);
            DataAccess.addSqlParam("@WindowTimeZone", ParameterDirection.Input, 16, MySqlDbType.Int32, patchingPolicy.WindowTimeZoneSR.ID);
        //    DataAccess.addSqlParam("@DefineScheduleType", ParameterDirection.Input, 16, MySqlDbType.Int32, patchingPolicy.DefineScheduleTypeSR.ID);
         //   DataAccess.addSqlParam("@MonthOfDay", ParameterDirection.Input, 16, MySqlDbType.Int32, patchingPolicy.MonthOfDaySR.ID);
            DataAccess.addSqlParam("@WeekOfDay", ParameterDirection.Input, 16, MySqlDbType.Int32, patchingPolicy.WeekOfDayWR.ID);
            DataAccess.addSqlParam("@RebootPermission", ParameterDirection.Input, 16, MySqlDbType.Int32, patchingPolicy.RebootPermissionSR.ID);
            DataAccess.addSqlParam("@RebootWindowStartTime", ParameterDirection.Input, 10, MySqlDbType.VarChar, patchingPolicy.RebootWindowStartTimeSR);
            DataAccess.addSqlParam("@RebootWindowEndTime", ParameterDirection.Input, 10, MySqlDbType.VarChar, patchingPolicy.RebootWindowEndTimeSR);
            DataAccess.addSqlParam("@WindowStartTime", ParameterDirection.Input, 10, MySqlDbType.VarChar, patchingPolicy.WindowStartTimeSR);
            DataAccess.addSqlParam("@WindowEndTime", ParameterDirection.Input, 10, MySqlDbType.VarChar, patchingPolicy.WindowEndTimeSR);
            DataAccess.addSqlParam("@RebootWindowTimeZone", ParameterDirection.Input, 16, MySqlDbType.Int32, patchingPolicy.RebootWindowTimeZoneSR.ID);
            DataAccess.addSqlParam("@Identity", ParameterDirection.InputOutput, 16, MySqlDbType.Int32);
            DataAccess.ExecuteNonQuery(SET_PatchingPolicy, ref errorNum, ref errorDesc);

            DataAccess.resetParams();
            DataAccess.addSqlParam("@PatchingPolicyID", ParameterDirection.Input, 16, MySqlDbType.Int32, patchingPolicy.PatchingPolicyIDDND);
            DataAccess.addSqlParam("@CustomerID", ParameterDirection.Input, 16, MySqlDbType.Int32, patchingPolicy.CustomerID);
            DataAccess.addSqlParam("@PatchingPolicyType", ParameterDirection.Input, 16, MySqlDbType.Int32, patchingPolicy.PatchingPolicyTypeIDND = 3);
            DataAccess.addSqlParam("@PolicyName", ParameterDirection.Input, 50, MySqlDbType.VarChar, patchingPolicy.PolicyNameND);
            DataAccess.addSqlParam("@WindowTimeZone", ParameterDirection.Input, 16, MySqlDbType.Int32, patchingPolicy.WindowTimeZoneND.ID);
         //   DataAccess.addSqlParam("@DefineScheduleType", ParameterDirection.Input, 16, MySqlDbType.Int32, patchingPolicy.DefineScheduleTypeND.ID);
         //   DataAccess.addSqlParam("@MonthOfDay", ParameterDirection.Input, 16, MySqlDbType.Int32, patchingPolicy.MonthOfDayND.ID);
            DataAccess.addSqlParam("@WeekOfDay", ParameterDirection.Input, 16, MySqlDbType.Int32, patchingPolicy.WeekOfDayND.ID);
            DataAccess.addSqlParam("@RebootPermission", ParameterDirection.Input, 16, MySqlDbType.Int32, patchingPolicy.RebootPermissionND.ID);
            DataAccess.addSqlParam("@RebootWindowStartTime", ParameterDirection.Input, 10, MySqlDbType.VarChar, patchingPolicy.RebootWindowStartTimeND);
            DataAccess.addSqlParam("@RebootWindowEndTime", ParameterDirection.Input, 10, MySqlDbType.VarChar, patchingPolicy.RebootWindowEndTimeND);
            DataAccess.addSqlParam("@WindowStartTime", ParameterDirection.Input, 10, MySqlDbType.VarChar, patchingPolicy.WindowStartTimeND);
            DataAccess.addSqlParam("@WindowEndTime", ParameterDirection.Input, 10, MySqlDbType.VarChar, patchingPolicy.WindowEndTimeND);
            DataAccess.addSqlParam("@RebootWindowTimeZone", ParameterDirection.Input, 16, MySqlDbType.Int32, patchingPolicy.WindowTimeZoneND.ID);
            DataAccess.addSqlParam("@Identity", ParameterDirection.InputOutput, 16, MySqlDbType.Int32);
            DataAccess.ExecuteNonQuery(SET_PatchingPolicy, ref errorNum, ref errorDesc);

            return Common.ConvertToInt(DataAccess.getSQLParam("@Identity").ToString());
        }

        public int SaveBackUpPolicy(tblBackUpPolicy BackUpPolicy)
        {
            int errorNum = 0;
            string errorDesc = "";
            DataAccess.resetParams();
            DataAccess.addSqlParam("@BackUpPolicyID", ParameterDirection.Input, 16, MySqlDbType.Int32, BackUpPolicy.BackUpPolicyID);
            DataAccess.addSqlParam("@CustomerID", ParameterDirection.Input, 16, MySqlDbType.Int32, BackUpPolicy.CustomerID);
            DataAccess.addSqlParam("@PolicyName", ParameterDirection.Input, 100, MySqlDbType.VarChar, BackUpPolicy.PolicyName);
            DataAccess.addSqlParam("@ProductName", ParameterDirection.Input, 100, MySqlDbType.VarChar, BackUpPolicy.ProductName);
            DataAccess.addSqlParam("@VolumeLocation", ParameterDirection.Input, 100, MySqlDbType.VarChar, BackUpPolicy.VolumeLocation);
            DataAccess.addSqlParam("@FolderLocation", ParameterDirection.Input, 100, MySqlDbType.VarChar, BackUpPolicy.FolderLocation);
            DataAccess.addSqlParam("@ScheduleTime", ParameterDirection.Input, 10, MySqlDbType.VarChar, BackUpPolicy.ScheduleTime);
            DataAccess.addSqlParam("@ScheduleTimeZone", ParameterDirection.Input, 16, MySqlDbType.Int32, BackUpPolicy.ScheduleTimeZone.ID);
            DataAccess.addSqlParam("@BackUpSetDetails", ParameterDirection.Input, 100, MySqlDbType.VarChar, BackUpPolicy.BackUpSetDetails);
            DataAccess.addSqlParam("@DifferentialEveryDay", ParameterDirection.Input, 16, MySqlDbType.Int32, BackUpPolicy.DifferentialEveryDay);
            DataAccess.addSqlParam("@PreviousBackupSaved", ParameterDirection.Input, 16, MySqlDbType.Int32, BackUpPolicy.PreviousBackupSaved);
            DataAccess.addSqlParam("@FullBackUpEveryDay", ParameterDirection.Input, 16, MySqlDbType.Int32, BackUpPolicy.FullBackUpEveryDay);
            DataAccess.addSqlParam("@DomainName", ParameterDirection.Input, 100, MySqlDbType.VarChar, BackUpPolicy.DomainName);
            DataAccess.addSqlParam("@UserName", ParameterDirection.Input, 100, MySqlDbType.VarChar, BackUpPolicy.UserName);
            DataAccess.addSqlParam("@Password", ParameterDirection.Input, 100, MySqlDbType.VarChar, BackUpPolicy.Password);
            DataAccess.addSqlParam("@Identity", ParameterDirection.InputOutput, 16, MySqlDbType.Int32);
            DataAccess.ExecuteNonQuery(SET_BackUpPolicy, ref errorNum, ref errorDesc);
            return Common.ConvertToInt(DataAccess.getSQLParam("@Identity").ToString());
        }

        public int SaveAntiVirusPolicy(tblAntiVirusPolicy antiVirus)
        {
            int errorNum = 0;
            string errorDesc = "";

            DataAccess.resetParams();
            DataAccess.addSqlParam("@AntiVirusID", ParameterDirection.Input, 16, MySqlDbType.Int32, antiVirus.AntiVirusID);
            DataAccess.addSqlParam("@CustomerID", ParameterDirection.Input, 16, MySqlDbType.Int32, antiVirus.CustomerID);
            DataAccess.addSqlParam("@PolicyName", ParameterDirection.Input, 100, MySqlDbType.VarChar, antiVirus.PolicyName);
            DataAccess.addSqlParam("@ProductName", ParameterDirection.Input, 100, MySqlDbType.VarChar, antiVirus.ProductName);
            DataAccess.addSqlParam("@SetupStatus", ParameterDirection.Input, 16, MySqlDbType.Int32, antiVirus.SetupStatus);
            DataAccess.addSqlParam("@PatchingTime", ParameterDirection.Input, 10, MySqlDbType.VarChar, antiVirus.PatchingTime);
            DataAccess.addSqlParam("@PatchingTimeZone", ParameterDirection.Input, 16, MySqlDbType.Int32, antiVirus.PatchingTimeZone.ID);
            DataAccess.addSqlParam("@WeekOfDay", ParameterDirection.Input, 16, MySqlDbType.Int32, antiVirus.WeekOfDay.ID);
            DataAccess.addSqlParam("@MonthofDay", ParameterDirection.Input, 16, MySqlDbType.Int32, antiVirus.MonthOfDay.ID);
            DataAccess.addSqlParam("@ExcludedFilesExtension", ParameterDirection.Input, 100, MySqlDbType.VarChar, antiVirus.ExcludedFilesExtension);
            DataAccess.addSqlParam("@ExcludedFileTypes", ParameterDirection.Input, 100, MySqlDbType.VarChar, antiVirus.ExcludedFileTypes);
            DataAccess.addSqlParam("@ExcludedFilePaths", ParameterDirection.Input, 100, MySqlDbType.VarChar, antiVirus.ExcludedFilePaths);

            DataAccess.addSqlParam("@Identity", ParameterDirection.InputOutput, 16, MySqlDbType.Int32);
            DataAccess.ExecuteNonQuery(SET_AntiVirusPolicy, ref errorNum, ref errorDesc);
            return Common.ConvertToInt(DataAccess.getSQLParam("@Identity").ToString());
        }

        public tblCustomerDetails GetCustomer(int CustomerID)
        {
            int errorNum = 0;
            string errorDesc = "";
            DataAccess.resetParams();
            DataAccess.addSqlParam("@CustomerID", ParameterDirection.Input, 16, MySqlDbType.Int32, CustomerID);
            DataSet ds = DataAccess.ExecuteDataSet(GET_CUSTOMER, ref errorNum, ref errorDesc);

            tblCustomerDetails customerDetails = new tblCustomerDetails();
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dtCutomer = ds.Tables[0];
                if (dtCutomer != null && dtCutomer.Rows.Count > 0)
                {
                    DataRow dr = dtCutomer.Rows[0];
                    customerDetails.CustomerID = Common.ConvertToInt(dr, "CustomerID");
                    customerDetails.RoleID = Common.ConvertToInt(dr, "RoleID");
                    customerDetails.IsActive = Common.ConvertToInt(dr, "Isactive");
                    customerDetails.CustomerName = Common.ConvertToString(dr, "CustomerName");
                    customerDetails.EmailAddress = Common.ConvertToString(dr, "EmailAddress");
                    customerDetails.CustomerContactName = Common.ConvertToString(dr, "CustomerContactName");
                    customerDetails.Password = Common.ConvertToString(dr, "Password");
                    customerDetails.TimeZone = new TimeZoneFX();
                    customerDetails.TimeZone.ID = Common.ConvertToInt(dr, "TimeZone");
                    customerDetails.TimeZone.Name = LookUpValue.GetTimeZone(customerDetails.TimeZone.ID.Value);
                    customerDetails.NOCCommunication = new NOCCommunicationBy();
                    customerDetails.NOCCommunication.ID = Common.ConvertToInt(dr, "NOCCommunicationBy");
                }
            }
            return customerDetails;
        }

      

        public StringBuilder GetDeleteCustomer(int CustomerID)
        {
            int errorNum = 0;
            bool flag = false;
            string errorDesc = "";
            StringBuilder str = new StringBuilder();
            DataAccess.resetParams();
            DataAccess.addSqlParam("@CustomerID", ParameterDirection.Input, 16, MySqlDbType.Int32, CustomerID);
            DataSet ds = DataAccess.ExecuteDataSet(DELETE_CUSTOMER, ref errorNum, ref errorDesc);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dtCutomer = ds.Tables[0];

                if (dtCutomer != null && dtCutomer.Rows.Count > 0)
                {
                    DataRow[] dr = dtCutomer.Select("CKey = 'UserName'");

                    string UserName = Common.ConvertToString(dr[0], "Value");
                    //Step start to build string
                    //Step 1: Create string for CustomerContact
                    dr = dtCutomer.Select("CKey = 'tblCustomerContact'");
                    if (dr != null && dr.Length > 1)
                    {
                        //Multi Record message
                        str.Append("<li>");
                        str.Append("User <b>" + UserName + "</b> belongs to <b>" + dr.Length.ToString() + "</b> contacts please delete them and try again to delete the user.");
                        str.Append("</li>");
                        str.Append("</br>");
                        flag = true;
                    }
                    else if (dr.Length == 1)
                    {
                        //Single record message
                        str.Append("<li>");
                        str.Append("Customer <b>" + UserName + "</b> belongs to user <b>" + Common.ConvertToString(dr[0], "Value") + "</b>. Please delete that record and then try again to delete the user.");
                        str.Append("</li>");
                        str.Append("</br>");
                        flag = true;
                    }


                    //Step 2: Create string for ClientSite
                    dr = dtCutomer.Select("CKey = 'tblclientsite'");
                    if (dr != null && dr.Length > 1)
                    {
                        //Multi Record message
                        str.Append("<li>");
                        str.Append("User <b>" + UserName + "</b> belongs to <b>" + dr.Length.ToString() + "</b> client please delete them and try again to delete the user.");
                        str.Append("</li>");
                        str.Append("</br>");
                        flag = true;
                    }
                    else if (dr.Length == 1)
                    {
                        //Single record message
                        str.Append("<li>");
                        str.Append("Customer <b>" + UserName + "</b> belongs to user <b>" + Common.ConvertToString(dr[0], "Value") + "</b>. Please delete that record and then try again to delete the user.");
                        str.Append("</li>");
                        str.Append("</br>");
                        flag = true;
                    }

                    //Step 3: Create string for Anti virus policy
                    dr = dtCutomer.Select("CKey = 'tblantiviruspolicy'");
                    if (dr != null && dr.Length > 1)
                    {
                        //Multi Record message
                        str.Append("<li>");
                        str.Append("User <b>" + UserName + "</b> belongs to <b>" + dr.Length.ToString() + "</b> anti virus policy please delete them and try again to delete the user.");
                        str.Append("</li>");
                        str.Append("</br>");
                        flag = true;
                    }
                    else if (dr.Length == 1)
                    {
                        //Single record message
                        str.Append("<li>");
                        str.Append("Customer <b>" + UserName + "</b> belongs to user <b>" + Common.ConvertToString(dr[0], "Value") + "</b>. Please delete that record and then try again to delete the user.");
                        str.Append("</li>");
                        str.Append("</br>");
                        flag = true;
                    }

                    //Step 4: Create string for back up policy
                    dr = dtCutomer.Select("CKey = 'tblbackuppolicy'");
                    if (dr != null && dr.Length > 1)
                    {
                        //Multi Record message
                        str.Append("<li>");
                        str.Append("User <b>" + UserName + "</b> belongs to <b>" + dr.Length.ToString() + "</b> back up policy please delete them and try again to delete the user.");
                        str.Append("</li>");
                        str.Append("</br>");
                        flag = true;
                    }
                    else if (dr.Length == 1)
                    {
                        //Single record message
                        str.Append("<li>");
                        str.Append("Customer <b>" + UserName + "</b> belongs to user <b>" + Common.ConvertToString(dr[0], "Value") + "</b>. Please delete that record and then try again to delete the user.");
                        str.Append("</li>");
                        str.Append("</br>");
                        flag = true;
                    }

                    //Step 5: Create string for back up policy
                    dr = dtCutomer.Select("CKey = 'tblmaintenancepolicy'");
                    if (dr != null && dr.Length > 1)
                    {
                        //Multi Record message
                        str.Append("<li>");
                        str.Append("User <b>" + UserName + "</b> belongs to <b>" + dr.Length.ToString() + "</b> maintenance policy please delete them and try again to delete the user.");
                        str.Append("</li>");
                        str.Append("</br>");
                        flag = true;
                    }
                    else if (dr.Length == 1)
                    {
                        //Single record message
                        str.Append("<li>");
                        str.Append("Customer <b>" + UserName + "</b> belongs to user <b>" + Common.ConvertToString(dr[0], "Value") + "</b>. Please delete that record and then try again to delete the user.");
                        str.Append("</li>");
                        str.Append("</br>");
                        flag = true;
                    }

                    //Step 6: Create string for patching policy
                    dr = dtCutomer.Select("CKey = 'tblpatchingpolicy'");
                    if (dr != null && dr.Length > 1)
                    {
                        //Multi Record message
                        str.Append("<li>");
                        str.Append("User <b>" + UserName + "</b> belongs to <b>" + dr.Length.ToString() + "</b> patchingpolicy policy please delete them and try again to delete the user.");
                        str.Append("</li>");
                        str.Append("</br>");
                        flag = true;
                    }
                    else if (dr.Length == 1)
                    {
                        //Single record message
                        str.Append("<li>");
                        str.Append("Customer <b>" + UserName + "</b> belongs to user <b>" + Common.ConvertToString(dr[0], "Value") + "</b>. Please delete that record and then try again to delete the user.");
                        str.Append("</li>");
                        flag = true;
                    }

                    //Step 7: Create string for tool information
                    dr = dtCutomer.Select("CKey = 'tblaccesspolicy'");
                    if (dr != null && dr.Length > 1)
                    {
                        //Multi Record message
                        str.Append("<li>");
                        str.Append("User <b>" + UserName + "</b> belongs to <b>" + dr.Length.ToString() + "</b> accesspolicy policy please delete them and try again to delete the user.");
                        str.Append("</li>");
                        str.Append("</br>");
                        flag = true;
                    }
                    else if (dr.Length == 1)
                    {
                        //Single record message
                        str.Append("<li>");
                        str.Append("Customer <b>" + UserName + "</b> belongs to user <b>" + Common.ConvertToString(dr[0], "Value") + "</b>. Please delete that record and then try again to delete the user.");
                        str.Append("</li>");
                        str.Append("</br>");
                        flag = true;
                    }

                    //Step 8: Create string for tool informations
                    dr = dtCutomer.Select("CKey = 'tbltoolinformations'");
                    if (dr != null && dr.Length > 1)
                    {
                        //Multi Record message
                        str.Append("<li>");
                        str.Append("User <b>" + UserName + "</b> belongs to <b>" + dr.Length.ToString() + "</b> maintenance policy please delete them and try again to delete the user.");
                        str.Append("</li>");
                        flag = true;
                    }
                    else if (dr.Length == 1)
                    {
                        //Single record message
                        str.Append("<li>");
                        str.Append("Customer <b>" + UserName + "</b> belongs to user <b>" + Common.ConvertToString(dr[0], "Value") + "</b>. Please delete that record and then try again to delete the user.");
                        str.Append("</li>");
                        flag = true;
                    }
                    if (flag)
                    {
                        str.Append("<ul>");
                        str.Append("</ul>");
                    }
                }
            }
            return str;
        }

        public List<tblAntiVirusPolicy> GetAVPIndex(int CustomerID, int pageNumber, int pageSize, int sortColumnIndex, string sortOrder, string searchText, ref int totalRecords)
        {
            int errorNum = 0;
            string errorDesc = "";
            DataAccess.resetParams();
            DataAccess.addSqlParam("@CustomerID", ParameterDirection.Input, 16, MySqlDbType.Int32, CustomerID);
            DataAccess.addSqlParam("@pageNumber", ParameterDirection.Input, 16, MySqlDbType.Int32, pageNumber);
            DataAccess.addSqlParam("@pageRecord", ParameterDirection.Input, 16, MySqlDbType.Int32, pageSize);
            DataAccess.addSqlParam("@sortColumnIndex", ParameterDirection.Input, 16, MySqlDbType.Int32, sortColumnIndex);
            DataAccess.addSqlParam("@searchOrder", ParameterDirection.Input, 4, MySqlDbType.VarChar, sortOrder);
            DataAccess.addSqlParam("@searchText", ParameterDirection.Input, 100, MySqlDbType.VarChar, searchText);

            DataSet ds = DataAccess.ExecuteDataSet(GET_AVPINDX, ref errorNum, ref errorDesc);
            tblAntiVirusPolicy antiVirus = new tblAntiVirusPolicy();

            List<tblAntiVirusPolicy> antiViruss = new List<tblAntiVirusPolicy>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                DataTable dtAntiVirus = ds.Tables[0];
                totalRecords = Common.ConvertToInt(dtAntiVirus.Rows[0], "TotalRecords");
                if (dtAntiVirus != null && dtAntiVirus.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtAntiVirus.Rows)
                    {
                        antiVirus = new tblAntiVirusPolicy();
                        antiVirus.WeekOfDay = new WeekOfDay();
                        antiVirus.MonthOfDay = new MonthOfDay();
                        antiVirus.PatchingTimeZone = new TimeZoneFX();

                        antiVirus.ScheduleType = new ScheduleType();
                        antiVirus.CustomerID = Common.ConvertToInt(dr, "CustomerID");
                        antiVirus.AntiVirusID = Common.ConvertToInt(dr, "AntiVirusID");
                        antiVirus.PatchingTime = Common.ConvertToString(dr, "PatchingTime");

                        antiVirus.PatchingTimeZone.ID = Common.ConvertToInt(dr, "PatchingTimeZone");
                        antiVirus.PatchingTimeZone.Name = LookUpValue.GetTimeZone(antiVirus.PatchingTimeZone.ID.Value);
                        antiVirus.WeekOfDay.ID = Common.ConvertToInt(dr, "WeekOfDay");
                        antiVirus.WeekOfDay.Name = LookUpValue.GetWeekOfDay(antiVirus.WeekOfDay.ID.Value);
                        antiVirus.MonthOfDay.ID = Common.ConvertToInt(dr, "MonthOfDay");
                        antiVirus.MonthOfDay.Name = LookUpValue.GetMonthOfDays(antiVirus.MonthOfDay.ID.Value);
                        antiVirus.PolicyName = Common.ConvertToString(dr, "PolicyName");
                        antiVirus.ProductName = Common.ConvertToString(dr, "ProductName");
                        antiVirus.ExcludedFilesExtension = Common.ConvertToString(dr, "ExcludedFilesExtension");
                        antiVirus.ExcludedFileTypes = Common.ConvertToString(dr, "ExcludedFileTypes");
                        antiVirus.ExcludedFilePaths = Common.ConvertToString(dr, "ExcludedFilePaths");
                        antiVirus.PatchingTimeZone.ID = Common.ConvertToInt(dr, "PatchingTimeZone");
                        antiVirus.PatchingTimeZone.Name = LookUpValue.GetTimeZone(antiVirus.PatchingTimeZone.ID.Value);
                        antiViruss.Add(antiVirus);
                    }
                }
            }
            return antiViruss;
        }

        public List<tblBackUpPolicy> GetBPIndex(int CustomerID, int pageNumber, int pageSize, int sortColumnIndex, string sortOrder, string searchText, ref int totalRecords)
        {
            int errorNum = 0;
            string errorDesc = "";
            DataAccess.resetParams();
            DataAccess.addSqlParam("@CustomerID", ParameterDirection.Input, 16, MySqlDbType.Int32, CustomerID);
            DataAccess.addSqlParam("@pageNumber", ParameterDirection.Input, 16, MySqlDbType.Int32, pageNumber);
            DataAccess.addSqlParam("@pageRecord", ParameterDirection.Input, 16, MySqlDbType.Int32, pageSize);
            DataAccess.addSqlParam("@sortColumnIndex", ParameterDirection.Input, 16, MySqlDbType.Int32, sortColumnIndex);
            DataAccess.addSqlParam("@searchOrder", ParameterDirection.Input, 4, MySqlDbType.VarChar, sortOrder);
            DataAccess.addSqlParam("@searchText", ParameterDirection.Input, 100, MySqlDbType.VarChar, searchText);

            DataSet ds = DataAccess.ExecuteDataSet(GET_BACKUPPOLICIES, ref errorNum, ref errorDesc);
            tblBackUpPolicy backup = new tblBackUpPolicy();
            List<tblBackUpPolicy> backups = new List<tblBackUpPolicy>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dtAntiVirus = ds.Tables[0];
                totalRecords = Common.ConvertToInt(dtAntiVirus.Rows[0], "TotalRecords");
                if (dtAntiVirus != null && dtAntiVirus.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtAntiVirus.Rows)
                    {
                        backup = new tblBackUpPolicy();
                        backup.ScheduleTimeZone = new TimeZoneFX();
                        backup.CustomerID = Common.ConvertToInt(dr, "CustomerID");
                        backup.BackUpPolicyID = Common.ConvertToInt(dr, "BackUpPolicyID");
                        backup.ProductName = Common.ConvertToString(dr, "ProductName");
                        backup.VolumeLocation = Common.ConvertToString(dr, "VolumeLocation");
                        backup.FolderLocation = Common.ConvertToString(dr, "FolderLocation");
                        backup.ScheduleTime = Common.ConvertToString(dr, "ScheduleTime");
                        backup.PolicyName = Common.ConvertToString(dr, "PolicyName");
                        backup.ProductName = Common.ConvertToString(dr, "ProductName");
                        backup.ScheduleTimeZone.ID = Common.ConvertToInt(dr, "ScheduleTimeZone");
                        backup.ScheduleTimeZone.Name = LookUpValue.GetTimeZone(backup.ScheduleTimeZone.ID.Value);
                        backup.BackUpSetDetails = Common.ConvertToString(dr, "BackUpSetDetails");
                        backup.PreviousBackupSaved = Common.ConvertToInt(dr, "PreviousBackupSaved");
                        backup.FullBackUpEveryDay = Common.ConvertToInt(dr, "FullBackUpEveryDay");
                        backup.DomainName = Common.ConvertToString(dr, "DomainName");
                        backup.DifferentialEveryDay = Common.ConvertToInt(dr, "DifferentialEveryDay");
                        backup.UserName = Common.ConvertToString(dr, "UserName");
                        backup.Password = Common.ConvertToString(dr, "Password");
                        backups.Add(backup);
                    }
                }
            }
            return backups;
        }

        public StringBuilder DeletePolicy(int BackUpPolicyID, string PolicyType)
        {
            int errorNum = 0;
            bool flagPol = false;
            string errorDesc = "";
            StringBuilder str = new StringBuilder();
            DataAccess.resetParams();
            DataAccess.addSqlParam("@PolicyID", ParameterDirection.Input, 16, MySqlDbType.Int32, BackUpPolicyID);
            DataAccess.addSqlParam("@PolicyType", ParameterDirection.Input, 50, MySqlDbType.VarChar, PolicyType);
            DataSet ds = DataAccess.ExecuteDataSet(DELETE_BACKUPPOLICY, ref errorNum, ref errorDesc);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dtbackupPolicy = ds.Tables[0];

                if (dtbackupPolicy != null && dtbackupPolicy.Rows.Count > 0)
                {
                    DataRow[] dr = dtbackupPolicy.Select("CKey = 'PolicyName'");

                    string PolicyName = Common.ConvertToString(dr[0], "Value");
                    //Step start to build string
                    //Step 1: Create string for back up policy
                    dr = dtbackupPolicy.Select("CKey = 'tblbackuppolicy'");
                    if (dr != null && dr.Length > 1)
                    {
                        //Multi Record message
                        str.Append("<li>");
                        str.Append("Policy <b>" + PolicyName + "</b> belongs to <b>" + dr.Length.ToString() + "</b> devies. Please delete them and try again to delete the policy.");
                        str.Append("</li>");
                        str.Append("</br>");
                        flagPol = true;
                    }
                    else if (dr.Length == 1)
                    {
                        //Single record message
                        str.Append("<li>");
                        str.Append("Policy <b>" + PolicyName + "</b> belongs to device <b>" + Common.ConvertToString(dr[0], "Value") + "</b>. Please delete that device and then try again to delete the policy.");
                        str.Append("</li>");
                        str.Append("</br>");
                        flagPol = true;
                    }


                    //Step 2: Create string for ClientSite
                    dr = dtbackupPolicy.Select("CKey = 'tblantiviruspolicy'");
                    if (dr != null && dr.Length > 1)
                    {
                        //Multi Record message
                        str.Append("<li>");
                        str.Append("Policy <b>" + PolicyName + "</b> belongs to <b>" + dr.Length.ToString() + "</b> devies. Please delete them and try again to delete the policy.");
                        str.Append("</li>");
                        str.Append("</br>");
                        flagPol = true;
                    }
                    else if (dr.Length == 1)
                    {
                        //Single record message
                        str.Append("<li>");
                        str.Append("Policy <b>" + PolicyName + "</b> belongs to Policy <b>" + Common.ConvertToString(dr[0], "Value") + "</b>. Please delete that device and then try again to delete the policy.");
                        str.Append("</li>");
                        str.Append("</br>");
                        flagPol = true;
                    }

                    //Step 3: Create string for Anti virus policy
                    dr = dtbackupPolicy.Select("CKey = 'tblmaintenancepolicy'");
                    if (dr != null && dr.Length > 1)
                    {
                        //Multi Record message
                        str.Append("<li>");
                        str.Append("Policy <b>" + PolicyName + "</b> belongs to <b>" + dr.Length.ToString() + "</b> devies. Please delete them and try again to delete the policy.");
                        str.Append("</li>");
                        str.Append("</br>");
                        flagPol = true;
                    }
                    else if (dr.Length == 1)
                    {
                        //Single record message
                        str.Append("<li>");
                        str.Append("Policy <b>" + PolicyName + "</b> belongs to Policy <b>" + Common.ConvertToString(dr[0], "Value") + "</b>. Please delete that device and then try again to delete the policy.");
                        str.Append("</li>");
                        flagPol = true;
                    }
                    if (flagPol)
                    {
                        str.Append("<ul>");
                        str.Append("</ul>");
                    }
                }
            }
            return str;
        }

        public tblBackUpPolicy GetBackUp(int BackUpPolicyID)
        {
            int errorNum = 0;
            string errorDesc = "";
            DataAccess.resetParams();
            DataAccess.addSqlParam("@BackUpPolicyID", ParameterDirection.Input, 16, MySqlDbType.Int32, BackUpPolicyID);
            DataSet ds = DataAccess.ExecuteDataSet(GET_BACKUPPOLICY, ref errorNum, ref errorDesc);

            tblBackUpPolicy backup = new tblBackUpPolicy();
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dtAntiVirus = ds.Tables[0];
                if (dtAntiVirus != null && dtAntiVirus.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtAntiVirus.Rows)
                    {
                        if (dtAntiVirus != null && dtAntiVirus.Rows.Count > 0)
                        {
                            backup.ScheduleTimeZone = new TimeZoneFX();
                            backup.CustomerID = Common.ConvertToInt(dr, "CustomerID");
                            backup.BackUpPolicyID = Common.ConvertToInt(dr, "BackUpPolicyID");
                            backup.ProductName = Common.ConvertToString(dr, "ProductName");
                            backup.VolumeLocation = Common.ConvertToString(dr, "VolumeLocation");
                            backup.FolderLocation = Common.ConvertToString(dr, "FolderLocation");
                            backup.ScheduleTime = Common.ConvertToString(dr, "ScheduleTime");

                            backup.PolicyName = Common.ConvertToString(dr, "PolicyName");
                            backup.ProductName = Common.ConvertToString(dr, "ProductName");
                            backup.ScheduleTimeZone.ID = Common.ConvertToInt(dr, "ScheduleTimeZone");
                            backup.ScheduleTimeZone.Name = LookUpValue.GetTimeZone(backup.ScheduleTimeZone.ID.Value);
                            backup.BackUpSetDetails = Common.ConvertToString(dr, "BackUpSetDetails");
                            backup.PreviousBackupSaved = Common.ConvertToInt(dr, "PreviousBackupSaved");
                            backup.FullBackUpEveryDay = Common.ConvertToInt(dr, "FullBackUpEveryDay");
                            backup.DomainName = Common.ConvertToString(dr, "DomainName");
                            backup.DifferentialEveryDay = Common.ConvertToInt(dr, "DifferentialEveryDay");
                            backup.UserName = Common.ConvertToString(dr, "UserName");
                            backup.Password = Common.ConvertToString(dr, "Password");
                        }
                    }
                }
            }
            return backup;
        }

        public tblAntiVirusPolicy GetAntiVirus(int AntiVirusID)
        {
            int errorNum = 0;
            string errorDesc = "";
            DataAccess.resetParams();
            DataAccess.addSqlParam("@oAntiVirusID", ParameterDirection.Input, 16, MySqlDbType.Int32, AntiVirusID);
            DataSet ds = DataAccess.ExecuteDataSet(GET_ANTIVIRUS, ref errorNum, ref errorDesc);
            tblAntiVirusPolicy antiVirus = new tblAntiVirusPolicy();

            if (ds != null && ds.Tables.Count > 0)
            {

                DataTable dtAntiVirus = ds.Tables[0];
                if (dtAntiVirus != null && dtAntiVirus.Rows.Count > 0)
                {

                    DataRow dr = dtAntiVirus.Rows[0];
                    antiVirus = new tblAntiVirusPolicy();
                    antiVirus.WeekOfDay = new WeekOfDay();
                    antiVirus.MonthOfDay = new MonthOfDay();
                    antiVirus.PatchingTimeZone = new TimeZoneFX();
                    //  antiVirus.PatchingTime = new FXTime();



                    antiVirus.CustomerID = Common.ConvertToInt(dr, "CustomerID");
                    antiVirus.AntiVirusID = Common.ConvertToInt(dr, "AntiVirusID");
                    antiVirus.PatchingTime = Common.ConvertToString(dr, "PatchingTime");
                    antiVirus.PatchingTimeZone.ID = Common.ConvertToInt(dr, "PatchingTimeZone");
                    antiVirus.WeekOfDay.ID = Common.ConvertToInt(dr, "WeekOfDay");
                    antiVirus.MonthOfDay.ID = Common.ConvertToInt(dr, "MonthOfDay");
                    antiVirus.PolicyName = Common.ConvertToString(dr, "PolicyName");
                    antiVirus.ProductName = Common.ConvertToString(dr, "ProductName");
                    antiVirus.ExcludedFilesExtension = Common.ConvertToString(dr, "ExcludedFilesExtension");
                    antiVirus.ExcludedFileTypes = Common.ConvertToString(dr, "ExcludedFileTypes");
                    antiVirus.ExcludedFilePaths = Common.ConvertToString(dr, "ExcludedFilePaths");

                    antiVirus.PatchingTimeZone.Name = LookUpValue.GetTimeZone(antiVirus.PatchingTimeZone.ID.Value);
                    // antiVirus.PatchingTime.Name = LookUpValue.GetFXTime(antiVirus.PatchingTime.ID.Value);
                    antiVirus.WeekOfDay.Name = LookUpValue.GetWeekOfDay(antiVirus.WeekOfDay.ID.Value);
                    antiVirus.MonthOfDay.Name = LookUpValue.GetMonthOfDays(antiVirus.MonthOfDay.ID.Value);


                }
            }
            return antiVirus;
        }

        public tblMaintenancePolicy GetMaintenance(int MaintenancePolicyID)
        {
            int errorNum = 0;
            string errorDesc = "";
            DataAccess.resetParams();
            DataAccess.addSqlParam("@oMaintenancePolicyID", ParameterDirection.Input, 16, MySqlDbType.Int32, MaintenancePolicyID);
            DataSet ds = DataAccess.ExecuteDataSet(GET_MAINTENANCEEDIT, ref errorNum, ref errorDesc);
            tblMaintenancePolicy maintenance = new tblMaintenancePolicy();

            if (ds != null && ds.Tables.Count > 0)
            {

                DataTable dtmaintenance = ds.Tables[0];
                if (dtmaintenance != null && dtmaintenance.Rows.Count > 0)
                {
                    DataRow dr = dtmaintenance.Rows[0];
                    maintenance.ScheduleType = new ScheduleTypeMaintenancePolicy();
                    maintenance.WeekOfDays = new WeekOfDay();
                    maintenance.MonthOfDays = new MonthOfDay();
                    maintenance.TimeZone = new TimeZoneFX();

                    maintenance.CustomerID = Common.ConvertToInt(dr, "CustomerID");
                    maintenance.MaintenancePolicyID = Common.ConvertToInt(dr, "MaintenancePolicyID");
                    maintenance.ActivityName = Common.ConvertToString(dr, "ActivityName");
                    maintenance.ScheduleType.ID = Common.ConvertToInt(dr, "ScheduleType");
                    maintenance.ScheduleType.Name = LookUpValue.GetScheduleType(maintenance.ScheduleType.ID.Value);
                    maintenance.WeekOfDays.ID = Common.ConvertToInt(dr, "WeekOfDay");
                    maintenance.WeekOfDays.Name = LookUpValue.GetWeekOfDay(maintenance.WeekOfDays.ID.Value);
                 //   maintenance.MonthOfDays.ID = Common.ConvertToInt(dr, "MonthOfDay");
                    maintenance.ScheduledStartTime = Common.ConvertToString(dr, "ScheduledStartTime");
                    maintenance.ScheduledEndTime = Common.ConvertToString(dr, "ScheduledEndTime");
                    maintenance.ScheduledStartDate = Common.ConvertToDateTime(dr, "ScheduledStartDate");
                    maintenance.TimeZone.ID = Common.ConvertToInt(dr, "TimeZone");
                    maintenance.WeekOfMonth = Common.ConvertToInt(dr, "WeekOfMonth");
                    maintenance.TimeZone.Name = LookUpValue.GetTimeZone(maintenance.TimeZone.ID.Value);
                 //   antiVirus.PatchingTimeZone.Name = LookUpValue.GetTimeZone(antiVirus.PatchingTimeZone.ID.Value);
                }
            }
            return maintenance;
        }

        public List<tblMaintenancePolicy> GetMPIndex(int CustomerID, int pageNumber, int pageSize, int sortColumnIndex, string sortOrder, string searchText, ref int totalRecords)
        {
            int errorNum = 0;
            string errorDesc = "";
            DataAccess.resetParams();
            DataAccess.addSqlParam("@oCustomerID", ParameterDirection.Input, 16, MySqlDbType.Int32, CustomerID);
            DataAccess.addSqlParam("@pageNumber", ParameterDirection.Input, 16, MySqlDbType.Int32, pageNumber);
            DataAccess.addSqlParam("@pageRecord", ParameterDirection.Input, 16, MySqlDbType.Int32, pageSize);
            DataAccess.addSqlParam("@sortColumnIndex", ParameterDirection.Input, 16, MySqlDbType.Int32, sortColumnIndex);
            DataAccess.addSqlParam("@searchOrder", ParameterDirection.Input, 4, MySqlDbType.VarChar, sortOrder);
            DataAccess.addSqlParam("@searchText", ParameterDirection.Input, 100, MySqlDbType.VarChar, searchText);

            DataSet ds = DataAccess.ExecuteDataSet(GET_MAINTENANCE, ref errorNum, ref errorDesc);
            tblMaintenancePolicy maintenance = new tblMaintenancePolicy();
            List<tblMaintenancePolicy> maintenances = new List<tblMaintenancePolicy>();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dtMaintenance = ds.Tables[0];
                DataTable dtTotalRecords = ds.Tables[1];
                totalRecords = Common.ConvertToInt(dtTotalRecords.Rows[0], "TotalRecords");
                if (dtMaintenance != null && dtMaintenance.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtMaintenance.Rows)
                    {
                        maintenance = new tblMaintenancePolicy();
                        maintenance.WeekOfDays = new WeekOfDay();
                        maintenance.MonthOfDays = new MonthOfDay();
                        maintenance.TimeZone = new TimeZoneFX();
                        //  maintenance.ScheduledEndTime = new FXTime();
                        // maintenance.ScheduledStartTime = new FXTime();
                        maintenance.ScheduleType = new ScheduleTypeMaintenancePolicy();
                        maintenance.CustomerID = Common.ConvertToInt(dr, "CustomerID");
                        maintenance.MaintenancePolicyID = Common.ConvertToInt(dr, "MaintenancePolicyID");
                        maintenance.ActivityName = Common.ConvertToString(dr, "ActivityName");
                        maintenance.ScheduleType.ID = Common.ConvertToInt(dr, "ScheduleType");
                        maintenance.ScheduleType.Name = LookUpValue.GetScheduleType(maintenance.ScheduleType.ID.Value);
                        maintenance.WeekOfDays.ID = Common.ConvertToInt(dr, "WeekOfDay");
                        maintenance.WeekOfDays.Name = LookUpValue.GetWeekOfDay(maintenance.WeekOfDays.ID.Value);
                        maintenance.MonthOfDays.ID = Common.ConvertToInt(dr, "MonthOfDay");
                        maintenance.MonthOfDays.Name = LookUpValue.GetMonthOfDays(maintenance.MonthOfDays.ID.Value);
                        maintenance.ScheduledStartTime = Common.ConvertToString(dr, "ScheduledStartTime");
                        //  maintenance.ScheduledStartTime.Name = LookUpValue.GetFXTime(maintenance.ScheduledStartTime.ID.Value);
                        maintenance.ScheduledEndTime = Common.ConvertToString(dr, "ScheduledEndTime");
                        // maintenance.ScheduledEndTime.Name = LookUpValue.GetFXTime(maintenance.ScheduledEndTime.ID.Value);
                        maintenance.ScheduledStartDate = Common.ConvertToDateTime(dr, "ScheduledStartDate");
                        maintenance.TimeZone.ID = Common.ConvertToInt(dr, "TimeZone");
                        maintenance.WeekOfMonth = Common.ConvertToInt(dr, "WeekOfMonth");
                        maintenance.TimeZone.Name = LookUpValue.GetTimeZone(maintenance.TimeZone.ID.Value);
                        maintenances.Add(maintenance);
                    }
                }
            }
            return maintenances;
        }

        public tblAccessPolicy GetAccessPolicy(int CustomerID)
        {
            int errorNum = 0;
            string errorDesc = "";
            DataAccess.resetParams();
            DataAccess.addSqlParam("@CustomerID", ParameterDirection.Input, 16, MySqlDbType.Int32, CustomerID);
            DataAccess.addSqlParam("@AccessPolicyID", ParameterDirection.Input, 16, MySqlDbType.Int32, 0);
            DataSet ds = DataAccess.ExecuteDataSet(GET_ACCESSPLOY, ref errorNum, ref errorDesc);

            tblAccessPolicy rowDetails = new tblAccessPolicy();
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dtCutomer = ds.Tables[0];
                if (dtCutomer != null && dtCutomer.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtCutomer.Rows)
                    {
                        if (Common.ConvertToInt(dr, "AccessPolicyType") == 1)
                        {
                            rowDetails.CustomerID = Common.ConvertToInt(dr, "CustomerID");
                            rowDetails.AccessPolicyIDWR = Common.ConvertToInt(dr, "AccessPolicyID");
                            rowDetails.RemoteControlPermissionWR = new OptionType();
                            rowDetails.RemoteControlPermissionWR.ID = Common.ConvertToInt(dr, "RemoteControlPermission");
                            rowDetails.RemoteControlPermissionWR.Name = LookUpValue.GetOptionType(rowDetails.RemoteControlPermissionWR.ID.Value);
                            rowDetails.RebootWindowEndTimeWR = Common.ConvertToString(dr, "RebootWindowEndTime");
                            rowDetails.RebootPermissionWR = new OptionType();
                            rowDetails.RebootPermissionWR.ID = Common.ConvertToInt(dr, "RebootPermission");
                            rowDetails.RebootPermissionWR.Name = LookUpValue.GetOptionType(rowDetails.RebootPermissionWR.ID.Value);
                            rowDetails.RebootWindowStartTimeWR = Common.ConvertToString(dr, "RebootWindowStartTime");
                            rowDetails.RemoteAccessStartTimeWR = Common.ConvertToString(dr, "RemoteAccessStartTime");
                            rowDetails.RemoteAccessEndTimeWR = Common.ConvertToString(dr, "RemoteAccessEndTime");
                            rowDetails.RemoteAccessTimeZoneWR = new TimeZoneFX();
                            rowDetails.RemoteAccessTimeZoneWR.ID = Common.ConvertToInt(dr, "RemoteAccessTimeZone");
                            rowDetails.RemoteAccessTimeZoneWR.Name = LookUpValue.GetTimeZone(rowDetails.RemoteAccessTimeZoneWR.ID.Value);
                            rowDetails.RebootTimeZoneWR = new TimeZoneFX();
                            rowDetails.RebootTimeZoneWR.ID = Common.ConvertToInt(dr, "RebootTimeZone");
                            rowDetails.RebootTimeZoneWR.Name = LookUpValue.GetTimeZone(rowDetails.RebootTimeZoneWR.ID.Value);

                        }
                        else if (Common.ConvertToInt(dr, "AccessPolicyType") == 2)
                        {
                            rowDetails.RemoteAccessStartTimeSR = Common.ConvertToString(dr, "RemoteAccessStartTime");
                            rowDetails.AccessPolicyIDSR = Common.ConvertToInt(dr, "AccessPolicyID");
                            rowDetails.RemoteControlPermissionSR = new OptionType();
                            rowDetails.RemoteControlPermissionSR.ID = Common.ConvertToInt(dr, "RemoteControlPermission");
                            rowDetails.RemoteControlPermissionSR.Name = LookUpValue.GetOptionType(rowDetails.RemoteControlPermissionSR.ID.Value);
                            rowDetails.RebootWindowEndTimeSR = Common.ConvertToString(dr, "RebootWindowEndTime");
                            rowDetails.RebootWindowStartTimeSR = Common.ConvertToString(dr, "RebootWindowStartTime");
                            rowDetails.RemoteAccessEndTimeSR = Common.ConvertToString(dr, "RemoteAccessEndTime");
                            rowDetails.RemoteAccessTimeZoneSR = new TimeZoneFX();
                            rowDetails.RemoteAccessTimeZoneSR.ID = Common.ConvertToInt(dr, "RemoteAccessTimeZone");
                            rowDetails.RemoteAccessTimeZoneSR.Name = LookUpValue.GetTimeZone(rowDetails.RemoteAccessTimeZoneSR.ID.Value);
                            rowDetails.RebootPermissionSR = new OptionType();
                            rowDetails.RebootPermissionSR.ID = Common.ConvertToInt(dr, "RebootPermission");
                            rowDetails.RebootPermissionSR.Name = LookUpValue.GetOptionType(rowDetails.RebootPermissionSR.ID.Value);
                            rowDetails.RebootTimeZoneSR = new TimeZoneFX();
                            rowDetails.RebootTimeZoneSR.ID = Common.ConvertToInt(dr, "RebootTimeZone");
                            rowDetails.RebootTimeZoneSR.Name = LookUpValue.GetTimeZone(rowDetails.RebootTimeZoneSR.ID.Value);
                        }
                        else if (Common.ConvertToInt(dr, "AccessPolicyType") == 3)
                        {
                            rowDetails.RemoteAccessStartTimeND = Common.ConvertToString(dr, "RemoteAccessStartTime");
                            rowDetails.AccessPolicyIDND = Common.ConvertToInt(dr, "AccessPolicyID");
                            rowDetails.RemoteControlPermissionND = new OptionType();
                            rowDetails.RemoteControlPermissionND.ID = Common.ConvertToInt(dr, "RemoteControlPermission");
                            rowDetails.RemoteControlPermissionND.Name = LookUpValue.GetOptionType(rowDetails.RemoteControlPermissionND.ID.Value);
                            rowDetails.RebootWindowEndTimeND = Common.ConvertToString(dr, "RebootWindowEndTime");
                            rowDetails.RebootWindowStartTimeND = Common.ConvertToString(dr, "RebootWindowStartTime");
                            rowDetails.RemoteAccessEndTimeND = Common.ConvertToString(dr, "RemoteAccessEndTime");
                            rowDetails.RemoteAccessTimeZoneND = new TimeZoneFX();
                            rowDetails.RemoteAccessTimeZoneND.ID = Common.ConvertToInt(dr, "RemoteAccessTimeZone");
                            rowDetails.RemoteAccessTimeZoneND.Name = LookUpValue.GetTimeZone(rowDetails.RemoteAccessTimeZoneND.ID.Value);
                            rowDetails.RebootPermissionND = new OptionType();
                            rowDetails.RebootPermissionND.ID = Common.ConvertToInt(dr, "RebootPermission");
                            rowDetails.RebootPermissionND.Name = LookUpValue.GetOptionType(rowDetails.RebootPermissionND.ID.Value);
                            rowDetails.RebootTimeZoneND = new TimeZoneFX();
                            rowDetails.RebootTimeZoneND.ID = Common.ConvertToInt(dr, "RebootTimeZone");
                            rowDetails.RebootTimeZoneND.Name = LookUpValue.GetTimeZone(rowDetails.RebootTimeZoneND.ID.Value);
                        }
                    }
                }
            }
            return rowDetails;
        }

        public tblAccessPolicy GetAccessPolicyByID(int AccessPolicyID)
        {
            int errorNum = 0;
            string errorDesc = "";
            DataAccess.resetParams();
            DataAccess.addSqlParam("@CustomerID", ParameterDirection.Input, 16, MySqlDbType.Int32, 0);
            DataAccess.addSqlParam("@AccessPolicyID", ParameterDirection.Input, 16, MySqlDbType.Int32, AccessPolicyID);
            DataSet ds = DataAccess.ExecuteDataSet(GET_ACCESSPLOY, ref errorNum, ref errorDesc);

            tblAccessPolicy rowDetails = new tblAccessPolicy();
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dtCutomer = ds.Tables[0];
                if (dtCutomer != null && dtCutomer.Rows.Count > 0)
                {
                    DataRow dr = dtCutomer.Rows[0];
                    rowDetails.AccessPolicyType = new PolicyType();
                    rowDetails.AccessPolicyType.ID = Common.ConvertToInt(dr, "AccessPolicyType");
                    rowDetails.CustomerID = Common.ConvertToInt(dr, "CustomerID");

                    switch (rowDetails.AccessPolicyType.ID)
                    {
                        case 1:
                            rowDetails.AccessPolicyIDWR = Common.ConvertToInt(dr, "AccessPolicyID");
                            rowDetails.RemoteControlPermissionWR = new OptionType();
                            rowDetails.RemoteControlPermissionWR.ID = Common.ConvertToInt(dr, "RemoteControlPermission");
                            rowDetails.RemoteControlPermissionWR.Name = LookUpValue.GetOptionType(rowDetails.RemoteControlPermissionWR.ID.Value);
                            rowDetails.RebootWindowEndTimeWR = Common.ConvertToString(dr, "RebootWindowEndTime");
                            rowDetails.RebootPermissionWR = new OptionType();
                            rowDetails.RebootPermissionWR.ID = Common.ConvertToInt(dr, "RebootPermission");
                            rowDetails.RebootPermissionWR.Name = LookUpValue.GetOptionType(rowDetails.RebootPermissionWR.ID.Value);
                            rowDetails.RebootWindowStartTimeWR = Common.ConvertToString(dr, "RebootWindowStartTime");
                            rowDetails.RemoteAccessStartTimeWR = Common.ConvertToString(dr, "RemoteAccessStartTime");
                            rowDetails.RemoteAccessEndTimeWR = Common.ConvertToString(dr, "RemoteAccessEndTime");
                            rowDetails.RemoteAccessTimeZoneWR = new TimeZoneFX();
                            rowDetails.RemoteAccessTimeZoneWR.ID = Common.ConvertToInt(dr, "RemoteAccessTimeZone");
                            rowDetails.RemoteAccessTimeZoneWR.Name = LookUpValue.GetTimeZone(rowDetails.RemoteAccessTimeZoneWR.ID.Value);
                            rowDetails.RebootTimeZoneWR = new TimeZoneFX();
                            rowDetails.RebootTimeZoneWR.ID = Common.ConvertToInt(dr, "RebootTimeZone");
                            rowDetails.RebootTimeZoneWR.Name = LookUpValue.GetTimeZone(rowDetails.RebootTimeZoneWR.ID.Value);
                            break;
                        case 2:
                            rowDetails.RemoteAccessStartTimeSR = Common.ConvertToString(dr, "RemoteAccessStartTime");
                            rowDetails.AccessPolicyIDSR = Common.ConvertToInt(dr, "AccessPolicyID");
                            rowDetails.RemoteControlPermissionSR = new OptionType();
                            rowDetails.RemoteControlPermissionSR.ID = Common.ConvertToInt(dr, "RemoteControlPermission");
                            rowDetails.RemoteControlPermissionSR.Name = LookUpValue.GetOptionType(rowDetails.RemoteControlPermissionSR.ID.Value);
                            rowDetails.RebootWindowEndTimeSR = Common.ConvertToString(dr, "RebootWindowEndTime");
                            rowDetails.RebootPermissionSR = new OptionType();
                            rowDetails.RebootPermissionSR.ID = Common.ConvertToInt(dr, "RebootPermission");
                            rowDetails.RebootPermissionSR.Name = LookUpValue.GetOptionType(rowDetails.RebootPermissionSR.ID.Value);
                            rowDetails.RebootWindowStartTimeSR = Common.ConvertToString(dr, "RebootWindowStartTime");
                            rowDetails.RemoteAccessEndTimeSR = Common.ConvertToString(dr, "RemoteAccessEndTime");
                            rowDetails.RemoteAccessTimeZoneSR = new TimeZoneFX();
                            rowDetails.RemoteAccessTimeZoneSR.ID = Common.ConvertToInt(dr, "RemoteAccessTimeZone");
                            rowDetails.RemoteAccessTimeZoneSR.Name = LookUpValue.GetTimeZone(rowDetails.RemoteAccessTimeZoneSR.ID.Value);
                            rowDetails.RebootTimeZoneSR = new TimeZoneFX();
                            rowDetails.RebootTimeZoneSR.ID = Common.ConvertToInt(dr, "RebootTimeZone");
                            rowDetails.RebootTimeZoneSR.Name = LookUpValue.GetTimeZone(rowDetails.RebootTimeZoneSR.ID.Value);
                            break;
                        case 3:
                            rowDetails.RemoteAccessStartTimeND = Common.ConvertToString(dr, "RemoteAccessStartTime");
                            rowDetails.AccessPolicyIDND = Common.ConvertToInt(dr, "AccessPolicyID");
                            rowDetails.RemoteControlPermissionND = new OptionType();
                            rowDetails.RemoteControlPermissionND.ID = Common.ConvertToInt(dr, "RemoteControlPermission");
                            rowDetails.RemoteControlPermissionND.Name = LookUpValue.GetOptionType(rowDetails.RemoteControlPermissionND.ID.Value);
                            rowDetails.RebootWindowEndTimeND = Common.ConvertToString(dr, "RebootWindowEndTime");
                            rowDetails.RebootWindowStartTimeND = Common.ConvertToString(dr, "RebootWindowStartTime");
                            rowDetails.RemoteAccessEndTimeND = Common.ConvertToString(dr, "RemoteAccessEndTime");
                            rowDetails.RemoteAccessTimeZoneND = new TimeZoneFX();
                            rowDetails.RemoteAccessTimeZoneND.ID = Common.ConvertToInt(dr, "RemoteAccessTimeZone");
                            rowDetails.RemoteAccessTimeZoneND.Name = LookUpValue.GetTimeZone(rowDetails.RemoteAccessTimeZoneND.ID.Value);
                            rowDetails.RebootPermissionND = new OptionType();
                            rowDetails.RebootPermissionND.ID = Common.ConvertToInt(dr, "RebootPermission");
                            rowDetails.RebootPermissionND.Name = LookUpValue.GetOptionType(rowDetails.RebootPermissionND.ID.Value);
                            rowDetails.RebootTimeZoneND = new TimeZoneFX();
                            rowDetails.RebootTimeZoneND.ID = Common.ConvertToInt(dr, "RebootTimeZone");
                            rowDetails.RebootTimeZoneND.Name = LookUpValue.GetTimeZone(rowDetails.RebootTimeZoneND.ID.Value);
                            break;
                    }
                }
            }
            return rowDetails;
        }

        public tblPatchingPolicy GetPatchingPolicy(int CustomerID)
        {
            int errorNum = 0;
            string errorDesc = "";
            DataAccess.resetParams();
            DataAccess.addSqlParam("@oCustomerID", ParameterDirection.Input, 16, MySqlDbType.Int32, CustomerID);
            DataAccess.addSqlParam("@PatchingPolicyID", ParameterDirection.Input, 16, MySqlDbType.Int32, 0);
            DataSet ds = DataAccess.ExecuteDataSet(GET_PATCHING, ref errorNum, ref errorDesc);

            tblPatchingPolicy rowDetails = new tblPatchingPolicy();
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dtCutomer = ds.Tables[0];
                if (dtCutomer != null && dtCutomer.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtCutomer.Rows)
                    {
                        if (Common.ConvertToInt(dr, "PatchingPolicyType") == 1)
                        {
                            rowDetails.CustomerID = Common.ConvertToInt(dr, "oCustomerID");
                            rowDetails.PatchingPolicyIDWR = Common.ConvertToInt(dr, "PatchingPolicyID");
                            rowDetails.PolicyNameWR = Common.ConvertToString(dr, "PolicyName");
                            rowDetails.PatchingPolicyTypeIDWR = Common.ConvertToInt(dr, "PatchingPolicyType");
                            rowDetails.WindowStartTimeWR = Common.ConvertToString(dr, "WindowStartTime");
                            rowDetails.WindowEndTimeWR = Common.ConvertToString(dr, "WindowEndTime");
                            rowDetails.WindowTimeZoneWR = new TimeZoneFX();
                            rowDetails.WindowTimeZoneWR.ID = Common.ConvertToInt(dr, "WindowTimeZone");
                            rowDetails.WindowTimeZoneWR.Name = LookUpValue.GetTimeZone(rowDetails.WindowTimeZoneWR.ID.Value);
                            rowDetails.DefineScheduleTypeWR = new ScheduleType();
                            rowDetails.DefineScheduleTypeWR.ID = Common.ConvertToInt(dr, "DefineScheduleType");
                            rowDetails.DefineScheduleTypeWR.Name = LookUpValue.GetScheduleType(rowDetails.DefineScheduleTypeWR.ID.Value);
                            rowDetails.WeekOfDayWR = new WeekOfDay();
                            rowDetails.WeekOfDayWR.ID = Common.ConvertToInt(dr, "WeekOfDay");
                            rowDetails.WeekOfDayWR.Name = LookUpValue.GetWeekOfDay(rowDetails.WeekOfDayWR.ID.Value);
                            rowDetails.MonthOfDayWR = new MonthOfDay();
                            rowDetails.MonthOfDayWR.ID = Common.ConvertToInt(dr, "MonthOfDay");
                            rowDetails.MonthOfDayWR.Name = LookUpValue.GetMonthOfDays(rowDetails.MonthOfDayWR.ID.Value);
                            rowDetails.RebootPermissionWR = new OptionType();
                            rowDetails.RebootPermissionWR.ID = Common.ConvertToInt(dr, "RebootPermission");
                            rowDetails.RebootPermissionWR.Name = LookUpValue.GetOptionType(rowDetails.RebootPermissionWR.ID.Value);
                            rowDetails.RebootWindowStartTimeWR = Common.ConvertToString(dr, "RebootWindowStartTime");
                            rowDetails.RebootWindowEndTimeWR = Common.ConvertToString(dr, "RebootWindowEndTime");
                            rowDetails.RebootWindowTimeZoneWR = new TimeZoneFX();
                            rowDetails.RebootWindowTimeZoneWR.ID = Common.ConvertToInt(dr, "RebootWindowTimeZone");
                            rowDetails.RebootWindowTimeZoneWR.Name = LookUpValue.GetTimeZone(rowDetails.RebootWindowTimeZoneWR.ID.Value);

                        }
                        else if (Common.ConvertToInt(dr, "PatchingPolicyType") == 2)
                        {
                            rowDetails.CustomerID = Common.ConvertToInt(dr, "oCustomerID");
                            rowDetails.PatchingPolicyIDSR = Common.ConvertToInt(dr, "PatchingPolicyID");
                            rowDetails.PolicyNameSR = Common.ConvertToString(dr, "PolicyName");
                            rowDetails.PatchingPolicyTypeIDSR = Common.ConvertToInt(dr, "PatchingPolicyType");
                            rowDetails.WindowStartTimeSR = Common.ConvertToString(dr, "WindowStartTime");
                            rowDetails.WindowEndTimeSR = Common.ConvertToString(dr, "WindowEndTime");
                            rowDetails.WindowTimeZoneSR = new TimeZoneFX();
                            rowDetails.WindowTimeZoneSR.ID = Common.ConvertToInt(dr, "WindowTimeZone");
                            rowDetails.WindowTimeZoneSR.Name = LookUpValue.GetTimeZone(rowDetails.WindowTimeZoneSR.ID.Value);
                            rowDetails.DefineScheduleTypeSR = new ScheduleType();
                            rowDetails.DefineScheduleTypeSR.ID = Common.ConvertToInt(dr, "DefineScheduleType");
                            rowDetails.DefineScheduleTypeSR.Name = LookUpValue.GetScheduleType(rowDetails.DefineScheduleTypeSR.ID.Value);
                            rowDetails.WeekOfDaySR = new WeekOfDay();
                            rowDetails.WeekOfDaySR.ID = Common.ConvertToInt(dr, "WeekOfDay");
                            rowDetails.WeekOfDaySR.Name = LookUpValue.GetWeekOfDay(rowDetails.WeekOfDaySR.ID.Value);
                            rowDetails.MonthOfDaySR = new MonthOfDay();
                            rowDetails.MonthOfDaySR.ID = Common.ConvertToInt(dr, "MonthOfDay");
                            rowDetails.MonthOfDaySR.Name = LookUpValue.GetMonthOfDays(rowDetails.MonthOfDaySR.ID.Value);
                            rowDetails.RebootPermissionSR = new OptionType();
                            rowDetails.RebootPermissionSR.ID = Common.ConvertToInt(dr, "RebootPermission");
                            rowDetails.RebootPermissionSR.Name = LookUpValue.GetOptionType(rowDetails.RebootPermissionSR.ID.Value);
                            rowDetails.RebootWindowStartTimeSR = Common.ConvertToString(dr, "RebootWindowStartTime");
                            rowDetails.RebootWindowEndTimeSR = Common.ConvertToString(dr, "RebootWindowEndTime");

                            rowDetails.RebootWindowTimeZoneSR = new TimeZoneFX();
                            rowDetails.RebootWindowTimeZoneSR.ID = Common.ConvertToInt(dr, "RebootWindowTimeZone");
                            rowDetails.RebootWindowTimeZoneSR.Name = LookUpValue.GetTimeZone(rowDetails.RebootWindowTimeZoneWR.ID.Value);
                        }
                        else if (Common.ConvertToInt(dr, "PatchingPolicyType") == 3)
                        {
                            rowDetails.PatchingPolicyIDDND = Common.ConvertToInt(dr, "PatchingPolicyID");
                            rowDetails.MonthOfDayND = new MonthOfDay();
                            rowDetails.MonthOfDayND.ID = Common.ConvertToInt(dr, "MonthOfDay");
                            rowDetails.MonthOfDayND.Name = LookUpValue.GetMonthOfDays(rowDetails.MonthOfDayND.ID.Value);
                            rowDetails.PatchingPolicyTypeIDND = Common.ConvertToInt(dr, "PatchingPolicyType");
                            rowDetails.PolicyNameND = Common.ConvertToString(dr, "PolicyName");
                            //  rowDetails.PatchingPolicyTypeIDWR = Common.ConvertToInt(dr, "PatchingPolicyType");
                            rowDetails.WindowStartTimeND = Common.ConvertToString(dr, "WindowStartTime");
                            rowDetails.WindowEndTimeND = Common.ConvertToString(dr, "WindowEndTime");
                            rowDetails.WindowTimeZoneND = new TimeZoneFX();
                            rowDetails.WindowTimeZoneND.ID = Common.ConvertToInt(dr, "WindowTimeZone");
                            rowDetails.WindowTimeZoneND.Name = LookUpValue.GetTimeZone(rowDetails.WindowTimeZoneND.ID.Value);
                            rowDetails.DefineScheduleTypeND = new ScheduleType();
                            rowDetails.DefineScheduleTypeND.ID = Common.ConvertToInt(dr, "DefineScheduleType");
                            rowDetails.DefineScheduleTypeND.Name = LookUpValue.GetScheduleType(rowDetails.DefineScheduleTypeND.ID.Value);
                            rowDetails.WeekOfDayND = new WeekOfDay();
                            rowDetails.WeekOfDayND.ID = Common.ConvertToInt(dr, "WeekOfDay");
                            rowDetails.WeekOfDayND.Name = LookUpValue.GetWeekOfDay(rowDetails.WeekOfDayND.ID.Value);
                            rowDetails.MonthOfDayND = new MonthOfDay();
                            rowDetails.MonthOfDayND.ID = Common.ConvertToInt(dr, "MonthOfDay");
                            rowDetails.MonthOfDayND.Name = LookUpValue.GetMonthOfDays(rowDetails.MonthOfDayND.ID.Value);
                            rowDetails.RebootPermissionND = new OptionType();
                            rowDetails.RebootPermissionND.ID = Common.ConvertToInt(dr, "RebootPermission");
                            rowDetails.RebootPermissionND.Name = LookUpValue.GetOptionType(rowDetails.RebootPermissionND.ID.Value);
                            rowDetails.RebootWindowStartTimeND = Common.ConvertToString(dr, "RebootWindowStartTime");
                            rowDetails.RebootWindowEndTimeND = Common.ConvertToString(dr, "RebootWindowEndTime");
                            rowDetails.RebootWindowTimeZoneND = new TimeZoneFX();
                            rowDetails.RebootWindowTimeZoneND.ID = Common.ConvertToInt(dr, "RebootWindowTimeZone");
                            rowDetails.RebootWindowTimeZoneND.Name = LookUpValue.GetTimeZone(rowDetails.RebootWindowTimeZoneND.ID.Value);
                        }
                    }
                }
            }
            return rowDetails;
        }

        public tblPatchingPolicy GetPatchingPolicyByID(int PatchingPolicyID)
        {
            int errorNum = 0;
            string errorDesc = "";
            DataAccess.resetParams();
            DataAccess.addSqlParam("@oCustomerID", ParameterDirection.Input, 16, MySqlDbType.Int32, 0);
            DataAccess.addSqlParam("@PatchingPolicyID", ParameterDirection.Input, 16, MySqlDbType.Int32, PatchingPolicyID);
            DataSet ds = DataAccess.ExecuteDataSet(GET_PATCHING, ref errorNum, ref errorDesc);

            tblPatchingPolicy rowDetails = new tblPatchingPolicy();
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dtCutomer = ds.Tables[0];
                if (dtCutomer != null && dtCutomer.Rows.Count > 0)
                {
                    DataRow dr = dtCutomer.Rows[0];

                    switch (Common.ConvertToInt(dr, "PatchingPolicyType"))
                    {
                        case 1:
                            rowDetails.CustomerID = Common.ConvertToInt(dr, "oCustomerID");
                            rowDetails.PatchingPolicyIDWR = Common.ConvertToInt(dr, "PatchingPolicyID");
                            rowDetails.PolicyNameWR = Common.ConvertToString(dr, "PolicyName");
                            rowDetails.PatchingPolicyTypeIDWR = Common.ConvertToInt(dr, "PatchingPolicyType");
                            rowDetails.WindowStartTimeWR = Common.ConvertToString(dr, "WindowStartTime");
                            rowDetails.WindowEndTimeWR = Common.ConvertToString(dr, "WindowEndTime");
                            rowDetails.WindowTimeZoneWR = new TimeZoneFX();
                            rowDetails.WindowTimeZoneWR.ID = Common.ConvertToInt(dr, "WindowTimeZone");
                            rowDetails.WindowTimeZoneWR.Name = LookUpValue.GetTimeZone(rowDetails.WindowTimeZoneWR.ID.Value);
                        //    rowDetails.DefineScheduleTypeWR = new ScheduleType();
                        //    rowDetails.DefineScheduleTypeWR.ID = Common.ConvertToInt(dr, "DefineScheduleType");
                         //   rowDetails.DefineScheduleTypeWR.Name = LookUpValue.GetScheduleType(rowDetails.DefineScheduleTypeWR.ID.Value);
                            rowDetails.WeekOfDayWR = new WeekOfDay();
                            rowDetails.WeekOfDayWR.ID = Common.ConvertToInt(dr, "WeekOfDay");
                            rowDetails.WeekOfDayWR.Name = LookUpValue.GetWeekOfDay(rowDetails.WeekOfDayWR.ID.Value);
                       //     rowDetails.MonthOfDayWR = new MonthOfDay();
                       //     rowDetails.MonthOfDayWR.ID = Common.ConvertToInt(dr, "MonthOfDay");
                        //    rowDetails.MonthOfDayWR.Name = LookUpValue.GetMonthOfDays(rowDetails.MonthOfDayWR.ID.Value);
                            rowDetails.RebootPermissionWR = new OptionType();
                            rowDetails.RebootPermissionWR.ID = Common.ConvertToInt(dr, "RebootPermission");
                            rowDetails.RebootPermissionWR.Name = LookUpValue.GetOptionType(rowDetails.RebootPermissionWR.ID.Value);
                            rowDetails.RebootWindowStartTimeWR = Common.ConvertToString(dr, "RebootWindowStartTime");
                            rowDetails.RebootWindowEndTimeWR = Common.ConvertToString(dr, "RebootWindowEndTime");
                            rowDetails.RebootWindowTimeZoneWR = new TimeZoneFX();
                            rowDetails.RebootWindowTimeZoneWR.ID = Common.ConvertToInt(dr, "RebootWindowTimeZone");
                            rowDetails.RebootWindowTimeZoneWR.Name = LookUpValue.GetTimeZone(rowDetails.RebootWindowTimeZoneWR.ID.Value);
                            break;
                        case 2:
                            rowDetails.CustomerID = Common.ConvertToInt(dr, "oCustomerID");
                            rowDetails.PatchingPolicyIDSR = Common.ConvertToInt(dr, "PatchingPolicyID");
                            rowDetails.PolicyNameSR = Common.ConvertToString(dr, "PolicyName");
                            rowDetails.PatchingPolicyTypeIDSR = Common.ConvertToInt(dr, "PatchingPolicyType");
                            rowDetails.WindowStartTimeSR = Common.ConvertToString(dr, "WindowStartTime");
                            rowDetails.WindowEndTimeSR = Common.ConvertToString(dr, "WindowEndTime");
                            rowDetails.WindowTimeZoneSR = new TimeZoneFX();
                            rowDetails.WindowTimeZoneSR.ID = Common.ConvertToInt(dr, "WindowTimeZone");
                            rowDetails.WindowTimeZoneSR.Name = LookUpValue.GetTimeZone(rowDetails.WindowTimeZoneSR.ID.Value);
                       //     rowDetails.DefineScheduleTypeSR = new ScheduleType();
                       //     rowDetails.DefineScheduleTypeSR.ID = Common.ConvertToInt(dr, "DefineScheduleType");
                       //     rowDetails.DefineScheduleTypeSR.Name = LookUpValue.GetScheduleType(rowDetails.DefineScheduleTypeSR.ID.Value);
                            rowDetails.WeekOfDaySR = new WeekOfDay();
                            rowDetails.WeekOfDaySR.ID = Common.ConvertToInt(dr, "WeekOfDay");
                            rowDetails.WeekOfDaySR.Name = LookUpValue.GetWeekOfDay(rowDetails.WeekOfDaySR.ID.Value);
                       //     rowDetails.MonthOfDaySR = new MonthOfDay();
                       //     rowDetails.MonthOfDaySR.ID = Common.ConvertToInt(dr, "MonthOfDay");
                      //      rowDetails.MonthOfDaySR.Name = LookUpValue.GetMonthOfDays(rowDetails.MonthOfDaySR.ID.Value);
                            rowDetails.RebootPermissionSR = new OptionType();
                            rowDetails.RebootPermissionSR.ID = Common.ConvertToInt(dr, "RebootPermission");
                            rowDetails.RebootPermissionSR.Name = LookUpValue.GetOptionType(rowDetails.RebootPermissionSR.ID.Value);
                            rowDetails.RebootWindowStartTimeSR = Common.ConvertToString(dr, "RebootWindowStartTime");
                            rowDetails.RebootWindowEndTimeSR = Common.ConvertToString(dr, "RebootWindowEndTime");
                            rowDetails.RebootWindowTimeZoneSR = new TimeZoneFX();
                            rowDetails.RebootWindowTimeZoneSR.ID = Common.ConvertToInt(dr, "RebootWindowTimeZone");
                            rowDetails.RebootWindowTimeZoneSR.Name = LookUpValue.GetTimeZone(rowDetails.RebootWindowTimeZoneSR.ID.Value);
                            break;
                        case 3:
                            rowDetails.PatchingPolicyIDDND = Common.ConvertToInt(dr, "PatchingPolicyID");
                            rowDetails.PatchingPolicyTypeIDND = Common.ConvertToInt(dr, "PatchingPolicyType");
                            rowDetails.PolicyNameND = Common.ConvertToString(dr, "PolicyName");
                            rowDetails.WindowStartTimeND = Common.ConvertToString(dr, "WindowStartTime");
                            rowDetails.WindowEndTimeND = Common.ConvertToString(dr, "WindowEndTime");
                            rowDetails.WindowTimeZoneND = new TimeZoneFX();
                            rowDetails.WindowTimeZoneND.ID = Common.ConvertToInt(dr, "WindowTimeZone");
                            rowDetails.WindowTimeZoneND.Name = LookUpValue.GetTimeZone(rowDetails.WindowTimeZoneND.ID.Value);
                      //      rowDetails.DefineScheduleTypeND = new ScheduleType();
                     //       rowDetails.DefineScheduleTypeND.ID = Common.ConvertToInt(dr, "DefineScheduleType");
                     //       rowDetails.DefineScheduleTypeND.Name = LookUpValue.GetScheduleType(rowDetails.DefineScheduleTypeND.ID.Value);
                            rowDetails.WeekOfDayND = new WeekOfDay();
                            rowDetails.WeekOfDayND.ID = Common.ConvertToInt(dr, "WeekOfDay");
                            rowDetails.WeekOfDayND.Name = LookUpValue.GetWeekOfDay(rowDetails.WeekOfDayND.ID.Value);
                     //       rowDetails.MonthOfDayND = new MonthOfDay();
                    //        rowDetails.MonthOfDayND.ID = Common.ConvertToInt(dr, "MonthOfDay");
                    //        rowDetails.MonthOfDayND.Name = LookUpValue.GetMonthOfDays(rowDetails.MonthOfDayND.ID.Value);
                            rowDetails.RebootPermissionND = new OptionType();
                            rowDetails.RebootPermissionND.ID = Common.ConvertToInt(dr, "RebootPermission");
                            rowDetails.RebootPermissionND.Name = LookUpValue.GetOptionType(rowDetails.RebootPermissionND.ID.Value);
                            rowDetails.RebootWindowStartTimeND = Common.ConvertToString(dr, "RebootWindowStartTime");
                            rowDetails.RebootWindowEndTimeND = Common.ConvertToString(dr, "RebootWindowEndTime");
                            rowDetails.RebootWindowTimeZoneND = new TimeZoneFX();  
                            rowDetails.RebootWindowTimeZoneND.ID = Common.ConvertToInt(dr, "RebootWindowTimeZone");
                            rowDetails.RebootWindowTimeZoneND.Name = LookUpValue.GetTimeZone(rowDetails.RebootWindowTimeZoneND.ID.Value);
                            break;
                    }
                }
            }
            return rowDetails;
        }

        public List<tblCustomerDetails> GetAllCustomer(int CustomerID)
        {
            List<tblCustomerDetails> lstCustomer = new List<tblCustomerDetails>();

            int errorNum = 0;
            string errorDesc = "";
            DataAccess.resetParams();
            DataAccess.addSqlParam("@CustomerID", ParameterDirection.Input, 16, MySqlDbType.Int32, CustomerID);
            DataSet ds = DataAccess.ExecuteDataSet(GET_CUSTOMER, ref errorNum, ref errorDesc);
            tblCustomerDetails customerDetails = new tblCustomerDetails();
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dtCutomer = ds.Tables[0];
                if (dtCutomer != null && dtCutomer.Rows.Count > 0)
                {
                    for (int i = 0; i < dtCutomer.Rows.Count; i++)
                    {
                        DataRow dr = dtCutomer.Rows[i];
                        customerDetails.CustomerID = Common.ConvertToInt(dr, "CustomerID");
                        customerDetails.CustomerName = Common.ConvertToString(dr, "CustomerName");
                        customerDetails.EmailAddress = Common.ConvertToString(dr, "EmailAddress");
                        customerDetails.CustomerContactName = Common.ConvertToString(dr, "CustomerContactName");
                        customerDetails.Password = Common.ConvertToString(dr, "Password");
                        customerDetails.TimeZone = new TimeZoneFX();
                        customerDetails.TimeZone.ID = Common.ConvertToInt(dr, "TimeZone");
                        customerDetails.TimeZone.Name = LookUpValue.GetTimeZone(customerDetails.TimeZone.ID.Value);
                        customerDetails.NOCCommunication = new NOCCommunicationBy();
                        customerDetails.NOCCommunication.ID = Common.ConvertToInt(dr, "NOCCommunication");
                        lstCustomer.Add(customerDetails);
                    }
                }
            }
            return lstCustomer;
        }

    }
}