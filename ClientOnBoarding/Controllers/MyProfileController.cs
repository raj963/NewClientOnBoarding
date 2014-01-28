using ClientOnBoarding.BAL;
using ClientOnBoarding.Models;
using System.Web;
using System.Web.Mvc;


namespace ClientOnBoarding.Controllers
{
    [AuthorizeAdminAndStaffAttribute]
    public class MyProfileController : Controller
    {
        //Get
        public ActionResult User()
        {
            BLCustomer customerBL = new BLCustomer();
            tblCustomerDetails customer = customerBL.GetCustomer(SessionHelper.UserSession.CustomerID);

            ViewBag.TimeZones = LookUpData.GetTimeZoneCI();
            ViewBag.NOCCommunicationBy = LookUpData.GetNOCCommunicationBy();

            if (Request.IsAjaxRequest())
                return PartialView( customer);
            else
                return View(customer);
        }

        [HttpPost]
        public ActionResult User(tblCustomerDetails customerdetail)
        {
            try
            {
                BLManageUser customerBL = new BLManageUser();
                customerBL.SetUsers(customerdetail);
                SessionHelper.UserSession.Name = customerdetail.CustomerName;
            }
            catch
            {
                ModelState.AddModelError("", "Error occurred while updating profile update.");
                return Json(new { errors = KeyValue.GetErrorsFromModelState(ViewData) });
            }

           
            UrlHelper u = new UrlHelper(HttpContext.Request.RequestContext);
            string url = u.Action("Index", "Search", null);

            return Json(new { success = true, redirect = url });
        }
    }
}
