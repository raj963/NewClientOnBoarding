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
    public class SetupCustomerController : Controller
    {
        #region -- Index --

        //GET        
        public ActionResult Index()
        {
            BLCustomer customerBL = new BLCustomer();
            tblCustomerDetails customer = customerBL.GetCustomer(SessionHelper.UserSession.CustomerID);

            ViewBag.TimeZones = LookUpData.GetTimeZoneCI();
            ViewBag.NOCCommunicationBy = LookUpData.GetNOCCommunicationBy();

            if (Request.IsAjaxRequest())
                return PartialView(customer);
            else
                return View(customer);
        }

        #endregion -- Index --

        #region -- Customer Information --

        // GET        
        public ActionResult CustomerInformation(int? CustomerID)
        {
            BLCustomer customerBL = new BLCustomer();
            tblCustomerDetails customer = customerBL.GetCustomer(CustomerID.Value);

            ViewBag.TimeZones = LookUpData.GetTimeZoneCI();
            ViewBag.NOCCommunicationBy = LookUpData.GetNOCCommunicationBy();

            if (Request.IsAjaxRequest())
                return PartialView(customer);
            else
                return View(customer);
        }

        [HttpPost]        
        public JsonResult CustomerInformation(tblCustomerDetails customerDetails)
        {
            try
            {
                BLCustomer customerBL = new BLCustomer();
                customerDetails.CustomerID = SessionHelper.UserSession.CustomerID;
                customerBL.SaveCustomer(customerDetails);
            }
            catch
            {
                ModelState.AddModelError("", "Error occurred durung Customer Information save.");
                return Json(new { errors = KeyValue.GetErrorsFromModelState(ViewData) });
            }

            return Json(new { success = true, CustomerID = SessionHelper.UserSession.CustomerID, Tab = "CI" });
        }


        #endregion -- Customer Information --

        #region -- Tool Information --

        // GET        
        public ActionResult ToolInformation(int? CustomerID)
        {

            BLCustomer customerBL = new BLCustomer();
            tbltoolInfomation tbltoolinfo = customerBL.GetToolInfo(CustomerID.Value);
            if (Request.IsAjaxRequest())
                return PartialView(tbltoolinfo);
            else
                return View(tbltoolinfo);
        }
                
        [HttpPost]        
        public JsonResult ToolInformation(tbltoolInfomation toolInfo)
        {
            try
            {
                BLCustomer customerBL = new BLCustomer();
                toolInfo.CustomerID = SessionHelper.UserSession.CustomerID;
                customerBL.SaveToolInfo(toolInfo);
            }
            catch
            {
                ModelState.AddModelError("", "Error occurred durung Tool Information save.");
                return Json(new { errors = KeyValue.GetErrorsFromModelState(ViewData) });
            }

            return Json(new { success = true, Tab = "TI" });
        }

        #endregion -- Tool Information --

        #region -- AccessPolicy --

        // GET        
        public ActionResult AccessPolicy(int? CustomerID)
        {
            BLCustomer customerBL = new BLCustomer();

            tblAccessPolicy Accesspoly = customerBL.GetAccessPolicy(CustomerID.Value);

            ViewBag.SearchTypes = LookUpData.GetPolicyType();
            ViewBag.TimeZones = LookUpData.GetTimeZone();
            ViewBag.OptionTypeBag = LookUpData.GetOptionType();
            ViewBag.time = LookUpData.GetFXTime();
            ViewBag.ReadOnly = false;

            if (Request.IsAjaxRequest())
                return PartialView(Accesspoly);
            else
                return View(Accesspoly);
        }
                
        [HttpPost]        
        public JsonResult AccessPolicy(tblAccessPolicy AccessPolicy)
        {
            try
            {
                BLCustomer customerBL = new BLCustomer();
                AccessPolicy.CustomerID = SessionHelper.UserSession.CustomerID;
                customerBL.SaveAccessPolicy(AccessPolicy);
            }
            catch
            {
                ModelState.AddModelError("", "Error occurred durung Customer Information save.");
                return Json(new { errors = KeyValue.GetErrorsFromModelState(ViewData) });
            }
            return Json(new { success = true, Tab = "AP" });
        }

        #endregion -- AccessPolicy --

        #region -- Maintenance Policy --

        // GET        
        public ActionResult MPIndex(int? page)
        {
            if (Request.IsAjaxRequest())
                return PartialView();
            else
                return View();
        }

        // GET        
        public ActionResult MaintenancePolicy(int? MaintenancePolicyID)
        {

            BLCustomer MaintenanceBL = new BLCustomer();
            tblMaintenancePolicy tblmaintenance = new tblMaintenancePolicy();
            tblmaintenance = MaintenanceBL.GetMaintenance(MaintenancePolicyID.Value);
            if (MaintenancePolicyID.Value == 0)
                tblmaintenance.ScheduledStartDate = DateTime.Now;
            ViewBag.WeekOfMonths = LookUpData.GetWeekOfMonth();
            ViewBag.WeekOfDay = LookUpData.GetWeekOfDay();
            ViewBag.time = LookUpData.GetFXTime();
            ViewBag.MonthOfDay = LookUpData.GetMonthOfDay();
            ViewBag.TimeZones = LookUpData.GetTimeZone();
            ViewBag.ScheduleTypeM = LookUpData.GetScheduleType();

            if (Request.IsAjaxRequest())
                return PartialView("MaintenancePolicy", tblmaintenance);
            else
                return View("MaintenancePolicy", tblmaintenance);
        }

        // GET        
        public ActionResult MaintenancePolicies(jQueryDataTableParamModel param)
        {
            int totalRecords = 0;

            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            var sortDirection = Request["sSortDir_0"]; // asc or desc

            BLCustomer MaintenanceBL = new BLCustomer();
            List<tblMaintenancePolicy> lstMaintenancePolity = MaintenanceBL.GetMPIndex(SessionHelper.UserSession.CustomerID, param.iDisplayStart, param.iDisplayLength, sortColumnIndex, sortDirection, param.sSearch, ref totalRecords);

            var result = from m in lstMaintenancePolity
                         let ScheduleTypeName = m.ScheduleType.Name
                         let WeekOfDaysName = m.WeekOfDays.Name
                       //  let MonthOfDaysName = m.MonthOfDays.Name
                         let ScheduleDetail = m.ScheduledStartTime + " - " + m.ScheduledEndTime + " " + m.TimeZone.Name
                         select new[] { m.ActivityName, ScheduleTypeName, WeekOfDaysName, ScheduleDetail, m.ScheduledStartDate.ToShortDateString(), m.MaintenancePolicyID.ToString() };

            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = result
            }, JsonRequestBehavior.AllowGet);
        }
                
        [HttpPost]        
        public JsonResult MaintenancePolicy(tblMaintenancePolicy Manintenance)
        {
            try
            {
                BLCustomer customerBL = new BLCustomer();
                Manintenance.CustomerID = SessionHelper.UserSession.CustomerID;
                customerBL.SaveMaintenancePolicy(Manintenance);
            }
            catch
            {
                ModelState.AddModelError("", "Error occurred durung Customer Information save.");
                return Json(new { errors = KeyValue.GetErrorsFromModelState(ViewData) });
            }
            return Json(new { success = true, Tab = "MP" });
        }

        #endregion -- Maintenance Policy --

        #region -- Patching Policy --

        // GET        
        public ActionResult PatchingPolicy(int? CustomerID)
        {
            BLCustomer customerBL = new BLCustomer();
            tblPatchingPolicy Patchpoly = customerBL.GetPatchingPolicy(CustomerID.Value);
            ViewBag.SearchTypes = LookUpData.GetPolicyType();
            ViewBag.WeekOfMonth = LookUpData.GetWeekOfMonth();
            ViewBag.time = LookUpData.GetFXTime();
            ViewBag.MonthOfDay = LookUpData.GetMonthOfDay();
            ViewBag.TimeZones = LookUpData.GetTimeZone();
            ViewBag.Schedule = LookUpData.GetScheduleType();
            ViewBag.OptionTypeBag = LookUpData.GetOptionType();

            tblPatchingPolicy patchingPolicy = new tblPatchingPolicy();

            if (Request.IsAjaxRequest())
                return PartialView(Patchpoly);
            else
                return View(Patchpoly);

        }

        [HttpPost]        
        public JsonResult PatchingPolicy(tblPatchingPolicy patching)
        {
            try
            {
                BLCustomer customerBL = new BLCustomer();
                patching.CustomerID = SessionHelper.UserSession.CustomerID;
                customerBL.SavePatchingPolicy(patching);
            }
            catch
            {
                ModelState.AddModelError("", "Error occurred durung Customer Information save.");
                return Json(new { errors = KeyValue.GetErrorsFromModelState(ViewData) });
            }


            return Json(new { success = true, Tab = "PP" });
        }

        #endregion -- PatchingPolicy --

        #region -- AntiVirusPolicy --
        
        // GET
        public ActionResult AVPIndex()
        {
            if (Request.IsAjaxRequest())
                return PartialView();
            else
                return View();
        }

        // GET
        public ActionResult AntiVirusPolicy(int? AntiVirusID)
        {
            BLCustomer AntiVirusBL = new BLCustomer();
            tblAntiVirusPolicy tblantivirus = new tblAntiVirusPolicy();
            tblantivirus = AntiVirusBL.GetAntiVirus(AntiVirusID.Value);

            ViewBag.staus = LookUpData.GetSetUpStatus();
            ViewBag.ScheduleAV = LookUpData.GetScheduleType();
            ViewBag.SearchTypes = LookUpData.GetPolicyType();


            ViewBag.WeekOfDayAV = LookUpData.GetWeekOfDay();
            ViewBag.time = LookUpData.GetFXTime();
            ViewBag.MonthOfDayAV = LookUpData.GetMonthOfDay();
            ViewBag.TimeZones = LookUpData.GetTimeZone();
            ViewBag.ScheduleType = LookUpData.GetScheduleType();

            if (Request.IsAjaxRequest())
                return PartialView(tblantivirus);
            else
                return View(tblantivirus);
        }

        // GET
        public ActionResult AntiVirusPolicies(jQueryDataTableParamModel param)
        {
            int totalRecords = 0;

            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            var sortDirection = Request["sSortDir_0"]; // asc or desc

            BLCustomer MaintenanceBL = new BLCustomer();
            List<tblAntiVirusPolicy> lstAntiVirusPolicy = MaintenanceBL.GetAVPIndex(SessionHelper.UserSession.CustomerID, param.iDisplayStart, param.iDisplayLength, sortColumnIndex, sortDirection, param.sSearch, ref totalRecords);

            var result = from a in lstAntiVirusPolicy
                         let PatchingTime = a.PatchingTime + " - " + a.PatchingTimeZone.Name
                         let WeekOfDaysName = a.WeekOfDay.Name
                         let MonthOfDaysName = a.MonthOfDay.Name
                         select new[] { a.PolicyName, a.ProductName, PatchingTime, WeekOfDaysName, MonthOfDaysName, a.ExcludedFilesExtension, a.ExcludedFileTypes, a.ExcludedFilePaths, a.AntiVirusID.ToString() };

            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = result
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]        
        public JsonResult AntiVirusPolicy(tblAntiVirusPolicy AntiVirus)
        {
            try
            {
                BLCustomer customerBL = new BLCustomer();
                AntiVirus.CustomerID = SessionHelper.UserSession.CustomerID;
                customerBL.SaveAntiVirusPolicy(AntiVirus);
            }
            catch
            {
                ModelState.AddModelError("", "Error occurred durung Customer Information save.");
                return Json(new { errors = KeyValue.GetErrorsFromModelState(ViewData) });
            }

            return Json(new { success = true, Tab = "VP" });
        }

        #endregion -- AntiVirusPolicy --

        #region -- BackUpPolicy --
        
        // GET
        public ActionResult BPIndex(int? page)
        {
            if (Request.IsAjaxRequest())
                return PartialView();
            else
                return View();
        }

        // GET
        public ActionResult BackUpPolicy(int? BackUpPolicyID)
        {
            BLCustomer BackupBL = new BLCustomer();

            tblBackUpPolicy tblbackup = new tblBackUpPolicy();
            tblbackup = BackupBL.GetBackUp(BackUpPolicyID.Value);

            ViewBag.Schedule = LookUpData.GetScheduleType();
            ViewBag.time = LookUpData.GetFXTime();
            ViewBag.TimeZones = LookUpData.GetTimeZone();

            if (Request.IsAjaxRequest())
                return PartialView(tblbackup);
            else
                return View(tblbackup);
        }

        //Get
        public ActionResult DeletePolicy(int? PolicyID, string PolicyType)
        {
            BLCustomer customerBL = new BLCustomer();
            StringBuilder str = customerBL.DeletePolicy(PolicyID.Value, PolicyType);
            if (str.Length > 0)
                return Json(new { success = false, resultmsg = str.ToString() }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { success = true, resultmsg = "Policy deleted successfully" }, JsonRequestBehavior.AllowGet);
        }

        // GET
        public ActionResult BackUpPolicies(jQueryDataTableParamModel param)
        {
            int totalRecords = 0;

            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            var sortDirection = Request["sSortDir_0"]; // asc or desc

            BLCustomer BackupBL = new BLCustomer();
            List<tblBackUpPolicy> lstBackUpPolicies = BackupBL.GetBPIndex(SessionHelper.UserSession.CustomerID, param.iDisplayStart, param.iDisplayLength, sortColumnIndex, sortDirection, param.sSearch, ref totalRecords);

            var result = from a in lstBackUpPolicies
                         let BackUpSchedule = a.ScheduleTime + " - " + a.ScheduleTimeZone.Name
                         select new[] { a.PolicyName, a.ProductName, a.VolumeLocation, a.FolderLocation, BackUpSchedule, a.BackUpSetDetails, 
                             a.DifferentialEveryDay.ToString(), a.PreviousBackupSaved.ToString(), a.FullBackUpEveryDay.ToString(),
                             a.BackUpPolicyID.ToString() };

            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = result
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult BackUpPolicy(tblBackUpPolicy tblbackup)
        {
            try
            {
                BLCustomer customerBL = new BLCustomer();
                tblbackup.CustomerID = SessionHelper.UserSession.CustomerID;
                customerBL.SaveBackUpPolicy(tblbackup);
            }
            catch
            {
                ModelState.AddModelError("", "Error occurred durung Customer Information save.");
                return Json(new { errors = KeyValue.GetErrorsFromModelState(ViewData) });
            }

            return Json(new { success = true, Tab = "BP" });
        }

        #endregion -- BackUpPolicy --
    }
}
