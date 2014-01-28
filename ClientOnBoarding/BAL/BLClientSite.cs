using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClientOnBoarding.Models;
using System.Data;
using MySql.Data.MySqlClient;
using ClientOnBoarding;
using System.Text;

namespace ClientOnBoarding.BAL
{
    public class BLClientSite
    {
        public const string GET_CLIENT_SITE = "spGetClientSite";
        public const string Get_CustomerClient = "spGetCustomerClient";
        public const string SET_CLIENT_SITE = "spSetClientSite";
        public const string GET_ALL_CLIENT_SITE = "spGetAllClientSite";
        public const string DELETE_CLIENT = "spDelClientsite";



        public int SaveClientSite(tblClientSite clientSite)
        {
            int errorNum = 0;
            string errorDesc = "";

            DataAccess.resetParams();
            DataAccess.addSqlParam("@oClientID", ParameterDirection.Input, 16, MySqlDbType.Int32, clientSite.ClientID);
            DataAccess.addSqlParam("@CustomerID", ParameterDirection.Input, 16, MySqlDbType.Int32, clientSite.CustomerID);
            DataAccess.addSqlParam("@BusinessName", ParameterDirection.Input, 100, MySqlDbType.VarChar, clientSite.BusinessName);
            DataAccess.addSqlParam("@ServiceType", ParameterDirection.Input, 16, MySqlDbType.Int32, clientSite.ServiceType.ID);
            DataAccess.addSqlParam("@ClientStatus", ParameterDirection.Input, 16, MySqlDbType.Int32, clientSite.Status.ID);
            DataAccess.addSqlParam("@TimeZone", ParameterDirection.Input, 16, MySqlDbType.Int32, clientSite.TimeZone.ID);
            DataAccess.addSqlParam("@Isactive", ParameterDirection.Input, 16, MySqlDbType.Int16,1);
            DataAccess.addSqlParam("@Identity", ParameterDirection.InputOutput, 16, MySqlDbType.Int32);
            DataAccess.ExecuteNonQuery(SET_CLIENT_SITE, ref errorNum, ref errorDesc);

            return Common.ConvertToInt(DataAccess.getSQLParam("@Identity").ToString());
        }

        /// <summary>
        /// Use Ciient ID in this function
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public tblClientSite GetClientSite(int ClientID)
        {
            int errorNum = 0;
            string errorDesc = "";

            DataAccess.resetParams();
            DataAccess.addSqlParam("@ClientID", ParameterDirection.Input, 16, MySqlDbType.Int32, ClientID);
            DataSet ds = DataAccess.ExecuteDataSet(GET_CLIENT_SITE, ref errorNum, ref errorDesc);
            tblClientSite Clientsite = new tblClientSite();
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dtClientsite = ds.Tables[0];
                if (dtClientsite != null && dtClientsite.Rows.Count > 0)
                {
                    DataRow dr = dtClientsite.Rows[0];
                    Clientsite.ClientID = Common.ConvertToInt(dr, "ClientID");
                    Clientsite.CustomerID = Common.ConvertToInt(dr, "CustomerID");
                    Clientsite.IsActive = Common.ConvertToInt(dr, "Isactive");
                    Clientsite.BusinessName = Common.ConvertToString(dr, "BusinessName");
                    Clientsite.ServiceType = new ServiceType();
                    Clientsite.ServiceType.ID = Common.ConvertToInt(dr, "ServiceType");
                    Clientsite.ServiceType.Name = LookUpValue.GetServiceType(Clientsite.ServiceType.ID.Value);
                    Clientsite.Status = new SetUpStaus();
                    Clientsite.Status.ID = Common.ConvertToInt(dr, "Status");
                    Clientsite.Status.Name = LookUpValue.GetSetUpStaus(Clientsite.Status.ID.Value);
                    Clientsite.TimeZone = new TimeZoneFX();
                    Clientsite.TimeZone.ID = Common.ConvertToInt(dr, "TimeZone");
                    Clientsite.TimeZone.Name = LookUpValue.GetTimeZone(Clientsite.TimeZone.ID.Value);
                }
            }
            return Clientsite;
        }

