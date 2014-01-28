using ClientOnBoarding.BAL;
using ClientOnBoarding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ClientOnBoarding.Controllers
{
    [AuthorizeNormalAttribute]
    public class ManageClientDevicesController : Controller
    {
        //Get        
        public ActionResult Index()
        {
            BLClientSite clientSiteBL = new BLClientSite();
            List<tblClientSite> lstClientSite = new List<tblClientSite>();
            lstClientSite.Add(new tblClientSite() { ClientID = 0, BusinessName = " -- Select -- " });
            lstClientSite.AddRange(clientSiteBL.GetCustomerClient(SessionHelper.UserSession.CustomerID));

            ViewBag.CustomerClientSites = lstClientSite;

            tblClientSite clientSite = new tblClientSite();
            clientSite.TimeZone = new TimeZoneFX();
            clientSite.Status = new SetUpStaus();

            if (Request.IsAjaxRequest())
                return PartialView(clientSite);
            else
                return View(clientSite);
        }

        //Get        
        public ActionResult DeviceGridRecord(jQueryDataTableParamModel param)
        {
            int totalRecords = 0;

            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            var sortDirection = Request["sSortDir_0"]; // asc or desc

            int clientID = Common.ConvertToInt(Request["ClientID"].ToString());

            BLClientSiteDevice clientSiteDeviceBL = new BLClientSiteDevice();
            List<tblClientSiteDevice> lstClientSiteDevices = clientSiteDeviceBL.GetAllClientSiteDevice(clientID, param.iDisplayStart, param.iDisplayLength, sortColumnIndex, sortDirection, param.sSearch, ref totalRecords);

            var result = from c in lstClientSiteDevices
                         let DeviceTypeName = c.DeviceType.Name
                         let AccessPolicyName = c.IsAccessPolicy.Name
                         let MaintenancePolicyName = c.IsMaintenancePolicy.Name
                         let PatchingPolicyName = c.IsPatchingPolicy.Name
                         let AntiVirusPolicyName = c.IsAntiVirus.Name
                         let BackupPolicyName = c.IsBackUpPolicy.Name
                         let RMMToolName = c.IsRMMTool.Name                        
                         select new[] { c.DeviceIDFromRMMTool, DeviceTypeName, c.DeviceDescription, AccessPolicyName, MaintenancePolicyName, 
                                       PatchingPolicyName , RMMToolName,c.MiscInfo,c.DeviceID.ToString() };

            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = result
            }, JsonRequestBehavior.AllowGet);
        }

        //Get        
        public ActionResult DeviceGrid(int? ClientID, int? page)
        {  
            if (Request.IsAjaxRequest())
                return PartialView();
            else
                return View();
        }

        //Get        
        public ActionResult Device(int? DeviceID, int? ClientID)
        {
            BindLookups();

            List<tblMaintenancePolicy> Manitenancepoly = new List<tblMaintenancePolicy>();
            List<tblAccessPolicy> Accesspoly = new List<tblAccessPolicy>();
            List<tblAntiVirusPolicy> Antiviruspoly = new List<tblAntiVirusPolicy>();
            List<tblBackUpPolicy> Backuppoly = new List<tblBackUpPolicy>();
            List<tblPatchingPolicy> PatchingPoly = new List<tblPatchingPolicy>();
            List<tbltoolInfomation> ToolInfoPoly = new List<tbltoolInfomation>();

            BLClientSiteDevice clientSiteDeviceBL = new BLClientSiteDevice();

            clientSiteDeviceBL.FillDropDownList(ref Manitenancepoly, ref Accesspoly, ref Antiviruspoly, ref Backuppoly, ref PatchingPoly, ref ToolInfoPoly);
            tblClientSiteDevice clientSiteDevice = clientSiteDeviceBL.GetClientSiteDevice(DeviceID.Value,SessionHelper.UserSession.CustomerID);
            clientSiteDevice.ClientID = ClientID.Value;

            ViewBag.Maintenancepoly = Manitenancepoly;
            ViewBag.Accesspoly = Accesspoly;
            ViewBag.Antiviruspoly = Antiviruspoly;
            ViewBag.Backuppoly = Backuppoly;
            ViewBag.PatchingPoly = PatchingPoly;
            ViewBag.ToolInfoPoly = ToolInfoPoly;
            if (Request.IsAjaxRequest())
                return PartialView(clientSiteDevice);
            else
                return View(clientSiteDevice);
        }

        //Get        
        public JsonResult JsonClient(int? ClientID)
        {
            BLClientSite clientBL = new BLClientSite();
            tblClientSite editclient = new tblClientSite();
            editclient = clientBL.GetClientSite(ClientID.Value);

            return Json(new { success = true, ClientTimeZone = editclient.TimeZone == null ? "" : editclient.TimeZone.Name, ClientStatus = editclient.Status == null ? "" : editclient.Status.Name }, JsonRequestBehavior.AllowGet);
        }

        //Get
        public JsonResult DeleteDevices(int? contactID)
        {
            BLClientSiteDevice clientBL = new BLClientSiteDevice();

            int? DeletionStatus = null;
            if (contactID != null && contactID.Value > 0)
            {
                DeletionStatus = clientBL.DelContact(contactID.Value);
            }
            if (DeletionStatus.Value == 0)
            {
                return Json(new { success = true, errDesc = "Device deleted successfully" }, JsonRequestBehavior.AllowGet);

            };

            return Json(new { errors = true, errDesc = "Device deletion failed" }, JsonRequestBehavior.AllowGet);
        }

        //Post
        [HttpPost]        
        public JsonResult Device(tblClientSiteDevice model)
        {
            try
            {
                BLClientSiteDevice clientSiteDeviceBL = new BLClientSiteDevice();
                model.CustomerID = SessionHelper.UserSession.CustomerID;
                clientSiteDeviceBL.SaveClientSiteDevice(model);
            }
            catch
            {
                ModelState.AddModelError("", "Error occurred while saving device information.");
                return Json(new { errors = KeyValue.GetErrorsFromModelState(ViewData) });
            }

            return Json(new { success = true, ClientID = model.ClientID });
        }

        #region -- Private Methods --

        private void BindLookups()
        {
            ViewBag.OptionTypeBag = LookUpData.GetOptionType();
            ViewBag.DeviceType1= LookUpData.GetDeviceType();
        }

        #endregion -- Private Methods --

    }
}
