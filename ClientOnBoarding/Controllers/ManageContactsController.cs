using ClientOnBoarding.BAL;
using ClientOnBoarding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ClientOnBoarding.Controllers
{
    [AuthorizeNormalAttribute]
    public class ManageContactsController : Controller
    {
        #region--Declare Variable for paging
        private IList<tblCustomerContact> allProducts = new List<tblCustomerContact>();
        #endregion

        //Get        
        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
                return PartialView();
            else
                return View();

        }

        //Get        
        public ActionResult GetContacts(jQueryDataTableParamModel param)
        {
            int totalRecords = 0;

            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            var sortDirection = Request["sSortDir_0"]; // asc or desc

            BLContact contactBL = new BLContact();
            List<tblCustomerContact> lstCustomerContacts = contactBL.GetContacts(SessionHelper.UserSession.CustomerID, param.iDisplayStart, param.iDisplayLength,sortColumnIndex, sortDirection, param.sSearch, ref totalRecords);

            

            var result = from c in lstCustomerContacts
                       //  maintenancePolicy.WeekOfDays == null ? 0: maintenancePolicy.WeekOfDays.ID
                         let FirstPhoneNo = c.ExtNofirst == "" ? c.FirstPhoneNo + "" : c.FirstPhoneNo + " (" + c.ExtNofirst + ")"
                         let SecondPhoneNo = c.ExtNosecond == "" ? c.SecondPhoneNo + "" : c.SecondPhoneNo + " (" + c.ExtNosecond + ")"
                         select new[] { c.ContactName, c.ContactType.Name, c.Email, FirstPhoneNo, SecondPhoneNo, c.SMS, c.ContactID.ToString(), c.ExtNofirst.ToString(), c.ExtNosecond.ToString() };

            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = result
            }, JsonRequestBehavior.AllowGet);
        }

        //Get        
        public ActionResult Contact(int? contactID)
        {
            BindLookups();

            BLContact contactBL = new BLContact();
            tblCustomerContact customerContact = new tblCustomerContact();

            if (contactID != null && contactID.Value > 0)
                customerContact = contactBL.GetContact(contactID.Value);

            if (Request.IsAjaxRequest())

                return PartialView(customerContact);
            else
                return View(customerContact);

        }
        
        //Get
        public JsonResult DeleteContact(int? contactID)
        {
            BLContact contactBL = new BLContact();
        
          int? DeletionStatus=null;
          if (contactID.HasValue && contactID.Value > 0)
          {
              DeletionStatus = contactBL.DelContact(contactID.Value);
          }

          if (DeletionStatus.HasValue && DeletionStatus.Value == 0) {
              return Json(new { success = true,errDesc= "Contact deleted successfully !" }, JsonRequestBehavior.AllowGet);          
          };

          return Json(new { errors = true, errDesc = "Contact deletion failed !" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]        
        public JsonResult Contact(tblCustomerContact contactsModel)
        {
            try
            {
                BLContact contactBL = new BLContact();
                contactsModel.CustomerID = SessionHelper.UserSession.CustomerID;
                contactBL.SaveContact(contactsModel);
            }
            catch
            {
                ModelState.AddModelError("", "Please provide valid User Name/Password.");
                return Json(new { errors = KeyValue.GetErrorsFromModelState(ViewData) });
            }

            return Json(new { success = true });
        }

        #region -- Private Methods --

        private void BindLookups()
        {
            ViewBag.ContactType = LookUpData.GetContactType();
        }

        #endregion -- Private Methods --

    }
}
