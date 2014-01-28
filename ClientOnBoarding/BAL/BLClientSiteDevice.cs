using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClientOnBoarding.Models;
using System.Data;
using MySql.Data.MySqlClient;
namespace ClientOnBoarding.BAL
{
    public class BLClientSiteDevice
    {
        public const string GET_CLIENT_SITE_DEVICE = "spGetClientSiteDevice";
        public const string SET_CLIENT_SITE_DEVICE = "spSetClientSiteDevice";
        public const string GET_ALL_CLIENT_SITE_DEVICE = "spGetAllClientSiteDevice";
        public const string DEL_CLIENT_SITE_DEVICE = "spDelClientsitedevice";

        public int SaveClientSiteDevice(tblClientSiteDevice clientSitedevice)
        {
            int errorNum = 0;
            string errorDesc = "";
            DataAccess.resetParams();
            DataAccess.addSqlParam("@ClientSiteDeviceID", ParameterDirection.Input, 16, MySqlDbType.Int32, clientSitedevice.DeviceID);
            DataAccess.addSqlParam("@ClientID", ParameterDirection.Input, 16, MySqlDbType.Int32, clientSitedevice.ClientID);
            DataAccess.addSqlParam("@DeviceType", ParameterDirection.Input, 16, MySqlDbType.Int32, clientSitedevice.DeviceType.ID);
            DataAccess.addSqlParam("@Description", ParameterDirection.Input, 100, MySqlDbType.VarChar, clientSitedevice.DeviceDescription);
            DataAccess.addSqlParam("@DeviceIDFromRMMTool", ParameterDirection.Input, 100, MySqlDbType.VarChar, clientSitedevice.DeviceIDFromRMMTool);
            DataAccess.addSqlParam("@MiscInfo", ParameterDirection.Input, 200, MySqlDbType.VarChar, clientSitedevice.MiscInfo);
            DataAccess.addSqlParam("@UserName", ParameterDirection.Input, 100, MySqlDbType.VarChar, clientSitedevice.UserName);
            DataAccess.addSqlParam("@Password", ParameterDirection.Input, 100, MySqlDbType.VarChar, clientSitedevice.Password);
            DataAccess.addSqlParam("@AccessPolicyID", ParameterDirection.Input, 16, MySqlDbType.Int32, clientSitedevice.IsAccessPolicy.ID);
            DataAccess.addSqlParam("@MaintenancePolicyID", ParameterDirection.Input, 16, MySqlDbType.Int32, clientSitedevice.IsMaintenancePolicy.ID);
            DataAccess.addSqlParam("@PatchingPolicyID", ParameterDirection.Input, 16, MySqlDbType.Int32, clientSitedevice.IsPatchingPolicy.ID);
            DataAccess.addSqlParam("@AntiVirusID", ParameterDirection.Input, 16, MySqlDbType.Int32, clientSitedevice.IsAntiVirus == null ? 0:clientSitedevice.IsAntiVirus.ID);
            DataAccess.addSqlParam("@BackUpPolicyID", ParameterDirection.Input, 16, MySqlDbType.Int32, clientSitedevice.IsBackUpPolicy == null ? 0:clientSitedevice.IsBackUpPolicy.ID);
            DataAccess.addSqlParam("@ToolID", ParameterDirection.Input, 16, MySqlDbType.Int32, clientSitedevice.IsRMMTool.ID);
            DataAccess.addSqlParam("@Isactive", ParameterDirection.InputOutput, 16, MySqlDbType.Int16, 1);
            DataAccess.addSqlParam("@Identity", ParameterDirection.InputOutput, 16, MySqlDbType.Int32);
         
            DataAccess.ExecuteNonQuery(SET_CLIENT_SITE_DEVICE, ref errorNum, ref errorDesc);
            return Common.ConvertToInt(DataAccess.getSQLParam("@Identity").ToString());
        }



