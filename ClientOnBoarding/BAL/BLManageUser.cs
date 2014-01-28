using ClientOnBoarding.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ClientOnBoarding.BAL
{
    public class BLManageUser
    {
        public const string GET_USERS = "spGetUsers";
        public const string spGetEmails = "spGetCheckemail";
        public const string SET_USERS = "spSetUsers";

        public List<tblCustomerDetails> GetUsers(int roleID, int pageNumber, int pageSize, int sortColumnIndex, string sortOrder, string searchText, ref int totalRecords)
        {
            List<tblCustomerDetails> lstCustomerDetail = new List<tblCustomerDetails>();

            int errNum = 0;
            string errDesc = "";
           
            DataAccess.resetParams();
                   
            DataAccess.addSqlParam("@pageNumber", ParameterDirection.Input, 16, MySqlDbType.Int32, pageNumber);
            DataAccess.addSqlParam("@pageRecord", ParameterDirection.Input, 16, MySqlDbType.Int32, pageSize);
            DataAccess.addSqlParam("@sortColumnIndex", ParameterDirection.Input, 16, MySqlDbType.Int32, sortColumnIndex);
            DataAccess.addSqlParam("@searchOrder", ParameterDirection.Input, 4, MySqlDbType.VarChar, sortOrder);
            DataAccess.addSqlParam("@searchText", ParameterDirection.Input, 100, MySqlDbType.VarChar, searchText);
            DataAccess.addSqlParam("@roleID", ParameterDirection.Input, 16, MySqlDbType.Int32, roleID);

            DataSet ds = DataAccess.ExecuteDataSet(GET_USERS, ref errNum, ref errDesc);
            tblCustomerDetails customer = new tblCustomerDetails();            

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dtContact = ds.Tables[0];
                totalRecords = Common.ConvertToInt(ds.Tables[1].Rows[0], "TotalRecords");
                if (dtContact != null && dtContact.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtContact.Rows)
                    {
                        customer = new tblCustomerDetails();
                        customer.CustomerID = Common.ConvertToInt(dr, "CustomerID");
                        customer.CustomerName = Common.ConvertToString(dr, "CustomerName");
                        customer.EmailAddress = Common.ConvertToString(dr, "EmailAddress");
                        customer.CustomerContactName = Common.ConvertToString(dr, "CustomerContactName");
                        customer.Password = Common.ConvertToString(dr, "Password");
                        customer.TimeZone = new TimeZoneFX();
                        customer.TimeZone.ID = Common.ConvertToInt(dr, "TimeZone");
                        customer.TimeZone.Name = LookUpValue.GetTimeZone(customer.TimeZone.ID.Value);
                        customer.NOCCommunication = new NOCCommunicationBy();
                        customer.NOCCommunication.ID = Common.ConvertToInt(dr, "NOCCommunicationBy");
                        customer.NOCCommunication.Name = LookUpValue.GetNOCCommunicationBy(customer.NOCCommunication.ID.Value);
                        lstCustomerDetail.Add(customer);
                    }
                }
            }

            return lstCustomerDetail;
        }
        public int Emailexists(string emailID)
        {
            int errorNum = 0;
            string errorDesc = "";

            DataAccess.resetParams();

            DataAccess.addSqlParam("@Email", ParameterDirection.Input, 100, MySqlDbType.VarChar, emailID);
            DataAccess.addSqlParam("@isEmailExist", ParameterDirection.Output, 1, MySqlDbType.Int16);
            DataSet ds = DataAccess.ExecuteDataSet(spGetEmails, ref errorNum, ref errorDesc);
          //  DataAccess.ExecuteNonQuery(spGetEmails, ref errorNum, ref errorDesc);

            return Common.ConvertToInt(DataAccess.getSQLParam("@isEmailExist").ToString());
        }

        public int SetUsers(tblCustomerDetails CustomerDetails)
        {
            int errorNum = 0;
            string errorDesc = "";
            DataAccess.resetParams();
            DataAccess.addSqlParam("@CustomerID", ParameterDirection.Input, 16, MySqlDbType.Int32, CustomerDetails.CustomerID);
            DataAccess.addSqlParam("@CustomerName", ParameterDirection.Input, 100, MySqlDbType.VarChar, CustomerDetails.CustomerName);
            DataAccess.addSqlParam("@EmailAddress", ParameterDirection.Input, 150, MySqlDbType.VarChar, CustomerDetails.EmailAddress);
            DataAccess.addSqlParam("@Password", ParameterDirection.Input, 150, MySqlDbType.VarChar, CustomerDetails.Password);
            DataAccess.addSqlParam("@CustomerContactName", ParameterDirection.Input, 100, MySqlDbType.VarChar, CustomerDetails.CustomerContactName);
            DataAccess.addSqlParam("@TimeZone", ParameterDirection.Input, 16, MySqlDbType.Int32, CustomerDetails.TimeZone == null ? 0 : CustomerDetails.TimeZone.ID);
            DataAccess.addSqlParam("@NOCCommunicationBy", ParameterDirection.Input, 16, MySqlDbType.Int32, CustomerDetails.NOCCommunication == null ? 0 : CustomerDetails.NOCCommunication.ID);
            DataAccess.addSqlParam("@RoleID", ParameterDirection.Input, 16, MySqlDbType.Int32, CustomerDetails.RoleID);
            DataAccess.addSqlParam("@Isactive", ParameterDirection.InputOutput, 16, MySqlDbType.Int16,1);
            DataAccess.addSqlParam("@Identity", ParameterDirection.InputOutput, 16, MySqlDbType.Int32);
            DataAccess.ExecuteNonQuery(SET_USERS, ref errorNum, ref errorDesc);

            return Common.ConvertToInt(DataAccess.getSQLParam("@Identity").ToString());
        }
       
    }
}