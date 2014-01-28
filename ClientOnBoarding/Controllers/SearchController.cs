using ClientOnBoarding.BAL;
using ClientOnBoarding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;


namespace ClientOnBoarding.Controllers
{
    [AuthorizeAdminAndStaffAttribute]
    public class SearchController : Controller
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
        public ActionResult SearchResultData(jQueryDataTableParamModel param)
        {
            int totalRecords = 0;

            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            var sortDirection = Request["sSortDir_0"]; // asc or desc

            string CustomerName = Request["CustomerName"].ToString();
            string ClientName = Request["ClientName"].ToString();
            string DeviceDesc = Request["DeviceDesc"].ToString();

            BLSearch Blsearch = new BLSearch();
            List<SearchResult> lstSearchResult = Blsearch.GetSearchResult(CustomerName, ClientName, DeviceDesc, param.iDisplayStart, param.iDisplayLength, sortColumnIndex, sortDirection, param.sSearch, ref totalRecords);

            var result = from c in lstSearchResult
                         select new[] { c.CustomerName, c.ClientSiteName, c.ClientSiteStatus, c.DeviceName, c.DeviceDesc, 
                                        c.AccessPolicy, c.AccessPolicyID.ToString(), c.MaintenancePolicy, c.MaintenancePolicyID.ToString(),
                                        c.PatchingPolicy, c.PatchingPolicyID.ToString(),  c.RMMTool, c.RMMToolID.ToString(),c.ClientSiteDeviceID.ToString() };
            //c.AntiVirusPolicy, c.AntiVirusPolicyID.ToString(),
            //                            c.BackUpPolicy, c.BackUpPolicyID.ToString(),

            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = result
            }, JsonRequestBehavior.AllowGet);
        }

        //Get
        public ActionResult SearchResult()
        {
            if (Request.IsAjaxRequest())
                return PartialView("SearchGrid");
            else
                return View("SearchGrid");
        }

        // GET        
        public ActionResult MaintenancePolicy(int? MaintenancePolicyID)
        {
            BLCustomer MaintenanceBL = new BLCustomer();
            tblMaintenancePolicy tblmaintenance = new tblMaintenancePolicy();
            tblmaintenance = MaintenanceBL.GetMaintenance(MaintenancePolicyID.Value);

            if (MaintenancePolicyID.Value == 0)
                tblmaintenance.ScheduledStartDate = DateTime.Now;

            List<KeyValue> lstKeyVal = new List<KeyValue>();
            lstKeyVal.Add(new KeyValue() { Key = "Activity Name", Value = tblmaintenance.ActivityName });
            lstKeyVal.Add(new KeyValue() { Key = "Define Regular Schedule", Value = tblmaintenance.ScheduleType.Name});
            lstKeyVal.Add(new KeyValue() { Key = "Define Week Day (for weekly schedule)", Value = tblmaintenance.WeekOfDays.Name });
        //  lstKeyVal.Add(new KeyValue() { Key = "Define Month of Day (for Monthly Schedule)", Value = tblmaintenance.MonthOfDays.Name });
            lstKeyVal.Add(new KeyValue()
            {
                Key = "Scheduled Time",
                Value = tblmaintenance.ScheduledStartTime + " - " +
                        tblmaintenance.ScheduledEndTime + " " +
                        tblmaintenance.TimeZone.Name
            });
            lstKeyVal.Add(new KeyValue() { Key = "Start Date", Value = tblmaintenance.ScheduledStartDate.ToString("MM/dd/yyyy") });

            if (Request.IsAjaxRequest())
                return PartialView("Detail", lstKeyVal);
            else
                return View("Detail", lstKeyVal);

        }

        // GET        
        public ActionResult AntiVirusPolicy(int? AntiVirusID)
        {
            BLCustomer AntiVirusBL = new BLCustomer();
            tblAntiVirusPolicy tblantivirus = new tblAntiVirusPolicy();
            tblantivirus = AntiVirusBL.GetAntiVirus(AntiVirusID.Value);

            List<KeyValue> lstKeyVal = new List<KeyValue>();
            lstKeyVal.Add(new KeyValue() { Key = "Anti-Virus Policy Name", Value = tblantivirus.PolicyName });
            lstKeyVal.Add(new KeyValue() { Key = "Av Product Name", Value = tblantivirus.ProductName });
            lstKeyVal.Add(new KeyValue()
            {
                Key = "Patching Time",
                Value = tblantivirus.PatchingTime + " " + tblantivirus.PatchingTimeZone.ID
            });
            lstKeyVal.Add(new KeyValue() { Key = "Define Week Day (for weekly schedule)", Value = tblantivirus.WeekOfDay.Name });
            lstKeyVal.Add(new KeyValue() { Key = "Define Month of Day (for Monthly Schedule) ", Value = tblantivirus.MonthOfDay.Name });
            lstKeyVal.Add(new KeyValue() { Key = "Anti-Virus Scan Exclusion", Value = tblantivirus.PolicyName });
            lstKeyVal.Add(new KeyValue() { Key = "Exclude files with an extension", Value = tblantivirus.ExcludedFilesExtension });
            lstKeyVal.Add(new KeyValue() { Key = "Exclude File Types", Value = tblantivirus.ExcludedFileTypes });
            lstKeyVal.Add(new KeyValue() { Key = "Exclude File Paths", Value = tblantivirus.ExcludedFilePaths });

            if (Request.IsAjaxRequest())
                return PartialView("Detail", lstKeyVal);
            else
                return View("Detail", lstKeyVal);
        }

        // GET        
        public ActionResult AccessPolicy(int? AccessPolicyID)
        {
            BLCustomer customerBL = new BLCustomer();
            tblAccessPolicy Accesspoly = customerBL.GetAccessPolicyByID(AccessPolicyID.Value);
            List<KeyValue> lstKeyVal = new List<KeyValue>();
            //AccessPolicyIDWR
            if (Accesspoly.AccessPolicyIDWR > 0)
            {
                lstKeyVal.Add(new KeyValue() { Key = "Remote control permission allowed?", Value = Accesspoly.RemoteControlPermissionWR.Name });
                lstKeyVal.Add(new KeyValue()
                {
                    Key = "Allowed Remote Access Start Time",
                    Value = Accesspoly.RemoteAccessStartTimeWR + " - " + Accesspoly.RemoteAccessEndTimeWR + " " + Accesspoly.RemoteAccessTimeZoneWR.Name
                });
                lstKeyVal.Add(new KeyValue() { Key = "Reboot Permission", Value = Accesspoly.RebootPermissionWR.Name });
                lstKeyVal.Add(new KeyValue()
                {
                    Key = "Allowed Reboot Window",
                    Value = Accesspoly.RebootWindowStartTimeWR + " - " + Accesspoly.RebootWindowEndTimeWR + " " + Accesspoly.RebootTimeZoneWR.Name
                });
            }
            //AccessPolicyIDSR
            else if (Accesspoly.AccessPolicyIDSR > 0)
            {
                lstKeyVal.Add(new KeyValue() { Key = "Remote control permission allowed?", Value = Accesspoly.RemoteControlPermissionSR.Name });
                lstKeyVal.Add(new KeyValue()
                {
                    Key = "Allowed Remote Access Start Time",
                    Value = Accesspoly.RemoteAccessStartTimeSR + " - " + Accesspoly.RemoteAccessEndTimeSR + " " + Accesspoly.RemoteAccessTimeZoneSR.Name
                });
                lstKeyVal.Add(new KeyValue() { Key = "Reboot Permission", Value = Accesspoly.RebootPermissionSR.Name });
                lstKeyVal.Add(new KeyValue()
                {
                    Key = "Allowed Reboot Window",
                    Value = Accesspoly.RebootWindowStartTimeSR + " - " + Accesspoly.RebootWindowEndTimeSR + " " + Accesspoly.RebootTimeZoneSR.Name
                });
            }
            //AccessPolicyIDND
            else if (Accesspoly.AccessPolicyIDND > 0)
            {
                lstKeyVal.Add(new KeyValue() { Key = "Remote control permission allowed?", Value = Accesspoly.RemoteControlPermissionND.Name });
                lstKeyVal.Add(new KeyValue()
                {
                    Key = "Allowed Remote Access Start Time",
                    Value = Accesspoly.RemoteAccessStartTimeND + " - " + Accesspoly.RemoteAccessEndTimeND + " " + Accesspoly.RemoteAccessTimeZoneND.Name
                });
                lstKeyVal.Add(new KeyValue() { Key = "Reboot Permission", Value = Accesspoly.RebootPermissionND.Name });
                lstKeyVal.Add(new KeyValue()
                {
                    Key = "Allowed Reboot Window",
                    Value = Accesspoly.RebootWindowStartTimeND + " - " + Accesspoly.RebootWindowEndTimeND + " " + Accesspoly.RebootTimeZoneND.Name
                });
            }
            if (Request.IsAjaxRequest())
                return PartialView("Detail", lstKeyVal);
            else
                return View("Detail", lstKeyVal);
        }
        //GET
        public ActionResult ManageDevice(int? DeviceID)
        {
            BLClientSiteDevice customerBL = new BLClientSiteDevice();
            tblClientSiteDevice tblclientdevice = customerBL.GetClientSiteDevice(DeviceID.Value,SessionHelper.UserSession.CustomerID);
            List<KeyValue> lstKeyVal = new List<KeyValue>();
         
            //ToolTypeRT

            lstKeyVal.Add(new KeyValue() { Key = "User Name", Value = tblclientdevice.UserName });
            lstKeyVal.Add(new KeyValue() { Key = "Password", Value = tblclientdevice.Password });
            lstKeyVal.Add(new KeyValue() { Key = "Misc Information", Value = tblclientdevice.MiscInfo });
                    
            if (Request.IsAjaxRequest())
                return PartialView("Detail", lstKeyVal);
            else
                return View("Detail", lstKeyVal);
        }

        // GET        
        public ActionResult PatchingPolicy(int? PatchingPolicyID)
        {
            BLCustomer customerBL = new BLCustomer();
            tblPatchingPolicy Patchpoly = customerBL.GetPatchingPolicyByID(PatchingPolicyID.Value);

            List<KeyValue> lstKeyVal = new List<KeyValue>();
            //PatchingPolicyIDWR
            if (Patchpoly.PatchingPolicyIDWR > 0)
            {
                lstKeyVal.Add(new KeyValue() { Key = "Patching Policy Name", Value = Patchpoly.PolicyNameWR });
                lstKeyVal.Add(new KeyValue()
                {
                    Key = "Patching Window ",
                    Value = Patchpoly.WindowStartTimeWR + " - " + Patchpoly.WindowEndTimeWR + " " + Patchpoly.WindowTimeZoneWR.Name
                });
              //  lstKeyVal.Add(new KeyValue() { Key = "Define Patching Schedule", Value = Patchpoly.DefineScheduleTypeWR.Name });
                lstKeyVal.Add(new KeyValue() { Key = "Define Week Day (for weekly schedule) ", Value = Patchpoly.WeekOfDayWR.Name });
             //   lstKeyVal.Add(new KeyValue() { Key = "Define Month of Day (for Monthly Schedule)", Value = Patchpoly.MonthOfDayWR.Name });
                lstKeyVal.Add(new KeyValue() { Key = "BReboot Permission ", Value = Patchpoly.RebootPermissionWR.Name });
                lstKeyVal.Add(new KeyValue()
                {
                    Key = "Allowed Reboot Window",
                    Value = Patchpoly.RebootWindowStartTimeWR + " - " + Patchpoly.RebootWindowEndTimeWR + "  " + Patchpoly.RebootWindowTimeZoneWR.Name
                });
            }
            //PatchingPolicyIDSR
            else if (Patchpoly.PatchingPolicyIDSR > 0)
            {
                lstKeyVal.Add(new KeyValue() { Key = "Patching Policy Name", Value = Patchpoly.PolicyNameSR });
                lstKeyVal.Add(new KeyValue()
                {
                    Key = "Patching Window  ",
                    Value = Patchpoly.WindowStartTimeSR + " - " + Patchpoly.WindowEndTimeSR + "  " + Patchpoly.WindowTimeZoneSR.Name
                });
               // lstKeyVal.Add(new KeyValue() { Key = "Define Patching Schedule ", Value = Patchpoly.DefineScheduleTypeSR.Name });
                lstKeyVal.Add(new KeyValue() { Key = "Define Week Day (for weekly schedule) ", Value = Patchpoly.WeekOfDaySR.Name });
                //lstKeyVal.Add(new KeyValue() { Key = "Define Month of Day (for Monthly Schedule) ", Value = Patchpoly.MonthOfDaySR.Name });
                lstKeyVal.Add(new KeyValue() { Key = "Reboot Permission ", Value = Patchpoly.RebootPermissionSR.Name });
                lstKeyVal.Add(new KeyValue()
                {
                    Key = "Allowed Reboot Window",
                    Value = Patchpoly.RebootWindowStartTimeSR.ToString() + " - " + Patchpoly.RebootWindowEndTimeSR.ToString() + "  " +Patchpoly.RebootWindowTimeZoneSR.Name
                });
            }
            //PatchingPolicyIDDND
            else if (Patchpoly.PatchingPolicyIDDND > 0)
            {
                lstKeyVal.Add(new KeyValue() { Key = "Patching Policy Name ", Value = Patchpoly.PolicyNameND });
                lstKeyVal.Add(new KeyValue()
                {
                    Key = "Patching Window ",
                    Value = Patchpoly.WindowStartTimeND + " - " + Patchpoly.WindowEndTimeND + "  " + Patchpoly.WindowTimeZoneND.Name
                });
               // lstKeyVal.Add(new KeyValue() { Key = "Define Patching Schedule   ", Value = Patchpoly.DefineScheduleTypeND.Name });
                lstKeyVal.Add(new KeyValue() { Key = "Define Week Day (for weekly schedule) ", Value = Patchpoly.WeekOfDayND.Name });
                //lstKeyVal.Add(new KeyValue() { Key = "Define Month of Day (for Monthly Schedule) ", Value = Patchpoly.MonthOfDayND.Name });
                lstKeyVal.Add(new KeyValue() { Key = "Reboot Permission ", Value = Patchpoly.RebootPermissionND.Name });
                lstKeyVal.Add(new KeyValue()
                {
                    Key = "Allowed Reboot Window ",
                    Value = Patchpoly.RebootWindowStartTimeND + " - " + Patchpoly.RebootWindowEndTimeND + "  " + Patchpoly.RebootWindowTimeZoneND.Name
                });
            }

            if (Request.IsAjaxRequest())
                return PartialView("Detail", lstKeyVal);
            else
                return View("Detail", lstKeyVal);
        }

        // GET        
        public ActionResult BackUpPolicy(int? BackUpPolicyID)
        {
            BLCustomer BackupBL = new BLCustomer();

            tblBackUpPolicy tblbackup = new tblBackUpPolicy();
            tblbackup = BackupBL.GetBackUp(BackUpPolicyID.Value);

            List<KeyValue> lstKeyVal = new List<KeyValue>();
            lstKeyVal.Add(new KeyValue() { Key = "Back Up Policy Name", Value = tblbackup.PolicyName });
            lstKeyVal.Add(new KeyValue() { Key = "Back Up Product Name", Value = tblbackup.ProductName });
            lstKeyVal.Add(new KeyValue() { Key = "Volume Back Up Image Location", Value = tblbackup.VolumeLocation });
            lstKeyVal.Add(new KeyValue() { Key = "Folder Back Up Image Location ", Value = tblbackup.FolderLocation });
            lstKeyVal.Add(new KeyValue()
            {
                Key = "BackUp Schedule",
                Value = tblbackup.ScheduleTime + " " + tblbackup.ScheduleTimeZone.Name
            });
            lstKeyVal.Add(new KeyValue() { Key = "BackUp Set Details", Value = tblbackup.BackUpSetDetails });
            lstKeyVal.Add(new KeyValue() { Key = "Differentials Every-Days", Value = tblbackup.DifferentialEveryDay.ToString() });
            lstKeyVal.Add(new KeyValue() { Key = "Number of BackUp Sets", Value = tblbackup.PreviousBackupSaved.ToString() });
            lstKeyVal.Add(new KeyValue() { Key = "Full BackUp Every-Day", Value = tblbackup.FullBackUpEveryDay.ToString() });
            lstKeyVal.Add(new KeyValue() { Key = "Domain Name", Value = tblbackup.DomainName });
            lstKeyVal.Add(new KeyValue() { Key = "User Name", Value = tblbackup.UserName });
            lstKeyVal.Add(new KeyValue() { Key = "Password", Value = tblbackup.Password });

            if (Request.IsAjaxRequest())
                return PartialView("Detail", lstKeyVal);
            else
                return View("Detail", lstKeyVal);
        }

        // GET
        [Authorize]
        public ActionResult ToolInformation(int? ToolID)
        {

            BLCustomer customerBL = new BLCustomer();
            tbltoolInfomation tbltoolinfo = customerBL.GetToolInfoByID(ToolID.Value);

            List<KeyValue> lstKeyVal = new List<KeyValue>();
            //ToolTypeRT
            if (tbltoolinfo.ToolTypeRT > 0)
            {
                lstKeyVal.Add(new KeyValue() { Key = "RMM Tool Name", Value = tbltoolinfo.RMMTool });
                lstKeyVal.Add(new KeyValue() { Key = "RMM Tool UserID", Value = tbltoolinfo.RMMToolUserName });
                lstKeyVal.Add(new KeyValue() { Key = "RMM Tool URL", Value = tbltoolinfo.RMMUrl });
                lstKeyVal.Add(new KeyValue() { Key = "RMM Tool Password", Value = tbltoolinfo.RMMToolPassword });
            }
            //ToolTypePT
            else if (tbltoolinfo.ToolTypePT > 0)
            {
                lstKeyVal.Add(new KeyValue() { Key = "Ticketing/PSA Tool", Value = tbltoolinfo.PSATool });
                lstKeyVal.Add(new KeyValue() { Key = "PSA Tool UserID", Value = tbltoolinfo.PSAToolUserName });
                lstKeyVal.Add(new KeyValue() { Key = "PSA Tool URL", Value = tbltoolinfo.PSAUrl });
                lstKeyVal.Add(new KeyValue() { Key = "PSA Tool Password", Value = tbltoolinfo.PSAToolPassword });
            }
            if (Request.IsAjaxRequest())
                return PartialView("Detail", lstKeyVal);
            else
                return View("Detail", lstKeyVal);
        }
    }
}