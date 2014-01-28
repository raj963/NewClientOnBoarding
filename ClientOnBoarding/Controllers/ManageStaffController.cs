using ClientOnBoarding.BAL;
using ClientOnBoarding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ClientOnBoarding.Controllers
{
    [AuthorizeSuperAdminAttribute]
    public class ManageStaffController : Controller
    {        
        // GET:        
        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
                return PartialView();
            else
                return View();
        }

        //Get        
        public ActionResult Staff(int? CustomerID)
        {
            BLManageStaff manageStaffBL = new BLManageStaff();
            tblCustomerDetails customer = manageStaffBL.GetStaff(CustomerID.Value);
            customer.RoleID = UserRole.Staff;
            ViewBag.TimeZones = LookUpData.GetTimeZone();
            ViewBag.NOCCommunicationBy = LookUpData.GetNOCCommunicationBy();

            if (Request.IsAjaxRequest())
                return PartialView(customer);
            else
                return View(customer);
        }
                
        [HttpPost]
        public ActionResult Staff(tblCustomerDetails customerdetail)
        {
            try
            {
                BLManageStaff staffBL = new BLManageStaff();
                staffBL.SetUsers(customerdetail);
            }
            catch
            {
                ModelState.AddModelError("", "Error occurred durung Staff Information save.");
                return Json(new { errors = KeyValue.GetErrorsFromModelState(ViewData) });
            }

            return Json(new { success = true, CustomerID = SessionHelper.UserSession.CustomerID });
        }

        //Get
        public ActionResult GetStaffUsers(jQueryDataTableParamModel param)
        {
            int totalRecords = 0;
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            var sortDirection = Request["sSortDir_0"]; // asc or desc

            BLManageStaff manageStaffBL = new BLManageStaff();
            List<tblCustomerDetails> lstCustomerDetail = manageStaffBL.GetStaff(UserRole.Staff, param.iDisplayStart, param.iDisplayLength, sortColumnIndex, sortDirection, param.sSearch, ref totalRecords);

            var result = from c in lstCustomerDetail
                         select new[] { c.CustomerName, 
                                        c.EmailAddress, 
                                        c.CustomerID.ToString() };

            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = result
            }, JsonRequestBehavior.AllowGet);
        }

        //Get        
        public ActionResult DeleteStaff(int? CustomerID)
        {
            BLManageStaff customerBL = new BLManageStaff();
            customerBL.GetDeleteCustomer(CustomerID.Value);
            return Json(new { success = true, resultmsg = "Staff deleted successfully" }, JsonRequestBehavior.AllowGet);
        }
    }
}