        /// Get All Client Site - CustomerID
        /// 
        public List<tblClientSite> GetAllClientSite(int CustomerID, int pageNumber, int pageSize, int sortColumnIndex, string sortOrder, string searchText, ref int totalRecords)
        {
            List<tblClientSite> lstClientSite = new List<tblClientSite>();

            int errorNum = 0;
            string errorDesc = "";
            DataAccess.resetParams();
            DataAccess.addSqlParam("@CustomerID", ParameterDirection.Input, 16, MySqlDbType.Int32, CustomerID);
            DataAccess.addSqlParam("@pageNumber", ParameterDirection.Input, 16, MySqlDbType.Int32, pageNumber);
            DataAccess.addSqlParam("@pageRecord", ParameterDirection.Input, 16, MySqlDbType.Int32, pageSize);
            DataAccess.addSqlParam("@sortColumnIndex", ParameterDirection.Input, 16, MySqlDbType.Int32, sortColumnIndex);
            DataAccess.addSqlParam("@searchOrder", ParameterDirection.Input, 4, MySqlDbType.VarChar, sortOrder);
            DataAccess.addSqlParam("@searchText", ParameterDirection.Input, 100, MySqlDbType.VarChar, searchText);

            DataSet ds = DataAccess.ExecuteDataSet(GET_ALL_CLIENT_SITE, ref errorNum, ref errorDesc);
            tblClientSite Clientsite = new tblClientSite();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dtClientsite = ds.Tables[0];
                DataTable dtTotalRecords = ds.Tables[1];
                totalRecords = Common.ConvertToInt(dtTotalRecords.Rows[0], "TotalRecords");
                if (dtClientsite != null && dtClientsite.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtClientsite.Rows)
                    {
                        Clientsite = new tblClientSite();
                        Clientsite.ClientID = Common.ConvertToInt(dr, "ClientID");
                        Clientsite.CustomerID = Common.ConvertToInt(dr, "CustomerID");
                        Clientsite.IsActive = Common.ConvertToInt(dr, "Isactive");
                        Clientsite.BusinessName = Common.ConvertToString(dr, "BusinessName");
                        Clientsite.ServiceType = new ServiceType();
                        Clientsite.ServiceType.ID = Common.ConvertToInt(dr, "ServiceType");
                        Clientsite.ServiceType.Name = LookUpValue.GetServiceType(Clientsite.ServiceType.ID.Value);
                        Clientsite.Status = new SetUpStaus();
                        Clientsite.Status.ID = Common.ConvertToInt(dr, "Status");
                        Clientsite.Status.Name = LookUpValue.GetSetUpStaus(Clientsite.Status.ID.Value);
                        Clientsite.TimeZone = new TimeZoneFX();
                        Clientsite.TimeZone.ID = Common.ConvertToInt(dr, "TimeZone");
                        Clientsite.TimeZone.Name = LookUpValue.GetTimeZone(Clientsite.TimeZone.ID.Value);
                        lstClientSite.Add(Clientsite);
                    }
                }
            }
            return lstClientSite;
        }
        public List<tblClientSite> GetCustomerClient(int CustomerID)
        {
            List<tblClientSite> lstClientSite = new List<tblClientSite>();

            int errorNum = 0;
            string errorDesc = "";
            DataAccess.resetParams();
            DataAccess.addSqlParam("@CustomerID", ParameterDirection.Input, 16, MySqlDbType.Int32, CustomerID);

            DataSet ds = DataAccess.ExecuteDataSet(Get_CustomerClient, ref errorNum, ref errorDesc);
            tblClientSite Clientsite = new tblClientSite();
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dtClientsite = ds.Tables[0];

                if (dtClientsite != null && dtClientsite.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtClientsite.Rows)
                    {
                        Clientsite = new tblClientSite();
                        Clientsite.ClientID = Common.ConvertToInt(dr, "ClientID");
                        Clientsite.CustomerID = Common.ConvertToInt(dr, "CustomerID");
                        Clientsite.BusinessName = Common.ConvertToString(dr, "BusinessName");
                        Clientsite.ServiceType = new ServiceType();
                        Clientsite.ServiceType.ID = Common.ConvertToInt(dr, "ServiceType");
                        Clientsite.ServiceType.Name = LookUpValue.GetServiceType(Clientsite.ServiceType.ID.Value);
                        Clientsite.Status = new SetUpStaus();
                        Clientsite.Status.ID = Common.ConvertToInt(dr, "Status");
                        Clientsite.Status.Name = LookUpValue.GetSetUpStaus(Clientsite.Status.ID.Value);
                        Clientsite.TimeZone = new TimeZoneFX();
                        Clientsite.TimeZone.ID = Common.ConvertToInt(dr, "TimeZone");
                        Clientsite.TimeZone.Name = LookUpValue.GetTimeZone(Clientsite.TimeZone.ID.Value);
                        lstClientSite.Add(Clientsite);
                    }
                }
            }
            return lstClientSite;
        }

        #region--Delete client
        public StringBuilder Deleteclientinfo(int clientid)
        {
            int errorNum = 0;
            string errorDesc = "";
            StringBuilder str = new StringBuilder();
            StringBuilder strResult = new StringBuilder();
            DataAccess.resetParams();
            DataAccess.addSqlParam("@ClientID", ParameterDirection.Input, 16, MySqlDbType.Int32, clientid);
            DataSet ds = DataAccess.ExecuteDataSet(DELETE_CLIENT, ref errorNum, ref errorDesc);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dtCutomer = ds.Tables[0];

                if (dtCutomer != null && dtCutomer.Rows.Count > 0)
                {
                    bool isResult = false;
                    DataRow[] dr = dtCutomer.Select("CKey = 'tblclientsite'");
                    string UserName = Common.ConvertToString(dr[0], "Value");
                    
                    //Step start to build string
                    //Step 1: Create string for clientsitedevice                    
                    dr = dtCutomer.Select("CKey = 'tblclientsitedevice'");
                    if (dr != null && dr.Length > 1)
                    {
                        isResult = true;
                        //Multi Record message
                        str.Append("<li>");
                        str.Append("Client <b> " + UserName + "</b> belongs to <b>" + dr.Length.ToString() + "</b> devices please delete them and try again to delete the client.");
                        str.Append("</li>");
                    }
                    else if (dr.Length == 1)
                    {
                        isResult = true;
                        //Single record message
                        str.Append("<li>");
                        str.Append("Client <b>" + UserName + "</b> belongs to devices <b>" + Common.ConvertToString(dr[0], "Value") + "</b>. Please delete that devices and then try again to delete the Client.");
                        str.Append("</li>");
                    }                    

                    if (isResult)
                    {
                        strResult.Append("<ul>");
                        strResult.Append(str.ToString());
                        strResult.Append("</ul>");
                    }
                }
            }
            return strResult; ;
        }

        #endregion
    }
}
