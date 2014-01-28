using ClientOnBoarding.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ClientOnBoarding.BAL
{


    public class BLSearch
    {
        public const string GET_SEARCHRESULT = "sp_GetCustomerSearch";

        public List<SearchResult> GetSearchResult(string CustomerName, string clientName, string DeviceDescription, int pageNumber, int pageSize, int sortColumnIndex, string sortOrder, string searchText, ref int totalRecords)
        {
            List<SearchResult> lstsearchresult = new List<SearchResult>();
            int errorNum = 0;
            string errorDesc = "";
            DataAccess.resetParams();
            DataAccess.addSqlParam("@CustomerName", ParameterDirection.Input, 50, MySqlDbType.VarChar, CustomerName);
            DataAccess.addSqlParam("@clientName", ParameterDirection.Input, 50, MySqlDbType.VarChar, clientName);
            DataAccess.addSqlParam("@DeviceIDrmmTool", ParameterDirection.Input, 50, MySqlDbType.VarChar, DeviceDescription);
            DataAccess.addSqlParam("@pageNumber", ParameterDirection.Input, 16, MySqlDbType.Int32, pageNumber);
            DataAccess.addSqlParam("@pageRecord", ParameterDirection.Input, 16, MySqlDbType.Int32, pageSize);
            DataAccess.addSqlParam("@sortColumnIndex", ParameterDirection.Input, 16, MySqlDbType.Int32, sortColumnIndex);
            DataAccess.addSqlParam("@searchOrder", ParameterDirection.Input, 4, MySqlDbType.VarChar, sortOrder);
           
        
            DataSet ds = DataAccess.ExecuteDataSet(GET_SEARCHRESULT, ref errorNum, ref errorDesc);

            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dtsearchresult = ds.Tables[0];
                if (dtsearchresult != null && dtsearchresult.Rows.Count > 0)
                {
                    totalRecords = Common.ConvertToInt(ds.Tables[1].Rows[0], "TotalRecords");

                    foreach (DataRow dr in dtsearchresult.Rows)
                    {
                        SearchResult searchresult = new SearchResult();
                        searchresult.CustomerName = Common.ConvertToString(dr, "CustomerName");
                        searchresult.ClientSiteName = Common.ConvertToString(dr, "ClientName");
                        searchresult.ClientSiteStatus = LookUpValue.GetSetUpStaus(Common.ConvertToInt(dr, "ClientStatus"));
                        searchresult.DeviceName = Common.ConvertToString(dr, "DeviceName");
                        searchresult.DeviceDesc = Common.ConvertToString(dr, "DeviceDesc");
                        searchresult.AccessPolicyID = Common.ConvertToInt(dr, "AccessPolicyID");
                        searchresult.AccessPolicy = LookUpValue.GetAccessPolicyType(Common.ConvertToInt(dr, "AccessPolicyType"));
                        searchresult.MaintenancePolicy = Common.ConvertToString(dr, "MaintenancePolicyName");
                        searchresult.MaintenancePolicyID = Common.ConvertToInt(dr, "MaintenancePolicyID");
                        searchresult.ClientSiteDeviceID = Common.ConvertToInt(dr, "ClientSiteDeviceID");
                        searchresult.PatchingPolicy = Common.ConvertToString(dr, "PatchingPolicyName");
                        searchresult.PatchingPolicyID = Common.ConvertToInt(dr, "PatchingPolicyID");
                        searchresult.AntiVirusPolicy = Common.ConvertToString(dr, "AntiVirusPolicyName");
                        searchresult.AntiVirusPolicyID = Common.ConvertToInt(dr, "AntiVirusPolicyID");
                        searchresult.BackUpPolicy = Common.ConvertToString(dr, "BackUpPolicyName");
                        searchresult.BackUpPolicyID = Common.ConvertToInt(dr, "BackUpPolicyID");
                        searchresult.RMMTool = Common.ConvertToString(dr, "RMMTool");
                        searchresult.RMMToolID = Common.ConvertToInt(dr, "RMMToolID");  

                        lstsearchresult.Add(searchresult);
                    }
                }
            }
            return lstsearchresult;
        }
    }
}