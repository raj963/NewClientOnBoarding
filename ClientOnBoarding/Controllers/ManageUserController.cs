using ClientOnBoarding.BAL;
using ClientOnBoarding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ClientOnBoarding.Controllers
{
    [AuthorizeAdminAttribute]
    public class ManageUserController : Controller
    {
        // Get
        public ActionResult NormalUser()
        {
            if (Request.IsAjaxRequest())
                return PartialView();
            else
                return View();
        }

        public JsonResult JsonCheckEmail(tblCustomerDetails input)
        {
            BLManageUser manageuserBL = new BLManageUser();

            var  result = manageuserBL.Emailexists(input.EmailAddress);
            if (result == 1)
                return Json( false,JsonRequestBehavior.AllowGet);

            return Json(true,JsonRequestBehavior.AllowGet); ;
        }



        //Get
        [AuthorizeSuperAdminAttribute]
        public ActionResult AdminUser()
        {
            if (Request.IsAjaxRequest())
                return PartialView();
            else
                return View();
        }

        //Get
        public ActionResult User(int? CustomerID, int? RoleID)
        {
            BLCustomer customerBL = new BLCustomer();
            tblCustomerDetails customer = customerBL.GetCustomer(CustomerID.Value);
            if (CustomerID.HasValue && CustomerID.Value > 0)
                customer.RoleID = RoleID.HasValue ? RoleID.Value : 0;
            ViewBag.TimeZones = LookUpData.GetTimeZoneCI();
            ViewBag.NOCCommunicationBy = LookUpData.GetNOCCommunicationBy();

            if (Request.IsAjaxRequest())
                return PartialView(customer);
            else
                return View(customer);
        }
        //Get
        public ActionResult DeleteUser(int? CustomerID)
        {
            BLCustomer customerBL = new BLCustomer();
            StringBuilder str = customerBL.GetDeleteCustomer(CustomerID.Value);
            if (str.Length > 0)
                return Json(new { success = false, resultmsg = str.ToString() }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { success = true, resultmsg = "User deleted successfully" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult User(tblCustomerDetails customerdetail)
        {
            try
            {
                string submitButtonID = Request["SubmitButtonID"].ToString();
                BLManageUser customerBL = new BLManageUser();
                customerBL.SetUsers(customerdetail);
                if (submitButtonID == "SbmtEmail")
                    Common.SendMail(customerdetail.EmailAddress, "Please login in flexis system with below credentials", "User Name: " + customerdetail.EmailAddress + ", " + "Password: " + customerdetail.Password);
            }
            catch
            {
                ModelState.AddModelError("", "Error occurred durung Customer Information save.");
                return Json(new { errors = KeyValue.GetErrorsFromModelState(ViewData) });
            }

            return Json(new { success = true, CustomerID = SessionHelper.UserSession.CustomerID, Tab = "CI" });
        }

        //Get
        public ActionResult Email(int CustomerID)
        {
            try
            {
                BLCustomer customerBL = new BLCustomer();
                tblCustomerDetails customer = customerBL.GetCustomer(CustomerID);
                Common.SendMail(customer.EmailAddress, "Please login in flexis system with these credentials !", "User Name: " + customer.EmailAddress + "<br>" + "Password: " + customer.Password);
            }
            catch
            {
                ModelState.AddModelError("", "Error occurred durung Customer Information save.");
                return Json(new { errors = KeyValue.GetErrorsFromModelState(ViewData) });
            }

            return Json(new { success = true, MessageDetail = "User's credentials successfully sent to his/her mail address." }, JsonRequestBehavior.AllowGet);
        }

        //Get
        [AuthorizeSuperAdminAttribute]
        public ActionResult GetAdminUsers(jQueryDataTableParamModel param)
        {
            return GetUsers(UserRole.Admin, param);
        }

        //Get
        public ActionResult GetNormalUsers(jQueryDataTableParamModel param)
        {
            return GetUsers(UserRole.NormalUser, param);
        }

        //Get
        private ActionResult GetUsers(int RoleID, jQueryDataTableParamModel param)
        {
            int totalRecords = 0;
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            var sortDirection = Request["sSortDir_0"]; // asc or desc

            BLManageUser manageUserBL = new BLManageUser();
            List<tblCustomerDetails> lstCustomerDetail = manageUserBL.GetUsers(RoleID, param.iDisplayStart, param.iDisplayLength, sortColumnIndex, sortDirection, param.sSearch, ref totalRecords);

            var result = from c in lstCustomerDetail
                         let TimeZoneName = c.TimeZone.Name
                         let NOCCommunicationName = c.NOCCommunication.Name
                         select new[] { c.CustomerName, 
                                        c.EmailAddress, 
                                        TimeZoneName, 
                                        c.CustomerContactName, 
                                        NOCCommunicationName, 
                                        c.CustomerID.ToString() };

            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = result
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