        public tblClientSiteDevice GetClientSiteDevice(int ClientSiteDeviceID, int CustomerID)
        {
            int errNum = 0;
            string errDesc = "";

            DataAccess.resetParams();
            DataAccess.addSqlParam("@ClientSiteDeviceID", ParameterDirection.Input, 16, MySqlDbType.Int32, ClientSiteDeviceID);
            DataAccess.addSqlParam("@CustomerID", ParameterDirection.Input, 16, MySqlDbType.Int32, CustomerID);
            DataSet ds = DataAccess.ExecuteDataSet(GET_CLIENT_SITE_DEVICE, ref errNum, ref errDesc);
            tblClientSiteDevice ClientsiteDevice = new tblClientSiteDevice();
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dtClientsiteDevice = ds.Tables[0];
                if (dtClientsiteDevice != null && dtClientsiteDevice.Rows.Count > 0)
                {
                    DataRow dr = dtClientsiteDevice.Rows[0];
                    ClientsiteDevice.DeviceID = Common.ConvertToInt(dr, "ClientSiteDeviceID");
                    ClientsiteDevice.ClientID = Common.ConvertToInt(dr, "ClientID");
                    ClientsiteDevice.DeviceType = new DeviceType();
                    ClientsiteDevice.DeviceType.ID = Common.ConvertToInt(dr, "DeviceType");
                    ClientsiteDevice.DeviceType.Name = LookUpValue.GetDeviceType(ClientsiteDevice.DeviceType.ID.Value);
                    ClientsiteDevice.DeviceIDFromRMMTool = Common.ConvertToString(dr, "DeviceIDFromRMMTool");
                    ClientsiteDevice.DeviceDescription = Common.ConvertToString(dr, "Description");
                    ClientsiteDevice.UserName = Common.ConvertToString(dr, "UserName");                   
                    ClientsiteDevice.Password = Common.ConvertToString(dr, "Password");
                    ClientsiteDevice.MiscInfo = Common.ConvertToString(dr, "MiscInfo");
                    ClientsiteDevice.IsAccessPolicy = new PolicyType();
                    ClientsiteDevice.IsAccessPolicy.ID = Common.ConvertToInt(dr, "PolicyID");
                    ClientsiteDevice.IsAccessPolicy.Name = LookUpValue.GetOptionType(ClientsiteDevice.IsAccessPolicy.ID.Value);
                    ClientsiteDevice.IsMaintenancePolicy = new PolicyType();
                    ClientsiteDevice.IsMaintenancePolicy.ID = Common.ConvertToInt(dr, "MaintenancePolicyID");
                    ClientsiteDevice.IsMaintenancePolicy.Name = LookUpValue.GetOptionType(ClientsiteDevice.IsMaintenancePolicy.ID.Value);
                    ClientsiteDevice.IsPatchingPolicy = new PolicyType();
                    ClientsiteDevice.IsPatchingPolicy.ID = Common.ConvertToInt(dr, "PatchingPolicyID");
                    ClientsiteDevice.IsPatchingPolicy.Name = LookUpValue.GetOptionType(ClientsiteDevice.IsPatchingPolicy.ID.Value);
                    ClientsiteDevice.IsAntiVirus = new PolicyType();
                    ClientsiteDevice.IsAntiVirus.ID = Common.ConvertToInt(dr, "AntiVirusID");
                    ClientsiteDevice.IsAntiVirus.Name = LookUpValue.GetOptionType(ClientsiteDevice.IsAntiVirus.ID.Value);
                    ClientsiteDevice.IsBackUpPolicy = new PolicyType();
                    ClientsiteDevice.IsBackUpPolicy.ID = Common.ConvertToInt(dr, "BackUpPolicyID");
                    ClientsiteDevice.IsBackUpPolicy.Name = LookUpValue.GetOptionType(ClientsiteDevice.IsBackUpPolicy.ID.Value);
                    ClientsiteDevice.IsRMMTool = new PolicyType();
                    ClientsiteDevice.IsRMMTool.ID = Common.ConvertToInt(dr, "ToolID");
                    ClientsiteDevice.IsRMMTool.Name = LookUpValue.GetOptionType(ClientsiteDevice.IsRMMTool.ID.Value);
                }
            }
            return ClientsiteDevice;
        }

