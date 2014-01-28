using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClientOnBoarding.Models
{
    public class tblClientSiteDevice
    {        
        public int ClientID { get; set; }
        public int CustomerID { get; set; }
        public DeviceType DeviceType { get; set; }
        public int DeviceID { get; set; }
        [Required(ErrorMessage = "Please Enter Device ID")]
        public string DeviceIDFromRMMTool { get; set; }
        public string DeviceDescription { get; set; }
        [Required(ErrorMessage = "Please Enter UserName ")]
        public string UserName { get; set; }
         [Required(ErrorMessage = "Please Enter Password ")]
        public string Password { get; set; }
        public string MiscInfo { get; set; }

        public PolicyType IsAccessPolicy { get; set; }
        public PolicyType IsMaintenancePolicy { get; set; }
        public PolicyType IsPatchingPolicy { get; set; }
        public PolicyType IsAntiVirus { get; set; }
        public PolicyType IsRMMTool { get; set; }
        public PolicyType IsBackUpPolicy { get; set; }        
        public TimeZoneFX TimeZone { get; set; }
    }  
}