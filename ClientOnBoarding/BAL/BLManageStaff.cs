using ClientOnBoarding.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;


namespace ClientOnBoarding.BAL
{
    public class BLManageStaff
    {
        public const string SET_STAFF = "spSetStaff";
        public const string GET_STAFF = "spGetUsers";
        public const string GET_STAFFID = "spGetCustomer";
        public const string DEL_STAFF = "spDelStaffInformation";

        public List<tblCustomerDetails> GetStaff(int roleID, int pageNumber, int pageSize, int sortColumnIndex, string sortOrder, string searchText, ref int totalRecords)
        {
            List<tblCustomerDetails> lstCustomerDetail = new List<tblCustomerDetails>();

            int errorNum = 0;
            string errorDesc = "";
            DataAccess.resetParams();

            DataAccess.addSqlParam("@pageNumber", ParameterDirection.Input, 16, MySqlDbType.Int32, pageNumber);
            DataAccess.addSqlParam("@pageRecord", ParameterDirection.Input, 16, MySqlDbType.Int32, pageSize);
            DataAccess.addSqlParam("@sortColumnIndex", ParameterDirection.Input, 16, MySqlDbType.Int32, sortColumnIndex);
            DataAccess.addSqlParam("@searchOrder", ParameterDirection.Input, 4, MySqlDbType.VarChar, sortOrder);
            DataAccess.addSqlParam("@searchText", ParameterDirection.Input, 100, MySqlDbType.VarChar, searchText);
            DataAccess.addSqlParam("@roleID", ParameterDirection.Input, 16, MySqlDbType.Int32, roleID);

            DataSet ds = DataAccess.ExecuteDataSet(GET_STAFF, ref errorNum, ref errorDesc);
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
                        customer.Password = Common.ConvertToString(dr, "Password");
                        lstCustomerDetail.Add(customer);
                    }
                }
            }

            return lstCustomerDetail;
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
            DataAccess.addSqlParam("@RoleID", ParameterDirection.Input, 16, MySqlDbType.Int32, 4);
            DataAccess.addSqlParam("@Isactive", ParameterDirection.InputOutput, 16, MySqlDbType.Int32,1);
            DataAccess.addSqlParam("@Identity", ParameterDirection.InputOutput, 16, MySqlDbType.Int32);
            DataAccess.ExecuteNonQuery(SET_STAFF, ref errorNum, ref errorDesc);

            return Common.ConvertToInt(DataAccess.getSQLParam("@Identity").ToString());
        }

        public tblCustomerDetails GetStaff(int CustomerID)
        {
            int errorNum = 0;
            string errorDesc = "";
            DataAccess.resetParams();
            DataAccess.addSqlParam("@CustomerID", ParameterDirection.Input, 16, MySqlDbType.Int32, CustomerID);
            DataSet ds = DataAccess.ExecuteDataSet(GET_STAFFID, ref errorNum, ref errorDesc);

            tblCustomerDetails customerDetails = new tblCustomerDetails();
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dtCutomer = ds.Tables[0];
                if (dtCutomer != null && dtCutomer.Rows.Count > 0)
                {
                    DataRow dr = dtCutomer.Rows[0];
                    customerDetails.CustomerID = Common.ConvertToInt(dr, "CustomerID");
                    customerDetails.CustomerName = Common.ConvertToString(dr, "CustomerName");
                    customerDetails.EmailAddress = Common.ConvertToString(dr, "EmailAddress");
                    customerDetails.Password = Common.ConvertToString(dr, "Password");
                }
            }
            return customerDetails;
        }
        public void GetDeleteCustomer(int CustomerID)
        {
            int errorNum = 0;
            string errorDesc = "";
            DataAccess.resetParams();
            DataAccess.addSqlParam("@CustomerID", ParameterDirection.Input, 16, MySqlDbType.Int32, CustomerID);
            DataAccess.ExecuteNonQuery(DEL_STAFF, ref errorNum, ref errorDesc);
        }

    }
}