        public List<tblClientSiteDevice> GetAllClientSiteDevice(int ClientID, int pageNumber, int pageSize, int sortColumnIndex, string sortOrder, string searchText, ref int totalRecords)
        {
            List<tblClientSiteDevice> lstAllClientSiteDevice = new List<tblClientSiteDevice>();

            int errNum = 0;
            string errDesc = "";

            DataAccess.resetParams();
            DataAccess.addSqlParam("@ClientID", ParameterDirection.Input, 16, MySqlDbType.Int32, ClientID);
            DataAccess.addSqlParam("@pageNumber", ParameterDirection.Input, 16, MySqlDbType.Int32, pageNumber);
            DataAccess.addSqlParam("@pageRecord", ParameterDirection.Input, 16, MySqlDbType.Int32, pageSize);
            DataAccess.addSqlParam("@sortColumnIndex", ParameterDirection.Input, 16, MySqlDbType.Int32, sortColumnIndex);
            DataAccess.addSqlParam("@searchOrder", ParameterDirection.Input, 4, MySqlDbType.VarChar, sortOrder);
            DataAccess.addSqlParam("@searchText", ParameterDirection.Input, 100, MySqlDbType.VarChar, searchText);

            DataSet ds = DataAccess.ExecuteDataSet(GET_ALL_CLIENT_SITE_DEVICE, ref errNum, ref errDesc);
            tblClientSiteDevice ClientsiteDevice = new tblClientSiteDevice();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dtClientsiteDevice = ds.Tables[0];
                DataTable dtTotalRecords = ds.Tables[1];
                totalRecords = Common.ConvertToInt(dtTotalRecords.Rows[0], "TotalRecords");
                if (dtClientsiteDevice != null && dtClientsiteDevice.Rows.Count > 0)
                {
                    for (int i = 0; i < dtClientsiteDevice.Rows.Count; i++)
                    {
                        DataRow dr = dtClientsiteDevice.Rows[i];
                        ClientsiteDevice = new tblClientSiteDevice();
                        ClientsiteDevice.DeviceID = Common.ConvertToInt(dr, "ClientSiteDeviceID");
                        ClientsiteDevice.ClientID = Common.ConvertToInt(dr, "ClientID");
                        ClientsiteDevice.DeviceType = new DeviceType();
                        ClientsiteDevice.DeviceType.ID = Common.ConvertToInt(dr, "DeviceType");
                        ClientsiteDevice.DeviceType.Name = LookUpValue.GetDeviceType(ClientsiteDevice.DeviceType.ID.Value);
                        ClientsiteDevice.DeviceIDFromRMMTool = Common.ConvertToString(dr, "DeviceIDFromRMMTool");
                        ClientsiteDevice.DeviceDescription = Common.ConvertToString(dr, "Description");
                        ClientsiteDevice.UserName = Common.ConvertToString(dr, "UserName");
                        ClientsiteDevice.Password = Common.ConvertToString(dr, "Password");
                        ClientsiteDevice.MiscInfo = Common.ConvertToString(dr, "MiscInfo");
                        ClientsiteDevice.IsAccessPolicy = new PolicyType();
                        ClientsiteDevice.IsAccessPolicy.Name = Common.ConvertToString(dr, "AccessPolicyName");
                        ClientsiteDevice.IsMaintenancePolicy = new PolicyType();
                        ClientsiteDevice.IsMaintenancePolicy.Name = Common.ConvertToString(dr, "MaintenancePolicyName");
                        ClientsiteDevice.IsPatchingPolicy = new PolicyType();
                        ClientsiteDevice.IsPatchingPolicy.Name = Common.ConvertToString(dr, "PatchingPolicyName");
                        ClientsiteDevice.IsAntiVirus = new PolicyType();
                        ClientsiteDevice.IsAntiVirus.Name = Common.ConvertToString(dr, "AVPolicy");
                      
                        ClientsiteDevice.IsRMMTool = new PolicyType();
                        ClientsiteDevice.IsRMMTool.Name = Common.ConvertToString(dr, "ToolInfomationName");
                        ClientsiteDevice.IsBackUpPolicy = new PolicyType();
                        ClientsiteDevice.IsBackUpPolicy.Name = Common.ConvertToString(dr, "BackupPolicyName");
                        lstAllClientSiteDevice.Add(ClientsiteDevice);
                    }
                }
            }
            return lstAllClientSiteDevice;
        }

        public int DelContact(int ContactID)
        {
            int errNum = 0;
            string errDesc = "";
            DataAccess.resetParams();
            DataAccess.addSqlParam("@ClientSiteDeviceID", ParameterDirection.Input, 16, MySqlDbType.Int32, ContactID);
            DataAccess.ExecuteNonQuery(DEL_CLIENT_SITE_DEVICE, ref errNum, ref errDesc);

            return errNum;
        }

        internal void FillDropDownList(ref List<tblMaintenancePolicy> Manitenancepoly, ref List<tblAccessPolicy> Accesspoly, ref List<tblAntiVirusPolicy> Antiviruspoly, ref List<tblBackUpPolicy> Backuppoly, ref List<tblPatchingPolicy> PatchingPoly, ref List<tbltoolInfomation> ToolInfoPoly)
        {

            int errorNum = 0;
            string errorDesc = "";

            DataAccess.resetParams();
            DataAccess.addSqlParam("@ClientSiteDeviceID", ParameterDirection.Input, 16, MySqlDbType.Int32);
            DataAccess.addSqlParam("@CustomerID", ParameterDirection.Input, 16, MySqlDbType.Int32, SessionHelper.UserSession.CustomerID);
            DataSet ds = DataAccess.ExecuteDataSet(GET_CLIENT_SITE_DEVICE, ref errorNum, ref errorDesc);
            Manitenancepoly = new List<tblMaintenancePolicy>();
            Manitenancepoly.Add(new tblMaintenancePolicy() { MaintenancePolicyID = 0, ActivityName = "-- Select --" });
            DataTable dttblmaintinance = ds.Tables[3];
            if (dttblmaintinance != null && dttblmaintinance.Rows.Count > 0)
            {
                foreach (DataRow dPP in dttblmaintinance.Rows)
                {
                    tblMaintenancePolicy tblmaintinance = new tblMaintenancePolicy();
                    tblmaintinance.MaintenancePolicyID = Common.ConvertToInt(dPP, "MaintenancePolicyID");
                    tblmaintinance.ActivityName = Common.ConvertToString(dPP, "ActivityName");
                    Manitenancepoly.Add(tblmaintinance);

                }

            }
            ToolInfoPoly = new List<tbltoolInfomation>();
            ToolInfoPoly.Add(new tbltoolInfomation() { ToolIDRT = 0, RMMTool ="-- Select --"});
            DataTable dttbltoolinfomation = ds.Tables[1];
            if (dttbltoolinfomation != null && dttbltoolinfomation.Rows.Count > 0)
            {
                //  DataRow dTI = dttbltoolinfomation.Rows[1];
                foreach (DataRow dTI in dttbltoolinfomation.Rows)
                {
                    tbltoolInfomation toolInfo = new tbltoolInfomation();
                    toolInfo.ToolIDRT = Common.ConvertToInt(dTI, "ToolID");
                    toolInfo.RMMTool = Common.ConvertToString(dTI, "ToolInfomationName");
                    ToolInfoPoly.Add(toolInfo);
                }
            }
            Accesspoly = new List<tblAccessPolicy>();
            Accesspoly.Add(new tblAccessPolicy() { AccessPolicyID = 0, productname = "-- Select --" });
            DataTable dttblAccpolicy = ds.Tables[4];
            if (dttblAccpolicy != null && dttblAccpolicy.Rows.Count > 0)
            {
                foreach (DataRow dAP in dttblAccpolicy.Rows)
                {
                    tblAccessPolicy tblaccess = new tblAccessPolicy();
                    tblaccess.AccessPolicyID = Common.ConvertToInt(dAP, "AccessPolicyID");
                    tblaccess.productname = Common.ConvertToString(dAP, "productname");
                    Accesspoly.Add(tblaccess);
                }
            }
            PatchingPoly = new List<tblPatchingPolicy>();
            PatchingPoly.Add(new tblPatchingPolicy() { PatchingPolicyIDWR = 0, PolicyNameND ="-- Select --"});
            DataTable dttblPatchingpolicy = ds.Tables[2];
            if (dttblPatchingpolicy != null && dttblPatchingpolicy.Rows.Count > 0)
            {
                //  DataRow dTI = dttbltoolinfomation.Rows[1];
                foreach (DataRow dPP in dttblPatchingpolicy.Rows)
                {
                    tblPatchingPolicy tblpatch = new tblPatchingPolicy();
                    tblpatch.PatchingPolicyIDWR = Common.ConvertToInt(dPP, "PatchingPolicyID");
                    tblpatch.PolicyNameND = Common.ConvertToString(dPP, "PolicyNameND");
                    PatchingPoly.Add(tblpatch);
                }
            }

            Antiviruspoly = new List<tblAntiVirusPolicy>();
            Antiviruspoly.Add(new tblAntiVirusPolicy() { AntiVirusID = 0, PolicyName = "-- Select --" });
            DataTable dttblAantivspolicy = ds.Tables[5];
            if (dttblAantivspolicy != null && dttblAantivspolicy.Rows.Count > 0)
            {
                foreach (DataRow dAP in dttblAantivspolicy.Rows)
                {
                    tblAntiVirusPolicy tblantivirus = new tblAntiVirusPolicy();
                    tblantivirus.AntiVirusID = Common.ConvertToInt(dAP, "AntiVirusID");
                    tblantivirus.PolicyName = Common.ConvertToString(dAP, "PolicyName");
                    Antiviruspoly.Add(tblantivirus);
                }
            }
            Backuppoly = new List<tblBackUpPolicy>();
            Backuppoly.Add(new tblBackUpPolicy() { BackUpPolicyID = 0, PolicyName = "-- Select --" });
            DataTable dttblBackupspolicy = ds.Tables[6];
            if (dttblBackupspolicy != null && dttblBackupspolicy.Rows.Count > 0)
            {
                foreach (DataRow dAP in dttblBackupspolicy.Rows)
                {
                    tblBackUpPolicy tblbackup = new tblBackUpPolicy();
                    tblbackup.BackUpPolicyID = Common.ConvertToInt(dAP, "BackUpPolicyID");
                    tblbackup.PolicyName = Common.ConvertToString(dAP, "PolicyName");
                    Backuppoly.Add(tblbackup);
                }
            }

        }
    }
}