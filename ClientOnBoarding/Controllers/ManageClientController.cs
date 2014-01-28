using ClientOnBoarding.BAL;
using ClientOnBoarding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ClientOnBoarding.Controllers
{
    [AuthorizeNormalAttribute]
    public class ManageClientController : Controller
    {
        //Get        
        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
                return PartialView();
            else
                return View();
        }

        //Get        
        public ActionResult GetClients(jQueryDataTableParamModel param)
        {
            int totalRecords = 0;

            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            var sortDirection = Request["sSortDir_0"]; // asc or desc

            BLClientSite clientSiteBL = new BLClientSite();
            List<tblClientSite> lstCustomerClients = clientSiteBL.GetAllClientSite(SessionHelper.UserSession.CustomerID, param.iDisplayStart, param.iDisplayLength, sortColumnIndex, sortDirection, param.sSearch, ref totalRecords);


            var result = from c in lstCustomerClients
                         let ServiceTypeName = c.ServiceType.Name
                         let TimeZoneName = c.TimeZone.Name
                         let StatusName = c.Status.Name
                         select new[] { c.BusinessName, ServiceTypeName, TimeZoneName, StatusName, c.ClientID.ToString() };

            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = result
            }, JsonRequestBehavior.AllowGet);
        }

        //Get             
        public ActionResult Client(int? clientID)
        {
            BindLookups();

            BLClientSite clientBL = new BLClientSite();
            tblClientSite editclient = new tblClientSite();
            editclient = clientBL.GetClientSite(clientID.Value);

            if (Request.IsAjaxRequest())
                return PartialView(editclient);
            else
                return View(editclient);
        }

        [HttpPost]        
        public ActionResult Client(tblClientSite clientSite)
        {
            try
            {
                BLClientSite clientBL = new BLClientSite();
                clientSite.CustomerID = SessionHelper.UserSession.CustomerID;
                clientBL.SaveClientSite(clientSite);
            }
            catch
            {
                ModelState.AddModelError("", "Please provide valid User Name/Password.");
                return Json(new { errors = KeyValue.GetErrorsFromModelState(ViewData) });
            }

            return Json(new { success = true });
        }

        //Get
        public JsonResult deleteClient(int? clientid)
        {
            BLClientSite clientBL = new BLClientSite();
            StringBuilder DeletionStatus = null;
            if (clientid.HasValue  && clientid.Value > 0)
            {
                DeletionStatus = clientBL.Deleteclientinfo(clientid.Value);
            }
            if (DeletionStatus.Length == 0)
            {
                return Json(new { success = true, resultmsg = "Client deleted successfully" }, JsonRequestBehavior.AllowGet);

            };

            return Json(new { success = false, resultmsg = DeletionStatus.ToString() }, JsonRequestBehavior.AllowGet);
        }

        #region -- Private Methods --

        private void BindLookups()
        {
            ViewBag.ServiceTypes = LookUpData.GetServiceType();
            ViewBag.TimeZones = LookUpData.GetTimeZone();
            ViewBag.Staus = LookUpData.GetSetupUpStatus();
        }

        #endregion -- Private Methods --
    }
}
