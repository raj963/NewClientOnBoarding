using System.Data;
using ClientOnBoarding.BAL;
using ClientOnBoarding.Models;
using System.Collections.Generic;
using MySql.Data.MySqlClient;


namespace ClientOnBoarding
{
    public class AccountBL
    {
        #region--Stored Procedures

        public const string GET_CHECK_LOGIN = "spGetCheckLogin";

        #endregion

        public ContactDetails CheckLogin(string emailaddress, string password)
        {
            int errorNum = 0;
            string errorDesc = "";

            DataAccess.resetParams();
            DataAccess.addSqlParam("@EmailAddress", ParameterDirection.Input, 50, MySqlDbType.VarChar, emailaddress);
            DataAccess.addSqlParam("@Password", ParameterDirection.Input, 50, MySqlDbType.VarChar, password);
            DataAccess.addSqlParam("@CustomerID", ParameterDirection.InputOutput, 16, MySqlDbType.Int32);
            DataSet ds = DataAccess.ExecuteDataSet(GET_CHECK_LOGIN, ref errorNum, ref errorDesc);

            ContactDetails cd = new ContactDetails();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                cd.CustomerID = Common.ConvertToInt(ds.Tables[0].Rows[0], "CustomerID");
                cd.Name = Common.ConvertToString(ds.Tables[0].Rows[0], "CustomerName");
                cd.IsAdmin = Common.ConvertToInt(ds.Tables[0].Rows[0], "IsAdmin") == 1 ? true: false;
                cd.RoleID = Common.ConvertToInt(ds.Tables[0].Rows[0], "RoleID");
            }

            return cd;
        }
    }
}
