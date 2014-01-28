using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClientOnBoarding.Models;
using System.Data;
using MySql.Data.MySqlClient;
using ClientOnBoarding;

namespace ClientOnBoarding.BAL
{
    public class BLContact
    {
        public const string GET_CONTACT = "spGetContact";
        public const string GET_CONTACTS = "spGetContacts";
        public const string SET_CONTACT = "spSetContact";
        public const string DEL_CONTACT = "spDelContact";

        public List<tblCustomerContact> GetContacts(int CustomerID, int pageNumber, int pageSize, int sortColumnIndex, string sortOrder, string searchText, ref int totalRecords)
        {
            int errorNum = 0;
            string errorDesc = "";
            DataAccess.resetParams();
            //DataAccess.addSqlParam("@contactname", ParameterDirection.Input, 50, MySqlDbType.VarChar, contactname);
            DataAccess.addSqlParam("@CustomerID", ParameterDirection.Input, 16, MySqlDbType.Int32, CustomerID);
            DataAccess.addSqlParam("@pageNumber", ParameterDirection.Input, 16, MySqlDbType.Int32, pageNumber);
            DataAccess.addSqlParam("@pageRecord", ParameterDirection.Input, 16, MySqlDbType.Int32, pageSize);
            DataAccess.addSqlParam("@sortColumnIndex", ParameterDirection.Input, 16, MySqlDbType.Int32, sortColumnIndex);
            DataAccess.addSqlParam("@searchOrder", ParameterDirection.Input, 4, MySqlDbType.VarChar, sortOrder);
            DataAccess.addSqlParam("@searchText", ParameterDirection.Input, 100, MySqlDbType.VarChar, searchText);

            DataSet ds = DataAccess.ExecuteDataSet(GET_CONTACTS, ref errorNum, ref errorDesc);
            tblCustomerContact contact = new tblCustomerContact();
            List<tblCustomerContact> contacts = new List<tblCustomerContact>();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dtContact = ds.Tables[0];
                DataTable dtTotalRecords = ds.Tables[1];
                totalRecords = Common.ConvertToInt(dtTotalRecords.Rows[0], "TotalRecords");
                if (dtContact != null && dtContact.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtContact.Rows)
                    {
                        contact = new tblCustomerContact();
                        contact.CustomerID = Common.ConvertToInt(dr, "CustomerID");
                        contact.ExtNofirst = Common.ConvertToString(dr, "ExtNofirst");
                        contact.ExtNosecond = Common.ConvertToString(dr, "ExtNosecond");
                        contact.ContactID = Common.ConvertToInt(dr, "ContactID");
                        contact.ContactName = Common.ConvertToString(dr, "ContactName");
                        contact.Email = Common.ConvertToString(dr, "Email");
                        contact.FirstPhoneNo = Common.ConvertToString(dr, "FirstPhoneNo");
                        contact.SecondPhoneNo = Common.ConvertToString(dr, "SecondPhoneNo");
                        contact.ExtNofirst = Common.ConvertToString(dr, "ExtNofirst");
                        contact.ExtNosecond = Common.ConvertToString(dr, "ExtNosecond"); 
                        contact.SMS = Common.ConvertToString(dr, "SMS");
                        contact.IsActive = Common.ConvertToInt(dr, "Isactive");
                        contact.ContactType = new ContactType();
                        contact.ContactType.ID = Common.ConvertToInt(dr, "ContactTypeID");
                        contact.ContactType.Name = LookUpValue.GetContactType(contact.ContactType.ID.Value);
                        contacts.Add(contact);
                    }
                }
            }
            return contacts;
        }

        public int SaveContact(tblCustomerContact Customercontact)
        {
            int errorNum = 0;
            string errorDesc = "";
            DataAccess.resetParams();
            DataAccess.addSqlParam("@oCustomerID", ParameterDirection.Input, 16, MySqlDbType.Int32, Customercontact.CustomerID);
            DataAccess.addSqlParam("@oContactID", ParameterDirection.Input, 16, MySqlDbType.Int32, Customercontact.ContactID);
            DataAccess.addSqlParam("@oContactName", ParameterDirection.Input, 100, MySqlDbType.VarChar, Customercontact.ContactName);
            DataAccess.addSqlParam("@oContactTypeID", ParameterDirection.Input, 16, MySqlDbType.Int32, Customercontact.ContactType.ID);
            DataAccess.addSqlParam("@oEmail", ParameterDirection.Input, 100, MySqlDbType.VarChar, Customercontact.Email);
            DataAccess.addSqlParam("@oSMS", ParameterDirection.Input, 150, MySqlDbType.VarChar, Customercontact.SMS);
            DataAccess.addSqlParam("@oExtNofirst", ParameterDirection.Input, 100, MySqlDbType.VarChar, Customercontact.ExtNofirst);
            DataAccess.addSqlParam("@oExtNosecond", ParameterDirection.Input, 150, MySqlDbType.VarChar, Customercontact.ExtNosecond);
            DataAccess.addSqlParam("@oIsActive", ParameterDirection.Input, 150, MySqlDbType.Int16, 1);
            
            DataAccess.addSqlParam("@oFirstPhoneNo", ParameterDirection.Input, 60, MySqlDbType.VarChar, Customercontact.FirstPhoneNo);
            DataAccess.addSqlParam("@oSecondPhoneNo", ParameterDirection.Input, 60, MySqlDbType.VarChar, Customercontact.SecondPhoneNo);
            DataAccess.addSqlParam("@Identity", ParameterDirection.InputOutput, 16, MySqlDbType.Int32);
            DataAccess.ExecuteNonQuery(SET_CONTACT, ref errorNum, ref errorDesc);

            return Common.ConvertToInt(DataAccess.getSQLParam("@Identity").ToString());
        }

        public tblCustomerContact GetContact(int ContactID)
        {
            int errorNum = 0;
            string errorDesc = "";
            DataAccess.resetParams();
            DataAccess.addSqlParam("@oContactID", ParameterDirection.Input, 16, MySqlDbType.Int32, ContactID);
            DataSet ds = DataAccess.ExecuteDataSet(GET_CONTACT, ref errorNum, ref errorDesc);
            tblCustomerContact contact = new tblCustomerContact();
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dtContact = ds.Tables[0];
                if (dtContact != null && dtContact.Rows.Count > 0)
                {
                    DataRow dr = dtContact.Rows[0];
                    contact.CustomerID = Common.ConvertToInt(dr, "CustomerID");
                    contact.ContactID = Common.ConvertToInt(dr, "ContactID");
                    contact.ContactName = Common.ConvertToString(dr, "ContactName");
                    contact.Email = Common.ConvertToString(dr, "Email");
                    contact.FirstPhoneNo = Common.ConvertToString(dr, "FirstPhoneNo");
                    contact.SecondPhoneNo = Common.ConvertToString(dr, "SecondPhoneNo");
                //    contact.IsActive = Common.ConvertToBool(dr, "Isactive");
                    contact.SMS = Common.ConvertToString(dr, "SMS");
                    contact.IsActive = Common.ConvertToInt(dr, "Isactive");
                    contact.ExtNofirst = Common.ConvertToString(dr, "ExtNofirst");
                    contact.ExtNosecond = Common.ConvertToString(dr, "ExtNosecond");                    
                    contact.ContactType = new ContactType();
                    contact.ContactType.ID = Common.ConvertToInt(dr, "ContactTypeID");
                    contact.ContactType.Name = LookUpValue.GetContactType(contact.ContactType.ID.Value);
                }
            }
            return contact;
        }
        public int DelContact(int ContactID)
        {
            int errNum = 0;
            string errDesc = "";
            DataAccess.resetParams();
            DataAccess.addSqlParam("@ContactID", ParameterDirection.Input, 16, MySqlDbType.Int32, ContactID);
            DataAccess.ExecuteNonQuery(DEL_CONTACT, ref errNum, ref errDesc);

            return errNum;
        }
    }
}